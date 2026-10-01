using System;
namespace Manejo_de_Exepciones
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public  decimal Amount { get; set; }

        public string Type { get; set; }

        public DateTime Date { get; set; }

        public bool Process()
        {
            try
            {
                if (string.IsNullOrEmpty(Type))
                {
                    throw new InvalidTypeException("Transaction type is required");
                }
                if (Amount <= 0)
                {
                    throw new AmountNegativeException("Transaction amount must be positive");
                }

                if (Type != "Deposit" && Type != "Withdrawal")
                {
                    throw new NotSupportedException($"Transaction type {Type} is not supported");
                }

                Console.WriteLine($"Transaction {TransactionId} processed successfully");
                return true;

            }
            catch (AmountNegativeException ex)
            {
                Console.WriteLine($"Error processimg transaction: {ex.Message}");
                return false;
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Error processimg transaction: {ex.Message}");
                return false;
            }
            catch (InvalidTypeException ex)
            {
                Console.WriteLine($"Error processimg transaction: {ex.Message}");
                return false;
            }
        }

        public void PrintDetails()
        {
                Console.WriteLine($"iD: {TransactionId}, Type: {Type}, Amount: {Amount}, Date: {Date}");
        }

        public class InvalidTypeException : Exception
    {
        public InvalidTypeException(string message) :  base(message) {}
    }
    }
}
