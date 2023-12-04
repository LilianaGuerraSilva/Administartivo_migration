using System;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Galac.Adm.Uil.SttDef {
    /// <summary>
    /// Interaction logic for winInformacionGVentas.xaml
    /// </summary>
    public partial class winInformacionGVentas : System.Windows.Window {
        public winInformacionGVentas() {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e) {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }
    }
}
