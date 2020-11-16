VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsUsuarioSP"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsUsuarioSp"
End Function
Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsUsuarioSp"
End Function
Public Function fCreateDropUsuarioSP(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
'      vResult = vResult And fCreateSpINS(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnUsuarioParaS10(valShowMessage, valWithEncryption)
   Else
      vResult = vResult And fDropFnUsuarioParaS10(valShowMessage)
      vResult = vResult And fDropSpINS(valShowMessage)
   End If
   fCreateDropUsuarioSP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropUsuarioSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSPINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSPINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function GetSpNameINS() As String
   GetSpNameINS = "UsuarioINS"
End Function
Private Function fSpParametersINS() As String
   Dim vResult As String
   vResult = " @NombreDelUsuario " & gUtilSQL.getVarCharTypeForDB(10) & ", " & vbCrLf
   vResult = vResult & " @Status " & gUtilSQL.getCharTypeForDB(1) & " = '0',"
   vResult = vResult & " @NombreUltimaCompaniaUsada " & gUtilSQL.getVarCharTypeForDB(50) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @FldPassword " & gUtilSQL.getVarCharTypeForDB(10) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @NombreYApellido " & gUtilSQL.getVarCharTypeForDB(40) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @Cargo " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @EsSupervisor " & gUtilSQL.getCharTypeForDB(1) & " = " & gUtilSQL.fBooleanToStrSQL(False) & ", " & vbCrLf
   vResult = vResult & " @email " & gUtilSQL.getVarCharTypeForDB(30) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @UsaContabilidad " & gUtilSQL.getCharTypeForDB(1) & " = " & gUtilSQL.fBooleanToStrSQL(False) & ", " & vbCrLf
   vResult = vResult & " @OpcionesCompania " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @Nivel1 " & gUtilSQL.getCharTypeForDB(1) & " = " & gUtilSQL.fBooleanToStrSQL(False) & ", " & vbCrLf
   vResult = vResult & " @Nivel2 " & gUtilSQL.getCharTypeForDB(1) & " = " & gUtilSQL.fBooleanToStrSQL(False) & ", " & vbCrLf
   vResult = vResult & " @Nivel3 " & gUtilSQL.getCharTypeForDB(1) & " = " & gUtilSQL.fBooleanToStrSQL(False) & ", " & vbCrLf
   vResult = vResult & " @Nivel4 " & gUtilSQL.getCharTypeForDB(1) & " = " & gUtilSQL.fBooleanToStrSQL(False) & ", " & vbCrLf
   vResult = vResult & " @OpcionesFactura " & gUtilSQL.getVarCharTypeForDB(40) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesResumenDiarioVentas " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesCajaRegistradora " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesCotizacion " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesCliente " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesAutorizacion " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesLlamada " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesVisita " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesCxC " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesCobranza " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesArtInventario " & gUtilSQL.getVarCharTypeForDB(40) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesAlmacen " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesNotaEntSalida " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesExistXAlmacen " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesTransferencia " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesVendedor " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesTablas " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesParametros " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesUsuario " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesDespacho " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesOrdenDeServicio " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesCurso " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesCxP " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesPago " & gUtilSQL.getVarCharTypeForDB(40) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesProveedor " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesTablaRetencion " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesContrato " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesCuentaBancaria " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesMovimientoBancario " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesConceptoBancario " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesAlicuotaIVA " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesCompra " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesConteoFisico " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesPeriodo " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesCuenta " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesComprobante " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesCatalogoGeneral " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesActivoFijo " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesConciliacion " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesReglasDeContabilizacion " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesPlantillaRet " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesARCV " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesRA " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesGestionDeCobranza " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesPlanillaForma00030 " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesLoteAdm " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesAnticipoCobrado " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesAnticipoPagado " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @MenuDeBalances " & gUtilSQL.getCharTypeForDB(1) & " = " & gUtilSQL.fBooleanToStrSQL(False) & ", " & vbCrLf
   vResult = vResult & " @MenuDeInformes " & gUtilSQL.getCharTypeForDB(1) & " = " & gUtilSQL.fBooleanToStrSQL(False) & ", " & vbCrLf
   vResult = vResult & " @EsUsuarioTipoCajero " & gUtilSQL.getCharTypeForDB(1) & " = " & gUtilSQL.fBooleanToStrSQL(False) & ", " & vbCrLf
   vResult = vResult & " @OpcionesCajeroFactura " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesCajeroCliente " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesCajeroArtInventario " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesCajeroCaja " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesColor " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesTalla " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesGrupoTC " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & ", " & vbCrLf
   vResult = vResult & " @OpcionesExportarImportarSAW " & gUtilSQL.getVarCharTypeForDB(20) & " = " & gUtilSQL.fSimpleSqlValue("") & vbCrLf
   
   fSpParametersINS = vResult
End Function
Private Function fStatementsSpINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Psw varchar(10) " & vbCrLf
   vResult = vResult & "   SET @FechaUltimaModificacion = CONVERT(" & gUtilSQL.getDateTypeForDB() & ", GETDATE(),103) " & vbCrLf
   vResult = vResult & "   IF @FldPassword = '' " & vbCrLf
   vResult = vResult & "      SET @Psw = @NombreDelUsuario " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Psw = @FldPassword " & vbCrLf
   vResult = vResult & "   INSERT INTO USUARIO(NombreDelUsuario " & vbCrLf
   vResult = vResult & "      ,Status " & vbCrLf
   vResult = vResult & "      ,NombreUltimaCompaniaUsada " & vbCrLf
   vResult = vResult & "      ,FldPassword " & vbCrLf
   vResult = vResult & "      ,NombreYApellido " & vbCrLf
   vResult = vResult & "      ,Cargo " & vbCrLf
   vResult = vResult & "      ,EsSupervisor " & vbCrLf
   vResult = vResult & "      ,email " & vbCrLf
   vResult = vResult & "      ,UsaContabilidad " & vbCrLf
   vResult = vResult & "      ,OpcionesCompania " & vbCrLf
   vResult = vResult & "      ,Nivel1 " & vbCrLf
   vResult = vResult & "      ,Nivel2 " & vbCrLf
   vResult = vResult & "      ,Nivel3 " & vbCrLf
   vResult = vResult & "      ,Nivel4 " & vbCrLf
   vResult = vResult & "      ,OpcionesFactura " & vbCrLf
   vResult = vResult & "      ,OpcionesResumenDiarioVentas " & vbCrLf
   vResult = vResult & "      ,OpcionesCajaRegistradora " & vbCrLf
   vResult = vResult & "      ,OpcionesCotizacion " & vbCrLf
   vResult = vResult & "      ,OpcionesCliente " & vbCrLf
   vResult = vResult & "      ,OpcionesAutorizacion " & vbCrLf
   vResult = vResult & "      ,OpcionesLlamada " & vbCrLf
   vResult = vResult & "      ,OpcionesVisita " & vbCrLf
   vResult = vResult & "      ,OpcionesCxC " & vbCrLf
   vResult = vResult & "      ,OpcionesCobranza " & vbCrLf
   vResult = vResult & "      ,OpcionesArtInventario " & vbCrLf
   vResult = vResult & "      ,OpcionesAlmacen " & vbCrLf
   vResult = vResult & "      ,OpcionesNotaEntSalida " & vbCrLf
   vResult = vResult & "      ,OpcionesExistXAlmacen " & vbCrLf
   vResult = vResult & "      ,OpcionesTransferencia " & vbCrLf
   vResult = vResult & "      ,OpcionesVendedor " & vbCrLf
   vResult = vResult & "      ,OpcionesTablas " & vbCrLf
   vResult = vResult & "      ,OpcionesParametros " & vbCrLf
   vResult = vResult & "      ,OpcionesUsuario " & vbCrLf
   vResult = vResult & "      ,OpcionesDespacho " & vbCrLf
   vResult = vResult & "      ,OpcionesOrdenDeServicio " & vbCrLf
   vResult = vResult & "      ,OpcionesCurso " & vbCrLf
   vResult = vResult & "      ,OpcionesCxP " & vbCrLf
   vResult = vResult & "      ,OpcionesPago " & vbCrLf
   vResult = vResult & "      ,OpcionesProveedor " & vbCrLf
   vResult = vResult & "      ,OpcionesTablaRetencion " & vbCrLf
   vResult = vResult & "      ,OpcionesContrato " & vbCrLf
   vResult = vResult & "      ,OpcionesCuentaBancaria " & vbCrLf
   vResult = vResult & "      ,OpcionesMovimientoBancario " & vbCrLf
   vResult = vResult & "      ,OpcionesConceptoBancario " & vbCrLf
   vResult = vResult & "      ,OpcionesAlicuotaIVA " & vbCrLf
   vResult = vResult & "      ,OpcionesCompra " & vbCrLf
   vResult = vResult & "      ,OpcionesConteoFisico " & vbCrLf
   vResult = vResult & "      ,OpcionesPeriodo " & vbCrLf
   vResult = vResult & "      ,OpcionesCuenta " & vbCrLf
   vResult = vResult & "      ,OpcionesComprobante " & vbCrLf
   vResult = vResult & "      ,OpcionesCatalogoGeneral " & vbCrLf
   vResult = vResult & "      ,OpcionesActivoFijo " & vbCrLf
   vResult = vResult & "      ,OpcionesConciliacion " & vbCrLf
   vResult = vResult & "      ,OpcionesReglasDeContabilizacion " & vbCrLf
   vResult = vResult & "      ,OpcionesPlantillaRet " & vbCrLf
   vResult = vResult & "      ,OpcionesARCV " & vbCrLf
   vResult = vResult & "      ,OpcionesRA " & vbCrLf
   vResult = vResult & "      ,OpcionesGestionDeCobranza " & vbCrLf
   vResult = vResult & "      ,OpcionesPlanillaForma00030 " & vbCrLf
   vResult = vResult & "      ,OpcionesLoteAdm " & vbCrLf
   vResult = vResult & "      ,OpcionesAnticipoCobrado " & vbCrLf
   vResult = vResult & "      ,OpcionesAnticipoPagado " & vbCrLf
   vResult = vResult & "      ,MenuDeBalances " & vbCrLf
   vResult = vResult & "      ,MenuDeInformes " & vbCrLf
   vResult = vResult & "      ,EsUsuarioTipoCajero " & vbCrLf
   vResult = vResult & "      ,OpcionesCajeroFactura " & vbCrLf
   vResult = vResult & "      ,OpcionesCajeroCliente " & vbCrLf
   vResult = vResult & "      ,OpcionesCajeroArtInventario " & vbCrLf
   vResult = vResult & "      ,OpcionesCajeroCaja " & vbCrLf
   vResult = vResult & "      ,OpcionesColor " & vbCrLf
   vResult = vResult & "      ,OpcionesTalla " & vbCrLf
   vResult = vResult & "      ,OpcionesGrupoTC " & vbCrLf
   vResult = vResult & "      ,OpcionesExportarImportarSAW " & vbCrLf
   vResult = vResult & "      ,fechaUltimaModificacion " & vbCrLf
   vResult = vResult & "   )VALUES( " & vbCrLf
   vResult = vResult & "         @NombreDelUsuario, " & vbCrLf
   vResult = vResult & "         @Status, " & vbCrLf
   vResult = vResult & "         @NombreUltimaCompaniaUsada, " & vbCrLf
   vResult = vResult & "         @Psw, " & vbCrLf
   vResult = vResult & "         @NombreYApellido, " & vbCrLf
   vResult = vResult & "         @Cargo, " & vbCrLf
   vResult = vResult & "         @EsSupervisor, " & vbCrLf
   vResult = vResult & "         @email, " & vbCrLf
   vResult = vResult & "         @UsaContabilidad, " & vbCrLf
   vResult = vResult & "         @OpcionesCompania, " & vbCrLf
   vResult = vResult & "         @Nivel1, " & vbCrLf
   vResult = vResult & "         @Nivel2, " & vbCrLf
   vResult = vResult & "         @Nivel3, " & vbCrLf
   vResult = vResult & "         @Nivel4, " & vbCrLf
   vResult = vResult & "         @OpcionesFactura, " & vbCrLf
   vResult = vResult & "         @OpcionesResumenDiarioVentas, " & vbCrLf
   vResult = vResult & "         @OpcionesCajaRegistradora, " & vbCrLf
   vResult = vResult & "         @OpcionesCotizacion, " & vbCrLf
   vResult = vResult & "         @OpcionesCliente, " & vbCrLf
   vResult = vResult & "         @OpcionesAutorizacion, " & vbCrLf
   vResult = vResult & "         @OpcionesLlamada, " & vbCrLf
   vResult = vResult & "         @OpcionesVisita, " & vbCrLf
   vResult = vResult & "         @OpcionesCxC, " & vbCrLf
   vResult = vResult & "         @OpcionesCobranza, " & vbCrLf
   vResult = vResult & "         @OpcionesArtInventario, " & vbCrLf
   vResult = vResult & "         @OpcionesAlmacen, " & vbCrLf
   vResult = vResult & "         @OpcionesNotaEntSalida, " & vbCrLf
   vResult = vResult & "         @OpcionesExistXAlmacen, " & vbCrLf
   vResult = vResult & "         @OpcionesTransferencia, " & vbCrLf
   vResult = vResult & "         @OpcionesVendedor, " & vbCrLf
   vResult = vResult & "         @OpcionesTablas, " & vbCrLf
   vResult = vResult & "         @OpcionesParametros, " & vbCrLf
   vResult = vResult & "         @OpcionesUsuario, " & vbCrLf
   vResult = vResult & "         @OpcionesDespacho, " & vbCrLf
   vResult = vResult & "         @OpcionesOrdenDeServicio, " & vbCrLf
   vResult = vResult & "         @OpcionesCurso, " & vbCrLf
   vResult = vResult & "         @OpcionesCxP, " & vbCrLf
   vResult = vResult & "         @OpcionesPago, " & vbCrLf
   vResult = vResult & "         @OpcionesProveedor, " & vbCrLf
   vResult = vResult & "         @OpcionesTablaRetencion, " & vbCrLf
   vResult = vResult & "         @OpcionesContrato, " & vbCrLf
   vResult = vResult & "         @OpcionesCuentaBancaria, " & vbCrLf
   vResult = vResult & "         @OpcionesMovimientoBancario, " & vbCrLf
   vResult = vResult & "         @OpcionesConceptoBancario, " & vbCrLf
   vResult = vResult & "         @OpcionesAlicuotaIVA, " & vbCrLf
   vResult = vResult & "         @OpcionesCompra, " & vbCrLf
   vResult = vResult & "         @OpcionesConteoFisico, " & vbCrLf
   vResult = vResult & "         @OpcionesPeriodo, " & vbCrLf
   vResult = vResult & "         @OpcionesCuenta, " & vbCrLf
   vResult = vResult & "         @OpcionesComprobante, " & vbCrLf
   vResult = vResult & "         @OpcionesCatalogoGeneral, " & vbCrLf
   vResult = vResult & "         @OpcionesActivoFijo, " & vbCrLf
   vResult = vResult & "         @OpcionesConciliacion, " & vbCrLf
   vResult = vResult & "         @OpcionesReglasDeContabilizacion, " & vbCrLf
   vResult = vResult & "         @OpcionesPlantillaRet, " & vbCrLf
   vResult = vResult & "         @OpcionesARCV, " & vbCrLf
   vResult = vResult & "         @OpcionesRA, " & vbCrLf
   vResult = vResult & "         @OpcionesGestionDeCobranza, " & vbCrLf
   vResult = vResult & "         @OpcionesPlanillaForma00030, " & vbCrLf
   vResult = vResult & "         @OpcionesLoteAdm, " & vbCrLf
   vResult = vResult & "         @OpcionesAnticipoCobrado, " & vbCrLf
   vResult = vResult & "         @OpcionesAnticipoPagado, " & vbCrLf
   vResult = vResult & "         @MenuDeBalances, " & vbCrLf
   vResult = vResult & "         @MenuDeInformes, " & vbCrLf
   vResult = vResult & "         @EsUsuarioTipoCajero, " & vbCrLf
   vResult = vResult & "         @OpcionesCajeroFactura, " & vbCrLf
   vResult = vResult & "         @OpcionesCajeroCliente, " & vbCrLf
   vResult = vResult & "         @OpcionesCajeroArtInventario, " & vbCrLf
   vResult = vResult & "         @OpcionesCajeroCaja, " & vbCrLf
   vResult = vResult & "         @OpcionesColor, " & vbCrLf
   vResult = vResult & "         @OpcionesTalla, " & vbCrLf
   vResult = vResult & "         @OpcionesGrupoTC, " & vbCrLf
   vResult = vResult & "         @OpcionesExportarImportarSAW, " & vbCrLf
   vResult = vResult & "         @FechaUltimaModificacion) " & vbCrLf
   vResult = vResult & "END "
   fStatementsSpINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSPINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnUsuarioParaS10(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameUsuarioParaS10(), fFnParametersUsuarioParaS10(), fFnScalarReturnDataTypeUsuarioParaS10(), fStatementsFnUsuarioParaS10(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnUsuarioParaS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnUsuarioParaS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fDropFnUsuarioParaS10(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameUsuarioParaS10(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnUsuarioParaS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnUsuarioParaS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function GetFnNameUsuarioParaS10() As String
   GetFnNameUsuarioParaS10 = "UsuarioS10"
End Function
Private Function fFnParametersUsuarioParaS10() As String
   fFnParametersUsuarioParaS10 = ""
End Function
Private Function fFnScalarReturnDataTypeUsuarioParaS10() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = gUtilSQL.getVarCharTypeForDB(10)
   fFnScalarReturnDataTypeUsuarioParaS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnScalarReturnDataTypeUsuarioParaS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fStatementsFnUsuarioParaS10() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @UsuarioS10 " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   SET @UsuarioS10 = " & gUtilSQL.fSimpleSqlValue("UserS10") & vbCrLf
   vResult = vResult & "   RETURN @UsuarioS10 " & vbCrLf
   vResult = vResult & " END "
   fStatementsFnUsuarioParaS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnUsuarioParaS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *

