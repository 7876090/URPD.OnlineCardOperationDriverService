using System.Text.Json.Serialization;

namespace URPD.OnlineCardOperationDriverService.DataTypes
{
    /// <summary>
    /// Билет
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// Интервальный
        /// </summary>
        [JsonPropertyName("IsInterval")]
        public bool IsInterval { get; set; }

        /// <summary>
        /// Месячный
        /// </summary>
        [JsonPropertyName("IsMonthly")]
        public bool IsMonthly { get; set; }

        /// <summary>
        /// Месячный комбинированный
        /// </summary>
        [JsonPropertyName("IsMonthlyCombined")]
        public bool IsMonthlyCombined { get; set; }

        /// <summary>
        /// Электронный кошелек
        /// </summary>
        [JsonPropertyName("IsOfflineEWallet")]
        public bool IsOfflineEWallet { get; set; }

        /// <summary>
        /// Удаленное пополнение
        /// </summary>
        [JsonPropertyName("NoBitmap")]
        public bool NoBitmap { get; set; }

        /// <summary>
        /// Тип билета
        /// </summary>
        [JsonPropertyName("TicketType")]
        public int TicketType { get; set; }

        /// <summary>
        /// Подтип билета
        /// </summary>
        [JsonPropertyName("TicketSubtype")]
        public int TicketSubtype { get; set; }

        /// <summary>
        /// Количество билетов
        /// </summary>
        [JsonPropertyName("TicketsCount")]
        public int TicketsCount { get; set; }

        /// <summary>
        /// День начала действия
        /// </summary>
        [JsonPropertyName("StartDay")]
        public int StartDay { get; set; }

        /// <summary>
        /// Месяц начала действия
        /// </summary>
        [JsonPropertyName("StartMonth")]
        public int StartMonth { get; set; }

        /// <summary>
        /// Год начала действия 
        /// </summary>
        [JsonPropertyName("StartYear")]
        public int StartYear { get; set; }

        /// <summary>
        /// День окончания действия
        /// </summary>
        [JsonPropertyName("EndDay")]
        public int EndDay { get; set; }

        /// <summary>
        /// Месяц окончания действия 
        /// </summary>
        [JsonPropertyName("EndMonth")]
        public int EndMonth { get; set; }

        /// <summary>
        /// Год окончания действия
        /// </summary>
        [JsonPropertyName("EndYear")]
        public int EndYear { get; set; }

        /// <summary>
        /// Внутренний номер карты dec, исользуется для удаленного пополнения
        /// </summary>
        [JsonPropertyName("SmartCardNativeId")]
        public string SmartCardNativeId { get; set; } = "";

        /// <summary>
        /// Уникальный идентификатор операции автора документа.
        /// Максимально 26 символов.
        /// </summary>
        [JsonPropertyName("IssuerOperationId")]
        public int IssuerOperationId { get; set; }
    }
}
