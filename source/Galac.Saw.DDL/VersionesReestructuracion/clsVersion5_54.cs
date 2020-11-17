using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_54 : clsVersionARestructurar{
        public clsVersion5_54(string valCurrentDataBaseName):base(valCurrentDataBaseName){
            _VersionDataBase="5.54";
        }

        public override bool UpdateToVersion(){
            StartConnectionNoTransaction();
            CrearCampoRucProveedor();
            AgregarNuevoParametroPrimerNumeroComprobante();
            LimpiarComprobanteImpMunicipal();
            ActualizarFormatoImpMunicipales();
			UpgradeToVersion2_22FromWinCONT();
            UpgradeDBVersion();
            DisposeConnectionNoTransaction();
            return true;
        }

        private bool CrearCampoRucProveedor() { 
            if(!ColumnExists ("Adm.Proveedor", "NumeroRUC")){
                AddColumnString("Adm.Proveedor", "NumeroRUC", 20, "", ""); 
            }
            return true; 
        }

        private bool AgregarNuevoParametroPrimerNumeroComprobante() {
            return AgregarNuevoParametro("PrimerNumeroComprobanteRetImpuestoMunicipal","CxP/Compras",6,"6.2.- CxP / Proveedor / Pagos",2,"",'2',"",'N',"0");          
        }

        private void LimpiarComprobanteImpMunicipal(){          
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE dbo.cxP");
            vSql.AppendLine(" SET dbo.cxP.NumeroComprobanteImpuestoMunicipal = REPLACE(NumeroComprobanteImpuestoMunicipal, CHAR(10 ),'')");
            Execute(vSql.ToString(), -1);
        }

        private void ActualizarFormatoImpMunicipales(){
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Comun.FormatosImpMunicipales");
            vSql.AppendLine(" SET SeparadorDeMiles = '', Separador = CHAR(9), UsaSeparador = 'S'");          
            Execute(vSql.ToString(), -1);

            vSql.Clear();
            vSql.AppendLine("UPDATE Comun.FormatosImpMunicipales");
            vSql.AppendLine(" SET Separador = '', UsaSeparador = 'N'");          
            vSql.AppendLine(" WHERE Codigo = 1");
            Execute(vSql.ToString(), -1);

            vSql.Clear();
            vSql.AppendLine("UPDATE Comun.FormatosImpMunicipales");
            vSql.AppendLine(" SET OrigenDeDatos = 'UPPER(RifCompaniaModelo1)'");          
            vSql.AppendLine(" WHERE Codigo = 2");
            Execute(vSql.ToString(), -1);

            vSql.Clear();
            vSql.AppendLine("UPDATE Comun.FormatosImpMunicipales");
            vSql.AppendLine(" SET OrigenDeDatos = 'UPPER(RifProveedorModelo1)'");          
            vSql.AppendLine(" WHERE Codigo = 5");
            Execute(vSql.ToString(), -1);

            vSql.Clear();
            vSql.AppendLine("UPDATE Comun.FormatosImpMunicipales");
            vSql.AppendLine(" SET OrigenDeDatos = 'RIGHT(''00''+CONVERT(VARCHAR, day(Fecha),2), 2) +''/''+RIGHT(''00''+CONVERT(VARCHAR, month(Fecha), 2), 2) +''/''+CONVERT(VARCHAR, year(Fecha), 4)'");          
            vSql.AppendLine(" WHERE Codigo = 7");
            Execute(vSql.ToString(), -1);

            vSql.Clear();
            vSql.AppendLine("UPDATE Comun.FormatosImpMunicipales");
            vSql.AppendLine(" SET OrigenDeDatos = 'CONVERT(DECIMAL(10,2),MontoBaseImponible)'");          
            vSql.AppendLine(" WHERE Codigo = 15");
            Execute(vSql.ToString(), -1);

            vSql.Clear();
            vSql.AppendLine("UPDATE Comun.FormatosImpMunicipales");
            vSql.AppendLine(" SET OrigenDeDatos = 'CONVERT(DECIMAL(10,2),AlicuotaRetencion)'");          
            vSql.AppendLine(" WHERE Codigo = 16");
            Execute(vSql.ToString(), -1);

            vSql.Clear();
            vSql.AppendLine("UPDATE Comun.FormatosImpMunicipales");
            vSql.AppendLine(" SET OrigenDeDatos = 'CONVERT(DECIMAL(10,2),MontoRetencion)'");          
            vSql.AppendLine(" WHERE Codigo = 18");
            Execute(vSql.ToString(), -1);

            vSql.Clear();
            vSql.AppendLine("IF NOT EXISTS (SELECT * FROM Comun.FormatosImpMunicipales WHERE Codigo = 20)");
            vSql.AppendLine(" INSERT INTO Comun.FormatosImpMunicipales");
            vSql.AppendLine(" (Codigo,CodigoMunicipio,Columna,OrigenDeDatos,Longitud,Posicion,Linea,Formato,UsaSeparador,Separador,NombreOperador,FechaUltimaModificacion) VALUES");          
            vSql.AppendLine(" (20,'VENBOL0001','NumeroRUC','(CASE WHEN (ISNULL(NumeroRUC,'''') ) = '''' THEN ''NA'' ELSE NumeroRUC END)',15,19,1,'000000000000000','S',CHAR(9),'JEFE',GETDATE())");
            Execute(vSql.ToString(), -1);
        }
    
        private void UpgradeToVersion2_22FromWinCONT() {
            if(CrearCampoPorcentajePorcentajeGastoAdmisible()) {}
            if(CrearCampoPorcentajeGananciaMaxima()) {}
            CreateViewAndSP("Producto,CriterioDeDistribucion");
         }

        private bool CrearCampoPorcentajePorcentajeGastoAdmisible() {
            if (!ColumnExists("Contab.ParametrosConciliacion", "PorcentajeGastoAdmisible")) {
                AddColumnDecimal("Contab.ParametrosConciliacion", "PorcentajeGastoAdmisible", 25, 4, "", LibGalac.Aos.Base.LibImportData.ToDec("12.5"));
            }
            return true;
        }

        private bool CrearCampoPorcentajeGananciaMaxima() {
            if (!ColumnExists("Contab.ParametrosConciliacion", "PorcentajeGananciaMaxima")) {
                AddColumnDecimal("Contab.ParametrosConciliacion", "PorcentajeGananciaMaxima", 25, 4, "", LibGalac.Aos.Base.LibImportData.ToDec("30.0"));
            }
            return true;
        }
   
    }
}
