using InternationalVillage_Admin.Store;
using InternationalVillage_Admin.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace InternationalVillage_Admin.ViewModel
{
    class ChangePasswordViewModel : BaseViewModel
    {
        public ICommand CloseForm { get; set; }
        public ICommand ClearFocus { get; set; }

        public ICommand PasswordChanged { get; set; }
        public ICommand ValidatePassword { get; set; }

        public ICommand NewPasswordChanged { get; set; }
        public ICommand ValidateNewPassword { get; set; }

        public ICommand ConfirmChanged { get; set; }
        public ICommand ValidateConfirm { get; set; }

        public ICommand Update { get; set; }

        string Password = "";
        string NewPassword = "";
        string ConfirmNewPassword = "";
        bool isPasswordCorrect = false;
        bool isNewPasswordCorrect = false;
        bool isConfirmCorrect = false;

        public ICommand ShowCurrentPassword { get; set; }
        public ICommand HideCurrentPassword { get; set; }

        public ICommand ShowNewPassword { get; set; }
        public ICommand HideNewPassword { get; set; }

        public ICommand ShowConfirmNewPassword { get; set; }
        public ICommand HideConfirmNewPassword { get; set; }

        public ChangePasswordViewModel()
        {
            CloseForm = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            ClearFocus = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Keyboard.ClearFocus();
            });

            PasswordChanged = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Password = Validate.Instance.PasswordChanged(p, 5);
                EnableImage(p, "ImgCurrentPasswordShowHide",Password);
            });
            ValidatePassword = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                isPasswordCorrect = Validate.Instance.Required(p, Password, "Current Password", 5);
            });

            NewPasswordChanged = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                NewPassword = Validate.Instance.PasswordChanged(p, 5);
                EnableImage(p, "ImgNewPasswordShowHide",NewPassword);

            });
            ValidateNewPassword = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                isNewPasswordCorrect = Validate.Instance.Required(p, NewPassword, "New Password", 5);
            });

            ConfirmChanged = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                ConfirmNewPassword = Validate.Instance.PasswordChanged(p, 5);
                EnableImage(p, "ImgConfirmPasswordShowHide",ConfirmNewPassword);
            });
            ValidateConfirm = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                isConfirmCorrect = Validate.Instance.Required(p, ConfirmNewPassword, "Confirm New Password", 5);
                if (isConfirmCorrect)
                {
                    if (!NewPassword.Equals(ConfirmNewPassword))
                    {
                        isConfirmCorrect = false;
                        p.Text = "Confirm new password is not a same as new password !";
                        p.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        p.Visibility = Visibility.Hidden;
                        isConfirmCorrect = true;
                    }
                }
            });

            Update = new RelayCommand<Window>((p) => { return isPasswordCorrect && isNewPasswordCorrect && isConfirmCorrect; }, (p) =>
            {
                if (AccountStore.Instance.CheckPassword(Password))
                {
                    if (AccountStore.Instance.UpdatePassword(NewPassword))
                    {
                        MessageBox.Show("Success");
                        p.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Current Password is wrong");
                }
            });


            ShowCurrentPassword = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                showPassword(p, "ImgCurrentPasswordShowHide", "CurrentPassword", "VisibleCurrentPassword");
            });

            HideCurrentPassword = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                hidePassword(p, "ImgCurrentPasswordShowHide", "CurrentPassword", "VisibleCurrentPassword");
            });

            ShowNewPassword = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                showPassword(p, "ImgNewPasswordShowHide", "NewPassword", "VisibleNewPassword");
            });

            HideNewPassword = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                hidePassword(p, "ImgNewPasswordShowHide", "NewPassword", "VisibleNewPassword");
            });

            ShowConfirmNewPassword = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                showPassword(p, "ImgConfirmPasswordShowHide", "Confirm", "VisibleConfirmPassword");
            });

            HideConfirmNewPassword = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                hidePassword(p, "ImgConfirmPasswordShowHide", "Confirm", "VisibleConfirmPassword");
            });

        }

        void EnableImage(PasswordBox p, string image, string password)
        {
            if (p.Parent is Grid parent)
            {
                Image img = parent.FindName(image) as Image;
                if (password.Length > 0)
                {
                    img.Visibility = Visibility.Visible;
                }
                else
                {
                    img.Visibility = Visibility.Hidden;
                }
            }
        }

        void showPassword(Grid p, string image, string passwordBox, string visiblePasswordBox)
        {
            Image img = p.FindName(image) as Image;
            TextBox visiblePassword = p.FindName(visiblePasswordBox) as TextBox;
            PasswordBox password = p.FindName(passwordBox) as PasswordBox;

            img.Source = new BitmapImage(new Uri("Image/Hide.jpg", UriKind.RelativeOrAbsolute));
            visiblePassword.Visibility = Visibility.Visible;
            password.Visibility = Visibility.Hidden;

            visiblePassword.Text = password.Password;
        }

        void hidePassword(Grid p, string image, string passwordBox, string visiblePasswordBox)
        {
            Image img = p.FindName(image) as Image;
            TextBox visiblePassword = p.FindName(visiblePasswordBox) as TextBox;
            PasswordBox password = p.FindName(passwordBox) as PasswordBox;

            img.Source = new BitmapImage(new Uri("Image/Show.jpg", UriKind.RelativeOrAbsolute));
            visiblePassword.Visibility = Visibility.Hidden;
            password.Visibility = Visibility.Visible;

            password.Focus();
        }
    }
}
