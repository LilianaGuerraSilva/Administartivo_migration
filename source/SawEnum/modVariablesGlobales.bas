Attribute VB_Name = "modVariablesGlobales"
Option Explicit

Private Const SiglasWinCont As String = "WCO"

Public gError As clsError
Public gMessage As clsMessage
Public gTexto As clsTexto
Public gDefGen As clsDefgen
Public gConvert As clsConvert
Public gUtilFile As clsUtilFiles
Public gApi As clsDefensiveAPI
Public gGlobalization As clsUtilGlobalization
Public gEnumProyecto As clsEnumAdministrativo
Public gProyUsuarioActual As Object
Public GetSiglasAdmInterno As String
Public mNombreEmpresaActual As String
Public gDefProg As Object
Public mEsSistemaAdmInterno As Boolean
Public gDbUtil As clsDBUtil
Public gUtilMathOperations As clsUtilMathOperations
Public gWorkPaths As clsUtilWorkPaths

Public Function gProyParametros_GetEsSistemaParaIG() As Boolean
   gProyParametros_GetEsSistemaParaIG = True And _
         mNombreEmpresaActual = "INFOTAX, INFORMATICA TRIBUTARIA"
End Function

Public Function GetSiglasWinCont() As String
   GetSiglasWinCont = SiglasWinCont
End Function

Public Function getDllVersion() As String
   getDllVersion = App.Title & ".dll " & " Versión: " & App.Major & "." & App.Minor & "." & App.Revision
End Function

Public Function GetEsSistemaAdmInterno() As Boolean
   GetEsSistemaAdmInterno = mEsSistemaAdmInterno
End Function
