namespace SmartHome
{
    // Контракт 1: Подключение к сети
    public interface IConnectable
    {
        void ConnectToWiFi();
        void DisconnectFromWiFi();
    }

    // Контракт 2: Режим работы термостата (Объект поведения)
    public interface IHeatingMode
    {
        int GetTargetTemperature();
        string GetModeName();
    }
}