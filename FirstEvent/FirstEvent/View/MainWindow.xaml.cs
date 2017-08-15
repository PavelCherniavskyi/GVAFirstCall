using System;
using System.Collections.ObjectModel;
using System.Windows;
using FirstEvent.Model;
using GalaSoft.MvvmLight.Messaging;

namespace FirstEvent.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private readonly DoctorsList _doctorsList = new DoctorsList();
        private Welcome _welcome = new Welcome();

        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register(this, new Action<string>(ProcessMessage));
            
        }

        private void ProcessMessage(string msg)
        {
            switch (msg)
            {
                case "doctorsListShow":
                    _doctorsList.ShowDialog();
                    break;
                case "doctorsListHide":
                    _doctorsList.Hide();
                    break;
            }
        }

        

        
    }
}
