VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsProveedorSP"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsProveedorSp"
End Function
Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsProveedorSp"
End Function
Public Function fCreateDropProveedorSP(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      vResult = vResult And fCreateSpINS(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpUPD(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpDel(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnProveedorCanBeDeleted(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpExecuteDel(valShowMessage, valWithEncryption)
      
      vResult = vResult And fCreateFnS10Exists(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10Insert(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10Select(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10Update(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10Delete(valShowMessage, valWithEncryption)
   Else
      vResult = vResult And fDropSpS10Delete(valShowMessage)
      vResult = vResult And fDropSpS10Update(valShowMessage)
      vResult = vResult And fDropSpS10Select(valShowMessage)
      vResult = vResult And fDropSpS10Insert(valShowMessage)
      vResult = vResult And fDropFnS10Exists(valShowMessage)
      vResult = vResult And fDropFnProveedorCanBeDeleted(valShowMessage)
      vResult = vResult And fDropSpExecuteDel(valShowMessage)
      vResult = vResult And fDropSpDel(valShowMessage)
      vResult = vResult And fDropSpUPD(valShowMessage)
      vResult = vResult And fDropSpINS(valShowMessage)
   End If
   fCreateDropProveedorSP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropProveedorSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetSpNameINS = "ProveedorINS"
End Function
Private Function fSpParametersINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "         @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "         @NombreProveedor " & gUtilSQL.getVarCharTypeForDB(60) & "," & vbCrLf
   vResult = vResult & "         @Contacto " & gUtilSQL.getVarCharTypeForDB(35) & " = ''," & vbCrLf
   vResult = vResult & "         @NumeroRIF " & gUtilSQL.getVarCharTypeForDB(20) & " = ''," & vbCrLf
   vResult = vResult & "         @NumeroNIT " & gUtilSQL.getVarCharTypeForDB(12) & " = ''," & vbCrLf
   vResult = vResult & "         @TipoDePersona char(1) = '0'," & vbCrLf
   vResult = vResult & "         @CodigoRetencionUsual " & gUtilSQL.getVarCharTypeForDB(6) & "," & vbCrLf
   vResult = vResult & "         @Telefonos " & gUtilSQL.getVarCharTypeForDB(40) & " = ''," & vbCrLf
   vResult = vResult & "         @Direccion " & gUtilSQL.getVarCharTypeForDB(255) & " = ''," & vbCrLf
   vResult = vResult & "         @Fax " & gUtilSQL.getVarCharTypeForDB(25) & " = ''," & vbCrLf
   vResult = vResult & "         @Email " & gUtilSQL.getVarCharTypeForDB(40) & " = ''," & vbCrLf
   vResult = vResult & "         @TipodeProveedor " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "         @TipoDeProveedorDeLibrosFiscales char(1) = '0'," & vbCrLf
   vResult = vResult & "         @PorcentajeRetencionIVA money = 0," & vbCrLf
   vResult = vResult & "         @CuentaContableCxP " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "         @CuentaContableGastos " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "         @CuentaContableAnticipo " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "         @CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & " = ''," & vbCrLf
   vResult = vResult & "         @Beneficiario " & gUtilSQL.getVarCharTypeForDB(60) & " = ''," & vbCrLf
   vResult = vResult & "         @UsarBeneficiarioImpCheq char(1) = 'N'," & vbCrLf
   vResult = vResult & "         @TipoDocumentoIdentificacion char(1) = '0'," & vbCrLf
   vResult = vResult & "         @EsAgenteDeRetencionIva char(1) = 'N'," & vbCrLf
   vResult = vResult & "         @ApellidoPaterno " & gUtilSQL.getVarCharTypeForDB(20) & " = ''," & vbCrLf
   vResult = vResult & "         @ApellidoMaterno " & gUtilSQL.getVarCharTypeForDB(20) & " = ''," & vbCrLf
   vResult = vResult & "         @Nombre " & gUtilSQL.getVarCharTypeForDB(20) & " = ''," & vbCrLf
   vResult = vResult & "         @NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
    fSpParametersINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fStatementsSpINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   SET @FechaUltimaModificacion = " & gUtilSQL.DfCDateSQL(gUtilSQL.GetSQLFechaActual()) & "" & vbCrLf
   vResult = vResult & "   INSERT INTO Proveedor(" & vbCrLf
   vResult = vResult & "         ConsecutivoCompania," & vbCrLf
   vResult = vResult & "         CodigoProveedor," & vbCrLf
   vResult = vResult & "         NombreProveedor," & vbCrLf
   vResult = vResult & "         Contacto," & vbCrLf
   vResult = vResult & "         NumeroRIF," & vbCrLf
   vResult = vResult & "         NumeroNIT," & vbCrLf
   vResult = vResult & "         TipoDePersona," & vbCrLf
   vResult = vResult & "         CodigoRetencionUsual," & vbCrLf
   vResult = vResult & "         Telefonos," & vbCrLf
   vResult = vResult & "         Direccion," & vbCrLf
   vResult = vResult & "         Fax," & vbCrLf
   vResult = vResult & "         Email," & vbCrLf
   vResult = vResult & "         TipodeProveedor," & vbCrLf
   vResult = vResult & "         TipoDeProveedorDeLibrosFiscales," & vbCrLf
   vResult = vResult & "         PorcentajeRetencionIVA," & vbCrLf
   vResult = vResult & "         CuentaContableCxP," & vbCrLf
   vResult = vResult & "         CuentaContableGastos," & vbCrLf
   vResult = vResult & "         CuentaContableAnticipo," & vbCrLf
   vResult = vResult & "         CodigoLote," & vbCrLf
   vResult = vResult & "         Beneficiario," & vbCrLf
   vResult = vResult & "         UsarBeneficiarioImpCheq," & vbCrLf
   vResult = vResult & "         TipoDocumentoIdentificacion," & vbCrLf
   vResult = vResult & "         EsAgenteDeRetencionIva," & vbCrLf
   vResult = vResult & "         Nombre," & vbCrLf
   vResult = vResult & "         ApellidoPaterno," & vbCrLf
   vResult = vResult & "         ApellidoMaterno," & vbCrLf
   vResult = vResult & "         NombreOperador," & vbCrLf
   vResult = vResult & "         FechaUltimaModificacion)" & vbCrLf
   vResult = vResult & "      VALUES(" & vbCrLf
   vResult = vResult & "         @ConsecutivoCompania," & vbCrLf
   vResult = vResult & "         @CodigoProveedor," & vbCrLf
   vResult = vResult & "         @NombreProveedor," & vbCrLf
   vResult = vResult & "         @Contacto," & vbCrLf
   vResult = vResult & "         @NumeroRIF," & vbCrLf
   vResult = vResult & "         @NumeroNIT," & vbCrLf
   vResult = vResult & "         @TipoDePersona," & vbCrLf
   vResult = vResult & "         @CodigoRetencionUsual," & vbCrLf
   vResult = vResult & "         @Telefonos," & vbCrLf
   vResult = vResult & "         @Direccion," & vbCrLf
   vResult = vResult & "         @Fax," & vbCrLf
   vResult = vResult & "         @Email," & vbCrLf
   vResult = vResult & "         @TipodeProveedor," & vbCrLf
   vResult = vResult & "         @TipoDeProveedorDeLibrosFiscales," & vbCrLf
   vResult = vResult & "         @PorcentajeRetencionIVA," & vbCrLf
   vResult = vResult & "         @CuentaContableCxP," & vbCrLf
   vResult = vResult & "         @CuentaContableGastos," & vbCrLf
   vResult = vResult & "         @CuentaContableAnticipo," & vbCrLf
   vResult = vResult & "         @CodigoLote," & vbCrLf
   vResult = vResult & "         @Beneficiario," & vbCrLf
   vResult = vResult & "         @UsarBeneficiarioImpCheq," & vbCrLf
   vResult = vResult & "         @TipoDocumentoIdentificacion," & vbCrLf
   vResult = vResult & "         @EsAgenteDeRetencionIva," & vbCrLf
   vResult = vResult & "         @Nombre," & vbCrLf
   vResult = vResult & "         @ApellidoPaterno," & vbCrLf
   vResult = vResult & "         @ApellidoMaterno," & vbCrLf
   vResult = vResult & "         @NombreOperador," & vbCrLf
   vResult = vResult & "         @FechaUltimaModificacion)" & vbCrLf
   vResult = vResult & "   RETURN @@ROWCOUNT" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Public Function fCreateSpUPD(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
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
Public Function fDropSpUPD(ByVal valShowMessage As Boolean) As Boolean
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
   GetSpNameUPD = "ProveedorUPD"
End Function
Private Function fSpParametersUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "      @CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "      @NombreProveedor " & gUtilSQL.getVarCharTypeForDB(60) & " = NULL," & vbCrLf
   vResult = vResult & "      @Contacto " & gUtilSQL.getVarCharTypeForDB(35) & " = NULL," & vbCrLf
   vResult = vResult & "      @NumeroRIF " & gUtilSQL.getVarCharTypeForDB(20) & " = NULL," & vbCrLf
   vResult = vResult & "      @NumeroNIT " & gUtilSQL.getVarCharTypeForDB(12) & " = NULL," & vbCrLf
   vResult = vResult & "      @TipoDePersona char(1) = NULL," & vbCrLf
   vResult = vResult & "      @CodigoRetencionUsual " & gUtilSQL.getVarCharTypeForDB(6) & " = NULL," & vbCrLf
   vResult = vResult & "      @Telefonos " & gUtilSQL.getVarCharTypeForDB(40) & " = NULL," & vbCrLf
   vResult = vResult & "      @Direccion " & gUtilSQL.getVarCharTypeForDB(255) & " = NULL," & vbCrLf
   vResult = vResult & "      @Fax " & gUtilSQL.getVarCharTypeForDB(25) & " = NULL," & vbCrLf
   vResult = vResult & "      @Email " & gUtilSQL.getVarCharTypeForDB(40) & " = NULL," & vbCrLf
   vResult = vResult & "      @TipodeProveedor " & gUtilSQL.getVarCharTypeForDB(20) & " = NULL," & vbCrLf
   vResult = vResult & "      @TipoDeProveedorDeLibrosFiscales char(1) = NULL," & vbCrLf
   vResult = vResult & "      @PorcentajeRetencionIVA money = NULL," & vbCrLf
   vResult = vResult & "      @CuentaContableCxP " & gUtilSQL.getVarCharTypeForDB(30) & " = NULL," & vbCrLf
   vResult = vResult & "      @CuentaContableGastos " & gUtilSQL.getVarCharTypeForDB(30) & " = NULL," & vbCrLf
   vResult = vResult & "      @CuentaContableAnticipo " & gUtilSQL.getVarCharTypeForDB(30) & " = NULL," & vbCrLf
   vResult = vResult & "      @CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & " = NULL," & vbCrLf
   vResult = vResult & "      @Beneficiario " & gUtilSQL.getVarCharTypeForDB(60) & " = NULL," & vbCrLf
   vResult = vResult & "      @UsarBeneficiarioImpCheq char(1) = NULL," & vbCrLf
   vResult = vResult & "      @TipoDocumentoIdentificacion char(1) = NULL," & vbCrLf
   vResult = vResult & "      @EsAgenteDeRetencionIva char(1) = NULL," & vbCrLf
   vResult = vResult & "      @ApellidoPaterno " & gUtilSQL.getVarCharTypeForDB(20) & " = NULL," & vbCrLf
   vResult = vResult & "      @ApellidoMaterno " & gUtilSQL.getVarCharTypeForDB(20) & " = NULL," & vbCrLf
   vResult = vResult & "      @Nombre " & gUtilSQL.getVarCharTypeForDB(20) & " = NULL," & vbCrLf
   vResult = vResult & "      @NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & " = NULL" & vbCrLf
    fSpParametersUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NombreProveedor " & gUtilSQL.getVarCharTypeForDB(60) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Contacto " & gUtilSQL.getVarCharTypeForDB(35) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroRIF " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroNIT " & gUtilSQL.getVarCharTypeForDB(12) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TipoDePersona char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoRetencionUsual " & gUtilSQL.getVarCharTypeForDB(6) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Telefonos " & gUtilSQL.getVarCharTypeForDB(40) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Direccion " & gUtilSQL.getVarCharTypeForDB(255) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Fax " & gUtilSQL.getVarCharTypeForDB(25) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Email " & gUtilSQL.getVarCharTypeForDB(40) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TipodeProveedor " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TipoDeProveedorDeLibrosFiscales char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_PorcentajeRetencionIVA money" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CuentaContableCxP " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CuentaContableGastos " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CuentaContableAnticipo " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Beneficiario " & gUtilSQL.getVarCharTypeForDB(60) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_UsarBeneficiarioImpCheq char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TipoDocumentoIdentificacion char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_EsAgenteDeRetencionIva char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_ApellidoPaterno " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_ApellidoMaterno " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Nombre " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TimeStamp timestamp" & vbCrLf
   vResult = vResult & "   DECLARE @Up_NombreProveedor " & gUtilSQL.getVarCharTypeForDB(60) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Contacto " & gUtilSQL.getVarCharTypeForDB(35) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroRIF " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroNIT " & gUtilSQL.getVarCharTypeForDB(12) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_TipoDePersona char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoRetencionUsual " & gUtilSQL.getVarCharTypeForDB(6) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Telefonos " & gUtilSQL.getVarCharTypeForDB(40) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Direccion " & gUtilSQL.getVarCharTypeForDB(255) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Fax " & gUtilSQL.getVarCharTypeForDB(25) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Email " & gUtilSQL.getVarCharTypeForDB(40) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_TipodeProveedor " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_TipoDeProveedorDeLibrosFiscales char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_PorcentajeRetencionIVA money" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CuentaContableCxP " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CuentaContableGastos " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CuentaContableAnticipo " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Beneficiario " & gUtilSQL.getVarCharTypeForDB(60) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_UsarBeneficiarioImpCheq char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_TipoDocumentoIdentificacion char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_EsAgenteDeRetencionIva char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_ApellidoPaterno " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_ApellidoMaterno " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Nombre " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   BEGIN TRAN" & vbCrLf
   vResult = vResult & "   SELECT @Ori_NombreProveedor = NombreProveedor," & vbCrLf
   vResult = vResult & "           @Ori_Contacto = Contacto," & vbCrLf
   vResult = vResult & "           @Ori_NumeroRIF = NumeroRIF," & vbCrLf
   vResult = vResult & "           @Ori_NumeroNIT = NumeroNIT," & vbCrLf
   vResult = vResult & "           @Ori_TipoDePersona = TipoDePersona," & vbCrLf
   vResult = vResult & "           @Ori_CodigoRetencionUsual = CodigoRetencionUsual," & vbCrLf
   vResult = vResult & "           @Ori_Telefonos = Telefonos," & vbCrLf
   vResult = vResult & "           @Ori_Direccion = Direccion," & vbCrLf
   vResult = vResult & "           @Ori_Fax = Fax," & vbCrLf
   vResult = vResult & "           @Ori_Email = Email," & vbCrLf
   vResult = vResult & "           @Ori_TipodeProveedor = TipodeProveedor," & vbCrLf
   vResult = vResult & "           @Ori_TipoDeProveedorDeLibrosFiscales = TipoDeProveedorDeLibrosFiscales," & vbCrLf
   vResult = vResult & "           @Ori_PorcentajeRetencionIVA = PorcentajeRetencionIVA," & vbCrLf
   vResult = vResult & "           @Ori_CuentaContableCxP = CuentaContableCxP," & vbCrLf
   vResult = vResult & "           @Ori_CuentaContableGastos = CuentaContableGastos," & vbCrLf
   vResult = vResult & "           @Ori_CuentaContableAnticipo = CuentaContableAnticipo," & vbCrLf
   vResult = vResult & "           @Ori_CodigoLote = CodigoLote," & vbCrLf
   vResult = vResult & "           @Ori_Beneficiario = Beneficiario," & vbCrLf
   vResult = vResult & "           @Ori_UsarBeneficiarioImpCheq = UsarBeneficiarioImpCheq," & vbCrLf
   vResult = vResult & "           @Ori_TipoDocumentoIdentificacion = TipoDocumentoIdentificacion," & vbCrLf
   vResult = vResult & "           @Ori_EsAgenteDeRetencionIva = EsAgenteDeRetencionIva," & vbCrLf
   vResult = vResult & "           @Ori_Nombre = Nombre," & vbCrLf
   vResult = vResult & "           @Ori_ApellidoPaterno = ApellidoPaterno," & vbCrLf
   vResult = vResult & "           @Ori_ApellidoMaterno = ApellidoMaterno," & vbCrLf
   vResult = vResult & "           @Ori_NombreOperador = NombreOperador," & vbCrLf
   vResult = vResult & "           @Ori_TimeStamp = fldTimeStamp" & vbCrLf
   vResult = vResult & "   FROM Proveedor" & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "     AND CodigoProveedor = @CodigoProveedor" & vbCrLf
   vResult = vResult & "   IF @NombreProveedor is null" & vbCrLf
   vResult = vResult & "      SET @Up_NombreProveedor = @Ori_NombreProveedor" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NombreProveedor = @NombreProveedor" & vbCrLf
   vResult = vResult & "   IF @Contacto is null" & vbCrLf
   vResult = vResult & "      SET @Up_Contacto = @Ori_Contacto" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Contacto = @Contacto" & vbCrLf
   vResult = vResult & "   IF @NumeroRIF is null" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroRIF = @Ori_NumeroRIF" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroRIF = @NumeroRIF" & vbCrLf
   vResult = vResult & "   IF @NumeroNIT is null" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroNIT = @Ori_NumeroNIT" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroNIT = @NumeroNIT" & vbCrLf
   vResult = vResult & "   IF @TipoDePersona is null" & vbCrLf
   vResult = vResult & "      SET @Up_TipoDePersona = @Ori_TipoDePersona" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TipoDePersona = @TipoDePersona" & vbCrLf
   vResult = vResult & "   IF @CodigoRetencionUsual is null" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoRetencionUsual = @Ori_CodigoRetencionUsual" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoRetencionUsual = @CodigoRetencionUsual" & vbCrLf
   vResult = vResult & "   IF @Telefonos is null" & vbCrLf
   vResult = vResult & "      SET @Up_Telefonos = @Ori_Telefonos" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Telefonos = @Telefonos" & vbCrLf
   vResult = vResult & "   IF @Direccion is null" & vbCrLf
   vResult = vResult & "      SET @Up_Direccion = @Ori_Direccion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Direccion = @Direccion" & vbCrLf
   vResult = vResult & "   IF @Fax is null" & vbCrLf
   vResult = vResult & "      SET @Up_Fax = @Ori_Fax" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Fax = @Fax" & vbCrLf
   vResult = vResult & "   IF @Email is null" & vbCrLf
   vResult = vResult & "      SET @Up_Email = @Ori_Email" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Email = @Email" & vbCrLf
   vResult = vResult & "   IF @TipodeProveedor is null" & vbCrLf
   vResult = vResult & "      SET @Up_TipodeProveedor = @Ori_TipodeProveedor" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TipodeProveedor = @TipodeProveedor" & vbCrLf
   vResult = vResult & "   IF @TipoDeProveedorDeLibrosFiscales is null" & vbCrLf
   vResult = vResult & "      SET @Up_TipoDeProveedorDeLibrosFiscales = @Ori_TipoDeProveedorDeLibrosFiscales" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TipoDeProveedorDeLibrosFiscales = @TipoDeProveedorDeLibrosFiscales" & vbCrLf
   vResult = vResult & "   IF @PorcentajeRetencionIVA is null" & vbCrLf
   vResult = vResult & "      SET @Up_PorcentajeRetencionIVA = @Ori_PorcentajeRetencionIVA" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_PorcentajeRetencionIVA = @PorcentajeRetencionIVA" & vbCrLf
   vResult = vResult & "   IF @CuentaContableCxP is null" & vbCrLf
   vResult = vResult & "      SET @Up_CuentaContableCxP = @Ori_CuentaContableCxP" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CuentaContableCxP = @CuentaContableCxP" & vbCrLf
   vResult = vResult & "   IF @CuentaContableGastos is null" & vbCrLf
   vResult = vResult & "      SET @Up_CuentaContableGastos = @Ori_CuentaContableGastos" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CuentaContableGastos = @CuentaContableGastos" & vbCrLf
   vResult = vResult & "   IF @CuentaContableAnticipo is null" & vbCrLf
   vResult = vResult & "      SET @Up_CuentaContableAnticipo = @Ori_CuentaContableAnticipo" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CuentaContableAnticipo = @CuentaContableAnticipo" & vbCrLf
   vResult = vResult & "   IF @CodigoLote is null" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoLote = @Ori_CodigoLote" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoLote = @CodigoLote" & vbCrLf
   vResult = vResult & "   IF @Beneficiario is null" & vbCrLf
   vResult = vResult & "      SET @Up_Beneficiario = @Ori_Beneficiario" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Beneficiario = @Beneficiario" & vbCrLf
   vResult = vResult & "   IF @UsarBeneficiarioImpCheq is null" & vbCrLf
   vResult = vResult & "      SET @Up_UsarBeneficiarioImpCheq = @Ori_UsarBeneficiarioImpCheq" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_UsarBeneficiarioImpCheq = @UsarBeneficiarioImpCheq" & vbCrLf
   vResult = vResult & "   IF @TipoDocumentoIdentificacion is null" & vbCrLf
   vResult = vResult & "      SET @Up_TipoDocumentoIdentificacion = @Ori_TipoDocumentoIdentificacion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TipoDocumentoIdentificacion = @TipoDocumentoIdentificacion" & vbCrLf
   vResult = vResult & "   IF @EsAgenteDeRetencionIva is null" & vbCrLf
   vResult = vResult & "      SET @Up_EsAgenteDeRetencionIva = @Ori_EsAgenteDeRetencionIva" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_EsAgenteDeRetencionIva = @EsAgenteDeRetencionIva" & vbCrLf
   vResult = vResult & "   IF @Nombre is null" & vbCrLf
   vResult = vResult & "      SET @Up_Nombre = @Ori_Nombre" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Nombre = @Nombre" & vbCrLf
   vResult = vResult & "   IF @ApellidoPaterno is null" & vbCrLf
   vResult = vResult & "      SET @Up_ApellidoPaterno = @Ori_ApellidoPaterno" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_ApellidoPaterno = @ApellidoPaterno" & vbCrLf
   vResult = vResult & "   IF @ApellidoMaterno is null" & vbCrLf
   vResult = vResult & "      SET @Up_ApellidoMaterno = @Ori_ApellidoMaterno" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_ApellidoMaterno = @ApellidoMaterno" & vbCrLf
   vResult = vResult & "   IF @NombreOperador is null" & vbCrLf
   vResult = vResult & "      SET @Up_NombreOperador = @Ori_NombreOperador" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NombreOperador = @NombreOperador" & vbCrLf
   vResult = vResult & "   UPDATE Proveedor" & vbCrLf
   vResult = vResult & "   SET NombreProveedor = @Up_NombreProveedor," & vbCrLf
   vResult = vResult & "        Contacto = @Up_Contacto," & vbCrLf
   vResult = vResult & "        NumeroRIF = @Up_NumeroRIF," & vbCrLf
   vResult = vResult & "        NumeroNIT = @Up_NumeroNIT," & vbCrLf
   vResult = vResult & "        TipoDePersona = @Up_TipoDePersona," & vbCrLf
   vResult = vResult & "        CodigoRetencionUsual = @Up_CodigoRetencionUsual," & vbCrLf
   vResult = vResult & "        Telefonos = @Up_Telefonos," & vbCrLf
   vResult = vResult & "        Direccion = @Up_Direccion," & vbCrLf
   vResult = vResult & "        Fax = @Up_Fax," & vbCrLf
   vResult = vResult & "        Email = @Up_Email," & vbCrLf
   vResult = vResult & "        TipodeProveedor = @Up_TipodeProveedor," & vbCrLf
   vResult = vResult & "        TipoDeProveedorDeLibrosFiscales = @Up_TipoDeProveedorDeLibrosFiscales," & vbCrLf
   vResult = vResult & "        PorcentajeRetencionIVA = @Up_PorcentajeRetencionIVA," & vbCrLf
   vResult = vResult & "        CuentaContableCxP = @Up_CuentaContableCxP," & vbCrLf
   vResult = vResult & "        CuentaContableGastos = @Up_CuentaContableGastos," & vbCrLf
   vResult = vResult & "        CuentaContableAnticipo = @Up_CuentaContableAnticipo," & vbCrLf
   vResult = vResult & "        CodigoLote = @Up_CodigoLote," & vbCrLf
   vResult = vResult & "        Beneficiario = @Up_Beneficiario," & vbCrLf
   vResult = vResult & "        UsarBeneficiarioImpCheq = @Up_UsarBeneficiarioImpCheq," & vbCrLf
   vResult = vResult & "        TipoDocumentoIdentificacion = @Up_TipoDocumentoIdentificacion," & vbCrLf
   vResult = vResult & "        EsAgenteDeRetencionIva = @Up_EsAgenteDeRetencionIva," & vbCrLf
   vResult = vResult & "        Nombre = @Up_Nombre," & vbCrLf
   vResult = vResult & "        ApellidoPaterno = @Up_ApellidoPaterno," & vbCrLf
   vResult = vResult & "        ApellidoMaterno = @Up_ApellidoMaterno," & vbCrLf
   vResult = vResult & "        NombreOperador = @Up_NombreOperador," & vbCrLf
   vResult = vResult & "        FechaUltimaModificacion = " & gUtilSQL.DfCDateSQL(gUtilSQL.GetSQLFechaActual()) & "" & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "     AND CodigoProveedor = @CodigoProveedor" & vbCrLf
   vResult = vResult & "     AND fldTimeStamp = @Ori_TimeStamp" & vbCrLf
   vResult = vResult & "   IF @@ERROR = 0" & vbCrLf
   vResult = vResult & "      COMMIT TRAN" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      ROLLBACK" & vbCrLf
   vResult = vResult & "END "
   fStatementsSpUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Public Function fCreateSpDel(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
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
Public Function fDropSpDel(ByVal valShowMessage As Boolean) As Boolean
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
   GetSpNameDel = "ProveedorDEL"
End Function
Private Function fSpParametersDel() As String
   Dim vResult As String
   vResult = vResult & "   @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & "   @CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   fSpParametersDel = vResult
End Function
Private Function fStatementsSpDel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DELETE FROM Proveedor WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Public Function fCreateSpS10Insert(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10INS(), fSpParametersS10INS(), fStatementsSpS10Insert(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10Insert = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10Insert", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fDropSpS10Insert(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10INS(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10Insert = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10Insert", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function GetSpNameS10INS() As String
   GetSpNameS10INS = "S10_ProveedorINS"
End Function
Private Function fSpParametersS10INS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @CCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @Codigo " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "         @Nombre " & gUtilSQL.getVarCharTypeForDB(60) & "," & vbCrLf
   vResult = vResult & "         @RifRuc " & gUtilSQL.getVarCharTypeForDB(20) & " = ''," & vbCrLf
   vResult = vResult & "         @Tlf " & gUtilSQL.getVarCharTypeForDB(40) & " = ''," & vbCrLf
   vResult = vResult & "         @Dir " & gUtilSQL.getVarCharTypeForDB(255) & " = ''," & vbCrLf
   vResult = vResult & "         @Fax " & gUtilSQL.getVarCharTypeForDB(25) & " = ''," & vbCrLf
   vResult = vResult & "         @Correo " & gUtilSQL.getVarCharTypeForDB(40) & " = ''" & vbCrLf
   fSpParametersS10INS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10Ins", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fStatementsSpS10Insert() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Ret " & gUtilSQL.getVarCharTypeForDB(6) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Tipo " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Operador " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   IF NOT EXISTS(SELECT ConsecutivoCompania FROM COMPANIA WHERE ConsecutivoCompania = @CCompania)" & vbCrLf
   vResult = vResult & "      RAISERROR ('No existe registro de Compañía para el Consecutivo dado%d.', 16, 1, @CCompania)" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @Ret = dbo.Gf_CodRetUsualS10()" & vbCrLf
   vResult = vResult & "      SET @Tipo = dbo.Gf_TipoProvS10(@CCompania)" & vbCrLf
   vResult = vResult & "      SET @Operador = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "      IF EXISTS(SELECT CodigoProveedor FROM Proveedor WHERE ConsecutivoCompania = @CCompania AND CodigoProveedor = @Codigo)" & vbCrLf
   vResult = vResult & "         RAISERROR('Ya existe un registro para los datos suministrados de Proveedor.',16,1)" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "         EXEC Gp_ProveedorINS @ConsecutivoCompania = @CCompania," & vbCrLf
   vResult = vResult & "            @CodigoProveedor = @Codigo," & vbCrLf
   vResult = vResult & "            @NombreProveedor = @Nombre," & vbCrLf
   vResult = vResult & "            @NumeroRIF = @RifRuc," & vbCrLf
   vResult = vResult & "            @CodigoRetencionUsual = @Ret," & vbCrLf
   vResult = vResult & "            @Telefonos = @Tlf," & vbCrLf
   vResult = vResult & "            @Direccion = @Dir," & vbCrLf
   vResult = vResult & "            @Fax = @Fax," & vbCrLf
   vResult = vResult & "            @Email = @Correo," & vbCrLf
   vResult = vResult & "            @TipodeProveedor = @Tipo," & vbCrLf
   vResult = vResult & "            @NombreOperador = @Operador" & vbCrLf
   vResult = vResult & "         EXEC Gp_AuxiliarGenera" & vbCrLf
   vResult = vResult & "            @P_ConsecutivoCompania = @CCompania" & vbCrLf
   vResult = vResult & "            ,@P_TipoAuxiliar = '1' -- Proveedor" & vbCrLf
   vResult = vResult & "            ,@P_Codigo  = @Codigo" & vbCrLf
   vResult = vResult & "            ,@P_Nombre = @Nombre" & vbCrLf
   vResult = vResult & "            ,@P_Rif = @RifRuc" & vbCrLf
   vResult = vResult & "            ,@P_Operador = @Operador" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "END "
   fStatementsSpS10Insert = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10Insert", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Public Function fCreateSpS10Update(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10UPD(), fSpParametersS10UPD(), fStatementsSpS10Update(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10Update = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10Update", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fDropSpS10Update(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10UPD(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10Update = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10Update", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function GetSpNameS10UPD() As String
   GetSpNameS10UPD = "S10_ProveedorUPD"
End Function
Private Function fSpParametersS10UPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "         @P_ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @P_CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "         @P_NombreProveedor " & gUtilSQL.getVarCharTypeForDB(60) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_NumeroRIF " & gUtilSQL.getVarCharTypeForDB(20) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_Telefonos " & gUtilSQL.getVarCharTypeForDB(40) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_Direccion " & gUtilSQL.getVarCharTypeForDB(255) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_Fax " & gUtilSQL.getVarCharTypeForDB(25) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_Correo " & gUtilSQL.getVarCharTypeForDB(40) & " = NULL" & vbCrLf
   fSpParametersS10UPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10Upd", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fStatementsSpS10Update() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Operador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   SET @Operador = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "   EXEC Gp_ProveedorUPD" & vbCrLf
   vResult = vResult & "         @ConsecutivoCompania = @P_ConsecutivoCompania," & vbCrLf
   vResult = vResult & "         @CodigoProveedor = @P_CodigoProveedor," & vbCrLf
   vResult = vResult & "         @NombreProveedor = @P_NombreProveedor," & vbCrLf
   vResult = vResult & "         @NumeroRIF = @P_NumeroRIF," & vbCrLf
   vResult = vResult & "         @Telefonos = @P_Telefonos," & vbCrLf
   vResult = vResult & "         @Direccion = @P_Direccion," & vbCrLf
   vResult = vResult & "         @Fax = @P_Fax," & vbCrLf
   vResult = vResult & "         @Email = @P_Correo," & vbCrLf
   vResult = vResult & "         @NombreOperador = @Operador" & vbCrLf
   vResult = vResult & "   EXEC Gp_AuxiliarActualiza" & vbCrLf
   vResult = vResult & "      @P_ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "      ,@P_TipoAuxiliar = '1'" & vbCrLf
   vResult = vResult & "      ,@P_Codigo = @P_CodigoProveedor" & vbCrLf
   vResult = vResult & "      ,@P_Nombre = @P_NombreProveedor" & vbCrLf
   vResult = vResult & "      ,@P_Rif = @P_NumeroRIF" & vbCrLf
   vResult = vResult & "      ,@P_Operador = @Operador" & vbCrLf
   vResult = vResult & "END "
   fStatementsSpS10Update = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10Update", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Public Function fCreateSpS10Delete(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10DEL(), fSpParametersS10DEL(), fStatementsSpS10Delete(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10Delete = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10Delete", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fDropSpS10Delete(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10DEL(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10Delete = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10Delete", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function GetSpNameS10DEL() As String
   GetSpNameS10DEL = "S10_ProveedorDEL"
End Function
Private Function fSpParametersS10DEL() As String
   Dim vResult As String
   vResult = "   @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & "   @CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   fSpParametersS10DEL = vResult
End Function
Private Function fStatementsSpS10Delete() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   EXEC Gp_AxuliarBorrar " & vbCrLf
   vResult = vResult & "      @P_ConsecutivoCompania = @ConsecutivoCompania " & vbCrLf
   vResult = vResult & "      ,@P_TipoAuxiliar = '1' " & vbCrLf
   vResult = vResult & "      ,@P_Codigo = @CodigoProveedor " & vbCrLf
   vResult = vResult & "   EXEC Gp_ExecuteProveedorDEL @ConsecutivoCompania, @CodigoProveedor" & vbCrLf
   vResult = vResult & "END "
   fStatementsSpS10Delete = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10Delete", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Public Function fCreateSpS10Select(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10SEL(), fSpParametersS10SEL(), fStatementsSpS10Select(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10Select = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10Select", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fDropSpS10Select(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10SEL(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10Select = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10Select", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function GetSpNameS10SEL() As String
   GetSpNameS10SEL = "S10_ProveedorSEL"
End Function
Private Function fSpParametersS10SEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @FixedWhereClause " & gUtilSQL.getVarCharTypeForDB(4000) & " = ''" & vbCrLf
   fSpParametersS10SEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10Sel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fStatementsSpS10Select() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Sql varchar(6000)" & vbCrLf
   vResult = vResult & "   SET @Sql = ' SELECT CodigoProveedor, NombreProveedor," & vbCrLf
   vResult = vResult & "                 Contacto, NumeroRIF, Telefonos, Direccion," & vbCrLf
   vResult = vResult & "                 Fax , Email, CuentaContableCxP, CuentaContableGastos, CuentaContableAnticipo  " & vbCrLf
   vResult = vResult & "            FROM proveedor" & vbCrLf
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
   GetSpNameExecuteDel = "ExecuteProveedorDEL"
End Function

Private Function fSpParametersExecuteDel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "   @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "   @CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
    fSpParametersExecuteDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersExecuteDel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpExecuteDel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @CanBeDeleted bit" & vbCrLf
   vResult = vResult & "   SET @CanBeDeleted = dbo.Gf_ProveedorCanDel(@ConsecutivoCompania, @CodigoProveedor)" & vbCrLf
   vResult = vResult & "   IF @CanBeDeleted = 1 --TRUE" & vbCrLf
   vResult = vResult & "      EXEC Gp_ProveedorDEL @ConsecutivoCompania, @CodigoProveedor" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      RETURN 0" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpExecuteDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpExecuteDel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnProveedorCanBeDeleted(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameProveedorCanBeDeleted(), fFnParametersProveedorCanBeDeleted(), fFnScalarReturnDataTypeProveedorCanBeDeleted(), fStatementsFnProveedorCanBeDeleted(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnProveedorCanBeDeleted = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnProveedorCanBeDeleted", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnProveedorCanBeDeleted(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameProveedorCanBeDeleted(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnProveedorCanBeDeleted = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnProveedorCanBeDeleted", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameProveedorCanBeDeleted() As String
   GetFnNameProveedorCanBeDeleted = "ProveedorCanDel"
End Function

Private Function fFnParametersProveedorCanBeDeleted() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10)
   fFnParametersProveedorCanBeDeleted = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersProveedorCanBeDeleted", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeProveedorCanBeDeleted() As String
   fFnScalarReturnDataTypeProveedorCanBeDeleted = "bit"
End Function

Private Function fStatementsFnProveedorCanBeDeleted() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Result bit" & vbCrLf
   vResult = vResult & "   DECLARE @Continue bit" & vbCrLf
   vResult = vResult & "   SET @Result = 0 --False" & vbCrLf
   vResult = vResult & "   SET @Continue = 1 --TRUE" & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      IF EXISTS(SELECT CodigoProveedor FROM Proveedor WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor)" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         IF EXISTS(SELECT ConsecutivoCompania FROM COMPANIA WHERE ConsecutivoCompania = @ConsecutivoCompania AND UsaModuloDeContabilidad = 'S' AND UsaAuxiliares = 'S')" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            --Si el código de proveedor existe como auxiliar en algún período de la compañía" & vbCrLf
   vResult = vResult & "            IF EXISTS(SELECT Codigo FROM auxiliar WHERE ConsecutivoPeriodo IN (SELECT ConsecutivoPeriodo FROM PERIODO WHERE ConsecutivoCompania = @ConsecutivoCompania) AND Codigo = @CodigoProveedor AND TipoDeAuxiliar = '1')" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "               SET @Continue = 0 --FALSE" & vbCrLf
   vResult = vResult & "            END " & vbCrLf
   vResult = vResult & "           END " & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SET @Continue = 0 --FALSE" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Continue = 0 --FALSE" & vbCrLf
   vResult = vResult & "   IF @Continue = 1" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      IF EXISTS(SELECT CodigoProveedor FROM CxP WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor)" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "      ELSE IF EXISTS(SELECT NumeroComprobante FROM Pago WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor)" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "      ELSE IF EXISTS(SELECT NumeroComprobante FROM RetPago WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor)" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "      ELSE IF EXISTS(SELECT NumeroSecuencial FROM Compra WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor)" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "      ELSE IF EXISTS(SELECT ConsecutivoAnticipo FROM Anticipo WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor)" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "      ELSE IF EXISTS(SELECT Numero FROM ARCV WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor)" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "         SET @Result = 1 --TRUE" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnProveedorCanBeDeleted = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnProveedorCanBeDeleted", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetFnNameS10Exists = "S10_ProveedorExists"
End Function

Private Function fFnParametersS10Exists() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania int, @CodigoProveedor varchar(10)"
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
   vResult = vResult & "   IF EXISTS(SELECT CodigoProveedor FROM proveedor WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor)" & vbCrLf
   vResult = vResult & "      SET @Result = 1 --True" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnS10Exists = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnS10Exists", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *

