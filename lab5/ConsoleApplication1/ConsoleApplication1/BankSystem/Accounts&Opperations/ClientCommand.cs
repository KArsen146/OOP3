namespace ConsoleApplication1.BankSystem
{
    public abstract class ClientCommand : Command
    {
        public double Money;
        internal ClientCommand(double money):base()
        {
            Money = money;
        }
        
    }
}