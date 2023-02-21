using System.Text.Json.Serialization;

namespace URPD.OnlineCardOperationDriverService.DataTypes
{
    /// <summary>
    /// Описание результат операции 
    /// </summary>
    public class ResultDescription
    {
        /// <summary>
        /// Код результата
        /// </summary>
        [JsonPropertyName("ResultCode")]
        public int ResultCode { get; set; }

        /// <summary>
        /// Описание результата
        /// </summary>
        [JsonPropertyName("ResultCodeDescription")]               
        public string ? ResultCodeDescription { get; set; }

        /// <summary>
        /// Опиание карты
        /// </summary>
        [JsonPropertyName("SmartCardDescription")]
        public SmartCard SmartCard { get; set; } = new SmartCard();
    }
}
