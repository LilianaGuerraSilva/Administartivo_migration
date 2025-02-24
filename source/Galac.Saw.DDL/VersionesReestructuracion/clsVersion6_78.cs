using Galac.Adm.Dal.Vendedor;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Lib;
using LibGalac.Aos.Cnf;
using System.Data;
using LibGalac.Aos.DefGen;
using Galac.Saw.Dal.Tablas;
using Galac.Adm.Dal.Venta;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_78 : clsVersionARestructurar {
        public clsVersion6_78(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            CrearAuditoriaConfiguracion();
            CrearParametroUsaMaquinaFiscal();
            AjustarParametroUsaCobroDirecto();
            LimpiaParametroAccionAlAnularFactDeMesesAnt();
            CorregirInconsistenciasEnCajasQueNoUtilizanMF();
            LimpiaParametroImprimirPrecioEnNotaES();
            CorreccionDeDatosNullEnCliente();
            DisposeConnectionNoTransaction();
            CambiarPermisosUsuarioFactura();
            CrearEscalada();
            AgregarColumnaNDCaja();
            AmpliarColumnaCompaniaImprentaDigitalClave();
            AgregarColumnaImprentaGUIDFactura();
            return true;
        }

        private void CrearAuditoriaConfiguracion() {            
            new clsAuditoriaConfiguracionED().InstalarTabla();
        }
        private void CrearParametroUsaMaquinaFiscal() {
            StringBuilder vSql = new StringBuilder();            

            AgregarNuevoParametro("UsaMaquinaFiscal", "Factura", 2, "2.2.- Facturación (Continuación)", 1, "", eTipoDeDatoParametros.String, "", 'N', "N");

            vSql.AppendLine("UPDATE Comun.SettValueByCompany");
            vSql.AppendLine(" SET Value = " + InsSql.ToSqlValue("S"));
            vSql.AppendLine(" WHERE NameSettDefinition = " + InsSql.ToSqlValue("UsaMaquinaFiscal"));
            vSql.AppendLine(" AND ConsecutivoCompania IN ");
            vSql.AppendLine(" (SELECT DISTINCT ConsecutivoCompania FROM Adm.Caja ");
            vSql.AppendLine(" WHERE UsaMaquinaFiscal = " + InsSql.ToSqlValue("S") + ")");
            Execute(vSql.ToString(), 0);
        }

        private void AjustarParametroUsaCobroDirecto() {
            StringBuilder vSql = new StringBuilder();
            
            vSql.AppendLine("UPDATE Comun.SettValueByCompany");
            vSql.AppendLine(" SET Value = " + InsSql.ToSqlValue("S"));
            vSql.AppendLine(" WHERE NameSettDefinition = " + InsSql.ToSqlValue("UsaCobroDirecto"));
            vSql.AppendLine(" AND ConsecutivoCompania IN ");
            vSql.AppendLine(" (SELECT DISTINCT ConsecutivoCompania FROM Adm.Caja ");
            vSql.AppendLine(" WHERE UsaMaquinaFiscal = " + InsSql.ToSqlValue("S") + ")");
            Execute(vSql.ToString(), 0);
        }
        private void LimpiaParametroAccionAlAnularFactDeMesesAnt() {
            StringBuilder vSql = new StringBuilder();            

            vSql.AppendLine("UPDATE Comun.SettValueByCompany");
            vSql.AppendLine(" SET Value = " + InsSql.ToSqlValue("2"));
            vSql.AppendLine(" WHERE NameSettDefinition = " + InsSql.ToSqlValue("AccionAlAnularFactDeMesesAnt"));
            Execute(vSql.ToString(), 0);
        }

        private void CorregirInconsistenciasEnCajasQueNoUtilizanMF() {
            StringBuilder vSql = new StringBuilder();            
            String vVersionProgramaActual = LibDefGen.ProgramInfo.ProgramVersion;

            if (!(RecordCountOfSql("SELECT * FROM Adm.AuditoriaConfiguracion WHERE VersionPrograma = " + InsSql.ToSqlValue(vVersionProgramaActual)) > 0)) {
                vSql.AppendLine("INSERT INTO Adm.AuditoriaConfiguracion ");
                vSql.AppendLine(" (ConsecutivoCompania, ConsecutivoAuditoria, VersionPrograma, FechayHora, Accion, Motivo, ConfiguracionOriginal, ConfiguracionNueva, NombreOperador, FechaUltimaModificacion)");
                vSql.AppendLine(" SELECT ConsecutivoCompania, ROW_NUMBER() OVER (PARTITION BY ConsecutivoCompania ORDER BY ConsecutivoCompania ASC), " + InsSql.ToSqlValue(vVersionProgramaActual) + ", ");
                vSql.AppendLine(_TodayAsSqlValue + ", " + InsSql.ToSqlValue("REESTRUCTURACION") + ", " + InsSql.ToSqlValue("Corrección Configuración Inicial") + ", ");
                vSql.AppendLine(InsSql.ToSqlValue("NombreCaja: ") + " + CAST(NombreCaja AS varchar) + " + InsSql.ToSqlValue("; UsaMaquinaFiscal: N; Serial: ") + " + CAST(SerialDeMaquinaFiscal AS varchar) + " + InsSql.ToSqlValue("; Versión Programa: ") + " + CAST(" + InsSql.ToSqlValue(ReadProgramCurrentVersion()) + " AS varchar), ");
                vSql.AppendLine(InsSql.ToSqlValue("Serial: ") + ", ");
                vSql.AppendLine(" NombreOperador, ");
                vSql.AppendLine(_TodayAsSqlValue);
                vSql.AppendLine(" FROM Adm.Caja");
                vSql.AppendLine(" WHERE UsaMaquinaFiscal = " + InsSql.ToSqlValue("N"));
                vSql.AppendLine(" AND SerialDeMaquinaFiscal <> " + InsSql.ToSqlValue(""));
                Execute(vSql.ToString(), -1);
                vSql.Clear();
            }
            
            vSql.AppendLine("UPDATE Adm.Caja");
            vSql.AppendLine(" SET SerialDeMaquinaFiscal = " + InsSql.ToSqlValue(""));
            vSql.AppendLine(" WHERE UsaMaquinaFiscal = " + InsSql.ToSqlValue("N"));
            vSql.AppendLine(" AND SerialDeMaquinaFiscal <> " + InsSql.ToSqlValue(""));
            Execute(vSql.ToString(), 0);
        }

        private string ReadProgramCurrentVersion() {
            string vResult = "";            
            string vSql = "SELECT fldVersionPrograma FROM dbo.Version WHERE fldSiglasPrograma = " + InsSql.ToSqlValue(LibDefGen.ProgramInfo.ProgramInitials);

            object vValue = ExecuteScalar(vSql, 0);
            if (vValue != null) {
                vResult = vValue.ToString();
            }
            return vResult;
        }

        private void LimpiaParametroImprimirPrecioEnNotaES() {
            StringBuilder vSql = new StringBuilder();

            vSql.AppendLine("UPDATE Comun.SettValueByCompany SET Value = " + InsSql.ToSqlValue(false));
            vSql.AppendLine(" WHERE NameSettDefinition  = " + InsSql.ToSqlValue("ImprimirNotaESconPrecio"));
            Execute(vSql.ToString(), 0);
        }

        private void CorreccionDeDatosNullEnCliente() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Cliente SET CuentaContableCxc = '', CuentaContableIngresos = '', CuentaContableAnticipo = '' ");
            vSql.AppendLine("WHERE (Codigo = 'RD_Cliente' OR (Codigo = '000000000A' AND Nombre = 'OFICINA')) ");
            vSql.AppendLine("AND (CuentaContableCxc IS NULL OR CuentaContableIngresos IS NULL OR CuentaContableAnticipo IS NULL)");

            Execute(vSql.ToString(), 0);
        }

        private void CambiarPermisosUsuarioFactura() {
            Execute(SqlUpdateParaCambiarNombrePermisoFactura("Insertar Factura Borrador", "Insertar Factura en Espera"));
            Execute(SqlUpdateParaCambiarNombrePermisoFactura("Modificar Borrador", "Modificar Documento en Espera"));
            Execute(SqlUpdateParaCambiarNombrePermisoFactura("Modificar", "Modificar Documento en Espera"));
            Execute(SqlUpdateParaCambiarNombrePermisoFactura("Eliminar Borrador", "Eliminar Documento en Espera"));
            Execute(SqlUpdateParaCambiarNombrePermisoFactura("Eliminar", "Eliminar Documento en Espera"));
            Execute(SqlUpdateParaCambiarNombrePermisoFactura("Insertar Devolución / Reverso", "Insertar Nota de Crédito por Devolución/Reverso"));
            Execute(SqlUpdateParaCambiarNombrePermisoFactura("Modificar Descripción y Precio en Borrador", "Modificar Descripción y Precio de Doc. en Espera"));
            Execute(SqlUpdateParaCambiarNombrePermisoFactura("Cambiar Descripción y Precio", "Modificar Descripción y Precio de Doc. en Espera"));
            Execute(SqlUpdateParaCambiarNombrePermisoFactura("Modificar Precio en Borrador", "Modificar Precio de Doc. en Espera"));
            Execute(SqlUpdateParaCambiarNombrePermisoFactura("Modificar Precio en Factura", "Modificar Precio de Doc. en Espera"));
            Execute(SqlUpdateParaCambiarNombrePermisoFactura("Modificar Vendedor en Factura Emitida", "Corregir Vendedor"));
            Execute(SqlUpdateParaCambiarNombrePermisoFactura("Emisión sin Impresión Fiscal", "Corregir falla de emisión Imp. Fiscal"));
            Execute(SqlUpdateParaCambiarNombrePermisoFactura("Emitir y Cobrar sin Impresión Fiscal", "Corregir falla de emisión Imp. Fiscal y Cobrar"));
        }

        private string SqlUpdateParaCambiarNombrePermisoFactura(string valNombreViejo, string valNombreNuevo) {
            string vSql = "UPDATE Lib.GUserSecurity SET ProjectAction = " + InsSql.ToSqlValue(valNombreNuevo) + " WHERE ProjectModule = 'Factura'  AND ProjectAction = " + InsSql.ToSqlValue(valNombreViejo);
            return vSql;
        }
        private void CrearEscalada() {
            new clsEscaladaED().InstalarTabla();
        }

        private void AgregarColumnaNDCaja() {
            if (AddColumnString("Adm.Caja", "UltimoNumeroNDFiscal", 12, "", "")) {
                AddDefaultConstraint("Adm.Caja", "d_CajUlNuND", "''", "UltimoNumeroNDFiscal");
            }
        }

        private void AmpliarColumnaCompaniaImprentaDigitalClave() {
            ModifyLengthOfColumnString("Compania", "ImprentaDigitalClave", 1000, "");
        }

        private void AgregarColumnaImprentaGUIDFactura() {           
            if (AddColumnString("factura", "ImprentaDigitalGUID", 50, "", "")) {
                AddDefaultConstraint("factura", "ImDigGuid", "''", "ImprentaDigitalGUID");                  
            }
        }
    }
}
