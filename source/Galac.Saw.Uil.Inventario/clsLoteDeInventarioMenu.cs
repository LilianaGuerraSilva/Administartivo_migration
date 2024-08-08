using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Uil.Inventario.ViewModel;
using System.Xml.Linq;

namespace Galac.Saw.Uil.Inventario {
    public class clsLoteDeInventarioMenu: ILibMenu {
        int _ConsecutivoCompana;
        string _CodigoArticulo;

        public clsLoteDeInventarioMenu(int initConsecutivoCompania, string initCodigoArticulo) {
            _ConsecutivoCompana = initConsecutivoCompania;
            _CodigoArticulo = initCodigoArticulo;
        }

        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            //XElement vLotesPorArticulo = ((ILoteDeInventarioPdn)new clsLoteDeInventarioMovimientoNav()).FindByConsecutivoCompaniaCodigoArticulo(_ConsecutivoCompana, _CodigoArticulo);
            //LoteDeInventarioMngViewModel vViewModel = new LoteDeInventarioMngViewModel();
            //vViewModel.ExecuteConsultarLotesPorArticulo(vLotesPorArticulo);
            //vViewModel.ExecuteSearchAndInitLookAndFeel();
            //LibSearchView insFrmSearch = new LibSearchView(vViewModel);
            //if (valUseInterop == 0) {
            //    insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
            //    insFrmSearch.Show();
            //} else {
            //    insFrmSearch.ShowDialog();
            //}
        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkLoteDeInventarioViewModel>("Lote de Inventario", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsLoteDeInventarioNav(), "ConsecutivoCompania, FechaDeVencimiento, FechaDeElaboracion, Consecutivo", true);
        }
        #endregion //Metodos Generados


    } //End of class clsLoteDeInventarioMenu

} //End of namespace Galac.Saw.Uil.Inventario

