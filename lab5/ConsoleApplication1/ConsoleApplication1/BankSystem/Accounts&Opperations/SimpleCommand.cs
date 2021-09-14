namespace ConsoleApplication1.BankSystem
{
    public abstract class SimpleCommand:ClientCommand
    {
        protected Account Account;

        internal SimpleCommand(Account account, double money):base(money)
        {
            Account = account;
            accountId = Account.AccountId;
        }

        internal abstract override void Execute();

        internal abstract override void Undo();
    }
}