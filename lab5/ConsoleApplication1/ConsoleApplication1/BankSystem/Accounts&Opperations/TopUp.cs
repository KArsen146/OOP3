namespace ConsoleApplication1.BankSystem
{
    public class TopUp : SimpleCommand
    {
        
        public TopUp(Account account, double money):base(account, money)
        {
            Account = account;
            type = 1;
        }

        internal override void Execute()
        {
            Account.AccountBalance += Money;
        }

        internal override void Undo()
        {
            Account.AccountBalance -= Money;
            Account.History.Remove(this);
        }
    }
}