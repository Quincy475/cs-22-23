using AutoMapper;
using CS_Eind.Models;
using CS_Eind.Models.DTO;
using CS_Eind.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;
using System;
using Microsoft.AspNetCore.Authorization;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace CS_Eind.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]

    public class LocationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ISearchService _searchService;
        private readonly IMapper _mapper;
        

        public LocationsController(ISearchService searchService, IReservationService reservationService, IMapper mapper)
        {
            _searchService = searchService;
            _reservationService = reservationService;
            _mapper = mapper;
        }

        /// <summary>
        /// Deze Method geeft alle locaties terug. Deze method heeft zowel een API v1 als v2 versie.
        /// </summary>
        /// <returns>Alle locaties met a.d.h.v. LocationDTO</returns>
        ///  <response code="200">Alle locaties met a.d.h.v. LocationDTO of LocationDTOv2</response>
        /// <response code="404">Geen locaties gevonden die een image hebben voor Landlord of Location</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetLocation(CancellationToken cancellationToken)
        {
            var locations = await _searchService.GetAllLocationsAsync(cancellationToken);
            if (locations == null)
            {
                return NotFound();
            }
            return Ok(locations);
        }


        /// <summary>
        /// Method om alle Locatie objecten op te halen.
        /// </summary>
        /// <returns>Returns alle locaties</returns>
        ///  <response code="200">Returns all locations</response>
        /// <response code="404">No Locations found in DB</response>
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var locations = await _searchService.GetAllLocations(cancellationToken);
            if (locations == null)
            {
                return NotFound();
            }
            return Ok(locations);
        }


        /// <summary>
        /// Method om duurste locatieprijs te laten zien. 
        /// </summary>
        /// <returns>Duurste locatie prijs</returns>
        ///  <response code="200">Returns duurste locationsprijs </response>
        /// <response code="404">No Locations found in DB</response>

        [HttpGet("GetMaxPrice")]
        //[Route]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<object>> GetMaxPrice(CancellationToken cancellationToken)
        {
            var maxPrice = await _searchService.GetMaxPrice(cancellationToken);

            if (maxPrice == null)
            {
                return NotFound();
            }

            return Ok(new { Price = maxPrice });
        }
        /// <summary>
        /// Method om specifieke locatie in detail te bekijken 
        /// </summary>
        /// <returns>details van een specifieke locatie</returns>
        ///  <response code="200">laat specifieke locatie ziens </response>
        /// <response code="404">No Location with this Id found in DB</response>
        [HttpGet("GetDetails/{id}")]
        public async Task<ActionResult<LocationDetailsDTO>> GetLocationDetails(int id, CancellationToken cancellationToken)
        {
            var location = await _searchService.GetLocationById(id, cancellationToken);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }


        /// <summary>
        /// Method om de onbeschikbare data te zien van een specifieke locatie 
        /// </summary>
        /// <returns>Lijst van alle unavailable dates</returns>
        ///  <response code="200">Returns unavailabe dates </response>
        /// <response code="404">No Locations found in DB with this Id</response>
        [HttpGet("UnAvailableDates/{id}")]
        public async Task<ActionResult<UnavailableDatesDTO>> GetUnavailableDates(int id, CancellationToken cancellationToken)
        {
            var dates = await _searchService.GetUnavailableDates(id, cancellationToken);
            if (dates == null)
            {
                return NotFound();
            }
            
            return Ok(dates);
        }


        /// <summary>
        /// Method om locaties te bekijken die gefilterd zijn naar wens. 
        /// </summary>
        /// <returns>Alle locaties die voldoen aan gestelde eisen</returns>
        ///  <response code="200">Returns locations with applied filters </response>
        /// <response code="404">No Locations found in DB</response>

        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<LocationDTOv2>>> Search([FromBody] SearchDTO search, CancellationToken cancellationToken)
        {
            var locations = await _searchService.SearchLocations(search, cancellationToken);

            return Ok(locations);
        }

        


    }



 

}