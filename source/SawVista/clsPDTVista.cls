VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsPDTVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsPDTVista"
Private Const CM_MESSAGE_NAME As String = "Vista PDT"
Private gEnumProyecto As clsEnumAdministrativo

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetViewNamePDTVentasRentaMensual() As String
   GetViewNamePDTVentasRentaMensual = "IGV_PDTVentasRentaMensual"
End Function

Public Function fViewPDTVentasRentaMensual() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = fSQLViewVentasFacturaRentaMensual(False)
   vSQL = vSQL & " UNION " & fSQLViewVentasCxCRentaMensual
   vSQL = vSQL & " UNION " & fSQLViewVentasFacturaRentaMensual(True)
h_EXIT: On Error GoTo 0
   fViewPDTVentasRentaMensual = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fViewPDTVentasRentaMensual", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLViewVentasFacturaRentaMensual(ByVal valEsParaResumenDiarioDeVentas As Boolean) As String
   Dim vSQL As String
   Dim insCobranzaVista As clsCobranzaVista
   Dim insFacturaVista As clsFacturaVista
   Dim sqlWhere As String
   Dim sqlTipoTransaccion As String
   Dim sqlAlicuotaExenta As String
   Dim sqlAlicuotaG As String
   Dim sqlVtaInterna As String
   Dim sqlVtaExportacion As String
   On Error GoTo h_ERROR
   Set insCobranzaVista = New clsCobranzaVista
   Set insFacturaVista = New clsFacturaVista
   
   sqlTipoTransaccion = " AND (" & insFacturaVista.GetViewNameVentasGeneradasFactura & ".TIPODETRANSACCION" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO) & _
               " OR " & insFacturaVista.GetViewNameVentasGeneradasFactura & ".TIPODETRANSACCION " & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A02_COMPLEMENTO) & _
               " OR " & insFacturaVista.GetViewNameVentasGeneradasFactura & ".TIPODETRANSACCION " & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A03_ANULACION) & ")"

   sqlAlicuotaExenta = "ALICUOTAIVA " & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_EXENTO)
   sqlAlicuotaG = "ALICUOTAIVA " & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_ALICUOTAGENERAL)
   sqlVtaExportacion = "TIPODEVENTA " & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeVenta.eTD_EXPORTACION)
   sqlVtaInterna = " TIPODEVENTA " & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeVenta.eTD_EXPORTACION)

   If valEsParaResumenDiarioDeVentas Then
      sqlWhere = sqlWhere & insFacturaVista.GetViewNameVentasGeneradasFactura & ".STATUSFACTURA = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
      sqlWhere = sqlWhere & " AND " & insFacturaVista.GetViewNameVentasGeneradasFactura & ".TIPODEDOCUMENTO = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Else
      sqlWhere = sqlWhere & insFacturaVista.GetViewNameVentasGeneradasFactura & ".STATUSFACTURA = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
      sqlWhere = sqlWhere & " AND " & insFacturaVista.GetViewNameVentasGeneradasFactura & ".TIPODEDOCUMENTO <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   End If
   
   vSQL = "SELECT "
   vSQL = vSQL & "ConsecutivoCompania, "
   vSQL = vSQL & "Numero, "
   vSQL = vSQL & "Fecha, "
   vSQL = vSQL & "CambioABolivares, "
   vSQL = vSQL & "Moneda, "
   vSQL = vSQL & "TipoDeDocumento, "
   vSQL = vSQL & gUtilSQL.getIIF(sqlAlicuotaExenta, _
             "TotalSinIvaDelRenglonesConDescuento", "0", True) & " AS ventasExentas, "
   vSQL = vSQL & gUtilSQL.getIIF(sqlAlicuotaExenta & " AND " & sqlVtaInterna & sqlTipoTransaccion, _
             "TotalSinIvaDelRenglonesConDescuento", "0", True) & " AS VentasInternasNoGravadas, "
   vSQL = vSQL & gUtilSQL.getIIF(sqlVtaExportacion & sqlTipoTransaccion, _
             "TotalSinIvaDelRenglonesConDescuento", "0", True) & " AS VentasDeExportacion, "
   vSQL = vSQL & gUtilSQL.getIIF(sqlAlicuotaG & " AND " & sqlVtaInterna & sqlTipoTransaccion, _
             "TotalSinIvaDelRenglonesConDescuento", "0", True) & " AS VentasIntBaseAlicuotaGeneral, "
   vSQL = vSQL & gUtilSQL.getIIF(sqlAlicuotaG & " AND " & sqlVtaInterna & sqlTipoTransaccion, _
             "TotalIvaDelRenglonConElDescuento", "0", True) & " AS VentasIntIvaAlicuotaGeneral, "
   vSQL = vSQL & gUtilSQL.getIIF("DescuentoRenglon  > 0 OR DescuentoFactura > 0", gUtilSQL.getIIF(sqlAlicuotaG & " AND " & sqlVtaInterna & sqlTipoTransaccion, _
             "TotalDescuentoRenglon", "0", True), "0", True) & " AS DescRenglonVentasGeneral, "
   vSQL = vSQL & gUtilSQL.getIIF("DescuentoRenglon > 0 OR DescuentoFactura > 0", gUtilSQL.getIIF(sqlAlicuotaG & " AND " & sqlVtaInterna & sqlTipoTransaccion, _
             "TotalDescuentoFactura", "0", True), "0", True) & " AS DescIvaAlicuotaGeneralVentasInt, "
   vSQL = vSQL & gUtilSQL.getIIF(insFacturaVista.GetViewNameVentasGeneradasFactura & ".TIPODETRANSACCION " & _
             " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A04_AJUSTE), _
             "TotalIvaDelRenglonConElDescuento", "0", True) & " AS MontoEditableAjusteDebito "
   vSQL = vSQL & " FROM " & insFacturaVista.GetViewNameVentasGeneradasFactura
   vSQL = vSQL & " WHERE " & sqlWhere
   Set insCobranzaVista = Nothing
   Set insFacturaVista = Nothing
h_EXIT: On Error GoTo 0
   fSQLViewVentasFacturaRentaMensual = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLViewVentasFacturaRentaMensual", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLViewVentasCxCRentaMensual() As String
   Dim vSQL As String
   Dim insCxCVista As clsCxCVista
   Dim sqlWhere As String
   Dim sqlCxCCambio As String
   Dim sqlCxCMoneda As String
   On Error GoTo h_ERROR
   Set insCxCVista = New clsCxCVista
   vSQL = "SELECT "
   vSQL = vSQL & "ConsecutivoCompania, "
   vSQL = vSQL & "Numero, "
   vSQL = vSQL & "Fecha, "
   vSQL = vSQL & "CambioABolivares, "
   vSQL = vSQL & "Moneda, "
   vSQL = vSQL & "0 AS TipoDeDocumento, "
   vSQL = vSQL & "ventasExentas, "
   vSQL = vSQL & "VentasInternasNoGravadas, "
   vSQL = vSQL & "0 AS VentasDeExportacion, "
   vSQL = vSQL & "VentasIntBaseAlicuotaGeneral, "
   vSQL = vSQL & "VentasIntIvaAlicuotaGeneral, "
   vSQL = vSQL & "0 AS DescRenglonVentasGeneral, "
   vSQL = vSQL & "0 AS DescIvaAlicuotaGeneralVentasInt, "
   vSQL = vSQL & "0 AS MontoEditableAjusteDebito "
   vSQL = vSQL & " FROM " & insCxCVista.GetViewNameVentasGeneradasCxC
   Set insCxCVista = Nothing
h_EXIT: On Error GoTo 0
   fSQLViewVentasCxCRentaMensual = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLViewVentasCxCRentaMensual", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'Public Function GetViewNamePDTVentasRentaMensual() As String
'   GetViewNamePDTVentasRentaMensual = "IGV_PDTVentasRentaMensual"
'End Function
'
'Public Function fViewPDTVentasRentaMensual() As String
'   Dim vSQL As String
'   On Error GoTo h_ERROR
'   vSQL = fSQLViewVentasFacturaRentaMensual(False)
'   vSQL = vSQL & " UNION " & fSQLViewVentasCxCRentaMensual
'   vSQL = vSQL & " UNION " & fSQLViewVentasFacturaRentaMensual(True)
'h_EXIT: On Error GoTo 0
'   fViewPDTVentasRentaMensual = vSQL
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'         "fViewPDTVentasRentaMensual", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'
'Private Function fSQLViewVentasFacturaDetalleOperaciones(ByVal valEsParaResumenDiarioDeVentas As Boolean) As String
'   Dim vSQL As String
'   Dim insFacturaVista As clsFacturaVista
'   Dim sqlWhere As String
'   Dim sqlTipoTransaccion As String
'   Dim sqlAlicuotaExenta As String
'   Dim sqlAlicuotaG As String
'   Dim sqlVtaInterna As String
'   Dim sqlVtaExportacion As String
'   On Error GoTo h_ERROR
'   Set insFacturaVista = New clsFacturaVista
'   sqlTipoTransaccion = " AND (" & insFacturaVista.GetViewNameVentasGeneradasFactura & ".TIPODETRANSACCION" & _
'               " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A04_AJUSTE)
'   sqlAlicuotaG = "ALICUOTAIVA " & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_ALICUOTAGENERAL)
'   sqlVtaInterna = " TIPODEVENTA " & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeVenta.eTD_EXPORTACION)
'   If valEsParaResumenDiarioDeVentas Then
'      sqlWhere = sqlWhere & insFacturaVista.GetViewNameVentasGeneradasFactura & ".STATUSFACTURA = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
'      sqlWhere = sqlWhere & " AND " & insFacturaVista.GetViewNameVentasGeneradasFactura & ".TIPODEDOCUMENTO = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
'   Else
'      sqlWhere = sqlWhere & insFacturaVista.GetViewNameVentasGeneradasFactura & ".STATUSFACTURA = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
'      sqlWhere = sqlWhere & " AND " & insFacturaVista.GetViewNameVentasGeneradasFactura & ".TIPODEDOCUMENTO <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
'   End If
'
'   vSQL = "SELECT "
'   vSQL = vSQL & "ConsecutivoCompania, "
'   vSQL = vSQL & "Fecha, "
'   vSQL = vSQL & "CambioABolivares, "
'   vSQL = vSQL & "Moneda, "
'   vSQL = vSQL & "TipoDeDocumento, "
'   vSQL = vSQL & gUtilSQL.getIIF(sqlAlicuotaG & " AND " & sqlVtaInterna & sqlTipoTransaccion, _
'             "TotalSinIvaDelRenglonesConDescuento", "0", True) & " AS VentasIntBaseAlicuotaGeneral, "
'   vSQL = vSQL & gUtilSQL.getIIF(sqlAlicuotaG & " AND " & sqlVtaInterna & sqlTipoTransaccion, _
'             "TotalIvaDelRenglonConElDescuento", "0", True) & " AS VentasIntIvaAlicuotaGeneral, "
'   vSQL = vSQL & " FROM " & insFacturaVista.GetViewNameVentasGeneradasFactura
'   vSQL = vSQL & " WHERE " & sqlWhere
'   Set insFacturaVista = Nothing
'h_EXIT: On Error GoTo 0
'   fSQLViewVentasFacturaRentaMensual = vSQL
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'         "fSQLViewVentasFacturaDetalleOperaciones", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'
