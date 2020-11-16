VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsGenerarComprobanteSp"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsGenerarComprobanteSp"
End Function

Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsGenerarComprobanteSp"
End Function

Public Function fCreateDropGenerarComprobanteSP(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      'CxP
      vResult = vResult And fCreateSpCxPContabilizar(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10CxPContabilizar(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10ComprobanteContableCxPSEL(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10ComprobanteAsientoCxPSEL(valShowMessage, valWithEncryption)
      'CxC
      vResult = vResult And fCreateSpCxCContabilizar(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10CxCContabilizar(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10CxCComprobanteContableSEL(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10CxCComprobanteAsientoSEL(valShowMessage, valWithEncryption)
   Else
      'CxC
      vResult = vResult And fDropSpS10CxCComprobanteAsientoSEL(valShowMessage)
      vResult = vResult And fDropSpS10CxCComprobanteContableSEL(valShowMessage)
      vResult = vResult And fDropSpS10CxCContabilizar(valShowMessage)
      vResult = vResult And fDropSpCxCContabilizar(valShowMessage)
      'CxP
      vResult = vResult And fDropSpS10ComprobanteAsientoS10CxPSEL(valShowMessage)
      vResult = vResult And fDropSpS10ComprobanteContableCxPSEL(valShowMessage)
      vResult = vResult And fDropSpS10CxPContabilizar(valShowMessage)
      vResult = vResult And fDropSpCxPContabilizar(valShowMessage)
   End If
   fCreateDropGenerarComprobanteSP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropGenerarComprobanteSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpS10CxPContabilizar(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10CxPContabilizar(), fSpParametersS10CxPContabilizar(), fStatementsSpS10CxPContabilizar(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10CxPContabilizar = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10CxPContabilizar", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10CxPContabilizar(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10CxPContabilizar(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10CxPContabilizar = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10CxPContabilizar", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10CxPContabilizar() As String
   GetSpNameS10CxPContabilizar = "S10_CxPContabilizar"
End Function

Private Function fSpParametersS10CxPContabilizar() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "      @FechaCxP " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "      @NumeroCxP " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "      @CodigoCuentaContable " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "      @DescripcionCuentaContable " & gUtilSQL.getVarCharTypeForDB(40) & " = ''," & vbCrLf
   vResult = vResult & "      @DebeHaber char(1)," & vbCrLf
   vResult = vResult & "      @MontoAsiento money," & vbCrLf
   vResult = vResult & "      @DescripcionAsiento " & gUtilSQL.getVarCharTypeForDB(160) & " = ''," & vbCrLf
   vResult = vResult & "      @CodigoCentroDeCostos " & gUtilSQL.getVarCharTypeForDB(10) & " = ''," & vbCrLf
   vResult = vResult & "      @NumeroComprobanteGenerado " & gUtilSQL.getVarCharTypeForDB(8) & " = '' OUTPUT" & vbCrLf
   fSpParametersS10CxPContabilizar = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10CxPContabilizar", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10CxPContabilizar() As String
   Dim vResult As String
   On Error GoTo h_ERROR
'   vResult = vResult & "-- =============================================" & vbCrLf
'   vResult = vResult & "-- Author:     Mayelin Sánchez" & vbCrLf
'   vResult = vResult & "-- Create date:" & vbCrLf
'   vResult = vResult & "-- Description:   Este proceso permite contabilizar para S10, ello requieren una contabilización DETALLADA" & vbCrLf
'   vResult = vResult & "-- =============================================" & vbCrLf
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @CompaniaUsaAuxiliares char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Operador " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxPChar char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxPEnum " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @DescripcionComprobante " & gUtilSQL.getVarCharTypeForDB(160) & "" & vbCrLf
   vResult = vResult & "   DECLARE @TipoDeComprobante char(2)" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoCxP " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @GrupoAuxiliar " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Auxiliar " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @NombreAuxiliar " & gUtilSQL.getVarCharTypeForDB(35) & vbCrLf
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
   vResult = vResult & "            AND FechaAperturaDelPeriodo <= @FechaCxP" & vbCrLf
   vResult = vResult & "            AND FechaCierreDelPeriodo >= @FechaCxP" & vbCrLf
   vResult = vResult & "            AND PeriodoCerrado = 'N'" & vbCrLf
   vResult = vResult & "      IF @ConsecutivoPeriodo IS NULL" & vbCrLf
   vResult = vResult & "         RAISERROR('No existe Consecutivo Período válido para la fecha del documento.', 16, 1)" & vbCrLf
   vResult = vResult & "      ELSE IF @ConsecutivoPeriodo = 0" & vbCrLf
   vResult = vResult & "         RAISERROR('No existe Consecutivo Período válido para la fecha del documento.', 16, 1)" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   
   
   
   vResult = vResult & "         SET @GrupoAuxiliar  = ''" & vbCrLf
   vResult = vResult & "         SET @Auxiliar = ''" & vbCrLf
   vResult = vResult & "         SET @NombreAuxiliar = ''" & vbCrLf
   vResult = vResult & "         IF (UPPER(@CompaniaUsaAuxiliares) = 'S')" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @GrupoAuxiliar = dbo.Gf_GrupoAuxiliar(@CodigoCuentaContable,@ConsecutivoCompania,@FechaCxP)" & vbCrLf
   vResult = vResult & "            IF (@GrupoAuxiliar <> '') AND @GrupoAuxiliar = '1' -- 1 = Proveedores" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SELECT   @Auxiliar =  ISNULL(auxiliar.Codigo,'FALTA'), @NombreAuxiliar = ISNULL(auxiliar.Nombre,'')" & vbCrLf
   vResult = vResult & "               FROM auxiliar INNER JOIN" & vbCrLf
   vResult = vResult & "                  PERIODO ON auxiliar.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo INNER JOIN" & vbCrLf
   vResult = vResult & "                  COMPANIA ON PERIODO.ConsecutivoCompania = COMPANIA.ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                WHERE (COMPANIA.ConsecutivoCompania = 1) AND (PERIODO.FechaAperturaDelPeriodo <= @FechaCxP) AND" & vbCrLf
   vResult = vResult & "                      (PERIODO.FechaCierreDelPeriodo >= @FechaCxP) AND (auxiliar.TipoDeAuxiliar = '1') -- PROVEEDORES" & vbCrLf
   vResult = vResult & "                      AND (auxiliar.Codigo = @CodigoProveedor)" & vbCrLf
   vResult = vResult & "                IF (@Auxiliar = '')  SET @Auxiliar = @CodigoProveedor" & vbCrLf
   vResult = vResult & "                IF (@NombreAuxiliar = '') SELECT  @NombreAuxiliar = NombreProveedor FROM Proveedor WHERE (ConsecutivoCompania = @ConsecutivoCompania) AND (CodigoProveedor = @CodigoProveedor)" & vbCrLf
   vResult = vResult & "            END" & vbCrLf
   vResult = vResult & "         END" & vbCrLf
   
   
   
   vResult = vResult & "         SELECT @TipoDeComprobante = CxPtipoComprobante" & vbCrLf
   vResult = vResult & "            FROM reglasDeContabilizacion" & vbCrLf
   vResult = vResult & "            WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "         IF @TipoDeComprobante IS NULL" & vbCrLf
   vResult = vResult & "            SET @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "         IF @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SELECT @TipoDeComprobante = CodigoDelTipo" & vbCrLf
   vResult = vResult & "               FROM tipoDeComprobante" & vbCrLf
   vResult = vResult & "               WHERE NombreDelTipo = 'Cuentas x Pagar'" & vbCrLf
   vResult = vResult & "            IF @TipoDeComprobante IS NULL" & vbCrLf
   vResult = vResult & "               SET @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "         IF @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "            SET @TipoDeComprobante = '04'" & vbCrLf
   vResult = vResult & "         SELECT @ConsecutivoCxP = ConsecutivoCxp" & vbCrLf
   vResult = vResult & "            FROM cxP" & vbCrLf
   vResult = vResult & "            WHERE ConsecutivoCompania = @ConsecutivoCompania AND Numero = @NumeroCxP AND CodigoProveedor = @CodigoProveedor" & vbCrLf
   vResult = vResult & "         SET @Operador = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "         SET @NumeroDocumentoOrigen = CAST(@ConsecutivoCxP AS varchar)" & vbCrLf
   vResult = vResult & "         SET @GeneradoPorCxPEnum = 11 --CxP" & vbCrLf
   vResult = vResult & "         SET @GeneradoPorCxPChar = dbo.Gf_EnumToDBValue(@GeneradoPorCxPEnum) --CxP" & vbCrLf
   vResult = vResult & "         SELECT @NumeroComprobante = Numero" & vbCrLf
   vResult = vResult & "            FROM Comprobante" & vbCrLf
   vResult = vResult & "            WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "               AND NoDocumentoOrigen = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "               AND GeneradoPor = @GeneradoPorCxPChar" & vbCrLf
   vResult = vResult & "         IF @NumeroComprobante IS NULL" & vbCrLf
   vResult = vResult & "            SET @NumeroComprobante = ''" & vbCrLf
   vResult = vResult & "         SET @DescripcionComprobante = 'CxP N°' + @NumeroCxP + ' Proveedor ' + @CodigoProveedor" & vbCrLf
   vResult = vResult & "         IF (@CodigoCentroDeCostos <> NULL AND @CodigoCentroDeCostos <> '')" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            IF EXISTS(SELECT Codigo FROM CentroDeCostos WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Codigo = @CodigoCentroDeCostos )" & vbCrLf
   vResult = vResult & "               SET @Continuar = 1" & vbCrLf
   vResult = vResult & "            Else" & vbCrLf
   vResult = vResult & "               SET @Continuar = 0" & vbCrLf
   vResult = vResult & "         End" & vbCrLf
   vResult = vResult & "         Else" & vbCrLf
   vResult = vResult & "            SET @Continuar = 1" & vbCrLf
   vResult = vResult & "         IF (@Continuar = 1)" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "               EXEC Gp_AsientoComprobanteIMP" & vbCrLf
   vResult = vResult & "                  @NombreOperador = @Operador," & vbCrLf
   vResult = vResult & "                  @P_ConsecutivoPeriodo = @ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "                  @P_ConsecutivoDocOrigen = @ConsecutivoCxP," & vbCrLf
   vResult = vResult & "                  @P_NumeroComprobante = @NumeroComprobante," & vbCrLf
   vResult = vResult & "                  @P_FechaComprobante = @FechaCxP," & vbCrLf
   vResult = vResult & "                  @P_DescripcionComprobante = @DescripcionComprobante," & vbCrLf
   vResult = vResult & "                  @P_CodigoCuenta = @CodigoCuentaContable," & vbCrLf
   vResult = vResult & "                  @P_DescripcionCuenta = @DescripcionCuentaContable," & vbCrLf
   vResult = vResult & "                  @P_DebeHaber = @DebeHaber," & vbCrLf
   vResult = vResult & "                  @P_Monto = @MontoAsiento," & vbCrLf
   vResult = vResult & "                  @P_FechaDeReferencia = @FechaCxP," & vbCrLf
   vResult = vResult & "                  @P_NumeroReferencia = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "                  @P_DescripcionAsiento = @DescripcionAsiento," & vbCrLf
   vResult = vResult & "                  @P_GrupoAuxiliar = @GrupoAuxiliar," & vbCrLf
   vResult = vResult & "                  @P_Auxiliar = @Auxiliar," & vbCrLf
   vResult = vResult & "                  @P_NombreAuxiliar = @NombreAuxiliar," & vbCrLf
   vResult = vResult & "                  @P_CentroDeCostos = @CodigoCentroDeCostos," & vbCrLf
   vResult = vResult & "                  @P_DescripcionCentroDeCostos = ''," & vbCrLf
   vResult = vResult & "                  @P_GeneradoPorInt = @GeneradoPorCxPEnum," & vbCrLf
   vResult = vResult & "                  @P_NumeroDocumentoOrigen = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "                  @P_TipoDeComprobante = @TipoDeComprobante," & vbCrLf
   vResult = vResult & "                  @P_NumeroComprobanteGenerado = @NumeroComprobanteGenerado OUTPUT" & vbCrLf
   vResult = vResult & "         END" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpS10CxPContabilizar = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10CxPContabilizar", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpS10ComprobanteAsientoCxPSEL(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
      Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10ComprobanteAsientoCxPSEL(), fSpParametersS10ComprobanteAsientoCxPSEL(), fStatementsSpS10ComprobanteAsientoCxPSEL(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10ComprobanteAsientoCxPSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10ComprobanteAsientoCxPSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10ComprobanteAsientoS10CxPSEL(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10ComprobanteAsientoCxPSEL(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10ComprobanteAsientoS10CxPSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10ComprobanteAsientoS10CxPSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10ComprobanteAsientoCxPSEL() As String
   GetSpNameS10ComprobanteAsientoCxPSEL = "S10_CxPComprobanteAsientoSEL"
End Function

Private Function fSpParametersS10ComprobanteAsientoCxPSEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "      @CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "      @NumeroCxP " & gUtilSQL.getVarCharTypeForDB(20) & vbCrLf
    fSpParametersS10ComprobanteAsientoCxPSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10ComprobanteAsientoCxPSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10ComprobanteAsientoCxPSEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & "" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxPEnum " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxPChar char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @CountComprobantes " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoCxp " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   IF NOT EXISTS(SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      RAISERROR('No existe Compañía con Consecutivo Compañía:%d', 16, 1, @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "      RETURN 0" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   ELSE IF NOT EXISTS(SELECT NombreProveedor FROM Proveedor WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      RAISERROR('No existe Proveedor con Código:%s', 16, 1, @CodigoProveedor)" & vbCrLf
   vResult = vResult & "      RETURN 0" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   ELSE IF NOT EXISTS(SELECT Numero FROM CxP WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor AND Numero = @NumeroCxP)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      RAISERROR('Para el proveedor indicado, no existe Cuenta por Pagar número:%s', 16, 1, @NumeroCxP)" & vbCrLf
   vResult = vResult & "      RETURN 0" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT @ConsecutivoCxp = ConsecutivoCxp FROM CxP WHERE ConsecutivoCompania = @ConsecutivoCompania AND Numero = @NumeroCxP AND CodigoProveedor = @CodigoProveedor" & vbCrLf
   vResult = vResult & "      SET @NumeroDocumentoOrigen = CAST(@ConsecutivoCxp AS varchar)" & vbCrLf
   vResult = vResult & "      SET @GeneradoPorCxPEnum = 11 --CxP" & vbCrLf
   vResult = vResult & "      SET @GeneradoPorCxPChar = dbo.Gf_EnumToDBValue(@GeneradoPorCxPEnum) --CxP" & vbCrLf
   vResult = vResult & "      SELECT @CountComprobantes = COUNT(COMPROBANTE.ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "         FROM COMPROBANTE INNER JOIN PERIODO" & vbCrLf
   vResult = vResult & "            ON COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "         WHERE PERIODO.ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "            AND COMPROBANTE.NoDocumentoOrigen = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "            AND COMPROBANTE.GeneradoPor = @GeneradoPorCxPChar" & vbCrLf
   vResult = vResult & "            AND COMPROBANTE.ConsecutivoDocOrigen = @ConsecutivoCxp" & vbCrLf
   vResult = vResult & "      IF (@CountComprobantes > 1)" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         RAISERROR('Existe más de 1 Comprobante Contable para el documento deseado.', 16, 1)" & vbCrLf
   vResult = vResult & "         RETURN 0" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SELECT @ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "               @NumeroComprobante = COMPROBANTE.Numero" & vbCrLf
   vResult = vResult & "            FROM COMPROBANTE INNER JOIN PERIODO" & vbCrLf
   vResult = vResult & "               ON COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "            WHERE PERIODO.ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "               AND COMPROBANTE.NoDocumentoOrigen = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "               AND COMPROBANTE.GeneradoPor = @GeneradoPorCxPChar" & vbCrLf
   vResult = vResult & "               AND COMPROBANTE.ConsecutivoDocOrigen = @ConsecutivoCxp" & vbCrLf
   vResult = vResult & "         EXEC Gp_S10_ComprobanteAsientoSEL @ConsecutivoPeriodo, @NumeroComprobante" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpS10ComprobanteAsientoCxPSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10ComprobanteAsientoCxPSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpS10ComprobanteContableCxPSEL(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10ComprobanteContableCxPSEL(), fSpParametersS10ComprobanteContableCxPSEL(), fStatementsSpS10ComprobanteContableCxPSEL(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10ComprobanteContableCxPSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10ComprobanteContableCxPSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10ComprobanteContableCxPSEL(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10ComprobanteContableCxPSEL(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10ComprobanteContableCxPSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10ComprobanteContableCxPSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10ComprobanteContableCxPSEL() As String
   GetSpNameS10ComprobanteContableCxPSEL = "S10_CxPComprobanteContableSEL"
End Function

Private Function fSpParametersS10ComprobanteContableCxPSEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "      @CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "      @NumeroCxP " & gUtilSQL.getVarCharTypeForDB(20) & vbCrLf
    fSpParametersS10ComprobanteContableCxPSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10ComprobanteContableCxPSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10ComprobanteContableCxPSEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroDocumentoOrigen varchar(30)" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoPeriodo int" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroComprobante varchar(8)" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxPEnum int" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxPChar char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @CountComprobantes int" & vbCrLf
   vResult = vResult & "   IF NOT EXISTS(SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "      RAISERROR('No existe Compañía con Consecutivo Compañía:%d', 16, 1, @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   ELSE IF NOT EXISTS(SELECT NombreProveedor FROM Proveedor WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor)" & vbCrLf
   vResult = vResult & "      RAISERROR('No existe Proveedor con Código:%s', 16, 1, @CodigoProveedor)" & vbCrLf
   vResult = vResult & "   ELSE IF NOT EXISTS(SELECT Numero FROM CxP WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor AND Numero = @NumeroCxP)" & vbCrLf
   vResult = vResult & "      RAISERROR('Para el proveedor indicado, no existe Cuenta por Pagar número:%s', 16, 1, @NumeroCxP)" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @NumeroDocumentoOrigen = @CodigoProveedor + CHAR(9) + @NumeroCxP" & vbCrLf
   vResult = vResult & "      SET @GeneradoPorCxPEnum = 11 --CxP" & vbCrLf
   vResult = vResult & "      SET @GeneradoPorCxPChar = dbo.Gf_EnumToDBValue(@GeneradoPorCxPEnum) --CxP" & vbCrLf
   vResult = vResult & "      SELECT @CountComprobantes = COUNT(COMPROBANTE.ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "         FROM COMPROBANTE INNER JOIN PERIODO" & vbCrLf
   vResult = vResult & "            ON COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "         WHERE PERIODO.ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "            AND COMPROBANTE.NoDocumentoOrigen = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "            AND COMPROBANTE.GeneradoPor = @GeneradoPorCxPChar" & vbCrLf
   vResult = vResult & "      IF (@CountComprobantes > 1)" & vbCrLf
   vResult = vResult & "         RAISERROR('Existe más de 1 Comprobante Contable para el documento deseado.', 16, 1)" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SELECT @ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "               @NumeroComprobante = COMPROBANTE.Numero" & vbCrLf
   vResult = vResult & "            FROM COMPROBANTE INNER JOIN PERIODO" & vbCrLf
   vResult = vResult & "               ON COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "            WHERE PERIODO.ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "               AND COMPROBANTE.NoDocumentoOrigen = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "               AND COMPROBANTE.GeneradoPor = @GeneradoPorCxPChar" & vbCrLf
   vResult = vResult & "         EXEC Gp_S10_ComprobanteContableSEL @ConsecutivoPeriodo, @NumeroComprobante" & vbCrLf
   vResult = vResult & "      END" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpS10ComprobanteContableCxPSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10ComprobanteContableCxPSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpS10CxCContabilizar(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10CxCContabilizar(), fSpParametersS10CxCContabilizar(), fStatementsSpS10CxCContabilizar(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10CxCContabilizar = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10CxCContabilizar", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10CxCContabilizar(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10CxCContabilizar(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10CxCContabilizar = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10CxCContabilizar", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10CxCContabilizar() As String
   GetSpNameS10CxCContabilizar = "S10_CxCContabilizar"
End Function

Private Function fSpParametersS10CxCContabilizar() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "      @FechaCxC " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @NumeroCxC " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "      @CodigoTipoDeDocumentoCxC varchar(10)," & vbCrLf
   vResult = vResult & "      @CodigoCliente " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "      @CodigoCuentaContable " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "      @DescripcionCuentaContable " & gUtilSQL.getVarCharTypeForDB(40) & " = ''," & vbCrLf
   vResult = vResult & "      @DebeHaber char(1)," & vbCrLf
   vResult = vResult & "      @MontoAsiento money," & vbCrLf
   vResult = vResult & "      @DescripcionAsiento " & gUtilSQL.getVarCharTypeForDB(160) & " = ''," & vbCrLf
   vResult = vResult & "      @CodigoCentroDeCostos " & gUtilSQL.getVarCharTypeForDB(5) & " = ''," & vbCrLf
   vResult = vResult & "      @NumeroComprobanteGenerado " & gUtilSQL.getVarCharTypeForDB(8) & " = '' OUTPUT" & vbCrLf
   fSpParametersS10CxCContabilizar = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10CxCContabilizar", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10CxCContabilizar() As String
   Dim vResult As String
   On Error GoTo h_ERROR
'   vResult = vResult & "-- =============================================" & vbCrLf
'   vResult = vResult & "-- Author:     Mayelin Sánchez" & vbCrLf
'   vResult = vResult & "-- Create date: <Create Date,,>" & vbCrLf
'   vResult = vResult & "-- Description:   Este proceso permite contabilizar para S10, ello requieren una contabilización DETALLADA" & vbCrLf
'   vResult = vResult & "-- =============================================" & vbCrLf
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @CompaniaUsaAuxiliares char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Operador " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxCChar char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxCEnum " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @DescripcionComprobante " & gUtilSQL.getVarCharTypeForDB(160) & "" & vbCrLf
   vResult = vResult & "   DECLARE @TipoDeComprobante char(2)" & vbCrLf
   vResult = vResult & "   DECLARE @TipoCxc char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @GrupoAuxiliar " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Auxiliar " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @NombreAuxiliar " & gUtilSQL.getVarCharTypeForDB(35) & vbCrLf
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
   vResult = vResult & "            AND FechaAperturaDelPeriodo <= @FechaCxC" & vbCrLf
   vResult = vResult & "            AND FechaCierreDelPeriodo >= @FechaCxC" & vbCrLf
   vResult = vResult & "            AND PeriodoCerrado = 'N'" & vbCrLf
   vResult = vResult & "      IF @ConsecutivoPeriodo IS NULL" & vbCrLf
   vResult = vResult & "         RAISERROR('No existe Consecutivo Período válido para la fecha del documento.', 16, 1)" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   
   vResult = vResult & "         SET @GrupoAuxiliar  = ''" & vbCrLf
   vResult = vResult & "         SET @Auxiliar = ''" & vbCrLf
   vResult = vResult & "         SET @NombreAuxiliar = ''" & vbCrLf
   vResult = vResult & "         IF (UPPER(@CompaniaUsaAuxiliares) = 'S')" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @GrupoAuxiliar = dbo.Gf_GrupoAuxiliar(@CodigoCuentaContable, @ConsecutivoCompania, @FechaCxC)" & vbCrLf
   vResult = vResult & "            IF (@GrupoAuxiliar <> '') AND @GrupoAuxiliar = '0' -- 1 = CLIENTES" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SELECT   @Auxiliar =  ISNULL(auxiliar.Codigo,'FALTA'), @NombreAuxiliar = ISNULL(auxiliar.Nombre,'')" & vbCrLf
   vResult = vResult & "               FROM auxiliar INNER JOIN" & vbCrLf
   vResult = vResult & "                  PERIODO ON auxiliar.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo INNER JOIN" & vbCrLf
   vResult = vResult & "                  COMPANIA ON PERIODO.ConsecutivoCompania = COMPANIA.ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                WHERE (COMPANIA.ConsecutivoCompania = 1) AND (PERIODO.FechaAperturaDelPeriodo <= @FechaCxC) AND" & vbCrLf
   vResult = vResult & "                      (PERIODO.FechaCierreDelPeriodo >= @FechaCxC) AND (auxiliar.TipoDeAuxiliar = '0') -- CLIENTES" & vbCrLf
   vResult = vResult & "                      AND (auxiliar.Codigo = @CodigoCliente)" & vbCrLf
   vResult = vResult & "                IF (@Auxiliar = '')  SET @Auxiliar = @CodigoCliente" & vbCrLf
   vResult = vResult & "                IF (@NombreAuxiliar = '') SELECT  @NombreAuxiliar = Nombre FROM Cliente WHERE (ConsecutivoCompania = @ConsecutivoCompania) AND (Codigo = @CodigoCliente)" & vbCrLf
   vResult = vResult & "            END" & vbCrLf
   vResult = vResult & "         END" & vbCrLf
   
   vResult = vResult & "         IF @ConsecutivoPeriodo = 0" & vbCrLf
   vResult = vResult & "            RAISERROR('No existe Consecutivo Período válido para la fecha del documento.', 16, 1)" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SELECT @TipoCxc = TipoDeDocumento FROM TipoDeDocumentoLey WHERE (Codigo = @CodigoTipoDeDocumentoCxC)" & vbCrLf
   vResult = vResult & "            IF (@TipoCxc IS NULL)" & vbCrLf
   vResult = vResult & "               SET @TipoCxc = '0'" & vbCrLf
   vResult = vResult & "            SELECT @TipoDeComprobante =  CxCtipoComprobante" & vbCrLf
   vResult = vResult & "               FROM reglasDeContabilizacion" & vbCrLf
   vResult = vResult & "               WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "            IF @TipoDeComprobante IS NULL" & vbCrLf
   vResult = vResult & "               SET @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "            IF @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SELECT @TipoDeComprobante = CodigoDelTipo" & vbCrLf
   vResult = vResult & "                  FROM tipoDeComprobante" & vbCrLf
   vResult = vResult & "                  WHERE NombreDelTipo = 'Cuentas x Cobrar'" & vbCrLf
   vResult = vResult & "               IF @TipoDeComprobante IS NULL" & vbCrLf
   vResult = vResult & "                  SET @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "            END " & vbCrLf
   vResult = vResult & "            IF @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "               SET @TipoDeComprobante = '03'" & vbCrLf
   vResult = vResult & "            SET @Operador = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "            SET @NumeroDocumentoOrigen = @TipoCxc + char(9) + @NumeroCxC" & vbCrLf
   vResult = vResult & "            SET @GeneradoPorCxCEnum = 9 --CxC" & vbCrLf
   vResult = vResult & "            SET @GeneradoPorCxCChar = dbo.Gf_EnumToDBValue(@GeneradoPorCxCEnum) --CxC" & vbCrLf
   vResult = vResult & "            SELECT @NumeroComprobante = Numero" & vbCrLf
   vResult = vResult & "               FROM Comprobante" & vbCrLf
   vResult = vResult & "               WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "                  AND NoDocumentoOrigen = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "                  AND GeneradoPor = @GeneradoPorCxCChar" & vbCrLf
   vResult = vResult & "            IF @NumeroComprobante IS NULL" & vbCrLf
   vResult = vResult & "               SET @NumeroComprobante = ''" & vbCrLf
   vResult = vResult & "            SET @DescripcionComprobante = 'CxC N°' + @NumeroCxC + ' Cliente ' + @CodigoCliente" & vbCrLf
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
   vResult = vResult & "                     @NombreOperador = @Operador," & vbCrLf
   vResult = vResult & "                     @P_ConsecutivoPeriodo = @ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "                     @P_ConsecutivoDocOrigen = 0," & vbCrLf
   vResult = vResult & "                     @P_NumeroComprobante = @NumeroComprobante," & vbCrLf
   vResult = vResult & "                     @P_FechaComprobante = @FechaCxC," & vbCrLf
   vResult = vResult & "                     @P_DescripcionComprobante = @DescripcionComprobante," & vbCrLf
   vResult = vResult & "                     @P_CodigoCuenta = @CodigoCuentaContable," & vbCrLf
   vResult = vResult & "                     @P_DescripcionCuenta = @DescripcionCuentaContable," & vbCrLf
   vResult = vResult & "                     @P_DebeHaber = @DebeHaber," & vbCrLf
   vResult = vResult & "                     @P_Monto = @MontoAsiento," & vbCrLf
   vResult = vResult & "                     @P_FechaDeReferencia = @FechaCxC," & vbCrLf
   vResult = vResult & "                     @P_NumeroReferencia = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "                     @P_DescripcionAsiento = @DescripcionAsiento," & vbCrLf
   vResult = vResult & "                     @P_GrupoAuxiliar = @GrupoAuxiliar," & vbCrLf
   vResult = vResult & "                     @P_Auxiliar = @Auxiliar," & vbCrLf
   vResult = vResult & "                     @P_NombreAuxiliar = @NombreAuxiliar," & vbCrLf
   vResult = vResult & "                     @P_CentroDeCostos = @CodigoCentroDeCostos," & vbCrLf
   vResult = vResult & "                     @P_DescripcionCentroDeCostos = ''," & vbCrLf
   vResult = vResult & "                     @P_GeneradoPorInt = @GeneradoPorCxCEnum," & vbCrLf
   vResult = vResult & "                     @P_NumeroDocumentoOrigen = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "                     @P_TipoDeComprobante = @TipoDeComprobante," & vbCrLf
   vResult = vResult & "                     @P_NumeroComprobanteGenerado = @NumeroComprobanteGenerado OUTPUT" & vbCrLf
   vResult = vResult & "            END" & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpS10CxCContabilizar = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10CxCContabilizar", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpS10CxCComprobanteContableSEL(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10CxCComprobanteContableSEL(), fSpParametersS10CxCComprobanteContableSEL(), fStatementsSpS10CxCComprobanteContableSEL(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10CxCComprobanteContableSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10CxCComprobanteContableSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10CxCComprobanteContableSEL(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10CxCComprobanteContableSEL(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10CxCComprobanteContableSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10CxCComprobanteContableSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10CxCComprobanteContableSEL() As String
   GetSpNameS10CxCComprobanteContableSEL = "S10_CxCComprobanteContableSEL"
End Function

Private Function fSpParametersS10CxCComprobanteContableSEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @CodigoTipoDeDocumentoCxC " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "         @NumeroCxC " & gUtilSQL.getVarCharTypeForDB(20) & vbCrLf
    fSpParametersS10CxCComprobanteContableSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10CxCComprobanteContableSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10CxCComprobanteContableSEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxCEnum " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxCChar " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @CountComprobantes " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @TipoCxc " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   
   vResult = vResult & "   IF NOT EXISTS(SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "      RAISERROR('No existe Compañía con Consecutivo Compañía:%d', 16, 1, @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT Numero FROM CxC WHERE ConsecutivoCompania = @ConsecutivoCompania AND TipoCxc = @TipoCxc AND Numero = @NumeroCxC)" & vbCrLf
   vResult = vResult & "         RAISERROR('No existe Cuenta por Cobrar número:%s', 16, 1, @NumeroCxC)" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SELECT @TipoCxc = TipoDeDocumento FROM TipoDeDocumentoLey WHERE (Codigo = @CodigoTipoDeDocumentoCxC)" & vbCrLf
   vResult = vResult & "         SET @NumeroDocumentoOrigen = @TipoCxc + CHAR(9) + @NumeroCxC" & vbCrLf
   vResult = vResult & "         SET @GeneradoPorCxCEnum = 9 --CxC" & vbCrLf
   vResult = vResult & "         SET @GeneradoPorCxCChar = dbo.Gf_EnumToDBValue(@GeneradoPorCxCEnum) --CxC" & vbCrLf
   vResult = vResult & "         SELECT @CountComprobantes = COUNT(COMPROBANTE.ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "            FROM COMPROBANTE INNER JOIN PERIODO" & vbCrLf
   vResult = vResult & "               ON COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "            WHERE PERIODO.ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "               AND COMPROBANTE.NoDocumentoOrigen = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "               AND COMPROBANTE.GeneradoPor = @GeneradoPorCxCChar" & vbCrLf
   vResult = vResult & "         IF (@CountComprobantes > 1)" & vbCrLf
   vResult = vResult & "            RAISERROR('Existe más de 1 Comprobante Contable para el documento deseado.', 16, 1)" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SELECT @ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "                  @NumeroComprobante = COMPROBANTE.Numero" & vbCrLf
   vResult = vResult & "               FROM COMPROBANTE INNER JOIN PERIODO" & vbCrLf
   vResult = vResult & "                  ON COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "               WHERE PERIODO.ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                  AND COMPROBANTE.NoDocumentoOrigen = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "                  AND COMPROBANTE.GeneradoPor = @GeneradoPorCxCChar" & vbCrLf
   vResult = vResult & "            EXEC Gp_S10_ComprobanteContableSEL @ConsecutivoPeriodo, @NumeroComprobante" & vbCrLf
   vResult = vResult & "         END" & vbCrLf
   vResult = vResult & "      END" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpS10CxCComprobanteContableSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10CxCComprobanteContableSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpS10CxCComprobanteAsientoSEL(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10CxCComprobanteAsientoSEL(), fSpParametersS10CxCComprobanteAsientoSEL(), fStatementsSpS10CxCComprobanteAsientoSEL(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10CxCComprobanteAsientoSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10CxCComprobanteAsientoSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10CxCComprobanteAsientoSEL(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10CxCComprobanteAsientoSEL(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10CxCComprobanteAsientoSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10CxCComprobanteAsientoSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10CxCComprobanteAsientoSEL() As String
   GetSpNameS10CxCComprobanteAsientoSEL = "S10_CxCComprobanteAsientoSEL"
End Function

Private Function fSpParametersS10CxCComprobanteAsientoSEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @CodigoTipoDeDocumentoCxC " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "         @NumeroCxC " & gUtilSQL.getVarCharTypeForDB(20) & vbCrLf
    fSpParametersS10CxCComprobanteAsientoSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10CxCComprobanteAsientoSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10CxCComprobanteAsientoSEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxCEnum " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxCChar " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @CountComprobantes " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @TipoCxc " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   
   vResult = vResult & "   IF NOT EXISTS(SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "      RAISERROR('No existe Compañía con Consecutivo Compañía:%d', 16, 1, @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT Numero FROM CxC WHERE ConsecutivoCompania = @ConsecutivoCompania AND TipoCxc = @TipoCxc AND Numero = @NumeroCxC)" & vbCrLf
   vResult = vResult & "         RAISERROR('No existe Cuenta por Cobrar número:%s', 16, 1, @NumeroCxC)" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SELECT @TipoCxc = TipoDeDocumento FROM TipoDeDocumentoLey WHERE (Codigo = @CodigoTipoDeDocumentoCxC)" & vbCrLf
   vResult = vResult & "         SET @NumeroDocumentoOrigen = @TipoCxc + CHAR(9) + @NumeroCxC" & vbCrLf
   vResult = vResult & "         SET @GeneradoPorCxCEnum = 9 --CxC" & vbCrLf
   vResult = vResult & "         SET @GeneradoPorCxCChar = dbo.Gf_EnumToDBValue(@GeneradoPorCxCEnum) --CxC" & vbCrLf
   vResult = vResult & "         SELECT @CountComprobantes = COUNT(COMPROBANTE.ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "            FROM COMPROBANTE INNER JOIN PERIODO" & vbCrLf
   vResult = vResult & "               ON COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "            WHERE PERIODO.ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "               AND COMPROBANTE.NoDocumentoOrigen = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "               AND COMPROBANTE.GeneradoPor = @GeneradoPorCxCChar" & vbCrLf
   vResult = vResult & "         IF (@CountComprobantes > 1)" & vbCrLf
   vResult = vResult & "            RAISERROR('Existe más de 1 Comprobante Contable para el documento deseado.', 16, 1)" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SELECT @ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "                  @NumeroComprobante = COMPROBANTE.Numero" & vbCrLf
   vResult = vResult & "               FROM COMPROBANTE INNER JOIN PERIODO" & vbCrLf
   vResult = vResult & "                  ON COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "               WHERE PERIODO.ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                  AND COMPROBANTE.NoDocumentoOrigen = @NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & "                  AND COMPROBANTE.GeneradoPor = @GeneradoPorCxCChar" & vbCrLf
   vResult = vResult & "            EXEC Gp_S10_ComprobanteAsientoSEL @ConsecutivoPeriodo, @NumeroComprobante" & vbCrLf
   vResult = vResult & "         END" & vbCrLf
   vResult = vResult & "      END" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpS10CxCComprobanteAsientoSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10CxCComprobanteAsientoSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpCxPContabilizar(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameCxPContabilizar(), fSpParametersCxPContabilizar(), fStatementsSpCxPContabilizar(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpCxPContabilizar = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpCxPContabilizar", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fDropSpCxPContabilizar(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameCxPContabilizar(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpCxPContabilizar = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpCxPContabilizar", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameCxPContabilizar() As String
   GetSpNameCxPContabilizar = "CxPContabilizar"
End Function

Private Function fSpParametersCxPContabilizar() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "      @FechaCxP " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "      @NumeroCxP " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "      @CuentaContableDEBEGastos " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "      @CuentaContableDEBEIVACreditoFiscal " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "      @CuentaContableHABERCxPProveedor " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "      @CuentaContableHABERRetencionIVA " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "      @Operador " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "      @NumeroComprobanteGenerado " & gUtilSQL.getVarCharTypeForDB(8) & " = '' OUTPUT" & vbCrLf
   fSpParametersCxPContabilizar = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersCxPContabilizar", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpCxPContabilizar() As String
   Dim vResult As String
   On Error GoTo h_ERROR
'   vResult = vResult & "/*" & vbCrLf
'   vResult = vResult & "         DEBE  HABER" & vbCrLf
'   vResult = vResult & "GASTOS      XXX" & vbCrLf
'   vResult = vResult & "IVA CF      XXX" & vbCrLf
'   vResult = vResult & "CxP Prov       XXX" & vbCrLf
'   vResult = vResult & "RET IVA           XXX" & vbCrLf
'   vResult = vResult & "*/" & vbCrLf
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @CompaniaUsaAuxiliares char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxPChar char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxPEnum " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @DescripcionComprobante " & gUtilSQL.getVarCharTypeForDB(160) & "" & vbCrLf
   vResult = vResult & "   DECLARE @TipoDeComprobante char(2)" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoCxP " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @TipoDeContribuyenteIva char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @CCDebeGastos " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CCDebeIVACreditoFiscal " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CCHaberCxPProveedor " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CCHaberRetencionIVA " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @DondeContabilizarRetIva char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @MontoDebeGastos money" & vbCrLf
   vResult = vResult & "   DECLARE @MontoDebeIVACreditoFiscal money" & vbCrLf
   vResult = vResult & "   DECLARE @MontoHaberCxPProveedor money" & vbCrLf
   vResult = vResult & "   DECLARE @MontoHaberRetencionIVA money" & vbCrLf
   vResult = vResult & "   DECLARE @CCDGastos " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CCDIVACF " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CCHCxPProv " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CCHRetIVA " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Continue bit" & vbCrLf
   vResult = vResult & "   SET @Continue = 1 --True" & vbCrLf
   vResult = vResult & "   IF NOT EXISTS (SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @Continue = 0 --False" & vbCrLf
   vResult = vResult & "      RAISERROR('No existe Compañía para ConsecutivoCompania:%d', 16, 1, @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT @CompaniaUsaAuxiliares = UsaAuxiliares, @TipoDeContribuyenteIva = TipoDeContribuyenteIva" & vbCrLf
   vResult = vResult & "         FROM Compania" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   IF NOT EXISTS (SELECT ConsecutivoCxp FROM cxP WHERE ConsecutivoCompania = @ConsecutivoCompania AND Numero = @NumeroCxP AND CodigoProveedor = @CodigoProveedor)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @Continue = 0 --False" & vbCrLf
   vResult = vResult & "      RAISERROR('No existe CxP para los datos suministrados', 16, 1)" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT @ConsecutivoCxP = ConsecutivoCxp" & vbCrLf
   vResult = vResult & "         FROM cxP" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoCompania = @ConsecutivoCompania AND Numero = @NumeroCxP AND CodigoProveedor = @CodigoProveedor" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   IF NOT EXISTS(SELECT ConsecutivoPeriodo FROM Periodo WHERE ConsecutivoCompania = @ConsecutivoCompania AND FechaAperturaDelPeriodo <= @FechaCxP AND FechaCierreDelPeriodo >= @FechaCxP AND PeriodoCerrado = 'N')" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @Continue = 0 --False" & vbCrLf
   vResult = vResult & "      RAISERROR('No existe Consecutivo Período válido para la fecha del documento.', 16, 1)" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT @ConsecutivoPeriodo = ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "         FROM Periodo" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoCompania = @ConsecutivoCompania AND FechaAperturaDelPeriodo <= @FechaCxP AND FechaCierreDelPeriodo >= @FechaCxP AND PeriodoCerrado = 'N'" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   IF (@Continue = 1) --True" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT TOP 1 @CCDGastos = CuentaCxPgasto," & vbCrLf
   vResult = vResult & "               @CCDIVACF = CuentaIva1Credito," & vbCrLf
   vResult = vResult & "               @CCHCxPProv = CuentaCxPproveedores," & vbCrLf
   vResult = vResult & "               @CCHRetIVA = CuentaRetencionIva," & vbCrLf
   vResult = vResult & "               @DondeContabilizarRetIva = DondeContabilizarRetIva," & vbCrLf
   vResult = vResult & "               @TipoDeComprobante = CxPtipoComprobante" & vbCrLf
   vResult = vResult & "         FROM reglasDeContabilizacion" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "      IF (@CuentaContableDEBEGastos = '')" & vbCrLf
   vResult = vResult & "         SET @CCDebeGastos = @CCDGastos" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "         SET @CCDebeGastos = @CuentaContableDEBEGastos" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT Codigo FROM CUENTA WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Codigo = @CCDebeGastos)" & vbCrLf
   vResult = vResult & "         SET @CCDebeGastos = ''" & vbCrLf
   vResult = vResult & "      IF (@CuentaContableDEBEIVACreditoFiscal = '')" & vbCrLf
   vResult = vResult & "         SET @CCDebeIVACreditoFiscal = @CCDIVACF" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "         SET @CCDebeIVACreditoFiscal = @CuentaContableDEBEIVACreditoFiscal" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT Codigo FROM CUENTA WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Codigo = @CCDebeIVACreditoFiscal)" & vbCrLf
   vResult = vResult & "         SET @CCDebeIVACreditoFiscal = ''" & vbCrLf
   vResult = vResult & "      IF (@CuentaContableHABERCxPProveedor = '')" & vbCrLf
   vResult = vResult & "         SET @CCHaberCxPProveedor = @CCHCxPProv" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "         SET @CCHaberCxPProveedor = @CuentaContableHABERCxPProveedor" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT Codigo FROM CUENTA WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Codigo = @CCHaberCxPProveedor)" & vbCrLf
   vResult = vResult & "         SET @CCHaberCxPProveedor = ''" & vbCrLf
   vResult = vResult & "      SET @CCHaberRetencionIVA = ''" & vbCrLf
   vResult = vResult & "      IF (@TipoDeContribuyenteIva = '2') --CONTRIBUYENTE_ESPECIAL" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         IF (@CuentaContableHABERRetencionIVA = '')" & vbCrLf
   vResult = vResult & "            SET @CCHaberRetencionIVA = @CCHRetIVA" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "            SET @CCHaberRetencionIVA = @CuentaContableHABERRetencionIVA" & vbCrLf
   vResult = vResult & "         IF NOT EXISTS(SELECT Codigo FROM CUENTA WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Codigo = @CCHaberRetencionIVA)" & vbCrLf
   vResult = vResult & "            SET @CCHaberRetencionIVA = ''" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "      IF (@TipoDeContribuyenteIva = '2') --CONTRIBUYENTE_ESPECIAL" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         IF (@CCDebeGastos = '') OR (@CCDebeIVACreditoFiscal = '') OR (@CCHaberCxPProveedor = '') OR (@CCHaberRetencionIVA = '')" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @Continue = 0 --False" & vbCrLf
   vResult = vResult & "            RAISERROR('No se puede contabilizar esta CxP ya que las cuentas contables no están completas.', 16, 1)" & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         IF (@CCDebeGastos = '') OR (@CCDebeIVACreditoFiscal = '') OR (@CCHaberCxPProveedor = '')" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @Continue = 0 --False" & vbCrLf
   vResult = vResult & "            RAISERROR('No se puede contabilizar esta CxP ya que las cuentas contables no están completas.', 16, 1)" & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   IF (@Continue = 1) --True" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      IF @TipoDeComprobante IS NULL" & vbCrLf
   vResult = vResult & "         SET @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "      IF @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SELECT @TipoDeComprobante = CodigoDelTipo FROM tipoDeComprobante WHERE NombreDelTipo = 'Cuentas x Pagar'" & vbCrLf
   vResult = vResult & "         IF @TipoDeComprobante IS NULL" & vbCrLf
   vResult = vResult & "            SET @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "      IF @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "         SET @TipoDeComprobante = '04'" & vbCrLf
   vResult = vResult & "      SET @NumeroDocumentoOrigen = CAST(@ConsecutivoCxP AS varchar)" & vbCrLf
   vResult = vResult & "      SET @GeneradoPorCxPEnum = 11 --CxP" & vbCrLf
   vResult = vResult & "      SET @GeneradoPorCxPChar = dbo.Gf_EnumToDBValue(@GeneradoPorCxPEnum) --CxP" & vbCrLf
   vResult = vResult & "      SELECT @NumeroComprobante = Numero" & vbCrLf
   vResult = vResult & "         FROM Comprobante" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND NoDocumentoOrigen = @NumeroDocumentoOrigen AND GeneradoPor = @GeneradoPorCxPChar" & vbCrLf
   vResult = vResult & "      IF @NumeroComprobante IS NULL" & vbCrLf
   vResult = vResult & "         SET @NumeroComprobante = ''" & vbCrLf
   vResult = vResult & "      SET @DescripcionComprobante = 'CxP N°' + @NumeroCxP + ' Proveedor ' + @CodigoProveedor" & vbCrLf
   vResult = vResult & "      SELECT @MontoDebeGastos = (CambioAbolivares * (MontoExento + MontoGravado))," & vbCrLf
   vResult = vResult & "            @MontoDebeIVACreditoFiscal = (CambioAbolivares * MontoIva)," & vbCrLf
   vResult = vResult & "            @MontoHaberCxPProveedor = (CambioAbolivares * (MontoGravado + MontoIva + MontoExento + TotalOtrosImpuestos))," & vbCrLf
   vResult = vResult & "            @MontoHaberRetencionIVA = (CambioAbolivares * MontoRetenido)" & vbCrLf
   vResult = vResult & "         FROM cxP" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCxp = @ConsecutivoCxP" & vbCrLf
   vResult = vResult & "      EXEC Gp_AsientoComprobanteIMP" & vbCrLf
   vResult = vResult & "         @NombreOperador = @Operador," & vbCrLf
   vResult = vResult & "         @P_ConsecutivoPeriodo = @ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "         @P_NumeroComprobante = ''," & vbCrLf
   vResult = vResult & "         @P_FechaComprobante = @FechaCxP," & vbCrLf
   vResult = vResult & "         @P_DescripcionComprobante = @DescripcionComprobante," & vbCrLf
   vResult = vResult & "         @P_CodigoCuenta = @CCDebeGastos," & vbCrLf
   vResult = vResult & "         @P_DescripcionCuenta = ''," & vbCrLf
   vResult = vResult & "         @P_DebeHaber = 'D'," & vbCrLf
   vResult = vResult & "         @P_Monto = @MontoDebeGastos," & vbCrLf
   vResult = vResult & "         @P_FechaDeReferencia = @FechaCxP," & vbCrLf
   vResult = vResult & "         @P_NumeroReferencia = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "         @P_DescripcionAsiento = @DescripcionComprobante," & vbCrLf
   vResult = vResult & "         @P_GeneradoPorInt = @GeneradoPorCxPEnum," & vbCrLf
   vResult = vResult & "         @P_NumeroDocumentoOrigen = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "         @P_TipoDeComprobante = @TipoDeComprobante," & vbCrLf
   vResult = vResult & "         @P_NumeroComprobanteGenerado = @NumeroComprobanteGenerado OUTPUT" & vbCrLf
   vResult = vResult & "      EXEC Gp_AsientoComprobanteIMP" & vbCrLf
   vResult = vResult & "         @NombreOperador = @Operador," & vbCrLf
   vResult = vResult & "         @P_ConsecutivoPeriodo = @ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "         @P_NumeroComprobante = @NumeroComprobanteGenerado," & vbCrLf
   vResult = vResult & "         @P_FechaComprobante = @FechaCxP," & vbCrLf
   vResult = vResult & "         @P_DescripcionComprobante = @DescripcionComprobante," & vbCrLf
   vResult = vResult & "         @P_CodigoCuenta = @CCDebeIVACreditoFiscal," & vbCrLf
   vResult = vResult & "         @P_DescripcionCuenta = ''," & vbCrLf
   vResult = vResult & "         @P_DebeHaber = 'D'," & vbCrLf
   vResult = vResult & "         @P_Monto = @MontoDebeIVACreditoFiscal," & vbCrLf
   vResult = vResult & "         @P_FechaDeReferencia = @FechaCxP," & vbCrLf
   vResult = vResult & "         @P_NumeroReferencia = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "         @P_DescripcionAsiento = @DescripcionComprobante," & vbCrLf
   vResult = vResult & "         @P_GeneradoPorInt = @GeneradoPorCxPEnum," & vbCrLf
   vResult = vResult & "         @P_NumeroDocumentoOrigen = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "         @P_TipoDeComprobante = @TipoDeComprobante" & vbCrLf
   vResult = vResult & "      EXEC Gp_AsientoComprobanteIMP" & vbCrLf
   vResult = vResult & "         @NombreOperador = @Operador," & vbCrLf
   vResult = vResult & "         @P_ConsecutivoPeriodo = @ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "         @P_NumeroComprobante = @NumeroComprobanteGenerado," & vbCrLf
   vResult = vResult & "         @P_FechaComprobante = @FechaCxP," & vbCrLf
   vResult = vResult & "         @P_DescripcionComprobante = @DescripcionComprobante," & vbCrLf
   vResult = vResult & "         @P_CodigoCuenta = @CCHaberCxPProveedor," & vbCrLf
   vResult = vResult & "         @P_DescripcionCuenta = ''," & vbCrLf
   vResult = vResult & "         @P_DebeHaber = 'D'," & vbCrLf
   vResult = vResult & "         @P_Monto = @MontoHaberCxPProveedor," & vbCrLf
   vResult = vResult & "         @P_FechaDeReferencia = @FechaCxP," & vbCrLf
   vResult = vResult & "         @P_NumeroReferencia = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "         @P_DescripcionAsiento = @DescripcionComprobante," & vbCrLf
   vResult = vResult & "         @P_GeneradoPorInt = @GeneradoPorCxPEnum," & vbCrLf
   vResult = vResult & "         @P_NumeroDocumentoOrigen = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "         @P_TipoDeComprobante = @TipoDeComprobante" & vbCrLf
   vResult = vResult & "      IF (@TipoDeContribuyenteIva <> '2') --CONTRIBUYENTE_ESPECIAL" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         EXEC Gp_AsientoComprobanteIMP" & vbCrLf
   vResult = vResult & "            @NombreOperador = @Operador," & vbCrLf
   vResult = vResult & "            @P_ConsecutivoPeriodo = @ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "            @P_NumeroComprobante = @NumeroComprobanteGenerado," & vbCrLf
   vResult = vResult & "            @P_FechaComprobante = @FechaCxP," & vbCrLf
   vResult = vResult & "            @P_DescripcionComprobante = @DescripcionComprobante," & vbCrLf
   vResult = vResult & "            @P_CodigoCuenta = @CCHaberRetencionIVA," & vbCrLf
   vResult = vResult & "            @P_DescripcionCuenta = ''," & vbCrLf
   vResult = vResult & "            @P_DebeHaber = 'D'," & vbCrLf
   vResult = vResult & "            @P_Monto = @MontoHaberRetencionIVA," & vbCrLf
   vResult = vResult & "            @P_FechaDeReferencia = @FechaCxP," & vbCrLf
   vResult = vResult & "            @P_NumeroReferencia = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "            @P_DescripcionAsiento = @DescripcionComprobante," & vbCrLf
   vResult = vResult & "            @P_GeneradoPorInt = @GeneradoPorCxPEnum," & vbCrLf
   vResult = vResult & "            @P_NumeroDocumentoOrigen = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "            @P_TipoDeComprobante = @TipoDeComprobante" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpCxPContabilizar = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpCxPContabilizar", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpCxCContabilizar(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameCxCContabilizar(), fSpParametersCxCContabilizar(), fStatementsSpCxCContabilizar(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpCxCContabilizar = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpCxCContabilizar", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fDropSpCxCContabilizar(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameCxCContabilizar(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpCxCContabilizar = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpCxCContabilizar", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameCxCContabilizar() As String
   GetSpNameCxCContabilizar = "CxCContabilizar"
End Function

Private Function fSpParametersCxCContabilizar() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "      @FechaCxC " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @NumeroCxC " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "      @TipoCxc char(1)," & vbCrLf
   vResult = vResult & "      @CuentaContableDEBECxCCliente " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "      @CuentaContableHABERIVADebitoFiscal " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "      @CuentaContableHABERIngresos " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "      @Operador " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "      @NumeroComprobanteGenerado " & gUtilSQL.getVarCharTypeForDB(8) & " = '' OUTPUT" & vbCrLf
   fSpParametersCxCContabilizar = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersCxCContabilizar", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpCxCContabilizar() As String
   Dim vResult As String
   On Error GoTo h_ERROR
'   vResult = vResult & "/*" & vbCrLf
'   vResult = vResult & "         DEBE  HABER"  & vbCrLf
'   vResult = vResult & "CxC Cli  XXX"          & vbCrLf
'   vResult = vResult & "IVA DF         XXX"    & vbCrLf
'   vResult = vResult & "INGRESOS       XXX"    & vbCrLf
'   vResult = vResult & "*/" & vbCrLf
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @CompaniaUsaAuxiliares char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & "" & vbCrLf
   vResult = vResult & "   DECLARE @NumeroDocumentoOrigen " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxCChar char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @GeneradoPorCxCEnum " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @DescripcionComprobante " & gUtilSQL.getVarCharTypeForDB(160) & "" & vbCrLf
   vResult = vResult & "   DECLARE @TipoDeComprobante char(2)" & vbCrLf
   vResult = vResult & "   DECLARE @CCDebeCxCCliente " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CCHaberIVADebitoFiscal " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CCHaberIngresos " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @MontoDebeCxCCliente money" & vbCrLf
   vResult = vResult & "   DECLARE @MontoHaberIVADebitoFiscal money" & vbCrLf
   vResult = vResult & "   DECLARE @MontoHaberIngresos money" & vbCrLf
   vResult = vResult & "   DECLARE @CCDCxCCliente " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CCHIVADebitoFiscal " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CCHIngresos " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Continue bit" & vbCrLf
   vResult = vResult & "   SET @Continue = 1 --True" & vbCrLf
   vResult = vResult & "   IF NOT EXISTS (SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @Continue = 0 --False" & vbCrLf
   vResult = vResult & "      RAISERROR('No existe Compañía para ConsecutivoCompania:%d', 16, 1, @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT @CompaniaUsaAuxiliares = UsaAuxiliares" & vbCrLf
   vResult = vResult & "         FROM Compania" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   IF NOT EXISTS (SELECT Numero FROM CxC WHERE ConsecutivoCompania = @ConsecutivoCompania AND Numero = @NumeroCxC AND TipoCxc = @TipoCxc)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @Continue = 0 --False" & vbCrLf
   vResult = vResult & "      RAISERROR('No existe CxC para los datos suministrados', 16, 1)" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   IF NOT EXISTS(SELECT ConsecutivoPeriodo FROM Periodo WHERE ConsecutivoCompania = @ConsecutivoCompania AND FechaAperturaDelPeriodo <= @FechaCxC AND FechaCierreDelPeriodo >= @FechaCxC AND PeriodoCerrado = 'N')" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @Continue = 0 --False" & vbCrLf
   vResult = vResult & "      RAISERROR('No existe Consecutivo Período válido para la fecha del documento.', 16, 1)" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT @ConsecutivoPeriodo = ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "         FROM Periodo" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoCompania = @ConsecutivoCompania AND FechaAperturaDelPeriodo <= @FechaCxC AND FechaCierreDelPeriodo >= @FechaCxC AND PeriodoCerrado = 'N'" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   IF (@Continue = 1) --True" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT TOP 1 @CCDCxCCliente = CuentaCxCclientes," & vbCrLf
   vResult = vResult & "               @CCHIVADebitoFiscal = CuentaIva1Debito," & vbCrLf
   vResult = vResult & "               @CCHIngresos = CuentaCxCingresos," & vbCrLf
   vResult = vResult & "               @TipoDeComprobante = CxCtipoComprobante" & vbCrLf
   vResult = vResult & "         FROM reglasDeContabilizacion" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "      IF (@CuentaContableDEBECxCCliente = '')" & vbCrLf
   vResult = vResult & "         SET @CCDebeCxCCliente = @CCDCxCCliente" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "         SET @CCDebeCxCCliente = @CuentaContableDEBECxCCliente" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT Codigo FROM CUENTA WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Codigo = @CCDebeCxCCliente)" & vbCrLf
   vResult = vResult & "         SET @CCDebeCxCCliente = ''" & vbCrLf
   vResult = vResult & "      IF (@CuentaContableHABERIVADebitoFiscal = '')" & vbCrLf
   vResult = vResult & "         SET @CCHaberIVADebitoFiscal = @CCHIVADebitoFiscal" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "         SET @CCHaberIVADebitoFiscal = @CuentaContableHABERIVADebitoFiscal" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT Codigo FROM CUENTA WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Codigo = @CCHaberIVADebitoFiscal)" & vbCrLf
   vResult = vResult & "         SET @CCHaberIVADebitoFiscal = ''" & vbCrLf
   vResult = vResult & "      IF (@CuentaContableHABERIngresos = '')" & vbCrLf
   vResult = vResult & "         SET @CCHaberIngresos = @CCHIngresos" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "         SET @CCHaberIngresos = @CuentaContableHABERIngresos" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT Codigo FROM CUENTA WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Codigo = @CCHaberIngresos)" & vbCrLf
   vResult = vResult & "         SET @CCHaberIngresos = ''" & vbCrLf
   vResult = vResult & "      IF (@CCDebeCxCCliente = '') OR (@CCHaberIVADebitoFiscal = '') OR (@CCHaberIngresos = '')" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SET @Continue = 0 --False" & vbCrLf
   vResult = vResult & "         RAISERROR('No se puede contabilizar esta CxC ya que las cuentas contables no están completas.', 16, 1)" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   IF (@Continue = 1) --True" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      IF @TipoDeComprobante IS NULL" & vbCrLf
   vResult = vResult & "         SET @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "      IF @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SELECT @TipoDeComprobante = CodigoDelTipo FROM tipoDeComprobante WHERE NombreDelTipo = 'Cuentas x Cobrar'" & vbCrLf
   vResult = vResult & "         IF @TipoDeComprobante IS NULL" & vbCrLf
   vResult = vResult & "            SET @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "      IF @TipoDeComprobante = ''" & vbCrLf
   vResult = vResult & "         SET @TipoDeComprobante = '03'" & vbCrLf
   vResult = vResult & "      SET @NumeroDocumentoOrigen = @TipoCxc + char(9) + @NumeroCxC" & vbCrLf
   vResult = vResult & "      SET @GeneradoPorCxCEnum = 9 --CxC" & vbCrLf
   vResult = vResult & "      SET @GeneradoPorCxCChar = dbo.Gf_EnumToDBValue(@GeneradoPorCxCEnum) --CxC" & vbCrLf
   vResult = vResult & "      SELECT @NumeroComprobante = Numero" & vbCrLf
   vResult = vResult & "         FROM Comprobante" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND NoDocumentoOrigen = @NumeroDocumentoOrigen AND GeneradoPor = @GeneradoPorCxCChar" & vbCrLf
   vResult = vResult & "      IF @NumeroComprobante IS NULL" & vbCrLf
   vResult = vResult & "         SET @NumeroComprobante = ''" & vbCrLf
   vResult = vResult & "      SET @DescripcionComprobante = 'CxC N°' + @NumeroCxC + ' Tipo ' + @TipoCxc" & vbCrLf
   vResult = vResult & "      SELECT @MontoDebeCxCCliente = (CambioAbolivares * (MontoExento + MontoGravado + MontoIva))," & vbCrLf
   vResult = vResult & "            @MontoHaberIVADebitoFiscal = (CambioAbolivares * MontoIva)," & vbCrLf
   vResult = vResult & "            @MontoHaberIngresos  = (CambioAbolivares * (MontoExento + MontoGravado))" & vbCrLf
   vResult = vResult & "         FROM CxC" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoCompania = @ConsecutivoCompania AND Numero = @NumeroCxC AND TipoCxc = @TipoCxc" & vbCrLf
   vResult = vResult & "      EXEC Gp_AsientoComprobanteIMP" & vbCrLf
   vResult = vResult & "         @NombreOperador = @Operador," & vbCrLf
   vResult = vResult & "         @P_ConsecutivoPeriodo = @ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "         @P_NumeroComprobante = ''," & vbCrLf
   vResult = vResult & "         @P_FechaComprobante = @FechaCxC," & vbCrLf
   vResult = vResult & "         @P_DescripcionComprobante = @DescripcionComprobante," & vbCrLf
   vResult = vResult & "         @P_CodigoCuenta = @CCDebeCxCCliente," & vbCrLf
   vResult = vResult & "         @P_DescripcionCuenta = ''," & vbCrLf
   vResult = vResult & "         @P_DebeHaber = 'D'," & vbCrLf
   vResult = vResult & "         @P_Monto = @MontoDebeCxCCliente," & vbCrLf
   vResult = vResult & "         @P_FechaDeReferencia = @FechaCxC," & vbCrLf
   vResult = vResult & "         @P_NumeroReferencia = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "         @P_DescripcionAsiento = @DescripcionComprobante," & vbCrLf
   vResult = vResult & "         @P_GeneradoPorInt = @GeneradoPorCxCEnum," & vbCrLf
   vResult = vResult & "         @P_NumeroDocumentoOrigen = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "         @P_TipoDeComprobante = @TipoDeComprobante," & vbCrLf
   vResult = vResult & "         @P_NumeroComprobanteGenerado = @NumeroComprobanteGenerado OUTPUT" & vbCrLf
   vResult = vResult & "      EXEC Gp_AsientoComprobanteIMP" & vbCrLf
   vResult = vResult & "         @NombreOperador = @Operador," & vbCrLf
   vResult = vResult & "         @P_ConsecutivoPeriodo = @ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "         @P_NumeroComprobante = @NumeroComprobanteGenerado," & vbCrLf
   vResult = vResult & "         @P_FechaComprobante = @FechaCxC," & vbCrLf
   vResult = vResult & "         @P_DescripcionComprobante = @DescripcionComprobante," & vbCrLf
   vResult = vResult & "         @P_CodigoCuenta = @CCHaberIVADebitoFiscal," & vbCrLf
   vResult = vResult & "         @P_DescripcionCuenta = ''," & vbCrLf
   vResult = vResult & "         @P_DebeHaber = 'H'," & vbCrLf
   vResult = vResult & "         @P_Monto = @MontoHaberIVADebitoFiscal," & vbCrLf
   vResult = vResult & "         @P_FechaDeReferencia = @FechaCxC," & vbCrLf
   vResult = vResult & "         @P_NumeroReferencia = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "         @P_DescripcionAsiento = @DescripcionComprobante," & vbCrLf
   vResult = vResult & "         @P_GeneradoPorInt = @GeneradoPorCxCEnum," & vbCrLf
   vResult = vResult & "         @P_NumeroDocumentoOrigen = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "         @P_TipoDeComprobante = @TipoDeComprobante" & vbCrLf
   vResult = vResult & "      EXEC Gp_AsientoComprobanteIMP" & vbCrLf
   vResult = vResult & "         @NombreOperador = @Operador," & vbCrLf
   vResult = vResult & "         @P_ConsecutivoPeriodo = @ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "         @P_NumeroComprobante = @NumeroComprobanteGenerado," & vbCrLf
   vResult = vResult & "         @P_FechaComprobante = @FechaCxC," & vbCrLf
   vResult = vResult & "         @P_DescripcionComprobante = @DescripcionComprobante," & vbCrLf
   vResult = vResult & "         @P_CodigoCuenta = @CCHaberIngresos," & vbCrLf
   vResult = vResult & "         @P_DescripcionCuenta = ''," & vbCrLf
   vResult = vResult & "         @P_DebeHaber = 'H'," & vbCrLf
   vResult = vResult & "         @P_Monto = @MontoHaberIngresos," & vbCrLf
   vResult = vResult & "         @P_FechaDeReferencia = @FechaCxC," & vbCrLf
   vResult = vResult & "         @P_NumeroReferencia = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "         @P_DescripcionAsiento = @DescripcionComprobante," & vbCrLf
   vResult = vResult & "         @P_GeneradoPorInt = @GeneradoPorCxCEnum," & vbCrLf
   vResult = vResult & "         @P_NumeroDocumentoOrigen = @NumeroDocumentoOrigen," & vbCrLf
   vResult = vResult & "         @P_TipoDeComprobante = @TipoDeComprobante" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpCxCContabilizar = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpCxCContabilizar", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *

