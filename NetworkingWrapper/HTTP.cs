using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace NetworkingWrapper
{
    public class HTTP
    {
        public async static Task<string> PostWithResponse(string requestUri, string bodyContent)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), requestUri))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");
                    request.Content = new StringContent($"\"{bodyContent}\"");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
        }

        public async static void Post(string requestUri, string bodyContent)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), requestUri))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");
                    request.Content = new StringContent($"\"{bodyContent}\"");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    await httpClient.SendAsync(request);
                }
            }
        }

        public async static Task<T> Get<T>(string requestUri)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), requestUri))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");
                    
                    var response = await httpClient.SendAsync(request);
                    var responseString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseString);
                }
            }
        }
    }
}