using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Wpf;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Galac.Saw.Lib.Uil.Controls {
    public partial class GSCuadroDeBusqueda : UserControl {
        private const string CuadroDeBusquedaDeArticulosViewModelPropertyName = "CuadroDeBusquedaDeArticulosViewModel";
        private const string CuadroDeBusquedaDeArticulosVerificadorViewModelPropertyName = "CuadroDeBusquedaDeArticulosVerificadorViewModel";
        private const string CuadroDeBusquedaDeClientesViewModelPropertyName = "CuadroDeBusquedaDeClientesViewModel";
        public GSCuadroDeBusqueda() {
            InitializeComponent();
            LibMessages.Notification.Register<string>(this, EnfocarFiltroDeBusqueda);
        }
        public void click_boton(object sender, EventArgs e) {
            ((ISearchBoxViewModel)DataContext).SelectItemFromListCommand.Execute(((Button)sender).Content);
        }
        #region Cambio de Foco
        private void TextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Down) {
                e.Handled = true;
                if (Elementos.Items.Count == 0) {
                    return;
                }
                if (Elementos.SelectedItem == null) {
                    Elementos.SelectedItem = Elementos.Items[0];
                }
                Elementos.UpdateLayout();
                if (!EnfocarItem())
                    Elementos.Focus();
            }
        }

        private bool EnfocarItem() {
            var vListBoxItem = (ListBoxItem)Elementos
                              .ItemContainerGenerator
                              .ContainerFromItem(Elementos.SelectedItem);
            if (vListBoxItem == null) {
                return false;
            }
            vListBoxItem.Focus();
            return true;
        }
        #endregion

        #region Cambio de Foco y Cargado de Páginas en listbox 
        private void Elementos_PreviewKeyDown(object sender, KeyEventArgs e) {
            ScrollViewer scrollViewer = ObtenerScrollViewer();
            var z = scrollViewer.ContentVerticalOffset;

            if (e.Key == Key.Up && ((ListBox)sender).SelectedIndex == 0 && ((ISearchBoxViewModel)DataContext).CurrentPage == 1
                || e.Key == Key.Enter) {
                if (e.Key == Key.Enter)
                    ((ISearchBoxViewModel)DataContext).SelectItemFromListCommand.Execute(Elementos.SelectedItem);
                EnfocarYSeleccionar();
            } else if (e.Key == Key.Up && ((ListBox)sender).SelectedIndex == 0) {
                ((ISearchBoxViewModel)DataContext).LoadPreviousPage();
                Elementos.SelectedItem = Elementos.Items[Elementos.Items.Count-1];
                EnfocarItem();
            } else if (e.Key == Key.Down && ((ListBox)sender).SelectedIndex == 9) {
                if (((ISearchBoxViewModel)DataContext).LoadNextPage()) {
                    var vFirstElement = LibXamlHelper.FindVisualChildren<ListBoxItem>(Elementos).FirstOrDefault();
                    Elementos.Focus();
                    vFirstElement.Focus();
                }
            } else if (e.Key == Key.Up && ((ListBox)sender).SelectedItem == null) {
                Elementos.SelectedIndex = 9;
            }
        }

        private void EnfocarFiltroDeBusqueda(NotificationMessage<string> notificationMessage) {
            if (notificationMessage.Notification.Equals("Focus")) {
                switch (DataContext.GetType().Name) {
                    case CuadroDeBusquedaDeArticulosViewModelPropertyName:
                        EnfocarUOcultar(notificationMessage, CuadroDeBusquedaDeArticulosViewModelPropertyName);
                        break;
                    case CuadroDeBusquedaDeArticulosVerificadorViewModelPropertyName:
                        EnfocarUOcultar(notificationMessage, CuadroDeBusquedaDeArticulosVerificadorViewModelPropertyName);
                        break;
                    case CuadroDeBusquedaDeClientesViewModelPropertyName:
                        EnfocarUOcultar(notificationMessage, CuadroDeBusquedaDeClientesViewModelPropertyName);
                        break;
                    default:
                        break;
                }
            }
        }

        private void EnfocarUOcultar(NotificationMessage<string> notificationMessage, string viewModelPropertyName) {
            if (notificationMessage.Content == viewModelPropertyName) {
                EnfocarYSeleccionar();
            } else {
                ((ISearchBoxViewModel)DataContext).HideListCommand.Execute(null);
            }
        }

        private void EnfocarYSeleccionar() {
            FiltroDeBusqueda.Focus();
            FiltroDeBusqueda.SelectAll();
            var viewModel = (ISearchBoxViewModel)DataContext;
            ((ISearchBoxViewModel)DataContext).HideListCommand.Execute(null);
        }

        private void Elementos_MouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer scrollViewer = ObtenerScrollViewer();
            var posicionScroll = scrollViewer.ContentVerticalOffset;
            if (e.Delta < 0 && posicionScroll >= 5) {
                ((ISearchBoxViewModel)DataContext).LoadNextPage();
            }
            if (e.Delta > 0 && posicionScroll == 0 && ((ISearchBoxViewModel)DataContext).CurrentPage != 1) {
                ((ISearchBoxViewModel)DataContext).LoadPreviousPage();
            }
        }

        private ScrollViewer ObtenerScrollViewer() {
            Decorator border = VisualTreeHelper.GetChild(Elementos, 0) as Decorator;
            ScrollViewer scrollViewer = border.Child as ScrollViewer;
            return scrollViewer;
        }
        #endregion
    }
}
