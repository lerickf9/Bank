using Microsoft.AspNetCore.Mvc;
using FakeBankApi_MSP.Modelos;

namespace MusicProAPI.Controllers
{
	[ApiController]
	[Route("Tarjeta")]
	public class TarjetaController : Controller
	{
		GlobalMetods metods = new GlobalMetods();

		[HttpGet]
		[Route("GetTarjetas")]
		public dynamic GetTarjetas()
		{
			string[] list = metods.getContentFile("Tarjetas");

			if (list.Count() == 0)
			{
				return new
				{
					resultTransaccion = false,
					message = "No hay tarjetas registradas"
				};
			}

			List<Tarjeta> tarjetas = new List<Tarjeta>();

			for (int i = 0; i < list.Count(); i++)
			{
				string[] splitArr = list[i].Split("||");
				Tarjeta tarjeta = new Tarjeta();

				tarjeta.Rut_Persona = splitArr[0];
				tarjeta.NumeroTarjeta = splitArr[1];
				tarjeta.Cvv = Convert.ToInt32(splitArr[2]);
				tarjeta.ClaveTarjeta = Convert.ToInt32(splitArr[3]);
				tarjeta.NumeroCuenta = splitArr[4];
				tarjeta.TipoTarjeta = splitArr[5];
				tarjeta.FechaCreacion = splitArr[6];
				tarjeta.FechaExpiracion = splitArr[7];
				tarjeta.Activa = Convert.ToBoolean(splitArr[8]);

				tarjetas.Add(tarjeta);
			}

			return tarjetas;
		}

		[HttpGet]
		[Route("GetTarjetasPersona")]
		public dynamic GetTarjetasPersona(string Rut_Persona)
		{
			if (!metods.validarRut(Rut_Persona))
			{
				return new
				{
					resultTransaccion = false,
					message = "El formato de rut es invalido, formato requerido: 99999999-9"
				};
			}

			string[] list = metods.getContentFile("Tarjetas");

			if (list.Count() == 0)
			{
				return new
				{
					resultTransaccion = false,
					message = "No hay tarjetas registradas"
				};
			}

			string[] listUsuarios = metods.getContentFile("Personas");

			bool personaEncontrada = false;

			for (int i = 0; i < listUsuarios.Count(); i++)
			{
				string[] splitArr = listUsuarios[i].Split("||");

				if (splitArr[0] == Rut_Persona)
				{
					personaEncontrada = true;
				}
			}

			if (!personaEncontrada)
			{
				return new
				{
					resultTransaccion = false,
					message = "La persona con rut '" + Rut_Persona + "' no existe en los registros",
				};
			}

			List<Tarjeta> tarjetas = new List<Tarjeta>();

			for (int i = 0; i < list.Count(); i++)
			{
				string[] splitArr = list[i].Split("||");

				if (splitArr[0] == Rut_Persona)
				{
					Tarjeta tarjeta = new Tarjeta();

					tarjeta.Rut_Persona = splitArr[0];
					tarjeta.NumeroTarjeta = splitArr[1];
					tarjeta.Cvv = Convert.ToInt32(splitArr[2]);
					tarjeta.ClaveTarjeta = Convert.ToInt32(splitArr[3]);
					tarjeta.NumeroCuenta = splitArr[4];
					tarjeta.TipoTarjeta = splitArr[5];
					tarjeta.FechaCreacion = splitArr[6];
					tarjeta.FechaExpiracion = splitArr[7];
					tarjeta.Activa = Convert.ToBoolean(splitArr[8]);

					tarjetas.Add(tarjeta);
				}
			}

			return tarjetas;
		}

		[HttpGet]
		[Route("GetTarjeta")]
		public dynamic GetTarjeta(string Rut_Persona, string NumeroTarjeta)
		{
			if (!metods.validarRut(Rut_Persona))
			{
				return new
				{
					resultTransaccion = false,
					message = "El formato de rut es invalido, formato requerido: 99999999-9"
				};
			}

			string[] list = metods.getContentFile("Tarjetas");

			if (list.Count() == 0)
			{
				return new
				{
					resultTransaccion = false,
					message = "No hay tarjetas registradas"
				};
			}

			string[] listUsuarios = metods.getContentFile("Personas");

			bool personaEncontrada = false;

			for (int i = 0; i < listUsuarios.Count(); i++)
			{
				string[] splitArr = listUsuarios[i].Split("||");

				if (splitArr[0] == Rut_Persona)
				{
					personaEncontrada = true;
				}
			}

			if (!personaEncontrada)
			{
				return new
				{
					resultTransaccion = false,
					message = "La persona con rut '" + Rut_Persona + "' no existe en los registros",
				};
			}

			Tarjeta tarjeta = new Tarjeta();

			bool encontrado = false;

			for (int i = 0; i < list.Count(); i++)
			{
				string[] splitArr = list[i].Split("||");

				if (splitArr[0] == Rut_Persona && splitArr[1] == NumeroTarjeta)
				{
					tarjeta.Rut_Persona = splitArr[0];
					tarjeta.NumeroTarjeta = splitArr[1];
					tarjeta.Cvv = Convert.ToInt32(splitArr[2]);
					tarjeta.ClaveTarjeta = Convert.ToInt32(splitArr[3]);
					tarjeta.NumeroCuenta = splitArr[4];
					tarjeta.TipoTarjeta = splitArr[5];
					tarjeta.FechaCreacion = splitArr[6];
					tarjeta.FechaExpiracion = splitArr[7];
					tarjeta.Activa = Convert.ToBoolean(splitArr[8]);

					encontrado = true;
					break;
				}
			}

			if (!encontrado)
			{
				return new
				{
					resultTransaccion = false,
					message = "La tarjeta '" + NumeroTarjeta + "' no existe en los registros"
				};
			}

			return tarjeta;
		}

		[HttpPost]
		[Route("CrearTarjeta")]
		public dynamic CrearTarjeta(string Rut_Persona, string NumeroCuenta, string tipoTarjeta, int claveTarjeta)
		{
			if (!metods.validarRut(Rut_Persona))
			{
				return new
				{
					resultTransaccion = false,
					message = "El formato de rut es invalido, formato requerido: 99999999-9"
				};
			}

			string[] listUsuarios = metods.getContentFile("Personas");

			bool personaEncontrada = false;

			for (int i = 0; i < listUsuarios.Count(); i++)
			{
				string[] splitArr = listUsuarios[i].Split("||");

				if (splitArr[0] == Rut_Persona)
				{
					personaEncontrada = true;
				}
			}

			if (!personaEncontrada)
			{
				return new
				{
					resultTransaccion = false,
					message = "La persona con rut '" + Rut_Persona + "' no existe en los registros",
				};
			}

			if (tipoTarjeta != "visa" && tipoTarjeta != "mastercard" && tipoTarjeta != "american_express")
			{
				return new
				{
					resultTransaccion = false,
					message = "Formato tipo cuenta incorrecto los tipo de tarjeta debe ser 'visa', 'mastercard' o 'american_express' ",
				};
			}

			if (claveTarjeta.ToString().Length > 4)
			{
				return new
				{
					resultTransaccion = false,
					message = "La contraseña no puede tener mas de 4 digitos",
				};
			}

			string[] listCuentas = metods.getContentFile("Cuentas");
			bool cuentaEncontrada = false;
			for (int i = 0; i < listCuentas.Count(); i++)
			{
				string[] splitArr = listCuentas[i].Split("||");

				if (splitArr[0] == Rut_Persona && splitArr[1] == NumeroCuenta)
				{
					if (!Convert.ToBoolean(splitArr[3]))
					{
						return new
						{
							resultTransaccion = false,
							mesage = "La cuenta '" + NumeroCuenta + "' se encuentra inhabilitada"
						};
					}

					cuentaEncontrada = true;
					break;
				}
			}

			if (!cuentaEncontrada)
			{
				return new
				{
					resultTransaccion = false,
					message = "La cuenta '" + NumeroCuenta + "' no existe en los registros"
				};
			}

			Tarjeta tarjeta = new Tarjeta();
			tarjeta.Rut_Persona = Rut_Persona;
			tarjeta.NumeroTarjeta = metods.GenerateCreditCardNumber(tipoTarjeta);
			tarjeta.Cvv = metods.GenerateCreditAccountCvv();
			tarjeta.ClaveTarjeta = claveTarjeta;
			tarjeta.NumeroCuenta = NumeroCuenta;
			tarjeta.TipoTarjeta = tipoTarjeta;
			tarjeta.FechaCreacion = DateTime.Now.ToString("dd/MM/yyyy");
			tarjeta.FechaExpiracion = DateTime.Now.AddYears(5).ToString("MM/yyyy");
			tarjeta.Activa = true;

			metods.saveLineFile("Tarjetas", String.Format("{0}||{1}||{2}||{3}||{4}||{5}||{6}||{7}||{8}", tarjeta.Rut_Persona, tarjeta.NumeroTarjeta, tarjeta.Cvv, tarjeta.ClaveTarjeta, tarjeta.NumeroCuenta, tarjeta.TipoTarjeta, tarjeta.FechaCreacion, tarjeta.FechaExpiracion, tarjeta.Activa));

			return new
			{
				resultTransaccion = true,
				message = "Tarjeta registrada, numero de cuenta: " + tarjeta.NumeroCuenta + ", numero tarjeta: " + tarjeta.NumeroTarjeta + ""
			};
		}

		[HttpPut]
		[Route("ModificarClaveTarjeta")]
		public dynamic ModificarClaveTarjeta(string Rut_Persona, string NumeroTarjeta, string NumeroCuenta, int ClaveNueva)
		{
			if (!metods.validarRut(Rut_Persona))
			{
				return new
				{
					resultTransaccion = false,
					message = "El formato de rut es invalido, formato requerido: 99999999-9"
				};
			}

			string[] list = metods.getContentFile("Tarjetas");

			if (list.Count() == 0)
			{
				return new
				{
					resultTransaccion = false,
					message = "No hay categorias registradas"
				};
			}

			string[] listPersonas = metods.getContentFile("Personas");

			bool personaEncontrada = false;

			for (int i = 0; i < listPersonas.Count(); i++)
			{
				string[] splitArr = listPersonas[i].Split("||");

				if (splitArr[0] == Rut_Persona)
				{
					personaEncontrada = true;
				}
			}

			if (!personaEncontrada)
			{
				return new
				{
					resultTransaccion = false,
					message = "La persona con rut '" + Rut_Persona + "' no existe en los registros",
				};
			}

			if (ClaveNueva.ToString().Length > 4)
			{
				return new
				{
					resultTransaccion = false,
					message = "La contraseña no puede tener mas de 4 digitos",
				};
			}

			string[] listCuentas = metods.getContentFile("Cuentas");
			bool cuentaEncontrada = false;
			for (int i = 0; i < listCuentas.Count(); i++)
			{
				string[] splitArr = listCuentas[i].Split("||");

				if (splitArr[0] == Rut_Persona && splitArr[1] == NumeroCuenta)
				{
					if (Convert.ToBoolean(splitArr[3]))
					{
						return new
						{
							resultTransaccion = false,
							message = "La cuenta '" + NumeroCuenta + "' se encuentra inhabilitada"
						};
					}

					cuentaEncontrada = true;
					break;
				}
			}

			if (!cuentaEncontrada)
			{
				return new
				{
					resultTransaccion = false,
					message = "La cuenta '" + NumeroCuenta + "' no existe en los registros"
				};
			}

			bool encontrado = false;
			List<string> content = new List<string>();

			for (int i = 0; i < list.Count(); i++)
			{
				string[] splitArr = list[i].Split("||");

				if (splitArr[0] == Rut_Persona)
				{

					content.Add(String.Format("{0}||{1}||{2}||{3}||{4}||{5}||{6}||{7}||{8}", splitArr[0], splitArr[1], splitArr[2], ClaveNueva, splitArr[4], splitArr[5], splitArr[6], splitArr[7], splitArr[8]));

					encontrado = true;
					continue;
				}

				content.Add(list[i]);
			}

			if (!encontrado)
			{
				return new
				{
					resultTransaccion = false,
					message = "La tarjeta '" + NumeroTarjeta + "' no existe en los registros"
				};
			}

			metods.updateLineFile("Tarjetas", content);

			return new
			{
				resultTransaccion = true,
				message = "Se ha  cambiado la clave de la tarjeta '" + NumeroTarjeta + "'"
			};
		}

		[HttpDelete]
		[Route("EliminarTarjeta")]
		public dynamic EliminarTarjeta(string NumeroTarjeta)
		{
			string[] list = metods.getContentFile("Tarjetas");

			if (list.Count() == 0)
			{
				return new
				{
					resultTransaccion = false,
					message = "No hay categorias registrados"
				};
			}

			bool encontrado = false;
			List<string> content = new List<string>();

			for (int i = 0; i < list.Count(); i++)
			{
				string[] splitArr = list[i].Split("||");

				if (splitArr[1] != NumeroTarjeta)
				{
					content.Add(list[i]);
				}
				else
				{
					encontrado = true;
				}
			}

			if (!encontrado)
			{
				return new
				{
					resultTransaccion = false,
					message = "La tarjeta '" + NumeroTarjeta + "' no existe en los registros"
				};
			}

			metods.updateLineFile("Tarjetas", content);

			return new
			{
				resultTransaccion = true,
				message = "La tarjeta '" + NumeroTarjeta + "' fue eliminado exitosamente"
			};
		}

	}
}
