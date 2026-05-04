using System.Windows;

namespace Master_floor
{
    public partial class PartnerWindow : Window
    {
        public Partner NewPartner { get; set; }
        private int _editId = 0;

        public PartnerWindow(int? id = null)
        {
            InitializeComponent();

            if (id.HasValue)
            {
                _editId = id.Value;
                DatabaseHelper db = new DatabaseHelper();
                Partner p = db.GetPartnerById(id.Value);
                if (p != null)
                {
                    cmbType.Text = p.Тип_партнера;
                    txtName.Text = p.Наименование_партнера;
                    txtDirector.Text = p.Директор;
                    txtPhone.Text = p.Телефон_партнера;
                }
            }
        }

        void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите наименование");
                return;
            }

            NewPartner = new Partner
            {
                ID = _editId,
                Тип_партнера = cmbType.Text,
                Наименование_партнера = txtName.Text,
                Директор = txtDirector.Text,
                Телефон_партнера = txtPhone.Text,
                Рейтинг = 0
            };

            DialogResult = true;
            Close();
        }

        void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}