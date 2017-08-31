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
using FirstEvent.ViewModel;
using FirstEvent.ViewModel.Sections;

namespace FirstEvent.View.MainWindows
{
    /// <summary>
    /// Interaction logic for MainAdminWindow.xaml
    /// </summary>
    public partial class MainAdminWindow : Window
    {
        public MainAdminWindow()
        {
            InitializeComponent();
            DataContext = new MainAdminViewModel(new Caller(), new GeneralInfo(), new Membership(), new Subscriber(),
                new CallInfo(), new TreatDoctor(), new ContactSection());
            new WarningWindow("Admin window running via default constructor!\nIt is error somewhere.").ShowDialog();
        }

        public MainAdminWindow(MainAdminViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void Expander_OnExpanded(object sender, RoutedEventArgs e)
        {
            var ex = sender as Expander;
            ex?.BringIntoView();
        }
    }
}
