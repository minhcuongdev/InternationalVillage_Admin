using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InternationalVillage_Admin.Utilities
{
    public class Validate
    {
        private static Validate instance;
        internal static Validate Instance { get { if (instance == null) instance = new Validate(); return instance; } set => instance = value; }
        public Validate() { }

        public string TextChanged(TextBox p, int minimumLength = 0, int maximumLength = 50)
        {
            string data = p.Text.Trim();
            if (data.Length > minimumLength && data.Length <= maximumLength)
            {
                p.BorderBrush = System.Windows.Media.Brushes.Green;
            }
            else
            {
                p.BorderBrush = System.Windows.Media.Brushes.Black;
            }

            return data;
        }

        public DateTime DateChanged(DatePicker p)
        {
            DateTime data = p.SelectedDate.Value;
            if (data != null)
            {
                p.BorderBrush = System.Windows.Media.Brushes.Green;
            }
            else
            {
                p.BorderBrush = System.Windows.Media.Brushes.Black;
            }

            return data;
        }

        public string SelecttionChanged(ComboBox p)
        {
            string data = p.Text.Trim();
            if (data.Length > 0)
            {
                p.BorderBrush = System.Windows.Media.Brushes.Green;
            }
            else
            {
                p.BorderBrush = System.Windows.Media.Brushes.Black;
            }

            return data;
        }

        public string PasswordChanged(PasswordBox p, int minimumLength = 0, int maximumLength = 50)
        {
            string data = p.Password.Trim();
            if (data.Length > minimumLength && data.Length < maximumLength)
            {
                p.BorderBrush = System.Windows.Media.Brushes.Green;
            }
            else
            {
                p.BorderBrush = System.Windows.Media.Brushes.Black;
            }

            return data;
        }

        public bool Number(TextBlock p, string data, string name, int minimumLenght = 0, int maximumLength = 50)
        {
            if (!isNumber(data))
            {
                p.Text = name + " must be number !";
                p.Visibility = Visibility.Visible;
                return false;
            }
            else
            {
                if (data.Length < minimumLenght)
                {
                    p.Visibility = Visibility.Visible;
                    p.Text = name + " must be lager than " + minimumLenght + " number !";
                    return false;
                }
                else
                {
                    p.Visibility = Visibility.Hidden;
                    if (data.Length > maximumLength)
                    {
                        p.Visibility = Visibility.Visible;
                        p.Text = name + " must not be lager than " + maximumLength + " number !";
                        return false;
                    }
                    else
                    {
                        p.Visibility = Visibility.Hidden;
                    }
                }
            }

            return true;
        }

        public bool Required(TextBlock p, string data, string name, int minimumLenght = 0, int maximumLength = 50)
        {
            p.Foreground = System.Windows.Media.Brushes.Red;
            if (data.Equals(""))
            {
                p.Text = "Please enter " + name + " !";
                p.Visibility = Visibility.Visible;
                return false;
            }
            else
            {
                if (data.Length <= minimumLenght)
                {
                    p.Visibility = Visibility.Visible;
                    p.Text = name + " must be lager than " + minimumLenght + " character !";
                    return false;
                }
                else
                {
                    p.Visibility = Visibility.Hidden;

                    if (data.Length > maximumLength)
                    {
                        p.Visibility = Visibility.Visible;
                        p.Text = name + " must not be lager than " + maximumLength + " character !";
                        return false;
                    }
                    else
                    {
                        p.Visibility = Visibility.Hidden;
                    }
                }
            }
            return true;
        }

        public bool NumberRange(TextBlock p, string data, string name, int minimum = 0, int maximum = 50)
        {
            p.Foreground = System.Windows.Media.Brushes.Red;
            if (data.Equals(""))
            {
                p.Text = " Please enter " + name;
                p.Visibility = Visibility.Visible;
                return false;
            }
            if (!isNumber(data))
            {
                p.Text = name + " must be number !";
                p.Foreground = System.Windows.Media.Brushes.Red;
                p.Visibility = Visibility.Visible;
                return false;
            }
            else
            {
                if (Int32.Parse(data) < minimum + 1)
                {
                    p.Visibility = Visibility.Visible;
                    p.Foreground = System.Windows.Media.Brushes.Red;
                    p.Text = name + " must be greater than " + minimum + " !";
                    return false;
                }
                else
                {
                    p.Visibility = Visibility.Hidden;
                    if (Int32.Parse(data) > maximum)
                    {
                        p.Visibility = Visibility.Visible;
                        p.Foreground = System.Windows.Media.Brushes.Red;
                        p.Text = name + " must not be exceed " + maximum + " !";
                        return false;
                    }
                    else
                    {
                        p.Visibility = Visibility.Hidden;
                    }
                }
            }

            return true;
        }


        public bool isNumber(string data)
        {
            bool isNumeric = true;
            foreach (char c in data)
            {
                if (!Char.IsNumber(c))
                {
                    isNumeric = false;
                    break;
                }
            }

            return isNumeric;
        }
    }
}
