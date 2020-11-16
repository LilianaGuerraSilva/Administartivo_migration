VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCompaniaSP"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsCompaniaSp"
End Function
Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsCompaniaSp"
End Function
Public Function fCreateDropCompaniaSP(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      vResult = vResult And fCreateSpUPD(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10Select(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpConectarAS10(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpDesconectarDeS10(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpCompaniaUpdateConexionNomina(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpCompaniaFND(valShowMessage, valWithEncryption)
   Else
      vResult = vResult And fDropSpCompaniaFND(valShowMessage)
      vResult = vResult And fDropSpCompaniaUpdateConexionNomina(valShowMessage)
      vResult = vResult And fDropSpDesconectarDeS10(valShowMessage)
      vResult = vResult And fDropSpConectarAS10(valShowMessage)
      vResult = vResult And fDropSpS10Select(valShowMessage)
      vResult = vResult And fDropSpUPD(valShowMessage)
   End If
   fCreateDropCompaniaSP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropCompaniaSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Public Function fCreateSpUPD(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameCompaniaUPD(), fSpParametersUPD(), fStatementsSpUPD(), valShowMessage, valWithEncryption)
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
   vResult = insLibSp.fDropStoredProc(GetSpNameCompaniaUPD(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function GetSpNameCompaniaUPD() As String
   GetSpNameCompaniaUPD = "CompaniaUPD"
End Function
Private Function fSpParametersUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "   @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "   @Codigo " & gUtilSQL.getVarCharTypeForDB(4) & " = NULL," & vbCrLf
   vResult = vResult & "   @Nombre " & gUtilSQL.getVarCharTypeForDB(50) & " = NULL," & vbCrLf
   vResult = vResult & "   @NumeroDeRIF " & gUtilSQL.getVarCharTypeForDB(20) & " = NULL," & vbCrLf
   vResult = vResult & "   @NumeroNIT " & gUtilSQL.getVarCharTypeForDB(12) & " = NULL," & vbCrLf
   vResult = vResult & "   @TipoDeContribuyente char(1) = NULL," & vbCrLf
   vResult = vResult & "   @Direccion " & gUtilSQL.getVarCharTypeForDB(255) & " = NULL," & vbCrLf
   vResult = vResult & "   @Ciudad " & gUtilSQL.getVarCharTypeForDB(100) & " = NULL," & vbCrLf
   vResult = vResult & "   @Estado " & gUtilSQL.getVarCharTypeForDB(20) & " = NULL," & vbCrLf
   vResult = vResult & "   @ZonaPostal " & gUtilSQL.getVarCharTypeForDB(7) & " = NULL," & vbCrLf
   vResult = vResult & "   @Telefono1 " & gUtilSQL.getVarCharTypeForDB(9) & " = NULL," & vbCrLf
   vResult = vResult & "   @Telefono2 " & gUtilSQL.getVarCharTypeForDB(9) & " = NULL," & vbCrLf
   vResult = vResult & "   @Telefono3 " & gUtilSQL.getVarCharTypeForDB(9) & " = NULL," & vbCrLf
   vResult = vResult & "   @CodArea1 " & gUtilSQL.getVarCharTypeForDB(4) & " = NULL," & vbCrLf
   vResult = vResult & "   @CodArea2 " & gUtilSQL.getVarCharTypeForDB(4) & " = NULL," & vbCrLf
   vResult = vResult & "   @CodArea3 " & gUtilSQL.getVarCharTypeForDB(4) & " = NULL," & vbCrLf
   vResult = vResult & "   @FAX " & gUtilSQL.getVarCharTypeForDB(9) & " = NULL," & vbCrLf
   vResult = vResult & "   @CodAreaFax " & gUtilSQL.getVarCharTypeForDB(4) & " = NULL," & vbCrLf
   vResult = vResult & "   @FechaConversion " & gUtilSQL.getDateTypeForDB() & " = NULL," & vbCrLf
   vResult = vResult & "   @CompaniaConvertida char(1) = NULL," & vbCrLf
   vResult = vResult & "   @UsaModuloDeActivoFijo char(1) = NULL," & vbCrLf
   vResult = vResult & "   @UsaConexionConISLR char(1) = NULL," & vbCrLf
   vResult = vResult & "   @UsaConexionConAXI char(1) = NULL," & vbCrLf
   vResult = vResult & "   @UsaCentroDeCostos char(1) = NULL," & vbCrLf
   vResult = vResult & "   @UsaCostoDeVentas char(1) = NULL," & vbCrLf
   vResult = vResult & "   @UsaAuxiliares char(1) = NULL," & vbCrLf
   vResult = vResult & "   @UsaModuloDeContabilidad char(1) = NULL," & vbCrLf
   vResult = vResult & "   @EsCatalogoGeneral char(1) = NULL," & vbCrLf
   vResult = vResult & "   @TipoDeContribuyenteIVA char(1) = NULL," & vbCrLf
   vResult = vResult & "   @TablaCIIU char(1) = NULL," & vbCrLf
   vResult = vResult & "   @CodigoCIIU " & gUtilSQL.getVarCharTypeForDB(6) & " = NULL," & vbCrLf
   vResult = vResult & "   @PorcentajeDePatente " & gUtilSQL.getNumericTypeForDB(12, 2) & " = NULL," & vbCrLf
   vResult = vResult & "   @PersonaFiscal char(1) = NULL," & vbCrLf
   vResult = vResult & "   @Pais " & gUtilSQL.getVarCharTypeForDB(80) & " = NULL," & vbCrLf
   vResult = vResult & "   @CodigoPais " & gUtilSQL.getVarCharTypeForDB(4) & " = NULL," & vbCrLf
   vResult = vResult & "   @S10Ids " & gUtilSQL.getVarCharTypeForDB(4000) & " = NULL," & vbCrLf
   vResult = vResult & "   @UsaS10 char(1) = NULL," & vbCrLf
   vResult = vResult & "   @EsAgenteDeRetencionIva char(1) = NULL," & vbCrLf
   vResult = vResult & "   @NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & " = NULL" & vbCrLf
    fSpParametersUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Codigo " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Nombre " & gUtilSQL.getVarCharTypeForDB(50) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroDeRIF " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroNIT " & gUtilSQL.getVarCharTypeForDB(12) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TipoDeContribuyente char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Direccion " & gUtilSQL.getVarCharTypeForDB(255) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Ciudad " & gUtilSQL.getVarCharTypeForDB(100) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Estado " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_ZonaPostal " & gUtilSQL.getVarCharTypeForDB(7) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Telefono1 " & gUtilSQL.getVarCharTypeForDB(9) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Telefono2 " & gUtilSQL.getVarCharTypeForDB(9) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Telefono3 " & gUtilSQL.getVarCharTypeForDB(9) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodArea1 " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodArea2 " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodArea3 " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_FAX " & gUtilSQL.getVarCharTypeForDB(9) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodAreaFax " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_FechaConversion " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CompaniaConvertida char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_UsaModuloDeActivoFijo char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_UsaConexionConISLR char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_UsaConexionConAXI char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_UsaCentroDeCostos char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_UsaCostoDeVentas char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_UsaAuxiliares char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_UsaModuloDeContabilidad char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_EsCatalogoGeneral char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TipoDeContribuyenteIVA char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TablaCIIU char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoCIIU " & gUtilSQL.getVarCharTypeForDB(6) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_PorcentajeDePatente " & gUtilSQL.getNumericTypeForDB(12, 2) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_PersonaFiscal char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Pais " & gUtilSQL.getVarCharTypeForDB(80) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoPais " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_S10Ids " & gUtilSQL.getVarCharTypeForDB(4000) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_UsaS10 char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_EsAgenteDeRetencionIva char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "        DECLARE @Ori_TimeStamp timestamp" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Codigo " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Nombre " & gUtilSQL.getVarCharTypeForDB(50) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroDeRIF " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroNIT " & gUtilSQL.getVarCharTypeForDB(12) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_TipoDeContribuyente char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Direccion " & gUtilSQL.getVarCharTypeForDB(255) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Ciudad " & gUtilSQL.getVarCharTypeForDB(100) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Estado " & gUtilSQL.getVarCharTypeForDB(20) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_ZonaPostal " & gUtilSQL.getVarCharTypeForDB(7) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Telefono1 " & gUtilSQL.getVarCharTypeForDB(9) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Telefono2 " & gUtilSQL.getVarCharTypeForDB(9) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Telefono3 " & gUtilSQL.getVarCharTypeForDB(9) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodArea1 " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodArea2 " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodArea3 " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_FAX " & gUtilSQL.getVarCharTypeForDB(9) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodAreaFax " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_FechaConversion " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CompaniaConvertida char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_UsaModuloDeActivoFijo char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_UsaConexionConISLR char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_UsaConexionConAXI char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_UsaCentroDeCostos char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_UsaCostoDeVentas char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_UsaAuxiliares char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_UsaModuloDeContabilidad char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_EsCatalogoGeneral char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_TipoDeContribuyenteIVA char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_TablaCIIU char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoCIIU " & gUtilSQL.getVarCharTypeForDB(6) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_PorcentajeDePatente " & gUtilSQL.getNumericTypeForDB(12, 2) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_PersonaFiscal char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_Pais " & gUtilSQL.getVarCharTypeForDB(80) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoPais " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_S10Ids " & gUtilSQL.getVarCharTypeForDB(4000) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Up_UsaS10 char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_EsAgenteDeRetencionIva char(1)" & vbCrLf
   vResult = vResult & "   DECLARE @Up_NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   BEGIN TRAN" & vbCrLf
   vResult = vResult & "   SELECT @Ori_Codigo = Codigo," & vbCrLf
   vResult = vResult & "           @Ori_Nombre = Nombre," & vbCrLf
   vResult = vResult & "           @Ori_NumeroDeRIF = NumeroDeRIF," & vbCrLf
   vResult = vResult & "           @Ori_NumeroNIT = NumeroNIT," & vbCrLf
   vResult = vResult & "           @Ori_TipoDeContribuyente = TipoDeContribuyente," & vbCrLf
   vResult = vResult & "           @Ori_Direccion = Direccion," & vbCrLf
   vResult = vResult & "           @Ori_Ciudad = Ciudad," & vbCrLf
   vResult = vResult & "           @Ori_Estado = Estado," & vbCrLf
   vResult = vResult & "           @Ori_ZonaPostal = ZonaPostal," & vbCrLf
   vResult = vResult & "           @Ori_Telefono1 = Telefono1," & vbCrLf
   vResult = vResult & "           @Ori_Telefono2 = Telefono2," & vbCrLf
   vResult = vResult & "           @Ori_Telefono3 = Telefono3," & vbCrLf
   vResult = vResult & "           @Ori_CodArea1 = CodArea1," & vbCrLf
   vResult = vResult & "           @Ori_CodArea2 = CodArea2," & vbCrLf
   vResult = vResult & "           @Ori_CodArea3 = CodArea3," & vbCrLf
   vResult = vResult & "           @Ori_FAX = FAX," & vbCrLf
   vResult = vResult & "           @Ori_CodAreaFax = CodAreaFax," & vbCrLf
   vResult = vResult & "           @Ori_FechaConversion = FechaConversion," & vbCrLf
   vResult = vResult & "           @Ori_CompaniaConvertida = CompaniaConvertida," & vbCrLf
   vResult = vResult & "           @Ori_UsaModuloDeActivoFijo = UsaModuloDeActivoFijo," & vbCrLf
   vResult = vResult & "           @Ori_UsaConexionConISLR = UsaConexionConISLR," & vbCrLf
   vResult = vResult & "           @Ori_UsaConexionConAXI = UsaConexionConAXI," & vbCrLf
   vResult = vResult & "           @Ori_UsaCentroDeCostos = UsaCentroDeCostos," & vbCrLf
   vResult = vResult & "           @Ori_UsaCostoDeVentas = UsaCostoDeVentas," & vbCrLf
   vResult = vResult & "           @Ori_UsaAuxiliares = UsaAuxiliares," & vbCrLf
   vResult = vResult & "           @Ori_UsaModuloDeContabilidad = UsaModuloDeContabilidad," & vbCrLf
   vResult = vResult & "           @Ori_EsCatalogoGeneral = EsCatalogoGeneral," & vbCrLf
   vResult = vResult & "           @Ori_TipoDeContribuyenteIVA = TipoDeContribuyenteIVA," & vbCrLf
   vResult = vResult & "           @Ori_TablaCIIU = TablaCIIU," & vbCrLf
   vResult = vResult & "           @Ori_CodigoCIIU = CodigoCIIU," & vbCrLf
   vResult = vResult & "           @Ori_PorcentajeDePatente = PorcentajeDePatente," & vbCrLf
   vResult = vResult & "           @Ori_PersonaFiscal = PersonaFiscal," & vbCrLf
   vResult = vResult & "           @Ori_Pais = Pais," & vbCrLf
   vResult = vResult & "           @Ori_CodigoPais = CodigoPais," & vbCrLf
   vResult = vResult & "           @Ori_S10Ids = S10Ids," & vbCrLf
   vResult = vResult & "           @Ori_UsaS10 = UsaS10," & vbCrLf
   vResult = vResult & "           @Ori_EsAgenteDeRetencionIva = EsAgenteDeRetencionIva," & vbCrLf
   vResult = vResult & "           @Ori_NombreOperador = NombreOperador," & vbCrLf
   vResult = vResult & "           @Ori_TimeStamp = fldTimeStamp" & vbCrLf
   vResult = vResult & "   FROM Compania" & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "   IF @Codigo is null" & vbCrLf
   vResult = vResult & "      SET @Up_Codigo = @Ori_Codigo" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Codigo = @Codigo" & vbCrLf
   vResult = vResult & "   IF @Nombre is null" & vbCrLf
   vResult = vResult & "      SET @Up_Nombre = @Ori_Nombre" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Nombre = @Nombre" & vbCrLf
   vResult = vResult & "   IF @NumeroDeRIF is null" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroDeRIF = @Ori_NumeroDeRIF" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroDeRIF = @NumeroDeRIF" & vbCrLf
   vResult = vResult & "   IF @NumeroNIT is null" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroNIT = @Ori_NumeroNIT" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroNIT = @NumeroNIT" & vbCrLf
   vResult = vResult & "   IF @TipoDeContribuyente is null" & vbCrLf
   vResult = vResult & "      SET @Up_TipoDeContribuyente = @Ori_TipoDeContribuyente" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TipoDeContribuyente = @TipoDeContribuyente" & vbCrLf
   vResult = vResult & "   IF @Direccion is null" & vbCrLf
   vResult = vResult & "      SET @Up_Direccion = @Ori_Direccion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Direccion = @Direccion" & vbCrLf
   vResult = vResult & "   IF @Ciudad is null" & vbCrLf
   vResult = vResult & "      SET @Up_Ciudad = @Ori_Ciudad" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Ciudad = @Ciudad" & vbCrLf
   vResult = vResult & "   IF @Estado is null" & vbCrLf
   vResult = vResult & "      SET @Up_Estado = @Ori_Estado" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Estado = @Estado" & vbCrLf
   vResult = vResult & "   IF @ZonaPostal is null" & vbCrLf
   vResult = vResult & "      SET @Up_ZonaPostal = @Ori_ZonaPostal" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_ZonaPostal = @ZonaPostal" & vbCrLf
   vResult = vResult & "   IF @Telefono1 is null" & vbCrLf
   vResult = vResult & "      SET @Up_Telefono1 = @Ori_Telefono1" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Telefono1 = @Telefono1" & vbCrLf
   vResult = vResult & "   IF @Telefono2 is null" & vbCrLf
   vResult = vResult & "      SET @Up_Telefono2 = @Ori_Telefono2" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Telefono2 = @Telefono2" & vbCrLf
   vResult = vResult & "   IF @Telefono3 is null" & vbCrLf
   vResult = vResult & "      SET @Up_Telefono3 = @Ori_Telefono3" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Telefono3 = @Telefono3" & vbCrLf
   vResult = vResult & "   IF @CodArea1 is null" & vbCrLf
   vResult = vResult & "      SET @Up_CodArea1 = @Ori_CodArea1" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodArea1 = @CodArea1" & vbCrLf
   vResult = vResult & "   IF @CodArea2 is null" & vbCrLf
   vResult = vResult & "      SET @Up_CodArea2 = @Ori_CodArea2" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodArea2 = @CodArea2" & vbCrLf
   vResult = vResult & "   IF @CodArea3 is null" & vbCrLf
   vResult = vResult & "      SET @Up_CodArea3 = @Ori_CodArea3" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodArea3 = @CodArea3" & vbCrLf
   vResult = vResult & "   IF @FAX is null" & vbCrLf
   vResult = vResult & "      SET @Up_FAX = @Ori_FAX" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_FAX = @FAX" & vbCrLf
   vResult = vResult & "   IF @CodAreaFax is null" & vbCrLf
   vResult = vResult & "      SET @Up_CodAreaFax = @Ori_CodAreaFax" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodAreaFax = @CodAreaFax" & vbCrLf
   vResult = vResult & "   IF @FechaConversion is null" & vbCrLf
   vResult = vResult & "      SET @Up_FechaConversion = @Ori_FechaConversion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_FechaConversion = @FechaConversion" & vbCrLf
   vResult = vResult & "   IF @CompaniaConvertida is null" & vbCrLf
   vResult = vResult & "      SET @Up_CompaniaConvertida = @Ori_CompaniaConvertida" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CompaniaConvertida = @CompaniaConvertida" & vbCrLf
   vResult = vResult & "   IF @UsaModuloDeActivoFijo is null" & vbCrLf
   vResult = vResult & "      SET @Up_UsaModuloDeActivoFijo = @Ori_UsaModuloDeActivoFijo" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_UsaModuloDeActivoFijo = @UsaModuloDeActivoFijo" & vbCrLf
   vResult = vResult & "   IF @UsaConexionConISLR is null" & vbCrLf
   vResult = vResult & "      SET @Up_UsaConexionConISLR = @Ori_UsaConexionConISLR" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_UsaConexionConISLR = @UsaConexionConISLR" & vbCrLf
   vResult = vResult & "   IF @UsaConexionConAXI is null" & vbCrLf
   vResult = vResult & "      SET @Up_UsaConexionConAXI = @Ori_UsaConexionConAXI" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_UsaConexionConAXI = @UsaConexionConAXI" & vbCrLf
   vResult = vResult & "   IF @UsaCentroDeCostos is null" & vbCrLf
   vResult = vResult & "      SET @Up_UsaCentroDeCostos = @Ori_UsaCentroDeCostos" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_UsaCentroDeCostos = @UsaCentroDeCostos" & vbCrLf
   vResult = vResult & "   IF @UsaCostoDeVentas is null" & vbCrLf
   vResult = vResult & "      SET @Up_UsaCostoDeVentas = @Ori_UsaCostoDeVentas" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_UsaCostoDeVentas = @UsaCostoDeVentas" & vbCrLf
   vResult = vResult & "   IF @UsaAuxiliares is null" & vbCrLf
   vResult = vResult & "      SET @Up_UsaAuxiliares = @Ori_UsaAuxiliares" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_UsaAuxiliares = @UsaAuxiliares" & vbCrLf
   vResult = vResult & "   IF @UsaModuloDeContabilidad is null" & vbCrLf
   vResult = vResult & "      SET @Up_UsaModuloDeContabilidad = @Ori_UsaModuloDeContabilidad" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_UsaModuloDeContabilidad = @UsaModuloDeContabilidad" & vbCrLf
   vResult = vResult & "   IF @EsCatalogoGeneral is null" & vbCrLf
   vResult = vResult & "      SET @Up_EsCatalogoGeneral = @Ori_EsCatalogoGeneral" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_EsCatalogoGeneral = @EsCatalogoGeneral" & vbCrLf
   vResult = vResult & "   IF @TipoDeContribuyenteIVA is null" & vbCrLf
   vResult = vResult & "      SET @Up_TipoDeContribuyenteIVA = @Ori_TipoDeContribuyenteIVA" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TipoDeContribuyenteIVA = @TipoDeContribuyenteIVA" & vbCrLf
   vResult = vResult & "   IF @TablaCIIU is null" & vbCrLf
   vResult = vResult & "      SET @Up_TablaCIIU = @Ori_TablaCIIU" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TablaCIIU = @TablaCIIU" & vbCrLf
   vResult = vResult & "   IF @CodigoCIIU is null" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoCIIU = @Ori_CodigoCIIU" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoCIIU = @CodigoCIIU" & vbCrLf
   vResult = vResult & "   IF @PorcentajeDePatente is null" & vbCrLf
   vResult = vResult & "      SET @Up_PorcentajeDePatente = @Ori_PorcentajeDePatente" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_PorcentajeDePatente = @PorcentajeDePatente" & vbCrLf
   vResult = vResult & "   IF @PersonaFiscal is null" & vbCrLf
   vResult = vResult & "      SET @Up_PersonaFiscal = @Ori_PersonaFiscal" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_PersonaFiscal = @PersonaFiscal" & vbCrLf
   vResult = vResult & "   IF @Pais is null" & vbCrLf
   vResult = vResult & "      SET @Up_Pais = @Ori_Pais" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Pais = @Pais" & vbCrLf
   vResult = vResult & "   IF @CodigoPais is null" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoPais = @Ori_CodigoPais" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoPais = @CodigoPais" & vbCrLf
   vResult = vResult & "   IF @S10Ids is null" & vbCrLf
   vResult = vResult & "      SET @Up_S10Ids = @Ori_S10Ids" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_S10Ids = @S10Ids" & vbCrLf
   vResult = vResult & "   IF @UsaS10 is null" & vbCrLf
   vResult = vResult & "      SET @Up_UsaS10 = @Ori_UsaS10" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_UsaS10 = @UsaS10" & vbCrLf
   vResult = vResult & "   IF @EsAgenteDeRetencionIva is null" & vbCrLf
   vResult = vResult & "      SET @Up_EsAgenteDeRetencionIva = @Ori_EsAgenteDeRetencionIva" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_EsAgenteDeRetencionIva = @EsAgenteDeRetencionIva" & vbCrLf
   vResult = vResult & "   IF @NombreOperador is null" & vbCrLf
   vResult = vResult & "      SET @Up_NombreOperador = @Ori_NombreOperador" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NombreOperador = @NombreOperador" & vbCrLf
   vResult = vResult & "   UPDATE Compania" & vbCrLf
   vResult = vResult & "   SET Codigo = @Up_Codigo," & vbCrLf
   vResult = vResult & "        Nombre = @Up_Nombre," & vbCrLf
   vResult = vResult & "        NumeroDeRIF = @Up_NumeroDeRIF," & vbCrLf
   vResult = vResult & "        NumeroNIT = @Up_NumeroNIT," & vbCrLf
   vResult = vResult & "        TipoDeContribuyente = @Up_TipoDeContribuyente," & vbCrLf
   vResult = vResult & "        Direccion = @Up_Direccion," & vbCrLf
   vResult = vResult & "        Ciudad = @Up_Ciudad," & vbCrLf
   vResult = vResult & "        Estado = @Up_Estado," & vbCrLf
   vResult = vResult & "        ZonaPostal = @Up_ZonaPostal," & vbCrLf
   vResult = vResult & "        Telefono1 = @Up_Telefono1," & vbCrLf
   vResult = vResult & "        Telefono2 = @Up_Telefono2," & vbCrLf
   vResult = vResult & "        Telefono3 = @Up_Telefono3," & vbCrLf
   vResult = vResult & "        CodArea1 = @Up_CodArea1," & vbCrLf
   vResult = vResult & "        CodArea2 = @Up_CodArea2," & vbCrLf
   vResult = vResult & "        CodArea3 = @Up_CodArea3," & vbCrLf
   vResult = vResult & "        FAX = @Up_FAX," & vbCrLf
   vResult = vResult & "        CodAreaFax = @Up_CodAreaFax," & vbCrLf
   vResult = vResult & "        FechaConversion = @Up_FechaConversion," & vbCrLf
   vResult = vResult & "        CompaniaConvertida = @Up_CompaniaConvertida," & vbCrLf
   vResult = vResult & "        UsaModuloDeActivoFijo = @Up_UsaModuloDeActivoFijo," & vbCrLf
   vResult = vResult & "        UsaConexionConISLR = @Up_UsaConexionConISLR," & vbCrLf
   vResult = vResult & "        UsaConexionConAXI = @Up_UsaConexionConAXI," & vbCrLf
   vResult = vResult & "        UsaCentroDeCostos = @Up_UsaCentroDeCostos," & vbCrLf
   vResult = vResult & "        UsaCostoDeVentas = @Up_UsaCostoDeVentas," & vbCrLf
   vResult = vResult & "        UsaAuxiliares = @Up_UsaAuxiliares," & vbCrLf
   vResult = vResult & "        UsaModuloDeContabilidad = @Up_UsaModuloDeContabilidad," & vbCrLf
   vResult = vResult & "        EsCatalogoGeneral = @Up_EsCatalogoGeneral," & vbCrLf
   vResult = vResult & "        TipoDeContribuyenteIVA = @Up_TipoDeContribuyenteIVA," & vbCrLf
   vResult = vResult & "        TablaCIIU = @Up_TablaCIIU," & vbCrLf
   vResult = vResult & "        CodigoCIIU = @Up_CodigoCIIU," & vbCrLf
   vResult = vResult & "        PorcentajeDePatente = @Up_PorcentajeDePatente," & vbCrLf
   vResult = vResult & "        PersonaFiscal = @Up_PersonaFiscal," & vbCrLf
   vResult = vResult & "        Pais = @Up_Pais," & vbCrLf
   vResult = vResult & "        CodigoPais = @Up_CodigoPais," & vbCrLf
   vResult = vResult & "        S10Ids = @Up_S10Ids," & vbCrLf
   vResult = vResult & "        UsaS10 = @Up_UsaS10," & vbCrLf
   vResult = vResult & "        EsAgenteDeRetencionIva = @Up_EsAgenteDeRetencionIva," & vbCrLf
   vResult = vResult & "        NombreOperador = @Up_NombreOperador," & vbCrLf
   vResult = vResult & "        FechaUltimaModificacion = " & gUtilSQL.DfCDateSQL(gUtilSQL.GetSQLFechaActual()) & "" & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
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
Public Function fCreateSpConectarAS10(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameConectarAS10(), fSpParametersConectarAS10(), fStatementsSpConectarAS10(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpConectarAS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpConectarAS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fDropSpConectarAS10(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameConectarAS10(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpConectarAS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpConectarAS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function GetSpNameConectarAS10() As String
   GetSpNameConectarAS10 = "S10_CompaniaConectarS10"
End Function
Private Function fSpParametersConectarAS10() As String
   Dim vResult As String
   vResult = " @ConsecutivoCompaniaGalac " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & " @S10Id " & gUtilSQL.getVarCharTypeForDB(8) & " " & vbCrLf
   fSpParametersConectarAS10 = vResult
End Function
Private Function fStatementsSpConectarAS10() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = " BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @S10IdsExistentes varchar(4000)" & vbCrLf
   vResult = vResult & "   DECLARE @S10IdAct varchar(8)" & vbCrLf
   vResult = vResult & "   DECLARE @S10IdExists bit" & vbCrLf
   vResult = vResult & "   DECLARE @Value varchar(4000)" & vbCrLf
   vResult = vResult & "   DECLARE @Operador varchar(10)" & vbCrLf
   vResult = vResult & "   DECLARE @TipoProveedor varchar(20)" & vbCrLf
   vResult = vResult & "   DECLARE @S10DBName varchar(128)" & vbCrLf
   vResult = vResult & "   SELECT TOP 1"
   vResult = vResult & "   @S10DBName = Y.Value"
   vResult = vResult & "   FROM Comun.SettValueByCompany Stt"
   vResult = vResult & "   CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = " & gUtilSQL.fSimpleSqlValue("PuedeUsarS10") & ") AS X(Value)"
   vResult = vResult & "   CROSS APPLY (SELECT Value FROM Comun.SettValueByCompany WHERE Comun.SettValueByCompany.ConsecutivoCompania = Stt.ConsecutivoCompania AND NameSettDefinition = " & gUtilSQL.fSimpleSqlValue("S10DBName") & ") AS Y(Value)"
   vResult = vResult & "   WHERE X.Value = " & gUtilSQL.fSimpleSqlValue("S")
   vResult = vResult & "   GROUP BY Y.Value"
   vResult = vResult & "   IF EXISTS(SELECT name FROM master.sys.databases WHERE name COLLATE Modern_Spanish_CI_AS = @S10DBName)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      IF EXISTS(SELECT ConsecutivoCompania FROM COMPANIA WHERE ConsecutivoCompania = @ConsecutivoCompaniaGalac)" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SELECT @S10IdsExistentes = S10Ids FROM COMPANIA WHERE ConsecutivoCompania = @ConsecutivoCompaniaGalac" & vbCrLf
   vResult = vResult & "         IF ((@S10IdsExistentes = '') OR (@S10IdsExistentes IS NULL))" & vbCrLf
   vResult = vResult & "            SET @Value = @S10Id" & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            IF ((PATINDEX('%;' + @S10Id + ';%', @S10IdsExistentes) > 0)" & vbCrLf
   vResult = vResult & "               OR (PATINDEX(@S10Id + ';%', @S10IdsExistentes) > 0)" & vbCrLf
   vResult = vResult & "               OR (PATINDEX('%;' + @S10Id, @S10IdsExistentes) > 0))" & vbCrLf
   vResult = vResult & "               SET @S10IdExists = '1' -- TRUE" & vbCrLf
   vResult = vResult & "            ELSE" & vbCrLf
   vResult = vResult & "               SET @S10IdExists = '0' -- FALSE" & vbCrLf
   vResult = vResult & "            IF @S10IdExists = '1' --TRUE" & vbCrLf
   vResult = vResult & "               SET @Value = @S10IdsExistentes + ';' + @S10Id" & vbCrLf
   vResult = vResult & "            ELSE" & vbCrLf
   vResult = vResult & "               SET @Value = @S10Id" & vbCrLf
   vResult = vResult & "         END" & vbCrLf
   vResult = vResult & "         SET @Operador = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "         EXEC Gp_CompaniaUPD @ConsecutivoCompania = @ConsecutivoCompaniaGalac, @UsaS10 = 'S', @S10Ids = @Value, @NombreOperador = @Operador" & vbCrLf
   vResult = vResult & "         SELECT @TipoProveedor = Nombre FROM TipoProveedor WHERE Nombre = 'NACIONAL'" & vbCrLf
   vResult = vResult & "         IF ((@TipoProveedor = null) or (@TipoProveedor = ''))" & vbCrLf
   vResult = vResult & "            EXEC Gp_TipoProveedorINS @ConsecutivoCompaniaGalac, 'NACIONAL'" & vbCrLf
   vResult = vResult & "      END" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpConectarAS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpConectarAS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Public Function fCreateSpDesconectarDeS10(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameDesconectarDeS10(), fSpParametersCompaniaDesconectarDeS10(), fStatementsSpDesconectarDeS10(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpDesconectarDeS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpDesconectarDeS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fDropSpDesconectarDeS10(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameDesconectarDeS10(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpDesconectarDeS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpDesconectarDeS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function GetSpNameDesconectarDeS10() As String
   GetSpNameDesconectarDeS10 = "S10_CompaniaDesconectarS10"
End Function
Private Function fSpParametersCompaniaDesconectarDeS10() As String
   Dim vResult As String
   vResult = " @ConsecutivoCompaniaGalac " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & " @S10Id " & gUtilSQL.getVarCharTypeForDB(8) & " " & vbCrLf
   fSpParametersCompaniaDesconectarDeS10 = vResult
End Function
Private Function fStatementsSpDesconectarDeS10() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = " BEGIN "
   vResult = vResult & "   DECLARE @S10IdsExistentes " & gUtilSQL.getVarCharTypeForDB(4000) & vbCrLf
   vResult = vResult & "   DECLARE @S10IdExists " & gUtilSQL.getBoolTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @S10IdAct " & gUtilSQL.getVarCharTypeForDB(8) & vbCrLf
   vResult = vResult & "   DECLARE @Value " & gUtilSQL.getVarCharTypeForDB(4000) & vbCrLf
   vResult = vResult & "   DECLARE @UsaS10Chr " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Operador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @I " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @J " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   IF EXISTS(SELECT ConsecutivoCompania FROM COMPANIA WHERE ConsecutivoCompania = @ConsecutivoCompaniaGalac)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT @S10IdsExistentes = S10Ids FROM COMPANIA WHERE ConsecutivoCompania = @ConsecutivoCompaniaGalac" & vbCrLf
   vResult = vResult & "      IF ((@S10IdsExistentes = '') OR (@S10IdsExistentes IS NULL))" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SET @Value = ''" & vbCrLf
   vResult = vResult & "         SET @UsaS10Chr = 'N'" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "      ELSE " & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         IF ((PATINDEX('%;' + @S10Id + ';%', @S10IdsExistentes) > 0)" & vbCrLf
   vResult = vResult & "            OR (PATINDEX(@S10Id + ';%', @S10IdsExistentes) > 0)" & vbCrLf
   vResult = vResult & "            OR (PATINDEX('%;' + @S10Id, @S10IdsExistentes) > 0))" & vbCrLf
   vResult = vResult & "            SET @S10IdExists = '1' -- TRUE" & vbCrLf
   vResult = vResult & "         ELSE " & vbCrLf
   vResult = vResult & "            SET @S10IdExists = '0' -- FALSE" & vbCrLf
   vResult = vResult & "         IF @S10IdExists = '1' --TRUE" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @I = PATINDEX('%' + @S10Id + '%', @S10IdsExistentes)" & vbCrLf
   vResult = vResult & "            SET @J = LEN(@S10Id)" & vbCrLf
   vResult = vResult & "            SET @Value = LEFT(@S10IdsExistentes, @I - 1) + SUBSTRING(@S10IdsExistentes, @I + @J + 1, LEN(@S10IdsExistentes) - @J)" & vbCrLf
   vResult = vResult & "            IF (RIGHT(@Value, 1) = ';')" & vbCrLf
   vResult = vResult & "               SET @Value = LEFT(@Value, LEN(@Value) - 1)" & vbCrLf
   vResult = vResult & "            IF (LEN(@Value) > 0)" & vbCrLf
   vResult = vResult & "               SET @UsaS10Chr = " & gUtilSQL.fBooleanToStrSQL(True) & vbCrLf
   vResult = vResult & "            ELSE " & vbCrLf
   vResult = vResult & "               SET @UsaS10Chr = " & gUtilSQL.fBooleanToStrSQL(False) & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "         ELSE " & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @Value = @S10IdsExistentes" & vbCrLf
   vResult = vResult & "            SET @UsaS10Chr = " & gUtilSQL.fBooleanToSqlValue(True) & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "      SET @Operador = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "      EXEC Gp_CompaniaUPD @ConsecutivoCompania = @ConsecutivoCompaniaGalac, @UsaS10 = @UsaS10Chr, @S10Ids = @Value, @NombreOperador = @Operador" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpDesconectarDeS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpDesconectarDeS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetSpNameS10Select = "S10_CompaniaSEL"
End Function

Private Function fSpParametersS10Select() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "   @CCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " = 0" & vbCrLf
    fSpParametersS10Select = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10Select", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10Select() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Sql " & gUtilSQL.getVarCharTypeForDB(4000) & "" & vbCrLf
   vResult = vResult & "   DECLARE @SqlWhere " & gUtilSQL.getVarCharTypeForDB(1000) & "" & vbCrLf
   vResult = vResult & "   IF @CCompania > 0" & vbCrLf
   vResult = vResult & "      SET @SqlWhere = ' WHERE ConsecutivoCompania = ' + CAST(@CCompania AS varchar)" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @SqlWhere = ''" & vbCrLf
   vResult = vResult & "   SET @Sql = 'SELECT ConsecutivoCompania, Codigo, Nombre, NumeroDeRIF, UsaS10, S10Ids FROM COMPANIA ' + @SqlWhere" & vbCrLf
   vResult = vResult & "   EXEC (@Sql)" & vbCrLf
   vResult = vResult & "   RETURN @@ROWCOUNT" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpS10Select = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10Select", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
 
'--------------------
Private Function fCreateSpCompaniaUpdateConexionNomina(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameCompaniaUpdateConexionNomina(), fSpParametersCompaniaUpdateConexionNomina(), fStatementsSpCompaniaUpdateConexionNomina(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpCompaniaUpdateConexionNomina = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpCompaniaUpdateConexionNomina", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpCompaniaUpdateConexionNomina(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameCompaniaUpdateConexionNomina(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpCompaniaUpdateConexionNomina = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpCompaniaUpdateConexionNomina", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameCompaniaUpdateConexionNomina() As String
   GetSpNameCompaniaUpdateConexionNomina = "CompaniaUpdateConexionNomina"
End Function

Private Function fSpParametersCompaniaUpdateConexionNomina() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "(@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & " @CodigoDeIntegracion " & gUtilSQL.getVarCharTypeForDB(40) & "," & vbCrLf
   vResult = vResult & " @EstaIntegradaConNomina char(1))" & vbCrLf
    fSpParametersCompaniaUpdateConexionNomina = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersCompaniaUpdateConexionNomina", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpCompaniaUpdateConexionNomina() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "    DECLARE @ReturnValue " & gUtilSQL.getNumericTypeForDB(9, 0) & ";" & vbCrLf
   vResult = vResult & "    DECLARE @ValidationMsg " & gUtilSQL.getVarCharTypeForDB(1500) & ";" & vbCrLf
   vResult = vResult & "    SET NOCOUNT ON;" & vbCrLf
   vResult = vResult & "    SET @ReturnValue = -1;" & vbCrLf
   vResult = vResult & "    SET @ValidationMsg = '';" & vbCrLf
   vResult = vResult & "    IF EXISTS(SELECT ConsecutivoCompania FROM COMPANIA  WHERE CodigoDeIntegracion = @CodigoDeIntegracion AND ConsecutivoCompania <> @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "       SET @ReturnValue = -1" & vbCrLf
   vResult = vResult & "      END  " & vbCrLf
   vResult = vResult & "    ELSE" & vbCrLf
   vResult = vResult & "    IF EXISTS(SELECT ConsecutivoCompania FROM COMPANIA  WHERE ConsecutivoCompania = @ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "          BEGIN TRAN" & vbCrLf
   vResult = vResult & "             UPDATE  dbo.COMPANIA" & vbCrLf
   vResult = vResult & "             SET  CodigoDeIntegracion = @CodigoDeIntegracion," & vbCrLf
   vResult = vResult & "                  EstaIntegradaConNomina = @EstaIntegradaConNomina" & vbCrLf
   vResult = vResult & "             WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "             SET @ReturnValue = @@ROWCOUNT" & vbCrLf
   vResult = vResult & "                  IF @@ERROR = 0" & vbCrLf
   vResult = vResult & "                      BEGIN" & vbCrLf
   vResult = vResult & "                         COMMIT TRAN" & vbCrLf
   vResult = vResult & "                         IF @ReturnValue = 0" & vbCrLf
   vResult = vResult & "                          RAISERROR('El registro ha sido modificado o eliminado por otro usuario', 14, 1)" & vbCrLf
   vResult = vResult & "                     END " & vbCrLf
   vResult = vResult & "                   ELSE" & vbCrLf
   vResult = vResult & "                      BEGIN" & vbCrLf
   vResult = vResult & "                    SET @ReturnValue = -1" & vbCrLf
   vResult = vResult & "                        SET @ValidationMsg = 'Se ha producido un error al Modificar: ' + CAST(@@ERROR AS NVARCHAR(8))" & vbCrLf
   vResult = vResult & "                        RAISERROR(@ValidationMsg, 14 ,1)" & vbCrLf
   vResult = vResult & "                        ROLLBACK" & vbCrLf
   vResult = vResult & "                       END  " & vbCrLf
   vResult = vResult & "       END  " & vbCrLf
   vResult = vResult & "    ELSE " & vbCrLf
   vResult = vResult & "      RAISERROR('El registro no existe.', 14, 1)" & vbCrLf
   vResult = vResult & "    RETURN @ReturnValue" & vbCrLf
   vResult = vResult & "END "
   fStatementsSpCompaniaUpdateConexionNomina = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpCompaniaUpdateConexionNomina", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function



Private Function fCreateSpCompaniaFND(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameCompaniaFND(), fSpParametersCompaniaFND(), fStatementsSpCompaniaFND(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpCompaniaFND = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSp]", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpCompaniaFND(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameCompaniaFND(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpCompaniaFND = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpCompaniaFND", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameCompaniaFND() As String
  GetSpNameCompaniaFND = "CompaniaFND"
End Function

Private Function fSpParametersCompaniaFND() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "@SQLWhere varchar (2000)  = null," & vbCrLf
   vResult = vResult & "@SQLOrderBy varchar (500)  = null," & vbCrLf
   vResult = vResult & "@DateFormat varchar (3)  = null" & vbCrLf
   fSpParametersCompaniaFND = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersCompaniaFND", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpCompaniaFND() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   SET NOCOUNT ON;" & vbCrLf
   vResult = vResult & "   DECLARE @strSQL AS  varchar (7000)" & vbCrLf
   vResult = vResult & "   SET @strSQL = 'SET DateFormat ' + @DateFormat +" & vbCrLf
   vResult = vResult & "      ' SELECT" & vbCrLf
   vResult = vResult & "      Codigo," & vbCrLf
   vResult = vResult & "      Nombre," & vbCrLf
   vResult = vResult & "      CodigoDeIntegracion," & vbCrLf
   vResult = vResult & "      ''COLPIVOTE'' AS ColControl," & vbCrLf
   vResult = vResult & "      ConsecutivoCompania" & vbCrLf
   vResult = vResult & "      FROM dbo.COMPANIA'" & vbCrLf
   vResult = vResult & "   IF (NOT @SQLWhere IS NULL) AND (@SQLWhere <> '')" & vbCrLf
   vResult = vResult & "      SET @strSQL = @strSQL + ' WHERE ' + @SQLWhere" & vbCrLf
   vResult = vResult & "   IF (NOT @SQLOrderBy IS NULL) AND (@SQLOrderBy <> '')" & vbCrLf
   vResult = vResult & "      SET @strSQL = @strSQL + ' ORDER BY ' + @SQLOrderBy" & vbCrLf
   vResult = vResult & "   EXEC(@strSQL)" & vbCrLf
   vResult = vResult & "END "
  fStatementsSpCompaniaFND = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpCompaniaFND", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function



