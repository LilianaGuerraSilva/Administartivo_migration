VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsSectorDeNegocioNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private mrsSectorDeNegocio As ADODB.Recordset
Private mDescripcion As String

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsSectorDeNegocioNavigator"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Sector de Negocio"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function fPrimaryKeyExistMessage() As String
   fPrimaryKeyExistMessage = "Ya existe Descripcion"
End Function

Private Sub sClrRecord()
   On Error GoTo h_ERROR
   SetDescripcion ""
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sClrRecord", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function GetDescripcion() As String
   GetDescripcion = mDescripcion
End Function

Private Sub SetDescripcion(ByVal valNewValue As String)
   mDescripcion = gTexto.DfMid(valNewValue, 1, 20)
End Sub

Private Sub setRSWithValues(ByVal valAction As AccionSobreRecord, Optional ByRef refRecordset As ADODB.Recordset = Nothing)
   On Error GoTo h_ERROR
   If refRecordset Is Nothing Then
      Set refRecordset = mrsSectorDeNegocio
   End If
   If (valAction = insertar) Or (valAction = Modificar) Then
      If gDbUtil.FieldInRecordSet(refRecordset, "Descripcion") Then
         refRecordset("Descripcion") = GetDescripcion()
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "setRSWithValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub setValuesWithRS(Optional ByVal valRecordSet As ADODB.Recordset = Nothing)
   On Error GoTo h_ERROR
   If valRecordSet Is Nothing Then
      Set valRecordSet = mrsSectorDeNegocio
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "Descripcion") Then
      If Not IsNull(valRecordSet("Descripcion")) Then
         SetDescripcion valRecordSet("Descripcion")
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "setValuesWithRS", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   If Not (mrsSectorDeNegocio Is Nothing) Then
      gDbUtil.sCloseIfOpened mrsSectorDeNegocio
      Set mrsSectorDeNegocio = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Class_Terminate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fInsert() As Boolean
   On Error GoTo h_ERROR
   fInsert = False
   If mrsSectorDeNegocio Is Nothing Then
      gDbUtil.sInitRS mrsSectorDeNegocio, "Comun.SectorDeNegocio", True
   End If
   mrsSectorDeNegocio.AddNew
   setRSWithValues insertar
   fInsert = gDbUtil.fInsertarElRegistro(mrsSectorDeNegocio, CM_MESSAGE_NAME, fPrimaryKeyExistMessage)
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fInsert", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSearchByField(ByVal fieldName As String, _
      ByVal fieldValue As String, Optional ByVal valChangeRecordSet As Boolean = True, _
      Optional ByVal valShowMessageIfNotFound As Boolean = False) As Boolean
   Dim rsBeforeChanges As ADODB.Recordset
   On Error GoTo h_ERROR
   fSearchByField = False
   If Not valChangeRecordSet Then
      Set rsBeforeChanges = mrsSectorDeNegocio
      Set mrsSectorDeNegocio = New ADODB.Recordset
   End If
   gDbUtil.sInitRS mrsSectorDeNegocio, "Comun.Gv_SectorDeNegocio_B1", False
   If gDbUtil.fOpenRecordSetAllParameters(mrsSectorDeNegocio, gUtilSQL.fSQLSearchByFieldString("Comun.Gv_SectorDeNegocio_B1", fieldName, fieldValue), _
         gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If mrsSectorDeNegocio.RecordCount = 0 Then
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
      Set mrsSectorDeNegocio = Nothing
      Set mrsSectorDeNegocio = rsBeforeChanges
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSearchByField = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSearchByField", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sRestaurarDatos(ByVal valRecordSet As ADODB.Recordset)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   While Not valRecordSet.EOF
      If gDbUtil.fRecordCountOfSql(gUtilSQL.fSQLSearchByFieldString("Comun.Gv_SectorDeNegocio_B1", "Descripcion", valRecordSet("Descripcion").Value)) <= 0 Then
         Set vCmd = insUtAdoCmd.fCreateCmd("Comun.Gp_SectorDeNegocioINS", gDefDatabase.Conexion)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Descripcion", 20, valRecordSet("Descripcion").Value)
         insUtAdoCmd.fExecCmd vCmd
         Set vCmd = Nothing
      End If
      gUtilRespaldo.sAddProgress
      valRecordSet.MoveNext
   Wend
   Set insUtAdoCmd = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRestaurarDatos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
'REGLAS DE NEGOCIO
Public Function fDefaultValueForDescripcion() As String
   fDefaultValueForDescripcion = "No Asignado"
End Function
Public Function fEstableceSectorDeNegocio() As String
    Dim rsSectorNegocio As ADODB.Recordset
    Dim SQL  As String
    Dim sectorDeNegocio As String
    On Error GoTo h_ERROR
    'Me traje lo que tenian en importacion que parecia estar mejor
    fEstableceSectorDeNegocio = fPrimerSectorDeNegocio
'    Set rsSectorNegocio = New ADODB.Recordset
'    SQL = " SELECT " & gUtilSQL.fFirst("Comun.SectorDeNegocio.Descripcion", "SectorNegocio")
'    SQL = SQL & " FROM Comun.SectorDeNegocio "
'    If gDbUtil.fOpenRecordSetAllParameters(rsSectorNegocio, SQL, Conexion, adLockReadOnly, adUseClient, adOpenStatic) Then
'        If gDbUtil.fRecordCount(rsSectorNegocio) > 0 Then
'            sectorDeNegocio = rsSectorNegocio.Fields("SectorNegocio").value
'        Else
'            SetDescripcion "Peluqueria"
'            fInsert
'        End If
'    End If
'    gDbUtil.sDestroyRecordSet rsSectorNegocio
h_EXIT: On Error GoTo 0
'    fEstableceSectorDeNegocio = sectorDeNegocio
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEstableceSectorDeNegocio", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fPrimerSectorDeNegocio() As String
   Dim rsSectorDeNegocio As ADODB.Recordset
   Dim SQL As String
   Dim PrimerSector As String
   On Error GoTo h_ERROR
   PrimerSector = ""
   SQL = " SELECT Descripcion"
   SQL = SQL & " FROM Comun.Gv_SectorDeNegocio_B1"
   Set rsSectorDeNegocio = New ADODB.Recordset
   If gDbUtil.fOpenRecordset(rsSectorDeNegocio, SQL, gDefDatabase.Conexion) Then
      If gDbUtil.fRecordCount(rsSectorDeNegocio) Then
         rsSectorDeNegocio.MoveFirst
         PrimerSector = rsSectorDeNegocio.Fields("Descripcion").Value
      End If
   End If
   gDbUtil.sDestroyRecordSet rsSectorDeNegocio
h_EXIT: On Error GoTo 0
   fPrimerSectorDeNegocio = PrimerSector
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fPrimerSectorDeNegocio", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

