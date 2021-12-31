using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using InternationalVillage_Admin.Store;

namespace InternationalVillage_Admin.ViewModel
{
    public class NotificationUCViewModel:BaseViewModel
    {
        public ICommand CloseNoti { get; set; }

        public NotificationUCViewModel()
        {
            CloseNoti = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                FrameworkElement stackPanel = GetStackPanelParent(p);
                if (stackPanel is StackPanel s)
                {
                    if (p.FindName("TimeCreate") is TextBlock time)
                    {
                        DateTime TimeCreate = DateTime.Parse(time.Text);
                        if (NotificationStore.Instance.DeleteNotification(AccountStore.Instance.IdUser, TimeCreate))
                        {
                            s.Children.Remove(p);
                        }
                    }
                }
            });
        }

        FrameworkElement GetStackPanelParent(UserControl p)
        {
            FrameworkElement parent = p.Parent as StackPanel;

            return parent;
        }
    }
}
