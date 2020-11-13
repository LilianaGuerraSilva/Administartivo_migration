VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsRendicionVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsRendicionVista"
Private Const CM_MESSAGE_NAME As String = "Vista Rendición"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetViewNameSaldoRendicionesAbiertas() As String
   GetViewNameSaldoRendicionesAbiertas = "IGV_SaldoRencionesAbiertas"
End Function

Public Function fSqlViewSaldoRendicionesAbiertas() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = "SELECT Rendicion.ConsecutivoCompania, Rendicion.Consecutivo, "
   vSQL = vSQL & " Rendicion.CodigoBeneficiario,    Rendicion.FechaApertura , "
   vSQL = vSQL & " ISNull(SUM(anticipo.MontoTotal - (anticipo.MontoUsado + anticipo.MontoDevuelto)),0) as  TotalAnticipos "
   '                   CASE WHEN SUM(anticipo.MontoTotal - (anticipo.MontoUsado + anticipo.MontoDevuelto)) IS NULL
   '                   THEN 0 ELSE SUM(anticipo.MontoTotal - (anticipo.MontoUsado + anticipo.MontoDevuelto)) END AS TotalAnticipos
   vSQL = vSQL & " FROM Rendicion LEFT OUTER JOIN "
   vSQL = vSQL & " anticipo ON Rendicion.CodigoBeneficiario = anticipo.CodigoProveedor AND "
   vSQL = vSQL & " Rendicion.Consecutivo = anticipo.ConsecutivoRendicion AND "
   vSQL = vSQL & " Rendicion.ConsecutivoCompania = anticipo.ConsecutivoCompania"
   vSQL = vSQL & " WHERE (Rendicion.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusRendicion.eSRd_EN_PROCESO) & ") "
   vSQL = vSQL & " GROUP BY Rendicion.ConsecutivoCompania, Rendicion.Consecutivo, Rendicion.CodigoBeneficiario, Rendicion.FechaApertura"
h_EXIT:     On Error GoTo 0
   fSqlViewSaldoRendicionesAbiertas = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewSaldoRendicionesAbiertas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
