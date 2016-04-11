using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using TP.Common;

namespace DataMaintenance.View
{
    public class MainWindowView : BindableBase
    {

        private ICommand _openWindowCommand;
        public ICommand OpenWindowCommand {
            get { return _openWindowCommand; }
            set { SetProperty(ref _openWindowCommand, value); }
        }

        public ObservableCollection<StartupConfigurationElement> _startupList;

        public MainWindowView()
        {   
            _startupList = new ObservableCollection<StartupConfigurationElement>();
        }

        public ObservableCollection<StartupConfigurationElement> StartupList {
            get { return _startupList; }
            set { SetProperty(ref _startupList, value); }
        }
    }
}
