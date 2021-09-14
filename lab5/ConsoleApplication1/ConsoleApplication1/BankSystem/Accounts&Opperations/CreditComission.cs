namespace ConsoleApplication1.BankSystem
{
    public class CreditComission : AccountCommand
    {
        public CreditComission(double yearPercent, Account account) : base(yearPercent, account)
        {
        }

        internal override void Execute()
        {
            Account.AccountBalance += MonthPercent;
        }
        internal override void AddDailySumm(double balance)
        {
            if (balance < 0)
                MonthPercent += balance * DailyPercent;
        }
        internal override void Undo()
        {
            Account.AccountBalance -= MonthPercent;
        }
        
    }
}