VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsComprobanteSp"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsComprobanteSp"
End Function

Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsComprobanteSp"
End Function

Public Function fCreateDropAsientoSP(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      vResult = vResult And fCreateSpINS(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpUPD(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpDEL(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpUpdTotal(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpAnular(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnGeneraNumero(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpAsientoComprobanteIMP(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10ComprobanteContableSEL(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10DEL(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10PeriodoSel(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10AsientoComprobanteIMP(valShowMessage, valWithEncryption)
   Else
      vResult = vResult And fDropSpS10ComprobanteContableSEL(valShowMessage)
      vResult = vResult And fDropSpAsientoComprobanteIMP(valShowMessage)
      vResult = vResult And fDropFnGeneraNumero(valShowMessage)
      vResult = vResult And fDropSpAnular(valShowMessage)
      vResult = vResult And fDropSpUpdTotal(valShowMessage)
      vResult = vResult And fDropSpDEL(valShowMessage)
      vResult = vResult And fDropSpUPD(valShowMessage)
      vResult = vResult And fDropSpINS(valShowMessage)
      vResult = vResult And fDropSpS10DEL(valShowMessage)
      vResult = vResult And fDropSpS10PeriodoSel(valShowMessage)
      vResult = vResult And fDropSpS10AsientoComprobanteIMP(valShowMessage)
   End If
   fCreateDropAsientoSP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropAsientoSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetSpNameINS = "ComprobanteINS"
End Function

Private Function fSpParametersINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & "         @ConsecutivoDocOrigen " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & "         @Numero " & gUtilSQL.getVarCharTypeForDB(8) & ", " & vbCrLf
   vResult = vResult & "         @Status " & gUtilSQL.getCharTypeForDB(1) & " = '0', " & vbCrLf
   vResult = vResult & "         @Fecha " & gUtilSQL.getDateTypeForDB() & " = '', " & vbCrLf
   vResult = vResult & "         @Descripcion " & gUtilSQL.getVarCharTypeForDB(160) & " = '', " & vbCrLf
   vResult = vResult & "         @TotalDebe " & gUtilSQL.getNumericTypeForDB(12, 2) & " = 0, " & vbCrLf
   vResult = vResult & "         @TotalHaber " & gUtilSQL.getNumericTypeForDB(12, 2) & " = 0, " & vbCrLf
   vResult = vResult & "         @TipoAsientoModificable " & gUtilSQL.getCharTypeForDB(1) & " = 'N', " & vbCrLf
   vResult = vResult & "         @GeneradoPor " & gUtilSQL.getCharTypeForDB(1) & ", " & vbCrLf
   vResult = vResult & "         @NumeroDeLote " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0, " & vbCrLf
   vResult = vResult & "         @NoDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & " = '', " & vbCrLf
   vResult = vResult & "         @FueModificado " & gUtilSQL.getCharTypeForDB(1) & " = 'N', " & vbCrLf
   vResult = vResult & "         @VieneDeConversionInfo2000 " & gUtilSQL.getCharTypeForDB(1) & " = 'N', " & vbCrLf
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
   vResult = vResult & "   SET @FechaUltimaModificacion = " & gUtilSQL.DfCDateSQL(gUtilSQL.GetSQLFechaActual()) & vbCrLf
   vResult = vResult & "   INSERT INTO COMPROBANTE" & vbCrLf
   vResult = vResult & "            (ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "            ,ConsecutivoDocOrigen" & vbCrLf
   vResult = vResult & "            ,Numero" & vbCrLf
   vResult = vResult & "            ,Status" & vbCrLf
   vResult = vResult & "            ,Fecha" & vbCrLf
   vResult = vResult & "            ,Descripcion" & vbCrLf
   vResult = vResult & "            ,TotalDebe" & vbCrLf
   vResult = vResult & "            ,TotalHaber" & vbCrLf
   vResult = vResult & "            ,TipoAsientoModificable" & vbCrLf
   vResult = vResult & "            ,GeneradoPor" & vbCrLf
   vResult = vResult & "            ,NumeroDeLote" & vbCrLf
   vResult = vResult & "            ,NoDocumentoOrigen" & vbCrLf
   vResult = vResult & "            ,FueModificado" & vbCrLf
   vResult = vResult & "            ,VieneDeConversionInfo2000" & vbCrLf
   vResult = vResult & "            ,NombreOperador" & vbCrLf
   vResult = vResult & "            ,FechaUltimaModificacion)" & vbCrLf
   vResult = vResult & "       VALUES" & vbCrLf
   vResult = vResult & "            (@ConsecutivoPeriodo, " & vbCrLf
   vResult = vResult & "            @ConsecutivoDocOrigen, " & vbCrLf
   vResult = vResult & "            @Numero, " & vbCrLf
   vResult = vResult & "            @Status, " & vbCrLf
   vResult = vResult & "            @Fecha, " & vbCrLf
   vResult = vResult & "            @Descripcion, " & vbCrLf
   vResult = vResult & "            @TotalDebe, " & vbCrLf
   vResult = vResult & "            @TotalHaber, " & vbCrLf
   vResult = vResult & "            @TipoAsientoModificable, " & vbCrLf
   vResult = vResult & "            @GeneradoPor, " & vbCrLf
   vResult = vResult & "            @NumeroDeLote, " & vbCrLf
   vResult = vResult & "            @NoDocumentoOrigen, " & vbCrLf
   vResult = vResult & "            @FueModificado, " & vbCrLf
   vResult = vResult & "            @VieneDeConversionInfo2000, " & vbCrLf
   vResult = vResult & "            @NombreOperador, " & vbCrLf
   vResult = vResult & "            @FechaUltimaModificacion)" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpAsientoComprobanteIMP(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameComprobanteIMP(), fSpParametersComprobanteIMP(), fStatementsSpComprobanteIMP(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpAsientoComprobanteIMP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpAsientoComprobanteIMP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpAsientoComprobanteIMP(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameComprobanteIMP(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpAsientoComprobanteIMP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpAsientoComprobanteIMP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameComprobanteIMP() As String
   GetSpNameComprobanteIMP = "AsientoComprobanteIMP"
End Function

Private Function fSpParametersComprobanteIMP() As String
   Dim vResult As String
   On Error GoTo h_ERROR

   vResult = "         @NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "         @P_ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @P_ConsecutivoDocOrigen " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @P_NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & "," & vbCrLf
   vResult = vResult & "         @P_FechaComprobante " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "         @P_DescripcionComprobante " & gUtilSQL.getVarCharTypeForDB(160) & "," & vbCrLf
   vResult = vResult & "         @P_CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "         @P_DescripcionCuenta " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "         @P_DebeHaber char(1)," & vbCrLf
   vResult = vResult & "         @P_Monto money," & vbCrLf
   vResult = vResult & "         @P_FechaDeReferencia " & gUtilSQL.getDateTypeForDB() & " = ''," & vbCrLf
   vResult = vResult & "         @P_NumeroReferencia " & gUtilSQL.getVarCharTypeForDB(16) & " = ''," & vbCrLf
   vResult = vResult & "         @P_DescripcionAsiento " & gUtilSQL.getVarCharTypeForDB(160) & " = ''," & vbCrLf
   vResult = vResult & "         @P_GrupoAuxiliar char(1) = ''," & vbCrLf
   vResult = vResult & "         @P_Auxiliar " & gUtilSQL.getVarCharTypeForDB(10) & " = ''," & vbCrLf
   vResult = vResult & "         @P_NombreAuxiliar " & gUtilSQL.getVarCharTypeForDB(35) & " = ''," & vbCrLf
   vResult = vResult & "         @P_CentroDeCostos " & gUtilSQL.getVarCharTypeForDB(5) & " = ''," & vbCrLf
   vResult = vResult & "         @P_DescripcionCentroDeCostos " & gUtilSQL.getVarCharTypeForDB(40) & " = ''," & vbCrLf
   vResult = vResult & "         @P_SeccionEstadoDemostrativo " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 3," & vbCrLf
   vResult = vResult & "         @P_IngresosPropiosActividad " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 2," & vbCrLf
   vResult = vResult & "         @P_CostoDeVentas " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 10," & vbCrLf
   vResult = vResult & "         @P_Gastos " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 7," & vbCrLf
   vResult = vResult & "         @P_GeneradoPorInt " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 6," & vbCrLf
   vResult = vResult & "         @P_NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "         @P_TipoDeComprobante " & gUtilSQL.getVarCharTypeForDB(2) & " = '91'," & vbCrLf
   vResult = vResult & "         @P_NumeroComprobanteGenerado " & gUtilSQL.getVarCharTypeForDB(8) & " = '' OUTPUT" & vbCrLf
    fSpParametersComprobanteIMP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersComprobanteIMP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpComprobanteIMP() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @FechaInicioPeriodo " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @FechaFinPeriodo " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @CuentaCierreDelEjercicio " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CompaniaUsaAuxiliaresSN char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @CompaniaUsaCentroDeCostosSN char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @K " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CharAct char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NuevoComprobante bit" & vbCrLf
   vResult = vResult & "   DECLARE @Status char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @TipoAsientoModificable char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPor char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @NoDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CuentaValida bit" & vbCrLf
   vResult = vResult & "   DECLARE @EsActivoFijoSN char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @MontoDebe money" & vbCrLf
   vResult = vResult & "   DECLARE @MontoHaber money" & vbCrLf
   vResult = vResult & "   DECLARE @DescripcionAsiento " & gUtilSQL.getVarCharTypeForDB(160) & "" & vbCrLf
   vResult = vResult & "   DECLARE @TieneAuxiliar bit" & vbCrLf
   vResult = vResult & "   DECLARE @GrupoAuxiliarCtaChar char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @GrupoAuxiliarCtaEnum " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @GrupoAuxiliarInputEnum " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @GrupoAuxiliarChar char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @GrupoAuxiliarEnum " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Auxiliar " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NombreAuxiliar " & gUtilSQL.getVarCharTypeForDB(35) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CentroDeCostos " & gUtilSQL.getVarCharTypeForDB(5) & "" & vbCrLf
   vResult = vResult & "   DECLARE @DescripcionCentroDeCostos " & gUtilSQL.getVarCharTypeForDB(40) & "" & vbCrLf
   vResult = vResult & "   DECLARE @SeccionEstadoDemostrativoChar char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @IngresosPropiosActividadChar char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @CostoDeVentasChar char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @GastosChar char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @RC " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Continue bit" & vbCrLf
   vResult = vResult & "   DECLARE @HayError bit" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoAsiento " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT ConsecutivoPeriodo FROM PERIODO WHERE ConsecutivoPeriodo = @P_ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @HayError = 0 --False" & vbCrLf
   vResult = vResult & "      --Variables" & vbCrLf
   vResult = vResult & "      SELECT @ConsecutivoCompania = ConsecutivoCompania," & vbCrLf
   vResult = vResult & "            @FechaInicioPeriodo = FechaAperturaDelPeriodo," & vbCrLf
   vResult = vResult & "            @FechaFinPeriodo = FechaCierreDelPeriodo," & vbCrLf
   vResult = vResult & "            @CuentaCierreDelEjercicio = CierreDelEjercicio" & vbCrLf
   vResult = vResult & "         FROM Periodo WHERE ConsecutivoPeriodo = @P_ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "      SELECT @CompaniaUsaAuxiliaresSN = UsaAuxiliares," & vbCrLf
   vResult = vResult & "            @CompaniaUsaCentroDeCostosSN = UsaCentroDeCostos" & vbCrLf
   vResult = vResult & "         FROM COMPANIA WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "      --Limpia número de comprobante" & vbCrLf
   vResult = vResult & "      SET @NumeroComprobante = @P_NumeroComprobante" & vbCrLf
   vResult = vResult & "      SET @K = 1" & vbCrLf
   vResult = vResult & "      WHILE @K <= LEN(@NumeroComprobante)" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SET @CharAct = SUBSTRING(@NumeroComprobante, @K, 1)" & vbCrLf
   vResult = vResult & "         IF NOT ((CHARINDEX(UPPER(@CharAct), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ') > 0) OR (CHARINDEX(@CharAct, '0123456789') > 0))" & vbCrLf
   vResult = vResult & "            SET @NumeroComprobante = REPLACE(@NumeroComprobante, @CharAct, '')" & vbCrLf
   vResult = vResult & "         SET @K = @K + 1" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "      IF (LEN(@NumeroComprobante) < 8)" & vbCrLf
   vResult = vResult & "         SET @NumeroComprobante = RIGHT('00000000' + @NumeroComprobante, 8)" & vbCrLf
   vResult = vResult & "      IF (@NumeroComprobante = '00000000')" & vbCrLf
   vResult = vResult & "         SET @NuevoComprobante = 1 --True" & vbCrLf
   vResult = vResult & "      ELSE IF EXISTS(SELECT Numero FROM COMPROBANTE WHERE ConsecutivoPeriodo = @P_ConsecutivoPeriodo AND Numero = @NumeroComprobante)" & vbCrLf
   vResult = vResult & "         SET @NuevoComprobante = 0 --False" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "         SET @NuevoComprobante = 1 --True" & vbCrLf
   vResult = vResult & "      IF ((@P_FechaComprobante < @FechaInicioPeriodo) OR (@P_FechaComprobante > @FechaFinPeriodo))" & vbCrLf
   vResult = vResult & "         RAISERROR ('Fecha Inválida.', 16, 1)" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SET @Status = dbo.Gf_EnumToDBValue(2) --Descuadrado" & vbCrLf
   vResult = vResult & "         SET @TipoAsientoModificable = dbo.Gf_EnumToDBValue(2) --Todos" & vbCrLf
   vResult = vResult & "         SET @GeneradoPor = dbo.Gf_EnumToDBValue(@P_GeneradoPorInt) --Por defecto 6 = Importado" & vbCrLf
   vResult = vResult & "         SET @NoDocumentoOrigen = ''" & vbCrLf
   vResult = vResult & "         IF @P_GeneradoPorInt = 5 OR @P_GeneradoPorInt = 9 OR @P_GeneradoPorInt = 11  --Administrativo" & vbCrLf
   vResult = vResult & "            SET @NoDocumentoOrigen = @P_NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "         SET @CodigoCuenta = REPLACE(@P_CodigoCuenta, '.', '')" & vbCrLf
   vResult = vResult & "         SET @CodigoCuenta = REPLACE(@CodigoCuenta, '-', '')" & vbCrLf
   vResult = vResult & "         SET @CodigoCuenta = dbo.Gf_AdjustCode(@ConsecutivoCompania, @P_ConsecutivoPeriodo, @CodigoCuenta)" & vbCrLf
   vResult = vResult & "         SET @CuentaValida = dbo.Gf_CuentaIsValidForImport(@ConsecutivoCompania, @P_ConsecutivoPeriodo, @CodigoCuenta)" & vbCrLf
   vResult = vResult & "         IF (@CuentaValida = 0)  --False" & vbCrLf
   vResult = vResult & "            RAISERROR ('La Cuenta Contable:%s no es válida.', 16, 1, @P_CodigoCuenta)" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SELECT @EsActivoFijoSN = EsActivoFijo FROM CUENTA WHERE ConsecutivoPeriodo = @P_ConsecutivoPeriodo AND Codigo = @CodigoCuenta" & vbCrLf
   vResult = vResult & "            IF (UPPER(@EsActivoFijoSN) = 'S')" & vbCrLf
   vResult = vResult & "               RAISERROR ('La Cuenta Contable:%s no es válida, es de Activo Fijo.', 16, 1, @P_CodigoCuenta)" & vbCrLf
   vResult = vResult & "            ELSE" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               IF @CodigoCuenta = @CuentaCierreDelEjercicio" & vbCrLf
   vResult = vResult & "                  RAISERROR ('La Cuenta Contable:%s no es válida, es de Resultado de Cierre del Ejercicio', 16, 1, @P_CodigoCuenta)" & vbCrLf
   vResult = vResult & "               ELSE" & vbCrLf
   vResult = vResult & "               BEGIN" & vbCrLf
   vResult = vResult & "                  --Validación de conciliación cuando aplique, si aplica" & vbCrLf
   vResult = vResult & "                  SET @MontoDebe = 0" & vbCrLf
   vResult = vResult & "                  SET @MontoHaber = 0" & vbCrLf
   vResult = vResult & "                  IF (UPPER(@P_DebeHaber) = 'D')" & vbCrLf
   vResult = vResult & "                     IF (@P_Monto >= 0)" & vbCrLf
   vResult = vResult & "                        SET @MontoDebe = @P_Monto" & vbCrLf
   vResult = vResult & "                     ELSE" & vbCrLf
   vResult = vResult & "                        SET @MontoHaber = ABS(@P_Monto)" & vbCrLf
   vResult = vResult & "                  ELSE IF (UPPER(@P_DebeHaber) = 'H')" & vbCrLf
   vResult = vResult & "                     IF (@P_Monto < 0)" & vbCrLf
   vResult = vResult & "                        SET @MontoDebe = ABS(@P_Monto)" & vbCrLf
   vResult = vResult & "                     ELSE" & vbCrLf
   vResult = vResult & "                        SET @MontoHaber = @P_Monto" & vbCrLf
   vResult = vResult & "                  ELSE" & vbCrLf
   vResult = vResult & "                  BEGIN" & vbCrLf
   vResult = vResult & "                     SET @HayError = 1 --True" & vbCrLf
   vResult = vResult & "                     RAISERROR ('Tipo Debe/Haber:%s no válido, signo indefinido.', 16, 1, @P_DebeHaber)" & vbCrLf
   vResult = vResult & "                  END " & vbCrLf
   vResult = vResult & "                  IF (@HayError = 0) --False" & vbCrLf
   vResult = vResult & "                  BEGIN" & vbCrLf
   vResult = vResult & "                     IF @P_DescripcionAsiento = ''" & vbCrLf
   vResult = vResult & "                        SET @DescripcionAsiento = @P_DescripcionComprobante" & vbCrLf
   vResult = vResult & "                     ELSE" & vbCrLf
   vResult = vResult & "                        SET @DescripcionAsiento = @P_DescripcionAsiento" & vbCrLf
   vResult = vResult & "                     SET @TieneAuxiliar = 0 --False" & vbCrLf
   vResult = vResult & "                     SET @GrupoAuxiliarEnum = 3 --SINGRUPO" & vbCrLf
   vResult = vResult & "                     SET @GrupoAuxiliarChar = dbo.Gf_DBValueToEnum(@GrupoAuxiliarEnum) --SINGRUPO" & vbCrLf
   vResult = vResult & "                     SET @Auxiliar = ''" & vbCrLf
   vResult = vResult & "                     SET @NombreAuxiliar = ''" & vbCrLf
   vResult = vResult & "                     IF (UPPER(@CompaniaUsaAuxiliaresSN) = 'S')" & vbCrLf
   vResult = vResult & "                     BEGIN" & vbCrLf
   vResult = vResult & "                        SELECT @GrupoAuxiliarCtaChar = GrupoAuxiliar FROM Cuenta WHERE Codigo = @CodigoCuenta AND ConsecutivoPeriodo = @P_ConsecutivoPeriodo AND TieneAuxiliar = 'S'" & vbCrLf
   vResult = vResult & "                        IF ((@P_GrupoAuxiliar <> '') AND NOT(@GrupoAuxiliarCtaChar IS NULL))" & vbCrLf
   vResult = vResult & "                        BEGIN" & vbCrLf
   vResult = vResult & "                           SET @GrupoAuxiliarCtaEnum = dbo.Gf_DBValueToEnum(@GrupoAuxiliarCtaChar)" & vbCrLf
   vResult = vResult & "                           SET @GrupoAuxiliarInputEnum = dbo.Gf_DBValueToEnum(@P_GrupoAuxiliar)" & vbCrLf
   vResult = vResult & "                           IF (@GrupoAuxiliarCtaEnum = @GrupoAuxiliarInputEnum)" & vbCrLf
   vResult = vResult & "                           BEGIN" & vbCrLf
   vResult = vResult & "                              SET @GrupoAuxiliarEnum = @GrupoAuxiliarInputEnum" & vbCrLf
   vResult = vResult & "                              SET @GrupoAuxiliarChar = @P_GrupoAuxiliar" & vbCrLf
   vResult = vResult & "                              IF @GrupoAuxiliarInputEnum <> 3 --SINGRUPO" & vbCrLf
   vResult = vResult & "                                 SET @TieneAuxiliar = 1 --True" & vbCrLf
   vResult = vResult & "                           END " & vbCrLf
   vResult = vResult & "                           ELSE" & vbCrLf
   vResult = vResult & "                           BEGIN" & vbCrLf
   vResult = vResult & "                              SET @HayError = 1 --True" & vbCrLf
   vResult = vResult & "                              RAISERROR ('El Grupo Auxiliar no corresponde:%s', 16, 1, @P_GrupoAuxiliar)" & vbCrLf
   vResult = vResult & "                            END " & vbCrLf
   vResult = vResult & "                        END " & vbCrLf
   vResult = vResult & "                        ELSE" & vbCrLf
   vResult = vResult & "                        BEGIN" & vbCrLf
   vResult = vResult & "                           SET @GrupoAuxiliarEnum = 3 --SINGRUPO" & vbCrLf
   vResult = vResult & "                           SET @GrupoAuxiliarChar = dbo.Gf_DBValueToEnum(@GrupoAuxiliarEnum) --SINGRUPO" & vbCrLf
   vResult = vResult & "                           SET @TieneAuxiliar = 0 --False" & vbCrLf
   vResult = vResult & "                        END " & vbCrLf
   vResult = vResult & "                        IF (@HayError = 0) --False" & vbCrLf
   vResult = vResult & "                        BEGIN" & vbCrLf
   vResult = vResult & "                           IF ((@GrupoAuxiliarEnum <> 3) AND (@TieneAuxiliar = 1))" & vbCrLf
   vResult = vResult & "                              SET @Auxiliar = @P_Auxiliar" & vbCrLf
   vResult = vResult & "                           ELSE" & vbCrLf
   vResult = vResult & "                              SET @Auxiliar = ''" & vbCrLf
   vResult = vResult & "                           IF ((@GrupoAuxiliarEnum <> 3) AND (@GrupoAuxiliarCtaEnum = @GrupoAuxiliarInputEnum))" & vbCrLf
   vResult = vResult & "                              IF (@Auxiliar = '')" & vbCrLf
   vResult = vResult & "                              BEGIN" & vbCrLf
   vResult = vResult & "                                 SET @HayError = 1 --True" & vbCrLf
   vResult = vResult & "                                 RAISERROR ('Código Auxiliar No Existe:%s', 16, 1, @P_Auxiliar)" & vbCrLf
   vResult = vResult & "                              END " & vbCrLf
   vResult = vResult & "                              ELSE" & vbCrLf
   vResult = vResult & "                              BEGIN" & vbCrLf
   vResult = vResult & "                                 IF NOT EXISTS(SELECT Nombre FROM auxiliar WHERE ConsecutivoPeriodo = @P_ConsecutivoPeriodo AND TipoDeAuxiliar = @P_GrupoAuxiliar AND Codigo = @P_Auxiliar)" & vbCrLf
   vResult = vResult & "                                 BEGIN" & vbCrLf
   vResult = vResult & "                                    EXEC Gp_AuxiliarINS @P_ConsecutivoPeriodo, @P_GrupoAuxiliar, @P_Auxiliar, 'IMPORTADO', '', '', @NombreOperador" & vbCrLf
   vResult = vResult & "                                    IF NOT EXISTS(SELECT Nombre FROM auxiliar WHERE ConsecutivoPeriodo = @P_ConsecutivoPeriodo AND TipoDeAuxiliar = @P_GrupoAuxiliar AND Codigo = @P_Auxiliar)" & vbCrLf
   vResult = vResult & "                                    BEGIN" & vbCrLf
   vResult = vResult & "                                       SET @HayError = 1 --True" & vbCrLf
   vResult = vResult & "                                       RAISERROR ('Código Auxiliar No Existe:%s', 16, 1, @P_Auxiliar)" & vbCrLf
   vResult = vResult & "                                    END " & vbCrLf
   vResult = vResult & "                                 END " & vbCrLf
   vResult = vResult & "                           END " & vbCrLf
   vResult = vResult & "                           ELSE IF (@GrupoAuxiliarEnum = 3)" & vbCrLf
   vResult = vResult & "                           BEGIN" & vbCrLf
   vResult = vResult & "                              IF ((@Auxiliar <> '') AND (@TieneAuxiliar = 1))" & vbCrLf
   vResult = vResult & "                              BEGIN" & vbCrLf
   vResult = vResult & "                                 SET @HayError = 1 --True" & vbCrLf
   vResult = vResult & "                                 RAISERROR ('Cuenta sin Código Auxiliar', 16, 1)" & vbCrLf
   vResult = vResult & "                           END " & vbCrLf
   vResult = vResult & "                              SET @Auxiliar = ''" & vbCrLf
   vResult = vResult & "                              SET @NombreAuxiliar = ''" & vbCrLf
   vResult = vResult & "                        END " & vbCrLf
   vResult = vResult & "                     END " & vbCrLf
   vResult = vResult & "                  END " & vbCrLf
   vResult = vResult & "                     IF (@HayError = 0) --False" & vbCrLf
   vResult = vResult & "                     BEGIN" & vbCrLf
   vResult = vResult & "                        SET @CentroDeCostos = ''" & vbCrLf
   vResult = vResult & "                        SET @DescripcionCentroDeCostos = ''" & vbCrLf
   vResult = vResult & "                        IF (UPPER(@CompaniaUsaCentroDeCostosSN) = 'S')" & vbCrLf
   vResult = vResult & "                        BEGIN" & vbCrLf
   vResult = vResult & "                           SET @CentroDeCostos = @P_CentroDeCostos" & vbCrLf
   vResult = vResult & "                           SET @DescripcionCentroDeCostos = @P_DescripcionCentroDeCostos" & vbCrLf
   vResult = vResult & "                           IF @CentroDeCostos = ''" & vbCrLf
   vResult = vResult & "                           BEGIN" & vbCrLf
   vResult = vResult & "                              IF @DescripcionCentroDeCostos <> ''" & vbCrLf
   vResult = vResult & "                              BEGIN" & vbCrLf
   vResult = vResult & "                                 SET @HayError = 1 --True" & vbCrLf
   vResult = vResult & "                                 RAISERROR ('Falta Código de Centro de Costos:%s', 16, 1, @P_DescripcionCentroDeCostos)" & vbCrLf
   vResult = vResult & "                              END " & vbCrLf
   vResult = vResult & "                           END " & vbCrLf
   vResult = vResult & "                           ELSE" & vbCrLf
   vResult = vResult & "                           BEGIN" & vbCrLf
   vResult = vResult & "                              IF NOT EXISTS(SELECT Codigo FROM CentroDeCostos WHERE ConsecutivoPeriodo = @P_ConsecutivoPeriodo AND Codigo = @CentroDeCostos)" & vbCrLf
   vResult = vResult & "                              BEGIN" & vbCrLf
   vResult = vResult & "                                 EXEC Gp_CentroDeCostosINS" & vbCrLf
   vResult = vResult & "                                 IF NOT EXISTS(SELECT Codigo FROM CentroDeCostos WHERE ConsecutivoPeriodo = @P_ConsecutivoPeriodo AND Codigo = @CentroDeCostos)" & vbCrLf
   vResult = vResult & "                                 BEGIN" & vbCrLf
   vResult = vResult & "                                    SET @HayError = 1 --True" & vbCrLf
   vResult = vResult & "                                    RAISERROR ('Centro de Costos no existe:%s', 16, 1, @P_CentroDeCostos)" & vbCrLf
   vResult = vResult & "                                 END " & vbCrLf
   vResult = vResult & "                              END " & vbCrLf
   vResult = vResult & "                           END " & vbCrLf
   vResult = vResult & "                        END " & vbCrLf
   vResult = vResult & "                        IF (@HayError = 0) --False" & vbCrLf
   vResult = vResult & "                        BEGIN" & vbCrLf
   vResult = vResult & "                           SET @SeccionEstadoDemostrativoChar = dbo.Gf_EnumToDBValue(@P_SeccionEstadoDemostrativo)" & vbCrLf
   vResult = vResult & "                           SET @IngresosPropiosActividadChar = dbo.Gf_EnumToDBValue(@P_IngresosPropiosActividad)" & vbCrLf
   vResult = vResult & "                           SET @CostoDeVentasChar = dbo.Gf_EnumToDBValue(@P_CostoDeVentas)" & vbCrLf
   vResult = vResult & "                           SET @GastosChar = dbo.Gf_EnumToDBValue(@P_Gastos)" & vbCrLf
   vResult = vResult & "                           SET @Continue = 0 --False" & vbCrLf
   vResult = vResult & "                           IF EXISTS(SELECT Codigo FROM CUENTA WHERE ConsecutivoPeriodo = @P_ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "                              SET @Continue = 1 --True" & vbCrLf
   vResult = vResult & "                           ELSE" & vbCrLf
   vResult = vResult & "                           BEGIN" & vbCrLf
   vResult = vResult & "                              EXEC @RC = Gp_CuentaIMP  @ConsecutivoCompania, @P_ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "                                    @CodigoCuenta, @P_DescripcionCuenta, @P_DebeHaber, 'N', 0, 'N', 'N'," & vbCrLf
   vResult = vResult & "                                    @TieneAuxiliar, @GrupoAuxiliarCtaEnum, 'N', 0, '', @P_SeccionEstadoDemostrativo," & vbCrLf
   vResult = vResult & "                                    @P_IngresosPropiosActividad, @P_CostoDeVentas, @P_Gastos, 0, 0," & vbCrLf
   vResult = vResult & "                                    '0', '0', '6', '6', 0, 0, '0', '0', '', ''," & vbCrLf
   vResult = vResult & "                                    @NombreOperador" & vbCrLf
   vResult = vResult & "                              IF @RC >= 0" & vbCrLf
   vResult = vResult & "                                 SET @Continue = 1 --True" & vbCrLf
   vResult = vResult & "                           END " & vbCrLf
   vResult = vResult & "                           IF (@Continue = 1) --True" & vbCrLf
   vResult = vResult & "                           BEGIN" & vbCrLf
   vResult = vResult & "                              IF (@NuevoComprobante = 1) -- True" & vbCrLf
   vResult = vResult & "                              BEGIN" & vbCrLf
   vResult = vResult & "                                 SET @NumeroComprobante = dbo.Gf_ComprobanteGenNumero(@P_ConsecutivoPeriodo, @ConsecutivoCompania, @P_FechaComprobante, @P_TipoDeComprobante)" & vbCrLf
   vResult = vResult & "                                 IF @P_TipoDeComprobante = '91'" & vbCrLf
   vResult = vResult & "                                    SET @NoDocumentoOrigen = @NumeroComprobante" & vbCrLf
   vResult = vResult & "                                 EXEC Gp_ComprobanteINS @P_ConsecutivoPeriodo, @P_ConsecutivoDocOrigen, @NumeroComprobante, @Status," & vbCrLf
   vResult = vResult & "                                       @P_FechaComprobante, @P_DescripcionComprobante, 0, 0, @TipoAsientoModificable," & vbCrLf
   vResult = vResult & "                                       @GeneradoPor, 0, @NoDocumentoOrigen, 'N', 'N', @NombreOperador" & vbCrLf
   vResult = vResult & "                                 SET @P_NumeroComprobanteGenerado = @NumeroComprobante" & vbCrLf
   vResult = vResult & "                              END " & vbCrLf
   vResult = vResult & "                              ELSE" & vbCrLf
   vResult = vResult & "                                 SET @P_NumeroComprobanteGenerado = ''" & vbCrLf
   vResult = vResult & "                              SET @ConsecutivoAsiento = dbo.Gf_AsientoGenConsecutivo(@P_ConsecutivoPeriodo, @NumeroComprobante)" & vbCrLf
   vResult = vResult & "                              EXEC Gp_AsientoINS @P_ConsecutivoPeriodo, @ConsecutivoAsiento, @NumeroComprobante," & vbCrLf
   vResult = vResult & "                                    @P_FechaComprobante, @CodigoCuenta, @MontoDebe, @MontoHaber, @DescripcionAsiento," & vbCrLf
   vResult = vResult & "                                    @P_FechaDeReferencia, @P_NumeroReferencia, @CentroDeCostos, @Auxiliar, 'N', '', '', 'N'," & vbCrLf
   vResult = vResult & "                                    'N', 'N', '0'" & vbCrLf
   vResult = vResult & "                              EXEC Gp_ComprobanteUpdTotal @P_ConsecutivoPeriodo, @NumeroComprobante, @NombreOperador" & vbCrLf
   vResult = vResult & "                           END " & vbCrLf
   vResult = vResult & "                           ELSE" & vbCrLf
   vResult = vResult & "                              RAISERROR('Cuenta no válida:%s', 16, 1, @CodigoCuenta)" & vbCrLf
   vResult = vResult & "                        END " & vbCrLf
   vResult = vResult & "                     END " & vbCrLf
   vResult = vResult & "                  END " & vbCrLf
   vResult = vResult & "               END " & vbCrLf
   vResult = vResult & "            END " & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      RAISERROR ('No se encuentra Consecutivo Período:%d', 16, 1, @P_ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpComprobanteIMP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpComprobanteIMP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetSpNameUPD = "ComprobanteUPD"
End Function

Private Function fSpParametersUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & "         @Numero " & gUtilSQL.getVarCharTypeForDB(8) & ", " & vbCrLf
   vResult = vResult & "         @Status " & gUtilSQL.getCharTypeForDB(1) & " = NULL, " & vbCrLf
   vResult = vResult & "         @Fecha " & gUtilSQL.getDateTypeForDB() & " = NULL, " & vbCrLf
   vResult = vResult & "         @Descripcion " & gUtilSQL.getVarCharTypeForDB(160) & " = NULL, " & vbCrLf
   vResult = vResult & "         @TotalDebe " & gUtilSQL.getNumericTypeForDB(12, 2) & " = NULL, " & vbCrLf
   vResult = vResult & "         @TotalHaber " & gUtilSQL.getNumericTypeForDB(12, 2) & " = NULL, " & vbCrLf
   vResult = vResult & "         @TipoAsientoModificable " & gUtilSQL.getCharTypeForDB(1) & " = NULL, " & vbCrLf
   vResult = vResult & "         @GeneradoPor " & gUtilSQL.getCharTypeForDB(1) & " = NULL, " & vbCrLf
   vResult = vResult & "         @NumeroDeLote " & gUtilSQL.getNumericTypeForDB(9, 0) & " = NULL, " & vbCrLf
   vResult = vResult & "         @NoDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & " = NULL, " & vbCrLf
   vResult = vResult & "         @FueModificado " & gUtilSQL.getCharTypeForDB(1) & " = NULL, " & vbCrLf
   vResult = vResult & "         @VieneDeConversionInfo2000 " & gUtilSQL.getCharTypeForDB(1) & " = NULL, " & vbCrLf
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
   vResult = vResult & "   DECLARE @Ori_Fecha " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Descripcion " & gUtilSQL.getVarCharTypeForDB(160) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TotalDebe " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TotalHaber " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TipoAsientoModificable " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_GeneradoPor " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroDeLote " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NoDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_FueModificado " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_VieneDeConversionInfo2000 " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TimeStamp timestamp" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Status " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_Fecha " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Up_Descripcion " & gUtilSQL.getVarCharTypeForDB(160) & vbCrLf
   vResult = vResult & "   DECLARE @Up_TotalDebe " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_TotalHaber " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_TipoAsientoModificable " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_GeneradoPor " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroDeLote " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NoDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & vbCrLf
   vResult = vResult & "   DECLARE @Up_FueModificado " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_VieneDeConversionInfo2000 " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   BEGIN TRAN" & vbCrLf
   vResult = vResult & "   SELECT" & vbCrLf
   vResult = vResult & "      @Ori_Status = Status, " & vbCrLf
   vResult = vResult & "      @Ori_Fecha = Fecha, " & vbCrLf
   vResult = vResult & "      @Ori_Descripcion = Descripcion, " & vbCrLf
   vResult = vResult & "      @Ori_TotalDebe = TotalDebe, " & vbCrLf
   vResult = vResult & "      @Ori_TotalHaber = TotalHaber, " & vbCrLf
   vResult = vResult & "      @Ori_TipoAsientoModificable = TipoAsientoModificable, " & vbCrLf
   vResult = vResult & "      @Ori_GeneradoPor = GeneradoPor, " & vbCrLf
   vResult = vResult & "      @Ori_NumeroDeLote = NumeroDeLote, " & vbCrLf
   vResult = vResult & "      @Ori_NoDocumentoOrigen = NoDocumentoOrigen, " & vbCrLf
   vResult = vResult & "      @Ori_FueModificado = FueModificado, " & vbCrLf
   vResult = vResult & "      @Ori_VieneDeConversionInfo2000 = VieneDeConversionInfo2000, " & vbCrLf
   vResult = vResult & "      @Ori_NombreOperador = NombreOperador, " & vbCrLf
   vResult = vResult & "      @Ori_TimeStamp = fldTimeStamp" & vbCrLf
   vResult = vResult & "   FROM COMPROBANTE" & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "      AND Numero = @Numero" & vbCrLf
   vResult = vResult & "   IF @Status IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Status = @Ori_Status" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Status = @Status" & vbCrLf
   vResult = vResult & "   IF @Fecha IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Fecha = @Ori_Fecha" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Fecha = @Fecha" & vbCrLf
   vResult = vResult & "   IF @Descripcion IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Descripcion = @Ori_Descripcion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Descripcion = @Descripcion" & vbCrLf
   vResult = vResult & "   IF @TotalDebe IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_TotalDebe = @Ori_TotalDebe" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TotalDebe = @TotalDebe" & vbCrLf
   vResult = vResult & "   IF @TotalHaber IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_TotalHaber = @Ori_TotalHaber" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TotalHaber = @TotalHaber" & vbCrLf
   vResult = vResult & "   IF @TipoAsientoModificable IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_TipoAsientoModificable = @Ori_TipoAsientoModificable" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TipoAsientoModificable = @TipoAsientoModificable" & vbCrLf
   vResult = vResult & "   IF @GeneradoPor IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_GeneradoPor = @Ori_GeneradoPor" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_GeneradoPor = @GeneradoPor" & vbCrLf
   vResult = vResult & "   IF @NumeroDeLote IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroDeLote = @Ori_NumeroDeLote" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroDeLote = @NumeroDeLote" & vbCrLf
   vResult = vResult & "   IF @NoDocumentoOrigen IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_NoDocumentoOrigen = @Ori_NoDocumentoOrigen" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NoDocumentoOrigen = @NoDocumentoOrigen" & vbCrLf
   vResult = vResult & "   IF @FueModificado IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_FueModificado = @Ori_FueModificado" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_FueModificado = @FueModificado" & vbCrLf
   vResult = vResult & "   IF @VieneDeConversionInfo2000 IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_VieneDeConversionInfo2000 = @Ori_VieneDeConversionInfo2000" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_VieneDeConversionInfo2000 = @VieneDeConversionInfo2000" & vbCrLf
   vResult = vResult & "   IF @NombreOperador IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_NombreOperador = @Ori_NombreOperador" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NombreOperador = @NombreOperador" & vbCrLf
   vResult = vResult & "   SET @FechaUltimaModificacion = CONVERT(" & gUtilSQL.getDateTypeForDB() & ", GETDATE(), 103)" & vbCrLf
   vResult = vResult & "   UPDATE COMPROBANTE" & vbCrLf
   vResult = vResult & "      SET" & vbCrLf
   vResult = vResult & "         Status = @Up_Status, " & vbCrLf
   vResult = vResult & "         Fecha = @Up_Fecha, " & vbCrLf
   vResult = vResult & "         Descripcion = @Up_Descripcion, " & vbCrLf
   vResult = vResult & "         TotalDebe = @Up_TotalDebe, " & vbCrLf
   vResult = vResult & "         TotalHaber = @Up_TotalHaber, " & vbCrLf
   vResult = vResult & "         TipoAsientoModificable = @Up_TipoAsientoModificable, " & vbCrLf
   vResult = vResult & "         GeneradoPor = @Up_GeneradoPor, " & vbCrLf
   vResult = vResult & "         NumeroDeLote = @Up_NumeroDeLote, " & vbCrLf
   vResult = vResult & "         NoDocumentoOrigen = @Up_NoDocumentoOrigen, " & vbCrLf
   vResult = vResult & "         FueModificado = @Up_FueModificado, " & vbCrLf
   vResult = vResult & "         VieneDeConversionInfo2000 = @Up_VieneDeConversionInfo2000, " & vbCrLf
   vResult = vResult & "         NombreOperador = @Up_NombreOperador, " & vbCrLf
   vResult = vResult & "         FechaUltimaModificacion = @FechaUltimaModificacion" & vbCrLf
   vResult = vResult & "      WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "         AND Numero = @Numero" & vbCrLf
   vResult = vResult & "         AND fldTimeStamp = @Ori_TimeStamp" & vbCrLf
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
Private Function fCreateSpUpdTotal(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameUpdTotal(), fSpParametersUpdTotal(), fStatementsSpUpdTotal(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpUpdTotal = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpUpdTotal", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpUpdTotal(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameUpdTotal(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpUpdTotal = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpUpdTotal", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameUpdTotal() As String
   GetSpNameUpdTotal = "ComprobanteUpdTotal"
End Function

Private Function fSpParametersUpdTotal() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @P_ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & "         @P_NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & ", " & vbCrLf
   vResult = vResult & "         @P_NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
    fSpParametersUpdTotal = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersUpdTotal", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpUpdTotal() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @SumMontoDebe " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @SumMontoHaber " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @StatusComprobante " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Contabilizado " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   SET @StatusComprobante = null " & vbCrLf
   vResult = vResult & "   SET @Contabilizado = 'N' " & vbCrLf
   vResult = vResult & "   SELECT @SumMontoDebe = SUM(MontoDebe), " & vbCrLf
   vResult = vResult & "         @SumMontoHaber = SUM(MontoHaber)" & vbCrLf
   vResult = vResult & "      FROM ASIENTO" & vbCrLf
   vResult = vResult & "      WHERE ConsecutivoPeriodo = @P_ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "         AND NumeroComprobante = @P_NumeroComprobante" & vbCrLf
   vResult = vResult & "   IF (cast((@SumMontoDebe - @SumMontoHaber) as decimal) = cast(0 as decimal) )" & vbCrLf
   vResult = vResult & "   BEGIN " & vbCrLf
   vResult = vResult & "      SET @StatusComprobante = '0'  -- CONTABILIZADO" & vbCrLf
   vResult = vResult & "      SET @Contabilizado = 'S'  -- CONTABILIZADO" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   Else " & vbCrLf
   vResult = vResult & "      SET @StatusComprobante = '2'  -- DESCUADRADO" & vbCrLf
   vResult = vResult & "   EXEC Gp_AsientoCuadrarUPD" & vbCrLf
   vResult = vResult & "         @ConsecutivoPeriodo = @P_ConsecutivoPeriodo, " & vbCrLf
   vResult = vResult & "         @NumeroComprobante = @P_NumeroComprobante, " & vbCrLf
   vResult = vResult & "         @Contabilizado = @Contabilizado " & vbCrLf
   vResult = vResult & "   EXEC Gp_ComprobanteUPD" & vbCrLf
   vResult = vResult & "         @ConsecutivoPeriodo = @P_ConsecutivoPeriodo, " & vbCrLf
   vResult = vResult & "         @Status = @StatusComprobante, " & vbCrLf
   vResult = vResult & "         @Numero = @P_NumeroComprobante, " & vbCrLf
   vResult = vResult & "         @TotalDebe = @SumMontoDebe, " & vbCrLf
   vResult = vResult & "         @TotalHaber = @SumMontoHaber, " & vbCrLf
   vResult = vResult & "         @NombreOperador = @P_NombreOperador" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpUpdTotal = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpUpdTotal", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnGeneraNumero(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameGeneraNumero(), fFnParametersGeneraNumero(), fFnScalarReturnDataTypeGeneraNumero(), fStatementsFnGeneraNumero(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnGeneraNumero = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnGeneraNumero", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnGeneraNumero(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameGeneraNumero(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnGeneraNumero = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnGeneraNumero", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameGeneraNumero() As String
   GetFnNameGeneraNumero = "ComprobanteGenNumero"
End Function

Private Function fFnParametersGeneraNumero() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & "@FechaComprobante " & gUtilSQL.getDateTypeForDB() & ", " & vbCrLf
   vResult = vResult & "@TipoComprobante " & gUtilSQL.getVarCharTypeForDB(2) & " = '91'" & vbCrLf
   fFnParametersGeneraNumero = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersGeneraNumero", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeGeneraNumero() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = gUtilSQL.getVarCharTypeForDB(8)
   fFnScalarReturnDataTypeGeneraNumero = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnScalarReturnDataTypeGeneraNumero", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsFnGeneraNumero() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Result " & gUtilSQL.getVarCharTypeForDB(8) & vbCrLf
   vResult = vResult & "   DECLARE @TipoNumeracionChr " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @MaxNumeroVarChar " & gUtilSQL.getVarCharTypeForDB(8) & vbCrLf
   vResult = vResult & "   DECLARE @MaxNumeroInt " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @MesStr " & gUtilSQL.getVarCharTypeForDB(2) & vbCrLf
   vResult = vResult & "   DECLARE @Continuar bit" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & vbCrLf
   vResult = vResult & "   DECLARE @Relleno " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Prefijo " & gUtilSQL.getVarCharTypeForDB(4) & vbCrLf
   vResult = vResult & "   DECLARE @PrefijoComodin " & gUtilSQL.getVarCharTypeForDB(5) & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT ConsecutivoPeriodo FROM Periodo WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND ConsecutivoCompania = @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "                SET @Result = '00000001'" & vbCrLf
   vResult = vResult & "           SELECT @TipoNumeracionChr = TipoDeNumeracion" & vbCrLf
   vResult = vResult & "                  FROM Periodo" & vbCrLf
   vResult = vResult & "                  WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "                         AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "           IF EXISTS(SELECT Numero FROM Comprobante WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "           BEGIN" & vbCrLf
   vResult = vResult & "                  IF @TipoNumeracionChr = '0' -- Tipo-Mes-Consecutivo" & vbCrLf
   vResult = vResult & "                  BEGIN" & vbCrLf
   vResult = vResult & "                         SET @Relleno = 4" & vbCrLf
   vResult = vResult & "                         SET @MesStr = RIGHT('00' + CAST(MONTH(@FechaComprobante) as varchar), 2)" & vbCrLf
   vResult = vResult & "                         SET @Prefijo = @TipoComprobante + @MesStr" & vbCrLf
   vResult = vResult & "                         SET @PrefijoComodin = @Prefijo + '%'" & vbCrLf
   vResult = vResult & "                         SELECT @MaxNumeroVarChar = MAX(Numero) FROM Comprobante WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Numero LIKE @PrefijoComodin" & vbCrLf
   vResult = vResult & "                         SET @MaxNumeroVarChar = SUBSTRING(ISNULL(@MaxNumeroVarChar,@Prefijo + '0001'), 3, 6)" & vbCrLf
   vResult = vResult & "                         SET @MaxNumeroInt = CAST(@MaxNumeroVarChar AS " & gUtilSQL.getNumericTypeForDB(9, 0) & ")" & vbCrLf
   vResult = vResult & "                  END" & vbCrLf
   vResult = vResult & "                  ELSE IF @TipoNumeracionChr = '2' -- Mes-Consecutivo" & vbCrLf
   vResult = vResult & "                  BEGIN" & vbCrLf
   vResult = vResult & "                         SET @Relleno = 6" & vbCrLf
   vResult = vResult & "                         SET @MesStr = RIGHT('00' + CAST(MONTH(@FechaComprobante) as varchar), 2)" & vbCrLf
   vResult = vResult & "                         SET @Prefijo = @MesStr" & vbCrLf
   vResult = vResult & "                         SET @PrefijoComodin = @Prefijo + '%'" & vbCrLf
   vResult = vResult & "                         SELECT @MaxNumeroVarChar = MAX(Numero) FROM Comprobante WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Numero LIKE @PrefijoComodin" & vbCrLf
   vResult = vResult & "                         SET @MaxNumeroVarChar = SUBSTRING(ISNULL(@MaxNumeroVarChar,@MesStr+'000001'), 3, 6)" & vbCrLf
   vResult = vResult & "                         SET @MaxNumeroInt = CAST(@MaxNumeroVarChar AS " & gUtilSQL.getNumericTypeForDB(9, 0) & ")" & vbCrLf
   vResult = vResult & "                  END" & vbCrLf
   vResult = vResult & "                  ELSE --Consecutivo" & vbCrLf
   vResult = vResult & "                  BEGIN" & vbCrLf
   vResult = vResult & "                         SET @Relleno = 8" & vbCrLf
   vResult = vResult & "                         SET @Prefijo = ''" & vbCrLf
   vResult = vResult & "                         SELECT @MaxNumeroVarChar = MAX(Numero) FROM Comprobante WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "                         SET @MaxNumeroInt = CAST(@MaxNumeroVarChar AS " & gUtilSQL.getNumericTypeForDB(9, 0) & ")" & vbCrLf
   vResult = vResult & "                  END" & vbCrLf
   vResult = vResult & "           END" & vbCrLf
   vResult = vResult & "           ELSE" & vbCrLf
   vResult = vResult & "           BEGIN" & vbCrLf
   vResult = vResult & "                  SET @MaxNumeroInt = 1" & vbCrLf
   vResult = vResult & "                  IF @TipoNumeracionChr = '0' -- Tipo-Mes-Consecutivo" & vbCrLf
   vResult = vResult & "                  BEGIN" & vbCrLf
   vResult = vResult & "                         SET @Relleno = 4" & vbCrLf
   vResult = vResult & "                         SET @MesStr = RIGHT('00' + CAST(MONTH(@FechaComprobante) as varchar), 2)" & vbCrLf
   vResult = vResult & "                         SET @Prefijo = @TipoComprobante + @MesStr" & vbCrLf
   vResult = vResult & "                  END" & vbCrLf
   vResult = vResult & "                  ELSE IF @TipoNumeracionChr = '2' -- Mes-Consecutivo" & vbCrLf
   vResult = vResult & "                  BEGIN" & vbCrLf
   vResult = vResult & "                         SET @Relleno = 6" & vbCrLf
   vResult = vResult & "                         SET @MesStr = RIGHT('00' + CAST(MONTH(@FechaComprobante) as varchar), 2)" & vbCrLf
   vResult = vResult & "                         SET @Prefijo = @MesStr" & vbCrLf
   vResult = vResult & "                  END" & vbCrLf
   vResult = vResult & "                  ELSE --Consecutivo" & vbCrLf
   vResult = vResult & "                  BEGIN" & vbCrLf
   vResult = vResult & "                         SET @Relleno = 8" & vbCrLf
   vResult = vResult & "                         SET @Prefijo = ''" & vbCrLf
   vResult = vResult & "                  END" & vbCrLf
   vResult = vResult & "              END" & vbCrLf
   vResult = vResult & "           SET @Continuar = 1 --True" & vbCrLf
   vResult = vResult & "           WHILE @Continuar = 1" & vbCrLf
   vResult = vResult & "           BEGIN" & vbCrLf
   vResult = vResult & "                  SET @NumeroComprobante = CAST(@MaxNumeroInt AS varchar)" & vbCrLf
   vResult = vResult & "                  SET @NumeroComprobante = RIGHT('00000000' + @NumeroComprobante, @Relleno)" & vbCrLf
   vResult = vResult & "                  SET @NumeroComprobante = ISNULL(@Prefijo, '') + @NumeroComprobante" & vbCrLf
   vResult = vResult & "                  IF EXISTS(SELECT Numero FROM Comprobante WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Numero = @NumeroComprobante)" & vbCrLf
   vResult = vResult & "                         SET @MaxNumeroInt = @MaxNumeroInt + 1" & vbCrLf
   vResult = vResult & "                  ELSE" & vbCrLf
   vResult = vResult & "                  BEGIN" & vbCrLf
   vResult = vResult & "                         SET @Result = @NumeroComprobante" & vbCrLf
   vResult = vResult & "                         SET @Continuar = 0 --False" & vbCrLf
   vResult = vResult & "                  END" & vbCrLf
   vResult = vResult & "      END" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Result = null" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsFnGeneraNumero = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnGeneraNumero", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpS10ComprobanteContableSEL(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10ComprobanteContableSEL(), fSpParametersS10ComprobanteContableSEL(), fStatementsSpS10ComprobanteContableSEL(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10ComprobanteContableSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10ComprobanteContableSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10ComprobanteContableSEL(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10ComprobanteContableSEL(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10ComprobanteContableSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10ComprobanteContableSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10ComprobanteContableSEL() As String
   GetSpNameS10ComprobanteContableSEL = "S10_ComprobanteContableSEL"
End Function

Private Function fSpParametersS10ComprobanteContableSEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "      @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & vbCrLf
    fSpParametersS10ComprobanteContableSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10ComprobanteContableSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10ComprobanteContableSEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   IF NOT EXISTS(SELECT ConsecutivoCompania FROM PERIODO WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      RAISERROR('No existe Período con Consecutivo Período:%d', 16, 1, @ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "      RETURN 0" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT COMPROBANTE.ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "            COMPROBANTE.Numero," & vbCrLf
   vResult = vResult & "            COMPROBANTE.Status," & vbCrLf
   vResult = vResult & "            COMPROBANTE.Fecha," & vbCrLf
   vResult = vResult & "            COMPROBANTE.Descripcion," & vbCrLf
   vResult = vResult & "            COMPROBANTE.TotalDebe," & vbCrLf
   vResult = vResult & "            COMPROBANTE.TotalHaber" & vbCrLf
   vResult = vResult & "         FROM COMPROBANTE" & vbCrLf
   vResult = vResult & "         WHERE COMPROBANTE.ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "            AND COMPROBANTE.Numero = @NumeroComprobante" & vbCrLf
   vResult = vResult & "      RETURN @@ROWCOUNT" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpS10ComprobanteContableSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10ComprobanteContableSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpDEL(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameDEL(), fSpParametersDEL(), fStatementsSpDEL(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpDEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpDEL(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameDEL(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpDEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameDEL() As String
   GetSpNameDEL = "ComprobanteDEL"
End Function

Private Function fSpParametersDEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @Numero " & gUtilSQL.getVarCharTypeForDB(8) & vbCrLf
    fSpParametersDEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpDEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DELETE FROM COMPROBANTE" & vbCrLf
   vResult = vResult & "        WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Numero = @Numero" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpDEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpAnular(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameAnular(), fSpParametersAnular(), fStatementsSpAnular(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpAnular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpAnular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpAnular(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameAnular(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpAnular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpAnular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameAnular() As String
   GetSpNameAnular = "ComprobanteAnular"
End Function

Private Function fSpParametersAnular() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @Numero " & gUtilSQL.getVarCharTypeForDB(8) & "," & vbCrLf
   vResult = vResult & "         @NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
    fSpParametersAnular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersAnular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpAnular() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT Numero FROM Comprobante WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Numero = @Numero)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      UPDATE ASIENTO" & vbCrLf
   vResult = vResult & "         SET" & vbCrLf
   vResult = vResult & "            MontoDebe = 0," & vbCrLf
   vResult = vResult & "            MontoHaber = 0," & vbCrLf
   vResult = vResult & "            Contabilizado = 'S'" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND NumeroComprobante = @Numero" & vbCrLf
   vResult = vResult & "      UPDATE COMPROBANTE" & vbCrLf
   vResult = vResult & "         SET" & vbCrLf
   vResult = vResult & "            TotalDebe = 0," & vbCrLf
   vResult = vResult & "            TotalHaber = 0," & vbCrLf
   vResult = vResult & "            Status = '0'," & vbCrLf
   vResult = vResult & "            NombreOperador = @NombreOperador" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Numero = @Numero" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpAnular = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpAnular", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
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
   GetSpNameS10DEL = "S10_ComprobanteDEL"
End Function

Private Function fSpParametersS10DEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "   @P_ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "   @P_NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & "," & vbCrLf
   vResult = vResult & "   @P_FechaComprobante " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
    fSpParametersS10DEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10DEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10DEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @ConsPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   SELECT @ConsPeriodo = ConsecutivoPeriodo FROM PERIODO" & vbCrLf
   vResult = vResult & "         WHERE" & vbCrLf
   vResult = vResult & "         FechaAperturaDelPeriodo <= @P_FechaComprobante" & vbCrLf
   vResult = vResult & "         AND FechaCierreDelPeriodo >= @P_FechaComprobante" & vbCrLf
   vResult = vResult & "         AND ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "   IF Not(@ConsPeriodo Is Null)" & vbCrLf
   vResult = vResult & "      EXEC Gp_ComprobanteDEL" & vbCrLf
   vResult = vResult & "         @ConsecutivoPeriodo = @ConsPeriodo," & vbCrLf
   vResult = vResult & "         @Numero = @P_NumeroComprobante" & vbCrLf
   vResult = vResult & "END "
   fStatementsSpS10DEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10DEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateSpS10PeriodoSel(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10PeriodoSel(), fSpParametersS10PeriodoSel(), fStatementsS10PeriodoSel(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10PeriodoSel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10PeriodoSel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10PeriodoSel(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10PeriodoSel(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10PeriodoSel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10PeriodoSel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10PeriodoSel() As String
   GetSpNameS10PeriodoSel = "S10_PeriodoSel"
End Function

Private Function fSpParametersS10PeriodoSel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @FixedWhereClause  " & gUtilSQL.getVarCharTypeForDB(4000) & "  = ''" & vbCrLf
   fSpParametersS10PeriodoSel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsS10PeriodoSel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsS10PeriodoSel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Sql  " & gUtilSQL.getVarCharTypeForDB(6000) & vbCrLf
   vResult = vResult & "   DECLARE @UsaS10 " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   SET @UsaS10 = 'S' " & vbCrLf
   vResult = vResult & "   SET @Sql = 'SELECT    PERIODO.ConsecutivoCompania, ConsecutivoPeriodo, PeriodoCerrado, FechaAperturaDelPeriodo, FechaCierreDelPeriodo, Mes1Cerrado, Mes2Cerrado, Mes3Cerrado, Mes4Cerrado, Mes5Cerrado, Mes6Cerrado," & vbCrLf
   vResult = vResult & "                      Mes7Cerrado, Mes8Cerrado, Mes9Cerrado, Mes10Cerrado, Mes11Cerrado, Mes12Cerrado" & vbCrLf
   vResult = vResult & "               FROM PERIODO INNER JOIN" & vbCrLf
   vResult = vResult & "                    COMPANIA ON PERIODO.ConsecutivoCompania = COMPANIA.ConsecutivoCompania" & vbCrLf
   vResult = vResult & "               WHERE (COMPANIA.UsaS10 = ' + CHAR(39) + 'S' + + CHAR(39) + ')'" & vbCrLf
   vResult = vResult & "   IF @FixedWhereClause <> ''" & vbCrLf
   vResult = vResult & "      SET @Sql = @Sql + ' AND ' + @FixedWhereClause" & vbCrLf
   vResult = vResult & "   EXEC(@Sql)" & vbCrLf
   vResult = vResult & "END "
   fStatementsS10PeriodoSel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsS10PeriodoSel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'***********************************************************************************

Private Function fCreateSpS10AsientoComprobanteIMP(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10AsientoComprobanteIMP(), fSpParametersS10AsientoComprobanteIMP(), fStatementsSpS10AsientoComprobanteIMP(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10AsientoComprobanteIMP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10AsientoComprobanteIMP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10AsientoComprobanteIMP(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10AsientoComprobanteIMP(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10AsientoComprobanteIMP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10AsientoComprobanteIMP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10AsientoComprobanteIMP() As String
   GetSpNameS10AsientoComprobanteIMP = "S10_AsientoComprobanteIMP"
End Function

Private Function fSpParametersS10AsientoComprobanteIMP() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "       @ConsecutivoCompania  " & gUtilSQL.getNumericTypeForDB(9, 0) & " ," & vbCrLf
   vResult = vResult & "      @FechaComprobante  " & gUtilSQL.getDateTypeForDB() & " ," & vbCrLf
   vResult = vResult & "      @FechaDeReferencia  " & gUtilSQL.getDateTypeForDB() & " ," & vbCrLf
   vResult = vResult & "      @DescripcionComprobante  " & gUtilSQL.getVarCharTypeForDB(160) & " ," & vbCrLf
   vResult = vResult & "      @CodigoCuentaContable  " & gUtilSQL.getVarCharTypeForDB(30) & " ," & vbCrLf
   vResult = vResult & "      @DescripcionCuentaContable  " & gUtilSQL.getVarCharTypeForDB(40) & "  = ''," & vbCrLf
   vResult = vResult & "      @DebeHaber " & gUtilSQL.getCharTypeForDB(1) & "," & vbCrLf
   vResult = vResult & "      @MontoAsiento money," & vbCrLf
   vResult = vResult & "      @DescripcionAsiento  " & gUtilSQL.getVarCharTypeForDB(160) & "  = ''," & vbCrLf
   vResult = vResult & "      @NumeroReferencia " & gUtilSQL.getVarCharTypeForDB(8) & " = ''," & vbCrLf
   vResult = vResult & "      @CodigoCentroDeCostos " & gUtilSQL.getVarCharTypeForDB(5) & " = ''," & vbCrLf
   vResult = vResult & "      @NumeroComprobanteGenerado  " & gUtilSQL.getVarCharTypeForDB(8) & "  = '' OUTPUT" & vbCrLf
    fSpParametersS10AsientoComprobanteIMP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10AsientoComprobanteIMP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10AsientoComprobanteIMP() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @CompaniaUsaAuxiliares char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Operador  " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoPeriodo  " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroComprobante  " & gUtilSQL.getVarCharTypeForDB(8) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroDocumentoOrigen  " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxCChar char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxCEnum  " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @TipoDeComprobante char(2)" & vbCrLf
   vResult = vResult & "   DECLARE @Continuar bit " & vbCrLf
   vResult = vResult & "   IF NOT EXISTS (SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "      RAISERROR('No existe Compañía para ConsecutivoCompania:%d', 16, 1, @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT @CompaniaUsaAuxiliares = UsaAuxiliares" & vbCrLf
   vResult = vResult & "         FROM Compania" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "      SELECT @ConsecutivoPeriodo = ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "         FROM Periodo" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "            AND FechaAperturaDelPeriodo <= @FechaComprobante" & vbCrLf
   vResult = vResult & "            AND FechaCierreDelPeriodo >= @FechaComprobante" & vbCrLf
   vResult = vResult & "            AND PeriodoCerrado = 'N'" & vbCrLf
   vResult = vResult & "      IF @ConsecutivoPeriodo IS NULL" & vbCrLf
   vResult = vResult & "         RAISERROR('No existe Consecutivo Período válido para la fecha del documento.', 16, 1)" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         IF @ConsecutivoPeriodo = 0" & vbCrLf
   vResult = vResult & "            RAISERROR('No existe Consecutivo Período válido para la fecha del documento.', 16, 1)" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            IF @TipoDeComprobante IS NULL" & vbCrLf
   vResult = vResult & "               SET @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "            IF @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SELECT @TipoDeComprobante = CodigoDelTipo" & vbCrLf
   vResult = vResult & "                  FROM tipoDeComprobante" & vbCrLf
   vResult = vResult & "                  WHERE NombreDelTipo = 'Importar'" & vbCrLf
   vResult = vResult & "               IF @TipoDeComprobante IS NULL" & vbCrLf
   vResult = vResult & "                  SET @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "            END " & vbCrLf
   vResult = vResult & "            IF @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "               SET @TipoDeComprobante = '91' -- Importacion" & vbCrLf
   vResult = vResult & "            SET @Operador = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "            SET @GeneradoPorCxCEnum = 0 --Manual" & vbCrLf
   vResult = vResult & "            SET @GeneradoPorCxCChar = dbo.Gf_EnumToDBValue('0') --CxC" & vbCrLf
   vResult = vResult & "            SELECT @NumeroComprobante = Numero" & vbCrLf
   vResult = vResult & "               FROM Comprobante" & vbCrLf
   vResult = vResult & "               WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "                  AND Numero = @NumeroComprobanteGenerado" & vbCrLf
   vResult = vResult & "                  AND GeneradoPor = @GeneradoPorCxCChar" & vbCrLf
   vResult = vResult & "            IF @NumeroComprobante IS NULL" & vbCrLf
   vResult = vResult & "               SET @NumeroComprobante = ''" & vbCrLf
   vResult = vResult & "            IF (@CodigoCentroDeCostos <> NULL AND @CodigoCentroDeCostos <> '')" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               IF EXISTS(SELECT Codigo FROM CentroDeCostos WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Codigo = @CodigoCentroDeCostos )" & vbCrLf
   vResult = vResult & "                  SET @Continuar = 1" & vbCrLf
   vResult = vResult & "               Else" & vbCrLf
   vResult = vResult & "                  SET @Continuar = 0" & vbCrLf
   vResult = vResult & "            End" & vbCrLf
   vResult = vResult & "            Else" & vbCrLf
   vResult = vResult & "               SET @Continuar = 1" & vbCrLf
   vResult = vResult & "            IF (@Continuar = 1)" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               EXEC Gp_AsientoComprobanteIMP" & vbCrLf
   vResult = vResult & "                  @NombreOperador = @Operador," & vbCrLf
   vResult = vResult & "                  @P_ConsecutivoPeriodo = @ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "                  @P_ConsecutivoDocOrigen = 0, -- Cero" & vbCrLf
   vResult = vResult & "                  @P_NumeroComprobante = @NumeroComprobante," & vbCrLf
   vResult = vResult & "                  @P_FechaComprobante = @FechaComprobante," & vbCrLf
   vResult = vResult & "                  @P_DescripcionComprobante = @DescripcionComprobante," & vbCrLf
   vResult = vResult & "                  @P_CodigoCuenta = @CodigoCuentaContable," & vbCrLf
   vResult = vResult & "                  @P_DescripcionCuenta = @DescripcionCuentaContable," & vbCrLf
   vResult = vResult & "                  @P_DebeHaber = @DebeHaber," & vbCrLf
   vResult = vResult & "                  @P_Monto = @MontoAsiento," & vbCrLf
   vResult = vResult & "                  @P_FechaDeReferencia = @FechaDeReferencia," & vbCrLf
   vResult = vResult & "                  @P_NumeroReferencia = @NumeroReferencia," & vbCrLf
   vResult = vResult & "                  @P_DescripcionAsiento = @DescripcionAsiento," & vbCrLf
   vResult = vResult & "                  @P_GrupoAuxiliar = ''," & vbCrLf
   vResult = vResult & "                  @P_Auxiliar = ''," & vbCrLf
   vResult = vResult & "                  @P_NombreAuxiliar = ''," & vbCrLf
   vResult = vResult & "                  @P_CentroDeCostos = @CodigoCentroDeCostos," & vbCrLf
   vResult = vResult & "                  @P_DescripcionCentroDeCostos = ''," & vbCrLf
   vResult = vResult & "                  @P_GeneradoPorInt = @GeneradoPorCxCEnum," & vbCrLf
   vResult = vResult & "                  @P_NumeroDocumentoOrigen = @NumeroComprobante, -- Blanco" & vbCrLf
   vResult = vResult & "                  @P_TipoDeComprobante = @TipoDeComprobante," & vbCrLf
   vResult = vResult & "                  @P_NumeroComprobanteGenerado = @NumeroComprobanteGenerado OUTPUT" & vbCrLf
   vResult = vResult & "            END" & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "END "
   fStatementsSpS10AsientoComprobanteIMP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10AsientoComprobanteIMP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
