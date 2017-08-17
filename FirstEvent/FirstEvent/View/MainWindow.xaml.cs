using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
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
        private readonly RelationToSubscrList _relationToSubscrListList = new RelationToSubscrList();

        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register(this, new Action<string>(ProcessMessage));
        }

        private void ProcessMessage(string msg)
        {
            switch (msg)
            {
                case "DoctorsListShow":
                    _doctorsList.ShowDialog();
                    break;
                case "DoctorsListHide":
                    _doctorsList.Hide();
                    break;
                case "SubscrListShow":
                    _relationToSubscrListList.ShowDialog();
                    break;
                case "SubscrListHide":
                    _relationToSubscrListList.Hide();
                    break;
            }
        }
    }
}
