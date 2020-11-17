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
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Brl.TablasGen;
using Galac.Comun.Uil.TablasGen.ViewModel;

namespace Galac.Adm.Uil.GestionCompras.ViewModel {
    public class CompraDetalleGastoViewModel : LibInputDetailViewModelMfc<CompraDetalleGasto> {
        
        #region Constantes

        private const string CxpNumeroPropertyName = "CxpNumero";
        private const string TipoDeCostoPropertyName = "TipoDeCosto";
        private const string MontoPropertyName = "Monto";
        private const string CodigoProveedorPropertyName = "CodigoProveedor";
        private const string NombreProveedorPropertyName = "NombreProveedor";
        private const string CodigoMonedaPropertyName = "CodigoMoneda";

        #endregion

        #region Variables

        private FkCxPViewModel _ConexionNumeroCxp = null;

        #endregion

        #region Propiedades

        public override string ModuleName {
            get { return "Compra Detalle Gasto"; }
        }

        public int  ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if (Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                }
            }
        }

        public int  ConsecutivoCompra {
            get {
                return Model.ConsecutivoCompra;
            }
            set {
                if (Model.ConsecutivoCompra != value) {
                    Model.ConsecutivoCompra = value;
                }
            }
        }

        public int  ConsecutivoCxP {
            get {
                return Model.ConsecutivoCxP;
            }
            set {
                if (Model.ConsecutivoCxP != value) {
                    Model.ConsecutivoCxP = value;
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Cxp Numero es requerido.")]
        [LibGridColum("Cxp Numero", eGridColumType.Connection, ConnectionDisplayMemberPath = "Numero", ConnectionModelPropertyName = "CxpNumero", ConnectionSearchCommandName = "ChooseCxpNumeroCommand", MaxWidth = 120)]
        public string CxpNumero {
            get {
                return Model.CxpNumero;
            }
            set {
                if (Model.CxpNumero != value) {
                    Model.CxpNumero = value;
                    IsDirty = true;
                    RaisePropertyChanged(CxpNumeroPropertyName);
                    if (LibString.IsNullOrEmpty(CxpNumero, true)) {
                        ConexionCxpNumero = null;
                    }
                }
            }
        }

        public int ConsecutivoRenglon {
            get {
                return Model.ConsecutivoRenglon;
            }
            set {
                if (Model.ConsecutivoRenglon != value) {
                    Model.ConsecutivoRenglon = value;
                }
            }
        }

       

        [LibRequired(ErrorMessage = "El campo Codigo Proveedor es requerido.")]
        [LibGridColum("Codigo Proveedor", MaxLength = 10)]
        public string CodigoProveedor {
            get {
                return Model.CodigoProveedor;
            }
            set {
                if (Model.CodigoProveedor != value) {
                    Model.CodigoProveedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoProveedorPropertyName);
                }
            }
        }

        [LibGridColum("Nombre Proveedor", MaxLength = 60)]
        public string NombreProveedor {
            get {
                return Model.NombreProveedor;
            }
            set {
                if (Model.NombreProveedor != value) {
                    Model.NombreProveedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreProveedorPropertyName);
                }
            }
        }

        public string CodigoMoneda {
            get {
                return Model.CodigoMoneda;
            }
            set {
                if (Model.CodigoMoneda != value) {
                    Model.CodigoMoneda = value;
                    RaisePropertyChanged(CodigoMonedaPropertyName);
                }
            }
        }

        [LibGridColum("Monto", eGridColumType.Numeric)]
        public decimal Monto {
            get {
                return Model.Monto;
            }
            set {
                if (Model.Monto != value) {
                    Model.Monto = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoPropertyName);
                }
            }
        }

        [LibGridColum("Tipo De Costo", eGridColumType.Enum, PrintingMemberPath = "TipoDeCostoStr")]
        public eTipoDeCosto TipoDeCosto {
            get {
                return Model.TipoDeCostoAsEnum;
            }
            set {
                if (Model.TipoDeCostoAsEnum != value) {
                    Model.TipoDeCostoAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDeCostoPropertyName);
                }
            }
        }
        public eTipoDeCosto[] ArrayTipoDeCosto {
            get {
                return LibEnumHelper<eTipoDeCosto>.GetValuesInArray();
            }
        }

        public CompraViewModel Master {
            get;
            set;
        }

        public FkCxPViewModel ConexionCxpNumero {
            get {
                return _ConexionNumeroCxp;
            }
            set {
                if (_ConexionNumeroCxp != value) {
                    _ConexionNumeroCxp = value;
                    RaisePropertyChanged(CxpNumeroPropertyName);
                }
                if (_ConexionNumeroCxp == null) {                
                    ConsecutivoCxP = 0;
                    CxpNumero = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseNumeroCxpCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public CompraDetalleGastoViewModel()
            : base(new CompraDetalleGasto(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public CompraDetalleGastoViewModel(CompraViewModel initMaster, CompraDetalleGasto initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(CompraDetalleGasto valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ILibBusinessDetailComponent<IList<CompraDetalleGasto>, IList<CompraDetalleGasto>> GetBusinessComponent() {
            return new clsCompraDetalleGastoNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNumeroCxpCommand = new RelayCommand<string>(ExecuteChooseNumeroCxpCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
           // ConexionCxpNumero = Master.FirstConnectionRecordOrDefault<FkCxPViewModel>("Cx P", LibSearchCriteria.CreateCriteria("Numero", CxpNumero));
        }

        private void ExecuteChooseNumeroCxpCommand(string valNumero) {
            IMonedaLocalActual vMonedaLocalActual = new clsMonedaLocalActual();
            try {
                if (valNumero == null) {
                    valNumero = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("dbo.cxP.Numero", valNumero);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("dbo.cxP.ConsecutivoCompania", Mfc.GetInt("Compania"));
                vDefaultCriteria.Add(LibSearchCriteria.CreateCriteria(" dbo.cxP.Status ", LibConvert.EnumToDbValue((int)eStatusDocumentoCxP.PorCancelar)), eLogicOperatorType.And);
                ConexionCxpNumero = Master.ChooseRecord<FkCxPViewModel>("CxP", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCxpNumero != null) {
                    if (SePuedeUsarLaCxP(Master.ConsecutivoCompania, ConexionCxpNumero.ConsecutivoCxP, Master.Consecutivo)) {                        
                    } else {
                        LibMessages.MessageBox.Alert(this, "La CxP " + ConexionCxpNumero.Numero + " no puede ser usada ya que fue asignada a otro registro", Master.ModuleName);
                        ConexionCxpNumero = null;
                    }
                }
                if (ConexionCxpNumero != null) {
                    ConsecutivoCxP = ConexionCxpNumero.ConsecutivoCxP;
                    CxpNumero = ConexionCxpNumero.Numero;
                    CodigoProveedor = ConexionCxpNumero.CodigoProveedor;
                    NombreProveedor = ConexionCxpNumero.NombreProveedor;
                    Monto = ConexionCxpNumero.Monto;
                    CodigoMoneda = ConexionCxpNumero.CodigoMoneda;
                    if(vMonedaLocalActual.EsMonedaLocalDelPais(CodigoMoneda) &&  !vMonedaLocalActual.EsMonedaLocalDelPais(Master.CodigoMoneda)) {
                        Monto /= Master.CambioAMonedaLocal;
                    } else if(!vMonedaLocalActual.EsMonedaLocalDelPais(CodigoMoneda) && vMonedaLocalActual.EsMonedaLocalDelPais(Master.CodigoMoneda)) {
                        decimal vCambio;
                        bool vExisteCambioDelDía = ((ICambioPdn)new clsCambioNav()).ExisteTasaDeCambioParaElDia(CodigoMoneda, Master.Fecha, out  vCambio);
                        if(!vExisteCambioDelDía) {
                            vCambio = 1;
                            CambioViewModel vViewModel = new CambioViewModel(CodigoMoneda);
                            vViewModel.InitializeViewModel(eAccionSR.Insertar);
                            vViewModel.FechaDeVigencia = Master.Fecha;
                            vViewModel.IsEnabledFecha = vViewModel.IsEnabledMoneda = false;
                            bool vResult = LibMessages.EditViewModel.ShowEditor(vViewModel, true);
                            if(vResult) {
                                ((ICambioPdn)new clsCambioNav()).ExisteTasaDeCambioParaElDia(CodigoMoneda, Master.Fecha, out vCambio);
                            }
                        }
                        Monto *= vCambio;
                    }
                } else {
                    ConsecutivoCxP = 0;
                    CxpNumero = string.Empty;
                    CodigoProveedor = string.Empty;
                    NombreProveedor = string.Empty;
                    Monto = 0;
                    CodigoMoneda = string.Empty;
                }

            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private bool SePuedeUsarLaCxP(int valConsecutivoCompania, int valConsecutivoCxP, int valConsecutivoCompra) {
            bool vResult = true;
            ICompraDetalleGastoPdn vPdn = GetBusinessComponent() as ICompraDetalleGastoPdn;
            vResult = !vPdn.CxpEstaSiendoUsadaEnOtroCompra(valConsecutivoCompania, valConsecutivoCxP, valConsecutivoCompra);            
            return vResult;    
        }

        #endregion //Metodos Generados


    } //End of class CompraDetalleGastoViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

