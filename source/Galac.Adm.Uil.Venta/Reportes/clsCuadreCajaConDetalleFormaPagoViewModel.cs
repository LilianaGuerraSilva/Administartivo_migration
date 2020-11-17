using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using LibGalac.Aos.DefGen;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;
using System.Collections.ObjectModel;

namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsCuadreCajaConDetalleFormaPagoViewModel : LibInputRptViewModelBase<Caja> {
		#region Constantes
        private const string FechaInicialPropertyName = "FechaInicial";
        private const string FechaFinalPropertyName = "FechaFinal";
        private const string MonedaPropertyName = "Moneda";
        private const string TipoDeInformePropertyName = "TipoDeInforme";
        private const string TotalesPorFormaDePagoPropertyName = "TotalesPorFormaDePago";
        #endregion
        #region Variables
        private DateTime _FechaInicial;
        private DateTime _FechaFinal;
        private Galac.Saw.Lib.eMonedaParaImpresion _Moneda;
        private Galac.Saw.Lib.eTipoDeInforme _TipoDeInforme;
        private bool _TotalesPorFormaDePago;
        #endregion //Variables
        #region Propiedades

        public override string DisplayName {
            get { return "Cuadre de Caja con Detalle de Pago";}
        }
        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }

        public override bool IsSSRS {
            get {
                return false;
            }
        }

        [LibCustomValidation("FechaInicialValidating")]
        [LibGridColum("Fecha Inicial", eGridColumType.DatePicker)]
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
        [LibGridColum("Fecha Final", eGridColumType.DatePicker)]
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

        public Galac.Saw.Lib.eMonedaParaImpresion Moneda {
            get {
                return _Moneda;
            }
            set {
                if (_Moneda != value)
                {
                    _Moneda = value;
                    RaisePropertyChanged(MonedaPropertyName);
                }
            }
        }

        public Galac.Saw.Lib.eTipoDeInforme TipoDeInforme {
            get {
                return _TipoDeInforme;
            }
            set {
                if (_TipoDeInforme != value) {
                    _TipoDeInforme = value;
                    RaisePropertyChanged(TipoDeInformePropertyName);
                }
            }
        }

        public Saw.Lib.eTipoDeInforme[] ArrayTipoDeInforme {
            get {
                return LibEnumHelper<Saw.Lib.eTipoDeInforme>.GetValuesInArray();
            }
        }
        
        public bool  TotalesPorFormaDePago {
            get {
                return _TotalesPorFormaDePago;
            }
            set {
                if (_TotalesPorFormaDePago != value) {
                    _TotalesPorFormaDePago = value;
                    RaisePropertyChanged(TotalesPorFormaDePagoPropertyName);
                }
            }
        }

        private ObservableCollection<Galac.Saw.Lib.eMonedaParaImpresion> _MonedadeReporte = new ObservableCollection<Galac.Saw.Lib.eMonedaParaImpresion>();

        public ObservableCollection<Galac.Saw.Lib.eMonedaParaImpresion> MonedaDeReporte {
            get {
                return _MonedadeReporte;
            }
            set {
                _MonedadeReporte = value;
            }
        }

        private void LlenarEnumerativosMonedaDeReporte() {
            MonedaDeReporte.Clear();
            MonedaDeReporte.Add(Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal);
            if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                MonedaDeReporte.Add(Saw.Lib.eMonedaParaImpresion.EnBolivares);
            } else if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                MonedaDeReporte.Add(Saw.Lib.eMonedaParaImpresion.EnSoles);
            }
        }


        #endregion //Propiedades
        #region Constructores

        public clsCuadreCajaConDetalleFormaPagoViewModel() {
            FechaInicial = LibDate.Today();
            FechaFinal = LibDate.Today();
            LlenarEnumerativosMonedaDeReporte();
            Moneda = Galac.Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal;
            TotalesPorFormaDePago = true;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsCajaNav();
        }

        private ValidationResult FechaInicialValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaInicial, false, eAccionSR.InformesPantalla)) {
                vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha inicial"));
            } else if(LibDate.F1IsGreaterThanF2(FechaInicial,FechaFinal)) {
                vResult = new ValidationResult("La fecha inicial no puede ser mayor a la fecha final");
            }
            return vResult;
        }
        private ValidationResult FechaFinalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaFinal, false, eAccionSR.InformesPantalla)) {
                vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha final"));
            } else if (!LibDate.F1IsGreaterOrEqualThanF2(FechaFinal, FechaInicial)) {
                vResult = new ValidationResult("La fecha final debe ser mayor o igual a la fecha inicial:" + FechaInicial.ToShortDateString());
            }
            return vResult;
        }
        #endregion //Metodos Generados
    } //End of class clsCuadreCajaConDetalleFormaPagoViewModel

} //End of namespace Galac.Adm.Uil.Venta

