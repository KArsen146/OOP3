namespace ConsoleApplication1.BankSystem
{
    public class WithDraw : SimpleCommand
    {
        public WithDraw(Account account, double money) : base(account, money)
        {
            type = 2;
        }

        internal override void Execute()
        {
            Account.AccountBalance -= Money;
        }

        internal override void Undo()
        {
            Account.AccountBalance += Money;
        }
        
    }
}