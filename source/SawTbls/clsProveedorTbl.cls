VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsProveedorTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit


Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsProveedorTbl"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Proveedor"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetTableName() As String
   GetTableName = "Proveedor"
End Function

Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   gDbUtil.AddForeignKeySegunTablaPrimaria "Saw.TipoProveedor", GetTableName(), "Nombre", _
         "TipodeProveedor", Not usarSoloUnElementoEnLaRelacion, _
         "ConsecutivoCompania", "ConsecutivoCompania", Not deleteOnCascade
        
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania,CodigoProveedor)"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Contacto"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NumeroRif"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NumeroNit"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoRetencionUsual"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Telefonos"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Direccion"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Fax"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Email"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CuentaContableCxp"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CuentaContableGastos"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoLote"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Beneficiario"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreOperador"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "TipoDePersona"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoRetencionUsual"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "TipoDocumentoIdentificacion"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NumeroCuentaBancaria"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sAllowCeroLengthFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sModifyOtherPropertiesOfFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaUltimaModificacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sModifyOtherPropertiesOfFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim sql As String
   On Error GoTo h_ERROR
   sql = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   sql = sql & "ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   sql = sql & "CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & " CONSTRAINT nn" & GetTableName() & "CodigoProv NOT NULL, "
   sql = sql & "NombreProveedor " & gUtilSQL.getVarCharTypeForDB(60) & " CONSTRAINT nn" & GetTableName() & "NombreProv NOT NULL, "
   sql = sql & "Contacto " & gUtilSQL.getVarCharTypeForDB(35) & ", "
   sql = sql & "NumeroRIF" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   sql = sql & "NumeroNit " & gUtilSQL.getVarCharTypeForDB(12) & ", "
   sql = sql & "TipoDePersona " & gUtilSQL.getCharTypeForDB(1) & ", "
   sql = sql & "CodigoRetencionUsual " & gUtilSQL.getVarCharTypeForDB(6) & " CONSTRAINT nn" & GetTableName() & "CodigoRete NOT NULL, "
   sql = sql & "Telefonos " & gUtilSQL.getVarCharTypeForDB(40) & ", "
   sql = sql & "Direccion " & gUtilSQL.getVarCharTypeForDB(255) & ", "
   sql = sql & "Fax " & gUtilSQL.getVarCharTypeForDB(25) & ", "
   sql = sql & "Email " & gUtilSQL.getVarCharTypeForDB(100) & ", "
   sql = sql & "TipodeProveedor " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   sql = sql & "TipoDeProveedorDeLibrosFiscales " & gUtilSQL.getCharTypeForDB(1) & ", "
   sql = sql & "PorcentajeRetencionIva " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   sql = sql & "CuentaContableCxp " & gUtilSQL.getVarCharTypeForDB(30) & ", "
   sql = sql & "CuentaContableGastos " & gUtilSQL.getVarCharTypeForDB(30) & ", "
   sql = sql & "CuentaContableAnticipo " & gUtilSQL.getVarCharTypeForDB(30) & ", "
   sql = sql & "CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   sql = sql & "Beneficiario " & gUtilSQL.getVarCharTypeForDB(60) & ", "
   sql = sql & "UsarBeneficiarioImpCheq " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsarBenefi NOT NULL, "
   sql = sql & "TipoDocumentoIdentificacion" & gUtilSQL.getCharTypeForDB(1) & ", "
   sql = sql & "EsAgenteDeRetencionIva" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "EsAgenteDe NOT NULL, "
   sql = sql & "ApellidoPaterno" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   sql = sql & "ApellidoMaterno" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   sql = sql & "Nombre" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   sql = sql & "NumeroCuentaBancaria" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "CodigoContribuyente" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   sql = sql & "NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   sql = sql & "FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & ", "

   sql = sql & gDefDatabase.getFN_TIMESTAMP() & " timestamp )"
   gDbUtil.Execute gDefDatabase.Conexion, sql
   sAddPrimaryKeyAndOtherConstrains
   sAllowCeroLengthFields
   sModifyOtherPropertiesOfFields
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

