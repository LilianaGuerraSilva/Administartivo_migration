VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsRendicionSp"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsRendicionSp"
End Function

Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsRendicionSp"
End Function

Public Function fCreateDropRendicionSP(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      vResult = vResult And fCreateSpINS(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpUPD(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpDel(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10INS(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10RendiconesSaldoSel(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10RendicionCxPUPD(valShowMessage, valWithEncryption)
   Else
      vResult = vResult And fDropSpDel(valShowMessage)
      vResult = vResult And fDropSpUPD(valShowMessage)
      vResult = vResult And fDropSpINS(valShowMessage)
      vResult = vResult And fDropSpS10INS(valShowMessage)
      vResult = vResult And fDropSpS10RendiconesSaldoSel(valShowMessage)
      vResult = vResult And fDropS10RendicionCxPUPD(valShowMessage)
   End If
   fCreateDropRendicionSP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropRendicionSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

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
   GetSpNameINS = "RendicionINS"
End Function

Private Function fSpParametersINS() As String
   Dim vResult As String
   vResult = "   @ConsecutivoCompania int," & vbCrLf
   vResult = vResult & "   @Consecutivo int," & vbCrLf
   vResult = vResult & "   @CodigoBeneficiario varchar(10) = ''," & vbCrLf
   vResult = vResult & "   @FechaApertura smalldatetime = ''," & vbCrLf
   vResult = vResult & "   @FechaCierre smalldatetime = ''," & vbCrLf
   vResult = vResult & "   @Status char(1) = '0'," & vbCrLf
   vResult = vResult & "   @TotalAdelantos decimal(25,5) = 0," & vbCrLf
   vResult = vResult & "   @TotalCxP decimal(25,5) = 0," & vbCrLf
   vResult = vResult & "   @GeneraReembolso char(1) = 'N'," & vbCrLf
   vResult = vResult & "   @CodigoCuentaBancaria varchar(5) = ''," & vbCrLf
   vResult = vResult & "   @GeneraImpuestoBancario char(1) = 'N'," & vbCrLf
   vResult = vResult & "   @CodigoConceptoBancario varchar(8) = ''," & vbCrLf
   vResult = vResult & "   @NumeroDocumento varchar(15) = ''," & vbCrLf
   vResult = vResult & "   @Observaciones varchar(100) = ''," & vbCrLf
   vResult = vResult & "   @NombreOperador varchar(10) = ''," & vbCrLf
   vResult = vResult & "   @FechaUltimaModificacion smalldatetime = ''" & vbCrLf
   fSpParametersINS = vResult
End Function

Private Function fStatementsSpINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   SET NOCOUNT ON;" & vbCrLf
   vResult = vResult & "   INSERT INTO Rendicion(" & vbCrLf
   vResult = vResult & "         ConsecutivoCompania," & vbCrLf
   vResult = vResult & "         Consecutivo," & vbCrLf
   vResult = vResult & "         CodigoBeneficiario," & vbCrLf
   vResult = vResult & "         FechaApertura," & vbCrLf
   vResult = vResult & "         FechaCierre," & vbCrLf
   vResult = vResult & "         Status," & vbCrLf
   vResult = vResult & "         TotalAdelantos," & vbCrLf
   vResult = vResult & "         TotalCxP," & vbCrLf
   vResult = vResult & "         GeneraReembolso," & vbCrLf
   vResult = vResult & "         CodigoCuentaBancaria," & vbCrLf
   vResult = vResult & "         GeneraImpuestoBancario," & vbCrLf
   vResult = vResult & "         CodigoConceptoBancario," & vbCrLf
   vResult = vResult & "         NumeroDocumento," & vbCrLf
   vResult = vResult & "         Observaciones," & vbCrLf
   vResult = vResult & "         NombreOperador," & vbCrLf
   vResult = vResult & "         FechaUltimaModificacion)" & vbCrLf
   vResult = vResult & "      VALUES(" & vbCrLf
   vResult = vResult & "         @ConsecutivoCompania," & vbCrLf
   vResult = vResult & "         @Consecutivo," & vbCrLf
   vResult = vResult & "         @CodigoBeneficiario," & vbCrLf
   vResult = vResult & "         @FechaApertura," & vbCrLf
   vResult = vResult & "         @FechaCierre," & vbCrLf
   vResult = vResult & "         @Status," & vbCrLf
   vResult = vResult & "         @TotalAdelantos," & vbCrLf
   vResult = vResult & "         @TotalCxP," & vbCrLf
   vResult = vResult & "         @GeneraReembolso," & vbCrLf
   vResult = vResult & "         @CodigoCuentaBancaria," & vbCrLf
   vResult = vResult & "         @GeneraImpuestoBancario," & vbCrLf
   vResult = vResult & "         @CodigoConceptoBancario," & vbCrLf
   vResult = vResult & "         @NumeroDocumento," & vbCrLf
   vResult = vResult & "         @Observaciones," & vbCrLf
   vResult = vResult & "         @NombreOperador," & vbCrLf
   vResult = vResult & "         @FechaUltimaModificacion)" & vbCrLf
   vResult = vResult & "END "
   fStatementsSpINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

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
   GetSpNameUPD = "RendicionUPD"
End Function

Private Function fSpParametersUPD() As String
   Dim vResult As String
   vResult = "   @ConsecutivoCompania int," & vbCrLf
   vResult = vResult & "   @Consecutivo int," & vbCrLf
   vResult = vResult & "   @CodigoBeneficiario varchar(10) = NULL," & vbCrLf
   vResult = vResult & "   @FechaApertura smalldatetime = NULL," & vbCrLf
   vResult = vResult & "   @FechaCierre smalldatetime = NULL," & vbCrLf
   vResult = vResult & "   @Status char(1) = NULL," & vbCrLf
   vResult = vResult & "   @TotalAdelantos decimal(25,5) = NULL," & vbCrLf
   vResult = vResult & "   @TotalCxP decimal(25,5) = NULL," & vbCrLf
   vResult = vResult & "   @GeneraReembolso char(1) = NULL," & vbCrLf
   vResult = vResult & "   @CodigoCuentaBancaria varchar(5) = NULL," & vbCrLf
   vResult = vResult & "   @GeneraImpuestoBancario char(1) = NULL," & vbCrLf
   vResult = vResult & "   @CodigoConceptoBancario varchar(8) = NULL," & vbCrLf
   vResult = vResult & "   @NumeroDocumento varchar(15) = NULL," & vbCrLf
   vResult = vResult & "   @Observaciones varchar(100) = NULL," & vbCrLf
   vResult = vResult & "   @NombreOperador varchar(10) = NULL," & vbCrLf
   vResult = vResult & "   @FechaUltimaModificacion smalldatetime = NULL" & vbCrLf
   fSpParametersUPD = vResult
End Function

Private Function fStatementsSpUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoBeneficiario " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_FechaApertura " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Ori_FechaCierre " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Status " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TotalAdelantos " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TotalCxP " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_GeneraReembolso " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoCuentaBancaria " & gUtilSQL.getVarCharTypeForDB(5) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_GeneraImpuestoBancario " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoConceptoBancario " & gUtilSQL.getVarCharTypeForDB(8) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumeroDocumento " & gUtilSQL.getVarCharTypeForDB(15) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_Observaciones " & gUtilSQL.getVarCharTypeForDB(100) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TimeStamp timestamp" & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoBeneficiario " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Up_FechaApertura " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Up_FechaCierre " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   DECLARE @Up_Status " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_TotalAdelantos " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_TotalCxP " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_GeneraReembolso " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoCuentaBancaria " & gUtilSQL.getVarCharTypeForDB(5) & vbCrLf
   vResult = vResult & "   DECLARE @Up_GeneraImpuestoBancario " & gUtilSQL.getVarCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoConceptoBancario " & gUtilSQL.getVarCharTypeForDB(8) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumeroDocumento " & gUtilSQL.getVarCharTypeForDB(15) & vbCrLf
   vResult = vResult & "   DECLARE @Up_Observaciones " & gUtilSQL.getVarCharTypeForDB(100) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Up_FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   BEGIN TRAN" & vbCrLf
   vResult = vResult & "   SELECT @Ori_CodigoBeneficiario = CodigoBeneficiario," & vbCrLf
   vResult = vResult & "          @Ori_FechaApertura = FechaApertura," & vbCrLf
   vResult = vResult & "          @Ori_FechaCierre = FechaCierre," & vbCrLf
   vResult = vResult & "          @Ori_Status = Status," & vbCrLf
   vResult = vResult & "          @Ori_TotalAdelantos = TotalAdelantos," & vbCrLf
   vResult = vResult & "          @Ori_TotalCxP = TotalCxP," & vbCrLf
   vResult = vResult & "          @Ori_GeneraReembolso = GeneraReembolso," & vbCrLf
   vResult = vResult & "          @Ori_CodigoCuentaBancaria = CodigoCuentaBancaria," & vbCrLf
   vResult = vResult & "          @Ori_GeneraImpuestoBancario = GeneraImpuestoBancario," & vbCrLf
   vResult = vResult & "          @Ori_CodigoConceptoBancario = CodigoConceptoBancario," & vbCrLf
   vResult = vResult & "          @Ori_NumeroDocumento = NumeroDocumento," & vbCrLf
   vResult = vResult & "          @Ori_Observaciones = Observaciones," & vbCrLf
   vResult = vResult & "          @Ori_NombreOperador = NombreOperador," & vbCrLf
   vResult = vResult & "          @Ori_FechaUltimaModificacion = FechaUltimaModificacion," & vbCrLf
   vResult = vResult & "          @Ori_TimeStamp = fldTimeStamp" & vbCrLf
   vResult = vResult & "      FROM Rendicion" & vbCrLf
   vResult = vResult & "      WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "      AND Consecutivo = @Consecutivo" & vbCrLf
   vResult = vResult & "   IF @CodigoBeneficiario is null" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoBeneficiario = @Ori_CodigoBeneficiario" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoBeneficiario = @CodigoBeneficiario" & vbCrLf
   vResult = vResult & "   IF @FechaApertura is null" & vbCrLf
   vResult = vResult & "      SET @Up_FechaApertura = @Ori_FechaApertura" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_FechaApertura = @FechaApertura" & vbCrLf
   vResult = vResult & "   IF @FechaCierre is null" & vbCrLf
   vResult = vResult & "      SET @Up_FechaCierre = @Ori_FechaCierre" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_FechaCierre = @FechaCierre" & vbCrLf
   vResult = vResult & "   IF @Status is null" & vbCrLf
   vResult = vResult & "      SET @Up_Status = @Ori_Status" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Status = @Status" & vbCrLf
   vResult = vResult & "   IF @TotalAdelantos is null" & vbCrLf
   vResult = vResult & "      SET @Up_TotalAdelantos = @Ori_TotalAdelantos" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TotalAdelantos = @TotalAdelantos" & vbCrLf
   vResult = vResult & "   IF @TotalCxP is null" & vbCrLf
   vResult = vResult & "      SET @Up_TotalCxP = @Ori_TotalCxP" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_TotalCxP = @TotalCxP" & vbCrLf
   vResult = vResult & "   IF @GeneraReembolso is null" & vbCrLf
   vResult = vResult & "      SET @Up_GeneraReembolso = @Ori_GeneraReembolso" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_GeneraReembolso = @GeneraReembolso" & vbCrLf
   vResult = vResult & "   IF @CodigoCuentaBancaria is null" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoCuentaBancaria = @Ori_CodigoCuentaBancaria" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoCuentaBancaria = @CodigoCuentaBancaria" & vbCrLf
   vResult = vResult & "   IF @GeneraImpuestoBancario is null" & vbCrLf
   vResult = vResult & "      SET @Up_GeneraImpuestoBancario = @Ori_GeneraImpuestoBancario" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_GeneraImpuestoBancario = @GeneraImpuestoBancario" & vbCrLf
   vResult = vResult & "   IF @CodigoConceptoBancario is null" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoConceptoBancario = @Ori_CodigoConceptoBancario" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_CodigoConceptoBancario = @CodigoConceptoBancario" & vbCrLf
   vResult = vResult & "   IF @NumeroDocumento is null" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroDocumento = @Ori_NumeroDocumento" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NumeroDocumento = @NumeroDocumento" & vbCrLf
   vResult = vResult & "   IF @Observaciones is null" & vbCrLf
   vResult = vResult & "      SET @Up_Observaciones = @Ori_Observaciones" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_Observaciones = @Observaciones" & vbCrLf
   vResult = vResult & "   IF @NombreOperador is null" & vbCrLf
   vResult = vResult & "      SET @Up_NombreOperador = @Ori_NombreOperador" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_NombreOperador = @NombreOperador" & vbCrLf
   vResult = vResult & "   IF @FechaUltimaModificacion is null" & vbCrLf
   vResult = vResult & "      SET @Up_FechaUltimaModificacion = @Ori_FechaUltimaModificacion" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Up_FechaUltimaModificacion = @FechaUltimaModificacion" & vbCrLf
   vResult = vResult & "   UPDATE Rendicion" & vbCrLf
   vResult = vResult & "      SET CodigoBeneficiario = @Up_CodigoBeneficiario," & vbCrLf
   vResult = vResult & "         FechaApertura = @Up_FechaApertura," & vbCrLf
   vResult = vResult & "         FechaCierre = @Up_FechaCierre," & vbCrLf
   vResult = vResult & "         Status = @Up_Status," & vbCrLf
   vResult = vResult & "         TotalAdelantos = @Up_TotalAdelantos," & vbCrLf
   vResult = vResult & "         TotalCxP = @Up_TotalCxP," & vbCrLf
   vResult = vResult & "         GeneraReembolso = @Up_GeneraReembolso," & vbCrLf
   vResult = vResult & "         CodigoCuentaBancaria = @Up_CodigoCuentaBancaria," & vbCrLf
   vResult = vResult & "         GeneraImpuestoBancario = @Up_GeneraImpuestoBancario," & vbCrLf
   vResult = vResult & "         CodigoConceptoBancario = @Up_CodigoConceptoBancario," & vbCrLf
   vResult = vResult & "         NumeroDocumento = @Up_NumeroDocumento," & vbCrLf
   vResult = vResult & "         Observaciones = @Up_Observaciones," & vbCrLf
   vResult = vResult & "         NombreOperador = @Up_NombreOperador," & vbCrLf
   vResult = vResult & "         FechaUltimaModificacion = @Up_FechaUltimaModificacion" & vbCrLf
   vResult = vResult & "      WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "        AND Consecutivo = @Consecutivo" & vbCrLf
   vResult = vResult & "         AND fldTimeStamp = @Ori_TimeStamp" & vbCrLf
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
   GetSpNameDel = "RendicionDEL"
End Function

Private Function fSpParametersDel() As String
   Dim vResult As String
   vResult = "   @ConsecutivoCompania int," & vbCrLf
   vResult = vResult & "   @Consecutivo int"
   fSpParametersDel = vResult
End Function

Private Function fStatementsSpDel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   BEGIN TRAN" & vbCrLf
   vResult = vResult & "   DELETE FROM Rendicion" & vbCrLf
   vResult = vResult & "      WHERE ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "      AND Consecutivo = @Consecutivo" & vbCrLf
   vResult = vResult & "   IF @@ERROR = 0" & vbCrLf
   vResult = vResult & "      COMMIT TRAN" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      ROLLBACK" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSpDel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpDEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'=================================================================================================
'      Stored Procedure para la inseción de Rendición desde S10
'=================================================================================================
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
   GetSpNameS10INS = "S10_RendicionINS"
End Function

Private Function fSpParametersS10INS() As String
   Dim vResult As String
   vResult = "   @P_ConsecutivoCompania  int," & vbCrLf
   vResult = vResult & "   @P_CodigoBeneficiario varchar(10)," & vbCrLf
   vResult = vResult & "   @P_FechaApertura smalldatetime," & vbCrLf
   vResult = vResult & "   @P_Observaciones varchar(100) = ''," & vbCrLf
   vResult = vResult & "   @ConsecutivoRendicion int OUTPUT" & vbCrLf
   fSpParametersS10INS = vResult
End Function

Private Function fStatementsSpS10INS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "   BEGIN"
   vResult = vResult & "   DECLARE @NextConsecutivo int"
   vResult = vResult & "   DECLARE @Operador  varchar(10)"
   vResult = vResult & "   DECLARE @StatusRen char(1)"
   vResult = vResult & "   DECLARE @TotAdelantos money"
   vResult = vResult & "   DECLARE @TotCxP money"
   vResult = vResult & "   DECLARE @GenerarReembolso char(1)"
   vResult = vResult & "   DECLARE @CodCtaBancaria varchar(5)"
   vResult = vResult & "   DECLARE @CodCptoBancario varchar(8)"
   vResult = vResult & "   DECLARE @NumDoc varchar(15)"
   vResult = vResult & "   DECLARE @Hoy smalldatetime"
   vResult = vResult & "   SET NOCOUNT ON;"
   vResult = vResult & "   IF NOT EXISTS(SELECT ConsecutivoCompania FROM COMPANIA WHERE ConsecutivoCompania = @P_ConsecutivoCompania)"
   vResult = vResult & "         BEGIN"
   vResult = vResult & "            SET @ConsecutivoRendicion = -1"
   vResult = vResult & "            RAISERROR ('No existe registro de Compañía para el Consecutivo dado%d.', 16, 1, @P_ConsecutivoCompania)"
   vResult = vResult & "         End"
   vResult = vResult & "      Else"
   vResult = vResult & "      BEGIN"
   vResult = vResult & "         IF NOT EXISTS(SELECT CodigoProveedor FROM Proveedor WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND CodigoProveedor = @P_CodigoBeneficiario)"
   vResult = vResult & "         BEGIN"
   vResult = vResult & "            SET @ConsecutivoRendicion = -1"
   vResult = vResult & "            RAISERROR ('No existe registro de Beneficiario para el Código dado%s.', 16, 1, @P_CodigoBeneficiario)"
   vResult = vResult & "         End"
   vResult = vResult & "         Else"
   vResult = vResult & "         BEGIN"
   vResult = vResult & "            BEGIN TRAN"
   vResult = vResult & "            SET @Operador = dbo.Gf_UsuarioS10()"
   vResult = vResult & "            SET @StatusRen = N'0'"
   vResult = vResult & "            SET @TotAdelantos = 0"
   vResult = vResult & "         SET @TotCxP = 0"
   vResult = vResult & "         SET @GenerarReembolso = N'N'"
   vResult = vResult & "         SET @CodCtaBancaria = ''"
   vResult = vResult & "         SET @CodCptoBancario = ''"
   vResult = vResult & "         SET @NumDoc = ''"
   vResult = vResult & "         SET @Hoy = CONVERT(smalldatetime,  GETDATE()  ,103)"
   vResult = vResult & "         SELECT @NextConsecutivo = Max(Consecutivo)  FROM Rendicion WHERE ConsecutivoCompania = @P_ConsecutivoCompania"
   vResult = vResult & "         IF @NextConsecutivo IS NULL"
   vResult = vResult & "            SET @NextConsecutivo = 1"
   vResult = vResult & "         Else"
   vResult = vResult & "         BEGIN"
   vResult = vResult & "            IF @NextConsecutivo > 0"
   vResult = vResult & "               SET @NextConsecutivo = @NextConsecutivo + 1"
   vResult = vResult & "            Else"
   vResult = vResult & "               SET @NextConsecutivo = 1"
   vResult = vResult & "         End"
   vResult = vResult & "         EXEC Gp_RendicionINS"
   vResult = vResult & "            @ConsecutivoCompania = @P_ConsecutivoCompania,"
   vResult = vResult & "            @Consecutivo = @NextConsecutivo,"
   vResult = vResult & "            @CodigoBeneficiario = @P_CodigoBeneficiario,"
   vResult = vResult & "            @FechaApertura = @P_FechaApertura,"
   vResult = vResult & "            @FechaCierre = @Hoy,"
   vResult = vResult & "            @Status = @StatusRen,"
   vResult = vResult & "            @TotalAdelantos = @TotAdelantos,"
   vResult = vResult & "            @TotalCxP = @TotCxP,"
   vResult = vResult & "            @GeneraReembolso = @GenerarReembolso,"
   vResult = vResult & "            @CodigoCuentaBancaria = @CodCtaBancaria,"
   vResult = vResult & "            @CodigoConceptoBancario = @CodCptoBancario,"
   vResult = vResult & "            @NumeroDocumento = @NumDoc,"
   vResult = vResult & "            @Observaciones = @P_Observaciones,"
   vResult = vResult & "            @NombreOperador = @Operador"
   vResult = vResult & "         SET @ConsecutivoRendicion = @NextConsecutivo"
   vResult = vResult & "         IF @@ERROR > 0"
   vResult = vResult & "         BEGIN"
   vResult = vResult & "            ROLLBACK TRAN"
   vResult = vResult & "            SET @ConsecutivoRendicion = -1"
   vResult = vResult & "            RAISERROR ('Se Generó un error por Concurrencia de Datos',16,1)"
   vResult = vResult & "         End"
   vResult = vResult & "         Else"
   vResult = vResult & "            COMMIT TRAN"
   vResult = vResult & "         End"
   vResult = vResult & "      End"
   vResult = vResult & "   End"
   fStatementsSpS10INS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10INS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
Private Function fCreateSpS10RendiconesSaldoSel(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10RendiconesSaldoSel(), fSpParametersS10RendiconesSaldoSel(), fStatementsSpS10RendiconesSaldoSel(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10RendiconesSaldoSel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10RendiconesSaldoSel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10RendiconesSaldoSel(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10RendiconesSaldoSel(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10RendiconesSaldoSel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10RendiconesSaldoSel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10RendiconesSaldoSel() As String
   GetSpNameS10RendiconesSaldoSel = "S10_RendiconesSaldoSel"
End Function

Private Function fSpParametersS10RendiconesSaldoSel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "   @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "   @P_ConsecutivoRendicion " & gUtilSQL.getNumericTypeForDB(9, 0) & " = Null," & vbCrLf
   vResult = vResult & "   @P_CodigoBeneficiario " & gUtilSQL.getVarCharTypeForDB(10) & " = Null" & vbCrLf
   fSpParametersS10RendiconesSaldoSel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10RendiconesSaldoSel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10RendiconesSaldoSel() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   SET NOCOUNT ON;" & vbCrLf
   vResult = vResult & "   DECLARE @Sql  " & gUtilSQL.getVarCharTypeForDB(6000) & "" & vbCrLf
   vResult = vResult & "   DECLARE @SqlWhere Varchar(1000)" & vbCrLf
   vResult = vResult & "   DECLARE @CodigoBeneficiario Varchar(10)" & vbCrLf
   vResult = vResult & "   SET @SqlWhere = ''" & vbCrLf
   vResult = vResult & "   IF Not (@P_ConsecutivoRendicion Is Null)" & vbCrLf
   vResult = vResult & "      SET  @SqlWhere = ' AND Consecutivo = ' + Cast(@P_ConsecutivoRendicion as varchar)" & vbCrLf
   vResult = vResult & "   IF Not (@P_CodigoBeneficiario is Null)" & vbCrLf
   vResult = vResult & "      SET  @SqlWhere = @SqlWhere + ' AND CodigoBeneficiario = ' + @P_CodigoBeneficiario" & vbCrLf
   vResult = vResult & "   SET @Sql = 'SELECT     Consecutivo, CodigoBeneficiario," & vbCrLf
   vResult = vResult & " FechaApertura, TotalAnticipos" & vbCrLf
   vResult = vResult & "         FROM         IGV_SaldoRencionesAbiertas" & vbCrLf
   vResult = vResult & "         WHERE  ConsecutivoCompania = ' + CAST(@ConsecutivoCompania as varchar) + @SqlWhere" & vbCrLf
   vResult = vResult & "   EXEC(@Sql)" & vbCrLf
   vResult = vResult & "   RETURN @@ROWCOUNT" & vbCrLf
   vResult = vResult & "END "
   fStatementsSpS10RendiconesSaldoSel = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10RendiconesSaldoSel", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

Private Function fCreateSpS10RendicionCxPUPD(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10RendicionCxPUPD(), fSpParametersS10RendicionCxPUPD(), fStatementsSpS10RendicionCxPUPD(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10RendicionCxPUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10RendicionCxPUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropS10RendicionCxPUPD(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10RendicionCxPUPD(), valShowMessage)
   Set insLibSp = Nothing
   fDropS10RendicionCxPUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropS10RendicionCxPUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10RendicionCxPUPD() As String
   GetSpNameS10RendicionCxPUPD = "S10_Rendicion_CxPUPD"
End Function

Private Function fSpParametersS10RendicionCxPUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "          @P_ConsecutivoCompania  " & gUtilSQL.getNumericTypeForDB(9, 0) & " ," & vbCrLf
   vResult = vResult & "         @P_ConsecutivoRendicion  " & gUtilSQL.getVarCharTypeForDB(11) & " ," & vbCrLf
   vResult = vResult & "         @P_CodigoTipoDeDocumentoCxp " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "         @P_Numero  " & gUtilSQL.getVarCharTypeForDB(20) & " ," & vbCrLf
   vResult = vResult & "         @P_CodigoProveedor  " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   fSpParametersS10RendicionCxPUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10RendicionCxPUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10RendicionCxPUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Operador  " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoCxP " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @TipoDeCxP char(1)" & vbCrLf
   vResult = vResult & "   IF NOT EXISTS(SELECT ConsecutivoCompania FROM COMPANIA WHERE ConsecutivoCompania = @P_ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @ConsecutivoCxP = -1" & vbCrLf
   vResult = vResult & "      RAISERROR ('No existe registro de Compañía para el Consecutivo dado%d.', 16, 1, @P_ConsecutivoCompania)" & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @Operador = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT CodigoProveedor FROM Proveedor WHERE ConsecutivoCompania = @P_ConsecutivoCompania AND CodigoProveedor = @P_CodigoProveedor)" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SET @ConsecutivoCxP = -1" & vbCrLf
   vResult = vResult & "         RAISERROR ('No existe registro de Proveedor para el Código dado%s.', 16, 1, @P_CodigoProveedor)" & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "      ELSE" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         SELECT @TipoDeCxP = TipoDeDocumento FROM TipoDeDocumentoLey WHERE (Codigo = @P_CodigoTipoDeDocumentoCxp)" & vbCrLf
   vResult = vResult & "         IF  NOT EXISTS(SELECT  ConsecutivoCxP FROM CxP WHERE  ConsecutivoCompania = @P_ConsecutivoCompania AND Numero = @P_Numero AND TipoDeCxp = @TipoDeCxP AND CodigoProveedor = @P_CodigoProveedor)" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SET @ConsecutivoCxP = -1" & vbCrLf
   vResult = vResult & "            RAISERROR('No existe un registro para los datos suministrados para Cuenta por Pagar.',16,1)" & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "         ELSE" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "            SELECT  @ConsecutivoCxP = ConsecutivoCxP FROM CxP WHERE  ConsecutivoCompania = @P_ConsecutivoCompania AND Numero = @P_Numero AND TipoDeCxp = @TipoDeCxP AND CodigoProveedor = @P_CodigoProveedor" & vbCrLf
   vResult = vResult & "            IF NOT EXISTS(SELECT Consecutivo FROM Rendicion WHERE  ConsecutivoCompania = @P_ConsecutivoCompania AND Consecutivo = @P_ConsecutivoRendicion)" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               SET @ConsecutivoCxP = -1" & vbCrLf
   vResult = vResult & "               RAISERROR('No existe un registro con este Consecutivo de Rendición.',16,1)" & vbCrLf
   vResult = vResult & "            END " & vbCrLf
   vResult = vResult & "            ELSE" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "               EXEC Gp_CxPUPD" & vbCrLf
   vResult = vResult & "                  @ConsecutivoCompania = @P_ConsecutivoCompania," & vbCrLf
   vResult = vResult & "                  @ConsecutivoCxP = @ConsecutivoCxP," & vbCrLf
   vResult = vResult & "                  @Numero = @P_Numero," & vbCrLf
   vResult = vResult & "                  @CodigoProveedor = @P_CodigoProveedor," & vbCrLf
   vResult = vResult & "                  @EstaAsociadoARendicion = 'S'," & vbCrLf
   vResult = vResult & "                  @ConsecutivoRendicion = @P_ConsecutivoRendicion," & vbCrLf
   vResult = vResult & "                  @NombreOperador = @Operador" & vbCrLf
   vResult = vResult & "            END " & vbCrLf
   vResult = vResult & "         END " & vbCrLf
   vResult = vResult & "      END " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "END "
   fStatementsSpS10RendicionCxPUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10RendicionCxPUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
