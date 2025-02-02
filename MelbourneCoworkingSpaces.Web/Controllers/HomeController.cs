using coworking_spaces.Models;
using MelbourneCoworkingSpaces.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml;

namespace coworking_spaces.Controllers
{
    public class HomeController : Controller
    {

        // Home Coworking Spaces
        public async Task<IActionResult> Index(string searchQuery = null, string organisationFilter = null, int pageNumber = 1)
        {
            try
            {
                var coworkingSpaces = await CoworkingSpacesServices.FetchCoworkingSpacesAsync();
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
                var coworkingSpaces = await CoworkingSpacesServices.FetchCoworkingSpacesAsync();
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