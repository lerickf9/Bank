using System.Collections.Generic;

namespace FakeBankApi_MSP.Modelos
{
    public class TasaMoneda
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class ListUf
        {
            public List<UF> UFs { get; set; } = new List<UF>();
		}

        public class UF
        {
            public string Valor { get; set; } = string.Empty;
			public string Fecha { get; set; } = string.Empty;
		}

        public class ListDolar
        {
            public List<Dolar> Dolares { get; set; } = new List<Dolar>();
		}

        public class Dolar
        {
            public string Valor { get; set; } = string.Empty;
			public string Fecha { get; set; } = string.Empty;
		}

        public class ListEuro
        {
            public List<Euro> Euros { get; set; } = new List<Euro>();
        }

        public class Euro
        {
            public string Valor { get; set; } = string.Empty;
			public string Fecha { get; set; } = string.Empty;
		}
    }
}
