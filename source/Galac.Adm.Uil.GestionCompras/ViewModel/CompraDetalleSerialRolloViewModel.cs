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

namespace Galac.Adm.Uil.GestionCompras.ViewModel {
    public class CompraDetalleSerialRolloViewModel : LibInputDetailViewModelMfc<CompraDetalleSerialRollo> {
        #region Constantes
        public const string CodigoArticuloPropertyName = "CodigoArticulo";
        public const string SerialPropertyName = "Serial";
        public const string RolloPropertyName = "Rollo";
        public const string CantidadPropertyName = "Cantidad";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Compra Detalle Serial Rollo"; }
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

        public int ConsecutivoCompra {
            get {
                return Model.ConsecutivoCompra;
            }
            set {
                if (Model.ConsecutivoCompra != value) {
                    Model.ConsecutivoCompra = value;
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

        public string CodigoArticulo {
            get {
                return Model.CodigoArticulo;
            }
            set {
                if (Model.CodigoArticulo != value) {
                    Model.CodigoArticulo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                }
            }
        }
        [LibGridColum("Serial")]
        [LibCustomValidation("SerialValidating")]
        public string Serial {
            get {
                return Model.Serial;
            }
            set {
                if (Model.Serial != value) {
                    Model.Serial = value;
                    IsDirty = true;
                    RaisePropertyChanged(SerialPropertyName);
                }
            }
        }
        [LibGridColum("Rollo")]
        [LibCustomValidation("RolloValidating")]
        public string Rollo {
            get {
                return Model.Rollo;
            }
            set {
                if (Model.Rollo != value) {
                    Model.Rollo = value;
                    IsDirty = true;
                    RaisePropertyChanged(RolloPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Cantidad es requerido.")]
        [LibGridColum("Cantidad", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
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

        public CompraViewModel Master {
            get;
            set;
        }

        bool _IsVisibleRollo;
        public bool IsVisibleRollo {
            get { return _IsVisibleRollo; }
            set {
                _IsVisibleRollo = value;
                RaisePropertyChanged("IsVisibleRollo");
            }
        }

        public string SinonimoSerial {
            get {
                string vResult = "Serial";
                if (!LibString.IsNullOrEmpty(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "SinonimoSerial"))) {
                    vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "SinonimoSerial");
                }
                return vResult;
            }
        }

        public string SinonimoRollo {
            get {
                string vResult = "Rollo";
                if (!LibString.IsNullOrEmpty(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "SinonimoRollo"))) {
                    vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "SinonimoRollo");
                }
                return vResult;
            }
        }
        #endregion //Propiedades
        #region Constructores
        public CompraDetalleSerialRolloViewModel()
            : base(new CompraDetalleSerialRollo(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public CompraDetalleSerialRolloViewModel(CompraViewModel initMaster, CompraDetalleSerialRollo initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
        }
        public CompraDetalleSerialRolloViewModel(CompraViewModel initMaster, CompraDetalleSerialRollo initModel, eAccionSR initAction, Saw.Ccl.Inventario.eTipoArticuloInv initTipoArticuloInv)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            UsaRollo(initTipoArticuloInv);
        }

    
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(CompraDetalleSerialRollo valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ILibBusinessDetailComponent<IList<CompraDetalleSerialRollo>, IList<CompraDetalleSerialRollo>> GetBusinessComponent() {
            return new clsCompraDetalleSerialRolloNav();
        }

        internal void UsaRollo(Saw.Ccl.Inventario.eTipoArticuloInv valTipoArticuloInv) {
            if (valTipoArticuloInv == Saw.Ccl.Inventario.eTipoArticuloInv.UsaSerial) {
                IsVisibleRollo = false;
                Cantidad = 1;
            } else if (valTipoArticuloInv == Saw.Ccl.Inventario.eTipoArticuloInv.UsaTallaColorySerial || valTipoArticuloInv == Saw.Ccl.Inventario.eTipoArticuloInv.UsaSerialRollo) {
                IsVisibleRollo = true;
            }
        }

        private ValidationResult SerialValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(Serial)) { 
                    vResult = new ValidationResult("El campo "+ SinonimoSerial +" es requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult RolloValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (IsVisibleRollo && (LibString.IsNullOrEmpty(Rollo) || LibString.Len(LibString.Trim(Rollo)) == 0)) {
                    vResult = new ValidationResult("El campo " + SinonimoRollo + " es requerido.");
                }
            }
            return vResult;
        }



        #endregion //Metodos Generados


    } //End of class CompraDetalleSerialRolloViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

