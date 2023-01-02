VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsContratoTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit


Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsContratoTbl"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Contrato"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetTableName() As String
   GetTableName = "Contrato"
End Function

Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   
   gDbUtil.AddForeignKeySegunTablaPrimaria "Cliente", GetTableName(), "Consecutivo", _
         "ConsecutivoCliente", Not usarSoloUnElementoEnLaRelacion, _
         "ConsecutivoCompania", "ConsecutivoCompania", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "Moneda", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Codigo", "CodigoMoneda", Not deleteOnCascade
'   gDbUtil.AddForeignKeySegunTablaPrimaria "Adm.Vendedor", GetTableName(), "Consecutivo", _
'         "ConsecutivoVendedor", Not usarSoloUnElementoEnLaRelacion, _
'         "ConsecutivoCompania", "ConsecutivoCompania", Not deleteOnCascade
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE Contrato ADD CONSTRAINT fk_ContratoVend FOREIGN KEY (ConsecutivoCompania,ConsecutivoVendedor) REFERENCES Adm.Vendedor (ConsecutivoCompania,Consecutivo) ON UPDATE NO ACTION ON DELETE NO ACTION"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania,NumeroContrato)"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Observaciones"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreOperador"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sAllowCeroLengthFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sModifyOtherPropertiesOfFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaDeInicio", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaFinal", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaUltimaModificacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sModifyOtherPropertiesOfFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim SQL As String
   Dim insRenglonContrato As clsRenglonContratoTbl
   Dim insMesGenerado As clsMesGeneradoTbl
   
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "NumeroContrato " & gUtilSQL.getVarCharTypeForDB(5) & " CONSTRAINT nn" & GetTableName() & "NumeroCont NOT NULL, "
   SQL = SQL & "StatusContrato " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "CodigoCliente " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "ConsecutivoCliente" & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "ConsecutiClien NOT NULL, "
   SQL = SQL & "DuracionDelContrato " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "FechaDeInicio " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "FechaFinal " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "Observaciones " & gUtilSQL.getVarCharTypeForDB(255) & ", "
   SQL = SQL & "CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & " CONSTRAINT nn" & GetTableName() & "CodigoMoneda NOT NULL, "
   SQL = SQL & "NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "ConsecutivoVendedor " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "VenCon NOT NULL, "
   SQL = SQL & "NombreVendedor " & gUtilSQL.getVarCharTypeForDB(35) & ", "
   SQL = SQL & "Moneda " & gUtilSQL.getVarCharTypeForDB(80) & ", "
   SQL = SQL & "FacturarAOtroCliente" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "FacturarAO NOT NULL, "
   SQL = SQL & "CodigoClienteAFacturar" & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "NombreClienteAFacturar" & gUtilSQL.getVarCharTypeForDB(80) & ", "
   SQL = SQL & "ContratoFueModificado" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ContratoFu NOT NULL, "
   SQL = SQL & "ContratoFueModificadoPor" & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & gDefDatabase.getFN_TIMESTAMP() & " timestamp )"
   gDbUtil.Execute gDefDatabase.Conexion, SQL
   sAddPrimaryKeyAndOtherConstrains
   sAllowCeroLengthFields
   sModifyOtherPropertiesOfFields
   If valShowMessage Then
      gMessage.exito "La Tabla '" & CM_MESSAGE_NAME & "' fue creada exitosamente"
   End If

   Set insRenglonContrato = New clsRenglonContratoTbl
   Set insMesGenerado = New clsMesGeneradoTbl
   If insRenglonContrato.fCreateTable(valShowMessage) Then
      If insMesGenerado.fCreateTable(valShowMessage) Then
         fCreateTable = True
      End If
   End If
   Set insRenglonContrato = Nothing
   Set insMesGenerado = Nothing

h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fCreateTable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sDropTable()
   Dim insRenglonContrato As clsRenglonContratoTbl
   Dim insMesGenerado As clsMesGeneradoTbl
   On Error GoTo h_ERROR
   Set insRenglonContrato = New clsRenglonContratoTbl
   insRenglonContrato.sDropTable
   Set insRenglonContrato = Nothing
   Set insMesGenerado = New clsMesGeneradoTbl
   
   insMesGenerado.sDropTable
   Set insMesGenerado = Nothing
   gDbUtil.ExecuteDropTable GetTableName, True
   gMessage.ProgrammerMessage "La Tabla '" & CM_MESSAGE_NAME & "' fue borrada exitosamente"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sDropTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

