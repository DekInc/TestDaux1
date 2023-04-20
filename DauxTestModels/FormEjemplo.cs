using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

namespace DauxTestModels
{
	public class FormEjemplo
	{
		[BindProperty]
		[Required(ErrorMessage = MensajesValidacion.Requerido)]
		[MinLength(3, ErrorMessage = MensajesValidacion.MinLargo3)]
		public string? TxtNombre { get; set; }

		[BindProperty]
		[Required(ErrorMessage = MensajesValidacion.Requerido)]
		[MinLength(3, ErrorMessage = MensajesValidacion.MinLargo3)]
		public string? TxtApellido { get; set; }
	}
}