using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Jellyfin.Plugin.JellyExplorer.Models;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Controller.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Jellyfin.Plugin.JellyExplorer.Controllers
{
    [Route("JellyExplorer")]
    [Authorize(Policy = "DefaultAuthorization")]
    public class JellyExplorerController : ControllerBase
    {
        private readonly IServerConfigurationManager _config;
        private readonly HttpClient _httpClient;

        public JellyExplorerController(IServerConfigurationManager config)
        {
            _config = config;
            _httpClient = new HttpClient();
        }

        [HttpGet("SearchMovies")]
        public async Task<ActionResult<IEnumerable<MovieSearchResult>>> SearchMovies([FromQuery] string query)
        {
            var config = _config.GetConfiguration<PluginConfiguration>("JellyExplorer");
            var apiKey = config.TmdbApiKey;

            if (string.IsNullOrEmpty(apiKey))
            {
                return BadRequest("TMDB API key is not set");
            }

            var url = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={Uri.EscapeDataString(query)}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Error fetching data from TMDB");
            }

            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);
            var results = new List<MovieSearchResult>();

            foreach (var result in json["results"] ?? new JArray())
            {
                results.Add(new MovieSearchResult
                {
                    Title = result["title"]?.ToString() ?? string.Empty,
                    Overview = result["overview"]?.ToString() ?? string.Empty,
                    ReleaseDate = result["release_date"]?.ToString() ?? string.Empty
                });
            }

            return Ok(results);
        }
    }

    public class MovieSearchResult
    {
        public string Title { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string ReleaseDate { get; set; } = string.Empty;
    }
}