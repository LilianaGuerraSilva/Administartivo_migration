using Galac.Adm.Uil.ImprentaDigital.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Galac.Adm.Uil.ImprentaDigital.Views {
    /// <summary>
    /// Interaction logic for GSEnviarDocumentoView.xaml
    /// </summary>
    public partial class GSEnviarDocumentoView: UserControl {
        #region Constructores

        public GSEnviarDocumentoView() {
            InitializeComponent();
            Loaded += new RoutedEventHandler(GSEnviarDocumentoView_Loaded);
        }

        private void GSEnviarDocumentoView_Loaded(object sender, RoutedEventArgs e) {
            if (((EnviarDocumentoViewModel)DataContext).Accion != LibGalac.Aos.Base.eAccionSR.Emitir) {
                ((EnviarDocumentoViewModel)DataContext).EjecutarProcesos();
            }
        }
        #endregion //Constructores
    } //End of class GSEnviarDocumentoView.xaml

} //End of namespace Galac.Adm.Uil.ImprentaDigital

