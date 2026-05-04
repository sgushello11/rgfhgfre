using System;
using System.Data;
using System.Windows;

namespace Master_floor
{
    public partial class HistoryWindow : Window
    {
        public HistoryWindow(int partnerId)
        {
            InitializeComponent();

            DatabaseHelper db = new DatabaseHelper();
            string name = db.GetPartnerName(partnerId);
            tbPartner.Text = "Партнер: " + name;

            DataTable dt = db.GetSalesHistory(partnerId);
            dgSales.ItemsSource = dt.DefaultView;

            if (dt.Rows.Count == 0)
                tbPartner.Text += " - нет продаж";
        }
    }
}