VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCambioVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsCambioVista"
Private Const CM_MESSAGE_NAME As String = "Vista Cambio "

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Public Function GetViewNameCambioMaxFecha() As String
  GetViewNameCambioMaxFecha = "IGV_CambioMaxFecha"
End Function

Public Function fSqlViewCambioMaxFecha() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = " SELECT CodigoMoneda,"
   vSQL = vSQL & " MAX(FechaDeVigencia) As FechaDeVigencia"
   vSQL = vSQL & " FROM dbo.Cambio "
   vSQL = vSQL & "GROUP BY CodigoMoneda"
   fSqlViewCambioMaxFecha = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewCambioMaxFecha", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameCambioAlaFecha() As String
  GetViewNameCambioAlaFecha = "IGV_CambioAlaFecha"
End Function

Public Function fSqlViewCambioAlaFecha() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   
  vSQL = "SELECT"
  vSQL = vSQL & " dbo.Cambio.CodigoMoneda,"
  vSQL = vSQL & " dbo.Cambio.FechaDeVigencia,"
  vSQL = vSQL & " dbo.Cambio.Nombre ,"
  vSQL = vSQL & " dbo.Cambio.CambioAbolivares"
  vSQL = vSQL & " FROM  dbo.Cambio INNER JOIN "
  vSQL = vSQL & GetViewNameCambioMaxFecha & " ON dbo.Cambio.CodigoMoneda = " & GetViewNameCambioMaxFecha & ".CodigoMoneda AND"
  vSQL = vSQL & " dbo.Cambio.FechaDeVigencia = " & GetViewNameCambioMaxFecha & ".FechaDeVigencia"

   fSqlViewCambioAlaFecha = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewCambioAlaFecha", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
