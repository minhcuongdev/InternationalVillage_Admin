using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using InternationalVillage_Admin.Store;
using InternationalVillage_Admin.Component;

namespace InternationalVillage_Admin.ViewModel
{
    public class ApartmentUCViewModel : BaseViewModel
    {
        public ICommand Selected { get; set; }

        public ApartmentUCViewModel()
        {
            Selected = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                var bc = new BrushConverter();
                ToggleButton t = p.FindName("Toggle") as ToggleButton;
                Grid g = p.FindName("StatusBg") as Grid;
                if (t.IsChecked == true)
                {
                    g.Background = System.Windows.Media.Brushes.DarkBlue;
                    ApartmentStore.Instance.ApartmentSlectedList.Add(p as ApartmentUC);
                    RenderNumber(p);
                } else
                {
                    g.Background = (Brush)bc.ConvertFrom("#2f9cfa");
                    ApartmentStore.Instance.ApartmentSlectedList.Remove(p as ApartmentUC);
                    RenderNumber(p);
                }
            });
        }

        void RenderNumber(UserControl p)
        {
            FrameworkElement grid = p;
            for (int i = 0; i < 3; i++)
                grid = grid.Parent as FrameworkElement;
            if((grid as Grid) != null)
            {
                Grid g = grid as Grid;
                TextBlock text = g.FindName("Number") as TextBlock;
                text.Text = ApartmentStore.Instance.ApartmentSlectedList.Count.ToString();
            }
        }
    }
}
