VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsInitSAWCT"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Public Sub sInitializeInternalGlobalClasses(ByRef refInsError As clsError, ByRef refInsUtilSQL As clsUtilSQLStatement, ByRef refInsDefDatabase As clsDefDatabase, ByRef refInsMessage As clsMessage, ByRef refInsDbUtil As clsDBUtil)
   Set gError = refInsError
   Set gUtilSQL = refInsUtilSQL
   Set gDefDatabase = refInsDefDatabase
   Set gMessage = refInsMessage
   Set gDbUtil = refInsDbUtil
End Sub
Private Sub sInstances()
   Dim insUsuario As clsUsuarioTbl
   Dim insParametrosAdministrativo As clsParametrosAdministrativoTbl
   Dim insParametrosCompania As clsParametrosCompaniaTbl
   Dim insArticuloInventario As clsArticuloInventarioTbl
   Dim insProductoCompuestoTbl As clsProductoCompuestoTbl
   Dim insCamposMonedaExtranjeraTbl As clsCamposMonedaExtranjeraTbl
   Dim insExistenciaPorGrupoTbl As clsExistenciaPorGrupoTbl
   Dim insCierreCostoDelPeriodo As clsCierreCostoDelPeriodoTbl
End Sub
