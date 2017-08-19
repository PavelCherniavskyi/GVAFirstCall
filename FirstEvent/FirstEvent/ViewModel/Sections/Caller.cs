using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using FirstEvent.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FirstEvent.ViewModel.Sections
{
    public class Caller : ViewModelBase
    {
        private RelayCommand _showRelationToSubscrListCommand;
        private RelayCommand _cancelRelationToSubscrListCommand;
        private RelationToSubscriber _relationToSubscriber;
        private RelayCommand _enterKeyCommand;
        private Brush _relationToSubscrColorTextBox;
        private bool _isRelToSubscrReadOnly;

        public Caller()
        {
            Messenger.Default.Register(this, "CallerRelationToSubscr", new Action<RelationToSubscriber>(CallerMessage));
            _relationToSubscriber = new RelationToSubscriber();
        }

        public bool IsRelToSubscrReadOnly
        {
            get { return _isRelToSubscrReadOnly; }
            set
            {
                _isRelToSubscrReadOnly = value;
                RelationToSubscrColorTextBox = _isRelToSubscrReadOnly ?
                    new SolidColorBrush(Color.FromArgb(255, 240, 240, 240)) :
                    new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                RaisePropertyChanged("IsRelToSubscrReadOnly");
            }
        }

        public RelationToSubscriber RelationToSubscriber
        {
            get { return _relationToSubscriber; }
            set { _relationToSubscriber = value; RaisePropertyChanged("RelationToSubscriber"); }
        }

        public Brush RelationToSubscrColorTextBox
        {
            get { return _relationToSubscrColorTextBox; }
            set { _relationToSubscrColorTextBox = value; RaisePropertyChanged("RelationToSubscrColorTextBox"); }
        }

        public ICommand ShowRelationToSubscrListWindow
        {
            get
            {
                return _showRelationToSubscrListCommand ??
                    (_showRelationToSubscrListCommand = new RelayCommand(() => Messenger.Default.Send<string>("SubscrListShow"), () => true));
            }
        }

        public ICommand EnterKeyCommand
        {
            get
            {
                return _enterKeyCommand ??
                    (_enterKeyCommand = new RelayCommand(EnterKeyCommandExecute, () => true));
            }
        }

        public ICommand CancelRelationToSubscrListWindow
        {
            get
            {
                return _cancelRelationToSubscrListCommand ??
                    (_cancelRelationToSubscrListCommand = new RelayCommand(ClearDocFeildExecute, () => true));
            }
        }

        private void ClearDocFeildExecute()
        {
            RelationToSubscriber = new RelationToSubscriber();
            IsRelToSubscrReadOnly = false;
        }

        private void EnterKeyCommandExecute()
        {
            int tempId;
            if (int.TryParse(RelationToSubscriber.Name, out tempId))
            {
                foreach (var d in DataBaseManager.RelationsToSubscriber)
                {
                    if (tempId != d.Id)
                        continue;
                    RelationToSubscriber = d;
                    IsRelToSubscrReadOnly = true;
                    break;
                }
            }
            else
            {
                var strToSrch = RelationToSubscriber.Name.ToUpper();

                foreach (var d in DataBaseManager.RelationsToSubscriber)
                {
                    if (!d.Name.ToUpper().Contains(strToSrch))
                        continue;
                    RelationToSubscriber = d;
                    IsRelToSubscrReadOnly = true;
                }
            }
        }

        private void CallerMessage(RelationToSubscriber rel)
        {
            if (rel != null)
            {
                RelationToSubscriber = rel;
                IsRelToSubscrReadOnly = true;
            }
            Messenger.Default.Send<string>("SubscrListHide");

        }
    }
}
