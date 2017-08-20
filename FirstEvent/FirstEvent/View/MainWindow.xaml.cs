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
        private readonly RelationToSubscrList _relationToSubscrList = new RelationToSubscrList();
        private readonly CountryList _countryList = new CountryList();
        private readonly RegionList _regionList = new RegionList();
        private readonly CityList _cityList = new CityList();

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
                    _relationToSubscrList.ShowDialog();
                    break;
                case "SubscrListHide":
                    _relationToSubscrList.Hide();
                    break;
                case "CountryListShow":
                    _countryList.ShowDialog();
                    break;
                case "CountryListHide":
                    _countryList.Hide();
                    break;
                case "RegionListShow":
                    _regionList.ShowDialog();
                    break;
                case "RegionListHide":
                    _regionList.Hide();
                    break;
                case "CityListShow":
                    _cityList.ShowDialog();
                    break;
                case "CityListHide":
                    _cityList.Hide();
                    break;
            }
        }
    }
}
