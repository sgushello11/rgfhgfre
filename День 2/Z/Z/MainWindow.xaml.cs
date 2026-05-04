using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Master_floor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TestBaseEntities db = new TestBaseEntities();
        public MainWindow()
        {
            InitializeComponent();
            tolist();
        }

        void tolist()
        {
            var listpartners = new List<Partner>();
            foreach (var a in db.Partners.ToList())
            {

                var sum = db.Partners_product.Where(y => a.ID == y.ID_Partner).Sum(x => x.Количество_продукции);
                string sale = "0%";
                if (sum < 10000)
                {
                    sale = "0%";
                }
                if (sum <= 10000 && sum < 50000)
                {
                    sale = "5%";
                }
                if (sum <= 50000 && sum < 300000)
                {
                    sale = "10%";
                }
                if (sum >= 300000)
                {
                    sale = "15%";
                }
                listpartners.Add(new Partner { ID = a.ID, Директор = a.Директор, Наименование_партнера = a.Наименование_партнера, Рейтинг = "Рейтинг: " + a.Рейтинг, Телефон_партнера = a.Телефон_партнера, Тип_партнера = a.Тип_партнера, Скидка = sale });
            }

            listPartner.ItemsSource = listpartners;
        }

        private void listPartner_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnAddPartner_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            var history = new History();
            history.Show();
            this.Close();
        }
    }
}
