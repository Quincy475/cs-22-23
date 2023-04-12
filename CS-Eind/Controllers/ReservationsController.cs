using CS_Eind.Services;
using Microsoft.AspNetCore.Mvc;

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
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ISearchService _searchService;
        private readonly IMapper _mapper;
        public ReservationsController(ISearchService searchService, IReservationService reservationService, IMapper mapper)
        {
            _searchService = searchService;
            _reservationService = reservationService;
            _mapper = mapper;
        }




        /// <summary>
        /// Method om een nieuwe reservering te maken 
        /// </summary>
        /// <returns></returns>
        ///  <response code="200">Returns duurste locationsprijs </response>
        /// <response code="404">No Locations found in DB</response>
        [HttpPost]
        public async Task<ActionResult<ResResponseDTO>> MakeReservation([FromBody] ResRequestDTO resevation, CancellationToken cancellationToken)
        {
            var resevationRequest = await _reservationService.MakeReservation(resevation, cancellationToken);

            if (resevationRequest == null)
            {
                return BadRequest();
            }

            return Ok(resevationRequest);
        }
    }




}





    