using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Jellyfin.Plugin.JellyExplorer.Services;

namespace Jellyfin.Plugin.JellyExplorer.Api
{
    [ApiController]
    [Authorize(Policy = "DefaultAuthorization")]
    [Route("JellyExplorer")]
    public class JellyExplorerController : ControllerBase
    {
        private readonly TmdbService _tmdbService;

        public JellyExplorerController(TmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> SearchMovies([FromQuery] string query)
        {
            try
            {
                var result = await _tmdbService.SearchMovies(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }
    }
}