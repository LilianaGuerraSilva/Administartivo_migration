using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal;
namespace Galac.Saw.DbMigrator {
    public class clsCrearBDD : Saw.DDL.clsCrearDatabase {
        public clsCrearBDD() {
            CreateCompatibilityView = false;
        }

        public bool CreateLibObjects() {
            LibGalac.Aos.Dal.LibTpvCreator insDbLibObjects = new LibGalac.Aos.Dal.LibTpvCreator();
            bool vResult = true;
            vResult = vResult && insDbLibObjects.CreateEnumBase(false);
            if (insDbLibObjects.IsAnyProcMissing()) {
                vResult = vResult && insDbLibObjects.CreateProcedures();
                vResult = vResult && insDbLibObjects.CreateViews();
            }
            return vResult;
        }
        public bool CrearTablas(List<CustomRole> valAppPermissions, string[] valModulos) {
            bool vResult = true;
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Usuario")) {
                vResult = vResult && new LibGalac.Aos.Dal.DDL.LibCreateDb(valAppPermissions).CreateGUser(CreateCompatibilityView);
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Ciudad")) {
                vResult = vResult && CrearCiudad();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "SectorDeNegocio")) {
                vResult = vResult && CrearSectorDeNegocio();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Banco")) {
                vResult = vResult && CrearBanco();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Pais")) {
                vResult = vResult && CrearPais();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Ciiu")) {
                vResult = vResult && CrearCiiu();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Categoria")) {
                vResult = vResult && CrearCategoria();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "UnidadDeVenta")) {
                vResult = vResult && CrearUnidadDeVenta();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "UrbanizacionZP")) {
                vResult = vResult && CrearUrbanizacionZP();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "ZonaCobranza")) {
                vResult = vResult && CrearZonaCobranza();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "MaquinaFiscal")) {
                vResult = vResult && CrearMaquinaFiscal();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "PropAnalisisVenc")) {
                vResult = vResult && CrearPropAnalisisVenc();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Talla")) {
                vResult = vResult && CrearTalla();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Color")) {
                vResult = vResult && CrearColor();
            }

            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "NotaFinal")) {
                vResult = vResult && CrearNotaFinal();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "TipoProveedor")) {
                vResult = vResult && CrearTipoProveedor();
            }

            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Beneficiario")) {
                vResult = vResult && CrearBeneficiario();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "SolicitudesDePago")) {
                vResult = vResult && CrearSolicitudesDePago();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Integracion")) {
                vResult = vResult && CrearIntegracion();
            }

            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "ReglasDeContabilizacion")) {
                vResult = vResult && CrearReglasDeContabilizacion();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "FormaDelCobro")) {
                vResult = vResult && CrearFormaDelCobro();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Vehiculo")) {
                vResult = vResult && CrearVehiculo();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "PrnStt")) {
                vResult = vResult && new LibGalac.Aos.Dal.DDL.LibCreateDb().CreatePrnStt(false);
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Auxiliar")) {
                vResult = vResult && new Galac.Contab.Dal.WinCont.clsAuxiliarED().InstalarTabla();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "ParametrosContables")) {
                vResult = vResult && CrearParametrosConciliacion();
                vResult = vResult && CrearParametrosGen();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Almacen")) {
                vResult = vResult && CrearAlmacen();
            }

            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "ValorUT")) {
                vResult = vResult && CrearValorUT();
            }

            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Municipio")) {
                vResult = vResult && CrearMunicipio();
            }

            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "MunicipioCiudad")) {
                vResult = vResult &&  CrearMunicipioCiudad();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "CuentaBancaria")) {
                vResult = vResult && CrearCuentaBancaria();
            }            

            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "ClasificadorActividadEconomica")) {
                vResult = vResult && CrearClasificadorActividadEconomica();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "FormatosImpMunicipales")) {
                vResult = vResult && CrearFormatosImpMunicipales();
            }
 
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "SettDefinition")) {
                vResult = vResult && CrearSettDefinition();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "SettValueByCompany")) {
                vResult = vResult && CrearSettValueByCompany();
            }
			 if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "ConceptoBancario")) {
                vResult = vResult && CrearConceptoBancario();
            }
			if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "TarifaN2")) {
                 vResult = vResult && CrearTarifaN2();
             } 
             if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "TablaRetencion")) {
                 vResult = vResult && CrearTablaRetencion();
             }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Proveedor")) {
                vResult = vResult && CrearProveedor();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(valModulos, "Vendedor")) {
                vResult = vResult && CrearVendedor();
            }
            return vResult;
        }


    }
}
