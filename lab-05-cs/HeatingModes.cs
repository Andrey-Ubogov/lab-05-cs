namespace SmartHome
{
    public class EconomyMode : IHeatingMode
    {
        public int GetTargetTemperature() => 18;
        public string GetModeName() => "Экономия";
    }

    public class ComfortMode : IHeatingMode
    {
        public int GetTargetTemperature() => 23;
        public string GetModeName() => "Комфорт";
    }
}