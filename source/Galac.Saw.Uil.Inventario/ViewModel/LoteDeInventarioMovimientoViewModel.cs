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
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Uil.Inventario.ViewModel {
    public class LoteDeInventarioMovimientoViewModel : LibInputDetailViewModelMfc<LoteDeInventarioMovimiento> {
        #region Constantes
        public const string FechaPropertyName = "Fecha";
        public const string ModuloPropertyName = "Modulo";
        public const string CantidadPropertyName = "Cantidad";
        public const string NumeroDocumentoOrigenPropertyName = "NumeroDocumentoOrigen";
        public const string StatusDocumentoOrigenPropertyName = "StatusDocumentoOrigen";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Lote De Inventario Movimiento"; }
        }

        public int ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if (Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                }
            }
        }

        public int ConsecutivoLote {
            get {
                return Model.ConsecutivoLote;
            }
            set {
                if (Model.ConsecutivoLote != value) {
                    Model.ConsecutivoLote = value;
                }
            }
        }

        public int Consecutivo {
            get {
                return Model.Consecutivo;
            }
            set {
                if (Model.Consecutivo != value) {
                    Model.Consecutivo = value;
                }
            }
        }

        [LibCustomValidation("FechaValidating")]
        [LibGridColum("Fecha", eGridColumType.DatePicker, ColumnOrder = 4)]
        public DateTime Fecha {
            get {
                return Model.Fecha;
            }
            set {
                if (Model.Fecha != value) {
                    Model.Fecha = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaPropertyName);
                }
            }
        }

        [LibGridColum("Módulo", eGridColumType.Enum, PrintingMemberPath = "ModuloStr", ColumnOrder = 3)]
        public eOrigenLoteInv Modulo {
            get {
                return Model.ModuloAsEnum;
            }
            set {
                if (Model.ModuloAsEnum != value) {
                    Model.ModuloAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ModuloPropertyName);
                }
            }
        }

        [LibGridColum("Cantidad", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ColumnOrder = 1)]
        public decimal Cantidad {
            get {
                return Model.Cantidad;
            }
            set {
                if (Model.Cantidad != value) {
                    Model.Cantidad = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadPropertyName);
                }
            }
        }

        public int ConsecutivoDocumentoOrigen {
            get {
                return Model.ConsecutivoDocumentoOrigen;
            }
            set {
                if (Model.ConsecutivoDocumentoOrigen != value) {
                    Model.ConsecutivoDocumentoOrigen = value;
                }
            }
        }

        [LibGridColum("Número Doc.", MaxLength = 30, ColumnOrder = 0)]
        public string NumeroDocumentoOrigen {
            get {
                return Model.NumeroDocumentoOrigen;
            }
            set {
                if (Model.NumeroDocumentoOrigen != value) {
                    Model.NumeroDocumentoOrigen = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroDocumentoOrigenPropertyName);
                }
            }
        }

        [LibGridColum("Status", eGridColumType.Enum, PrintingMemberPath = "StatusDocumentoOrigenStr", ColumnOrder = 2)]
        public eStatusDocOrigenLoteInv StatusDocumentoOrigen {
            get {
                return Model.StatusDocumentoOrigenAsEnum;
            }
            set {
                if (Model.StatusDocumentoOrigenAsEnum != value) {
                    Model.StatusDocumentoOrigenAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(StatusDocumentoOrigenPropertyName);
                }
            }
        }

        public eOrigenLoteInv[] ArrayOrigenLoteInv {
            get {
                return LibEnumHelper<eOrigenLoteInv>.GetValuesInArray();
            }
        }

        public eStatusDocOrigenLoteInv[] ArrayStatusDocOrigenLoteInv {
            get {
                return LibEnumHelper<eStatusDocOrigenLoteInv>.GetValuesInArray();
            }
        }

        public LoteDeInventarioViewModel Master {
            get;
            set;
        }
        #endregion //Propiedades
        #region Constructores
        public LoteDeInventarioMovimientoViewModel()
            : base(new LoteDeInventarioMovimiento(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public LoteDeInventarioMovimientoViewModel(LoteDeInventarioViewModel initMaster, LoteDeInventarioMovimiento initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(LoteDeInventarioMovimiento valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ILibBusinessDetailComponent<IList<LoteDeInventarioMovimiento>, IList<LoteDeInventarioMovimiento>> GetBusinessComponent() {
            return new clsLoteDeInventarioMovimientoNav();
        }

        private ValidationResult FechaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(Fecha, false, Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha"));
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class LoteDeInventarioMovimientoViewModel

} //End of namespace Galac.Saw.Uil.Inventario

