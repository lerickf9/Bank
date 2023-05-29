using Microsoft.AspNetCore.Mvc;
using MusicProAPI;
using Newtonsoft.Json;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Net.Http;
using System.Text.Json;
using FakeBankApi_MSP.Modelos;

namespace FakeBankApi_MSP.Controllers
{
	[ApiController]
	[Route("TasaMoneda")]
	public class TasaMonedaController : Controller
	{
		GlobalMetods metods = new GlobalMetods();

		ObtenerTazaMoneda tasa = new ObtenerTazaMoneda();

		[HttpGet]
		[Route("GetTasaMonedas")]
		public async Task<dynamic> GetTasaMonedas()
		{
			var resultDolar = await tasa.GetTasaDolar();
			var resultEuro = await tasa.GetTasaEuro();
			var resultUf = await tasa.GetTasaUF();

			var deserializeDolar = JsonConvert.DeserializeObject<TasaMoneda.ListDolar>(resultDolar);
			var deserializeEuro = JsonConvert.DeserializeObject<TasaMoneda.ListEuro>(resultEuro);
			var deserializeUf = JsonConvert.DeserializeObject<TasaMoneda.ListUf>(resultUf);

			return new
			{
				ResultsDolar = deserializeDolar,
				ResultEuro = deserializeEuro,
				resultUf = deserializeUf
			};
		}

		[HttpGet]
		[Route("GetDolar")]
		public async Task<dynamic> GetDolar()
		{
			var resultDolar = await tasa.GetTasaDolar();

			var deserializeDolar = JsonConvert.DeserializeObject<TasaMoneda.ListDolar>(resultDolar);

			return new
			{
				ResultsDolar = deserializeDolar,
			};
		}
	}
}
