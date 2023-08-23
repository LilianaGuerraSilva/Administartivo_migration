using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Uil.Venta.ViewModel  {
    public class C2PMegasoftViewModel : LibGenericViewModel {
        #region Constantes
        #endregion
        #region Propiedades
        public override string ModuleName {
            get { return "Cambio por Pago Móvil C2P"; }
        }

        public string NombreCliente { get; set; }
        public string NroFactura { get; set; }
        public string Monto { get; set; }
        public string Rif { get; set; }
        public eCodigoCel CodigoTelefono { get; set; }
        public string NumeroTelefono { get; set; }
        public eBancoPM Banco { get; set; }
        public decimal Vuelto { get; set; }
        public string CodigoAfiliacion { get; set; }
        public string NroControl { get; set; }

        public eCodigoCel[] ArrayCodigoCel { get { return LibEnumHelper<eCodigoCel>.GetValuesInArray(); } }
        public eBancoPM[] ArrayBancoPM { get { return LibEnumHelper<eBancoPM>.GetValuesInArray(); } }
        #endregion //Propiedades
        #region Constructores
        public C2PMegasoftViewModel() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel() {
        }
        #endregion //Metodos Generados


    } //End of class C2PMegasoftViewModel

} //End of namespace Galac.Adm.Uil.Venta.ViewModel