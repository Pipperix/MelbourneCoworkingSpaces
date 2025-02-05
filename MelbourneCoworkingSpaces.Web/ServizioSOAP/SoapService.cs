using coworking_spaces.Models;
using coworking_spaces.Services;
using System.ServiceModel;

namespace coworking_spaces.ServizioSOAP
{
    [ServiceContract]
    public interface ISoapService
    {
        // Definisce un'operazione di servizio che restituisce tutti i dati
        [OperationContract]
        Rootobject GetAllData();

        // Definisce un'operazione di servizio che restituisce dati filtrati per organizzazione
        [OperationContract] 
        Result[] GetFilteredData(string organisation);

        // Definisce un'operazione di servizio che restituisce un numero limitato di record
        [OperationContract]
        Result[] GetDataLimit(int limit);
    }

    // Implementa il servizio SOAP
    public class SoapService : ISoapService
    {
        public Rootobject GetAllData()
        {
            // Chiama il servizio per ottenere tutti i dati
            return CoworkingSpacesServices.FetchCoworkingSpacesAsync().Result;
        }

        public Result[] GetFilteredData(string organisation)
        {
            // Metodo alternativo con API (commentato)
            // return CoworkingSpacesServices.FetchCoworkingSpacesAsync($"where=organisation%20like%20%22{organisation}%22").Result.results;

            // Ottiene tutti i dati e li filtra in base al nome dell'organizzazione
            Result[] filteredData = CoworkingSpacesServices.FetchCoworkingSpacesAsync().Result.results;
            return filteredData.Where(s => s.organisation.ToLower().Contains(organisation.ToLower())).ToArray();
        }

        public Result[] GetDataLimit(int limit)
        {
            // Chiama il servizio per ottenere un numero limitato di dati
            Rootobject contents = CoworkingSpacesServices.FetchCoworkingSpacesAsync(limit: limit).Result;
            return contents.results;
        }
    }
}
