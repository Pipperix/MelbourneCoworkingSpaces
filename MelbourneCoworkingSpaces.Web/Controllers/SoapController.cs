using Microsoft.AspNetCore.Mvc;
using SoapService;

namespace coworking_spaces.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoapController : Controller
    {
        SoapServiceClient client;

        public SoapController()
        {
            client = new SoapServiceClient(SoapServiceClient.EndpointConfiguration.BasicHttpBinding_ISoapService);
        }

        [HttpGet("fetch-data")]
        public async Task<IActionResult> FetchData()
        {
            try
            {
                // Chiama il metodo del servizio SOAP
                var response = await client.GetDataAsync();

                client.Close();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
