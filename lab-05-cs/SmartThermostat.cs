using System;

namespace SmartHome
{
    public sealed class SmartThermostat : BaseDevice
    {
        // Объект поведения!
        public IHeatingMode Mode { get; private set; }

        public SmartThermostat(string name, int power, string roomName, IHeatingMode mode)
            : base(name, power, roomName)
        {
            Mode = mode;
            Console.WriteLine($"[SmartThermostat] Создан термостат: {Name}");
        }

        public override string GetDeviceType() => "Термостат";

        public override string Describe()
        {
            return $"Термостат '{Name}': режим '{Mode.GetModeName()}', целевая температура {Mode.GetTargetTemperature()} C";
        }

        // Динамическое конфигурирование: меняем объект поведения
        public void SetMode(IHeatingMode newMode)
        {
            Mode = newMode;
            Console.WriteLine($"  Термостат '{Name}' переключен в режим: {Mode.GetModeName()}");
        }
    }
}