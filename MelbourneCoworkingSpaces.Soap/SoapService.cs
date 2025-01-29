using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Net.Http;
using Newtonsoft.Json;

namespace MelbourneCoworkingSpaces.Soap
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice e nel file di configurazione contemporaneamente.
    public class SoapService : ISoapService
    {

        private readonly string apiUrl = "https://data.melbourne.vic.gov.au/api/explore/v2.1/catalog/datasets/coworking-spaces/records?limit=100";

        public string GetData()
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync(apiUrl).Result;

            if (!response.IsSuccessStatusCode)
                throw new Exception("Impossibile recuperare i dati dall'API REST");
            var jsonData = response.Content.ReadAsStringAsync().Result;

            // Conversione da JSON a XML (necessario per SOAP)
            XmlDocument xmlDocument = JsonConvert.DeserializeXmlNode(jsonData, "CoworkingSpaces");
            return xmlDocument.OuterXml;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
