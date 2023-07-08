using Core.DataModels;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portal.Models;
using RestSharp;
using System.Diagnostics;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RestClient client = new RestClient("https://localhost:44316/");

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetPreData()
        {
            var request = new RestRequest("api/LocalNotesApi", method: Method.Get);
            var response = await client.GetAsync(request);
            IList<NoteDataModel> data = JsonConvert.DeserializeObject<IList<NoteDataModel>>(response.Content);
            return Ok(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_ModalPartialView");
        }
        [HttpGet]
        public IActionResult AddChild()
        {
            return PartialView("_ModalChildPartialView");
        }

        [HttpPost(Name = "AddMain")]
        public async Task<ActionResult> AddMainNote(NoteMainModel noteMainModel)
        {
            var request = new RestRequest("api/LocalNotesApi/AddMainNote", method: Method.Delete)
                .AddHeader("accept", "*/*")
                .AddBody(noteMainModel);
            var response = await client.PostAsync(request);
            return Ok(response.Content);
        }

        [HttpPost(Name = "AddChild")]
        public async Task<ActionResult> AddChildNote(NoteChildModel noteChildModel)
        {
            var request = new RestRequest("api/LocalNotesApi/AddChildNote", method: Method.Delete)
                .AddHeader("accept", "*/*")
                .AddBody(noteChildModel);
            var response = await client.PostAsync(request);
            return Ok(response.Content);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteChild(int id, int[]? ids)
        {
            var request = new RestRequest("api/LocalNotesApi/DeleteChild?id=" + id, method: Method.Delete)
                .AddHeader("accept", "*/*")
                .AddBody(ids);
            var response = await client.DeleteAsync(request);
            return Ok(response.Content);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteMain(int id, int[]? ids)
        {
            var request = new RestRequest("api/LocalNotesApi/DeleteMain?id=" + id, method: Method.Delete)
                .AddHeader("accept", "*/*")
                .AddBody(ids);
            var response = await client.DeleteAsync(request);
            return Ok(response.Content);
        }
    }
}