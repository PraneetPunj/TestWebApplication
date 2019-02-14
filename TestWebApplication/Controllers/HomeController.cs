using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestWebApplication.Contracts;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public HomeController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<IActionResult> Index()
        {
            var PersonResultList = await _personRepository.GetAll();
            var result = PersonResultList
                                .Where(p => p.pets != null)
                                .GroupBy(p => p.gender)
                                    .SelectMany(gps => gps
                                          .SelectMany(gp => gp.pets
                                                        .Where(c => c.type == "Cat"),(gp, c) => new ResultSetViewModel{gender = gp.gender,name = c.name }));

            return View(result);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
