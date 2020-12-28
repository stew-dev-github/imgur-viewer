using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace imgur_viewer.Services
{
    public interface IImgurService
    {
        Task<ImgurResponse<Album>> GetAlbumImagesAsync(string hashcode);
        Task<ImgurResponse<Image>> GetImageAsync(string hashcode);

        Models.Type GetTypeForUrl(string url);

        string GetHashcodeForUrl(string url);
    }

    public class ImgurService : IImgurService
    {
        private const string nonMatchingUrlPattern = @"^(?:http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/|www.)?";
        private readonly Dictionary<string, Models.Type> _patternDictionary = new Dictionary<string, Models.Type>
            {
                { nonMatchingUrlPattern + @"imgur\.com/gallery/([A-Za-z0-9]+)", Models.Type.Gallery },
                { nonMatchingUrlPattern + @"imgur\.com/a/([A-Za-z0-9]+)", Models.Type.Album },
                { nonMatchingUrlPattern + @"imgur\.com/([A-Za-z0-9]+)(?!/)", Models.Type.Image},
                { nonMatchingUrlPattern + @"(?:i\.)?imgur\.com\/([A-za-z0-9]+)\.(?:jpg|gif|png|jpeg)", Models.Type.Image }
            };

        private readonly string _client_id = "d32b287f79b910f";

        public async Task<ImgurResponse<Album>> GetAlbumImagesAsync(string hashcode)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Client-ID", _client_id);

                var result = await httpClient.GetAsync(@$"https://api.imgur.com/3/album/{hashcode}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                return await JsonSerializer.DeserializeAsync<ImgurResponse<Album>>(await result.Content.ReadAsStreamAsync(), options);
            }
        }

        public async Task<ImgurResponse<Image>> GetImageAsync(string hashcode)
        {
            using(var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Client-ID", _client_id);

                var result = await httpClient.GetAsync(@$"https://api.imgur.com/3/image/{hashcode}.json");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                return await JsonSerializer.DeserializeAsync<ImgurResponse<Image>>(await result.Content.ReadAsStreamAsync(), options);
            }
        }

        public Models.Type GetTypeForUrl(string url)
        {
            foreach(var pattern in _patternDictionary)
            {
                var match = Regex.Match(url, pattern.Key);
                if (match != null && match.Success)
                    return pattern.Value;
            }

            throw new ArgumentException($"This url does not appear to be an imgur URL or is in an unsupported format: '{url}'");
        }

        public string GetHashcodeForUrl(string url)
        {
            foreach(var pattern in _patternDictionary)
            {
                var match = Regex.Match(url, pattern.Key);
                if (match != null && match.Success && match.Groups.Count == 2)
                    return match.Groups[1].Value;
            }

            throw new ArgumentException($"This url does not appear to be an imgur URL or is in an unsupported format: '{url}'");
        }
    }
}
