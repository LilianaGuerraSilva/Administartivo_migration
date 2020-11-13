VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCuadreCajaSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsCuadreCajaSQL"
Private Const CM_MESSAGE_NAME As String = "Cuadre Caja SQL"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fSQLInformeListadoCajasAbiertas(ByVal ConsecutivoCompaniaActual As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT cajaapertura.consecutivocaja, cajaapertura.nombredelusuario,"
   SQL = SQL & " cajaapertura.fecha, caja.NombreCaja, caja.consecutivocaja, cajaapertura.montoapertura"
   SQL = SQL & " FROM Caja INNER JOIN CajaApertura ON "
   SQL = SQL & " (cajaapertura.consecutivocompania = caja.consecutivocompania) and (cajaapertura.consecutivocaja = caja.consecutivocaja)"
   SQL = SQL & " WHERE "
   SQL = SQL & " caja.consecutivocompania = " & ConsecutivoCompaniaActual
   SQL = SQL & " AND cajaapertura.cajacerrada = 'N'"
   SQL = SQL & " ORDER BY cajaapertura.consecutivocaja, cajaapertura.fecha desc"
h_EXIT: On Error GoTo 0
   fSQLInformeListadoCajasAbiertas = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLInformeListadoCajasAbiertas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


