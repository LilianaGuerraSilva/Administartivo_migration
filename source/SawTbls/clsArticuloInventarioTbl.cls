VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsArticuloInventarioTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsArticuloInventarioTbl"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "ArticuloInventario"
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Function fPrimaryKeyExistMessage() As String
   fPrimaryKeyExistMessage = "Ya existe ConsecutivoCompania, Codigo"
End Function
Public Function GetTableName() As String
   GetTableName = "ArticuloInventario"
End Function
Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   gDbUtil.AddForeignKeySegunTablaPrimaria "Adm.LineaDeProducto", GetTableName(), "Nombre", _
         "LineaDeProducto", Not usarSoloUnElementoEnLaRelacion, _
         "ConsecutivoCompania", "ConsecutivoCompania", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "SAW.categoria", GetTableName(), "Descripcion", _
         "Categoria", Not usarSoloUnElementoEnLaRelacion, _
         "ConsecutivoCompania", "ConsecutivoCompania", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "Saw.unidadDeVenta", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Nombre", "UnidadDeVenta", Not deleteOnCascade
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania,Codigo)"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePrograma"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Marca"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CampoDefinible1"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CampoDefinible2"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CampoDefinible3"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CampoDefinible4"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CampoDefinible5"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoLote"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreOperador"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAllowCeroLengthFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sModifyOtherPropertiesOfFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaDeVencimiento", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaUltimaModificacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sModifyOtherPropertiesOfFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim insCamposMonedaExtranjera As clsCamposMonedaExtranjeraTbl
   Dim insProductoCompuesto As clsProductoCompuestoTbl
   Dim insExistenciaPorGrupo As clsExistenciaPorGrupoTbl
   Dim insCierreCostoDelPeriodoTbl As clsCierreCostoDelPeriodoTbl
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "Codigo " & gUtilSQL.getVarCharTypeForDB(30) & " CONSTRAINT nn" & GetTableName() & "Codigo NOT NULL, "
   SQL = SQL & "Descripcion " & gUtilSQL.getVarCharTypeForLargeTextForDB() & " CONSTRAINT nn" & GetTableName() & "Descripcio NOT NULL, "
   SQL = SQL & "LineaDeProducto " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "StatusdelArticulo " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "TipoDeArticulo " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "AlicuotaIva " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "PrecioSinIva " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PrecioConIva " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PrecioSinIva2 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PrecioConIva2 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PrecioSinIva3 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PrecioConIva3 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PrecioSinIva4 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PrecioConIva4 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PorcentajeBaseImponible " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "CostoUnitario " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "Existencia " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "CantidadMinima " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "CantidadMaxima " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "Categoria " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "TipoDeProducto " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "NombrePrograma " & gUtilSQL.getVarCharTypeForDB(15) & ", "
   SQL = SQL & "Marca " & gUtilSQL.getVarCharTypeForDB(30) & ", "
   SQL = SQL & "FechaDeVencimiento " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "UnidadDeVenta " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "CampoDefinible1 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "CampoDefinible2 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "CampoDefinible3 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "CampoDefinible4 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "CampoDefinible5 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "MeCostoUnitario " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "UnidadDeVentaSecundaria " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "CuentaContableIngreso " & gUtilSQL.getVarCharTypeForDB(30) & ", "
   SQL = SQL & "CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "PorcentajeComision " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "ExcluirDeComision " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ExcluirDeC NOT NULL, "
   SQL = SQL & "CantArtReservado " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "CodigoGrupo " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "MargenGanancia" & gUtilSQL.getNumericTypeForDB(12, 4) & ", "
   SQL = SQL & "MargenGanancia2" & gUtilSQL.getNumericTypeForDB(12, 4) & ", "
   SQL = SQL & "MargenGanancia3" & gUtilSQL.getNumericTypeForDB(12, 4) & ", "
   SQL = SQL & "MargenGanancia4" & gUtilSQL.getNumericTypeForDB(12, 4) & ", "
   SQL = SQL & "TipoArticuloInv" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "TipoArticu NOT NULL, "
   SQL = SQL & "CostoPromedio" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "FechaCierreActualizada" & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "RecalcularCierre" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "RecalcularCi NOT NULL, "
   SQL = SQL & "ComisionaPorcentaje" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ComisionaP NOT NULL, "
   SQL = SQL & "RecalcularCosto" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "RecalcularCo NOT NULL, "
   SQL = SQL & "CuentaCostoDeVenta" & gUtilSQL.getVarCharTypeForDB(30) & ", "
   SQL = SQL & "CuentaInventario" & gUtilSQL.getVarCharTypeForDB(30) & ", "
   SQL = SQL & "GastosAdmisibles" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MargenGananciaXLey" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MargenGananciaXLey2" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MargenGananciaXLey3" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MargenGananciaXLey4" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "Peso " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "ArancelesCodigo " & gUtilSQL.getVarCharTypeForDB(13) & ", "
   SQL = SQL & "TipoDeMercanciaProduccion" & gUtilSQL.getCharTypeForDB(1) & " DEFAULT '0', "
   SQL = SQL & "UsaBalanza" & gUtilSQL.getCharTypeForDB(1) & " DEFAULT 'N' , "
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
   Set insCamposMonedaExtranjera = New clsCamposMonedaExtranjeraTbl
   If insCamposMonedaExtranjera.fCreateTable(valShowMessage) Then
      fCreateTable = True
   End If
   Set insCamposMonedaExtranjera = Nothing
   fCreateTable = False
   Set insProductoCompuesto = New clsProductoCompuestoTbl
   If insProductoCompuesto.fCreateTable(valShowMessage) Then
      fCreateTable = True
   End If
   Set insProductoCompuesto = Nothing
   fCreateTable = False
   Set insExistenciaPorGrupo = New clsExistenciaPorGrupoTbl
   If insExistenciaPorGrupo.fCreateTable(valShowMessage) Then
      fCreateTable = True
   End If
   Set insExistenciaPorGrupo = Nothing
   Set insCierreCostoDelPeriodoTbl = New clsCierreCostoDelPeriodoTbl
   If insCierreCostoDelPeriodoTbl.fCreateTable(valShowMessage) Then
      fCreateTable = True
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fCreateTable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fCreateTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
