namespace FakeBankApi_MSP.Modelos
{
	public class Tarjeta
	{
		public string Rut_Persona { get; set; } = string.Empty;
		public string NumeroTarjeta { get; set; } = string.Empty;
		public int Cvv { get; set; }
		public int ClaveTarjeta { get; set; }
		public string NumeroCuenta { get; set; } = string.Empty;
		public string TipoTarjeta { get; set; } = string.Empty;
		public string FechaCreacion { get; set; } = string.Empty;
		public string FechaExpiracion { get; set; } = string.Empty;
		public bool Activa { get; set; }

	}
}
