VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsClienteSp"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsClienteSp"
End Function

Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsClienteSp"
End Function

Public Function fCreateDropClienteSP(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      vResult = vResult And fCreateSpINS(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpUPD(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpDel(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpExecuteDel(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnClienteCanBeDeleted(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnS10Exists(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10Insert(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10Update(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10Delete(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10Select(valShowMessage, valWithEncryption)
   Else
      vResult = vResult And fDropSpS10Select(valShowMessage)
      vResult = vResult And fDropSpS10Delete(valShowMessage)
      vResult = vResult And fDropSpS10Update(valShowMessage)
      vResult = vResult And fDropSpS10Insert(valShowMessage)
      vResult = vResult And fDropFnS10Exists(valShowMessage)
      vResult = vResult And fDropFnClienteCanBeDeleted(valShowMessage)
      vResult = vResult And fDropSpExecuteDel(valShowMessage)
      vResult = vResult And fDropSpDel(valShowMessage)
      vResult = vResult And fDropSpUPD(valShowMessage)
      vResult = vResult And fDropSpINS(valShowMessage)
   End If
   fCreateDropClienteSP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropClienteSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetSpNameINS = "ClienteINS"
End Function

Private Function fSpParametersINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @Codigo " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "         @Nombre " & gUtilSQL.getVarCharTypeForDB(80) & "," & vbCrLf
   vResult = vResult & "         @NumeroRIF " & gUtilSQL.getVarCharTypeForDB(20) & " = ''," & vbCrLf
   vResult = vResult & "         @NumeroNIT " & gUtilSQL.getVarCharTypeForDB(12) & " = ''," & vbCrLf
   vResult = vResult & "         @Direccion " & gUtilSQL.getVarCharTypeForDB(255) & " = ''," & vbCrLf
   vResult = vResult & "         @Ciudad " & gUtilSQL.getVarCharTypeForDB(100) & "," & vbCrLf
   vResult = vResult & "         @ZonaPostal " & gUtilSQL.getVarCharTypeForDB(7) & " = ''," & vbCrLf
   vResult = vResult & "         @Telefono " & gUtilSQL.getVarCharTypeForDB(40) & " = ''," & vbCrLf
   vResult = vResult & "         @Fax " & gUtilSQL.getVarCharTypeForDB(25) & " = ''," & vbCrLf
   vResult = vResult & "         @Status " & gUtilSQL.getCharTypeForDB(1) & " = '0'," & vbCrLf
   vResult = vResult & "         @Contacto " & gUtilSQL.getVarCharTypeForDB(35) & " = ''," & vbCrLf
   vResult = vResult & "         @ZonaDeCobranza " & gUtilSQL.getVarCharTypeForDB(100) & "," & vbCrLf
   vResult = vResult & "         @CodigoVendedor " & gUtilSQL.getVarCharTypeForDB(5) & " = ''," & vbCrLf
   vResult = vResult & "         @RazonInactividad " & gUtilSQL.getVarCharTypeForDB(35) & " = ''," & vbCrLf
   vResult = vResult & "         @Email " & gUtilSQL.getVarCharTypeForDB(40) & " = ''," & vbCrLf
   vResult = vResult & "         @ActivarAvisoAlEscoger " & gUtilSQL.getCharTypeForDB(1) & " = 'N'," & vbCrLf
   vResult = vResult & "         @TextoDelAviso " & gUtilSQL.getVarCharTypeForDB(150) & " = ''," & vbCrLf
   vResult = vResult & "         @CuentaContableCxc " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "         @CuentaContableIngresos " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "         @CuentaContableAnticipo " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "         @SectorDeNegocio " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "         @CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & " = ''," & vbCrLf
   vResult = vResult & "         @NivelDePrecio " & gUtilSQL.getCharTypeForDB(1) & " = '0'," & vbCrLf
   vResult = vResult & "         @Origen " & gUtilSQL.getCharTypeForDB(1) & " = '1'," & vbCrLf
   vResult = vResult & "         @DiaCumpleanos " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "         @MesCumpleanos " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0," & vbCrLf
   vResult = vResult & "         @CorrespondenciaXenviar " & gUtilSQL.getCharTypeForDB(1) & " = 'N'," & vbCrLf
   vResult = vResult & "         @EsExtranjero " & gUtilSQL.getCharTypeForDB(1) & " = 'N'," & vbCrLf
   vResult = vResult & "         @TipoDocumentoIdentificacion " & gUtilSQL.getCharTypeForDB(1) & " = '0'," & vbCrLf
   vResult = vResult & "   @NombreOperador varchar(10) = ''," & vbCrLf
   vResult = vResult & "   @CampoDefinible1 varchar(20) = ''" & vbCrLf
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
   vResult = vResult & "   INSERT INTO cliente(" & vbCrLf
   vResult = vResult & "         ConsecutivoCompania," & vbCrLf
   vResult = vResult & "         Codigo," & vbCrLf
   vResult = vResult & "         Nombre," & vbCrLf
   vResult = vResult & "         NumeroRIF," & vbCrLf
   vResult = vResult & "         NumeroNIT," & vbCrLf
   vResult = vResult & "         Direccion," & vbCrLf
   vResult = vResult & "         Ciudad," & vbCrLf
   vResult = vResult & "         ZonaPostal," & vbCrLf
   vResult = vResult & "         Telefono," & vbCrLf
   vResult = vResult & "         Fax," & vbCrLf
   vResult = vResult & "         Status," & vbCrLf
   vResult = vResult & "         Contacto," & vbCrLf
   vResult = vResult & "         ZonaDeCobranza," & vbCrLf
   vResult = vResult & "         CodigoVendedor," & vbCrLf
   vResult = vResult & "         RazonInactividad," & vbCrLf
   vResult = vResult & "         Email," & vbCrLf
   vResult = vResult & "         ActivarAvisoAlEscoger," & vbCrLf
   vResult = vResult & "         TextoDelAviso," & vbCrLf
   vResult = vResult & "         CuentaContableCxc," & vbCrLf
   vResult = vResult & "         CuentaContableIngresos," & vbCrLf
   vResult = vResult & "         CuentaContableAnticipo," & vbCrLf
   vResult = vResult & "         SectorDeNegocio," & vbCrLf
   vResult = vResult & "         CodigoLote," & vbCrLf
   vResult = vResult & "         NivelDePrecio," & vbCrLf
   vResult = vResult & "         Origen," & vbCrLf
   vResult = vResult & "         DiaCumpleanos," & vbCrLf
   vResult = vResult & "         MesCumpleanos," & vbCrLf
   vResult = vResult & "         CorrespondenciaXenviar," & vbCrLf
   vResult = vResult & "         EsExtranjero," & vbCrLf
   vResult = vResult & "         TipoDocumentoIdentificacion," & vbCrLf
   vResult = vResult & "         NombreOperador," & vbCrLf
   vResult = vResult & "         CampoDefinible1," & vbCrLf
   vResult = vResult & "         FechaUltimaModificacion" & vbCrLf
   vResult = vResult & "      )VALUES(" & vbCrLf
   vResult = vResult & "         @ConsecutivoCompania," & vbCrLf
   vResult = vResult & "         @Codigo," & vbCrLf
   vResult = vResult & "         @Nombre," & vbCrLf
   vResult = vResult & "         @NumeroRIF," & vbCrLf
   vResult = vResult & "         @NumeroNIT," & vbCrLf
   vResult = vResult & "         @Direccion," & vbCrLf
   vResult = vResult & "         @Ciudad," & vbCrLf
   vResult = vResult & "         @ZonaPostal," & vbCrLf
   vResult = vResult & "         @Telefono," & vbCrLf
   vResult = vResult & "         @Fax," & vbCrLf
   vResult = vResult & "         @Status," & vbCrLf
   vResult = vResult & "         @Contacto," & vbCrLf
   vResult = vResult & "         @ZonaDeCobranza," & vbCrLf
   vResult = vResult & "         @CodigoVendedor," & vbCrLf
   vResult = vResult & "         @RazonInactividad," & vbCrLf
   vResult = vResult & "         @Email," & vbCrLf
   vResult = vResult & "         @ActivarAvisoAlEscoger," & vbCrLf
   vResult = vResult & "         @TextoDelAviso," & vbCrLf
   vResult = vResult & "         @CuentaContableCxc," & vbCrLf
   vResult = vResult & "         @CuentaContableIngresos," & vbCrLf
   vResult = vResult & "         @CuentaContableAnticipo," & vbCrLf
   vResult = vResult & "         @SectorDeNegocio," & vbCrLf
   vResult = vResult & "         @CodigoLote," & vbCrLf
   vResult = vResult & "         @NivelDePrecio," & vbCrLf
   vResult = vResult & "         @Origen," & vbCrLf
   vResult = vResult & "         @DiaCumpleanos," & vbCrLf
   vResult = vResult & "         @MesCumpleanos," & vbCrLf
   vResult = vResult & "         @CorrespondenciaXenviar," & vbCrLf
   vResult = vResult & "         @EsExtranjero," & vbCrLf
   vResult = vResult & "         @TipoDocumentoIdentificacion," & vbCrLf
   vResult = vResult & "         @NombreOperador," & vbCrLf
   vResult = vResult & "         @CampoDefinible1," & vbCrLf
   vResult = vResult & "         @FechaUltimaModificacion)" & vbCrLf
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
   GetSpNameUPD = "ClienteUPD"
End Function

Private Function fSpParametersUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @Codigo " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "         @Nombre " & gUtilSQL.getVarCharTypeForDB(80) & " = NULL," & vbCrLf
   vResult = vResult & "         @NumeroRIF " & gUtilSQL.getVarCharTypeForDB(20) & " = NULL," & vbCrLf
   vResult = vResult & "         @NumeroNIT " & gUtilSQL.getVarCharTypeForDB(12) & " = NULL," & vbCrLf
   vResult = vResult & "         @Direccion " & gUtilSQL.getVarCharTypeForDB(255) & " = NULL," & vbCrLf
   vResult = vResult & "         @Ciudad " & gUtilSQL.getVarCharTypeForDB(100) & " = NULL," & vbCrLf
   vResult = vResult & "         @ZonaPostal " & gUtilSQL.getVarCharTypeForDB(7) & " = NULL," & vbCrLf
   vResult = vResult & "         @Telefono " & gUtilSQL.getVarCharTypeForDB(40) & " = NULL," & vbCrLf
   vResult = vResult & "         @Fax " & gUtilSQL.getVarCharTypeForDB(25) & " = NULL," & vbCrLf
   vResult = vResult & "         @Status " & gUtilSQL.getCharTypeForDB(1) & " = NULL," & vbCrLf
   vResult = vResult & "         @Contacto " & gUtilSQL.getVarCharTypeForDB(35) & " = NULL," & vbCrLf
   vResult = vResult & "         @ZonaDeCobranza " & gUtilSQL.getVarCharTypeForDB(100) & " = NULL," & vbCrLf
   vResult = vResult & "         @CodigoVendedor " & gUtilSQL.getVarCharTypeForDB(5) & " = NULL," & vbCrLf
   vResult = vResult & "         @RazonInactividad " & gUtilSQL.getVarCharTypeForDB(35) & " = NULL," & vbCrLf
   vResult = vResult & "         @Email " & gUtilSQL.getVarCharTypeForDB(40) & " = NULL," & vbCrLf
   vResult = vResult & "         @ActivarAvisoAlEscoger " & gUtilSQL.getCharTypeForDB(1) & " = NULL," & vbCrLf
   vResult = vResult & "         @TextoDelAviso " & gUtilSQL.getVarCharTypeForDB(150) & " = NULL," & vbCrLf
   vResult = vResult & "         @CuentaContableCxc " & gUtilSQL.getVarCharTypeForDB(30) & " = NULL," & vbCrLf
   vResult = vResult & "         @CuentaContableIngresos " & gUtilSQL.getVarCharTypeForDB(30) & " = NULL," & vbCrLf
   vResult = vResult & "         @CuentaContableAnticipo " & gUtilSQL.getVarCharTypeForDB(30) & " = NULL," & vbCrLf
   vResult = vResult & "         @SectorDeNegocio " & gUtilSQL.getVarCharTypeForDB(20) & " = NULL," & vbCrLf
   vResult = vResult & "         @CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & " = NULL," & vbCrLf
   vResult = vResult & "         @NivelDePrecio " & gUtilSQL.getCharTypeForDB(1) & " = NULL," & vbCrLf
   vResult = vResult & "         @Origen " & gUtilSQL.getCharTypeForDB(1) & " = NULL," & vbCrLf
   vResult = vResult & "         @DiaCumpleanos " & gUtilSQL.getNumericTypeForDB(9, 0) & " = NULL," & vbCrLf
   vResult = vResult & "         @MesCumpleanos " & gUtilSQL.getNumericTypeForDB(9, 0) & " = NULL," & vbCrLf
   vResult = vResult & "         @CorrespondenciaXenviar " & gUtilSQL.getCharTypeForDB(1) & " = NULL," & vbCrLf
   vResult = vResult & "         @EsExtranjero " & gUtilSQL.getCharTypeForDB(1) & " = NULL," & vbCrLf
   vResult = vResult & "   @NombreOperador varchar(10) = NULL," & vbCrLf
   vResult = vResult & "   @CampoDefinible1 varchar(20) = NULL" & vbCrLf
    fSpParametersUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Nombre " & gUtilSQL.getVarCharTypeForDB(80) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroRIF " & gUtilSQL.getVarCharTypeForDB(20) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroNIT " & gUtilSQL.getVarCharTypeForDB(12) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Direccion " & gUtilSQL.getVarCharTypeForDB(255) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Ciudad " & gUtilSQL.getVarCharTypeForDB(100) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_ZonaPostal " & gUtilSQL.getVarCharTypeForDB(7) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Telefono " & gUtilSQL.getVarCharTypeForDB(40) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Fax " & gUtilSQL.getVarCharTypeForDB(25) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Status " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Contacto " & gUtilSQL.getVarCharTypeForDB(35) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_ZonaDeCobranza " & gUtilSQL.getVarCharTypeForDB(100) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoVendedor " & gUtilSQL.getVarCharTypeForDB(5) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_RazonInactividad " & gUtilSQL.getVarCharTypeForDB(35) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Email " & gUtilSQL.getVarCharTypeForDB(40) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_ActivarAvisoAlEscoger " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TextoDelAviso " & gUtilSQL.getVarCharTypeForDB(150) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CuentaContableCxc " & gUtilSQL.getVarCharTypeForDB(30) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CuentaContableIngresos " & gUtilSQL.getVarCharTypeForDB(30) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CuentaContableAnticipo " & gUtilSQL.getVarCharTypeForDB(30) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_SectorDeNegocio " & gUtilSQL.getVarCharTypeForDB(20) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NivelDePrecio " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Origen " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_DiaCumpleanos " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MesCumpleanos " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CorrespondenciaXenviar " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_EsExtranjero " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CampoDefinible1 " & gUtilSQL.getVarCharTypeForDB(20) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TimeStamp timestamp" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Nombre " & gUtilSQL.getVarCharTypeForDB(80) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroRIF " & gUtilSQL.getVarCharTypeForDB(20) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroNIT " & gUtilSQL.getVarCharTypeForDB(12) & vbCrLf
   vResult = vResult & "   DECLARE @Up_Direccion " & gUtilSQL.getVarCharTypeForDB(255) & vbCrLf
   vResult = vResult & "   DECLARE @Up_Ciudad " & gUtilSQL.getVarCharTypeForDB(100) & vbCrLf
   vResult = vResult & "   DECLARE @Up_ZonaPostal " & gUtilSQL.getVarCharTypeForDB(7) & vbCrLf
   vResult = vResult & "   DECLARE @Up_Telefono " & gUtilSQL.getVarCharTypeForDB(40) & vbCrLf
   vResult = vResult & "   DECLARE @Up_Fax " & gUtilSQL.getVarCharTypeForDB(25) & vbCrLf
   vResult = vResult & "   DECLARE @Up_Status " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_Contacto " & gUtilSQL.getVarCharTypeForDB(35) & vbCrLf
   vResult = vResult & "   DECLARE @Up_ZonaDeCobranza " & gUtilSQL.getVarCharTypeForDB(100) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoVendedor " & gUtilSQL.getVarCharTypeForDB(5) & vbCrLf
   vResult = vResult & "   DECLARE @Up_RazonInactividad " & gUtilSQL.getVarCharTypeForDB(35) & vbCrLf
   vResult = vResult & "   DECLARE @Up_Email " & gUtilSQL.getVarCharTypeForDB(40) & vbCrLf
   vResult = vResult & "   DECLARE @Up_ActivarAvisoAlEscoger " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_TextoDelAviso " & gUtilSQL.getVarCharTypeForDB(150) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CuentaContableCxc " & gUtilSQL.getVarCharTypeForDB(30) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CuentaContableIngresos " & gUtilSQL.getVarCharTypeForDB(30) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CuentaContableAnticipo " & gUtilSQL.getVarCharTypeForDB(30) & vbCrLf
   vResult = vResult & "   DECLARE @Up_SectorDeNegocio " & gUtilSQL.getVarCharTypeForDB(20) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NivelDePrecio " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_Origen " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_DiaCumpleanos " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Up_MesCumpleanos " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CorrespondenciaXenviar " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_EsExtranjero " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CampoDefinible1 " & gUtilSQL.getVarCharTypeForDB(20) & vbCrLf
   vResult = vResult & "   DECLARE @FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   BEGIN TRAN" & vbCrLf
   vResult = vResult & "   SELECT" & vbCrLf
   vResult = vResult & "      @Ori_Nombre = Nombre," & vbCrLf
   vResult = vResult & "      @Ori_NumeroRIF = NumeroRIF," & vbCrLf
   vResult = vResult & "      @Ori_NumeroNIT = NumeroNIT," & vbCrLf
   vResult = vResult & "      @Ori_Direccion = Direccion," & vbCrLf
   vResult = vResult & "      @Ori_Ciudad = Ciudad," & vbCrLf
   vResult = vResult & "      @Ori_ZonaPostal = ZonaPostal," & vbCrLf
   vResult = vResult & "      @Ori_Telefono = Telefono," & vbCrLf
   vResult = vResult & "      @Ori_Fax = Fax," & vbCrLf
   vResult = vResult & "      @Ori_Status = Status," & vbCrLf
   vResult = vResult & "      @Ori_Contacto = Contacto," & vbCrLf
   vResult = vResult & "      @Ori_ZonaDeCobranza = ZonaDeCobranza," & vbCrLf
   vResult = vResult & "      @Ori_CodigoVendedor = CodigoVendedor," & vbCrLf
   vResult = vResult & "      @Ori_RazonInactividad = RazonInactividad," & vbCrLf
   vResult = vResult & "      @Ori_Email = Email," & vbCrLf
   vResult = vResult & "      @Ori_ActivarAvisoAlEscoger = ActivarAvisoAlEscoger," & vbCrLf
   vResult = vResult & "      @Ori_TextoDelAviso = TextoDelAviso," & vbCrLf
   vResult = vResult & "      @Ori_CuentaContableCxc = CuentaContableCxc," & vbCrLf
   vResult = vResult & "      @Ori_CuentaContableIngresos = CuentaContableIngresos," & vbCrLf
   vResult = vResult & "      @Ori_CuentaContableAnticipo = CuentaContableAnticipo," & vbCrLf
   vResult = vResult & "      @Ori_SectorDeNegocio = SectorDeNegocio," & vbCrLf
   vResult = vResult & "      @Ori_CodigoLote = CodigoLote," & vbCrLf
   vResult = vResult & "      @Ori_NivelDePrecio = NivelDePrecio," & vbCrLf
   vResult = vResult & "      @Ori_Origen = Origen," & vbCrLf
   vResult = vResult & "      @Ori_DiaCumpleanos = DiaCumpleanos," & vbCrLf
   vResult = vResult & "      @Ori_MesCumpleanos = MesCumpleanos," & vbCrLf
   vResult = vResult & "      @Ori_CorrespondenciaXenviar = CorrespondenciaXenviar," & vbCrLf
   vResult = vResult & "      @Ori_EsExtranjero = EsExtranjero," & vbCrLf
   vResult = vResult & "      @Ori_NombreOperador = NombreOperador," & vbCrLf
   vResult = vResult & "          @Ori_CampoDefinible1 = CampoDefinible1," & vbCrLf
   vResult = vResult & "      @Ori_TimeStamp = fldTimeStamp" & vbCrLf
   vResult = vResult & "   FROM cliente" & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "      AND Codigo = @Codigo" & vbCrLf
   vResult = vResult & "   IF @Nombre IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Nombre = @Ori_Nombre" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Nombre = @Nombre" & vbCrLf
   vResult = vResult & "   IF @NumeroRIF IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroRIF = @Ori_NumeroRIF" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroRIF = @NumeroRIF" & vbCrLf
   vResult = vResult & "   IF @NumeroNIT IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroNIT = @Ori_NumeroNIT" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroNIT = @NumeroNIT" & vbCrLf
   vResult = vResult & "   IF @Direccion IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Direccion = @Ori_Direccion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Direccion = @Direccion" & vbCrLf
   vResult = vResult & "   IF @Ciudad IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Ciudad = @Ori_Ciudad" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Ciudad = @Ciudad" & vbCrLf
   vResult = vResult & "   IF @ZonaPostal IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_ZonaPostal = @Ori_ZonaPostal" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_ZonaPostal = @ZonaPostal" & vbCrLf
   vResult = vResult & "   IF @Telefono IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Telefono = @Ori_Telefono" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Telefono = @Telefono" & vbCrLf
   vResult = vResult & "   IF @Fax IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Fax = @Ori_Fax" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Fax = @Fax" & vbCrLf
   vResult = vResult & "   IF @Status IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Status = @Ori_Status" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Status = @Status" & vbCrLf
   vResult = vResult & "   IF @Contacto IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Contacto = @Ori_Contacto" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Contacto = @Contacto" & vbCrLf
   vResult = vResult & "   IF @ZonaDeCobranza IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_ZonaDeCobranza = @Ori_ZonaDeCobranza" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_ZonaDeCobranza = @ZonaDeCobranza" & vbCrLf
   vResult = vResult & "   IF @CodigoVendedor IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoVendedor = @Ori_CodigoVendedor" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoVendedor = @CodigoVendedor" & vbCrLf
   vResult = vResult & "   IF @RazonInactividad IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_RazonInactividad = @Ori_RazonInactividad" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_RazonInactividad = @RazonInactividad" & vbCrLf
   vResult = vResult & "   IF @Email IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Email = @Ori_Email" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Email = @Email" & vbCrLf
   vResult = vResult & "   IF @ActivarAvisoAlEscoger IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_ActivarAvisoAlEscoger = @Ori_ActivarAvisoAlEscoger" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_ActivarAvisoAlEscoger = @ActivarAvisoAlEscoger" & vbCrLf
   vResult = vResult & "   IF @TextoDelAviso IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_TextoDelAviso = @Ori_TextoDelAviso" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TextoDelAviso = @TextoDelAviso" & vbCrLf
   vResult = vResult & "   IF @CuentaContableCxc IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_CuentaContableCxc = @Ori_CuentaContableCxc" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CuentaContableCxc = @CuentaContableCxc" & vbCrLf
   vResult = vResult & "   IF @CuentaContableIngresos IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_CuentaContableIngresos = @Ori_CuentaContableIngresos" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CuentaContableIngresos = @CuentaContableIngresos" & vbCrLf
   vResult = vResult & "   IF @CuentaContableAnticipo IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_CuentaContableAnticipo = @Ori_CuentaContableAnticipo" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CuentaContableAnticipo = @CuentaContableAnticipo" & vbCrLf
   vResult = vResult & "   IF @SectorDeNegocio IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_SectorDeNegocio = @Ori_SectorDeNegocio" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_SectorDeNegocio = @SectorDeNegocio" & vbCrLf
   vResult = vResult & "   IF @CodigoLote IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoLote = @Ori_CodigoLote" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoLote = @CodigoLote" & vbCrLf
   vResult = vResult & "   IF @NivelDePrecio IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_NivelDePrecio = @Ori_NivelDePrecio" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NivelDePrecio = @NivelDePrecio" & vbCrLf
   vResult = vResult & "   IF @Origen IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_Origen = @Ori_Origen" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Origen = @Origen" & vbCrLf
   vResult = vResult & "   IF @DiaCumpleanos IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_DiaCumpleanos = @Ori_DiaCumpleanos" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_DiaCumpleanos = @DiaCumpleanos" & vbCrLf
   vResult = vResult & "   IF @MesCumpleanos IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_MesCumpleanos = @Ori_MesCumpleanos" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_MesCumpleanos = @MesCumpleanos" & vbCrLf
   vResult = vResult & "   IF @CorrespondenciaXenviar IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_CorrespondenciaXenviar = @Ori_CorrespondenciaXenviar" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CorrespondenciaXenviar = @CorrespondenciaXenviar" & vbCrLf
   vResult = vResult & "   IF @EsExtranjero IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_EsExtranjero = @Ori_EsExtranjero" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_EsExtranjero = @EsExtranjero" & vbCrLf
   vResult = vResult & "   IF @NombreOperador IS NULL" & vbCrLf
   vResult = vResult & "      SET @Up_NombreOperador = @Ori_NombreOperador" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NombreOperador = @NombreOperador" & vbCrLf
   vResult = vResult & "   SET @FechaUltimaModificacion = CONVERT(" & gUtilSQL.getDateTypeForDB() & ", GETDATE(), 103)" & vbCrLf
   vResult = vResult & "   IF @CampoDefinible1 is null" & vbCrLf
   vResult = vResult & "      SET @Up_CampoDefinible1 = @Ori_CampoDefinible1" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CampoDefinible1 = @CampoDefinible1" & vbCrLf
   vResult = vResult & "   UPDATE cliente" & vbCrLf
   vResult = vResult & "      SET" & vbCrLf
   vResult = vResult & "         Nombre = @Up_Nombre," & vbCrLf
   vResult = vResult & "         NumeroRIF = @Up_NumeroRIF," & vbCrLf
   vResult = vResult & "         NumeroNIT = @Up_NumeroNIT," & vbCrLf
   vResult = vResult & "         Direccion = @Up_Direccion," & vbCrLf
   vResult = vResult & "         Ciudad = @Up_Ciudad," & vbCrLf
   vResult = vResult & "         ZonaPostal = @Up_ZonaPostal," & vbCrLf
   vResult = vResult & "         Telefono = @Up_Telefono," & vbCrLf
   vResult = vResult & "         Fax = @Up_Fax," & vbCrLf
   vResult = vResult & "         Status = @Up_Status," & vbCrLf
   vResult = vResult & "         Contacto = @Up_Contacto," & vbCrLf
   vResult = vResult & "         ZonaDeCobranza = @Up_ZonaDeCobranza," & vbCrLf
   vResult = vResult & "         CodigoVendedor = @Up_CodigoVendedor," & vbCrLf
   vResult = vResult & "         RazonInactividad = @Up_RazonInactividad," & vbCrLf
   vResult = vResult & "         Email = @Up_Email," & vbCrLf
   vResult = vResult & "         ActivarAvisoAlEscoger = @Up_ActivarAvisoAlEscoger," & vbCrLf
   vResult = vResult & "         TextoDelAviso = @Up_TextoDelAviso," & vbCrLf
   vResult = vResult & "         CuentaContableCxc = @Up_CuentaContableCxc," & vbCrLf
   vResult = vResult & "         CuentaContableIngresos = @Up_CuentaContableIngresos," & vbCrLf
   vResult = vResult & "         CuentaContableAnticipo = @Up_CuentaContableAnticipo," & vbCrLf
   vResult = vResult & "         SectorDeNegocio = @Up_SectorDeNegocio," & vbCrLf
   vResult = vResult & "         CodigoLote = @Up_CodigoLote," & vbCrLf
   vResult = vResult & "         NivelDePrecio = @Up_NivelDePrecio," & vbCrLf
   vResult = vResult & "         Origen = @Up_Origen," & vbCrLf
   vResult = vResult & "         DiaCumpleanos = @Up_DiaCumpleanos," & vbCrLf
   vResult = vResult & "         MesCumpleanos = @Up_MesCumpleanos," & vbCrLf
   vResult = vResult & "         CorrespondenciaXenviar = @Up_CorrespondenciaXenviar," & vbCrLf
   vResult = vResult & "         EsExtranjero = @Up_EsExtranjero," & vbCrLf
   vResult = vResult & "         NombreOperador = @Up_NombreOperador," & vbCrLf
   vResult = vResult & "         CampoDefinible1 = @Up_CampoDefinible1," & vbCrLf
   vResult = vResult & "         FechaUltimaModificacion = @FechaUltimaModificacion" & vbCrLf
   vResult = vResult & "      WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "         AND Codigo = @Codigo" & vbCrLf
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
   GetSpNameDel = "ClienteDEL"
End Function

Private Function fSpParametersDel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @Codigo " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
    fSpParametersDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpDel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DELETE FROM cliente" & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "      AND Codigo = @Codigo" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpExecuteDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpExecuteDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameExecuteDel() As String
   GetSpNameExecuteDel = "ExecuteClienteDEL"
End Function

Private Function fSpParametersExecuteDel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "   @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "   @CodigoCliente " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
    fSpParametersExecuteDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersExecuteDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpExecuteDel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @CanBeDeleted bit" & vbCrLf
   vResult = vResult & "   SET @CanBeDeleted = dbo.Gf_ClienteCanDel(@ConsecutivoCompania, @CodigoCliente)" & vbCrLf
   vResult = vResult & "   IF @CanBeDeleted = 1 --TRUE" & vbCrLf
   vResult = vResult & "      EXEC Gp_ClienteDEL @ConsecutivoCompania, @CodigoCliente" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      RETURN 0" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpExecuteDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpExecuteDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnClienteCanBeDeleted(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameClienteCanBeDeleted(), fFnParametersClienteCanBeDeleted(), fFnScalarReturnDataTypeClienteCanBeDeleted(), fStatementsFnClienteCanBeDeleted(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnClienteCanBeDeleted = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnClienteCanBeDeleted", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnClienteCanBeDeleted(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameClienteCanBeDeleted(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnClienteCanBeDeleted = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnClienteCanBeDeleted", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameClienteCanBeDeleted() As String
   GetFnNameClienteCanBeDeleted = "ClienteCanDel"
End Function

Private Function fFnParametersClienteCanBeDeleted() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & "@CodigoCliente " & gUtilSQL.getVarCharTypeForDB(10)
   fFnParametersClienteCanBeDeleted = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersClienteCanBeDeleted", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeClienteCanBeDeleted() As String
   fFnScalarReturnDataTypeClienteCanBeDeleted = "bit"
End Function

Private Function fStatementsFnClienteCanBeDeleted() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Result bit" & vbCrLf
   vResult = vResult & "   DECLARE @Continue bit" & vbCrLf
   vResult = vResult & "   SET @Result  = 0 --False" & vbCrLf
   vResult = vResult & "   SET   @Continue = 1 --True" & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      IF EXISTS(SELECT Codigo FROM Cliente WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @CodigoCliente)" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         IF EXISTS(SELECT ConsecutivoCompania FROM COMPANIA WHERE ConsecutivoCompania = @ConsecutivoCompania AND UsaModuloDeContabilidad = 'S' AND UsaAuxiliares = 'S')" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            --Si el código de cliente existe como auxiliar en algún período de la compañía" & vbCrLf
   vResult = vResult & "            IF EXISTS(SELECT Codigo FROM auxiliar WHERE ConsecutivoPeriodo IN (SELECT ConsecutivoPeriodo FROM PERIODO WHERE ConsecutivoCompania = @ConsecutivoCompania) AND Codigo = @CodigoCliente AND TipoDeAuxiliar = '0')" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "               SET @Continue = 0 --FALSE" & vbCrLf
   vResult = vResult & "            END " & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SET   @Continue = 0 --False" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET   @Continue = 0 --False" & vbCrLf
   vResult = vResult & "   IF @Continue = 1 --True" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      IF @CodigoCliente = 'RD_Cliente'" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "      ELSE IF EXISTS(SELECT Numero FROM CxC WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoCliente = @CodigoCliente)" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "      ELSE IF EXISTS(SELECT Numero FROM Cotizacion WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoCliente = @CodigoCliente)" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   'vResult = vResult & "      ELSE IF EXISTS(SELECT ConsecutivoCompania FROM ParametrosCompania WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoGenericoCliente = @CodigoCliente)" & vbCrLf
   vResult = vResult & "      ELSE IF EXISTS(SELECT Value FROM Comun.SettValueByCompany WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "      AND Value =  @CodigoCliente" & vbCrLf
   vResult = vResult & "      AND NameSettDefinition = " & gUtilSQL.fSimpleSqlValue("CodigoGenericoCliente") & ")" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "      ELSE IF EXISTS(SELECT Numero FROM Cobranza WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoCliente = @CodigoCliente)" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "      ELSE IF EXISTS(SELECT Numero, TipoDeDocumento FROM Factura WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoCliente = @CodigoCliente)" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   'vResult = vResult & "      ELSE IF EXISTS(SELECT Codigo FROM Almacen WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoDelCliente = @CodigoCliente)" & vbCrLf
   vResult = vResult & "      ELSE IF EXISTS(SELECT Almacen.Codigo FROM Almacen INNER JOIN Cliente ON (Almacen.ConsecutivoCompania = Cliente.ConsecutivoCompania AND Almacen.ConsecutivoCliente = Cliente.Consecutivo) WHERE Almacen.ConsecutivoCompania = @ConsecutivoCompania AND Cliente.Codigo = @CodigoCliente)" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "      ELSE IF EXISTS(SELECT NumeroDocumento FROM NotaDeEntradaSalida WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoCliente = @CodigoCliente)" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "      ELSE IF EXISTS(SELECT ConsecutivoDireccion FROM DireccionDeDespacho WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoCliente = @CodigoCliente)" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "      ELSE IF EXISTS(SELECT NumeroContrato FROM Contrato WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoCliente = @CodigoCliente)" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "      ELSE IF EXISTS(SELECT ConsecutivoAnticipo FROM Anticipo WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoCliente = @CodigoCliente)" & vbCrLf
   vResult = vResult & "         SET @Result = 0 --FALSE" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "         SET @Result = 1 --True" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsFnClienteCanBeDeleted = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnClienteCanBeDeleted", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetSpNameS10Insert = "S10_ClienteINS"
End Function

Private Function fSpParametersS10Insert() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @P_ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @P_Codigo " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "         @P_Nombre " & gUtilSQL.getVarCharTypeForDB(80) & "," & vbCrLf
   vResult = vResult & "         @P_NumeroRIF " & gUtilSQL.getVarCharTypeForDB(20) & " = ''," & vbCrLf
   vResult = vResult & "         @P_Direccion " & gUtilSQL.getVarCharTypeForDB(255) & " = ''," & vbCrLf
   vResult = vResult & "         @P_Ciudad " & gUtilSQL.getVarCharTypeForDB(100) & " = ''," & vbCrLf
   vResult = vResult & "         @P_Telefono " & gUtilSQL.getVarCharTypeForDB(40) & " = ''," & vbCrLf
   vResult = vResult & "         @P_FAX " & gUtilSQL.getVarCharTypeForDB(25) & " = ''," & vbCrLf
   vResult = vResult & "         @P_Email " & gUtilSQL.getVarCharTypeForDB(40) & " = ''," & vbCrLf
   vResult = vResult & "         @P_TipoDocumentoIdentificacion  char(1) = '0'" & vbCrLf
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
   vResult = vResult & "   DECLARE @ZonaCobranza " & gUtilSQL.getVarCharTypeForDB(100) & "" & vbCrLf
   vResult = vResult & "   DECLARE @ZonaCobranzaXCompania " & gUtilSQL.getVarCharTypeForDB(100) & "" & vbCrLf
   vResult = vResult & "   DECLARE @FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @Vendedor " & gUtilSQL.getVarCharTypeForDB(5) & "" & vbCrLf
   vResult = vResult & "   DECLARE @SectorNeg " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @return_valueZC int " & vbCrLf
   vResult = vResult & "   IF NOT EXISTS(SELECT ConsecutivoCompania FROM COMPANIA WHERE ConsecutivoCompania = @P_ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "      RAISERROR ('No existe registro de Compañía para el Consecutivo dado%d.', 16, 1, @P_ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @Operador = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "      SET @FechaUltimaModificacion = CONVERT(" & gUtilSQL.getDateTypeForDB() & ", GETDATE(), 103)" & vbCrLf
   vResult = vResult & "      SELECT @ZonaCobranzaXCompania = Ciudad FROM COMPANIA WHERE (ConsecutivoCompania = @P_ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "      IF (LEN(@P_Ciudad) = 0)" & vbCrLf
   vResult = vResult & "         SET @P_Ciudad = @ZonaCobranzaXCompania" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT NombreCiudad FROM Ciudad WHERE NombreCiudad = @P_Ciudad)" & vbCrLf
   vResult = vResult & "         INSERT INTO Ciudad (NombreCiudad) VALUES (@P_Ciudad)" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT Nombre FROM zonaCobranza WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND Nombre = @ZonaCobranzaXCompania)" & vbCrLf
   vResult = vResult & "         EXEC  @return_valueZC = Gp_ZonaCobranzaINS @P_ConsecutivoCompania, @ZonaCobranzaXCompania, @Operador" & vbCrLf
   'vResult = vResult & "      SELECT @Vendedor = CodigoGenericoVendedor FROM parametrosCompania" & vbCrLf
   vResult = vResult & "      SELECT @Vendedor = Value FROM Comun.SettValueByCompany" & vbCrLf
   vResult = vResult & "         WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND NameSettDefinition = " & gUtilSQL.fSimpleSqlValue("CodigoGenericoVendedor") & vbCrLf
   vResult = vResult & "      SET @SectorNeg = 'No Asignado'" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT Descripcion FROM SectorDeNegocio WHERE Descripcion = @SectorNeg)" & vbCrLf
   vResult = vResult & "         INSERT INTO SectorDeNegocio (Descripcion)" & vbCrLf
   vResult = vResult & "            VALUES (@SectorNeg)" & vbCrLf
   vResult = vResult & "      IF EXISTS(SELECT Codigo FROM Cliente WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND Codigo = @P_Codigo)" & vbCrLf
   vResult = vResult & "         RAISERROR('Ya existe un registro para los datos suministrados para Cliente.',16,1)" & vbCrLf
   vResult = vResult & "      ELSE IF EXISTS(SELECT Nombre FROM Cliente WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND Nombre = @P_Nombre)" & vbCrLf
   vResult = vResult & "         RAISERROR('Ya existe un registro para los datos suministrados para Cliente.',16,1)" & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "         EXEC Gp_ClienteINS" & vbCrLf
   vResult = vResult & "            @ConsecutivoCompania = @P_ConsecutivoCompania," & vbCrLf
   vResult = vResult & "            @Codigo = @P_Codigo," & vbCrLf
   vResult = vResult & "            @Nombre = @P_Nombre," & vbCrLf
   vResult = vResult & "            @NumeroRIF = @P_NumeroRIF," & vbCrLf
   vResult = vResult & "            @Direccion = @P_Direccion," & vbCrLf
   vResult = vResult & "            @Ciudad = @P_Ciudad," & vbCrLf
   vResult = vResult & "            @ZonaPostal = ''," & vbCrLf
   vResult = vResult & "            @Telefono = @P_Telefono," & vbCrLf
   vResult = vResult & "            @Fax = @P_FAX," & vbCrLf
   vResult = vResult & "            @Status = '0'," & vbCrLf
   vResult = vResult & "            @Contacto = ''," & vbCrLf
   vResult = vResult & "            @ZonaDeCobranza = @ZonaCobranzaXCompania," & vbCrLf
   vResult = vResult & "            @CodigoVendedor = @Vendedor," & vbCrLf
   vResult = vResult & "            @RazonInactividad = ''," & vbCrLf
   vResult = vResult & "            @Email = @P_Email," & vbCrLf
   vResult = vResult & "            @ActivarAvisoAlEscoger = 'N'," & vbCrLf
   vResult = vResult & "            @TextoDelAviso = ''," & vbCrLf
   vResult = vResult & "            @CuentaContableCxc = ''," & vbCrLf
   vResult = vResult & "            @CuentaContableIngresos = ''," & vbCrLf
   vResult = vResult & "            @CuentaContableAnticipo = ''," & vbCrLf
   vResult = vResult & "            @SectorDeNegocio = @SectorNeg," & vbCrLf
   vResult = vResult & "            @CodigoLote = ''," & vbCrLf
   vResult = vResult & "            @NivelDePrecio = '0'," & vbCrLf
   vResult = vResult & "            @Origen = '1'," & vbCrLf
   vResult = vResult & "            @DiaCumpleanos = 0," & vbCrLf
   vResult = vResult & "            @MesCumpleanos = 0," & vbCrLf
   vResult = vResult & "            @CorrespondenciaXenviar = 'N'," & vbCrLf
   vResult = vResult & "            @EsExtranjero = 'N'," & vbCrLf
   vResult = vResult & "            @TipoDocumentoIdentificacion = @P_TipoDocumentoIdentificacion," & vbCrLf
   vResult = vResult & "            @NombreOperador = @Operador" & vbCrLf
   vResult = vResult & "         EXEC Gp_AuxiliarGenera" & vbCrLf
   vResult = vResult & "            @P_ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "            ,@P_TipoAuxiliar = '0' -- Cliente" & vbCrLf
   vResult = vResult & "            ,@P_Codigo  = @P_Codigo" & vbCrLf
   vResult = vResult & "            ,@P_Nombre = @P_Nombre" & vbCrLf
   vResult = vResult & "            ,@P_Rif = @P_NumeroRIF" & vbCrLf
   vResult = vResult & "            ,@P_Operador = @Operador" & vbCrLf
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
   GetSpNameS10Update = "S10_ClienteUPD"
End Function

Private Function fSpParametersS10Update() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @P_ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @P_Codigo " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "         @P_Nombre " & gUtilSQL.getVarCharTypeForDB(80) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_NumeroRIF " & gUtilSQL.getVarCharTypeForDB(20) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_Direccion " & gUtilSQL.getVarCharTypeForDB(255) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_Ciudad " & gUtilSQL.getVarCharTypeForDB(100) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_Telefono " & gUtilSQL.getVarCharTypeForDB(40) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_FAX " & gUtilSQL.getVarCharTypeForDB(25) & " = NULL," & vbCrLf
   vResult = vResult & "         @P_Email " & gUtilSQL.getVarCharTypeForDB(40) & " = NULL" & vbCrLf
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
   vResult = vResult & "   SET @Operador = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "   IF NOT(@P_Ciudad IS Null)" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT NombreCiudad FROM Ciudad WHERE NombreCiudad = @P_Ciudad)" & vbCrLf
   vResult = vResult & "         INSERT INTO Ciudad (NombreCiudad) VALUES (@P_Ciudad)" & vbCrLf
   vResult = vResult & "   EXEC Gp_ClienteUPD" & vbCrLf
   vResult = vResult & "      @ConsecutivoCompania = @P_ConsecutivoCompania," & vbCrLf
   vResult = vResult & "      @Codigo = @P_Codigo," & vbCrLf
   vResult = vResult & "      @Nombre = @P_Nombre," & vbCrLf
   vResult = vResult & "      @NumeroRIF = @P_NumeroRIF," & vbCrLf
   vResult = vResult & "      @Direccion = @P_Direccion," & vbCrLf
   vResult = vResult & "      @Ciudad = @P_Ciudad," & vbCrLf
   vResult = vResult & "      @Telefono = @P_Telefono," & vbCrLf
   vResult = vResult & "      @FAX = @P_FAX," & vbCrLf
   vResult = vResult & "      @Email = @P_Email," & vbCrLf
   vResult = vResult & "      @NombreOperador = @Operador" & vbCrLf
   vResult = vResult & "   EXEC Gp_AuxiliarActualiza" & vbCrLf
   vResult = vResult & "      @P_ConsecutivoCompania = @P_ConsecutivoCompania" & vbCrLf
   vResult = vResult & "      ,@P_TipoAuxiliar = '0'" & vbCrLf
   vResult = vResult & "      ,@P_Codigo = @P_Codigo" & vbCrLf
   vResult = vResult & "      ,@P_Nombre = @P_Nombre" & vbCrLf
   vResult = vResult & "      ,@P_Rif = @P_NumeroRIF" & vbCrLf
   vResult = vResult & "      ,@P_Operador = @Operador" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpS10Update = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10Update", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetSpNameS10Delete = "S10_ClienteDEL"
End Function

Private Function fSpParametersS10Delete() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "   @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "   @CodigoCliente " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
    fSpParametersS10Delete = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10Delete", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10Delete() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   EXEC Gp_AxuliarBorrar " & vbCrLf
   vResult = vResult & "      @P_ConsecutivoCompania = @ConsecutivoCompania " & vbCrLf
   vResult = vResult & "      ,@P_TipoAuxiliar = '0' " & vbCrLf
   vResult = vResult & "      ,@P_Codigo = @CodigoCliente " & vbCrLf
   vResult = vResult & "   EXEC Gp_ExecuteClienteDEL @ConsecutivoCompania, @CodigoCliente" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpS10Delete = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10Delete", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetSpNameS10Select = "S10_ClienteSEL"
End Function

Private Function fSpParametersS10Select() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "   @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "   @FixedWhereClause " & gUtilSQL.getVarCharTypeForDB(4000) & " = ''" & vbCrLf
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
   vResult = vResult & "   SET @Sql = 'SELECT Codigo, Nombre, NumeroRIF," & vbCrLf
   vResult = vResult & "               Direccion, Ciudad, Telefono," & vbCrLf
   vResult = vResult & "               CuentaContableCxc, CuentaContableIngresos, CuentaContableAnticipo" & vbCrLf
   vResult = vResult & "         FROM cliente" & vbCrLf
   vResult = vResult & "         WHERE  ConsecutivoCompania = ' + CAST(@ConsecutivoCompania as varchar)" & vbCrLf
   vResult = vResult & "   IF @FixedWhereClause <> ''" & vbCrLf
   vResult = vResult & "      SET @Sql = @Sql + ' AND ' + @FixedWhereClause" & vbCrLf
   vResult = vResult & "   EXEC(@Sql)" & vbCrLf
   vResult = vResult & "   RETURN @@ROWCOUNT" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpS10Select = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10Select", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetFnNameS10Exists = "S10_ClienteExists"
End Function

Private Function fFnParametersS10Exists() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania int, @CodigoCliente varchar(10)"
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
   vResult = vResult & "   IF EXISTS(SELECT Codigo FROM cliente WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @CodigoCliente)" & vbCrLf
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
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *

