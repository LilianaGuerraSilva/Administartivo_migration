using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Brl.Tablas;
using Galac.Saw.Uil.Tablas.ViewModel;

namespace Galac.Saw.Uil.Tablas {
    public class clsOtrosCargosDeFacturaMenu: ILibMenu {
        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            OtrosCargosDeFacturaMngViewModel vViewModel = new OtrosCargosDeFacturaMngViewModel();
            vViewModel.ExecuteSearchAndInitLookAndFeel();
            LibSearchView insFrmSearch = new LibSearchView(vViewModel);
            if (valUseInterop == 0) {
                insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                insFrmSearch.Show();
            } else {
                insFrmSearch.ShowDialog();
            }
        }

        public static bool ChooseOtrosCargosDeFactura(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkOtrosCargosDeFacturaViewModel>("Otros Cargos de Factura", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsOtrosCargosDeFacturaNav());
        }
        #endregion //Metodos Generados


    } //End of class clsOtrosCargosDeFacturaMenu

} //End of namespace Galac.Saw.Uil.Tablas

