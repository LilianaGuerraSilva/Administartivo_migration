using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Uil.Venta.ViewModel;
using System.Collections.ObjectModel;
namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsCuadreCajaPorTipoCobroViewModel : LibInputRptViewModelBase<Caja> {
		#region Constantes
        public const string FechaInicialPropertyName = "FechaInicial";
        public const string FechaFinalPropertyName = "FechaFinal";
        public const string MonedaPropertyName = "Moneda";
        public const string TipoDeInformePropertyName = "TipoDeInforme";
        #endregion

        #region Variables
        #region Codigo Ejemplo
        private DateTime _FechaInicial;
        private DateTime _FechaFinal;
        private Galac.Saw.Lib.eMonedaParaImpresion _Moneda;
        private Galac.Saw.Lib.eTipoDeInforme _TipoDeInforme;
        #endregion //Codigo Ejemplo
        #endregion //Variables
        #region Propiedades

        public override string DisplayName {
            get { return "Cuadre de Caja por Tipo de Cobro";}
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
        public DateTime FechaInicial {
            get {
                return _FechaInicial;
            }
            set {
                if (_FechaInicial != value)
                {
                    _FechaInicial = value;
                    RaisePropertyChanged(FechaInicialPropertyName);
                }
            }
        }
        [LibCustomValidation("FechaFinalValidating")]
        [LibGridColum("Fecha Final", eGridColumType.DatePicker)]
        public DateTime FechaFinal {
            get {
                return _FechaFinal;
            }
            set {
                if (_FechaFinal != value)
                {
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
        public ObservableCollection<Galac.Saw.Lib.eMonedaParaImpresion> _MonedaDeReporte = new ObservableCollection<Galac.Saw.Lib.eMonedaParaImpresion>();
        public ObservableCollection<Galac.Saw.Lib.eMonedaParaImpresion> MonedaDeReporte { get { return _MonedaDeReporte; } set { _MonedaDeReporte = value; } }
        private void LlenarEnumerativosMonedaDeReporte () {
            MonedaDeReporte.Clear();
            MonedaDeReporte.Add(Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal);
            MonedaDeReporte.Add(Saw.Lib.eMonedaParaImpresion.EnBolivares);
        }
        public Saw.Lib.eTipoDeInforme TipoDeInforme {
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

		//public Saw.Lib.eTipoDeInforme[] ArrayMonedaDelInforme {
  //          get {
  //              return LibEnumHelper<Saw.Lib.eTipoDeInforme>.GetValuesInArray();
  //          }
  //      }

        public Saw.Lib.eTipoDeInforme[] ArrayTipoDeInforme {
            get {
                return LibEnumHelper<Saw.Lib.eTipoDeInforme>.GetValuesInArray();
            }
        }
        
        #endregion //Propiedades
        #region Constructores

        public clsCuadreCajaPorTipoCobroViewModel() {
            FechaInicial = LibDate.Today();
            FechaFinal = LibDate.Today();
            LlenarEnumerativosMonedaDeReporte();
            Moneda = Galac.Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal;
            TipoDeInforme = Galac.Saw.Lib.eTipoDeInforme.Detallado;
        }
        #endregion //Constructores
        #region Metodos Generados
        protected override ILibBusinessSearch GetBusinessComponent()
        {
            return new clsCajaNav();
        }

        private ValidationResult FechaInicialValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaFinal, false, eAccionSR.Imprimir))
            {
                vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha inicial"));
            }
            return vResult;
        }
        private ValidationResult FechaFinalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (!LibDate.F1IsGreaterOrEqualThanF2(FechaFinal, FechaInicial))
            {
                vResult = new ValidationResult("La fecha final debe ser mayor o igual a la fecha inicial:" + FechaInicial.ToShortDateString());
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCuadreCajaPorTipoCobroViewModel

} //End of namespace Galac.Adm.Uil.Venta

