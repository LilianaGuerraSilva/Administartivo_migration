using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class VueltoEfectivoViewModel : LibGenericViewModel {
        public override string ModuleName { get { return "Vuelto en Efectivo"; } }

        decimal _CambioAMonedaLocalParaMostrar;
        public decimal CambioAMonedaLocalParaMostrar {
            get { return _CambioAMonedaLocalParaMostrar; }
            set { _CambioAMonedaLocalParaMostrar = value; }
        }

        decimal _PorVueltoMonedaLocal;
        public decimal PorVueltoMonedaLocal {
            get { return _PorVueltoMonedaLocal; }
            set { _PorVueltoMonedaLocal = value; }
        }

        decimal _PorVueltoDivisa;
        public decimal PorVueltoDivisa {
            get { return _PorVueltoDivisa; }
            set { _PorVueltoDivisa = value; }
        }

        string _NombreDeMonedaLocal;
        public string NombreDeMonedaLocal {
            get { return _NombreDeMonedaLocal; }
            set { _NombreDeMonedaLocal = value; }
        }

        string _NombreDeDivisa;
        public string NombreDeDivisa {
            get { return _NombreDeDivisa; }
            set { _NombreDeDivisa = value; }
        }

        decimal _EfectivoMonedaLocal;
        public decimal EfectivoMonedaLocal {
            get { return _EfectivoMonedaLocal; }
            set { _EfectivoMonedaLocal = value; }
        }

        decimal _EfectivoMonedaDivisa;

        public decimal EfectivoMonedaDivisa {
            get { return _EfectivoMonedaDivisa; }
            set { _EfectivoMonedaDivisa = value; }
        }

        public VueltoEfectivoViewModel(decimal initCambioAMonedaLocalParaMostrar, decimal initPorVueltoMonedaLocal, decimal initPorVueltoDivisa, string initNombreDeMonedaLocal, string initNombreDeDivisa) {
            CambioAMonedaLocalParaMostrar = initCambioAMonedaLocalParaMostrar;
            PorVueltoMonedaLocal = initPorVueltoMonedaLocal;
            PorVueltoDivisa = initPorVueltoDivisa;
            NombreDeMonedaLocal = initNombreDeMonedaLocal;
            NombreDeDivisa = initNombreDeDivisa;
        }



    }
}