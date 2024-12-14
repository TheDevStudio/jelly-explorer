(function () {
    'use strict';

    function saveTmdbApiKey() {
        const tmdbApiKey = document.getElementById('tmdbApiKey').value;
        ApiClient.updatePluginConfiguration('JellyExplorer', {
            TmdbApiKey: tmdbApiKey
        }).then(function () {
            Dashboard.processPluginConfigurationUpdateResult();
        });
    }

    function searchMovies() {
        const query = document.getElementById('searchQuery').value;
        ApiClient.getJSON(ApiClient.getUrl('JellyExplorer/SearchMovies', { query: query }))
            .then(function (results) {
                const movieList = document.getElementById('movieList');
                movieList.innerHTML = '';

                results.forEach(function (movie) {
                    const li = document.createElement('li');
                    li.className = 'movie-item';
                    li.innerHTML = `
                        <div class="movie-title">${movie.title} (${movie.releaseDate.split('-')[0]})</div>
                        <div class="movie-overview">${movie.overview}</div>
                    `;
                    movieList.appendChild(li);
                });
            })
            .catch(function (error) {
                console.error('Error searching movies:', error);
            });
    }

    document.addEventListener('pageshow', function () {
        const form = document.querySelector('#JellyExplorerConfigPage #tmdbForm');
        form.addEventListener('submit', function (e) {
            e.preventDefault();
            saveTmdbApiKey();
        });

        const searchButton = document.getElementById('searchButton');
        searchButton.addEventListener('click', searchMovies);

        ApiClient.getPluginConfiguration('JellyExplorer').then(function (config) {
            document.getElementById('tmdbApiKey').value = config.TmdbApiKey;
        });
    });
})();