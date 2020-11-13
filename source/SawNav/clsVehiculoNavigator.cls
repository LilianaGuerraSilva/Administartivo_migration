VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsVehiculoNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private mConsecutivoCompania As Long
Private mConsecutivo As Long
Private mPlaca As String
Private mserialVIN As String
Private mNombreModelo As String
Private mMarca As String
Private mAno As Long
Private mCodigoColor As String
Private mCodigoCliente As String
Private mNombreCliente As String
Private mRIFCliente As String
Private mNumeroPoliza As String
Private mSerialMotor As String
Private mNombreOperador As String
Private mFechaUltimaModificacion As Date
Private mFechaUltimaModificacionIsSet As Boolean

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsVehiculoNavigator"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Vehiculo"
End Function

Public Function GetConsecutivoCompania() As Long
   GetConsecutivoCompania = mConsecutivoCompania
End Function

Public Sub SetConsecutivoCompania(ByVal valNewValue As Long)
   mConsecutivoCompania = valNewValue
End Sub

Public Function GetConsecutivo() As Long
   GetConsecutivo = mConsecutivo
End Function

Public Sub SetConsecutivo(ByVal valNewValue As Long)
   mConsecutivo = valNewValue
End Sub

Public Function GetPlaca() As String
   GetPlaca = mPlaca
End Function

Public Sub SetPlaca(ByVal valNewValue As String)
   mPlaca = gTexto.DfMid(valNewValue, 1, 20)
End Sub

Public Function GetserialVIN() As String
   GetserialVIN = mserialVIN
End Function

Public Sub SetserialVIN(ByVal valNewValue As String)
   mserialVIN = gTexto.DfMid(valNewValue, 1, 40)
End Sub

Public Function GetNombreModelo() As String
   GetNombreModelo = mNombreModelo
End Function

Public Sub SetNombreModelo(ByVal valNewValue As String)
   mNombreModelo = gTexto.DfMid(valNewValue, 1, 20)
End Sub

Public Function GetMarca() As String
   GetMarca = mMarca
End Function

Public Sub SetMarca(ByVal valNewValue As String)
   mMarca = gTexto.DfMid(valNewValue, 1, 20)
End Sub

Public Function GetAno() As Long
   GetAno = mAno
End Function

Public Sub SetAno(ByVal valNewValue As Long)
   mAno = valNewValue
End Sub

Public Function GetCodigoColor() As String
   GetCodigoColor = mCodigoColor
End Function

Public Sub SetCodigoColor(ByVal valNewValue As String)
   mCodigoColor = gTexto.DfMid(valNewValue, 1, 3)
End Sub

Public Function GetCodigoCliente() As String
   GetCodigoCliente = mCodigoCliente
End Function

Public Sub SetCodigoCliente(ByVal valNewValue As String)
   mCodigoCliente = gTexto.DfMid(valNewValue, 1, 10)
End Sub

Public Function GetNombreCliente() As String
   GetNombreCliente = mNombreCliente
End Function

Public Sub SetNombreCliente(ByVal valNewValue As String)
   mNombreCliente = gTexto.DfMid(valNewValue, 1, 80)
End Sub

Public Function GetRIFCliente() As String
   GetRIFCliente = mRIFCliente
End Function

Public Sub SetRIFCliente(ByVal valNewValue As String)
   mRIFCliente = gTexto.DfMid(valNewValue, 1, 80)
End Sub

Public Function GetNumeroPoliza() As String
   GetNumeroPoliza = mNumeroPoliza
End Function

Public Sub SetNumeroPoliza(ByVal valNewValue As String)
   mNumeroPoliza = gTexto.DfMid(valNewValue, 1, 20)
End Sub

Public Function GetSerialMotor() As String
   GetSerialMotor = mSerialMotor
End Function

Public Sub SetSerialMotor(ByVal valNewValue As String)
   mSerialMotor = gTexto.DfMid(valNewValue, 1, 40)
End Sub

Public Function GetNombreOperador() As String
   GetNombreOperador = mNombreOperador
End Function

Public Sub SetNombreOperador(ByVal valNewValue As String)
   mNombreOperador = gTexto.DfMid(valNewValue, 1, 10)
End Sub

Public Function GetFechaUltimaModificacion() As Date
   GetFechaUltimaModificacion = mFechaUltimaModificacion
End Function

Public Sub SetFechaUltimaModificacion(ByVal valNewValue As Date)
   mFechaUltimaModificacion = valNewValue
   mFechaUltimaModificacionIsSet = True
End Sub

Public Function GetTableName() As String
   GetTableName = "Vehiculo"
End Function


Public Function fSearchByField(ByVal fieldName As String, ByVal fieldValue As String, Optional ByVal valShowMessageIfNotFound As Boolean = False) As Boolean
   Dim mrsVehiculo As ADODB.Recordset
   On Error GoTo h_ERROR
   fSearchByField = False
      Set mrsVehiculo = New ADODB.Recordset
   If gDbUtil.fOpenRecordSetAllParameters(mrsVehiculo, gProyCompaniaActual.getSQLSearchByFieldStringAndConsecutivo("Saw.Vehiculo", fieldName, fieldValue), _
         gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If mrsVehiculo.RecordCount = 0 Then
         fSearchByField = False
         If valShowMessageIfNotFound Then
            gMessage.sNoExistenRegistrosConEstasCaracteristicas CM_MESSAGE_NAME, eg_Male
         End If
      Else
         fSearchByField = True
         mConsecutivoCompania = mrsVehiculo("ConsecutivoCompania").value
         mConsecutivo = mrsVehiculo("Consecutivo").value
         mPlaca = mrsVehiculo("Placa").value
         mserialVIN = mrsVehiculo("serialVIN").value
         mNombreModelo = mrsVehiculo("NombreModelo").value
         mAno = mrsVehiculo("Ano").value
         mCodigoColor = mrsVehiculo("CodigoColor").value
         mCodigoCliente = mrsVehiculo("CodigoCliente").value
         mNumeroPoliza = mrsVehiculo("NumeroPoliza").value
         mSerialMotor = mrsVehiculo("SerialMotor").value
      End If
   End If
   gDbUtil.sDestroyRecordSet mrsVehiculo
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSearchByField = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSearchByField", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sRestaurarDatos(ByVal valRecordSet As ADODB.Recordset, ByVal valConsecutivoCompaniaOld As Long, ByVal valConsecutivoCompaniaNew As Long)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   valRecordSet.Filter = "ConsecutivoCompania = " & valConsecutivoCompaniaOld
   While Not valRecordSet.EOF
      Set vCmd = insUtAdoCmd.fCreateCmd("Saw.Gp_VehiculoINS", gDefDatabase.Conexion)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaNew)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", valRecordSet("Consecutivo").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Placa", 20, valRecordSet("Placa").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@serialVIN", 40, valRecordSet("serialVIN").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreModelo", 20, valRecordSet("NombreModelo").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Ano", valRecordSet("Ano").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoColor", 3, valRecordSet("CodigoColor").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoCliente", 10, valRecordSet("CodigoCliente").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NumeroPoliza", 20, valRecordSet("NumeroPoliza").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@SerialMotor", 40, valRecordSet("SerialMotor").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, valRecordSet("NombreOperador").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", valRecordSet("FechaUltimaModificacion").value)
      insUtAdoCmd.fExecCmd vCmd
      Set vCmd = Nothing
      gUtilRespaldo.sAddProgress
      valRecordSet.MoveNext
   Wend
   valRecordSet.Filter = adFilterNone
   Set insUtAdoCmd = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRestaurarDatos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLlenaDatosDeVehiculoXCliente(ByVal valConsecutivo As Long, ByRef refPlaca As String, ByRef refSerialVIN As String, ByRef refModelo As String, ByRef refMarca, ByRef refAno As String, ByRef refColor As String, ByRef refNumeroPoliza As String, ByRef refSerialMotor As String)
   Dim vSQL As String
   Dim rsVehiculo As ADODB.Recordset
   On Error GoTo h_ERROR
   vSQL = "SELECT Saw.Vehiculo.Placa,"
   vSQL = vSQL & " Saw.Vehiculo.serialVIN,"
   vSQL = vSQL & " Saw.Vehiculo.NombreModelo,"
   vSQL = vSQL & " Saw.Modelo.Marca ,"
   vSQL = vSQL & " Saw.Vehiculo.Ano,"
   vSQL = vSQL & " Saw.Color.DescripcionColor AS Color,"
   vSQL = vSQL & " Saw.Vehiculo.NumeroPoliza,"
   vSQL = vSQL & " Saw.Vehiculo.SerialMotor"
   vSQL = vSQL & " FROM Saw.Vehiculo INNER JOIN"
   vSQL = vSQL & " Saw.Color ON Saw.Vehiculo.ConsecutivoCompania = Saw.Color.ConsecutivoCompania AND Saw.Vehiculo.CodigoColor = Saw.Color.CodigoColor INNER JOIN"
   vSQL = vSQL & " Saw.Modelo ON Saw.Vehiculo.NombreModelo = Saw.Modelo.Nombre"
   vSQL = vSQL & " WHERE Saw.Vehiculo.Consecutivo = " & valConsecutivo & " AND SAW.Vehiculo.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Set rsVehiculo = New ADODB.Recordset
   If gDbUtil.fOpenRecordset(rsVehiculo, vSQL, gDefDatabase.Conexion) Then
      If gDbUtil.fRecordCount(rsVehiculo) > 0 Then
         refPlaca = rsVehiculo.Fields("Placa").value
         refSerialVIN = rsVehiculo.Fields("SerialVIN").value
         refModelo = rsVehiculo.Fields("NombreModelo").value
         refMarca = rsVehiculo.Fields("Marca").value
         refAno = rsVehiculo.Fields("Ano").value
         refColor = rsVehiculo.Fields("Color").value
         refNumeroPoliza = rsVehiculo.Fields("NumeroPoliza").value
         refSerialMotor = rsVehiculo.Fields("SerialMotor").value
      End If
   End If
   Set rsVehiculo = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLlenaDatosDeVehiculoXCliente", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
