VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsUsuarioTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsUsuarioTbl"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Usuario"
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Function fPrimaryKeyExistMessage() As String
   fPrimaryKeyExistMessage = "Ya existe NombreDelUsuario"
End Function
Public Function GetTableName() As String
   GetTableName = "Usuario"
End Function
Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (NombreDelUsuario)"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NOMBREULTIMACOMPANIAUSADA"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NOMBREYAPELLIDO"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CARGO"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "EMAIL"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCOMPANIA"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESFACTURA"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESRESUMENDIARIOVENTAS"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCAJAREGISTRADORA"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCOTIZACION"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCLIENTE"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESAUTORIZACION"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESLLAMADA"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESVISITA"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCXC"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCOBRANZA"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESARTINVENTARIO"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESALMACEN"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESNOTAENTSALIDA"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESEXISTXALMACEN"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESTRANSFERENCIA"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESVENDEDOR"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESTABLAS"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESPARAMETROS"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESUSUARIO"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESDESPACHO"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESORDENDESERVICIO"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCURSO"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCXP"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESPAGO"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESPROVEEDOR"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESTABLARETENCION"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCONTRATO"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCUENTABANCARIA"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESMOVIMIENTOBANCARIO"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCONCEPTOBANCARIO"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESALICUOTAIVA"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCOMPRA"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCONTEOFISICO"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESPERIODO"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCUENTA"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCOMPROBANTE"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCATALOGOGENERAL"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESACTIVOFIJO"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCONCILIACION"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESREGLASDECONTABILIZACION"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESPLANTILLARET"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESARCV"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESRA"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESGESTIONDECOBRANZA"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESPLANILLAFORMA00030"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESLOTEADM"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESANTICIPOCOBRADO"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESANTICIPOPAGADO"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESCOLOR"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESTALLA"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESGRUPOTC"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OpcionesCajeroFactura"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OpcionesCajeroCliente"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OpcionesCajeroArtInventario"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OpcionesCajeroCaja"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OpcionesExportarImportarSAW"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OpcionesParametrosActivoFijo"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESADELANTOS"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "OPCIONESRENDICIONES"
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
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "NOMBREDELUSUARIO " & gUtilSQL.getVarCharTypeForDB(10) & " CONSTRAINT nn" & GetTableName() & "NombreDelU NOT NULL, "
   SQL = SQL & "STATUS " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "Status NOT NULL, "
   SQL = SQL & "NOMBREULTIMACOMPANIAUSADA " & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "FldPassword " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "NOMBREYAPELLIDO " & gUtilSQL.getVarCharTypeForDB(40) & ", "
   SQL = SQL & "CARGO " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "ESSUPERVISOR " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "EsSupervis NOT NULL, "
   SQL = SQL & "EMAIL " & gUtilSQL.getVarCharTypeForDB(30) & ", "
   SQL = SQL & "USACONTABILIDAD " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "OPCIONESCOMPANIA " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NIVEL1 " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "Nivel1 NOT NULL, "
   SQL = SQL & "NIVEL2 " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "Nivel2 NOT NULL, "
   SQL = SQL & "NIVEL3 " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "Nivel3 NOT NULL, "
   SQL = SQL & "NIVEL4 " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "Nivel4 NOT NULL, "
   SQL = SQL & "OPCIONESFACTURA " & gUtilSQL.getVarCharTypeForDB(40) & ", "
   SQL = SQL & "OPCIONESRESUMENDIARIOVENTAS " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESCAJAREGISTRADORA " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESCOTIZACION " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESCLIENTE " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESAUTORIZACION " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESLLAMADA " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESVISITA " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESCXC " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESCOBRANZA " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESARTINVENTARIO " & gUtilSQL.getVarCharTypeForDB(40) & ", "
   SQL = SQL & "OPCIONESALMACEN " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESNOTAENTSALIDA " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESEXISTXALMACEN " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESTRANSFERENCIA " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESVENDEDOR " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESTABLAS " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESPARAMETROS " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESUSUARIO " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESDESPACHO " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESORDENDESERVICIO " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESCURSO " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESCXP " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESPAGO " & gUtilSQL.getVarCharTypeForDB(40) & ", "
   SQL = SQL & "OPCIONESPROVEEDOR " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESTABLARETENCION " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESCONTRATO " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESCUENTABANCARIA " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESMOVIMIENTOBANCARIO " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESCONCEPTOBANCARIO " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESALICUOTAIVA " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESCOMPRA " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESCONTEOFISICO " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESPERIODO " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESCUENTA " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESCOMPROBANTE " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESCATALOGOGENERAL " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESACTIVOFIJO " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESCONCILIACION " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESREGLASDECONTABILIZACION " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESPLANTILLARET " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESARCV " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESRA " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESGESTIONDECOBRANZA " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESPLANILLAFORMA00030 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESLOTEADM " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESANTICIPOCOBRADO " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESANTICIPOPAGADO " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "MENUDEBALANCES " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "MenuDeBala NOT NULL, "
   SQL = SQL & "MENUDEINFORMES " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "MenuDeInfo NOT NULL, "
   SQL = SQL & "ReiniciarTablasTemporales" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ReinciarTa NOT NULL, "
   SQL = SQL & "EsUsuarioTipoCajero" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "EsUsuarioT NOT NULL, "
   SQL = SQL & "OpcionesCajeroFactura" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OpcionesCajeroCliente" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OpcionesCajeroArtInventario" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OpcionesCajeroCaja" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESCOLOR " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESTALLA " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESGRUPOTC " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OpcionesExportarImportarSAW" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OpcionesParametrosActivoFijo" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESADELANTOS" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OPCIONESRENDICIONES" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "FECHAULTIMAMODIFICACION " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & gDefDatabase.getFN_TIMESTAMP() & " timestamp )"
   gDbUtil.Execute gDefDatabase.Conexion, SQL
   sAddPrimaryKeyAndOtherConstrains
   sAllowCeroLengthFields
   If valShowMessage Then
      gMessage.exito "La Tabla '" & CM_MESSAGE_NAME & "' fue creada exitosamente"
   End If
   fCreateTable = True
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fCreateTable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "fCreateTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Sub sDropTable()
   On Error GoTo h_ERROR
   gDbUtil.ExecuteDropTable GetTableName, True
   gMessage.ProgrammerMessage "La Tabla '" & CM_MESSAGE_NAME & "' fue borrada exitosamente"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sDropTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

