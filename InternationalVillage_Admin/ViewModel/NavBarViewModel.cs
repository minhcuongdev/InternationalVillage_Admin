using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace InternationalVillage_Admin.ViewModel
{
    public class NavBarViewModel : BaseViewModel
    {
        public ICommand CloseWindow { get; set; }
        public ICommand MaximizeWindow { get; set; }
        public ICommand MinimizeWindow { get; set; }
        public ICommand DragWindow { get; set; }

        public NavBarViewModel()
        {
            CloseWindow = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                FrameworkElement window = GetWindowParent(p);
                if (window is Window w)
                {
                    w.Close();
                }
            });
            MaximizeWindow = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                FrameworkElement window = GetWindowParent(p);
                if (window is Window w)
                {
                    if (w.WindowState != WindowState.Maximized)
                    {
                        w.WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        w.WindowState = WindowState.Normal;
                    }
                }
            });
            MinimizeWindow = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                FrameworkElement window = GetWindowParent(p);
                if (window is Window w)
                {
                    if (w.WindowState != WindowState.Minimized)
                    {
                        w.WindowState = WindowState.Minimized;
                    }
                    else
                    {
                        w.WindowState = WindowState.Normal;
                    }
                }
            });
            DragWindow = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                FrameworkElement window = GetWindowParent(p);
                if (window is Window w)
                {
                    w.DragMove();
                }
            });
        }

        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = Window.GetWindow(p);

            return parent;
        }
    }
}
