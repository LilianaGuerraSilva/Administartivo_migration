VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsInitSawSP"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Public Sub sInitializeInternalGlobalClasses(ByVal valSiglasDelPrograma As String, ByRef refInsError As clsError, ByRef refInsUtilSQL As clsUtilSQLStatement, ByRef refInsDefDatabase As clsDefDatabase, ByRef refInsMessage As clsMessage, ByRef refInsWorkPaths As clsUtilWorkPaths, ByRef refInsDefgen As clsDefgen, ByRef refInsDbUtil As clsDBUtil, ByRef refInsTexto As clsTexto)
   Set gError = refInsError
   Set gUtilSQL = refInsUtilSQL
   Set gDefDatabase = refInsDefDatabase
   Set gMessage = refInsMessage
   Set gWorkPaths = refInsWorkPaths
   Set gDefgen = refInsDefgen
   Set gDbUtil = refInsDbUtil
   Set gTexto = refInsTexto
End Sub

Private Sub sDeclaraciones()
   'Create/Drop
   Dim insArticuloInventarioSp As clsArticuloInventarioSp
   Dim insAsientoSp As clsAsientoSp
   Dim insCentroDeCostosSp As clsCentroDeCostosSp
   Dim insCompaniaSP As clsCompaniaSP
   Dim insComprobanteSp As clsComprobanteSp
   Dim insCuentaSp As clsCuentaSp
   Dim insCxCSp As clsCxCSp
   Dim insCxPSp As clsCxPSp
   Dim insGenerarComprobanteSp As clsGenerarComprobanteSp
   Dim insGrupoDeActivosSp  As clsGrupoDeActivosSp
   Dim insParametrosAdministrativoSP  As clsParametrosAdministrativoSP
   Dim insParametrosSp As clsParametrosSp
   Dim insProveedorSP As clsProveedorSP
   Dim insRendicionSp As clsRendicionSp
   Dim insTablaRetencionSp As clsTablaRetencionSp
   Dim insTipoProveedorSp As clsTipoProveedorSp
   Dim insUsuarioSP As clsUsuarioSP
   
   'Excecute
   Dim insCuentaSpExec As clsCuentaSpExec
   Dim insArticuloInventarioSpExec As clsArticuloInventarioSpExec
   Dim insCxPSpExec As clsCxPSpExec
   Dim insClienteSpExec As clsClienteSpExec
End Sub
