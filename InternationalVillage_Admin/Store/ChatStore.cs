using InternationalVillage_Admin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalVillage_Admin.Store
{
    class ChatStore
    {
        private static ChatStore instance;
        internal static ChatStore Instance { get { if (instance == null) instance = new ChatStore(); return instance; } set => instance = value; }


        ChatStore() { }

        private bool isConnected = false;
        public bool IsConnected { get => isConnected; set => isConnected = value; }

        private bool isLoggedIn = false;
        public bool IsLoggedIn { get => isLoggedIn; set => isLoggedIn = value; }

        private IChatService chatService;
        public IChatService ChatService { get => chatService; set => chatService = value; }
    }
}
