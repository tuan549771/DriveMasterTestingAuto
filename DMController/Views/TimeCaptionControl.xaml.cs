using System;
using System.Collections.Generic;
using System.Linq;
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

namespace DMController.Views
{
    /// <summary>
    /// Interaction logic for TimeCaptionControl.xaml
    /// </summary>
    public partial class TimeCaptionControl : UserControl
    {
        public TimeCaptionControl()
        {
            InitializeComponent();
        }

        private void txtModeValueTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;
            base.OnPreviewTextInput(e);
        }

        private void txtModeValueTime_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space)
                e.Handled = true;
            base.OnPreviewKeyDown(e);
        }


    }
}
