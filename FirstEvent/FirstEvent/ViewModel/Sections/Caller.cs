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
        private RelationToSubscr _relationToSubscr;
        private RelayCommand _enterKeyCommand;
        private Brush _relationToSubscrColorTextBox;
        private bool _isRelToSubscrReadOnly;

        public Caller()
        {
            Messenger.Default.Register(this, new Action<RelationToSubscr>(CallerMessage));
            _relationToSubscr = new RelationToSubscr();
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

        public RelationToSubscr RelationToSubscr
        {
            get { return _relationToSubscr; }
            set { _relationToSubscr = value; RaisePropertyChanged("RelationToSubscr"); }
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
                    (_showRelationToSubscrListCommand = new RelayCommand(() => Messenger.Default.Send<string>("subscrListShow"), () => true));
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
            RelationToSubscr = new RelationToSubscr();
            IsRelToSubscrReadOnly = false;
        }

        private void EnterKeyCommandExecute()
        {
            var tempId = int.Parse(RelationToSubscr.Relation);
            foreach (var d in DataBaseManager.RelationsToSubscr)
            {
                if (tempId != d.Id)
                    continue;
                RelationToSubscr = d;
                IsRelToSubscrReadOnly = true;
            }
        }

        private void CallerMessage(RelationToSubscr relToSubscr)
        {
            if (relToSubscr != null)
            {
                RelationToSubscr = relToSubscr;
                IsRelToSubscrReadOnly = true;
            }
            Messenger.Default.Send<string>("subscrListHide");

        }
    }
}
