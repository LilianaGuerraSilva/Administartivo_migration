using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using Galac.Saw.Brl.Integracion;
using Galac.Saw.Ccl.Integracion;
using Galac.Saw.DDL.VersionesReestructuracion;
using Galac.Contab.Ccl.WinCont;

namespace Galac.Saw.DDL {
    /// <summary>
    /// aqui irian los metodos de reestructuracion de la base de datos
    /// </summary>
    public class clsReestructurarDatabase : QAdvReest, ILibRestructureDb, ILibRestructureToVersion {


        string _CurrentVersionInDb { get; set; }
        string _VersionToUpdate { get; set; }
        string _CurrentDataBaseName { get; set; }
        string _TodayAsSqlValue { get; set; }

        string CurrentVersionInDb {
            get { return _CurrentVersionInDb; }
            set { _CurrentVersionInDb = value; }
        }


        #region Constructores

        public clsReestructurarDatabase() {
        }

        public clsReestructurarDatabase(string initCurrentVersionInDb) {
            _TodayAsSqlValue = InsSql.ToSqlValue(LibDate.Today());
            CurrentVersionInDb = initCurrentVersionInDb;
        }

        public clsReestructurarDatabase(string initCurrentVersion, string initVersionToUpdate, string initDatabaseName)
           : base() {
            _CurrentVersionInDb = initCurrentVersion;
            _VersionToUpdate = initVersionToUpdate;
            _CurrentDataBaseName = initDatabaseName;
            _TodayAsSqlValue = InsSql.ToSqlValue(LibDate.Today());
        }

        public clsReestructurarDatabase(string initCurrentVersion, string initVersionToUpdate, string initConfigKeyForDbService, string initDatabaseName)
           : base(initConfigKeyForDbService) {
            _CurrentVersionInDb = initCurrentVersion;
            _VersionToUpdate = initVersionToUpdate;
            _CurrentDataBaseName = initDatabaseName;
            _TodayAsSqlValue = InsSql.ToSqlValue(LibDate.Today());
        }
        #endregion Constructores

        #region Vistas

        #region Miembros de ILibRestructureDb
        bool ILibRestructureDb.CreateAllViewsAndSps() {
            return CrearVistasYSps();
        }

        bool ILibRestructureDb.DropAllViewsAndSps() {
            return BorrarVistasYSps();
        }

        string ILibRestructureDb.GetMinVersionAppAllowedForReestructure() {
            return "1.0";
        }
        #endregion

        #region Miembros de ILibRestructureToVersion
        string ILibRestructureToVersion.FriendlyDescription {
            get { return "Actualizar versiones anteriores de base de datos"; }
        }

        bool ILibRestructureToVersion.HasToUpgradeToVersion(string valCurrentVersionInDb) {
            return true;
        }

        string ILibRestructureToVersion.NewDbVersion {
            get { return "2.21"; }
        }

        void ILibRestructureToVersion.UpgradeToNewVersion(string valCurrentVersionInDb, System.ComponentModel.BackgroundWorker valBgWorker) {
            UpgradeToNewVersion(valCurrentVersionInDb, string.Empty);
        }

        void ILibRestructureToVersion.UpgradeToTempNonOfficialVersion(string valCurrentVersionInDb, System.ComponentModel.BackgroundWorker valBgWorker) {

        }
        #endregion

        public bool CrearVistasYSps() {
            bool vResult = true;
            vResult = vResult && CrearVistasDeContabilidad();
            vResult = vResult && new LibGalac.Aos.Dal.PASOnLine.LibDiagnosticED().InstalarVistasYSps();
            vResult = vResult && new LibGalac.Aos.Dal.Settings.LibDiagnosticSttED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsCiudadED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsSectorDeNegocioED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsBancoED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsPaisED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsCIIUED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMonedaED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMonedaLocalED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Inventario.clsCategoriaED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Vehiculo.clsMarcaED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Vehiculo.clsModeloED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsUnidadDeVentaED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsUrbanizacionZPED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsZonaCobranzaED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsMaquinaFiscalED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsPropAnalisisVencED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Inventario.clsTallaED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Inventario.clsColorED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsNotaFinalED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsTipoProveedorED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Banco.clsBeneficiarioED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Banco.clsSolicitudesDePagoED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Banco.clsRenglonSolicitudesDePagoED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.DispositivosExternos.clsBalanzaED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Integracion.clsIntegracionSawED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Contabilizacion.clsReglasDeContabilizacionED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsFormaDelCobroED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Vehiculo.clsVehiculoED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Cliente.clsClienteED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Inventario.clsAlmacenED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMunicipioED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMunicipioCiudadED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMonedaED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasLey.clsValorUTED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Banco.clsCuentaBancariaED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.Impuesto.clsClasificadorActividadEconomicaED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.Impuesto.clsFormatosImpMunicipalesED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.SttDef.clsSettDefinitionED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.SttDef.clsSettValueByCompanyED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Vendedor.clsVendedorED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Banco.clsConceptoBancarioED().InstalarVistasYSps();
            //vResult = vResult && new Galac.Adm.Dal.CajaChica.clsAnticipoED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Banco.clsMovimientoBancarioED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.CajaChica.clsRendicionED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.CajaChica.clsAlicuotaIVAED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.CajaChica.clsCxPED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.CajaChica.clsRenglonImpuestoMunicipalRetED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Banco.clsMovimientoBancarioED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasLey.clsTarifaN2ED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasLey.clsTablaRetencionED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsProveedorED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Inventario.clsArticuloInventarioED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Venta.clsFacturaRapidaED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Venta.clsRenglonCobroDeFacturaED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.CAnticipo.clsAnticipoCobradoED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.CAnticipo.clsAnticipoED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.Impuesto.clsTablaDetraccionED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.Impuesto.clsAlicuotaImpuestoEspecialED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsOrdenDeCompraED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsCompraED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsCargaInicialED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.Impuesto.clsArancelesED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsImpuestoBancarioED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsCondicionesDePagoED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Venta.clsCajaED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Venta.clsCajaAperturaED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsCambioED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.GestionProduccion.clsListaDeMaterialesED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionED().InstalarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Inventario.clsNotaDeEntradaSalidaED().InstalarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Venta.clsContratoED().InstalarVistasYSps();
			vResult = vResult && new Galac.Adm.Dal.Banco.clsTransferenciaEntreCuentasBancariasED().InstalarVistasYSps();
            vResult = vResult && CrearVistasDeCompatibilidad();
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = true;
            vResult = vResult && BorrarVistasDeCompatibilidad();
            vResult = vResult && BorrarVistasDeContabilidad();
            //ORGANICEN EL ORDEN DE ELIMINACION DE VISTAS Y SPS POR DEPENDENCIAS, ESTA DESORDENADO
            vResult = vResult && new Galac.Adm.Dal.Venta.clsContratoED().BorrarVistasYSps();
			vResult = vResult && new Galac.Adm.Dal.Banco.clsTransferenciaEntreCuentasBancariasED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Venta.clsCajaED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Venta.clsCajaAperturaED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Inventario.clsNotaDeEntradaSalidaED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.GestionProduccion.clsListaDeMaterialesED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsConveniosSunatED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsPaisED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsDepartamentoED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsProvinciaED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsUbigeoED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.Impuesto.clsArancelesED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsOrdenDeCompraED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsCompraED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsCargaInicialED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.Impuesto.clsAlicuotaImpuestoEspecialED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.Impuesto.clsTablaDetraccionED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.CAnticipo.clsAnticipoCobradoED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.CAnticipo.clsAnticipoED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Venta.clsRenglonCobroDeFacturaED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Venta.clsFacturaRapidaED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Inventario.clsArticuloInventarioED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsCondicionesDePagoED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.GestionCompras.clsProveedorED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasLey.clsTablaRetencionED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasLey.clsTarifaN2ED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Banco.clsMovimientoBancarioED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.CajaChica.clsRenglonImpuestoMunicipalRetED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.CajaChica.clsCxPED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.CajaChica.clsAlicuotaIVAED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.CajaChica.clsRendicionED().BorrarVistasYSps();
            //vResult = vResult && new Galac.Adm.Dal.CajaChica.clsAnticipoED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Banco.clsConceptoBancarioED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Vendedor.clsVendedorED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.SttDef.clsSettValueByCompanyED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.SttDef.clsSettDefinitionED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.Impuesto.clsFormatosImpMunicipalesED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.Impuesto.clsClasificadorActividadEconomicaED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Banco.clsCuentaBancariaED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasLey.clsValorUTED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMonedaED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMunicipioCiudadED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMunicipioED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Inventario.clsAlmacenED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Cliente.clsClienteED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Vehiculo.clsVehiculoED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsFormaDelCobroED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Contabilizacion.clsReglasDeContabilizacionED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Integracion.clsIntegracionSawED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.DispositivosExternos.clsBalanzaED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Banco.clsSolicitudesDePagoED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Banco.clsRenglonSolicitudesDePagoED().BorrarVistasYSps();
            vResult = vResult && new Galac.Adm.Dal.Banco.clsBeneficiarioED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsTipoProveedorED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsNotaFinalED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Inventario.clsColorED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Inventario.clsTallaED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsPropAnalisisVencED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsMaquinaFiscalED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsZonaCobranzaED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsUrbanizacionZPED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsUnidadDeVentaED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Vehiculo.clsModeloED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Vehiculo.clsMarcaED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Inventario.clsCategoriaED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsCIIUED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsPaisED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsBancoED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsSectorDeNegocioED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsCiudadED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMonedaLocalED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMonedaED().BorrarVistasYSps();
            vResult = vResult && new LibGalac.Aos.Dal.Settings.LibDiagnosticSttED().BorrarVistasYSps();
            vResult = vResult && new LibGalac.Aos.Dal.PASOnLine.LibDiagnosticED().BorrarVistasYSps();
            vResult = vResult && new Galac.Saw.Dal.Tablas.clsImpuestoBancarioED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsCambioED().BorrarVistasYSps();
            new LibDbo().Drop("dbo.Gf_UsaDataTemporalParaLibros", eDboType.Funcion);
            return vResult;
        }

        private bool BorrarVistasDeCompatibilidad() {
            bool vResult = true;
            LibDbo insDbo = new LibDbo();
            vResult = vResult && insDbo.Drop("dbo.RenglonCompraXSerial", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.RenglonCompra", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.Compra", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.Proveedor", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.TablaRetencion", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.TarifaN2", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.ConceptoBancario", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.CuentaBancaria", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.Almacen", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.Vehiculo", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.FormaDelCobro", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.ReglasDeContabilizacion", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.TipoProveedor", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.NotaFinal", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.Color", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.Talla", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.PropAnalisisVenc", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.MaquinaFiscal", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.ZonaCobranza", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.UrbanizacionZP", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.UnidadDeVenta", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.Categoria", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.Ciiu2004", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.Ciiu", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.Pais", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.MonedaLocal", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.Banco", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.SectorDeNegocio", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.Ciudad", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.Usuario", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.Auxiliar", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.CentroDeCostos", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.TipoDeComprobante", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.ImpTransacBancarias", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.Compra", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.RenglonCompra", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.RenglonCompraXSerial", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.CajaApertura", eDboType.Vista);
            vResult = vResult && insDbo.Drop("dbo.Caja", eDboType.Vista);
            return vResult;
        }

        private bool CrearVistasDeCompatibilidad() {
            bool vResult = true;
            vResult = vResult && new LibGalac.Aos.Dal.DDL.LibCreateDb().CreateViewDboUsuario();
            vResult = vResult && clsCompatViews.CrearVistaDboCiudad();
            vResult = vResult && clsCompatViews.CrearVistaDboSectorDeNegocio();
            vResult = vResult && clsCompatViews.CrearVistaDboBanco();
            vResult = vResult && clsCompatViews.CrearVistaDboPais();
            vResult = vResult && clsCompatViews.CrearVistaDboCiiu();
            vResult = vResult && clsCompatViews.CrearVistaDboCategoria();
            vResult = vResult && clsCompatViews.CrearVistaDboUnidadDeVenta();
            vResult = vResult && clsCompatViews.CrearVistaDboUrbanizacionZP();
            vResult = vResult && clsCompatViews.CrearVistaDboZonaCobranza();
            vResult = vResult && clsCompatViews.CrearVistaDboMaquinaFiscal();
            vResult = vResult && clsCompatViews.CrearVistaDboPropAnalisisVenc();
            vResult = vResult && clsCompatViews.CrearVistaDboColor();
            vResult = vResult && clsCompatViews.CrearVistaDboTalla();
            vResult = vResult && clsCompatViews.CrearVistaDboNotaFinal();
            vResult = vResult && clsCompatViews.CrearVistaDboTipoProveedor();
            vResult = vResult && clsCompatViews.CrearVistaDboReglasDeContabilizacion();
            vResult = vResult && clsCompatViews.CrearVistaDboFormaDelCobro();
            vResult = vResult && clsCompatViews.CrearVistaDboVehiculo();
            vResult = vResult && clsCompatViews.CrearVistaDboAlmacen();
            vResult = vResult && clsCompatViews.CrearVistaDboCuentaBancaria();
            vResult = vResult && clsCompatViews.CrearVistaDboConceptoBancario();
            vResult = vResult && clsCompatViews.CrearVistaDboTarifaN2();
            vResult = vResult && clsCompatViews.CrearVistaDboTablaRetencion();
            vResult = vResult && clsCompatViews.CrearVistaDboProveedor();
            vResult = vResult && clsCompatViews.CrearVistaDboCierreCostoDelPeriodo();
            vResult = vResult && new Galac.Comun.Dal.TablasGen.clsMonedaLocalMD().CrearVistaDboMonedaLocal();
            vResult = vResult && new Galac.Contab.Dal.WinCont.clsAuxiliarMD().CrearVistaDboAuxiliar();
            vResult = vResult && new Galac.Contab.Dal.WinCont.clsCentroDeCostosMD().CrearVistaDboCentroDeCostos();
            vResult = vResult && new Galac.Contab.Dal.Tablas.clsTipoDeComprobanteMD().CrearVistaDboTipoDeComprobante();
            vResult = vResult && clsCompatViews.CrearVistaDboCompra();
            vResult = vResult && clsCompatViews.CrearVistaDboImpTrasnBancarias();
            vResult = vResult && clsCompatViews.CrearVistaDboCaja();
            return vResult;
        }

        private bool BorrarVistasDeContabilidad() {
            bool vResult = true;
            vResult = vResult && new Galac.Comun.Dal.LeyCosto.clsCriterioDeDistribucionED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.LeyCosto.clsElementoDelCostoED().BorrarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.LeyCosto.clsProductoED().BorrarVistasYSps();
            vResult = vResult && new Galac.Contab.Dal.WinCont.clsAuxiliarED().BorrarVistasYSps();
            vResult = vResult && new Galac.Contab.Dal.WinCont.clsCentroDeCostosED().BorrarVistasYSps();
            vResult = vResult && new Galac.Contab.Dal.WinCont.clsCuentaED().BorrarVistasYSps();
            vResult = vResult && new Galac.Contab.Dal.WinCont.clsGrupoDeActivosED().BorrarVistasYSps();
            vResult = vResult && new Galac.Ent.Dal.Empresarial.clsCompaniaBaseED().BorrarVistasYSps();
            vResult = vResult && new Galac.Contab.Dal.Tablas.clsTipoDeComprobanteED().BorrarVistasYSps();
            vResult = vResult && new Galac.Contab.Dal.WinCont.clsParametrosConciliacionED().BorrarVistasYSps();
            vResult = vResult && new Galac.Contab.Dal.WinCont.clsParametrosGenED().BorrarVistasYSps();
            return vResult;
        }

        private bool CrearVistasDeContabilidad() {
            bool vResult = true;
            vResult = vResult && new Galac.Contab.Dal.WinCont.clsParametrosGenED().InstalarVistasYSps();
            vResult = vResult && new Galac.Contab.Dal.WinCont.clsParametrosConciliacionED().InstalarVistasYSps();
            vResult = vResult && new Galac.Contab.Dal.Tablas.clsTipoDeComprobanteED().InstalarVistasYSps();
            vResult = vResult && new Galac.Ent.Dal.Empresarial.clsCompaniaBaseED().InstalarVistasYSps();
            vResult = vResult && new Galac.Contab.Dal.WinCont.clsGrupoDeActivosED().InstalarVistasYSps();
            vResult = vResult && new Galac.Contab.Dal.WinCont.clsCuentaED().InstalarVistasYSps();
            vResult = vResult && new Galac.Contab.Dal.WinCont.clsCentroDeCostosED().InstalarVistasYSps();
            vResult = vResult && new Galac.Contab.Dal.WinCont.clsAuxiliarED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.LeyCosto.clsProductoED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.LeyCosto.clsElementoDelCostoED().InstalarVistasYSps();
            vResult = vResult && new Galac.Comun.Dal.LeyCosto.clsCriterioDeDistribucionED().InstalarVistasYSps();
            LibViews insVistas = new LibViews(Galac.Contab.Ccl.WinCont.clsCkn.ConfigKeyForDbService);
            vResult = vResult && insVistas.Create("Contab" + ".Gv_EnumComprobanteGeneradoPor", LibTpvCreator.SqlViewStandardEnum(typeof(eComprobanteGeneradoPor), InsSql), true, true);
            return vResult;
        }
        #endregion Vistas

        #region Verificacion para Actualizar

        private bool HasToUpgradeToVersion(string valVersion) {
            bool vResult;
            vResult = false;
            if (LibGalac.Aos.Base.LibString.S1IsLessThanS2(CurrentVersionInDb, valVersion)) {
                vResult = true;
            }
            return vResult;
        }


        #endregion Verificacion para Actualizar

        #region Consistencia entre BD Reestructurada y Creada

        private bool CreateLostFields() {
            bool vResult = true;
            //StartConnectionNoTransaction();
            #region Campos extraviados de la version 9_04
            if (!ColumnExists("Cotizacion", "promocion")) {
                vResult = vResult && AddColumnString("Cotizacion", "promocion", 1, "promocion NOT NULL", "");
            }
            if (!ColumnExists("Cotizacion", "Confirmada")) {
                vResult = vResult && AddColumnString("Cotizacion", "Confirmada", 1, "Confirmada NOT NULL", "");
            }
            if (!ColumnExists("Cotizacion", "ModificadoPorOperador")) {
                vResult = vResult && AddColumnString("Cotizacion", "ModificadoPorOperador", 10, "", "");
            }
            if (!ColumnExists("Cotizacion", "Seguimiento")) {
                vResult = vResult && AddColumnString("Cotizacion", "Seguimiento", 255, "", "");
            }
            if (!ColumnExists("Cotizacion", "FechaDeProximaLlamada")) {
                vResult = vResult && AddColumnDate("Cotizacion", "FechaDeProximaLlamada", "");
            }
            if (!ColumnExists("Cotizacion", "EstadoDeLaCotizacion")) {
                vResult = vResult && AddColumnString("Cotizacion", "EstadoDeLaCotizacion", 1, "", "");
            }
            if (!ColumnExists("Cotizacion", "FormaDeEnvio")) {
                vResult = vResult && AddColumnString("Cotizacion", "FormaDeEnvio", 1, "", "");
            }
            if (!ColumnExists("Cotizacion", "FechaDeUltimaEmisionDeFax")) {
                vResult = vResult && AddColumnDate("Cotizacion", "FechaDeUltimaEmisionDeFax", "");
            }
            if (!ColumnExists("Cotizacion", "NumeroDeVecesFaxeado")) {
                vResult = vResult && AddColumnNumeric("Cotizacion", "NumeroDeVecesFaxeado", 20, 0, "", 0);
            }
            if (!ColumnExists("Cotizacion", "NumeroParaResumen")) {
                vResult = vResult && AddColumnNumeric("Cotizacion", "NumeroParaResumen", 12, 0, "", 0);
            }
            if (!ColumnExists("cliente", "AQueSeDedicaElCliente")) {
                vResult = vResult && AddColumnString("cliente", "AQueSeDedicaElCliente", 100, "", "");
            }
            if (!ColumnExists("cliente", "InfoGalac")) {
                vResult = vResult && AddColumnString("cliente", "InfoGalac", 1, "", "");
            }
            #endregion Campos extraviados de la version 9_04
            //DisposeConnectionNoTransaction();
            return vResult;
        }

        #endregion Consistencia entre BD Reestructurada y Creada

        //public bool UpdateToVersion(string valVersionToUpdate) {
        //    try{
        //        bool vResult = true;
        //        if (HasToUpdate("5.53", valVersionToUpdate) && vResult) {
        //            vResult = vResult && new clsVersion5_53(_CurrentDataBaseName).UpdateToVersion();
        //        }
        //        if (HasToUpdate("5.54", valVersionToUpdate)  && vResult){
        //            vResult = vResult && new clsVersion5_54(_CurrentDataBaseName).UpdateToVersion();                
        //        }

        //        vResult = vResult && new clsVersionTemporalNoOficial(_CurrentDataBaseName).UpdateToVersion();
        //        vResult = vResult && CreateLostFields();
        //        return vResult;
        //    }catch(Exception vEx){
        //        throw vEx;
        //    }
        //}


        public bool UpgradeToNewVersion(string valCurrentVersionInDb, string valDestinyVersion) {
            bool vResult = true;
            try {
                CurrentVersionInDb = valCurrentVersionInDb;
                if (HasToUpgradeToVersion("5.53")) {
                    vResult = vResult && new clsVersion5_53(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.54")) {
                    vResult = vResult && new clsVersion5_54(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.55")) {
                    vResult = vResult && new clsVersion5_55(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.56")) {
                    vResult = vResult && new clsVersion5_56(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.57")) {
                    vResult = vResult && new clsVersion5_57(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.58")) {
                    vResult = vResult && new clsVersion5_58(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.59")) {
                    vResult = vResult && new clsVersion5_59(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.60")) {
                    vResult = vResult && new clsVersion5_60(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.61")) {
                    vResult = vResult && new clsVersion5_61(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.62")) {
                    vResult = vResult && new clsVersion5_62(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.63")) {
                    vResult = vResult && new clsVersion5_63(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.64")) {
                    vResult = vResult && new clsVersion5_64(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.65")) {
                    vResult = vResult && new clsVersion5_65(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.66")) {
                    vResult = vResult && new clsVersion5_66(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.67")) {
                    vResult = vResult && new clsVersion5_67(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.68")) {
                    vResult = vResult && new clsVersion5_68(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.69")) {
                    vResult = vResult && new clsVersion5_69(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.70")) {
                    vResult = vResult && new clsVersion5_70(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.71")) {
                    vResult = vResult && new clsVersion5_71(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.72")) {
                    vResult = vResult && new clsVersion5_72(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.73")) {
                    vResult = vResult && new clsVersion5_73(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.74")) {
                    vResult = vResult && new clsVersion5_74(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.75")) {
                    vResult = vResult && new clsVersion5_75(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.76")) {
                    vResult = vResult && new clsVersion5_76(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.77")) {
                    vResult = vResult && new clsVersion5_77(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.78")) {
                    vResult = vResult && new clsVersion5_78(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.79")) {
                    vResult = vResult && new clsVersion5_79(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.80")) {
                    vResult = vResult && new clsVersion5_80(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.81")) {
                    vResult = vResult && new clsVersion5_81(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.82")) {
                    vResult = vResult && new clsVersion5_82(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.83")) {
                    vResult = vResult && new clsVersion5_83(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.84")) {
                    vResult = vResult && new clsVersion5_84(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.85")) {
                    vResult = vResult && new clsVersion5_85(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.86")) {
                    vResult = vResult && new clsVersion5_86(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.87")) {
                    vResult = vResult && new clsVersion5_87(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.88")) {
                    vResult = vResult && new clsVersion5_88(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.89")) { // usada en Peru, SAW 11.5, en espera de merge
                    vResult = vResult && new clsVersion5_89(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.90")) {
                    vResult = vResult && new clsVersion5_90(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.91")) {
                    vResult = vResult && new clsVersion5_91(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.92")) {
                    vResult = vResult && new clsVersion5_92(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.93")) {
                    vResult = vResult && new clsVersion5_93(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.94")) {
                    vResult = vResult && new clsVersion5_94(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.95")) {
                    vResult = vResult && new clsVersion5_95(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.96")) {
                    vResult = vResult && new clsVersion5_96(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.97")) {
                    vResult = vResult && new clsVersion5_97(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.98")) {
                    vResult = vResult && new clsVersion5_98(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("5.99")) {
                    vResult = vResult && new clsVersion5_99(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.00")) {
                    vResult = vResult && new clsVersion6_00(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.01")) {
                    vResult = vResult && new clsVersion6_01(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.02")) {
                    vResult = vResult && new clsVersion6_02(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.03")) {
                    vResult = vResult && new clsVersion6_03(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.04")) {
                    vResult = vResult && new clsVersion6_04(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.05")) {
                    vResult = vResult && new clsVersion6_05(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.06")) {
                    vResult = vResult && new clsVersion6_06(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.07")) {
                    vResult = vResult && new clsVersion6_07(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.08")) {
                    vResult = vResult && new clsVersion6_08(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.09")) {
                    vResult = vResult && new clsVersion6_09(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.10")) {
                    vResult = vResult && new clsVersion6_10(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.11")) {
                    vResult = vResult && new clsVersion6_11(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.12")) {
                    vResult = vResult && new clsVersion6_12(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.13")) {
                    vResult = vResult && new clsVersion6_13(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.14")) {
                    vResult = vResult && new clsVersion6_14(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.16")) {
                    vResult = vResult && new clsVersion6_16(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.17")) {
                    vResult = vResult && new clsVersion6_17(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.18")) {
                    vResult = vResult && new clsVersion6_18(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.19")) {
                    vResult = vResult && new clsVersion6_19(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.50")) {
                    vResult = vResult && new clsVersion6_50(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.51")) {
                    vResult = vResult && new clsVersion6_51(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.52")) {
                    vResult = vResult && new clsVersion6_52(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.54")) {
                    vResult = vResult && new clsVersion6_54(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.55")) {
                    vResult = vResult && new clsVersion6_55(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.56")) {
                    vResult = vResult && new clsVersion6_56(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.57")) {
                    vResult = vResult && new clsVersion6_57(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.58")) {
                    vResult = vResult && new clsVersion6_58(_CurrentDataBaseName).UpdateToVersion();
                }
                if (HasToUpgradeToVersion("6.59")) {
                    vResult = vResult && new clsVersion6_59(_CurrentDataBaseName).UpdateToVersion();
                }
                vResult = vResult && new clsVersionTemporalNoOficial(_CurrentDataBaseName).UpdateToVersion();
                //vResult = vResult && CreateLostFields();
                return vResult;
            } catch (Exception vEx) {
                throw vEx;
            }
        }

        string ILibRestructureDb.CurrentVersionInDb {
            get { return CurrentVersionInDb; }
        }

    }
}