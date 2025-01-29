using coworking_spaces.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml;

namespace coworking_spaces.Controllers
{
    public class HomeController : Controller
    {

        // Da capire
        public HomeController() { }

        // ???
        private async Task<Rootobject> FetchCoworkingSpacesAsync()
        {
            string BaseUri = "https://data.melbourne.vic.gov.au/api/explore/v2.1/catalog/datasets/coworking-spaces/records?limit=100";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(BaseUri);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Errore API: {response.StatusCode}");

            string contents = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Rootobject>(contents);
        }

        // Home Coworking Spaces
        public async Task<IActionResult> Index(string searchQuery = null, string organisationFilter = null, int pageNumber = 1)
        {
            try
            {
                var coworkingSpaces = await FetchCoworkingSpacesAsync();
                ViewBag.totalCount = coworkingSpaces.total_count;
                ViewBag.PageNumber = pageNumber;

                var filteredResults = coworkingSpaces.results.AsEnumerable();

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    filteredResults = filteredResults.Where(r => 
                        r.organisation.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                        r.address.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrEmpty(organisationFilter))
                {
                    filteredResults = filteredResults.Where(r => r.organisation == organisationFilter);
                }

                ViewBag.results = filteredResults.ToList();

                // Prendo la lista di organizzazioni uniche e le ordino alfabeticamente
                ViewBag.organisations = coworkingSpaces.results
                    .Select(r => r.organisation)
                    .Distinct()
                    .OrderBy(o => o)
                    .ToList();

                ViewBag.SelectedOrganisation = organisationFilter;
                ViewBag.SearchQuery = searchQuery;

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Si è verificato un errore imprevisto: " + ex.Message;
                ViewBag.results = new List<Result>();
                ViewBag.organisations = new List<string>();
                return View();
            }
        }

        // Home Coworking Spaces
        public async Task<IActionResult> Map()
        {
            try
            {
                var coworkingSpaces = await FetchCoworkingSpacesAsync();
                ViewBag.totalCount = coworkingSpaces.total_count;
                ViewBag.results = coworkingSpaces.results;

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Si è verificato un errore imprevisto: " + ex.Message;
                return View();
            }
        }
    }
}