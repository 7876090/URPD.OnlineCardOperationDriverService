using System.Text.Json.Serialization;
using URPD.OnlineCardOperationDriverService.DataTypes;

namespace URPD.OnlineCardOperationDriverService.DataTypes
{
    /// <summary>
    /// Описание операции продажи билета
    /// </summary>
    public class TicketSale
    {
        /// <summary>
        /// Описание параметров продажи карты
        /// </summary>
        [JsonPropertyName("CardSale")]
        public CardSale ? CardSale { get; set; }

        /// <summary>
        /// Описание параметров продажи билета
        /// </summary>
        [JsonPropertyName("Ticket")]
        public Ticket ? Ticket { get; set; }
    }
}
