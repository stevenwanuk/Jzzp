using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TP.Annotations;
using TP.ModelView;

namespace TP.View
{
    public class MainView : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<TPCallInMV> _callInMvs;
        public ObservableCollection<TPCallInMV> callInMvs
        {
            get { return _callInMvs; }
            set
            {
                if (_callInMvs != value)
                {
                    _callInMvs = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
