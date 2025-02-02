using coworking_spaces.Models;
using MelbourneCoworkingSpaces.Web.Services;
using System.ServiceModel;

namespace MelbourneCoworkingSpaces.Web.ServizioSOAP
{
    [ServiceContract]
    public interface ISoapService
    {
        [OperationContract]
        Rootobject GetAllData();

        [OperationContract] 
        Rootobject GetFilteredData(string organisation);

        [OperationContract]
        Result[] GetDataLimit(int limit);
    }

    public class SoapService : ISoapService
    {
        // Restituisce tutti i record
        public Rootobject GetAllData()
        {
            return CoworkingSpacesServices.FetchCoworkingSpacesAsync().Result;
        }

        // Restituisce record filtrati in base al nome dell'organizzazione
        public Rootobject GetFilteredData(string organisation)
        {
            return CoworkingSpacesServices.FetchCoworkingSpacesAsync($"where=organisation%20like%20%22{organisation}%22").Result;
        }

        // Restituisce numero di record in base al limit
        public Result[] GetDataLimit(int limit)
        {
            Rootobject contents = CoworkingSpacesServices.FetchCoworkingSpacesAsync(limit: limit).Result;
            return contents.results;
        }
    }
}
