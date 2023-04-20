namespace DauxTestModels
{
	public class RespuestaRet<T>
	{
		public T? Respuesta { get; set; }
		public string MensajeError = "";
	}
}
