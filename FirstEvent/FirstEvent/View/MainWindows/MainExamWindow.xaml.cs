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
    /// Interaction logic for MainExamWindow.xaml
    /// </summary>
    public partial class MainExamWindow : Window
    {
        public MainExamWindow()
        {
            InitializeComponent();
        }
        
        public MainExamWindow(string dutyOps)
        {
            InitializeComponent();
            DataContext = new MainExamViewModel(dutyOps);
        }

        private void Expander_OnExpanded(object sender, RoutedEventArgs e)
        {
            var ex = sender as Expander;
            ex?.BringIntoView();
        }
    }
}
