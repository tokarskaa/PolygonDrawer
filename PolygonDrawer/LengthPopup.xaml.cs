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
using System.Windows.Shapes;

namespace PolygonDrawer
{
    /// <summary>
    /// Logika interakcji dla klasy LengthPopup.xaml
    /// </summary>
    public partial class LengthPopup : Window
    {
        public int Length;
        public LengthPopup()
        {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (!Int32.TryParse(text.Text, out Length))
            {
                MessageBox.Show("Wrong input!");
            }
            else
                DialogResult = true;
        }
    }
}
