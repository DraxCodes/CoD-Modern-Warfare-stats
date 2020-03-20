using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodMwStats.AccountLogic.ServerAccounts
{
    public static class ServerAccounts
    {
        private static  List<ServerAccount> _accounts;
        private static string _accountsFile = "Resources/ServerAccounts.json";

        public static IReadOnlyCollection<ServerAccount> GetAllAccounts()
        {
            return ServerAccounts._accounts.AsReadOnly();
        }

        static ServerAccounts()
        {
            if (ServerDataStorage.SaveExists(_accountsFile))
            {
                _accounts = ServerDataStorage.LoadServerAccounts(_accountsFile).ToList();
            }
            else
            {
                _accounts = new List<ServerAccount>();
                SaveAccounts();
            }
        }

        public static void SaveAccounts()
        {
            ServerDataStorage.SaveServerAccounts(_accounts, _accountsFile);
        }

        public static ServerAccount GetAccount(SocketGuild guild)
        {
            return GetOrCreateAccount(guild.Id, guild.Name);
        }

        private static ServerAccount GetOrCreateAccount(ulong id, string username)
        {
            var result = from a in _accounts
                where a.ID == id
                select a;

            var account = result.FirstOrDefault();
            if (account == null) account = CreateUserAccount(id, username);
            return account;
        }

        private static ServerAccount CreateUserAccount(ulong id, string username)
        {
            var newAccount = new ServerAccount()
            {
                ID = id,
                ServerName = username,
                Prefix = ">",
                Joined = DateTimeOffset.UtcNow.ToString("d.MM.yyyy HH:mm:ss")
            };

            _accounts.Add(newAccount);
            SaveAccounts();
            return newAccount;
        }
    }
}
