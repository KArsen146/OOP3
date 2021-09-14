namespace ConsoleApplication1.BankSystem
{
    public class DebitAccount : Account
    {
        private readonly double _yearPercent;
        
        public DebitAccount(uint clientid, double money, double yearPercent, uint accountId, bool isTrusted, double untrustedLimit) : base(clientid, money, accountId, isTrusted, untrustedLimit)
        {
            _yearPercent = yearPercent;
            NewMonth();
        }

        protected sealed override void NewMonth()
        {
            Accrual = new DebitPercent(_yearPercent, this);
        }
        
    }
}