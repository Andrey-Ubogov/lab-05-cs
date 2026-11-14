using System;

namespace SmartHome
{
    // Собственное исключение (в C# наследуем от Exception)
    public class DeviceBrokenException : Exception
    {
        public DeviceBrokenException(string deviceName)
            : base($"Устройство '{deviceName}' сломано! Нельзя включить.")
        {
        }
    }
}