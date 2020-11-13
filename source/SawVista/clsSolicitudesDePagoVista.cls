VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsSolicitudesDePagoVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsSolicitudesDePagoVista"
Private Const CM_MESSAGE_NAME As String = "Vista SolicitudesDePago"
Private gEnumProyecto As clsEnumAdministrativo

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function



Public Function GetViewNameBeneficiarioSolicitudesDePago_B1() As String
   GetViewNameBeneficiarioSolicitudesDePago_B1 = "IGV_BeneficiarioSolicitudesDePago_B1"
End Function

Public Function fSqlViewBeneficiarioSolicitudesDePago_B1() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
      vSQL = " SELECT "
      vSQL = vSQL & "Saw.Gv_Beneficiario_B1.ConsecutivoCompania,"
      vSQL = vSQL & "Saw.Gv_Beneficiario_B1.Consecutivo,"
      vSQL = vSQL & "Saw.Gv_SolicitudesDePago_B1.ConsecutivoSolicitud,"
      vSQL = vSQL & "Saw.Gv_Beneficiario_B1.Codigo,"
      vSQL = vSQL & "Saw.Gv_Beneficiario_B1.NumeroRIF AS RIF,"
      vSQL = vSQL & "Saw.Gv_Beneficiario_B1.NombreBeneficiario,"
      vSQL = vSQL & "Saw.Gv_Beneficiario_B1.Origen,"
      vSQL = vSQL & "Saw.Gv_Beneficiario_B1.OrigenStr,"
      vSQL = vSQL & "Saw.Gv_Beneficiario_B1.TipoDeBeneficiario,"
      vSQL = vSQL & "Saw.Gv_Beneficiario_B1.TipoDeBeneficiarioStr,"
      vSQL = vSQL & "Saw.Gv_RenglonSolicitudesDePago_B1.CuentaBancaria,"
      vSQL = vSQL & "Saw.Gv_RenglonSolicitudesDePago_B1.FormaDePago,"
      vSQL = vSQL & "Saw.Gv_RenglonSolicitudesDePago_B1.FormaDePagoStr,"
      vSQL = vSQL & "Saw.Gv_RenglonSolicitudesDePago_B1.Status,"
      vSQL = vSQL & "Saw.Gv_RenglonSolicitudesDePago_B1.StatusStr,"
      vSQL = vSQL & "Saw.Gv_RenglonSolicitudesDePago_B1.Monto,"
      vSQL = vSQL & "Saw.Gv_RenglonSolicitudesDePago_B1.NumeroDocumento,"
      vSQL = vSQL & "Saw.Gv_SolicitudesDePago_B1.FechaSolicitud,"
      vSQL = vSQL & "Saw.Gv_SolicitudesDePago_B1.NumeroDocumentoOrigen,"
      vSQL = vSQL & "Saw.Gv_SolicitudesDePago_B1.Status AS StatusSolicitud,"
      vSQL = vSQL & "Saw.Gv_SolicitudesDePago_B1.StatusStr AS StatusSolicitudStr,"
      vSQL = vSQL & "Saw.Gv_SolicitudesDePago_B1.GeneradoPor, Saw.Gv_SolicitudesDePago_B1.GeneradoPorStr,"
      vSQL = vSQL & "Saw.Gv_RenglonSolicitudesDePago_B1.consecutivoRenglon,"
      vSQL = vSQL & "CAST(Saw.Gv_SolicitudesDePago_B1.ConsecutivoSolicitud AS varchar) + CAST(Saw.Gv_RenglonSolicitudesDePago_B1.consecutivoRenglon AS varchar) AS NumeroAsociado "
      vSQL = vSQL & "FROM Saw.Gv_Beneficiario_B1 "
      vSQL = vSQL & "INNER JOIN   Saw.Gv_SolicitudesDePago_B1 ON "
      vSQL = vSQL & "Saw.Gv_Beneficiario_B1.ConsecutivoCompania = Saw.Gv_SolicitudesDePago_B1.ConsecutivoCompania "
      vSQL = vSQL & "INNER JOIN   Saw.Gv_RenglonSolicitudesDePago_B1 ON "
      vSQL = vSQL & "Saw.Gv_Beneficiario_B1.Consecutivo = Saw.Gv_RenglonSolicitudesDePago_B1.ConsecutivoBeneficiario AND "
      vSQL = vSQL & "Saw.Gv_Beneficiario_B1.ConsecutivoCompania = Saw.Gv_RenglonSolicitudesDePago_B1.ConsecutivoCompania AND "
      vSQL = vSQL & "Saw.Gv_SolicitudesDePago_B1.ConsecutivoSolicitud = Saw.Gv_RenglonSolicitudesDePago_B1.ConsecutivoSolicitud AND "
      vSQL = vSQL & "Saw.Gv_SolicitudesDePago_B1.ConsecutivoCompania = Saw.Gv_RenglonSolicitudesDePago_B1.ConsecutivoCompania "
  
  fSqlViewBeneficiarioSolicitudesDePago_B1 = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewBeneficiarioSolicitudesDePago_B1", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

