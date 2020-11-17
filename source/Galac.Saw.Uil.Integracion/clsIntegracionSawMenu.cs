using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Integracion;
using Galac.Saw.Brl.Integracion;
using Galac.Saw.Uil.Integracion.ViewModel;
using LibGalac.Aos.UI.Mvvm.Helpers;

namespace Galac.Saw.Uil.Integracion {
    public class clsIntegracionSawMenu: ILibMenu {
        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            IntegracionMngViewModel vViewModel = new IntegracionMngViewModel();
            vViewModel.ExecuteSearchAndInitLookAndFeel();
            LibSearchView insFrmSearch = new LibSearchView(vViewModel);
            if (valUseInterop == 0) {
                insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                insFrmSearch.Show();
            } else {
                insFrmSearch.ShowDialog();
            }
        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkIntegracionViewModel>("Integración", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsIntegracionSawNav());
        }

        public static bool ChooseCompaniaFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkCompaniaIntegracionViewModel>("Compania", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsIntegracionSawNav());
        }
        #endregion //Metodos Generados


    } //End of class clsIntegracionSawMenu
    //provisional por error de versión del componente Integración
    class FkCompaniaIntegracionViewModel {
        [LibGridColum("Código")]
        public string Codigo { get; set; }
        [LibGridColum("Nombre",Width=400 )]
        public string Nombre { get; set; }
        public string CodigoDeIntegracion { get; set; }
        public int ConsecutivoCompania{ get; set; }
    }
} //End of namespace Galac.Saw.Uil.Integracion

