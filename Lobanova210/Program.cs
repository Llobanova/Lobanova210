using System;

public class Factory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class ProcessingUnit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int FactoryId { get; set; }
}

public class Tank
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Volume { get; set; }
    public int MaxVolume { get; set; }
    public int UnitId { get; set; }
}

public class Program
{
    public static Tank FindTankByName(Tank[] tanks, string tankName)
    {
        return Array.Find(tanks, tank => tank.Name == tankName);
    }

    public static int CalculateTotalLoad(Tank[] tanks)
    {
        int totalLoad = 0;
        foreach (var tank in tanks)
        {
            totalLoad += tank.Volume;
        }
        return totalLoad;
    }

    public static void Main()
    {
        Factory[] factories = new Factory[]
        {
            new Factory { Id = 1, Name = "НПЗ№1", Description = "Первый нефтеперерабатывающий завод" },
            new Factory { Id = 2, Name = "НПЗ№2", Description = "Второй нефтеперерабатывающий завод" }
        };

        ProcessingUnit[] processingUnits = new ProcessingUnit[]
        {
            new ProcessingUnit { Id = 1, Name = "ГФУ-2", Description = "Газофракционирующая установка", FactoryId = 1 },
            new ProcessingUnit { Id = 2, Name = "АВТ-6", Description = "Атмосферно-вакуумная трубчатка", FactoryId = 1 },
            new ProcessingUnit { Id = 3, Name = "АВТ-10", Description = "Атмосферно-вакуумная трубчатка", FactoryId = 2 }
        };

        Tank[] tanks = new Tank[]
        {
            new Tank { Id = 1, Name = "Резервуар 1", Description = "Надземный-вертикальный", Volume = 1500, MaxVolume = 2000, UnitId = 1 },
            new Tank { Id = 2, Name = "Резервуар 2", Description = "Надземный-горизонтальный", Volume = 2500, MaxVolume = 3000, UnitId = 1 },
            new Tank { Id = 3, Name = "Дополнительный резервуар 24", Description = "Надземный-горизонтальный", Volume = 3000, MaxVolume = 3000, UnitId = 2 },
            new Tank { Id = 4, Name = "Резервуар 35", Description = "Надземный-вертикальный", Volume = 3000, MaxVolume = 3000, UnitId = 2 },
            new Tank { Id = 5, Name = "Резервуар 47", Description = "Подземный-двустенный", Volume = 4000, MaxVolume = 5000, UnitId = 2 },
            new Tank { Id = 6, Name = "Резервуар 256", Description = "Подводный", Volume = 500, MaxVolume = 500, UnitId = 3 }
        };

        // Вывод информации о резервуарах
        foreach (var tank in tanks)
        {
            var processingUnit = processingUnits.FirstOrDefault(p => p.Id == tank.UnitId);
            if (processingUnit != null)
            {
                var factory = factories.FirstOrDefault(f => f.Id == processingUnit.FactoryId);
                Console.WriteLine($"Tank: {tank.Name}, Description: {tank.Description}, Volume: {tank.Volume}, MaxVolume: {tank.MaxVolume}");
                if (factory != null)
                {
                    Console.WriteLine($"Processing Unit: {processingUnit.Name}, Factory: {factory.Name}");
                }
            }
        }

        // Вычисление общей загрузки всех резервуаров
        int totalLoad = CalculateTotalLoad(tanks);
        Console.WriteLine($"\nОбщая загрузка всех резервуаров: {totalLoad}");

        // Поиск резервуара по наименованию
        Console.Write("\nВведите имя резервуара для поиска: ");
        string tankName = Console.ReadLine();

        Tank foundTank = FindTankByName(tanks, tankName);

        if (foundTank != null)
        {
            Console.WriteLine($"{foundTank.Name} найден. Описание: {foundTank.Description}");

            // Использование switch для вывода информации о типе резервуара
            switch (foundTank.Description)
            {
                case "Надземный-вертикальный":
                    Console.WriteLine("Это надземный вертикальный резервуар.");
                    break;
                case "Надземный-горизонтальный":
                    Console.WriteLine("Это надземный горизонтальный резервуар.");
                    break;
                case "Подземный-двустенный":
                    Console.WriteLine("Это подземный двустенный резервуар.");
                    break;
                default:
                    Console.WriteLine("Тип резервуара не определен.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Резервуар не найден.");
        }
    }
}