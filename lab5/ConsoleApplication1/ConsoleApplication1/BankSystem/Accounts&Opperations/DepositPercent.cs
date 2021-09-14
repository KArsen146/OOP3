namespace ConsoleApplication1.BankSystem
{
    public class DepositPercent : AccountCommand
    {
        public DepositPercent(double yearPercent, Account account) : base(yearPercent, account)
        {
        }


        internal override void Execute()
        {
            Account.AccountBalance += MonthPercent;
        }

        internal override void Undo()
        {
            Account.AccountBalance -= MonthPercent;
        }
    }
}