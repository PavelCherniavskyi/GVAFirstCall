using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FirstEvent.Model;
using FirstEvent.ViewModel;
using FirstEvent.ViewModel.Sections;
using GalaSoft.MvvmLight.Messaging;

namespace FirstEvent.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel("Testing", false);
        }
        
        public MainWindow(string dutyOps, bool isExam)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(dutyOps, isExam);
        }

        public MainWindow(MainWindowViewModel vm)
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
