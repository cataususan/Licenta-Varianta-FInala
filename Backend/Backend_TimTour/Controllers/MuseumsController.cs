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
    public class MuseumsController : ControllerBase
    {
        private readonly IMuseumRepository _museumRepository;
        private readonly IMuseumParser _museumParser;
        public MuseumsController(IMuseumRepository museumRepository, IMuseumParser museumParser)
        {
            this._museumRepository = museumRepository;
            this._museumParser = museumParser;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SimplifiedMuseum>>> GetMuseumsAsync()
        {
            try
            {
                var museums = await _museumRepository.GetAllMuseumsAsync();
                IEnumerable<RequestMuseum> transformedMuseums = museums
                    .Select(_museumParser.ParseMuseumToDatabaseObject)
                    .ToList();
                return Ok(transformedMuseums);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
        [HttpGet("simplifiedMuseum")]
        public async Task<ActionResult<IEnumerable<SimplifiedMuseum>>> GetSimplifiedMuseumsAsync()
        {
            try
            {
                var museums = await _museumRepository.GetSimplifiedMuseumsAsync();
                return Ok(museums);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
        [HttpGet("pendingMuseum")]
        public async Task<ActionResult<IEnumerable<SimplifiedMuseum>>> GetPendingMuseumsAsync()
        {
            try
            {
                var museums = await _museumRepository.GetPendingMuseumsAsync();
                return Ok(museums);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
        [HttpPost]
        public async Task<ActionResult<RepositoryResult>> InsertMuseumAsync([FromBody] AddMuseumRequestModel Request)
        {
            var museumToParse = Request.museum;
            try
            {
                var (museumToInsert, result) = await _museumParser.ParseRequestToMuseum(museumToParse);
                if (result)
                {
                    museumToInsert.Status = "pending";
                    var insertionResult = await _museumRepository.AddMuseumAsync(museumToInsert);
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
        [HttpPatch("updateMuseumStatus")]
        public async Task<ActionResult<RepositoryResult>> UpdateMuseumStatusAsync([FromBody] UpdateMuseumStatusRequestModel Request)
        {
            string status = Request.museumStatus;
            string museumName = Request.museumName;

            try
            {
                var insertionResult = await _museumRepository.SwitchStatusAsync(status, museumName);
                return Ok(new { result = insertionResult.ToString() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
    }
}
