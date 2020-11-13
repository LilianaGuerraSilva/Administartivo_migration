VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsInitSawNav"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Public Sub sInitializeInternalGlobalClasses(ByRef refInsError As clsError, _
            ByRef refInsMessage As clsMessage, _
            ByRef refInsTexto As clsTexto, _
            ByRef refInsDefGen As clsDefgen, _
            ByRef refInsConvert As clsConvert, _
            ByRef refInsApi As clsDefensiveAPI, _
            ByRef refInsUtilFile As clsUtilFiles, _
            ByRef refInsUtilDate As clsUtilDate, _
            ByRef refInsErrorCodesIG As clsErrorCodesIG, _
            ByRef refInsUtilMathOperations As clsUtilMathOperations, _
            ByRef refInsDefProg As Object, _
            ByRef refInsUtilRespaldo As clsUtilRespaldo, _
            ByRef refInsUtilWrp As Object, _
            ByRef refInsUsuarioActual As Object, _
            ByRef refInsCompaniaActual, _
            ByRef refInsUtilWorkPaths As clsUtilWorkPaths, _
            ByRef refInsLibSaw As Object, _
            ByRef refInsEnumReport As clsEnumReport)
            
      Set gError = refInsError
      Set gErrorCodesIG = refInsErrorCodesIG
      Set gMessage = refInsMessage
      Set gTexto = refInsTexto
      Set gDefgen = refInsDefGen
      Set gConvert = refInsConvert
      Set gApi = refInsApi
      Set gUtilFile = refInsUtilFile
      Set gUtilDate = refInsUtilDate
      Set gUtilMathOperations = refInsUtilMathOperations
      Set gDefProg = refInsDefProg
      Set gUtilRespaldo = refInsUtilRespaldo
      Set gUtilWrp = refInsUtilWrp
      Set gProyUsuarioActual = refInsUsuarioActual
      Set gProyCompaniaActual = refInsCompaniaActual
      Set gWorkPaths = refInsUtilWorkPaths
      Set gLibSaw = refInsLibSaw
      Set gEnumReport = refInsEnumReport
End Sub
Public Sub sInitializeDatabaseClasses(ByRef refInsUtSql As clsUtilSQLStatement, ByRef refInsDBUtil As clsDBUtil, ByRef refInsDefDatabase As clsDefDatabase)
   Set gUtilSQL = refInsUtSql
   Set gDbUtil = refInsDBUtil
   Set gDefDatabase = refInsDefDatabase
End Sub

