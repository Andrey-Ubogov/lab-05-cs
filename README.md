# Лабораторная работа №5. Полиморфизм

## Часть 2. C# (интерфейсы, связывание, исключения)

## Тема проекта
**Симулятор умного дома**

Предметная область описывает виртуальный умный дом. В этой части проекта
добавлены интерфейсы, продемонстрировано раннее и позднее связывание,
динамическое конфигурирование поведения, стандартный интерфейс `IComparable`
и замена проверок правил на исключения.

---

## Структура проекта

lab-05-cs/
├── Interfaces.cs # Интерфейсы IConnectable и IHeatingMode
├── Exceptions.cs # Собственный класс исключения DeviceBrokenException
├── HeatingModes.cs # Реализации режимов (EconomyMode, ComfortMode)
├── BaseDevice.cs # Абстрактный базовый класс
├── SmartLamp.cs # Наследник + IConnectable + IComparable
├── SmartThermostat.cs # Наследник + конфигурирование поведения
├── Program.cs # Демонстрационная программа
├── lab-05-cs.csproj # Файл проекта
└── README.md # Этот файл

## Иерархия классов


### 1. Интерфейс `IConnectable`
**Контракт:** «устройство умеет подключаться к Wi-Fi».

В C# интерфейс объявляется ключевым словом `interface` и содержит только
сигнатуры методов (без реализации и без полей):
- `void ConnectToWiFi();`
- `void DisconnectFromWiFi();`

### 2. Множественное наследование
`SmartLamp` наследует **и** `BaseDevice` (состояние), **и** `IConnectable`
(контракт), **и** `IComparable` (стандартный контракт для сортировки):
```csharp
public class SmartLamp : BaseDevice, IConnectable, IComparable<SmartLamp>

### 1. `BaseDevice` — абстрактный класс
**Ответственность:** общее состояние и поведение для всех умных устройств.

**Поля (private):**
- `_lastId` — статический счётчик ID;
- `Id`, `Name`, `Power`, `RoomName` — свойства (properties).
- `IsOn`, `IsBroken` — свойства с `protected set`.

**Методы:**
- `abstract string GetDeviceType()` — чисто виртуальный аналог.
- `virtual string Describe()` — может быть переопределён.
- `override bool Equals(object obj)` и `override int GetHashCode()` — сравнение по содержимому (имя и мощность).

### 2. `SmartLamp` — наследник
**Ответственность:** умная лампа с яркостью.

**Переопределяет:**
- `GetDeviceType()` — возвращает `"Умная лампа"`;
- `Describe()` — вызывает `base.Describe()` и дополняет яркостью.

**Копирование:**
- `ShallowCopy()` — через `MemberwiseClone()`;
- `DeepCopy()` — через вызов конструктора (независимая копия).

### 3. `SmartThermostat` — наследник (`sealed`)
**Ответственность:** термостат с целевой температурой.

**Переопределяет:**
- `GetDeviceType()` — возвращает `"Термостат"`;
- `Describe()` — полностью заменяет описание, **не вызывая** `base.Describe()`.

`sealed` запрещает дальнейшее наследование.

---

## Сборка и запуск из командной строки

### Требования
- .NET SDK 8.0 или новее.

### Команды
```bash
cd lab-05-cs
dotnet build
dotnet run