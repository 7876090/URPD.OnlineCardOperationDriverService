using System.Text.Json.Serialization;

namespace URPD.OnlineCardOperationDriverService.DataTypes
{
    /// <summary>
    /// Описание процесса записи билета
    /// </summary>
    public class CardSale
    {
        /// <summary>
        /// 1- нал, 2 - безнал
        /// </summary>
        [JsonPropertyName("PaymentForm")]
        public int PaymentForm { get; set; }

        /// <summary>
        /// Комментарий к операции
        /// </summary>
        [JsonPropertyName("OperationComment")]
        public string OperationComment { get; set; } = "";

        /// <summary>
        /// Идентификатор операции, GUID
        /// </summary>
        [JsonPropertyName("OperationId")]
        public string OperationId { get; set; } = "";

        /// <summary>
        /// Уникальный идентификатор операции автора документа.
        /// Максимально 26 символов.
        /// </summary>
        [JsonPropertyName("IssuerOperationId")]
        public int IssuerOperationId { get; set; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        [JsonPropertyName("OperationCost")]
        public int OperationCost { get; set; }
    }
}
