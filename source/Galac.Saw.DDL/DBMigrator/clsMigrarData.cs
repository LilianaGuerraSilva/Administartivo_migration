using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using System.Xml;
using System.Xml.Linq;

namespace Galac.Saw.DbMigrator {
    public class clsMigrarData {
        LibTrn insTrn;
        string[] _ModulesArray;
        string _CurrentUserName;
        string _CurrentUserNameAsSqlValue;
        string _TodayAsSqlValue;
        public clsMigrarData(string[] initModules) {
            insTrn = new LibTrn();
            _ModulesArray = initModules;
            _CurrentUserNameAsSqlValue = new QAdvSql("").ToSqlValue(GetCurrentUserName());
            _TodayAsSqlValue = new QAdvSql("").ToSqlValue(LibDate.Today());

        }
        #region Migrar Usuarios
        private void MigrarUsuarios() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Lib.Guser (UserName, UserPassword, FirstAndLastName, Cargo, Email, [status], IsSuperviser, NameLastUsedCompany, LastUsedMFCList) SELECT NombreDelUsuario, fldPassword, NombreYApellido, cargo, email, CASE WHEN [status] = 'S' THEN '0' ELSE '1' END, essupervisor, nombreultimacompaniausada, '' FROM Usuario ";
            insTrn.Execute(vSql,90);
            EncriptarClaves();
            MigrarNivelesDeSeguridad();
            AgregarPermisosAOS();
            insTrn.CommitTransaction();
            if(BorrarTablaAnterior("dbo.Usuario")) {
                if(CrearVistaDboUsuario()) {
                }
            }
        }

        private bool CrearVistaDboUsuario() {
            bool vResult;
            LibGalac.Aos.Dal.DDL.LibCreateDb insCreateDb = new LibGalac.Aos.Dal.DDL.LibCreateDb();
            vResult = insCreateDb.CreateViewDboUsuario();
            return vResult;
        }

        private void EncriptarClaves() {
            string vSql = "SELECT * FROM Lib.Guser";
            System.Data.DataSet vDs = insTrn.ExecuteDataset(vSql,-1);
            if(vDs != null && vDs.Tables != null && vDs.Tables[0] != null) {
                foreach(System.Data.DataRow vDr in vDs.Tables[0].Rows) {
                    string vName = vDr["UserName"].ToString();
                    string vPwd = LibGalac.Aos.Base.LibCryptography.SymEncryptDES(vDr["UserPassword"].ToString());
                    vSql = "UPDATE Lib.Guser SET UserPassword = '" + vPwd + "' WHERE UserName = '" + vName + "'";
                    insTrn.Execute(vSql,-1);
                }
            }
            vDs.Dispose();
        }

        private void AgregarPermisosAOS() {
            StringBuilder vSqlSb = new StringBuilder();
            vSqlSb.AppendLine("INSERT INTO Lib.GUserSecurity (UserName, ProjectModule, ProjectAction, HasAccess) VALUES ('EDOARDO', 'Permisos Especiales', 'Es Tester AOS', '" + LibConvert.BoolToSN(true) + "');");
            vSqlSb.AppendLine("INSERT INTO Lib.GUserSecurity (UserName, ProjectModule, ProjectAction, HasAccess) VALUES ('ALYIRA', 'Permisos Especiales', 'Es Tester AOS', '" + LibConvert.BoolToSN(true) + "');");
            vSqlSb.AppendLine("INSERT INTO Lib.GUserSecurity (UserName, ProjectModule, ProjectAction, HasAccess) VALUES ('TCEBALLOS', 'Permisos Especiales', 'Es Tester AOS', '" + LibConvert.BoolToSN(true) + "');");
            insTrn.Execute(vSqlSb.ToString(),-1);
        }
        private bool TheUserHasAccessToOption(string valLevels,int valOptionToCheck) {
            string optionASStr;
            bool vResult = false;
            optionASStr = LibGalac.Aos.Base.LibConvert.EnumToDbValue(valOptionToCheck);
            vResult = (LibGalac.Aos.Base.LibString.IndexOf(valLevels,optionASStr) >= 0);
            return vResult;
        }
        private void MigrarNivelesDeSeguridad() {
            string vSql = "SELECT * FROM Usuario";
            System.Data.DataSet vDs = insTrn.ExecuteDataset(vSql,-1);
            StringBuilder vSqlSb = new StringBuilder();
            if(vDs != null && vDs.Tables != null && vDs.Tables[0] != null) {
                foreach(System.Data.DataRow vDr in vDs.Tables[0].Rows) {
                    string vName = vDr["NombreDelUsuario"].ToString();
                    string vLevels = "";
                    string vFunctionalGroup = "";
                    int vFunctionalGroupLevel = 0;
                    #region OpcionesCompania
                    vFunctionalGroup = "Compañía / Parámetros / Niveles de Precio";
                    vFunctionalGroupLevel = 2;
                    vLevels = vDr["OpcionesCompania"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Compañía","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Compañía","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Compañía","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Compañía","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesParametros
                    vLevels = vDr["OpcionesParametros"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Parámetros","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Parámetros","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Parámetros","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesLoteAdm
                    vLevels = vDr["OpcionesLoteAdm"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Lote Adm","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Lote Adm","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Lote Adm","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region  Niveles de Precio
                    vLevels = vDr["Nivel1"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Niveles de Precio","Nivel 1",LibConvert.SNToBool(vLevels),vFunctionalGroup,vFunctionalGroupLevel));
                    vLevels = vDr["Nivel2"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Niveles de Precio","Nivel 2",LibConvert.SNToBool(vLevels),vFunctionalGroup,vFunctionalGroupLevel));
                    vLevels = vDr["Nivel3"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Niveles de Precio","Nivel 3",LibConvert.SNToBool(vLevels),vFunctionalGroup,vFunctionalGroupLevel));
                    vLevels = vDr["Nivel4"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Niveles de Precio","Nivel 4",LibConvert.SNToBool(vLevels),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesFactura
                    vFunctionalGroup = "Principal";
                    vFunctionalGroupLevel = 1;
                    vLevels = vDr["OpcionesFactura"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Emitir",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_EMITIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Emitir Directo",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_EMITIR_DIRECTO),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Anular",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ANULAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Insertar Copia",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR_COPIA),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Insertar Factura Borrador",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR_FACTURA_BORRADOR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Insertar Factura Manual",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR_MANUAL),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Insertar Devolución / Reverso",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR_REVERSO),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Insertar Nota de Crédito",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR_NOTA_CREDITO),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Insertar Nota de Débito",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR_NOTA_DEBITO),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Ingresar Fecha de Entrega",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INGRESAR_FECHA_ENTREGA),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Insertar Factura Histórica",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR_HISTORICA),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Insertar Nota de Crédito Histórica",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR_NOTA_CREDITO_HISTORICA),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Insertar Nota de Débito Histórica",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR_NOTA_DEBITO_HISTORICA),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Imprimir Nota de Entrega",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR_NOTA_DE_ENTREGA),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Generar Factura desde Contrato",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_GENERAR_FACTURA_DESDE_CONTRATO),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Generar Factura desde Cotización",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_GENERAR_FACTURA_DESDE_COTIZACION),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Informes Gerenciales",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INFORMES_GERENCIALES),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Informes de Libros",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INFORME_VENTAS_COMPRAS),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Otorgar Descuento",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_DESCUENTO),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Cobro Directo",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_COBRO_DIRECTO),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Cambiar Descripción y Precio",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_DESCRIPCION_Y_PRECIO),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Modificar Precio en Factura",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR_PRECIO_EN_FACTURA),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Importar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPORTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Exportar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_EXPORTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Reservar Mercancía",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_RESERVAR_MERCANCIA),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Modificar Vendedor en Factura Emitida",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR_VENDEDOR_EN_FACTURA_EMITIDA),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Factura","Reactivar Factura Anulada",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_REACTIVAR_FACTURA_ANULADA),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesCotizacion
                    vLevels = vDr["OpcionesCotizacion"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cotización","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cotización","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cotización","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cotización","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cotización","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cotización","Otorgar Descuento",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_DESCUENTO),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cotización","Reservar Mercancía",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_RESERVAR_MERCANCIA),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesContrato
                    vLevels = vDr["OpcionesContrato"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Contrato","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Contrato","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Contrato","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Contrato","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Contrato","Extender",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_EXTENDER),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Contrato","Activar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ACTIVAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Contrato","Desactivar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_DESACTIVAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Contrato","Ajustar Fecha Contrato",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_AJUSTAR_FECHA_CONTRATO),vFunctionalGroup,vFunctionalGroupLevel));

                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesResumenDiarioVentas
                    vLevels = vDr["OpcionesResumenDiarioVentas"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Resumen Diario","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Resumen Diario","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Resumen Diario","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Resumen Diario","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Resumen Diario","Realizar Cierre Z",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_REALIZAR_CIERRE_Z),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesCajaRegistradora
                    vLevels = vDr["OpcionesCajaRegistradora"].ToString();//revisar
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Caja Registradora","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Caja Registradora","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Caja Registradora","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Caja Registradora","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Caja Registradora","Abrir Gaveta",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ABRIR_GAVETA),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Caja Registradora","Asignar Caja Registradora",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ASIGNAR_CAJA_REGISTRADORA),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesExportarImportarSAW
                    vLevels = vDr["OpcionesExportarImportarSAW"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Importar/Exportar","Importar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPORTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Importar/Exportar","Exportar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_EXPORTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Importar/Exportar","Exportar Formato Mercantil",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_EXPORTAR_BANCO_MERCANTIL),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesCliente
                    vFunctionalGroup = "Cliente / CxC";
                    vFunctionalGroupLevel = 3;
                    vLevels = vDr["OpcionesCliente"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cliente","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cliente","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cliente","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cliente","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cliente","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cliente","Unificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_UNIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cliente","Informes de Libros",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INFORME_VENTAS_COMPRAS),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cliente","Importar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPORTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cliente","Exportar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_EXPORTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cliente","Ingresar cliente por mostrador",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INGRESAR_CLIENTE_POR_MOSTRADOR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesCxC
                    vLevels = vDr["OpcionesCxC"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxC","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxC","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxC","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxC","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxC","Anular",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ANULAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxC","Reimprimir Comprobante CxC",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_REIMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxC","Informes de Libros",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INFORME_VENTAS_COMPRAS),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxC","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxC","Importar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPORTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxC","Exportar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_EXPORTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxC","Refinanciar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_REFINANCIAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxC","Anular Refinanciamiento",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ANULAR_REFINANCIAR),vFunctionalGroup,vFunctionalGroupLevel));//revisar
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesCobranza
                    vLevels = vDr["OpcionesCobranza"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cobranza","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cobranza","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cobranza","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cobranza","Anular",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ANULAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cobranza","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cobranza","Distribuir Retención IVA",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ACTUALIZAR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesAnticipoCobrado
                    vLevels = vDr["OpcionesAnticipoCobrado"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Anticipo Cobrado","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Anticipo Cobrado","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Anticipo Cobrado","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Anticipo Cobrado","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Anticipo Cobrado","Anular",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ANULAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Anticipo Cobrado","Devolver",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_DEVOLVER),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Anticipo Cobrado","Reimprimir",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_REIMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Anticipo Cobrado","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INFORMES),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    ////OpcionesPropAnalVenc
                    //vLevels = vDr["OpcionesPropAnalVenc"].ToString();
                    //vSqlSb.AppendLine(SqlInsertSecLevel(vName, "Propiedades Análisis de Vencimiento", "Consultar", TheUserHasAccessToOption(vLevels, (int)NivelesUsuarioOld.ADM_CONSULTAR) , vFunctionalGroup, vFunctionalGroupLevel));
                    //vSqlSb.AppendLine(SqlInsertSecLevel(vName, "Propiedades Análisis de Vencimiento", "Insertar", TheUserHasAccessToOption(vLevels, (int)NivelesUsuarioOld.ADM_INSERTAR) , vFunctionalGroup, vFunctionalGroupLevel));
                    //vSqlSb.AppendLine(SqlInsertSecLevel(vName, "Propiedades Análisis de Vencimiento", "Modificar", TheUserHasAccessToOption(vLevels, (int)NivelesUsuarioOld.ADM_MODIFICAR) , vFunctionalGroup, vFunctionalGroupLevel));
                    //vSqlSb.AppendLine(SqlInsertSecLevel(vName, "Propiedades Análisis de Vencimiento", "Anular", TheUserHasAccessToOption(vLevels, (int)NivelesUsuarioOld.ADM_ELIMINAR) , vFunctionalGroup, vFunctionalGroupLevel));
                    //insTrn.Execute(vSqlSb.ToString(), -1);
                    #region OpcionesArtInventario
                    vFunctionalGroup = "Inventario";
                    vFunctionalGroupLevel = 4;
                    vLevels = vDr["OpcionesArtInventario"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Reincorporar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_REINCORPORAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Desincorporar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_DESINCORPORAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Recalcular Existencia",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_RECALCULAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Ajustar Precio",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_AJUSTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Importar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPORTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Exportar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_EXPORTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Autorizar Sobregiro",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_AUTORIZARSOBREGIRO),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Insertar Art. por Porcentaje de Comisión",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR_ARTICULO_POR_PORCENTAJE_COMISION),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Modificar Serial",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR_SERIAL),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Modificar Rollo",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR_ROLLO),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Consultar Cortes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR_CORTES),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Ajustar Precios por Costos",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_AJUSTAR_PRECIOS_POR_COSTOS),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Artículo","Método de Costo",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_METODO_COSTO),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesAlmacen
                    vLevels = vDr["OpcionesAlmacen"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Almacén","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Almacén","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Almacén","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Almacén","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesNotaEntSalida
                    vLevels = vDr["OpcionesNotaEntSalida"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Nota de Entrada/Salida","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Nota de Entrada/Salida","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Nota de Entrada/Salida","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Nota de Entrada/Salida","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesExistXAlmacen
                    vLevels = vDr["OpcionesExistXAlmacen"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Existencia por Almacén","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Existencia por Almacén","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesTransferencia
                    vLevels = vDr["OpcionesTransferencia"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Transferencia","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Transferencia","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Transferencia","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Transferencia","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesCompra
                    vLevels = vDr["OpcionesCompra"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Compra","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Compra","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Compra","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Compra","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Compra","Anular",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ANULAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Compra","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesConteoFisico
                    vLevels = vDr["OpcionesConteoFisico"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Conteo Físico","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Conteo Físico","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Conteo Físico","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Conteo Físico","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Conteo Físico","Emitir",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_EMITIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Conteo Físico","Anular",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ANULAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Conteo Físico","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesColor
                    vLevels = vDr["OpcionesColor"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Color","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Color","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Color","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Color","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesTalla
                    vLevels = vDr["OpcionesTalla"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Talla","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Talla","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Talla","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Talla","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesGrupoTC
                    vLevels = vDr["OpcionesGrupoTC"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Grupo Talla/Color","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Grupo Talla/Color","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Grupo Talla/Color","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Grupo Talla/Color","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesVendedor
                    vFunctionalGroup = "CxP / Vendedor";
                    vFunctionalGroupLevel = 5;
                    vLevels = vDr["OpcionesVendedor"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Vendedor","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Vendedor","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Vendedor","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Vendedor","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Vendedor","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Vendedor","Importar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPORTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Vendedor","Exportar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_EXPORTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion

                    #region OpcionesCxP
                    vLevels = vDr["OpcionesCxP"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxP","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxP","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxP","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxP","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxP","Anular",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ANULAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxP","Reimprimir Comprobante CxP",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_REIMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxP","Informes de Libros",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INFORME_VENTAS_COMPRAS),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxP","Imprimir Comprobante de Ret. de IVA",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR_COMPROBANTE_RETENCION_IVA),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxP","Insertar CxP Histórica",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR_HISTORICA),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxP","Importar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPORTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"CxP","Exportar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_EXPORTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesPago
                    vLevels = vDr["OpcionesPago"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Pago","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Pago","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Pago","Anular",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ANULAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Pago","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Pago","Insertar Pago Histórico",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR_HISTORICA),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Pago","Reimprimir Comprobante de Pago",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR_COMPROBANTE_PAGO),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Pago","Reimprimir Comprobante de Retención",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR_COMPROBANTE_RETENCION_ISLR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Pago","Reimprimir Comprobante de Ret. de IVA",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR_COMPROBANTE_RETENCION_IVA),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Pago","Modificar Beneficiario Cheque",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR_BENEFICIARIO_CHEQUE),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesProveedor
                    vLevels = vDr["OpcionesProveedor"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Proveedor","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Proveedor","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Proveedor","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Proveedor","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Proveedor","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Proveedor","Unificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_UNIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Proveedor","Informes de Libros",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INFORME_VENTAS_COMPRAS),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Proveedor","Importar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPORTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Proveedor","Exportar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_EXPORTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesAnticipoPagado
                    vLevels = vDr["OpcionesAnticipoPagado"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Anticipo Pagado","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Anticipo Pagado","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Anticipo Pagado","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Anticipo Pagado","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Anticipo Pagado","Anular",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ANULAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Anticipo Pagado","Devolver",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_DEVOLVER),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Anticipo Pagado","Reimprimir",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_REIMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Anticipo Pagado","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INFORMES),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesTablaRetencion
                    vFunctionalGroup = "Retenciones / Forma 30";
                    vFunctionalGroupLevel = 6;
                    vLevels = vDr["OpcionesTablaRetencion"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Tabla Retención","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Tabla Retención","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Tabla Retención","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Tabla Retención","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesPlantillaRet
                    vLevels = vDr["OpcionesPlantillaRet"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Plantilla Ret","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Plantilla Ret","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Plantilla Ret","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Plantilla Ret","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesARCV
                    vLevels = vDr["OpcionesARCV"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"ARCV","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"ARCV","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"ARCV","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"ARCV","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"ARCV","Reimprimir",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_REIMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"ARCV","Generar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_GENERAR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesRA
                    vLevels = vDr["OpcionesRA"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Relación Anual","Forma Impresa",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_FORMA_IMPRESA),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Relación Anual","Medio Magnético",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MEDIO_MAGNETICO),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesPlanillaForma00030
                    vLevels = vDr["OpcionesPlanillaForma00030"].ToString();//revisar
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Planilla Forma 00030","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Planilla Forma 00030","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Planilla Forma 00030","Insertar Sustitutiva",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR_SUSTITUTIVA),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Planilla Forma 00030","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Planilla Forma 00030","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Planilla Forma 00030","Imprimir",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Planilla Forma 00030","Borrador",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR_BORRADOR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesCuentaBancaria
                    vFunctionalGroup = "Bancos";
                    vFunctionalGroupLevel = 8;
                    vLevels = vDr["OpcionesCuentaBancaria"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cuenta Bancaria","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cuenta Bancaria","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cuenta Bancaria","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cuenta Bancaria","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cuenta Bancaria","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesMovimientoBancario
                    vLevels = vDr["OpcionesMovimientoBancario"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Movimiento Bancario","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Movimiento Bancario","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Movimiento Bancario","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Movimiento Bancario","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Movimiento Bancario","Anular",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ANULAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Movimiento Bancario","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));

                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesConceptoBancario
                    vLevels = vDr["OpcionesConceptoBancario"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Concepto Bancario","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Concepto Bancario","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Concepto Bancario","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Concepto Bancario","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesConciliacion
                    vLevels = vDr["OpcionesConciliacion"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Conciliación","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Conciliación","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Conciliación","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Conciliación","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Conciliación","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Conciliación","Cerrar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CERRAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Conciliación","Abrir",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ABRIR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion

                    #region OpcionesPeriodo
                    vFunctionalGroup = "Contabilidad";
                    vFunctionalGroupLevel = 7;
                    vLevels = vDr["OpcionesPeriodo"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Período","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Período","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Período","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Período","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Período","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Período","Abrir Período",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.WCO_REABRIR_PERIODO),vFunctionalGroup,vFunctionalGroupLevel));//revisar
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Período","Cerrar Período",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.WCO_CERRAR_PERIODO),vFunctionalGroup,vFunctionalGroupLevel));//revisar
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Período","Abrir Mes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ABRIR),vFunctionalGroup,vFunctionalGroupLevel));//revisar
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Período","Cerrar Mes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CERRAR),vFunctionalGroup,vFunctionalGroupLevel));//revisar
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesCuenta
                    vLevels = vDr["OpcionesCuenta"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cuenta","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cuenta","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cuenta","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cuenta","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cuenta","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesComprobante
                    vLevels = vDr["OpcionesComprobante"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Comprobante","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Comprobante","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Comprobante","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Comprobante","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Comprobante","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Comprobante","Modificar Comprobantes Automáticos",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.WCO_ModificarComprobantesAutomaticos),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesCatalogoGeneral
                    vLevels = vDr["OpcionesCatalogoGeneral"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Catálogo General","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Catálogo General","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Catálogo General","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Catálogo General","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesActivoFijo
                    vLevels = vDr["OpcionesActivoFijo"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Activo Fijo","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Activo Fijo","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Activo Fijo","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Activo Fijo","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Activo Fijo","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Activo Fijo","Retirar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.WCO_RetirarActivos),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Activo Fijo","Reincorporar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_REINCORPORAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Activo Fijo","Usa Cálculo Manual",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.WCO_CALCULOMANUAL),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion

                    #region OpcionesReglasDeContabilizacion
                    vLevels = vDr["OpcionesReglasDeContabilizacion"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Reglas de Contabilización","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Reglas de Contabilización","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Reglas de Contabilización","Informes",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_IMPRIMIR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesParametrosActivoFijo
                    vLevels = vDr["OpcionesParametrosActivoFijo"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Parámetros Activo Fijo","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Parámetros Activo Fijo","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region Opciones Informes
                    vLevels = vDr["UsaContabilidad"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Acceso","Usa Módulo de Contabilidad",LibConvert.SNToBool(vLevels),vFunctionalGroup,vFunctionalGroupLevel));
                    vLevels = vDr["MenuDeBalances"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Informes Contables","Menú de Balances",LibConvert.SNToBool(vLevels),vFunctionalGroup,vFunctionalGroupLevel));
                    vLevels = vDr["MenuDeInformes"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Informes Contables","Menú de Informes",LibConvert.SNToBool(vLevels),vFunctionalGroup,vFunctionalGroupLevel));
                    vLevels = vDr["ReiniciarTablasTemporales"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Informes Contables","Reiniciar Tablas Temporales",LibConvert.SNToBool(vLevels),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    ////OpcionesAlicuotaIVA
                    //vLevels = vDr["OpcionesAlicuotaIVA"].ToString(); //revisar
                    //vSqlSb.AppendLine(SqlInsertSecLevel(vName, "Alícuota IVA", "Consultar", TheUserHasAccessToOption(vLevels, (int)NivelesUsuarioOld.ADM_CONSULTAR) , vFunctionalGroup, vFunctionalGroupLevel));
                    //vSqlSb.AppendLine(SqlInsertSecLevel(vName, "Alícuota IVA", "Insertar", TheUserHasAccessToOption(vLevels, (int)NivelesUsuarioOld.ADM_INSERTAR) , vFunctionalGroup, vFunctionalGroupLevel));
                    //vSqlSb.AppendLine(SqlInsertSecLevel(vName, "Alícuota IVA", "Modificar", TheUserHasAccessToOption(vLevels, (int)NivelesUsuarioOld.ADM_MODIFICAR) , vFunctionalGroup, vFunctionalGroupLevel));
                    //vSqlSb.AppendLine(SqlInsertSecLevel(vName, "Alícuota IVA", "Eliminar", TheUserHasAccessToOption(vLevels, (int)NivelesUsuarioOld.ADM_ELIMINAR) , vFunctionalGroup, vFunctionalGroupLevel));
                    //vSqlSb.AppendLine(SqlInsertSecLevel(vName, "Alícuota IVA", "Informes", TheUserHasAccessToOption(vLevels, (int)NivelesUsuarioOld.ADM_INFORMES) , vFunctionalGroup, vFunctionalGroupLevel));
                    //insTrn.Execute(vSqlSb.ToString(), -1);



                    //#region OpcionesGestionDeCobranza
                    //vLevels = vDr["OpcionesGestionDeCobranza"].ToString();
                    //vSqlSb.AppendLine(SqlInsertSecLevel(vName, "Gestión de Cobranza", "Consultar", TheUserHasAccessToOption(vLevels, (int)NivelesUsuarioOld.ADM_CONSULTAR) , vFunctionalGroup, vFunctionalGroupLevel));
                    //vSqlSb.AppendLine(SqlInsertSecLevel(vName, "Gestión de Cobranza", "Insertar", TheUserHasAccessToOption(vLevels, (int)NivelesUsuarioOld.ADM_INSERTAR) , vFunctionalGroup, vFunctionalGroupLevel));
                    //vSqlSb.AppendLine(SqlInsertSecLevel(vName, "Gestión de Cobranza", "Modificar", TheUserHasAccessToOption(vLevels, (int)NivelesUsuarioOld.ADM_MODIFICAR) , vFunctionalGroup, vFunctionalGroupLevel));
                    //vSqlSb.AppendLine(SqlInsertSecLevel(vName, "Gestión de Cobranza", "Eliminar", TheUserHasAccessToOption(vLevels, (int)NivelesUsuarioOld.ADM_ELIMINAR) , vFunctionalGroup, vFunctionalGroupLevel));
                    //vSqlSb.AppendLine(SqlInsertSecLevel(vName, "Gestión de Cobranza", "Informes", TheUserHasAccessToOption(vLevels, (int)NivelesUsuarioOld.ADM_IMPRIMIR) , vFunctionalGroup, vFunctionalGroupLevel));
                    //insTrn.Execute(vSqlSb.ToString(), -1);
                    //vSqlSb.Remove(0, vSqlSb.Length);
                    //#endregion









                    #region OpcionesCajeroFactura
                    vFunctionalGroup = "Opciones de Cajero";
                    vFunctionalGroupLevel = 9;
                    vLevels = vDr["OpcionesCajeroFactura"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cajero Factura","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cajero Factura","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cajero Factura","Emitir y Cobrar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_COBRO_DIRECTO),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cajero Factura","Modificar Precio",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR_PRECIO_EN_FACTURA),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cajero Factura","Emitir Directo",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_EMITIR_DIRECTO),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesCajeroCliente
                    vLevels = vDr["OpcionesCajeroCliente"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cajero Cliente","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cajero Cliente","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cajero Cliente","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cajero Cliente","Insertar por Mostrador",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INGRESAR_CLIENTE_POR_MOSTRADOR),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesCajeroArtInventario
                    vLevels = vDr["OpcionesCajeroArtInventario"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cajero Artículo","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cajero Artículo","Consultar Corte",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR_CORTES),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesCajeroCaja
                    vLevels = vDr["OpcionesCajeroCaja"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Cajero Caja","Abrir Gaveta",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ABRIR_GAVETA),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion

                    #region UsuarioTipocajero
                    vLevels = vDr["EsUsuarioTipoCajero"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Acceso","Es Usuario TipoCajero",LibConvert.SNToBool(vLevels),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion

                    #region OpcionesTablas
                    vFunctionalGroup = "Tablas Generales";
                    vFunctionalGroupLevel = 10;
                    vLevels = vDr["OpcionesTablas"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Tablas","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Tablas","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Tablas","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Tablas","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Tablas","Ingresar Cambio del Día",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTARTASADECAMBIO),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                    #region OpcionesRespaldo
                    vFunctionalGroup = "Mantenimiento";
                    vFunctionalGroupLevel = 11;
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Respaldo","Respaldar",LibConvert.SNToBool(vDr["EsSupervisor"].ToString()),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Respaldo","Restaurar",LibConvert.SNToBool(vDr["EsSupervisor"].ToString()),vFunctionalGroup,vFunctionalGroupLevel));
                    #endregion

                    #region OpcionesUsuario
                    vFunctionalGroup = "Seguridad";
                    vFunctionalGroupLevel = 12;
                    vLevels = vDr["OpcionesUsuario"].ToString();
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Usuario","Consultar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_CONSULTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Usuario","Insertar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Usuario","Modificar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_MODIFICAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Usuario","Eliminar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ELIMINAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Usuario","Activar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_ACTIVAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Usuario","Desactivar",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_DESACTIVAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Usuario","Insertar Copia",TheUserHasAccessToOption(vLevels,(int)NivelesUsuarioOld.ADM_INSERTAR),vFunctionalGroup,vFunctionalGroupLevel));
                    vSqlSb.AppendLine(SqlInsertSecLevel(vName,"Usuario","Reiniciar Password",LibConvert.SNToBool(vDr["EsSupervisor"].ToString()),vFunctionalGroup,vFunctionalGroupLevel));
                    insTrn.Execute(vSqlSb.ToString(),-1);
                    vSqlSb.Remove(0,vSqlSb.Length);
                    #endregion
                }
            }
            vDs.Dispose();
        }

        private string SqlInsertSecLevel(string valUserName,string valProjectModule,string valProjectAction,bool valHasAccess,string valFunctionalGroup,int valFunctionalGroupLevel) {
            return "INSERT INTO Lib.GUserSecurity (UserName, ProjectModule, ProjectAction, HasAccess, FunctionalGroup, FunctionalGroupLevel) VALUES ('" + valUserName + "', '" + valProjectModule + "', '" + valProjectAction + "', '" + LibConvert.BoolToSN(valHasAccess) + "', '" + valFunctionalGroup + "', " + valFunctionalGroupLevel + ");";
        }

        #endregion Migrar Usuarios
        #region Migrar Sector de Negocios
        private void MigrarSectorDeNegocios() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Comun.SectorDeNegocio (Descripcion) SELECT Descripcion FROM SectorDeNegocio";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            if(BorrarTablaAnterior("dbo.SectorDeNegocio")) {
                Galac.Saw.DDL.clsCompatViews.CrearVistaDboSectorDeNegocio();
            }
        }

        #endregion
        #region Migrar Ciudad
        private void MigrarCiudad() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Comun.Ciudad (NombreCiudad ,fldOrigen ) SELECT NombreCiudad, '0' FROM Ciudad";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            if(BorrarTablaAnterior("dbo.Ciudad")) {
                Galac.Saw.DDL.clsCompatViews.CrearVistaDboCiudad();
            }
        }
        #endregion
        #region Migrar Banco
        private void MigrarBanco() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Comun.Banco (Consecutivo,Nombre,fldOrigen,NombreOperador,FechaUltimaModificacion) SELECT  Codigo, Nombre, '1'," + _CurrentUserNameAsSqlValue + ", " + _TodayAsSqlValue + " FROM Banco";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboBanco();
        }
        #endregion
        #region Migrar Pais
        private void MigrarPais() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Comun.Pais (Codigo,Nombre, fldOrigen, NombreOperador, FechaUltimaModificacion) SELECT  Codigo, Nombre, '1', " + _CurrentUserNameAsSqlValue + ", " + _TodayAsSqlValue + " FROM Pais";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboPais();
        }

        #endregion
        #region Migrar Ciiu
        private void MigrarCiiu() {
            int vTipoTabla = 1; //Tabla Anterior a 2004
            MigrarCiiuSegunTipoTabla(vTipoTabla);
            vTipoTabla = 2; //Tabla 2004
            MigrarCiiuSegunTipoTabla(vTipoTabla);
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboCiiu();
        }
        private void MigrarCiiuSegunTipoTabla(int valTipoTabla) {
            string vNombreTablaCiiu = "";
            string vSqlTipoTabla = new QAdvSql("").EnumToSqlValue(valTipoTabla);
            insTrn.StartTransaction();
            if(valTipoTabla == 1) {
                vNombreTablaCiiu = "dbo.CIIU";
            } else if(valTipoTabla == 2) {
                vNombreTablaCiiu = "dbo.CIIU2004";
            }
            string vSql = "INSERT INTO Comun.Ciiu (TipoTabla,Codigo,Descripcion, EsTitulo, fldOrigen, NombreOperador, FechaUltimaModificacion) SELECT " + vSqlTipoTabla + ", CodigoCIIU, Descripcion, EsTitulo, '1', " + _CurrentUserNameAsSqlValue + ", " + _TodayAsSqlValue + " FROM " + vNombreTablaCiiu;
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
        }

        #endregion
        #region Migrar Categoria
        private void MigrarCategoria() {
            insTrn.StartTransaction();
            //string vSql = "INSERT INTO Saw.Categoria (ConsecutivoCompania,Consecutivo,Descripcion, NombreOperador, FechaUltimaModificacion) SELECT ConsecutivoCompania,Consecutivo,Descripcion,"  + _CurrentUserNameAsSqlValue + ", " + _TodayAsSqlValue + "FROM (SELECT ConsecutivoCompania,Descripcion,ROW_NUMBER() OVER(PARTITION BY ConsecutivoCompania ORDER BY ConsecutivoCompania desc) AS Consecutivo FROM  Categoria )  as T";
            //cambiar!!!
            string vSql = "INSERT INTO Saw.Categoria (ConsecutivoCompania,Consecutivo,Descripcion, NombreOperador, FechaUltimaModificacion) SELECT ConsecutivoCompania,Consecutivo,Descripcion,'JEFE', getdate() FROM (SELECT ConsecutivoCompania,Descripcion,ROW_NUMBER() OVER(PARTITION BY ConsecutivoCompania ORDER BY ConsecutivoCompania desc) AS Consecutivo FROM  Categoria )  as T";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboCategoria();
        }


        #endregion

        #region Migrar Unidad De Venta
        private void MigrarUnidadDeVenta() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Saw.UnidadDeVenta (Nombre, NombreOperador, FechaUltimaModificacion) SELECT Nombre, NombreOperador, FechaUltimaModificacion FROM UnidadDeVenta";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboUnidadDeVenta();
        }

        #endregion

        #region Migrar Urbanizacion ZP
        private void MigrarUrbanizacionZP() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Saw.UrbanizacionZP (Urbanizacion, ZonaPostal) SELECT Urbanizacion, ZonaPostal FROM UrbanizacionZP";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboUrbanizacionZP();
        }

        #endregion

        #region Migrar Zona Cobranza
        private void MigrarZonaCobranza() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Saw.ZonaCobranza (ConsecutivoCompania, Nombre, NombreOperador, FechaUltimaModificacion) SELECT ConsecutivoCompania, Nombre, NombreOperador, FechaUltimaModificacion FROM ZonaCobranza";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboZonaCobranza();
        }

        #endregion

        #region Migrar Maquina Fiscal
        private void MigrarMaquinaFiscal() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Saw.MaquinaFiscal (ConsecutivoCompania, ConsecutivoMaquinaFiscal, Descripcion, NumeroRegistro, Status, LongitudNumeroFiscal, NombreOperador, FechaUltimaModificacion) SELECT ConsecutivoCompania, ConsecutivoMaquinaFiscal, Descripcion, NumeroRegistro, Status, 0 ,NombreOperador, FechaUltimaModificacion FROM MaquinaFiscal";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboMaquinaFiscal();
        }

        #endregion

        #region Migrar Prop Analisis de Vencimiento
        private void MigrarPropAnalisisVenc() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Saw.PropAnalisisVenc (SecuencialUnique0, PrimerVencimiento, SegundoVencimiento, TercerVencimiento, NombreOperador, FechaUltimaModificacion) SELECT  ROW_NUMBER() OVER(ORDER BY SecuencialUnique0 asc), PrimerVencimiento, SegundoVencimiento, TercerVencimiento, NombreOperador, FechaUltimaModificacion FROM PropAnalisisVenc";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboPropAnalisisVenc();
        }

        #endregion

        #region Migrar Talla
        private void MigrarTalla() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Saw.Talla (ConsecutivoCompania, CodigoTalla, DescripcionTalla, CodigoLote, NombreOperador, FechaUltimaModificacion) SELECT ConsecutivoCompania, CodigoTalla, DescripcionTalla, CodigoLote , NombreOperador, FechaUltimaModificacion FROM Talla";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboTalla();
        }

        #endregion


        #region Migrar Color
        private void MigrarColor() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Saw.Color (ConsecutivoCompania, CodigoColor, DescripcionColor, CodigoLote, NombreOperador, FechaUltimaModificacion) SELECT ConsecutivoCompania, CodigoColor, DescripcionColor, CodigoLote , NombreOperador, FechaUltimaModificacion FROM Color";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboColor();
        }

        #endregion

        #region Migrar NotaFinal
        private void MigrarNotaFinal() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Saw.NotaFinal (ConsecutivoCompania, CodigoDeLaNota, Descripcion, NombreOperador, FechaUltimaModificacion) SELECT ConsecutivoCompania, CodigoDeLaNota, Descripcion, NombreOperador, FechaUltimaModificacion FROM NotaFinal";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboNotaFinal();
        }

        #endregion

        #region Migrar TipoProveedor
        private void MigrarTipoProveedor() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Saw.TipoProveedor (ConsecutivoCompania, Nombre, NombreOperador, FechaUltimaModificacion) SELECT ConsecutivoCompania, Nombre, " + _CurrentUserNameAsSqlValue + ", " + _TodayAsSqlValue + " FROM TipoProveedor";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboTipoProveedor();
        }


        #endregion




        #region Migrar Reglas De Contabilizacion
        private void MigrarReglasDeContabilizacion() {

            QAdvSql insQAdvSql = new QAdvSql("");

            StringBuilder vSQL = new StringBuilder();
            insTrn.StartTransaction();
            vSQL.Append("INSERT INTO Saw.ReglasDeContabilizacion (");
            vSQL.Append("ConsecutivoCompania");
            vSQL.Append(",Numero");
            vSQL.Append(",DiferenciaEnCambioyCalculo");
            vSQL.Append(",CuentaIva1Credito");
            vSQL.Append(",CuentaIva1Debito");
            vSQL.Append(",DondeContabilizarRetIva");
            vSQL.Append(",CuentaRetencionIva");
            vSQL.Append(",TipoContabilizacionCxc");
            vSQL.Append(",ContabIndividualCxc");
            vSQL.Append(",ContabPorLoteCxc");
            vSQL.Append(",CuentaCxCclientes");
            vSQL.Append(",CuentaCxCingresos");
            vSQL.Append(",TipoContabilizacionCxp");
            vSQL.Append(",ContabIndividualCxp");
            vSQL.Append(",ContabPorLoteCxp");
            vSQL.Append(",CuentaCxPgasto");
            vSQL.Append(",CuentaCxPproveedores");
            vSQL.Append(",TipoContabilizacionCobranza");
            vSQL.Append(",ContabIndividualCobranza");
            vSQL.Append(",ContabPorLoteCobranza");
            vSQL.Append(",CuentaCobranzaCobradoEnEfectivo");
            vSQL.Append(",CuentaCobranzaCobradoEnCheque");
            vSQL.Append(",CuentaCobranzaCobradoEnTarjeta");
            vSQL.Append(",CuentaCobranzaRetencionIslr");
            vSQL.Append(",CuentaCobranzaRetencionIva");
            vSQL.Append(",CuentaCobranzaOtros");
            vSQL.Append(",CuentaCobranzaCxCclientes");
            vSQL.Append(",CuentaCobranzaCobradoAnticipo");
            vSQL.Append(",TipoContabilizacionPagos");
            vSQL.Append(",ContabIndividualPagos");
            vSQL.Append(",ContabPorLotePagos");
            vSQL.Append(",CuentaPagosCxPproveedores");
            vSQL.Append(",CuentaPagosRetencionIslr");
            vSQL.Append(",CuentaPagosOtros");
            vSQL.Append(",CuentaPagosBanco");
            vSQL.Append(",CuentaPagosPagadoAnticipo");
            vSQL.Append(",TipoContabilizacionFacturacion");
            vSQL.Append(",ContabIndividualFacturacion");
            vSQL.Append(",ContabPorLoteFacturacion");
            vSQL.Append(",CuentaFacturacionCxCclientes");
            vSQL.Append(",CuentaFacturacionMontoTotalFactura");
            vSQL.Append(",CuentaFacturacionCargos");
            vSQL.Append(",CuentaFacturacionDescuentos");
            vSQL.Append(",ContabilizarPorArticulo");
            vSQL.Append(",AgruparPorCuentaDeArticulo");
            vSQL.Append(",AgruparPorCargosDescuentos");
            vSQL.Append(",TipoContabilizacionRdvtas");
            vSQL.Append(",ContabIndividualRdvtas");
            vSQL.Append(",ContabPorLoteRdvtas");
            vSQL.Append(",CuentaRdvtasCaja");
            vSQL.Append(",CuentaRdvtasMontoTotal");
            vSQL.Append(",ContabilizarPorArticuloRdvtas");
            vSQL.Append(",AgruparPorCuentaDeArticuloRdvtas");
            vSQL.Append(",TipoContabilizacionMovBancario");
            vSQL.Append(",ContabIndividualMovBancario");
            vSQL.Append(",ContabPorLoteMovBancario");
            vSQL.Append(",CuentaMovBancarioGasto");
            vSQL.Append(",CuentaMovBancarioBancosHaber");
            vSQL.Append(",CuentaMovBancarioBancosDebe");
            vSQL.Append(",CuentaMovBancarioIngresos");
            vSQL.Append(",CuentaDebitoBancarioGasto");
            vSQL.Append(",CuentaDebitoBancarioBancos");
            vSQL.Append(",CuentaCreditoBancarioGasto");
            vSQL.Append(",CuentaCreditoBancarioBancos");
            vSQL.Append(",TipoContabilizacionAnticipo");
            vSQL.Append(",ContabIndividualAnticipo");
            vSQL.Append(",ContabPorLoteAnticipo");
            vSQL.Append(",CuentaAnticipoCaja");
            vSQL.Append(",CuentaAnticipoCobrado");
            vSQL.Append(",CuentaAnticipoOtrosIngresos");
            vSQL.Append(",CuentaAnticipoPagado");
            vSQL.Append(",CuentaAnticipoBanco");
            vSQL.Append(",CuentaAnticipoOtrosEgresos");
            vSQL.Append(",FacturaTipoComprobante");
            vSQL.Append(",CxCtipoComprobante");
            vSQL.Append(",CxPtipoComprobante");
            vSQL.Append(",CobranzaTipoComprobante");
            vSQL.Append(",PagoTipoComprobante");
            vSQL.Append(",MovimientoBancarioTipoComprobante");
            vSQL.Append(",AnticipoTipoComprobante");
            vSQL.Append(",CuentaCostoDeVenta");
            vSQL.Append(",CuentaInventario");
            vSQL.Append(",TipoContabilizacionInventario");
            vSQL.Append(",AgruparPorCuentaDeArticuloInven");
            vSQL.Append(",InventarioTipoComprobante");
            vSQL.Append(",CtaDePagosSueldos");
            vSQL.Append(",CtaDePagosSueldosBanco");
            vSQL.Append(",ContabIndividualPagosSueldos");
            vSQL.Append(",PagosSueldosTipoComprobante");
            vSQL.Append(",TipoContabilizacionDePagosSueldos");
            vSQL.Append(",EditarComprobanteDePagosSueldos");
            vSQL.Append(",EditarComprobanteAfterInsertCxC");
            vSQL.Append(",EditarComprobanteAfterInsertCxP");
            vSQL.Append(",EditarComprobanteAfterInsertCobranza");
            vSQL.Append(",EditarComprobanteAfterInsertPagos");
            vSQL.Append(",EditarComprobanteAfterInsertFactura");
            vSQL.Append(",EditarComprobanteAfterInsertResDia");
            vSQL.Append(",EditarComprobanteAfterInsertMovBan");
            vSQL.Append(",EditarComprobanteAfterInsertImpTraBan");
            vSQL.Append(",EditarComprobanteAfterInsertAnticipo");
            vSQL.Append(",EditarComprobanteAfterInsertInventario");
            vSQL.Append(",EditarComprobanteAfterInsertCajaChica");
            vSQL.Append(",NombreOperador");
            vSQL.Append(",SiglasTipoComprobanteCajaChica");
            vSQL.Append(",ContabIndividualCajaChica");
            vSQL.Append(",CuentaCajaChicaGasto");
            vSQL.Append(",MostrarDesglosadoCajaChica");
            vSQL.Append(",CuentaCajaChicaBancoHaber");
            vSQL.Append(",CuentaCajaChicaBancoDebe");
            vSQL.Append(",CuentaCajaChicaBanco");
            vSQL.Append(",SiglasTipoComprobanteRendiciones");
            vSQL.Append(",ContabIndividualRendiciones");
            vSQL.Append(",CuentaRendicionesGasto");
            vSQL.Append(",CuentaRendicionesBanco");
            vSQL.Append(",CuentaRendicionesAnticipos");
            vSQL.Append(",MostrarDesglosadoRendiciones");
            vSQL.Append(",FechaUltimaModificacion)");
            vSQL.Append("  SELECT  ");
            vSQL.Append("ConsecutivoCompania");
            vSQL.Append(",Numero");
            vSQL.Append(",DiferenciaEnCambioyCalculo");
            vSQL.Append(",CuentaIva1Credito");
            vSQL.Append(",CuentaIva1Debito");
            vSQL.Append(",DondeContabilizarRetIva");
            vSQL.Append(",CuentaRetencionIva");
            vSQL.Append(",TipoContabilizacionCxc");
            vSQL.Append(",ContabIndividualCxc");
            vSQL.Append(",ContabPorLoteCxc");
            vSQL.Append(",CuentaCxCclientes");
            vSQL.Append(",CuentaCxCingresos");
            vSQL.Append(",TipoContabilizacionCxp");
            vSQL.Append(",ContabIndividualCxp");
            vSQL.Append(",ContabPorLoteCxp");
            vSQL.Append(",CuentaCxPgasto");
            vSQL.Append(",CuentaCxPproveedores");
            vSQL.Append(",TipoContabilizacionCobranza");
            vSQL.Append(",ContabIndividualCobranza");
            vSQL.Append(",ContabPorLoteCobranza");
            vSQL.Append(",CuentaCobranzaCobradoEnEfectivo");
            vSQL.Append(",CuentaCobranzaCobradoEnCheque");
            vSQL.Append(",CuentaCobranzaCobradoEnTarjeta");
            vSQL.Append(",CuentaCobranzaRetencionIslr");
            vSQL.Append(",CuentaCobranzaRetencionIva");
            vSQL.Append(",CuentaCobranzaOtros");
            vSQL.Append(",CuentaCobranzaCxCclientes");
            vSQL.Append(",CuentaCobranzaCobradoAnticipo");
            vSQL.Append(",TipoContabilizacionPagos");
            vSQL.Append(",ContabIndividualPagos");
            vSQL.Append(",ContabPorLotePagos");
            vSQL.Append(",CuentaPagosCxPproveedores");
            vSQL.Append(",CuentaPagosRetencionIslr");
            vSQL.Append(",CuentaPagosOtros");
            vSQL.Append(",CuentaPagosBanco");
            vSQL.Append(",CuentaPagosPagadoAnticipo");
            vSQL.Append(",TipoContabilizacionFacturacion");
            vSQL.Append(",ContabIndividualFacturacion");
            vSQL.Append(",ContabPorLoteFacturacion");
            vSQL.Append(",CuentaFacturacionCxCclientes");
            vSQL.Append(",CuentaFacturacionMontoTotalFactura");
            vSQL.Append(",CuentaFacturacionCargos");
            vSQL.Append(",CuentaFacturacionDescuentos");
            vSQL.Append(",ContabilizarPorArticulo");
            vSQL.Append(",AgruparPorCuentaDeArticulo");
            vSQL.Append(",AgruparPorCargosDescuentos");
            vSQL.Append(",TipoContabilizacionRdvtas");
            vSQL.Append(",ContabIndividualRdvtas");
            vSQL.Append(",ContabPorLoteRdvtas");
            vSQL.Append(",CuentaRdvtasCaja");
            vSQL.Append(",CuentaRdvtasMontoTotal");
            vSQL.Append(",ContabilizarPorArticuloRdvtas");
            vSQL.Append(",AgruparPorCuentaDeArticuloRdvtas");
            vSQL.Append(",TipoContabilizacionMovBancario");
            vSQL.Append(",ContabIndividualMovBancario");
            vSQL.Append(",ContabPorLoteMovBancario");
            vSQL.Append(",CuentaMovBancarioGasto");
            vSQL.Append(",CuentaMovBancarioBancosHaber");
            vSQL.Append(",CuentaMovBancarioBancosDebe");
            vSQL.Append(",CuentaMovBancarioIngresos");
            vSQL.Append(",CuentaDebitoBancarioGasto");
            vSQL.Append(",CuentaDebitoBancarioBancos");
            vSQL.Append(",CuentaCreditoBancarioGasto");
            vSQL.Append(",CuentaCreditoBancarioBancos");
            vSQL.Append(",TipoContabilizacionAnticipo");
            vSQL.Append(",ContabIndividualAnticipo");
            vSQL.Append(",ContabPorLoteAnticipo");
            vSQL.Append(",CuentaAnticipoCaja");
            vSQL.Append(",CuentaAnticipoCobrado");
            vSQL.Append(",CuentaAnticipoOtrosIngresos");
            vSQL.Append(",CuentaAnticipoPagado");
            vSQL.Append(",CuentaAnticipoBanco");
            vSQL.Append(",CuentaAnticipoOtrosEgresos");
            vSQL.Append(",FacturaTipoComprobante");
            vSQL.Append(",CxCtipoComprobante");
            vSQL.Append(",CxPtipoComprobante");
            vSQL.Append(",CobranzaTipoComprobante");
            vSQL.Append(",PagoTipoComprobante");
            vSQL.Append(",MovimientoBancarioTipoComprobante");
            vSQL.Append(",AnticipoTipoComprobante");
            vSQL.Append(",CuentaCostoDeVenta");
            vSQL.Append(",CuentaInventario");
            vSQL.Append(",TipoContabilizacionInventario");
            vSQL.Append(",AgruparPorCuentaDeArticuloInven");
            vSQL.Append(",InventarioTipoComprobante");
            vSQL.Append(",CtaDePagosSueldos");
            vSQL.Append(",CtaDePagosSueldosBanco");
            vSQL.Append(",ContabIndividualPagosSueldos");
            vSQL.Append(",PagosSueldosTipoComprobante");
            vSQL.Append(",TipoContabilizacionDePagosSueldos");
            vSQL.Append(",EditarComprobanteDePagosSueldos");
            vSQL.Append(",EditarComprobanteAfterInsertCxC");
            vSQL.Append(",EditarComprobanteAfterInsertCxP");
            vSQL.Append(",EditarComprobanteAfterInsertCobranza");
            vSQL.Append(",EditarComprobanteAfterInsertPagos");
            vSQL.Append(",EditarComprobanteAfterInsertFactura");
            vSQL.Append(",EditarComprobanteAfterInsertResDia");
            vSQL.Append(",EditarComprobanteAfterInsertMovBan");
            vSQL.Append(",EditarComprobanteAfterInsertImpTraBan");
            vSQL.Append(",EditarComprobanteAfterInsertAnticipo");
            vSQL.Append(",EditarComprobanteAfterInsertInventario");
            vSQL.Append(",'N'");
            vSQL.Append(",NombreOperador");
            vSQL.Append(",''");
            vSQL.Append(",'0'");
            vSQL.Append(",''");
            vSQL.Append(",'S'");
            vSQL.Append(",''");
            vSQL.Append(",''");
            vSQL.Append(",''");
            vSQL.Append(",''");
            vSQL.Append(",0");
            vSQL.Append(",''");
            vSQL.Append(",''");
            vSQL.Append(",''");
            vSQL.Append(",'S'");
            vSQL.Append(",FechaUltimaModificacion");
            vSQL.Append(" FROM ReglasDeContabilizacion");
            insTrn.Execute(vSQL.ToString(),90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboReglasDeContabilizacion();
        }


        #endregion

        #region Migrar FormaDelCobro
        private void MigrarFormaDelCobro() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Saw.FormaDelCobro (Codigo, Nombre, TipoDePago) SELECT Codigo, Nombre, TipoDePago FROM FormaDelCobro";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboFormaDelCobro();
        }

        #endregion
        #region Migrar Vehiculo
        private void MigrarVehiculo() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Saw.Vehiculo (ConsecutivoCompania, Consecutivo, Placa, serialVIN, NombreModelo, Ano, CodigoColor, CodigoCliente, NumeroPoliza, SerialMotor, NombreOperador, FechaUltimaModificacion) SELECT ConsecutivoCompania, Consecutivo, Placa, serialVIN, NombreModelo, Ano, CodigoColor, CodigoCliente, NumeroPoliza, SerialMotor, NombreOperador, FechaUltimaModificacion FROM (SELECT ConsecutivoCompania, Placa, serialVIN, NombreModelo, Ano, CodigoColor, CodigoCliente, NumeroPoliza, SerialMotor, NombreOperador, FechaUltimaModificacion, ROW_NUMBER() OVER(PARTITION BY ConsecutivoCompania ORDER BY ConsecutivoCompania desc) AS Consecutivo FROM  Vehiculo )  as T";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboVehiculo();
        }
        #endregion

        #region Migrar ParametrosContables

        private void MigrarParametrosContables() {
            MigrarParametrosGen();
            MigrarDatosDeParametrosCia();
            MigrarParametrosConciliacion();
        }

        private void MigrarParametrosGen() {
            insTrn.StartTransaction();
            string vUpdSql = "UPDATE Contab.ParametrosGen SET ";
            vUpdSql = vUpdSql + "PermitirComprobantesDescuadrados = PARAMETROS.PermitirComprobantesDescuadrados";
            vUpdSql = vUpdSql + ", PermitirAsientosEnCero = PARAMETROS.PermitirAsientosEnCero";
            vUpdSql = vUpdSql + ", RepetirDescripcionComprobanteEnElAsiento = PARAMETROS.RepetirDescripcionComprobanteEnElAsiento";
            vUpdSql = vUpdSql + ", RepetirDescripcionUltimoAsiento = PARAMETROS.RepetirDescripcionUltimoAsiento";
            vUpdSql = vUpdSql + ", RepetirNumReferenciaUltimoAsiento = PARAMETROS.RepetirNumReferenciaUltimoAsiento";
            vUpdSql = vUpdSql + ", FormaDeEscogerCompania = PARAMETROS.FormaDeEscogerCompania";
            vUpdSql = vUpdSql + ", EscogerCompaniaAlEntrar = PARAMETROS.EscogerCompaniaAlEntrar";
            vUpdSql = vUpdSql + ", EscogerUltimaCompaniaUsada = PARAMETROS.EscogerUltimaCompaniaUsada";
            vUpdSql = vUpdSql + ", ImprimirDatosDeAuxiliaresYCentros = PARAMETROS.ImprimirDatosDeAuxiliaresYCentros";
            vUpdSql = vUpdSql + ", ImprimirSignoMenosConNumerosNegativos = PARAMETROS.ImprimirSignoMenosConNumerosNegativos";
            vUpdSql = vUpdSql + ", ImprimirComprobanteDespuesDeInsertar = PARAMETROS.ImprimirComprobanteDespuesDeInsertar";
            vUpdSql = vUpdSql + ", RepetirFechaDeReferenciaDelUltimoAsiento = PARAMETROS.RepetirFechaDeReferenciaDelUltimoAsiento";
            vUpdSql = vUpdSql + ", VerAuditoriaDeComprobante = PARAMETROS.VerAuditoriaDeComprobante";
            vUpdSql = vUpdSql + ", NombreDiarioDeComprobante = PARAMETROS.NombreDiarioDeComprobante";
            vUpdSql = vUpdSql + ", NombreBalanceDeComprobacion = PARAMETROS.NombreBalanceDeComprobacion";
            vUpdSql = vUpdSql + ", NombreMovimientoDeUnaCuenta = PARAMETROS.NombreMovimientoDeUnaCuenta";
            vUpdSql = vUpdSql + ", NombreMayorAnaliticoDetallado = PARAMETROS.NombreMayorAnaliticoDetallado";
            vUpdSql = vUpdSql + ", NombreOperador = " + _CurrentUserNameAsSqlValue;
            vUpdSql = vUpdSql + ", FechaUltimaModificacion = " + _TodayAsSqlValue;
            vUpdSql = vUpdSql + " FROM Contab.ParametrosGen";
            vUpdSql = vUpdSql + " INNER JOIN PARAMETROS";
            vUpdSql = vUpdSql + " ON Contab.ParametrosGen.Secuencial = PARAMETROS.Secuencial";
            insTrn.Execute(vUpdSql,-1);
            insTrn.CommitTransaction();
            if(BorrarTablaAnterior("dbo.PARAMETROS")) {
            }
        }

        private void MigrarDatosDeParametrosCia() {
            insTrn.StartTransaction();
            string vUpdSql = "UPDATE Contab.ParametrosGen SET";
            vUpdSql = vUpdSql + " FormaDeValidarCentroDeCostos = parametrosCiaWco.FormaDeValidarCentroDeCostos";
            vUpdSql = vUpdSql + ", RepetirFechaDocumentoAnteriorInsertandoComprobante = parametrosCiaWco.RepetirFechaDocumentoAnteriorInsertandoComprobante";
            vUpdSql = vUpdSql + ", AgregarDatosParaFirmaEnInforme = parametrosCiaWco.AgregarDatosParaFirmaEnInforme";
            vUpdSql = vUpdSql + ", MostrarCuentasTituloYMovimiento = parametrosCiaWco.MostrarCuentasTituloYMovimiento";
            vUpdSql = vUpdSql + " FROM Contab.ParametrosGen";
            vUpdSql = vUpdSql + " CROSS JOIN parametrosCiaWco";
            insTrn.Execute(vUpdSql,-1);
            insTrn.CommitTransaction();
        }

        #region Migrar Auxiliar
        private void MigrarAuxiliar() {
            new Galac.Contab.Dal.WinCont.clsAuxiliarMD().MigrarAuxiliar();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboAuxiliar();
        }
        #endregion Migrar Auxiliar
        private void MigrarParametrosConciliacion() {
            insTrn.StartTransaction();
            string vSql = "SET DATEFORMAT dmy ";
            vSql = "INSERT INTO Contab.ParametrosConciliacion (";
            vSql = vSql + "ConsecutivoCompania, UsaFechaInicioConciliacion, FechaDeInicioConciliacion, NombreOperador, FechaUltimaModificacion,UsaLeyCosto) ";
            vSql = vSql + "SELECT ConsecutivoCompania, UsaFechaInicioConciliacion, FechaDeInicioConciliacion, " + _CurrentUserNameAsSqlValue + ", " + _TodayAsSqlValue + ",'N' FROM ParametrosCiaWco";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            if(BorrarTablaAnterior("dbo.ParametrosCiaWco")) {
            }
        }
        #endregion Migrar ParametrosContables

        #region Migrar Almacen
        private void MigrarAlmacen() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Saw.Almacen (ConsecutivoCompania, Consecutivo, Codigo, NombreAlmacen, TipoDeAlmacen, ConsecutivoCliente, CodigoCc, Descripcion, NombreOperador, FechaUltimaModificacion) " +
            "SELECT ConsecutivoCompania, Consecutivo, Codigo, NombreAlmacen, TipoDeAlmacen, ConsecutivoCliente, CodigoCc, Descripcion, NombreOperador, FechaUltimaModificacion " +
            "FROM (SELECT Almacen.ConsecutivoCompania, Almacen.Codigo, Almacen.NombreAlmacen, Almacen.TipoDeAlmacen, Cliente.Consecutivo as ConsecutivoCliente, Almacen.CodigoCc, Almacen.Descripcion, Almacen.NombreOperador, Almacen.FechaUltimaModificacion, ROW_NUMBER() OVER (PARTITION BY Almacen.ConsecutivoCompania ORDER BY Almacen.FechaUltimaModificacion desc) AS Consecutivo FROM Almacen INNER JOIN Cliente ON Almacen.CodigoDelCliente = Cliente.Codigo and Almacen.ConsecutivoCompania = Cliente.ConsecutivoCompania) as T";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboAlmacen();
        }
        #endregion
        #region Migrar CuentaBancaria
        private void MigrarCuentaBancaria() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Saw.CuentaBancaria (ConsecutivoCompania, Codigo, Status, NumeroCuenta, NombreCuenta,CodigoBanco, NombreSucursal, TipoCtaBancaria, ManejaDebitoBancario, ManejaCreditoBancario, SaldoDisponible, NombreDeLaMoneda, NombrePlantillaCheque, CuentaContable, CodigoMoneda, NombreOperador, FechaUltimaModificacion,EsCajaChica) SELECT ConsecutivoCompania, Codigo, Status, NumeroCuenta, NombreCuenta,CodigoBanco, NombreSucursal, TipoCtaBancaria, ManejaDebitoBancario, ManejaCreditoBancario, SaldoDisponible, NombreDeLaMoneda, NombrePlantillaCheque, CuentaContable, CodigoMoneda, NombreOperador, FechaUltimaModificacion,'N' FROM CuentaBancaria";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboCuentaBancaria();
        }
        #endregion
        #region Migrar Parametros
        private void MigrarSettValueByCompany() {
            XElement vRecordsCompania = ListadoCompania();
            IList<int> vCompaniaToList = CompaniaToList(vRecordsCompania);
            if(vCompaniaToList != null && vCompaniaToList.Count > 0) {
                MigrarParametrosCompania(vCompaniaToList);
                MigrarParametrosAdministrativo(vCompaniaToList);
            }
        }
        private void MigrarParametrosCompania(IList<int> valCompaniaToList) {
            XElement vRecords = Parametrosactuales(SqlParametrosActuales(ColumnasParametrosCompania(),"ParametrosCompania"));
            MigraParaMetrosActuales(valCompaniaToList,ColumnasToList(ColumnasParametrosCompania()),vRecords,false);
        }
        private void MigrarParametrosAdministrativo(IList<int> valCompaniaToList) {
            XElement vRecords = Parametrosactuales(SqlParametrosActuales(ColumnasParametrosAdministrativos(),"ParametrosAdministrativo"));
            MigraParaMetrosActuales(valCompaniaToList,ColumnasToList(ColumnasParametrosAdministrativos()),vRecords,true);
        }
        #endregion

        #region Migrar ConceptoBancario
        private void MigrarConceptoBancario() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Adm.ConceptoBancario (Consecutivo, Codigo, Descripcion, Tipo, NombreOperador, FechaUltimaModificacion) SELECT Consecutivo, Codigo, Descripcion, Tipo, NombreOperador, FechaUltimaModificacion FROM (SELECT  Codigo, Descripcion, Tipo, NombreOperador, FechaUltimaModificacion, ROW_NUMBER() OVER (ORDER BY Codigo desc) AS Consecutivo FROM ConceptoBancario) as T";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboConceptoBancario();
        }
        #endregion

        #region Migrar TarifaN2
        private void MigrarTarifaN2() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Comun.TarifaN2 (Consecutivo, UTDesde, UTHasta, Porcentaje, Sustraendo, NombreOperador, FechaUltimaModificacion) SELECT Consecutivo, UTDesde, UTHasta, Porcentaje, Sustraendo, NombreOperador, FechaUltimaModificacion FROM TarifaN2";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboTarifaN2();
        }
        #endregion

        #region Migrar TablaRetencion
        private void MigrarTablaRetencion() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Comun.TablaRetencion (TipoDePersona, Codigo, CodigoSeniat, TipoDePago, Comentarios, BaseImponible, Tarifa, ParaPagosMayoresDe, FechaAplicacion, Sustraendo, AcumulaParaPJND, SecuencialDePlantilla, CodigoMoneda, FechaDeInicioDeVigencia, NombreOperador, FechaUltimaModificacion) SELECT TipoDePersona, Codigo, CodigoSeniat, TipoDePago, Comentarios, BaseImponible, Tarifa, ParaPagosMayoresDe, FechaAplicacion, Sustraendo, AcumulaParaPJND, SecuencialDePlantilla, CodigoMoneda, FechaDeInicioDeVigencia, NombreOperador, FechaUltimaModificacion FROM TablaRetencion";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboTablaRetencion();
        }
        #endregion

        #region Migrar Proveedor
        private void MigrarProveedor() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Adm.Proveedor (ConsecutivoCompania,CodigoProveedor,NombreProveedor,Contacto,NumeroRIF,NumeroNit,TipoDePersona,CodigoRetencionUsual,Telefonos,Direccion,Fax,Email,TipodeProveedor,TipoDeProveedorDeLibrosFiscales,PorcentajeRetencionIva,CuentaContableCxp,CuentaContableGastos,CuentaContableAnticipo,CodigoLote,Beneficiario,UsarBeneficiarioImpCheq,TipoDocumentoIdentificacion,EsAgenteDeRetencionIva,ApellidoPaterno,ApellidoMaterno,Nombre,NumeroCuentaBancaria,CodigoContribuyente,NombreOperador,FechaUltimaModificacion,Consecutivo) ";
            vSql = vSql + " SELECT ConsecutivoCompania,CodigoProveedor,NombreProveedor,Contacto,NumeroRIF,NumeroNit,TipoDePersona,CodigoRetencionUsual,Telefonos,Direccion,Fax,Email,TipodeProveedor,TipoDeProveedorDeLibrosFiscales,PorcentajeRetencionIva,CuentaContableCxp,CuentaContableGastos,CuentaContableAnticipo,CodigoLote,Beneficiario,UsarBeneficiarioImpCheq,TipoDocumentoIdentificacion,EsAgenteDeRetencionIva,ApellidoPaterno,ApellidoMaterno,Nombre,NumeroCuentaBancaria,CodigoContribuyente,NombreOperador,FechaUltimaModificacion,ROW_NUMBER() OVER (PARTITION BY ConsecutivoCompania ORDER BY FechaUltimaModificacion desc) AS Consecutivo FROM Proveedor";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboProveedor();
        }
        #endregion

        #region Migrar Cambio
        private void MigrarCambio() {
            insTrn.StartTransaction();
            string vSql = "INSERT INTO Comun.Cambio (CodigoMoneda,FechaDeVigencia,CambioAMonedaLocal,CambioAMonedaLocalVenta,NombreOperador,FechaUltimaModificacion) SELECT CodigoMoneda, FechaDeVigencia, CambioAbolivares, CambioAbolivares, NombreOperador, FechaUltimaModificacion FROM dbo.Cambio";
            insTrn.Execute(vSql,90);
            insTrn.CommitTransaction();
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboCambio();
        }

        #endregion

        #region Migrar Data Compra y Orden De Compra

        private void MigrarCompraOrdenDeCompra() {
            MigrarCompra();
            MigrarOrdenDeCompra();
        }

        private void MigrarCompra() {
            StringBuilder vSQL = new StringBuilder();
            LibDataScope vDb = new LibDataScope();
            vSQL.AppendLine("INSERT INTO Adm.Compra(ConsecutivoCompania, Consecutivo,Serie, Numero,");
            vSQL.AppendLine("   Fecha, ConsecutivoProveedor, ConsecutivoAlmacen, Moneda, CodigoMoneda, ");
            vSQL.AppendLine("   CambioABolivares, GenerarCXP, UsaSeguro, TipoDeDistribucion, ");
            vSQL.AppendLine("   TasaAduanera, TasaDolar, ValorUT, TotalRenglones, TotalOtrosGastos, ");
            vSQL.AppendLine("   TotalCompra, Comentarios, StatusCompra, TipoDeCompra, ");
            vSQL.AppendLine("   FechaDeAnulacion, ConsecutivoOrdenDeCompra, NumeroDeOrdenDeCompra, ");
            vSQL.AppendLine("   NoFacturaNotaEntrega, TipoDeCompraParaCxP, ");
            vSQL.AppendLine("   NombreOperador, FechaUltimaModificacion)");
            vSQL.AppendLine("SELECT Compra.ConsecutivoCompania, ROW_NUMBER()OVER(PARTITION BY Compra.ConsecutivoCompania ORDER BY Compra.ConsecutivoCompania, NumeroSecuencial ASC)AS Consecutivo, ROW_NUMBER()OVER(PARTITION BY Compra.ConsecutivoCompania ORDER BY Compra.ConsecutivoCompania, NumeroSecuencial ASC) AS Serie, Numero, ");
            vSQL.AppendLine("   Fecha, adm.Proveedor.Consecutivo AS ConsecutivoProveedor, ConsecutivoAlmacen, Moneda, CodigoMoneda, ");
            vSQL.AppendLine("   CambioAbolivares, GenerarCxp, 'N' AS UsaSeguro, CASE WHEN DistribuirGastos = '0' THEN '1' ELSE '2' END AS TipoDeDistribucion, ");
            vSQL.AppendLine("   0 AS TasaAduanera, 0 AS TasaDolar, 0 AS ValorUT, TotalRenglones, OtrosGastos, ");
            vSQL.AppendLine("   TotalCompra, Comentarios, StatusCompra, '0', ");
            vSQL.AppendLine("   FechadeAnulacion, 0 AS ConsecutivoOrdenDeCompra, '' AS NumeroDeOrdenDeCompra, ");
            vSQL.AppendLine("   NoFacturaNotaEntrega, CASE WHEN TipoDeCompraParaCxp='' THEN '1'  ELSE TipoDeCompraParaCxp END,");
            vSQL.AppendLine("   Compra.NombreOperador, Compra.FechaUltimaModificacion");
            vSQL.AppendLine("FROM Compra");
            vSQL.AppendLine("   INNER JOIN adm.Proveedor ON adm.Proveedor.ConsecutivoCompania = Compra.ConsecutivoCompania AND adm.Proveedor.CodigoProveedor = Compra.CodigoProveedor");
            vSQL.AppendLine("WHERE EsUnaOrdenDeCompra = 'N'");
            vDb.ExecuteWithScope(vSQL.ToString());
            MigrarReglonCompra();
            MigrarReglonCompraXSerial();
        }
        private void MigrarReglonCompra() {
            StringBuilder vSQL = new StringBuilder();
            LibDataScope vDb = new LibDataScope();
            vSQL.AppendLine(";WITH CTE_CompraConsecutivo(ConsecutivoCompania, Consecutivo, NumeroSecuencial, OtrosGastos, PorcentajeDeDistribucion, EsUnaOrdenDeCompra)");
            vSQL.AppendLine("AS(");
            vSQL.AppendLine("   SELECT Compra.ConsecutivoCompania, ROW_NUMBER()OVER(PARTITION BY Compra.ConsecutivoCompania ORDER BY Compra.ConsecutivoCompania, NumeroSecuencial ASC)AS Consecutivo, ");
            vSQL.AppendLine("      NumeroSecuencial, OtrosGastos, PorcentajeDeDistribucion,EsUnaOrdenDeCompra");
            vSQL.AppendLine("   FROM Compra WHERE EsUnaOrdenDeCompra = 'N')");
            vSQL.AppendLine("INSERT INTO Adm.CompraDetalleArticuloInventario	(ConsecutivoCompania, ConsecutivoCompra, Consecutivo, ");
            vSQL.AppendLine("   CodigoArticulo, Cantidad, PrecioUnitario, CantidadRecibida, PorcentajeDeDistribucion, ");
            vSQL.AppendLine("   MontoDistribucion, PorcentajeSeguro)");
            vSQL.AppendLine("SELECT RenglonCompra.ConsecutivoCompania, CTE_CompraConsecutivo.Consecutivo AS ConsecutivCompra, ConsecutivoRenglon AS Consecutivo, ");
            vSQL.AppendLine("   CodigoArticulo, Cantidad, CostoUnitario AS PrecioUnitario, CantidadRecibida, PorcentajeDeDistribucion,");
            vSQL.AppendLine("   OtrosGastos AS MontoDistribucion, 0 AS PorcentajeSeguro");
            vSQL.AppendLine("FROM CTE_CompraConsecutivo");
            vSQL.AppendLine("   INNER JOIN RenglonCompra ");
            vSQL.AppendLine("     ON RenglonCompra.ConsecutivoCompania = CTE_CompraConsecutivo.ConsecutivoCompania AND");
            vSQL.AppendLine("	RenglonCompra.NumeroSecuencialCompra = CTE_CompraConsecutivo.NumeroSecuencial");
            vSQL.AppendLine("WHERE CTE_CompraConsecutivo.EsUnaOrdenDeCompra = 'N'");
            vDb.ExecuteWithScope(vSQL.ToString());
        }
        private void MigrarReglonCompraXSerial() {
            StringBuilder vSQL = new StringBuilder();
            LibDataScope vDb = new LibDataScope();
            vSQL.AppendLine(";WITH CTE_CompraConsecutivo(ConsecutivoCompania, Consecutivo, NumeroSecuencial, OtrosGastos, PorcentajeDeDistribucion, EsUnaOrdenDeCompra)");
            vSQL.AppendLine("AS(");
            vSQL.AppendLine("   SELECT Compra.ConsecutivoCompania, ROW_NUMBER()OVER(PARTITION BY Compra.ConsecutivoCompania ORDER BY Compra.ConsecutivoCompania, NumeroSecuencial ASC)AS Consecutivo, ");
            vSQL.AppendLine("      NumeroSecuencial, OtrosGastos, PorcentajeDeDistribucion,EsUnaOrdenDeCompra");
            vSQL.AppendLine("   FROM Compra WHERE EsUnaOrdenDeCompra = 'N')");
            vSQL.AppendLine("INSERT INTO Adm.CompraDetalleSerialRollo(ConsecutivoCompania, ConsecutivoCompra, Consecutivo, ");
            vSQL.AppendLine("   CodigoArticulo, Serial, Rollo, Cantidad)");
            vSQL.AppendLine("SELECT RenglonCompraXSerial.ConsecutivoCompania, CTE_CompraConsecutivo.Consecutivo AS ConsecutivoCompra, ");
            vSQL.AppendLine("ROW_NUMBER()OVER(PARTITION BY RenglonCompraXSerial.NumeroSecuencialCompra ORDER BY RenglonCompraXSerial.ConsecutivoCompania,ConsecutivoRenglon ASC)AS Consecutivo, ");
            vSQL.AppendLine("   CodigoArticulo, Serial, Rollo, Cantidad");
            vSQL.AppendLine("FROM CTE_CompraConsecutivo");
            vSQL.AppendLine("   INNER JOIN RenglonCompraXSerial ON RenglonCompraXSerial.ConsecutivoCompania = CTE_CompraConsecutivo.ConsecutivoCompania AND");
            vSQL.AppendLine("   RenglonCompraXSerial.NumeroSecuencialCompra = CTE_CompraConsecutivo.NumeroSecuencial");
            vSQL.AppendLine("WHERE CTE_CompraConsecutivo.EsUnaOrdenDeCompra = 'N'");
            vDb.ExecuteWithScope(vSQL.ToString());
        }
        private void MigrarOrdenDeCompra() {
            StringBuilder vSQL = new StringBuilder();
            LibDataScope vDb = new LibDataScope();
            vSQL.AppendLine("INSERT INTO Adm.OrdenDeCompra (ConsecutivoCompania ,Consecutivo ,Serie ,Numero ");
            vSQL.AppendLine("   ,Fecha,ConsecutivoProveedor,Moneda,CodigoMoneda,CambioABolivares");
            vSQL.AppendLine("   ,TotalRenglones ,TotalCompra ,TipoDeCompra ,Comentarios ,StatusOrdenDeCompra,FechaDeAnulacion, CondicionesDePago");
            vSQL.AppendLine("   ,NombreOperador ,FechaUltimaModificacion)");
            vSQL.AppendLine("SELECT Compra.ConsecutivoCompania, ROW_NUMBER() OVER(PARTITION BY Compra.ConsecutivoCompania ORDER BY Compra.ConsecutivoCompania, NumeroSecuencial ASC) AS Consecutivo, '' AS Serie, Numero, ");
            vSQL.AppendLine("   Fecha, adm.Proveedor.Consecutivo AS ConsecutivoProveedor, Moneda,CodigoMoneda, CambioAbolivares, 	");
            vSQL.AppendLine("   TotalRenglones, TotalCompra, '0' AS TipoDeCompra, Comentarios,  StatusCompra AS StatusOrdenDeCompra,FechadeAnulacion, 0 AS CondicionesDePago,");
            vSQL.AppendLine("   Compra.NombreOperador, Compra.FechaUltimaModificacion");
            vSQL.AppendLine("FROM Compra");
            vSQL.AppendLine("   INNER JOIN adm.Proveedor ON adm.Proveedor.ConsecutivoCompania = Compra.ConsecutivoCompania  AND adm.Proveedor.CodigoProveedor   = Compra.CodigoProveedor");
            vSQL.AppendLine("WHERE EsUnaOrdenDeCompra = 'S'");
            vDb.ExecuteWithScope(vSQL.ToString());
            MigraOrdenDeCompraRenglon();
        }

        private void MigraOrdenDeCompraRenglon() {
            StringBuilder vSQL = new StringBuilder();
            LibDataScope vDb = new LibDataScope();
            vSQL.AppendLine(";WITH CTE_CompraConsecutivo  (ConsecutivoCompania, Consecutivo, NumeroSecuencial, OtrosGastos, PorcentajeDeDistribucion, EsUnaOrdenDeCompra)");
            vSQL.AppendLine("AS(");
            vSQL.AppendLine("   SELECT Compra.ConsecutivoCompania, ROW_NUMBER() OVER(PARTITION BY Compra.ConsecutivoCompania ORDER BY Compra.ConsecutivoCompania, NumeroSecuencial ASC) AS Consecutivo, NumeroSecuencial, OtrosGastos, PorcentajeDeDistribucion, EsUnaOrdenDeCompra");
            vSQL.AppendLine("   FROM Compra WHERE EsUnaOrdenDeCompra = 'S')");
            vSQL.AppendLine("INSERT INTO Adm.OrdenDeCompraDetalleArticuloInventario (ConsecutivoCompania,ConsecutivoOrdenDeCompra,Consecutivo");
            vSQL.AppendLine("   ,CodigoArticulo,Cantidad,CostoUnitario,CantidadRecibida, DescripcionArticulo)");
            vSQL.AppendLine("SELECT RenglonCompra.ConsecutivoCompania, CTE_CompraConsecutivo.Consecutivo AS ConsecutivoOrdenDeCompra, ConsecutivoRenglon AS Consecutivo, ");
            vSQL.AppendLine("   CodigoArticulo, Cantidad, RenglonCompra.CostoUnitario, CantidadRecibida, ArticuloInventario.Descripcion ");
            vSQL.AppendLine("FROM  CTE_CompraConsecutivo");
            vSQL.AppendLine("   INNER JOIN RenglonCompra ON RenglonCompra.ConsecutivoCompania = CTE_CompraConsecutivo.ConsecutivoCompania AND RenglonCompra.NumeroSecuencialCompra = CTE_CompraConsecutivo.NumeroSecuencial");
            vSQL.AppendLine("   INNER JOIN ArticuloInventario ON RenglonCompra.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania  AND RenglonCompra.CodigoArticulo = ArticuloInventario.Codigo ");
            vSQL.AppendLine("WHERE CTE_CompraConsecutivo.EsUnaOrdenDeCompra = 'S'");
            vDb.ExecuteWithScope(vSQL.ToString());
        }
        #endregion Compra
        #region Carga Inicial
        private void MigrarCargaInicial() {
            StringBuilder vSQL = new StringBuilder();
            LibDataScope vDb = new LibDataScope();
            vSQL.AppendLine(" INSERT INTO Adm.CargaInicial (ConsecutivoCompania ,Consecutivo, Fecha, Existencia, Costo, CodigoArticulo, EsCargaInicial)");
            vSQL.AppendLine(" SELECT ConsecutivoCompania,  ROW_NUMBER() OVER(PARTITION BY ConsecutivoCompania ORDER BY ConsecutivoCompania) AS Consecutivo, Fecha, Existencia, Costo, Codigo AS CodigoArticulo, 'S' AS EsCargaInicial  FROM CierreCostoDelPeriodo");
            vDb.ExecuteWithScope(vSQL.ToString());
        }
        #endregion
        #region Migracion Caja
        private void MigrarCaja() {
            StringBuilder vSQL = new StringBuilder();
            LibDataScope vDb = new LibDataScope();
            vSQL.AppendLine("INSERT INTO Adm.Caja");
            vSQL.AppendLine("(ConsecutivoCompania ,");
            vSQL.AppendLine("Consecutivo ,");
            vSQL.AppendLine("NombreCaja ,");
            vSQL.AppendLine("UsaGaveta ,");
            vSQL.AppendLine("Puerto ,");
            vSQL.AppendLine("Comando,");
            vSQL.AppendLine("PermitirAbrirSinSupervisor,");
            vSQL.AppendLine("UsaAccesoRapido,");
            vSQL.AppendLine("UsaMaquinaFiscal,");
            vSQL.AppendLine("FamiliaImpresoraFiscal,"); 
            vSQL.AppendLine("ModeloDeMaquinaFiscal,");
            vSQL.AppendLine("SerialDeMaquinaFiscal,");
            vSQL.AppendLine("PuertoMaquinaFiscal,");
            vSQL.AppendLine("AbrirGavetaDeDinero,");
            vSQL.AppendLine("UltimoNumeroCompFiscal,");
            vSQL.AppendLine("UltimoNumeroNCFiscal ,");
            vSQL.AppendLine("TipoConexion,");
            vSQL.AppendLine("IpParaConexion,");
            vSQL.AppendLine("MascaraSubred,Gateway,");
            vSQL.AppendLine("PermitirDescripcionDelArticuloExtendida,");
            vSQL.AppendLine("PermitirNombreDelClienteExtendido ,");
            vSQL.AppendLine("UsarModoDotNet,");
            vSQL.AppendLine("NombreOperador,");
            vSQL.AppendLine("FechaUltimaModificacion)");
            vSQL.AppendLine("SELECT");
            vSQL.AppendLine("ConsecutivoCompania,");
            vSQL.AppendLine("ConsecutivoCaja,"); 
            vSQL.AppendLine("NombreCaja ,");
            vSQL.AppendLine("UsaGaveta ,");
            vSQL.AppendLine("Puerto ,");
            vSQL.AppendLine("Comando,");
            vSQL.AppendLine("PermitirAbrirSinSupervisor,");
            vSQL.AppendLine("UsaAccesoRapido,");
            vSQL.AppendLine("UsaMaquinaFiscal,");           
            vSQL.AppendLine("CASE WHEN (ModeloDeMaquinaFiscal = '' OR ModeloDeMaquinaFiscal IS NULL) THEN '0'");
            vSQL.AppendLine(" WHEN (ModeloDeMaquinaFiscal IN('5','7','8','9','=','@','A','B','C','D','E')) THEN '0' ");
            vSQL.AppendLine(" WHEN (ModeloDeMaquinaFiscal IN('?')) THEN '1' ");
            vSQL.AppendLine(" WHEN (ModeloDeMaquinaFiscal IN('4','<')) THEN '2' ");
            vSQL.AppendLine(" WHEN (ModeloDeMaquinaFiscal IN('0','1','2','3','6')) THEN '3' ");
            vSQL.AppendLine(" WHEN (ModeloDeMaquinaFiscal IN('>')) THEN '4' ");
            vSQL.AppendLine(" WHEN (ModeloDeMaquinaFiscal IN(':',';','F')) THEN '5' ");
            vSQL.AppendLine(" END  AS FamiliaImpresoraFiscal, ");
            vSQL.AppendLine("(CASE WHEN ModeloDeMaquinaFiscal = '' OR ModeloDeMaquinaFiscal IS NULL THEN '0' ELSE ModeloDeMaquinaFiscal END) AS ModeloDeMaquinaFiscal,");
            vSQL.AppendLine("SerialDeMaquinaFiscal,");
            vSQL.AppendLine("(CASE WHEN PuertoMaquinaFiscal = '' OR PuertoMaquinaFiscal IS NULL THEN '1' ELSE PuertoMaquinaFiscal END) AS PuertoMaquinaFiscal,");
            vSQL.AppendLine("AbrirGavetaDeDinero,");
            vSQL.AppendLine("PrimerNumeroCompFiscal,");
            vSQL.AppendLine("ISNULL(NumeroNCFiscal,''),");
            vSQL.AppendLine("(CASE WHEN TipoConexion = '' OR TipoConexion IS NULL THEN '0' ELSE TipoConexion END) AS TipoConexion,");
            vSQL.AppendLine("ISNULL(IpParaConexion,''),");
            vSQL.AppendLine("ISNULL(MascaraSubred,''),");
            vSQL.AppendLine("ISNULL(Gateway,''),");
            vSQL.AppendLine("ISNULL(PermitirDescripcionDelArticuloExtendida,'N'),");
            vSQL.AppendLine("ISNULL(PermitirNombreDelClienteExtendido,'N'),");
            vSQL.AppendLine("ISNULL(UsarModoDotNet,'N'),");
            vSQL.AppendLine("NombreOperador,FechaUltimaModificacion ");
            vSQL.AppendLine("FROM dbo.Caja");
            vDb.ExecuteWithScope(vSQL.ToString());
        }

        private void MigrarCajaApertura() {
            StringBuilder vSQL = new StringBuilder();
            LibDataScope vDb = new LibDataScope();
            vSQL.AppendLine("INSERT INTO Adm.CajaApertura");
            vSQL.AppendLine("(ConsecutivoCompania ,");
            vSQL.AppendLine("Consecutivo,");
            vSQL.AppendLine("ConsecutivoCaja,");
            vSQL.AppendLine("NombreDelUsuario,");
            vSQL.AppendLine("MontoApertura,");
            vSQL.AppendLine("MontoCierre,");
            vSQL.AppendLine("MontoEfectivo,");
            vSQL.AppendLine("MontoTarjeta,");
            vSQL.AppendLine("MontoCheque,");
            vSQL.AppendLine("MontoDeposito,");
            vSQL.AppendLine("MontoAnticipo,");
            vSQL.AppendLine("Fecha,");
            vSQL.AppendLine("HoraApertura,");
            vSQL.AppendLine("HoraCierre,");
            vSQL.AppendLine("CajaCerrada,");
            vSQL.AppendLine("NombreOperador,");
            vSQL.AppendLine("FechaUltimaModificacion )");
            vSQL.AppendLine("SELECT ConsecutivoCompania,");
            vSQL.AppendLine("ROW_NUMBER() OVER(PARTITION BY ConsecutivoCompania,ConsecutivoCaja ORDER BY ConsecutivoCompania,ConsecutivoCaja ASC) AS Consecutivo, ");
            vSQL.AppendLine("ConsecutivoCaja,");
            vSQL.AppendLine("NombreDelUsuario,");
            vSQL.AppendLine("MontoApertura,");
            vSQL.AppendLine("MontoCierre,");
            vSQL.AppendLine("MontoEfectivo,");
            vSQL.AppendLine("MontoTarjeta,");
            vSQL.AppendLine("MontoCheque,");
            vSQL.AppendLine("0.00,");
            vSQL.AppendLine("0.00,");
            vSQL.AppendLine("Fecha,");
            vSQL.AppendLine("HoraApertura,");
            vSQL.AppendLine("HoraCierre,");
            vSQL.AppendLine("CajaCerrada,");
            vSQL.AppendLine("'Jefe' AS UserName,");
            vSQL.AppendLine("Fecha ");
            vSQL.AppendLine("FROM dbo.CajaApertura ");
            vSQL.AppendLine(" ORDER BY ConsecutivoCompania,ConsecutivoCaja ");
            vDb.ExecuteWithScope(vSQL.ToString());
        }
        #endregion

        #region Migrar Vendedor
        public void MigrarVendedorYDetalleComisiones() {            
            MigrarVendedor();
            MigrarVendedorDetalleComisiones();
        }
        public void MigrarVendedor() {
            QAdvSql vUtilSql = new QAdvSql("");
            StringBuilder vSQL = new StringBuilder();
            LibDataScope vDb = new LibDataScope();
            vSQL.AppendLine("INSERT INTO Adm.Vendedor (");
            vSQL.AppendLine("ConsecutivoCompania, ");
            vSQL.AppendLine("Consecutivo, ");
            vSQL.AppendLine("Codigo, ");
            vSQL.AppendLine("Nombre, ");
            vSQL.AppendLine("RIF, ");
            vSQL.AppendLine("StatusVendedor, ");
            vSQL.AppendLine("Direccion, ");
            vSQL.AppendLine("Ciudad, ");
            vSQL.AppendLine("ZonaPostal, ");
            vSQL.AppendLine("Telefono, ");
            vSQL.AppendLine("Fax, ");
            vSQL.AppendLine("Email, ");
            vSQL.AppendLine("Notas, ");
            vSQL.AppendLine("ComisionPorVenta, ");
            vSQL.AppendLine("ComisionPorCobro, ");
            vSQL.AppendLine("TopeInicialVenta1, ");
            vSQL.AppendLine("TopeFinalVenta1, ");
            vSQL.AppendLine("PorcentajeVentas1, ");
            vSQL.AppendLine("TopeFinalVenta2, ");
            vSQL.AppendLine("PorcentajeVentas2, ");
            vSQL.AppendLine("TopeFinalVenta3, ");
            vSQL.AppendLine("PorcentajeVentas3, ");
            vSQL.AppendLine("TopeFinalVenta4, ");
            vSQL.AppendLine("PorcentajeVentas4, ");
            vSQL.AppendLine("TopeFinalVenta5, ");
            vSQL.AppendLine("PorcentajeVentas5, ");
            vSQL.AppendLine("TopeInicialCobranza1, ");
            vSQL.AppendLine("TopeFinalCobranza1, ");
            vSQL.AppendLine("PorcentajeCobranza1, ");
            vSQL.AppendLine("TopeFinalCobranza2, ");
            vSQL.AppendLine("PorcentajeCobranza2, ");
            vSQL.AppendLine("TopeFinalCobranza3, ");
            vSQL.AppendLine("PorcentajeCobranza3, ");
            vSQL.AppendLine("TopeFinalCobranza4, ");
            vSQL.AppendLine("PorcentajeCobranza4, ");
            vSQL.AppendLine("TopeFinalCobranza5, ");
            vSQL.AppendLine("PorcentajeCobranza5, ");
            vSQL.AppendLine("UsaComisionPorVenta, ");
            vSQL.AppendLine("UsaComisionPorCobranza, ");
            vSQL.AppendLine("CodigoLote, ");
            vSQL.AppendLine("ConsecutivoRutaDeComercializacion,  ");
            vSQL.AppendLine("NombreOperador, ");
            vSQL.AppendLine("FechaUltimaModificacion) ");
            vSQL.AppendLine("SELECT ConsecutivoCompania, ");
            vSQL.AppendLine("ROW_NUMBER() OVER(PARTITION BY ConsecutivoCompania ORDER BY ConsecutivoCompania,Codigo ASC) AS Consecutivo, ");
            vSQL.AppendLine("Codigo, ");
            vSQL.AppendLine("Nombre, ");
            vSQL.AppendLine("RIF, ");
            vSQL.AppendLine("ISNULL(StatusVendedor, '0'), ");
            vSQL.AppendLine("ISNULL(Direccion, ''), ");
            vSQL.AppendLine("Ciudad, ");
            vSQL.AppendLine("ISNULL(ZonaPostal, ''), ");
            vSQL.AppendLine("ISNULL(Telefono, ''), ");
            vSQL.AppendLine("ISNULL(Fax, ''), ");
            vSQL.AppendLine("ISNULL(Email, ''), ");
            vSQL.AppendLine("ISNULL(Notas, ''), ");
            vSQL.AppendLine("ISNULL(ComisionPorVenta, '0'), ");
            vSQL.AppendLine("ISNULL(ComisionPorCobro, '0'), ");
            vSQL.AppendLine("ISNULL(TopeInicialVenta1, 0), ");
            vSQL.AppendLine("ISNULL(TopeFinalVenta1, '0'), ");
            vSQL.AppendLine("ISNULL(PorcentajeVentas1, '0'), ");
            vSQL.AppendLine("ISNULL(TopeFinalVenta2, '0'), ");
            vSQL.AppendLine("ISNULL(PorcentajeVentas2, '0'), ");
            vSQL.AppendLine("ISNULL(TopeFinalVenta3, '0'), ");
            vSQL.AppendLine("ISNULL(PorcentajeVentas3, '0'), ");
            vSQL.AppendLine("ISNULL(TopeFinalVenta4, '0'), ");
            vSQL.AppendLine("ISNULL(PorcentajeVentas4, '0'), ");
            vSQL.AppendLine("ISNULL(TopeFinalVenta5, '0'), ");
            vSQL.AppendLine("ISNULL(PorcentajeVentas5, '0'), ");
            vSQL.AppendLine("ISNULL(TopeInicialCobranza1, '0'), ");
            vSQL.AppendLine("ISNULL(TopeFinalCobranza1, '0'), ");
            vSQL.AppendLine("ISNULL(PorcentajeCobranza1, '0'), ");
            vSQL.AppendLine("ISNULL(TopeFinalCobranza2, '0'), ");
            vSQL.AppendLine("ISNULL(PorcentajeCobranza2, '0'), ");
            vSQL.AppendLine("ISNULL(TopeFinalCobranza3, '0'), ");
            vSQL.AppendLine("ISNULL(PorcentajeCobranza3, '0'), ");
            vSQL.AppendLine("ISNULL(TopeFinalCobranza4, '0'), ");
            vSQL.AppendLine("ISNULL(PorcentajeCobranza4, '0'), ");
            vSQL.AppendLine("ISNULL(TopeFinalCobranza5, '0'), ");
            vSQL.AppendLine("ISNULL(PorcentajeCobranza5, '0'), ");
            vSQL.AppendLine("ISNULL(UsaComisionPorVenta, 'N'), ");
            vSQL.AppendLine("ISNULL(UsaComisionPorCobranza, 'N'), ");
            vSQL.AppendLine("ISNULL(CodigoLote, ''), ");
            vSQL.AppendLine("'1', ");
            vSQL.AppendLine("NombreOperador, ");
            vSQL.AppendLine("FechaUltimaModificacion ");
            vSQL.AppendLine("FROM dbo.Vendedor ");
            vSQL.AppendLine("ORDER BY ConsecutivoCompania, Codigo ");
            vDb.ExecuteWithScope(vSQL.ToString());
        }
        public void MigrarVendedorDetalleComisiones() {
            QAdvSql vUtilSql = new QAdvSql("");
            StringBuilder vSQL = new StringBuilder();
            LibDataScope vDb = new LibDataScope();
            vSQL.AppendLine("INSERT INTO Adm.VendedorDetalleComisiones (");
            vSQL.AppendLine("ConsecutivoCompania, ");
            vSQL.AppendLine("ConsecutivoVendedor, ");           
            vSQL.AppendLine("Consecutivo, ");
            vSQL.AppendLine("NombreDeLineaDeProducto, ");
            vSQL.AppendLine("TipoDeComision, ");
            vSQL.AppendLine("Monto, ");
            vSQL.AppendLine("Porcentaje ) ");
            vSQL.AppendLine("SELECT dbo.RenglonComisionesDeVendedor.ConsecutivoCompania, ");
            vSQL.AppendLine("Adm.Vendedor.Consecutivo AS ConsecutivoVendedor, ");            
            vSQL.AppendLine("dbo.RenglonComisionesDeVendedor.ConsecutivoRenglon, ");
            vSQL.AppendLine("dbo.RenglonComisionesDeVendedor.NombreDeLineaDeProducto, ");
            vSQL.AppendLine("dbo.RenglonComisionesDeVendedor.TipoDeComision, ");
            vSQL.AppendLine("dbo.RenglonComisionesDeVendedor.Monto, ");
            vSQL.AppendLine("dbo.RenglonComisionesDeVendedor.Porcentaje ");
            vSQL.AppendLine("FROM dbo.RenglonComisionesDeVendedor ");
            vSQL.AppendLine("INNER JOIN Adm.Vendedor ");
            vSQL.AppendLine("ON dbo.RenglonComisionesDeVendedor.ConsecutivoCompania = Adm.Vendedor.ConsecutivoCompania AND ");
            vSQL.AppendLine(" dbo.RenglonComisionesDeVendedor.CodigoVendedor = Adm.Vendedor.Codigo ");
            vSQL.AppendLine("ORDER BY dbo.RenglonComisionesDeVendedor.ConsecutivoCompania, dbo.RenglonComisionesDeVendedor.CodigoVendedor, dbo.RenglonComisionesDeVendedor.ConsecutivoRenglon ");
            vDb.ExecuteWithScope(vSQL.ToString());
        }
        #endregion

        #region Migrar Data
        public bool MigrarData() {
            bool vResult = true;
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"Usuario")) {
                MigrarUsuarios();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"Ciudad")) {
                MigrarCiudad();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"SectorDeNegocio")) {
                MigrarSectorDeNegocios();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"Banco")) {
                MigrarBanco();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"Pais")) {
                MigrarPais();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"Ciiu")) {
                MigrarCiiu();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"Categoria")) {
                MigrarCategoria();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"UnidadDeVenta")) {
                MigrarUnidadDeVenta();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"UrbanizacionZP")) {
                MigrarUrbanizacionZP();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"ZonaCobranza")) {
                MigrarZonaCobranza();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"MaquinaFiscal")) {
                MigrarMaquinaFiscal();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"PropAnalisisVenc")) {
                MigrarPropAnalisisVenc();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"Talla")) {
                MigrarTalla();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"Color")) {
                MigrarColor();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"NotaFinal")) {
                MigrarNotaFinal();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"TipoProveedor")) {
                MigrarTipoProveedor();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"ReglasDeContabilizacion")) {
                MigrarReglasDeContabilizacion();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"FormaDelCobro")) {
                MigrarFormaDelCobro();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"Vehiculo")) {
                MigrarVehiculo();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"PrnStt")) {
                MigrarPrnStt();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"Auxiliar")) {
                MigrarAuxiliar();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"ParametrosContables")) {
                MigrarParametrosContables();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"Almacen")) {
                MigrarAlmacen();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"CuentaBancaria")) {
                MigrarCuentaBancaria();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"SettValueByCompany")) {
                MigrarSettValueByCompany();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"ConceptoBancario")) {
                MigrarConceptoBancario();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"TarifaN2")) {
                MigrarTarifaN2();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"TablaRetencion")) {
                MigrarTablaRetencion();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"Proveedor")) {
                MigrarProveedor();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"Compra")) {
                MigrarCompraOrdenDeCompra();
            }
            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"CargaInicial")) {
                MigrarCargaInicial();
            }

            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"Caja")) {
                MigrarCaja();
            }

            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"CajaApertura")) {
                MigrarCajaApertura();
            }

            if(LibGalac.Aos.Base.LibArray.Contains(_ModulesArray,"Cambio")) {
                MigrarCambio();
            }
            if (LibGalac.Aos.Base.LibArray.Contains(_ModulesArray, "Vendedor")) {
                MigrarVendedorYDetalleComisiones();
            }
            return vResult;
        }

        private void MigrarPrnStt() {
            new LibGalac.Aos.Dal.PrnStt.LibMigratePrnStt(insTrn).MigratePrnSttACI();
        }
        private bool BorrarTablaAnterior(string valTableName) {

            return new LibDbo().Drop(valTableName,eDboType.Tabla);
        }
        #endregion

        private string GetCurrentUserName() {
            string vResult;
            if(string.IsNullOrEmpty(_CurrentUserName)) {
                _CurrentUserName = "JEFE";
                if(System.Threading.Thread.CurrentPrincipal != null) {
                    if(System.Threading.Thread.CurrentPrincipal.Identity != null) {
                        if(System.Threading.Thread.CurrentPrincipal.Identity.GetType().Equals(typeof(CustomIdentity))) {
                            _CurrentUserName = ((CustomIdentity)System.Threading.Thread.CurrentPrincipal.Identity).Login;
                        }
                    }
                }
            }
            vResult = _CurrentUserName;
            return vResult;
        }
        private void MigraParaMetrosActuales(IList<int> valCompanias,IList<string> valColumnas,XElement valRecords,bool valGeneral) {
            foreach(int vCompania in valCompanias) {
                XElement vParametrosPorCompania = null;
                foreach(string vColumna in valColumnas) {
                    if(vColumna != "ConsecutivoCompania") {
                        if(valGeneral) {
                            vParametrosPorCompania = ParametrosGenerales(vCompania,valRecords,vColumna,vParametrosPorCompania);
                        } else {
                            vParametrosPorCompania = ParametrosDeEstaCompania(vCompania,valRecords,vColumna,vParametrosPorCompania);
                        }
                    }
                }
                GuardaDatosEnNuevaTabla(vCompania,vParametrosPorCompania);
            }
        }

        XElement ParametrosDeEstaCompania(int valConsecutivoCompania,XElement vRecords,string valColumna,XElement valIten) {
            XElement vResult;
            XElement vxmlItens = null;
            var vEntity = from vRecord in vRecords.Descendants("GpResult")
                          where LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")) == valConsecutivoCompania
                          select vRecord;
            foreach(XElement vItem in vEntity) {
                vxmlItens = new XElement("GpData",
                new XElement("GpResult",
                new XElement("ConsecutivoCompania",valConsecutivoCompania),
                new XElement("NameSettDefinition",valColumna),
                new XElement("value",FortamateaColumna(vItem,valColumna))));
            }
            if(valIten == null) {
                vResult = vxmlItens;
            } else {
                vResult = new XElement("GpData",
                from vData in vxmlItens.Elements()
                select vData,
                from vData in valIten.Elements()
                select vData);
            }
            return vResult;
        }
        XElement ParametrosGenerales(int valConsecutivoCompania,XElement vRecords,string valColumna,XElement valIten) {
            XElement vResult;
            XElement vxmlItens = null;
            var vEntity = from vRecord in vRecords.Descendants("GpResult")
                          select vRecord;
            foreach(XElement vItem in vEntity) {
                vxmlItens = new XElement("GpData",
                new XElement("GpResult",
                new XElement("ConsecutivoCompania",valConsecutivoCompania),
                new XElement("NameSettDefinition",valColumna),
                new XElement("value",FortamateaColumna(vItem,valColumna))));
            }
            if(valIten == null) {
                vResult = vxmlItens;
            } else {
                vResult = new XElement("GpData",
                from vData in vxmlItens.Elements()
                select vData,
                from vData in valIten.Elements()
                select vData);
            }
            return vResult;
        }
        string FortamateaColumna(XElement valItem,string valColumna) {
            string vResullt = "";
            if(!(System.NullReferenceException.ReferenceEquals(valItem.Element(valColumna),null))) {
                vResullt = FortamateaValoresBool(valItem.Element(valColumna).Value);
            }
            return vResullt;
        }
        string FortamateaValoresBool(string valValue) {
            string vResullt = "";
            vResullt = valValue;
            return vResullt;
        }

        IList<int> CompaniaToList(XElement valItem) {
            IList<int> vDetailList = new List<int>();
            foreach(XElement vItemDetail in valItem.Descendants("GpResult")) {
                vDetailList.Add(LibConvert.ToInt(vItemDetail.Element("ConsecutivoCompania")));
            }
            return vDetailList;
        }
        IList<string> ColumnasToList(string valColumnas) {
            IList<string> vDetailList = new List<string>();
            string[] vLista = LibGalac.Aos.Base.LibString.Split(valColumnas,",");
            foreach(string var in vLista) {
                vDetailList.Add(var);
            }
            return vDetailList;
        }

        private XElement Parametrosactuales(string valSql) {
            XElement vResult = null;
            QAdvSql insQAdvSql = new QAdvSql("");
            LibDatabase insDb = new LibDatabase();
            vResult = LibXml.ToXElement(insDb.LoadData(valSql,90));
            return vResult;
        }

        private XElement ListadoCompania() {
            StringBuilder vSQL = new StringBuilder();
            XElement vResult = null;
            QAdvSql insQAdvSql = new QAdvSql("");
            LibDatabase insDb = new LibDatabase();
            vSQL.Append(" SELECT ");
            vSQL.Append(" COMPANIA.ConsecutivoCompania ");
            vSQL.Append(" FROM COMPANIA ");
            vSQL.Append(" INNER JOIN ParametrosCompania ON");
            vSQL.Append(" (COMPANIA.ConsecutivoCompania = ParametrosCompania.ConsecutivoCompania)");
            vResult = LibXml.ToXElement(insDb.LoadData(vSQL.ToString(),90));
            return vResult;
        }

        private string SqlParametrosActuales(string valColumnas,string valFROM) {
            string vResult = "";
            StringBuilder vSQL = new StringBuilder();
            LibDatabase insDb = new LibDatabase();
            vSQL.Append(insDb.InsSql.SetDateFormat("dmy"));
            vSQL.Append(" SELECT ");
            vSQL.Append(valColumnas);
            vSQL.Append(" FROM " + valFROM);
            vResult = vSQL.ToString();
            return vResult;
        }

        private void GuardaDatosEnNuevaTabla(int valConsecutivoCompania,XElement valRecord) {
            insTrn.StartTransaction();
            insTrn.ExecSpNonQuery(insTrn.ToSpName("Comun","SettValueByCompanyINSList"),ParametrosActualizacion(valConsecutivoCompania,valRecord));
            insTrn.CommitTransaction();
        }

        #region Parametros

        private StringBuilder ParametrosActualizacion(int valConsecutivoCompania,XElement valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            vParams.AddInXml("XmlDataDetail",valRecord);
            vParams.AddInString("NombreOperador",_CurrentUserName,10);
            vParams.AddInDateTime("FechaUltimaModificacion",LibDate.Today());
            vResult = vParams.Get();
            return vResult;
        }
        private string ColumnasParametrosCompania() {
            StringBuilder vSQL = new StringBuilder();
            string vResult = "";
            vSQL.Append("ConsecutivoCompania,");
            vSQL.Append("UsaMonedaExtranjera,");
            vSQL.Append("UsaPrecioSinIva,");
            vSQL.Append("EmitirDirecto,");
            vSQL.Append("CantidadDeCopiasDeLaFacturaAlImprimir,");
            vSQL.Append("UsaAlmacen,");
            vSQL.Append("CodigoGenericoCliente,");
            vSQL.Append("CodigoGenericoVendedor,");
            vSQL.Append("ConceptoReversoCobranza,");
            vSQL.Append("ConceptoDebitoBancario,");
            vSQL.Append("ConceptoCreditoBancario,");
            vSQL.Append("ModeloDeFactura,");
            vSQL.Append("UsaCamposDefinibles,");
            vSQL.Append("NombreCampoDefinible1,");
            vSQL.Append("NombreCampoDefinible2,");
            vSQL.Append("NombreCampoDefinible3,");
            vSQL.Append("NombreCampoDefinible4,");
            vSQL.Append("NombreCampoDefinible5,");
            vSQL.Append("NombreCampoDefinible6,");
            vSQL.Append("NombreCampoDefinible7,");
            vSQL.Append("NombreCampoDefinible8,");
            vSQL.Append("NombreCampoDefinible9,");
            vSQL.Append("NombreCampoDefinible10,");
            vSQL.Append("NombreCampoDefinible11,");
            vSQL.Append("NombreCampoDefinible12,");
            vSQL.Append("FacturaPreNumerada,");
            vSQL.Append("PrimeraFactura,");
            vSQL.Append("NumeroDeCerosALaIzquierda,");
            vSQL.Append("TipoDePrefijo,");
            vSQL.Append("Prefijo,");
            vSQL.Append("CodigoAlmacenGenerico,");
            vSQL.Append("NoImprimirFactura,");
            vSQL.Append("RellenaCerosAlaIzquierda,");
            vSQL.Append("NumCopiasComprobanteRetencion,");
            vSQL.Append("NumCopiasComprobantepago,");
            vSQL.Append("NombreCampoDefinibleInventario1,");
            vSQL.Append("NombreCampoDefinibleInventario2,");
            vSQL.Append("NombreCampoDefinibleInventario3,");
            vSQL.Append("NombreCampoDefinibleInventario4,");
            vSQL.Append("NombreCampoDefinibleInventario5,");
            vSQL.Append("PermitirFacturarConCantidadCero,");
            vSQL.Append("UsaRetencion,");
            vSQL.Append("InsertandoPorPrimeraVez,");
            vSQL.Append("NombrePlantillaComprobanteDePago,");
            vSQL.Append("NombrePlantillaFactura,");
            vSQL.Append("TipoDeOrdenDePagoAImprimir,");
            vSQL.Append("CodigoGenericoCuentaBancaria,");
            vSQL.Append("ImprimirFacturaConSubtotalesPorLineaDeProducto,");
            vSQL.Append("NombrePlantillaCotizacion,");
            vSQL.Append("NombrePlantillaCompobanteCobranza,");
            vSQL.Append("ImprimirCombrobanteAlIngresarCobranza,");
            vSQL.Append("NoImprimirComprobanteDePago,");
            vSQL.Append("UsarOtrosCargoDeFactura,");
            vSQL.Append("UsarBaseImponibleDiferenteA0Y100,");
            vSQL.Append("SolicitarIngresoDeTasaDeCambioAlEmitir,");
            vSQL.Append("NombrePlantillaOrdenDeCompra,");
            vSQL.Append("ImprimirOrdenDeCompra,");
            vSQL.Append("UsarDecimalesAlImprimirCantidad,");
            vSQL.Append("NombrePlantillaNotaDeCredito,");
            vSQL.Append("NombrePlantillaNotaDeDebito,");
            vSQL.Append("NombrePlantillaBoleta,");
            vSQL.Append("FormaDeCalcularComisionesSobreCobranza,");
            vSQL.Append("NombrePlantillaComisionSobreCobranza,");
            vSQL.Append("ImprimirReporteAlIngresarNotaEntradaSalida,");
            vSQL.Append("NombrePlantillaNotaEntradaSalida,");
            vSQL.Append("PrimeraNotaDeCredito,");
            vSQL.Append("PrimeraNotaDeDebito,");
            vSQL.Append("PrimeraBoleta,");
            vSQL.Append("UsaMultiplesAlicuotas,");
            vSQL.Append("ForzarFechaFacturaAmesEspecifico,");
            vSQL.Append("MesFacturacionEnCurso,");
            vSQL.Append("FechaDeInicioContabilizacion,");
            vSQL.Append("PermitirIncluirFacturacionHistorica,");
            vSQL.Append("UltimaFechaDeFacturacionHistorica,");
            vSQL.Append("GenerarCxCalEmitirUnaFacturaHistorica,");
            vSQL.Append("UsarDosTalonarios,");
            vSQL.Append("FacturaPreNumerada2,");
            vSQL.Append("ModeloDeFactura2,");
            vSQL.Append("PrimeraFactura2,");
            vSQL.Append("TipoDePrefijo2,");
            vSQL.Append("Prefijo2,");
            vSQL.Append("NombrePlantillaFactura2,");
            vSQL.Append("ConsideraConciliadosLosMovIngresadosAntesDeFecha,");
            vSQL.Append("FechaDeInicioConciliacion,");
            vSQL.Append("DiaDelCierreFiscal,");
            vSQL.Append("MesDelCierreFiscal,");
            vSQL.Append("NombreYApellidoR,");
            vSQL.Append("CodTelfR,");
            vSQL.Append("TelefonoR,");
            vSQL.Append("DireccionR,");
            vSQL.Append("CiudadRepLegal,");
            vSQL.Append("NumeroRIFR,");
            vSQL.Append("CorreoElectronicoRepLegal,");
            vSQL.Append("UsaCamposExtrasEnRenglonFactura,");
            vSQL.Append("PedirInformacionLibroVentasXlsalEmitirFactura,");
            vSQL.Append("SugerirNumeroControlFactura,");
            vSQL.Append("EnDondeRetenerIVA,");
            vSQL.Append("PrimerNumeroComprobanteRetIVA,");
            vSQL.Append("FormaDeReiniciarElNumeroDeComprobanteRetIVA,");
            vSQL.Append("ImprimirComprobanteContableDePago,");
            vSQL.Append("NumCopiasComprobanteMovBancario,");
            vSQL.Append("NombrePlantillaComprobanteDeMovBancario,");
            vSQL.Append("ConfirmarImpresionMovBancarioPorSecciones,");
            vSQL.Append("ImprimirComprobanteDeMovBancario,");
            vSQL.Append("IvaEsCostoEnCompras,");
            vSQL.Append("ImprimirComprobanteDeRetIVA,");
            vSQL.Append("NumeroDeCopiasComprobanteRetencionIVA,");
            vSQL.Append("NombreContador,");
            vSQL.Append("CedulaContador,");
            vSQL.Append("NumeroCPC,");
            vSQL.Append("ImprimirCentimosEnPlanilla,");
            vSQL.Append("ModeloPlanillaForma00030,");
            vSQL.Append("ImprimirCompContDespuesDeChequeMovBancario,");
            vSQL.Append("ImprimirCompraAlInsertar,");
            vSQL.Append("NombrePlantillaCompra,");
            vSQL.Append("ExigirRifdeClienteAlEmitirFactura,");
            vSQL.Append("UsarResumenDiarioDeVentas,");
            vSQL.Append("UsaPrecioSinIvaEnResumenVtas,");
            vSQL.Append("UsarRenglonesEnResumenVtas,");
            vSQL.Append("ResumenVtasAfectaInventario,");
            vSQL.Append("UnComprobanteDeRetIVAPorHoja,");
            vSQL.Append("NombrePlantillaComprobanteDeRetIVA,");
            vSQL.Append("GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACero,");
            vSQL.Append("ExigirInformacionLibroDeCompras,");
            vSQL.Append("DetalleProdCompFactura,");
            vSQL.Append("DetalleProdCompCotizacion,");
            vSQL.Append("FormaDeOrdenarDetalleFactura,");
            vSQL.Append("ImprimirBorradorAlInsertarFactura,");
            vSQL.Append("CuentaBancariaAnticipo,");
            vSQL.Append("ConceptoBancarioAnticipoCobrado,");
            vSQL.Append("ConceptoBancarioAnticipoPagado,");
            vSQL.Append("ConceptoBancarioReversoAnticipoCobrado,");
            vSQL.Append("ConceptoBancarioReversoAnticipoPagado,");
            vSQL.Append("NombrePlantillaReciboDeAnticipoCobrado,");
            vSQL.Append("NombrePlantillaReciboDeAnticipoPagado,");
            vSQL.Append("UsaCobroDirecto,");
            vSQL.Append("CuentaBancariaCobroDirecto,");
            vSQL.Append("ConceptoBancarioCobroDirecto,");
            vSQL.Append("TipoDeNivelDePrecios,");
            vSQL.Append("SugerirConsecutivoAnticipo,");
            vSQL.Append("TipoComprobanteDeAnticipoAImprimir,");
            vSQL.Append("TomarEnCuentaRetencionesCeroParaARCVyRA,");
            vSQL.Append("CambiarFechaEnCuotasLuegoDeFijarFechaEntrega,");
            vSQL.Append("EsAsociadoEnCtaDeParticipacion,");
            vSQL.Append("ComisionesEnFactura,");
            vSQL.Append("ComisionesEnRenglones,");
            vSQL.Append("NombreVendedorUno,");
            vSQL.Append("NombreVendedorDos,");
            vSQL.Append("NombreVendedorTres,");
            vSQL.Append("MaximoDescuentoEnFactura,");
            vSQL.Append("ItemsMonto,");
            vSQL.Append("ConceptoBancarioReversoDePago,");
            vSQL.Append("AccionAlAnularFactDeMesesAnt,");
            vSQL.Append("ColocarEnFacturaElVendedorAsinagoAlCliente,");
            vSQL.Append("ModeloFacturaModoTexto,");
            vSQL.Append("ModeloFacturaModoTexto2,");
            vSQL.Append("PermitirDobleDescuentoEnFactura,");
            vSQL.Append("ComplConComodinEnBusqDeArtInv,");
            vSQL.Append("ImprimirAnexoDeSerial,");
            vSQL.Append("NombrePlantillaAnexoSeriales,");
            vSQL.Append("SinonimoGrupo,");
            vSQL.Append("SinonimoTalla,");
            vSQL.Append("SinonimoColor,");
            vSQL.Append("SinonimoSerial,");
            vSQL.Append("SinonimoRollo,");
            vSQL.Append("ImprimirDatosClienteEnCompFiscal,");
            vSQL.Append("AsignarComisionDeVendedorEnCobranza,");
            vSQL.Append("ManejaDebitoBancario,");
            vSQL.Append("RedondeaMontoDebitoBancario,");
            vSQL.Append("ManejaCreditoBancario,");
            vSQL.Append("RedondeaMontoCreditoBancario,");
            vSQL.Append("FormatoDeFecha,");
            vSQL.Append("ConcatenaLetraEaArticuloExento,");
            vSQL.Append("NumItemImprimirFactura,");
            vSQL.Append("AccionLimiteItemsFactura,");
            vSQL.Append("LimpiezaDeCotizacionXFactura,");
            vSQL.Append("VerificarDocumentoSinContabilizar,");
            vSQL.Append("NombrePlantillaCodigoDeBarras,");
            vSQL.Append("ImprimeDireccionAlFinalDelComprobanteFiscal,");
            vSQL.Append("BloquearNumeroCobranza,");
            vSQL.Append("AsociaCentroDeCostoyAlmacen,");
            vSQL.Append("CambiarCobradorVendedor,");
            vSQL.Append("MetodoDeCosteo,");
            vSQL.Append("FechaDesdeUsoMetodoDeCosteo,");
            vSQL.Append("FechaContabilizacionDeCosteo,");
            vSQL.Append("CalculoAutomaticoDeCosto,");
            vSQL.Append("ComprobanteCostoDetallado,");
            vSQL.Append("NCPreNumerada,");
            vSQL.Append("TipoDePrefijoNC,");
            vSQL.Append("PrefijoNC,");
            vSQL.Append("NDPreNumerada,");
            vSQL.Append("TipoDePrefijoND,");
            vSQL.Append("PrefijoND,");
            vSQL.Append("TipoDeAgrupacionParaLibrosDeVenta,");
            vSQL.Append("CantidadDeDecimales,");
            vSQL.Append("IntegracionRIS,");
            vSQL.Append("AvisoDeFacturacionMenor,");
            vSQL.Append("UsaMismoNumeroCompRetTodasCxP,");
            vSQL.Append("TipoNegocio,");
            vSQL.Append("FechaMinimaIngresarDatos,");
            vSQL.Append("NombreCampoDefinibleCliente1,");
            vSQL.Append("OrdenarCxPPorFacturaDocumento,");
            vSQL.Append("BuscarArticuloXSerialAlFacturar,");
            vSQL.Append("AvisoDeReservasvencidas,");
            vSQL.Append("ImprimirTipoCobroEnFactura,");
            vSQL.Append("VerificarStock,");
            vSQL.Append("ImprimirTransferenciaAlInsertar,");
            vSQL.Append("BeneficiarioGenerico,");
            vSQL.Append("ConceptoBancarioReversoSolicitudDePago,");
            vSQL.Append("NombrePlantillaComprobanteDePagoSueldo,");
            vSQL.Append("ActivarFacturacionPorAlmacen,");
            vSQL.Append("EnDondeRetenerISLR,");
            vSQL.Append("ImprimirNotaESconPrecio,");
            vSQL.Append("AutorellenaResumenDiario,");
            vSQL.Append("RetieneImpuestoMunicipal,");
            vSQL.Append("NombrePlantillaRetencionImpuestoMunicipal,");
            vSQL.Append("ModeloNotaEntrega,");
            vSQL.Append("NotaEntregaPreNumerada,");
            vSQL.Append("PrimeraNotaEntrega,");
            vSQL.Append("TipoPrefijoNotaEntrega,");
            vSQL.Append("PrefijoNotaEntrega,");
            vSQL.Append("NombrePlantillaNotaEntrega,");
            vSQL.Append("NombrePlantillaOrdenDeDespacho,");
            vSQL.Append("NumCopiasOrdenDeDespacho,");
            vSQL.Append("UsaNotaEntrega,");
            vSQL.Append("GenerarMovReversoSiAnulaPago,");
            vSQL.Append("ConsecutivoAlmacenGenerico,");
            vSQL.Append("ValidarArticulosAlGenerarFactura,");
            vSQL.Append("CampoCodigoAlternativoDeArticulo,");
            vSQL.Append("ImprimeSerialRolloLuegoDeDescripArticulo,");
            vSQL.Append("ModeloNotaEntregaModoTexto,");
            vSQL.Append("ProductoTerminado,");
            vSQL.Append("ProductoMateriaPrima");
            vSQL.Append("ConceptoBancarioReversoTransfIngreso,");
            vSQL.Append("ConceptoBancarioReversoTransfEgreso,");
            vResult = vSQL.ToString();
            return vResult;

        }

        private string ColumnasParametrosAdministrativos() {
            StringBuilder vSQL = new StringBuilder();
            string vResult = "";
            vSQL.Append("FormaDeEscogerCompania,");
            vSQL.Append("EscogerCompaniaAlEntrar,");
            vSQL.Append("EscogerUltimaCompaniaUsada,");
            vSQL.Append("UsarZonaCobranza,");
            vSQL.Append("UsaCodigoClienteEnPantalla,");
            vSQL.Append("UsaCodigoVendedorEnPantalla,");
            vSQL.Append("UsarCodigoProveedorEnPantalla,");
            vSQL.Append("UsaCodigoBancoEnPantalla,");
            vSQL.Append("VerificarFacturasManualesFaltantes,");
            vSQL.Append("UsaCodigoConceptoBancarioEnPantalla,");
            vSQL.Append("GenerarMovBancarioDesdePago,");
            vSQL.Append("NombreMonedaLocal,");
            vSQL.Append("NombreMonedaExtranjera,");
            vSQL.Append("GenerarMovBancarioDesdeCobro,");
            vSQL.Append("MandarMensajeNumeroDeMovimientoBancario,");
            vSQL.Append("SugerirConsecutivoEnCobranza,");
            vSQL.Append("NumFacturasManualesFaltantes,");
            vSQL.Append("LongitudCodigoVendedor,");
            vSQL.Append("LongitudCodigoCliente,");
            vSQL.Append("PermitirEditarIVAenCxC_CxP,");
            vSQL.Append("LongitudCodigoProveedor,");
            vSQL.Append("ConfirmarImpresionPorSecciones,");
            vSQL.Append("GenerarCxPdesdeCompra,");
            vSQL.Append("PermitirSobregiro,");
            vSQL.Append("SeResincronizaronLosSupervisores,");
            vSQL.Append("DevolucionReversoSeGeneraComo,");
            vSQL.Append("AvisoDeClienteConDeuda,");
            vSQL.Append("MontoApartirDelCualEnviarAvisoDeuda,");
            vSQL.Append("OrdenamientoDeCodigoString,");
            vSQL.Append("ImprimirComprobanteDeCxC,");
            vSQL.Append("ImprimirComprobanteDeCxP,");
            vSQL.Append("CodigoMonedaLocal,");
            vSQL.Append("CodigoMonedaExtranjera,");
            vSQL.Append("AvisarSiProveedorTieneAnticipos,");
            vSQL.Append("EsSistemaParaIG,");
            vSQL.Append("BuscarClienteXRifAlFacturar,");
            vSQL.Append("ValidarRifEnLaWeb,");
            vSQL.Append("CodigoEmpresaIntegrada,");
            vSQL.Append("PuedeUsarS10,");
            vSQL.Append("S10DBName");
            vResult = vSQL.ToString();
            return vResult;

        }
        #endregion        
    }

    internal enum NivelesUsuarioOld {
        ADM_CONSULTAR = 1,
        ADM_INSERTAR = 2,
        ADM_MODIFICAR = 3,
        ADM_ELIMINAR = 4,
        ADM_ANULAR = 5,
        ADM_EMITIR = 6,
        ADM_INSERTAR_MANUAL = 7,
        ADM_IMPRIMIR = 8,
        ADM_RESPALDAR = 9,
        ADM_CONTABILIZAR = 10,
        ADM_DESCONTABILIZAR = 11,
        ADM_ACTIVAR = 12,
        ADM_DESACTIVAR = 13,
        ADM_DESCUENTO = 14,
        ADM_DESCRIPCION_Y_PRECIO = 15,
        ADM_UNIFICAR = 16,
        ADM_INFORMES_GERENCIALES = 17,
        ADM_MODIFICAR_STATUS_CURSO = 18,
        ADM_GENERAR = 19,
        ADM_AVANZADO = 20,
        ADM_SUPERVISOR = 21,
        ADM_DISTRIBUIDOR = 22,
        ADM_Gerente = 23,
        ADM_AUTORIZARSOBREGIRO = 24,
        ADM_INSERTARTASADECAMBIO = 25,
        ADM_CERRAR = 26,
        ADM_ABRIR = 27,
        WCO_ModificarComprobantesAutomaticos = 28,
        WCO_CALCULOMANUAL = 29,
        ADM_GENERAR_FACTURA_DESDE_COTIZACION = 30,
        WCO_RetirarActivos = 31,
        ADM_REIMPRIMIR = 32,
        ADM_FORMA_IMPRESA = 33,
        ADM_MEDIO_MAGNETICO = 34,
        ADM_ACTUALIZAR = 35,
        ADM_REINCORPORAR = 36,
        ADM_REFINANCIAR = 37,
        ADM_ANULAR_REFINANCIAR = 38,
        ADM_INFORME_VENTAS_COMPRAS = 39,
        ADM_EMITIR_DIRECTO = 40,
        ADM_INSERTAR_SUSTITUTIVA = 41,
        ADM_IMPRIMIR_BORRADOR = 42,
        ADM_IMPORTAR = 43,
        ADM_RECALCULAR = 44,
        ADM_AJUSTAR = 45,
        ADM_DESINCORPORAR = 46,
        ADM_IMPRIMIR_NOTA_DE_ENTREGA = 47,
        ADM_EXTENDER = 48,
        ADM_INSERTAR_COPIA = 49,
        ADM_INSERTAR_REVERSO = 50,
        ADM_INSERTAR_NOTA_CREDITO = 51,
        ADM_INSERTAR_NOTA_DEBITO = 52,
        ADM_INSERTAR_HISTORICA = 53,
        ADM_INSERTAR_NOTA_CREDITO_HISTORICA = 54,
        ADM_INSERTAR_NOTA_DEBITO_HISTORICA = 55,
        ADM_GENERAR_FACTURA_DESDE_CONTRATO = 56,
        ADM_IMPRIMIR_COMPROBANTE_PAGO = 57,
        ADM_IMPRIMIR_COMPROBANTE_RETENCION_ISLR = 58,
        ADM_IMPRIMIR_COMPROBANTE_RETENCION_IVA = 59,
        ADM_EXPORTAR = 60,
        ADM_DEVOLVER = 61,
        ADM_INFORMES = 62,
        ADM_COBRO_DIRECTO = 63,
        ADM_INSERTAR_FACTURA_BORRADOR = 64,
        ADM_ABRIR_GAVETA = 65,
        ADM_ASIGNAR_CAJA_REGISTRADORA = 66,
        ADM_INGRESAR_CLIENTE_POR_MOSTRADOR = 67,
        ADM_INGRESAR_FECHA_ENTREGA = 68,
        ADM_MODIFICAR_PRECIO_EN_FACTURA = 69,
        ADM_MODIFICAR_BENEFICIARIO_CHEQUE = 70,
        ADM_RESERVAR_MERCANCIA = 71,
        ADM_INSERTAR_ARTICULO_POR_PORCENTAJE_COMISION = 72,
        ADM_MODIFICAR_SERIAL = 73,
        ADM_MODIFICAR_ROLLO = 74,
        ADM_MODIFICAR_VENDEDOR_EN_FACTURA_EMITIDA = 75,
        ADM_CONSULTAR_CORTES = 76,
        ADM_RESULTADO_VISITA = 77,
        ADM_IMPORTAR_VISITA = 78,
        ADM_EXPORTAR_VISITA = 79,
        ADM_REALIZAR_CIERRE_Z = 80,
        //ADM_ES_USUARIO_TIPO_CAJERO = 79,
        ADM_ES_USUARIO_TIPO_CAJERO = 81,
        ADM_AJUSTAR_PRECIOS_POR_COSTOS = 82,
        ADM_REACTIVAR_FACTURA_ANULADA = 83,
        WCO_REABRIR_PERIODO = 84,
        WCO_CERRAR_PERIODO = 85,
        ADM_METODO_COSTO = 86,
        ADM_EXPORTAR_BANCO_MERCANTIL = 87,
        ADM_AJUSTAR_FECHA_CONTRATO = 88
    }

     
}



