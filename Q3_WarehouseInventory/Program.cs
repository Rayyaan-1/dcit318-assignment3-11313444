using System;
using System.Collections.Generic;

// =======================
// a. Marker Interface
// =======================
public interface IInventoryItem
{
    int Id { get; }
    string Name { get; }
    int Quantity { get; set; }
}

// =======================
// b. Product Classes
// =======================
public class ElectronicItem : IInventoryItem
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Quantity { get; set; }
    public string Brand { get; private set; }
    public int WarrantyMonths { get; private set; }

    public ElectronicItem(int id, string name, int quantity, string brand, int warrantyMonths)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        Brand = brand;
        WarrantyMonths = warrantyMonths;
    }
}

public class GroceryItem : IInventoryItem
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Quantity { get; set; }
    public DateTime ExpiryDate { get; private set; }

    public GroceryItem(int id, string name, int quantity, DateTime expiryDate)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        ExpiryDate = expiryDate;
    }
}

// =======================
// e. Custom Exceptions
// =======================
public class DuplicateItemException : Exception
{
    public DuplicateItemException(string message) : base(message) { }
}

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(string message) : base(message) { }
}

public class InvalidQuantityException : Exception
{
    public InvalidQuantityException(string message) : base(message) { }
}

// =======================
// d. Generic Repository
// =======================
public class InventoryRepository<T> where T : IInventoryItem
{
    private Dictionary<int, T> _items = new Dictionary<int, T>();

    public void AddItem(T item)
    {
        if (_items.ContainsKey(item.Id))
            throw new DuplicateItemException($"Item with ID {item.Id} already exists.");
        _items[item.Id] = item;
    }

    public T GetItemById(int id)
    {
        if (!_items.ContainsKey(id))
            throw new ItemNotFoundException($"Item with ID {id} not found.");
        return _items[id];
    }

    public void RemoveItem(int id)
    {
        if (!_items.ContainsKey(id))
            throw new ItemNotFoundException($"Item with ID {id} not found.");
        _items.Remove(id);
    }

    public List<T> GetAllItems()
    {
        return new List<T>(_items.Values);
    }

    public void UpdateQuantity(int id, int newQuantity)
    {
        if (newQuantity < 0)
            throw new InvalidQuantityException("Quantity cannot be negative.");
        if (!_items.ContainsKey(id))
            throw new ItemNotFoundException($"Item with ID {id} not found.");
        _items[id].Quantity = newQuantity;
    }
}

// =======================
// f. WareHouseManager
// =======================
public class WareHouseManager
{
    private InventoryRepository<ElectronicItem> _electronics = new InventoryRepository<ElectronicItem>();
    private InventoryRepository<GroceryItem> _groceries = new InventoryRepository<GroceryItem>();

    public void SeedData()
    {
        // Add Electronics
        _electronics.AddItem(new ElectronicItem(1, "Laptop", 10, "Dell", 24));
        _electronics.AddItem(new ElectronicItem(2, "Smartphone", 20, "Samsung", 12));

        // Add Groceries
        _groceries.AddItem(new GroceryItem(101, "Rice", 50, DateTime.Now.AddMonths(12)));
        _groceries.AddItem(new GroceryItem(102, "Milk", 30, DateTime.Now.AddDays(10)));
    }

    public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
    {
        foreach (var item in repo.GetAllItems())
        {
            Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Qty: {item.Quantity}");
        }
    }

    public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantity) where T : IInventoryItem
    {
        try
        {
            var item = repo.GetItemById(id);
            repo.UpdateQuantity(id, item.Quantity + quantity);
            Console.WriteLine($"Stock updated for item {item.Name}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating stock: {ex.Message}");
        }
    }

    public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
    {
        try
        {
            repo.RemoveItem(id);
            Console.WriteLine($"Item with ID {id} removed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error removing item: {ex.Message}");
        }
    }

    public InventoryRepository<ElectronicItem> Electronics => _electronics;
    public InventoryRepository<GroceryItem> Groceries => _groceries;
}

// =======================
// Main Program
// =======================
class Program
{
    static void Main()
    {
        WareHouseManager manager = new WareHouseManager();
        manager.SeedData();

        Console.WriteLine("\n--- Grocery Items ---");
        manager.PrintAllItems(manager.Groceries);

        Console.WriteLine("\n--- Electronic Items ---");
        manager.PrintAllItems(manager.Electronics);

        // Exception demonstrations
        Console.WriteLine("\n--- Exception Demonstrations ---");

        try
        {
            // Duplicate
            manager.Electronics.AddItem(new ElectronicItem(1, "Tablet", 5, "Apple", 12));
        }
        catch (DuplicateItemException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        try
        {
            // Remove non-existent
            manager.RemoveItemById(manager.Groceries, 999);
        }
        catch (ItemNotFoundException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        try
        {
            // Invalid quantity
            manager.Electronics.UpdateQuantity(1, -5);
        }
        catch (InvalidQuantityException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("\nProgram finished.");
    }
}

