using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace FirstEvent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly DataGridView _dataGridView = new DataGridView();
        public MainWindow()
        {
            InitializeComponent();

            GeneralInfoDocDate.Value = DateTime.Now;
            GeneralInfoEventDate.Value = DateTime.Now;
        }

        private void ContactsAddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            DataContext = _dataGridView.AddContact("Mother", "Petya", "456", "he is good");
            
        }

        private void ContactsCopyBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var context = DataContext as ObservableCollection<Contact>;
            if (context == null)
                return;
            if (DataGridContacts.SelectedIndex == context.Count - 1)
            {
                context.Add(context[DataGridContacts.SelectedIndex]);
            }
            else
            {
                context.Insert(DataGridContacts.SelectedIndex + 1, context[DataGridContacts.SelectedIndex]);
            }
            
            DataContext = context;
        }

        private void ContactsDeleteBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var context = DataContext as ObservableCollection<Contact>;
            if (context == null)
                return;
            context.RemoveAt(DataGridContacts.SelectedIndex);
            DataContext = context;
        }


        private void ButtonDutyDoc_OnClick(object sender, RoutedEventArgs e)
        {
            DoctorsList doctorsList = new DoctorsList();
            doctorsList.Owner = this;
            doctorsList.ShowDialog();
        }
    }
}
