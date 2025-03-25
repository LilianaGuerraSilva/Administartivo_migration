using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;
using LibGalac.Aos.Base;
using LibGalac.Aos.Uil.Usal;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Dal.PASOnLine;
using Galac.Contab.Ccl.Tablas;
using Galac.Contab.Brl.Tablas;
using Galac.Saw.Wrp.DDL;
using Galac.Saw;

#if IsExeBsF
namespace Galac.SawBsF.Wrp.DDL {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.DDL {
#else
namespace Galac.Saw.Wrp.DDL {
#endif
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpDbCreator: System.EnterpriseServices.ServicedComponent, IwrpVBDDLSaw {
        #region Miembros de IwrpDbCreator

        void IwrpVBDDLSaw.InitializeComponent(string vfwPath) {
            /* 17/10/2016 debido a que el proceso de Creacion de BD no usa formularios de interaccion con el usuario
             * No se requiere agregar la instruccion LibWrpHelper.ConfigureRuntimeContext 
             */
            AddConfiguration(vfwPath);
            clsNivelesDeSeguridad.DefinirPlantilla();
            LibSessionParameters.PlatformArchitecture = 1;
        }

        void IwrpVBDDLSaw.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
            LibGalac.Aos.DefGen.LibDefGen.InitializeProgramInfo(vfwProgramInitials, vfwProgramVersion, vfwProgramVersion, Convert.ToDateTime(vfwStrDateOfVersion), vfwStrHourOfVersion, "", vfwCountry, Convert.ToInt32(vfwCMTO));

        }

        void IwrpVBDDLSaw.CreateTables(string vfwListDeTablasACrear) {
            bool vSuccess = false;
            string vCurrent = "";
            string[] vArrayDeTablas = LibString.Split(vfwListDeTablasACrear, ',');
            if (vArrayDeTablas != null) {
                foreach (string vNombreTabla in vArrayDeTablas) {
                    vCurrent = vNombreTabla;
                    if (!CrearTabla(vNombreTabla)) {
                        vSuccess = false;
                        break;
                    } else {
                        vSuccess = true;
                    }
                }

                if (!vSuccess) {
                    throw new ApplicationException("Ocurrió un error creando la Tabla '" + vCurrent + "' o uno de los objetos asociados.");
                }
            }
        }

        void IwrpVBDDLSaw.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(" - Inicialización", vEx);
            }
        }

        void IwrpVBDDLSaw.CreateViewsAndSPs(string vfwListOfModulesToCreateDbObjects) {
            bool vSuccess = false;
            string vCurrent = "";
            string[] vModulesArray = LibString.Split(vfwListOfModulesToCreateDbObjects, ',');
            if (vModulesArray != null) {
                if (!CreateViewAndSP(vModulesArray)) {
                    vSuccess = false;
                } else {
                    vSuccess = true;
                }
            }
            if (!vSuccess) {
                throw new ApplicationException("Ocurrió un error creando la Vista o Procedimiento Almacenado  '" + vCurrent + "' o uno de los objetos asociados.");
            }
        }

        //CreateViewAndSP(TransformArray(vfwListOfModulesToCreateDbObjects, ','));


        #endregion

        private void AddConfiguration(string valPath) {
            clsLibSawDDL.SetAppConfigToCurrentDomain(valPath);
        }
        private bool CrearTabla(string valTableName) {
            bool vResult = false;
            if (valTableName.Equals("Usuario", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new LibGalac.Aos.Dal.DDL.LibCreateDb(LibSecurityLevels.Levels).CreateGUser(true);
            } else if (valTableName.Equals("Ciudad", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearCiudad();
            } else if (valTableName.Equals("SectorDeNegocio", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearSectorDeNegocio();
            } else if (valTableName.Equals("Lib", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new LibGalac.Aos.Dal.DDL.LibCreateDb(null).CreateLibObjects();
                vResult = new LibGalac.Aos.Dal.DDL.LibCreateDb(null).CreateMonetaryReconversion(true);
            } else if (valTableName.Equals("Banco", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearBanco();
            } else if (valTableName.Equals("Pais", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearPais();
            } else if (valTableName.Equals("Ciiu", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearCiiu();
            } else if (valTableName.Equals("Categoria", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearCategoria();
            } else if (valTableName.Equals("Marca", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearMarca();
            } else if (valTableName.Equals("Modelo", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearModelo();
            } else if (valTableName.Equals("UnidadDeVenta", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearUnidadDeVenta();
            } else if (valTableName.Equals("UrbanizacionZP", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearUrbanizacionZP();
            } else if (valTableName.Equals("ZonaCobranza", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearZonaCobranza();
            } else if (valTableName.Equals("MaquinaFiscal", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearMaquinaFiscal();
            } else if (valTableName.Equals("PropAnalisisVenc", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearPropAnalisisVenc();
            } else if (valTableName.Equals("Talla", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearTalla();
            } else if (valTableName.Equals("Color", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearColor();
            } else if (valTableName.Equals("notaFinal", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearNotaFinal();
            } else if (valTableName.Equals("tipoProveedor", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearTipoProveedor();
            } else if (valTableName.Equals("Beneficiario", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearBeneficiario();
            } else if (valTableName.Equals("SolicitudesDePago", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearSolicitudesDePago();
            } else if (valTableName.Equals("Integracion", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearIntegracion();
            } else if (valTableName.Equals("ReglasDeContabilizacion", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearReglasDeContabilizacion();
            } else if (valTableName.Equals("FormaDelCobro", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearFormaDelCobro();
            } else if (valTableName.Equals("Vehiculo", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearVehiculo();
            } else if (valTableName.Equals("PrnStt", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new LibGalac.Aos.Dal.DDL.LibCreateDb(null).CreatePrnStt(true);
            } else if (valTableName.Equals("SendMailStt", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new LibGalac.Aos.Dal.DDL.LibCreateDb(null).CreateSendMailStt(true);
            } else if (valTableName.Equals("Auxiliar", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearAuxiliar();
            } else if (valTableName.Equals("ParametrosContables", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearParametrosConciliacion();
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearParametrosGen();
            } else if (valTableName.Equals("Almacen", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearAlmacen();
            } else if (valTableName.Equals("Municipio", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearMunicipio();
            } else if (valTableName.Equals("MunicipioCiudad", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearMunicipioCiudad();
            } else if (valTableName.Equals("ValorUT", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearValorUT();
            } else if (valTableName.Equals("Moneda", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearMoneda();
            } else if (valTableName.Equals("CuentaBancaria", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearCuentaBancaria();
            } else if (valTableName.Equals("ClasificadorActividadEconomica", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearClasificadorActividadEconomica();
            } else if (valTableName.Equals("FormatosImpMunicipales", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearFormatosImpMunicipales();
            } else if (valTableName.Equals("SettDefinition", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearSettDefinition();
            } else if (valTableName.Equals("SettValueByCompany", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearSettValueByCompany();
            } else if (valTableName.Equals("Rendicion", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearRendicion();
            } else if (valTableName.Equals("ConceptoBancario", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearConceptoBancario();
            } else if (valTableName.Equals("Producto", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearProducto();
            } else if (valTableName.Equals("CriterioDeDistribucion", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearCriterioDeDistribucion();
            } else if (valTableName.Equals("TarifaN2", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearTarifaN2();
            } else if (valTableName.Equals("TablaRetencion", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearTablaRetencion();
            } else if (valTableName.Equals("Proveedor", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearProveedor();
            } else if (valTableName.Equals("MonedaLocal", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearMonedaLocal();
            } else if (valTableName.Equals("TipoDeComprobante", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearTipoDeComprobante();
                if (vResult) {
                    ITipoDeComprobantePdn vTipoDeComprobanteNav = new clsTipoDeComprobanteNav();
                    vResult = vTipoDeComprobanteNav.InsertDefaultRecords();
                }
            } else if (valTableName.Equals("CentroDeCostos", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearCentroDeCostos();
            } else if (valTableName.Equals("Moneda", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearMoneda();
            } else if (valTableName.Equals("ElementoDelCosto", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearElementoDelCosto();
            } else if (valTableName.Equals("ParametersLib", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new LibGalac.Aos.Dal.DDL.LibCreateDb(null).CreateParametersLib(true);
            } else if (valTableName.Equals("ContactInformation", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new LibGalac.Aos.Dal.DDL.LibCreateDb(null).CreateContactInformation(true);
            } else if (valTableName.Equals("NotificationPAS", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new LibGalac.Aos.Dal.DDL.LibCreateDb(null).CreateNotificationPAS(true);
            } else if (valTableName.Equals("TablaDetraccion", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearTablaDetraccion();
            } else if (valTableName.Equals("AlicuotaImpuestoEspecial", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearAlicuotaImpuestoEspecial();
            } else if (valTableName.Equals("Diagnostic", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new LibGalac.Aos.Dal.PASOnLine.LibDiagnosticED().InstalarTabla();
            } else if (valTableName.Equals("DiagnosticStt", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new LibGalac.Aos.Dal.Settings.LibDiagnosticSttED().InstalarTabla();
            } else if (valTableName.Equals("Compra", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearCompra();
            } else if (valTableName.Equals("CargaInicial", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearCargaInicial();
            } else if (valTableName.Equals("Aranceles", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearAranceles();
            } else if (valTableName.Equals("LineaDeProducto", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearLineaDeProducto();
            } else if (valTableName.Equals("Balanza", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearBalanza();
            } else if (valTableName.Equals("ImpuestoBancario", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearImpuestoBancario();
            } else if (valTableName.Equals("CondicionesDePago", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearCondicionesDePago();
            } else if (valTableName.Equals("WcontVbTablas", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearWcontVbTablas();
            } else if (valTableName.Equals("Cambio", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearCambio();
            } else if (valTableName.Equals("ListaDeMateriales", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearListaDeMateriales();
            } else if (valTableName.Equals("OrdenDeProduccion", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearOrdenDeProduccion();
            } else if (valTableName.Equals("NotaDeEntradaSalida", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearNotaDeEntregaSalida();
            } else if (valTableName.Equals("CajaRegistradora", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearCajaRegistradora();
            } else if (valTableName.Equals("Vendedor", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearVendedor();
            } else if (valTableName.Equals("Escalada", StringComparison.CurrentCultureIgnoreCase)) { 
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearEscalada();
            } else if (valTableName.Equals("TransferenciaEntreCuentasBancarias", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearTransferenciaEntreCuentasBancarias();
            } else if (valTableName.Equals("RutaDeComercializacion", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearRutaDeComercializacion();
            } else if (valTableName.Equals("GVentasDefiniciones", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearGVentasDefiniciones();
            } else if (valTableName.Equals("otrosCargosDeFactura", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearOtrosCargosDeFactura();
            } else if (valTableName.Equals("LoteDeInventario", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearLoteDeInventario();
            } else if (valTableName.Equals("AuditoriaConfiguracion", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearAuditoriaConfiguracion();
            } else if (valTableName.Equals("ExistenciaPorAlmacenDetLoteInv", StringComparison.CurrentCultureIgnoreCase)) {
                vResult = new Galac.Saw.DDL.clsCrearDatabase().CrearExistenciaPorAlmacenDetLoteInv ();
            } else {
                throw new NotImplementedException("Aún no ha sido implementada la creación de la Tabla " + valTableName + " y sus objetos asociados.");
            }
            return vResult;
        }

        private bool CreateViewAndSP(string[] valModulos) {
            bool result = false;
            if (valModulos != null) {
                Galac.Saw.DDL.clsCrearDatabase insBdd = new Galac.Saw.DDL.clsCrearDatabase();
                result = insBdd.CrearVistasYProcedimientos(valModulos);
            }
            return result;
        }
    }//End of class
}//End of namespace
