using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Contabilizacion;
using Galac.Saw.Brl.Contabilizacion;
using Galac.Saw.Uil.Contabilizacion.ViewModel;

namespace Galac.Saw.Uil.Contabilizacion {
    public class clsReglasDeContabilizacionMenu : ILibMenuMultiFile {        
        #region Metodos Generados
		
		void ILibMenuMultiFile.Ejecuta(eAccionSR valAction, int valUseInterop, IDictionary<string, XmlDocument> refGlobalValues) {
            ReglasDeContabilizacionMngViewModel vViewModel = new ReglasDeContabilizacionMngViewModel();
            vViewModel.ExecuteSearchAndInitLookAndFeel();
            LibSearchView insFrmSearch = new LibSearchView(vViewModel);
            if (valUseInterop == 0) {
                insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                insFrmSearch.Show();
            } else {
                insFrmSearch.ShowDialog();
            }
        }
        #endregion //Metodos Generados


    } //End of class clsReglasDeContabilizacionMenu

} //End of namespace Galac.Saw.Uil.Contabilizacion

