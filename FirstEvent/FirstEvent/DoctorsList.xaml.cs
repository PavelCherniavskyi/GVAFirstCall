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
using System.Windows.Shapes;

namespace FirstEvent
{
    /// <summary>
    /// Interaction logic for DoctorsList.xaml
    /// </summary>
    public partial class DoctorsList : Window
    {
        public DoctorsList()
        {
            InitializeComponent();
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (Doctor)DocList.SelectedItem;
            if (item == null)
                MessageBox.Show("No doctor selected");

            DialogResult = true;
        }

        private void ButtonOK_OnClick(object sender, RoutedEventArgs e)
        {
            var item = (Doctor)DocList.SelectedItem;
            if (item == null)
                MessageBox.Show("No doctor selected");
            Owner.Gene
            DialogResult = true;
        }
    }
}
