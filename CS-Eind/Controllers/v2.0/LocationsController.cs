using AutoMapper;
using CS_Eind.Models.DTO;
using CS_Eind.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CS_Eind.Controllers.v2._0
{
    [Route("api/[controller]")]
    [ApiVersion("2.0")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly IMapper _mapper;

        public LocationsController(ISearchService searchService, IMapper mapper)
        {
            _searchService = searchService;
            _mapper = mapper;
        }


        // GET: api/Locations
        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<IEnumerable<LocationDTOv2>>> GetLocation(CancellationToken cancellationToken)
        {
            var locations = await _searchService.GetAllLocationsV2Async(cancellationToken);
            if (locations == null)
            {
                return NotFound();
            }
            return Ok(locations);
        }



    }
}

