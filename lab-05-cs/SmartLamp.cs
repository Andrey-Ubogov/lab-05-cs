using System;

namespace SmartHome
{
    // Множественное наследование: BaseDevice + IConnectable + IComparable
    public class SmartLamp : BaseDevice, IConnectable, IComparable<SmartLamp>
    {
        public int Brightness { get; private set; }
        private bool _isConnected;

        public SmartLamp(string name, int power, string roomName, int brightness = 100)
            : base(name, power, roomName)
        {
            Brightness = brightness;
            _isConnected = false;
            Console.WriteLine($"[SmartLamp] Создана лампа: {Name}");
        }

        public override string GetDeviceType() => "Умная лампа";

        public override string Describe()
        {
            return base.Describe() + $", яркость: {Brightness}%";
        }

        // РАННЕЕ СВЯЗЫВАНИЕ: скрываем метод базового класса с помощью new!
        public new string GetCategory()
        {
            return "Осветительный прибор";
        }

        // Реализация IConnectable
        public void ConnectToWiFi()
        {
            _isConnected = true;
            Console.WriteLine($"  Лампа '{Name}' подключена к Wi-Fi.");
        }

        public void DisconnectFromWiFi()
        {
            _isConnected = false;
            Console.WriteLine($"  Лампа '{Name}' отключена от Wi-Fi.");
        }

        // Реализация IComparable (для сортировки ламп по яркости)
        public int CompareTo(SmartLamp other)
        {
            if (other == null) return 1;
            return Brightness.CompareTo(other.Brightness);
        }
    }
}