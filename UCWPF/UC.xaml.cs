using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UCWPF.Annotations;

namespace UCWPF
{
    /// <summary>
    /// Interaction logic for UC.xaml
    /// </summary>
    public partial class UC : UserControl
    {
        public UC()
        {
            InitializeComponent();
        }

        public TSModel TSModel
        {
            get { return (TSModel)GetValue(TSModelProperty); }
            set { SetValue(TSModelProperty, value); }
        }

        public static readonly DependencyProperty TSModelProperty = DependencyProperty.Register("TSModel", typeof(TSModel), typeof(UC));


        public static readonly DependencyProperty FilenameProperty = DependencyProperty.Register("Filename", typeof(string), typeof(UC));

        public string Filename
        {
            get { return (string)GetValue(FilenameProperty); }
            set { SetValue(FilenameProperty, value); }
        }



    }

    public class TSModel
    {
        
        public string TSName { get; set; }
        
    }
}
