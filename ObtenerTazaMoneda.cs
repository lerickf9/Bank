using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FakeBankApi_MSP
{   

	public class ObtenerTazaMoneda : Controller
	{
		public string ApiKey = "cc0d4bb5dd926e37ee19aec05e77a71fb769661e";

		public async Task<string> GetTasaEuro()
		{
			var httpClient = new HttpClient();

			string url = "https://api.cmfchile.cl/api-sbifv3/recursos_api/euro?apikey=" + ApiKey + "&formato=json";

			HttpResponseMessage response = await httpClient.GetAsync(url);

			string responseData = "";

			if (response.IsSuccessStatusCode)
			{
				responseData = await response.Content.ReadAsStringAsync();
			}

			return responseData;
		}

		public async Task<string> GetTasaDolar()
		{
			var httpClient = new HttpClient();

			string url = "https://api.sbif.cl/api-sbifv3/recursos_api/dolar?apikey=" + ApiKey + "&formato=json";
			

            var response = await httpClient.GetAsync(url);

			string responseData = "";

			if (response.IsSuccessStatusCode)
			{
				responseData = await response.Content.ReadAsStringAsync();
			}

			return responseData;
		}

		public async Task<string> GetTasaUF()
		{
			var httpClient = new HttpClient();

			string url = "https://api.cmfchile.cl/api-sbifv3/recursos_api/uf?apikey=" + ApiKey + "&formato=json";

			HttpResponseMessage response = await httpClient.GetAsync(url);

			string responseData = "";

			if (response.IsSuccessStatusCode)
			{
				responseData = await response.Content.ReadAsStringAsync();
			}

			return responseData;
		}
	}

}
