using coworking_spaces.Models;
using coworking_spaces.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml;

namespace coworking_spaces.Controllers
{
    public class HomeController : Controller
    {
        // Home Coworking Spaces
        [HttpGet]
        public async Task<IActionResult> Index(string searchQuery = null, string organisationFilter = null, int pageNumber = 1)
        {
            try
            {
                // Recupera i dati degli spazi di coworking
                var coworkingSpaces = await CoworkingSpacesServices.FetchCoworkingSpacesAsync();
                
                // Imposta il numero totale di record e il numero di pagina nella ViewBag
                ViewBag.totalCount = coworkingSpaces.total_count;
                ViewBag.PageNumber = pageNumber;

                // Filtra i risultati inizialmente con tutti i dati
                var filteredResults = coworkingSpaces.results.AsEnumerable();

                // Filtra i risultati in base alla query di ricerca, se fornita
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    filteredResults = filteredResults.Where(r => 
                        r.organisation.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                        r.address.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
                }

                // Filtra i risultati in base al filtro dell'organizzazione, se fornito
                if (!string.IsNullOrEmpty(organisationFilter))
                {
                    filteredResults = filteredResults.Where(r => r.organisation == organisationFilter);
                }

                // Imposta i risultati filtrati nella ViewBag
                ViewBag.results = filteredResults.ToList();

                // Prende la lista di organizzazioni uniche e le ordina alfabeticamente
                ViewBag.organisations = coworkingSpaces.results
                    .Select(r => r.organisation)
                    .Distinct()
                    .OrderBy(o => o)
                    .ToList();

                // Imposta i filtri selezionati nella ViewBag
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
        [HttpGet]
        public async Task<IActionResult> Map()
        {
            try
            {
                // Recupera i dati degli spazi di coworking
                var coworkingSpaces = await CoworkingSpacesServices.FetchCoworkingSpacesAsync();
                
                // Imposta il numero totale di record nella ViewBag
                ViewBag.totalCount = coworkingSpaces.total_count;
                
                // Imposta i risultati nella ViewBag
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