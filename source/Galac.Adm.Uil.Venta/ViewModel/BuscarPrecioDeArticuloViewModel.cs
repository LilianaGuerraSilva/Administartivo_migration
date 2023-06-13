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
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Brl.Inventario;
using LibGalac.Aos.Uil;
using System.Xml;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class BuscarPrecioDeArticuloViewModel : LibGenericViewModel {
        #region Constantes
        public const string ArticuloPropertyName = "Articulo";
        public const string DescripcionPropertyName = "Descripcion";
        public const string PrecioSinIVAPropertyName = "PrecioSinIVA";
        public const string PrecioConIVAPropertyName = "PrecioConIVA";
        #endregion
        #region Variables
        private FkArticuloInventarioViewModel _ConexionArticulo = null;
        private FkArticuloInventarioViewModel _ConexionDescripcion = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Consultar Precio De Articulo"; }
        }

        private int _ConsecutivoCompania;
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

        private string _Articulo;
        [LibGridColum("Código", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "Articulo", ConnectionSearchCommandName = "ChooseArticuloCommand", MaxWidth=120)]
        public string  Articulo {
            get {
                return _Articulo;
            }
            set {
                if (_Articulo != value) {
                    _Articulo = value;
                    RaisePropertyChanged(ArticuloPropertyName);
                    if (LibString.IsNullOrEmpty(Articulo, true)) {
                        ConexionArticulo = null;
                    }
                }
            }
        }

        private string _Descripcion;
        [LibGridColum("Descripción", eGridColumType.Connection, ConnectionDisplayMemberPath = "Descripcion", ConnectionModelPropertyName = "Descripcion", ConnectionSearchCommandName = "ChooseDescripcionCommand", MaxWidth=120)]
        public string  Descripcion {
            get {
                return _Descripcion;
            }
            set {
                if (_Descripcion != value) {
                    _Descripcion = value;
                    RaisePropertyChanged(DescripcionPropertyName);
                    if (LibString.IsNullOrEmpty(Descripcion, true)) {
                        ConexionDescripcion = null;
                    }
                }
            }
        }

        private decimal _PrecioSinIVA;
        [LibGridColum("Precio sin IVA", eGridColumType.Numeric)]
        public decimal  PrecioSinIVA {
            get {
                return _PrecioSinIVA;
            }
            set {
                if (_PrecioSinIVA != value) {
                    _PrecioSinIVA = value;
                    RaisePropertyChanged(PrecioSinIVAPropertyName);
                }
            }
        }

        private decimal _PrecioConIVA;
        [LibGridColum("Precio Con IVA", eGridColumType.Numeric)]
        public decimal  PrecioConIVA {
            get {
                return _PrecioConIVA;
            }
            set {
                if (_PrecioConIVA != value) {
                    _PrecioConIVA = value;
                    RaisePropertyChanged(PrecioConIVAPropertyName);
                }
            }
        }

        public FkArticuloInventarioViewModel ConexionArticulo {
            get {
                return _ConexionArticulo;
            }
            set {
                if (_ConexionArticulo != value) {
                    _ConexionArticulo = value;
                    RaisePropertyChanged(ArticuloPropertyName);
                }
                if (_ConexionArticulo == null) {
                    Articulo = string.Empty;
                }
            }
        }

        public FkArticuloInventarioViewModel ConexionDescripcion {
            get {
                return _ConexionDescripcion;
            }
            set {
                if (_ConexionDescripcion != value) {
                    _ConexionDescripcion = value;
                    RaisePropertyChanged(DescripcionPropertyName);
                }
                if (_ConexionDescripcion == null) {
                    Descripcion = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseArticuloCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseDescripcionCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public BuscarPrecioDeArticuloViewModel(){
//            : this(new ArticuloInventario(), eAccionSR.Insertar) {
        }
        //public BuscarPrecioDeArticuloViewModel(ArticuloInventario initModel, eAccionSR initAction)
        //    : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        //    DefaultFocusedPropertyName = ArticuloPropertyName;
        //    ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
        //}
        #endregion //Constructores
        #region Metodos Generados

        //protected override void InitializeLookAndFeel(ArticuloInventario valModel) {
        //    base.InitializeLookAndFeel(valModel);
        //}

        //protected override ArticuloInventario FindCurrentRecord(ArticuloInventario valModel) {
        //    if (valModel == null) {
        //        return null;
        //    }
        //    LibGpParams vParams = new LibGpParams();
        //    vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
        //    vParams.AddInString("Articulo", Articulo, 15);
        //    return BusinessComponent.GetData(eProcessMessageType.SpName, "Gp_ArticuloInventarioGET", vParams.Get()).FirstOrDefault();
        //}

        //protected override ILibBusinessComponentWithSearch<IList<ArticuloInventario>, IList<ArticuloInventario>> GetBusinessComponent() {
        //    return new clsArticuloInventarioNav();
        //}

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseArticuloCommand = new RelayCommand<string>(ExecuteChooseArticuloCommand);
            ChooseDescripcionCommand = new RelayCommand<string>(ExecuteChooseDescripcionCommand);
        }

        //protected override void ReloadRelatedConnections() {
            //base.ReloadRelatedConnections();
            //ConexionArticulo = FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Articulo Inventario", LibSearchCriteria.CreateCriteria("Codigo", Articulo));
            //ConexionDescripcion = FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Articulo Inventario", LibSearchCriteria.CreateCriteria("Descripcion", Descripcion));
        //}

        private void ExecuteChooseArticuloCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }

                List<LibSearchDefaultValues> vDefaultCriteria = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedCriteria = null;
                vDefaultCriteria.Add(new LibSearchDefaultValues("Codigo", valCodigo, true, typeof(string)));
                vFixedCriteria.Add(new LibSearchDefaultValues("ConsecutivoCompania", LibConvert.ToStr(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania")), true, typeof(int)));
                XmlDocument XmlData = new XmlDocument();
                if (LibFKRetrievalHelper.ChooseRecord<FkArticuloInventarioViewModel>("Articulo Inventario", ref XmlData, vDefaultCriteria, vFixedCriteria, new clsArticuloInventarioNav(), string.Empty)) {
                    ConexionArticulo = LibParserHelper.ParseToList<FkArticuloInventarioViewModel>(XmlData).FirstOrDefault();
                }

            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseDescripcionCommand(string valDescripcion) {
            try {
                if (valDescripcion == null) {
                    valDescripcion = string.Empty;
                }
                List<LibSearchDefaultValues> vDefaultCriteria = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedCriteria = null;
                vDefaultCriteria.Add(new LibSearchDefaultValues("Descripcion", valDescripcion, true, typeof(string)));
                vFixedCriteria.Add(new LibSearchDefaultValues("ConsecutivoCompania", LibConvert.ToStr(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania")), true, typeof(int)));
                XmlDocument XmlData = new XmlDocument();
                if (LibFKRetrievalHelper.ChooseRecord<FkArticuloInventarioViewModel>("Articulo Inventario", ref XmlData, vDefaultCriteria, vFixedCriteria, new clsArticuloInventarioNav(), string.Empty)) {
                    ConexionDescripcion = LibParserHelper.ParseToList<FkArticuloInventarioViewModel>(XmlData).FirstOrDefault();
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //Metodos Generados


    } //End of class BuscarPrecioDeArticuloViewModel

} //End of namespace Galac.Adm.Uil.Venta

