using Galac.Saw.Uil.Inventario.ViewModel;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.UI.WpfControls;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Galac.Saw.Uil.Inventario.Views {
    /// <summary>
    /// Interaction logic for GSVerificadorDePreciosView.xaml
    /// </summary>
    public partial class GSVerificadorDePreciosView : Window {
        public GSVerificadorDePreciosView(VerificadorDePreciosViewModel verificadorDePreciosViewModel) {
            InitializeComponent();
            Loaded += GSVerificadorDePreciosView_Loaded;
            DataContext = verificadorDePreciosViewModel;
            FocusManager.SetFocusedElement(this, CuadroDeBusqueda);
            Closing += GSVerificadorDePreciosView_Closing;
        }

        private void GSVerificadorDePreciosView_Loaded(object sender, RoutedEventArgs e) {
            var viewModel = DataContext as VerificadorDePreciosViewModel;
            if (viewModel != null) {
                if (!viewModel.BusquedaPorCodigo) {
                    viewModel.CuadroDeBusquedaDeArticulosViewModel.NotifyFocusAndSelect();
                } else {
                    viewModel.FocusCuadroBusquedaPorCodigo();
                }
            }
        }

        private void GSVerificadorDePreciosView_Closing(object sender, CancelEventArgs e) {
            var viewModel = DataContext as VerificadorDePreciosViewModel;
            if (viewModel != null && viewModel.RequestLoginAtClosing) {
                var login = new LibGalac.Aos.Uil.Usal.GUserLogin();
                e.Cancel = !login.RequestCredential("Confirme Credenciales para salir");
            }
        }

        private void CuadroDeBusquedaPorCodigo_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                var vTxtCriteria = LibXamlHelper.FindVisualChild<GSTextBoxWpf>((GSSeekConnection)sender, "PART_SearchCriteria");
                if (vTxtCriteria != null) {
                    CuadroDeBusquedaPorCodigo.SearchCommand.Execute(vTxtCriteria.Text);
                    vTxtCriteria.SelectAll();
                }
            } else if (e.Key == Key.Tab) {
                e.Handled = true;
            }
        }
    }
}
