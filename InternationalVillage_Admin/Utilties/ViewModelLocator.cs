using InternationalVillage_Admin.Services;
using InternationalVillage_Admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace InternationalVillage_Admin.Utilties
{
    class ViewModelLocator
    {
        private UnityContainer container;

        public ViewModelLocator()
        {
            container = new UnityContainer();
            container.RegisterType<IChatService, ChatService>();
            container.RegisterType<IDialogService, DialogService>();
        }

        public MenuViewModel MenuVM
        {
            get { return container.Resolve<MenuViewModel>(); }
        }

        public ChatViewModel ChatVM
        {
            get { return container.Resolve<ChatViewModel>(); }
        }


    }
}
