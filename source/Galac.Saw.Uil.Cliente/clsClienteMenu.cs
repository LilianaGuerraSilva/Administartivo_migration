using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Cliente;
using Galac.Saw.Uil.Cliente.ViewModel;
using Galac.Saw.Brl.Cliente;
using LibGalac.Aos.UI.Mvvm;
using Galac.Saw.Uil.Cliente.Reportes;
using LibGalac.Aos.UI.Mvvm.Messaging;

namespace Galac.Saw.Uil.Cliente {
    public class clsClienteMenu: ILibMenu {
        #region Constructores
        public clsClienteMenu() {
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Miembros de ILibMenuMultiFile
        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            if (valAction == eAccionSR.InformesPantalla) {
                EjecutaInformesCliente();
            } else {
                ClienteMngViewModel vViewModel = new ClienteMngViewModel();
                vViewModel.ExecuteSearchAndInitLookAndFeel();
                LibSearchView insFrmSearch = new LibSearchView(vViewModel);
                if (valUseInterop == 0) {
                    insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                    insFrmSearch.Show();
                } else {
                    insFrmSearch.ShowDialog();
                }
            }
        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkClienteViewModel>("Cliente", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsClienteNav());
        }
        #endregion //Miembros de ILibMenu
        #endregion //Metodos Generados
        private void EjecutaInformesCliente() {
            LibReportsViewModel _LibReportsViewModel = new clsClienteInformesViewModel();
            if (LibMessages.ReportsView.ShowReportsView(_LibReportsViewModel, false)) {
            }
        }


    } //End of class clsClienteMenu

} //End of namespace Galac.Saw.Uil.Clientes

