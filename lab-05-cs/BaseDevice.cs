using System;

namespace SmartHome
{
    public abstract class BaseDevice
    {
        private static int _lastId = 0;

        public int Id { get; }
        public string Name { get; }
        public bool IsOn { get; protected set; }
        public bool IsBroken { get; protected set; }
        public int Power { get; }
        public string RoomName { get; protected set; }

        protected BaseDevice(string name, int power, string roomName)
        {
            Id = ++_lastId;
            Name = name;
            Power = power;
            RoomName = roomName;
            IsOn = false;
            IsBroken = false;
            Console.WriteLine($"[BaseDevice] Создано устройство #{Id}: {Name}");
        }

        public abstract string GetDeviceType();

        public virtual string Describe()
        {
            return $"Устройство #{Id} '{Name}' ({Power} Вт, комната: {RoomName}), тип: {GetDeviceType()}";
        }

        public string GetCategory()
        {
            return "Обычное устройство";
        }

        public void TurnOn()
        {
            if (IsBroken)
            {
                throw new DeviceBrokenException(Name);
            }
            IsOn = true;
            Console.WriteLine($"  Устройство '{Name}' включено.");
        }

        public void Break()
        {
            IsBroken = true;
            IsOn = false;
            Console.WriteLine($"  Устройство '{Name}' сломалось!");
        }
    }
}