using LibGalac.Aos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Galac.Adm.Uil.Vendedor.Views {
    /// <summary>
    /// Interaction logic for GSVendedorView.xaml
    /// </summary>
    public partial class GSVendedorView : UserControl {
        public GSVendedorView() {
            InitializeComponent();
            txtCodigo.LostFocus += TxtCodigo_LostFocus;
        }

        private void TxtCodigo_LostFocus(object sender, RoutedEventArgs e) {
            if (!LibString.IsNullOrEmpty(txtCodigo.Text) && LibString.Len(txtCodigo.Text) < 5) {
                txtCodigo.Text = LibText.FillWithCharToLeft(txtCodigo.Text, "0", 5);
            }
        }
    }
}
