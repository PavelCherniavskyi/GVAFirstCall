using System;
using System.Collections.ObjectModel;
using System.Windows;
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
    }
}
