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

namespace Galac.Adm.Uil.Venta {
    /// <summary>
    /// Interaction logic for winMesesGenerados.xaml
    /// </summary>
    public partial class winMesesGenerados : Window {
        public winMesesGenerados(string initNumeroContrato, string initConjuntoMesesGenerados) {
            InitializeComponent();
            string[] vMesesGenerados = LibGalac.Aos.Base.LibString.Split(initConjuntoMesesGenerados, ",");
            FillLisBoxWithMesesGenerados(vMesesGenerados);
            lblNumeroDeContrato.Content = "Contrato Número: " + initNumeroContrato;
        }

        private void FillLisBoxWithMesesGenerados(string[] valMesesGenerados) {
            lsbMesesGenerados.Items.Clear();
            foreach (string vItem in valMesesGenerados) {
                lsbMesesGenerados.Items.Add(vItem);
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
