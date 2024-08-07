using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt;
using LibGalac.Aos.ARRpt.Reports;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Cib;

namespace Galac.Saw.Uil.Inventario.Reportes {

    public class clsNotaDeEntradaSalidaInformesViewModel : LibReportsViewModel {
        #region Constructores

        public clsNotaDeEntradaSalidaInformesViewModel()
            : this(null, null) {
        }

        public clsNotaDeEntradaSalidaInformesViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            Title = "Informes de Nota de Entrada/Salida";
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsNotaDeEntradaSalidaInformesViewModel

} //End of namespace Galac.Saw.Uil.Inventario

