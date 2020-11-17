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
using LibGalac.Aos.Base;
using Galac.Adm.Uil.Venta.ViewModel;
using LibGalac.Aos.UI.WpfControls;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.UI.Mvvm.Helpers;

namespace Galac.Adm.Uil.Venta.Views {
    /// <summary>
    /// Interaction logic for GSFacturaRapidaView.xaml
    /// </summary>
    public partial class GSFacturaRapidaView : UserControl {
        #region Constructores

        public GSFacturaRapidaView() {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(GSFacturaRapidaView_Loaded);
            txtArticulo.PreviewKeyDown += new KeyEventHandler(txtArticulo_PreviewKeyDown);
            txtArticulo.PreviewKeyUp += new KeyEventHandler(txtArticulo_PreviewKeyUp);
            txtArticulo.GotFocus += new RoutedEventHandler(txtArticulo_GotFocus);
            txtCantidad.PreviewGotKeyboardFocus += new KeyboardFocusChangedEventHandler(txtCantidad_PreviewGotKeyboardFocus);
            txtCantidad.PreviewKeyUp += new KeyEventHandler(txtCantidad_PreviewKeyUp);
            DataContextChanged += new DependencyPropertyChangedEventHandler(GSFacturaRapidaView_DataContextChanged);
            dFacturaRapidaDetalle.LoadingRow += DFacturaRapidaDetalle_LoadingRow;
            dFacturaRapidaDetalle.BeginningEdit += new EventHandler<DataGridBeginningEditEventArgs>(dFacturaRapidaDetalle_BeginningEdit);
            dFacturaRapidaDetalle.CellEditEnding += new EventHandler<DataGridCellEditEndingEventArgs>(dFacturaRapidaDetalle_CellEditEnding);
        }

        void dFacturaRapidaDetalle_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e) {
                int vIndexColumPrecio = 3;
                if (e.Column.DisplayIndex == vIndexColumPrecio) {
                    txtArticulo.Focus();
                }
        }

        #endregion //Constructores

        private void GSFacturaRapidaView_Loaded(object sender, RoutedEventArgs e) {
            try {
                var vParentWindow = LibXamlHelper.FindVisualParent<Window>(this);
                if (vParentWindow != null) {
                    vParentWindow.PreviewKeyDown += new KeyEventHandler(ParentWindow_PreviewKeyDown);
                    if (txtNumeroRIF.Visibility == System.Windows.Visibility.Visible && txtNumeroRIF.IsEnabled) {
                        FocusManager.SetFocusedElement(this, txtNumeroRIF);
                        txtNumeroRIF.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                    } else {
                        FocusManager.SetFocusedElement(this, txtNombreCliente);
                        txtNombreCliente.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                    }
                }
                dFacturaRapidaDetalle.Width = ActualWidth - 7;
                dFacturaRapidaDetalle.Height = ActualHeight - 290;
                ((FacturaRapidaViewModel)DataContext).ValidateImpresoraFiscal();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ParentWindow_PreviewKeyDown(object sender, KeyEventArgs e) {
            try {
                if (e.Key == Key.Down || e.Key == Key.Up || e.Key == Key.Left || e.Key == Key.Right) {
                    if (dFacturaRapidaDetalle.IsFocused && dFacturaRapidaDetalle.IsKeyboardFocusWithin) {
                        FocusManager.SetFocusedElement(this, dFacturaRapidaDetalle);
                        dFacturaRapidaDetalle.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                    }
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void txtArticulo_GotFocus(object sender, RoutedEventArgs e) {
            try {
                var vTxtCriteria = LibXamlHelper.FindVisualChild<GSTextBoxWpf>(txtArticulo, "PART_SearchCriteria");
                if (vTxtCriteria != null) {
                    vTxtCriteria.SelectAll();
                }
                dFacturaRapidaDetalle.SelectedIndex = -1;
                FacturaRapidaViewModel vViewModel = DataContext as FacturaRapidaViewModel;
                vViewModel.DetailFacturaRapidaDetalle.SelectedIndex = -1;
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void txtArticulo_PreviewKeyDown(object sender, KeyEventArgs e) 
        {
            try {
                if (e.Key == Key.Enter) {
                    FacturaRapidaViewModel vViewModel = DataContext as FacturaRapidaViewModel;
                    if (vViewModel != null) {
                        vViewModel.IsEnterKeyPressedArticulo = true;
                    }
                    var vTxtCriteria = LibXamlHelper.FindVisualChild<GSTextBoxWpf>(txtArticulo, "PART_SearchCriteria");
                    if (vTxtCriteria != null) {
                        txtArticulo.SearchCommand.Execute(vTxtCriteria.Text);
                        vTxtCriteria.SelectAll();
                    }
                } else if (e.Key == Key.Tab) {
                    e.Handled = true;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void txtArticulo_PreviewKeyUp(object sender, KeyEventArgs e) {
            try {
                FacturaRapidaViewModel vViewModel = DataContext as FacturaRapidaViewModel;
                if (vViewModel != null) {
                    vViewModel.IsEnterKeyPressedArticulo = false;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void txtCantidad_PreviewKeyUp(object sender, KeyEventArgs e) {
            try {
                if (e.Key == Key.Enter) {
                    FocusManager.SetFocusedElement(this, txtArticulo);
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void txtCantidad_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) {
            try {
                FacturaRapidaViewModel vViewModel = DataContext as FacturaRapidaViewModel;
                if (vViewModel != null) {
                    vViewModel.LimpiarArticulo();
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        void GSFacturaRapidaView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {

            try {
                Galac.Adm.Uil.Venta.ViewModel.FacturaRapidaViewModel vViewModel = DataContext as Galac.Adm.Uil.Venta.ViewModel.FacturaRapidaViewModel;
                if (vViewModel != null) {
                    vViewModel.IrAlGridEvent += new EventHandler(vViewModel_IrAlGridEvent);
                    vViewModel.IrADescripcionGridEvent += new EventHandler(vViewModel_IrADescripcionEvent);
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }

        }

        void vViewModel_IrAlGridEvent(object sender, EventArgs e) {
            try {
                int vIndexColumnPrecio = 3;
                dFacturaRapidaDetalle.SelectedIndex = dFacturaRapidaDetalle.Items.Count - 2;
                dFacturaRapidaDetalle.SelectedItem = dFacturaRapidaDetalle.Items[dFacturaRapidaDetalle.SelectedIndex];
                DataGridRow vDataGridRow = (DataGridRow)dFacturaRapidaDetalle.ItemContainerGenerator.ContainerFromIndex(dFacturaRapidaDetalle.SelectedIndex);
                System.Windows.Controls.Primitives.DataGridCellsPresenter cellPresenter = LibXamlHelper.FindVisualChildren<System.Windows.Controls.Primitives.DataGridCellsPresenter>(vDataGridRow).FirstOrDefault();
                if (cellPresenter != null) {
                    System.Windows.Controls.DataGridCell vDataGridCell = (System.Windows.Controls.DataGridCell)(cellPresenter.ItemContainerGenerator.ContainerFromIndex(vIndexColumnPrecio));
                    vDataGridCell.Focus();
                    vDataGridCell.IsEditing = true;
                } else {
                    vDataGridRow.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void DFacturaRapidaDetalle_LoadingRow(object sender, DataGridRowEventArgs e) {
            try {
                dFacturaRapidaDetalle.ScrollIntoView(e.Row.Item);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        void dFacturaRapidaDetalle_BeginningEdit(object sender, DataGridBeginningEditEventArgs e) {
            if (!LibSecurityManager.CurrentUserHasAccessTo("Punto de Venta", "Modificar Precio del Item")) {
                int vIndexColumPrecio = 3;
                if (e.Column.DisplayIndex == vIndexColumPrecio) {
                    LibGalac.Aos.Uil.Usal.GUserLogin vGUserLogin = new LibGalac.Aos.Uil.Usal.GUserLogin();
                    List<CustomRole> vListRoles = new List<CustomRole>();
                    vListRoles.Add(new CustomRole("Punto de Venta", "Modificar Precio del Item"));
                    if (!vGUserLogin.RequestCredential("Modificar Precio del Item", true, vListRoles) || !LibSecurityManager.CurrentUserIsSuperviser()) {                                            
                        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(null, "Usuario o Clave no válido.", "Información");
                        e.Cancel = true;
                    }
                }
            }
            if (!LibSecurityManager.CurrentUserHasAccessTo("Punto de Venta", "Modificar Descripción del Item")) {
                int vIndexColumnDescripcion = 1;
                if (e.Column.DisplayIndex == vIndexColumnDescripcion) {
                    e.Cancel = true;
                }
                
            }
        }

        void vViewModel_IrADescripcionEvent(object sender, EventArgs e) {
            try {
                int vIndexColumnDescripcion = 1;
                dFacturaRapidaDetalle.SelectedIndex = dFacturaRapidaDetalle.Items.Count - 2;
                dFacturaRapidaDetalle.SelectedItem = dFacturaRapidaDetalle.Items[dFacturaRapidaDetalle.SelectedIndex];
                DataGridRow vDataGridRow = (DataGridRow)dFacturaRapidaDetalle.ItemContainerGenerator.ContainerFromIndex(dFacturaRapidaDetalle.SelectedIndex);
                System.Windows.Controls.Primitives.DataGridCellsPresenter cellPresenter = LibXamlHelper.FindVisualChildren<System.Windows.Controls.Primitives.DataGridCellsPresenter>(vDataGridRow).FirstOrDefault();
                if (cellPresenter != null) {
                    System.Windows.Controls.DataGridCell vDataGridCell = (System.Windows.Controls.DataGridCell)(cellPresenter.ItemContainerGenerator.ContainerFromIndex(vIndexColumnDescripcion));
                    vDataGridCell.Focus();
                    vDataGridCell.IsEditing = true;
                } else {
                    vDataGridRow.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void txtNombreVendedor_PreviewKeyUp(object sender, KeyEventArgs e) {
            try {
                if (e.Key == Key.Enter) {
                    FocusManager.SetFocusedElement(this, txtArticulo);
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void txtNombreVendedor_PreviewKeyDown(object sender, KeyEventArgs e) {
            try {
                if (e.Key == Key.Enter) {
                    FocusManager.SetFocusedElement(this, txtNombreVendedor);
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }
    } //End of class GSFacturaRapidaView.xaml

} //End of namespace Galac.Adm.Uil.Venta

