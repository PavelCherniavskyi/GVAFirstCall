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

namespace FirstEvent.View.MainWindows
{
    /// <summary>
    /// Interaction logic for MainTestWindow.xaml
    /// </summary>
    public partial class MainTestWindow : Window
    {
        public MainTestWindow()
        {
            InitializeComponent();
            DataContext = new MainTestViewModel("Defaulf constructor.");
        }

        public MainTestWindow(string dutyOps)
        {
            InitializeComponent();
            DataContext = new MainTestViewModel(dutyOps);
        }

        private void Expander_OnExpanded(object sender, RoutedEventArgs e)
        {
            var ex = sender as Expander;
            ex?.BringIntoView();
        }
    }
}
