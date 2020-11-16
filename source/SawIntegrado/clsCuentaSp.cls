VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCuentaSp"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsCuentaSp"
End Function

Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsCuentaSp"
End Function

Public Function fCreateDropCuentaSP(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      vResult = vResult And fCreateSpINS(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnNivelesDefinicionCuenta(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnCuadraElCodigoConLosNiveles(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnAdjustCode(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnLaCuentaEstaENDefiniconCuentas(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnLaCuentaDelNivelAnteriorEsDeMovimiento(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnExistsCuenta(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnEsCuentaDeUltimoNivel(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnCuentaTieneSubCuenta(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnCuentaIsValidForImport(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpCrearCuentasDeNivelesAnterioresSiAplica(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpIMP(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnExistsCuentaInConciliacionContable(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnS10Exists(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10Insert(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpInvS10CuentaINS(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpInvS10CuentaUPD(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpInvS10CuentaDEL(valShowMessage, valWithEncryption)
   Else
      vResult = vResult And fDropSpInvS10CuentaDEL(valShowMessage)
      vResult = vResult And fDropSpInvS10CuentaUPD(valShowMessage)
      vResult = vResult And fDropSpInvS10CuentaINS(valShowMessage)
      vResult = vResult And fDropSpS10Insert(valShowMessage)
      vResult = vResult And fDropFnS10Exists(valShowMessage)
      vResult = vResult And fDropFnExistsCuentaInConciliacionContable(valShowMessage)
      vResult = vResult And fDropSpIMP(valShowMessage)
      vResult = vResult And fDropSpCrearCuentasDeNivelesAnterioresSiAplica(valShowMessage)
      vResult = vResult And fDropFnCuentaIsValidForImport(valShowMessage)
      vResult = vResult And fDropFnCuentaTieneSubCuenta(valShowMessage)
      vResult = vResult And fDropFnEsCuentaDeUltimoNivel(valShowMessage)
      vResult = vResult And fDropFnExistsCuenta(valShowMessage)
      vResult = vResult And fDropFnLaCuentaDelNivelAnteriorEsDeMovimiento(valShowMessage)
      vResult = vResult And fDropFnLaCuentaEstaENDefiniconCuentas(valShowMessage)
      vResult = vResult And fDropFnAdjustCode(valShowMessage)
      vResult = vResult And fDropFnCuadraElCodigoConLosNiveles(valShowMessage)
      vResult = vResult And fDropFnNivelesDefinicionCuenta(valShowMessage)
      vResult = vResult And fDropSpINS(valShowMessage)
   End If
   fCreateDropCuentaSP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropCuentaSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetSpNameINS = "CuentaINS"
End Function

Private Function fSpParametersINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @ConsecutivoPeriodo int," & vbCrLf
   vResult = vResult & "      @Codigo varchar(30)," & vbCrLf
   vResult = vResult & "      @Descripcion varchar(40)," & vbCrLf
   vResult = vResult & "      @NaturalezaDeLaCuenta char(1) = '0'," & vbCrLf
   vResult = vResult & "      @TieneSubCuentas char(1) = 'N'," & vbCrLf
   vResult = vResult & "      @TieneAuxiliar char(1) = 'N'," & vbCrLf
   vResult = vResult & "      @GrupoAuxiliar char(1) = '3'," & vbCrLf
   vResult = vResult & "      @VieneDelPeriodoAnterior char(1) = 'N'," & vbCrLf
   vResult = vResult & "      @StatusCuenta char(1) = '0'," & vbCrLf
   vResult = vResult & "      @MetAjusteYactivoStr varchar(4) = ''," & vbCrLf
   vResult = vResult & "      @TipoDeInventario char(1) = '0'," & vbCrLf
   vResult = vResult & "      @SeccionEstadoDemostrativo char(1) = '0'," & vbCrLf
   vResult = vResult & "      @CostoDeVentas char(1) = '0'," & vbCrLf
   vResult = vResult & "      @Gastos char(1) = '0'," & vbCrLf
   vResult = vResult & "      @IngresosPropiosActividad char(1) = '0'," & vbCrLf
   vResult = vResult & "      @ConciliacionInfo2000 char(1) = '0'," & vbCrLf
   vResult = vResult & "      @EsActivoFijo char(1) = 'N'," & vbCrLf
   vResult = vResult & "      @CodigoGrupoActivo int = 0," & vbCrLf
   vResult = vResult & "      @SaldoInicial money = 0," & vbCrLf
   vResult = vResult & "      @TipoDeCuentaFiscal char(1) = '0'," & vbCrLf
   vResult = vResult & "      @TipoDeCuentaDpc10 char(1) = '0'," & vbCrLf
   vResult = vResult & "      @Dpc10MetodoAjuste char(1) = '0'," & vbCrLf
   vResult = vResult & "      @MetodoAjuste char(1) = '0'," & vbCrLf
   vResult = vResult & "      @DpcCodigoGrupoActivoFijo int = 0," & vbCrLf
   vResult = vResult & "      @CodigoGrupoAjustaPorIpc int  = 0," & vbCrLf
   vResult = vResult & "      @TipoDeOtrasNoMonetarias char(1) = '0'," & vbCrLf
   vResult = vResult & "      @TipoRefleja char(1) = '0'," & vbCrLf
   vResult = vResult & "      @CuentaReflejaAlDebe varchar(30) = ''," & vbCrLf
   vResult = vResult & "      @CuentaReflejaAlHaber varchar(30) = ''," & vbCrLf
   vResult = vResult & "      @NombreOperador varchar(10)" & vbCrLf
   fSpParametersINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @FechaUltimaModificacion smalldatetime"
   vResult = vResult & "   SET @FechaUltimaModificacion = CONVERT(smalldatetime, GETDATE(),103)"
   vResult = vResult & "   INSERT INTO Cuenta(" & vbCrLf
   vResult = vResult & "         ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "         Codigo," & vbCrLf
   vResult = vResult & "         Descripcion," & vbCrLf
   vResult = vResult & "         NaturalezaDeLaCuenta," & vbCrLf
   vResult = vResult & "         TieneSubCuentas," & vbCrLf
   vResult = vResult & "         TieneAuxiliar," & vbCrLf
   vResult = vResult & "         GrupoAuxiliar," & vbCrLf
   vResult = vResult & "         vieneDelPeriodoAnterior," & vbCrLf
   vResult = vResult & "         StatusCuenta," & vbCrLf
   vResult = vResult & "         MetAjusteYActivoStr," & vbCrLf
   vResult = vResult & "         TipoDeInventario," & vbCrLf
   vResult = vResult & "         SeccionEstadoDemostrativo," & vbCrLf
   vResult = vResult & "         CostoDeVentas," & vbCrLf
   vResult = vResult & "         Gastos," & vbCrLf
   vResult = vResult & "         IngresosPropiosActividad," & vbCrLf
   vResult = vResult & "         ConciliacionInfo2000," & vbCrLf
   vResult = vResult & "         EsActivoFijo," & vbCrLf
   vResult = vResult & "         CodigoGrupoActivo," & vbCrLf
   vResult = vResult & "         SaldoInicial," & vbCrLf
   vResult = vResult & "         TipoDeCuentaFiscal," & vbCrLf
   vResult = vResult & "         TipoDeCuentaDpc10," & vbCrLf
   vResult = vResult & "         Dpc10MetodoAjuste," & vbCrLf
   vResult = vResult & "         MetodoAjuste," & vbCrLf
   vResult = vResult & "         DpcCodigoGrupoActivoFijo," & vbCrLf
   vResult = vResult & "         CodigoGrupoAjustaPorIpc," & vbCrLf
   vResult = vResult & "         TipoDeOtrasNoMonetarias," & vbCrLf
   vResult = vResult & "         TipoRefleja," & vbCrLf
   vResult = vResult & "         CuentaReflejaAlDebe," & vbCrLf
   vResult = vResult & "         CuentaReflejaAlHaber," & vbCrLf
   vResult = vResult & "         NombreOperador," & vbCrLf
   vResult = vResult & "         FechaUltimaModificacion)" & vbCrLf
   vResult = vResult & "      VALUES(" & vbCrLf
   vResult = vResult & "         @ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "         @Codigo," & vbCrLf
   vResult = vResult & "         @Descripcion," & vbCrLf
   vResult = vResult & "         @NaturalezaDeLaCuenta," & vbCrLf
   vResult = vResult & "         @TieneSubCuentas," & vbCrLf
   vResult = vResult & "         @TieneAuxiliar," & vbCrLf
   vResult = vResult & "         @GrupoAuxiliar," & vbCrLf
   vResult = vResult & "         @VieneDelPeriodoAnterior," & vbCrLf
   vResult = vResult & "         @StatusCuenta," & vbCrLf
   vResult = vResult & "         @MetAjusteYactivoStr," & vbCrLf
   vResult = vResult & "         @TipoDeInventario," & vbCrLf
   vResult = vResult & "         @SeccionEstadoDemostrativo," & vbCrLf
   vResult = vResult & "         @CostoDeVentas," & vbCrLf
   vResult = vResult & "         @Gastos," & vbCrLf
   vResult = vResult & "         @IngresosPropiosActividad," & vbCrLf
   vResult = vResult & "         @ConciliacionInfo2000," & vbCrLf
   vResult = vResult & "         @EsActivoFijo," & vbCrLf
   vResult = vResult & "         @CodigoGrupoActivo," & vbCrLf
   vResult = vResult & "         @SaldoInicial," & vbCrLf
   vResult = vResult & "         @TipoDeCuentaFiscal," & vbCrLf
   vResult = vResult & "         @TipoDeCuentaDpc10," & vbCrLf
   vResult = vResult & "         @Dpc10MetodoAjuste," & vbCrLf
   vResult = vResult & "         @MetodoAjuste," & vbCrLf
   vResult = vResult & "         @DpcCodigoGrupoActivoFijo," & vbCrLf
   vResult = vResult & "         @CodigoGrupoAjustaPorIpc," & vbCrLf
   vResult = vResult & "         @TipoDeOtrasNoMonetarias," & vbCrLf
   vResult = vResult & "         @TipoRefleja," & vbCrLf
   vResult = vResult & "         @CuentaReflejaAlDebe," & vbCrLf
   vResult = vResult & "         @CuentaReflejaAlHaber," & vbCrLf
   vResult = vResult & "         @NombreOperador," & vbCrLf
   vResult = vResult & "         @FechaUltimaModificacion)" & vbCrLf
   vResult = vResult & "   RETURN @@ROWCOUNT" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpIMP(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameIMP(), fSpParametersIMP(), fStatementsSpIMP(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpIMP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpIMP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpIMP(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameIMP(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpIMP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpIMP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameIMP() As String
   GetSpNameIMP = "CuentaIMP"
End Function

Private Function fSpParametersIMP() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "         @DescripcionCuenta " & gUtilSQL.getVarCharTypeForDB(40) & " = ''," & vbCrLf
   vResult = vResult & "         @Naturaleza char(1)," & vbCrLf
   vResult = vResult & "         @VieneDelPeriodoAnterior bit," & vbCrLf
   vResult = vResult & "         @SaldoInicial money," & vbCrLf
   vResult = vResult & "         @CrearComprobanteApertura bit," & vbCrLf
   vResult = vResult & "         @TieneSubCuentas bit = 0," & vbCrLf
   vResult = vResult & "         @TieneAuxiliares bit = 0," & vbCrLf
   vResult = vResult & "         @GrupoAuxiliares " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "         @EsActivoFijo bit = 0," & vbCrLf
   vResult = vResult & "         @GrupoActivoFijo " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "         @NombreGrupoActivoFijo " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "         @SeccionEstadoDemostrativo " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "         @IngresosPropiosActividad " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "         @CostoDeVentas " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "         @Gastos " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "         @TipoDeInventario " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "         @ConciliacionInfo2000 " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "         @TipoDeCuentaFiscal char(1) = '0'," & vbCrLf
   vResult = vResult & "         @TipoDeCuentaDpc10 char(1) = '0'," & vbCrLf
   vResult = vResult & "         @Dpc10MetodoAjuste char(1) = '6'," & vbCrLf
   vResult = vResult & "         @MetodoAjuste char(1) = '6'," & vbCrLf
   vResult = vResult & "         @DpcCodigoGrupoActivoFijo " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "         @CodigoGrupoAjustaPorIpc " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "         @TipoDeOtrasNoMonetarias char(1) = '0'," & vbCrLf
   vResult = vResult & "         @TipoRefleja char(1) = '0'," & vbCrLf
   vResult = vResult & "         @CuentaReflejaAlDebe " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "         @CuentaReflejaAlHaber " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "         @NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
    fSpParametersIMP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersIMP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpIMP() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @CodigoCuentaLimpio " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CuentaValida bit" & vbCrLf
   vResult = vResult & "   DECLARE @CuentaExiste bit" & vbCrLf
   vResult = vResult & "   DECLARE @DescripcionCuentaFinal " & gUtilSQL.getVarCharTypeForDB(40) & "" & vbCrLf
   vResult = vResult & "   DECLARE @TieneSubCuentasSN char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @TieneAuxiliaresSN char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @VieneDelPeriodoAnteriorSN char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @SaldoInicialLimpio money" & vbCrLf
   vResult = vResult & "   DECLARE @InsertarGrupoActivoFijo bit" & vbCrLf
   vResult = vResult & "   DECLARE @GrupoAuxiliaresChr char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @EsActivoFijoSN char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @SeccionEstadoDemostrativoChr char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @IngresosPropiosActividadChr char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @CostoDeVentasChr char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @GastosChr char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @TipoDeInventarioChr char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @ConciliacionInfo2000Chr char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @StatusCuentaChr char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @HayError bit" & vbCrLf
   vResult = vResult & "   SET @HayError = 0 --False" & vbCrLf
   vResult = vResult & "   SET @FechaUltimaModificacion = " & gUtilSQL.DfCDateSQL(gUtilSQL.GetSQLFechaActual()) & "" & vbCrLf
   vResult = vResult & "   IF @VieneDelPeriodoAnteriorSN = 1 --True" & vbCrLf
   vResult = vResult & "      SET @VieneDelPeriodoAnteriorSN = 'S'" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @VieneDelPeriodoAnteriorSN = 'N'" & vbCrLf
   vResult = vResult & "   SET @CodigoCuentaLimpio = REPLACE(@CodigoCuenta, '.', '')" & vbCrLf
   vResult = vResult & "   SET @CodigoCuentaLimpio = REPLACE(@CodigoCuentaLimpio, '-', '')" & vbCrLf
   vResult = vResult & "   SET @CodigoCuentaLimpio = dbo.Gf_AdjustCode(@ConsecutivoCompania, @ConsecutivoPeriodo, @CodigoCuentaLimpio)" & vbCrLf
   vResult = vResult & "   SET @CuentaValida = dbo.Gf_CuentaIsValidForImport(@ConsecutivoCompania, @ConsecutivoPeriodo, @CodigoCuentaLimpio)" & vbCrLf
   vResult = vResult & "   IF (@CuentaValida = 0)  --False" & vbCrLf
   vResult = vResult & "      RAISERROR ('La Cuenta Contable:%s no es válida.', 16, 1, @CodigoCuenta)" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      IF (@DescripcionCuenta = '')" & vbCrLf
   vResult = vResult & "         SET @DescripcionCuentaFinal = 'SIN DESCRIPCIÓN ' + @CodigoCuentaLimpio" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "         SET @DescripcionCuentaFinal = @DescripcionCuenta" & vbCrLf
   vResult = vResult & "      IF (UPPER(@Naturaleza) <> '0') AND (UPPER(@Naturaleza) <> '1')" & vbCrLf
   vResult = vResult & "         RAISERROR ('La Naturaleza de la Cuenta Contable:%s no es válida (0 = Debe, 1 = Haber).', 16, 1, @Naturaleza)" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         IF (@TieneSubCuentas = 1) --True" & vbCrLf
   vResult = vResult & "            SET @TieneSubCuentasSN = 'S'" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "            SET @TieneSubCuentasSN = 'N'" & vbCrLf
   vResult = vResult & "         IF (@CrearComprobanteApertura = 1) --True" & vbCrLf
   vResult = vResult & "            SET @SaldoInicialLimpio = @SaldoInicial" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "            SET @SaldoInicialLimpio = 0" & vbCrLf
   vResult = vResult & "         IF (@TieneAuxiliares = 1) --True" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @TieneAuxiliaresSN = 'S'" & vbCrLf
   vResult = vResult & "            IF ((@GrupoAuxiliares = 0) OR (@GrupoAuxiliares = 1) OR (@GrupoAuxiliares = 2))" & vbCrLf
   vResult = vResult & "               SET @GrupoAuxiliaresChr = dbo.Gf_EnumToDBValue(@GrupoAuxiliares)" & vbCrLf
   vResult = vResult & "            ELSE" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SET @HayError = 1 --True" & vbCrLf
   vResult = vResult & "               RAISERROR ('El Grupo de Auxiliares:%d no es válido.', 16, 1, @GrupoAuxiliares)" & vbCrLf
   vResult = vResult & "            END " & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @TieneAuxiliaresSN = 'N'" & vbCrLf
   vResult = vResult & "            SET @GrupoAuxiliaresChr = dbo.Gf_EnumToDBValue(3)" & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "         IF (@HayError = 0) --False" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            IF (@EsActivoFijo = 1)--True" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SET @EsActivoFijoSN = 'S'" & vbCrLf
   vResult = vResult & "               IF (EXISTS(SELECT Codigo FROM GrupoDeActivos WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Codigo = @GrupoActivoFijo))" & vbCrLf
   vResult = vResult & "                  SET @InsertarGrupoActivoFijo = 0 --False" & vbCrLf
   vResult = vResult & "               ELSE" & vbCrLf
   vResult = vResult & "                  SET @InsertarGrupoActivoFijo = 1 --True" & vbCrLf
   vResult = vResult & "            END " & vbCrLf
   vResult = vResult & "            ELSE" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SET @EsActivoFijoSN = 'N'" & vbCrLf
   vResult = vResult & "               SET @InsertarGrupoActivoFijo = 0" & vbCrLf
   vResult = vResult & "            END " & vbCrLf
   vResult = vResult & "            SET @SeccionEstadoDemostrativoChr = dbo.Gf_EnumToDBValue(@SeccionEstadoDemostrativo)" & vbCrLf
   vResult = vResult & "            SET @IngresosPropiosActividadChr = dbo.Gf_EnumToDBValue(@IngresosPropiosActividad)" & vbCrLf
   vResult = vResult & "            SET @CostoDeVentasChr = dbo.Gf_EnumToDBValue(@CostoDeVentas)" & vbCrLf
   vResult = vResult & "            SET @GastosChr = dbo.Gf_EnumToDBValue(@Gastos)" & vbCrLf
   vResult = vResult & "            SET @TipoDeInventarioChr = dbo.Gf_EnumToDBValue(@TipoDeInventario)" & vbCrLf
   vResult = vResult & "            SET @ConciliacionInfo2000Chr = dbo.Gf_EnumToDBValue(@ConciliacionInfo2000)" & vbCrLf
   vResult = vResult & "            SET @StatusCuentaChr = dbo.Gf_EnumToDBValue(0)" & vbCrLf
   vResult = vResult & "            EXEC Gp_CrearCuentasDeNivelesAnterioresSiAplica" & vbCrLf
   vResult = vResult & "                  @ConsecutivoCompania," & vbCrLf
   vResult = vResult & "                  @ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "                  @CodigoCuentaLimpio," & vbCrLf
   vResult = vResult & "                  @DescripcionCuenta," & vbCrLf
   vResult = vResult & "                  @Naturaleza," & vbCrLf
   vResult = vResult & "                  @NombreOperador" & vbCrLf
   vResult = vResult & "            IF @InsertarGrupoActivoFijo = 1 --True" & vbCrLf
   vResult = vResult & "               EXEC Gp_CrearGrupoDeActivoFijoDesdeImportacionCuentaSiAplica @ConsecutivoCompania, @ConsecutivoPeriodo,@GrupoActivoFijo, @NombreGrupoActivoFijo" & vbCrLf
   vResult = vResult & "            IF (dbo.Gf_ExistsCuenta(@ConsecutivoPeriodo, @CodigoCuenta) = 0)--No existe la cuenta" & vbCrLf
   vResult = vResult & "               EXEC Gp_CuentaINS @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "                        ,@CodigoCuentaLimpio" & vbCrLf
   vResult = vResult & "                        ,@DescripcionCuentaFinal" & vbCrLf
   vResult = vResult & "                        ,@Naturaleza" & vbCrLf
   vResult = vResult & "                        ,@TieneSubCuentasSN" & vbCrLf
   vResult = vResult & "                        ,@TieneAuxiliaresSN" & vbCrLf
   vResult = vResult & "                        ,@GrupoAuxiliaresChr" & vbCrLf
   vResult = vResult & "                        ,@VieneDelPeriodoAnteriorSN" & vbCrLf
   vResult = vResult & "                        ,@StatusCuentaChr" & vbCrLf
   vResult = vResult & "                        ,''" & vbCrLf
   vResult = vResult & "                        ,@TipoDeInventarioChr" & vbCrLf
   vResult = vResult & "                        ,@SeccionEstadoDemostrativoChr" & vbCrLf
   vResult = vResult & "                        ,@CostoDeVentasChr" & vbCrLf
   vResult = vResult & "                        ,@GastosChr" & vbCrLf
   vResult = vResult & "                        ,@IngresosPropiosActividadChr" & vbCrLf
   vResult = vResult & "                        ,@ConciliacionInfo2000Chr" & vbCrLf
   vResult = vResult & "                        ,@EsActivoFijoSN" & vbCrLf
   vResult = vResult & "                        ,@GrupoActivoFijo" & vbCrLf
   vResult = vResult & "                        ,@SaldoInicialLimpio" & vbCrLf
   vResult = vResult & "                        ,@TipoDeCuentaFiscal" & vbCrLf
   vResult = vResult & "                        ,@TipoDeCuentaDpc10" & vbCrLf
   vResult = vResult & "                        ,@Dpc10MetodoAjuste" & vbCrLf
   vResult = vResult & "                        ,@MetodoAjuste" & vbCrLf
   vResult = vResult & "                        ,@DpcCodigoGrupoActivoFijo" & vbCrLf
   vResult = vResult & "                        ,@CodigoGrupoAjustaPorIpc" & vbCrLf
   vResult = vResult & "                        ,@TipoDeOtrasNoMonetarias" & vbCrLf
   vResult = vResult & "                        ,@TipoRefleja" & vbCrLf
   vResult = vResult & "                        ,@CuentaReflejaAlDebe" & vbCrLf
   vResult = vResult & "                        ,@CuentaReflejaAlHaber" & vbCrLf
   vResult = vResult & "                        ,@NombreOperador" & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpIMP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpIMP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetSpNameS10Insert = "S10_CuentaINS"
End Function

Private Function fSpParametersS10Insert() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "   @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & "   @CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "   @DescripcionCuenta " & gUtilSQL.getVarCharTypeForDB(40) & " = ''," & vbCrLf
   vResult = vResult & "   @Naturaleza char(1)," & vbCrLf
   vResult = vResult & "   @TieneSubCuentas bit," & vbCrLf
   vResult = vResult & "   @TipoRefleja char(1) = '0'," & vbCrLf
   vResult = vResult & "   @CuentaReflejaAlDebe " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "   @CuentaReflejaAlHaber " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "   @SaldoInicial money = 0" & vbCrLf
   fSpParametersS10Insert = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10Insert", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10Insert() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoPrimerPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT ConsecutivoCompania FROM COMPANIA WHERE ConsecutivoCompania = @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT TOP 1 @ConsecutivoPrimerPeriodo = ConsecutivoPeriodo FROM PERIODO WHERE ConsecutivoCompania = @ConsecutivoCompania ORDER BY FechaAperturaDelPeriodo" & vbCrLf
   vResult = vResult & "      IF (NOT @ConsecutivoPrimerPeriodo IS NULL) AND (@ConsecutivoPrimerPeriodo > 0)" & vbCrLf
   vResult = vResult & "         EXEC Gp_CuentaIMP @ConsecutivoCompania," & vbCrLf
   vResult = vResult & "            @ConsecutivoPrimerPeriodo," & vbCrLf
   vResult = vResult & "            @CodigoCuenta," & vbCrLf
   vResult = vResult & "            @DescripcionCuenta," & vbCrLf
   vResult = vResult & "            @Naturaleza, 0, @SaldoInicial, 0, @TieneSubCuentas,0, 0, 0, 0, '', 0,0,0,0,0,0," & vbCrLf
   vResult = vResult & "            '0', '0', '6', '6', 0, 0, '0', @TipoRefleja, @CuentaReflejaAlDebe, @CuentaReflejaAlHaber," & vbCrLf
   vResult = vResult & "            'JEFE'" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "         RAISERROR('No existen períodos para el Consecutivo Compañía: %d', 16, 1, @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      RAISERROR ('No se consigue Consecutivo Compañía.', 16, 1)" & vbCrLf
   vResult = vResult & "END "
   fStatementsSpS10Insert = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10Insert", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpCrearCuentasDeNivelesAnterioresSiAplica(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameCrearCuentasDeNivelesAnterioresSiAplica(), fSpParametersCrearCuentasDeNivelesAnterioresSiAplica(), fStatementsSpCrearCuentasDeNivelesAnterioresSiAplica(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpCrearCuentasDeNivelesAnterioresSiAplica = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpCrearCuentasDeNivelesAnterioresSiAplica", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpCrearCuentasDeNivelesAnterioresSiAplica(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameCrearCuentasDeNivelesAnterioresSiAplica(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpCrearCuentasDeNivelesAnterioresSiAplica = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpCrearCuentasDeNivelesAnterioresSiAplica", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameCrearCuentasDeNivelesAnterioresSiAplica() As String
   GetSpNameCrearCuentasDeNivelesAnterioresSiAplica = "CrearCuentasDeNivelesAnterioresSiAplica"
End Function

Private Function fSpParametersCrearCuentasDeNivelesAnterioresSiAplica() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "   @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "   @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "   @CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "   @DescripcionCuenta " & gUtilSQL.getVarCharTypeForDB(40) & "," & vbCrLf
   vResult = vResult & "   @NaturalezaDeLaCuenta " & gUtilSQL.getCharTypeForDB(1) & "," & vbCrLf
   vResult = vResult & "   @NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
    fSpParametersCrearCuentasDeNivelesAnterioresSiAplica = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersCrearCuentasDeNivelesAnterioresSiAplica", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpCrearCuentasDeNivelesAnterioresSiAplica() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @CuentasNivelesPrevios nvarchar(max)" & vbCrLf
   vResult = vResult & "   DECLARE @Niveles nvarchar(max)" & vbCrLf
   vResult = vResult & "   DECLARE @CuentaPrimerNivel nvarchar(max)" & vbCrLf
   vResult = vResult & "   DECLARE @CuentaNueva varchar(max) " & vbCrLf
   vResult = vResult & "--BEGIN PERÚ" & vbCrLf
   vResult = vResult & "   DECLARE @Pais varchar(4) " & vbCrLf
   vResult = vResult & "--END PERU" & vbCrLf
   vResult = vResult & "   SET @Niveles = dbo.Gf_LevelsOfDefCuenta(@ConsecutivoCompania, @ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "   SET @CuentaPrimerNivel = dbo.Gf_CuentaPrimerNivel(@CodigoCuenta, @Niveles)" & vbCrLf
   vResult = vResult & "   SET @CuentasNivelesPrevios = dbo.Gf_CuentasNivelesPrevios(@CodigoCuenta, @Niveles)" & vbCrLf
   vResult = vResult & "--BEGIN PERÚ" & vbCrLf
   vResult = vResult & "   SELECT @Pais = CodigoPais FROM COMPANIA WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "   IF (@Pais = 'PE')" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT Codigo FROM CUENTA WHERE Codigo = @CuentaPrimerNivel AND ConsecutivoPeriodo = @ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         EXEC Gp_CuentaINS @ConsecutivoPeriodo, @CuentaPrimerNivel, 'POR DEFINIR', @NaturalezaDeLaCuenta," & vbCrLf
   vResult = vResult & "               'S', 'N', '0', 'N', '0', '', '0', '0', '0', '0', '0' , '0', 'N', 0, 0," & vbCrLf
   vResult = vResult & "               '0', '0', '0', '0', 0, 0, '0', '0', '', ''," & vbCrLf
   vResult = vResult & "               @NombreOperador" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "--END PERÚ" & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT Codigo FROM CUENTA WHERE Codigo = @CuentaPrimerNivel AND ConsecutivoPeriodo = @ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      WHILE LEN(@CuentasNivelesPrevios) > 0" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         IF @CuentasNivelesPrevios = ';'" & vbCrLf
   vResult = vResult & "            SET @CuentasNivelesPrevios = ''" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @CuentaNueva = SUBSTRING(@CuentasNivelesPrevios, 1,  CHARINDEX(';', @CuentasNivelesPrevios)-1)" & vbCrLf
   vResult = vResult & "            SET @CuentasNivelesPrevios = SUBSTRING(@CuentasNivelesPrevios, LEN(@CuentaNueva)+2, LEN(@CuentasNivelesPrevios))" & vbCrLf
   vResult = vResult & "            IF (LEN(@CuentaNueva) > 0)" & vbCrLf
   vResult = vResult & "               IF (NOT EXISTS(SELECT Codigo FROM CUENTA WHERE Codigo = @CuentaNueva AND ConsecutivoPeriodo = @ConsecutivoPeriodo))" & vbCrLf
   vResult = vResult & "                  EXEC Gp_CuentaINS @ConsecutivoPeriodo, @CuentaNueva, @DescripcionCuenta, @NaturalezaDeLaCuenta," & vbCrLf
   vResult = vResult & "                        'S', 'N', '0', 'N', '0', '', '0', '0', '0', '0', '0' , '0', 'N', 0, 0," & vbCrLf
   vResult = vResult & "                        '0', '0', '0', '0', 0, 0, '0', '0', '', ''," & vbCrLf
   vResult = vResult & "                        @NombreOperador" & vbCrLf
   vResult = vResult & "          END " & vbCrLf
   vResult = vResult & "       END " & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpCrearCuentasDeNivelesAnterioresSiAplica = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpCrearCuentasDeNivelesAnterioresSiAplica", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnAdjustCode(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameAdjustCode(), fFnParametersAdjustCode(), fFnScalarReturnDataTypeAdjustCode(), fStatementsFnAdjustCode(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnAdjustCode = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnAdjustCode", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnAdjustCode(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameAdjustCode(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnAdjustCode = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnAdjustCode", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameAdjustCode() As String
   GetFnNameAdjustCode = "AdjustCode"
End Function

Private Function fFnParametersAdjustCode() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & "@ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & "@CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30)
   fFnParametersAdjustCode = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersAdjustCode", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeAdjustCode() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = gUtilSQL.getVarCharTypeForDB(30)
   fFnScalarReturnDataTypeAdjustCode = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnScalarReturnDataTypeAdjustCode", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsFnAdjustCode() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @CodigoCuentaLimpio " & gUtilSQL.getVarCharTypeForDB(30) & vbCrLf
   vResult = vResult & "   DECLARE @Pos " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   SET @CodigoCuentaLimpio = @CodigoCuenta" & vbCrLf
   vResult = vResult & "   SET @CodigoCuentaLimpio = REPLACE(@CodigoCuentaLimpio, '-', '.')" & vbCrLf
   vResult = vResult & "   SET @Pos = CHARINDEX('..', @CodigoCuentaLimpio)" & vbCrLf
   vResult = vResult & "   WHILE (@Pos > 0)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @CodigoCuentaLimpio = REPLACE(@CodigoCuentaLimpio, '..', '.')" & vbCrLf
   vResult = vResult & "      SET @Pos = CHARINDEX('..', @CodigoCuentaLimpio)" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "   IF (LEN(@CodigoCuentaLimpio) > 0)" & vbCrLf
   vResult = vResult & "      IF (LEFT(@CodigoCuentaLimpio, 1) = '.')" & vbCrLf
   vResult = vResult & "         SET @CodigoCuentaLimpio = SUBSTRING(@CodigoCuentaLimpio, 2, LEN(@CodigoCuentaLimpio))" & vbCrLf
   vResult = vResult & "   IF (LEN(@CodigoCuentaLimpio) > 0)" & vbCrLf
   vResult = vResult & "      IF (RIGHT(@CodigoCuentaLimpio, 1) = '.')" & vbCrLf
   vResult = vResult & "         SET @CodigoCuentaLimpio = SUBSTRING(@CodigoCuentaLimpio, 1, LEN(@CodigoCuentaLimpio) - 1)" & vbCrLf
   vResult = vResult & "   SET @CodigoCuentaLimpio = dbo.Gf_CuadraElCodigoConLosNiveles(@ConsecutivoCompania, @ConsecutivoPeriodo, @CodigoCuentaLimpio)" & vbCrLf
   vResult = vResult & "   RETURN @CodigoCuentaLimpio" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnAdjustCode = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnAdjustCode", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnCuentaIsValidForImport(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameCuentaIsValidForImport(), fFnParametersCuentaIsValidForImport(), fFnScalarReturnDataTypeCuentaIsValidForImport(), fStatementsFnCuentaIsValidForImport(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnCuentaIsValidForImport = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnCuentaIsValidForImport", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnCuentaIsValidForImport(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameCuentaIsValidForImport(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnCuentaIsValidForImport = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnCuentaIsValidForImport", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameCuentaIsValidForImport() As String
   GetFnNameCuentaIsValidForImport = "CuentaIsValidForImport"
End Function

Private Function fFnParametersCuentaIsValidForImport() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & "@ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "@CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30)
   fFnParametersCuentaIsValidForImport = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersCuentaIsValidForImport", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeCuentaIsValidForImport() As String
   fFnScalarReturnDataTypeCuentaIsValidForImport = "bit"
End Function

Private Function fStatementsFnCuentaIsValidForImport() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @IsValid bit" & vbCrLf
   vResult = vResult & "   DECLARE @Pais " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Niveles nvarchar(max)" & vbCrLf
   vResult = vResult & "   DECLARE @NivelDelCodigo " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   SET @IsValid = 0 --False" & vbCrLf
   vResult = vResult & "   SELECT @Pais = CodigoPais FROM COMPANIA WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "   IF (@Pais = 'PE')" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @Niveles = dbo.Gf_LevelsOfDefCuenta(@ConsecutivoCompania, @ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "      SET @NivelDelCodigo = dbo.Gf_NivelDelCodigo(@CodigoCuenta, @Niveles)" & vbCrLf
   vResult = vResult & "      IF (@NivelDelCodigo = 1)" & vbCrLf
   vResult = vResult & "         SET @IsValid = 1 --True" & vbCrLf
   vResult = vResult & "      ELSE IF NOT (dbo.Gf_ExistsCuenta(@ConsecutivoPeriodo, @CodigoCuenta) = 1)" & vbCrLf
   vResult = vResult & "         SET @IsValid = 1 --True" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   IF (@IsValid = 0) --False" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      IF (dbo.Gf_ExistsCuenta(@ConsecutivoPeriodo, @CodigoCuenta) = 1)" & vbCrLf
   vResult = vResult & "         IF ((dbo.Gf_EsCuentaDeUltimoNivel(@ConsecutivoCompania, @ConsecutivoPeriodo, @CodigoCuenta) = 1) OR  (NOT (dbo.Gf_CuentaTieneSubCuenta(@ConsecutivoPeriodo, @CodigoCuenta) = 1)))" & vbCrLf
   vResult = vResult & "            SET @IsValid = 1 --True" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "         IF (NOT (dbo.Gf_LaCuentaDelNivelAnteriorEsDeMovimiento(@ConsecutivoCompania, @ConsecutivoPeriodo, @CodigoCuenta) = 1)) AND (dbo.Gf_LaCuentaEstaEnDefiniconCuentas(@ConsecutivoCompania, @ConsecutivoPeriodo, @CodigoCuenta) = 1)" & vbCrLf
   vResult = vResult & "            SET @IsValid = 1 --True" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   RETURN @IsValid" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsFnCuentaIsValidForImport = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnCuentaIsValidForImport", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnExistsCuentaInConciliacionContable(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameExistsCuentaInConciliacionContable(), fFnParametersExistsCuentaInConciliacionContable(), fFnScalarReturnDataTypeExistsCuentaInConciliacionContable(), fStatementsFnExistsCuentaInConciliacionContable(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnExistsCuentaInConciliacionContable = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnExistsCuentaInConciliacionContable", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnExistsCuentaInConciliacionContable(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameExistsCuentaInConciliacionContable(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnExistsCuentaInConciliacionContable = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnExistsCuentaInConciliacionContable", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameExistsCuentaInConciliacionContable() As String
   GetFnNameExistsCuentaInConciliacionContable = "ExistsCuentaInConciliacionContable"
End Function

Private Function fFnParametersExistsCuentaInConciliacionContable() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & "@CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30)
   fFnParametersExistsCuentaInConciliacionContable = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersExistsCuentaInConciliacionContable", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeExistsCuentaInConciliacionContable() As String
   fFnScalarReturnDataTypeExistsCuentaInConciliacionContable = "bit"
End Function

Private Function fStatementsFnExistsCuentaInConciliacionContable() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Exists bit" & vbCrLf
   vResult = vResult & "   SET @Exists = 0 --False" & vbCrLf
   vResult = vResult & "   RETURN @Exists" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnExistsCuentaInConciliacionContable = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnExistsCuentaInConciliacionContable", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnNivelesDefinicionCuenta(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameNivelesDefinicionCuenta(), fFnParametersNivelesDefinicionCuenta(), fFnScalarReturnDataTypeNivelesDefinicionCuenta(), fStatementsFnNivelesDefinicionCuenta(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnNivelesDefinicionCuenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnNivelesDefinicionCuenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnNivelesDefinicionCuenta(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameNivelesDefinicionCuenta(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnNivelesDefinicionCuenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnNivelesDefinicionCuenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameNivelesDefinicionCuenta() As String
   GetFnNameNivelesDefinicionCuenta = "LevelsOfDefCuenta"
End Function

Private Function fFnParametersNivelesDefinicionCuenta() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & "@ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0)
   fFnParametersNivelesDefinicionCuenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersNivelesDefinicionCuenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeNivelesDefinicionCuenta() As String
   fFnScalarReturnDataTypeNivelesDefinicionCuenta = "varchar(max)"
End Function

Private Function fStatementsFnNivelesDefinicionCuenta() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Result varchar(max)" & vbCrLf
   vResult = vResult & "   DECLARE @CeroDer " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Nivel1 " & gUtilSQL.getVarCharTypeForDB(3) & vbCrLf
   vResult = vResult & "   DECLARE @Nivel2 " & gUtilSQL.getVarCharTypeForDB(3) & vbCrLf
   vResult = vResult & "   DECLARE @Nivel3 " & gUtilSQL.getVarCharTypeForDB(3) & vbCrLf
   vResult = vResult & "   DECLARE @Nivel4 " & gUtilSQL.getVarCharTypeForDB(3) & vbCrLf
   vResult = vResult & "   DECLARE @Nivel5 " & gUtilSQL.getVarCharTypeForDB(3) & vbCrLf
   vResult = vResult & "   DECLARE @Nivel6 " & gUtilSQL.getVarCharTypeForDB(3) & vbCrLf
   vResult = vResult & "   DECLARE @Nivel7 " & gUtilSQL.getVarCharTypeForDB(3) & vbCrLf
   vResult = vResult & "   DECLARE @Nivel8 " & gUtilSQL.getVarCharTypeForDB(3) & vbCrLf
   vResult = vResult & "   DECLARE @Nivel9 " & gUtilSQL.getVarCharTypeForDB(3) & vbCrLf
   vResult = vResult & "   DECLARE @Nivel10 " & gUtilSQL.getVarCharTypeForDB(3) & vbCrLf
   vResult = vResult & "   IF (EXISTS(SELECT  ConsecutivoPeriodo FROM PERIODO WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoPeriodo = @ConsecutivoPeriodo))" & vbCrLf
   vResult = vResult & "      SELECT  @CeroDer = ISNULL(CuentaConCerosADerecha, 'N')," & vbCrLf
   vResult = vResult & "         @Nivel1 = ISNULL(CAST(Nivel1 AS varchar), '0')," & vbCrLf
   vResult = vResult & "         @Nivel2 = ISNULL(CAST(Nivel2 AS varchar), '0')," & vbCrLf
   vResult = vResult & "         @Nivel3 = ISNULL(CAST(Nivel3 AS varchar), '0')," & vbCrLf
   vResult = vResult & "         @Nivel4 = ISNULL(CAST(Nivel4 AS varchar), '0')," & vbCrLf
   vResult = vResult & "         @Nivel5 = ISNULL(CAST(Nivel5 AS varchar), '0')," & vbCrLf
   vResult = vResult & "         @Nivel6 = ISNULL(CAST(Nivel6 AS varchar), '0')," & vbCrLf
   vResult = vResult & "         @Nivel7 = ISNULL(CAST(Nivel7 AS varchar), '0')," & vbCrLf
   vResult = vResult & "         @Nivel8 = ISNULL(CAST(Nivel8 AS varchar), '0')," & vbCrLf
   vResult = vResult & "         @Nivel9 = ISNULL(CAST(Nivel9 AS varchar), '0')," & vbCrLf
   vResult = vResult & "         @Nivel10 =ISNULL(CAST(Nivel10 AS varchar), '0')" & vbCrLf
   vResult = vResult & "         FROM PERIODO" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SET @CeroDer = 'N'" & vbCrLf
   vResult = vResult & "         SET @Nivel1 = '0'" & vbCrLf
   vResult = vResult & "         SET @Nivel2 = '0'" & vbCrLf
   vResult = vResult & "         SET @Nivel3 = '0'" & vbCrLf
   vResult = vResult & "         SET @Nivel4 = '0'" & vbCrLf
   vResult = vResult & "         SET @Nivel5 = '0'" & vbCrLf
   vResult = vResult & "         SET @Nivel6 = '0'" & vbCrLf
   vResult = vResult & "         SET @Nivel7 = '0'" & vbCrLf
   vResult = vResult & "         SET @Nivel8 = '0'" & vbCrLf
   vResult = vResult & "         SET @Nivel9 = '0'" & vbCrLf
   vResult = vResult & "         SET @Nivel10 = '0'" & vbCrLf
   vResult = vResult & "      END" & vbCrLf
   vResult = vResult & "   SET @Result = @CeroDer + ';' + @Nivel1 + ';' + @Nivel2 + ';' + @Nivel3 + ';' + @Nivel4 + ';' + @Nivel5 + ';' + @Nivel6 + ';' + @Nivel7 + ';' + @Nivel8 + ';' + @Nivel9 + ';' + @Nivel10 + ';'" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnNivelesDefinicionCuenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnNivelesDefinicionCuenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnLaCuentaEstaENDefiniconCuentas(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameLaCuentaEstaENDefiniconCuentas(), fFnParametersLaCuentaEstaENDefiniconCuentas(), fFnScalarReturnDataTypeLaCuentaEstaENDefiniconCuentas(), fStatementsFnLaCuentaEstaENDefiniconCuentas(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnLaCuentaEstaENDefiniconCuentas = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnLaCuentaEstaENDefiniconCuentas", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnLaCuentaEstaENDefiniconCuentas(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameLaCuentaEstaENDefiniconCuentas(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnLaCuentaEstaENDefiniconCuentas = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnLaCuentaEstaENDefiniconCuentas", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameLaCuentaEstaENDefiniconCuentas() As String
   GetFnNameLaCuentaEstaENDefiniconCuentas = "LaCuentaEstaENDefiniconCuentas"
End Function

Private Function fFnParametersLaCuentaEstaENDefiniconCuentas() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30)
   fFnParametersLaCuentaEstaENDefiniconCuentas = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersLaCuentaEstaENDefiniconCuentas", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeLaCuentaEstaENDefiniconCuentas() As String
   fFnScalarReturnDataTypeLaCuentaEstaENDefiniconCuentas = " bit"
End Function

Private Function fStatementsFnLaCuentaEstaENDefiniconCuentas() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Result bit" & vbCrLf
   vResult = vResult & "   SET @Result = 0 --False" & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT ConsecutivoPeriodo FROM PERIODO WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoPeriodo = @ConsecutivoPeriodo) " & vbCrLf
   vResult = vResult & "      SET @Result = 1 --True" & vbCrLf
   vResult = vResult & "   ELSE IF EXISTS(SELECT ConsecutivoPeriodo FROM PERIODO WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoPeriodo = @ConsecutivoPeriodo) " & vbCrLf
   vResult = vResult & "      SET @Result = 1 --True" & vbCrLf
   vResult = vResult & "   ELSE IF EXISTS(SELECT ConsecutivoPeriodo FROM PERIODO WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoPeriodo = @ConsecutivoPeriodo) " & vbCrLf
   vResult = vResult & "      SET @Result = 1 --True" & vbCrLf
   vResult = vResult & "   ELSE IF EXISTS(SELECT ConsecutivoPeriodo FROM PERIODO WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoPeriodo = @ConsecutivoPeriodo) " & vbCrLf
   vResult = vResult & "      SET @Result = 1 --True" & vbCrLf
   vResult = vResult & "   ELSE IF EXISTS(SELECT ConsecutivoPeriodo FROM PERIODO WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoPeriodo = @ConsecutivoPeriodo) " & vbCrLf
   vResult = vResult & "      SET @Result = 1 --True" & vbCrLf
   vResult = vResult & "   ELSE IF EXISTS(SELECT ConsecutivoPeriodo FROM PERIODO WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoPeriodo = @ConsecutivoPeriodo) " & vbCrLf
   vResult = vResult & "      SET @Result = 1 --True" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnLaCuentaEstaENDefiniconCuentas = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnLaCuentaEstaENDefiniconCuentas", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnLaCuentaDelNivelAnteriorEsDeMovimiento(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameLaCuentaDelNivelAnteriorEsDeMovimiento(), fFnParametersLaCuentaDelNivelAnteriorEsDeMovimiento(), fFnScalarReturnDataTypeLaCuentaDelNivelAnteriorEsDeMovimiento(), fStatementsFnLaCuentaDelNivelAnteriorEsDeMovimiento(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnLaCuentaDelNivelAnteriorEsDeMovimiento = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnLaCuentaDelNivelAnteriorEsDeMovimiento", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnLaCuentaDelNivelAnteriorEsDeMovimiento(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameLaCuentaDelNivelAnteriorEsDeMovimiento(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnLaCuentaDelNivelAnteriorEsDeMovimiento = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnLaCuentaDelNivelAnteriorEsDeMovimiento", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameLaCuentaDelNivelAnteriorEsDeMovimiento() As String
   GetFnNameLaCuentaDelNivelAnteriorEsDeMovimiento = "LaCuentaDelNivelAnteriorEsDeMovimiento"
End Function

Private Function fFnParametersLaCuentaDelNivelAnteriorEsDeMovimiento() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30)
   fFnParametersLaCuentaDelNivelAnteriorEsDeMovimiento = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersLaCuentaDelNivelAnteriorEsDeMovimiento", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeLaCuentaDelNivelAnteriorEsDeMovimiento() As String
   fFnScalarReturnDataTypeLaCuentaDelNivelAnteriorEsDeMovimiento = "bit"
End Function

Private Function fStatementsFnLaCuentaDelNivelAnteriorEsDeMovimiento() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Result bit" & vbCrLf
   vResult = vResult & "   DECLARE @CuentaNivelAnt varchar(30)" & vbCrLf
   vResult = vResult & "   DECLARE @Niveles nvarchar(max)" & vbCrLf
   vResult = vResult & "   DECLARE @TieneSubCuentas " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   SET @Result = 0 --False" & vbCrLf
   vResult = vResult & "   SET @Niveles = dbo.Gf_LevelsOfDefCuenta(@ConsecutivoCompania, @ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "   SET @CuentaNivelAnt = dbo.Gf_CuentaNivelAnterior(@CodigoCuenta, @Niveles)" & vbCrLf
   vResult = vResult & "   SELECT @TieneSubCuentas = TieneSubCuentas FROM CUENTA WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Codigo = @CuentaNivelAnt" & vbCrLf
   vResult = vResult & "   IF (UPPER(@TieneSubCuentas) = 'N')" & vbCrLf
   vResult = vResult & "      SET @Result = 1 -- True" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnLaCuentaDelNivelAnteriorEsDeMovimiento = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnLaCuentaDelNivelAnteriorEsDeMovimiento", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnExistsCuenta(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameExistsCuenta(), fFnParametersExistsCuenta(), fFnScalarReturnDataTypeExistsCuenta(), fStatementsFnExistsCuenta(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnExistsCuenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnExistsCuenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnExistsCuenta(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameExistsCuenta(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnExistsCuenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnExistsCuenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameExistsCuenta() As String
   GetFnNameExistsCuenta = "ExistsCuenta"
End Function

Private Function fFnParametersExistsCuenta() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30)
   fFnParametersExistsCuenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersExistsCuenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeExistsCuenta() As String
   fFnScalarReturnDataTypeExistsCuenta = "bit"
End Function

Private Function fStatementsFnExistsCuenta() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Result bit" & vbCrLf
   vResult = vResult & "   SET @Result = 0 --False" & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT Codigo FROM CUENTA WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Codigo = @CodigoCuenta)" & vbCrLf
   vResult = vResult & "      SET @Result = 1 --True" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnExistsCuenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnExistsCuenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnEsCuentaDeUltimoNivel(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameEsCuentaDeUltimoNivel(), fFnParametersEsCuentaDeUltimoNivel(), fFnScalarReturnDataTypeEsCuentaDeUltimoNivel(), fStatementsFnEsCuentaDeUltimoNivel(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnEsCuentaDeUltimoNivel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnEsCuentaDeUltimoNivel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnEsCuentaDeUltimoNivel(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameEsCuentaDeUltimoNivel(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnEsCuentaDeUltimoNivel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnEsCuentaDeUltimoNivel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameEsCuentaDeUltimoNivel() As String
   GetFnNameEsCuentaDeUltimoNivel = "EsCuentaDeUltimoNivel"
End Function

Private Function fFnParametersEsCuentaDeUltimoNivel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@CodigoCuenta varchar(30)"
   fFnParametersEsCuentaDeUltimoNivel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersEsCuentaDeUltimoNivel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeEsCuentaDeUltimoNivel() As String
   fFnScalarReturnDataTypeEsCuentaDeUltimoNivel = "bit"
End Function

Private Function fStatementsFnEsCuentaDeUltimoNivel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Result bit" & vbCrLf
   vResult = vResult & "   DECLARE @CantidadTotalNivelesPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @NivelDelCodigo " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Niveles nvarchar(max)" & vbCrLf
   vResult = vResult & "   SET @Result = 0 --False" & vbCrLf
   vResult = vResult & "   SELECT @CantidadTotalNivelesPeriodo = ((CASE WHEN ISNULL(Nivel1, 0) > 0 THEN 1 ELSE 0 END)" & vbCrLf
   vResult = vResult & "         + (CASE WHEN ISNULL(Nivel2, 0) > 0 THEN 1 ELSE 0 END) + (CASE WHEN ISNULL(Nivel3, 0) > 0 THEN 1 ELSE 0 END)" & vbCrLf
   vResult = vResult & "         + (CASE WHEN ISNULL(Nivel4, 0) > 0 THEN 1 ELSE 0 END) + (CASE WHEN ISNULL(Nivel5, 0) > 0 THEN 1 ELSE 0 END)" & vbCrLf
   vResult = vResult & "         + (CASE WHEN ISNULL(Nivel6, 0) > 0 THEN 1 ELSE 0 END) + (CASE WHEN ISNULL(Nivel7, 0) > 0 THEN 1 ELSE 0 END)" & vbCrLf
   vResult = vResult & "         + (CASE WHEN ISNULL(Nivel8, 0) > 0 THEN 1 ELSE 0 END) + (CASE WHEN ISNULL(Nivel9, 0) > 0 THEN 1 ELSE 0 END)" & vbCrLf
   vResult = vResult & "         + (CASE WHEN ISNULL(Nivel10, 0) > 0 THEN 1 ELSE 0 END))" & vbCrLf
   vResult = vResult & "      FROM PERIODO" & vbCrLf
   vResult = vResult & "      WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "   SET @Niveles = dbo.Gf_LevelsOfDefCuenta(@ConsecutivoCompania, @ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "   SET @NivelDelCodigo = dbo.Gf_NivelDelCodigo(@CodigoCuenta, @Niveles)" & vbCrLf
   vResult = vResult & "   IF (@NivelDelCodigo = @CantidadTotalNivelesPeriodo)" & vbCrLf
   vResult = vResult & "      SET @Result = 1 --True" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnEsCuentaDeUltimoNivel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnEsCuentaDeUltimoNivel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnCuentaTieneSubCuenta(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameCuentaTieneSubCuenta(), fFnParametersCuentaTieneSubCuenta(), fFnScalarReturnDataTypeCuentaTieneSubCuenta(), fStatementsFnCuentaTieneSubCuenta(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnCuentaTieneSubCuenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnCuentaTieneSubCuenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnCuentaTieneSubCuenta(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameCuentaTieneSubCuenta(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnCuentaTieneSubCuenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnCuentaTieneSubCuenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameCuentaTieneSubCuenta() As String
   GetFnNameCuentaTieneSubCuenta = "CuentaTieneSubCuenta"
End Function

Private Function fFnParametersCuentaTieneSubCuenta() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30)
   fFnParametersCuentaTieneSubCuenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersCuentaTieneSubCuenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeCuentaTieneSubCuenta() As String
   fFnScalarReturnDataTypeCuentaTieneSubCuenta = "bit"
End Function

Private Function fStatementsFnCuentaTieneSubCuenta() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Result bit" & vbCrLf
   vResult = vResult & "   DECLARE @TieneSubCuentas " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   SET @Result = 0 --False" & vbCrLf
   vResult = vResult & "   SELECT @TieneSubCuentas = TieneSubCuentas FROM CUENTA WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Codigo = @CodigoCuenta" & vbCrLf
   vResult = vResult & "   IF (UPPER(@TieneSubCuentas) = 'S')" & vbCrLf
   vResult = vResult & "      SET @Result = 1 --True" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnCuentaTieneSubCuenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnCuentaTieneSubCuenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnCuadraElCodigoConLosNiveles(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameCuadraElCodigoConLosNiveles(), fFnParametersCuadraElCodigoConLosNiveles(), fFnScalarReturnDataTypeCuadraElCodigoConLosNiveles(), fStatementsFnCuadraElCodigoConLosNiveles(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnCuadraElCodigoConLosNiveles = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnCuadraElCodigoConLosNiveles", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnCuadraElCodigoConLosNiveles(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameCuadraElCodigoConLosNiveles(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnCuadraElCodigoConLosNiveles = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnCuadraElCodigoConLosNiveles", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameCuadraElCodigoConLosNiveles() As String
   GetFnNameCuadraElCodigoConLosNiveles = "CuadraElCodigoConLosNiveles"
End Function

Private Function fFnParametersCuadraElCodigoConLosNiveles() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@CodigoCuentaLimpio " & gUtilSQL.getVarCharTypeForDB(30)
   fFnParametersCuadraElCodigoConLosNiveles = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersCuadraElCodigoConLosNiveles", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeCuadraElCodigoConLosNiveles() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = gUtilSQL.getVarCharTypeForDB(30)
   fFnScalarReturnDataTypeCuadraElCodigoConLosNiveles = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnScalarReturnDataTypeCuadraElCodigoConLosNiveles", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsFnCuadraElCodigoConLosNiveles() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Result " & gUtilSQL.getVarCharTypeForDB(30) & vbCrLf
   vResult = vResult & "   DECLARE @Niveles nvarchar(max)" & vbCrLf
   vResult = vResult & "   SET @Niveles = dbo.Gf_LevelsOfDefCuenta(@ConsecutivoCompania, @ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "   SET @Result = dbo.Gf_CuadrarCodigo(@CodigoCuentaLimpio, @Niveles)" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsFnCuadraElCodigoConLosNiveles = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnCuadraElCodigoConLosNiveles", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnS10Exists(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameS10Exists(), fFnParametersS10Exists(), fFnScalarReturnDataTypeS10Exists(), fStatementsFnS10Exists(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnS10Exists = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnS10Exists", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnS10Exists(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameS10Exists(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnS10Exists = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnS10Exists", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameS10Exists() As String
   GetFnNameS10Exists = "S10_CuentaExists"
End Function

Private Function fFnParametersS10Exists() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania int, @CodigoCuenta varchar(10)"
   fFnParametersS10Exists = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersS10Exists", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeS10Exists() As String
   fFnScalarReturnDataTypeS10Exists = "bit"
End Function

Private Function fStatementsFnS10Exists() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Result bit" & vbCrLf
   vResult = vResult & "   SET @Result = 0 --False" & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT CUENTA.Codigo FROM CUENTA INNER JOIN PERIODO ON CUENTA.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo WHERE CUENTA.Codigo = @CodigoCuenta AND PERIODO.ConsecutivoCompania = @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "      SET @Result = 1 --True" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnS10Exists = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnS10Exists", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpInvS10CuentaUPD(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameInvS10CuentaUPD(), fSpParametersInvS10CuentaUPD(), fStatementsSpInvS10CuentaUPD(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpInvS10CuentaUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpInvS10CuentaUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpInvS10CuentaUPD(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameInvS10CuentaUPD(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpInvS10CuentaUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpInvS10CuentaUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameInvS10CuentaUPD() As String
   GetSpNameInvS10CuentaUPD = "InvS10_CuentaUPD"
End Function

Private Function fSpParametersInvS10CuentaUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "         @DescripcionCuenta " & gUtilSQL.getVarCharTypeForDB(40) & "," & vbCrLf
   vResult = vResult & "         @Naturaleza char(1)" & vbCrLf
    fSpParametersInvS10CuentaUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParameters0_CuentaUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpInvS10CuentaUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Continue bit" & vbCrLf
   vResult = vResult & "   DECLARE @NaturalezaVarchar  " & gUtilSQL.getVarCharTypeForDB(1) & "" & vbCrLf
   vResult = vResult & "   DECLARE @DataBaseName as " & gUtilSQL.getVarCharTypeForDB(128) & "" & vbCrLf
   vResult = vResult & "   DECLARE @SQLString nvarchar(500)" & vbCrLf
   vResult = vResult & "   DECLARE @ParmDefinition nvarchar(500)" & vbCrLf
   vResult = vResult & "   SELECT TOP 1 @DataBaseName = S10DBName from ParametrosAdministrativo" & vbCrLf
   vResult = vResult & "   IF (@DataBaseName IS NULL) OR (@DataBaseName = '')" & vbCrLf
   vResult = vResult & "      RAISERROR('No se puede ejecutar la sincronización de esta cuenta ya que no esta correctamente configurado la base de datos de conexión',16,3)" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @Continue = 0 --False" & vbCrLf
   vResult = vResult & "      SET @SQLString = 'SELECT @ContinueOUT = ' + @DataBaseName + '.dbo.S10f_ExistsCuenta(@ConsecutivoCompania, @CodigoCuenta)'" & vbCrLf
   vResult = vResult & "      SET @ParmDefinition = '@ConsecutivoCompania  int, @CodigoCuenta  " & gUtilSQL.getVarCharTypeForDB(30) & ", @ContinueOUT bit OUTPUT'" & vbCrLf
   vResult = vResult & "      EXEC sp_executesql @SQLString, @ParmDefinition, @ConsecutivoCompania = @ConsecutivoCompania, @CodigoCuenta = @CodigoCuenta, @ContinueOUT = @Continue OUTPUT" & vbCrLf
   vResult = vResult & "      IF @Continue = 1 --True" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SET @NaturalezaVarchar = CAST(@Naturaleza as char)" & vbCrLf
   vResult = vResult & "         SET @SQLString = 'EXEC ' + @DataBaseName + '.dbo.S10_Gp_CuentaUPD @ConsecutivoCompania, @CodigoCuenta, @DescripcionCuenta, @NaturalezaVarchar'" & vbCrLf
   vResult = vResult & "         SET @ParmDefinition = '@ConsecutivoCompania int, @CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30) & ", @DescripcionCuenta varchar(40), @NaturalezaVarchar varchar(1)'" & vbCrLf
   vResult = vResult & "         EXEC sp_executesql @SQLString, @ParmDefinition, @ConsecutivoCompania = @ConsecutivoCompania, @CodigoCuenta = @CodigoCuenta,  @DescripcionCuenta = @DescripcionCuenta, @NaturalezaVarchar = @NaturalezaVarchar" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpInvS10CuentaUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpStatementsInvS10CuentaUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *

Private Function fCreateSpInvS10CuentaINS(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameInvS10CuentaINS(), fSpParametersInvS10CuentaINS(), fStatementsSpInvS10CuentaINS(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpInvS10CuentaINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpInvS10CuentaINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpInvS10CuentaINS(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameInvS10CuentaINS(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpInvS10CuentaINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpInvS10CuentaINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameInvS10CuentaINS() As String
   GetSpNameInvS10CuentaINS = "InvS10_CuentaINS"
End Function

Private Function fSpParametersInvS10CuentaINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "         @DescripcionCuenta " & gUtilSQL.getVarCharTypeForDB(40) & "," & vbCrLf
   vResult = vResult & "         @Naturaleza char(1)" & vbCrLf
   fSpParametersInvS10CuentaINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParameters0_CuentaINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpInvS10CuentaINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Continue bit" & vbCrLf
   vResult = vResult & "   DECLARE @NaturalezaVarchar  " & gUtilSQL.getVarCharTypeForDB(1) & "" & vbCrLf
   vResult = vResult & "   DECLARE @DataBaseName as " & gUtilSQL.getVarCharTypeForDB(128) & "" & vbCrLf
   vResult = vResult & "   DECLARE @SQLString nvarchar(500)" & vbCrLf
   vResult = vResult & "   DECLARE @ParmDefinition nvarchar(500)" & vbCrLf
   vResult = vResult & "   SELECT TOP 1 @DataBaseName = S10DBName from ParametrosAdministrativo" & vbCrLf
   vResult = vResult & "   IF (@DataBaseName IS NULL) OR (@DataBaseName = '')" & vbCrLf
   vResult = vResult & "      RAISERROR('No se puede ejecutar la sincronización de esta cuenta ya que no esta correctamente configurado la base de datos de conexión',16,3)" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @Continue = 0 --False" & vbCrLf
   vResult = vResult & "      SET @SQLString = 'SELECT @ContinueOUT = ' + @DataBaseName + '.dbo.S10f_ExistsCuenta(@ConsecutivoCompania, @CodigoCuenta)'" & vbCrLf
   vResult = vResult & "      SET @ParmDefinition = '@ConsecutivoCompania  int, @CodigoCuenta  " & gUtilSQL.getVarCharTypeForDB(30) & ", @ContinueOUT bit OUTPUT'" & vbCrLf
   vResult = vResult & "      EXEC sp_executesql @SQLString, @ParmDefinition, @ConsecutivoCompania = @ConsecutivoCompania, @CodigoCuenta = @CodigoCuenta, @ContinueOUT = @Continue OUTPUT" & vbCrLf
   vResult = vResult & "      IF @Continue = 0 --False" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SET @NaturalezaVarchar = CAST(@Naturaleza as char)" & vbCrLf
   vResult = vResult & "         SET @SQLString = 'EXEC ' + @DataBaseName + '.dbo.S10_Gp_CuentaINS @ConsecutivoCompania, @CodigoCuenta, @DescripcionCuenta, @NaturalezaVarchar'" & vbCrLf
   vResult = vResult & "         SET @ParmDefinition = '@ConsecutivoCompania int, @CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30) & ", @DescripcionCuenta varchar(40), @NaturalezaVarchar varchar(1)'" & vbCrLf
   vResult = vResult & "         EXEC sp_executesql @SQLString, @ParmDefinition, @ConsecutivoCompania = @ConsecutivoCompania, @CodigoCuenta = @CodigoCuenta,  @DescripcionCuenta = @DescripcionCuenta, @NaturalezaVarchar = @NaturalezaVarchar" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpInvS10CuentaINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpInvS10CuentaINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpInvS10CuentaDEL(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameInvS10CuentaDEL(), fSpParametersInvS10CuentaDEL(), fStatementsSpInvS10CuentaDEL(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpInvS10CuentaDEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpInvS10CuentaDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpInvS10CuentaDEL(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameInvS10CuentaDEL(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpInvS10CuentaDEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpInvS10CuentaDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameInvS10CuentaDEL() As String
   GetSpNameInvS10CuentaDEL = "InvS10_CuentaDEL"
End Function

Private Function fSpParametersInvS10CuentaDEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
    fSpParametersInvS10CuentaDEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersInvS10CuentaDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpInvS10CuentaDEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Continue bit" & vbCrLf
   vResult = vResult & "   DECLARE @DataBaseName as " & gUtilSQL.getVarCharTypeForDB(128) & "" & vbCrLf
   vResult = vResult & "   DECLARE @SQLString nvarchar(500)" & vbCrLf
   vResult = vResult & "   DECLARE @ParmDefinition nvarchar(500)" & vbCrLf
   vResult = vResult & "   SELECT TOP 1 @DataBaseName = S10DBName from ParametrosAdministrativo" & vbCrLf
   vResult = vResult & "   IF (@DataBaseName IS NULL) OR (@DataBaseName = '')" & vbCrLf
   vResult = vResult & "      RAISERROR('No se puede ejecutar la sincronización de esta cuenta ya que no esta correctamente configurado la base de datos de conexión',16,3)" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @Continue = 0 --False" & vbCrLf
   vResult = vResult & "      SET @SQLString = 'SELECT @ContinueOUT = ' + @DataBaseName + '.dbo.S10f_ExistsCuenta(@ConsecutivoCompania, @CodigoCuenta)'" & vbCrLf
   vResult = vResult & "      SET @ParmDefinition = '@ConsecutivoCompania  int, @CodigoCuenta  " & gUtilSQL.getVarCharTypeForDB(30) & ", @ContinueOUT bit OUTPUT'" & vbCrLf
   vResult = vResult & "      EXEC sp_executesql @SQLString, @ParmDefinition, @ConsecutivoCompania = @ConsecutivoCompania, @CodigoCuenta = @CodigoCuenta, @ContinueOUT = @Continue OUTPUT" & vbCrLf
   vResult = vResult & "      IF @Continue = 1 --True" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SET @SQLString = 'EXEC ' + @DataBaseName + '.dbo.S10_Gp_CuentaDEL @ConsecutivoCompania, @CodigoCuenta'" & vbCrLf
   vResult = vResult & "         SET @ParmDefinition = '@ConsecutivoCompania int, @CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30) & "'" & vbCrLf
   vResult = vResult & "         EXEC sp_executesql @SQLString, @ParmDefinition, @ConsecutivoCompania = @ConsecutivoCompania, @CodigoCuenta = @CodigoCuenta" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpInvS10CuentaDEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpInvS10CuentaDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *


