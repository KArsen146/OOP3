namespace ConsoleApplication1.BankSystem
{
    public class Client
    {
        public string FirstName { get; }

        private bool _clientTrusted;

        internal bool IsClientTrusted => _clientTrusted;

        public string SecondName { get; }

        private uint _pasportNumber;
        public uint PasportNumber => _pasportNumber;
        private string _adress;
        public string Adress => _adress;
        internal Client(string firstName, string secondName)
        {
            FirstName = firstName;
            SecondName = secondName;
            _pasportNumber = 0;
            _adress = "";
            ClientId = ++_clientId;
            _clientTrusted = false;
            BankManager.AddClient(this.ClientId, this);
        }

        internal readonly uint ClientId;
        private static uint _clientId;
        public void AddPassportNumber(uint value)
        {
            _pasportNumber = value;
            CheckForBecameTrusted();
        }

        public void AddAdress(string adress)
        {
            _adress = adress;
            CheckForBecameTrusted();
        }

        private void CheckForBecameTrusted()
        {
            if (_adress != null && _pasportNumber != 0)
            {
                _clientTrusted = true;
                BankManager.ClientBecameTrusted(ClientId);
            }
        }

        public ClientCommand TopUp(uint accountId, double money)
        {
            return BankManager.TopUp(accountId, ClientId, money);
        }
        public ClientCommand WithDraw(uint accountId, double money)
        {
            return BankManager.WithDraw(accountId, ClientId, money);
        }
        public ClientCommand Transfer(uint accountFrom, uint accountTo, double money)
        {
            return BankManager.Transfer(accountFrom, accountTo, ClientId, money);
        }

        public void Undo(ClientCommand command)
        {
            BankManager.Undo(command, ClientId);
        }
    }
}