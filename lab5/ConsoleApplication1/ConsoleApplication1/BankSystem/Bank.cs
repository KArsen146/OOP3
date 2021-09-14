using System;
using System.Collections.Generic;

namespace ConsoleApplication1.BankSystem
{
    public class Bank
    {
        public readonly string Bankname;

        public double DebitPercent { get; }
        
        public uint BankId { get; }

        private readonly Dictionary<uint, Account> _accounts;

        public List<KeyValuePair<uint, double>> DepositPercent { get; }

        public DateTime DepositClosingDate;

        public double LoanComission { get; }
        
        public double LoanLimit { get; }

        public double UntrustedClientLimit { get; }

        internal Bank(uint bankId, string bankname, double debitPercent, List<KeyValuePair<uint, double>> depositPercent, 
            DateTime depositClosingDate, double loanComission, double loanLimit, double untrustedClientLimit)
        {
            BankId = bankId;
            Bankname = bankname;
            DebitPercent = debitPercent;
            DepositPercent = depositPercent;
            DepositClosingDate = depositClosingDate;
            LoanComission = loanComission;
            LoanLimit = loanLimit;
            UntrustedClientLimit = untrustedClientLimit;
            _accounts=new Dictionary<uint, Account>();
            _accountId = 1000 * BankId;
        }

        public enum AccountType
        {
            Debit,
            Deposit,
            Loan
        }

        private uint _accountId;
        public uint CreateAccount(uint clientid, AccountType type, double money)
        {
            bool isTrusted = BankManager.IsClientTrusted(clientid);
            Account account;
            switch (type)
            {
                   
                case AccountType.Debit:
                {
                    
                    account = new DebitAccount(clientid , money, DebitPercent, ++_accountId, isTrusted, UntrustedClientLimit);
                    break;
                }
                case AccountType.Deposit:
                {
                    account = new DepositAccount(clientid, money, DepositPercent, DepositClosingDate, ++_accountId, isTrusted, UntrustedClientLimit);
                    break;
                }
                default:
                {
                    account = new LoanAccount( clientid, money, LoanComission, LoanLimit,++_accountId, isTrusted, UntrustedClientLimit);
                    break;
                }
            }
            _accounts.Add(account.AccountId, account);
            return account.AccountId;
        }

        internal void ClientBecameTrusted(uint clientId)
        {
            foreach (var account in _accounts.Values)
            {
                if (account.ClientId == clientId)
                    account.BecameTrusted();
            }
        }

        internal ClientCommand TopUp(uint accountId, uint clientId, double money)
        {
            CheckAccountBeforeCommand(accountId, clientId);
            var topUp = new TopUp(_accounts[accountId], money);
            topUp.Execute();
            _accounts[accountId].History.Add(topUp);
            return topUp;
        }

        internal ClientCommand WithDraw(uint accountId, uint clientId, double money)
        {
            CheckAccountBeforeCommand(accountId, clientId);
            CheckForWithDrawLimits(_accounts[accountId], money);
            var withdraw = new WithDraw(_accounts[accountId], money);
            withdraw.Execute();
            _accounts[accountId].History.Add(withdraw);
            return withdraw;
        }

        internal ClientCommand TransferFrom(uint accountFrom, uint accountTo, uint clientId, uint bank2Id, double money)
        {
            CheckAccountBeforeCommand(accountFrom, clientId);
            CheckForWithDrawLimits(_accounts[accountFrom], money);
            var bank2 = BankManager.GetBank(bank2Id);
            if (!bank2.AccountExists(accountTo))
                throw new Exception("Incorrect Account Number");
            var transfer = new Transfer(new WithDraw(_accounts[accountFrom], money));
            _accounts[accountFrom].History.Add(transfer);
            return bank2.TransferTo(transfer, accountTo);
        }

        private ClientCommand TransferTo(Transfer transfer, uint accountTo)
        {
            transfer.TransferTo(new TopUp(_accounts[accountTo], transfer.Money));
            transfer.Execute();
            _accounts[accountTo].History.Add(transfer);
            return transfer;
        }

        private bool AccountExists(uint accountId)
        {
            return _accounts.ContainsKey(accountId);
        }

        private bool AccountBelongsTo(uint accountId, uint clientId)
        {
            return _accounts[accountId].ClientId == clientId;
        }

        private void UndoTopUp(Account account, ClientCommand command)
        {
            if (!account.CheckForPossibility(command.Money))
                throw new Exception("Can't undo command, because it would be incorrect account balance");
            command.Undo();
            account.History.Remove(command);
        }

        private void UndoTransferTo(Transfer command)
        {
            var account = _accounts[command.ToAccountId];
            if (!account.CheckForPossibility(command.Money))
                throw new Exception("Can't undo command, because it would be incorrect account balance");
            account.History.Remove(command);
            var bank2Id = command.FromAccountId/1000;
            var bank2 = BankManager.GetBank(bank2Id);
            bank2.UndoTransferFrom(command);
        }

        private void UndoTransferFrom(Transfer command)
        {
            command.Undo();
            _accounts.Remove(command.ToAccountId);
        }
        internal void Undo(ClientCommand command, uint clientid)
        {
            var account = _accounts[command.AccountId];
            if (account.ClientId != clientid)
                throw new Exception("Client can't undo this command");
            if (!account.History.Contains(command))
                throw new Exception("Command was already undo");
            switch (command.type)
            {
                case 1:
                {
                    UndoTopUp(account, command);
                    break;
                }
                case 2:
                {
                    command.Undo();
                    account.History.Remove(command);
                    break;
                }
                case 3:
                {
                    var transfer = command as Transfer;
                    UndoTransferTo(transfer);
                    break;
                }
                    
            }
        }

        private void CheckAccountBeforeCommand(uint accountId, uint clientId)
        {
            if (!AccountExists(accountId))
                throw new Exception("Incorrect Account Number");
            if (!AccountBelongsTo(accountId, clientId))
                throw new Exception("Account doesn't belongs to this Client");
        }

        private static void CheckForWithDrawLimits(Account account, double money)
        {
            if (!account.IsTrusted && money > account.UnTrustedLimit)
                throw new Exception("Can't with draw, because it is untrusted account");
            if (!account.CheckForPossibility(money, false))
                throw new Exception("Can't with draw, because it would be incorrect account balance");
        }

        public void UpdateDate()
        {
            foreach (var account in _accounts.Values)
                account.FinishDay();
        }
    }
}