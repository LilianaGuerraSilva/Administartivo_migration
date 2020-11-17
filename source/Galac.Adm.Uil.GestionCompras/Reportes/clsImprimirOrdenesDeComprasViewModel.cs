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
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Uil.GestionCompras.Reportes {
    public class clsImprimirOrdenesDeComprasViewModel :  LibInputRptViewModelBase<Compra> {
        #region Constantes
        public const string FechaInicialPropertyName = "FechaInicial";
        public const string FechaFinalPropertyName = "FechaFinal";
        public const string ImprimirRenglonesPropertyName = "ImprimirRenglones";
        public const string StatusDeOrdenDeCompraPropertyName = "StatusDeOrdenDeCompra";
        #endregion
        #region Variables
        DateTime _FechaInicial;
        DateTime _FechaFinal;
        bool _ImprimirRenglones;
        eStatusDeOrdenDeCompra _StatusDeOrdenDeCompra;
        #endregion
        #region Propiedades

        public override string DisplayName {
            get { return "Ordenes de Compra"; }
        }

        [LibCustomValidation("FechaInicialValidating")]
        public DateTime  FechaInicial {
            get {
                return _FechaInicial;
            }
            set {
                if (_FechaInicial != value) {
                    _FechaInicial = value;
                    RaisePropertyChanged(FechaInicialPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaFinalValidating")]
        public DateTime  FechaFinal {
            get {
                return _FechaFinal;
            }
            set {
                if (_FechaFinal != value) {
                    _FechaFinal = value;
                    RaisePropertyChanged(FechaFinalPropertyName);
                }
            }
        }

        public bool  ImprimirRenglones {
            get {
                return _ImprimirRenglones;
            }
            set {
                if (_ImprimirRenglones != value) {
                    _ImprimirRenglones = value;
                    RaisePropertyChanged(ImprimirRenglonesPropertyName);
                }
            }
        }

        public eStatusDeOrdenDeCompra  StatusDeOrdenDeCompra {
            get {
                return _StatusDeOrdenDeCompra;
            }
            set {
                if (_StatusDeOrdenDeCompra != value) {
                    _StatusDeOrdenDeCompra = value;
                    RaisePropertyChanged(StatusDeOrdenDeCompraPropertyName);
                }
            }
        }

        public bool CompletamenteProcesada {
            get {
                return _StatusDeOrdenDeCompra == eStatusDeOrdenDeCompra.CompletamenteProcesada;
            }
            set {
                if (value) {
                    _StatusDeOrdenDeCompra = eStatusDeOrdenDeCompra.CompletamenteProcesada;
                }
            }
        }

        public bool ParcialmenteProcesada {
            get {
                return _StatusDeOrdenDeCompra == eStatusDeOrdenDeCompra.ParcialmenteProcesada;
            }
            set {
                if (value) {
                    _StatusDeOrdenDeCompra = eStatusDeOrdenDeCompra.ParcialmenteProcesada;
                }
            }
        }

        public bool SinProcesar {
            get {
                return _StatusDeOrdenDeCompra == eStatusDeOrdenDeCompra.SinProcesar;
            }
            set {
                if (value) {
                    _StatusDeOrdenDeCompra = eStatusDeOrdenDeCompra.SinProcesar;
                }
            }
        }

        //public eStatusDeOrdenDeCompra[] ArrayStatusDeOrdenDeCompra {
        //    get {
        //        return LibEnumHelper<eStatusDeOrdenDeCompra>.GetValuesInArray();
        //    }
        //}
        #endregion //Propiedades
        #region Constructores

        #endregion //Constructores
        public clsImprimirOrdenesDeComprasViewModel() {
            FechaInicial = LibDate.Today();
            FechaFinal = LibDate.Today();
            StatusDeOrdenDeCompra = eStatusDeOrdenDeCompra.CompletamenteProcesada;
            ImprimirRenglones = false;
        }
        #region Metodos Generados

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsCompraNav();
        }

        private ValidationResult FechaInicialValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaInicial, false, eAccionSR.Imprimir)) {
                vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha Inicial"));
            }
            return vResult;
        }

        private ValidationResult FechaFinalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (!LibDate.F1IsGreaterOrEqualThanF2(FechaFinal, FechaInicial)) {
                vResult = new ValidationResult("La fecha de traslado debe ser mayor o igual a la fecha: " + FechaInicial.ToShortDateString());
            }
            return vResult;
        }
        #endregion //Metodos Generados

        public override bool IsSSRS {
            get {
                return false;
            }
        }

    } //End of class ImprimirOrdenesDeComprasViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

