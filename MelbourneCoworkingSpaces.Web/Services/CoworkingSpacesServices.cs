using coworking_spaces.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace coworking_spaces.Services
{
    public class CoworkingSpacesServices
    {
        public static async Task<Rootobject> FetchCoworkingSpacesAsync(string queryParams = "", int? limit = 100)
        {
            // Base URI dell'API con il limite di record
            string BaseUri = $"https://data.melbourne.vic.gov.au/api/explore/v2.1/catalog/datasets/coworking-spaces/records?limit={limit}";
            
            // Aggiunge eventuali parametri di query all'URI
            if (!string.IsNullOrWhiteSpace(queryParams))
                BaseUri += $"&{queryParams}";

            // Crea un'istanza di HttpClient per effettuare la richiesta HTTP
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(BaseUri);

            // Verifica se la risposta è stata completata con successo
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Errore API: {response.StatusCode}");

            // Legge il contenuto della risposta come stringa
            string contents = await response.Content.ReadAsStringAsync();

            // Deserializza il contenuto JSON in un oggetto Rootobject
            return JsonConvert.DeserializeObject<Rootobject>(contents);
        }
    }
}
