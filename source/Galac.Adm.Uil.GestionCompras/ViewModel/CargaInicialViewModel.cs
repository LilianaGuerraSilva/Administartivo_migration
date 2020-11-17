using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Ccl.GestionCompras;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Galac.Adm.Uil.GestionCompras.ViewModel {
    public class CargaInicialViewModel : LibInputViewModelMfc<CargaInicial> {

        #region Constantes
        public const string ConsecutivoCompaniaPropertyName = "ConsecutivoCompania";
        public const string FechaPropertyName = "Fecha";
        public const string ExistenciaPropertyName = "Existencia";
        public const string CostoPropertyName = "Costo";
        public const string CodigoArticuloPropertyName = "CodigoArticulo";
        public const string EsCargaInicialPropertyName = "EsCargaInicial";
        private const string ArticulosPropertyName = "Articulos";
        private const string EstaHabilitadaLaBusquedaPropertyName = "EstaHabilitadaLaBusqueda";
        private const string EstaOcupadoBuscandoPropertyName = "EstaOcupadoBuscando";
        private const string ElCampoCostoEsSoloLecturaPropertyName = "ElCampoCostoEsSoloLectura";
        private const string TextoBotonFiltradoPropertyName = "TextoBotonFiltrado";
        private const string CodigoArticuloDesdePropertyName = "CodigoArticuloDesde";
        private const string CodigoArticuloHastaPropertyName = "CodigoArticuloHasta";
        private const string CategoriaPropertyName = "Categoria";
        private const string MarcaPropertyName = "Marca";
        private const string LineaDeProductoPropertyName = "LineaDeProducto";
        private const string ConexionLineaArticuloPropertyName = "ConexionLineaArticulo";
        private const string ConexionCategoriaArticuloPropertyName = "ConexionCategoriaArticulo";
        #endregion

        #region Variables
        private FkCategoriaViewModel conexionCategoriaViewModel;
        private FkLineaDeProductoViewModel conexionLineaArticulo;
        private FkArticuloInventarioViewModel _ConexionCodigoArticulo = null;
        private bool estaOcupadoBuscando;
        private bool estaHabilitadaLaBusqueda;
        private bool elCampoCostoEsSoloLectura;
        private string marca;
        private string categoria;
        private string articuloDesde;
        private string articuloHasta;
        private string lineaDeProducto;
        private IServicioDeDatosCargaInicial servicioDeDatos;
        private ObservableCollection<ArticuloCargaInicialViewModel> articulos;
        private string textoBotonFiltrado;
        #endregion //Variables

        #region Propiedades

        public ObservableCollection<ArticuloCargaInicialViewModel> Articulos {
            get { return articulos; }
            set {
                if (articulos != value) {
                    articulos = value;
                    RaisePropertyChanged(ArticulosPropertyName);
                }
            }
        }

        public override string ModuleName {
            get { return "Carga Inicial"; }
        }

        #region Columnas del grid
        [LibGridColum("Código del Artículo")]
        public string CodigoArticulo {
            get {
                return Model.CodigoArticulo;
            }
            set {
                if (Model.CodigoArticulo != value) {
                    Model.CodigoArticulo = value;
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                }
            }
        }

        [LibGridColum("Existencia")]
        public decimal Existencia {
            get {
                return Model.Existencia;
            }
            set {
                if (Model.Existencia != value) {
                    Model.Existencia = value;
                    IsDirty = true;
                    RaisePropertyChanged(ExistenciaPropertyName);
                }
            }
        }

        [LibGridColum("Costo")]
        public decimal Costo {
            get {
                return Model.Costo;
            }
            set {
                if (Model.Costo != value) {
                    Model.Costo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CostoPropertyName);
                }
            }
        }
        #endregion

        #region Validaciones
        [LibCustomValidation("FechaValidating")]
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
        #endregion

        #region Visibilidad
        public bool EstaHabilitadaLaBusqueda {
            get { return estaHabilitadaLaBusqueda; }
            set {
                if (estaHabilitadaLaBusqueda != value) {
                    estaHabilitadaLaBusqueda = value;
                    RaisePropertyChanged(EstaHabilitadaLaBusquedaPropertyName);
                    FiltrarBusquedaCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public bool EstaOcupadoBuscando {
            get { return estaOcupadoBuscando; }
            set {
                if (estaOcupadoBuscando != value) {
                    estaOcupadoBuscando = value;
                    RaisePropertyChanged(EstaOcupadoBuscandoPropertyName);
                }
            }
        }

        public bool ElCampoCostoEsSoloLectura {
            get { return elCampoCostoEsSoloLectura; }
            set {
                if (elCampoCostoEsSoloLectura != value) {
                    elCampoCostoEsSoloLectura = value;
                    IsDirty = true;
                    RaisePropertyChanged(ElCampoCostoEsSoloLecturaPropertyName);
                }
            }
        }

        public string TextoBotonFiltrado {
            get {
                if (Action == eAccionSR.Modificar) {
                    return "Aplicar otro filtro";
                } else {
                    return "Filtrar Busqueda";
                }
            }
            set {
                if (value != textoBotonFiltrado) {
                    textoBotonFiltrado = value;
                }
                RaisePropertyChanged(TextoBotonFiltradoPropertyName);
            }
        }

        #endregion

        #region Commands
        public RelayCommand<string> ChooseCodigoDesdeArticuloCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoHastaArticuloCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCategoriaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseLineaDeProductoCommand {
            get;
            private set;
        }
        public RelayCommand FiltrarBusquedaCommand {
            get;
            private set;
        }

        #endregion

        #region Filtros de Busqueda
        public string CodigoArticuloDesde {
            get { return articuloDesde; }
            set {
                if (articuloDesde != value) {
                    articuloDesde = value;
                    RaisePropertyChanged(CodigoArticuloDesdePropertyName);
                }
            }
        }

        public string CodigoArticuloHasta {
            get { return articuloHasta; }
            set {
                if (articuloHasta != value) {
                    articuloHasta = value;
                    RaisePropertyChanged(CodigoArticuloHastaPropertyName);
                }
            }
        }

        public string Categoria {
            get { return categoria; }
            set {
                if (categoria != value) {
                    categoria = value;
                    RaisePropertyChanged(CategoriaPropertyName);
                }
            }
        }

        public string Marca {
            get { return marca; }
            set {
                if (marca != value) {
                    marca = value;
                    RaisePropertyChanged(MarcaPropertyName);
                }
            }
        }

        public string LineaDeProducto {
            get { return lineaDeProducto; }
            set {
                if (lineaDeProducto != value) {
                    lineaDeProducto = value;
                    RaisePropertyChanged(LineaDeProductoPropertyName);
                }
            }
        }

        #endregion

        #region Conexiones FK
        public FkLineaDeProductoViewModel ConexionLineaArticulo {
            get { return conexionLineaArticulo; }
            set {
                if (conexionLineaArticulo != value) {
                    conexionLineaArticulo = value;
                    RaisePropertyChanged(ConexionLineaArticuloPropertyName);
                }
            }
        }

        public FkArticuloInventarioViewModel ConexionCodigoArticulo {
            get {
                return _ConexionCodigoArticulo;
            }
            set {
                if (_ConexionCodigoArticulo != value) {
                    _ConexionCodigoArticulo = value;
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                }
            }
        }

        public FkCategoriaViewModel ConexionCategoriaArticulo {
            get { return conexionCategoriaViewModel; }
            set {
                if (conexionCategoriaViewModel != value) {
                    conexionCategoriaViewModel = value;
                    RaisePropertyChanged(ConexionCategoriaArticuloPropertyName);
                }
            }
        }
        #endregion 

        #endregion //Propiedades

        #region Constructores
        public CargaInicialViewModel()
            : this(new CargaInicial(), eAccionSR.Insertar) {
        }
        public CargaInicialViewModel(CargaInicial initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {

            if (Action == eAccionSR.Consultar) {
                ElCampoCostoEsSoloLectura = true;
                EstaHabilitadaLaBusqueda = false;
            } else {
                ElCampoCostoEsSoloLectura = false;
                EstaHabilitadaLaBusqueda = true;
            }
            if (servicioDeDatos == null) {
                servicioDeDatos = new clsCargaInicialNav();
            }
            DefaultFocusedPropertyName = ConsecutivoCompaniaPropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            Articulos = new ObservableCollection<ArticuloCargaInicialViewModel>();
            EstaOcupadoBuscando = false;
        }
        #endregion //Constructores

        #region Metodos Generados

        protected override void InitializeLookAndFeel(CargaInicial valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override CargaInicial FindCurrentRecord(CargaInicial valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            Articulos = new ObservableCollection<ArticuloCargaInicialViewModel>();
            Articulos.Add(new ArticuloCargaInicialViewModel(valModel));
            if (Action == eAccionSR.Modificar) {
                if (servicioDeDatos == null) {
                    servicioDeDatos = new clsCargaInicialNav();
                }
                XElement updatedRecord = servicioDeDatos.ActualizarRecordModificado(valModel.ConsecutivoCompania, valModel.Consecutivo);
                ActualizarRecordModificado(updatedRecord);
            }
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<CargaInicial>, IList<CargaInicial>> GetBusinessComponent() {
            return new clsCargaInicialNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            FiltrarBusquedaCommand = new RelayCommand(ExecuteFiltrarBusquedaCommand, () => { return EstaHabilitadaLaBusqueda; });
            ChooseCategoriaCommand = new RelayCommand<string>(ExecuteChooseCategoriaCommand, t => { return EstaHabilitadaLaBusqueda; });
            ChooseLineaDeProductoCommand = new RelayCommand<string>(ExecuteChooseLineaDeProductoCommand, t => { return EstaHabilitadaLaBusqueda; });
            ChooseCodigoDesdeArticuloCommand = new RelayCommand<string>(ExecuteChooseCodigoDesdeArticuloCommand, t => { return EstaHabilitadaLaBusqueda; });
            ChooseCodigoHastaArticuloCommand = new RelayCommand<string>(ExecuteChooseCodigoHastaArticuloCommand, t => { return EstaHabilitadaLaBusqueda; });
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

        #region Executes
        private void ExecuteChooseCategoriaCommand(string valCategoria) {
            try {
                if (valCategoria == null) {
                    valCategoria = string.Empty;
                }
                if (Categoria == valCategoria && valCategoria != string.Empty) return;
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Descripcion", valCategoria);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCategoriaArticulo = ChooseRecord<FkCategoriaViewModel>("Categoria", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCategoriaArticulo != null) {
                    Categoria = ConexionCategoriaArticulo.Descripcion;
                } else {
                    Categoria = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseLineaDeProductoCommand(string valLinea) {
            try {
                if (valLinea == null) {
                    valLinea = string.Empty;
                }
                if (LineaDeProducto == valLinea) {
                    return;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valLinea);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionLineaArticulo = ChooseRecord<FkLineaDeProductoViewModel>("Linea De Producto", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionLineaArticulo != null) {
                    LineaDeProducto = ConexionLineaArticulo.Nombre;
                } else {
                    Categoria = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCodigoDesdeArticuloCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                if (valCodigo == CodigoArticuloDesde && !LibString.IsNullOrEmpty(CodigoArticuloDesde)) {
                    return;
                }
                CodigoArticuloDesde = valCodigo;
                if (Action == eAccionSR.Insertar) {
                    LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("ArticuloInventario.Codigo", valCodigo);
                    LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ArticuloInventario.ConsecutivoCompania", Mfc.GetInt("Compania"));
                    ConexionCodigoArticulo = ChooseRecord<FkArticuloInventarioViewModel>("Articulo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                } else {
                    LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Adm.Gv_CargaInicial_B1.CodigoArticulo", valCodigo);
                    LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_CargaInicial_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                    ConexionCodigoArticulo = ChooseRecord<FkArticuloInventarioViewModel>("Articulo Inventario - Carga Inicial", vDefaultCriteria, vFixedCriteria, string.Empty);
                }
                if (ConexionCodigoArticulo != null) {
                    CodigoArticuloDesde = ConexionCodigoArticulo.Codigo;
                } else {
                    CodigoArticuloDesde = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCodigoHastaArticuloCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                if (valCodigo == CodigoArticuloHasta && !LibString.IsNullOrEmpty(CodigoArticuloDesde)) {
                    return;
                }
                if (Action == eAccionSR.Insertar) {
                    LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("ArticuloInventario.Codigo", valCodigo);
                    LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ArticuloInventario.ConsecutivoCompania", Mfc.GetInt("Compania"));
                    ConexionCodigoArticulo = ChooseRecord<FkArticuloInventarioViewModel>("Articulo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                } else {
                    LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Adm.Gv_CargaInicial_B1.CodigoArticulo", valCodigo);
                    LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_CargaInicial_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                    ConexionCodigoArticulo = ChooseRecord<FkArticuloInventarioViewModel>("Articulo Inventario - Carga Inicial", vDefaultCriteria, vFixedCriteria, string.Empty);
                }

                if (ConexionCodigoArticulo != null) {
                    CodigoArticuloHasta = ConexionCodigoArticulo.Codigo;
                } else {
                    CodigoArticuloHasta = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteFiltrarBusquedaCommand() {
            DateTime vFechaInicial = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("CargaInicial", "FechaInicial");
            int vConsecutivoCompania = Mfc.GetInt("Compania");
            FiltrarArticulosAsync(vConsecutivoCompania, vFechaInicial);
            ChooseCategoriaCommand.RaiseCanExecuteChanged();
        }
        #endregion

        #region Metodos Auxiliares
        private void FiltrarArticulosAsync(int valConsecutivoCompania, DateTime valFechaInicial) {
            try {
                if (Action == eAccionSR.Modificar) {
                    if (Articulos != null && Articulos.Any(t => t.articulo.TieneCambios)) {
                        if (!LibMessages.MessageBox.YesNo(this, "Algunos artículos fueron modificados, y los cambios no han sido guardados. ¿Desea continuar?",
                            "Cambios sin guardar")) {
                            return;
                        }
                    }
                }
                EstaHabilitadaLaBusqueda = false;
                EstaOcupadoBuscando = true;
                RecargarCanExecuteCommands();
                Articulos = null;
                Task<XElement> vTask = Task<XElement>.Factory.StartNew(() => {
                    return (Action == eAccionSR.Insertar)
                        ? servicioDeDatos.ObtenerArticulosInsertarCargaInicial(valConsecutivoCompania, Marca, valFechaInicial, Categoria, LineaDeProducto, CodigoArticuloDesde, CodigoArticuloHasta)
                        : servicioDeDatos.ObtenerArticulosModificarCargaInicial(valConsecutivoCompania, Marca, valFechaInicial, Categoria, LineaDeProducto, CodigoArticuloDesde, CodigoArticuloHasta);
                });
                vTask.ContinueWith((t) => {
                    Articulos = XmlToObservableCollection(vTask.Result);
                }).ContinueWith((t) => {
                    EstaHabilitadaLaBusqueda = true;
                    EstaOcupadoBuscando = false;
                    if (Articulos == null || Articulos.Count == 0) {
                        LibMessages.MessageBox.Information(this, "No se encontraron articulos con estas características", "Articulos no encontrados");
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            } catch (Exception ex) {
                LibMessages.MessageBox.Error(this, "Error de consulta de base de datos" + Environment.NewLine + ex.Message, "Error");
                EstaHabilitadaLaBusqueda = true;
            }
        }

        private ObservableCollection<ArticuloCargaInicialViewModel> XmlToObservableCollection(XElement datosXML) {
            if (datosXML == null) return null;
            var Codigos = datosXML.Descendants().Where(y => y.Name == "CodigoArticulo").Select((t) => t.Value).ToList();
            var Cantidades = datosXML.Descendants().Where(y => y.Name == ((Action == eAccionSR.Insertar) ? "Cantidad" : "Existencia")).Select((t) => t.Value).ToList();
            var Costos = datosXML.Descendants().Where(y => y.Name == ((Action == eAccionSR.Insertar) ? "CostoUnitario" : "Costo")).Select((t) => t.Value).ToList();
            List<string> Consecutivos = (Action == eAccionSR.Modificar)
                ? datosXML.Descendants().Where(y => y.Name == "Consecutivo").Select(t => t.Value).ToList()
                : null;
            List<string> TimeStamps = (Action == eAccionSR.Modificar)
                ? datosXML.Descendants().Where(t => t.Name == "fldTimeStampBigint").Select(t => t.Value).ToList()
                : null;
            ObservableCollection<ArticuloCargaInicialViewModel> articulos = (Action == eAccionSR.Insertar)
                ? CargaDeArticulosAObservableCollecionConsultarOInsertar(Codigos, Cantidades, Costos)
                : (Action == eAccionSR.Consultar)
                    ? CargaDeArticulosAObservableCollecionConsultarOInsertar(Codigos, Cantidades, Costos)
                    : CargaDeArticulosAObservableCollecionModificar(Codigos, Cantidades, Costos, Consecutivos, TimeStamps);
            return articulos;
        }

        private ObservableCollection<ArticuloCargaInicialViewModel> CargaDeArticulosAObservableCollecionConsultarOInsertar(List<string> Codigos, List<string> Cantidades, List<string> Costos) {
            ObservableCollection<ArticuloCargaInicialViewModel> vArticulos = new ObservableCollection<ArticuloCargaInicialViewModel>();
            for (int i = 0; i < Codigos.Count; i++) {
                vArticulos.Add(new ArticuloCargaInicialViewModel(new CargaInicial() {
                    CodigoArticulo = Codigos[i],
                    Existencia = LibConvert.ToDec(Cantidades[i].Replace(".", ","), 2),
                    Costo = LibConvert.ToDec(Costos[i].Replace(".", ","), 2)
                }));
            }
            return vArticulos;
        }

        private ObservableCollection<ArticuloCargaInicialViewModel> CargaDeArticulosAObservableCollecionModificar(List<string> Codigos, List<string> Cantidades, List<string> Costos, List<string> Consecutivos, List<string> TimeStamps) {
            ObservableCollection<ArticuloCargaInicialViewModel> articulos = new ObservableCollection<ArticuloCargaInicialViewModel>();
            for (int i = 0; i < Codigos.Count; i++) {
                decimal vExistencia = LibConvert.ToDec(Cantidades[i].Replace(".", ","), 2);
                decimal vCostoInicial = LibConvert.ToDec(Costos[i].Replace(".", ","), 2);
                articulos.Add(new ArticuloCargaInicialViewModel(new CargaInicial(vCostoInicial) {
                    fldTimeStamp = LibConvert.ToLong(TimeStamps[i]),
                    Consecutivo = LibConvert.ToInt(Consecutivos[i]),
                    CodigoArticulo = Codigos[i],
                    Existencia = decimal.Round(vExistencia, 2),
                    Costo = decimal.Round(vCostoInicial, 2)
                }));
            }
            return articulos;
        }

        private void ActualizarRecordModificado(XElement document) {
            int vConsecutivo = LibConvert.ToInt(ObtenerValorTagXml(document, "Consecutivo"));
            long vTimeStamp = LibConvert.ToLong(ObtenerValorTagXml(document, "fldTimeStampBigint"));
            if (vConsecutivo == 0 || vTimeStamp == 0) {
                return;
            }
            ActualizarRecord(vConsecutivo, vTimeStamp);
        }

        private void ActualizarRecord(int vConsecutivo, long vTimeStamp) {
            try {
                if (Articulos != null) {
                    Articulos.Where(t => t.articulo.Consecutivo == vConsecutivo).
                    Single(t => {
                        t.articulo.fldTimeStamp = vTimeStamp;
                        return true;
                    });
                }
            } catch (InvalidOperationException ex) {
            } catch (Exception exc) {
                LibMessages.MessageBox.Error(this, "Hubo un error al intentar modificar el artículo." + Environment.NewLine + exc.Message, "Error");
            }
        }

        private string ObtenerValorTagXml(XmlDocument document, string nombreEtiqueta) {
            string vResult = string.Empty;
            try {
                vResult = XElement.Parse(document.InnerXml).Descendants().Where(y => y.Name == nombreEtiqueta).Select(t => t.Value).First(); //(nombreEtiqueta).First().Value.ToString();
            } catch (Exception ex) {
            }
            return vResult;
        }

        private string ObtenerValorTagXml(XElement document, string nombreEtiqueta) {
            string vResult = string.Empty;
            try {
                vResult = XElement.Parse(document.ToString()).Descendants().Where(y => y.Name == nombreEtiqueta).Select(t => t.Value).First(); //(nombreEtiqueta).First().Value.ToString();
            } catch (Exception ex) {
            }
            return vResult;
        }

        private void RecargarCanExecuteCommands() {
            FiltrarBusquedaCommand.RaiseCanExecuteChanged();
            ChooseCategoriaCommand.RaiseCanExecuteChanged();
            ChooseLineaDeProductoCommand.RaiseCanExecuteChanged();
            ChooseCodigoDesdeArticuloCommand.RaiseCanExecuteChanged();
            ChooseCodigoHastaArticuloCommand.RaiseCanExecuteChanged();
        }

        public IEnumerable<ArticuloCargaInicial> ToArticuloCargaInicial(IEnumerable<ArticuloCargaInicialViewModel> valArticulos) {
            IList<ArticuloCargaInicial> vArticulos = new List<ArticuloCargaInicial>();
            foreach (var articulo in valArticulos) {
                vArticulos.Add(articulo.ToArticuloCargaInicial());
            }
            return vArticulos;
        }

        #endregion

        #region Overrides Libreria
        protected override bool CreateRecord() {
            if (Articulos == null || Articulos.Count == 0) {
                LibMessages.MessageBox.Warning(this, "No existen artículos a insertar. " + Environment.NewLine + "Debe Agregar a la lista los artículos que desea insertar.", "Lista de artículos vacía");
                return false;
            }
            int vConsecutivoCompania = Mfc.GetInt("Compania");
            DateTime vFecha = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("CargaInicial", "FechaInicial");
            bool vResult = servicioDeDatos.InsertarCargaDeArticulos(vConsecutivoCompania, ToArticuloCargaInicial(Articulos), vFecha, (Action == eAccionSR.Insertar));
            Articulos.Clear();
            LibMessages.MessageBox.Information(this, "Los artículos ya fueron insertados.", "Artículos Insertados");
            return vResult;
        }

        protected override bool UpdateRecord() {
            if (Articulos == null || Articulos.Count == 0 || !Articulos.Any(t => t.articulo.TieneCambios)) {
                LibMessages.MessageBox.Warning(this, "No modificó ningún artículo. " + Environment.NewLine + "Debe Agregar modificar al menos algún artículo para continuar.", "No se realizó ninguna modificación");
                return false;
            }
            int vConsecutivoCompania = Mfc.GetInt("Compania");
            DateTime vFecha = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("CargaInicial", "FechaInicial");
            bool vResult = servicioDeDatos.ModificarCargaDeArticulos(vConsecutivoCompania, ToArticuloCargaInicial(Articulos), vFecha);
            if (vResult) {
                LibMessages.MessageBox.Information(this, "Los artículos ya fueron modificados.", "Artículos Modificados");
            } else {
                LibMessages.MessageBox.Warning(this, "No modificó ningún artículo. " + Environment.NewLine + "Debe Agregar modificar al menos algún artículo para continuar.", "No se realizó ninguna modificación");
            }
            var x = Articulos.Where(t => t.articulo.TieneCambios).
                All(t => {
                    t.articulo.TieneCambios = false;
                    return true;
                });
            return vResult;
        }

        protected override void ExecuteCancel() {
            if (Articulos.Any(t => t.articulo.TieneCambios)) {
                if (LibMessages.MessageBox.YesNo(this,
                    "Hay articulos modificados sin guardar. ¿Desea salir sin guardar?",
                    "Salir sin guardar")) {
                    base.ExecuteCancel();
                }
            }
            else {
                base.ExecuteCancel();
            }
        }

        protected override bool CanExecuteAction() {
            return Action != eAccionSR.Modificar;
        }
        #endregion

    } //End of class CargaInicialViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras