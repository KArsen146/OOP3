namespace ConsoleApplication1.BankSystem
{
    public abstract class Command
    {
        private static uint _commandId = 0;
        public readonly uint CommandId;
        public int type;
        public uint AccountId=> accountId;

        protected uint accountId;
        public Command()
        {
            CommandId = ++_commandId;
        }

        internal abstract void Execute();
        internal abstract void Undo();
    }
}