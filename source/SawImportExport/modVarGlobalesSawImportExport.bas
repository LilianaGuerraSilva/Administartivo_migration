Attribute VB_Name = "modVarGlobalesSawImportExport"
Option Explicit

Public gUtilSQL As clsUtilSQLStatement
Public gError As clsError
Public gMessage As clsMessage
Public gTexto As clsTexto
Public gDefGen As clsDefgen
Public gConvert As clsConvert
Public gApi As clsDefensiveAPI
Public gUtilFile As clsUtilFiles
Public gUtilDate As clsUtilDate
Public gErrorCodesIG As clsErrorCodesIG
Public gUtilMathOperations As clsUtilMathOperations
Public gEnumProyecto As clsEnumAdministrativo
Public gEnumProyectoWincont As clsEnumContabilidad
Public gEnumTablaRetencion As clsEnumTablaRetencion
Public gDefDatabase As clsDefDatabase
Public gDbUtil As clsDBUtil
Public gUtilImport As clsUtilImport
Public gProyParametrosCompania As Object
Public gAdmAlicuotaIvaActual As Object
Public gGlobalization As clsUtilGlobalization
Public insFactura As Object
Public insRenglonFactura As Object
Public insCtaContable As Object
Public insCxC As Object
Public insCxP As Object
Public insVendedor As Object
Public insCliente As Object
Public insMoneda As Object
Public insRenglonComisionesDeVendedor As Object
Public insProveedor As Object
Public insLoteDeAdm As Object
Public insColor As Object
Public insTalla As Object
Public insGrupoTallaColor As Object
Public insRenglonGrupoColor As Object
Public insRenglonGrupoTalla As Object
Public insArticuloInventario As Object
Public insCamposMonedaExtranjera As Object
Public insExistenciaPorGrupo As Object
Public varNoComunWincont As Object
Public gProyCompaniaActual As Object
Public insTablaRetencionNavigator As Object
Public insTipoProveedor As Object
Public insInputCamposEspeciales As Object
Public insCiudad As Object
Public insZonaCobranza As Object
Public insSectorNegocio As Object
Public insCaja As Object
Public insAlmacen As Object
Public insLineaDeProducto As Object
Public insCategoriaNav As Object
Public insUnidadDeVenta As Object
Public insNotaDeEntrada As Object
Public insExistenciaNavigator As Object
Public insRenglonCobroDeFactura As Object
Public insWrpAlmacen As Object
Public insTipoDeComprobante As Object
Public gLibGalacDataParse

Public mConsecutivoPeriodo As Long
Public mConsecutivoCompaniaActual As Long
Public mNombreCompaniaActual As String
Public mCiudadCompaniaActual As String
Public mNombreDelUsuario As String
Public mCodigoMonedaLocal As String
Public mCodigoMonedaExtranjera As String
Public mNombreMonedaExtranjera As String
Public mNombreMonedaLocal As String

Public Function gProyParametros_GetEsSistemaParaIG() As Boolean
   gProyParametros_GetEsSistemaParaIG = True And _
         mNombreCompaniaActual = "INFOTAX, INFORMATICA TRIBUTARIA"
End Function

Public Function getDllVersion() As String
   getDllVersion = App.Title & ".dll " & " Versión: " & App.Major & "." & App.Minor & "." & App.Revision
End Function

Public Function Conexion() As ADODB.Connection
   Set Conexion = gDefDatabase.Conexion
End Function

Public Function getDatabaseName() As String
   getDatabaseName = gDefDatabase.getDatabaseName
End Function

Public Function getFN_TIMESTAMP()
   getFN_TIMESTAMP = gDefDatabase.getFN_TIMESTAMP '   "fldTimeStamp"
End Function

Public Function getFN_NOMBRE_OPERADOR()
   getFN_NOMBRE_OPERADOR = gDefDatabase.getFN_NOMBRE_OPERADOR '  "NombreOperador"
End Function

Public Function getFN_FECHA_ULTIMA_MODIFICACION()
   getFN_FECHA_ULTIMA_MODIFICACION = gDefDatabase.getFN_FECHA_ULTIMA_MODIFICACION ' "FechaUltimaModificacion"
End Function

Private Function getPathActualDeLaAplicacion() As String
   getPathActualDeLaAplicacion = gDefDatabase.getPathActualDeLaAplicacion ' UCase(msPathActualDeLaAplicacion)
End Function

Public Function getIsSQLDataBase() As Boolean
   getIsSQLDataBase = gDefDatabase.getIsSQLDataBase '  mIsSQLDataBase
End Function

