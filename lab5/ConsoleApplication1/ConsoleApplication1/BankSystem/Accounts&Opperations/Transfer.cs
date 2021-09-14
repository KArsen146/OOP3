namespace ConsoleApplication1.BankSystem
{
    public class Transfer : ClientCommand
    {
        internal Transfer(WithDraw from) : base(from.Money)
        {
            type = 3;
            From = from;
            FromAccountId = From.AccountId;
        }

        internal void TransferTo(TopUp to)
        {
            To = to;
            ToAccountId = to.AccountId;
            accountId = ToAccountId;
        }
        protected TopUp To;
        public uint FromAccountId { get; }
        public uint ToAccountId { get; private set; }

        protected WithDraw From;
        internal override void Execute()
        {
            From.Execute();
            To.Execute();
        }

        internal override void Undo()
        {
            To.Undo();
            From.Undo();
        }
    }
}