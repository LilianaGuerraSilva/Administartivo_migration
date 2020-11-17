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
            bool vResult = new Galac.Saw.Dal.Vehiculo.clsModeloED().InstalarTabla();
            return vResult;
        }

        /// <summary>
        /// Crea la Tabla de Marca y sus Vistas y Sp's
        /// </summary>
        /// <returns></returns>
        public bool CrearMarca() {
            bool vResult = new Galac.Saw.Dal.Vehiculo.clsMarcaED().InstalarTabla();
            return vResult;
        }


        //<summary>
        //Crea la Tabla de Unidad de venta y sus Vistas y Sp's
        //</summary>
        //<returns></returns>
        public bool CrearUnidadDeVenta() {
            //ILibHCIL insCargaInicial;
            bool vResult = new Galac.Saw.Dal.Tablas.clsUnidadDeVentaED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboUnidadDeVenta();
            }
            /*if (vResult) {
                insCargaInicial = new Galac.Saw.Dal.Tablas.clsUnidadDeVentaDat();
                vResult = insCargaInicial.InsertDefaultRecords();
            }*/
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
            //if (vResult) {
            //   insCargaInicial = new Galac.Saw.Dal.Tablas.clsPropAnalisisVencDat();
            //   vResult = insCargaInicial.InsertDefaultRecords();
            //}
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
            bool vResult = new Galac.Adm.Dal.Banco.clsBeneficiarioED().InstalarTabla();
            return vResult;
        }
        public bool CrearSolicitudesDePago() {
            bool vResult;
            vResult = new Galac.Adm.Dal.Banco.clsSolicitudesDePagoED().InstalarTabla();
            return vResult;
        }
        public bool CrearIntegracion() {
            bool vResult;
            vResult = new Galac.Saw.Dal.Integracion.clsIntegracionSawED().InstalarTabla();
            return vResult;
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
            bool vResult;
            vResult = new Galac.Comun.Dal.TablasGen.clsMunicipioED().InstalarTabla();
            return vResult;

        }
        public bool CrearMunicipioCiudad() {
            bool vResult;
            vResult = new Galac.Comun.Dal.TablasGen.clsMunicipioCiudadED().InstalarTabla();
            return vResult;

        }

        public bool CrearValorUT() {
            bool vResult;
            vResult = new Galac.Comun.Dal.TablasLey.clsValorUTED().InstalarTabla();
            return vResult;
        }

        public bool CrearMoneda() {
            bool vResult;
            vResult = new Galac.Comun.Dal.TablasGen.clsMonedaED().InstalarTabla();
            return vResult;
        }

        public bool CrearCuentaBancaria() {
            bool vResult = new Galac.Adm.Dal.Banco.clsCuentaBancariaED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboCuentaBancaria();
            }
            return vResult;
        }

        public bool CrearSettDefinition() {
            bool vResult = new Galac.Comun.Dal.SttDef.clsSettDefinitionED().InstalarTabla();
            return vResult;
        }
        public bool CrearSettValueByCompany() {
            bool vResult = new Galac.Comun.Dal.SttDef.clsSettValueByCompanyED().InstalarTabla();
            return vResult;
        }
        public bool BorrarTablaAnterior(string valTableName) {
            return new LibDbo().Drop(valTableName, eDboType.Tabla);
        }

        public bool CrearParametrosConciliacion() {
            bool vResult = new Galac.Contab.Dal.WinCont.clsParametrosConciliacionED().InstalarTabla();
            return vResult;
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
            bool vResult = true;
            vResult = vResult && new Galac.Adm.Dal.CajaChica.clsRendicionED().InstalarTabla();
            return vResult;
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
            bool vResult = true;
            vResult = new Galac.Comun.Dal.Impuesto.clsClasificadorActividadEconomicaED().InstalarTabla();
            return vResult;
        }

        public bool CrearFormatosImpMunicipales() {
            bool vResult = true;
            vResult = vResult && new Galac.Comun.Dal.Impuesto.clsFormatosImpMunicipalesED().InstalarTabla();
            return vResult;
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
            bool vResult = new Galac.Comun.Dal.LeyCosto.clsElementoDelCostoED().InstalarTabla();
            return vResult;
        }

        public bool CrearTablaDetraccion() {
            bool vResult = new Galac.Comun.Dal.Impuesto.clsTablaDetraccionED().InstalarTabla();
            return vResult;
        }

        public bool CrearAlicuotaImpuestoEspecial() {
            bool vResult = new Galac.Comun.Dal.Impuesto.clsAlicuotaImpuestoEspecialED().InstalarTabla();
            return vResult;
        }

        public bool CrearLineaDeProducto() {
            bool vResult = new Galac.Saw.Dal.Tablas.clsLineaDeProductoED().InstalarTabla();
            if (CreateCompatibilityView) {
                clsCompatViews.CrearVistaDboLineaDeProducto();
            }
            return vResult;
        }

        public bool CrearBalanza() {
            bool vResult = new Galac.Adm.Dal.DispositivosExternos.clsBalanzaED().InstalarTabla();
            return vResult;
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
            bool vResult = new Galac.Comun.Dal.Impuesto.clsArancelesED().InstalarTabla();
            return vResult;
        }

        public bool CrearProvincia() {
            bool vResult = new Galac.Comun.Dal.TablasGen.clsProvinciaED().InstalarTabla();
            return vResult;
        }
        public bool CrearDepartamento() {
            bool vResult = new Galac.Comun.Dal.TablasGen.clsDepartamentoED().InstalarTabla();
            return vResult;
        }
        public bool CrearUbigeo() {
            bool vResult = new Galac.Comun.Dal.TablasGen.clsUbigeoED().InstalarTabla();
            return vResult;
        }


        public bool CrearPaisSunat() {
            bool vResult = new Galac.Comun.Dal.TablasGen.clsPaisSunatED().InstalarTabla();
            return vResult;
        }


        public bool CrearConveniosSunat() {
            bool vResult = new Galac.Comun.Dal.TablasGen.clsConveniosSunatED().InstalarTabla();
            return vResult;
        }

        public bool CrearCondicionesDePago() {
            bool vResult = new Galac.Saw.Dal.Tablas.clsCondicionesDePagoED().InstalarTabla();
            return vResult;
        }


        public bool CrearCajaRegistradora() {
            bool vResult = new Galac.Adm.Dal.Venta.clsCajaED().InstalarTabla();
            if(vResult) {
                vResult &= new Galac.Adm.Dal.Venta.clsCajaAperturaED().InstalarTabla();
                if(CreateCompatibilityView) {
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
           bool vResult = new Galac.Adm.Dal.GestionProduccion.clsListaDeMaterialesED().InstalarTabla();
           return vResult;
       }
       
       public bool CrearOrdenDeProduccion() {
           bool vResult = new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionED().InstalarTabla();
           return vResult;
       }

        public bool CrearNotaDeEntregaSalida() {
            bool vResult = new Galac.Saw.Dal.Inventario.clsNotaDeEntradaSalidaED().InstalarTabla();
            return vResult;
        }

        public bool CrearVistasYProcedimientos(string[] valModulos) {
            bool vResult = true;
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Usuario")) {
                vResult = vResult && new LibGalac.Aos.Dal.Usal.LibGUserED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Ciudad")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsCiudadED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "SectorDeNegocio")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsSectorDeNegocioED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Banco")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsBancoED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Pais")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsPaisED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Ciiu")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsCIIUED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Categoria")) {
                vResult = vResult && new Galac.Saw.Dal.Inventario.clsCategoriaED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Marca")) {
                vResult = vResult && new Galac.Saw.Dal.Vehiculo.clsMarcaED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Modelo")) {
                vResult = vResult && new Galac.Saw.Dal.Vehiculo.clsModeloED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "UnidadDeVenta")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsUnidadDeVentaED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "UrbanizacionZP")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsUrbanizacionZPED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "MaquinaFiscal")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsMaquinaFiscalED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "PropAnalisisVenc")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsPropAnalisisVencED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "ZonaCobranza")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsZonaCobranzaED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "TablaDetraccion")) {
                vResult = vResult && new Galac.Comun.Dal.Impuesto.clsTablaDetraccionED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Talla")) {
                vResult = vResult && new Galac.Saw.Dal.Inventario.clsTallaED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Color")) {
                vResult = vResult && new Galac.Saw.Dal.Inventario.clsColorED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "NotaFinal")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsNotaFinalED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "TipoProveedor")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsTipoProveedorED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Beneficiario")) {
                vResult = vResult && new Galac.Adm.Dal.Banco.clsBeneficiarioED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "SolicitudesDePago")) {
                vResult = vResult && new Galac.Adm.Dal.Banco.clsSolicitudesDePagoED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Integracion")) {
                vResult = vResult && new Galac.Saw.Dal.Integracion.clsIntegracionSawED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "TipoDeComprobante")) {
                vResult = vResult && new Galac.Contab.Dal.Tablas.clsTipoDeComprobanteED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "ReglasDeContabilizacion")) {
                vResult = vResult && new Galac.Saw.Dal.Contabilizacion.clsReglasDeContabilizacionED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Cuentas")) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsCuentaED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "FormaDelCobro")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsFormaDelCobroED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Vehiculo")) {
                vResult = vResult && new Galac.Saw.Dal.Vehiculo.clsVehiculoED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Cliente")) {
                vResult = vResult && new Galac.Saw.Dal.Cliente.clsClienteED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "GrupoDeActivos")) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsGrupoDeActivosED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "ParametrosContables")) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsParametrosGenED().InstalarVistasYSps();
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsParametrosConciliacionED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "CentroDeCostos")) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsCentroDeCostosED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Almacen")) {
                vResult = vResult && new Galac.Saw.Dal.Inventario.clsAlmacenED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "PrnStt")) {
                vResult = vResult && new LibGalac.Aos.Dal.DDL.LibCreateDb().CreatePrnStt(false);
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Auxiliar")) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsAuxiliarED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Municipio")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMunicipioED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "MunicipioCiudad")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMunicipioCiudadED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Moneda")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMonedaED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "ValorUT")) {
                vResult = vResult && new Galac.Comun.Dal.TablasLey.clsValorUTED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "CuentaBancaria")) {
                vResult = vResult && new Galac.Adm.Dal.Banco.clsCuentaBancariaED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "ClasificadorActividadEconomica")) {
                vResult = vResult && new Galac.Comun.Dal.Impuesto.clsClasificadorActividadEconomicaED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "FormatosImpMunicipales")) {
                vResult = vResult && new Galac.Comun.Dal.Impuesto.clsFormatosImpMunicipalesED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "SettDefinition")) {
                vResult = vResult && new Galac.Comun.Dal.SttDef.clsSettDefinitionED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "SettValueByCompany")) {
                vResult = vResult && new Galac.Comun.Dal.SttDef.clsSettValueByCompanyED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Vendedor")) {
                vResult = vResult && new Galac.Saw.Dal.Vendedor.clsVendedorED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "ConceptoBancario")) {
                vResult = vResult && new Galac.Adm.Dal.Banco.clsConceptoBancarioED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Rendicion")) {
                vResult = vResult && new Galac.Adm.Dal.Banco.clsMovimientoBancarioED().InstalarVistasYSps();
                vResult = vResult && new Galac.Adm.Dal.CajaChica.clsRendicionED().InstalarVistasYSps();
                //vResult = vResult && new Galac.Adm.Dal.CajaChica.clsAnticipoED().InstalarVistasYSps();
                //vResult = vResult && new Galac.Adm.Dal.CajaChica.clsProveedorED().InstalarVistasYSps();
                vResult = vResult && new Galac.Adm.Dal.CajaChica.clsAlicuotaIVAED().InstalarVistasYSps();
                //vResult = vResult && new Galac.Adm.Dal.CajaChica.clsConceptoBancarioED().InstalarVistasYSps();
                vResult = vResult && new Galac.Adm.Dal.CajaChica.clsCxPED().InstalarVistasYSps();
                vResult = vResult && new Galac.Adm.Dal.CajaChica.clsRenglonImpuestoMunicipalRetED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Producto")) {
                vResult = vResult && new Galac.Comun.Dal.LeyCosto.clsProductoED().InstalarVistasYSps();
                vResult = vResult && new Galac.Ent.Dal.Empresarial.clsCompaniaBaseED().InstalarVistasYSps();
                vResult = vResult && new Galac.Comun.Dal.LeyCosto.clsElementoDelCostoED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "CriterioDeDistribucion")) {
                vResult = vResult && new Galac.Comun.Dal.LeyCosto.clsCriterioDeDistribucionED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "ElementoDelCosto")) {
                vResult = vResult && new Galac.Comun.Dal.LeyCosto.clsElementoDelCostoED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "TarifaN2")) {
                vResult = vResult && new Galac.Comun.Dal.TablasLey.clsTarifaN2ED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "TablaRetencion")) {
                vResult = vResult && new Galac.Comun.Dal.TablasLey.clsTablaRetencionED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Proveedor")) {
                vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsProveedorED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Moneda")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMonedaED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "MonedaLocal")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMonedaLocalED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Cuenta")) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsGrupoDeActivosED().InstalarVistasYSps();
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsCuentaED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "CentroDeCostos")) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsCentroDeCostosED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Auxiliar")) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsAuxiliarED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "LineaDeProducto")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsLineaDeProductoED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "ArticuloInventario")) {
                vResult = vResult && new Galac.Saw.Dal.Inventario.clsArticuloInventarioED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Factura")) {
                vResult = vResult && new Galac.Adm.Dal.Venta.clsFacturaRapidaED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "RenglonCobroDeFactura")) {
                vResult = vResult && new Galac.Adm.Dal.Venta.clsRenglonCobroDeFacturaED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Anticipo")) {
                vResult = vResult && new Galac.Adm.Dal.CAnticipo.clsAnticipoED().InstalarVistasYSps();
                vResult = vResult && new Galac.Adm.Dal.CAnticipo.clsAnticipoCobradoED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "TablaDetraccion")) {
                vResult = vResult && new Galac.Comun.Dal.Impuesto.clsTablaDetraccionED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "AlicuotaImpuestoEspecial")) {
                vResult = vResult && new Galac.Comun.Dal.Impuesto.clsAlicuotaImpuestoEspecialED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "LineaDeProducto")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsLineaDeProductoED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Compra")) {
                vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsCompraED().InstalarVistasYSps();
                vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsOrdenDeCompraED().InstalarVistasYSps();
                vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsCargaInicialED().InstalarVistasYSps();
            }

            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Compra")) {
                vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsCompraED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Aranceles")) {
                vResult = vResult && new Galac.Comun.Dal.Impuesto.clsArancelesED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Provincia")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsProvinciaED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Departamento")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsDepartamentoED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Ubigeo")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsUbigeoED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "PaisSunat")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsPaisSunatED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "ConveniosSunat")) {
                vResult = vResult && new Galac.Comun.Dal.TablasGen.clsConveniosSunatED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "CondicionesDePago")) {
                vResult = vResult && new Galac.Saw.Dal.Tablas.clsCondicionesDePagoED().InstalarVistasYSps();
            }

            LibViews insVistas = new LibViews(Galac.Contab.Ccl.WinCont.clsCkn.ConfigKeyForDbService);
            vResult = vResult && insVistas.Create("Contab" + ".Gv_EnumComprobanteGeneradoPor", LibTpvCreator.SqlViewStandardEnum(typeof(eComprobanteGeneradoPor), new LibGalac.Aos.Base.Dal.QAdvSql(Galac.Contab.Ccl.WinCont.clsCkn.ConfigKeyForDbService)), true, true);

            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "CargaInicial")) {
                vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsCargaInicialED().InstalarVistasYSps();
                
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "ListaDeMateriales")) {
                vResult = vResult && new Galac.Adm.Dal.GestionProduccion.clsListaDeMaterialesED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "OrdenDeProduccion")) {
                vResult = vResult && new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionED().InstalarVistasYSps();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "NotaDeEntradaSalida")) {
                vResult = vResult && new Galac.Saw.Dal.Inventario.clsNotaDeEntradaSalidaED().InstalarVistasYSps();
            }
            return vResult;
        }
    }
    #endregion codigo de transicion
}
