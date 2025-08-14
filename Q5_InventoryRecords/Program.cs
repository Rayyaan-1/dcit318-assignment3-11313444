using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// -------------------------------
// a. Immutable Inventory Record
// -------------------------------
public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity;

// -------------------------------
// b. Marker Interface
// -------------------------------
public interface IInventoryEntity
{
    int Id { get; }
}

// -------------------------------
// c. Generic Inventory Logger
// -------------------------------
public class InventoryLogger<T> where T : IInventoryEntity
{
    private List<T> _log = new List<T>();
    private string _filePath;

    public InventoryLogger(string filePath)
    {
        _filePath = filePath;
    }

    public void Add(T item)
    {
        _log.Add(item);
    }

    public List<T> GetAll()
    {
        return _log;
    }

    public void SaveToFile()
    {
        try
        {
            string json = JsonSerializer.Serialize(_log, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
            Console.WriteLine($"Data successfully saved to {_filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error saving to file: " + ex.Message);
        }
    }

    public void LoadFromFile()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine("File does not exist, nothing to load.");
                return;
            }

            string json = File.ReadAllText(_filePath);
            _log = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            Console.WriteLine($"Data successfully loaded from {_filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading from file: " + ex.Message);
        }
    }
}

// -------------------------------
// f. Integration Layer - InventoryApp
// -------------------------------
public class InventoryApp
{
    private InventoryLogger<InventoryItem> _logger;

    public InventoryApp(string filePath)
    {
        _logger = new InventoryLogger<InventoryItem>(filePath);
    }

    public void SeedSampleData()
    {
        _logger.Add(new InventoryItem(1, "Laptop", 10, DateTime.Now));
        _logger.Add(new InventoryItem(2, "Keyboard", 25, DateTime.Now));
        _logger.Add(new InventoryItem(3, "Mouse", 30, DateTime.Now));
        _logger.Add(new InventoryItem(4, "Monitor", 15, DateTime.Now));
    }

    public void SaveData()
    {
        _logger.SaveToFile();
    }

    public void LoadData()
    {
        _logger.LoadFromFile();
    }

    public void PrintAllItems()
    {
        Console.WriteLine("\n--- Inventory Items ---");
        foreach (var item in _logger.GetAll())
        {
            Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Date Added: {item.DateAdded}");
        }
    }
}

// -------------------------------
// g. Main Method
// -------------------------------
class Program
{
    static void Main(string[] args)
    {
        string filePath = "inventory.json";

        InventoryApp app = new InventoryApp(filePath);

        // Seed sample data
        app.SeedSampleData();

        // Save to file
        app.SaveData();

        // Clear memory to simulate a new session
        Console.WriteLine("\nSimulating new session...");

        InventoryApp newApp = new InventoryApp(filePath);

        // Load data from file
        newApp.LoadData();

        // Print all loaded items
        newApp.PrintAllItems();
    }
}

