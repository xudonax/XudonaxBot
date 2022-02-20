using System;
using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XudonaxBot.Bot.Models.Options;
using XudonaxBot.External.Tenor.Models;

namespace XudonaxBot.External.Tenor
{
    public class TenorService : ITenorService
    {
        private const string TenorApiUrl = "https://g.tenor.com/v1/search";
        private const string TenorGifLimit = "20";

        private readonly HttpClient _httpClient;
        private readonly BotOptions _botOptions;
        private readonly ILogger<TenorService> _logger;

        public TenorService(HttpClient httpClient, IOptions<BotOptions> options, ILogger<TenorService> logger)
        {
            _httpClient = httpClient;
            _botOptions = options.Value;
            _logger = logger;;
        }

        public async Task<string?> GetRandomGifFor(string searchText)
        {
            var uriBuilder = new UriBuilder(TenorApiUrl)
            {
                Query = $"?media_filter=minimal&contentfilter=high&key={_botOptions.TenorApiKey}&limit={TenorGifLimit}&q={WebUtility.UrlEncode(searchText)}"
            };

            try
            {
                var result = await _httpClient.GetFromJsonAsync<Wrapper>(uriBuilder.Uri);
                
                if (result == null || result.Results.Count == 0 || result.Results[0].Media.Count == 0) return null;

                var resultIndex = RandomNumberGenerator.GetInt32(0, result.Results.Count);

                return result.Results[resultIndex].Media[0]["gif"].Url;
            }
            catch (HttpRequestException hre)
            {
                _logger.LogError(hre, "Error while calling {Url}", uriBuilder.Uri.ToString());
            }
            catch (NotSupportedException nse)
            {
                _logger.LogError(nse, "Content type not supported while calling {Url}", uriBuilder.Uri.ToString());
            }
            catch (JsonException je)
            {
                _logger.LogError(je, "Unable to parse JSON while calling {Url}", uriBuilder.Uri.ToString());
            }

            return null;
        }
    }
}
