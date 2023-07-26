using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Brl.Venta;
namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsCajaCerradaViewModel : LibInputRptViewModelBase<CajaApertura> {
        #region Variables
        #endregion //Variables
        #region Propiedades
        public override string DisplayName { get { return "Caja Cerrada"; } }
        public LibXmlMemInfo AppMemoryInfo { get; set; }
        public LibXmlMFC Mfc { get; set; }
        public override bool IsSSRS => throw new NotImplementedException();

        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        #endregion //Propiedades
        #region Constructores
        public clsCajaCerradaViewModel() {
            FechaDesde = LibConvert.ToDate("01/01/" + LibDate.Today().Year.ToString());
            FechaHasta = LibDate.Today();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsCajaAperturaNav();
        }
        #endregion //Metodos Generados

    } //End of class clsCajaCerradaViewModel
} //End of namespace Galac.Adm.Uil.Venta