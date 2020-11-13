VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsControlDespachoVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsControlDespachoVista"
Private Const CM_MESSAGE_NAME As String = "Vista ControlDespacho"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetViewNameControlDespachoFactura() As String
   GetViewNameControlDespachoFactura = "IGV_ControlDespachoFactura"
End Function

Public Function fSqlViewControlDespachoFactura() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = "SELECT "
   vSQL = vSQL & " ControlDespacho.Numero, cotizacion.Fecha AS Fecha," & vbCrLf
   vSQL = vSQL & " ControlDespacho.CodigoCliente, Cliente.Nombre AS NombreCliente," & vbCrLf
   vSQL = vSQL & " SUM(detalledecontroldespacho.cantidaddespachada*articuloinventario.PrecioConIva) AS TotalCotizacion," & vbCrLf
   vSQL = vSQL & " ControlDespacho.ConsecutivoCompania, MAX(cotizacion.PorcentajeDescuento) AS PorcentajeDescuento," & vbCrLf
   vSQL = vSQL & " cotizacion.Contacto AS Contacto, cotizacion.CodigoVendedor AS CodigoVendedor, ControlDespacho.NombreOperador AS NombreOperador," & vbCrLf
   vSQL = vSQL & " SUM( (detalledecontroldespacho.cantidaddespachada*articuloinventario.PrecioConIva) - (detalledecontroldespacho.cantidaddespachada*articuloinventario.PrecioSinIva) ) AS TotalIVA," & vbCrLf
   vSQL = vSQL & " cotizacion.FechaDeRetiro AS FechaDeRetiro," & vbCrLf
   vSQL = vSQL & " cotizacion.UsarDireccionFiscal AS UsarDireccionFiscal, cotizacion.NoDirDespachoAimprimir AS NoDirDespachoAimprimir" & vbCrLf
   vSQL = vSQL & " ,SUM(CASE WHEN ArticuloInventario.AlicuotaIva = '0' THEN ArticuloInventario.PrecioSinIva ELSE 0 END) AS TotalMontoExento" & vbCrLf
   vSQL = vSQL & " ,SUM(CASE WHEN ArticuloInventario.AlicuotaIva <> '0' THEN ArticuloInventario.PrecioSinIva ELSE 0 END) AS TotalBaseImponible" & vbCrLf
   vSQL = vSQL & " FROM" & vbCrLf
   vSQL = vSQL & " ControlDespacho INNER JOIN detalledecontroldespacho ON" & vbCrLf
   vSQL = vSQL & " (ControlDespacho.ConsecutivoCompania = detalledecontroldespacho.ConsecutivoCompania" & vbCrLf
   vSQL = vSQL & " AND ControlDespacho.Numero = detalledecontroldespacho.NumeroControlDespacho)" & vbCrLf
   vSQL = vSQL & " INNER JOIN articuloinventario" & vbCrLf
   vSQL = vSQL & " ON (detalledecontroldespacho.ConsecutivoCompania = articuloinventario.ConsecutivoCompania" & vbCrLf
   vSQL = vSQL & " AND detalledecontroldespacho.CodigoArticulo = articuloinventario.Codigo)" & vbCrLf
   vSQL = vSQL & " INNER JOIN Cliente ON ControlDespacho.CodigoCliente = Cliente.Codigo AND ControlDespacho.ConsecutivoCompania = Cliente.ConsecutivoCompania" & vbCrLf
   vSQL = vSQL & " INNER JOIN Cotizacion ON (ControlDespacho.ConsecutivoCompania = Cotizacion.ConsecutivoCompania" & vbCrLf
   vSQL = vSQL & " AND ControlDespacho.NumeroCotizacion = Cotizacion.Numero)" & vbCrLf
   vSQL = vSQL & " WHERE ControlDespacho.StatusControl = " & gUtilSQL.fSimpleSqlValue(enum_StatusControlDespacho.eSCD_PorProcesar)
   vSQL = vSQL & " GROUP BY" & vbCrLf
   vSQL = vSQL & " ControlDespacho.Numero," & vbCrLf
   vSQL = vSQL & " cotizacion.Fecha, ControlDespacho.CodigoCliente, Cliente.Nombre," & vbCrLf
   vSQL = vSQL & " ControlDespacho.ConsecutivoCompania, cotizacion.Contacto, " & vbCrLf
   vSQL = vSQL & " cotizacion.CodigoVendedor," & vbCrLf
   vSQL = vSQL & " ControlDespacho.NombreOperador," & vbCrLf
   vSQL = vSQL & " cotizacion.FechaDeRetiro, cotizacion.UsarDireccionFiscal, cotizacion.NoDirDespachoAimprimir "

   fSqlViewControlDespachoFactura = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewControlDespachoFactura", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function GetViewNameDetalleControlDespachoFactura() As String
   GetViewNameDetalleControlDespachoFactura = "IGV_DetalleControlDespachoFactura"
End Function

Public Function fSqlViewDetalleControlDespachoFactura() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   
   vSQL = "SELECT "
   vSQL = vSQL & " DetalleDeControlDespacho.CodigoArticulo, ArticuloInventario.Descripcion," & vbCrLf
   vSQL = vSQL & " SUM(DetalleDeControlDespacho.CantidadDespachada) AS Cantidad, MAX(renglonCotizacion.PrecioSinIva) AS PrecioSinIva," & vbCrLf
   vSQL = vSQL & " MAX(renglonCotizacion.PrecioConIva) AS PrecioConIva, MAX(renglonCotizacion.PorcentajeDescuento) AS PorcentajeDescuento, renglonCotizacion.Serial, renglonCotizacion.Rollo, " & vbCrLf
   vSQL = vSQL & " SUM(DetalleDeControlDespacho.CantidadDespachada * renglonCotizacion.PrecioSinIva) AS TotalRenglon," & vbCrLf
   vSQL = vSQL & " renglonCotizacion.AlicuotaIVA, MAX(renglonCotizacion.PorcentajeBaseImponible) AS PorcentajeBaseImponible,DetalleDeControlDespacho.ConsecutivoCompania," & vbCrLf
   vSQL = vSQL & " DetalleDeControlDespacho.NumeroControlDespacho,"
   'vSQL = vSQL & " DetalleDeControlDespacho.ConsecutivoRenglon, "
   
   vSQL = vSQL & " ROW_NUMBER() OVER (PARTITION BY DetalleDeControlDespacho.ConsecutivoCompania,DetalleDeControlDespacho.NumeroControlDespacho "
   vSQL = vSQL & " ORDER BY DetalleDeControlDespacho.ConsecutivoCompania,DetalleDeControlDespacho.NumeroControlDespacho) AS ConsecutivoRenglon,"
   
   vSQL = vSQL & " MAX(renglonCotizacion.PorcentajeAlicuota) AS PorcentajeAlicuota," & vbCrLf
   vSQL = vSQL & " SUM(DetalleDeControlDespacho.CantidadDespachada) AS CantidadDespachada " & vbCrLf
   vSQL = vSQL & " ,MAX(renglonCotizacion.CantidadDespachada) AS Facturado"
   vSQL = vSQL & " ,MAX(renglonCotizacion.Cantidad) AS Cotizado"
   
   vSQL = vSQL & " FROM DetalleDeControlDespacho INNER JOIN ArticuloInventario ON" & vbCrLf
   vSQL = vSQL & " (DetalleDeControlDespacho.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania" & vbCrLf
   vSQL = vSQL & " AND DetalleDeControlDespacho.CodigoArticulo = ArticuloInventario.Codigo )" & vbCrLf
   vSQL = vSQL & " INNER JOIN ControlDespacho ON" & vbCrLf
   vSQL = vSQL & " (ControlDespacho.ConsecutivoCompania = DetalleDeControlDespacho.ConsecutivoCompania" & vbCrLf
   vSQL = vSQL & " AND ControlDespacho.Numero = DetalleDeControlDespacho.NumeroControlDespacho)" & vbCrLf
   vSQL = vSQL & " INNER JOIN renglonCotizacion ON" & vbCrLf
   vSQL = vSQL & " (DetalleDeControlDespacho.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania" & vbCrLf
   vSQL = vSQL & " AND SUBSTRING(DetalleDeControlDespacho.NumeroControlDespacho,1,LEN(renglonCotizacion.NumeroCotizacion)) = renglonCotizacion.NumeroCotizacion" & vbCrLf
   vSQL = vSQL & " AND DetalleDeControlDespacho.CodigoArticulo = renglonCotizacion.CodigoArticulo)" & vbCrLf
   vSQL = vSQL & " GROUP BY" & vbCrLf
   vSQL = vSQL & " DetalleDeControlDespacho.CodigoArticulo, ArticuloInventario.Descripcion," & vbCrLf
   vSQL = vSQL & " renglonCotizacion.Serial ,renglonCotizacion.Rollo," & vbCrLf
   vSQL = vSQL & " renglonCotizacion.AlicuotaIVA, DetalleDeControlDespacho.ConsecutivoCompania,DetalleDeControlDespacho.NumeroControlDespacho,renglonCotizacion.PorcentajeAlicuota" & vbCrLf

   fSqlViewDetalleControlDespachoFactura = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewDetalleControlDespachoFactura", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function



