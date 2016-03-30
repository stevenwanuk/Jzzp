using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Internal;
using Jzzp.DAL;
using ModelGenerator;
using TP.Annotations;
using TP.Common;
using TP.ModelView;

namespace TP.View
{
    public class MainView : BindableBase
    {

        public MainView()
        {
            _callInMvs = new ObservableCollection<TPCallInMV>();
        }

        private TPCallInMV _selectedTPCallInMV;
        public TPCallInMV SelectedTPCallInMV {
            get { return _selectedTPCallInMV; }
            set {

                if (SetProperty(ref _selectedTPCallInMV, value))
                {

                    if (value?.TPBillRef != null)
                    {
                        foreach (var tpBillRefMv in value.TPBillRef)
                        {

                            if (tpBillRefMv != null)
                            {
                                //tpBillRefMv.
                            }
                        }
                        
                    }
                };
            }
        }

        private ObservableCollection<TPCallInMV> _callInMvs;
        public ObservableCollection<TPCallInMV> CallInMvs
        {
            get { return _callInMvs; }
            set { SetProperty(ref _callInMvs, value);}
        }

    }
}
