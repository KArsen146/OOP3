namespace ConsoleApplication1.BankSystem
{
    public class LoanAccount : Account
    {
        public readonly double Limit;
        public LoanAccount(uint clientId, double money, double comission, double limit, uint accountId, bool isTrusted, double untrustedLimit) : base( clientId, money, accountId, isTrusted, untrustedLimit)
        {
            _yearComission = comission;
            Limit = limit;
            NewMonth();
        }

        private readonly double _yearComission;
        protected sealed override void NewMonth()
        {
            Accrual = new CreditComission(_yearComission, this);
        }

        internal override bool CheckForPossibility(double money, bool undo = true)
        {
            return AccountBalance - money >= Limit;
        }
    }
}