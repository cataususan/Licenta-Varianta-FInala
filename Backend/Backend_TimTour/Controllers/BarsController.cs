using Backend_TimTour.Interfaces.LocationInterfaces;
using Backend_TimTour.Interfaces.Parsers;
using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.LocationModels.SimplifiedLocations;
using Backend_TimTour.Models.RequestModels;
using Backend_TimTour.Models.ResultsModels;
using Backend_TimTour.Parsers;
using Backend_TimTour.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_TimTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarsController : Controller
    {
        private readonly IBarRepository _barRepository;
        private readonly IBarParser _barParser;

        public BarsController(IBarRepository barRepository, IBarParser barParser)
        {
            _barRepository = barRepository;
            _barParser = barParser;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bar>>> GetBarsAsync()
        {
            try
            {
                var bars = await _barRepository.GetAllBarsAsync();
                IEnumerable<RequestBar> transformedBars = bars
                    .Select(_barParser.ParseBarToDatabaseObject)
                    .ToList();
                return Ok(transformedBars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
        [HttpGet("simplifiedBars")]
        public async Task<ActionResult<IEnumerable<SimplifiedBar>>> GetSimplifiedBarsAsync()
        {
            try
            {
                var bars = await _barRepository.GetAllBarsAsync();
                return Ok(bars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
        [HttpGet("pendingBar")]
        public async Task<ActionResult<IEnumerable<SimplifiedBar>>> GetPendingBarsAsync()
        {
            try
            {
                var bars = await _barRepository.GetPendingBarsAsync();
                return Ok(bars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
        [HttpPost("addBar")]
        public async Task<ActionResult<RepositoryResult>> InsertBarAsync([FromBody] AddBarRequestModel Request)
        {
            var barToParse = Request.bar;
            try
            {
                var (barToInsert, result) = await _barParser.ParseRequestToBar(barToParse);
                if (result)
                {
                    barToInsert.Status = "pending";
                    var insertionResult = await _barRepository.AddBarAsync(barToInsert);
                    return Ok(new { result = insertionResult.ToString() });
                }
                else
                {
                    return StatusCode(500, new { result = "Internal server error: " + "Parsing Failed" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
        [HttpPatch("updateBarStatus")]
        public async Task<ActionResult<RepositoryResult>> UpdateBarStatusAsync([FromBody] UpdateBarStatusRequestModel Request)
        {
            string status = Request.barStatus;
            string barName = Request.barName;

            try
            {
                var insertionResult = await _barRepository.SwitchStatusAsync(status, barName);
                return Ok(new { result = insertionResult.ToString() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
    }
}
