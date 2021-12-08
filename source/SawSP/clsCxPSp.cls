VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCxPSp"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsCxPSp"
End Function
Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsCxPSp"
End Function
Public Function fCreateDropCxPSP(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      vResult = vResult And fCreateSpCxPCorregirStatusYMontoAbonado(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpINS(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpDel(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpUPD(valShowMessage, valWithEncryption)
'
'      vResult = vResult And fCreateFnCanBeDeleted(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateFnCanBeUpdated(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateFnCanBeAnulado(valShowMessage, valWithEncryption)
'
'      vResult = vResult And fCreateSpExecuteAnular(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpExecuteDelete(valShowMessage, valWithEncryption) '42
'
'      vResult = vResult And fCreateSpS10Insert(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpS10Update(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpS10Anular(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpS10Delete(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpS10Select(valShowMessage, valWithEncryption)
'
'      vResult = vResult And fCreateSpS10RendicionInsert(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpGenerarNumero(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpGenerarNumeroCheque(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpInsertarDocumentoPagado(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpInsertarPagodesdeCxP(valShowMessage, valWithEncryption)
   Else
      vResult = vResult And fDropSpS10RendicionInsert(valShowMessage)
      
      vResult = vResult And fDropSpS10Select(valShowMessage)
      vResult = vResult And fDropSpS10Delete(valShowMessage)
      vResult = vResult And fDropSpS10Anular(valShowMessage)
      vResult = vResult And fDropSpS10Update(valShowMessage)
      vResult = vResult And fDropSpS10Insert(valShowMessage)
      
      vResult = vResult And fDropSpExecuteDelete(valShowMessage)
      vResult = vResult And fDropSpExecuteAnular(valShowMessage)
      
      vResult = vResult And fDropFnCanBeAnulado(valShowMessage)
      vResult = vResult And fDropFnCanBeUpdated(valShowMessage)
      vResult = vResult And fDropFnCanBeDeleted(valShowMessage)
      
      vResult = vResult And fDropSpUPD(valShowMessage)
      vResult = vResult And fDropSpDel(valShowMessage)
      vResult = vResult And fDropSpINS(valShowMessage)
      vResult = vResult And fDropSpCxPCorregirStatusYMontoAbonado(valShowMessage)
   End If
   fCreateDropCxPSP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropCxPSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Public Function fCreateSpINS(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
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
Public Function fDropSpINS(ByVal valShowMessage As Boolean) As Boolean
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
   GetSpNameINS = "CxPINS"
End Function
Private Function fSpParametersINS() As String
   Dim vResult As String
   
   vResult = "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoCxP " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "      @Numero " & gUtilSQL.getVarCharTypeForDB(25) & "," & vbCrLf
   vResult = vResult & "      @TipoDeCxP char(1) = '0'," & vbCrLf
   vResult = vResult & "      @Status char(1) = '0'," & vbCrLf
   vResult = vResult & "      @CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "      @Fecha " & gUtilSQL.getDateTypeForDB() & " = ''," & vbCrLf
   vResult = vResult & "      @FechaCancelacion " & gUtilSQL.getDateTypeForDB() & " = ''," & vbCrLf
   vResult = vResult & "      @FechaVencimiento " & gUtilSQL.getDateTypeForDB() & " = ''," & vbCrLf
   vResult = vResult & "      @FechaAnulacion " & gUtilSQL.getDateTypeForDB() & " = ''," & vbCrLf
   vResult = vResult & "      @Moneda " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "      @CambioABolivares money = 0," & vbCrLf
   vResult = vResult & "      @AplicaParaLibrodeCompras char(1) = 'N'," & vbCrLf
   vResult = vResult & "      @MontoExento money = 0," & vbCrLf
   vResult = vResult & "      @MontoGravado money = 0," & vbCrLf
   vResult = vResult & "      @MontoIva money = 0," & vbCrLf
   vResult = vResult & "      @MontoAbonado money = 0," & vbCrLf
   vResult = vResult & "      @MesDeAplicacion " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "      @AnoDeAplicacion " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "      @Observaciones " & gUtilSQL.getVarCharTypeForDB(255) & " = ''," & vbCrLf
   vResult = vResult & "      @CreditoFiscal char(1) = '0'," & vbCrLf
   vResult = vResult & "      @TipoDeCompra char(1) = '0'," & vbCrLf
   vResult = vResult & "      @SeHizoLaRetencion char(1) = 'N'," & vbCrLf
   vResult = vResult & "      @MontoGravableAlicuotaGeneral money = 0," & vbCrLf
   vResult = vResult & "      @MontoGravableAlicuota2 money = 0," & vbCrLf
   vResult = vResult & "      @MontoGravableAlicuota3 money = 0," & vbCrLf
   vResult = vResult & "      @MontoIVAAlicuotaGeneral money = 0," & vbCrLf
   vResult = vResult & "      @MontoIVAAlicuota2 money = 0," & vbCrLf
   vResult = vResult & "      @MontoIVAAlicuota3 money = 0," & vbCrLf
   vResult = vResult & "      @NumeroPlanillaDeImportacion " & gUtilSQL.getVarCharTypeForDB(20) & " = ''," & vbCrLf
   vResult = vResult & "      @NumeroExpedienteDeImportacion " & gUtilSQL.getVarCharTypeForDB(20) & " = ''," & vbCrLf
   vResult = vResult & "      @TipoDeTransaccion char(1) = '0'," & vbCrLf
   vResult = vResult & "      @NumeroDeFacturaAfectada " & gUtilSQL.getVarCharTypeForDB(11) & " = ''," & vbCrLf
   vResult = vResult & "      @NumeroControl " & gUtilSQL.getVarCharTypeForDB(20) & " = ''," & vbCrLf
   vResult = vResult & "      @SeHizoLaRetencionIVA char(1) = 'N'," & vbCrLf
   vResult = vResult & "      @NumeroComprobanteRetencion " & gUtilSQL.getVarCharTypeForDB(8) & " = ''," & vbCrLf
   vResult = vResult & "      @FechaAplicacionRetIVA " & gUtilSQL.getDateTypeForDB() & " = ''," & vbCrLf
   vResult = vResult & "      @PorcentajeRetencionAplicado money = 0," & vbCrLf
   vResult = vResult & "      @MontoRetenido money = 0," & vbCrLf
   vResult = vResult & "      @OrigenDeLaRetencion char(1) = '0'," & vbCrLf
   vResult = vResult & "      @RetencionAplicadaEnPago char(1) = 'N'," & vbCrLf
   vResult = vResult & "      @OrigenInformacionRetencion char(1) = '0'," & vbCrLf
   vResult = vResult & "      @CxPgeneradaPor char(1) = '0'," & vbCrLf
   vResult = vResult & "      @EsCxPhistorica char(1) = 'N'," & vbCrLf
   vResult = vResult & "      @NumDiasDeVencimiento " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "      @NumeroDocOrigen " & gUtilSQL.getVarCharTypeForDB(15) & " = ''," & vbCrLf
   vResult = vResult & "      @CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & " = ''," & vbCrLf
   vResult = vResult & "      @GenerarAsientoDeRetiroEnCuenta char(1) = 'N'," & vbCrLf
   vResult = vResult & "      @TotalOtrosImpuestos money = 0," & vbCrLf
   vResult = vResult & "      @SeContabilRetIva char(1) = 'N'," & vbCrLf
   vResult = vResult & "      @DondeContabilRetIva char(1) = '0'," & vbCrLf
   vResult = vResult & "      @CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & "," & vbCrLf
   vResult = vResult & "      @EstaAsociadoARendicion char(1) = 'N'," & vbCrLf
   vResult = vResult & "      @ConsecutivoRendicion" & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "      @CodigoTipoDeDocumentoLey varchar(10) = ''," & vbCrLf
   vResult = vResult & "      @AplicaDetraccion char(1) = 'N'," & vbCrLf
   vResult = vResult & "      @NumeroDetraccion varchar(15) = ''," & vbCrLf
   vResult = vResult & "      @CodigoDetraccion varchar(10) = ''," & vbCrLf
   vResult = vResult & "      @DescripcionDeDetraccion varchar(100) = ''," & vbCrLf
   vResult = vResult & "      @PorcentajeDetraccion decimal(25,5) = 0," & vbCrLf
   vResult = vResult & "      @TotalDetraccion decimal(25,5) = 0," & vbCrLf
   vResult = vResult & "      @StatusDetraccion char(1) = '0'," & vbCrLf
   vResult = vResult & "      @ConsecutivoMovimiento int = 0," & vbCrLf
   vResult = vResult & "   @NumeroDeclaracionAduana varchar(20) = ''," & vbCrLf
   vResult = vResult & "   @FechaDeclaracionAduana smalldatetime = ''," & vbCrLf
   vResult = vResult & "   @UsaPrefijoSerie char(1) = 'N'," & vbCrLf
   vResult = vResult & "   @EsUnaCuentaATerceros char(1) = 'N'," & vbCrLf
   vResult = vResult & "   @CodigoProveedorOriginalServicio varchar(10) = ''," & vbCrLf
   vResult = vResult & "   @AplicaIvaAlicuotaEspecial char(1) = 'N'," & vbCrLf
   vResult = vResult & "   @MontoGravableAlicuotaEspecial1 decimal(25,5) = 0," & vbCrLf
   vResult = vResult & "   @MontoIVAAlicuotaEspecial1 decimal(25,5) = 0," & vbCrLf
   vResult = vResult & "   @PorcentajeIvaAlicuotaEspecial1 decimal(25,5) = 0," & vbCrLf
   vResult = vResult & "   @MontoGravableAlicuotaEspecial2 decimal(25,5) = 0," & vbCrLf
   vResult = vResult & "   @MontoIVAAlicuotaEspecial2 decimal(25,5) = 0," & vbCrLf
   vResult = vResult & "   @PorcentajeIvaAlicuotaEspecial2 decimal(25,5) = 0," & vbCrLf
   vResult = vResult & "      @NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   fSpParametersINS = vResult
End Function
Private Function fStatementsSpINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   SET @FechaUltimaModificacion = " & gUtilSQL.DfCDateSQL(gUtilSQL.GetSQLFechaActual()) & "" & vbCrLf
   vResult = vResult & "   INSERT INTO CxP(" & vbCrLf
   vResult = vResult & "         ConsecutivoCompania," & vbCrLf
   vResult = vResult & "         ConsecutivoCxP," & vbCrLf
   vResult = vResult & "         Numero," & vbCrLf
   vResult = vResult & "         TipoDeCxP," & vbCrLf
   vResult = vResult & "         Status," & vbCrLf
   vResult = vResult & "         CodigoProveedor," & vbCrLf
   vResult = vResult & "         Fecha," & vbCrLf
   vResult = vResult & "         FechaCancelacion," & vbCrLf
   vResult = vResult & "         FechaVencimiento," & vbCrLf
   vResult = vResult & "         FechaAnulacion," & vbCrLf
   vResult = vResult & "         Moneda," & vbCrLf
   vResult = vResult & "         CambioABolivares," & vbCrLf
   vResult = vResult & "         AplicaParaLibrodeCompras," & vbCrLf
   vResult = vResult & "         MontoExento," & vbCrLf
   vResult = vResult & "         MontoGravado," & vbCrLf
   vResult = vResult & "         MontoIva," & vbCrLf
   vResult = vResult & "         MontoAbonado," & vbCrLf
   vResult = vResult & "         MesDeAplicacion," & vbCrLf
   vResult = vResult & "         AnoDeAplicacion," & vbCrLf
   vResult = vResult & "         Observaciones," & vbCrLf
   vResult = vResult & "         CreditoFiscal," & vbCrLf
   vResult = vResult & "         TipoDeCompra," & vbCrLf
   vResult = vResult & "         SeHizoLaRetencion," & vbCrLf
   vResult = vResult & "         MontoGravableAlicuotaGeneral," & vbCrLf
   vResult = vResult & "         MontoGravableAlicuota2," & vbCrLf
   vResult = vResult & "         MontoGravableAlicuota3," & vbCrLf
   vResult = vResult & "         MontoIVAAlicuotaGeneral," & vbCrLf
   vResult = vResult & "         MontoIVAAlicuota2," & vbCrLf
   vResult = vResult & "         MontoIVAAlicuota3," & vbCrLf
   vResult = vResult & "         NumeroPlanillaDeImportacion," & vbCrLf
   vResult = vResult & "         NumeroExpedienteDeImportacion," & vbCrLf
   vResult = vResult & "         TipoDeTransaccion," & vbCrLf
   vResult = vResult & "         NumeroDeFacturaAfectada," & vbCrLf
   vResult = vResult & "         NumeroControl," & vbCrLf
   vResult = vResult & "         SeHizoLaRetencionIVA," & vbCrLf
   vResult = vResult & "         NumeroComprobanteRetencion," & vbCrLf
   vResult = vResult & "         FechaAplicacionRetIVA," & vbCrLf
   vResult = vResult & "         PorcentajeRetencionAplicado," & vbCrLf
   vResult = vResult & "         MontoRetenido," & vbCrLf
   vResult = vResult & "         OrigenDeLaRetencion," & vbCrLf
   vResult = vResult & "         RetencionAplicadaEnPago," & vbCrLf
   vResult = vResult & "         OrigenInformacionRetencion," & vbCrLf
   vResult = vResult & "         CxPgeneradaPor," & vbCrLf
   vResult = vResult & "         EsCxPhistorica," & vbCrLf
   vResult = vResult & "         NumDiasDeVencimiento," & vbCrLf
   vResult = vResult & "         NumeroDocOrigen," & vbCrLf
   vResult = vResult & "         CodigoLote," & vbCrLf
   vResult = vResult & "         GenerarAsientoDeRetiroEnCuenta," & vbCrLf
   vResult = vResult & "         TotalOtrosImpuestos," & vbCrLf
   vResult = vResult & "         SeContabilRetIva," & vbCrLf
   vResult = vResult & "         DondeContabilRetIva," & vbCrLf
   vResult = vResult & "         CodigoMoneda," & vbCrLf
   vResult = vResult & "         EstaAsociadoARendicion," & vbCrLf
   vResult = vResult & "         ConsecutivoRendicion," & vbCrLf
   vResult = vResult & "         CodigoTipoDeDocumentoLey," & vbCrLf
   vResult = vResult & "         AplicaDetraccion," & vbCrLf
   vResult = vResult & "         NumeroDetraccion," & vbCrLf
   vResult = vResult & "         CodigoDetraccion," & vbCrLf
   vResult = vResult & "         DescripcionDeDetraccion," & vbCrLf
   vResult = vResult & "         PorcentajeDetraccion," & vbCrLf
   vResult = vResult & "         TotalDetraccion," & vbCrLf
   vResult = vResult & "         StatusDetraccion," & vbCrLf
   vResult = vResult & "         ConsecutivoMovimiento," & vbCrLf
   vResult = vResult & "         NumeroDeclaracionAduana," & vbCrLf
   vResult = vResult & "         FechaDeclaracionAduana," & vbCrLf
   vResult = vResult & "         UsaPrefijoSerie," & vbCrLf
   vResult = vResult & "         EsUnaCuentaATerceros," & vbCrLf
   vResult = vResult & "         CodigoProveedorOriginalServicio," & vbCrLf
   vResult = vResult & "         AplicaIvaAlicuotaEspecial," & vbCrLf
   vResult = vResult & "         MontoGravableAlicuotaEspecial1," & vbCrLf
   vResult = vResult & "         MontoIVAAlicuotaEspecial1," & vbCrLf
   vResult = vResult & "         PorcentajeIvaAlicuotaEspecial1," & vbCrLf
   vResult = vResult & "         MontoGravableAlicuotaEspecial2," & vbCrLf
   vResult = vResult & "         MontoIVAAlicuotaEspecial2," & vbCrLf
   vResult = vResult & "         PorcentajeIvaAlicuotaEspecial2," & vbCrLf
   vResult = vResult & "         NombreOperador," & vbCrLf
   vResult = vResult & "         FechaUltimaModificacion)" & vbCrLf
   vResult = vResult & "      VALUES(" & vbCrLf
   vResult = vResult & "         @ConsecutivoCompania," & vbCrLf
   vResult = vResult & "         @ConsecutivoCxP," & vbCrLf
   vResult = vResult & "         @Numero," & vbCrLf
   vResult = vResult & "         @TipoDeCxP," & vbCrLf
   vResult = vResult & "         @Status," & vbCrLf
   vResult = vResult & "         @CodigoProveedor," & vbCrLf
   vResult = vResult & "         @Fecha," & vbCrLf
   vResult = vResult & "         @FechaCancelacion," & vbCrLf
   vResult = vResult & "         @FechaVencimiento," & vbCrLf
   vResult = vResult & "         @FechaAnulacion," & vbCrLf
   vResult = vResult & "         @Moneda," & vbCrLf
   vResult = vResult & "         @CambioABolivares," & vbCrLf
   vResult = vResult & "         @AplicaParaLibrodeCompras," & vbCrLf
   vResult = vResult & "         @MontoExento," & vbCrLf
   vResult = vResult & "         @MontoGravado," & vbCrLf
   vResult = vResult & "         @MontoIva," & vbCrLf
   vResult = vResult & "         @MontoAbonado," & vbCrLf
   vResult = vResult & "         @MesDeAplicacion," & vbCrLf
   vResult = vResult & "         @AnoDeAplicacion," & vbCrLf
   vResult = vResult & "         @Observaciones," & vbCrLf
   vResult = vResult & "         @CreditoFiscal," & vbCrLf
   vResult = vResult & "         @TipoDeCompra," & vbCrLf
   vResult = vResult & "         @SeHizoLaRetencion," & vbCrLf
   vResult = vResult & "         @MontoGravableAlicuotaGeneral," & vbCrLf
   vResult = vResult & "         @MontoGravableAlicuota2," & vbCrLf
   vResult = vResult & "         @MontoGravableAlicuota3," & vbCrLf
   vResult = vResult & "         @MontoIVAAlicuotaGeneral," & vbCrLf
   vResult = vResult & "         @MontoIVAAlicuota2," & vbCrLf
   vResult = vResult & "         @MontoIVAAlicuota3," & vbCrLf
   vResult = vResult & "         @NumeroPlanillaDeImportacion," & vbCrLf
   vResult = vResult & "         @NumeroExpedienteDeImportacion," & vbCrLf
   vResult = vResult & "         @TipoDeTransaccion," & vbCrLf
   vResult = vResult & "         @NumeroDeFacturaAfectada," & vbCrLf
   vResult = vResult & "         @NumeroControl," & vbCrLf
   vResult = vResult & "         @SeHizoLaRetencionIVA," & vbCrLf
   vResult = vResult & "         @NumeroComprobanteRetencion," & vbCrLf
   vResult = vResult & "         @FechaAplicacionRetIVA," & vbCrLf
   vResult = vResult & "         @PorcentajeRetencionAplicado," & vbCrLf
   vResult = vResult & "         @MontoRetenido," & vbCrLf
   vResult = vResult & "         @OrigenDeLaRetencion," & vbCrLf
   vResult = vResult & "         @RetencionAplicadaEnPago," & vbCrLf
   vResult = vResult & "         @OrigenInformacionRetencion," & vbCrLf
   vResult = vResult & "         @CxPgeneradaPor," & vbCrLf
   vResult = vResult & "         @EsCxPhistorica," & vbCrLf
   vResult = vResult & "         @NumDiasDeVencimiento," & vbCrLf
   vResult = vResult & "         @NumeroDocOrigen," & vbCrLf
   vResult = vResult & "         @CodigoLote," & vbCrLf
   vResult = vResult & "         @GenerarAsientoDeRetiroEnCuenta," & vbCrLf
   vResult = vResult & "         @TotalOtrosImpuestos," & vbCrLf
   vResult = vResult & "         @SeContabilRetIva," & vbCrLf
   vResult = vResult & "         @DondeContabilRetIva," & vbCrLf
   vResult = vResult & "         @CodigoMoneda," & vbCrLf
   vResult = vResult & "         @EstaAsociadoARendicion," & vbCrLf
   vResult = vResult & "         @ConsecutivoRendicion," & vbCrLf
   vResult = vResult & "         @CodigoTipoDeDocumentoLey," & vbCrLf
   vResult = vResult & "         @AplicaDetraccion," & vbCrLf
   vResult = vResult & "         @NumeroDetraccion," & vbCrLf
   vResult = vResult & "         @CodigoDetraccion," & vbCrLf
   vResult = vResult & "         @DescripcionDeDetraccion," & vbCrLf
   vResult = vResult & "         @PorcentajeDetraccion," & vbCrLf
   vResult = vResult & "         @TotalDetraccion," & vbCrLf
   vResult = vResult & "         @StatusDetraccion," & vbCrLf
   vResult = vResult & "         @ConsecutivoMovimiento," & vbCrLf
   vResult = vResult & "         @NumeroDeclaracionAduana," & vbCrLf
   vResult = vResult & "         @FechaDeclaracionAduana," & vbCrLf
   vResult = vResult & "         @UsaPrefijoSerie," & vbCrLf
   vResult = vResult & "         @EsUnaCuentaATerceros," & vbCrLf
   vResult = vResult & "         @CodigoProveedorOriginalServicio," & vbCrLf
   vResult = vResult & "         @AplicaIvaAlicuotaEspecial," & vbCrLf
   vResult = vResult & "         @MontoGravableAlicuotaEspecial1," & vbCrLf
   vResult = vResult & "         @MontoIVAAlicuotaEspecial1," & vbCrLf
   vResult = vResult & "         @PorcentajeIvaAlicuotaEspecial1," & vbCrLf
   vResult = vResult & "         @MontoGravableAlicuotaEspecial2," & vbCrLf
   vResult = vResult & "         @MontoIVAAlicuotaEspecial2," & vbCrLf
   vResult = vResult & "         @PorcentajeIvaAlicuotaEspecial2," & vbCrLf
   vResult = vResult & "         @NombreOperador," & vbCrLf
   vResult = vResult & "         @FechaUltimaModificacion)" & vbCrLf
   vResult = vResult & "   RETURN @@ROWCOUNT" & vbCrLf
   vResult = vResult & "END "
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
   GetSpNameUPD = "CxPUPD"
End Function

Private Function fSpParametersUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @ConsecutivoCxP " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @Numero " & gUtilSQL.getVarCharTypeForDB(25) & " = NULL," & vbCrLf
   vResult = vResult & "         @TipoDeCxP char(1) = NULL," & vbCrLf
   vResult = vResult & "         @Status char(1) = NULL," & vbCrLf
   vResult = vResult & "         @CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & " = NULL," & vbCrLf
   vResult = vResult & "         @Fecha " & gUtilSQL.getDateTypeForDB() & " = NULL," & vbCrLf
   vResult = vResult & "         @FechaCancelacion " & gUtilSQL.getDateTypeForDB() & " = NULL," & vbCrLf
   vResult = vResult & "         @FechaVencimiento " & gUtilSQL.getDateTypeForDB() & " = NULL," & vbCrLf
   vResult = vResult & "         @FechaAnulacion " & gUtilSQL.getDateTypeForDB() & " = NULL," & vbCrLf
   vResult = vResult & "         @Moneda " & gUtilSQL.getVarCharTypeForDB(10) & " = NULL," & vbCrLf
   vResult = vResult & "         @CambioABolivares money = NULL," & vbCrLf
   vResult = vResult & "         @AplicaParaLibrodeCompras char(1) = NULL," & vbCrLf
   vResult = vResult & "         @MontoExento money = NULL," & vbCrLf
   vResult = vResult & "         @MontoGravado money = NULL," & vbCrLf
   vResult = vResult & "         @MontoIva money = NULL," & vbCrLf
   vResult = vResult & "         @MontoAbonado money = NULL," & vbCrLf
   vResult = vResult & "         @MesDeAplicacion " & gUtilSQL.getNumericTypeForDB(9, 0) & " = NULL," & vbCrLf
   vResult = vResult & "         @AnoDeAplicacion " & gUtilSQL.getNumericTypeForDB(9, 0) & " = NULL," & vbCrLf
   vResult = vResult & "         @Observaciones " & gUtilSQL.getVarCharTypeForDB(255) & " = NULL," & vbCrLf
   vResult = vResult & "         @CreditoFiscal char(1) = NULL," & vbCrLf
   vResult = vResult & "         @TipoDeCompra char(1) = NULL," & vbCrLf
   vResult = vResult & "         @SeHizoLaRetencion char(1) = NULL," & vbCrLf
   vResult = vResult & "         @MontoGravableAlicuotaGeneral money = NULL," & vbCrLf
   vResult = vResult & "         @MontoGravableAlicuota2 money = NULL," & vbCrLf
   vResult = vResult & "         @MontoGravableAlicuota3 money = NULL," & vbCrLf
   vResult = vResult & "         @MontoIVAAlicuotaGeneral money = NULL," & vbCrLf
   vResult = vResult & "         @MontoIVAAlicuota2 money = NULL," & vbCrLf
   vResult = vResult & "         @MontoIVAAlicuota3 money = NULL," & vbCrLf
   vResult = vResult & "         @NumeroPlanillaDeImportacion " & gUtilSQL.getVarCharTypeForDB(20) & " = NULL," & vbCrLf
   vResult = vResult & "         @NumeroExpedienteDeImportacion " & gUtilSQL.getVarCharTypeForDB(20) & " = NULL," & vbCrLf
   vResult = vResult & "         @TipoDeTransaccion char(1) = NULL," & vbCrLf
   vResult = vResult & "         @NumeroDeFacturaAfectada " & gUtilSQL.getVarCharTypeForDB(11) & " = NULL," & vbCrLf
   vResult = vResult & "         @NumeroControl " & gUtilSQL.getVarCharTypeForDB(20) & " = NULL," & vbCrLf
   vResult = vResult & "         @SeHizoLaRetencionIVA char(1) = NULL," & vbCrLf
   vResult = vResult & "         @NumeroComprobanteRetencion " & gUtilSQL.getVarCharTypeForDB(8) & " = NULL," & vbCrLf
   vResult = vResult & "         @FechaAplicacionRetIVA " & gUtilSQL.getDateTypeForDB() & " = NULL," & vbCrLf
   vResult = vResult & "         @PorcentajeRetencionAplicado money = NULL," & vbCrLf
   vResult = vResult & "         @MontoRetenido money = NULL," & vbCrLf
   vResult = vResult & "         @OrigenDeLaRetencion char(1) = NULL," & vbCrLf
   vResult = vResult & "         @RetencionAplicadaEnPago char(1) = NULL," & vbCrLf
   vResult = vResult & "         @OrigenInformacionRetencion char(1) = NULL," & vbCrLf
   vResult = vResult & "         @CxPgeneradaPor char(1) = NULL," & vbCrLf
   vResult = vResult & "         @EsCxPhistorica char(1) = NULL," & vbCrLf
   vResult = vResult & "         @NumDiasDeVencimiento " & gUtilSQL.getNumericTypeForDB(9, 0) & " = NULL," & vbCrLf
   vResult = vResult & "         @NumeroDocOrigen " & gUtilSQL.getVarCharTypeForDB(15) & " = NULL," & vbCrLf
   vResult = vResult & "         @CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & " = NULL," & vbCrLf
   vResult = vResult & "         @GenerarAsientoDeRetiroEnCuenta char(1) = NULL," & vbCrLf
   vResult = vResult & "         @TotalOtrosImpuestos " & gUtilSQL.getNumericTypeForDB(12, 2) & " = NULL," & vbCrLf
   vResult = vResult & "         @SeContabilRetIva char(1) = NULL," & vbCrLf
   vResult = vResult & "         @DondeContabilRetIva char(1) = NULL," & vbCrLf
   vResult = vResult & "         @CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & " = NULL," & vbCrLf
   vResult = vResult & "         @EstaAsociadoARendicion char(1) = NULL," & vbCrLf
   vResult = vResult & "         @ConsecutivoRendicion " & gUtilSQL.getNumericTypeForDB(9, 0) & " = NULL," & vbCrLf
   vResult = vResult & "   @CodigoTipoDeDocumentoLey varchar(10) = NULL," & vbCrLf
   vResult = vResult & "   @AplicaDetraccion char(1) = NULL," & vbCrLf
   vResult = vResult & "   @NumeroDetraccion varchar(15) = NULL," & vbCrLf
   vResult = vResult & "   @CodigoDetraccion varchar(10) = NULL," & vbCrLf
   vResult = vResult & "   @DescripcionDeDetraccion varchar(100) = NULL," & vbCrLf
   vResult = vResult & "   @PorcentajeDetraccion decimal(25,5) = NULL," & vbCrLf
   vResult = vResult & "   @TotalDetraccion decimal(25,5) = NULL," & vbCrLf
   vResult = vResult & "   @StatusDetraccion char(1) = NULL," & vbCrLf
   vResult = vResult & "   @ConsecutivoMovimiento int = NULL," & vbCrLf
   vResult = vResult & "   @NumeroDeclaracionAduana varchar(20) = NULL," & vbCrLf
   vResult = vResult & "   @FechaDeclaracionAduana smalldatetime = NULL," & vbCrLf
   vResult = vResult & "   @UsaPrefijoSerie char(1) = NULL," & vbCrLf
   vResult = vResult & "   @EsUnaCuentaATerceros char(1) = NULL," & vbCrLf
   vResult = vResult & "   @CodigoProveedorOriginalServicio varchar(10) = NULL," & vbCrLf
   vResult = vResult & "   @AplicaIvaAlicuotaEspecial char(1) = NULL," & vbCrLf
   vResult = vResult & "   @MontoGravableAlicuotaEspecial1 decimal(25,5) = NULL," & vbCrLf
   vResult = vResult & "   @MontoIVAAlicuotaEspecial1 decimal(25,5) = NULL," & vbCrLf
   vResult = vResult & "   @PorcentajeIvaAlicuotaEspecial1 decimal(25,5) = NULL," & vbCrLf
   vResult = vResult & "   @MontoGravableAlicuotaEspecial2 decimal(25,5) = NULL," & vbCrLf
   vResult = vResult & "   @MontoIVAAlicuotaEspecial2 decimal(25,5) = NULL," & vbCrLf
   vResult = vResult & "   @PorcentajeIvaAlicuotaEspecial2 decimal(25,5) = NULL," & vbCrLf
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
   vResult = vResult & "   DECLARE @Ori_Numero " & gUtilSQL.getVarCharTypeForDB(25) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TipoDeCxP char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Status char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Fecha " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_FechaCancelacion " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_FechaVencimiento " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_FechaAnulacion " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Moneda " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CambioABolivares money" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_AplicaParaLibrodeCompras char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MontoExento money" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MontoGravado money" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MontoIva money" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MontoAbonado money" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MesDeAplicacion " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_AnoDeAplicacion " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Observaciones " & gUtilSQL.getVarCharTypeForDB(255) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CreditoFiscal char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TipoDeCompra char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_SeHizoLaRetencion char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MontoGravableAlicuotaGeneral money" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MontoGravableAlicuota2 money" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MontoGravableAlicuota3 money" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MontoIVAAlicuotaGeneral money" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MontoIVAAlicuota2 money" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MontoIVAAlicuota3 money" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroPlanillaDeImportacion " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroExpedienteDeImportacion " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TipoDeTransaccion char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroDeFacturaAfectada " & gUtilSQL.getVarCharTypeForDB(11) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroControl " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_SeHizoLaRetencionIVA char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroComprobanteRetencion " & gUtilSQL.getVarCharTypeForDB(8) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_FechaAplicacionRetIVA " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_PorcentajeRetencionAplicado money" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MontoRetenido money" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_OrigenDeLaRetencion char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_RetencionAplicadaEnPago char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_OrigenInformacionRetencion char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CxPgeneradaPor char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_EsCxPhistorica char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumDiasDeVencimiento " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroDocOrigen " & gUtilSQL.getVarCharTypeForDB(15) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_GenerarAsientoDeRetiroEnCuenta char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TotalOtrosImpuestos money" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_SeContabilRetIva char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_DondeContabilRetIva char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_EstaAsociadoARendicion  char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_ConsecutivoRendicion  " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoTipoDeDocumentoLey " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_AplicaDetraccion " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroDetraccion " & gUtilSQL.getVarCharTypeForDB(15) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoDetraccion " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_DescripcionDeDetraccion " & gUtilSQL.getVarCharTypeForDB(100) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_PorcentajeDetraccion " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TotalDetraccion " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_StatusDetraccion " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_ConsecutivoMovimiento " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroDeclaracionAduana " & gUtilSQL.getVarCharTypeForDB(20) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_FechaDeclaracionAduana " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Ori_UsaPrefijoSerie " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_EsUnaCuentaATerceros " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoProveedorOriginalServicio " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TimeStamp timestamp" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Numero " & gUtilSQL.getVarCharTypeForDB(25) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_TipoDeCxP char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Status char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Fecha " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_FechaCancelacion " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_FechaVencimiento " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_FechaAnulacion " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Moneda " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CambioABolivares money" & vbCrLf
   vResult = vResult & "   DECLARE @Up_AplicaParaLibrodeCompras char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoExento money" & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoGravado money" & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoIva money" & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoAbonado money" & vbCrLf
   vResult = vResult & "   DECLARE @Up_MesDeAplicacion " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_AnoDeAplicacion " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Observaciones " & gUtilSQL.getVarCharTypeForDB(255) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CreditoFiscal char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_TipoDeCompra char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_SeHizoLaRetencion char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoGravableAlicuotaGeneral money" & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoGravableAlicuota2 money" & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoGravableAlicuota3 money" & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoIVAAlicuotaGeneral money" & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoIVAAlicuota2 money" & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoIVAAlicuota3 money" & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroPlanillaDeImportacion " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroExpedienteDeImportacion " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_TipoDeTransaccion char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroDeFacturaAfectada " & gUtilSQL.getVarCharTypeForDB(11) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroControl " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_SeHizoLaRetencionIVA char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroComprobanteRetencion " & gUtilSQL.getVarCharTypeForDB(8) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_FechaAplicacionRetIVA " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_PorcentajeRetencionAplicado money" & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoRetenido money" & vbCrLf
   vResult = vResult & "   DECLARE @Up_OrigenDeLaRetencion char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_RetencionAplicadaEnPago char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_OrigenInformacionRetencion char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CxPgeneradaPor char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_EsCxPhistorica char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumDiasDeVencimiento " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroDocOrigen " & gUtilSQL.getVarCharTypeForDB(15) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_GenerarAsientoDeRetiroEnCuenta char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_TotalOtrosImpuestos money" & vbCrLf
   vResult = vResult & "   DECLARE @Up_SeContabilRetIva char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_DondeContabilRetIva char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_EstaAsociadoARendicion char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_ConsecutivoRendicion " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoTipoDeDocumentoLey " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Up_AplicaDetraccion " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroDetraccion " & gUtilSQL.getVarCharTypeForDB(15) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoDetraccion " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Up_DescripcionDeDetraccion " & gUtilSQL.getVarCharTypeForDB(100) & vbCrLf
   vResult = vResult & "   DECLARE @Up_PorcentajeDetraccion " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_TotalDetraccion " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_StatusDetraccion " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_ConsecutivoMovimiento " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroDeclaracionAduana " & gUtilSQL.getVarCharTypeForDB(20) & vbCrLf
   vResult = vResult & "   DECLARE @Up_FechaDeclaracionAduana " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Up_UsaPrefijoSerie " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_EsUnaCuentaATerceros " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoProveedorOriginalServicio " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Up_AplicaIvaAlicuotaEspecial " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoGravableAlicuotaEspecial1 " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoIVAAlicuotaEspecial1 " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_PorcentajeIvaAlicuotaEspecial1 " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoGravableAlicuotaEspecial2 " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoIVAAlicuotaEspecial2 " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_PorcentajeIvaAlicuotaEspecial2 " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   BEGIN TRAN" & vbCrLf
   vResult = vResult & "   SELECT @Ori_Numero = Numero," & vbCrLf
   vResult = vResult & "           @Ori_TipoDeCxP = TipoDeCxP," & vbCrLf
   vResult = vResult & "           @Ori_Status = Status," & vbCrLf
   vResult = vResult & "           @Ori_CodigoProveedor = CodigoProveedor," & vbCrLf
   vResult = vResult & "           @Ori_Fecha = Fecha," & vbCrLf
   vResult = vResult & "           @Ori_FechaCancelacion = FechaCancelacion," & vbCrLf
   vResult = vResult & "           @Ori_FechaVencimiento = FechaVencimiento," & vbCrLf
   vResult = vResult & "           @Ori_FechaAnulacion = FechaAnulacion," & vbCrLf
   vResult = vResult & "           @Ori_Moneda = Moneda," & vbCrLf
   vResult = vResult & "           @Ori_CambioABolivares = CambioABolivares," & vbCrLf
   vResult = vResult & "           @Ori_AplicaParaLibrodeCompras = AplicaParaLibrodeCompras," & vbCrLf
   vResult = vResult & "           @Ori_MontoExento = MontoExento," & vbCrLf
   vResult = vResult & "           @Ori_MontoGravado = MontoGravado," & vbCrLf
   vResult = vResult & "           @Ori_MontoIva = MontoIva," & vbCrLf
   vResult = vResult & "           @Ori_MontoAbonado = MontoAbonado," & vbCrLf
   vResult = vResult & "           @Ori_MesDeAplicacion = MesDeAplicacion," & vbCrLf
   vResult = vResult & "           @Ori_AnoDeAplicacion = AnoDeAplicacion," & vbCrLf
   vResult = vResult & "           @Ori_Observaciones = Observaciones," & vbCrLf
   vResult = vResult & "           @Ori_CreditoFiscal = CreditoFiscal," & vbCrLf
   vResult = vResult & "           @Ori_TipoDeCompra = TipoDeCompra," & vbCrLf
   vResult = vResult & "           @Ori_SeHizoLaRetencion = SeHizoLaRetencion," & vbCrLf
   vResult = vResult & "           @Ori_MontoGravableAlicuotaGeneral = MontoGravableAlicuotaGeneral," & vbCrLf
   vResult = vResult & "           @Ori_MontoGravableAlicuota2 = MontoGravableAlicuota2," & vbCrLf
   vResult = vResult & "           @Ori_MontoGravableAlicuota3 = MontoGravableAlicuota3," & vbCrLf
   vResult = vResult & "           @Ori_MontoIVAAlicuotaGeneral = MontoIVAAlicuotaGeneral," & vbCrLf
   vResult = vResult & "           @Ori_MontoIVAAlicuota2 = MontoIVAAlicuota2," & vbCrLf
   vResult = vResult & "           @Ori_MontoIVAAlicuota3 = MontoIVAAlicuota3," & vbCrLf
   vResult = vResult & "           @Ori_NumeroPlanillaDeImportacion = NumeroPlanillaDeImportacion," & vbCrLf
   vResult = vResult & "           @Ori_NumeroExpedienteDeImportacion = NumeroExpedienteDeImportacion," & vbCrLf
   vResult = vResult & "           @Ori_TipoDeTransaccion = TipoDeTransaccion," & vbCrLf
   vResult = vResult & "           @Ori_NumeroDeFacturaAfectada = NumeroDeFacturaAfectada," & vbCrLf
   vResult = vResult & "           @Ori_NumeroControl = NumeroControl," & vbCrLf
   vResult = vResult & "           @Ori_SeHizoLaRetencionIVA = SeHizoLaRetencionIVA," & vbCrLf
   vResult = vResult & "           @Ori_NumeroComprobanteRetencion = NumeroComprobanteRetencion," & vbCrLf
   vResult = vResult & "           @Ori_FechaAplicacionRetIVA = FechaAplicacionRetIVA," & vbCrLf
   vResult = vResult & "           @Ori_PorcentajeRetencionAplicado = PorcentajeRetencionAplicado," & vbCrLf
   vResult = vResult & "           @Ori_MontoRetenido = MontoRetenido," & vbCrLf
   vResult = vResult & "           @Ori_OrigenDeLaRetencion = OrigenDeLaRetencion," & vbCrLf
   vResult = vResult & "           @Ori_RetencionAplicadaEnPago = RetencionAplicadaEnPago," & vbCrLf
   vResult = vResult & "           @Ori_OrigenInformacionRetencion = OrigenInformacionRetencion," & vbCrLf
   vResult = vResult & "           @Ori_CxPgeneradaPor = CxPgeneradaPor," & vbCrLf
   vResult = vResult & "           @Ori_EsCxPhistorica = EsCxPhistorica," & vbCrLf
   vResult = vResult & "           @Ori_NumDiasDeVencimiento = NumDiasDeVencimiento," & vbCrLf
   vResult = vResult & "           @Ori_NumeroDocOrigen = NumeroDocOrigen," & vbCrLf
   vResult = vResult & "           @Ori_CodigoLote = CodigoLote," & vbCrLf
   vResult = vResult & "           @Ori_GenerarAsientoDeRetiroEnCuenta = GenerarAsientoDeRetiroEnCuenta," & vbCrLf
   vResult = vResult & "           @Ori_TotalOtrosImpuestos = TotalOtrosImpuestos," & vbCrLf
   vResult = vResult & "           @Ori_SeContabilRetIva = SeContabilRetIva," & vbCrLf
   vResult = vResult & "           @Ori_DondeContabilRetIva = DondeContabilRetIva," & vbCrLf
   vResult = vResult & "           @Ori_CodigoMoneda = CodigoMoneda," & vbCrLf
   vResult = vResult & "           @Ori_EstaAsociadoARendicion = EstaAsociadoARendicion," & vbCrLf
   vResult = vResult & "           @Ori_ConsecutivoRendicion = ConsecutivoRendicion," & vbCrLf
   vResult = vResult & "          @Ori_CodigoTipoDeDocumentoLey = CodigoTipoDeDocumentoLey," & vbCrLf
   vResult = vResult & "          @Ori_AplicaDetraccion = AplicaDetraccion," & vbCrLf
   vResult = vResult & "          @Ori_NumeroDetraccion = NumeroDetraccion," & vbCrLf
   vResult = vResult & "          @Ori_CodigoDetraccion = CodigoDetraccion," & vbCrLf
   vResult = vResult & "          @Ori_DescripcionDeDetraccion = DescripcionDeDetraccion," & vbCrLf
   vResult = vResult & "          @Ori_PorcentajeDetraccion = PorcentajeDetraccion," & vbCrLf
   vResult = vResult & "          @Ori_TotalDetraccion = TotalDetraccion," & vbCrLf
   vResult = vResult & "          @Ori_StatusDetraccion = StatusDetraccion," & vbCrLf
   vResult = vResult & "          @Ori_ConsecutivoMovimiento = ConsecutivoMovimiento," & vbCrLf
   vResult = vResult & "          @Ori_NumeroDeclaracionAduana = NumeroDeclaracionAduana," & vbCrLf
   vResult = vResult & "          @Ori_FechaDeclaracionAduana = FechaDeclaracionAduana," & vbCrLf
   vResult = vResult & "          @Ori_UsaPrefijoSerie = UsaPrefijoSerie," & vbCrLf
   vResult = vResult & "          @Ori_EsUnaCuentaATerceros = EsUnaCuentaATerceros," & vbCrLf
   vResult = vResult & "          @Ori_CodigoProveedorOriginalServicio = CodigoProveedorOriginalServicio," & vbCrLf
   vResult = vResult & "          @Ori_AplicaIvaAlicuotaEspecial = AplicaIvaAlicuotaEspecial," & vbCrLf
   vResult = vResult & "          @Ori_MontoGravableAlicuotaEspecial1 = MontoGravableAlicuotaEspecial1," & vbCrLf
   vResult = vResult & "          @Ori_MontoIVAAlicuotaEspecial1 = MontoIVAAlicuotaEspecial1," & vbCrLf
   vResult = vResult & "          @Ori_PorcentajeIvaAlicuotaEspecial1 = PorcentajeIvaAlicuotaEspecial1," & vbCrLf
   vResult = vResult & "          @Ori_MontoGravableAlicuotaEspecial2 = MontoGravableAlicuotaEspecial2," & vbCrLf
   vResult = vResult & "          @Ori_MontoIVAAlicuotaEspecial2 = MontoIVAAlicuotaEspecial2," & vbCrLf
   vResult = vResult & "          @Ori_PorcentajeIvaAlicuotaEspecial2 = PorcentajeIvaAlicuotaEspecial2," & vbCrLf
   vResult = vResult & "           @Ori_NombreOperador = NombreOperador," & vbCrLf
   vResult = vResult & "           @Ori_TimeStamp = fldTimeStamp" & vbCrLf
   vResult = vResult & "   FROM CxP" & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "     AND ConsecutivoCxP = @ConsecutivoCxP" & vbCrLf
   vResult = vResult & "   IF @Numero IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Numero = @Ori_Numero" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Numero = @Numero" & vbCrLf
   vResult = vResult & "   IF @TipoDeCxP IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_TipoDeCxP = @Ori_TipoDeCxP" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TipoDeCxP = @TipoDeCxP" & vbCrLf
   vResult = vResult & "   IF @Status IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Status = @Ori_Status" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Status = @Status" & vbCrLf
   vResult = vResult & "   IF @CodigoProveedor IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoProveedor = @Ori_CodigoProveedor" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoProveedor = @CodigoProveedor" & vbCrLf
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
   vResult = vResult & "   IF @Moneda IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Moneda = @Ori_Moneda" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Moneda = @Moneda" & vbCrLf
   vResult = vResult & "   IF @CambioABolivares IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_CambioABolivares = @Ori_CambioABolivares" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CambioABolivares = @CambioABolivares" & vbCrLf
   vResult = vResult & "   IF @AplicaParaLibrodeCompras IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_AplicaParaLibrodeCompras = @Ori_AplicaParaLibrodeCompras" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_AplicaParaLibrodeCompras = @AplicaParaLibrodeCompras" & vbCrLf
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
   vResult = vResult & "   IF @MesDeAplicacion IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_MesDeAplicacion = @Ori_MesDeAplicacion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_MesDeAplicacion = @MesDeAplicacion" & vbCrLf
   vResult = vResult & "   IF @AnoDeAplicacion IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_AnoDeAplicacion = @Ori_AnoDeAplicacion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_AnoDeAplicacion = @AnoDeAplicacion" & vbCrLf
   vResult = vResult & "   IF @Observaciones IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Observaciones = @Ori_Observaciones" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Observaciones = @Observaciones" & vbCrLf
   vResult = vResult & "   IF @CreditoFiscal IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_CreditoFiscal = @Ori_CreditoFiscal" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CreditoFiscal = @CreditoFiscal" & vbCrLf
   vResult = vResult & "   IF @TipoDeCompra IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_TipoDeCompra = @Ori_TipoDeCompra" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TipoDeCompra = @TipoDeCompra" & vbCrLf
   vResult = vResult & "   IF @SeHizoLaRetencion IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_SeHizoLaRetencion = @Ori_SeHizoLaRetencion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_SeHizoLaRetencion = @SeHizoLaRetencion" & vbCrLf
   vResult = vResult & "   IF @MontoGravableAlicuotaGeneral IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_MontoGravableAlicuotaGeneral = @Ori_MontoGravableAlicuotaGeneral" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_MontoGravableAlicuotaGeneral = @MontoGravableAlicuotaGeneral" & vbCrLf
   vResult = vResult & "   IF @MontoGravableAlicuota2 IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_MontoGravableAlicuota2 = @Ori_MontoGravableAlicuota2" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_MontoGravableAlicuota2 = @MontoGravableAlicuota2" & vbCrLf
   vResult = vResult & "   IF @MontoGravableAlicuota3 is null" & vbCrLf
   vResult = vResult & "      SET @Up_MontoGravableAlicuota3 = @Ori_MontoGravableAlicuota3" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_MontoGravableAlicuota3 = @MontoGravableAlicuota3" & vbCrLf
   vResult = vResult & "   IF @MontoIVAAlicuotaGeneral is null" & vbCrLf
   vResult = vResult & "      SET @Up_MontoIVAAlicuotaGeneral = @Ori_MontoIVAAlicuotaGeneral" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_MontoIVAAlicuotaGeneral = @MontoIVAAlicuotaGeneral" & vbCrLf
   vResult = vResult & "   IF @MontoIVAAlicuota2 is null" & vbCrLf
   vResult = vResult & "      SET @Up_MontoIVAAlicuota2 = @Ori_MontoIVAAlicuota2" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_MontoIVAAlicuota2 = @MontoIVAAlicuota2" & vbCrLf
   vResult = vResult & "   IF @MontoIVAAlicuota3 is null" & vbCrLf
   vResult = vResult & "      SET @Up_MontoIVAAlicuota3 = @Ori_MontoIVAAlicuota3" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_MontoIVAAlicuota3 = @MontoIVAAlicuota3" & vbCrLf
   vResult = vResult & "   IF @NumeroPlanillaDeImportacion is null" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroPlanillaDeImportacion = @Ori_NumeroPlanillaDeImportacion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroPlanillaDeImportacion = @NumeroPlanillaDeImportacion" & vbCrLf
   vResult = vResult & "   IF @NumeroExpedienteDeImportacion is null" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroExpedienteDeImportacion = @Ori_NumeroExpedienteDeImportacion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroExpedienteDeImportacion = @NumeroExpedienteDeImportacion" & vbCrLf
   vResult = vResult & "   IF @TipoDeTransaccion is null" & vbCrLf
   vResult = vResult & "      SET @Up_TipoDeTransaccion = @Ori_TipoDeTransaccion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TipoDeTransaccion = @TipoDeTransaccion" & vbCrLf
   vResult = vResult & "   IF @NumeroDeFacturaAfectada is null" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroDeFacturaAfectada = @Ori_NumeroDeFacturaAfectada" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroDeFacturaAfectada = @NumeroDeFacturaAfectada" & vbCrLf
   vResult = vResult & "   IF @NumeroControl is null" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroControl = @Ori_NumeroControl" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroControl = @NumeroControl" & vbCrLf
   vResult = vResult & "   IF @SeHizoLaRetencionIVA is null" & vbCrLf
   vResult = vResult & "      SET @Up_SeHizoLaRetencionIVA = @Ori_SeHizoLaRetencionIVA" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_SeHizoLaRetencionIVA = @SeHizoLaRetencionIVA" & vbCrLf
   vResult = vResult & "   IF @NumeroComprobanteRetencion is null" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroComprobanteRetencion = @Ori_NumeroComprobanteRetencion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroComprobanteRetencion = @NumeroComprobanteRetencion" & vbCrLf
   vResult = vResult & "   IF @FechaAplicacionRetIVA is null" & vbCrLf
   vResult = vResult & "      SET @Up_FechaAplicacionRetIVA = @Ori_FechaAplicacionRetIVA" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_FechaAplicacionRetIVA = @FechaAplicacionRetIVA" & vbCrLf
   vResult = vResult & "   IF @PorcentajeRetencionAplicado is null" & vbCrLf
   vResult = vResult & "      SET @Up_PorcentajeRetencionAplicado = @Ori_PorcentajeRetencionAplicado" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_PorcentajeRetencionAplicado = @PorcentajeRetencionAplicado" & vbCrLf
   vResult = vResult & "   IF @MontoRetenido is null" & vbCrLf
   vResult = vResult & "      SET @Up_MontoRetenido = @Ori_MontoRetenido" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_MontoRetenido = @MontoRetenido" & vbCrLf
   vResult = vResult & "   IF @OrigenDeLaRetencion is null" & vbCrLf
   vResult = vResult & "      SET @Up_OrigenDeLaRetencion = @Ori_OrigenDeLaRetencion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_OrigenDeLaRetencion = @OrigenDeLaRetencion" & vbCrLf
   vResult = vResult & "   IF @RetencionAplicadaEnPago is null" & vbCrLf
   vResult = vResult & "      SET @Up_RetencionAplicadaEnPago = @Ori_RetencionAplicadaEnPago" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_RetencionAplicadaEnPago = @RetencionAplicadaEnPago" & vbCrLf
   vResult = vResult & "   IF @OrigenInformacionRetencion is null" & vbCrLf
   vResult = vResult & "      SET @Up_OrigenInformacionRetencion = @Ori_OrigenInformacionRetencion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_OrigenInformacionRetencion = @OrigenInformacionRetencion" & vbCrLf
   vResult = vResult & "   IF @CxPgeneradaPor is null" & vbCrLf
   vResult = vResult & "      SET @Up_CxPgeneradaPor = @Ori_CxPgeneradaPor" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CxPgeneradaPor = @CxPgeneradaPor" & vbCrLf
   vResult = vResult & "   IF @EsCxPhistorica is null" & vbCrLf
   vResult = vResult & "      SET @Up_EsCxPhistorica = @Ori_EsCxPhistorica" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_EsCxPhistorica = @EsCxPhistorica" & vbCrLf
   vResult = vResult & "   IF @NumDiasDeVencimiento is null" & vbCrLf
   vResult = vResult & "      SET @Up_NumDiasDeVencimiento = @Ori_NumDiasDeVencimiento" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumDiasDeVencimiento = @NumDiasDeVencimiento" & vbCrLf
   vResult = vResult & "   IF @NumeroDocOrigen is null" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroDocOrigen = @Ori_NumeroDocOrigen" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroDocOrigen = @NumeroDocOrigen" & vbCrLf
   vResult = vResult & "   IF @CodigoLote is null" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoLote = @Ori_CodigoLote" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoLote = @CodigoLote" & vbCrLf
   vResult = vResult & "   IF @GenerarAsientoDeRetiroEnCuenta is null" & vbCrLf
   vResult = vResult & "      SET @Up_GenerarAsientoDeRetiroEnCuenta = @Ori_GenerarAsientoDeRetiroEnCuenta" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_GenerarAsientoDeRetiroEnCuenta = @GenerarAsientoDeRetiroEnCuenta" & vbCrLf
   vResult = vResult & "   IF @TotalOtrosImpuestos is null" & vbCrLf
   vResult = vResult & "      SET @Up_TotalOtrosImpuestos = @Ori_TotalOtrosImpuestos" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TotalOtrosImpuestos = @TotalOtrosImpuestos" & vbCrLf
   vResult = vResult & "   IF @SeContabilRetIva IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_SeContabilRetIva = @Ori_SeContabilRetIva" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_SeContabilRetIva = @SeContabilRetIva" & vbCrLf
   vResult = vResult & "   IF @SeContabilRetIva IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_SeContabilRetIva = @Ori_SeContabilRetIva" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_SeContabilRetIva = @SeContabilRetIva" & vbCrLf
   vResult = vResult & "   IF @DondeContabilRetIva IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_DondeContabilRetIva = @Ori_DondeContabilRetIva" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_DondeContabilRetIva = @DondeContabilRetIva" & vbCrLf
   vResult = vResult & "   IF @CodigoMoneda IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoMoneda = @Ori_CodigoMoneda" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoMoneda = @CodigoMoneda" & vbCrLf
   vResult = vResult & "   IF @EstaAsociadoARendicion IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_EstaAsociadoARendicion = @Ori_EstaAsociadoARendicion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_EstaAsociadoARendicion = @EstaAsociadoARendicion" & vbCrLf
   vResult = vResult & "   IF @ConsecutivoRendicion IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_ConsecutivoRendicion = @Ori_ConsecutivoRendicion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_ConsecutivoRendicion = @ConsecutivoRendicion" & vbCrLf
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
   vResult = vResult & "   IF @NumeroDeclaracionAduana is null" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroDeclaracionAduana = @Ori_NumeroDeclaracionAduana" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroDeclaracionAduana = @NumeroDeclaracionAduana" & vbCrLf
   vResult = vResult & "   IF @FechaDeclaracionAduana is null" & vbCrLf
   vResult = vResult & "      SET @Up_FechaDeclaracionAduana = @Ori_FechaDeclaracionAduana" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_FechaDeclaracionAduana = @FechaDeclaracionAduana" & vbCrLf
   vResult = vResult & "   IF @UsaPrefijoSerie is null" & vbCrLf
   vResult = vResult & "      SET @Up_UsaPrefijoSerie = @Ori_UsaPrefijoSerie" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_UsaPrefijoSerie = @UsaPrefijoSerie" & vbCrLf
   vResult = vResult & "   IF @EsUnaCuentaATerceros is null" & vbCrLf
   vResult = vResult & "      SET @Up_EsUnaCuentaATerceros = @Ori_EsUnaCuentaATerceros" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_EsUnaCuentaATerceros = @EsUnaCuentaATerceros" & vbCrLf
   vResult = vResult & "   IF @CodigoProveedorOriginalServicio is null" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoProveedorOriginalServicio = @Ori_CodigoProveedorOriginalServicio" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoProveedorOriginalServicio = @CodigoProveedorOriginalServicio" & vbCrLf
   vResult = vResult & "   IF @AplicaIvaAlicuotaEspecial is null" & vbCrLf
   vResult = vResult & "      SET @Up_AplicaIvaAlicuotaEspecial = @Ori_AplicaIvaAlicuotaEspecial" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_AplicaIvaAlicuotaEspecial = @AplicaIvaAlicuotaEspecial" & vbCrLf
   vResult = vResult & "   IF @MontoGravableAlicuotaEspecial1 is null" & vbCrLf
   vResult = vResult & "      SET @Up_MontoGravableAlicuotaEspecial1 = @Ori_MontoGravableAlicuotaEspecial1" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_MontoGravableAlicuotaEspecial1 = @MontoGravableAlicuotaEspecial1" & vbCrLf
   vResult = vResult & "   IF @MontoIVAAlicuotaEspecial1 is null" & vbCrLf
   vResult = vResult & "      SET @Up_MontoIVAAlicuotaEspecial1 = @Ori_MontoIVAAlicuotaEspecial1" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_MontoIVAAlicuotaEspecial1 = @MontoIVAAlicuotaEspecial1" & vbCrLf
   vResult = vResult & "   IF @PorcentajeIvaAlicuotaEspecial1 is null" & vbCrLf
   vResult = vResult & "      SET @Up_PorcentajeIvaAlicuotaEspecial1 = @Ori_PorcentajeIvaAlicuotaEspecial1" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_PorcentajeIvaAlicuotaEspecial1 = @PorcentajeIvaAlicuotaEspecial1" & vbCrLf
   vResult = vResult & "   IF @MontoGravableAlicuotaEspecial2 is null" & vbCrLf
   vResult = vResult & "      SET @Up_MontoGravableAlicuotaEspecial2 = @Ori_MontoGravableAlicuotaEspecial2" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_MontoGravableAlicuotaEspecial2 = @MontoGravableAlicuotaEspecial2" & vbCrLf
   vResult = vResult & "   IF @MontoIVAAlicuotaEspecial2 is null" & vbCrLf
   vResult = vResult & "      SET @Up_MontoIVAAlicuotaEspecial2 = @Ori_MontoIVAAlicuotaEspecial2" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_MontoIVAAlicuotaEspecial2 = @MontoIVAAlicuotaEspecial2" & vbCrLf
   vResult = vResult & "   IF @PorcentajeIvaAlicuotaEspecial2 is null" & vbCrLf
   vResult = vResult & "      SET @Up_PorcentajeIvaAlicuotaEspecial2 = @Ori_PorcentajeIvaAlicuotaEspecial2" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_PorcentajeIvaAlicuotaEspecial2 = @PorcentajeIvaAlicuotaEspecial2" & vbCrLf
   vResult = vResult & "   IF @NombreOperador is null" & vbCrLf
   vResult = vResult & "      SET @Up_NombreOperador = @Ori_NombreOperador" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NombreOperador = @NombreOperador" & vbCrLf
   vResult = vResult & "   UPDATE CxP" & vbCrLf
   vResult = vResult & "   SET Numero = @Up_Numero," & vbCrLf
   vResult = vResult & "        TipoDeCxP = @Up_TipoDeCxP," & vbCrLf
   vResult = vResult & "        Status = @Up_Status," & vbCrLf
   vResult = vResult & "        CodigoProveedor = @Up_CodigoProveedor," & vbCrLf
   vResult = vResult & "        Fecha = @Up_Fecha," & vbCrLf
   vResult = vResult & "        FechaCancelacion = @Up_FechaCancelacion," & vbCrLf
   vResult = vResult & "        FechaVencimiento = @Up_FechaVencimiento," & vbCrLf
   vResult = vResult & "        FechaAnulacion = @Up_FechaAnulacion," & vbCrLf
   vResult = vResult & "        Moneda = @Up_Moneda," & vbCrLf
   vResult = vResult & "        CambioABolivares = @Up_CambioABolivares," & vbCrLf
   vResult = vResult & "        AplicaParaLibrodeCompras = @Up_AplicaParaLibrodeCompras," & vbCrLf
   vResult = vResult & "        MontoExento = @Up_MontoExento," & vbCrLf
   vResult = vResult & "        MontoGravado = @Up_MontoGravado," & vbCrLf
   vResult = vResult & "        MontoIva = @Up_MontoIva," & vbCrLf
   vResult = vResult & "        MontoAbonado = @Up_MontoAbonado," & vbCrLf
   vResult = vResult & "        MesDeAplicacion = @Up_MesDeAplicacion," & vbCrLf
   vResult = vResult & "        AnoDeAplicacion = @Up_AnoDeAplicacion," & vbCrLf
   vResult = vResult & "        Observaciones = @Up_Observaciones," & vbCrLf
   vResult = vResult & "        CreditoFiscal = @Up_CreditoFiscal," & vbCrLf
   vResult = vResult & "        TipoDeCompra = @Up_TipoDeCompra," & vbCrLf
   vResult = vResult & "        SeHizoLaRetencion = @Up_SeHizoLaRetencion," & vbCrLf
   vResult = vResult & "        MontoGravableAlicuotaGeneral = @Up_MontoGravableAlicuotaGeneral," & vbCrLf
   vResult = vResult & "        MontoGravableAlicuota2 = @Up_MontoGravableAlicuota2," & vbCrLf
   vResult = vResult & "        MontoGravableAlicuota3 = @Up_MontoGravableAlicuota3," & vbCrLf
   vResult = vResult & "        MontoIVAAlicuotaGeneral = @Up_MontoIVAAlicuotaGeneral," & vbCrLf
   vResult = vResult & "        MontoIVAAlicuota2 = @Up_MontoIVAAlicuota2," & vbCrLf
   vResult = vResult & "        MontoIVAAlicuota3 = @Up_MontoIVAAlicuota3," & vbCrLf
   vResult = vResult & "        NumeroPlanillaDeImportacion = @Up_NumeroPlanillaDeImportacion," & vbCrLf
   vResult = vResult & "        NumeroExpedienteDeImportacion = @Up_NumeroExpedienteDeImportacion," & vbCrLf
   vResult = vResult & "        TipoDeTransaccion = @Up_TipoDeTransaccion," & vbCrLf
   vResult = vResult & "        NumeroDeFacturaAfectada = @Up_NumeroDeFacturaAfectada," & vbCrLf
   vResult = vResult & "        NumeroControl = @Up_NumeroControl," & vbCrLf
   vResult = vResult & "        SeHizoLaRetencionIVA = @Up_SeHizoLaRetencionIVA," & vbCrLf
   vResult = vResult & "        NumeroComprobanteRetencion = @Up_NumeroComprobanteRetencion," & vbCrLf
   vResult = vResult & "        FechaAplicacionRetIVA = @Up_FechaAplicacionRetIVA," & vbCrLf
   vResult = vResult & "        PorcentajeRetencionAplicado = @Up_PorcentajeRetencionAplicado," & vbCrLf
   vResult = vResult & "        MontoRetenido = @Up_MontoRetenido," & vbCrLf
   vResult = vResult & "        OrigenDeLaRetencion = @Up_OrigenDeLaRetencion," & vbCrLf
   vResult = vResult & "        RetencionAplicadaEnPago = @Up_RetencionAplicadaEnPago," & vbCrLf
   vResult = vResult & "        OrigenInformacionRetencion = @Up_OrigenInformacionRetencion," & vbCrLf
   vResult = vResult & "        CxPgeneradaPor = @Up_CxPgeneradaPor," & vbCrLf
   vResult = vResult & "        EsCxPhistorica = @Up_EsCxPhistorica," & vbCrLf
   vResult = vResult & "        NumDiasDeVencimiento = @Up_NumDiasDeVencimiento," & vbCrLf
   vResult = vResult & "        NumeroDocOrigen = @Up_NumeroDocOrigen," & vbCrLf
   vResult = vResult & "        CodigoLote = @Up_CodigoLote," & vbCrLf
   vResult = vResult & "        GenerarAsientoDeRetiroEnCuenta = @Up_GenerarAsientoDeRetiroEnCuenta," & vbCrLf
   vResult = vResult & "        TotalOtrosImpuestos = @Up_TotalOtrosImpuestos," & vbCrLf
   vResult = vResult & "        SeContabilRetIva = @Up_SeContabilRetIva," & vbCrLf
   vResult = vResult & "        DondeContabilRetIva = @Up_DondeContabilRetIva," & vbCrLf
   vResult = vResult & "        CodigoMoneda = @Up_CodigoMoneda," & vbCrLf
   vResult = vResult & "        EstaAsociadoARendicion = @Up_EstaAsociadoARendicion," & vbCrLf
   vResult = vResult & "        ConsecutivoRendicion = @Up_ConsecutivoRendicion," & vbCrLf
   vResult = vResult & "         CodigoTipoDeDocumentoLey = @Up_CodigoTipoDeDocumentoLey," & vbCrLf
   vResult = vResult & "         AplicaDetraccion = @Up_AplicaDetraccion," & vbCrLf
   vResult = vResult & "         NumeroDetraccion = @Up_NumeroDetraccion," & vbCrLf
   vResult = vResult & "         CodigoDetraccion = @Up_CodigoDetraccion," & vbCrLf
   vResult = vResult & "         DescripcionDeDetraccion = @Up_DescripcionDeDetraccion," & vbCrLf
   vResult = vResult & "         PorcentajeDetraccion = @Up_PorcentajeDetraccion," & vbCrLf
   vResult = vResult & "         TotalDetraccion = @Up_TotalDetraccion," & vbCrLf
   vResult = vResult & "         StatusDetraccion = @Up_StatusDetraccion," & vbCrLf
   vResult = vResult & "         ConsecutivoMovimiento = @Up_ConsecutivoMovimiento," & vbCrLf
   vResult = vResult & "         NumeroDeclaracionAduana = @Up_NumeroDeclaracionAduana," & vbCrLf
   vResult = vResult & "         FechaDeclaracionAduana = @Up_FechaDeclaracionAduana," & vbCrLf
   vResult = vResult & "         UsaPrefijoSerie = @Up_UsaPrefijoSerie," & vbCrLf
   vResult = vResult & "         EsUnaCuentaATerceros = @Up_EsUnaCuentaATerceros," & vbCrLf
   vResult = vResult & "         CodigoProveedorOriginalServicio = @Up_CodigoProveedorOriginalServicio," & vbCrLf
   vResult = vResult & "         AplicaIvaAlicuotaEspecial = @Up_AplicaIvaAlicuotaEspecial," & vbCrLf
   vResult = vResult & "         MontoGravableAlicuotaEspecial1 = @Up_MontoGravableAlicuotaEspecial1," & vbCrLf
   vResult = vResult & "         MontoIVAAlicuotaEspecial1 = @Up_MontoIVAAlicuotaEspecial1," & vbCrLf
   vResult = vResult & "         PorcentajeIvaAlicuotaEspecial1 = @Up_PorcentajeIvaAlicuotaEspecial1," & vbCrLf
   vResult = vResult & "         MontoGravableAlicuotaEspecial2 = @Up_MontoGravableAlicuotaEspecial2," & vbCrLf
   vResult = vResult & "         MontoIVAAlicuotaEspecial2 = @Up_MontoIVAAlicuotaEspecial2," & vbCrLf
   vResult = vResult & "         PorcentajeIvaAlicuotaEspecial2 = @Up_PorcentajeIvaAlicuotaEspecial2," & vbCrLf
   vResult = vResult & "        NombreOperador = @Up_NombreOperador," & vbCrLf
   vResult = vResult & "        FechaUltimaModificacion = CONVERT(" & gUtilSQL.getDateTypeForDB() & ", GETDATE(), 103)" & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "     AND ConsecutivoCxP = @ConsecutivoCxP" & vbCrLf
   vResult = vResult & "     AND fldTimeStamp = @Ori_TimeStamp" & vbCrLf
   vResult = vResult & "   IF @@ERROR = 0" & vbCrLf
   vResult = vResult & "      COMMIT TRAN" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      ROLLBACK" & vbCrLf
   vResult = vResult & "END " & vbCrLf
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
   GetSpNameDel = "CxPDEL"
End Function

Private Function fSpParametersDel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "   @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "   @ConsecutivoCxP " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
    fSpParametersDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpDel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "    BEGIN TRAN" & vbCrLf
   vResult = vResult & "    DELETE FROM CxP" & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "     AND ConsecutivoCxP = @ConsecutivoCxP" & vbCrLf
   vResult = vResult & "   IF @@ERROR = 0" & vbCrLf
   vResult = vResult & "      COMMIT TRAN" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      ROLLBACK" & vbCrLf
   vResult = vResult & "END "
   fStatementsSpDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpS10Insert(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10Insert(), fSpParametersS10Insert(), fStatementsSpS10Insert(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10Insert = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10Insert", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10Insert(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10Insert(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10Insert = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10Insert", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10Insert() As String
   GetSpNameS10Insert = "S10_CxPINS"
End Function

Private Function fSpParametersS10Insert() As String
   Dim vResult As String
   On Error GoTo h_ERROR

   vResult = "         @P_ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @P_Numero " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "         @P_CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "         @P_CodigoTipoDeDocumentoCxp " & gUtilSQL.getVarCharTypeForDB(10) & " = '01'," & vbCrLf
   vResult = vResult & "         @P_Fecha " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "         @P_FechaVencimiento " & gUtilSQL.getDateTypeForDB() & " = ''," & vbCrLf
   vResult = vResult & "         @P_CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & "," & vbCrLf
   vResult = vResult & "         @P_CambioAMonedaLocal money = 1," & vbCrLf
   vResult = vResult & "         @P_MontoExento money = 0," & vbCrLf
   vResult = vResult & "         @P_MontoGravableAlicuotaGeneral money = 0," & vbCrLf
   vResult = vResult & "         @P_MontoGravableAlicuota2 money = 0," & vbCrLf
   vResult = vResult & "         @P_MontoGravableAlicuota3 money = 0," & vbCrLf
   vResult = vResult & "         @P_MontoIVAAlicuotaGeneral money = 0," & vbCrLf
   vResult = vResult & "         @P_MontoIVAAlicuota2 money = 0," & vbCrLf
   vResult = vResult & "         @P_MontoIVAAlicuota3 money = 0," & vbCrLf
   vResult = vResult & "         @P_TotalOtrosImpuestos money = 0," & vbCrLf
   vResult = vResult & "         @P_Observaciones " & gUtilSQL.getVarCharTypeForDB(255) & " = ''," & vbCrLf
   vResult = vResult & "         @P_NumeroDeFacturaAfectada " & gUtilSQL.getVarCharTypeForDB(11) & " = ''," & vbCrLf
   vResult = vResult & "         @P_NumeroControl " & gUtilSQL.getVarCharTypeForDB(20) & " = ''," & vbCrLf
   vResult = vResult & "         @P_AplicaParaLibrodeCompras  char(1)  = 'S'," & vbCrLf
   vResult = vResult & "         @P_AplicaDetraccion  char(1)  = 'N'," & vbCrLf
   vResult = vResult & "         @P_MesAplicacion  " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "         @P_AnoAplicacion " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "         @P_CodigoDetraccion " & gUtilSQL.getVarCharTypeForDB(10) & " = ''," & vbCrLf
   vResult = vResult & "         @ConsecutivoCxP " & gUtilSQL.getNumericTypeForDB(9, 0) & " OUTPUT" & vbCrLf
   fSpParametersS10Insert = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10Insert", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10Insert() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Operador " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @MtoGravado money" & vbCrLf
   vResult = vResult & "   DECLARE @MtoIva money" & vbCrLf
   vResult = vResult & "   DECLARE @Hoy " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @DiasVencimiento " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @FechaVenc " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @CodMoneda " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NombreMoneda " & gUtilSQL.getVarCharTypeForDB(80) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NextConsecutivoCxP " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @P_ConsecutivoRendicion " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @TipoDeCxP char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @MesYAnoAplicacionValidos bit --1 == true 0 == False" & vbCrLf
   vResult = vResult & "   DECLARE @DesccripcionDeDetraccion" & gUtilSQL.getVarCharTypeForDB(100) & vbCrLf
   vResult = vResult & "   DECLARE @PorcentajeDetraccion money" & vbCrLf
   vResult = vResult & "   DECLARE @TotalDetraccion money"
   vResult = vResult & "   DECLARE @TotalMontoCxP money"
   vResult = vResult & "   IF NOT EXISTS(SELECT ConsecutivoCompania FROM COMPANIA WHERE ConsecutivoCompania = @P_ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @ConsecutivoCxP = -1" & vbCrLf
   vResult = vResult & "      RAISERROR ('No existe registro de Compañía para el Consecutivo dado%d.', 16, 1, @P_ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT @TipoDeCxP = TipoDeDocumento FROM TipoDeDocumentoLey WHERE (Codigo = @P_CodigoTipoDeDocumentoCxp)" & vbCrLf
   vResult = vResult & "      IF (@TipoDeCxP IS NULL)" & vbCrLf
   vResult = vResult & "         SET @TipoDeCxP = '0'" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT CodigoProveedor FROM Proveedor WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND CodigoProveedor = @P_CodigoProveedor)" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SET @ConsecutivoCxP = -1" & vbCrLf
   vResult = vResult & "         RAISERROR ('No existe registro de Proveedor para el Código dado%s.', 16, 1, @P_CodigoProveedor)" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SET @P_ConsecutivoRendicion = 0" & vbCrLf
   vResult = vResult & "         SET @MtoGravado = @P_MontoGravableAlicuotaGeneral + @P_MontoGravableAlicuota2 + @P_MontoGravableAlicuota3" & vbCrLf
   vResult = vResult & "         SET @MtoIva = @P_MontoIVAAlicuotaGeneral + @P_MontoIVAAlicuota2 + @P_MontoIVAAlicuota3" & vbCrLf
   vResult = vResult & "         -- Validación de Fechas Mes y Año de Aplicacion" & vbCrLf
   vResult = vResult & "         SET @MesYAnoAplicacionValidos = 1 -- 1 = True" & vbCrLf
   vResult = vResult & "         IF @P_MesAplicacion = 0 AND @P_AnoAplicacion = 0" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @P_MesAplicacion = MONTH(@P_Fecha)" & vbCrLf
   vResult = vResult & "            SET @P_MesAplicacion = YEAR(@P_Fecha)" & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            IF @P_MesAplicacion < 1 AND @P_MesAplicacion > 12" & vbCrLf
   vResult = vResult & "               SET @MesYAnoAplicacionValidos = 0 -- FALSE" & vbCrLf
   vResult = vResult & "            IF @MesYAnoAplicacionValidos = 1 AND @P_AnoAplicacion < 1993 AND  @P_AnoAplicacion > 2080" & vbCrLf
   vResult = vResult & "               SET @MesYAnoAplicacionValidos = 0 -- FALSE" & vbCrLf
   vResult = vResult & "            IF @MesYAnoAplicacionValidos = 1 AND CONVERT(" & gUtilSQL.getDateTypeForDB() & ",Cast(DAY(@P_Fecha) as Varchar) + '/' + Cast(@P_MesAplicacion  as Varchar) + '/' + Cast(@P_AnoAplicacion as Varchar), 103) < CONVERT( " & gUtilSQL.getDateTypeForDB() & " , @P_Fecha, 103)" & vbCrLf
   vResult = vResult & "               SET @MesYAnoAplicacionValidos = 0 -- FALSE" & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "         IF @MesYAnoAplicacionValidos  = 1" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @Hoy = CONVERT(" & gUtilSQL.getDateTypeForDB() & ",  GETDATE()  ,103)" & vbCrLf
   vResult = vResult & "            IF @P_FechaVencimiento = ''" & vbCrLf
   vResult = vResult & "               SET @FechaVenc = @Hoy" & vbCrLf
   vResult = vResult & "            IF CONVERT(" & gUtilSQL.getDateTypeForDB() & ", @P_Fecha, 103) < CONVERT(" & gUtilSQL.getDateTypeForDB() & ", @FechaVenc, 103)" & vbCrLf
   vResult = vResult & "               SET @FechaVenc = @P_Fecha" & vbCrLf
   vResult = vResult & "            SET @DiasVencimiento = DATEDIFF(d, @P_Fecha, @FechaVenc)" & vbCrLf
   vResult = vResult & "            SET @Operador = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "            IF EXISTS(SELECT Nombre FROM Moneda WHERE Codigo = @P_CodigoMoneda)" & vbCrLf
   vResult = vResult & "               SELECT @NombreMoneda = Nombre, @CodMoneda = Codigo FROM Moneda WHERE Codigo = @P_CodigoMoneda" & vbCrLf
   vResult = vResult & "            ELSE" & vbCrLf
   vResult = vResult & "               IF @P_CambioAMonedaLocal > 1" & vbCrLf
   'vResult = vResult & "                  SELECT TOP 1 @NombreMoneda = NombreMonedaExtranjera, @CodMoneda = CodigoMonedaExtranjera FROM ParametrosAdministrativo" & vbCrLf
   vResult = vResult & "                  SELECT TOP 1"
   vResult = vResult & "                  @NombreMoneda = Aux1SettValue.Value,"
   vResult = vResult & "                  @CodMoneda = Aux2SettValue.Value"
   vResult = vResult & "                  FROM Comun.SettValueByCompany Stt"
   vResult = vResult & "                  CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'NombreMonedaExtranjera') AS Aux1SettValue(Value)"
   vResult = vResult & "                  CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'CodigoMonedaExtranjera') AS Aux2SettValue(Value)"
   vResult = vResult & "                  GROUP BY Aux1SettValue.Value,Aux2SettValue.Value"
   vResult = vResult & "               ELSE" & vbCrLf
   'vResult = vResult & "                  SELECT TOP 1 @NombreMoneda = NombreMonedaLocal, @CodMoneda = CodigoMonedaLocal FROM ParametrosAdministrativo" & vbCrLf
   vResult = vResult & "                  SELECT TOP 1"
   vResult = vResult & "                  @NombreMoneda = Aux1SettValue.Value,"
   vResult = vResult & "                  @CodMoneda = Aux2SettValue.Value"
   vResult = vResult & "                  FROM Comun.SettValueByCompany Stt"
   vResult = vResult & "                  CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'NombreMonedaExtranjera') AS Aux1SettValue(Value)"
   vResult = vResult & "                  CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'CodigoMonedaExtranjera') AS Aux2SettValue(Value)"
   vResult = vResult & "                  GROUP BY Aux1SettValue.Value,Aux2SettValue.Value"
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
   vResult = vResult & "               SET @TotalMontoCxP = @P_MontoExento + @P_MontoGravableAlicuotaGeneral + @P_MontoGravableAlicuota2 + @P_MontoGravableAlicuota3              + @P_MontoIVAAlicuotaGeneral +         @P_MontoIVAAlicuota2 +  @P_MontoIVAAlicuota3" & vbCrLf
   vResult = vResult & "               SET @TotalDetraccion = @TotalMontoCxP * (@PorcentajeDetraccion/100)" & vbCrLf
   vResult = vResult & "            END" & vbCrLf
   vResult = vResult & "            IF EXISTS(SELECT Numero FROM CxP WHERE  ConsecutivoCompania = @P_ConsecutivoCompania AND Numero = @P_Numero AND TipoDeCxp = @TipoDeCxP AND CodigoProveedor = @P_CodigoProveedor)" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SET @ConsecutivoCxP = -1" & vbCrLf
   vResult = vResult & "               RAISERROR('Ya existe un registro para los datos suministrados para Cuenta por Pagar.',16,1)" & vbCrLf
   vResult = vResult & "            END " & vbCrLf
   vResult = vResult & "            ELSE" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SELECT @NextConsecutivoCxP = MAX(ConsecutivoCxP) FROM CxP WHERE ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "               IF @NextConsecutivoCxP IS NULL" & vbCrLf
   vResult = vResult & "                  SET @NextConsecutivoCxP = 1" & vbCrLf
   vResult = vResult & "               ELSE" & vbCrLf
   vResult = vResult & "               BEGIN" & vbCrLf
   vResult = vResult & "                  IF @NextConsecutivoCxP > 0" & vbCrLf
   vResult = vResult & "                     SET @NextConsecutivoCxP = @NextConsecutivoCxP + 1" & vbCrLf
   vResult = vResult & "                  ELSE" & vbCrLf
   vResult = vResult & "                     SET @NextConsecutivoCxP = 1" & vbCrLf
   vResult = vResult & "               END " & vbCrLf
   vResult = vResult & "               EXEC Gp_CxPINS" & vbCrLf
   vResult = vResult & "                  @ConsecutivoCompania = @P_ConsecutivoCompania," & vbCrLf
   vResult = vResult & "                  @ConsecutivoCxP = @NextConsecutivoCxP," & vbCrLf
   vResult = vResult & "                  @Numero = @P_Numero," & vbCrLf
   vResult = vResult & "                  @TipoDeCxP = @TipoDeCxP," & vbCrLf
   vResult = vResult & "                  @Status = '0'," & vbCrLf
   vResult = vResult & "                  @CodigoProveedor = @P_CodigoProveedor," & vbCrLf
   vResult = vResult & "                  @Fecha = @P_Fecha," & vbCrLf
   vResult = vResult & "                  @FechaCancelacion = @Hoy," & vbCrLf
   vResult = vResult & "                  @FechaVencimiento = @FechaVenc," & vbCrLf
   vResult = vResult & "                  @FechaAnulacion = @Hoy," & vbCrLf
   vResult = vResult & "                  @Moneda = @NombreMoneda," & vbCrLf
   vResult = vResult & "                  @CambioABolivares = @P_CambioAMonedaLocal," & vbCrLf
   vResult = vResult & "                  @AplicaParaLibrodeCompras = @P_AplicaParaLibrodeCompras," & vbCrLf
   vResult = vResult & "                  @MontoExento = @P_MontoExento," & vbCrLf
   vResult = vResult & "                  @MontoGravado = @MtoGravado," & vbCrLf
   vResult = vResult & "                  @MontoIva = @MtoIva," & vbCrLf
   vResult = vResult & "                  @MontoAbonado = 0," & vbCrLf
   vResult = vResult & "                  @MesDeAplicacion = @P_MesAplicacion," & vbCrLf
   vResult = vResult & "                  @AnoDeAplicacion = @P_AnoAplicacion," & vbCrLf
   vResult = vResult & "                  @Observaciones = @P_Observaciones," & vbCrLf
   vResult = vResult & "                  @CreditoFiscal = '0'," & vbCrLf
   vResult = vResult & "                  @TipoDeCompra = '2'," & vbCrLf
   vResult = vResult & "                  @SeHizoLaRetencion = 'N'," & vbCrLf
   vResult = vResult & "                  @MontoGravableAlicuotaGeneral = @P_MontoGravableAlicuotaGeneral," & vbCrLf
   vResult = vResult & "                  @MontoGravableAlicuota2 = @P_MontoGravableAlicuota2," & vbCrLf
   vResult = vResult & "                  @MontoGravableAlicuota3 = @P_MontoGravableAlicuota3," & vbCrLf
   vResult = vResult & "                  @MontoIVAAlicuotaGeneral = @P_MontoIVAAlicuotaGeneral," & vbCrLf
   vResult = vResult & "                  @MontoIVAAlicuota2 = @P_MontoIVAAlicuota2," & vbCrLf
   vResult = vResult & "                  @MontoIVAAlicuota3 = @P_MontoIVAAlicuota3," & vbCrLf
   vResult = vResult & "                  @NumeroPlanillaDeImportacion = ''," & vbCrLf
   vResult = vResult & "                  @NumeroExpedienteDeImportacion = ''," & vbCrLf
   vResult = vResult & "                  @TipoDeTransaccion = '0'," & vbCrLf
   vResult = vResult & "                  @NumeroDeFacturaAfectada = @P_NumeroDeFacturaAfectada," & vbCrLf
   vResult = vResult & "                  @NumeroControl = @P_NumeroControl," & vbCrLf
   vResult = vResult & "                  @SeHizoLaRetencionIVA = 'N'," & vbCrLf
   vResult = vResult & "                  @NumeroComprobanteRetencion = ''," & vbCrLf
   vResult = vResult & "                  @FechaAplicacionRetIVA = @Hoy," & vbCrLf
   vResult = vResult & "                  @PorcentajeRetencionAplicado = 0," & vbCrLf
   vResult = vResult & "                  @MontoRetenido = 0," & vbCrLf
   vResult = vResult & "                  @OrigenDeLaRetencion = '0'," & vbCrLf
   vResult = vResult & "                  @RetencionAplicadaEnPago = 'N'," & vbCrLf
   vResult = vResult & "                  @OrigenInformacionRetencion = '0'," & vbCrLf
   vResult = vResult & "                  @CxPgeneradaPor = '0'," & vbCrLf
   vResult = vResult & "                  @EsCxPhistorica = 'N'," & vbCrLf
   vResult = vResult & "                  @NumDiasDeVencimiento = @DiasVencimiento," & vbCrLf
   vResult = vResult & "                  @NumeroDocOrigen = ''," & vbCrLf
   vResult = vResult & "                  @CodigoLote = ''," & vbCrLf
   vResult = vResult & "                  @GenerarAsientoDeRetiroEnCuenta = 'N'," & vbCrLf
   vResult = vResult & "                  @TotalOtrosImpuestos = @P_TotalOtrosImpuestos," & vbCrLf
   vResult = vResult & "                  @SeContabilRetIva = 'N'," & vbCrLf
   vResult = vResult & "                  @DondeContabilRetIva = '0'," & vbCrLf
   vResult = vResult & "                  @CodigoMoneda = @CodMoneda," & vbCrLf
   vResult = vResult & "                  @EstaAsociadoARendicion = 'N'," & vbCrLf
   vResult = vResult & "                  @ConsecutivoRendicion = @P_ConsecutivoRendicion," & vbCrLf
   vResult = vResult & "                  @CodigoTipoDeDocumentoLey = @P_CodigoTipoDeDocumentoCxp," & vbCrLf
   vResult = vResult & "                  @AplicaDetraccion = @P_AplicaDetraccion," & vbCrLf
   vResult = vResult & "                  @NumeroDetraccion = ''," & vbCrLf
   vResult = vResult & "                  @CodigoDetraccion = @P_CodigoDetraccion," & vbCrLf
   vResult = vResult & "                  @DescripcionDeDetraccion = @DesccripcionDeDetraccion," & vbCrLf
   vResult = vResult & "                  @PorcentajeDetraccion = @PorcentajeDetraccion," & vbCrLf
   vResult = vResult & "                  @TotalDetraccion = @TotalDetraccion," & vbCrLf
   vResult = vResult & "                  @StatusDetraccion = '2'," & vbCrLf
   vResult = vResult & "                  @ConsecutivoMovimiento = 0," & vbCrLf
   vResult = vResult & "                  @NombreOperador = @Operador" & vbCrLf
   vResult = vResult & "               SELECT @ConsecutivoCxP = ConsecutivoCxP" & vbCrLf
   vResult = vResult & "                  FROM cxP" & vbCrLf
   vResult = vResult & "                  WHERE ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                     AND Numero = @P_Numero" & vbCrLf
   vResult = vResult & "                     AND TipoDeCxp = @TipoDeCxP" & vbCrLf
   vResult = vResult & "                     AND CodigoProveedor = @P_CodigoProveedor" & vbCrLf
   vResult = vResult & "            END " & vbCrLf
   vResult = vResult & "          END " & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpS10Insert = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10Insert", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpS10Update(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10Update(), fSpParametersS10Update(), fStatementsSpS10Update(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10Update = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10Update", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10Update(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10Update(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10Update = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10Update", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10Update() As String
   GetSpNameS10Update = "S10_CxPUPD"
End Function

Private Function fSpParametersS10Update() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @P_ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @P_Numero " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "         @P_CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "         @P_CodigoTipoDeDocumentoCxP " & gUtilSQL.getVarCharTypeForDB(10) & " = null," & vbCrLf
   vResult = vResult & "         @P_FechaVencimiento " & gUtilSQL.getDateTypeForDB() & " = null," & vbCrLf
   vResult = vResult & "         @P_CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & " = null," & vbCrLf
   vResult = vResult & "         @P_CambioAMonedaLocal " & gUtilSQL.getNumericTypeForDB(12, 2) & " = null," & vbCrLf
   vResult = vResult & "         @P_AplicaParaLibrodeCompras " & gUtilSQL.getCharTypeForDB(1) & " = null," & vbCrLf
   vResult = vResult & "         @P_MontoExento " & gUtilSQL.getNumericTypeForDB(12, 2) & " = null," & vbCrLf
   vResult = vResult & "         @P_Observaciones " & gUtilSQL.getVarCharTypeForDB(255) & " = null," & vbCrLf
   vResult = vResult & "         @P_MontoGravableAlicuotaGeneral " & gUtilSQL.getNumericTypeForDB(12, 2) & " = null," & vbCrLf
   vResult = vResult & "         @P_MontoGravableAlicuota2 " & gUtilSQL.getNumericTypeForDB(12, 2) & " = null," & vbCrLf
   vResult = vResult & "         @P_MontoGravableAlicuota3 " & gUtilSQL.getNumericTypeForDB(12, 2) & " = null," & vbCrLf
   vResult = vResult & "         @P_MontoIVAAlicuotaGeneral " & gUtilSQL.getNumericTypeForDB(12, 2) & " = null," & vbCrLf
   vResult = vResult & "         @P_MontoIVAAlicuota2 " & gUtilSQL.getNumericTypeForDB(12, 2) & " = null," & vbCrLf
   vResult = vResult & "         @P_MontoIVAAlicuota3 " & gUtilSQL.getNumericTypeForDB(12, 2) & " = null, " & vbCrLf
   vResult = vResult & "         @P_NumeroControl " & gUtilSQL.getVarCharTypeForDB(20) & " = null," & vbCrLf
   vResult = vResult & "         @P_MesAplicacion  " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "         @P_AnoAplicacion " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "         @P_AplicaDetraccion " & gUtilSQL.getCharTypeForDB(1) & " = null," & vbCrLf
   vResult = vResult & "         @P_CodigoDetraccion " & gUtilSQL.getVarCharTypeForDB(10) & " = ''" & vbCrLf
    fSpParametersS10Update = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10Update", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10Update() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Operador " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @MtoGravado money" & vbCrLf
   vResult = vResult & "   DECLARE @MtoIva money" & vbCrLf
   vResult = vResult & "   DECLARE @CanBeUpdated bit" & vbCrLf
   vResult = vResult & "   DECLARE @CompaniaUsaModuloDeContabilidadSN char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxPEnum " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxPChar char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CodMoneda " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NombreMoneda " & gUtilSQL.getVarCharTypeForDB(80) & "" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecCxP " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @MesYAnoAplicacionValidos bit" & vbCrLf
   vResult = vResult & "   DECLARE @Fecha " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @TipoDeCxP " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @DesccripcionDeDetraccion" & gUtilSQL.getVarCharTypeForDB(100) & vbCrLf
   vResult = vResult & "   DECLARE @PorcentajeDetraccion money" & vbCrLf
   vResult = vResult & "   DECLARE @TotalDetraccion money"
   vResult = vResult & "   DECLARE @TotalMontoCxP money"
   vResult = vResult & "   IF EXISTS(SELECT ConsecutivoCxP FROM CxP WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND Numero = @P_Numero AND CodigoProveedor = @P_CodigoProveedor)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "                SELECT @Fecha = Fecha FROM CxP WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND Numero = @P_Numero AND CodigoProveedor = @P_CodigoProveedor" & vbCrLf
   vResult = vResult & "      SELECT @ConsecCxP = ConsecutivoCxP FROM CxP WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND Numero = @P_Numero AND CodigoProveedor = @P_CodigoProveedor" & vbCrLf
   vResult = vResult & "      SET @CanBeUpdated = dbo.Gf_CxPCanUpd(@P_ConsecutivoCompania, @ConsecCxP)" & vbCrLf
   vResult = vResult & "      IF @CanBeUpdated = 1 --True" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SELECT @TipoDeCxP = TipoDeDocumento FROM TipoDeDocumentoLey WHERE (Codigo = @P_CodigoTipoDeDocumentoCxp)" & vbCrLf
   vResult = vResult & "         SELECT @CompaniaUsaModuloDeContabilidadSN = UsaModuloDeContabilidad FROM Compania WHERE ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "         IF (UPPER(@CompaniaUsaModuloDeContabilidadSN) = 'S')" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @GeneradoPorCxPEnum = 11 --CxP" & vbCrLf
   vResult = vResult & "            SET @GeneradoPorCxPChar = dbo.Gf_EnumToDBValue(@GeneradoPorCxPEnum) --CxP" & vbCrLf
   vResult = vResult & "            SET @NumeroDocumentoOrigen = @P_CodigoProveedor + char(9) + @P_Numero" & vbCrLf
   vResult = vResult & "            SELECT @NumeroComprobante = COMPROBANTE.Numero," & vbCrLf
   vResult = vResult & "                  @ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "               FROM COMPROBANTE INNER JOIN PERIODO" & vbCrLf
   vResult = vResult & "                  ON COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "               WHERE PERIODO.ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                  AND COMPROBANTE.NoDocumentoOrigen = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "                  AND COMPROBANTE.GeneradoPor = @GeneradoPorCxPChar" & vbCrLf
   vResult = vResult & "            EXEC Gp_ComprobanteDEL @ConsecutivoPeriodo, @NumeroComprobante" & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "         IF (NOT @P_MontoGravableAlicuotaGeneral IS NULL) OR (NOT @P_MontoGravableAlicuota2 IS NULL) OR (NOT @P_MontoGravableAlicuota3 IS NULL)" & vbCrLf
   vResult = vResult & "            SET @MtoGravado = 0" & vbCrLf
   vResult = vResult & "            IF (NOT @P_MontoGravableAlicuotaGeneral IS NULL)" & vbCrLf
   vResult = vResult & "               SET @MtoGravado = @P_MontoGravableAlicuotaGeneral" & vbCrLf
   vResult = vResult & "            IF (NOT @P_MontoGravableAlicuota2 IS NULL)" & vbCrLf
   vResult = vResult & "               SET @MtoGravado = @MtoGravado + @P_MontoGravableAlicuota2" & vbCrLf
   vResult = vResult & "            IF (NOT @P_MontoGravableAlicuota3 IS NULL)" & vbCrLf
   vResult = vResult & "               SET @MtoGravado = @MtoGravado + @P_MontoGravableAlicuota3" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "            SET @MtoGravado = null" & vbCrLf
   vResult = vResult & "         IF (NOT @P_MontoIVAAlicuotaGeneral IS NULL) OR (NOT @P_MontoIVAAlicuota2 IS NULL) OR (NOT @P_MontoIVAAlicuota3 IS NULL)" & vbCrLf
   vResult = vResult & "            SET @MtoIva = 0" & vbCrLf
   vResult = vResult & "            IF (NOT @P_MontoIVAAlicuotaGeneral IS NULL)" & vbCrLf
   vResult = vResult & "               SET @MtoIva = @P_MontoIVAAlicuotaGeneral" & vbCrLf
   vResult = vResult & "            IF (NOT @P_MontoIVAAlicuota2 IS NULL)" & vbCrLf
   vResult = vResult & "               SET @MtoIva = @MtoIva + @P_MontoIVAAlicuota2" & vbCrLf
   vResult = vResult & "            IF (NOT @P_MontoIVAAlicuota3 IS NULL)" & vbCrLf
   vResult = vResult & "               SET @MtoIva = @MtoIva + @P_MontoIVAAlicuota3" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "            SET @MtoIva = null" & vbCrLf
   vResult = vResult & "         SET @Operador = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "         SET @MesYAnoAplicacionValidos = 1" & vbCrLf
   vResult = vResult & "         IF Not(@P_MesAplicacion Is Null) AND NOT(@P_AnoAplicacion Is Null)" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            IF @P_MesAplicacion < 1 AND @P_MesAplicacion > 12" & vbCrLf
   vResult = vResult & "               SET @MesYAnoAplicacionValidos = 0 -- FALSE" & vbCrLf
   vResult = vResult & "             IF @MesYAnoAplicacionValidos = 1 AND @P_AnoAplicacion < 1993 AND  @P_AnoAplicacion > 2080" & vbCrLf
   vResult = vResult & "                SET @MesYAnoAplicacionValidos = 0 -- FALSE" & vbCrLf
   vResult = vResult & "             IF @MesYAnoAplicacionValidos = 1 AND CONVERT(" & gUtilSQL.getDateTypeForDB() & ", Cast(DAY(@Fecha) as Varchar) + '/' + Cast(@P_MesAplicacion  as Varchar) + '/' + Cast(@P_AnoAplicacion as Varchar), 103) < CONVERT( " & gUtilSQL.getDateTypeForDB() & ", @Fecha, 103)" & vbCrLf
   vResult = vResult & "                SET @MesYAnoAplicacionValidos = 0 -- FALSE" & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "         IF @MesYAnoAplicacionValidos = 0" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @P_MesAplicacion = NULL" & vbCrLf
   vResult = vResult & "            SET @P_AnoAplicacion = NULL" & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "            SELECT  @DesccripcionDeDetraccion = BienServicio,  @PorcentajeDetraccion = Porcentaje FROM Detracciones WHERE Codigo = @P_CodigoDetraccion" & vbCrLf
   vResult = vResult & "            IF (@PorcentajeDetraccion Is Null) AND UPPER(@P_AplicaDetraccion) = 'S'" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SET @P_CodigoDetraccion = null" & vbCrLf
   vResult = vResult & "               SET @DesccripcionDeDetraccion = null" & vbCrLf
   vResult = vResult & "               SET @PorcentajeDetraccion = null" & vbCrLf
   vResult = vResult & "               SET @TotalDetraccion = null" & vbCrLf
   vResult = vResult & "            END" & vbCrLf
   vResult = vResult & "            ELSE" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SET @TotalMontoCxP = @P_MontoExento + @P_MontoGravableAlicuotaGeneral + @P_MontoGravableAlicuota2 + @P_MontoGravableAlicuota3              + @P_MontoIVAAlicuotaGeneral +         @P_MontoIVAAlicuota2 +  @P_MontoIVAAlicuota3" & vbCrLf
   vResult = vResult & "               SET @TotalDetraccion = @TotalMontoCxP * (@PorcentajeDetraccion/100)" & vbCrLf
   vResult = vResult & "            END" & vbCrLf
   vResult = vResult & "         IF EXISTS(SELECT Nombre FROM Moneda WHERE Codigo = @P_CodigoMoneda)" & vbCrLf
   vResult = vResult & "            SELECT @NombreMoneda = Nombre, @CodMoneda = Codigo FROM Moneda WHERE Codigo = @P_CodigoMoneda" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "            IF @P_CambioAMonedaLocal > 1" & vbCrLf
   'vResult = vResult & "               SELECT TOP 1 @NombreMoneda = NombreMonedaExtranjera, @CodMoneda = CodigoMonedaExtranjera FROM ParametrosAdministrativo" & vbCrLf
   vResult = vResult & "                  SELECT TOP 1"
   vResult = vResult & "                  @NombreMoneda = Aux1SettValue.Value,"
   vResult = vResult & "                  @CodMoneda = Aux2SettValue.Value"
   vResult = vResult & "                  FROM Comun.SettValueByCompany Stt"
   vResult = vResult & "                  CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'NombreMonedaExtranjera') AS Aux1SettValue(Value)"
   vResult = vResult & "                  CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'CodigoMonedaExtranjera') AS Aux2SettValue(Value)"
   vResult = vResult & "                  GROUP BY Aux1SettValue.Value,Aux2SettValue.Value"
   vResult = vResult & "            ELSE" & vbCrLf
   'vResult = vResult & "               SELECT TOP 1 @NombreMoneda = NombreMonedaLocal, @CodMoneda = CodigoMonedaLocal FROM ParametrosAdministrativo" & vbCrLf
   vResult = vResult & "                  SELECT TOP 1"
   vResult = vResult & "                  @NombreMoneda = Aux1SettValue.Value,"
   vResult = vResult & "                  @CodMoneda = Aux2SettValue.Value"
   vResult = vResult & "                  FROM Comun.SettValueByCompany Stt"
   vResult = vResult & "                  CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'NombreMonedaExtranjera') AS Aux1SettValue(Value)"
   vResult = vResult & "                  CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'CodigoMonedaExtranjera') AS Aux2SettValue(Value)"
   vResult = vResult & "                  GROUP BY Aux1SettValue.Value,Aux2SettValue.Value"
   vResult = vResult & "         EXEC Gp_CxPUPD" & vbCrLf
   vResult = vResult & "               @ConsecutivoCompania = @P_ConsecutivoCompania," & vbCrLf
   vResult = vResult & "               @ConsecutivoCxP = @ConsecCxP," & vbCrLf
   vResult = vResult & "               @Numero = @P_Numero," & vbCrLf
   vResult = vResult & "               @CodigoProveedor = @P_CodigoProveedor," & vbCrLf
   vResult = vResult & "               @TipoDeCxP = @TipoDeCxP," & vbCrLf
   vResult = vResult & "               @FechaVencimiento = @P_FechaVencimiento," & vbCrLf
   vResult = vResult & "               @Moneda = @NombreMoneda," & vbCrLf
   vResult = vResult & "               @CambioABolivares = @P_CambioAMonedaLocal," & vbCrLf
   vResult = vResult & "               @AplicaParaLibrodeCompras = @P_AplicaParaLibrodeCompras," & vbCrLf
   vResult = vResult & "               @MontoExento = @P_MontoExento," & vbCrLf
   vResult = vResult & "               @MontoGravado = @MtoGravado," & vbCrLf
   vResult = vResult & "               @MontoIva = @MtoIva," & vbCrLf
   vResult = vResult & "               @MesDeAplicacion = @P_MesAplicacion," & vbCrLf
   vResult = vResult & "               @AnoDeAplicacion = @P_AnoAplicacion," & vbCrLf
   vResult = vResult & "               @Observaciones = @P_Observaciones," & vbCrLf
   vResult = vResult & "               @MontoGravableAlicuotaGeneral = @P_MontoGravableAlicuotaGeneral," & vbCrLf
   vResult = vResult & "               @MontoGravableAlicuota2 = @P_MontoGravableAlicuota2," & vbCrLf
   vResult = vResult & "               @MontoGravableAlicuota3 = @P_MontoGravableAlicuota3," & vbCrLf
   vResult = vResult & "               @MontoIVAAlicuotaGeneral = @P_MontoIVAAlicuotaGeneral," & vbCrLf
   vResult = vResult & "               @MontoIVAAlicuota2 = @P_MontoIVAAlicuota2," & vbCrLf
   vResult = vResult & "               @MontoIVAAlicuota3 = @P_MontoIVAAlicuota3," & vbCrLf
   vResult = vResult & "               @CodigoMoneda = @CodMoneda," & vbCrLf
   vResult = vResult & "               @NumeroControl = @P_NumeroControl," & vbCrLf
   vResult = vResult & "               @CodigoTipoDeDocumentoLey = @P_CodigoTipoDeDocumentoCxP," & vbCrLf
   vResult = vResult & "               @AplicaDetraccion = @P_AplicaDetraccion," & vbCrLf
   vResult = vResult & "               @CodigoDetraccion = @P_CodigoDetraccion,"
   vResult = vResult & "               @DescripcionDeDetraccion = @DesccripcionDeDetraccion,"
   vResult = vResult & "               @PorcentajeDetraccion = @PorcentajeDetraccion,"
   vResult = vResult & "               @TotalDetraccion = @TotalDetraccion,"
   vResult = vResult & "               @NombreOperador = @Operador" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "         RETURN 0" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      RETURN 0" & vbCrLf
   vResult = vResult & "END "
   fStatementsSpS10Update = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10Update", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpS10Select(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10Select(), fSpParametersS10Select(), fStatementsSpS10Select(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10Select = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10Select", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10Select(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10Select(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10Select = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10Select", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10Select() As String
   GetSpNameS10Select = "S10_CxPSEL"
End Function

Private Function fSpParametersS10Select() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @FixedWhereClause " & gUtilSQL.getVarCharTypeForDB(4000) & " = ''" & vbCrLf
   fSpParametersS10Select = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10Select", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10Select() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Sql " & gUtilSQL.getVarCharTypeForDB(6000) & vbCrLf
   vResult = vResult & "   SET @Sql = 'SELECT" & vbCrLf
   vResult = vResult & "               Numero, CodigoTipoDeDocumentoLey as  CodigoTipoDeDocumentoCxp, Status, CodigoProveedor," & vbCrLf
   vResult = vResult & "               Fecha, FechaCancelacion, FechaVencimiento, FechaAnulacion," & vbCrLf
   vResult = vResult & "               CodigoMoneda, Moneda, CambioAbolivares AS CambioAMonedaLocal," & vbCrLf
   vResult = vResult & "               MontoExento, MontoGravado, MontoIva, MontoAbonado," & vbCrLf
   vResult = vResult & "               Observaciones," & vbCrLf
   vResult = vResult & "               MontoGravableAlicuotaGeneral, MontoGravableAlicuota2, MontoGravableAlicuota3," & vbCrLf
   vResult = vResult & "               MontoIvaalicuotaGeneral, MontoIvaalicuota2, MontoIvaalicuota3," & vbCrLf
   vResult = vResult & "               NumDiasDeVencimiento" & vbCrLf
   vResult = vResult & "            FROM cxP" & vbCrLf
   vResult = vResult & "            WHERE ConsecutivoCompania = ' + CAST(@ConsecutivoCompania as varchar)" & vbCrLf
   vResult = vResult & "   IF @FixedWhereClause <> ''" & vbCrLf
   vResult = vResult & "      SET @Sql = @Sql + ' AND ' + @FixedWhereClause" & vbCrLf
   vResult = vResult & "   EXEC(@Sql)" & vbCrLf
   vResult = vResult & "   RETURN @@ROWCOUNT" & vbCrLf
   vResult = vResult & "END "
   fStatementsSpS10Select = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10Select", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpS10Delete(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10Delete(), fSpParametersS10Delete(), fStatementsSpS10Delete(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10Delete = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10Delete", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10Delete(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10Delete(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10Delete = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10Delete", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10Delete() As String
   GetSpNameS10Delete = "S10_CxPDEL"   'No se modifico estaba igual
End Function

Private Function fSpParametersS10Delete() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "      @Numero " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "      @P_CodigoTipoDeDocumentoCxp " & gUtilSQL.getVarCharTypeForDB(10) & "  = '01'," & vbCrLf
   vResult = vResult & "      @CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
    fSpParametersS10Delete = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10Delete", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10Delete() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @TipoCxP " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   SELECT @TipoCxP = TipoDeDocumento FROM TipoDeDocumentoLey WHERE (Codigo = @P_CodigoTipoDeDocumentoCxp) " & vbCrLf
   vResult = vResult & "   EXEC Gp_ExecuteCxPDEL @ConsecutivoCompania, @Numero, @TipoCxP, @CodigoProveedor" & vbCrLf
   vResult = vResult & "END "
   fStatementsSpS10Delete = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10Delete", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetSpNameS10Anular = "S10_CxPAnular"
End Function

Private Function fSpParametersS10Anular() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @P_ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @P_Numero " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "         @P_CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
    fSpParametersS10Anular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10Anular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10Anular() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoCxP " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   SELECT @ConsecutivoCxP = ConsecutivoCxP FROM CxP WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND Numero = @P_Numero AND CodigoProveedor = @P_CodigoProveedor" & vbCrLf
   vResult = vResult & "   EXEC Gp_ExecuteCxPAnular @P_ConsecutivoCompania, @ConsecutivoCxP" & vbCrLf
   vResult = vResult & "END "
   fStatementsSpS10Anular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10Anular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpExecuteDelete(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameExecuteDelete(), fSpParametersExecuteDelete(), fStatementsSpExecuteDelete(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpExecuteDelete = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpExecuteDelete", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpExecuteDelete(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameExecuteDelete(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpExecuteDelete = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpExecuteDelete", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameExecuteDelete() As String
   GetSpNameExecuteDelete = "ExecuteCxPDEL"
End Function

Private Function fSpParametersExecuteDelete() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "      @Numero " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "      @TipoCxP " & gUtilSQL.getCharTypeForDB(1) & " = '0'," & vbCrLf
   vResult = vResult & "      @CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
    fSpParametersExecuteDelete = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersExecuteDelete", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpExecuteDelete() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @CanBeDeleted bit" & vbCrLf
   vResult = vResult & "   DECLARE @CompaniaUsaModuloDeContabilidadSN char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & "" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxPEnum " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxPChar char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoCxP " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT ConsecutivoCxP FROM CxP WHERE ConsecutivoCompania = @ConsecutivoCompania AND Numero = @Numero AND CodigoProveedor = @CodigoProveedor)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT @ConsecutivoCxP = ConsecutivoCxP FROM cxP WHERE ConsecutivoCompania = @ConsecutivoCompania AND Numero = @Numero AND CodigoProveedor = @CodigoProveedor" & vbCrLf
   vResult = vResult & "      SET @CanBeDeleted = dbo.Gf_CxPCanDel(@ConsecutivoCompania, @ConsecutivoCxP)" & vbCrLf
   vResult = vResult & "      IF @CanBeDeleted = 1 --TRUE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SELECT @CompaniaUsaModuloDeContabilidadSN = UsaModuloDeContabilidad FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "         IF (UPPER(@CompaniaUsaModuloDeContabilidadSN) = 'S')" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @GeneradoPorCxPEnum = 11 --CxP" & vbCrLf
   vResult = vResult & "            SET @GeneradoPorCxPChar = dbo.Gf_EnumToDBValue(@GeneradoPorCxPEnum) --CxP" & vbCrLf
   vResult = vResult & "            SET @NumeroDocumentoOrigen = @CodigoProveedor + char(9) + @Numero" & vbCrLf
   vResult = vResult & "            SELECT @NumeroComprobante = COMPROBANTE.Numero," & vbCrLf
   vResult = vResult & "                  @ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "               FROM COMPROBANTE INNER JOIN PERIODO" & vbCrLf
   vResult = vResult & "                  ON COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "               WHERE PERIODO.ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                  AND COMPROBANTE.NoDocumentoOrigen = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "                  AND COMPROBANTE.GeneradoPor = @GeneradoPorCxPChar" & vbCrLf
   vResult = vResult & "         END "
   vResult = vResult & "         EXEC Gp_CxPDEL @ConsecutivoCompania, @ConsecutivoCxP" & vbCrLf
   vResult = vResult & "         IF (UPPER(@CompaniaUsaModuloDeContabilidadSN) = 'S')" & vbCrLf
   vResult = vResult & "            EXEC Gp_ComprobanteDel @ConsecutivoPeriodo, @NumeroComprobante" & vbCrLf
   vResult = vResult & "      END "
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      RETURN 0" & vbCrLf
   vResult = vResult & "   END "
   vResult = vResult & "END "
   fStatementsSpExecuteDelete = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpExecuteDelete", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetFnNameCanBeDeleted = "CxPCanDel"
End Function

Private Function fFnParametersCanBeDeleted() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@ConsecutivoCxP" & gUtilSQL.getNumericTypeForDB(9, 0)
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
   vResult = vResult & "   DECLARE @StatusCxP char(1)" & vbCrLf
   vResult = vResult & "   SELECT @StatusCxP = Status FROM CxP WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCxP = ConsecutivoCxP" & vbCrLf
   vResult = vResult & "   IF @StatusCxP = '0' --Por Cancelar" & vbCrLf
   vResult = vResult & "      IF EXISTS(SELECT Secuencial FROM DocumentoPagado WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCxP = ConsecutivoCxP)" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "         SET @Result = 1 --TRUE" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnCanBeDeleted = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnCanBeDeleted", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnCanBeUpdated(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameCanBeUpdated(), fFnParametersCanBeUpdated(), fFnScalarReturnDataTypeCanBeUpdated(), fStatementsFnCanBeUpdated(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnCanBeUpdated = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnCanBeUpdated", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnCanBeUpdated(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameCanBeUpdated(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnCanBeUpdated = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnCanBeUpdated", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameCanBeUpdated() As String
   GetFnNameCanBeUpdated = "CxPCanUpd"
End Function

Private Function fFnParametersCanBeUpdated() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania  " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@ConsecutivoCxP  " & gUtilSQL.getNumericTypeForDB(9, 0)
   fFnParametersCanBeUpdated = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersCanBeUpdated", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeCanBeUpdated() As String
   fFnScalarReturnDataTypeCanBeUpdated = "bit"
End Function

Private Function fStatementsFnCanBeUpdated() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Result bit" & vbCrLf
   vResult = vResult & "   DECLARE @Continue bit" & vbCrLf
   vResult = vResult & "   DECLARE @StatusCxP char(1)" & vbCrLf
   vResult = vResult & "   SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT @ConsecutivoCxP FROM CxP WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCxP = @ConsecutivoCxP)" & vbCrLf
   vResult = vResult & "      SET @Continue = 1 --True" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Continue = 0 --False" & vbCrLf
   vResult = vResult & "   IF @Continue = 1 --True" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT @StatusCxP = Status FROM CxP WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCxP = @ConsecutivoCxP" & vbCrLf
   vResult = vResult & "      IF @StatusCxP = '0' --Por Cancelar" & vbCrLf
   vResult = vResult & "         IF NOT EXISTS(SELECT Secuencial FROM DocumentoPagado WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCxP = @ConsecutivoCxP)" & vbCrLf
   vResult = vResult & "            SET @Result = 1 --TRUE" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnCanBeUpdated = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnCanBeUpdated", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnCanBeAnulado(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameCanBeAnulado(), fFnParametersCanBeAnulado(), fFnScalarReturnDataTypeCanBeAnulado(), fStatementsFnCanBeAnulado(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnCanBeAnulado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnCanBeAnulado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnCanBeAnulado(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameCanBeAnulado(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnCanBeAnulado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnCanBeAnulado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameCanBeAnulado() As String
   GetFnNameCanBeAnulado = "CxPCanAnular"
End Function

Private Function fFnParametersCanBeAnulado() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@ConsecutivoCxP " & gUtilSQL.getNumericTypeForDB(9, 0)
   fFnParametersCanBeAnulado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersCanBeAnulado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeCanBeAnulado() As String
   fFnScalarReturnDataTypeCanBeAnulado = "bit"
End Function

Private Function fStatementsFnCanBeAnulado() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Result bit" & vbCrLf
   vResult = vResult & "   DECLARE @StatusActual char(1)" & vbCrLf
   vResult = vResult & "   SET @Result = 0 --False" & vbCrLf
   vResult = vResult & "   SELECT @StatusActual = Status" & vbCrLf
   vResult = vResult & "         FROM CxP" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "            AND ConsecutivoCxP = @ConsecutivoCxP" & vbCrLf
   vResult = vResult & "   IF @StatusActual = '0'" & vbCrLf
   vResult = vResult & "      SET @Result = 1 --True" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsFnCanBeAnulado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnCanBeAnulado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetSpNameExecuteAnular = "ExecuteCxPAnular"
End Function

Private Function fSpParametersExecuteAnular() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @P_ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @P_ConsecutivoCxP " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
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
   vResult = vResult & "   DECLARE @Operador " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Fecha " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @DondeContabilRetIvaNuevo char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @ContEsp char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @CompaniaUsaModuloDeContabilidadSN char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & "" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxPEnum " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxPChar char(1)" & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT ConsecutivoCxP FROM cxP WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND ConsecutivoCxP = @P_ConsecutivoCxP)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @CanBe = dbo.Gf_CxPCanAnular(@P_ConsecutivoCompania, @P_ConsecutivoCxP)" & vbCrLf
   vResult = vResult & "      IF @CanBe = 1 --True" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SELECT @ContEsp = TipoDeContribuyenteIVA FROM COMPANIA WHERE ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "         IF @ContEsp = '2'" & vbCrLf
   vResult = vResult & "            SET @DondeContabilRetIvaNuevo = '1'" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "            SET @DondeContabilRetIvaNuevo = null" & vbCrLf
   vResult = vResult & "         SET @Fecha = CONVERT(" & gUtilSQL.getDateTypeForDB() & ", GETDATE(), 103)" & vbCrLf
   vResult = vResult & "         SET @Operador = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "         SELECT @CompaniaUsaModuloDeContabilidadSN = UsaModuloDeContabilidad FROM Compania WHERE ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "         IF (UPPER(@CompaniaUsaModuloDeContabilidadSN) = 'S')" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @GeneradoPorCxPEnum = 11 --CxP" & vbCrLf
   vResult = vResult & "            SET @GeneradoPorCxPChar = dbo.Gf_EnumToDBValue(@GeneradoPorCxPEnum) --CxP" & vbCrLf
   vResult = vResult & "            SET @NumeroDocumentoOrigen = CAST(@P_ConsecutivoCxP as varchar)" & vbCrLf
   vResult = vResult & "            SELECT @NumeroComprobante = COMPROBANTE.Numero," & vbCrLf
   vResult = vResult & "                  @ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "               FROM COMPROBANTE INNER JOIN PERIODO" & vbCrLf
   vResult = vResult & "                  ON COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "               WHERE PERIODO.ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                  AND COMPROBANTE.ConsecutivoDocOrigen = @P_ConsecutivoCxP" & vbCrLf
   vResult = vResult & "                  AND COMPROBANTE.GeneradoPor = @GeneradoPorCxPChar" & vbCrLf
   vResult = vResult & "                  AND COMPROBANTE.NoDocumentoOrigen = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "         EXEC Gp_CxPUPD" & vbCrLf
   vResult = vResult & "               @ConsecutivoCompania = @P_ConsecutivoCompania," & vbCrLf
   vResult = vResult & "               @ConsecutivoCxP = @P_ConsecutivoCxP," & vbCrLf
   vResult = vResult & "               @Status = '4'," & vbCrLf
   vResult = vResult & "               @FechaAnulacion = @Fecha," & vbCrLf
   vResult = vResult & "               @DondeContabilRetIva = @DondeContabilRetIvaNuevo," & vbCrLf
   vResult = vResult & "               @NombreOperador   = @Operador" & vbCrLf
   vResult = vResult & "         IF (UPPER(@CompaniaUsaModuloDeContabilidadSN) = 'S')" & vbCrLf
   vResult = vResult & "            EXEC Gp_ComprobanteAnular @ConsecutivoPeriodo, @NumeroComprobante, @Operador" & vbCrLf
   vResult = vResult & "        END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpExecuteAnular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpExecuteAnular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'=============================================================================
' Stored Procedure para Insertar un CxP con Rendicion
'=============================================================================
Private Function fCreateSpS10RendicionInsert(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10RendicionInsert(), fSpParametersS10RendicionInsert(), fStatementsSpS10RendicionInsert(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10RendicionInsert = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10RendicionInsert", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10RendicionInsert(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10RendicionInsert(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10RendicionInsert = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10RendicionInsert", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10RendicionInsert() As String
   GetSpNameS10RendicionInsert = "S10_Rendicion_CxPINS"
End Function

Private Function fSpParametersS10RendicionInsert() As String
   Dim vResult As String
   On Error GoTo h_ERROR

   vResult = "         @P_ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @P_ConsecutivoRendicion " & gUtilSQL.getVarCharTypeForDB(11) & "," & vbCrLf
   vResult = vResult & "         @P_Numero " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "         @P_CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "         @P_TipoDeCxP char(1) = '0'," & vbCrLf
   vResult = vResult & "         @P_Fecha " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "         @P_FechaVencimiento " & gUtilSQL.getDateTypeForDB() & " = ''," & vbCrLf
   vResult = vResult & "         @P_CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & "," & vbCrLf
   vResult = vResult & "         @P_CambioAMonedaLocal money = 1," & vbCrLf
   vResult = vResult & "         @P_MontoExento money = 0," & vbCrLf
   vResult = vResult & "         @P_MontoGravableAlicuotaGeneral money = 0," & vbCrLf
   vResult = vResult & "         @P_MontoGravableAlicuota2 money = 0," & vbCrLf
   vResult = vResult & "         @P_MontoGravableAlicuota3 money = 0," & vbCrLf
   vResult = vResult & "         @P_MontoIVAAlicuotaGeneral money = 0," & vbCrLf
   vResult = vResult & "         @P_MontoIVAAlicuota2 money = 0," & vbCrLf
   vResult = vResult & "         @P_MontoIVAAlicuota3 money = 0," & vbCrLf
   vResult = vResult & "         @P_TotalOtrosImpuestos money = 0," & vbCrLf
   vResult = vResult & "         @P_Observaciones " & gUtilSQL.getVarCharTypeForDB(255) & " = ''," & vbCrLf
   vResult = vResult & "         @P_NumeroDeFacturaAfectada " & gUtilSQL.getVarCharTypeForDB(11) & " = ''," & vbCrLf
   vResult = vResult & "         @P_NumeroControl " & gUtilSQL.getVarCharTypeForDB(20) & " = ''," & vbCrLf
   vResult = vResult & "         @ConsecutivoCxP " & gUtilSQL.getNumericTypeForDB(9, 0) & " OUTPUT" & vbCrLf
   fSpParametersS10RendicionInsert = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10RendicionInsert", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10RendicionInsert() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Operador " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @MtoGravado money" & vbCrLf
   vResult = vResult & "   DECLARE @MtoIva money" & vbCrLf
   vResult = vResult & "   DECLARE @Mes " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ano " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Hoy " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @DiasVencimiento " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @FechaVenc " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @CodMoneda " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NombreMoneda " & gUtilSQL.getVarCharTypeForDB(80) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NextConsecutivoCxP " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   IF NOT EXISTS(SELECT ConsecutivoCompania FROM COMPANIA WHERE ConsecutivoCompania = @P_ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @ConsecutivoCxP = -1" & vbCrLf
   vResult = vResult & "      RAISERROR ('No existe registro de Compañía para el Consecutivo dado%d.', 16, 1, @P_ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT CodigoProveedor FROM Proveedor WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND CodigoProveedor = @P_CodigoProveedor)" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SET @ConsecutivoCxP = -1" & vbCrLf
   vResult = vResult & "         RAISERROR ('No existe registro de Proveedor para el Código dado%s.', 16, 1, @P_CodigoProveedor)" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SET @MtoGravado = @P_MontoGravableAlicuotaGeneral + @P_MontoGravableAlicuota2 + @P_MontoGravableAlicuota3" & vbCrLf
   vResult = vResult & "         SET @MtoIva = @P_MontoIVAAlicuotaGeneral + @P_MontoIVAAlicuota2 + @P_MontoIVAAlicuota3" & vbCrLf
   vResult = vResult & "         SET @Mes = MONTH(@P_Fecha)" & vbCrLf
   vResult = vResult & "         SET @Ano = YEAR(@P_Fecha)" & vbCrLf
   vResult = vResult & "         SET @Hoy = " & gUtilSQL.DfCDateSQL(gUtilSQL.GetSQLFechaActual()) & "" & vbCrLf
   vResult = vResult & "         IF @P_FechaVencimiento = ''" & vbCrLf
   vResult = vResult & "            SET @FechaVenc = @Hoy" & vbCrLf
   vResult = vResult & "         IF CONVERT(" & gUtilSQL.getDateTypeForDB() & ", @P_Fecha, 103) < CONVERT(" & gUtilSQL.getDateTypeForDB() & ", @FechaVenc, 103)" & vbCrLf
   vResult = vResult & "            SET @FechaVenc = @P_Fecha" & vbCrLf
   vResult = vResult & "         SET @DiasVencimiento = DATEDIFF(d, @P_Fecha, @FechaVenc)" & vbCrLf
   vResult = vResult & "         SET @Operador = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "         IF EXISTS(SELECT Nombre FROM Moneda WHERE Codigo = @P_CodigoMoneda)" & vbCrLf
   vResult = vResult & "            SELECT @NombreMoneda = Nombre, @CodMoneda = Codigo FROM Moneda WHERE Codigo = @P_CodigoMoneda" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "            IF @P_CambioAMonedaLocal > 1" & vbCrLf
   'vResult = vResult & "               SELECT TOP 1 @NombreMoneda = NombreMonedaExtranjera, @CodMoneda = CodigoMonedaExtranjera FROM ParametrosAdministrativo" & vbCrLf
   vResult = vResult & "                  SELECT TOP 1"
   vResult = vResult & "                  @NombreMoneda = Aux1SettValue.Value,"
   vResult = vResult & "                  @CodMoneda = Aux2SettValue.Value"
   vResult = vResult & "                  FROM Comun.SettValueByCompany Stt"
   vResult = vResult & "                  CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'NombreMonedaExtranjera') AS Aux1SettValue(Value)"
   vResult = vResult & "                  CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'CodigoMonedaExtranjera') AS Aux2SettValue(Value)"
   vResult = vResult & "                  GROUP BY Aux1SettValue.Value,Aux2SettValue.Value"
   vResult = vResult & "            ELSE" & vbCrLf
   'vResult = vResult & "               SELECT TOP 1 @NombreMoneda = NombreMonedaLocal, @CodMoneda = CodigoMonedaLocal FROM ParametrosAdministrativo" & vbCrLf
   vResult = vResult & "                  SELECT TOP 1"
   vResult = vResult & "                  @NombreMoneda = Aux1SettValue.Value,"
   vResult = vResult & "                  @CodMoneda = Aux2SettValue.Value"
   vResult = vResult & "                  FROM Comun.SettValueByCompany Stt"
   vResult = vResult & "                  CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'NombreMonedaExtranjera') AS Aux1SettValue(Value)"
   vResult = vResult & "                  CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = 'CodigoMonedaExtranjera') AS Aux2SettValue(Value)"
   vResult = vResult & "                  GROUP BY Aux1SettValue.Value,Aux2SettValue.Value"
   vResult = vResult & "         IF EXISTS(SELECT Numero FROM CxP WHERE  ConsecutivoCompania = @P_ConsecutivoCompania AND Numero = @P_Numero AND TipoDeCxp = @P_TipoDeCxP AND CodigoProveedor = @P_CodigoProveedor)" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @ConsecutivoCxP = -1" & vbCrLf
   vResult = vResult & "            RAISERROR('Ya existe un registro para los datos suministrados para Cuenta por Pagar.',16,1)" & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            IF NOT EXISTS(SELECT Consecutivo FROM Rendicion WHERE  ConsecutivoCompania = @P_ConsecutivoCompania AND Consecutivo = @P_ConsecutivoRendicion)" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SET @ConsecutivoCxP = -1" & vbCrLf
   vResult = vResult & "               RAISERROR('No existe un registro con este Consecutivo de Rendición.',16,1)" & vbCrLf
   vResult = vResult & "            END " & vbCrLf
   vResult = vResult & "            ELSE" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SELECT @NextConsecutivoCxP = MAX(ConsecutivoCxP) FROM CxP WHERE ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "               IF @NextConsecutivoCxP IS NULL" & vbCrLf
   vResult = vResult & "                  SET @NextConsecutivoCxP = 1" & vbCrLf
   vResult = vResult & "               ELSE" & vbCrLf
   vResult = vResult & "               BEGIN" & vbCrLf
   vResult = vResult & "                  IF @NextConsecutivoCxP > 0" & vbCrLf
   vResult = vResult & "                     SET @NextConsecutivoCxP = @NextConsecutivoCxP + 1" & vbCrLf
   vResult = vResult & "                  ELSE" & vbCrLf
   vResult = vResult & "                     SET @NextConsecutivoCxP = 1" & vbCrLf
   vResult = vResult & "               END " & vbCrLf
   vResult = vResult & "               EXEC Gp_CxPINS" & vbCrLf
   vResult = vResult & "                  @ConsecutivoCompania = @P_ConsecutivoCompania," & vbCrLf
   vResult = vResult & "                  @ConsecutivoCxP = @NextConsecutivoCxP," & vbCrLf
   vResult = vResult & "                  @Numero = @P_Numero," & vbCrLf
   vResult = vResult & "                  @TipoDeCxP = @P_TipoDeCxP," & vbCrLf
   vResult = vResult & "                  @Status = '0'," & vbCrLf
   vResult = vResult & "                  @CodigoProveedor = @P_CodigoProveedor," & vbCrLf
   vResult = vResult & "                  @Fecha = @P_Fecha," & vbCrLf
   vResult = vResult & "                  @FechaCancelacion = @Hoy," & vbCrLf
   vResult = vResult & "                  @FechaVencimiento = @FechaVenc," & vbCrLf
   vResult = vResult & "                  @FechaAnulacion = @Hoy," & vbCrLf
   vResult = vResult & "                  @Moneda = @NombreMoneda," & vbCrLf
   vResult = vResult & "                  @CambioABolivares = @P_CambioAMonedaLocal," & vbCrLf
   vResult = vResult & "                  @AplicaParaLibrodeCompras = 'S'," & vbCrLf
   vResult = vResult & "                  @MontoExento = @P_MontoExento," & vbCrLf
   vResult = vResult & "                  @MontoGravado = @MtoGravado," & vbCrLf
   vResult = vResult & "                  @MontoIva = @MtoIva," & vbCrLf
   vResult = vResult & "                  @MontoAbonado = 0," & vbCrLf
   vResult = vResult & "                  @MesDeAplicacion = @Mes," & vbCrLf
   vResult = vResult & "                  @AnoDeAplicacion = @Ano," & vbCrLf
   vResult = vResult & "                  @Observaciones = @P_Observaciones," & vbCrLf
   vResult = vResult & "                  @CreditoFiscal = '0'," & vbCrLf
   vResult = vResult & "                  @TipoDeCompra = '2'," & vbCrLf
   vResult = vResult & "                  @SeHizoLaRetencion = 'N'," & vbCrLf
   vResult = vResult & "                  @MontoGravableAlicuotaGeneral = @P_MontoGravableAlicuotaGeneral," & vbCrLf
   vResult = vResult & "                  @MontoGravableAlicuota2 = @P_MontoGravableAlicuota2," & vbCrLf
   vResult = vResult & "                  @MontoGravableAlicuota3 = @P_MontoGravableAlicuota3," & vbCrLf
   vResult = vResult & "                  @MontoIVAAlicuotaGeneral = @P_MontoIVAAlicuotaGeneral," & vbCrLf
   vResult = vResult & "                  @MontoIVAAlicuota2 = @P_MontoIVAAlicuota2," & vbCrLf
   vResult = vResult & "                  @MontoIVAAlicuota3 = @P_MontoIVAAlicuota3," & vbCrLf
   vResult = vResult & "                  @NumeroPlanillaDeImportacion = ''," & vbCrLf
   vResult = vResult & "                  @NumeroExpedienteDeImportacion = ''," & vbCrLf
   vResult = vResult & "                  @TipoDeTransaccion = '0'," & vbCrLf
   vResult = vResult & "                  @NumeroDeFacturaAfectada = @P_NumeroDeFacturaAfectada," & vbCrLf
   vResult = vResult & "                  @NumeroControl = @P_NumeroControl," & vbCrLf
   vResult = vResult & "                  @SeHizoLaRetencionIVA = 'N'," & vbCrLf
   vResult = vResult & "                  @NumeroComprobanteRetencion = ''," & vbCrLf
   vResult = vResult & "                  @FechaAplicacionRetIVA = @Hoy," & vbCrLf
   vResult = vResult & "                  @PorcentajeRetencionAplicado = 0," & vbCrLf
   vResult = vResult & "                  @MontoRetenido = 0," & vbCrLf
   vResult = vResult & "                  @OrigenDeLaRetencion = '0'," & vbCrLf
   vResult = vResult & "                  @RetencionAplicadaEnPago = 'N'," & vbCrLf
   vResult = vResult & "                  @OrigenInformacionRetencion = '0'," & vbCrLf
   vResult = vResult & "                  @CxPgeneradaPor = '0'," & vbCrLf
   vResult = vResult & "                  @EsCxPhistorica = 'N'," & vbCrLf
   vResult = vResult & "                  @NumDiasDeVencimiento = @DiasVencimiento," & vbCrLf
   vResult = vResult & "                  @NumeroDocOrigen = ''," & vbCrLf
   vResult = vResult & "                  @CodigoLote = ''," & vbCrLf
   vResult = vResult & "                  @GenerarAsientoDeRetiroEnCuenta = 'N'," & vbCrLf
   vResult = vResult & "                  @TotalOtrosImpuestos = @P_TotalOtrosImpuestos," & vbCrLf
   vResult = vResult & "                  @SeContabilRetIva = 'N'," & vbCrLf
   vResult = vResult & "                  @DondeContabilRetIva = '0'," & vbCrLf
   vResult = vResult & "                  @CodigoMoneda = @CodMoneda," & vbCrLf
   vResult = vResult & "                  @EstaAsociadoARendicion = 'S'," & vbCrLf
   vResult = vResult & "                  @ConsecutivoRendicion = @P_ConsecutivoRendicion," & vbCrLf
   vResult = vResult & "                  @CodigoTipoDeDocumentoLey = '01'," & vbCrLf
   vResult = vResult & "                  @AplicaDetraccion = 'N'," & vbCrLf
   vResult = vResult & "                  @NumeroDetraccion = ''," & vbCrLf
   vResult = vResult & "                  @CodigoDetraccion = ''," & vbCrLf
   vResult = vResult & "                  @DescripcionDeDetraccion = ''," & vbCrLf
   vResult = vResult & "                  @PorcentajeDetraccion = 0," & vbCrLf
   vResult = vResult & "                  @TotalDetraccion = 0," & vbCrLf
   vResult = vResult & "                  @StatusDetraccion = '2'," & vbCrLf
   vResult = vResult & "                  @ConsecutivoMovimiento = 0," & vbCrLf
   vResult = vResult & "                  @NombreOperador = @Operador" & vbCrLf
   vResult = vResult & "               SELECT @ConsecutivoCxP = ConsecutivoCxP" & vbCrLf
   vResult = vResult & "                  FROM cxP" & vbCrLf
   vResult = vResult & "                  WHERE ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                     AND Numero = @P_Numero" & vbCrLf
   vResult = vResult & "                     AND TipoDeCxp = @P_TipoDeCxP" & vbCrLf
   vResult = vResult & "                     AND CodigoProveedor = @P_CodigoProveedor" & vbCrLf
   vResult = vResult & "            END " & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpS10RendicionInsert = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10RendicionInsert", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function



'****************************************Caso Generar Pago desde Cxp ISLR*******************************************


Private Function fCreateSpGenerarNumero(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameGenerarNumero(), fSpParametersGenerarNumero(), fStatementsSpGenerarNumero(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpGenerarNumero = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpGenerarNumero", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpGenerarNumero(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameGenerarNumero(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpGenerarNumero = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpGenerarNumero", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameGenerarNumero() As String
   GetSpNameGenerarNumero = "CxPGenerarNumero"
End Function

Private Function fSpParametersGenerarNumero() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = ""
   vResult = vResult & "   @ConsecutivoCompania int, "
   vResult = vResult & "   @Numero int OUTPUT"
   fSpParametersGenerarNumero = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersGenerarNumero", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpGenerarNumero() As String
   Dim vResult As String
   Dim vSql As String
   On Error GoTo h_ERROR
   
   vSql = ""
   vSql = "SET @Numero= (SELECT MAX(NumeroComprobante) AS maximo FROM " & vbCrLf
   vSql = vSql & "Pago WHERE ConsecutivoCompania  = @ConsecutivoCompania)" & vbCrLf
   vSql = vSql & "IF @Numero > 0" & vbCrLf
   vSql = vSql & "BEGIN" & vbCrLf
   vSql = vSql & "    SET @Numero = @Numero + 1" & vbCrLf
   vSql = vSql & "END" & vbCrLf
   vSql = vSql & "ELSE" & vbCrLf
   vSql = vSql & "BEGIN" & vbCrLf
   vSql = vSql & "   SET @Numero = 1" & vbCrLf
   vSql = vSql & "END" & vbCrLf
   
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   BEGIN TRAN" & vbCrLf
   vResult = vResult & "    " & vSql & vbCrLf
   vResult = vResult & "   IF @@ERROR = 0" & vbCrLf
   vResult = vResult & "      COMMIT TRAN" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      ROLLBACK" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpGenerarNumero = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpGenerarNumero", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateSpGenerarNumeroCheque(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameGenerarNumeroCheque(), fSpParametersGenerarNumeroCheque(), fStatementsSpGenerarNumeroCheque(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpGenerarNumeroCheque = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpGenerarNumeroCheque", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpGenerarNumeroCheque(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameGenerarNumeroCheque(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpGenerarNumeroCheque = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpGenerarNumeroCheque", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameGenerarNumeroCheque() As String
   GetSpNameGenerarNumeroCheque = "CxPGenerarNumeroCheque"
End Function

Private Function fSpParametersGenerarNumeroCheque() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = ""
   vResult = vResult & "   @ConsecutivoCompania int, "
   vResult = vResult & "   @NumeroCheque varchar(20) OUTPUT"
   fSpParametersGenerarNumeroCheque = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersGenerarNumeroCheque", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpGenerarNumeroCheque() As String
   Dim vResult As String
   Dim vSql As String
   On Error GoTo h_ERROR
   
   vSql = ""
   vSql = vSql & " DECLARE @MaximoNumero int" & vbCrLf
   vSql = vSql & "" & vbCrLf
   vSql = vSql & "SET @NumeroCheque = (SELECT MAX(SUBSTRING(NumeroCheque,7, 9)) AS maximo FROM Pago" & vbCrLf
   vSql = vSql & "  WHERE ConsecutivoCompania  = @ConsecutivoCompania" & vbCrLf
   vSql = vSql & "  AND NumeroCheque LIKE 'RET%')" & vbCrLf
   vSql = vSql & "SET @MaximoNumero = CAST(@NumeroCheque as INT)" & vbCrLf
   vSql = vSql & "IF @MaximoNumero > 0" & vbCrLf
   vSql = vSql & "BEGIN" & vbCrLf
   vSql = vSql & "      SET @MaximoNumero = @MaximoNumero + 1" & vbCrLf
   vSql = vSql & "END" & vbCrLf
   vSql = vSql & "ELSE" & vbCrLf
   vSql = vSql & "BEGIN" & vbCrLf
   vSql = vSql & "   SET @MaximoNumero = 1" & vbCrLf
   vSql = vSql & "END" & vbCrLf
   vSql = vSql & "SET @NumeroCheque =  CAST(@MaximoNumero AS varchar(20))" & vbCrLf
   vSql = vSql & "SET @NumeroCheque = 'RET-' + substring('00000000' + @NumeroCheque,LEN(@NumeroCheque),9)" & vbCrLf
   
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   BEGIN TRAN" & vbCrLf
   vResult = vResult & "      " & vSql & vbCrLf
   vResult = vResult & "   IF @@ERROR = 0" & vbCrLf
   vResult = vResult & "      COMMIT TRAN" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      ROLLBACK" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpGenerarNumeroCheque = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpGenerarNumeroCheque", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateSpInsertarDocumentoPagado(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameInsertarDocumentoPagado(), fSpParametersInsertarDocumentoPagado(), fStatementsSpInsertarDocumentoPagado(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpInsertarDocumentoPagado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpInsertarDocumentoPagado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpInsertarDocumentoPagado(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameInsertarDocumentoPagado(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpInsertarDocumentoPagado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpInsertarDocumentoPagado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameInsertarDocumentoPagado() As String
   GetSpNameInsertarDocumentoPagado = "CxPInsertarDocumentoPagado"
End Function

Private Function fSpParametersInsertarDocumentoPagado() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = ""
   vResult = vResult & "   @ConsecutivoCompania int, "
   vResult = vResult & "   @NumeroComprobante int, "
   vResult = vResult & "   @ConsecutivoCxP int," & vbCrLf
   vResult = vResult & "   @Secuencial int, "
   vResult = vResult & "   @NumeroDelDocumentoPagado varchar(25), "
   vResult = vResult & "   @MontoAbonado decimal(25,5), "
   vResult = vResult & "   @RetenerIva char(1), "
   vResult = vResult & "   @MontoIvaRetenido decimal(25,5), "
   vResult = vResult & "   @CodigoMonedaDeCxP varchar(4), "
   vResult = vResult & "   @SimboloMonedaDeCxP varchar(4), "
   vResult = vResult & "   @CambioAMonedaDelPago money, "
   vResult = vResult & "   @MontoEnMonedaOriginalDeCxP money, "
   vResult = vResult & "   @MontoRestanteAlDiaDelPago money, "
   vResult = vResult & "   @MontoTotalDeCxP money, "
   vResult = vResult & "   @SeHizoLaRetencionIva char(1)"
   fSpParametersInsertarDocumentoPagado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersInsertarDocumentoPagado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpInsertarDocumentoPagado() As String
   Dim vResult As String
   Dim vSql As String
   On Error GoTo h_ERROR
   
   vSql = ""
   vSql = " SET NOCOUNT ON;" & vbCrLf
   vSql = vSql & " INSERT INTO documentoPagado" & vbCrLf
   vSql = vSql & "         (ConsecutivoCompania," & vbCrLf
   vSql = vSql & "         NumeroComprobante," & vbCrLf
   vSql = vSql & "         ConsecutivoCxP," & vbCrLf
   vSql = vSql & "         Secuencial," & vbCrLf
   vSql = vSql & "         NumeroDelDocumentoPagado," & vbCrLf
   vSql = vSql & "         MontoAbonado," & vbCrLf
   vSql = vSql & "         RetenerIva," & vbCrLf
   vSql = vSql & "         MontoIvaRetenido," & vbCrLf
   vSql = vSql & "         CodigoMonedaDeCxP," & vbCrLf
   vSql = vSql & "         SimboloMonedaDeCxP," & vbCrLf
   vSql = vSql & "         CambioAMonedaDelPago," & vbCrLf
   vSql = vSql & "         MontoEnMonedaOriginalDeCxP," & vbCrLf
   vSql = vSql & "         MontoRestanteAlDiaDelPago," & vbCrLf
   vSql = vSql & "         MontoTotalDeCxP," & vbCrLf
   vSql = vSql & "         SeHizoLaRetencionIva)" & vbCrLf
   vSql = vSql & "   Values " & vbCrLf
   vSql = vSql & "         (@ConsecutivoCompania," & vbCrLf
   vSql = vSql & "         @NumeroComprobante," & vbCrLf
   vSql = vSql & "         @ConsecutivoCxP," & vbCrLf
   vSql = vSql & "         @Secuencial," & vbCrLf
   vSql = vSql & "         @NumeroDelDocumentoPagado," & vbCrLf
   vSql = vSql & "         @MontoAbonado," & vbCrLf
   vSql = vSql & "         @RetenerIva," & vbCrLf
   vSql = vSql & "         @MontoIvaRetenido," & vbCrLf
   vSql = vSql & "         @CodigoMonedaDeCxP," & vbCrLf
   vSql = vSql & "         @SimboloMonedaDeCxP," & vbCrLf
   vSql = vSql & "         @CambioAMonedaDelPago," & vbCrLf
   vSql = vSql & "         @MontoEnMonedaOriginalDeCxP," & vbCrLf
   vSql = vSql & "         @MontoRestanteAlDiaDelPago," & vbCrLf
   vSql = vSql & "         @MontoTotalDeCxP," & vbCrLf
   vSql = vSql & "         @SeHizoLaRetencionIva)" & vbCrLf

   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   BEGIN TRAN" & vbCrLf
   vResult = vResult & "   " & vSql & vbCrLf
   vResult = vResult & "   IF @@ERROR = 0" & vbCrLf
   vResult = vResult & "      COMMIT TRAN" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      ROLLBACK" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpInsertarDocumentoPagado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpInsertarDocumentoPagado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateSpInsertarPagodesdeCxP(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameInsertarPagodesdeCxP(), fSpParametersInsertarPagodesdeCxP(), fStatementsSpInsertarPagodesdeCxP(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpInsertarPagodesdeCxP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpInsertarPagodesdeCxP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpInsertarPagodesdeCxP(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameInsertarPagodesdeCxP(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpInsertarPagodesdeCxP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpInsertarPagodesdeCxP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameInsertarPagodesdeCxP() As String
   GetSpNameInsertarPagodesdeCxP = "CxPInsertarPagodesdeCxP"
End Function

Private Function fSpParametersInsertarPagodesdeCxP() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = ""
   vResult = vResult & "   @ConsecutivoCompania int, "
   vResult = vResult & "   @NumeroComprobante int, "
   vResult = vResult & "   @StatusOrdenDePago char(1), "
   vResult = vResult & "   @Fecha smalldatetime, "
   vResult = vResult & "   @FechaAnulacion smalldatetime, "
   vResult = vResult & "   @CodigoProveedor varchar(10), "
   vResult = vResult & "   @TotalDocumentos decimal(25,5), "
   vResult = vResult & "   @TotalRetenido decimal(25,5), "
   vResult = vResult & "   @TotalOtros decimal(25,5), "
   vResult = vResult & "   @MontoCheque decimal(25,5), "
   vResult = vResult & "   @FormaDePago char(1), "
   vResult = vResult & "   @CodigoCuentaBancaria varchar(5), "
   vResult = vResult & "   @CodigoConcepto varchar(8), "
   vResult = vResult & "   @AplicaDebitoBancario char(1), "
   vResult = vResult & "   @DescripcionPago varchar(7000), "
   vResult = vResult & "   @Moneda varchar(80), "
   vResult = vResult & "   @CambioaBolivares decimal(25,5), "
   vResult = vResult & "   @SimboloMoneda varchar(4), "
   vResult = vResult & "   @TotalRetenidoIva decimal(25,5), "
   vResult = vResult & "   @EsPagoHistorico char(1), "
   vResult = vResult & "   @PagadoAnticipo decimal(25,5), "
   vResult = vResult & "   @Beneficiario varchar(60), "
   vResult = vResult & "   @CodigoMoneda varchar(4), "
   vResult = vResult & "   @NombreOperador varchar(10), "
   vResult = vResult & "   @FechaUltimaModificacion smalldatetime, "
   vResult = vResult & "   @ConsecutivoCxP int, "
   vResult = vResult & "   @NumeroDeCxP varchar(25),"
   vResult = vResult & "   @Origen char(1),"
   vResult = vResult & "   @NumeroLote int, "
   vResult = vResult & "   @TotalCxP decimal(25,5), "
   vResult = vResult & "   @NumeroCheque   varchar(25), "
   vResult = vResult & "   @TotalRetenidoImpuestoMunicipal decimal(25,5)"
   
   fSpParametersInsertarPagodesdeCxP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersInsertarPagodesdeCxP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpInsertarPagodesdeCxP() As String
   Dim vResult As String
   Dim vSql As String
   
   On Error GoTo h_ERROR
   vSql = "" & vbCrLf
  
   vSql = vSql & " DECLARE    @MontoAbonado           money" & vbCrLf
   vSql = vSql & " DECLARE    @MontoTotalMonedaLocal  money" & vbCrLf
   vSql = vSql & "" & vbCrLf
   vSql = vSql & "SET NOCOUNT ON;" & vbCrLf
   vSql = vSql & "" & vbCrLf
   vSql = vSql & " INSERT INTO Pago" & vbCrLf
   vSql = vSql & "         (ConsecutivoCompania" & vbCrLf
   vSql = vSql & "         , NumeroComprobante" & vbCrLf
   vSql = vSql & "         , NumeroCheque" & vbCrLf
   vSql = vSql & "         , StatusOrdenDePago" & vbCrLf
   vSql = vSql & "         , Fecha" & vbCrLf
   vSql = vSql & "         , FechaAnulacion" & vbCrLf
   vSql = vSql & "         , CodigoProveedor" & vbCrLf
   vSql = vSql & "         , TotalDocumentos" & vbCrLf
   vSql = vSql & "         , TotalRetenido" & vbCrLf
   vSql = vSql & "         , TotalOtros" & vbCrLf
   vSql = vSql & "         , MontoCheque" & vbCrLf
   vSql = vSql & "         , FormaDePago" & vbCrLf
   vSql = vSql & "         , CodigoCuentaBancaria" & vbCrLf
   vSql = vSql & "         , CodigoConcepto" & vbCrLf
   vSql = vSql & "         , AplicaDebitoBancario" & vbCrLf
   vSql = vSql & "         , DescripcionPago" & vbCrLf
   vSql = vSql & "         , Moneda" & vbCrLf
   vSql = vSql & "         , CambioaBolivares" & vbCrLf
   vSql = vSql & "         , TotalRetenidoIva" & vbCrLf
   vSql = vSql & "         , EsPagoHistorico" & vbCrLf
   vSql = vSql & "         , PagadoAnticipo" & vbCrLf
   vSql = vSql & "         , Beneficiario" & vbCrLf
   vSql = vSql & "         , CodigoMoneda" & vbCrLf
   vSql = vSql & "         , Origen" & vbCrLf
   vSql = vSql & "         , NumeroLote" & vbCrLf
   vSql = vSql & "         , TotalRetenidoImpuestoMunicipal  " & vbCrLf
   vSql = vSql & "         , NombreOperador" & vbCrLf
   vSql = vSql & "         , FechaUltimaModificacion )" & vbCrLf
   vSql = vSql & "   Values" & vbCrLf
   vSql = vSql & "         (@ConsecutivoCompania," & vbCrLf
   vSql = vSql & "         @NumeroComprobante," & vbCrLf
   vSql = vSql & "         @NumeroCheque," & vbCrLf
   vSql = vSql & "         @StatusOrdenDePago," & vbCrLf
   vSql = vSql & "         @Fecha," & vbCrLf
   vSql = vSql & "         @FechaAnulacion," & vbCrLf
   vSql = vSql & "         @CodigoProveedor," & vbCrLf
   vSql = vSql & "         @TotalDocumentos," & vbCrLf
   vSql = vSql & "         @TotalRetenido," & vbCrLf
   vSql = vSql & "         @TotalOtros," & vbCrLf
   vSql = vSql & "         @MontoCheque," & vbCrLf
   vSql = vSql & "         @FormaDePago," & vbCrLf
   vSql = vSql & "         @CodigoCuentaBancaria," & vbCrLf
   vSql = vSql & "         @CodigoConcepto," & vbCrLf
   vSql = vSql & "         @AplicaDebitoBancario," & vbCrLf
   vSql = vSql & "         @DescripcionPago," & vbCrLf
   vSql = vSql & "         @Moneda," & vbCrLf
   vSql = vSql & "         @CambioaBolivares," & vbCrLf
   vSql = vSql & "         @TotalRetenidoIva," & vbCrLf
   vSql = vSql & "         @EsPagoHistorico," & vbCrLf
   vSql = vSql & "         @PagadoAnticipo," & vbCrLf
   vSql = vSql & "         @Beneficiario," & vbCrLf
   vSql = vSql & "         @CodigoMoneda," & vbCrLf
   vSql = vSql & "         @Origen," & vbCrLf
   vSql = vSql & "         @NumeroLote," & vbCrLf
   vSql = vSql & "         @TotalRetenidoImpuestoMunicipal," & vbCrLf
   vSql = vSql & "         @NombreOperador," & vbCrLf
   vSql = vSql & "         @FechaUltimaModificacion)" & vbCrLf
   vSql = vSql & " " & vbCrLf
   vSql = vSql & " SET @MontoAbonado = ((@TotalRetenido + @TotalRetenidoIva + @TotalRetenidoImpuestoMunicipal + @TotalOtros) )" & vbCrLf
   vSql = vSql & " SET @MontoTotalMonedaLocal = (@TotalCxP * @CambioaBolivares)" & vbCrLf
   vSql = vSql & " EXEC Gp_CxPInsertarDocumentoPagado  @ConsecutivoCompania," & vbCrLf
   vSql = vSql & "                        @NumeroComprobante," & vbCrLf
   vSql = vSql & "                        @ConsecutivoCxP," & vbCrLf
   vSql = vSql & "                        1," & vbCrLf
   vSql = vSql & "                        @NumeroDeCxP," & vbCrLf
   vSql = vSql & "                        @MontoAbonado," & vbCrLf
   vSql = vSql & "                        'S'," & vbCrLf
   vSql = vSql & "                        @TotalRetenidoIva," & vbCrLf
   vSql = vSql & "                        @CodigoMoneda," & vbCrLf
   vSql = vSql & "                        @SimboloMoneda," & vbCrLf
   vSql = vSql & "                        @CambioaBolivares," & vbCrLf
   vSql = vSql & "                        @TotalCxP," & vbCrLf
   vSql = vSql & "                        @TotalCxP," & vbCrLf
   vSql = vSql & "                        @MontoTotalMonedaLocal," & vbCrLf
   vSql = vSql & "                        'S'" & vbCrLf
   vSql = vSql & " " & vbCrLf
   vSql = vSql & " " & vbCrLf
   vSql = vSql & "    UPDATE CxP " & vbCrLf
   vSql = vSql & "      SET CxP.montoAbonado  = @TotalDocumentos " & vbCrLf
   vSql = vSql & "            WHERE CxP.ConsecutivoCxP = @ConsecutivoCxP " & vbCrLf
   vSql = vSql & "            AND CxP.ConsecutivoCompania = @ConsecutivoCompania " & vbCrLf
   vSql = vSql & " " & vbCrLf
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   BEGIN TRAN" & vbCrLf
   vResult = vResult & "   " & vSql & vbCrLf
   vResult = vResult & "   IF @@ERROR = 0" & vbCrLf
   vResult = vResult & "      COMMIT TRAN" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      ROLLBACK" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpInsertarPagodesdeCxP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpInsertarPagodesdeCxP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateSpCxPCorregirStatusYMontoAbonado(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameCxPCorregirStatusYMontoAbonado(), fSpParametersCxPCorregirStatusYMontoAbonado(), fStatementsSpCxPCorregirStatusYMontoAbonado(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpCxPCorregirStatusYMontoAbonado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpCxPCorregirStatusYMontoAbonado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fDropSpCxPCorregirStatusYMontoAbonado(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameCxPCorregirStatusYMontoAbonado(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpCxPCorregirStatusYMontoAbonado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpCxPCorregirStatusYMontoAbonado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function GetSpNameCxPCorregirStatusYMontoAbonado() As String
   GetSpNameCxPCorregirStatusYMontoAbonado = "CxPCorregirStatusYMontoAbonado"
End Function
Private Function fSpParametersCxPCorregirStatusYMontoAbonado() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "   @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "   @ConsecutivoCxP " & gUtilSQL.getNumericTypeForDB(9, 0) & " = ''," & vbCrLf
   vResult = vResult & "   @TipoDeDocumentoPagado" & gUtilSQL.getCharTypeForDB(1) & " = ''," & vbCrLf
   vResult = vResult & "   @CodigoProveedor" & gUtilSQL.getCharTypeForDB(10) & " = ''" & vbCrLf
   fSpParametersCxPCorregirStatusYMontoAbonado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersCxPCorregirStatusYMontoAbonado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fStatementsSpCxPCorregirStatusYMontoAbonado() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @StatusOrdenPago AS CHAR(1)" & vbCrLf
   vResult = vResult & "   DECLARE @MontoAbonadoTotal AS MONEY" & vbCrLf
   vResult = vResult & "   DECLARE @MontoTotalCxP AS MONEY" & vbCrLf
   vResult = vResult & "   DECLARE @Aplicar AS VARCHAR(2)" & vbCrLf
   vResult = vResult & "   DECLARE @StatusCxP as VARCHAR(1)" & vbCrLf
   vResult = vResult & "   SET @Aplicar = 'SI'" & vbCrLf
   vResult = vResult & "   SET @StatusOrdenPago = '0'" & vbCrLf
   vResult = vResult & "   SET NOCOUNT OFF;" & vbCrLf
   'vResult = vResult & "   IF EXISTS( SELECT Numero FROM CxP WHERE Numero = @NumeroCxP AND ConsecutivoCompania = @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   IF EXISTS( SELECT Numero FROM CxP WHERE ConsecutivoCxP = @ConsecutivoCxP AND ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor) " & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "     SELECT  @MontoAbonadoTotal =   CASE WHEN SUM(dbo.DocumentoPagado.MontoAbonado) IS NULL THEN 0 ELSE SUM(dbo.DocumentoPagado.MontoAbonado) END" & vbCrLf
   vResult = vResult & "      FROM    dbo.pago INNER JOIN dbo.DocumentoPagado" & vbCrLf
   vResult = vResult & "            ON dbo.pago.ConsecutivoCompania = dbo.DocumentoPagado.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "               dbo.pago.NumeroComprobante = dbo.DocumentoPagado.NumeroComprobante" & vbCrLf
   'vResult = vResult & "      WHERE dbo.DocumentoPagado.NumeroDelDocumentoPagado = @NumeroCxP" & vbCrLf
   vResult = vResult & "      WHERE dbo.DocumentoPagado.ConsecutivoCxP = @ConsecutivoCxP" & vbCrLf
   vResult = vResult & "      AND Pago.StatusOrdenDePago = @StatusOrdenPago AND pago.ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "      AND Pago.CodigoProveedor = @CodigoProveedor " & vbCrLf
   
   vResult = vResult & "      SELECT @MontoTotalCxP = (MontoIva + MontoGravado + MontoExento)" & vbCrLf
   vResult = vResult & "      , @StatusCxP = Status"
   vResult = vResult & "      FROM CxP" & vbCrLf
   vResult = vResult & "      WHERE ConsecutivoCxP = @ConsecutivoCxP AND CxP.ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "      AND CxP.TipoDeCxP  = @TipoDeDocumentoPagado" & vbCrLf
   vResult = vResult & "      AND CxP.CodigoProveedor  = @CodigoProveedor" & vbCrLf
   vResult = vResult & "      IF @Aplicar = 'SI'" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         IF (ABS(ABS(@MontoAbonadoTotal) - ABS(@MontoTotalCxP)) <= 0.01 AND ABS(ABS(@MontoAbonadoTotal) - ABS(@MontoTotalCxP))> 0 )" & vbCrLf
   vResult = vResult & "            SET @MontoAbonadoTotal = @MontoTotalCxP" & vbCrLf
   vResult = vResult & "         UPDATE CxP SET MontoAbonado = @MontoAbonadoTotal WHERE" & vbCrLf
   vResult = vResult & "         ConsecutivoCxP = @ConsecutivoCxP AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "         AND CxP.TipoDeCxP  = @TipoDeDocumentoPagado" & vbCrLf
   vResult = vResult & "         AND CxP.CodigoProveedor  = @CodigoProveedor" & vbCrLf
   vResult = vResult & "         IF (@StatusCxP <> '4') "
   vResult = vResult & "         BEGIN   "
   vResult = vResult & "            IF (@MontoAbonadoTotal = @MontoTotalCxP)" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               UPDATE CxP SET Status = '1' WHERE" & vbCrLf
   vResult = vResult & "               ConsecutivoCxP = @ConsecutivoCxP AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "               AND CxP.TipoDeCxP  = @TipoDeDocumentoPagado" & vbCrLf
   vResult = vResult & "               AND CxP.CodigoProveedor  = @CodigoProveedor" & vbCrLf
   vResult = vResult & "            END "
   vResult = vResult & "            ELSE" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               IF (@MontoAbonadoTotal = 0 )" & vbCrLf
   vResult = vResult & "                  UPDATE CxP SET Status = '0' WHERE" & vbCrLf
   vResult = vResult & "                  ConsecutivoCxP = @ConsecutivoCxP AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                  AND CxP.TipoDeCxP  = @TipoDeDocumentoPagado" & vbCrLf
   vResult = vResult & "                  AND CxP.CodigoProveedor  = @CodigoProveedor" & vbCrLf
   vResult = vResult & "               ELSE" & vbCrLf
   vResult = vResult & "                  UPDATE CxP SET Status = '3' WHERE" & vbCrLf
   vResult = vResult & "                  ConsecutivoCxP = @ConsecutivoCxP AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                  AND CxP.TipoDeCxP  = @TipoDeDocumentoPagado" & vbCrLf
   vResult = vResult & "                  AND CxP.CodigoProveedor  = @CodigoProveedor" & vbCrLf
   vResult = vResult & "            END "
   vResult = vResult & "         END  "
   vResult = vResult & "      END "
   vResult = vResult & "   END "
   vResult = vResult & " END "
   fStatementsSpCxPCorregirStatusYMontoAbonado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpCxPCorregirStatusYMontoAbonado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

