namespace FakeBankApi_MSP.Modelos
{
	public class Transaccion
	{
		public string Rut_Persona { get; set; } = string.Empty;
		public string NumeroCuenta { get; set; } = string.Empty;
        public string NumeroTarjeta { get; set; } = string.Empty;
        public string Monto { get; set; } = string.Empty;
		public string TipoTransaccion { get; set; } = string.Empty; 
		public string FechaTransaccion { get; set; } = string.Empty;
	}
}
