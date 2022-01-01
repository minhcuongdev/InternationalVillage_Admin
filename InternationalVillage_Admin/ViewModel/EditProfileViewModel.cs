using InternationalVillage_Admin.Store;
using InternationalVillage_Admin.Utilities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;



namespace InternationalVillage_Admin.ViewModel
{
    class EditProfileViewModel : BaseViewModel
    {
        public ICommand OpenChangePassword { get; set; }
        public ICommand OpenForgotPassword { get; set; }

        public ICommand LoadProfile { get; set; }
        public ICommand SelectPicture { get; set; }
        public ICommand SavePersonalInfomation { get; set; }
        public ICommand SaveUsername { get; set; }

        public ICommand FullNameChanged { get; set; }
        public ICommand ValidateFullName { get; set; }

        public ICommand AddressChanged { get; set; }
        public ICommand ValidateAddress { get; set; }

        public ICommand UserNameChanged { get; set; }
        public ICommand ValidateUserName { get; set; }

        string Fullname = "";
        string Address = "";
        string UserName = "";
        string Avatar = "";

        bool isFullNameCorrect = true;
        bool isAddressCorrect = true;

        bool isUserNameCorrect = true;

        public EditProfileViewModel()
        {
            OpenChangePassword = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                ChangePasswordWindow changepassform = new ChangePasswordWindow();
                changepassform.ShowDialog();
            });

            OpenForgotPassword = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                ForgotPasswordWindow Fforgotpass = new ForgotPasswordWindow();
                Fforgotpass.ShowDialog();
            });

            SelectPicture = new RelayCommand<ImageBrush>((p) => { return true; }, (p) =>
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*",
                    CheckFileExists = true,
                    Multiselect = false
                };
                if (dialog.ShowDialog() == true)
                {
                    Avatar = dialog.FileName;
                    BitmapImage bitmap = new BitmapImage(new Uri(dialog.FileName));
                    p.ImageSource = bitmap;
                }

            });

            LoadProfile = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                if (!AccountStore.Instance.Avatar.Equals(""))
                {
                    ImageBrush img = (ImageBrush)p.FindName("AvatarProfile");
                    img.ImageSource = new ImageSourceConverter().ConvertFromString(AccountStore.Instance.Avatar) as ImageSource;
                }

                TextBox fullName = p.FindName("FullName") as TextBox;
                TextBox address = p.FindName("Address") as TextBox;
                TextBox username = p.FindName("Username") as TextBox;
                Label position = p.FindName("Position") as Label;

                fullName.Text = Fullname = AccountStore.Instance.Name;
                address.Text = Address = AccountStore.Instance.Address;
                username.Text = UserName = AccountStore.Instance.Username;
                position.Content = "Position: " + AccountStore.Instance.Role;
            });

            FullNameChanged = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                Fullname = Validate.Instance.TextChanged(p, 5);

            });
            ValidateFullName = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                isFullNameCorrect = Validate.Instance.Required(p, Fullname, "Full Name", 5);
            });

            AddressChanged = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                Address = Validate.Instance.TextChanged(p, 5);

            });
            ValidateAddress = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                isAddressCorrect = Validate.Instance.Required(p, Address, "Address", 5);
            });

            UserNameChanged = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                UserName = Validate.Instance.TextChanged(p, 5);

            });
            ValidateUserName = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                isUserNameCorrect = Validate.Instance.Required(p, UserName, "Username", 5);
            });

            SavePersonalInfomation = new RelayCommand<Page>((p) => { return isFullNameCorrect && isAddressCorrect; }, (p) =>
            {
                if (!Avatar.Equals(""))
                {
                    Window w = Window.GetWindow(p);
                    if (w != null)
                    {
                        ImageBrush img = w.FindName("Avatar") as ImageBrush;
                        BitmapImage bitmap = new BitmapImage(new Uri(Avatar));
                        img.ImageSource = bitmap;
                    }
                }

                if (AccountStore.Instance.UpdateProfile(Fullname, Address))
                {
                    AccountStore.Instance.Name = Fullname;
                    AccountStore.Instance.Address = Address;
                    MessageBox.Show("Success");
                }
            });

            SaveUsername = new RelayCommand<Page>((p) => { return isUserNameCorrect; }, (p) =>
            {
                if (AccountStore.Instance.UpdateUserName(UserName))
                {
                    AccountStore.Instance.Username = UserName;
                    MessageBox.Show("Success");
                }
            });
        }
    }
}
