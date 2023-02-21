using System.Text.Json.Serialization;

namespace URPD.OnlineCardOperationDriverService.DataTypes
{
    /// <summary>
    /// Описание карты
    /// </summary>
    public class SmartCard
    {
        /// <summary>
        /// Внутренний номер карты dec
        /// </summary>
        [JsonPropertyName("SmartCardNativeId")]
        public string SmartCardNativeId { get; set; } = "";

        /// <summary>
        /// Билеты на карте
        /// </summary>
        [JsonPropertyName("Tickets")]
        public List<Ticket> Ticket { get; set; } = new List<Ticket>();
    }
}
