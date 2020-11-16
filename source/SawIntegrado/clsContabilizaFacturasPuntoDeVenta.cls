VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsContabilizaFacturasPuntoDeVenta"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsContabilizaFacturasPuntoDeVenta"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Contabiliza Lote De Facturas de Punto De Venta"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Sub sEjecutaContabilizacionDeFacturas()
   Dim factura As clsFacturaNavigator
   Dim vSQL As String
   Dim rsFacturasAContabilizar As ADODB.Recordset
   On Error GoTo h_ERROR
   Set factura = New clsFacturaNavigator
   factura.fSearchForContabilizarRecords "factura.StatusFactura <>  " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & " AND factura.TipoDeDocumento =" & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL), vSQL
   gProyReglasDeContabilizacion.sSetEditaComprobanteDespuesDeInsertar eCG_FACTURA, False
   Set rsFacturasAContabilizar = New ADODB.Recordset
   If gDbUtil.fOpenRecordset(rsFacturasAContabilizar, vSQL, gDefDatabase.Conexion) Then
      If gDbUtil.fRecordCount(rsFacturasAContabilizar) > 0 Then
         If Not gProyReglasDeContabilizacion.fLasCuentasDeReglasDeContabilizacionEstanCompletas(gEnumProyectoWincont.strComprobanteGeneradoPorToNum(gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_FACTURA)), gProyUsuarioActual.GetUsaContabilidad, 0) Then
            gDbUtil.sDestroyRecordSet rsFacturasAContabilizar
            Set rsFacturasAContabilizar = Nothing
            GoTo h_EXIT
         End If
         rsFacturasAContabilizar.MoveFirst
         While Not rsFacturasAContabilizar.EOF
            If factura.fSearchByNumeroTipoDocumento(rsFacturasAContabilizar("Numero").value, rsFacturasAContabilizar("TipoDeDocumento").value, True, True) Then
               factura.sContabilizaEstaFactura False, Contabilizar, False
            End If
            rsFacturasAContabilizar.MoveNext
            
         Wend
         Set factura = Nothing
      End If
   End If
   gDbUtil.sDestroyRecordSet rsFacturasAContabilizar
   Set rsFacturasAContabilizar = Nothing
   Set factura = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaContabilizacionDeFacturas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
