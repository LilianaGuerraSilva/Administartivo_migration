VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsbusinessintelligenceVistas"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsBusinessIntelligenceVistas"
Private Const CM_MESSAGE_NAME As String = "Vista BusinessIntelligence"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Public Function GetViewNameBIDimensionVentas_B1() As String
   GetViewNameBIDimensionVentas_B1 = "IGV_BIDimensionVentas_B1"
End Function

Public Function fSqlViewBIDimensionVentas(ByVal valEsSistemaInterno As Boolean) As String
   Dim vSQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim vSQLWhere As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd("", "factura.StatusFactura", enum_StatusFactura.eSF_EMITIDA)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
  
    
    vSQL = ""
    vSQL = " SELECT "
    vSQL = vSQL & "dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania,"
    vSQL = vSQL & "dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo,"
    vSQL = vSQL & "dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Categoria,"
    vSQL = vSQL & "dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto,"
    vSQL = vSQL & gUtilSQL.DfSQLCaseIfForEnum("dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.TipoDeProducto", enum_TipoDeProducto.eTD_TP_NUEVO, gEnumProyecto.fEnumTipoDeProductoToStringInArray, "TipoDeProducto") & ","
    vSQL = vSQL & "dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.UnidadDeVentaSecundaria, "
    vSQL = vSQL & "dbo.vendedor.Codigo AS CodigoVendedor,"
    vSQL = vSQL & "dbo.vendedor.Nombre AS Vendedor,"
    vSQL = vSQL & "dbo.cliente.Codigo AS CodigoCliente,"
    vSQL = vSQL & "dbo.cliente.Nombre AS Cliente,"
    vSQL = vSQL & "dbo.cliente.Ciudad,"
    vSQL = vSQL & "dbo.cliente.SectorDeNegocio,"
    vSQL = vSQL & "dbo.renglonFactura.TotalRenglon AS Monto,"
    vSQL = vSQL & "dbo.renglonFactura.Cantidad AS Unidades,"
    vSQL = vSQL & "(dbo.renglonFactura.Cantidad * dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.UnidadDeVentaSecundaria) AS UnidadesEstadisticasTot,"
    vSQL = vSQL & "YEAR(dbo.factura.Fecha) AS Ano,"
    vSQL = vSQL & "MONTH(dbo.factura.Fecha) AS Mes,"
    vSQL = vSQL & "DAY(dbo.factura.Fecha) AS Dia,"
    vSQL = vSQL & "dbo.factura.Fecha, "
    vSQL = vSQL & "dbo.cliente.ZonaDeCobranza, "
    If (valEsSistemaInterno) Then
    vSQL = vSQL & "dbo.vendedor.TipoDeVendedor,"
    End If
    vSQL = vSQL & AsignacionTipoTrimestre("dbo.factura.Fecha")
    vSQL = vSQL & "FROM dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1 "
    vSQL = vSQL & "INNER JOIN  dbo.factura ON "
    vSQL = vSQL & "dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = dbo.factura.ConsecutivoCompania "
    vSQL = vSQL & "INNER JOIN dbo.renglonFactura ON "
    vSQL = vSQL & "dbo.factura.ConsecutivoCompania = dbo.renglonFactura.ConsecutivoCompania AND "
    vSQL = vSQL & "dbo.factura.Numero = dbo.renglonFactura.NumeroFactura AND dbo.factura.TipoDeDocumento = dbo.renglonFactura.TipoDeDocumento AND "
    vSQL = vSQL & "dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = dbo.renglonFactura.Articulo AND "
    vSQL = vSQL & "dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = dbo.renglonFactura.Serial AND "
    vSQL = vSQL & "dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = dbo.renglonFactura.Rollo "
    vSQL = vSQL & "INNER JOIN    dbo.cliente ON "
    vSQL = vSQL & " dbo.factura.ConsecutivoCompania = dbo.cliente.ConsecutivoCompania And dbo.factura.CodigoCliente = dbo.cliente.Codigo "
    vSQL = vSQL & "INNER JOIN    dbo.vendedor ON "
    vSQL = vSQL & " dbo.factura.ConsecutivoCompania = dbo.vendedor.ConsecutivoCompania And dbo.factura.CodigoVendedor = dbo.vendedor.Codigo "
    vSQL = vSQL & vSQLWhere
   fSqlViewBIDimensionVentas = vSQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewBIDimensionVentas", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function GetViewNameBIDimensionCotizacion_B1() As String
   GetViewNameBIDimensionCotizacion_B1 = "IGV_BIDimensionCotizacion_B1"
End Function

Public Function fSqlViewBIDimensionCotizacion(ByVal valEsSistemaInterno As Boolean) As String
   Dim vSQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim vSQLWhere As String
   On Error GoTo h_ERROR
     Set gEnumProyecto = New clsEnumAdministrativo
    vSQL = ""
    vSQL = " SELECT "
    vSQL = vSQL & "   dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania,"
    vSQL = vSQL & "   dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo,"
    vSQL = vSQL & "   dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Categoria,"
    vSQL = vSQL & "   dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.TipoArticuloInv,"
    vSQL = vSQL & "   dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.TipoDeArticulo,"
    vSQL = vSQL & gUtilSQL.DfSQLCaseIfForEnum("dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.TipoDeProducto", enum_TipoDeProducto.eTD_TP_NUEVO, gEnumProyecto.fEnumTipoDeProductoToStringInArray, "TipoDeProducto") & ","
    vSQL = vSQL & "dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.UnidadDeVentaSecundaria, "
    vSQL = vSQL & "   dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto,"
    vSQL = vSQL & "   dbo.cotizacion.Fecha,"
    vSQL = vSQL & "   YEAR(dbo.cotizacion.Fecha) AS Ano,"
    vSQL = vSQL & "   MONTH(dbo.cotizacion.Fecha) AS Mes,"
    vSQL = vSQL & "   DAY(dbo.cotizacion.Fecha) AS Dia, "
    vSQL = vSQL & AsignacionTipoTrimestre("dbo.cotizacion.Fecha") & " , "
    vSQL = vSQL & "   Adm.Vendedor.Codigo AS CodigoVendedor,"
    vSQL = vSQL & "   Adm.Vendedor.Nombre AS Vendedor,"
    If (valEsSistemaInterno) Then
    vSQL = vSQL & "   Adm.Vendedor.TipoDeVendedor,"
    End If
    vSQL = vSQL & "   dbo.cliente.Codigo AS CodigoCliente,"
    vSQL = vSQL & "   dbo.cliente.Nombre AS Cliente,"
    vSQL = vSQL & "   dbo.cliente.Ciudad,"
    vSQL = vSQL & "   dbo.cliente.SectorDeNegocio,"
    vSQL = vSQL & "   dbo.cotizacion.Contacto,"
    vSQL = vSQL & "   dbo.cotizacion.Moneda,"
    vSQL = vSQL & "   dbo.cotizacion.NombreOperador,"
    vSQL = vSQL & "   dbo.renglonCotizacion.Cantidad AS Unidades,"
    vSQL = vSQL & "   dbo.renglonCotizacion.TotalRenglon AS Monto,"
    vSQL = vSQL & "   CASE WHEN  dbo.factura.NoCotizacionDeOrigen IS NULL THEN 'Por Facturar' ELSE 'Facturada'  END  AS Estado,"
    vSQL = vSQL & "   (dbo.renglonCotizacion.Cantidad * dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.UnidadDeVentaSecundaria) AS UnidadesEstadisticasTot"
    vSQL = vSQL & "   FROM dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1"
    vSQL = vSQL & "   INNER JOIN      dbo.cotizacion ON"
    vSQL = vSQL & "   dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = dbo.cotizacion.ConsecutivoCompania"
    vSQL = vSQL & "   INNER JOIN      dbo.renglonCotizacion ON"
    vSQL = vSQL & "   dbo.cotizacion.ConsecutivoCompania = dbo.renglonCotizacion.ConsecutivoCompania AND"
    vSQL = vSQL & "   dbo.cotizacion.Numero = dbo.renglonCotizacion.NumeroCotizacion AND"
    vSQL = vSQL & "   dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = dbo.renglonCotizacion.CodigoArticulo"
    vSQL = vSQL & "   INNER JOIN    dbo.cliente ON"
    vSQL = vSQL & "   dbo.cotizacion.ConsecutivoCompania = dbo.cliente.ConsecutivoCompania And dbo.cotizacion.CodigoCliente = dbo.cliente.Codigo"
    vSQL = vSQL & "   INNER JOIN    Adm.Vendedor ON"
    vSQL = vSQL & "   dbo.cotizacion.ConsecutivoCompania = Adm.Vendedor.ConsecutivoCompania And dbo.cotizacion.ConsecutivoVendedor = Adm.Vendedor.Consecutivo"
    vSQL = vSQL & "   LEFT JOIN dbo.factura on"
    vSQL = vSQL & "   dbo.cotizacion.ConsecutivoCompania = dbo.renglonCotizacion.ConsecutivoCompania AND"
    vSQL = vSQL & "   dbo.cotizacion.Numero = dbo.factura.NoCotizacionDeOrigen"
   fSqlViewBIDimensionCotizacion = vSQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewBIDimensionCotizacion", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function AsignacionTipoTrimestre(ByVal valTabla As String) As String
   Dim vSQL As String
   On Error GoTo h_ERROR
    vSQL = " (CASE  WHEN  MONTH(" & valTabla & ") <= 3 THEN 'Primer' "
    vSQL = vSQL & " WHEN MONTH(" & valTabla & ") > 3 AND  MONTH(" & valTabla & ") < 6 THEN 'Segundo'"
    vSQL = vSQL & "  WHEN  MONTH(" & valTabla & ") > 6 AND MONTH(" & valTabla & ") <= 9  THEN 'Tercer' ELSE 'Cuarto' END ) AS Trimestre "
    AsignacionTipoTrimestre = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "AsignacionTipoTrimestre", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
