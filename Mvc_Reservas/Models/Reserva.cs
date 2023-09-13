using System.ComponentModel;

namespace Mvc_Reservas.Models
{
    public class Reserva
    {
        public Guid Id { get; set; }
        [DisplayName("Acomodação")]
        public string Acomodacao { get; set; }
        public DateOnly DataChegada { get; set; }
        [DisplayName("Saída")]
        public DateOnly DataSaida { get; set; }
        [DisplayName("Café Incluso")]
        public bool CafeIncluso { get; set; }
        [DisplayName("Valor")]
        public decimal Total { get; set; }
    }
}
