# DCIT318 Assignment 3 – 

This project is a comprehensive collection of C# console applications demonstrating object-oriented programming, generics, records, interfaces, exception handling, and file operations. It contains multiple systems that work together to show practical software engineering concepts.

---

## 1. Finance Management System

**Purpose:** Tracks transactions and account balances using immutable C# records, interfaces, and sealed classes.

- **Transaction Record:**  
  Represents a transaction with properties:
  - `Id` – Transaction ID
  - `Date` – Date of transaction
  - `Amount` – Transaction amount in GHS (Cedis)
  - `Category` – Transaction category (e.g., Groceries, Utilities)

- **Transaction Processors:**  
  Implement the `ITransactionProcessor` interface with the method `Process(Transaction transaction)`:
  - `BankTransferProcessor`
  - `MobileMoneyProcessor`
  - `CryptoWalletProcessor`  
  Each prints a message showing the transaction amount and category in Cedis.

- **Account Classes:**
  - `Account` – Base class with account number and balance.
  - `SavingsAccount` – Sealed class overriding `ApplyTransaction` to prevent overdrawing. Displays "Insufficient funds" when necessary and prints updated balance in GHS.

- **FinanceApp:**  
  Demonstrates system integration by creating a `SavingsAccount`, processing sample transactions (Groceries, Utilities, Entertainment), applying them to the account, and storing them in a list for tracking.

---

## 2. Healthcare Management System

**Purpose:** Manages patients and prescriptions using a repository pattern.

- **Patient Class:** Contains ID, Name, Age, Gender.
- **Prescription Class:** Contains ID, PatientID, MedicationName, Dosage, Duration.
- **HealthSystemApp:** Allows adding, updating, and removing patients and prescriptions. Uses exception handling to ensure operations maintain data integrity.

---

## 3. Warehouse Inventory Management System

**Purpose:** Demonstrates generics, collections, and custom exception handling.

- **IInventoryItem Interface:** Marker interface with properties:
  - `Id`, `Name`, `Quantity`

- **Product Classes:**  
  - `ElectronicItem`
  - `GroceryItem`  
  Both implement `IInventoryItem`.

- **InventoryRepository<T> Class:** Generic repository using a dictionary keyed by item ID. Methods:
  - `AddItem`, `GetItemById`, `RemoveItem`, `UpdateQuantity`, `GetAllItems`  
  Custom exceptions: `DuplicateItemException`, `ItemNotFoundException`, `InvalidQuantityException`.

- **WareHouseManager:** Manages grocery and electronic inventory, seeds sample data, and prints items. All modifying operations use try-catch blocks.

---

## 4. Student Grading System

**Purpose:** Reads student records from a `.txt` file, validates data, assigns grades, and writes a summary report.

- **Student Class:** Contains `Id`, `FullName`, `Score` with `GetGrade()` method:
  - 80–100 → "A"
  - 70–79 → "B"
  - 60–69 → "C"
  - 50–59 → "D"
  - Below 50 → "F"

- **Custom Exceptions:**  
  - `InvalidScoreFormatException` – Score cannot be converted to integer  
  - `MissingFieldException` – Missing fields in input

- **StudentResultProcessor:** Reads data line by line using `StreamReader`, validates fields, converts scores, and stores valid students. Writes a formatted report to a `.txt` file.

- **Example Students:**  
  Kwame Mensah, Abena Owusu, Kojo Antwi, Ama Adomako, Esi Boateng.  
  Grades reflect their scores, and errors in input are handled with try-catch blocks.

- **Note:** Ensure `students.txt` is in the project folder to run this program successfully.

---

## 5. Inventory Records System

**Purpose:** Uses immutable C# records for data representation with file persistence.

- **InventoryItem Record:** Contains `Id`, `Name`, `Quantity`, `DateAdded`.
- **IInventoryEntity Interface:** Marker interface for logging.
- **InventoryLogger<T>:** Generic class to manage inventory lists and persist to JSON files:
  - Methods: `Add`, `GetAll`, `SaveToFile`, `LoadFromFile`
  - Exception handling wraps all file operations.

- **InventoryApp:** Seeds sample items (Laptop, Keyboard, Mouse, Monitor), saves to `inventory.json`, clears memory, loads back, and prints all items with ID, Name, Quantity, and DateAdded.






