using System;
using System.Collections.Generic;

namespace ConsoleApplication1.BankSystem
{
    public class DepositAccount : Account
    {
        public DepositAccount(uint clientId, double money, List<KeyValuePair<uint, double>> percents, DateTime closingDate, uint accountId, bool isTrusted, double untrustedLimit) : base( clientId, money, accountId, isTrusted,  untrustedLimit)
        {
            int i = 0;
            for (;i < percents.Count - 1; i++)
            {
                if (money < percents[i + 1].Key)
                    break;
            }
            _closingDate = closingDate;
            _yearPercent = percents[i].Value;
            _isClosed = false;
            NewMonth();
        }

        private readonly double _yearPercent;

        private readonly DateTime _closingDate;

        private bool _isClosed;
        protected sealed override void NewMonth()
        {
            if (!_isClosed)
                Accrual = new DepositPercent(_yearPercent, this);
        }

        public override void FinishDay()
        {
            if (!_isClosed)
                base.FinishDay();
            if (_closingDate <= DateBank.DateTime)
            {
                _isClosed = true;
            }

        }

        internal override bool CheckForPossibility(double money, bool undo = true)
        {
            if (!undo)
                return false;
            return base.CheckForPossibility(money);
        }
    }
}