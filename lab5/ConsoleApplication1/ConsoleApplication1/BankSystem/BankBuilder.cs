using System;
using System.Collections.Generic;

namespace ConsoleApplication1.BankSystem
{
    public class BankBuilder
    {
        private readonly uint _bankId;

        private readonly string _bankName;
        
        private double _debitPercent;

        private List<KeyValuePair<uint, double>> _depositPercents;

        private DateTime _depositClosingDate;

        private double _loanComission;

        private double _loanLimit;

        private double _untrustedClientLimit;

        internal BankBuilder(string bankName, uint bankId)
        {
            _bankId = bankId;
            _bankName = bankName;
            _debitPercent = 0.05;
            _depositPercents=new List<KeyValuePair<uint, double>>();
            _loanComission = 0.01;
        }

        public void AddDebitPercent(double debitPercent)
        {
            try
            {
                if (debitPercent <= 0)
                    throw new Exception("Percent must be above 0");
                _debitPercent = debitPercent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddDepositPercent(List<KeyValuePair<uint, double>> percents)
        {
            try
            {
                foreach (var pair in percents)
                {
                    if (pair.Value <= 0)
                        throw new Exception("Percent's must be above 0");
                }

                _depositPercents = percents;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddLoanComission(double comission)
        {
            try
            {
                if (comission <= 0)
                    throw new Exception("Percent must be above 0");
                _loanComission = comission;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        public void AddLoanLimit(double limit)
        {
            try
            {
                if (limit >= 0)
                    throw new Exception("Percent must be above 0");
                _loanLimit = limit;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddDepositClosingDate(DateTime depositClosingDate)
        {
            _depositClosingDate = depositClosingDate;
        }

        public void AddUntrustedClientLimit(double limit)
        {
            try
            {
                if (limit < 0)
                    throw new Exception("Percent must be above 0");
                _untrustedClientLimit = limit;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public Bank GetResult()
        {
            var b = new Bank(_bankId, _bankName, _debitPercent, _depositPercents, _depositClosingDate, _loanComission, _loanLimit, _untrustedClientLimit);
            BankManager.AddBank(b);
            return b;
        }
    }
}