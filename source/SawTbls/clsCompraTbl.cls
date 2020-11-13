VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCompraTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit


Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsCompraTbl"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Compra"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetTableName() As String
   GetTableName = "Compra"
End Function

Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   gDbUtil.AddForeignKeySegunTablaPrimaria "Adm.Proveedor", GetTableName(), "CodigoProveedor", _
         "CodigoProveedor", Not usarSoloUnElementoEnLaRelacion, _
         "ConsecutivoCompania", "ConsecutivoCompania", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "Saw.Almacen", GetTableName(), "Consecutivo", _
         "ConsecutivoAlmacen", Not usarSoloUnElementoEnLaRelacion, _
         "ConsecutivoCompania", "ConsecutivoCompania", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "Moneda", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Codigo", "CodigoMoneda", Not deleteOnCascade
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania,NumeroSecuencial)"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Comentarios"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NoFacturaNotaEntrega"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreOperador"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sAllowCeroLengthFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sModifyOtherPropertiesOfFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "Fecha", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechadeAnulacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaUltimaModificacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sModifyOtherPropertiesOfFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim SQL As String
   Dim insRenglonCompra As clsRenglonCompraTbl
   Dim insRenglonCompraXSerial As clsRenglonCompraXSerialTbl
   
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "NumeroSecuencial " & gUtilSQL.getVarCharTypeForDB(10) & " CONSTRAINT nn" & GetTableName() & "NumeroSecu NOT NULL, "
   SQL = SQL & "Numero " & gUtilSQL.getVarCharTypeForDB(20) & " CONSTRAINT nn" & GetTableName() & "numero NOT NULL, "
   SQL = SQL & "Fecha " & gUtilSQL.getDateTypeForDB() & " CONSTRAINT nn" & GetTableName() & "Fecha NOT NULL, "
   SQL = SQL & "CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & " CONSTRAINT nn" & GetTableName() & "CodigoProv NOT NULL, "
   SQL = SQL & "CodigoAlmacen " & gUtilSQL.getVarCharTypeForDB(5) & ", "
   SQL = SQL & "Moneda " & gUtilSQL.getVarCharTypeForDB(80) & ", "
   SQL = SQL & "CambioAbolivares " & gUtilSQL.getNumericTypeForDB(12, 2) & " CONSTRAINT nn" & GetTableName() & "CambioABol NOT NULL, "
   SQL = SQL & "GenerarCxp " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "GenerarCXP NOT NULL, "
   SQL = SQL & "TotalRenglones " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "OtrosGastos " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "TotalCompra " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "Comentarios " & gUtilSQL.getVarCharTypeForLargeTextForDB() & ", "
   SQL = SQL & "DistribuirGastos " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "PorcentajeDeDistribucion " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "EsUnaOrdenDeCompra " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "EsUnaOrden NOT NULL, "
   SQL = SQL & "StatusCompra " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "FechadeAnulacion " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "TipoDeCompra " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "NoFacturaNotaEntrega " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "TipoDeCompraParaCxp " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & " CONSTRAINT nn" & GetTableName() & "CodigoMoneda NOT NULL, "
   SQL = SQL & "ConsecutivoAlmacen" & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & gDefDatabase.getFN_TIMESTAMP() & " timestamp )"
   gDbUtil.Execute gDefDatabase.Conexion, SQL
   sAddPrimaryKeyAndOtherConstrains
   sAllowCeroLengthFields
   sModifyOtherPropertiesOfFields
   If valShowMessage Then
      gMessage.exito "La Tabla '" & CM_MESSAGE_NAME & "' fue creada exitosamente"
   End If
   Set insRenglonCompra = New clsRenglonCompraTbl
   If insRenglonCompra.fCreateTable(valShowMessage) Then
      fCreateTable = True
   End If
   Set insRenglonCompra = Nothing
   fCreateTable = False
   Set insRenglonCompraXSerial = New clsRenglonCompraXSerialTbl
   If insRenglonCompraXSerial.fCreateTable(valShowMessage) Then
      fCreateTable = True
   End If
   Set insRenglonCompraXSerial = Nothing

h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fCreateTable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sDropTable()
   Dim insRenglonCompra As clsRenglonCompraTbl
   On Error GoTo h_ERROR
   Set insRenglonCompra = New clsRenglonCompraTbl
   insRenglonCompra.sDropTable
   Set insRenglonCompra = Nothing
   gDbUtil.ExecuteDropTable "RenglonCompraXSerial", True
   gDbUtil.ExecuteDropTable GetTableName, True
   gMessage.ProgrammerMessage "La Tabla '" & CM_MESSAGE_NAME & "' fue borrada exitosamente"

h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sDropTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

