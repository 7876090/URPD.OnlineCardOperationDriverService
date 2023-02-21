using System.Reflection;
using URPD.OnlineCardOperationDriverService.DataTypes;

namespace URPD.OnlineCardOperationDriverService.Services
{
    /// <summary>
    /// Интерфейс взаимодействия с IZProtokolDriver
    /// </summary>
    public class DriverService : IHostedService
    {
        private const int NO_ERROR_CODE = 0;
        private const string NO_ERROR_CODE_DESCRIPTION = "Ошибок нет.";
        private const int LOAD_ERROR_CODE = -1;
        private const string LOAD_ERROR_CODE_DESCRIPTION = "Ошибка загрузки компоненты  ZProtokolDriver. Возможно компонента не установлена!";
        private const string DRIVER_PROG_ID = "OpenAgami.OnlineCardOperationsDriver.Spb";

        private Type ? objType;
        private object ? obj;
        private string ? resultCodeDescription;
        private int resultCode;

        /// <summary>
        /// Описание результата выполнения
        /// </summary>
        public string ? ResultCodeDescription { get { return resultCodeDescription; } }

        /// <summary>
        /// Код результата выполнения 
        /// </summary>
        public int ResultCode { get { return resultCode; } }

        /// <summary>
        /// Внутренний номер карты dec
        /// </summary>
        public string SmartCardNativeId
        {
            get
            {
                var value = getProperty("SmartCardNativeId") ?? "";
                return (string)value;
            }
        }

        #region Initialization
        private void setError(int errorCode, string errorDescription)
        {
            resultCode = errorCode;
            resultCodeDescription = errorDescription;
        }
        private void resetError()
        {
            resultCode = NO_ERROR_CODE;
            resultCodeDescription = NO_ERROR_CODE_DESCRIPTION;
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public DriverService()
        {
            resetError();
        }

        /// <summary>
        /// Проверка состояния сервиса
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            resetError();
            bool result = true;
            if (objType == null)
            {
                objType = Type.GetTypeFromProgID(DRIVER_PROG_ID);
                if (objType != null)
                {
                    try
                    {
                        obj = Activator.CreateInstance(objType);
                    }
                    catch (Exception ex)
                    {
                        setError(-1, ex.Message);
                        result = false;
                    }
                }
                else
                {
                    setError(LOAD_ERROR_CODE, LOAD_ERROR_CODE_DESCRIPTION);
                }
            }

            return result;
        }
        private object? getProperty(string name)
        {
            object? value = null;
            if (objType != null)
            {
                value = objType.InvokeMember(name, BindingFlags.GetProperty, null, obj, null);
            }

            return value;
        }
        private bool runetimeError()
        {
            resultCode = (int)getProperty("ResultCode");
            if(resultCode != NO_ERROR_CODE)
            {
                resultCodeDescription = (string)getProperty("ResultCodeDescription");
            }

            return resultCode != NO_ERROR_CODE;
        }
        private void setProperty(string name, object value)
        {
            if (objType != null)
            {
                objType.InvokeMember(name, BindingFlags.SetProperty, null, obj, new object[] { value });
            }
        }
        private async Task<bool> executeMethod(string methodName)
        {
            return (int)objType.InvokeMember(methodName, BindingFlags.InvokeMethod, null, obj, new object[] { }) == 0;
        }
        #endregion

        #region Driver methods
        /// <summary>
        /// Открыть настройки IZProtokolDriver
        /// </summary>
        /// <returns></returns>
        public async Task ShowProperties()
        {
            if(Init())
            {
                await executeMethod("ShowProperties");
            }
        }

        /// <summary>
        /// Получить внутренний номер карты dec
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetSmartCardNativeId()
        {
            bool result = false;
            if(Init())
            {
                await executeMethod("ActivateCardWithoutSam");
                result = !runetimeError();
            }

            return result;
        }

        /// <summary>
        /// Продвжа билета
        /// </summary>
        /// <param name="ticketSale"></param>
        /// <returns></returns>
        public async Task<bool> TicketSale(TicketSale ticketSale)
        {
            bool result = false;

            return result;
        }
        #endregion

        #region IHostedService
        /// <summary>
        /// Реализация интерфейса IHostedService
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Init();

            if (resultCode == 0)
            {
                return Task.CompletedTask;
            }
            else
            {
                return Task.FromException(new Exception(resultCodeDescription));
            }
        }

        /// <summary>
        /// Реализация интерфейса IHostedService
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            obj = null;
            objType = null;

            return Task.CompletedTask;
        }
        #endregion
    }
}
