VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCxCSp"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsCxCSp"
End Function

Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsCxCSp"
End Function

Public Function fCreateDropCxCSP(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      vResult = vResult And fCreateSpCxCCorregirStatusYMontoAbonado(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpINS(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpUPD(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpDel(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateFnCanBeAnular(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateFnCanBeDeleted(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateFnCanBeUpdate(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpExecuteAnular(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpExecuteDel(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpS10INS(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpS10UPD(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpS10Anular(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpS10DEL(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpS10SEL(valShowMessage, valWithEncryption)
   Else
      vResult = vResult And fDropSpCxCCorregirStatusYMontoAbonado(valShowMessage)
      vResult = vResult And fDropSpS10SEL(valShowMessage)
      vResult = vResult And fDropSpS10DEL(valShowMessage)
      vResult = vResult And fDropSpS10Anular(valShowMessage)
      vResult = vResult And fDropSpS10UPD(valShowMessage)
      vResult = vResult And fDropSpS10INS(valShowMessage)
      vResult = vResult And fDropSpExecuteDel(valShowMessage)
      vResult = vResult And fDropSpExecuteAnular(valShowMessage)
      vResult = vResult And fDropFnCanBeUpdate(valShowMessage)
      vResult = vResult And fDropFnCanBeDeleted(valShowMessage)
      vResult = vResult And fDropFnCanBeAnular(valShowMessage)
      vResult = vResult And fDropSpDel(valShowMessage)
      vResult = vResult And fDropSpUPD(valShowMessage)
      vResult = vResult And fDropSpINS(valShowMessage)
   End If
   fCreateDropCxCSP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropCxCSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpINS(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameINS(), fSpParametersINS(), fStatementsSpINS(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpINS(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameINS(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameINS() As String
   GetSpNameINS = "CxCINS"
End Function

Private Function fSpParametersINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @Numero " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "         @TipoCxc " & gUtilSQL.getCharTypeForDB(1) & "," & vbCrLf
   vResult = vResult & "         @Status " & gUtilSQL.getCharTypeForDB(1) & " = '0'," & vbCrLf
   vResult = vResult & "         @CodigoCliente " & gUtilSQL.getVarCharTypeForDB(10) & " = ''," & vbCrLf
   vResult = vResult & "         @CodigoVendedor " & gUtilSQL.getVarCharTypeForDB(5) & " = ''," & vbCrLf
   vResult = vResult & "         @Origen " & gUtilSQL.getCharTypeForDB(1) & " = '1'," & vbCrLf
   vResult = vResult & "         @Fecha " & gUtilSQL.getDateTypeForDB() & " = ''," & vbCrLf
   vResult = vResult & "         @FechaCancelacion " & gUtilSQL.getDateTypeForDB() & " = ''," & vbCrLf
   vResult = vResult & "         @FechaVencimiento " & gUtilSQL.getDateTypeForDB() & " = ''," & vbCrLf
   vResult = vResult & "         @FechaAnulacion " & gUtilSQL.getDateTypeForDB() & " = ''," & vbCrLf
   vResult = vResult & "         @MontoExento " & gUtilSQL.getNumericTypeForDB(12, 2) & " = 0," & vbCrLf
   vResult = vResult & "         @MontoGravado " & gUtilSQL.getNumericTypeForDB(12, 2) & " = 0," & vbCrLf
   vResult = vResult & "         @MontoIva " & gUtilSQL.getNumericTypeForDB(12, 2) & " = 0," & vbCrLf
   vResult = vResult & "         @MontoAbonado " & gUtilSQL.getNumericTypeForDB(12, 2) & " = 0," & vbCrLf
   vResult = vResult & "         @Descripcion " & gUtilSQL.getVarCharTypeForDB(7000) & " = ''," & vbCrLf
   vResult = vResult & "         @Moneda " & gUtilSQL.getVarCharTypeForDB(80) & " = ''," & vbCrLf
   vResult = vResult & "         @CambioAbolivares " & gUtilSQL.getNumericTypeForDB(12, 2) & " = 1," & vbCrLf
   vResult = vResult & "         @CodigoCc " & gUtilSQL.getVarCharTypeForDB(5) & " = ''," & vbCrLf
   vResult = vResult & "         @CentroDeCostos " & gUtilSQL.getVarCharTypeForDB(40) & " = ''," & vbCrLf
   vResult = vResult & "         @SeRetuvoIva " & gUtilSQL.getCharTypeForDB(1) & " = 'N'," & vbCrLf
   vResult = vResult & "         @NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(20) & " = ''," & vbCrLf
   vResult = vResult & "         @RefinanciadoSn " & gUtilSQL.getCharTypeForDB(1) & " = '1'," & vbCrLf
   vResult = vResult & "         @NoAplicaParaLibroDeVentas " & gUtilSQL.getCharTypeForDB(1) & " = 'N'," & vbCrLf
   vResult = vResult & "         @CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & " = ''," & vbCrLf
   vResult = vResult & "         @CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & " = ''," & vbCrLf
   vResult = vResult & "         @CodigoTipoDeDocumentoLey varchar(10) = ''," & vbCrLf
   vResult = vResult & "         @AplicaDetraccion char(1) = 'N'," & vbCrLf
   vResult = vResult & "         @NumeroDetraccion varchar(15) = ''," & vbCrLf
   vResult = vResult & "         @CodigoDetraccion varchar(10) = ''," & vbCrLf
   vResult = vResult & "         @DescripcionDeDetraccion varchar(100) = ''," & vbCrLf
   vResult = vResult & "         @PorcentajeDetraccion decimal(25,5) = 0," & vbCrLf
   vResult = vResult & "         @TotalDetraccion decimal(25,5) = 0," & vbCrLf
   vResult = vResult & "         @StatusDetraccion char(1) = '0'," & vbCrLf
   vResult = vResult & "         @ConsecutivoMovimiento int = 0," & vbCrLf
   vResult = vResult & "   	 @NumeroControl varchar(11) = ''," & vbCrLf
   vResult = vResult & "   	 @VieneDeCreditoElectronico char(1) = 'N'," & vbCrLf
   vResult = vResult & "         @NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
    fSpParametersINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   SET @FechaUltimaModificacion = CONVERT(" & gUtilSQL.getDateTypeForDB() & ", GETDATE(), 103)" & vbCrLf
   vResult = vResult & "   INSERT INTO CxC(" & vbCrLf
   vResult = vResult & "         ConsecutivoCompania," & vbCrLf
   vResult = vResult & "         Numero," & vbCrLf
   vResult = vResult & "         Status," & vbCrLf
   vResult = vResult & "         TipoCxc," & vbCrLf
   vResult = vResult & "         CodigoCliente," & vbCrLf
   vResult = vResult & "         CodigoVendedor," & vbCrLf
   vResult = vResult & "         Origen," & vbCrLf
   vResult = vResult & "         Fecha," & vbCrLf
   vResult = vResult & "         FechaCancelacion," & vbCrLf
   vResult = vResult & "         FechaVencimiento," & vbCrLf
   vResult = vResult & "         FechaAnulacion," & vbCrLf
   vResult = vResult & "         MontoExento," & vbCrLf
   vResult = vResult & "         MontoGravado," & vbCrLf
   vResult = vResult & "         MontoIva," & vbCrLf
   vResult = vResult & "         MontoAbonado," & vbCrLf
   vResult = vResult & "         Descripcion," & vbCrLf
   vResult = vResult & "         Moneda," & vbCrLf
   vResult = vResult & "         CambioAbolivares," & vbCrLf
   vResult = vResult & "         CodigoCc," & vbCrLf
   vResult = vResult & "         CentroDeCostos," & vbCrLf
   vResult = vResult & "         SeRetuvoIva," & vbCrLf
   vResult = vResult & "         NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "         RefinanciadoSn," & vbCrLf
   vResult = vResult & "         NoAplicaParaLibroDeVentas," & vbCrLf
   vResult = vResult & "         CodigoLote," & vbCrLf
   vResult = vResult & "         CodigoMoneda," & vbCrLf
   vResult = vResult & "         CodigoTipoDeDocumentoLey," & vbCrLf
   vResult = vResult & "         AplicaDetraccion," & vbCrLf
   vResult = vResult & "         NumeroDetraccion," & vbCrLf
   vResult = vResult & "         CodigoDetraccion," & vbCrLf
   vResult = vResult & "         DescripcionDeDetraccion," & vbCrLf
   vResult = vResult & "         PorcentajeDetraccion," & vbCrLf
   vResult = vResult & "         TotalDetraccion," & vbCrLf
   vResult = vResult & "         StatusDetraccion," & vbCrLf
   vResult = vResult & "         ConsecutivoMovimiento," & vbCrLf
   vResult = vResult & "         NumeroControl," & vbCrLf
   vResult = vResult & "         VieneDeCreditoElectronico," & vbCrLf
   vResult = vResult & "         NombreOperador," & vbCrLf
   vResult = vResult & "         FechaUltimaModificacion" & vbCrLf
   vResult = vResult & "      )VALUES(" & vbCrLf
   vResult = vResult & "         @ConsecutivoCompania," & vbCrLf
   vResult = vResult & "         @Numero," & vbCrLf
   vResult = vResult & "         @Status," & vbCrLf
   vResult = vResult & "         @TipoCxc," & vbCrLf
   vResult = vResult & "         @CodigoCliente," & vbCrLf
   vResult = vResult & "         @CodigoVendedor," & vbCrLf
   vResult = vResult & "         @Origen," & vbCrLf
   vResult = vResult & "         @Fecha," & vbCrLf
   vResult = vResult & "         @FechaCancelacion," & vbCrLf
   vResult = vResult & "         @FechaVencimiento," & vbCrLf
   vResult = vResult & "         @FechaAnulacion," & vbCrLf
   vResult = vResult & "         @MontoExento," & vbCrLf
   vResult = vResult & "         @MontoGravado," & vbCrLf
   vResult = vResult & "         @MontoIva," & vbCrLf
   vResult = vResult & "         @MontoAbonado," & vbCrLf
   vResult = vResult & "         @Descripcion," & vbCrLf
   vResult = vResult & "         @Moneda," & vbCrLf
   vResult = vResult & "         @CambioAbolivares," & vbCrLf
   vResult = vResult & "         @CodigoCc," & vbCrLf
   vResult = vResult & "         @CentroDeCostos," & vbCrLf
   vResult = vResult & "         @SeRetuvoIva," & vbCrLf
   vResult = vResult & "         @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "         @RefinanciadoSn," & vbCrLf
   vResult = vResult & "         @NoAplicaParaLibroDeVentas," & vbCrLf
   vResult = vResult & "         @CodigoLote," & vbCrLf
   vResult = vResult & "         @CodigoMoneda," & vbCrLf
   vResult = vResult & "         @CodigoTipoDeDocumentoLey," & vbCrLf
   vResult = vResult & "         @AplicaDetraccion," & vbCrLf
   vResult = vResult & "         @NumeroDetraccion," & vbCrLf
   vResult = vResult & "         @CodigoDetraccion," & vbCrLf
   vResult = vResult & "         @DescripcionDeDetraccion," & vbCrLf
   vResult = vResult & "         @PorcentajeDetraccion," & vbCrLf
   vResult = vResult & "         @TotalDetraccion," & vbCrLf
   vResult = vResult & "         @StatusDetraccion," & vbCrLf
   vResult = vResult & "         @ConsecutivoMovimiento," & vbCrLf
   vResult = vResult & "         @NumeroControl," & vbCrLf
   vResult = vResult & "         @VieneDeCreditoElectronico," & vbCrLf
   vResult = vResult & "         @NombreOperador," & vbCrLf
   vResult = vResult & "         @FechaUltimaModificacion" & vbCrLf
   vResult = vResult & "      )" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpUPD(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameUPD(), fSpParametersUPD(), fStatementsSpUPD(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpUPD(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameUPD(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameUPD() As String
   GetSpNameUPD = "CxCUPD"
End Function

Private Function fSpParametersUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @Numero " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "         @TipoCxc " & gUtilSQL.getCharTypeForDB(1) & "," & vbCrLf
   vResult = vResult & "         @Status " & gUtilSQL.getCharTypeForDB(1) & " = NULL," & vbCrLf
   vResult = vResult & "         @CodigoCliente " & gUtilSQL.getVarCharTypeForDB(10) & " = NULL," & vbCrLf
   vResult = vResult & "         @CodigoVendedor " & gUtilSQL.getVarCharTypeForDB(5) & " = NULL," & vbCrLf
   vResult = vResult & "         @Origen " & gUtilSQL.getCharTypeForDB(1) & " = NULL," & vbCrLf
   vResult = vResult & "         @Fecha " & gUtilSQL.getDateTypeForDB() & " = NULL," & vbCrLf
   vResult = vResult & "         @FechaCancelacion " & gUtilSQL.getDateTypeForDB() & " = NULL," & vbCrLf
   vResult = vResult & "         @FechaVencimiento " & gUtilSQL.getDateTypeForDB() & " = NULL," & vbCrLf
   vResult = vResult & "         @FechaAnulacion " & gUtilSQL.getDateTypeForDB() & " = NULL," & vbCrLf
   vResult = vResult & "         @MontoExento " & gUtilSQL.getNumericTypeForDB(12, 2) & " = NULL," & vbCrLf
   vResult = vResult & "         @MontoGravado " & gUtilSQL.getNumericTypeForDB(12, 2) & " = NULL," & vbCrLf
   vResult = vResult & "         @MontoIva " & gUtilSQL.getNumericTypeForDB(12, 2) & " = NULL," & vbCrLf
   vResult = vResult & "         @MontoAbonado " & gUtilSQL.getNumericTypeForDB(12, 2) & " = NULL," & vbCrLf
   vResult = vResult & "         @Descripcion " & gUtilSQL.getVarCharTypeForDB(7000) & " = NULL," & vbCrLf
   vResult = vResult & "         @Moneda " & gUtilSQL.getVarCharTypeForDB(80) & " = NULL," & vbCrLf
   vResult = vResult & "         @CambioAbolivares " & gUtilSQL.getNumericTypeForDB(12, 2) & " = NULL," & vbCrLf
   vResult = vResult & "         @CodigoCc " & gUtilSQL.getVarCharTypeForDB(5) & " = NULL," & vbCrLf
   vResult = vResult & "         @CentroDeCostos " & gUtilSQL.getVarCharTypeForDB(40) & " = NULL," & vbCrLf
   vResult = vResult & "         @SeRetuvoIva " & gUtilSQL.getCharTypeForDB(1) & " = NULL," & vbCrLf
   vResult = vResult & "         @NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(20) & " = NULL," & vbCrLf
   vResult = vResult & "         @RefinanciadoSn " & gUtilSQL.getCharTypeForDB(1) & " = NULL," & vbCrLf
   vResult = vResult & "         @NoAplicaParaLibroDeVentas " & gUtilSQL.getCharTypeForDB(1) & " = NULL," & vbCrLf
   vResult = vResult & "         @CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & " = NULL," & vbCrLf
   vResult = vResult & "         @CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & " = NULL," & vbCrLf
   vResult = vResult & "   @CodigoTipoDeDocumentoLey varchar(10) = NULL," & vbCrLf
   vResult = vResult & "   @AplicaDetraccion char(1) = NULL," & vbCrLf
   vResult = vResult & "   @NumeroDetraccion varchar(15) = NULL," & vbCrLf
   vResult = vResult & "   @CodigoDetraccion varchar(10) = NULL," & vbCrLf
   vResult = vResult & "   @DescripcionDeDetraccion varchar(100) = NULL," & vbCrLf
   vResult = vResult & "   @PorcentajeDetraccion decimal(25,5) = NULL," & vbCrLf
   vResult = vResult & "   @TotalDetraccion decimal(25,5) = NULL," & vbCrLf
   vResult = vResult & "   @StatusDetraccion char(1) = NULL," & vbCrLf
   vResult = vResult & "   @ConsecutivoMovimiento int = NULL," & vbCrLf
   vResult = vResult & "   @NumeroControl varchar(11) = NULL," & vbCrLf
   vResult = vResult & "   @VieneDeCreditoElectronico char(1) = NULL," & vbCrLf
   vResult = vResult & "         @NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & " = NULL" & vbCrLf
    fSpParametersUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Status " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoCliente " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoVendedor " & gUtilSQL.getVarCharTypeForDB(5) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Origen " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Fecha " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Ori_FechaCancelacion " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Ori_FechaVencimiento " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Ori_FechaAnulacion " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MontoExento " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MontoGravado " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MontoIva " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MontoAbonado " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Descripcion " & gUtilSQL.getVarCharTypeForDB(7000) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Moneda " & gUtilSQL.getVarCharTypeForDB(80) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CambioAbolivares " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoCc " & gUtilSQL.getVarCharTypeForDB(5) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CentroDeCostos " & gUtilSQL.getVarCharTypeForDB(40) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_SeRetuvoIva " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(20) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_RefinanciadoSn " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NoAplicaParaLibroDeVentas " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoTipoDeDocumentoLey " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_AplicaDetraccion " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroDetraccion " & gUtilSQL.getVarCharTypeForDB(15) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoDetraccion " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_DescripcionDeDetraccion " & gUtilSQL.getVarCharTypeForDB(100) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_PorcentajeDetraccion " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TotalDetraccion " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_StatusDetraccion " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_ConsecutivoMovimiento " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_VieneDeCreditoElectronico " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TimeStamp timestamp" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Status " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoCliente " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoVendedor " & gUtilSQL.getVarCharTypeForDB(5) & vbCrLf
   vResult = vResult & "   DECLARE @Up_Origen " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_Fecha " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Up_FechaCancelacion " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Up_FechaVencimiento " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Up_FechaAnulacion " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoExento " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoGravado " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoIva " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoAbonado " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_Descripcion " & gUtilSQL.getVarCharTypeForDB(7000) & vbCrLf
   vResult = vResult & "   DECLARE @Up_Moneda " & gUtilSQL.getVarCharTypeForDB(80) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CambioAbolivares " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoCc " & gUtilSQL.getVarCharTypeForDB(5) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CentroDeCostos " & gUtilSQL.getVarCharTypeForDB(40) & vbCrLf
   vResult = vResult & "   DECLARE @Up_SeRetuvoIva " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(20) & vbCrLf
   vResult = vResult & "   DECLARE @Up_RefinanciadoSn " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NoAplicaParaLibroDeVentas " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoTipoDeDocumentoLey " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Up_AplicaDetraccion " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroDetraccion " & gUtilSQL.getVarCharTypeForDB(15) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoDetraccion " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Up_DescripcionDeDetraccion " & gUtilSQL.getVarCharTypeForDB(100) & vbCrLf
   vResult = vResult & "   DECLARE @Up_PorcentajeDetraccion " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_TotalDetraccion " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_StatusDetraccion " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_ConsecutivoMovimiento " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroControl " & gUtilSQL.getVarCharTypeForDB(11) & vbCrLf
   vResult = vResult & "   DECLARE @Up_VieneDeCreditoElectronico " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   BEGIN TRAN" & vbCrLf
   vResult = vResult & "   SELECT" & vbCrLf
   vResult = vResult & "         @Ori_Status = Status," & vbCrLf
   vResult = vResult & "         @Ori_CodigoCliente = CodigoCliente," & vbCrLf
   vResult = vResult & "         @Ori_CodigoVendedor = CodigoVendedor," & vbCrLf
   vResult = vResult & "         @Ori_Origen = Origen," & vbCrLf
   vResult = vResult & "         @Ori_Fecha = Fecha," & vbCrLf
   vResult = vResult & "         @Ori_FechaCancelacion = FechaCancelacion," & vbCrLf
   vResult = vResult & "         @Ori_FechaVencimiento = FechaVencimiento," & vbCrLf
   vResult = vResult & "         @Ori_FechaAnulacion = FechaAnulacion," & vbCrLf
   vResult = vResult & "         @Ori_MontoExento = MontoExento," & vbCrLf
   vResult = vResult & "         @Ori_MontoGravado = MontoGravado," & vbCrLf
   vResult = vResult & "         @Ori_MontoIva = MontoIva," & vbCrLf
   vResult = vResult & "         @Ori_MontoAbonado = MontoAbonado," & vbCrLf
   vResult = vResult & "         @Ori_Descripcion = Descripcion," & vbCrLf
   vResult = vResult & "         @Ori_Moneda = Moneda," & vbCrLf
   vResult = vResult & "         @Ori_CambioAbolivares = CambioAbolivares," & vbCrLf
   vResult = vResult & "         @Ori_CodigoCc = CodigoCc," & vbCrLf
   vResult = vResult & "         @Ori_CentroDeCostos = CentroDeCostos," & vbCrLf
   vResult = vResult & "         @Ori_SeRetuvoIva = SeRetuvoIva," & vbCrLf
   vResult = vResult & "         @Ori_NumeroDocumentoOrigen = NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "         @Ori_RefinanciadoSn = RefinanciadoSn," & vbCrLf
   vResult = vResult & "         @Ori_NoAplicaParaLibroDeVentas = NoAplicaParaLibroDeVentas," & vbCrLf
   vResult = vResult & "         @Ori_CodigoLote = CodigoLote," & vbCrLf
   vResult = vResult & "         @Ori_CodigoMoneda = CodigoMoneda," & vbCrLf
   vResult = vResult & "          @Ori_CodigoTipoDeDocumentoLey = CodigoTipoDeDocumentoLey," & vbCrLf
   vResult = vResult & "          @Ori_AplicaDetraccion = AplicaDetraccion," & vbCrLf
   vResult = vResult & "          @Ori_NumeroDetraccion = NumeroDetraccion," & vbCrLf
   vResult = vResult & "          @Ori_CodigoDetraccion = CodigoDetraccion," & vbCrLf
   vResult = vResult & "          @Ori_DescripcionDeDetraccion = DescripcionDeDetraccion," & vbCrLf
   vResult = vResult & "          @Ori_PorcentajeDetraccion = PorcentajeDetraccion," & vbCrLf
   vResult = vResult & "          @Ori_TotalDetraccion = TotalDetraccion," & vbCrLf
   vResult = vResult & "          @Ori_StatusDetraccion = StatusDetraccion," & vbCrLf
   vResult = vResult & "          @Ori_ConsecutivoMovimiento = ConsecutivoMovimiento," & vbCrLf
   vResult = vResult & "          @Ori_NumeroControl = NumeroControl," & vbCrLf
   vResult = vResult & "          @Ori_VieneDeCreditoElectronico = VieneDeCreditoElectronico," & vbCrLf
   vResult = vResult & "         @Ori_NombreOperador = NombreOperador," & vbCrLf
   vResult = vResult & "         @Ori_TimeStamp = fldTimeStamp" & vbCrLf
   vResult = vResult & "      FROM CxC" & vbCrLf
   vResult = vResult & "      WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "         AND Numero = @Numero" & vbCrLf
   vResult = vResult & "   IF @Status IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Status = @Ori_Status" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Status = @Status" & vbCrLf
   vResult = vResult & "   IF @CodigoCliente IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoCliente = @Ori_CodigoCliente" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoCliente = @CodigoCliente" & vbCrLf
   vResult = vResult & "   IF @CodigoVendedor IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoVendedor = @Ori_CodigoVendedor" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoVendedor = @CodigoVendedor" & vbCrLf
   vResult = vResult & "   IF @Origen IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Origen = @Ori_Origen" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Origen = @Origen" & vbCrLf
   vResult = vResult & "   IF @Fecha IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Fecha = @Ori_Fecha" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Fecha = @Fecha" & vbCrLf
   vResult = vResult & "   IF @FechaCancelacion IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_FechaCancelacion = @Ori_FechaCancelacion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_FechaCancelacion = @FechaCancelacion" & vbCrLf
   vResult = vResult & "   IF @FechaVencimiento IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_FechaVencimiento = @Ori_FechaVencimiento" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_FechaVencimiento = @FechaVencimiento" & vbCrLf
   vResult = vResult & "   IF @FechaAnulacion IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_FechaAnulacion = @Ori_FechaAnulacion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_FechaAnulacion = @FechaAnulacion" & vbCrLf
   vResult = vResult & "   IF @MontoExento IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_MontoExento = @Ori_MontoExento" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_MontoExento = @MontoExento" & vbCrLf
   vResult = vResult & "   IF @MontoGravado IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_MontoGravado = @Ori_MontoGravado" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_MontoGravado = @MontoGravado" & vbCrLf
   vResult = vResult & "   IF @MontoIva IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_MontoIva = @Ori_MontoIva" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_MontoIva = @MontoIva" & vbCrLf
   vResult = vResult & "   IF @MontoAbonado IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_MontoAbonado = @Ori_MontoAbonado" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_MontoAbonado = @MontoAbonado" & vbCrLf
   vResult = vResult & "   IF @Descripcion IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Descripcion = @Ori_Descripcion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Descripcion = @Descripcion" & vbCrLf
   vResult = vResult & "   IF @Moneda IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Moneda = @Ori_Moneda" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Moneda = @Moneda" & vbCrLf
   vResult = vResult & "   IF @CambioAbolivares IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_CambioAbolivares = @Ori_CambioAbolivares" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CambioAbolivares = @CambioAbolivares" & vbCrLf
   vResult = vResult & "   IF @CodigoCc IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoCc = @Ori_CodigoCc" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoCc = @CodigoCc" & vbCrLf
   vResult = vResult & "   IF @CentroDeCostos IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_CentroDeCostos = @Ori_CentroDeCostos" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CentroDeCostos = @CentroDeCostos" & vbCrLf
   vResult = vResult & "   IF @SeRetuvoIva IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_SeRetuvoIva = @Ori_SeRetuvoIva" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_SeRetuvoIva = @SeRetuvoIva" & vbCrLf
   vResult = vResult & "   IF @NumeroDocumentoOrigen IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroDocumentoOrigen = @Ori_NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroDocumentoOrigen = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "   IF @RefinanciadoSn IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_RefinanciadoSn = @Ori_RefinanciadoSn" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_RefinanciadoSn = @RefinanciadoSn" & vbCrLf
   vResult = vResult & "   IF @NoAplicaParaLibroDeVentas IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_NoAplicaParaLibroDeVentas = @Ori_NoAplicaParaLibroDeVentas" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NoAplicaParaLibroDeVentas = @NoAplicaParaLibroDeVentas" & vbCrLf
   vResult = vResult & "   IF @CodigoLote IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoLote = @Ori_CodigoLote" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoLote = @CodigoLote" & vbCrLf
   vResult = vResult & "   IF @CodigoMoneda IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoMoneda = @Ori_CodigoMoneda" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoMoneda = @CodigoMoneda" & vbCrLf
   vResult = vResult & "   IF @CodigoTipoDeDocumentoLey is null" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoTipoDeDocumentoLey = @Ori_CodigoTipoDeDocumentoLey" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoTipoDeDocumentoLey = @CodigoTipoDeDocumentoLey" & vbCrLf
   vResult = vResult & "   IF @AplicaDetraccion is null" & vbCrLf
   vResult = vResult & "      SET @Up_AplicaDetraccion = @Ori_AplicaDetraccion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_AplicaDetraccion = @AplicaDetraccion" & vbCrLf
   vResult = vResult & "   IF @NumeroDetraccion is null" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroDetraccion = @Ori_NumeroDetraccion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroDetraccion = @NumeroDetraccion" & vbCrLf
   vResult = vResult & "   IF @CodigoDetraccion is null" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoDetraccion = @Ori_CodigoDetraccion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoDetraccion = @CodigoDetraccion" & vbCrLf
   vResult = vResult & "   IF @DescripcionDeDetraccion is null" & vbCrLf
   vResult = vResult & "      SET @Up_DescripcionDeDetraccion = @Ori_DescripcionDeDetraccion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_DescripcionDeDetraccion = @DescripcionDeDetraccion" & vbCrLf
   vResult = vResult & "   IF @PorcentajeDetraccion is null" & vbCrLf
   vResult = vResult & "      SET @Up_PorcentajeDetraccion = @Ori_PorcentajeDetraccion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_PorcentajeDetraccion = @PorcentajeDetraccion" & vbCrLf
   vResult = vResult & "   IF @TotalDetraccion is null" & vbCrLf
   vResult = vResult & "      SET @Up_TotalDetraccion = @Ori_TotalDetraccion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TotalDetraccion = @TotalDetraccion" & vbCrLf
   vResult = vResult & "   IF @StatusDetraccion is null" & vbCrLf
   vResult = vResult & "      SET @Up_StatusDetraccion = @Ori_StatusDetraccion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_StatusDetraccion = @StatusDetraccion" & vbCrLf
   vResult = vResult & "   IF @ConsecutivoMovimiento is null" & vbCrLf
   vResult = vResult & "      SET @Up_ConsecutivoMovimiento = @Ori_ConsecutivoMovimiento" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_ConsecutivoMovimiento = @ConsecutivoMovimiento" & vbCrLf
   vResult = vResult & "   IF @NumeroControl is null" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroControl = @Ori_NumeroControl" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroControl = @NumeroControl" & vbCrLf
   vResult = vResult & "   IF @VieneDeCreditoElectronico is null" & vbCrLf
   vResult = vResult & "      SET @Up_VieneDeCreditoElectronico = @Ori_VieneDeCreditoElectronico" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_VieneDeCreditoElectronico = @VieneDeCreditoElectronico" & vbCrLf
   vResult = vResult & "   IF @NombreOperador IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_NombreOperador = @Ori_NombreOperador" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NombreOperador = @NombreOperador" & vbCrLf
   vResult = vResult & "   SET @FechaUltimaModificacion = CONVERT(" & gUtilSQL.getDateTypeForDB() & ", GETDATE(), 103)" & vbCrLf
   vResult = vResult & "   UPDATE CxC" & vbCrLf
   vResult = vResult & "   SET" & vbCrLf
   vResult = vResult & "      Status = @Up_Status," & vbCrLf
   vResult = vResult & "      CodigoCliente = @Up_CodigoCliente," & vbCrLf
   vResult = vResult & "      CodigoVendedor = @Up_CodigoVendedor," & vbCrLf
   vResult = vResult & "      Origen = @Up_Origen," & vbCrLf
   vResult = vResult & "      Fecha = @Up_Fecha," & vbCrLf
   vResult = vResult & "      FechaCancelacion = @Up_FechaCancelacion," & vbCrLf
   vResult = vResult & "      FechaVencimiento = @Up_FechaVencimiento," & vbCrLf
   vResult = vResult & "      FechaAnulacion = @Up_FechaAnulacion," & vbCrLf
   vResult = vResult & "      MontoExento = @Up_MontoExento," & vbCrLf
   vResult = vResult & "      MontoGravado = @Up_MontoGravado," & vbCrLf
   vResult = vResult & "      MontoIva = @Up_MontoIva," & vbCrLf
   vResult = vResult & "      MontoAbonado = @Up_MontoAbonado," & vbCrLf
   vResult = vResult & "      Descripcion = @Up_Descripcion," & vbCrLf
   vResult = vResult & "      Moneda = @Up_Moneda," & vbCrLf
   vResult = vResult & "      CambioAbolivares = @Up_CambioAbolivares," & vbCrLf
   vResult = vResult & "      CodigoCc = @Up_CodigoCc," & vbCrLf
   vResult = vResult & "      CentroDeCostos = @Up_CentroDeCostos," & vbCrLf
   vResult = vResult & "      SeRetuvoIva = @Up_SeRetuvoIva," & vbCrLf
   vResult = vResult & "      NumeroDocumentoOrigen = @Up_NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "      RefinanciadoSn = @Up_RefinanciadoSn," & vbCrLf
   vResult = vResult & "      NoAplicaParaLibroDeVentas = @Up_NoAplicaParaLibroDeVentas," & vbCrLf
   vResult = vResult & "      CodigoLote = @Up_CodigoLote," & vbCrLf
   vResult = vResult & "      CodigoMoneda = @Up_CodigoMoneda," & vbCrLf
   vResult = vResult & "         CodigoTipoDeDocumentoLey = @Up_CodigoTipoDeDocumentoLey," & vbCrLf
   vResult = vResult & "         AplicaDetraccion = @Up_AplicaDetraccion," & vbCrLf
   vResult = vResult & "         NumeroDetraccion = @Up_NumeroDetraccion," & vbCrLf
   vResult = vResult & "         CodigoDetraccion = @Up_CodigoDetraccion," & vbCrLf
   vResult = vResult & "         DescripcionDeDetraccion = @Up_DescripcionDeDetraccion," & vbCrLf
   vResult = vResult & "         PorcentajeDetraccion = @Up_PorcentajeDetraccion," & vbCrLf
   vResult = vResult & "         TotalDetraccion = @Up_TotalDetraccion," & vbCrLf
   vResult = vResult & "         StatusDetraccion = @Up_StatusDetraccion," & vbCrLf
   vResult = vResult & "         ConsecutivoMovimiento = @Up_ConsecutivoMovimiento," & vbCrLf
   vResult = vResult & "         NumeroControl = @Up_NumeroControl," & vbCrLf
   vResult = vResult & "         VieneDeCreditoElectronico = @Up_VieneDeCreditoElectronico," & vbCrLf
   vResult = vResult & "      NombreOperador = @Up_NombreOperador," & vbCrLf
   vResult = vResult & "      FechaUltimaModificacion = @FechaUltimaModificacion" & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "      AND Numero = @Numero" & vbCrLf
   vResult = vResult & "      AND TipoCxc = @TipoCxc" & vbCrLf
   vResult = vResult & "      AND fldTimeStamp = @Ori_TimeStamp" & vbCrLf
   vResult = vResult & "   IF @@ERROR = 0" & vbCrLf
   vResult = vResult & "      COMMIT TRAN" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      ROLLBACK" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpDel(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameDel(), fSpParametersDel(), fStatementsSpDel(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpDel(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameDel(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameDel() As String
   GetSpNameDel = "CxCDEL"
End Function

Private Function fSpParametersDel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @Numero " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "         @TipoCxc " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
    fSpParametersDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpDel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DELETE FROM CxC" & vbCrLf
   vResult = vResult & "      WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "         AND Numero = @Numero" & vbCrLf
   vResult = vResult & "         AND TipoCxc = @TipoCxc" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnCanBeDeleted(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameCanBeDeleted(), fFnParametersCanBeDeleted(), fFnScalarReturnDataTypeCanBeDeleted(), fStatementsFnCanBeDeleted(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnCanBeDeleted = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnCanBeDeleted", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnCanBeDeleted(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameCanBeDeleted(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnCanBeDeleted = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnCanBeDeleted", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameCanBeDeleted() As String
   GetFnNameCanBeDeleted = "CxCCanDel"
End Function

Private Function fFnParametersCanBeDeleted() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@Numero " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   vResult = vResult & "@TipoCxc " & gUtilSQL.getCharTypeForDB(1)
   fFnParametersCanBeDeleted = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersCanBeDeleted", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeCanBeDeleted() As String
   fFnScalarReturnDataTypeCanBeDeleted = "bit"
End Function

Private Function fStatementsFnCanBeDeleted() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Result bit" & vbCrLf
   vResult = vResult & "   DECLARE @Continue bit" & vbCrLf
   vResult = vResult & "   DECLARE @MontoAbonado " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Origen " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   SET @Result = 0 --False" & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT Numero FROM CxC WHERE ConsecutivoCompania = @ConsecutivoCompania AND Numero = @Numero AND TipoCxc = @TipoCxc)" & vbCrLf
   vResult = vResult & "      SET @Continue = 1 --True" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Continue = 0 --False" & vbCrLf
   vResult = vResult & "   IF @Continue = 1 --True" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT @MontoAbonado = MontoAbonado," & vbCrLf
   vResult = vResult & "            @Origen = Origen" & vbCrLf
   vResult = vResult & "         FROM CxC" & vbCrLf
   vResult = vResult & "         WHERE" & vbCrLf
   vResult = vResult & "            ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "            AND Numero = @Numero" & vbCrLf
   vResult = vResult & "            AND TipoCxc = @TipoCxc" & vbCrLf
   vResult = vResult & "      IF @MontoAbonado = 0" & vbCrLf
   vResult = vResult & "         IF @Origen = '1' --Origen Manual" & vbCrLf
   vResult = vResult & "            IF NOT EXISTS(SELECT NumeroCobranza FROM DocumentoCobrado WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroDelDocumentoCobrado = @Numero and TipoDeDocumentoCobrado = @TipoCxc)" & vbCrLf
   vResult = vResult & "               SET @Result = 1 --True" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnCanBeDeleted = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnCanBeDeleted", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnCanBeAnular(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameCanBeAnular(), fFnParametersCanBeAnular(), fFnScalarReturnDataTypeCanBeAnular(), fStatementsFnCanBeAnular(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnCanBeAnular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnCanBeAnular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnCanBeAnular(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameCanBeAnular(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnCanBeAnular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnCanBeAnular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameCanBeAnular() As String
   GetFnNameCanBeAnular = "CxCCanAnular"
End Function

Private Function fFnParametersCanBeAnular() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@Numero " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   vResult = vResult & "@TipoCxc " & gUtilSQL.getCharTypeForDB(1)
   fFnParametersCanBeAnular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersCanBeAnular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeCanBeAnular() As String
   fFnScalarReturnDataTypeCanBeAnular = "bit"
End Function

Private Function fStatementsFnCanBeAnular() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Result bit" & vbCrLf
   vResult = vResult & "   DECLARE @Continue bit" & vbCrLf
   vResult = vResult & "   DECLARE @MontoAbonado " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Origen " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @TipoFactura " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @NumeroDocumentoOrigen varchar(20)" & vbCrLf
   vResult = vResult & "   DECLARE @StatusFactura " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @StatusCxC " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   SET @Result = 0 --False" & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT Numero FROM CxC WHERE ConsecutivoCompania = @ConsecutivoCompania AND Numero = @Numero AND TipoCxc = @TipoCxc)" & vbCrLf
   vResult = vResult & "      SET @Continue = 1 --True" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Continue = 0 --False" & vbCrLf
   vResult = vResult & "   IF @Continue = 1 --True" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT @StatusCxC = Status," & vbCrLf
   vResult = vResult & "            @MontoAbonado = MontoAbonado," & vbCrLf
   vResult = vResult & "            @Origen = Origen," & vbCrLf
   vResult = vResult & "            @NumeroDocumentoOrigen = NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "         FROM CxC" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "            AND Numero = @Numero" & vbCrLf
   vResult = vResult & "            AND TipoCxc = @TipoCxc" & vbCrLf
   vResult = vResult & "      IF @StatusCxC = '4' --Anulado" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --False" & vbCrLf
   vResult = vResult & "      ELSE IF @StatusCxC <> '0' --Por Cancelar" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --False" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "         IF @MontoAbonado = 0" & vbCrLf
   vResult = vResult & "            IF @Origen = '1' --Origen Manual" & vbCrLf
   vResult = vResult & "               SET @Result = 1 --True" & vbCrLf
   vResult = vResult & "            ELSE --Origen Factura" & vbCrLf
   vResult = vResult & "               IF @NumeroDocumentoOrigen <> ''" & vbCrLf
   vResult = vResult & "               BEGIN" & vbCrLf
   vResult = vResult & "                  IF @TipoCxc = '3' --CxC Nota de Crédito" & vbCrLf
   vResult = vResult & "                     SET @TipoFactura = 1 --Fac Nota de Crédito" & vbCrLf
   vResult = vResult & "                  ELSE IF @TipoCxc = '4' --CxC Nota de Débito" & vbCrLf
   vResult = vResult & "                     SET @TipoFactura = '2' --Fac Nota de Débito" & vbCrLf
   vResult = vResult & "                  ELSE --IF @TipoCxc = 0 --CxC Factura" & vbCrLf
   vResult = vResult & "                     SET @TipoFactura = '0' --Fac Factura" & vbCrLf
   vResult = vResult & "                  SELECT @StatusFactura = StatusFactura" & vbCrLf
   vResult = vResult & "                     FROM Factura" & vbCrLf
   vResult = vResult & "                     WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                        AND Numero = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "                        AND TipoDeDocumento = @TipoFactura" & vbCrLf
   vResult = vResult & "                  IF @StatusFactura = '1' --Fact Anulada" & vbCrLf
   vResult = vResult & "                     IF NOT EXISTS(SELECT Numero" & vbCrLf
   vResult = vResult & "                                 FROM Cobranza INNER JOIN DocumentoCobrado" & vbCrLf
   vResult = vResult & "                                    ON Cobranza.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                                    AND Cobranza.Numero = DocumentoCobrado.NumeroCobranza" & vbCrLf
   vResult = vResult & "                                 WHERE Cobranza.ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                                    AND Cobranza.StatusCobranza = '0'" & vbCrLf
   vResult = vResult & "                                    AND DocumentoCobrado.NumeroDelDocumentoCobrado = @Numero" & vbCrLf
   vResult = vResult & "                                    AND DocumentoCobrado.TipoDeDocumentoCobrado = @TipoCxc)" & vbCrLf
   vResult = vResult & "                        SET @Result = 1 --True" & vbCrLf
   vResult = vResult & "               END" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnCanBeAnular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnCanBeAnular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnCanBeUpdate(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameCanBeUpdate(), fFnParametersCanBeUpdate(), fFnScalarReturnDataTypeCanBeUpdate(), fStatementsFnCanBeUpdate(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnCanBeUpdate = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnCanBeUpdate", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnCanBeUpdate(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameCanBeUpdate(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnCanBeUpdate = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnCanBeUpdate", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameCanBeUpdate() As String
   GetFnNameCanBeUpdate = "CxCCanUpd"
End Function

Private Function fFnParametersCanBeUpdate() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@Numero " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   vResult = vResult & "@TipoCxc " & gUtilSQL.getCharTypeForDB(1)
   fFnParametersCanBeUpdate = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersCanBeUpdate", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeCanBeUpdate() As String
   fFnScalarReturnDataTypeCanBeUpdate = "bit"
End Function

Private Function fStatementsFnCanBeUpdate() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Result bit" & vbCrLf
   vResult = vResult & "   DECLARE @Continue bit" & vbCrLf
   vResult = vResult & "   DECLARE @MontoAbonado " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Origen " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   SET @Result = 0 --False" & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT Numero FROM CxC WHERE ConsecutivoCompania = @ConsecutivoCompania AND Numero = @Numero AND TipoCxc = @TipoCxc)" & vbCrLf
   vResult = vResult & "      SET @Continue = 1 --True" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Continue = 0 --False" & vbCrLf
   vResult = vResult & "   IF @Continue = 1 --True" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT @MontoAbonado = MontoAbonado," & vbCrLf
   vResult = vResult & "            @Origen = Origen" & vbCrLf
   vResult = vResult & "         FROM CxC" & vbCrLf
   vResult = vResult & "         Where" & vbCrLf
   vResult = vResult & "            ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "            AND Numero = @Numero" & vbCrLf
   vResult = vResult & "            AND TipoCxc = @TipoCxc" & vbCrLf
   vResult = vResult & "      IF @MontoAbonado = 0" & vbCrLf
   vResult = vResult & "         IF @Origen = '1' --Origen Manual" & vbCrLf
   vResult = vResult & "            IF NOT EXISTS(SELECT NumeroCobranza FROM DocumentoCobrado WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroDelDocumentoCobrado = @Numero and TipoDeDocumentoCobrado = @TipoCxc)" & vbCrLf
   vResult = vResult & "               SET @Result = 1 --True" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnCanBeUpdate = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnCanBeUpdate", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpExecuteAnular(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameExecuteAnular(), fSpParametersExecuteAnular(), fStatementsSpExecuteAnular(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpExecuteAnular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpExecuteAnular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpExecuteAnular(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameExecuteAnular(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpExecuteAnular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpExecuteAnular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameExecuteAnular() As String
   GetSpNameExecuteAnular = "ExecuteCxCAnular"
End Function

Private Function fSpParametersExecuteAnular() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @P_ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @P_Numero " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "         @P_TipoCxc " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
    fSpParametersExecuteAnular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersExecuteAnular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpExecuteAnular() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @CanBe bit" & vbCrLf
   vResult = vResult & "   DECLARE @Operador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Fecha " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @CompaniaUsaModuloDeContabilidadSN " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxCEnum " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxCChar " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT Numero FROM CxC WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND Numero = @P_Numero AND TipoCxc = @P_TipoCxc)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @CanBe = dbo.Gf_CxCCanAnular(@P_ConsecutivoCompania, @P_Numero, @P_TipoCxc)" & vbCrLf
   vResult = vResult & "      IF @CanBe = 1 --True" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SET @Fecha = CONVERT(" & gUtilSQL.getDateTypeForDB() & ", GETDATE(), 103)" & vbCrLf
   vResult = vResult & "         SET @Operador = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "         SELECT @CompaniaUsaModuloDeContabilidadSN = UsaModuloDeContabilidad FROM Compania WHERE ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "         IF (UPPER(@CompaniaUsaModuloDeContabilidadSN) = 'S')" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @GeneradoPorCxCEnum = 9 --CxC" & vbCrLf
   vResult = vResult & "            SET @GeneradoPorCxCChar = dbo.Gf_EnumToDBValue(@GeneradoPorCxCEnum) --CxC" & vbCrLf
   vResult = vResult & "            SET @NumeroDocumentoOrigen = @P_TipoCxc + char(9) + @P_Numero" & vbCrLf
   vResult = vResult & "            SELECT @NumeroComprobante = COMPROBANTE.Numero," & vbCrLf
   vResult = vResult & "                  @ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "               FROM COMPROBANTE INNER JOIN PERIODO" & vbCrLf
   vResult = vResult & "                  ON COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "               WHERE PERIODO.ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                  AND COMPROBANTE.NoDocumentoOrigen = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "                  AND COMPROBANTE.GeneradoPor = @GeneradoPorCxCChar" & vbCrLf
   vResult = vResult & "         END" & vbCrLf
   vResult = vResult & "         EXEC Gp_CxCUPD" & vbCrLf
   vResult = vResult & "            @ConsecutivoCompania = @P_ConsecutivoCompania," & vbCrLf
   vResult = vResult & "            @Numero = @P_Numero," & vbCrLf
   vResult = vResult & "            @TipoCxc = @P_TipoCxc," & vbCrLf
   vResult = vResult & "            @Status = '4'," & vbCrLf
   vResult = vResult & "            @FechaAnulacion = @Fecha," & vbCrLf
   vResult = vResult & "            @NombreOperador = @Operador" & vbCrLf
   vResult = vResult & "         IF (UPPER(@CompaniaUsaModuloDeContabilidadSN) = 'S')" & vbCrLf
   vResult = vResult & "            EXEC Gp_ComprobanteAnular @ConsecutivoPeriodo, @NumeroComprobante, @Operador" & vbCrLf
   vResult = vResult & "      END" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpExecuteAnular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpExecuteAnular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpExecuteDel(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameExecuteDel(), fSpParametersExecuteDel(), fStatementsSpExecuteDel(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpExecuteDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpExecuteDel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpExecuteDel(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameExecuteDel(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpExecuteDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpExecuteDel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameExecuteDel() As String
   GetSpNameExecuteDel = "ExecuteCxCDel"
End Function

Private Function fSpParametersExecuteDel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @P_ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @P_Numero " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "         @P_TipoCxc " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
    fSpParametersExecuteDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersExecuteDel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpExecuteDel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @CanBe bit" & vbCrLf
   vResult = vResult & "   DECLARE @CompaniaUsaModuloDeContabilidadSN " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxCEnum " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxCChar " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT Numero FROM CxC WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND Numero = @P_Numero AND TipoCxc = @P_TipoCxc)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @CanBe = dbo.Gf_CxCCanDel(@P_ConsecutivoCompania, @P_Numero, @P_TipoCxc)" & vbCrLf
   vResult = vResult & "      IF @CanBe = 1 --True" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SELECT @CompaniaUsaModuloDeContabilidadSN = UsaModuloDeContabilidad FROM Compania WHERE ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "         IF (UPPER(@CompaniaUsaModuloDeContabilidadSN) = 'S')" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @GeneradoPorCxCEnum = 9 --CxC" & vbCrLf
   vResult = vResult & "            SET @GeneradoPorCxCChar = dbo.Gf_EnumToDBValue(@GeneradoPorCxCEnum) --CxC" & vbCrLf
   vResult = vResult & "            SET @NumeroDocumentoOrigen = @P_TipoCxc + char(9) + @P_Numero" & vbCrLf
   vResult = vResult & "            SELECT @NumeroComprobante = COMPROBANTE.Numero," & vbCrLf
   vResult = vResult & "                  @ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "               FROM COMPROBANTE INNER JOIN PERIODO" & vbCrLf
   vResult = vResult & "                  ON COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "               WHERE PERIODO.ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                  AND COMPROBANTE.NoDocumentoOrigen = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "                  AND COMPROBANTE.GeneradoPor = @GeneradoPorCxCChar" & vbCrLf
   vResult = vResult & "         END" & vbCrLf
   vResult = vResult & "         EXEC Gp_CxCDEL @P_ConsecutivoCompania, @P_Numero, @P_TipoCxc" & vbCrLf
   vResult = vResult & "         IF (UPPER(@CompaniaUsaModuloDeContabilidadSN) = 'S')" & vbCrLf
   vResult = vResult & "            EXEC Gp_ComprobanteDel @ConsecutivoPeriodo, @NumeroComprobante" & vbCrLf
   vResult = vResult & "      END" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpExecuteDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpExecuteDel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpS10INS(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10INS(), fSpParametersS10INS(), fStatementsSpS10INS(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10INS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10INS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10INS(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10INS(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10INS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10INS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10INS() As String
   GetSpNameS10INS = "S10_CxCINS"
End Function

Private Function fSpParametersS10INS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @P_ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @P_Numero " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "         @P_CodigoTipoDeDocumentoCxC " & gUtilSQL.getVarCharTypeForDB(10) & " = '01'," & vbCrLf
   vResult = vResult & "         @P_CodigoCliente " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "         @P_Fecha " & gUtilSQL.getDateTypeForDB() & " = ''," & vbCrLf
   vResult = vResult & "         @P_FechaVencimiento " & gUtilSQL.getDateTypeForDB() & " = ''," & vbCrLf
   vResult = vResult & "         @P_CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & " = ''," & vbCrLf
   vResult = vResult & "         @P_CambioAMonedaLocal " & gUtilSQL.getNumericTypeForDB(12, 2) & " = 1," & vbCrLf
   vResult = vResult & "         @P_MontoExento " & gUtilSQL.getNumericTypeForDB(12, 2) & " = 0," & vbCrLf
   vResult = vResult & "         @P_MontoGravado " & gUtilSQL.getNumericTypeForDB(12, 2) & " = 0," & vbCrLf
   vResult = vResult & "         @P_MontoIva " & gUtilSQL.getNumericTypeForDB(12, 2) & " = 0," & vbCrLf
   vResult = vResult & "         @P_Descripcion " & gUtilSQL.getVarCharTypeForDB(7000) & " = ''," & vbCrLf
   vResult = vResult & "         @P_CodigoVendedor " & gUtilSQL.getVarCharTypeForDB(5) & " = ''," & vbCrLf
   vResult = vResult & "         @P_AplicaParaLibroDeVentas " & gUtilSQL.getCharTypeForDB(1) & " = 'S'," & vbCrLf
   vResult = vResult & "         @P_AplicaDetraccion " & gUtilSQL.getCharTypeForDB(1) & " = 'N'," & vbCrLf
   vResult = vResult & "         @P_CodigoDetraccion " & gUtilSQL.getVarCharTypeForDB(10) & " = ''" & vbCrLf
   fSpParametersS10INS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10INS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10INS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Operador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Hoy " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @FechaCxC " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @FechaVenc " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @CodMoneda " & gUtilSQL.getVarCharTypeForDB(4) & vbCrLf
   vResult = vResult & "   DECLARE @NombreMoneda " & gUtilSQL.getVarCharTypeForDB(80) & vbCrLf
   vResult = vResult & "   DECLARE @CodVendedor " & gUtilSQL.getVarCharTypeForDB(5) & vbCrLf
   vResult = vResult & "   DECLARE @TipoCxc " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @DesccripcionDeDetraccion" & gUtilSQL.getVarCharTypeForDB(100) & vbCrLf
   vResult = vResult & "   DECLARE @PorcentajeDetraccion money" & vbCrLf
   vResult = vResult & "   DECLARE @TotalDetraccion money"
   vResult = vResult & "   DECLARE @TotalMontoCxC money"
   vResult = vResult & "   IF NOT EXISTS(SELECT ConsecutivoCompania FROM COMPANIA WHERE ConsecutivoCompania = @P_ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "      RAISERROR ('No existe registro de Compañía para el Consecutivo dado%d.', 16, 1, @P_ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT Codigo FROM Cliente WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND Codigo = @P_CodigoCliente)" & vbCrLf
   vResult = vResult & "         RAISERROR ('No existe registro de Cliente para el Código dado%s.', 16, 1, @P_CodigoCliente)" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SELECT @TipoCxc = TipoDeDocumento FROM TipoDeDocumentoLey WHERE (Codigo = @P_CodigoTipoDeDocumentoCxC)" & vbCrLf
   vResult = vResult & "         IF (@TipoCxc IS NULL)" & vbCrLf
   vResult = vResult & "            SET @TipoCxc = '0'" & vbCrLf
   vResult = vResult & "         SET @Operador = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "         SET @Hoy = CONVERT(smalldatetime, GETDATE(),103)" & vbCrLf
   vResult = vResult & "         IF @P_Fecha > @Hoy" & vbCrLf
   vResult = vResult & "            SET @FechaCxC = @Hoy" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "            SET @FechaCxC = @P_Fecha" & vbCrLf
   vResult = vResult & "         IF @P_FechaVencimiento = ''" & vbCrLf
   vResult = vResult & "            SET @FechaVenc = @Hoy" & vbCrLf
   vResult = vResult & "         IF CONVERT(smalldatetime, @FechaCxC, 103) < CONVERT(smalldatetime, @FechaVenc, 103)" & vbCrLf
   vResult = vResult & "            SET @FechaVenc = @P_Fecha" & vbCrLf
   vResult = vResult & "         IF EXISTS(SELECT Nombre FROM Moneda WHERE Codigo = @P_CodigoMoneda)" & vbCrLf
   vResult = vResult & "            SELECT @NombreMoneda = Nombre, @CodMoneda = Codigo FROM Moneda WHERE Codigo = @P_CodigoMoneda" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "            IF @P_CambioAMonedaLocal > 1" & vbCrLf
   'vResult = vResult & "               SELECT TOP 1 @NombreMoneda = NombreMonedaExtranjera, @CodMoneda = CodigoMonedaExtranjera FROM ParametrosAdministrativo" & vbCrLf
   vResult = vResult & "                SELECT TOP 1"
   vResult = vResult & "                @NombreMoneda = SettValueByCompany1.Value,"
   vResult = vResult & "                @CodMoneda = SettValueByCompany2.Value"
   vResult = vResult & "                FROM Comun.SettValueByCompany Stt"
   vResult = vResult & "                CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'NombreMonedaExtranjera') AS SettValueByCompany1(Value)"
   vResult = vResult & "                CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'CodigoMonedaExtranjera') AS SettValueByCompany2(Value)"
   vResult = vResult & "                GROUP BY SettValueByCompany1.Value,SettValueByCompany2.Value"
   vResult = vResult & "            ELSE" & vbCrLf
   'vResult = vResult & "               SELECT TOP 1 @NombreMoneda = NombreMonedaLocal, @CodMoneda = CodigoMonedaLocal FROM ParametrosAdministrativo" & vbCrLf
   vResult = vResult & "                SELECT TOP 1"
   vResult = vResult & "                @NombreMoneda = SettValueByCompany1.Value,"
   vResult = vResult & "                @CodMoneda = SettValueByCompany2.Value"
   vResult = vResult & "                FROM Comun.SettValueByCompany Stt"
   vResult = vResult & "                CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'NombreMonedaExtranjera') AS SettValueByCompany1(Value)"
   vResult = vResult & "                CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'CodigoMonedaExtranjera') AS SettValueByCompany2(Value)"
   vResult = vResult & "                GROUP BY SettValueByCompany1.Value, SettValueByCompany2.Value"
   vResult = vResult & "         IF EXISTS(SELECT Codigo FROM Vendedor WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND Codigo = @P_CodigoVendedor)" & vbCrLf
   vResult = vResult & "            SET @CodVendedor = @P_CodigoVendedor" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   'vResult = vResult & "            SELECT @CodVendedor = CodigoGenericoVendedor FROM ParametrosCompania WHERE ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "             SELECT @CodVendedor = Value FROM Comun.SettValueByCompany" & vbCrLf
   vResult = vResult & "             WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND NameSettDefinition = " & gUtilSQL.fSimpleSqlValue("CodigoGenericoVendedor") & vbCrLf
   
   vResult = vResult & "            SELECT  @DesccripcionDeDetraccion = BienServicio,  @PorcentajeDetraccion = Porcentaje FROM Detracciones WHERE Codigo = @P_CodigoDetraccion" & vbCrLf
   vResult = vResult & "            IF (@PorcentajeDetraccion Is Null) AND UPPER(@P_AplicaDetraccion) = 'S'" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SET @P_CodigoDetraccion = ''" & vbCrLf
   vResult = vResult & "               SET @DesccripcionDeDetraccion = ''" & vbCrLf
   vResult = vResult & "               SET @PorcentajeDetraccion = 0" & vbCrLf
   vResult = vResult & "               SET @TotalDetraccion = 0" & vbCrLf
   vResult = vResult & "            END" & vbCrLf
   vResult = vResult & "            ELSE" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SET @TotalMontoCxC = @P_MontoExento + @P_MontoGravado + @P_MontoIva" & vbCrLf
   vResult = vResult & "               SET @TotalDetraccion = @TotalMontoCxC * (@PorcentajeDetraccion/100)" & vbCrLf
   vResult = vResult & "            END" & vbCrLf
   
   vResult = vResult & "         IF EXISTS(SELECT Numero FROM CxC WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND Numero = @P_Numero AND TipoCxc = @TipoCxc)" & vbCrLf
   vResult = vResult & "            RAISERROR('Ya existe un registro para los datos suministrados para Cuenta por Cobrar',16,1)" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "            EXEC Gp_CxCINS" & vbCrLf
   vResult = vResult & "               @ConsecutivoCompania = @P_ConsecutivoCompania," & vbCrLf
   vResult = vResult & "               @Numero = @P_Numero," & vbCrLf
   vResult = vResult & "               @TipoCxc = @TipoCxc," & vbCrLf
   vResult = vResult & "               @Status = '0'," & vbCrLf
   vResult = vResult & "               @CodigoCliente = @P_CodigoCliente," & vbCrLf
   vResult = vResult & "               @CodigoVendedor = @CodVendedor," & vbCrLf
   vResult = vResult & "               @Origen = '1'," & vbCrLf
   vResult = vResult & "               @Fecha = @FechaCxC," & vbCrLf
   vResult = vResult & "               @FechaCancelacion = @FechaCxC," & vbCrLf
   vResult = vResult & "               @FechaVencimiento = @FechaVenc," & vbCrLf
   vResult = vResult & "               @FechaAnulacion = @FechaCxC," & vbCrLf
   vResult = vResult & "               @MontoExento = @P_MontoExento," & vbCrLf
   vResult = vResult & "               @MontoGravado = @P_MontoGravado," & vbCrLf
   vResult = vResult & "               @MontoIva = @P_MontoIva," & vbCrLf
   vResult = vResult & "               @MontoAbonado = 0," & vbCrLf
   vResult = vResult & "               @Descripcion = @P_Descripcion," & vbCrLf
   vResult = vResult & "               @Moneda = @NombreMoneda," & vbCrLf
   vResult = vResult & "               @CambioAbolivares = @P_CambioAMonedaLocal," & vbCrLf
   vResult = vResult & "               @CodigoCc = ''," & vbCrLf
   vResult = vResult & "               @CentroDeCostos = ''," & vbCrLf
   vResult = vResult & "               @SeRetuvoIva = 'N'," & vbCrLf
   vResult = vResult & "               @NumeroDocumentoOrigen = @P_Numero," & vbCrLf
   vResult = vResult & "               @RefinanciadoSn = '1'," & vbCrLf
   vResult = vResult & "               @NoAplicaParaLibroDeVentas =  @P_AplicaParaLibroDeVentas," & vbCrLf
   vResult = vResult & "               @CodigoLote = ''," & vbCrLf
   vResult = vResult & "               @CodigoMoneda = @CodMoneda," & vbCrLf
   vResult = vResult & "               @CodigoTipoDeDocumentoLey = @P_CodigoTipoDeDocumentoCxC," & vbCrLf
   vResult = vResult & "               @AplicaDetraccion = @P_AplicaDetraccion," & vbCrLf
   vResult = vResult & "               @NumeroDetraccion = ''," & vbCrLf

   vResult = vResult & "               @CodigoDetraccion = @P_CodigoDetraccion,"
   vResult = vResult & "               @DescripcionDeDetraccion = @DesccripcionDeDetraccion,"
   vResult = vResult & "               @PorcentajeDetraccion = @PorcentajeDetraccion,"
   vResult = vResult & "               @TotalDetraccion = @TotalDetraccion,"

   vResult = vResult & "               @StatusDetraccion = '2'," & vbCrLf
   vResult = vResult & "               @ConsecutivoMovimiento = 0," & vbCrLf
   vResult = vResult & "               @NombreOperador = @Operador" & vbCrLf
   vResult = vResult & "      END" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpS10INS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10INS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpS10UPD(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10UPD(), fSpParametersS10UPD(), fStatementsSpS10UPD(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10UPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10UPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10UPD(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10UPD(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10UPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10UPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10UPD() As String
   GetSpNameS10UPD = "S10_CxCUPD"
End Function

Private Function fSpParametersS10UPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @P_ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @P_Numero " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "         @P_CodigoTipoDeDocumentoCxc " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "         @P_CodigoCliente " & gUtilSQL.getVarCharTypeForDB(10) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_Fecha " & gUtilSQL.getDateTypeForDB() & " = NULL," & vbCrLf
   vResult = vResult & "         @P_FechaVencimiento " & gUtilSQL.getDateTypeForDB() & " = NULL," & vbCrLf
   vResult = vResult & "         @P_CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_CambioAMonedaLocal " & gUtilSQL.getNumericTypeForDB(12, 2) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_MontoExento " & gUtilSQL.getNumericTypeForDB(12, 2) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_MontoGravado " & gUtilSQL.getNumericTypeForDB(12, 2) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_MontoIva " & gUtilSQL.getNumericTypeForDB(12, 2) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_Descripcion " & gUtilSQL.getVarCharTypeForDB(7000) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_CodigoVendedor " & gUtilSQL.getVarCharTypeForDB(5) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_AplicaParaLibroDeVentas " & gUtilSQL.getCharTypeForDB(1) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_AplicaDetraccion " & gUtilSQL.getCharTypeForDB(1) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_CodigoDetraccion " & gUtilSQL.getVarCharTypeForDB(10) & " = NULL" & vbCrLf
    fSpParametersS10UPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10UPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10UPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Operador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @CanBeUpdated bit" & vbCrLf
   vResult = vResult & "   DECLARE @CompaniaUsaModuloDeContabilidadSN " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxCEnum " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxCChar " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @CodMoneda " & gUtilSQL.getVarCharTypeForDB(4) & vbCrLf
   vResult = vResult & "   DECLARE @NombreMoneda " & gUtilSQL.getVarCharTypeForDB(80) & vbCrLf
   vResult = vResult & "   DECLARE @TipoCxc " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @DesccripcionDeDetraccion" & gUtilSQL.getVarCharTypeForDB(100) & vbCrLf
   vResult = vResult & "   DECLARE @PorcentajeDetraccion money" & vbCrLf
   vResult = vResult & "   DECLARE @TotalDetraccion money"
   vResult = vResult & "   DECLARE @TotalMontoCxC money"
   vResult = vResult & "   IF EXISTS(SELECT Numero FROM CxC WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND Numero = @P_Numero AND TipoCxc = @TipoCxc)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @CanBeUpdated = dbo.Gf_CxCCanUpd(@P_ConsecutivoCompania, @P_Numero, @TipoCxc)" & vbCrLf
   vResult = vResult & "      IF @CanBeUpdated = 1 --True" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SELECT @TipoCxc = TipoDeDocumento FROM TipoDeDocumentoLey WHERE (Codigo = @P_CodigoTipoDeDocumentoCxC)" & vbCrLf
   vResult = vResult & "         SET @Operador = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "         SELECT @CompaniaUsaModuloDeContabilidadSN = UsaModuloDeContabilidad FROM Compania WHERE ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "         IF (UPPER(@CompaniaUsaModuloDeContabilidadSN) = 'S')" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @GeneradoPorCxCEnum = 9 --CxC" & vbCrLf
   vResult = vResult & "            SET @GeneradoPorCxCChar = dbo.Gf_EnumToDBValue(@GeneradoPorCxCEnum) --CxC" & vbCrLf
   vResult = vResult & "            SET @NumeroDocumentoOrigen = @TipoCxc + char(9) + @P_Numero" & vbCrLf
   vResult = vResult & "            SELECT @NumeroComprobante = COMPROBANTE.Numero," & vbCrLf
   vResult = vResult & "                  @ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "               FROM COMPROBANTE INNER JOIN PERIODO" & vbCrLf
   vResult = vResult & "                  ON COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "               WHERE PERIODO.ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                  AND COMPROBANTE.NoDocumentoOrigen = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "                  AND COMPROBANTE.GeneradoPor = @GeneradoPorCxCChar" & vbCrLf
   vResult = vResult & "            EXEC Gp_ComprobanteDEL @ConsecutivoPeriodo, @NumeroComprobante" & vbCrLf
   vResult = vResult & "         END" & vbCrLf
   vResult = vResult & "            SELECT  @DesccripcionDeDetraccion = BienServicio,  @PorcentajeDetraccion = Porcentaje FROM Detracciones WHERE Codigo = @P_CodigoDetraccion" & vbCrLf
   vResult = vResult & "            IF (@PorcentajeDetraccion Is Null) AND UPPER(@P_AplicaDetraccion) = 'S'" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SET @P_CodigoDetraccion = ''" & vbCrLf
   vResult = vResult & "               SET @DesccripcionDeDetraccion = ''" & vbCrLf
   vResult = vResult & "               SET @PorcentajeDetraccion = 0" & vbCrLf
   vResult = vResult & "               SET @TotalDetraccion = 0" & vbCrLf
   vResult = vResult & "            END" & vbCrLf
   vResult = vResult & "            ELSE" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SET @TotalMontoCxC = @P_MontoExento + @P_MontoGravado + @P_MontoIva" & vbCrLf
   vResult = vResult & "               SET @TotalDetraccion = @TotalMontoCxC * (@PorcentajeDetraccion/100)" & vbCrLf
   vResult = vResult & "            END" & vbCrLf
   vResult = vResult & "         IF EXISTS(SELECT Nombre FROM Moneda WHERE Codigo = @P_CodigoMoneda)" & vbCrLf
   vResult = vResult & "            SELECT @NombreMoneda = Nombre, @CodMoneda = Codigo FROM Moneda WHERE Codigo = @P_CodigoMoneda" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "            IF @P_CambioAMonedaLocal > 1" & vbCrLf
   'vResult = vResult & "               SELECT TOP 1 @NombreMoneda = NombreMonedaExtranjera, @CodMoneda = CodigoMonedaExtranjera FROM ParametrosAdministrativo" & vbCrLf
   vResult = vResult & "                SELECT TOP 1"
   vResult = vResult & "                @NombreMoneda = SettValueByCompany1.Value,"
   vResult = vResult & "                @CodMoneda = SettValueByCompany2.Value"
   vResult = vResult & "                FROM Comun.SettValueByCompany Stt"
   vResult = vResult & "                CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'NombreMonedaExtranjera') AS SettValueByCompany1(Value)"
   vResult = vResult & "                CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'CodigoMonedaExtranjera') AS SettValueByCompany2(Value)"
   vResult = vResult & "                GROUP BY SettValueByCompany1.Value, SettValueByCompany2.Value"
   vResult = vResult & "            ELSE" & vbCrLf
   'vResult = vResult & "               SELECT TOP 1 @NombreMoneda = NombreMonedaLocal, @CodMoneda = CodigoMonedaLocal FROM ParametrosAdministrativo" & vbCrLf
   vResult = vResult & "                SELECT TOP 1"
   vResult = vResult & "                @NombreMoneda = SettValueByCompany1.Value,"
   vResult = vResult & "                @CodMoneda = SettValueByCompany2.Value"
   vResult = vResult & "                FROM Comun.SettValueByCompany Stt"
   vResult = vResult & "                CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'NombreMonedaExtranjera') AS SettValueByCompany1(Value)"
   vResult = vResult & "                CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'CodigoMonedaExtranjera') AS SettValueByCompany2(Value)"
   vResult = vResult & "                GROUP BY SettValueByCompany1.Value, SettValueByCompany2.Value"
   vResult = vResult & "         EXEC Gp_CxCUPD" & vbCrLf
   vResult = vResult & "            @ConsecutivoCompania = @P_ConsecutivoCompania," & vbCrLf
   vResult = vResult & "            @Numero = @P_Numero," & vbCrLf
   vResult = vResult & "            @TipoCxc = @TipoCxc," & vbCrLf
   vResult = vResult & "            @CodigoCliente = @P_CodigoCliente," & vbCrLf
   vResult = vResult & "            @CodigoVendedor = @P_CodigoVendedor," & vbCrLf
   vResult = vResult & "            @Fecha = @P_Fecha," & vbCrLf
   vResult = vResult & "            @FechaVencimiento = @P_FechaVencimiento," & vbCrLf
   vResult = vResult & "            @MontoExento = @P_MontoExento," & vbCrLf
   vResult = vResult & "            @MontoGravado = @P_MontoGravado," & vbCrLf
   vResult = vResult & "            @MontoIva = @P_MontoIva," & vbCrLf
   vResult = vResult & "            @Descripcion = @P_Descripcion," & vbCrLf
   vResult = vResult & "            @Moneda = @NombreMoneda," & vbCrLf
   vResult = vResult & "            @CambioAbolivares = @P_CambioAMonedaLocal," & vbCrLf
   vResult = vResult & "            @CodigoMoneda = @CodMoneda," & vbCrLf
   vResult = vResult & "            @CodigoTipoDeDocumentoLey = @P_CodigoTipoDeDocumentoCxc," & vbCrLf
   vResult = vResult & "            @AplicaParaLibroDeVentas = @P_AplicaParaLibroDeVentas," & vbCrLf
   vResult = vResult & "            @AplicaDetraccion = @P_AplicaDetraccion," & vbCrLf
   vResult = vResult & "            @CodigoDetraccion = @P_CodigoDetraccion,"
   vResult = vResult & "            @DescripcionDeDetraccion = @DesccripcionDeDetraccion,"
   vResult = vResult & "            @PorcentajeDetraccion = @PorcentajeDetraccion,"
   vResult = vResult & "            @TotalDetraccion = @TotalDetraccion,"
   vResult = vResult & "            @NombreOperador = @Operador" & vbCrLf
   vResult = vResult & "      END" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpS10UPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10UPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpS10Anular(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10Anular(), fSpParametersS10Anular(), fStatementsSpS10Anular(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10Anular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10Anular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10Anular(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10Anular(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10Anular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10Anular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10Anular() As String
   GetSpNameS10Anular = "S10_CxCAnular"
End Function

Private Function fSpParametersS10Anular() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @P_ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @P_Numero " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "         @P_CodigoTipoDeDocumentoCxc  varchar(10) " & vbCrLf
    fSpParametersS10Anular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10Anular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10Anular() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @TipoCxc " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   SELECT @TipoCxc = TipoDeDocumento FROM TipoDeDocumentoLey WHERE (Codigo = @P_CodigoTipoDeDocumentoCxC)" & vbCrLf
   vResult = vResult & "   EXEC Gp_ExecuteCxCAnular @P_ConsecutivoCompania, @P_Numero, @TipoCxc" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpS10Anular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10Anular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpS10DEL(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10DEL(), fSpParametersS10DEL(), fStatementsSpS10DEL(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10DEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10DEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10DEL(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10DEL(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10DEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10DEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10DEL() As String
   GetSpNameS10DEL = "S10_CxCDEL"
End Function

Private Function fSpParametersS10DEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @P_ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @P_Numero " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "         @P_CodigoTipoDeDocumentoCxc  varchar(10) " & vbCrLf
    fSpParametersS10DEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10DEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10DEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @TipoCxc " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   SELECT @TipoCxc = TipoDeDocumento FROM TipoDeDocumentoLey WHERE (Codigo = @P_CodigoTipoDeDocumentoCxC)" & vbCrLf
   vResult = vResult & "   EXEC Gp_ExecuteCxCDel @P_ConsecutivoCompania, @P_Numero, @TipoCxc" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpS10DEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10DEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpS10SEL(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10SEL(), fSpParametersS10SEL(), fStatementsSpS10SEL(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10SEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10SEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10SEL(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10SEL(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10SEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10SEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10SEL() As String
   GetSpNameS10SEL = "S10_CxCSEL"
End Function

Private Function fSpParametersS10SEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "   @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "   @FixedWhereClause " & gUtilSQL.getVarCharTypeForDB(4000) & " = ''" & vbCrLf
    fSpParametersS10SEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10SEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10SEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Sql " & gUtilSQL.getVarCharTypeForDB(6000) & vbCrLf
   vResult = vResult & "   SET @Sql = 'SELECT Numero,  CodigoTipoDeDocumentoLey as CodigoTipoDeDocumentoLey, Status, CodigoCliente, CodigoVendedor," & vbCrLf
   vResult = vResult & "                  Fecha, FechaCancelacion, FechaVencimiento, FechaAnulacion," & vbCrLf
   vResult = vResult & "                  MontoExento, MontoGravado, MontoIva, MontoAbonado," & vbCrLf
   vResult = vResult & "                  Descripcion, CodigoMoneda, Moneda, CambioAbolivares AS CambioAMonedaLocal" & vbCrLf
   vResult = vResult & "               FROM CxC" & vbCrLf
   vResult = vResult & "               WHERE ConsecutivoCompania = ' + CAST(@ConsecutivoCompania as varchar)" & vbCrLf
   vResult = vResult & "   IF @FixedWhereClause <> ''" & vbCrLf
   vResult = vResult & "      SET @Sql = @Sql + ' AND ' + @FixedWhereClause" & vbCrLf
   vResult = vResult & "   EXEC(@Sql)" & vbCrLf
   vResult = vResult & "   RETURN @@ROWCOUNT" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpS10SEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10SEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *

Private Function fCreateSpCxCCorregirStatusYMontoAbonado(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameCxCCorregirStatusYMontoAbonado(), fSpParametersCxCCorregirStatusYMontoAbonado(), fStatementsSpCxCCorregirStatusYMontoAbonado(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpCxCCorregirStatusYMontoAbonado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpCxCCorregirStatusYMontoAbonado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpCxCCorregirStatusYMontoAbonado(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameCxCCorregirStatusYMontoAbonado(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpCxCCorregirStatusYMontoAbonado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpCxCCorregirStatusYMontoAbonado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameCxCCorregirStatusYMontoAbonado() As String
   GetSpNameCxCCorregirStatusYMontoAbonado = "CxCCorregirStatusYMontoAbonado"
End Function

Private Function fSpParametersCxCCorregirStatusYMontoAbonado() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "   @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "   @NumeroCxC " & gUtilSQL.getVarCharTypeForDB(20) & " = ''," & vbCrLf
   vResult = vResult & "   @TipoDeDocumentoCobrado " & gUtilSQL.getCharTypeForDB(1) & " = ''," & vbCrLf
   vResult = vResult & "   @CodigoCliente " & gUtilSQL.getVarCharTypeForDB(10) & " = ''," & vbCrLf
   vResult = vResult & "   @MontoAbonadoTotal " & gUtilSQL.getNumericTypeForDB(10, 4) & " = ''" & vbCrLf
    fSpParametersCxCCorregirStatusYMontoAbonado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersCxCCorregirStatusYMontoAbonado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpCxCCorregirStatusYMontoAbonado() As String
   Dim vResult As String
   
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @StatusCxC AS CHAR(1)" & vbCrLf
   vResult = vResult & "   DECLARE @MontoTotalCxC AS MONEY" & vbCrLf
   vResult = vResult & "   DECLARE @Aplicar AS VARCHAR(2)" & vbCrLf
   vResult = vResult & "   SET @Aplicar = 'SI'" & vbCrLf
   vResult = vResult & "   SET @StatusCxC = '0'" & vbCrLf
   vResult = vResult & "   SET NOCOUNT OFF;" & vbCrLf
   vResult = vResult & "   IF EXISTS( SELECT Numero FROM cxC WHERE Numero = @NumeroCxC AND ConsecutivoCompania = @ConsecutivoCompania AND CodigoCliente = @CodigoCliente)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      --SELECT  @MontoAbonadoTotal AS MontoAbonadoTotal" & vbCrLf
   vResult = vResult & "      SELECT @MontoTotalCxC = (MontoIVA + MontoGravado + MontoExento)" & vbCrLf
   vResult = vResult & "      FROM cxC" & vbCrLf
   vResult = vResult & "      WHERE Numero = @NumeroCxC AND cxC.ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "      AND cxc.TipoCxc  = @TipoDeDocumentoCobrado" & vbCrLf
   vResult = vResult & "      AND CodigoCliente = @CodigoCliente " & vbCrLf
   vResult = vResult & "      --SELECT @MontoTotalCxC AS MontoTotalCxC" & vbCrLf
   vResult = vResult & "      IF @Aplicar = 'SI'" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         IF (ABS(ABS(@MontoAbonadoTotal) - ABS(@MontoTotalCxC)) <= 0.01 AND ABS(ABS(@MontoAbonadoTotal) - ABS(@MontoTotalCxC))> 0 )" & vbCrLf
   vResult = vResult & "            SET @MontoAbonadoTotal = @MontoTotalCxC" & vbCrLf
   vResult = vResult & "         --SELECT @MontoAbonadoTotal AS MontoAbonadoTotal1" & vbCrLf
   vResult = vResult & "         UPDATE CxC SET MontoAbonado = @MontoAbonadoTotal WHERE" & vbCrLf
   vResult = vResult & "         Numero = @NumeroCxC AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "         AND cxc.TipoCxc  = @TipoDeDocumentoCobrado" & vbCrLf
   vResult = vResult & "         AND CodigoCliente = @CodigoCliente " & vbCrLf
   vResult = vResult & "         IF (@MontoAbonadoTotal = @MontoTotalCxC)" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               UPDATE CxC SET Status = '1' WHERE" & vbCrLf
   vResult = vResult & "               Numero = @NumeroCxC AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "               AND cxc.TipoCxc  = @TipoDeDocumentoCobrado" & vbCrLf
   vResult = vResult & "               AND CodigoCliente = @CodigoCliente " & vbCrLf
   vResult = vResult & "            END" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "            IF (@MontoAbonadoTotal = 0 )" & vbCrLf
   vResult = vResult & "               UPDATE CxC SET Status = '0' WHERE" & vbCrLf
   vResult = vResult & "               Numero = @NumeroCxC AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "               AND cxc.TipoCxc  = @TipoDeDocumentoCobrado" & vbCrLf
   vResult = vResult & "               AND CodigoCliente = @CodigoCliente " & vbCrLf
   vResult = vResult & "            ELSE" & vbCrLf
   vResult = vResult & "               UPDATE CxC SET Status = '3' WHERE" & vbCrLf
   vResult = vResult & "               Numero = @NumeroCxC AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "               AND cxc.TipoCxc  = @TipoDeDocumentoCobrado" & vbCrLf
   vResult = vResult & "               AND CodigoCliente = @CodigoCliente " & vbCrLf
   vResult = vResult & "            END" & vbCrLf
   vResult = vResult & "      END" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpCxCCorregirStatusYMontoAbonado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpCxCCorregirStatusYMontoAbonado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *

