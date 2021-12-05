using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using InternationalVillage_Admin.Store;
using InternationalVillage_Admin.Model;
using System.Windows;



namespace InternationalVillage_Admin.ViewModel
{
    class PaymentDetailViewModel : BaseViewModel
    {
        public ICommand ShowBank { get; set; }
        public ICommand ChooseBankChanged { get; set; }
        public ICommand UnCheckCash { get; set; }
        public ICommand UnCheckCard { get; set; }
        public ICommand LoadBillTable { get; set; }
        public ICommand LoadIDBill { get; set; }
        public ICommand LoadCustomer { get; set; }
        public ICommand LoadReceptionist { get; set; }
        public ICommand LoadCheckInDate { get; set; }
        public ICommand LoadCheckOutDate { get; set; }
        public ICommand LoadTotalMoney { get; set; }
        public ICommand LoadStatus { get; set; }
        public ICommand HiddenCheckCard { get; set; }
        public ICommand HiddenCheckCash { get; set; }
        public ICommand HiddenCBBank { get; set; }
        public ICommand Payment { get; set; }
        public ICommand UpdateChangeMoney { get; set; }
        public ICommand PaidChanged { get; set; }
        public ICommand HiddenPaidMoney { get; set; }
        public ICommand HiddenChangeMoney { get; set; }
        public ICommand CheckPaidByCard { get; set; }
        public ICommand CheckChangeByCard { get; set; }
        public int Paid { get => paid; set => paid = value; }
        public int Change { get => change; set => change = value; }
        public int Total { get => total; set => total = value; }
        public bool IsChooseBank { get => isChooseBank; set => isChooseBank = value; }

        int paid = 0;
        int change = 0;
        int total = Int32.Parse(PaymentStore.Instance.TotalMoney);
        string TypePay = "Cash";
        bool isChooseBank = false;
        public PaymentDetailViewModel()
        {
            ShowBank = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                if (TypePay.Equals("Cash"))
                {
                    p.Visibility = Visibility.Hidden;
                }
                else
                {
                    p.Visibility = Visibility.Visible;
                }
            });
            ChooseBankChanged = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                if (p.SelectedItem != null) isChooseBank = true;
            });
            UnCheckCard = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            {
                p.IsChecked = false;
                TypePay = "Cash";
            });
            UnCheckCash = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            {
                p.IsChecked = false;
                TypePay = "Card";
            });

            LoadBillTable = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                p.ItemsSource = PaymentStore.Instance.ListDetailBookings;

            });
            LoadIDBill = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = PaymentStore.Instance.IdBill;
                

            });
            LoadCustomer = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = PaymentStore.Instance.NameCustomer;


            });
            LoadReceptionist = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = PaymentStore.Instance.NameReceptionist;


            });
            LoadCheckInDate = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = DateTime.Parse(PaymentStore.Instance.Checkin).ToShortDateString();


            });
            LoadCheckOutDate = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = DateTime.Parse(PaymentStore.Instance.Checkout).ToShortDateString();


            });
            LoadTotalMoney = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = PaymentStore.Instance.TotalMoney + "$";
                Total = Int32.Parse(PaymentStore.Instance.TotalMoney);


            });
            LoadStatus = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                if (!PaymentStore.Instance.Status.Equals("Not accepted yet"))
                    p.Text = "Paid by "+PaymentStore.Instance.Status;
                else p.Text = PaymentStore.Instance.Status;

            });
            HiddenCheckCash = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            {
                if (!PaymentStore.Instance.Status.Equals("Not accepted yet"))
                    p.Visibility = Visibility.Hidden;

            });
            HiddenCheckCard = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            {
                if (!PaymentStore.Instance.Status.Equals("Not accepted yet"))
                    p.Visibility = Visibility.Hidden;

            });
            HiddenCBBank = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                    p.Visibility = Visibility.Hidden;

            });
            HiddenPaidMoney = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                if (!PaymentStore.Instance.Status.Equals("Not accepted yet"))
                    p.Visibility = Visibility.Hidden;

            });
            HiddenChangeMoney = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                if (!PaymentStore.Instance.Status.Equals("Not accepted yet"))
                    p.Visibility = Visibility.Hidden;

            });
            Payment = new RelayCommand<Button>((p) => { return (paid >= total); }, (p) =>
            {
                
                    string query = "Update Bill set Status= '"+TypePay+"' , Id_Receptionist = '"+AccountStore.Instance.IdUser+"' where ID_Bill = '"+PaymentStore.Instance.IdBill+"'";
                    int result = DataProvider.Instance.ExecuteNonQuery(query);
                    if (result != 0)
                    {
                        MessageBox.Show("Payment success!");
                        PaymentStore.Instance.Status = TypePay;
                    }
                    else
                    {
                        MessageBox.Show("Payment fail!");
                    }
                
            });
            PaidChanged = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                if (!p.Text.Equals(""))
                {
                    try
                    {
                        paid = Int32.Parse(p.Text);
                    }
                    catch (FormatException e)
                    {
                        MessageBox.Show("Paid money must be number!");
                    }
                }
            });
            CheckPaidByCard = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                if (TypePay.Equals("Card"))
                {
                    if (isChooseBank)
                    {
                        paid = total;
                        p.Text = paid.ToString();
                    }
                    p.IsReadOnly = true;
                }
                else
                {
                    paid = 0;
                    p.Text = "";
                    p.IsReadOnly = false;
                }
                
            });
            CheckChangeByCard = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                if (TypePay.Equals("Card"))
                {
                    if (isChooseBank)
                    {
                        change = 0;
                        p.Text = change.ToString();
                    }
                }
                else
                {
                    change = 0;
                    p.Text = "";
                }


            });
            UpdateChangeMoney = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                if (paid >= total) p.Text = (paid - total).ToString();
                else
                {
                    p.Text = "Paid money must be greater than or equal to total money!";
                    
                }
            });
           
        }
    }
}
