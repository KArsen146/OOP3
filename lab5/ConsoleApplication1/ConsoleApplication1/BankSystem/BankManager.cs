using System;
using System.Collections.Generic;

namespace ConsoleApplication1.BankSystem
{
    internal static class BankManager
    {
        private static Dictionary<uint, Client> Clients = new Dictionary<uint, Client>();

        private static Dictionary<uint, Bank> Banks = new Dictionary<uint, Bank>();

        private static uint _bankId;
        public static BankBuilder StartBankCreation(string name)
        {
            return new BankBuilder(name, ++_bankId);
        }

        public static Client CreateClient(string firstName, string secondName)
        {
            return new Client(firstName, secondName);
        }

        internal static ClientCommand TopUp(uint accountId, uint clientId, double money)
        {
            var bankId = accountId / 1000;
            if (!Banks.ContainsKey(bankId))
                throw new Exception("Incorrect Account number");
            return Banks[bankId].TopUp(accountId, clientId, money);
        }

        internal static ClientCommand WithDraw(uint accountId, uint clientId, double money)
        {
            var bankId = accountId / 1000;
            if (!Banks.ContainsKey(bankId))
                throw new Exception("Incorrect Account number");
            return Banks[bankId].WithDraw(accountId, clientId, money);
        }

        internal static ClientCommand Transfer(uint accountFrom, uint accountTo, uint clientId, double money)
        {
            var bankId1 = accountFrom / 1000;
            if (!Banks.ContainsKey(bankId1))
                throw new Exception($"Incorrect Account number{accountFrom}");
            var bankId2 = accountTo / 1000;
            if (!Banks.ContainsKey(bankId2))
                throw new Exception("Incorrect Account number");
            return Banks[bankId1].TransferFrom(accountFrom, accountTo, clientId, bankId2, money);
        }

        public static void Undo(ClientCommand command, uint clientId)
        {
            var bank = Banks[command.AccountId / 1000];
            bank.Undo(command, clientId);
        }

        internal static Bank GetBank(uint bankId)
        {
            return Banks[bankId];
        }

        internal static void AddBank(Bank bank)
        {
            Banks.Add(bank.BankId, bank);
        }

        internal static void AddClient(uint clientId, Client client)
        {
            Clients.Add(clientId, client);
        }

        internal static void ClientBecameTrusted(uint clientId)
        {
            foreach (var bank in Banks.Values)
            {
                bank.ClientBecameTrusted(clientId);
            }
        }

        internal static bool IsClientTrusted(uint clientId)
        {
            if (!Clients.ContainsKey(clientId))
                throw new Exception("No such client");
            return Clients[clientId].IsClientTrusted;
        }

        internal static void UpdateTime()
        {
            foreach (var bank in Banks.Values)
            {
                bank.UpdateDate();
            }
        }
    }
}