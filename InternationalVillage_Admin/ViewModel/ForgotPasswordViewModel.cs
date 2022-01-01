using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Input;

using InternationalVillage_Admin.Utilities;
using InternationalVillage_Admin.Store;
using System.Windows.Media.Imaging;
using System.Net.Mail;

namespace InternationalVillage_Admin.ViewModel
{
    class ForgotPasswordViewModel : BaseViewModel
    {
        string username;
        string password;


        public ICommand EmailChanged { get; set; }
        public ICommand ValidateEmail { get; set; }
        public ICommand UsernameChanged { get; set; }
        public ICommand ValidateUsername { get; set; }
        public ICommand ClearFocus { get; set; }
        public ICommand SendEmail { get; set; }
        public ICommand CloseForm { get; set; }

        public string Email { get => email; set => email = value; }

        private string email;
        bool isEmailCorrect = false;
        bool isUsernameCorrect = false;

        public ForgotPasswordViewModel()
        {
            ClearFocus = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Keyboard.ClearFocus();
            });

            CloseForm = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            EmailChanged = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                Email = Validate.Instance.TextChanged(p, 5);
            });

            ValidateEmail = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                isEmailCorrect = Validate.Instance.ValidateEmail(p, Email);
                if (isEmailCorrect)
                {
                    p.Visibility = Visibility.Hidden;

                }
                else
                {
                    p.Text = "Your email address is invalid";
                    p.Foreground = System.Windows.Media.Brushes.Red;
                    p.Visibility = Visibility.Visible;
                }
            });

            UsernameChanged = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                username = Validate.Instance.TextChanged(p, 5);
            });

            ValidateUsername = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                isUsernameCorrect = Validate.Instance.ValidateUsername(p, username, "Username", 5);

            });

            SendEmail = new RelayCommand<Window>((p) => { return isUsernameCorrect && isEmailCorrect; }, (p) =>
            {
                try
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress("internationalvillagesystem@gmail.com");
                        mail.To.Add(Email);
                        mail.Subject = "ACCOUNT RECOVERY";
                        //DataTable data = AccountStore.Instance.GetPasswordByUsername(username);
                        GetPassword();
                        if (password == "")
                        {
                            MessageBox.Show("Invalid Username. Please enter again!");
                            return;
                        }
                        else
                        {
                            string str = "Hello User: " + username + ".\r\n" + "Your password is: " + password;
                            mail.Body = str;
                            mail.IsBodyHtml = true;


                            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                            {
                                smtp.Credentials = new System.Net.NetworkCredential("internationalvillagesystem@gmail.com", "123456789cuongquynhtam");
                                smtp.EnableSsl = true;
                                smtp.Send(mail);
                                p.Close();
                                MessageBox.Show("Email has been sent successfully!");

                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);


                }
            });
        }

        string GetPassword()
        {
            DataTable data = AccountStore.Instance.GetPasswordByUsername(username);
            if (data.Rows.Count > 0)
            {
                foreach (DataRow item in data.Rows)
                {
                    password = item["password"].ToString();
                }
                ;
            }
            else
            {

                password = "";
            }
            return password;
        }
    }
}

