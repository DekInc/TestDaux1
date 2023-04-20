using System.Diagnostics;
using System.Text;
using DauxTestModels;

using Microsoft.AspNetCore.Mvc;

namespace DauxTestFrontEnd.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[HttpPost]
		public RespuestaRet<string> Index(FormEjemplo formEjemplo)
		{
			bool valid = ModelState.IsValid;
			if (ModelState.IsValid)
			{
				var a1 = Convert.ToBase64String(Encoding.Default.GetBytes(formEjemplo.TxtNombre + formEjemplo.TxtApellido));
				return new RespuestaRet<string>
				{
					Respuesta = "ok"
				};
			} 
			else 
			{
				string listaErrores = "";
				foreach (var itemAValidar in ModelState.Keys)
				{
					var itemModelState = ModelState[itemAValidar];
					if (itemModelState != null)
					{
						foreach (var itemError in itemModelState.Errors)
						{
							listaErrores += $"<div>{itemAValidar} - {itemError.ErrorMessage}</div>";
						}
					}
				}
				return new RespuestaRet<string>
				{
					Respuesta = listaErrores
				};
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}