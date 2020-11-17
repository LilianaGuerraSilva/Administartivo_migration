using Galac.Adm.Ccl.GestionCompras;
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
using Galac.Adm.Uil.GestionCompras.ViewModel;

namespace Galac.Adm.Uil.GestionCompras.Views {
    /// <summary>
    /// Interaction logic for GSCompraView.xaml
    /// </summary>
    public partial class GSCompraView : UserControl {
        #region Constructores

        public GSCompraView() {
            InitializeComponent();
            DataContextChanged += new DependencyPropertyChangedEventHandler(GSCompraView_DataContextChanged);
            Loaded += GSCompraView_Loaded;
            //dgArticulo.LoadingRow += new EventHandler<DataGridRowEventArgs>(dgArticulo_LoadingRow);            
        }

        void dgArticulo_LoadingRow(object sender, DataGridRowEventArgs e) {
            dgArticulo.ScrollIntoView(e.Row.Item);
        }

        
        private void GSCompraView_Loaded(object sender, RoutedEventArgs e) {
            AjustaGridDeDetalle();
        }

        private void GSCompraView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            try {
                Galac.Adm.Uil.GestionCompras.ViewModel.CompraViewModel vViewModel = DataContext as Galac.Adm.Uil.GestionCompras.ViewModel.CompraViewModel;
                if (vViewModel != null) {
                    vViewModel.AjustaColumnasSegunTipoEvent += new EventHandler(vViewModel_AjustaColumnasSegunTipoEvent);                    
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void vViewModel_AjustaColumnasSegunTipoEvent(object sender, EventArgs e) {
            AjustaGridDeDetalle();
        }

        private void AjustaGridDeDetalle() {
            Galac.Adm.Uil.GestionCompras.ViewModel.CompraViewModel vViewModel = DataContext as Galac.Adm.Uil.GestionCompras.ViewModel.CompraViewModel;
            for (int i = 0; i < dgArticulo.Columns.Count ; i++) {
                dgArticulo.Columns[i].Visibility = Visibility.Visible ;
            }
            if (!vViewModel.VieneDeOrdenDeCompra) {
                dgArticulo.Columns[4].Visibility = Visibility.Collapsed;
            }
            if (vViewModel.TipoDeDistribucion == eTipoDeDistribucion.ManualPorMonto) {
                dgArticulo.Columns[5].Visibility = Visibility.Collapsed;
                dgArticulo.Columns[7].Visibility = Visibility.Collapsed;
            } else if (vViewModel.TipoDeDistribucion == eTipoDeDistribucion.ManualPorPorcentaje) {
                dgArticulo.Columns[6].Visibility = Visibility.Collapsed;
                dgArticulo.Columns[7].Visibility = Visibility.Collapsed;
            } else if (vViewModel.TipoDeDistribucion == eTipoDeDistribucion.Automatica) {
                dgArticulo.Columns[5].Visibility = Visibility.Collapsed;
                dgArticulo.Columns[6].Visibility = Visibility.Collapsed;
                if (!vViewModel.UsaSeguro) {
                    dgArticulo.Columns[7].Visibility = Visibility.Collapsed;
                }
            } else if (vViewModel.TipoDeDistribucion == eTipoDeDistribucion.Ninguno) {
                dgArticulo.Columns[5].Visibility = Visibility.Collapsed;
                dgArticulo.Columns[6].Visibility = Visibility.Collapsed;
                dgArticulo.Columns[7].Visibility = Visibility.Collapsed;
            }

        }


        #endregion //Constructores

        private void dgArticulo_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            CompraDetalleArticuloInventarioViewModel vViewModel = dgArticulo.SelectedItem as CompraDetalleArticuloInventarioViewModel;
            if (vViewModel != null) {
                if (vViewModel.TipoArticuloInv == Saw.Ccl.Inventario.eTipoArticuloInv.UsaSerial || vViewModel.TipoArticuloInv == Saw.Ccl.Inventario.eTipoArticuloInv.UsaSerialRollo || vViewModel.TipoArticuloInv == Saw.Ccl.Inventario.eTipoArticuloInv.UsaTallaColorySerial) {
                    vViewModel.BuscarSerialLote(vViewModel.CodigoArticuloInv, vViewModel.CodigoGrupo, vViewModel.CodigoArticulo, vViewModel.TipoArticuloInv, true);
                }
            }
        }

    } //End of class GSCompraView.xaml

} //End of namespace Galac.Adm.Uil.GestionCompras

