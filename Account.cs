using System;
using System.Collections.Generic;

namespace Manejo_de_Exepciones
{
    public class Account
    {
        public string NumberAccount { get; set; }

        public string Holder { get; set; }

        public decimal Balance { get; private set; }

        public List<Transaction> Transactions { get; set; }

        private int _transactionId = 1;

        public Account (string numberAccount, string holder, decimal initialbalance)
        {
            NumberAccount = numberAccount;
            Holder = holder;
            Balance = initialbalance;

            Transactions =  new List<Transaction>();

        }
        public void Deposit(decimal amount)
        {
            try
            {
                if (amount <= 0)
                {
                    throw new AmountNegativeException("Deposit amount must be positive");
                }

                Balance += amount;

                Transaction transaction = new Transaction
                {
                    TransactionId = _transactionId++,
                    Amount = amount,
                    Type = "Deposit",
                    Date = DateTime.Now
                };

                Transactions.Add(transaction);

                Console.WriteLine($"Deposit Succesful. New balance: {Balance}");
            }
            catch (AmountNegativeException ex)
            {  
                Console.WriteLine($"Deposit failed: {ex.Message}"); 
            }

        }
        public void WithDraw (decimal amount)
        {
            try
            {
                if (amount <= 0)
                {
                    throw new AmountNegativeException("Amount must be positive");
                }

                if (amount > Balance)
                {
                    throw new InsufficientFundsException("Insufficient funds for withDraw");
                }

                Balance -= amount;

                Transaction transaction = new Transaction
                {
                    TransactionId = _transactionId++,
                    Amount = amount,
                    Type = "WithDrawal",
                    Date = DateTime.Now
                };
                Transactions.Add(transaction);

                Console.WriteLine($"Withdrawal successful. New balance: {Balance}");
            }
            catch (AmountNegativeException ex)
            {
                Console.WriteLine($"Withdrawal failed: {ex.Message}");
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"Withdrawal failed: {ex.Message}");
            }
        }

        public void ShowTransactions()
        {
            Console.WriteLine();
            Console.WriteLine("======== TRANSACTIONS ========");

            try
            {
                if (Transactions.Count == 0)
                {
                    throw new TransactionNotFoundException("Transactions not found.");
                }
                foreach (Transaction transaction in Transactions)
                {
                    transaction.PrintDetails();
                }

                Console.WriteLine("===============================================");
            
            }
            catch (TransactionNotFoundException ex)
            {
                
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        public void ExportStatementToFile(string filePath)
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath))
            {
                try
                {
                    writer.WriteLine($"Account: {NumberAccount}");
                    writer.WriteLine($"Holder: {Holder}");
                    writer.WriteLine($"Balance: {Balance}");

                    if (Balance < 0)
                    {
                        throw new InvalidOperationException("Cannot export account with negative balance");
                    }
                }
                catch (InvalidOperationException)
                {
                    writer.WriteLine("Error:Invalid operation");
                }
                 
            }
            
        }

    }
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(string message) :  base(message) {}
    }

    public class AmountNegativeException : Exception
    {
        public AmountNegativeException(string message) :  base(message) {}
    }

    public class TransactionNotFoundException : Exception
    {
        public TransactionNotFoundException(string message) :  base(message) {}
    }
}
