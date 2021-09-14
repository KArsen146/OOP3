using System;
using System.Collections.Generic;
using ConsoleApplication1.BankSystem;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                DateBank.DateTime = DateTime.Today;
                Client A = new Client("a", "b");
                A.AddAdress("lol");
                A.AddPassportNumber(1234567890);
                Client A2 = new Client("B", "B");
                BankBuilder B = BankManager.StartBankCreation("bank");
                BankBuilder B2 = BankManager.StartBankCreation("bank2");
                B.AddDebitPercent(5);
                B2.AddDebitPercent(5);
                var a = new List<KeyValuePair<uint, double>>();
                a.Add(new KeyValuePair<uint, double>(0, 100));
                B.AddDepositPercent(a);
                B.AddLoanComission(3);
                B.AddLoanLimit(-10000);
                B2.AddDepositPercent(a);
                B2.AddDepositClosingDate(DateBank.DateTime.AddDays(54));
                B.AddDepositClosingDate(DateBank.DateTime.AddDays(53));
                B2.AddLoanComission(3);
                B.AddUntrustedClientLimit(100000);
                B2.AddLoanLimit(-10000);
                var bank = B.GetResult();
                var bank2 = B2.GetResult();
                var acc = bank.CreateAccount(A.ClientId, Bank.AccountType.Loan, 1000);
                var acc2 = bank2.CreateAccount(A2.ClientId, Bank.AccountType.Debit, 1000);
                var c = A.Transfer(acc, acc2, 100);
                for (var i = 0; i < 100; i++)
                {
                    DateBank.DateTime = DateBank.DateTime.AddDays(1);
                    BankManager.UpdateTime();
                }
                A2.Undo(c);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}