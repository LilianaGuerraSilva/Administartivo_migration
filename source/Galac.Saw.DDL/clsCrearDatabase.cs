using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using LibGalac.Aos.Dal.DDL;
using LibGalac.Aos.DefGen;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using Galac.Contab.Ccl.WinCont;
using Galac.Adm.Dal.Venta;

namespace Galac.Saw.DDL {
    /// <summary>
    /// favor antes de incorporar codigo, colapse las regiones
    /// para que sepa en cual de las clases va
    /// </summary>
    #region Codigo Producto 100% aos
    public class clsCreateDb {

        private List<CustomRole> _AppPermissions;
        private List<string> _DalComponents;

        [ImportMany]
        public ObservableCollection<Lazy<ILibMefDalComponent, ILibMefDalComponentMetadata>> MefDataComponents {
            get;
            set;
        }

        public clsCreateDb(List<CustomRole> initAppPermissions, List<string> initDalComponents) {
            _AppPermissions = initAppPermissions;
            _DalComponents = initDalComponents;
        }

        public bool MustCreateTables() {
            QAdvDb insTrn = new QAdvDb();
            return insTrn.MustCreateTables(LibDefGen.ProgramInfo.ProgramInitials);
        }

        public bool CrearTablas() {
            bool vResult = true;
            CreateSchemasIfNecesary();
            vResult = CreateTablesIfNecesary();
            vResult = vResult && CreateAllViewsAndSps();
            return vResult;
        }

        private void CreateSchemasIfNecesary() {
            LibCreateDb vCreateDb = new LibCreateDb();
            vCreateDb.CreateSchemas(new string[] { "Lib", "Saw", "Adm", "Comun", "Contab" });
            if (MefDataComponents != null) {
                string[] vSchemas = MefDataComponents
                    .Select(c => c.Value.DbSchema)
                    .Where(s => !LibString.IsNullOrEmpty(s))
                    .Distinct().ToArray();
                vCreateDb.CreateSchemas(vSchemas);
            }
        }

        private bool CreateTablesIfNecesary() {
            bool vResult = true;
            vResult = vResult && CreateLibObjects();
            if (MefDataComponents != null && _DalComponents != null) {
                LibDbo vDbo = new LibDbo();
                foreach (string vDalComponentName in _DalComponents) {
                    ILibMefDalComponent vComponent = MefDataComponents.Where(c => c.Metadata.Name == vDalComponentName).Select(c => c.Value).FirstOrDefault();
                    if (vComponent != null) {
                        if (vDbo.Exists(vComponent.Table, eDboType.Tabla)) {
                            continue;
                        }
                        vResult = vResult && vComponent.InstallTable();
                    }
                }
            } else {
                vResult = false;
            }
            return vResult;
        }

        private bool CreateLibObjects() {
            bool vResult = true;
            vResult = vResult && new LibGalac.Aos.Dal.DDL.LibCreateDb(_AppPermissions).CreateAllLibBaseAndComponentsObjects();
            vResult = vResult && new LibGalac.Aos.Dal.DDL.LibCreateDb().CreateMonetaryReconversion(true);
            return vResult;
        }

        private bool CreateAllViewsAndSps() {
            return new clsReestructurarDatabase().CrearVistasYSps();
        }
    }
    #endregion codigo producto 100% aos

    #region codigo de transicion
    /// <summary>
    /// esto es a lo que a futuro va a ternminar siendo el CrearLasTablas que actualmente tenemos en controlversion
    /// </summary>
    public class clsCrearDatabase {
        bool _CreateCompatibilityView;

        public bool CreateCompatibilityView {
            get { return _CreateCompatibilityView; }
            protected set { _CreateCompatibilityView = value; }
        }

        public clsCrearDatabase() {
            _CreateCompatibilityView = true;
        }

        //<summary>
        //Crea la Tabla de Ciudad y Sus Vistas y SP's
        //Nota: Falta implementar la Carga Inicial
        //</summary>
        //<returns></returns>
        public bool CrearCiudad() {
            bool vResult = new Galac.Comun.Dal.TablasGen.clsCiudadED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboCiudad();
            }
            return vResult;
        }

        /// <summary>
        /// Crea la Tabla de sector de Negocio y sus Vistas y Sp's
        /// </summary>
        /// <returns></returns>
        public bool CrearSectorDeNegocio() {
            ILibHCIL insCargaInicial;
            bool vResult = new Galac.Comun.Dal.TablasGen.clsSectorDeNegocioED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboSectorDeNegocio();
            }
            if (vResult) {
                insCargaInicial = new Galac.Comun.Dal.TablasGen.clsSectorDeNegocioDat();
                vResult = insCargaInicial.InsertDefaultRecords();
            }
            return vResult;
        }

        /// <summary>
        /// Crea la Tabla de Banco y sus Vistas y Sp's
        /// </summary>
        /// <returns></returns>
        public bool CrearBanco() {
            bool vResult = new Galac.Comun.Dal.TablasGen.clsBancoED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboBanco();
            }
            return vResult;
        }

        /// <summary>
        /// Crea la Tabla de Pais y sus Vistas y Sp's
        /// </summary>
        /// <returns></returns>
        public bool CrearPais() {
            bool vResult = new Galac.Comun.Dal.TablasGen.clsPaisED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboPais();
            }
            return vResult;
        }

        /// <summary>
        /// Crea la Tabla de Ciiu y sus Vistas y Sp's
        /// </summary>
        /// <returns></returns>
        public bool CrearCiiu() {
            bool vResult = new Galac.Comun.Dal.TablasGen.clsCIIUED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboCiiu();
            }
            return vResult;
        }

        /// <summary>
        /// Crea la Tabla de Ciiu y sus Vistas y Sp's
        /// </summary>
        /// <returns></returns>
        public bool CrearCategoria() {
            bool vResult = new Galac.Saw.Dal.Inventario.clsCategoriaED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboCategoria();
            }
            return vResult;
        }

        /// <summary>
        /// Crea la Tabla de Modelo y sus Vistas y Sp's
        /// </summary>
        /// <returns></returns>
        public bool CrearModelo() {
            return new Galac.Saw.Dal.Vehiculo.clsModeloED().InstalarTabla();
        }

        /// <summary>
        /// Crea la Tabla de Marca y sus Vistas y Sp's
        /// </summary>
        /// <returns></returns>
        public bool CrearMarca() {
            return new Galac.Saw.Dal.Vehiculo.clsMarcaED().InstalarTabla();
        }

        //<summary>
        //Crea la Tabla de Unidad de venta y sus Vistas y Sp's
        //</summary>
        //<returns></returns>
        public bool CrearUnidadDeVenta() {
            bool vResult = new Galac.Saw.Dal.Tablas.clsUnidadDeVentaED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboUnidadDeVenta();
            }
            return vResult;
        }

        //<summary>
        //Crea la Tabla de Urbanizacion ZP y sus Vistas y Sp's
        //</summary>
        //<returns></returns>
        public bool CrearUrbanizacionZP() {
            bool vResult = new Galac.Saw.Dal.Tablas.clsUrbanizacionZPED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboUrbanizacionZP();
            }
            return vResult;
        }

        //<summary>
        //Crea la Tabla de Zona Cobranza y sus Vistas y Sp's
        //</summary>
        //<returns></returns>
        public bool CrearZonaCobranza() {
            bool vResult = new Galac.Saw.Dal.Tablas.clsZonaCobranzaED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboZonaCobranza();
            }
            return vResult;
        }

        //<summary>
        //Crea la Tabla de Maquina Fiscal y sus Vistas y Sp's
        //</summary>
        //<returns></returns>
        public bool CrearMaquinaFiscal() {
            bool vResult = new Galac.Saw.Dal.Tablas.clsMaquinaFiscalED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboMaquinaFiscal();
            }
            return vResult;
        }

        //<summary>
        //Crea la Tabla de Prop Analisis de Vencimiento y sus Vistas y Sp's
        //</summary>
        //<returns></returns>
        public bool CrearPropAnalisisVenc() {
            bool vResult = new Galac.Saw.Dal.Tablas.clsPropAnalisisVencED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboPropAnalisisVenc();
            }
            return vResult;
        }

        /// <summary>
        /// Crea la Tabla de Talla y sus Vistas y Sp's
        /// </summary>
        /// <returns></returns>
        public bool CrearTalla() {
            bool vResult = new Galac.Saw.Dal.Inventario.clsTallaED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboTalla();
            }
            return vResult;
        }

        /// <summary>
        /// Crea la Tabla de Color y sus Vistas y Sp's
        /// </summary>
        /// <returns></returns>
        public bool CrearColor() {
            bool vResult = new Galac.Saw.Dal.Inventario.clsColorED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboColor();
            }
            return vResult;
        }

        //<summary>
        //Crea la Tabla NotaFinal  y sus Vistas y Sp's
        //</summary>
        //<returns></returns>
        public bool CrearNotaFinal() {
            bool vResult = new Galac.Saw.Dal.Tablas.clsNotaFinalED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboNotaFinal();
            }
            return vResult;
        }

        //<summary>
        //Crea la Tabla de TipoProveedor y sus Vistas y Sp's
        //</summary>
        //<returns></returns>
        public bool CrearTipoProveedor() {
            bool vResult = new Galac.Saw.Dal.Tablas.clsTipoProveedorED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboTipoProveedor();
            }
            return vResult;
        }

        public bool CrearBeneficiario() {
            return new Galac.Adm.Dal.Banco.clsBeneficiarioED().InstalarTabla();
        }

        public bool CrearSolicitudesDePago() {
            return new Galac.Adm.Dal.Banco.clsSolicitudesDePagoED().InstalarTabla();
        }

        public bool CrearIntegracion() {
            return new Galac.Saw.Dal.Integracion.clsIntegracionSawED().InstalarTabla();
        }

        ///// <summary>
        ///// Crea la Tabla de  ReglasDeContabilizacion y sus Vistas y Sp's
        ///// </summary>
        ///// <returns></returns>
        public bool CrearReglasDeContabilizacion() {
            bool vResult = new Galac.Saw.Dal.Contabilizacion.clsReglasDeContabilizacionED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboReglasDeContabilizacion();
            }
            return vResult;
        }

        //<summary>
        //Crea la Tabla de  FormaDelCobro y sus Vistas y Sp's
        //</summary>
        //<returns></returns>
        public bool CrearFormaDelCobro() {
            bool vResult = new Galac.Saw.Dal.Tablas.clsFormaDelCobroED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboFormaDelCobro();
            }
            return vResult;
        }

        public bool CrearVehiculo() {
            bool vResult = new Galac.Saw.Dal.Vehiculo.clsVehiculoED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboVehiculo();
            }
            return vResult;
        }

        public bool CrearAuxiliar() {
            bool vResult = new Galac.Contab.Dal.WinCont.clsAuxiliarED().InstalarTabla();
            if (CreateCompatibilityView) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsAuxiliarMD().CrearVistaDboAuxiliar();
            }
            return vResult;
        }

        public bool CrearAlmacen() {
            bool vResult = new Galac.Saw.Dal.Inventario.clsAlmacenED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboAlmacen();
            }
            return vResult;
        }

        public bool CrearMunicipio() {
            return new Galac.Comun.Dal.TablasGen.clsMunicipioED().InstalarTabla();
        }

        public bool CrearMunicipioCiudad() {
            return new Galac.Comun.Dal.TablasGen.clsMunicipioCiudadED().InstalarTabla();
        }

        public bool CrearValorUT() {
            return new Galac.Comun.Dal.TablasLey.clsValorUTED().InstalarTabla();
        }

        public bool CrearMoneda() {
            return new Galac.Comun.Dal.TablasGen.clsMonedaED().InstalarTabla();
        }

        public bool CrearCuentaBancaria() {
            bool vResult = new Galac.Adm.Dal.Banco.clsCuentaBancariaED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboCuentaBancaria();
            }
            return vResult;
        }

        public bool CrearSettDefinition() {
            return new Galac.Saw.Dal.SttDef.clsSettDefinitionED().InstalarTabla();
        }

        public bool CrearSettValueByCompany() {
            return new Galac.Saw.Dal.SttDef.clsSettValueByCompanyED().InstalarTabla();
        }

        public bool BorrarTablaAnterior(string valTableName) {
            return new LibDbo().Drop(valTableName, eDboType.Tabla);
        }

        public bool CrearParametrosConciliacion() {
            return new Galac.Contab.Dal.WinCont.clsParametrosConciliacionED().InstalarTabla();
        }

        public bool CrearCargaInicial() {
            bool vResult = new Galac.Adm.Dal.GestionCompras.clsCargaInicialED().InstalarTabla();
            if (CreateCompatibilityView) {
                vResult = clsCompatViews.CrearVistaDboCierreCostoDelPeriodo() && vResult;
            }
            return vResult;
        }

        public bool CrearParametrosGen() {
            ILibHCIL insCargaInicial = new Galac.Contab.Dal.WinCont.clsParametrosGenDat();
            bool vResult = new Galac.Contab.Dal.WinCont.clsParametrosGenED().InstalarTabla();
            if (vResult) {
                vResult = insCargaInicial.InsertDefaultRecords();
            }
            return vResult;
        }

        public bool CrearRendicion() {
            return new Galac.Adm.Dal.CajaChica.clsRendicionED().InstalarTabla();
        }

        public bool CrearConceptoBancario() {
            bool vResult = true;
            vResult = vResult && new Galac.Adm.Dal.Banco.clsConceptoBancarioED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboConceptoBancario();
            }
            return vResult;
        }

        public bool CrearClasificadorActividadEconomica() {
            return new Galac.Comun.Dal.Impuesto.clsClasificadorActividadEconomicaED().InstalarTabla();
        }

        public bool CrearFormatosImpMunicipales() {
            return new Galac.Comun.Dal.Impuesto.clsFormatosImpMunicipalesED().InstalarTabla();
        }

        public bool CrearProducto() {
            bool vResult = new Galac.Comun.Dal.LeyCosto.clsProductoED().InstalarTabla();
            vResult = vResult && new Galac.Ent.Dal.Empresarial.clsCompaniaBaseED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.LeyCosto.clsElementoDelCostoED().BorrarVistasYSps();
            return vResult;
        }

        public bool CrearCriterioDeDistribucion() {
            bool vResult = new Galac.Comun.Dal.LeyCosto.clsCriterioDeDistribucionED().InstalarTabla();
            return vResult;
        }

        public bool CrearTarifaN2() {
            bool vResult;
            vResult = new Galac.Comun.Dal.TablasLey.clsTarifaN2ED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboTarifaN2();
            }
            return vResult;
        }

        public bool CrearTablaRetencion() {
            bool vResult;
            vResult = new Galac.Comun.Dal.TablasLey.clsTablaRetencionED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboTablaRetencion();
            }
            return vResult;
        }

        public bool CrearProveedor() {
            bool vResult;
            vResult = new Galac.Adm.Dal.GestionCompras.clsProveedorED().InstalarTabla();
            if (CreateCompatibilityView) {
                vResult = vResult && clsCompatViews.CrearVistaDboProveedor();
            }
            return vResult;
        }

        public bool CrearMonedaLocal() {
            bool vResult;
            vResult = new Galac.Comun.Dal.TablasGen.clsMonedaLocalED().InstalarTabla();
            if (CreateCompatibilityView) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMonedaLocalMD().CrearVistaDboMonedaLocal();
            }
            return vResult;
        }

        public bool CrearTipoDeComprobante() {
            bool vResult = new Galac.Contab.Dal.Tablas.clsTipoDeComprobanteED().InstalarTabla();
            if (CreateCompatibilityView) {
                vResult = vResult && new Galac.Contab.Dal.Tablas.clsTipoDeComprobanteMD().CrearVistaDboTipoDeComprobante();
            }
            return vResult;
        }

        public bool CrearCentroDeCostos() {
            bool vResult = new Galac.Contab.Dal.WinCont.clsCentroDeCostosED().InstalarTabla();
            if (CreateCompatibilityView) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsCentroDeCostosMD().CrearVistaDboCentroDeCostos();
            }
            return vResult;
        }

        public bool CrearElementoDelCosto() {
            return new Galac.Comun.Dal.LeyCosto.clsElementoDelCostoED().InstalarTabla();
        }

        public bool CrearTablaDetraccion() {
            return new Galac.Comun.Dal.Impuesto.clsTablaDetraccionED().InstalarTabla();
        }

        public bool CrearAlicuotaImpuestoEspecial() {
            return new Galac.Comun.Dal.Impuesto.clsAlicuotaImpuestoEspecialED().InstalarTabla();
        }

        public bool CrearLineaDeProducto() {
            bool vResult = new Galac.Saw.Dal.Tablas.clsLineaDeProductoED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboLineaDeProducto();
            }
            return vResult;
        }

        public bool CrearBalanza() {
            return new Galac.Adm.Dal.DispositivosExternos.clsBalanzaED().InstalarTabla();
        }

        public bool CrearImpuestoBancario() {
            bool vResult = new Galac.Saw.Dal.Tablas.clsImpuestoBancarioED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboImpTrasnBancarias();
            }
            return vResult;
        }

        public bool CrearWcontVbTablas() {
            return new Galac.Contab.TablasVbSinMigrar.clsWcontVbTablas().CrearTablas();
        }

        public bool CrearCompra() {
            new Galac.Saw.Dal.Inventario.clsArticuloInventarioED().InstalarVistasYSps();
            bool vResult = new Galac.Adm.Dal.GestionCompras.clsOrdenDeCompraED().InstalarTabla();
            if (vResult) {
                vResult = new Galac.Adm.Dal.GestionCompras.clsCompraED().InstalarTabla();
            }
            if (CreateCompatibilityView) {
                vResult = vResult && clsCompatViews.CrearVistaDboCompra();
            }
            return vResult;
        }

        public bool CrearAranceles() {
            return new Galac.Comun.Dal.Impuesto.clsArancelesED().InstalarTabla();
        }

        public bool CrearProvincia() {
            return new Galac.Comun.Dal.TablasGen.clsProvinciaED().InstalarTabla();
        }
        public bool CrearDepartamento() {
            return new Galac.Comun.Dal.TablasGen.clsDepartamentoED().InstalarTabla();
        }
        public bool CrearUbigeo() {
            return new Galac.Comun.Dal.TablasGen.clsUbigeoED().InstalarTabla();
        }

        public bool CrearPaisSunat() {
            return new Galac.Comun.Dal.TablasGen.clsPaisSunatED().InstalarTabla();
        }

        public bool CrearConveniosSunat() {
            return new Galac.Comun.Dal.TablasGen.clsConveniosSunatED().InstalarTabla();
        }

        public bool CrearCondicionesDePago() {
            return new Galac.Saw.Dal.Tablas.clsCondicionesDePagoED().InstalarTabla();
        }

        public bool CrearCajaRegistradora() {
            bool vResult = new Galac.Adm.Dal.Venta.clsCajaED().InstalarTabla();
            if (vResult) {
                vResult &= new Galac.Adm.Dal.Venta.clsCajaAperturaED().InstalarTabla();
                if (CreateCompatibilityView) {
                    vResult &= clsCompatViews.CrearVistaDboCaja();
                }
            }
            return vResult;
        }

        public bool CrearCambio() {
            bool vResult = new Galac.Comun.Dal.TablasGen.clsCambioED().InstalarTabla();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboCambio();
            return vResult;
        }

        public bool CrearListaDeMateriales() {
            return new Galac.Adm.Dal.GestionProduccion.clsListaDeMaterialesED().InstalarTabla();
        }

        public bool CrearOrdenDeProduccion() {
            return new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionED().InstalarTabla();
        }

        public bool CrearNotaDeEntregaSalida() {
            return new Galac.Saw.Dal.Inventario.clsNotaDeEntradaSalidaED().InstalarTabla();
        }

        public bool CrearTransferenciaEntreCuentasBancarias() {
            return new Galac.Adm.Dal.Banco.clsTransferenciaEntreCuentasBancariasED().InstalarTabla();
        }

        public bool CrearVendedor() {
            bool vResult;
            vResult = new Galac.Adm.Dal.Vendedor.clsVendedorED().InstalarTabla();
            if (CreateCompatibilityView) {
                vResult = vResult && clsCompatViews.CrearVistaDboVendedor();
                vResult = vResult && clsCompatViews.CrearVistaDboRenglonComisionesDeVendedor();
            }
            return vResult;
        }

        public bool CrearRutaDeComercializacion() {
            return new Galac.Saw.Dal.Tablas.clsRutaDeComercializacionED().InstalarTabla();
        }

        public bool CrearGVentasDefiniciones() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("CREATE TABLE dbo.GVentasDefiniciones (");
            vSql.AppendLine("    Id UNIQUEIDENTIFIER CONSTRAINT nnGVentasDefinicionesID NOT NULL,");
            vSql.AppendLine("    ConsecutivoCompania INT,");
            vSql.AppendLine("    ModuleName VARCHAR(100),");
            vSql.AppendLine("    Value VARCHAR(MAX),");
            vSql.AppendLine("   Eliminada BIT NOT NULL,");
            vSql.AppendLine("       CONSTRAINT p_GVentasDefiniciones PRIMARY KEY CLUSTERED (Id ASC)");
            vSql.AppendLine(")");
            return (new LibDbo()).Create("dbo.GVentasDefiniciones", vSql.ToString(), true, eDboType.Tabla);
        }

        public bool CrearLoteDeInventario() {
            return new Galac.Saw.Dal.Inventario.clsLoteDeInventarioED().InstalarTabla();
        }

        public bool CrearAuditoriaConfiguracion() {
            return new Galac.Saw.Dal.Tablas.clsAuditoriaConfiguracionED().InstalarTabla();
        }
        public bool CrearEscalada() {
            return new clsEscaladaED().InstalarTabla();
        }
        public bool CrearVistasYProcedimientos(string[] valModulos) {
            bool vResult = true;
            if (LibArray.Contains(valModulos, "Usuario")) {
                vResult = vResult && new LibGalac.Aos.Dal.Usal.LibGUserED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Ciudad")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsCiudadED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "SectorDeNegocio")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsSectorDeNegocioED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Banco")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsBancoED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Pais")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsPaisED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Ciiu")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsCIIUED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Categoria")) {
                vResult = vResult && new Galac.Saw.Dal.Inventario.clsCategoriaED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Marca")) {
                vResult = vResult && new Galac.Saw.Dal.Vehiculo.clsMarcaED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Modelo")) {
                vResult = vResult && new Galac.Saw.Dal.Vehiculo.clsModeloED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "UnidadDeVenta")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsUnidadDeVentaED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "UrbanizacionZP")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsUrbanizacionZPED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "MaquinaFiscal")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsMaquinaFiscalED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "PropAnalisisVenc")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsPropAnalisisVencED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "ZonaCobranza")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsZonaCobranzaED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "TablaDetraccion")) {
                vResult = vResult && new Galac.Comun.Dal.Impuesto.clsTablaDetraccionED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Talla")) {
                vResult = vResult && new Galac.Saw.Dal.Inventario.clsTallaED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Color")) {
                vResult = vResult && new Galac.Saw.Dal.Inventario.clsColorED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "NotaFinal")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsNotaFinalED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "TipoProveedor")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsTipoProveedorED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Beneficiario")) {
                vResult = vResult && new Galac.Adm.Dal.Banco.clsBeneficiarioED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "SolicitudesDePago")) {
                vResult = vResult && new Galac.Adm.Dal.Banco.clsSolicitudesDePagoED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Integracion")) {
                vResult = vResult && new Galac.Saw.Dal.Integracion.clsIntegracionSawED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "TipoDeComprobante")) {
                vResult = vResult && new Galac.Contab.Dal.Tablas.clsTipoDeComprobanteED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "ReglasDeContabilizacion")) {
                vResult = vResult && new Galac.Saw.Dal.Contabilizacion.clsReglasDeContabilizacionED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Cuentas")) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsCuentaED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "FormaDelCobro")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsFormaDelCobroED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Vehiculo")) {
                vResult = vResult && new Galac.Saw.Dal.Vehiculo.clsVehiculoED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Cliente")) {
                vResult = vResult && new Galac.Saw.Dal.Cliente.clsClienteED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "GrupoDeActivos")) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsGrupoDeActivosED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "ParametrosContables")) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsParametrosGenED().InstalarVistasYSps();
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsParametrosConciliacionED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "CentroDeCostos")) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsCentroDeCostosED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Almacen")) {
                vResult = vResult && new Galac.Saw.Dal.Inventario.clsAlmacenED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "PrnStt")) {
                vResult = vResult && new LibGalac.Aos.Dal.DDL.LibCreateDb().CreatePrnStt(false);
            }
            if (LibArray.Contains(valModulos, "Auxiliar")) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsAuxiliarED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Municipio")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMunicipioED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "MunicipioCiudad")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMunicipioCiudadED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Moneda")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMonedaED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "ValorUT")) {
                vResult = vResult && new Galac.Comun.Dal.TablasLey.clsValorUTED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "CuentaBancaria")) {
                vResult = vResult && new Galac.Adm.Dal.Banco.clsCuentaBancariaED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "ClasificadorActividadEconomica")) {
                vResult = vResult && new Galac.Comun.Dal.Impuesto.clsClasificadorActividadEconomicaED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "FormatosImpMunicipales")) {
                vResult = vResult && new Galac.Comun.Dal.Impuesto.clsFormatosImpMunicipalesED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "SettDefinition")) {
                vResult = vResult && new Galac.Saw.Dal.SttDef.clsSettDefinitionED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "SettValueByCompany")) {
                vResult = vResult && new Galac.Saw.Dal.SttDef.clsSettValueByCompanyED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Vendedor")) {
                vResult = vResult && new Galac.Adm.Dal.Vendedor.clsVendedorED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "ConceptoBancario")) {
                vResult = vResult && new Galac.Adm.Dal.Banco.clsConceptoBancarioED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Rendicion")) {
                vResult = vResult && new Galac.Adm.Dal.Banco.clsMovimientoBancarioED().InstalarVistasYSps();
                vResult = vResult && new Galac.Adm.Dal.CajaChica.clsRendicionED().InstalarVistasYSps();
                vResult = vResult && new Galac.Adm.Dal.CajaChica.clsAlicuotaIVAED().InstalarVistasYSps();
                vResult = vResult && new Galac.Adm.Dal.CajaChica.clsCxPED().InstalarVistasYSps();
                vResult = vResult && new Galac.Adm.Dal.CajaChica.clsRenglonImpuestoMunicipalRetED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Producto")) {
                vResult = vResult && new Galac.Comun.Dal.LeyCosto.clsProductoED().InstalarVistasYSps();
                vResult = vResult && new Galac.Ent.Dal.Empresarial.clsCompaniaBaseED().InstalarVistasYSps();
                vResult = vResult && new Galac.Comun.Dal.LeyCosto.clsElementoDelCostoED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "CriterioDeDistribucion")) {
                vResult = vResult && new Galac.Comun.Dal.LeyCosto.clsCriterioDeDistribucionED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "ElementoDelCosto")) {
                vResult = vResult && new Galac.Comun.Dal.LeyCosto.clsElementoDelCostoED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "TarifaN2")) {
                vResult = vResult && new Galac.Comun.Dal.TablasLey.clsTarifaN2ED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "TablaRetencion")) {
                vResult = vResult && new Galac.Comun.Dal.TablasLey.clsTablaRetencionED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Proveedor")) {
                vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsProveedorED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Moneda")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMonedaED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "MonedaLocal")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMonedaLocalED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Cuenta")) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsGrupoDeActivosED().InstalarVistasYSps();
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsCuentaED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "CentroDeCostos")) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsCentroDeCostosED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Auxiliar")) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsAuxiliarED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "LineaDeProducto")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsLineaDeProductoED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "ArticuloInventario")) {
                vResult = vResult && new Galac.Saw.Dal.Inventario.clsArticuloInventarioED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Factura")) {
                vResult = vResult && new Galac.Adm.Dal.Venta.clsFacturaRapidaED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "RenglonCobroDeFactura")) {
                vResult = vResult && new Galac.Adm.Dal.Venta.clsRenglonCobroDeFacturaED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Anticipo")) {
                vResult = vResult && new Galac.Adm.Dal.CAnticipo.clsAnticipoED().InstalarVistasYSps();
                vResult = vResult && new Galac.Adm.Dal.CAnticipo.clsAnticipoCobradoED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "TablaDetraccion")) {
                vResult = vResult && new Galac.Comun.Dal.Impuesto.clsTablaDetraccionED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "AlicuotaImpuestoEspecial")) {
                vResult = vResult && new Galac.Comun.Dal.Impuesto.clsAlicuotaImpuestoEspecialED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "LineaDeProducto")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsLineaDeProductoED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Compra")) {
                vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsCompraED().InstalarVistasYSps();
                vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsOrdenDeCompraED().InstalarVistasYSps();
                vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsCargaInicialED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Compra")) {
                vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsCompraED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Aranceles")) {
                vResult = vResult && new Galac.Comun.Dal.Impuesto.clsArancelesED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Provincia")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsProvinciaED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Departamento")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsDepartamentoED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Ubigeo")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsUbigeoED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "PaisSunat")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsPaisSunatED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "ConveniosSunat")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsConveniosSunatED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "CondicionesDePago")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsCondicionesDePagoED().InstalarVistasYSps();
            }
            LibViews insVistas = new LibViews(Galac.Contab.Ccl.WinCont.clsCkn.ConfigKeyForDbService);
            vResult = vResult && insVistas.Create("Contab" + ".Gv_EnumComprobanteGeneradoPor", LibTpvCreator.SqlViewStandardEnum(typeof(eComprobanteGeneradoPor), new LibGalac.Aos.Base.Dal.QAdvSql(Galac.Contab.Ccl.WinCont.clsCkn.ConfigKeyForDbService)), true, true);
            if (LibArray.Contains(valModulos, "CargaInicial")) {
                vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsCargaInicialED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "ListaDeMateriales")) {
                vResult = vResult && new Galac.Adm.Dal.GestionProduccion.clsListaDeMaterialesED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "OrdenDeProduccion")) {
                vResult = vResult && new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "NotaDeEntradaSalida")) {
                vResult = vResult && new Galac.Saw.Dal.Inventario.clsNotaDeEntradaSalidaED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "TransferenciaEntreCuentasBancarias")) {
                vResult = vResult && new Galac.Adm.Dal.Banco.clsTransferenciaEntreCuentasBancariasED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "RutaDeComercializacion")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsRutaDeComercializacionED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "LoteDeInventario")) {
                vResult = vResult && new Galac.Saw.Dal.Inventario.clsLoteDeInventarioED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "Contrato")) {
                vResult = vResult && new Galac.Adm.Dal.Venta.clsContratoED().InstalarVistasYSps();
            }
            if (LibArray.Contains(valModulos, "AuditoriaConfiguracion")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsAuditoriaConfiguracionED().InstalarVistasYSps();
            }
            return vResult;
        }
    }
    #endregion codigo de transicion
}