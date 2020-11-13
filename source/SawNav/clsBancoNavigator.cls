VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsBancoNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private mrsBanco As ADODB.Recordset
Private mConsecutivo As Long
Private mCodigo As String
Private mNombre As String
Private mStatus As Boolean
Private mNombreOperador As String
Private mFechaUltimaModificacion As Date

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsBancoNavigator"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Banco"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Function fPrimaryKeyExistMessage() As String
   fPrimaryKeyExistMessage = "Ya existe Codigo"
End Function
Private Sub sClrRecord()
   On Error GoTo h_ERROR
   SetConsecutivo 0
   SetCodigo ""
   SetNombre ""
   SetStatus False
   SetNombreOperador gProyUsuarioActual.GetNombreDelUsuario
   SetFechaUltimaModificacion gUtilDate.getFechaDeHoy
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sClrRecord", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function GetConsecutivo() As Long
   GetConsecutivo = mConsecutivo
End Function
Private Sub SetConsecutivo(ByVal valNewValue As Long)
   mConsecutivo = valNewValue
End Sub
Public Function GetCodigo() As Long
   GetCodigo = mCodigo
End Function
Private Sub SetCodigo(ByVal valNewValue As String)
   mCodigo = gTexto.DfMid(valNewValue, 1, 4)
End Sub
Public Function GetNombre() As String
   GetNombre = mNombre
End Function
Private Sub SetNombre(ByVal valNewValue As String)
   mNombre = gTexto.DfMid(valNewValue, 1, 40)
End Sub
Public Function GetStatus() As Boolean
   GetStatus = mStatus
End Function
Public Sub SetStatus(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mStatus = valNewValue
End Sub
Private Sub SetNombreOperador(ByVal valNewValue As String)
   mNombreOperador = gTexto.DfMid(valNewValue, 1, 20)
End Sub
Private Sub SetFechaUltimaModificacion(ByVal valNewValue As Date)
   mFechaUltimaModificacion = valNewValue
End Sub
Public Function GetTableName() As String
   GetTableName = "Comun.Gv_Banco_B1"
End Function
Private Sub setValuesWithRS(Optional ByVal valRecordSet As ADODB.Recordset = Nothing)
   On Error GoTo h_ERROR
   If valRecordSet Is Nothing Then
      Set valRecordSet = mrsBanco
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "Consecutivo") Then
      If Not IsNull(valRecordSet("Consecutivo")) Then
         SetConsecutivo valRecordSet("Consecutivo")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "Codigo") Then
      If Not IsNull(valRecordSet("Codigo")) Then
         SetCodigo valRecordSet("Codigo")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "Nombre") Then
      If Not IsNull(valRecordSet("Nombre")) Then
         SetNombre valRecordSet("Nombre")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "Status") Then
      If Not IsNull(valRecordSet("Status")) Then
         SetStatus gConvert.ConvertStringToBoolean(valRecordSet("Status"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "NombreOperador") Then
      If Not IsNull(valRecordSet("NombreOperador")) Then
         SetNombreOperador valRecordSet("NombreOperador")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "FechaUltimaModificacion") Then
      If Not gDbUtil.fDateIsNull(valRecordSet("FechaUltimaModificacion")) Then
         SetFechaUltimaModificacion valRecordSet("FechaUltimaModificacion")
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
   If Not (mrsBanco Is Nothing) Then
      gDbUtil.sCloseIfOpened mrsBanco
      Set mrsBanco = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Class_Terminate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function fInsert(Optional ByVal vBuscaCodigoBanco As Boolean) As Boolean
   Dim vCmd  As ADODB.Command
   Dim insUtAdoCmd As clsUtilAdoCmd
   On Error GoTo h_ERROR
   fInsert = False
   If (vBuscaCodigoBanco) Then
    While (fSearchByNumericField("Codigo", GetCodigo, False, False)) Or GetCodigo = 0
       SetCodigo gDbUtil.fNextLngConsecutive("Comun.Gv_Banco_B1", "Consecutivo", "")
    Wend
   End If
        
   Set insUtAdoCmd = New clsUtilAdoCmd
   Set vCmd = insUtAdoCmd.fCreateCmd("Comun.Gp_BancoINS", gDefDatabase.Conexion)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", GetConsecutivo)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Codigo", 4, GetCodigo)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Nombre", 40, GetNombre)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@Status", 1, "0")
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@fldOrigen", 1, "0")
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, gProyUsuarioActual.GetNombreDelUsuario)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", gUtilDate.getFechaDeHoy)
   vCmd.Execute
   Set vCmd = Nothing
   Set insUtAdoCmd = Nothing
   fInsert = True
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: mrsBanco.CancelUpdate
   If gErrorCodesIG.isErrorPrimaryKeyExist(Err.Number, Err.Description, Err.HelpContext) Then
      gMessage.Advertencia (fPrimaryKeyExistMessage)
      On Error GoTo 0
   Else
      Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fInsert", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
   End If
End Function
Public Function fSearchByField(ByVal fieldName As String, ByVal fieldValue As String, Optional ByVal valChangeRecordSet As Boolean = True, Optional ByVal valShowMessageIfNotFound As Boolean = False) As Boolean
   Dim rsBeforeChanges As ADODB.Recordset
   On Error GoTo h_ERROR
   fSearchByField = False
   If Not valChangeRecordSet Then
      Set rsBeforeChanges = mrsBanco
      Set mrsBanco = New ADODB.Recordset
   End If
   gDbUtil.sInitRS mrsBanco, GetTableName, False
   If gDbUtil.fOpenRecordSetAllParameters(mrsBanco, gUtilSQL.fSQLSearchByFieldString(GetTableName, fieldName, fieldValue), gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If mrsBanco.RecordCount = 0 Then
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
      Set mrsBanco = Nothing
      Set mrsBanco = rsBeforeChanges
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSearchByField = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSearchByField", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSearchByNumericField(ByVal fieldName As String, ByVal fieldValue As String, Optional ByVal valChangeRecordSet As Boolean = True, Optional ByVal valShowMessageIfNotFound As Boolean = False) As Boolean
   Dim rsBeforeChanges As ADODB.Recordset
   On Error GoTo h_ERROR
   fSearchByNumericField = False
   If Not valChangeRecordSet Then
      Set rsBeforeChanges = mrsBanco
      Set mrsBanco = New ADODB.Recordset
   End If
   gDbUtil.sInitRS mrsBanco, "Comun.Gv_Banco_B1", False
   If gDbUtil.fOpenRecordSetAllParameters(mrsBanco, gUtilSQL.fSQLSearchByNumericField("Comun.Gv_Banco_B1", fieldName, fieldValue), gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If mrsBanco.RecordCount = 0 Then
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
      Set mrsBanco = Nothing
      Set mrsBanco = rsBeforeChanges
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSearchByNumericField = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSearchByNumericField", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Sub sRestaurarDatos(ByVal valRecordSet As ADODB.Recordset)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vExisteConse As Boolean
   Dim vExisteCodigo As Boolean
   Dim vExisteNombre As Boolean
   Dim vConsecutvo As Long
   Dim vCodigo As String
   Dim vNombre As String
   Dim vStatus As String
   Dim vOrigen As String
   On Error GoTo h_ERROR
   While Not valRecordSet.EOF
      vExisteConse = True
      vExisteCodigo = True
      vExisteNombre = True
      If gDbUtil.FieldInRecordSet(valRecordSet, "Consecutivo") Then
         If gDbUtil.fRecordCountOfSql(gUtilSQL.fSQLSearchByNumericField("Comun.Gv_Banco_B1", "Consecutivo", valRecordSet("Consecutivo").value)) <= 0 Then
            vExisteConse = False
         End If
         vConsecutvo = valRecordSet("Consecutivo").value
      Else
         vExisteConse = False
         vConsecutvo = fGenerarProximoConsecutivo() + 1
      End If

      If gDbUtil.FieldInRecordSet(valRecordSet, "Status") Then
         vStatus = valRecordSet("Status").value
      Else
         vStatus = "0"
      End If
      
      If gDbUtil.FieldInRecordSet(valRecordSet, "fldOrigen") Then
         vOrigen = valRecordSet("fldOrigen").value
      Else
         vOrigen = "1"
      End If
     If gDbUtil.FieldInRecordSet(valRecordSet, "Codigo") Then
         If gDbUtil.fRecordCountOfSql(gUtilSQL.fSQLSearchByFieldString("Comun.Gv_Banco_B1", "Codigo", valRecordSet("Codigo").value)) > 0 Then
            vCodigo = valRecordSet("Codigo").value & vConsecutvo
            vExisteCodigo = False
         Else
            vCodigo = valRecordSet("Codigo").value
            vExisteCodigo = False
            If valRecordSet("Codigo").value = 0 Then
               vCodigo = fGenerateNextCodigo()
            End If
         End If
      Else
         vExisteCodigo = False
         vCodigo = fGenerateNextCodigo()
      End If
           
      If gDbUtil.fRecordCountOfSql(gUtilSQL.fSQLSearchByFieldString("Comun.Gv_Banco_B1", "Nombre", valRecordSet("Nombre").value)) > 0 Then
         vNombre = valRecordSet("Nombre").value & " " & vCodigo
      Else
         vNombre = valRecordSet("Nombre").value
         vExisteNombre = False
      End If
      If Not vExisteConse Then
         If Not vExisteCodigo Then
               Set insUtAdoCmd = New clsUtilAdoCmd
               Set vCmd = insUtAdoCmd.fCreateCmd("Comun.Gp_BancoINS", gDefDatabase.Conexion)
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", vConsecutvo)
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Codigo", 4, vCodigo)
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Nombre", 40, vNombre)
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@Status", 1, vStatus)
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@fldOrigen", 1, vOrigen)
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, valRecordSet("NombreOperador").value)
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", valRecordSet("FechaUltimaModificacion").value)
               insUtAdoCmd.fExecCmd vCmd
               Set vCmd = Nothing
               Set insUtAdoCmd = Nothing
         End If
      End If
      gUtilRespaldo.sAddProgress
      valRecordSet.MoveNext
   Wend

h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRestaurarDatos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLeeFileYGrabaRecordsEnLaTabla(ByVal valMuestraclsUtilProcessProgress As Boolean)
   Dim lineaActual As String
   Dim Continuar As Boolean
   Dim vArchivoDeLectura As String
   Dim varConsecutivo As String
   Dim varCodigo As String
   Dim varDescripcion As String
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim varCaracterSeparador As String
   Dim i As Long
   Dim insUtilImport As clsUtilImport
   Dim insProgres As clsUtilProcessProgress
   Dim varFSO As FileSystemObject
   Dim varFile As TextStream
   On Error GoTo h_ERROR
   Set varFSO = New FileSystemObject
   Set insProgres = New clsUtilProcessProgress
   Set insUtilImport = New clsUtilImport
   Set insUtAdoCmd = New clsUtilAdoCmd
   varCaracterSeparador = ";"
   Continuar = True
   vArchivoDeLectura = gWorkPaths.fPathDeLasTablasComunes(gDefProg.GetSiglasDelPrograma, gDefProg.GetPais) & "Banco.txt"
   If gUtilFile.fExisteElArchivo(vArchivoDeLectura) Then
      If valMuestraclsUtilProcessProgress Then
         If insProgres.fInitializeProgress("Creando Registros de " & CM_MESSAGE_NAME, "", True, True, 20, True, gDefgen.getMainForm) Then
         End If
      End If
      Set varFile = varFSO.OpenTextFile(vArchivoDeLectura, ForReading)
      Continuar = True
      i = -1
      While Continuar
         i = i + 1
         varConsecutivo = ""
         varCodigo = ""
         varDescripcion = ""
         lineaActual = varFile.ReadLine
         If lineaActual <> "" Then
            sClrRecord
            lineaActual = gTexto.fCleanStringForDOSImport(lineaActual)
            varCodigo = gDbUtil.fNextLngConsecutive("Comun.Gv_Banco_B1", "Consecutivo", "")
            varDescripcion = insUtilImport.fReadValueUntilDelimiterAndCut(lineaActual, varCaracterSeparador)
            varConsecutivo = gDbUtil.fNextLngConsecutive("Comun.Gv_Banco_B1", "Consecutivo", "")
            If gDbUtil.fRecordCountOfSql(gUtilSQL.fSQLSearchByFieldString("Comun.Gv_Banco_B1", "Nombre", varDescripcion)) = 0 Then
               Set vCmd = insUtAdoCmd.fCreateCmd("Comun.Gp_BancoINS", gDefDatabase.Conexion)
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", varConsecutivo)
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Codigo", 4, varCodigo)
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Nombre", 40, varDescripcion)
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@Status", 1, "0")
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@fldOrigen", 1, "1")
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, gProyUsuarioActual.GetNombreDelUsuario)
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", gUtilDate.getFechaDeHoy)
               vCmd.Execute
               Set vCmd = Nothing
            End If
            Continuar = Not (varFile.AtEndOfStream)
         End If
         If valMuestraclsUtilProcessProgress Then
            insProgres.sAddStandardRsProgress
         End If
      Wend
      varFile.Close
      If valMuestraclsUtilProcessProgress Then
         If insProgres.fDestroyProgress Then
         End If
      End If
   End If
   Set varFSO = Nothing
   Set insUtilImport = Nothing
   Set insProgres = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLeeFileYGrabaRecordsEnLaTabla", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

'LOGICA DE NEGOCIO  -  SAW
'Logica de Negocio en CuentaBancariaNavigator.fGeneraCuentaBancariaGenericaSiCiaNoGeneraMovimientosBancarios
Public Function fGetCodigoBancoAndInsertIfNotExists(ByVal valNombreBanco As String) As Long
   Dim vResult As Long
   On Error GoTo h_ERROR
   vResult = 0
   If fSearchByField("Nombre", valNombreBanco, True) Then
      vResult = GetCodigo
   Else
      SetCodigo gDbUtil.fNextLngConsecutive("Comun.Gv_Banco_B1", "Consecutivo", "")
      SetNombre valNombreBanco
      If fInsert Then
         vResult = GetCodigo
      End If
   End If
   fGetCodigoBancoAndInsertIfNotExists = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGetCodigoBancoAndInsertIfNotExists", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
' Logica de Negocio de CuentaBancariaNavigator.fBuscaCodigoDeBancoParaCuentaGenerica
Public Function fBuscaPrimerCodigoSiNoexisteCreaUnoGenerico() As Long
   Dim vCodigo As Long
   Dim vConsecutivo As Long
   Dim vinsBancoNavigator As clsBancoNavigator
   On Error GoTo h_ERROR
   Set vinsBancoNavigator = New clsBancoNavigator
   vConsecutivo = fGenerarProximoConsecutivo() + 1
   vCodigo = gConvert.ConvierteAlong(fGenerateNextCodigo())
   If Not vinsBancoNavigator.fSearchByField("Nombre", "BANCO GENÉRICO", True) Then
      SetCodigo vCodigo
      SetNombre "BANCO GENÉRICO"
      SetConsecutivo vConsecutivo
      fInsert False
      vConsecutivo = GetConsecutivo
   Else
      vConsecutivo = vinsBancoNavigator.GetConsecutivo
   End If
   fBuscaPrimerCodigoSiNoexisteCreaUnoGenerico = vConsecutivo
   Set vinsBancoNavigator = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fBuscaPrimerCodigoSiNoexisteCreaUnoGenerico", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fGenerarProximoConsecutivo() As Long
   Dim vResult As Long
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vRecord As ADODB.Recordset

   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   Set vRecord = New ADODB.Recordset

   Set vCmd = insUtAdoCmd.fCreateCmd("Lib.Gp_MaxConsecutive", gDefDatabase.Conexion)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@TableName", 128, "Comun.Banco")
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@FieldExpression", 128, "Consecutivo")
   Set vRecord = insUtAdoCmd.fExecCmd(vCmd)
   vResult = 0
   If (vRecord.RecordCount > 0) And (Not IsNull(vRecord("Maximo").value)) Then
      vResult = vRecord("Maximo").value
   End If

   Set vRecord = Nothing
   Set insUtAdoCmd = Nothing
   Set vCmd = Nothing

   fGenerarProximoConsecutivo = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGenerarProximoConsecutivo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fGenerateNextCodigo() As String
   Dim Sql As String
   Dim lngLastCodigo As String
   Dim rsMaximo As ADODB.Recordset
   On Error GoTo h_ERROR
   Set rsMaximo = New ADODB.Recordset
   
   Sql = "SELECT MAX(" & gUtilSQL.DfCDblSQL("Codigo") & ") AS maximo FROM Comun.Banco"
   If gDbUtil.fOpenRecordSetAllParameters(rsMaximo, Sql, gDefDatabase.Conexion, adLockReadOnly, adUseClient, adOpenStatic) Then
      If (rsMaximo.RecordCount > 0) And (Not IsNull(rsMaximo("maximo").value)) Then
         lngLastCodigo = Val(rsMaximo("maximo").value)
      End If
   End If
   lngLastCodigo = gTexto.fIncrementaNumeroString(lngLastCodigo)
   fGenerateNextCodigo = lngLastCodigo
   gDbUtil.sDestroyRecordSet rsMaximo
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fGenerateNextCodigo = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGenerateNextCodigo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

