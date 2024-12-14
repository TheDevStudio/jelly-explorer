using System;
using System.Net.Http;
using System.Threading.Tasks;
using Jellyfin.Plugin.JellyExplorer.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Jellyfin.Plugin.JellyExplorer.Services
{
    public class TmdbService
    {
        private readonly ILogger<TmdbService> _logger;
        private readonly HttpClient _httpClient;
        private const string TmdbApiBaseUrl = "https://api.themoviedb.org/3";

        public TmdbService(ILogger<TmdbService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<JObject> SearchMovies(string query)
        {
            var config = Plugin.Instance.Configuration;
            var apiKey = config.TmdbApiKey;

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("TMDB API key is not configured");
            }

            var url = $"{TmdbApiBaseUrl}/search/movie?api_key={apiKey}&query={Uri.EscapeDataString(query)}";

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                return JObject.Parse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching movies");
                throw;
            }
        }
    }
}