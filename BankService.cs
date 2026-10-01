using System;
namespace Manejo_de_Exepciones
{
    internal class BankService
    {

        public void ValidatePositiveAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
            }
        }

    

        public decimal CalculateInterest(Account account, decimal rate)
        {
            try
            {
                if (rate < 0 || rate > 100)
                {
                    throw new ArgumentException("Interest rate must be  between 0 and 100");
                }

                decimal interest = account.Balance * (rate / 100);
                return interest;
            }
            catch (ArgumentException ex)
            {
                LogError(ex.Message);
                throw new WrongInterestException("Failed to calculate interest due to invalid rate");
            }
        }

        public decimal GetAccountBalance(Account account)
        {
            try
            {
            if (account == null)
                {
                    throw new NullAccountException(nameof(account));
                }  

                return account.Balance;
            }
            catch (NullAccountException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        private void LogError(string message)
        {
            System.IO.File.AppendAllText("error.log", $"[{DateTime.Now}] {message}");
        }

        public class WrongInterestException : Exception
        {
            public WrongInterestException(string message) :  base(message) {}
        }

        public class NullAccountException : Exception
        {
            public NullAccountException(string message) :  base(message) {}
        }
    }

}
