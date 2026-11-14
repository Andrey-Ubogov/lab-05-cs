using System;
using System.Text;

namespace SmartHome
{
    public static class Program
    {
        // Работа через интерфейс
        public static void ConnectDeviceToNetwork(IConnectable device)
        {
            Console.WriteLine("Подключение к сети...");
            device.ConnectToWiFi();
        }

        // Проверка типа через 'is' и 'as' (аналог dynamic_cast)
        public static void TryToConnect(BaseDevice device)
        {
            if (device is IConnectable connectable)
            {
                connectable.ConnectToWiFi();
            }
            else
            {
                Console.WriteLine($"  Устройство '{device.Name}' не поддерживает Wi-Fi.");
            }

            // Альтернативный вариант через as:
            // var c = device as IConnectable;
            // if (c != null) c.ConnectToWiFi();
        }

        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("========== ЛР 5. Часть 2: C# ==========\n");

            // 1. Раннее и позднее связывание
            Console.WriteLine("--- 1. Раннее и позднее связывание ---");
            BaseDevice baseLamp = new SmartLamp("Лампа 1", 10, "Гостиная");

            // Раннее связывание (сработает метод базы, т.к. указатель типа BaseDevice)
            Console.WriteLine($"Раннее связывание (GetCategory): {baseLamp.GetCategory()}");

            // Позднее связывание (сработает метод наследника)
            Console.WriteLine($"Позднее связывание (Describe): {baseLamp.Describe()}");

            // 2. Проверка типа и интерфейсы
            Console.WriteLine("\n--- 2. Проверка типа (is/as) и интерфейсы ---");
            var lamp = new SmartLamp("Умная лампа", 10, "Гостиная");
            var thermostat = new SmartThermostat("Термостат", 15, "Гостиная", new ComfortMode());

            TryToConnect(lamp);       // Сработает is
            TryToConnect(thermostat); // Не сработает (не реализует IConnectable)

            ConnectDeviceToNetwork(lamp); // Прямая работа через интерфейс

            // 3. Конфигурирование поведения
            Console.WriteLine("\n--- 3. Конфигурирование поведения ---");
            Console.WriteLine($"До смены режима: {thermostat.Describe()}");
            thermostat.SetMode(new EconomyMode()); // Динамическое конфигурирование
            Console.WriteLine($"После смены режима: {thermostat.Describe()}");

            // 4. Исключения и finally
            Console.WriteLine("\n--- 4. Исключения и finally ---");
            try
            {
                baseLamp.TurnOn(); // Успешно
                baseLamp.Break();  // Ломаем
                baseLamp.TurnOn(); // Бросает исключение!
            }
            catch (DeviceBrokenException ex)
            {
                Console.WriteLine($"Поймано исключение: {ex.Message}");
            }
            finally
            {
                // Выполнится в любом случае
                Console.WriteLine("Блок finally выполнен.");
            }

            // 5. Стандартный интерфейс IComparable
            Console.WriteLine("\n--- 5. Стандартный интерфейс IComparable ---");
            SmartLamp[] lamps =
            {
                new SmartLamp("Лампа A", 10, "Спальня", 80),
                new SmartLamp("Лампа B", 10, "Кухня", 30),
                new SmartLamp("Лампа C", 10, "Зал", 100)
            };

            Console.WriteLine("До сортировки:");
            foreach (var l in lamps) Console.WriteLine($"  {l.Name} (яркость {l.Brightness}%)");

            Array.Sort(lamps); // Сортирует благодаря IComparable

            Console.WriteLine("После сортировки (по возрастанию яркости):");
            foreach (var l in lamps) Console.WriteLine($"  {l.Name} (яркость {l.Brightness}%)");

            Console.WriteLine("\n========== КОНЕЦ ==========");
        }
    }
}