using FakeBankApi_MSP.Modelos;
using Microsoft.AspNetCore.Mvc;
using MusicProAPI;
using System.Text.RegularExpressions;

namespace FakeBankApi_MSP.Controllers
{
	[ApiController]
	[Route("Persona")]
	public class PersonaController : Controller
	{
		GlobalMetods metods = new GlobalMetods();

		[HttpGet]
		[Route("GetPersonas")]
		public dynamic GetPersonas()
		{
			string[] list = metods.getContentFile("Personas");

			if (list.Count() == 0)
			{
				return new
				{
					resultTransaccion = false,
					message = "No hay personas registradas"
				};
			}

			List<Persona> personas = new List<Persona>();

			for (int i = 0; i < list.Count(); i++)
			{
				string[] splitArr = list[i].Split("||");
				Persona persona = new Persona();

				persona.Rut = splitArr[0];
				persona.PrimerNombre = splitArr[1];
				persona.SegundoNombre = splitArr[2];
				persona.PrimerApellido = splitArr[3];
				persona.SegundoApellido = splitArr[4];
				persona.FechaNacimiento = splitArr[5];

				personas.Add(persona);
			}

			return personas;
		}

		[HttpGet]
		[Route("GetPersona")]
		public dynamic GetPersona(string rut)
		{
			string[] list = metods.getContentFile("Personas");

			if (list.Count() == 0)
			{
				return new
				{
					resultTransaccion = false,
					message = "No hay personas registrados"
				};
			}

			if (!metods.validarRut(rut))
			{
				return new
				{
					resultTransaccion = false,
					message = "El formato de rut es invalido, formato requerido: 99999999-9"
				};
			}

			Persona persona = new Persona();

			bool encontrado = false;

			for (int i = 0; i < list.Count(); i++)
			{
				string[] splitArr = list[i].Split("||");

				if (splitArr[0] == rut)
				{
					persona.Rut = splitArr[0];
					persona.PrimerNombre = splitArr[1];
					persona.SegundoNombre = splitArr[2];
					persona.PrimerApellido = splitArr[3];
					persona.SegundoApellido = splitArr[4];
					persona.FechaNacimiento = splitArr[5];
					encontrado = true;
					break;
				}
			}

			if (!encontrado)
			{
				return new
				{
					resultTransaccion = false,
					message = "La persona con rut '" + rut + "' no existe en los registros"
				};
			}

			return persona;
		}

		[HttpPost]
		[Route("CrearPersona")]
		public dynamic CrearPersona(Persona persona)
		{
			if (string.IsNullOrEmpty(persona.PrimerNombre) || string.IsNullOrEmpty(persona.SegundoNombre) || string.IsNullOrEmpty(persona.PrimerApellido) || string.IsNullOrEmpty(persona.SegundoApellido) || string.IsNullOrEmpty(persona.FechaNacimiento) || string.IsNullOrEmpty(persona.Rut))
			{
				return new
				{
					resultTransaccion = false,
					message = "Faltan datos para almacenar la persona",
				};
			}

			string[] list = metods.getContentFile("Personas");

			for (int i = 0; i < list.Count(); i++)
			{
				string[] splitArr = list[i].Split("||");

				if (splitArr[0] == persona.Rut)
				{
					return new
					{
						resultTransaccion = false,
						message = "El RUT ingresado ya existe en los registros"
					};
				}
			}

			if (!metods.ValidarFormatoFecha(persona.FechaNacimiento))
			{
				return new
				{
					resultTransaccion = false,
					message = "El formato de fecha es invalido, formato requerido: 01/01/1900"
				};
			}

			if (!metods.validarRut(persona.Rut))
			{
				return new
				{
					resultTransaccion = false,
					message = "El formato de rut es invalido, formato requerido: 99999999-9"
				};
			}

			metods.saveLineFile("Personas", String.Format("{0}||{1}||{2}||{3}||{4}||{5}", persona.Rut, persona.PrimerNombre.Trim().Replace("|", ""), persona.SegundoNombre.Trim().Replace("|", ""), persona.PrimerApellido.Trim().Replace("|", ""), persona.SegundoApellido.Trim().Replace("|", ""), persona.FechaNacimiento.Trim().Replace("|", "")));

			return new
			{
				resultTransaccion = true,
				message = "La persona con rut " + persona.Rut + " ha sido registrada"
			};
		}

		[HttpPut]
		[Route("ModificarPersona")]
		public dynamic ModificarPersona(Persona persona)
		{
			string[] list = metods.getContentFile("Personas");

			if (list.Count() == 0)
			{
				return new
				{
					resultTransaccion = false,
					message = "No hay personas registradas"
				};
			}

			bool encontrado = false;
			List<string> content = new List<string>();

			for (int i = 0; i < list.Count(); i++)
			{
				string[] splitArr = list[i].Split("||");

				if (splitArr[0] == persona.Rut)
				{
					persona.Rut = splitArr[0];

					if (!string.IsNullOrEmpty(persona.FechaNacimiento))
					{
						if (!metods.ValidarFormatoFecha(persona.FechaNacimiento))
						{
							return new
							{
								resultTransaccion = false,
								message = "El formato de fecha es invalido, formato requerido: 01/01/1900"
							};
						}
					}

					content.Add(String.Format("{0}||{1}||{2}||{3}||{4}||{5}", persona.Rut, string.IsNullOrEmpty(persona.PrimerNombre) ? splitArr[1] : persona.PrimerNombre.Trim().Replace("|", ""), string.IsNullOrEmpty(persona.SegundoNombre) ? splitArr[2] : persona.SegundoNombre.Trim().Replace("|", ""), string.IsNullOrEmpty(persona.PrimerApellido) ? splitArr[3] : persona.PrimerApellido.Trim().Replace("|", ""), string.IsNullOrEmpty(persona.SegundoApellido) ? splitArr[4] : persona.SegundoApellido.Trim().Replace("|", ""), string.IsNullOrEmpty(persona.FechaNacimiento) ? splitArr[5] : persona.FechaNacimiento.Trim().Replace("|", "")));
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
					message = "La persona con rut '" + persona.Rut + "' no existe en los registros"
				};
			}

			metods.updateLineFile("Personas", content);

			return new
			{
				resultTransaccion = true,
				message = "Se han modificado los datos de la persona con rut " + persona.Rut
			};
		}

		[HttpDelete]
		[Route("EliminarUsuario")]
		public dynamic EliminarUsuario(string rut)
		{
			string[] list = metods.getContentFile("Personas");

			if (list.Count() == 0)
			{
				return new
				{
					resultTransaccion = false,
					message = "No hay personas registrados"
				};
			}

			if (!metods.validarRut(rut))
			{
				return new
				{
					resultTransaccion = false,
					message = "El formato de rut es invalido, formato requerido: 99999999-9"
				};
			}

			bool encontrado = false;
			List<string> content = new List<string>();

			for (int i = 0; i < list.Count(); i++)
			{
				string[] splitArr = list[i].Split("||");

				if (splitArr[0] != rut)
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
					message = "La persona con rut '" + rut + "' no existe en los registros"
				};
			}

			metods.updateLineFile("Personas", content);

			return new
			{
				resultTransaccion = true,
				message = "La persona con rut '" + rut + "' fue eliminada exitosamente"
			};
		}
	}
}
