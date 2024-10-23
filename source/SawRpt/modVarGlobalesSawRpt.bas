Attribute VB_Name = "modVarGlobalesSawRpt"
Option Explicit

Public gError As clsError
Public gUtilReports As clsUtilDDReports
Public gUtilMargins As clsUtilMargins
Public gConvert As clsConvert
Public gUtilScript As clsUtilDDScript
Public gProyParametros As Object
Public gMessage As clsMessage
Public gUtilDate As clsUtilDate
Public gEnumReport As clsEnumReport
Public gDbUtil As clsDBUtil
Public gTexto As clsTexto
Public gEnumProyecto As clsEnumAdministrativo
Public gEnumProyectoWincont As clsEnumContabilidad
Public gProyParametrosCompania As Object
Private mEsSistemaInfotax As Boolean
Public gAPI As clsDefensiveAPI
Public gProyUsuarioActual As Object
Public gLibGalacDataParse As Object
Public gDefgen As clsDefgen
Public gUtilSQL As clsUtilSQLStatement
Public gDefProg As Object
Public gGlobalization As clsUtilGlobalization
Public gUtilFile As clsUtilFiles
Public gLibControlesGalac As clsLibOtrosControles
Public gMonedaLocalActual As Object
Public gUltimaTasaDeCambio As Object
Public gListLibrary As Object
Public gWorkPaths As clsUtilWorkPaths
Public gDefDatabaseConexion As ADODB.Connection
Public gEnumTablaRetencion As clsEnumTablaRetencion
Public gAdmAlicuotaIvaActual As Object
Public gUtilMathOperations As clsUtilMathOperations
Public gPrinter As clsPrinterMargins

Public Sub SetProyParametros(ByRef refProyParametros As Object)
    Set gProyParametros = refProyParametros
End Sub

Public Sub SetProyParametrosCompania(ByRef refProyParametrosCompania As Object)
    Set gProyParametrosCompania = refProyParametrosCompania
End Sub
Public Function GetEsSistemaInfotax() As Boolean
   GetEsSistemaInfotax = mEsSistemaInfotax
End Function

Public Sub SetEsSistemaInfotax(ByVal valEsSistemaInfotax As Boolean)
   mEsSistemaInfotax = valEsSistemaInfotax
End Sub
