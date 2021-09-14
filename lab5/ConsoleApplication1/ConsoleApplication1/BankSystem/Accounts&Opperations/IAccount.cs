using System;
using System.Collections.Generic;

namespace ConsoleApplication1.BankSystem
{
    public abstract class Account
    {
        internal Account(uint clientId, double money, uint accountId, bool isTrusted, double unTrustedLimit)
        {
            History = new List<Command>();
            ClientId = clientId;
            OpeningDate = DateBank.DateTime;
            AccountBalance = money;
            AccountId = accountId;
            _isTrusted = isTrusted;
            UnTrustedLimit = unTrustedLimit;
        }

        internal readonly List<Command> History;

        protected AccountCommand Accrual;

        public double UnTrustedLimit { get; }

        private bool _isTrusted;
        
        public bool IsTrusted => _isTrusted;

        internal void BecameTrusted()
        {
            _isTrusted = true;
        }
        
        public uint AccountId { get; }
        
        public double AccountBalance;

        public readonly uint ClientId;
        
        public DateTime OpeningDate;
        protected abstract void NewMonth();
        
        public virtual void FinishDay()
        {
            Accrual.AddDailySumm(AccountBalance);
            if (DateBank.DateTime.Subtract(OpeningDate).Days >= 30)
            {
                Accrual.Execute();
                History.Add(Accrual);
                NewMonth();
            }
        }

        internal virtual bool CheckForPossibility(double money, bool undo = true)
        {
            return AccountBalance >= money;
        }
        
    }
}