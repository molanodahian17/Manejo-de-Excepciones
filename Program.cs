using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manejo_de_Exepciones
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account("001-123", "Dahian Molano", 500000);

            bool continuar = true;

            while (continuar)
            {
                Console.Clear();

                Console.WriteLine($"Cuenta: {account.NumberAccount}");
                Console.WriteLine($"Titular: {account.Holder}");
                Console.WriteLine($"Saldo: ${account.Balance:N2}");
                Console.WriteLine("===============================");

                Console.WriteLine("1. Depositar dinero");
                Console.WriteLine("2. Retirar dinero");
                Console.WriteLine("3. Consultar saldo");
                Console.WriteLine("4. Ver movimientos");
                Console.WriteLine("5. Salir");
                Console.WriteLine("===============================");
                Console.WriteLine("Seleccione una opcion: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        DepositMoney(account);
                        break;

                    case "2":
                        WithdrawMoney(account);
                        break;

                    case "3":
                        CheckBalance(account);
                        break;

                    case "4":
                        ViewTransactions(account);
                        break;

                    case "5":
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opcion invalida.");
                        break;
                }

            }

        }

        static void DepositMoney(Account account)
        {
            Console.Clear();

            Console.WriteLine("Ingrese el monto a depositar: ");

            decimal amount = decimal.Parse(Console.ReadLine());

            account.Deposit(amount);

            Pause();
        }

        static void WithdrawMoney(Account account)
        {
            Console.Clear();
            Console.WriteLine("Ingrese el monto a retirar: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            account.WithDraw(amount);
            Pause();
        }

        static void CheckBalance(Account account)
        {
            Console.WriteLine($"Saldo actual: ${account.Balance:N2}");
            Pause();
        }

        static void ViewTransactions(Account account)
        {
            Console.WriteLine("Movimientos de la cuenta:");
            foreach (var transaction in account.Transactions)
            {
                transaction.PrintDetails();
            }
            Pause();
        }

        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Presione ENTER para volver al menu");
            Console.ReadLine();
        }



    }
}
