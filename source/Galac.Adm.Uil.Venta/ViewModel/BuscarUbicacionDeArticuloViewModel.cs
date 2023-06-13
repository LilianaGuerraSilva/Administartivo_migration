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
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Brl.Inventario;
using LibGalac.Aos.Uil;
using System.Xml;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class BuscarUbicacionDeArticuloViewModel : LibGenericViewModel {
        #region Constantes
        public const string ArticuloPropertyName = "Articulo";
        public const string DescripcionPropertyName = "Descripcion";
        public const string AlmacenPropertyName = "Almacen";
        #endregion
        #region Variables
        private FkBuscarUbicacionDeArticuloViewModel _ConexionArticulo = null;
        private FkBuscarUbicacionDeArticuloViewModel _ConexionDescripcion = null;
        //private FkAlmacenViewModel _ConexionAlmacen = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Consultar Ubicacion De Artículo"; }
        }

        public int  ConsecutivoCompania {
            get {
                return ConsecutivoCompania;
            }
            set {
                if (ConsecutivoCompania != value) {
                    ConsecutivoCompania = value;
                }
            }
        }

        [LibGridColum("Código", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "Articulo", ConnectionSearchCommandName = "ChooseArticuloCommand", MaxWidth=120)]
        public string  Articulo {
            get {
                return Articulo;
            }
            set {
                if (Articulo != value) {
                    Articulo = value;
                    RaisePropertyChanged(ArticuloPropertyName);
                    if (LibString.IsNullOrEmpty(Articulo, true)) {
                        ConexionArticulo = null;
                    }
                }
            }
        }

        [LibGridColum("Descripción", eGridColumType.Connection, ConnectionDisplayMemberPath = "Descripcion", ConnectionModelPropertyName = "Descripcion", ConnectionSearchCommandName = "ChooseDescripcionCommand", MaxWidth=120)]
        public string  Descripcion {
            get {
                return Descripcion;
            }
            set {
                if (Descripcion != value) {
                    Descripcion = value;
                    RaisePropertyChanged(DescripcionPropertyName);
                    if (LibString.IsNullOrEmpty(Descripcion, true)) {
                        ConexionDescripcion = null;
                    }
                }
            }
        }

        [LibGridColum("Codigo", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "Almacen", ConnectionSearchCommandName = "ChooseAlmacenCommand", MaxWidth=120)]
        public string  Almacen {
            get {
                return Almacen;
            }
            set {
                if (Almacen != value) {
                    Almacen = value;
                    RaisePropertyChanged(AlmacenPropertyName);
                    if (LibString.IsNullOrEmpty(Almacen, true)) {
                        //ConexionAlmacen = null;
                    }
                }
            }
        }

        public FkBuscarUbicacionDeArticuloViewModel ConexionArticulo {
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

        public FkBuscarUbicacionDeArticuloViewModel ConexionDescripcion {
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

        //public FkAlmacenViewModel ConexionAlmacen {
        //    get {
        //        return _ConexionAlmacen;
        //    }
        //    set {
        //        if (_ConexionAlmacen != value) {
        //            _ConexionAlmacen = value;
        //            RaisePropertyChanged(AlmacenPropertyName);
        //        }
        //        if (_ConexionAlmacen == null) {
        //            Almacen = string.Empty;
        //        }
        //    }
        //}

        public RelayCommand<string> ChooseArticuloCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseDescripcionCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseAlmacenCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public BuscarUbicacionDeArticuloViewModel() { 
            //: this(new BuscarUbicacionArticulo(), eAccionSR.Insertar) {
        }
        //public BuscarUbicacionDeArticuloViewModel(BuscarUbicacionArticulo initModel, eAccionSR initAction)
        //    : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        //    DefaultFocusedPropertyName = ArticuloPropertyName;
        //    Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        //}
        #endregion //Constructores
        #region Metodos Generados

        //protected override void InitializeLookAndFeel(BuscarUbicacionArticulo valModel) {
        //    base.InitializeLookAndFeel(valModel);
        //}

        //protected override BuscarUbicacionDeArticulo FindCurrentRecord(BuscarUbicacionDeArticulo valModel) {
        //    if (valModel == null) {
        //        return null;
        //    }
        //    LibGpParams vParams = new LibGpParams();
        //    vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
        //    vParams.AddInString("Almacen", valModel.Almacen, 15);
        //    vParams.AddInString("Articulo", valModel.Articulo, 15);
        //    return BusinessComponent.GetData(eProcessMessageType.SpName, "BuscarUbicacionDeArticuloGET", vParams.Get()).FirstOrDefault();
        //}

        //protected override ILibBusinessComponentWithSearch<IList<BuscarUbicacionDeArticulo>, IList<BuscarUbicacionDeArticulo>> GetBusinessComponent() {
        //    return new clsBuscarUbicacionDeArticuloNav();
        //}

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseArticuloCommand = new RelayCommand<string>(ExecuteChooseArticuloCommand);
            ChooseDescripcionCommand = new RelayCommand<string>(ExecuteChooseDescripcionCommand);
            //ChooseAlmacenCommand = new RelayCommand<string>(ExecuteChooseAlmacenCommand);
        }

        //protected override void ReloadRelatedConnections() {
        //    base.ReloadRelatedConnections();
        //    ConexionArticulo = FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Artículo Inventario", LibSearchCriteria.CreateCriteria("Codigo", Articulo));
        //    ConexionDescripcion = FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Artículo Inventario", LibSearchCriteria.CreateCriteria("Descripcion", Descripcion));
        //    ConexionAlmacen = FirstConnectionRecordOrDefault<FkAlmacenViewModel>("Almacén", LibSearchCriteria.CreateCriteria("Codigo", Almacen));
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
                if (LibFKRetrievalHelper.ChooseRecord<FkBuscarUbicacionDeArticuloViewModel>("Ubicacion Articulo", ref XmlData, vDefaultCriteria, vFixedCriteria, new clsArticuloInventarioNav(), string.Empty)) {
                    ConexionArticulo = LibParserHelper.ParseToList<FkBuscarUbicacionDeArticuloViewModel>(XmlData).FirstOrDefault();
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
                //List<LibSearchDefaultValues> vDefaultCriteria = new List<LibSearchDefaultValues>();
                //List<LibSearchDefaultValues> vFixedCriteria = null;
                //vDefaultCriteria.Add(new LibSearchDefaultValues("Descripcion", valDescripcion, true, typeof(string)));
                //vFixedCriteria.Add(new LibSearchDefaultValues("ConsecutivoCompania", LibConvert.ToStr(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania")), true, typeof(int)));
                //XmlDocument XmlData = new XmlDocument();
                //if (LibFKRetrievalHelper.ChooseRecord<FkBuscarUbicacionDeArticuloViewModel>("Ubicacion Articulo", ref XmlData, vDefaultCriteria, vFixedCriteria, new clsArticuloInventarioNav(), string.Empty)) {
                //    ConexionDescripcion = LibParserHelper.ParseToList<FkBuscarUbicacionDeArticuloViewModel>(XmlData).FirstOrDefault();
                //}
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //Metodos Generados


    } //End of class BuscarUbicacionDeArticuloViewModel

} //End of namespace Galac.Adm.Uil.Venta

