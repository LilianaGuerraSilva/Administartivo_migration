VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsClienteTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit


Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsClienteTbl"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Cliente"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetTableName() As String
   GetTableName = "Cliente"
End Function

Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   gDbUtil.AddForeignKeySegunTablaPrimaria "Comun.Ciudad", GetTableName(), "", _
         "", usarSoloUnElementoEnLaRelacion, "NombreCiudad", "Ciudad", _
         Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "Saw.ZonaCobranza", GetTableName(), "Nombre", _
         "ZonaDeCobranza", Not usarSoloUnElementoEnLaRelacion, _
         "ConsecutivoCompania", "ConsecutivoCompania", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "Comun.SectorDeNegocio", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Descripcion", "SectorDeNegocio", Not deleteOnCascade
  
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania,Codigo)"
   'gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT u_ClienteNombre UNIQUE(ConsecutivoCompania, Nombre)"
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT u_ClienteConsecutivo UNIQUE(" & _
         "ConsecutivoCompania" & ", " & "Consecutivo" & ")"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NumeroRif"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NumeroNit"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Direccion"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "ZonaPostal"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Telefono"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Fax"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Contacto"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "RazonInactividad"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Email"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "TextoDelAviso"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CuentaContableCxc"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CuentaContableIngresos"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CuentaContableAnticipo"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoLote"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreOperador"
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

Public Function fCreateTable(ByVal valShowMessage As Boolean, ByVal valEsSistemaParaIG As Boolean) As Boolean
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania" & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "Consecutivo" & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "Codigo" & gUtilSQL.getVarCharTypeForDB(10) & " CONSTRAINT nn" & GetTableName() & "Codigo NOT NULL, "
   SQL = SQL & "Nombre" & gUtilSQL.getVarCharTypeForDB(160) & " CONSTRAINT nn" & GetTableName() & "Nombre NOT NULL, "
   SQL = SQL & "NumeroRIF" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NumeroNit" & gUtilSQL.getVarCharTypeForDB(12) & ", "
   SQL = SQL & "Direccion" & gUtilSQL.getVarCharTypeForDB(255) & ", "
   SQL = SQL & "Ciudad" & gUtilSQL.getVarCharTypeForDB(100) & ", "
   SQL = SQL & "ZonaPostal" & gUtilSQL.getVarCharTypeForDB(7) & ", "
   SQL = SQL & "Telefono" & gUtilSQL.getVarCharTypeForDB(40) & ", "
   SQL = SQL & "Fax" & gUtilSQL.getVarCharTypeForDB(25) & ", "
   SQL = SQL & "Status" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "Contacto" & gUtilSQL.getVarCharTypeForDB(35) & ", "
   SQL = SQL & "ZonaDeCobranza" & gUtilSQL.getVarCharTypeForDB(100) & ", "
   SQL = SQL & "CodigoVendedor" & gUtilSQL.getVarCharTypeForDB(5) & ", "
   SQL = SQL & "ConsecutivoVendedor" & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "ConVen NOT NULL, "
   SQL = SQL & "RazonInactividad" & gUtilSQL.getVarCharTypeForDB(35) & ", "
   SQL = SQL & "Email" & gUtilSQL.getVarCharTypeForDB(100) & ", "
   SQL = SQL & "ActivarAvisoAlEscoger" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ActivarAvi NOT NULL, "
   SQL = SQL & "TextoDelAviso" & gUtilSQL.getVarCharTypeForDB(150) & ", "
   SQL = SQL & "CuentaContableCxc " & gUtilSQL.getVarCharTypeForDB(30) & ", "
   SQL = SQL & "CuentaContableIngresos " & gUtilSQL.getVarCharTypeForDB(30) & ", "
   SQL = SQL & "CuentaContableAnticipo " & gUtilSQL.getVarCharTypeForDB(30) & ", "
      SQL = SQL & "InfoGalac " & gUtilSQL.getCharTypeForDB(1) & ", "
   
   SQL = SQL & "SectorDeNegocio " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "NivelDePrecio " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "Origen " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "DiaCumpleanos " & gUtilSQL.getNumericTypeForDB(4, 0) & ", "
   SQL = SQL & "MesCumpleanos " & gUtilSQL.getNumericTypeForDB(4, 0) & ", "
   SQL = SQL & "CorrespondenciaXenviar " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "Correspond NOT NULL, "
   SQL = SQL & "EsExtranjero " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "EsExtranje NOT NULL, "
   SQL = SQL & "ClienteDesdeFecha" & gUtilSQL.getDateTypeForDB() & ", "
      SQL = SQL & "AQueSeDedicaElCliente " & gUtilSQL.getVarCharTypeForDB(100) & ", "
   SQL = SQL & "NombreOperador" & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "FechaUltimaModificacion" & gUtilSQL.getDateTypeForDB & ", "
   SQL = SQL & "TipoDocumentoIdentificacion" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "TipoDeContribuyente" & gUtilSQL.getCharTypeForDB(1) & " , "
   SQL = SQL & "CampoDefinible1" & gUtilSQL.getVarCharTypeForDB(60) & ", "
   SQL = SQL & gDefDatabase.getFN_TIMESTAMP() & " timestamp )"
   gDbUtil.Execute gDefDatabase.Conexion, SQL
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

