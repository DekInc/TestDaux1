using System.Diagnostics;
using System.Text;

using DauxService;

using DauxTestModels;

using Microsoft.AspNetCore.Mvc;

namespace DauxTestFrontEnd.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly string UrlConexion;

		public HomeController(ILogger<HomeController> logger, IConfiguration config)
		{
			_logger = logger;
			UrlConexion = config["UrlConexion"];
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
            try
            {
                bool valid = ModelState.IsValid;
                if (ModelState.IsValid)
                {
                    var authToken = Convert.ToBase64String(Encoding.Default.GetBytes(formEjemplo.TxtNombre + formEjemplo.TxtApellido));
                    ClienteWebApi clienteWebApi = new ClienteWebApi(UrlConexion, authToken, new ApiRequestEjemplo { nombre = formEjemplo.TxtNombre, apellido = formEjemplo.TxtApellido });
                    string resultado = clienteWebApi.ObtenerResultado();
                    return new RespuestaRet<string>
                    {
                        Respuesta = resultado
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
            catch (Exception e1)
            {
                return new RespuestaRet<string>
                {
                    MensajeError = e1.Message
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