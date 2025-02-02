using coworking_spaces.Models;
using Newtonsoft.Json;
using System;

namespace MelbourneCoworkingSpaces.Web.Services
{
    public class CoworkingSpacesServices
    {
        public static async Task<Rootobject> FetchCoworkingSpacesAsync(string queryParams = "", int? limit = 100)
        {
            string BaseUri = $"https://data.melbourne.vic.gov.au/api/explore/v2.1/catalog/datasets/coworking-spaces/records?limit={limit}";
            
            if (!string.IsNullOrWhiteSpace(queryParams))
                BaseUri += $"&{queryParams}";

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(BaseUri);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Errore API: {response.StatusCode}");

            string contents = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Rootobject>(contents);
        }
    }
}
