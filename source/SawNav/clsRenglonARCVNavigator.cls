VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsRenglonARCVNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private mrsRenglonARCV As ADODB.Recordset
Private mConsecutivoCompania As Long
Private mNumeroARCV As String
Private mConsecutivoRenglon As Long
Private mMesParaARCV As String
Private mPagadoAbonado As Currency
Private mBaseImponible As Currency
Private mImpuestoRetenido As Currency
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsRenglonARCVNavigator"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "RenglonARCV"
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Function fPrimaryKeyExistMessage() As String
   fPrimaryKeyExistMessage = "Ya existe ConsecutivoCompania,NumeroARCV,ConsecutivoRenglon"
End Function
Private Sub sClrRecord()
   On Error GoTo h_ERROR
   SetConsecutivoCompania gProyCompaniaActual.GetConsecutivoCompania
   SetNumeroARCV ""
   SetConsecutivoRenglon 0
   SetMesParaARCVStr ""
   SetPagadoAbonado 0
   SetBaseImponible 0
   SetImpuestoRetenido 0
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sClrRecord", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Function GetConsecutivoCompania() As Long
   GetConsecutivoCompania = mConsecutivoCompania
End Function
Private Sub SetConsecutivoCompania(ByVal valNewValue As Long)
   mConsecutivoCompania = valNewValue
End Sub
Private Function GetNumeroARCV() As String
   GetNumeroARCV = mNumeroARCV
End Function
Private Sub SetNumeroARCV(ByVal valNewValue As String)
   mNumeroARCV = gTexto.DfMid(valNewValue, 1, 11)
End Sub
Private Function GetConsecutivoRenglon() As Long
   GetConsecutivoRenglon = mConsecutivoRenglon
End Function
Private Sub SetConsecutivoRenglon(ByVal valNewValue As Long)
   mConsecutivoRenglon = valNewValue
End Sub
Private Function GetMesParaARCVStr() As String
   GetMesParaARCVStr = mMesParaARCV
End Function
Private Function GetMesParaARCVChar() As String
   If GetMesParaARCVStr = "" Then
      GetMesParaARCVChar = ""
   Else
      GetMesParaARCVChar = gConvert.enumerativoAChar(gEnumReport.strMesToNum(GetMesParaARCVStr, False))
   End If
End Function
Public Function GetMesParaARCVAsEnum() As enum_Mes
   GetMesParaARCVAsEnum = gEnumReport.strMesToNum(GetMesParaARCVStr, False)
End Function
Private Sub SetMesParaARCVStr(ByVal valNewValue As String)
   mMesParaARCV = valNewValue
End Sub
Private Function GetPagadoAbonado() As Currency
   GetPagadoAbonado = mPagadoAbonado
End Function
Private Sub SetPagadoAbonado(ByVal valNewValue As Currency)
   mPagadoAbonado = gConvert.fDfTruncaANDecimales(valNewValue, 2)
End Sub
Private Function GetBaseImponible() As Currency
   GetBaseImponible = mBaseImponible
End Function
Private Sub SetBaseImponible(ByVal valNewValue As Currency)
   mBaseImponible = gConvert.fDfTruncaANDecimales(valNewValue, 2)
End Sub
Private Function GetImpuestoRetenido() As Currency
   GetImpuestoRetenido = mImpuestoRetenido
End Function
Private Sub SetImpuestoRetenido(ByVal valNewValue As Currency)
   mImpuestoRetenido = gConvert.fDfTruncaANDecimales(valNewValue, 2)
End Sub
Public Function GetTableName() As String
   GetTableName = "renglonARCV"
End Function
Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   
   On Error GoTo h_ERROR
   
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   gDbUtil.AddForeignKeySegunTablaPrimaria "ARCV", GetTableName(), "Numero", _
         "NumeroArcv", Not usarSoloUnElementoEnLaRelacion, _
         "ConsecutivoCompania", "ConsecutivoCompania", deleteOnCascade
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (" & "ConsecutivoCompania" & "," & "NumeroArcv" & "," & "ConsecutivoRenglon" & ")"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName) & " ( "
   SQL = SQL & "ConsecutivoCompania" & " " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "NumeroArcv" & " " & gUtilSQL.getVarCharTypeForDB(11) & " CONSTRAINT nn" & GetTableName() & "NumeroARCV NOT NULL, "
   SQL = SQL & "ConsecutivoRenglon" & " " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "MesParaArcv" & " " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "PagadoAbonado" & " " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "BaseImponible" & " " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "ImpuestoRetenido" & " " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & gDefDatabase.getFN_TIMESTAMP() & " timestamp )"
   gDbUtil.Execute gDefDatabase.Conexion, SQL
   sAddPrimaryKeyAndOtherConstrains
   If valShowMessage Then
      gMessage.exito "La Tabla '" & CM_MESSAGE_NAME & "' fue creada exitosamente"
   End If
   fCreateTable = True
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fCreateTable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Sub sDropTable()
   On Error GoTo h_ERROR
   gDbUtil.ExecuteDropTable GetTableName, True
   gMessage.ProgrammerMessage "La Tabla '" & CM_MESSAGE_NAME & "' fue borrada exitosamente"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sDropTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub setRSWithValues(ByVal valAction As AccionSobreRecord)
   On Error GoTo h_ERROR
   If valAction = insertar Then
      If gDbUtil.FieldInRecordSet(mrsRenglonARCV, "ConsecutivoCompania") Then
         mrsRenglonARCV("ConsecutivoCompania") = GetConsecutivoCompania()
      End If
      If gDbUtil.FieldInRecordSet(mrsRenglonARCV, "NumeroArcv") Then
         mrsRenglonARCV("NumeroArcv") = GetNumeroARCV()
      End If
      If gDbUtil.FieldInRecordSet(mrsRenglonARCV, "ConsecutivoRenglon") Then
         mrsRenglonARCV("ConsecutivoRenglon") = GetConsecutivoRenglon()
      End If
   End If
   If gDbUtil.FieldInRecordSet(mrsRenglonARCV, "MesParaArcv") Then
      mrsRenglonARCV("MesParaArcv") = GetMesParaARCVChar()
   End If
   If gDbUtil.FieldInRecordSet(mrsRenglonARCV, "PagadoAbonado") Then
      mrsRenglonARCV("PagadoAbonado") = GetPagadoAbonado()
   End If
   If gDbUtil.FieldInRecordSet(mrsRenglonARCV, "BaseImponible") Then
      mrsRenglonARCV("BaseImponible") = GetBaseImponible()
   End If
   If gDbUtil.FieldInRecordSet(mrsRenglonARCV, "ImpuestoRetenido") Then
      mrsRenglonARCV("ImpuestoRetenido") = GetImpuestoRetenido()
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "setRSWithValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub setValuesWithRS(Optional ByVal valRecordSet As ADODB.Recordset = Nothing)
   On Error GoTo h_ERROR
   If valRecordSet Is Nothing Then
      Set valRecordSet = mrsRenglonARCV
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ConsecutivoCompania") Then
      If Not IsNull(valRecordSet("ConsecutivoCompania")) Then
         SetConsecutivoCompania valRecordSet("ConsecutivoCompania")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "NumeroArcv") Then
      If Not IsNull(valRecordSet("NumeroArcv")) Then
         SetNumeroARCV valRecordSet("NumeroArcv")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ConsecutivoRenglon") Then
      If Not IsNull(valRecordSet("ConsecutivoRenglon")) Then
         SetConsecutivoRenglon valRecordSet("ConsecutivoRenglon")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "MesParaArcv") Then
      If Not IsNull(valRecordSet("MesParaArcv")) Then
         SetMesParaARCVStr gEnumReport.enumMesToString(gConvert.charAEnumerativoInt(valRecordSet("MesParaArcv")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "PagadoAbonado") Then
      If Not IsNull(valRecordSet("PagadoAbonado")) Then
         SetPagadoAbonado valRecordSet("PagadoAbonado")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "BaseImponible") Then
      If Not IsNull(valRecordSet("BaseImponible")) Then
         SetBaseImponible valRecordSet("BaseImponible")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ImpuestoRetenido") Then
      If Not IsNull(valRecordSet("ImpuestoRetenido")) Then
         SetImpuestoRetenido valRecordSet("ImpuestoRetenido")
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "setValuesWithRS", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Function getSQLSortOrder() As String
   getSQLSortOrder = " ORDER BY " & GetTableName() & "." & "ConsecutivoCompania" _
         & ", " & GetTableName() & "." & "NumeroArcv" _
         & ", " & GetTableName() & "." & "ConsecutivoRenglon"
End Function
Private Function fSQLSearch() As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = ""
   SQL = gUtilSQL.fSQLNumberValueWithAnd(SQL, GetTableName() & "." & "ConsecutivoCompania", GetConsecutivoCompania())
   SQL = gUtilSQL.fSQLValueWithAnd(SQL, GetTableName() & "." & "NumeroArcv", GetNumeroARCV(), False)
   SQL = gUtilSQL.fSQLNumberValueWithAnd(SQL, GetTableName() & "." & "ConsecutivoRenglon", GetConsecutivoRenglon())
   SQL = gUtilSQL.fSQLValueWithAnd(SQL, GetTableName() & "." & "MesParaArcv", GetMesParaARCVChar(), False)
   SQL = gUtilSQL.fSQLNumberValueWithAnd(SQL, GetTableName() & "." & "PagadoAbonado", GetPagadoAbonado())
   SQL = gUtilSQL.fSQLNumberValueWithAnd(SQL, GetTableName() & "." & "BaseImponible", GetBaseImponible())
   SQL = gUtilSQL.fSQLNumberValueWithAnd(SQL, GetTableName() & "." & "ImpuestoRetenido", GetImpuestoRetenido())
   fSQLSearch = "SELECT " & GetTableName & ".* FROM " & _
         GetTableName & gUtilSQL.getWhereSQL(SQL) & getSQLSortOrder()
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLSearch = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLSearch", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Sub sInitRS(ByVal valAction As AccionSobreRecord)
   On Error GoTo h_ERROR
   gDbUtil.sCloseIfOpened mrsRenglonARCV
   If mrsRenglonARCV Is Nothing Then
      Set mrsRenglonARCV = New ADODB.Recordset
   End If
   If (valAction = insertar) Then
      If gDbUtil.fOpenRecordSetAllParameters(mrsRenglonARCV, "SELECT * FROM " & GetTableName & " WHERE " & "ConsecutivoCompania" & "=-1", _
            gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitRS", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Sub sRsMoveRecord(ByVal valNumberOfPositions As Long, ByVal valAction As AccionSobreRecord, _
      ByRef refRenglonARCVRecord As ADODB.Recordset)
   On Error GoTo h_ERROR
   If valAction = Eliminar Then
      refRenglonARCVRecord.MoveFirst
   End If
   While valNumberOfPositions <> refRenglonARCVRecord.AbsolutePosition
      If valNumberOfPositions < refRenglonARCVRecord.AbsolutePosition Then
         refRenglonARCVRecord.MovePrevious
      Else
         refRenglonARCVRecord.MoveNext
      End If
   Wend
   setValuesWithRS
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRsMoveRecord", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub Class_Initialize()
   On Error GoTo h_ERROR
   sClrRecord
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Class_Initialize", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub Class_Terminate()
   On Error GoTo h_ERROR
   If Not (mrsRenglonARCV Is Nothing) Then
      gDbUtil.sCloseIfOpened mrsRenglonARCV
      Set mrsRenglonARCV = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Class_Terminate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function fSearchByField(ByVal fieldName As String, ByVal fieldValue As String, _
      Optional ByVal valChangeRecordSet As Boolean = True, Optional ByVal valShowMessageIfNotFound As Boolean = False) As Boolean
   Dim rsBeforeChanges As ADODB.Recordset
   On Error GoTo h_ERROR
   fSearchByField = False
   If Not valChangeRecordSet Then
      Set rsBeforeChanges = mrsRenglonARCV
      Set mrsRenglonARCV = New ADODB.Recordset
   End If
   sInitRS Buscar
   If gDbUtil.fOpenRecordSetAllParameters(mrsRenglonARCV, gProyCompaniaActual.getSQLSearchByFieldStringAndConsecutivo(GetTableName, fieldName, fieldValue), _
         gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If mrsRenglonARCV.RecordCount = 0 Then
         fSearchByField = False
         If valShowMessageIfNotFound Then
            gMessage.sNoExistenRegistrosConEstasCaracteristicas CM_MESSAGE_NAME, GetGender
         End If
      Else
         fSearchByField = True
         If valChangeRecordSet Then
            setValuesWithRS
         End If
      End If
   End If
   If Not valChangeRecordSet Then
      Set mrsRenglonARCV = Nothing
      Set mrsRenglonARCV = rsBeforeChanges
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSearchByField = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSearchByField", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSearchByNumericField(ByVal fieldName As String, ByVal fieldValue As String, _
      Optional ByVal valChangeRecordSet As Boolean = True, Optional ByVal valShowMessageIfNotFound As Boolean = False) As Boolean
   Dim rsBeforeChanges As ADODB.Recordset
   On Error GoTo h_ERROR
   fSearchByNumericField = False
   If Not valChangeRecordSet Then
      Set rsBeforeChanges = mrsRenglonARCV
      Set mrsRenglonARCV = New ADODB.Recordset
   End If
   sInitRS Buscar
   If gDbUtil.fOpenRecordSetAllParameters(mrsRenglonARCV, gProyCompaniaActual.getSQLSearchByFieldNumericAndConsecutivo(GetTableName, fieldName, fieldValue), _
         gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If mrsRenglonARCV.RecordCount = 0 Then
         fSearchByNumericField = False
         If valShowMessageIfNotFound Then
            gMessage.sNoExistenRegistrosConEstasCaracteristicas CM_MESSAGE_NAME, GetGender
         End If
      Else
         fSearchByNumericField = True
         If valChangeRecordSet Then
            setValuesWithRS
         End If
      End If
   End If
   If Not valChangeRecordSet Then
      Set mrsRenglonARCV = Nothing
      Set mrsRenglonARCV = rsBeforeChanges
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSearchByNumericField = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSearchByNumericField", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fOpenRenglonARCVForInsertDetail(ByRef refMrsDetailFromARCV As ADODB.Recordset) As Boolean
   Dim SQL As String
   On Error GoTo h_ERROR
   fOpenRenglonARCVForInsertDetail = False
   gDbUtil.sCloseIfOpened refMrsDetailFromARCV
   SQL = "SELECT " _
         & "MesParaArcv" & ", " _
         & "PagadoAbonado" & ", " _
         & "BaseImponible" & ", " _
         & "ImpuestoRetenido" & ", " _
         & "ConsecutivoCompania" & ", " _
         & "NumeroArcv" & ", " _
         & "ConsecutivoRenglon" _
         & " FROM " & GetTableName & " WHERE " _
         & "ConsecutivoRenglon" & " = -1" _
         & " AND " & "ConsecutivoCompania" & " = -1 "
   If gDbUtil.fOpenRecordSetAllParameters(refMrsDetailFromARCV, SQL, gDefDatabase.Conexion, adLockBatchOptimistic, adUseClient, adOpenStatic, adCmdText) Then
   End If
h_EXIT:
   fOpenRenglonARCVForInsertDetail = True
   On Error GoTo 0
   Exit Function
h_ERROR: fOpenRenglonARCVForInsertDetail = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fOpenRenglonARCVForInsertDetail", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fUpdateDetail(ByRef refMrsDetailFromARCV As ADODB.Recordset, ByRef refOtherUserUpdatedFirst As Boolean, ByVal valCmMessageName As String, ByVal valVieneDeEliminar As Boolean) As Boolean
   Dim ShowMessage As Boolean
   On Error GoTo h_ERROR
   fUpdateDetail = False
   ShowMessage = False
   If gDbUtil.fActualizarElRegistro(ShowMessage, refMrsDetailFromARCV, refOtherUserUpdatedFirst, valCmMessageName & ".<" & CM_MESSAGE_NAME & ">", GetGender) Then
      fUpdateDetail = True
   ElseIf refOtherUserUpdatedFirst Then
      fUpdateDetail = gDbUtil.fResincronizarRegistro(False, refMrsDetailFromARCV, CM_MESSAGE_NAME)
   End If
   If Not valVieneDeEliminar Then
      If Not refMrsDetailFromARCV.EOF Then
         If gDbUtil.fResincronizarRegistro(ShowMessage, refMrsDetailFromARCV, CM_MESSAGE_NAME) Then
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: If gErrorCodesIG.isErrorPrimaryKeyExist(Err.Number, Err.Description, Err.HelpContext) Then
      Err.Raise Err.Number, Err.Source, gError.fAddWarningToStackTrace(fPrimaryKeyExistMessage, CM_FILE_NAME, "fUpdateDetail", Err.Description)
   Else
      Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fUpdateDetail", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
   End If
End Function
Public Function fValidateDetail(ByVal valNumeroARCV As String, ByVal valConsecutivoCompania As Long, _
      ByRef refMrsDetailFromARCV As ADODB.Recordset, ByVal valVieneDeInsertar As Boolean, _
      ByVal valNumeroARCVOriginal As String, ByRef refInserarDetail) As Boolean
   Dim i As Long
   Dim LineaDelError As String
   Dim HayError As Boolean
   On Error GoTo h_ERROR
   fValidateDetail = False
   refInserarDetail = True
   If refMrsDetailFromARCV.RecordCount > 0 Then
      refMrsDetailFromARCV.MoveFirst
   Else
      refInserarDetail = False
   End If
   For i = 1 To refMrsDetailFromARCV.RecordCount
      refMrsDetailFromARCV("ConsecutivoCompania") = valConsecutivoCompania
      refMrsDetailFromARCV("ConsecutivoRenglon") = i
      If valVieneDeInsertar Then
         refMrsDetailFromARCV("NumeroArcv") = valNumeroARCV
      Else
         refMrsDetailFromARCV("NumeroArcv") = valNumeroARCVOriginal
      End If
      refMrsDetailFromARCV.MoveNext
   Next
   fValidateDetail = True
h_EXIT:
   If Not refInserarDetail Then
      fValidateDetail = True
   End If
   If refMrsDetailFromARCV.RecordCount > 0 Then
      refMrsDetailFromARCV.MoveFirst
   End If
   On Error GoTo 0
   Exit Function
h_ERROR: fValidateDetail = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fValidateDetail", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLSearchDetail(ByVal valNumeroARCV As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = ""
   SQL = gUtilSQL.fSQLNumberValueWithAnd(SQL, "ConsecutivoCompania", gProyCompaniaActual.GetConsecutivoCompania)
   SQL = gUtilSQL.fSQLValueWithAnd(SQL, "NumeroArcv", valNumeroARCV, False)
   fSQLSearchDetail = "SELECT " _
         & "MesParaArcv" & ", " _
         & "PagadoAbonado" & ", " _
         & "BaseImponible" & ", " _
         & "ImpuestoRetenido" & ", " _
         & "ConsecutivoCompania" & ", " _
         & "NumeroArcv" & ", " _
         & "ConsecutivoRenglon" _
         & " FROM " & GetTableName & " " & gUtilSQL.getWhereSQL(SQL) & getSQLSortOrder()
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLSearchDetail = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLSearchDetail", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Sub sAddDirectoDesdeRecordsetExterno(ByVal valConsecutivoCompaniaNew As Long, ByVal valRecordSet As ADODB.Recordset)
   On Error GoTo h_ERROR
   sClrRecord
   setValuesWithRS valRecordSet
   If valConsecutivoCompaniaNew > 0 Then
      SetConsecutivoCompania valConsecutivoCompaniaNew
   End If
   If mrsRenglonARCV Is Nothing Then
      sInitRS insertar
   End If
   mrsRenglonARCV.AddNew
   setRSWithValues insertar
   mrsRenglonARCV.Update
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: If gUtilRespaldo.fIgnorarElErrorDeRestauracion(Err.Number, Err.HelpContext, Err.Source, Err.Description, valRecordSet) Then
      mrsRenglonARCV.CancelUpdate
      GoTo h_EXIT
   Else
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddDirectoDesdeRecordsetExterno", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
   End If
End Sub
