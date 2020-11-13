VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsMesGeneradoNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsMesGeneradoNavigator"
Private Const CM_MESSAGE_NAME As String = "Mes Generado"
Private mrsMesGenerado As ADODB.Recordset
Private mConsecutivoCompania As Currency
Private mNumeroContrato As String
Private mFechaDeGeneracion As Date
Private mFechaDeGeneracionIsSet As Boolean
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function fPrimaryKeyExistMessage() As String
   fPrimaryKeyExistMessage = "Ya existe ConsecutivoCompania,NumeroContrato,FechaDeGeneracion"
End Function

Private Sub sClrRecord()
   On Error GoTo h_ERROR
   SetConsecutivoCompania gProyCompaniaActual.GetConsecutivoCompania
   SetNumeroContrato ""
   SetFechaDeGeneracion gUtilDate.getFechaDeHoy
   mFechaDeGeneracionIsSet = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sClrRecord", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function GetConsecutivoCompania() As Currency
   GetConsecutivoCompania = mConsecutivoCompania
End Function

Private Sub SetConsecutivoCompania(ByVal valNewValue As Currency)
   mConsecutivoCompania = valNewValue
End Sub

Private Function GetNumeroContrato() As String
   GetNumeroContrato = mNumeroContrato
End Function

Private Sub SetNumeroContrato(ByVal valNewValue As String)
   mNumeroContrato = valNewValue
End Sub

Private Function GetFechaDeGeneracion() As Date
   GetFechaDeGeneracion = mFechaDeGeneracion
End Function

Private Sub SetFechaDeGeneracion(ByVal valNewValue As Date)
   mFechaDeGeneracion = valNewValue
   mFechaDeGeneracionIsSet = True
End Sub

Public Function GetTableName() As String
   GetTableName = "mesGenerado"
End Function

Private Sub setRSWithValues()
   On Error GoTo h_ERROR
   If gDbUtil.FieldInRecordSet(mrsMesGenerado, "ConsecutivoCompania") Then
      mrsMesGenerado("ConsecutivoCompania") = GetConsecutivoCompania()
   End If
   If gDbUtil.FieldInRecordSet(mrsMesGenerado, "NumeroContrato") Then
      mrsMesGenerado("NumeroContrato") = GetNumeroContrato()
   End If
   If gDbUtil.FieldInRecordSet(mrsMesGenerado, "FechaDeGeneracion") Then
      mrsMesGenerado("FechaDeGeneracion") = GetFechaDeGeneracion()
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "setRSWithValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub setValuesWithRS(ByVal valRecordSet As ADODB.Recordset)
   On Error GoTo h_ERROR
   If gDbUtil.FieldInRecordSet(valRecordSet, "ConsecutivoCompania") Then
      If Not IsNull(valRecordSet("ConsecutivoCompania")) Then
         SetConsecutivoCompania valRecordSet("ConsecutivoCompania")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "NumeroContrato") Then
      If Not IsNull(valRecordSet("NumeroContrato")) Then
         SetNumeroContrato valRecordSet("NumeroContrato")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "FechaDeGeneracion") Then
      If Not IsNull(valRecordSet("FechaDeGeneracion")) Then
         SetFechaDeGeneracion valRecordSet("FechaDeGeneracion")
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "setValuesWithRS", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function getSQLSortOrder() As String
   getSQLSortOrder = " ORDER BY " & GetTableName() & ".ConsecutivoCompania" & ", " & GetTableName() & ".NumeroContrato" & ", " & GetTableName() & ".FechaDeGeneracion"
End Function

Private Function fRsOpened() As Boolean
   On Error GoTo h_ERROR
   fRsOpened = False
   If Not (mrsMesGenerado Is Nothing) Then
      If mrsMesGenerado.State = adStateOpen Then
         fRsOpened = True
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fRsOpened = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fRsOpened", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sCloseIfOpened()
   On Error GoTo h_ERROR
   If fRsOpened Then
      mrsMesGenerado.Close
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCloseIfOpened", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInitRS(ByVal valAction As AccionSobreRecord)
   On Error GoTo h_ERROR
   gDbUtil.sCloseIfOpened mrsMesGenerado
   Set mrsMesGenerado = New ADODB.Recordset
   If (valAction = insertar) Then
      If gDbUtil.fOpenRecordSetAllParameters(mrsMesGenerado, "SELECT * FROM " & GetTableName & " WHERE ConsecutivoCompania=-1", gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitRS", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fRsRecordCount() As Long
   On Error GoTo h_ERROR
   If fRsOpened Then
      fRsRecordCount = mrsMesGenerado.RecordCount
   Else
      fRsRecordCount = 0
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fRsRecordCount = 0
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fRsRecordCount", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetRsMesGenerado() As ADODB.Recordset
   On Error GoTo h_ERROR
   If fRsOpened Then
      Set GetRsMesGenerado = mrsMesGenerado
   Else
      Set GetRsMesGenerado = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "GetRsMesGenerado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub Class_Initialize()
   On Error GoTo h_ERROR
   sClrRecord
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Class_Initialize", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Class_Terminate()
   On Error GoTo h_ERROR
   If Not (mrsMesGenerado Is Nothing) Then
      sCloseIfOpened
      Set mrsMesGenerado = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Class_Terminate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fInsert() As Boolean
   On Error GoTo h_ERROR
   fInsert = False
   If mrsMesGenerado Is Nothing Then
      sInitRS insertar
   End If
   mrsMesGenerado.AddNew
   setRSWithValues
   mrsMesGenerado.Update
   fInsert = True
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: mrsMesGenerado.CancelUpdate
   If gErrorCodesIG.isErrorPrimaryKeyExist(Err.Number, Err.Description, Err.HelpContext) Then
      gMessage.Advertencia (fPrimaryKeyExistMessage)
      On Error GoTo 0
   Else
      Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fInsert", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
   End If
End Function
Private Function fSQLSearchDetail(ByVal valNumeroContrato As String) As String
   Dim sql As String
   On Error GoTo h_ERROR
   sql = ""
   sql = gUtilSQL.fSQLNumberValueWithAnd(sql, "ConsecutivoCompania", gProyCompaniaActual.GetConsecutivoCompania)
   SQL = gUtilSQL.fSQLValueWithAnd(SQL, "NumeroContrato", valNumeroContrato, False)
   fSQLSearchDetail = "SELECT " & "FechaDeGeneracion, " & "ConsecutivoCompania, " & "NumeroContrato" & " FROM " & GetTableName & " " & gUtilSQL.getWhereSQL(sql) & getSQLSortOrder()
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLSearchDetail = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLSearchDetail", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function HayAlMenosUnMesGenerado(ByVal valNumeroContrato As String) As Boolean
   Dim HayAlMenosUno As Boolean
   On Error GoTo h_ERROR
   HayAlMenosUno = False
   sInitRS Buscar
   If gDbUtil.fOpenRecordSetAllParameters(mrsMesGenerado, fSQLSearchDetail(valNumeroContrato), gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic, , adCriteriaTimeStamp) Then
      If fRsRecordCount > 0 Then
          HayAlMenosUno = True
      End If
   End If
   HayAlMenosUnMesGenerado = HayAlMenosUno
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "HayAlMenosUnMesGenerado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fAddNewMesGenerado(ByVal valNumeroContrato As String, ByVal valFechaDeGeneracion As Date) As Boolean
   On Error GoTo h_ERROR
   sClrRecord
   valFechaDeGeneracion = gUtilDate.fColocaUltimoDiaDelMes(valFechaDeGeneracion)
   SetConsecutivoCompania gProyCompaniaActual.GetConsecutivoCompania
   SetNumeroContrato valNumeroContrato
   SetFechaDeGeneracion valFechaDeGeneracion
   If fInsert Then
      fAddNewMesGenerado = True
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fAddNewMesGenerado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sAddDirectoDesdeRecordsetExterno(ByVal valConsecutivoCompaniaNew As Long, ByVal valRecordSet As ADODB.Recordset)
   On Error GoTo h_ERROR
   sClrRecord
   setValuesWithRS valRecordSet
   If valConsecutivoCompaniaNew > 0 Then
      SetConsecutivoCompania valConsecutivoCompaniaNew
   End If
   If mrsMesGenerado Is Nothing Then
      sInitRS insertar
   End If
   mrsMesGenerado.AddNew
   setRSWithValues
   mrsMesGenerado.Update
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: If gUtilRespaldo.fIgnorarElErrorDeRestauracion(Err.Number, Err.HelpContext, Err.Source, Err.Description, valRecordSet) Then
      mrsMesGenerado.CancelUpdate
      GoTo h_EXIT
   Else
      Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddDirectoDesdeRecordsetExterno", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
   End If
End Sub
Public Function fSiExistenBuscaLosMesesGenerados(ByVal valNumeroContrato As String, ByVal valConMensaje As Boolean) As Boolean
   Dim HayAlMenosUno As Boolean
   On Error GoTo h_ERROR
   HayAlMenosUno = False
   sInitRS Buscar
   If gDbUtil.fOpenRecordSetAllParameters(mrsMesGenerado, fSQLSearchDetail(valNumeroContrato), gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic, , adCriteriaTimeStamp) Then
      If fRsRecordCount > 0 Then
         HayAlMenosUno = True
      ElseIf valConMensaje Then
         gMessage.Advertencia "No han sido generadas facturas para el Contrato No." & valNumeroContrato
      End If
   End If
h_EXIT:
   fSiExistenBuscaLosMesesGenerados = HayAlMenosUno
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSiExistenBuscaLosMesesGenerados", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

