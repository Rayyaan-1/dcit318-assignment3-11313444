using System;
using System.Collections.Generic;
using System.Globalization;

namespace Q1_FinanceManagement
{
   
    // a. Transaction record

    public record Transaction(int Id, DateTime Date, decimal Amount, string Category);

    
    // b. ITransactionProcessor interface
   
    public interface ITransactionProcessor
    {
        void Process(Transaction transaction);
    }

    
    // c. Concrete Transaction Processors

    public class BankTransferProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"BankTransfer processed: GH₵{transaction.Amount:N2} for {transaction.Category}");
        }
    }

    public class MobileMoneyProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"MobileMoney processed: GH₵{transaction.Amount:N2} for {transaction.Category}");
        }
    }

    public class CryptoWalletProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"CryptoWallet processed: GH₵{transaction.Amount:N2} for {transaction.Category}");
        }
    }

  
    // d. Base Account class
    
    public class Account
    {
        public string AccountNumber { get; private set; }
        public decimal Balance { get; protected set; }

        public Account(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public virtual void ApplyTransaction(Transaction transaction)
        {
            Balance -= transaction.Amount;
            Console.WriteLine($"Transaction applied. New balance: GH₵{Balance:N2}");
        }
    }

 
    // e. Sealed SavingsAccount
    
    public sealed class SavingsAccount : Account
    {
        public SavingsAccount(string accountNumber, decimal initialBalance) 
            : base(accountNumber, initialBalance) { }

        public override void ApplyTransaction(Transaction transaction)
        {
            if (transaction.Amount > Balance)
            {
                Console.WriteLine("Insufficient funds");
            }
            else
            {
                Balance -= transaction.Amount;
                Console.WriteLine($"Transaction successful. Updated balance: GH₵{Balance:N2}");
            }
        }
    }

    
    // f. FinanceApp

    public class FinanceApp
    {
        private List<Transaction> _transactions = new List<Transaction>();

        public void Run()
        {
            // Instantiate account
            SavingsAccount account = new SavingsAccount("SA-123456", 1000m);

            // Create sample transactions
            Transaction t1 = new Transaction(1, DateTime.Now, 150m, "Groceries");
            Transaction t2 = new Transaction(2, DateTime.Now, 300m, "Utilities");
            Transaction t3 = new Transaction(3, DateTime.Now, 500m, "Entertainment");

            // Process transactions
            MobileMoneyProcessor mobileProcessor = new MobileMoneyProcessor();
            BankTransferProcessor bankProcessor = new BankTransferProcessor();
            CryptoWalletProcessor cryptoProcessor = new CryptoWalletProcessor();

            mobileProcessor.Process(t1);
            bankProcessor.Process(t2);
            cryptoProcessor.Process(t3);

            // Apply transactions to account
            account.ApplyTransaction(t1);
            account.ApplyTransaction(t2);
            account.ApplyTransaction(t3);

            // Add to transaction list
            _transactions.Add(t1);
            _transactions.Add(t2);
            _transactions.Add(t3);
        }
    }

  
    // Main method
    
    class Program
    {
        static void Main(string[] args)
        {
            FinanceApp app = new FinanceApp();
            app.Run();
        }
    }
}

