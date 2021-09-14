using System;

namespace ConsoleApplication1.BankSystem
{
    public abstract class AccountCommand : Command
    {
        protected double MonthPercent;

        protected DateTime OpenningDate;
        protected double DailyPercent { get; }

        protected readonly Account Account;

        protected AccountCommand(double yearPercent, Account account):base()
        {
            MonthPercent = 0;
            Account = account;
            OpenningDate = DateBank.DateTime;
            DailyPercent = yearPercent / 36500;
            type = 0;
        }
        
        internal virtual void AddDailySumm(double balance)
        {
            MonthPercent += balance * DailyPercent;
        }

        internal abstract override void Execute();

        internal abstract override void Undo();
    }
}