using System;
using System.Data;
using System.Windows;

namespace Master_floor
{
    public partial class MainWindow : Window
    {
        DatabaseHelper db = new DatabaseHelper();

        public MainWindow()
        {
            InitializeComponent();
            LoadPartners();
        }

        void LoadPartners()
        {
            DataTable dt = db.GetPartnersWithSales();
            dt.Columns.Add("Скидка", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                double sum = Convert.ToDouble(row["Общая_сумма_продаж"]);
                if (sum < 10000) row["Скидка"] = "0%";
                else if (sum < 50000) row["Скидка"] = "5%";
                else if (sum < 300000) row["Скидка"] = "10%";
                else row["Скидка"] = "15%";
            }

            dgPartners.ItemsSource = dt.DefaultView;

            if (dt.Rows.Count > 0)
                tbInfo.Text = "Выберите партнера";
        }

        void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var win = new PartnerWindow();
            if (win.ShowDialog() == true)
            {
                db.AddPartner(win.NewPartner);
                LoadPartners();
                MessageBox.Show("Добавлено");
            }
        }

        void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgPartners.SelectedItem == null)
            {
                MessageBox.Show("Выберите партнера");
                return;
            }

            DataRowView selected = (DataRowView)dgPartners.SelectedItem;
            int id = Convert.ToInt32(selected["ID"]);
            var win = new PartnerWindow(id);

            if (win.ShowDialog() == true)
            {
                win.NewPartner.ID = id;
                db.UpdatePartner(win.NewPartner);
                LoadPartners();
                MessageBox.Show("Обновлено");
            }
        }

        void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgPartners.SelectedItem == null)
            {
                MessageBox.Show("Выберите партнера");
                return;
            }

            DataRowView selected = (DataRowView)dgPartners.SelectedItem;
            string name = selected["Наименование_партнера"].ToString();
            int id = Convert.ToInt32(selected["ID"]);

            if (MessageBox.Show($"Удалить {name}?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                db.DeletePartner(id);
                LoadPartners();
                MessageBox.Show("Удалено");
            }
        }

        void BtnHistory_Click(object sender, RoutedEventArgs e)
        {
            if (dgPartners.SelectedItem == null)
            {
                MessageBox.Show("Выберите партнера");
                return;
            }

            DataRowView selected = (DataRowView)dgPartners.SelectedItem;
            int id = Convert.ToInt32(selected["ID"]);
            var win = new HistoryWindow(id);
            win.ShowDialog();
        }
    }
}