using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using InternationalVillage_Admin.Store;
using InternationalVillage_Admin.Model;
using InternationalVillage_Admin.Pages;
using InternationalVillage_Admin.Utilities;

using System.Windows;

namespace InternationalVillage_Admin.ViewModel
{
    class RequestRevenueReportViewModel : BaseViewModel
    {
        public ICommand LoadName { get; set; }
        public ICommand SelectedChangeType { get; set; }
        public ICommand SelectedChangeYear { get; set; }
        public ICommand CheckinDateChanged { get; set; }
        public ICommand ValidateCheckinDate { get; set; }
        public ICommand ValidateCheckoutDate { get; set; }
        public ICommand CheckoutDateChanged { get; set; }
        public ICommand LoadComboboxYear { get; set; }

        public ICommand HiddenStartDate { get; set; }
        public ICommand HiddenDueDate { get; set; }
        public ICommand HiddenYear { get; set; }
        public ICommand Send { get; set; }
        public ICommand InitYear { get; set; }

        private DateTime checkinDate = System.DateTime.Now;
        public DateTime CheckinDate { get => checkinDate; set => checkinDate = value; }
        private string strCheckinDate = "";
        public string StrCheckinDate { get => strCheckinDate; set => strCheckinDate = value; }
        bool isCheckinDateCorrect = false;


        private DateTime checkoutDate = System.DateTime.Now;
        public DateTime CheckoutDate { get => checkoutDate; set => checkoutDate = value; }
        private string strCheckoutDate = "";
        public string StrCheckoutDate { get => strCheckoutDate; set => strCheckoutDate = value; }
        bool isCheckoutDateCorrect = false;

        private string year;
        private string type = "By day";
        public RequestRevenueReportViewModel()
        {
            LoadName = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                p.Text = AccountStore.Instance.Name;
            });
            SelectedChangeType = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                ComboBoxItem typeItem = (ComboBoxItem)p.SelectedItem;
                string data = typeItem.Content.ToString();
                type = data;
                
            });
            SelectedChangeYear = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
              
                year = p.SelectedItem.ToString();

            });
            HiddenStartDate = new RelayCommand<DatePicker>((p) => { return true; }, (p) =>
            {
                if (type.Equals("By month")) p.Visibility = Visibility.Hidden;
                else p.Visibility = Visibility.Visible;
            });
            HiddenDueDate = new RelayCommand<DatePicker>((p) => { return true; }, (p) =>
            {
                if (type.Equals("By month")) p.Visibility = Visibility.Hidden;
                else p.Visibility = Visibility.Visible;
            });
            HiddenYear = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                if (type.Equals("By day")) p.Visibility = Visibility.Hidden;
                else p.Visibility = Visibility.Visible;
            });

            ValidateCheckinDate = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                isCheckinDateCorrect = Validate.Instance.Required(p, strCheckinDate, "Start Date");

            });
            ValidateCheckoutDate = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                isCheckoutDateCorrect = Validate.Instance.Required(p, strCheckoutDate, "Due Date");

            });
            CheckinDateChanged = new RelayCommand<DatePicker>((p) => { return true; }, (p) =>
            {
                checkinDate = Validate.Instance.DateChanged(p);
                strCheckinDate = checkinDate.ToString();


            });
            CheckoutDateChanged = new RelayCommand<DatePicker>((p) => { return true; }, (p) =>
            {
                checkoutDate = Validate.Instance.DateChanged(p);
                strCheckoutDate = checkoutDate.ToString();
            });

            Send = new RelayCommand<Button>((p) => { return Check(); }, (p) =>
            {
                if (type.Equals("By day"))
                {
                    int re = ReportStore.Instance.InsertReportByDay(checkinDate, checkoutDate);
                    string content = "Create revenue report from day "+ checkinDate.ToString("MM/dd/yyyy") + " to "+ checkoutDate.ToString("MM/dd/yyyy");
                    NotificationStore.Instance.NotificationAcceptedRequisition("Accountant_1", DateTime.Now, content);
                    if (re > 0) MessageBox.Show("Success!");
                }
                else
                {
                    int re = ReportStore.Instance.InsertReportByMonth(year);
                    string content = "Create revenue report for the year " + year;
                    NotificationStore.Instance.NotificationAcceptedRequisition("Accountant_1", DateTime.Now, content);
                    if (re > 0) MessageBox.Show("Success!");
                }

            });
            InitYear = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                List<string> list = new List<string>();
                for (int i = System.DateTime.Now.Year; i >= 2000; i--)
                    list.Add(i.ToString());
                p.ItemsSource = list;
                p.SelectedItem = list[0];
                year = list[0];
            });
        }

        bool Check()
        {
            if (type.Equals("By day"))
            {
                if (isCheckinDateCorrect && isCheckoutDateCorrect) return true;
            }
            if (type.Equals("By month"))
            {
                return true;
            }
            return false;
        }
    }
}
