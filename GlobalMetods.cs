using FakeBankApi_MSP;
using FakeBankApi_MSP.Modelos;
using Newtonsoft.Json;
using System.IO;

namespace MusicProAPI
{
	public class GlobalMetods
	{
		public string[] getContentFile(string NomDoc)
		{
			if (!Directory.Exists("C:\\txtFakeBank"))
			{
				Directory.CreateDirectory("C:\\txtFakeBank");
			}

			if (!System.IO.File.Exists("C:\\txtFakeBank\\" + NomDoc + ".txt"))
			{
				System.IO.File.Create("C:\\txtFakeBank\\" + NomDoc + ".txt").Close();
			}

			return System.IO.File.ReadAllLines("C:\\txtFakeBank\\" + NomDoc + ".txt");
		}

		public void saveLineFile(string NomDoc, string lineContent)
		{
			if (!Directory.Exists("C:\\txtFakeBank"))
			{
				Directory.CreateDirectory("C:\\txtFakeBank");
			}

			if (!System.IO.File.Exists("C:\\txtFakeBank\\" + NomDoc + ".txt"))
			{
				System.IO.File.Create("C:\\txtFakeBank\\" + NomDoc + ".txt").Close();
			}

			StreamWriter sw = System.IO.File.AppendText("C:\\txtFakeBank\\" + NomDoc + ".txt");
			sw.WriteLine(lineContent);
			sw.Close();
		}

		public void updateLineFile(string NomDoc, List<string> content)
		{
			if (!Directory.Exists("C:\\txtFakeBank"))
			{
				Directory.CreateDirectory("C:\\txtFakeBank");
			}

			if (!System.IO.File.Exists("C:\\txtFakeBank\\" + NomDoc + ".txt"))
			{
				System.IO.File.Create("C:\\txtFakeBank\\" + NomDoc + ".txt").Close();
			}

			System.IO.File.WriteAllLines("C:\\txtFakeBank\\" + NomDoc + ".txt", content);
		}

		public bool validarRut(string rut)
		{

			bool validacion = false;
			string saveOriginalRut = rut;
			try
			{
				rut = rut.ToUpper();
				rut = rut.Replace(".", "");
				rut = rut.Replace("-", "");
				int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

				char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

				int m = 0, s = 1;
				for (; rutAux != 0; rutAux /= 10)
				{
					s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
				}
				if (dv == (char)(s != 0 ? s + 47 : 75))
				{
					validacion = true;
				}

				string format = saveOriginalRut.Substring(saveOriginalRut.Length - 2, 1);
				if (format != "-")
				{
					validacion = false;
				}

			}
			catch (Exception)
			{
			}
			return validacion;
		}

		public string formatearRut(string rut)
		{
			int cont = 0;
			string format;
			if (rut.Length == 0)
			{
				return "";
			}
			else
			{
				rut = rut.Replace(".", "");
				rut = rut.Replace("-", "");
				format = "-" + rut.Substring(rut.Length - 1);
				for (int i = rut.Length - 2; i >= 0; i--)
				{
					format = rut.Substring(i, 1) + format;
					cont++;
					if (cont == 3 && i != 0)
					{
						format = "." + format;
						cont = 0;
					}
				}
				return format;
			}
		}

		public bool ValidarFormatoFecha(string fecha)
		{
			string formato = "dd/MM/yyyy";
			DateTime fechaValidada;

			if (DateTime.TryParseExact(fecha, formato, null, System.Globalization.DateTimeStyles.None, out fechaValidada))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		//Generacion de numeros de trjeta y de cuentas bancarias 

		//======================= Generador Nro Tarjeta ==============================
		public string GenerateCreditCardNumber(string cardType)
		{
			string cardNumber = string.Empty;

			switch (cardType)
			{
				case "visa":
					cardNumber = GenerateVisaCardNumber();
					break;
				case "mastercard":
					cardNumber = GenerateMastercardCardNumber();
					break;
				case "american_express":
					cardNumber = GenerateAmericanExpressCardNumber();
					break;
				default:
					// Generar un número de tarjeta genérico
					cardNumber = GenerateGenericCardNumber();
					break;
			}

			return cardNumber;
		}

		private string GenerateVisaCardNumber()
		{
			string prefix = "4";
			string number = GenerateRandomNumber(13);
			return prefix + number;
		}

		private string GenerateMastercardCardNumber()
		{
			string prefix = "5";
			string number = GenerateRandomNumber(15);
			return prefix + number;
		}

		private string GenerateAmericanExpressCardNumber()
		{
			string prefix = "3";
			string number = GenerateRandomNumber(11);
			return prefix + number;
		}

		private string GenerateGenericCardNumber()
		{
			string prefix = GenerateRandomNumber(1);
			string number = GenerateRandomNumber(15);
			return prefix + number;
		}

		private string GenerateRandomNumber(int length)
		{
			Random randomNumber = new Random();

			string number = string.Empty;

			for (int i = 0; i < length - 1; i++)
			{
				number += randomNumber.Next(0, 10);
			}

			return number;
		}

		//====================== Generador de numero de cuenta =======================
		public string GenerateCreditAccountNumber()
		{
			Random randomNumber = new Random();

			string number = string.Empty;

			for (int i = 0; i < 12; i++)
			{
				number += randomNumber.Next(0, 10);
			}

			return number;
		}
		public int GenerateCreditAccountCvv()
		{
			Random randomNumber = new Random();

			string number = string.Empty;

			for (int i = 0; i < 3; i++)
			{
				number += randomNumber.Next(0, 10);
			}

			return Convert.ToInt32(number);
		}

		//====================== Conversion a pesos chienos ==========================

		public async Task<decimal> ConvertionOfMoney(string Moneda, string Monto)
		{
			ObtenerTazaMoneda tasa = new ObtenerTazaMoneda();

			decimal MontoPeso = 0;

			if (Moneda == "dolar")
			{
				var resultDolar = await tasa.GetTasaDolar();
				var deserializeDolar = JsonConvert.DeserializeObject<TasaMoneda.ListDolar>(resultDolar);

				if (deserializeDolar != null)
				{
					MontoPeso = Math.Round(Convert.ToDecimal(Monto) * Convert.ToDecimal(deserializeDolar.Dolares.First().Valor), 0);
				}
				else
				{
					MontoPeso = 0;
				}

			}
			else if (Moneda == "euro")
			{
				var resultEuro = await tasa.GetTasaEuro();
				var deserializeEuro = JsonConvert.DeserializeObject<TasaMoneda.ListEuro>(resultEuro);

				if (deserializeEuro != null)
				{
					MontoPeso = Math.Round(Convert.ToDecimal(Monto) * Convert.ToDecimal(deserializeEuro.Euros.First().Valor), 0);
				}
				else
				{
					MontoPeso = 0;
				}
			}
			else if (Moneda == "uf")
			{
				var resultUf = await tasa.GetTasaUF();
				var deserializeUf = JsonConvert.DeserializeObject<TasaMoneda.ListUf>(resultUf);

				if (deserializeUf != null)
				{
					MontoPeso = Math.Round(Convert.ToDecimal(Monto) * Convert.ToDecimal(deserializeUf.UFs.First().Valor), 0);
				}
				else
				{
					MontoPeso = 0;
				}
			}

			return MontoPeso;
		}
	}
}
