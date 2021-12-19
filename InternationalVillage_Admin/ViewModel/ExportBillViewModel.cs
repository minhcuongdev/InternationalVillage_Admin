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
using System.Windows;



namespace InternationalVillage_Admin.ViewModel
{
    class ExportBillViewModel : BaseViewModel
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
        public ICommand LoadPayDate { get; set; }
        public ICommand LoadTotalMoney { get; set; }
        public ICommand LoadPaidMoney { get; set; }
        public ICommand LoadChange { get; set; }
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
        public ICommand ExportBill { get; set; }
        public int Paid { get => paid; set => paid = value; }
        public int Change { get => change; set => change = value; }
        public int Total { get => total; set => total = value; }
        public bool IsChooseBank { get => isChooseBank; set => isChooseBank = value; }

        int paid = 0;
        int change = 0;
        int total = Int32.Parse(PaymentStore.Instance.TotalMoney);
        string TypePay = "Cash";
        bool isChooseBank = false;
        public ExportBillViewModel()
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
                p.Text = DateTime.Parse(PaymentStore.Instance.Checkin).ToString("dd/MM/yyyy");


            });
            LoadCheckOutDate = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = DateTime.Parse(PaymentStore.Instance.Checkout).ToString("dd/MM/yyyy");


            });
            LoadPayDate = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                if (PaymentStore.Instance.PaydDate.Equals("")) p.Text = "";
                else p.Text = DateTime.Parse(PaymentStore.Instance.PaydDate).ToString("dd/MM/yyyy H:mm:ss");

            });
            LoadTotalMoney = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = PaymentStore.Instance.TotalMoney + "$";
                Total = Int32.Parse(PaymentStore.Instance.TotalMoney);


            });
            LoadPaidMoney = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = PaymentStore.Instance.PaidMoney;

            });
            LoadChange = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = PaymentStore.Instance.Change;


            });
            LoadStatus = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                if (!PaymentStore.Instance.Status.Equals("Not accepted yet"))
                    p.Text = "Paid by " + PaymentStore.Instance.Status;
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
            HiddenPaidMoney = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                if (!PaymentStore.Instance.Status.Equals("Not accepted yet"))
                    p.Visibility = Visibility.Hidden;

            });
            HiddenChangeMoney = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                if (!PaymentStore.Instance.Status.Equals("Not accepted yet"))
                    p.Visibility = Visibility.Hidden;

            });
            PaidChanged = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                if (!p.Text.Equals(""))
                {
                    try
                    {
                        paid = Int32.Parse(p.Text);
                    }
                    catch (FormatException e)
                    {

                        MessageBox.Show("Paid money must be number!" + e.Message);
                    }
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

            ExportBill = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                try
                {
                    PrintDialog printDialog = new PrintDialog();
                    if (printDialog.ShowDialog() == true)
                    {
                        printDialog.PrintVisual(p, "Bill");
                    }
                }
                finally
                {

                }

            });

        }
    }
}
