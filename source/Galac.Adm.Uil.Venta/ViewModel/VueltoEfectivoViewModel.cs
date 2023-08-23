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

        public string CambioAMonedaLocalParaMostrar {
            get { return LibConvert.NumToString(CambioAMonedaLocal, 4); }
        }

        decimal _CambioAMonedaLocal;
        public decimal CambioAMonedaLocal{
            get { return _CambioAMonedaLocal; }
            set {
                if (_CambioAMonedaLocal != value) {
                    _CambioAMonedaLocal = value;
                    RaisePropertyChanged(() => CambioAMonedaLocal);
                } 
            }
        }

        public string PorVueltoMonedaLocalParaMostrar {
            get { return LibConvert.NumToString(PorVueltoMonedaLocal, 2); }
        }

        decimal _PorVueltoMonedaLocal;
        public decimal PorVueltoMonedaLocal {
            get { return _PorVueltoMonedaLocal; }
            set {
                if (_PorVueltoMonedaLocal != value) {
                    _PorVueltoMonedaLocal = value;
                    RaisePropertyChanged(() => PorVueltoMonedaLocal);
                }
            }
        }

        public string PorVueltoDivisaParaMostrar {
            get { return LibConvert.NumToString(PorVueltoDivisa, 2); }
        }

        decimal _PorVueltoDivisa;
        public decimal PorVueltoDivisa {
            get { return _PorVueltoDivisa; }
            set {
                if (_PorVueltoDivisa != value) {
                    _PorVueltoDivisa = value;
                    RaisePropertyChanged(() => _PorVueltoDivisa);
                }
            }
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

        public VueltoEfectivoViewModel(decimal initCambioAMonedaLocal, decimal initPorVueltoMonedaLocal, decimal initPorVueltoDivisa, string initNombreDeMonedaLocal, string initNombreDeDivisa) {
            CambioAMonedaLocal = initCambioAMonedaLocal;
            PorVueltoMonedaLocal = initPorVueltoMonedaLocal;
            PorVueltoDivisa = initPorVueltoDivisa;
            NombreDeMonedaLocal = initNombreDeMonedaLocal;
            NombreDeDivisa = initNombreDeDivisa;
        }



    }
}