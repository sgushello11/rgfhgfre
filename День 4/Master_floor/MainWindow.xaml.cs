using System;
using System.Linq;
using System.Windows;

namespace Master_floor
{
    public partial class MainWindow : Window
    {
        TestBaseEntities db = new TestBaseEntities();

        public MainWindow()
        {
            InitializeComponent();
            LoadPartners();
        }

        void LoadPartners()
        {
            lbPartners.ItemsSource = db.Partners.ToList();
        }

        // Добавление
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new PartnerWindow();
            if (window.ShowDialog() == true)
            {
                db.Partners.Add(window.NewPartner);
                db.SaveChanges();
                LoadPartners();
            }
        }

        // Изменение
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lbPartners.SelectedItem == null)
            {
                MessageBox.Show("Выберите партнера");
                return;
            }

            var selected = (Partners)lbPartners.SelectedItem;
            var window = new PartnerWindow(selected);

            if (window.ShowDialog() == true)
            {
                selected.Тип_партнера = window.NewPartner.Тип_партнера;
                selected.Наименование_партнера = window.NewPartner.Наименование_партнера;
                selected.Директор = window.NewPartner.Директор;
                selected.Телефон_партнера = window.NewPartner.Телефон_партнера;

                db.SaveChanges();
                LoadPartners();
            }
        }

        // Удаление
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lbPartners.SelectedItem == null)
            {
                MessageBox.Show("Выберите партнера");
                return;
            }

            var selected = (Partners)lbPartners.SelectedItem;

            if (MessageBox.Show($"Удалить {selected.Наименование_партнера}?", "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                db.Partners.Remove(selected);
                db.SaveChanges();
                LoadPartners();
            }
        }

        // История продаж
        private void BtnHistory_Click(object sender, RoutedEventArgs e)
        {
            if (lbPartners.SelectedItem == null)
            {
                MessageBox.Show("Выберите партнера");
                return;
            }

            var selected = (Partners)lbPartners.SelectedItem;
            var window = new HistoryWindow(selected.ID);
            window.ShowDialog();
        }
    }
}