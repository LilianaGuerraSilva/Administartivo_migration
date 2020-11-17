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
using Galac.Adm.Ccl.CAnticipo;
//using Galac.Adm.Brl.CAnticipo;


namespace Galac.Adm.Uil.Venta.ViewModel {
    public class CobroDeFacturaRapidaAnticipoDetalleViewModel : LibInputDetailViewModelMfc<CobroDeFacturaRapidaAnticipoDetalle> {
        #region Constantes
        public const string CodigoFormaDelCobroPropertyName = "CodigoFormaDelCobro";
        public const string CodigoAnticipoPropertyName = "CodigoAnticipo";
        public const string NumeroAnticipoPropertyName = "NumeroAnticipo";
        public const string MontoDisponiblePropertyName = "MontoDisponible";
        public const string MontoPropertyName = "Monto";
        #endregion
        #region Variables
        private FkFormaDelCobroViewModel _ConexionCodigoFormaDelCobro = null;
        private FkAnticipoViewModel _ConexionCodigoAnticipo = null;

        int _ConsecutivoCompania;
        string _NumeroAnticipo;
        string _CodigoFormaDelCobro;
        string _NumeroDelDocumento;
        int _CodigoAnticipo;
        string _NombreBanco; // ver si es util 
        string _MontoDisponible;
        decimal _Monto;
  

        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Cobro De Factura Rapida Anticipo Detalle"; }
        }

        public int  ConsecutivoCompania {
            get {
                return _ConsecutivoCompania;
            }
            set {
                if (_ConsecutivoCompania != value) {
                    _ConsecutivoCompania = value;
                }
            }
        }

        public string  CodigoFormaDelCobro {
            get {
                return _CodigoFormaDelCobro;
            }
            set {
                if (_CodigoFormaDelCobro != value) {
                   _CodigoFormaDelCobro = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoFormaDelCobroPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoFormaDelCobro, true)) {
                        ConexionCodigoFormaDelCobro = null;
                    }
                }
            }
        }

       // [LibRequired(ErrorMessage = "El campo Código del Anticipo es requerido.")]
        public int  CodigoAnticipo {
            get {
                return _CodigoAnticipo;
            }
            set {
                if (_CodigoAnticipo != value) {
                    _CodigoAnticipo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoAnticipoPropertyName);
                    if (LibString.IsNullOrEmpty(LibConvert.ToStr(CodigoAnticipo), true)) {
                        ConexionCodigoAnticipo = null;
                    }
                }
            }
        }
        [LibRequired(ErrorMessage = "El campo Numero de Anticipo es requerido.")]
        [LibCustomValidation("CodigoAnticipoValidating")]
        [LibGridColum("Anticipo", eGridColumType.Connection, 
         DisplayMemberPath = "NumeroAnticipo", ConnectionDisplayMemberPath = "NumeroAnticipo", ConnectionModelPropertyName = "NumeroAnticipo", 
         ConnectionSearchCommandName = "ChooseCodigoAnticipoCommand", Width = 180, MaxLength = 250)]        
        public string  NumeroAnticipo {
            get {
                return _NumeroAnticipo;
            }
            set {
                if (_NumeroAnticipo != value) {
                    _NumeroAnticipo = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroAnticipoPropertyName);
                }
            }
        }

        //[LibRequired(ErrorMessage = "El campo Monto Disponible es requerido.")]
        [LibGridColum("Monto Disponible", MaxLength=60,Width = 180 , IsReadOnly = true)]
        public string  MontoDisponible {
            get {
                return _MontoDisponible;
            }
            set {
                if (_MontoDisponible != value) {
                    _MontoDisponible = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoDisponiblePropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Monto es requerido.")]
        [LibCustomValidation("MontoAnticipoValidating")]
        [LibGridColum("Monto", eGridColumType.UDecimal, Width = 180, MaxLength = 80, BindingStringFormat = "N2", Alignment = eTextAlignment.Right)]
        public decimal Monto {
            get {
                return _Monto;
            }
            set {
                if (_Monto != value) {
                    _Monto = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoPropertyName);

                }
            }
        }

        public CobroDeFacturaRapidaAnticipoViewModel Master {
            get;
            set;
        }

        public FkFormaDelCobroViewModel ConexionCodigoFormaDelCobro {
            get {
                return _ConexionCodigoFormaDelCobro;
            }
            set {
                if (_ConexionCodigoFormaDelCobro != value) {
                    _ConexionCodigoFormaDelCobro = value;
                    RaisePropertyChanged(CodigoFormaDelCobroPropertyName);
                }
                if (_ConexionCodigoFormaDelCobro == null) {
                    CodigoFormaDelCobro = string.Empty;
                }
            }
        }

        public FkAnticipoViewModel ConexionCodigoAnticipo {
            get {
                return _ConexionCodigoAnticipo;
            }
            set {
                if (_ConexionCodigoAnticipo != value) {
                    _ConexionCodigoAnticipo = value;
                    RaisePropertyChanged(CodigoAnticipoPropertyName);
                    if (_ConexionCodigoAnticipo != null) {
                        CodigoAnticipo = LibConvert.ToInt(_ConexionCodigoAnticipo.ConsecutivoAnticipo);
                        NumeroAnticipo  = LibConvert.ToStr (_ConexionCodigoAnticipo.Numero);
                        MontoDisponible = LibConvert.ToStr (LibMath.RoundToNDecimals((_ConexionCodigoAnticipo.MontoTotal - _ConexionCodigoAnticipo.MontoUsado), 2));
                        //NombreBanco = ConexionCodigoBanco.Nombre;
                    }
                }
                if (_ConexionCodigoAnticipo == null) {
                    CodigoAnticipo = 0;
                    //NombreBanco = string.Empty;
                }
            }
        }

        private ValidationResult MontoAnticipoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            decimal vDisponible = LibConvert.ToDec(MontoDisponible);
            decimal Resultado = Monto - vDisponible;
            decimal Diferencia = LibConvert.ToDec(Master.MontoDiferencia);
            // validar que no sea mayor al monto disponible del anticipo

            if (Monto > 0) {
                if (Resultado > 0) {
                    vResult = new ValidationResult("El monto del Anticipo no puede ser mayor al monto disponible");
                } else if (Diferencia < 0) {
                    vResult = new ValidationResult("El pago del Anticipo no puede ser menor que 0 (cero).");
                } else {
                    vResult = ValidationResult.Success;
                }
            } else {
                vResult = new ValidationResult("El monto del Anticipo no puede ser 0 (cero).");
            }

            //if (Monto > 0) {
            //    if (Resultado <=  0) {
            //        return ValidationResult.Success;
            //    } else {
            //        vResult = new ValidationResult("El monto del Anticipo no puede ser mayor al monto disponible");
            //    }
            //} else {
            //    vResult = new ValidationResult("El monto del Anticipo no puede ser 0 (cero).");
            //}
            return vResult;
        }

        private ValidationResult CodigoAnticipoValidating() {
            int vPrimeraPosicion = -1;
            ValidationResult vResult = ValidationResult.Success;
            if (EstaRepetidoCodigoAnticipoDetalle(ref vPrimeraPosicion)) {
                 if (!this.Equals(Master.DetailCobroDeFacturaRapidaAnticipoDetalle.Items[vPrimeraPosicion])) {
                        vResult = new ValidationResult("Ya se esta usando este anticipo en el Cobro");
                    }
                }
            
            return vResult;
        }
        private bool EstaRepetidoCodigoAnticipoDetalle(ref int refPrimeraPosicion) {
            int vContador;
            int vLongitudDetalle = Master.DetailCobroDeFacturaRapidaAnticipoDetalle.Items.Count;
            int vDiferencia;
            vContador = 0;
            for (int i = 0; i < vLongitudDetalle; i++) {
                vDiferencia = CodigoAnticipo - Master.DetailCobroDeFacturaRapidaAnticipoDetalle.Items[i].CodigoAnticipo;
                if (NumeroAnticipo  == Master.DetailCobroDeFacturaRapidaAnticipoDetalle.Items[i].NumeroAnticipo) {
                //if (vDiferencia != 0) {
                    if (refPrimeraPosicion < 0) {
                        refPrimeraPosicion = i;
                    }
                    vContador += 1;
                    if (vContador > 1) {
                        return true;
                    }
                }
            }
            return false;
        }

        public RelayCommand<string> ChooseCodigoFormaDelCobroCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoAnticipoCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public CobroDeFacturaRapidaAnticipoDetalleViewModel()
            : base(new CobroDeFacturaRapidaAnticipoDetalle(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public CobroDeFacturaRapidaAnticipoDetalleViewModel(CobroDeFacturaRapidaAnticipoViewModel initMaster, CobroDeFacturaRapidaAnticipoDetalle initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(CobroDeFacturaRapidaAnticipoDetalle valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ILibBusinessDetailComponent<IList<CobroDeFacturaRapidaAnticipoDetalle>, IList<CobroDeFacturaRapidaAnticipoDetalle>> GetBusinessComponent() {
            return new clsCobroDeFacturaRapidaAnticipoDetalleNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoFormaDelCobroCommand = new RelayCommand<string>(ExecuteChooseCodigoFormaDelCobroCommand);
            ChooseCodigoAnticipoCommand = new RelayCommand<string>(ExecuteChooseCodigoAnticipoCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
           // ConexionCodigoFormaDelCobro = Master.FirstConnectionRecordOrDefault<FkFormaDelCobroViewModel>("Forma Del Cobro", LibSearchCriteria.CreateCriteria("Codigo", CodigoFormaDelCobro));
            ConexionCodigoAnticipo = Master.FirstConnectionRecordOrDefault<FkAnticipoViewModel>("Anticipo", LibSearchCriteria.CreateCriteria("dbo.Gv_Anticipo_B1.Numero", NumeroAnticipo));
        }

        private void ExecuteChooseCodigoFormaDelCobroCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = null;
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
                vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
        */
        #endregion //Codigo Ejemplo
                ConexionCodigoFormaDelCobro = Master.ChooseRecord<FkFormaDelCobroViewModel>("Forma Del Cobro", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoFormaDelCobro != null) {
                    CodigoFormaDelCobro = ConexionCodigoFormaDelCobro.Codigo;
                } else {
                    CodigoFormaDelCobro = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCodigoAnticipoCommand(string valcodigo) {
            string vCodigoCliente;
            try {
                if (valcodigo == null) {
                    valcodigo = string.Empty;
                }
                vCodigoCliente = Master.CodigoCliente;
                LibSearchCriteria vDefaultCriteria = new LibSearchCriteria();
                vDefaultCriteria.Add("dbo.Gv_Anticipo_B1.Status", eStatusAnticipo.Vigente);
                vDefaultCriteria.Add("dbo.Gv_Anticipo_B1.Status", eBooleanOperatorType.IdentityEquality, eStatusAnticipo.ParcialmenteUsado, eLogicOperatorType.Or);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("dbo.Gv_Anticipo_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                vFixedCriteria.Add("dbo.Gv_Anticipo_B1.Tipo",  eTipoDeAnticipo.Cobrado);
                vFixedCriteria.Add("dbo.Gv_Anticipo_B1.CodigoCliente", vCodigoCliente);
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteriaFromText("dbo.Gv_Anticipo_B1.Numero", valcodigo), eLogicOperatorType.And);
                ConexionCodigoAnticipo = Master.ChooseRecord<FkAnticipoViewModel>("Anticipo", vDefaultCriteria, vFixedCriteria, "Numero");
                if (ConexionCodigoAnticipo != null) {
                    CodigoAnticipo = ConexionCodigoAnticipo.ConsecutivoAnticipo;
                    NumeroAnticipo = ConexionCodigoAnticipo.Numero;
                    MontoDisponible = LibConvert.ToStr(LibMath.RoundToNDecimals((ConexionCodigoAnticipo.MontoTotal - ConexionCodigoAnticipo.MontoUsado), 2));
                } else {
                    CodigoAnticipo = 0;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        internal void RecargarConexiones() {
            ReloadRelatedConnections();
        }
        #endregion //Metodos Generados


    } //End of class CobroDeFacturaRapidaAnticipoDetalleViewModel

} //End of namespace Galac.Adm.Uil.Venta

