VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsExportarImportar"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit
Private Const FOLDER_NAME_IMPORTACION_EXPORTACION As String = "ImpExp"
Private msNombreDelArchivo As String
Private mProgressBar As ProgressBar
Private mIsEntreFechas As Boolean
Private mFechaFinal As Date
Private mFechaInicial As Date
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsExportarImportar"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Exportar/Importar "
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Sub sEjecutarAccion(ByVal valAction As AccionSobreRecord, ByVal valEntidad As enum_EntidadAExportarOImportar, ByVal valNombreArchivo As String, ByVal valExtension As String, ByRef refProgressBar As ProgressBar, ByVal valGenerarCxCalImportar As Boolean, Optional ByRef refExito As Boolean, Optional ByVal valEsFacturaBorradores As Boolean = False, Optional ByVal valEntreFechas As Boolean = False, Optional ByVal dtpFechaInicial As Date, Optional ByVal dtpFechaFinal As Date, Optional ByRef lblporcentaje As Label, Optional ByRef refCallingWindow As Form, Optional ByRef reftxtProgreso As TextBox)
   Dim vSql As String
   Dim insExportar As clsExportarDatos
   Dim insImportarDatos As clsImportarDatosSAW
   Dim Borradores As String
   Dim NumFilasVector As Integer
   On Error GoTo h_ERROR
   Set insExportar = New clsExportarDatos
   Set insImportarDatos = New clsImportarDatosSAW
   If valEsFacturaBorradores Then
      Borradores = "Borradores de "
   Else
      Borradores = ""
   End If
   mIsEntreFechas = valEntreFechas
   If valEntreFechas Then
      mFechaInicial = dtpFechaInicial
      mFechaFinal = dtpFechaFinal
   End If
   If valAction = Exportar Then
      vSql = fSqlExportacionSegunEntidad(valEntidad, valEsFacturaBorradores, NumFilasVector)
      insExportar.sExportarLosDatosAlFile valNombreArchivo, valExtension, vSql, refProgressBar
      If valEntidad = eEAEI_FACTURA Then
        Dim valNombreArchivoRenglonCobro As String
        Dim PuntoInicial As Long
        PuntoInicial = gTexto.DfLen(valNombreArchivo) - 4
        valNombreArchivoRenglonCobro = gTexto.DfMid(valNombreArchivo, 1, PuntoInicial) & "RenglonCobro." & valExtension
        insExportar.sExportarLosDatosAlFile valNombreArchivoRenglonCobro, valExtension, fSQLExportarRenglonCobroDeFacturas, refProgressBar
      End If
      refExito = True
      gMessage.exito "¡Se ha exportado el modulo de " & Borradores & gEnumProyecto.enumEntidadAExportarOImportarToString(valEntidad) & " con exito!"
   Else
      insImportarDatos.sImportarModulosSaw valNombreArchivo, valExtension, refProgressBar, refExito, valEntidad, NumFilasVector, lblporcentaje, refCallingWindow, valEsFacturaBorradores, valGenerarCxCalImportar, reftxtProgreso
   End If
   Set insExportar = Nothing
   Set insImportarDatos = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR:
Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutarAccion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fRespaldaTodosLosArchivosEnUnZip(ByVal valPathOfTheFilesToCompress As String, ByVal valFilesToCompress As String, ByVal valDestinyDir As String, ByVal valDestinyZipFileName As String, ByRef refMapZip As Object) As Boolean
    Dim newZipClass As clsBinaryWorkZip
    On Error GoTo h_ERROR
    Set newZipClass = New clsBinaryWorkZip
    If gUtilFile.fExisteElFolder(valPathOfTheFilesToCompress) Then
      If newZipClass.fZipFilesUsingBWZip(refMapZip, valPathOfTheFilesToCompress, valFilesToCompress, valDestinyDir, valDestinyZipFileName, "", False) Then
         fRespaldaTodosLosArchivosEnUnZip = gUtilFile.fBorrarCarpeta(valPathOfTheFilesToCompress)
      End If
    Else
      fRespaldaTodosLosArchivosEnUnZip = False
    End If
    Set newZipClass = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fRespaldaTodosLosArchivosEnUnZip", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fDescomprimeLaCarpetaZip(ByRef refMapZip As Object, ByVal valChequearEspacioDisponibleEnDirectorioDestino As Boolean, ByVal valDestinationDir As String, ByVal valTamanoLimite, ByVal valZipFileToUncompress As String) As Boolean
    Dim newZipClass As clsBinaryWorkZip
    On Error GoTo h_ERROR
    Set newZipClass = New clsBinaryWorkZip
    If gUtilFile.fExisteElArchivo(valZipFileToUncompress) Then
      If newZipClass.fUnzipFilesUsingBWZip(refMapZip, valChequearEspacioDisponibleEnDirectorioDestino, valDestinationDir, valTamanoLimite, valZipFileToUncompress) Then
        fDescomprimeLaCarpetaZip = True
      End If
    Else
      fDescomprimeLaCarpetaZip = False
    End If
    Set newZipClass = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDescomprimeLaCarpetaZip", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLExportarFacturas(ByVal valExportarBorradores As Boolean) As String
   Dim SQL As String
   Dim sqlReplaceCondicionPago As String
   Dim sqlReplaceObservaciones
   On Error GoTo h_ERROR
   sqlReplaceCondicionPago = ""
   sqlReplaceCondicionPago = fReplace("factura.CondicionesDePago", gUtilSQL.DfChar(13), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceCondicionPago = fReplace(sqlReplaceCondicionPago, gUtilSQL.DfChar(12), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceCondicionPago = fReplace(sqlReplaceCondicionPago, gUtilSQL.DfChar(11), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceCondicionPago = fReplace(sqlReplaceCondicionPago, gUtilSQL.DfChar(10), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceCondicionPago = fReplace(sqlReplaceCondicionPago, gUtilSQL.DfChar(9), gUtilSQL.fSimpleSqlValue(""))
   
   sqlReplaceObservaciones = ""
   sqlReplaceObservaciones = fReplace("factura.Observaciones", gUtilSQL.DfChar(13), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceObservaciones = fReplace(sqlReplaceObservaciones, gUtilSQL.DfChar(12), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceObservaciones = fReplace(sqlReplaceObservaciones, gUtilSQL.DfChar(11), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceObservaciones = fReplace(sqlReplaceObservaciones, gUtilSQL.DfChar(10), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceObservaciones = fReplace(sqlReplaceObservaciones, gUtilSQL.DfChar(9), gUtilSQL.fSimpleSqlValue(""))
   
   SQL = "SELECT factura.Numero, "
   SQL = SQL & "factura.Fecha, "
   SQL = SQL & "factura.CodigoCliente, "
   SQL = SQL & "factura.CodigoVendedor, "
   SQL = SQL & sqlReplaceObservaciones & " AS Observaciones, "
   SQL = SQL & "factura.TotalMontoExento, "
   SQL = SQL & "factura.TotalBaseImponible, "
   SQL = SQL & "factura.TotalRenglones, "
   SQL = SQL & "factura.TotalIVA, "
   SQL = SQL & "factura.TotalFactura, "
   SQL = SQL & "factura.PorcentajeDescuento, "
   SQL = SQL & "factura.CodigoMoneda, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("factura.NivelDePrecio", enum_NivelDePrecio.eND_PRECIO_1, gEnumProyecto.fEnumNivelDePrecioToStringInArray(True, True), "NivelDePrecio") & ", "
   SQL = SQL & "factura.CodigoAlmacen, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("factura.StatusFactura", enum_StatusFactura.eSF_EMITIDA, gEnumProyecto.fEnumStatusFacturaToStringInArray(True, True), "StatusFactura") & ", "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, True), "TipoDeDocumento") & ", "
   SQL = SQL & "factura.Cancelada, "
   SQL = SQL & "factura.UsarDireccionFiscal, "
   SQL = SQL & "factura.NoDirDespachoAimprimir, "
   SQL = SQL & "factura.CambioABolivares, "
   SQL = SQL & "factura.MontoDelAbono, "
   SQL = SQL & "factura.FechaDeVencimiento, "
   SQL = SQL & sqlReplaceCondicionPago & " AS CondicionesDePago, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("factura.Talonario", enum_Talonario.eTa_TALONARIO1, gEnumProyecto.fEnumTalonarioToStringInArray(True, True), "Talonario") & ", "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("factura.FormaDeLaInicial", enum_FormaDeLaInicial.eFD_POR_MONTO, gEnumProyecto.fEnumFormaDeLaInicialToStringInArray(True, True), "FormaDeLaInicial") & ", "
   SQL = SQL & "factura.PorcentajeDeLaInicial, "
   SQL = SQL & "factura.NumeroDeCuotas, "
   SQL = SQL & "factura.MontoDeLasCuotas, "
   SQL = SQL & "factura.MontoUltimaCuota, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("factura.FormaDePago", enum_FormaDePago.eFP_CONTADO, gEnumProyecto.fEnumFormaDePagoToStringInArray(True, True), "FormaDePago") & ", "
   SQL = SQL & "factura.NumDiasDeVencimiento1aCuota, "
   SQL = SQL & "factura.EditarMontoCuota, "
   SQL = SQL & "factura.NumeroControl, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("factura.TipoDeTransaccion", enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO, gEnumProyecto.fEnumTransaccionFiscalToStringInArray(True, True), "TipoDeTransaccion") & ", "
   SQL = SQL & "factura.NumeroPlanillaExportacion, "
   SQL = SQL & "factura.NumeroFacturaAfectada, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("factura.TipoDeVenta", enum_TipoDeVenta.eTD_INTERNA, gEnumProyecto.fenumTipoDeVentaToStringInArray(True, True), "TipoDeVenta") & ", "
   SQL = SQL & "factura.MontoIvaRetenido, "
   SQL = SQL & "factura.FechaAplicacionRetIVA, "
   SQL = SQL & "factura.NumeroComprobanteRetIVA, "
   SQL = SQL & "factura.FechaComprobanteRetIVA, "
   SQL = SQL & "factura.SeRetuvoIVA, "
   SQL = SQL & "factura.FacturaConPreciosSinIva, "
   SQL = SQL & "factura.ReservarMercancia, "
   SQL = SQL & "factura.FechaDeRetiro, "
   SQL = SQL & "factura.CodigoAlmacen, "
   SQL = SQL & "factura.NoDirDespachoAimprimir, "
   SQL = SQL & "factura.VueltoDelCobroDirecto, "
   SQL = SQL & "factura.PorcentajeDescuento1, "
   SQL = SQL & "factura.PorcentajeDescuento2, "
   SQL = SQL & "factura.MontoDescuento1, "
   SQL = SQL & "factura.MontoDescuento2, "
   SQL = SQL & "factura.PorcentajeAlicuota1, "
   SQL = SQL & "factura.PorcentajeAlicuota2, "
   SQL = SQL & "factura.PorcentajeAlicuota3, "
   SQL = SQL & "factura.MontoIvaAlicuota1, "
   SQL = SQL & "factura.MontoIvaAlicuota2, "
   SQL = SQL & "factura.MontoIvaAlicuota3, "
   SQL = SQL & "factura.MontoGravableAlicuota1, "
   SQL = SQL & "factura.MontoGravableAlicuota2, "
   SQL = SQL & "factura.MontoGravableAlicuota3, "
   SQL = SQL & "factura.FechaDeFacturaAfectada, "
   SQL = SQL & "factura.AplicaDecretoIvaEspecial, "
   SQL = SQL & "factura.BaseImponibleIGTF, "
   SQL = SQL & "factura.IGTFML, "
   SQL = SQL & "factura.IGTFME, "
   SQL = SQL & "factura.AlicuotaIGTF, "
   SQL = SQL & "renglonFactura.Descripcion, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("renglonFactura.AlicuotaIVA", enum_TipoDeAlicuota.eTD_EXENTO, gEnumProyecto.fEnumTipoDeAlicuotaToStringInArray(True, True), "AlicuotaIVA") & ", "
   SQL = SQL & "renglonFactura.Cantidad, "
   SQL = SQL & "renglonFactura.PrecioSinIVA, "
   SQL = SQL & "renglonFactura.PrecioConIVA, "
   SQL = SQL & "renglonFactura.PorcentajeDescuento, "
   SQL = SQL & "renglonFactura.TotalRenglon, "
   SQL = SQL & "renglonFactura.PorcentajeBaseImponible, "
   SQL = SQL & "renglonFactura.CodigoVendedor1, "
   SQL = SQL & "renglonFactura.CodigoVendedor2, "
   SQL = SQL & "renglonFactura.CodigoVendedor3, "
   If gProyParametrosCompania.GetUsaCamposExtrasEnRenglonFactura Then
      SQL = SQL & "renglonFactura.CampoExtraEnRenglonFactura1, "
      SQL = SQL & "renglonFactura.CampoExtraEnRenglonFactura2, "
   Else
      SQL = SQL & "'' AS CampoExtraEnRenglonFactura1, "
      SQL = SQL & "'' AS CampoExtraEnRenglonFactura2, "
   End If
   SQL = SQL & "renglonFactura.Articulo "
   SQL = SQL & ", CASE WHEN factura.CodigoMonedaDeCobro = 'VED' THEN"
   SQL = SQL & "      'BOLÍVARES'"
   SQL = SQL & "   WHEN factura.CodigoMonedaDeCobro = 'USD' THEN"
   SQL = SQL & "         'DÓLARES'"
   SQL = SQL & "   WHEN factura.CodigoMonedaDeCobro = 'EUR' THEN"
   SQL = SQL & "      'EUROS'"
   SQL = SQL & "   WHEN factura.CodigoMonedaDeCobro = 'COP' THEN"
   SQL = SQL & "      'PESOS COLOMBIANOS'"
   SQL = SQL & "   WHEN factura.CodigoMonedaDeCobro = 'PEN' THEN"
   SQL = SQL & "      'NUEVOS SOLES'"
   SQL = SQL & "END"
   SQL = SQL & "   AS MonedaDeCobro "
   
   SQL = SQL & " FROM factura"
   SQL = SQL & " INNER JOIN renglonFactura ON "
   SQL = SQL & "(factura.TipoDeDocumento = "
   SQL = SQL & "renglonFactura.TipoDeDocumento)"
   SQL = SQL & " AND (factura.Numero = "
   SQL = SQL & "renglonFactura.NumeroFactura)"
   SQL = SQL & " AND (factura.ConsecutivoCompania = "
   SQL = SQL & "renglonFactura.ConsecutivoCompania)"
   SQL = SQL & " WHERE (factura.ConsecutivoCompania = " & mConsecutivoCompaniaActual & ")"
   SQL = SQL & " AND (factura.StatusFactura"
   If valExportarBorradores Then
      SQL = SQL & " = "
   Else
      SQL = SQL & " <> "
   End If
   SQL = SQL & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & ")"
   SQL = SQL & " AND (factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA) & " or "
   SQL = SQL & " factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL) & ")"
   If mIsEntreFechas Then
      SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("factura.fecha", mFechaInicial, mFechaFinal) & ")"
   End If
   SQL = SQL & " ORDER BY " & "factura.Numero"
h_EXIT: On Error GoTo 0
   fSQLExportarFacturas = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExportarFacturas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDeCxC() As String
   Dim SQL As String
   Dim sqlReplace As String
   On Error GoTo h_ERROR
   sqlReplace = ""
   sqlReplace = fReplace("cxC.Descripcion", gUtilSQL.DfChar(13), gUtilSQL.fSimpleSqlValue(""))
   sqlReplace = fReplace(sqlReplace, gUtilSQL.DfChar(12), gUtilSQL.fSimpleSqlValue(""))
   sqlReplace = fReplace(sqlReplace, gUtilSQL.DfChar(11), gUtilSQL.fSimpleSqlValue(""))
   sqlReplace = fReplace(sqlReplace, gUtilSQL.DfChar(10), gUtilSQL.fSimpleSqlValue(""))
   sqlReplace = fReplace(sqlReplace, gUtilSQL.DfChar(9), gUtilSQL.fSimpleSqlValue(""))

   SQL = "SELECT cxC.Numero, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxC.Status", enum_StatusDocumento.eSD_PORCANCELAR, gEnumProyecto.fEnumStatusCxCToStringInArray(True, True), "status") & ", "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxC.TipoCxC", enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, True), "tipoCxC") & ", "
   SQL = SQL & "cxC.CodigoCliente, "
   SQL = SQL & "cliente.Nombre, "
   SQL = SQL & "cxC.CodigoVendedor, "
   SQL = SQL & "vendedor.Nombre, "
   SQL = SQL & "cxC.Fecha, "
   SQL = SQL & "cxC.FechaCancelacion, "
   SQL = SQL & "cxC.FechaVencimiento, "
   SQL = SQL & "cxC.FechaAnulacion, "
   SQL = SQL & "cxC.MontoExento, "
   SQL = SQL & "cxC.MontoGravado, "
   SQL = SQL & "cxC.MontoIVA, "
   SQL = SQL & "cxC.MontoAbonado, "
   SQL = SQL & sqlReplace & " AS Descripcion, "
   SQL = SQL & "cxC.CodigoMoneda, "
   SQL = SQL & "cxC.CambioABolivares, "
   SQL = SQL & "cxC.CodigoCC, "
   SQL = SQL & "cxC.CentroDeCostos, "
   SQL = SQL & "cxC.SeRetuvoIva, "
   SQL = SQL & "cxC.NumeroDocumentoOrigen, "
   SQL = SQL & "cxC.NoAplicaParaLibroDeVentas, "
   SQL = SQL & "cxC.NumeroControl,"
   SQL = SQL & "cxC.NumeroComprobanteFiscal "
   SQL = SQL & " FROM ( cxC"
   SQL = SQL & " INNER JOIN vendedor"
   SQL = SQL & " ON ( cxC.CodigoVendedor = vendedor.Codigo"
   SQL = SQL & " ) AND ( cxC.ConsecutivoCompania = vendedor.ConsecutivoCompania"
   SQL = SQL & " )) INNER JOIN cliente"
   SQL = SQL & " ON ( cxC.CodigoCliente = cliente.Codigo"
   SQL = SQL & ") AND ( cxC.ConsecutivoCompania = cliente.ConsecutivoCompania"
   SQL = SQL & " ) WHERE cxC.ConsecutivoCompania = " & mConsecutivoCompaniaActual
   If mIsEntreFechas Then
      SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("cxC.Fecha", mFechaInicial, mFechaFinal) & ")"
   End If
   SQL = SQL & " ORDER BY cxC.Numero"
h_EXIT: On Error GoTo 0
   fSQLDeCxC = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDeCxC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDeClientes() As String
   Dim sqlReplace As String
   Dim sqlReplaceTextoDelAviso As String
   Dim SQL As String
   On Error GoTo h_ERROR
   sqlReplace = ""
   sqlReplace = fReplace("cliente.Direccion", gUtilSQL.DfChar(13), gUtilSQL.fSimpleSqlValue(""))
   sqlReplace = fReplace(sqlReplace, gUtilSQL.DfChar(12), gUtilSQL.fSimpleSqlValue(""))
   sqlReplace = fReplace(sqlReplace, gUtilSQL.DfChar(11), gUtilSQL.fSimpleSqlValue(""))
   sqlReplace = fReplace(sqlReplace, gUtilSQL.DfChar(10), gUtilSQL.fSimpleSqlValue(""))
   sqlReplace = fReplace(sqlReplace, gUtilSQL.DfChar(9), gUtilSQL.fSimpleSqlValue(""))
   
   sqlReplaceTextoDelAviso = ""
   sqlReplaceTextoDelAviso = fReplace("cliente.TextoDelAviso", gUtilSQL.DfChar(13), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceTextoDelAviso = fReplace(sqlReplaceTextoDelAviso, gUtilSQL.DfChar(12), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceTextoDelAviso = fReplace(sqlReplaceTextoDelAviso, gUtilSQL.DfChar(11), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceTextoDelAviso = fReplace(sqlReplaceTextoDelAviso, gUtilSQL.DfChar(10), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceTextoDelAviso = fReplace(sqlReplaceTextoDelAviso, gUtilSQL.DfChar(9), gUtilSQL.fSimpleSqlValue(""))
   
   SQL = "SELECT cliente.codigo, "
   SQL = SQL & "cliente.nombre, "
   SQL = SQL & "cliente.NumeroRIF, "
   SQL = SQL & "cliente.NumeroNIT, "
   SQL = SQL & "cliente.CuentaContableCxC, "
   SQL = SQL & "cliente.CuentaContableIngresos, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cliente.Status", enum_StatusCliente.eSC_ACTIVO, gEnumProyecto.fEnumStatusClienteToStringInArray(True), "StatusCliente") & ", "
   SQL = SQL & "cliente.Telefono, "
   SQL = SQL & "cliente.FAX, "
   SQL = SQL & sqlReplace & " AS Direccion, "
   SQL = SQL & "cliente.Ciudad, "
   SQL = SQL & "cliente.ZonaPostal, "
   SQL = SQL & "cliente.ZonaDeCobranza, "
   SQL = SQL & "cliente.SectorDeNegocio, "
   SQL = SQL & "cliente.CodigoVendedor, "
   SQL = SQL & "cliente.EsExtranjero, "
   SQL = SQL & "cliente.Email, "
   SQL = SQL & "cliente.Contacto, "
   SQL = SQL & "cliente.RazonInactividad, "
   SQL = SQL & "cliente.ActivarAvisoAlEscoger, "
   SQL = SQL & sqlReplaceTextoDelAviso & " AS TextoDelAviso, "
   SQL = SQL & "cliente.CuentaContableAnticipo, "
   SQL = SQL & "cliente.NivelDePrecio, "
   SQL = SQL & "cliente.Origen, "
   SQL = SQL & "cliente.ClienteDesdeFecha, "
   SQL = SQL & "cliente.TipoDeContribuyente "
   If gProyParametrosCompania.GetEsAsociadoEnCtaDeParticipacion Then
      SQL = SQL & ", cliente.DiaCumpleanos, "
      SQL = SQL & "cliente.MesCumpleanos, "
      SQL = SQL & "cliente.CorrespondenciaXEnviar"
   End If
   SQL = SQL & " FROM cliente"
   SQL = SQL & " WHERE " & "cliente.ConsecutivoCompania = " & mConsecutivoCompaniaActual
   SQL = SQL & " AND " & "cliente.Codigo <> " & gUtilSQL.fSimpleSqlValue(insCliente.getCodigoClienteResumen)
   If Not gMessage.Confirm("¿Desea incluir el Cliente Genérico en el proceso de exportación?") Then
      SQL = SQL & " AND " & "cliente.Codigo <> " & gUtilSQL.fSimpleSqlValue(gProyParametrosCompania.getCodigoGenericoCliente)
   End If
   If mIsEntreFechas Then
      SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("cliente.FechaUltimaModificacion", mFechaInicial, mFechaFinal) & ")"
   End If
   SQL = SQL & " ORDER BY " & "cliente.Codigo"
h_EXIT: On Error GoTo 0
      fSQLDeClientes = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDeClientes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDeProveedores() As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT proveedor.CodigoProveedor, "
   SQL = SQL & "proveedor.NombreProveedor, "
   SQL = SQL & "proveedor.Contacto, "
   SQL = SQL & "proveedor.NumeroRIF, "
   SQL = SQL & "proveedor.NumeroNIT, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("proveedor.TipoDePersona", enum_TipodePersonaRetencion.eTP_PJ_DOMICILIADA, gEnumTablaRetencion.fEnumTipoDePersonaRetencionSiglasToStringInArray(True), "TipoCond") & ", "
   SQL = SQL & "proveedor.CodigoRetencionUsual, "
   SQL = SQL & "proveedor.Telefonos, "
   SQL = SQL & "proveedor.Direccion, "
   SQL = SQL & "proveedor.Fax, "
   SQL = SQL & "proveedor.email, "
   SQL = SQL & "proveedor.TipodeProveedor, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("proveedor.TipoDeProveedorDeLibrosFiscales", enum_TipoDeProveedorDeLibrosFiscales.eTD_CON_RIF, gEnumProyecto.fEnumProveedorFiscalToStringInArray(True), "ProveedorLibro") & ", "
   SQL = SQL & "proveedor.PorcentajeRetencionIva, "
   SQL = SQL & "proveedor.CuentaContableCxP, "
   SQL = SQL & "proveedor.CuentaContableGastos, "
   SQL = SQL & "proveedor.CuentaContableAnticipo, "
   SQL = SQL & "proveedor.Beneficiario, "
   SQL = SQL & "proveedor.UsarBeneficiarioImpCheq, "
   SQL = SQL & "proveedor.TipoDocumentoIdentificacion, "
   SQL = SQL & "proveedor.NumeroCuentaBancaria, "
   If (gProyParametrosCompania.GetRetieneImpuestoMunicipal()) Then
      SQL = SQL & "proveedor.CodigoContribuyente, "
      SQL = SQL & "proveedor.NumeroRUC, "
   End If
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("proveedor.Nombre"), gUtilSQL.fSimpleSqlValue(""), "proveedor.Nombre") & ", "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("proveedor.ApellidoPaterno"), gUtilSQL.fSimpleSqlValue(""), "proveedor.ApellidoPaterno") & ", "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("proveedor.ApellidoMaterno"), gUtilSQL.fSimpleSqlValue(""), "proveedor.ApellidoMaterno") & ", "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("proveedor.TipoDePersonaLibrosElectronicos"), gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDePersonaLibrosElectronicos.eTDPLE_JuridicoDomiciliado), "proveedor.TipoDePersonaLibrosElectronicos", True) & gUtilSQL.fAlias("TipoDePersonaLibrosElectronicos") & ", "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("proveedor.CodigoPaisResidencia"), gUtilSQL.fSimpleSqlValue(""), "proveedor.CodigoPaisResidencia", True) & gUtilSQL.fAlias("CodigoPaisResidencia") & ","
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("proveedor.CodigoConveniosSunat"), gUtilSQL.fSimpleSqlValue(""), "proveedor.CodigoConveniosSunat", True) & gUtilSQL.fAlias("CodigoConveniosSunat")
   SQL = SQL & " FROM Adm.proveedor"
   SQL = SQL & " WHERE proveedor.ConsecutivoCompania = " & mConsecutivoCompaniaActual
   If mIsEntreFechas Then
      SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("proveedor.FechaUltimaModificacion", mFechaInicial, mFechaFinal) & ")"
   End If
   SQL = SQL & " ORDER BY proveedor.CodigoProveedor"
h_EXIT: On Error GoTo 0
   fSQLDeProveedores = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDeProveedores", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDeCxP() As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   If gGlobalization.fEsCodigoPeru Then
      SQL = SQL & "cxP.NumeroSerie,"
      SQL = SQL & "cxP.NumeroDeDocumento,"
      SQL = SQL & "cxP.CodigoTipoDeComprobante,"
   Else
      SQL = SQL & "cxP.Numero, "
      SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxP.TipoDeCxP", enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, True), "tipoCxP") & ", "
   End If
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxP.Status", enum_StatusDocumento.eSD_PORCANCELAR, gEnumProyecto.fEnumStatusCxCToStringInArray(True, True), "status") & ", "
   SQL = SQL & "cxP.CodigoProveedor, "
   SQL = SQL & "proveedor.NombreProveedor, "
   SQL = SQL & "cxP.Fecha, "
   SQL = SQL & "cxP.FechaCancelacion, "
   SQL = SQL & "cxP.FechaVencimiento, "
   SQL = SQL & "cxP.FechaAnulacion, "
   SQL = SQL & "cxP.CodigoMoneda, "
   SQL = SQL & "cxP.CambioABolivares, "
   SQL = SQL & "cxP.AplicaParaLibrodeCompras, "
   SQL = SQL & "cxP.MontoExento, "
   SQL = SQL & "cxP.MontoGravado, "
   SQL = SQL & "cxP.MontoIva, "
   SQL = SQL & "cxP.MontoAbonado, "
   SQL = SQL & "cxP.MesDeAplicacion, "
   SQL = SQL & "cxP.AnoDeAplicacion, "
   SQL = SQL & "cxP.Observaciones, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxP.CreditoFiscal", enum_CreditoFiscal.eCF_DEDUCIBLE, gEnumProyecto.fEnumCreditoFiscalToStringInArray(True, True), "CreditoFiscal") & ", "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxP.TipoDeCompra", enum_TipoDeCompra.eTD_COMPRASEXENTAS, gEnumProyecto.fenumTipoCompraToStringInArray(True), "TipoDeCompra") & ", "
   SQL = SQL & "cxP.SeHizoLaRetencion, "
   SQL = SQL & "cxP.MontoGravableAlicuotaGeneral, "
   SQL = SQL & "cxP.MontoGravableAlicuota2, "
   SQL = SQL & "cxP.MontoGravableAlicuota3, "
   SQL = SQL & "cxP.MontoIVAAlicuotaGeneral, "
   SQL = SQL & "cxP.MontoIVAAlicuota2, "
   SQL = SQL & "cxP.MontoIVAAlicuota3, "
   SQL = SQL & "cxP.NumeroPlanillaDeImportacion, "
   SQL = SQL & "cxP.NumeroExpedienteDeImportacion, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxP.TipoDeTransaccion", enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO, gEnumProyecto.fEnumTransaccionFiscalToStringInArray(True, True), "TipoTransLF") & ", "
   SQL = SQL & "cxP.NumeroDeFacturaAfectada, "
   SQL = SQL & "cxP.NumeroControl, "
   SQL = SQL & "cxP.SeHizoLaRetencionIVA, "
   SQL = SQL & "cxP.NumeroComprobanteRetencion, "
   SQL = SQL & "cxP.FechaAplicacionRetIVA, "
   SQL = SQL & "cxP.PorcentajeRetencionAplicado, "
   SQL = SQL & "cxP.MontoRetenido, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxP.OrigenDeLaRetencion", enum_DondeSeEfectuaLaRetencionIVA.eDS_NO_RETENIDA, gEnumProyecto.fEnumDondeSeEfectuaLaRetencionIVAToStringInArray(True, True), "OrigenDeRetencion") & ", "
   SQL = SQL & "cxP.RetencionAplicadaEnPago, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxP.OrigenInformacionRetencion", enum_TipoDeContribuyenteIVA.eTD_FORMAL, gEnumProyecto.fEnumTipoDeContribuyenteToStringInArray(True, True), "TipoContribuyente") & ", "
   SQL = SQL & "cxP.CxPgeneradaPor, "
   SQL = SQL & "cxP.EsCxPhistorica, "
   SQL = SQL & "cxP.NumDiasDeVencimiento, "
   SQL = SQL & "cxP.NumeroDocOrigen, "
   SQL = SQL & "cxP.GenerarAsientoDeRetiroEnCuenta, "
   SQL = SQL & "cxP.SeContabilRetIva, "
   SQL = SQL & "cxP.DondeContabilRetIva,"
   SQL = SQL & "cxP.FechaAplicacionImpuestoMunicipal,"
   SQL = SQL & "cxP.NumeroDeclaracionAduana, "
   SQL = SQL & "cxP.FechaDeclaracionAduana, "
   SQL = SQL & "cxP.UsaPrefijoSerie, "
   SQL = SQL & "cxP.EsUnaCuentaATerceros, "
   SQL = SQL & "cxP.CodigoProveedorOriginalServicio, "
   SQL = SQL & "cxP.SeHizoLaDetraccion, "
   SQL = SQL & "cxP.AplicaIvaAlicuotaEspecial,"
   SQL = SQL & "cxP.MontoGravableAlicuotaEspecial1,"
   SQL = SQL & "cxP.MontoIVAAlicuotaEspecial1,"
   SQL = SQL & "cxP.PorcentajeIvaAlicuotaEspecial1,"
   SQL = SQL & "cxP.MontoGravableAlicuotaEspecial2,"
   SQL = SQL & "cxP.MontoIVAAlicuotaEspecial2,"
   SQL = SQL & "cxP.PorcentajeIvaAlicuotaEspecial2,"
   SQL = SQL & "cxP.DiaDeAplicacion, "
   SQL = SQL & "ISNULL(cxP.NumeroSerieDocAfectado,'') AS NumeroSerieDocAfectado, "
   SQL = SQL & "ISNULL(cxP.NumeroDeDocumentoAfectado,'') AS NumeroDeDocumentoAfectado, "
   SQL = SQL & "cxP.BaseImponibleIGTFML,"
   SQL = SQL & "cxP.AlicuotaIGTFML,"
   SQL = SQL & "cxP.MontoIGTFML"
   SQL = SQL & " FROM proveedor"
   SQL = SQL & " INNER JOIN cxP"
   SQL = SQL & " ON ( proveedor.CodigoProveedor = cxP.CodigoProveedor"
   SQL = SQL & " )  AND ( proveedor.ConsecutivoCompania = cxP.ConsecutivoCompania"
   SQL = SQL & " ) WHERE cxP.ConsecutivoCompania = " & mConsecutivoCompaniaActual
   If mIsEntreFechas Then
      SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("cxP.Fecha", mFechaInicial, mFechaFinal) & ")"
   End If
   SQL = SQL & " ORDER BY cxP.Numero"
   fSQLDeCxP = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDeCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLExportarVendedor() As String
    On Error GoTo h_ERROR
    Select Case gProyParametrosCompania.GetComisionesEnFacturaAsEnum
      Case eCEF_SobreTotalFactura: fSQLExportarVendedor = fSQLExportarVendedorSobreFactura(mIsEntreFechas, mFechaInicial, mFechaFinal)
      Case eCEF_SobreRenglones: fSQLExportarVendedor = fSQLExportarVendedorSobreRenglon(mIsEntreFechas, mFechaInicial, mFechaFinal)
      Case eCEF_SobreTotalFacturayRenglones: fSQLExportarVendedor = fSQLExportarVendedorSobreRenglon(mIsEntreFechas, mFechaInicial, mFechaFinal, True)
    End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExportarVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLArticulosAExportar() As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " SELECT "
   SQL = SQL & "articuloInventario.Codigo, "
   SQL = SQL & "articuloInventario.Descripcion, "
   SQL = SQL & "articuloInventario.LineaDeProducto, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("articuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE, gEnumProyecto.fEnumStatusArticuloToStringInArray(True), "StatusdelArticulo") & ", "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("articuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA, gEnumProyecto.fEnumTipoDeArticuloToStringInArray(True), "TipoArticulo") & ", "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("articuloInventario.AlicuotaIVA", enum_TipoDeAlicuota.eTD_EXENTO, gEnumProyecto.fEnumTipoDeAlicuotaToStringInArray(True), "TipoAAlicuota") & ", "
   If Not gProyParametrosCompania.GetUsaMonedaExtranjera Then
      SQL = SQL & "'" & mCodigoMonedaLocal & "' AS TIPOMONEDA, "
   Else
      SQL = SQL & "'" & mCodigoMonedaExtranjera & "' AS TIPOMONEDA, "
   End If
   SQL = SQL & "articuloInventario.PrecioSinIVA, "
   SQL = SQL & "articuloInventario.PrecioConIVA, "
   SQL = SQL & "articuloInventario.PrecioSinIVA2, "
   SQL = SQL & "articuloInventario.PrecioConIVA2, "
   SQL = SQL & "articuloInventario.PrecioSinIVA3, "
   SQL = SQL & "articuloInventario.PrecioConIVA3, "
   SQL = SQL & "articuloInventario.PrecioSinIVA4, "
   SQL = SQL & "articuloInventario.PrecioConIVA4, "
   SQL = SQL & "camposMonedaExtranjera.MePrecioSinIVA, "
   SQL = SQL & "camposMonedaExtranjera.MePrecioConIVA, "
   SQL = SQL & "camposMonedaExtranjera.MePrecioSinIVA2, "
   SQL = SQL & "camposMonedaExtranjera.MePrecioConIVA2, "
   SQL = SQL & "camposMonedaExtranjera.MePrecioSinIVA3, "
   SQL = SQL & "camposMonedaExtranjera.MePrecioConIVA3, "
   SQL = SQL & "camposMonedaExtranjera.MePrecioSinIVA4, "
   SQL = SQL & "camposMonedaExtranjera.MePrecioConIVA4, "
   SQL = SQL & "articuloInventario.PorcentajeBaseImponible, "
   SQL = SQL & "articuloInventario.CostoUnitario, "
   SQL = SQL & "articuloInventario.Existencia, "
   SQL = SQL & "articuloInventario.CantidadMinima, "
   SQL = SQL & "articuloInventario.CantidadMaxima, "
   SQL = SQL & "articuloInventario.TipoDeProducto, "
   SQL = SQL & "articuloInventario.NombrePrograma, "
   SQL = SQL & "articuloInventario.Marca, "
   SQL = SQL & "articuloInventario.FechaDeVencimiento, "
   SQL = SQL & "articuloInventario.UnidadDeVenta, "
   SQL = SQL & "articuloInventario.CampoDefinible1, "
   SQL = SQL & "articuloInventario.CampoDefinible2, "
   SQL = SQL & "articuloInventario.CampoDefinible3, "
   SQL = SQL & "articuloInventario.CampoDefinible4, "
   SQL = SQL & "articuloInventario.CampoDefinible5, "
   SQL = SQL & "articuloInventario.MeCostoUnitario, "
   SQL = SQL & "articuloInventario.UnidadDeVentaSecundaria, "
   SQL = SQL & "articuloInventario.CuentaContableIngreso, "
   SQL = SQL & "articuloInventario.PorcentajeComision, "
   SQL = SQL & "articuloInventario.ExcluirDeComision, "
   SQL = SQL & "articuloInventario.CantArtReservado, "
   SQL = SQL & "articuloInventario.CodigoGrupo, "
   SQL = SQL & "articuloInventario.TipoArticuloInv, "
   SQL = SQL & "articuloInventario.UsaBalanza, "
   SQL = SQL & "Color.CodigoColor, "
   SQL = SQL & "Color.DescripcionColor, "
   SQL = SQL & "Talla.CodigoTalla, "
   SQL = SQL & "Talla.DescripcionTalla, "
   SQL = SQL & "GrupoTallaColor.CodigoGrupo, "
   SQL = SQL & "GrupoTallaColor.Descripcion, "
   SQL = SQL & "GrupoTallaColor.UsaTalla, "
   SQL = SQL & "GrupoTallaColor.UsaColor, "
   SQL = SQL & "RenglonGrupoColor.CodigoGrupo, "
   SQL = SQL & "RenglonGrupoColor.CodigoColor, "
   SQL = SQL & "RenglonGrupoTalla.CodigoGrupo, "
   SQL = SQL & "RenglonGrupoTalla.CodigoTalla, "
   SQL = SQL & "ExistenciaPorGrupo.CodigoArticulo, "
   SQL = SQL & "ExistenciaPorGrupo.CodigoGrupo, "
   SQL = SQL & "ExistenciaPorGrupo.CodigoTalla, "
   SQL = SQL & "ExistenciaPorGrupo.CodigoColor, "
   SQL = SQL & "ExistenciaPorGrupo.existencia, "
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace("ExistenciaPorGrupo.Serial", gUtilSQL.fSimpleSqlValue("")) & ", "
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace("ExistenciaPorGrupo.Rollo", gUtilSQL.fSimpleSqlValue("")) & ", "
   SQL = SQL & "ExistenciaPorGrupo.CantReservada, "
   SQL = SQL & "ExistenciaPorGrupo.fecha, "
   SQL = SQL & "ExistenciaPorGrupo.ubicacion, "
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace("IGV_ExistenciaPorAlmacenArticulosDeInven_pIB1.CodigoAlmacen", gUtilSQL.fSimpleSqlValue(gProyParametrosCompania.GetCodigoAlmacenGenerico)) & ", "
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace("IGV_ExistenciaPorAlmacenArticulosDeInven_pIB1.ExistenciaAlmacen", "0") & ", "
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace("IGV_ExistenciaPorAlmacenArticulosDeInven_pIB1.CodigoArticulo", "articuloInventario.Codigo") & ", "
   SQL = SQL & " Almacen.NombreAlmacen, "
   SQL = SQL & " Almacen.TipoDeAlmacen, "
   SQL = SQL & " Almacen.CodigoCc, "
   SQL = SQL & "ISNULL(Almacen.Descripcion,'' ) AS DescripcionAlmacen, "
   SQL = SQL & "ISNULL(articuloInventario.Peso, 0) AS Peso,"
   SQL = SQL & gUtilSQL.getIIF(" articuloInventario.ArancelesCodigo <> " & gUtilSQL.fSimpleSqlValue(""), "articuloInventario.ArancelesCodigo", gUtilSQL.fSimpleSqlValue(" ")) & " AS ArancelesCodigo,  "
   SQL = SQL & gUtilSQL.getIIF(" articuloInventario.TipoDeMercanciaProduccion <> " & gUtilSQL.fSimpleSqlValue(""), "articuloInventario.TipoDeMercanciaProduccion", gUtilSQL.fSimpleSqlValue("0")) & " AS TipoDeMercanciaProduccion "
   SQL = SQL & " FROM articuloInventario LEFT OUTER JOIN"
   SQL = SQL & " camposMonedaExtranjera ON articuloInventario.Codigo = camposMonedaExtranjera.Codigo AND"
   SQL = SQL & " articuloInventario.ConsecutivoCompania = camposMonedaExtranjera.ConsecutivoCompania FULL OUTER JOIN"
   SQL = SQL & " ExistenciaPorGrupo ON articuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania AND"
   SQL = SQL & " articuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo FULL OUTER JOIN"
   SQL = SQL & " Saw.Color  As Color INNER JOIN"
   SQL = SQL & " RenglonGrupoColor ON Color.CodigoColor = RenglonGrupoColor.CodigoColor AND"
   SQL = SQL & " Color.ConsecutivoCompania = RenglonGrupoColor.ConsecutivoCompania INNER JOIN"
   SQL = SQL & " GrupoTallaColor ON RenglonGrupoColor.CodigoGrupo = GrupoTallaColor.CodigoGrupo AND"
   SQL = SQL & " RenglonGrupoColor.ConsecutivoCompania = GrupoTallaColor.ConsecutivoCompania INNER JOIN"
   SQL = SQL & " RenglonGrupoTalla ON GrupoTallaColor.CodigoGrupo = RenglonGrupoTalla.CodigoGrupo AND"
   SQL = SQL & " GrupoTallaColor.ConsecutivoCompania = RenglonGrupoTalla.ConsecutivoCompania INNER JOIN"
   SQL = SQL & " Saw.Talla As Talla ON RenglonGrupoTalla.CodigoTalla = Talla.CodigoTalla AND RenglonGrupoTalla.ConsecutivoCompania = Talla.ConsecutivoCompania ON"
   SQL = SQL & " ExistenciaPorGrupo.CodigoColor = Color.CodigoColor AND ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla AND"
   SQL = SQL & " ExistenciaPorGrupo.CodigoGrupo = GrupoTallaColor.CodigoGrupo AND"
   SQL = SQL & " articuloInventario.ConsecutivoCompania = GrupoTallaColor.ConsecutivoCompania AND"
   SQL = SQL & " ArticuloInventario.CodigoGrupo = GrupoTallaColor.CodigoGrupo"

   SQL = SQL & " LEFT OUTER JOIN IGV_ExistenciaPorAlmacenArticulosDeInven_pIB1 "
   SQL = SQL & " ON (articuloInventario.ConsecutivoCompania = IGV_ExistenciaPorAlmacenArticulosDeInven_pIB1.ConsecutivoCompania "
   SQL = SQL & " AND (articuloInventario.Codigo "
   SQL = SQL & gUtilSQL.CharConcat
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace("Color.CodigoColor", gUtilSQL.fSimpleSqlValue(""))
   SQL = SQL & gUtilSQL.CharConcat
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace("Talla.CodigoTalla", gUtilSQL.fSimpleSqlValue(""))
   SQL = SQL & " = IGV_ExistenciaPorAlmacenArticulosDeInven_pIB1.CodigoArticulo)"
   SQL = SQL & " AND " & gUtilSQL.fIfResultIsNullReplace("ExistenciaPorGrupo.Serial", "0") & " = IGV_ExistenciaPorAlmacenArticulosDeInven_pIB1.Serial "
   SQL = SQL & " AND " & gUtilSQL.fIfResultIsNullReplace("ExistenciaPorGrupo.Rollo", "0") & " = IGV_ExistenciaPorAlmacenArticulosDeInven_pIB1.Rollo) "
   SQL = SQL & " LEFT OUTER JOIN Almacen "
   SQL = SQL & " ON (IGV_ExistenciaPorAlmacenArticulosDeInven_pIB1.ConsecutivoCompania = Almacen.ConsecutivoCompania "
   SQL = SQL & " AND IGV_ExistenciaPorAlmacenArticulosDeInven_pIB1.CodigoAlmacen = Almacen.Codigo) "

   SQL = SQL & " WHERE articuloInventario.ConsecutivoCompania = " & mConsecutivoCompaniaActual
   SQL = SQL & " AND articuloInventario.Codigo <> " & gUtilSQL.fSimpleSqlValue("RD_AliExenta  @")
   SQL = SQL & " AND articuloInventario.Codigo <> " & gUtilSQL.fSimpleSqlValue("RD_AliGeneral @")
   SQL = SQL & " AND articuloInventario.Codigo <> " & gUtilSQL.fSimpleSqlValue("RD_AliReducida@")
   SQL = SQL & " AND articuloInventario.Codigo <> " & gUtilSQL.fSimpleSqlValue("RD_AliExtendida")
   SQL = SQL & " AND articuloInventario.Codigo <> " & gUtilSQL.fSimpleSqlValue("RD_AliExentaNC @")
   SQL = SQL & " AND articuloInventario.Codigo <> " & gUtilSQL.fSimpleSqlValue("RD_AliGeneralNC @")
   SQL = SQL & " AND articuloInventario.Codigo <> " & gUtilSQL.fSimpleSqlValue("RD_AliReducidaNC @")
   SQL = SQL & " AND articuloInventario.Codigo <> " & gUtilSQL.fSimpleSqlValue("RD_AliExtendidaNC @")
   SQL = SQL & " AND articuloInventario.Codigo <> " & gUtilSQL.fSimpleSqlValue("RD_ComXPorcDeAlmacen @")
   SQL = SQL & " AND articuloInventario.Codigo <> " & gUtilSQL.fSimpleSqlValue("ND-NC IGTF @")
      
   If mIsEntreFechas Then
      SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("articuloInventario.FechaUltimaModificacion", mFechaInicial, mFechaFinal) & ")"
   End If
   SQL = SQL & " ORDER BY "
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace("IGV_ExistenciaPorAlmacenArticulosDeInven_pIB1.CodigoAlmacen", gUtilSQL.fSimpleSqlValue(gProyParametrosCompania.GetCodigoAlmacenGenerico)) & ", "
   SQL = SQL & " articuloInventario.Codigo"
h_EXIT: On Error GoTo 0
   fSQLArticulosAExportar = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLArticulosAExportar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExportarVendedorSobreFactura(Optional ByVal valEntreFechas As Boolean = False, Optional ByVal dtpFechaInicial As Date, Optional ByVal dtpFechaFinal As Date) As String
   Dim SQL As String
   Dim sqlreplaceDireccion As String
   Dim sqlreplaceNotas As String
   On Error GoTo h_ERROR
   sqlreplaceDireccion = ""
   sqlreplaceDireccion = fReplace("vendedor.Direccion", gUtilSQL.DfChar(13), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceDireccion = fReplace(sqlreplaceDireccion, gUtilSQL.DfChar(12), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceDireccion = fReplace(sqlreplaceDireccion, gUtilSQL.DfChar(11), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceDireccion = fReplace(sqlreplaceDireccion, gUtilSQL.DfChar(10), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceDireccion = fReplace(sqlreplaceDireccion, gUtilSQL.DfChar(9), gUtilSQL.fSimpleSqlValue(""))
   
   sqlreplaceNotas = ""
   sqlreplaceNotas = fReplace("vendedor.Notas", gUtilSQL.DfChar(13), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceNotas = fReplace(sqlreplaceNotas, gUtilSQL.DfChar(12), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceNotas = fReplace(sqlreplaceNotas, gUtilSQL.DfChar(11), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceNotas = fReplace(sqlreplaceNotas, gUtilSQL.DfChar(10), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceNotas = fReplace(sqlreplaceNotas, gUtilSQL.DfChar(9), gUtilSQL.fSimpleSqlValue(""))
   
   SQL = "SELECT vendedor.Codigo, "
   SQL = SQL & "vendedor.Nombre, "
   SQL = SQL & "vendedor.RIF, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("vendedor.StatusVendedor", enum_StatusVendedor.eSV_ACTIVO, gEnumProyecto.fEnumStatusVendedorToStringInArray(True, True), "StatusVendedor") & ", "
   SQL = SQL & sqlreplaceDireccion & " AS Direccion, "
   SQL = SQL & "vendedor.Ciudad, "
   SQL = SQL & "vendedor.ZonaPostal, "
   SQL = SQL & "vendedor.Telefono, "
   SQL = SQL & "vendedor.Fax, "
   SQL = SQL & "vendedor.email, "
   SQL = SQL & sqlreplaceNotas & " AS Notas, "
   SQL = SQL & "vendedor.comisionPorVenta, "
   SQL = SQL & "vendedor.comisionPorCobro, "
   SQL = SQL & "vendedor.TopeInicialVenta1, "
   SQL = SQL & "vendedor.TopeFinalVenta1, "
   SQL = SQL & "vendedor.PorcentajeVentas1, "
   SQL = SQL & "vendedor.TopeFinalVenta2, "
   SQL = SQL & "vendedor.PorcentajeVentas2, "
   SQL = SQL & "vendedor.TopeFinalVenta3, "
   SQL = SQL & "vendedor.PorcentajeVentas3, "
   SQL = SQL & "vendedor.TopeFinalVenta4, "
   SQL = SQL & "vendedor.PorcentajeVentas4, "
   SQL = SQL & "vendedor.TopeFinalVenta5, "
   SQL = SQL & "vendedor.PorcentajeVentas5, "
   SQL = SQL & "vendedor.TopeInicialCobranza1, "
   SQL = SQL & "vendedor.TopeFinalCobranza1, "
   SQL = SQL & "vendedor.PorcentajeCobranza1, "
   SQL = SQL & "vendedor.TopeFinalCobranza2, "
   SQL = SQL & "vendedor.PorcentajeCobranza2, "
   SQL = SQL & "vendedor.TopeFinalCobranza3, "
   SQL = SQL & "vendedor.PorcentajeCobranza3, "
   SQL = SQL & "vendedor.TopeFinalCobranza4, "
   SQL = SQL & "vendedor.PorcentajeCobranza4, "
   SQL = SQL & "vendedor.TopeFinalCobranza5, "
   SQL = SQL & "vendedor.PorcentajeCobranza5, "
   SQL = SQL & "vendedor.UsaComisionPorVenta, "
   SQL = SQL & "vendedor.UsaComisionPorCobranza, "
   SQL = SQL & "vendedor.ConsecutivoRutaDeComercializacion"
   SQL = SQL & " FROM Adm.Vendedor"
   SQL = SQL & " WHERE (vendedor.ConsecutivoCompania = " & mConsecutivoCompaniaActual & ")"
   If valEntreFechas Then
        SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("vendedor.FechaUltimaModificacion", dtpFechaInicial, dtpFechaFinal) & ")"
   End If
   If Not gMessage.Confirm("¿Desea incluir el Vendedor Genérico en el proceso de exportación?") Then
      SQL = SQL & " AND vendedor.Codigo <> " & gUtilSQL.fSimpleSqlValue(gProyParametrosCompania.getCodigoGenericoVendedor)
   End If
h_EXIT: On Error GoTo 0
   fSQLExportarVendedorSobreFactura = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExportarVendedorSobreFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExportarVendedorSobreRenglon(Optional ByVal valEntreFechas As Boolean = False, Optional ByVal dtpFechaInicial As Date, Optional ByVal dtpFechaFinal As Date, Optional ByVal valEsFacturayRenglon As Boolean = False) As String
   Dim SQL As String
   Dim TipoComision As String
   Dim sqlreplaceDireccion As String
   Dim sqlreplaceNotas As String
   On Error GoTo h_ERROR
   sqlreplaceDireccion = ""
   sqlreplaceDireccion = fReplace("vendedor.Direccion", gUtilSQL.DfChar(13), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceDireccion = fReplace(sqlreplaceDireccion, gUtilSQL.DfChar(12), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceDireccion = fReplace(sqlreplaceDireccion, gUtilSQL.DfChar(11), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceDireccion = fReplace(sqlreplaceDireccion, gUtilSQL.DfChar(10), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceDireccion = fReplace(sqlreplaceDireccion, gUtilSQL.DfChar(9), gUtilSQL.fSimpleSqlValue(""))
   
   sqlreplaceNotas = ""
   sqlreplaceNotas = fReplace("vendedor.Notas", gUtilSQL.DfChar(13), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceNotas = fReplace(sqlreplaceNotas, gUtilSQL.DfChar(12), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceNotas = fReplace(sqlreplaceNotas, gUtilSQL.DfChar(11), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceNotas = fReplace(sqlreplaceNotas, gUtilSQL.DfChar(10), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceNotas = fReplace(sqlreplaceNotas, gUtilSQL.DfChar(9), gUtilSQL.fSimpleSqlValue(""))
   
   TipoComision = gUtilSQL.DfSQLCaseIfForEnum("VendedorDetalleComisiones.TipoDeComision", enum_TipoComision.eTC_PorPorcentaje, gEnumProyecto.fEnumTipoComisionToStringInArray(True, True), "TipoDeComision")
   SQL = "SELECT " & "vendedor.Codigo, "
   SQL = SQL & "vendedor.Nombre, "
   SQL = SQL & "vendedor.RIF, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("vendedor.StatusVendedor", enum_StatusVendedor.eSV_ACTIVO, gEnumProyecto.fEnumStatusVendedorToStringInArray(True, True), "StatusVendedor") & ", "
   SQL = SQL & sqlreplaceDireccion & " AS Direccion, "
   SQL = SQL & "vendedor.Ciudad, "
   SQL = SQL & "vendedor.ZonaPostal, "
   SQL = SQL & "vendedor.Telefono, "
   SQL = SQL & "vendedor.Fax, "
   SQL = SQL & "vendedor.email, "
   SQL = SQL & sqlreplaceNotas & " AS Notas, "
   SQL = SQL & "vendedor.comisionPorVenta, "
   SQL = SQL & "vendedor.comisionPorCobro, "
   SQL = SQL & "vendedor.TopeInicialVenta1, "
   SQL = SQL & "vendedor.TopeFinalVenta1, "
   SQL = SQL & "vendedor.PorcentajeVentas1, "
   SQL = SQL & "vendedor.TopeFinalVenta2, "
   SQL = SQL & "vendedor.PorcentajeVentas2, "
   SQL = SQL & "vendedor.TopeFinalVenta3, "
   SQL = SQL & "vendedor.PorcentajeVentas3, "
   SQL = SQL & "vendedor.TopeFinalVenta4, "
   SQL = SQL & "vendedor.PorcentajeVentas4, "
   SQL = SQL & "vendedor.TopeFinalVenta5, "
   SQL = SQL & "vendedor.PorcentajeVentas5, "
   SQL = SQL & "vendedor.TopeInicialCobranza1, "
   SQL = SQL & "vendedor.TopeFinalCobranza1, "
   SQL = SQL & "vendedor.PorcentajeCobranza1, "
   SQL = SQL & "vendedor.TopeFinalCobranza2, "
   SQL = SQL & "vendedor.PorcentajeCobranza2, "
   SQL = SQL & "vendedor.TopeFinalCobranza3, "
   SQL = SQL & "vendedor.PorcentajeCobranza3, "
   SQL = SQL & "vendedor.TopeFinalCobranza4, "
   SQL = SQL & "vendedor.PorcentajeCobranza4, "
   SQL = SQL & "vendedor.TopeFinalCobranza5, "
   SQL = SQL & "vendedor.PorcentajeCobranza5, "
   SQL = SQL & "vendedor.UsaComisionPorVenta, "
   SQL = SQL & "vendedor.UsaComisionPorCobranza, "
   SQL = SQL & "VendedorDetalleComisiones.NombreDeLineaDeProducto, "
   SQL = SQL & "VendedorDetalleComisiones.Consecutivo, "
   SQL = SQL & TipoComision & ", "
   SQL = SQL & "VendedorDetalleComisiones.Monto, "
   SQL = SQL & "VendedorDetalleComisiones.Porcentaje, "
   SQL = SQL & "vendedor.ConsecutivoRutaDeComercializacion "
   If valEsFacturayRenglon Then
      SQL = SQL & " FROM Adm.Vendedor LEFT JOIN Adm.VendedorDetalleComisiones ON"
      SQL = SQL & " (vendedor.Consecutivo = VendedorDetalleComisiones.ConsecutivoVendedor)"
      SQL = SQL & " AND (vendedor.ConsecutivoCompania = VendedorDetalleComisiones.ConsecutivoCompania)"
   Else
      SQL = SQL & " FROM Adm.vendedor"
      SQL = SQL & " INNER JOIN Adm.VendedorDetalleComisiones ON "
      SQL = SQL & "(vendedor.ConsecutivoCompania = "
      SQL = SQL & "VendedorDetalleComisiones.ConsecutivoCompania)"
      SQL = SQL & " AND (vendedor.Consecutivo = "
      SQL = SQL & "VendedorDetalleComisiones.ConsecutivoVendedor)"
   End If
   SQL = SQL & " WHERE (vendedor.ConsecutivoCompania = " & mConsecutivoCompaniaActual & ")"
    If valEntreFechas Then
      SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("vendedor.FechaUltimaModificacion", dtpFechaInicial, dtpFechaFinal) & ")"
    End If
    If Not gMessage.Confirm("¿Desea incluir el Vendedor Genérico en el proceso de exportación?") Then
       SQL = SQL & " AND vendedor.Codigo <> " & gUtilSQL.fSimpleSqlValue(gProyParametrosCompania.getCodigoGenericoVendedor)
    End If
   SQL = SQL & " UNION "
   SQL = SQL & fSQLExportarVendedorSobreRenglon2(valEntreFechas, dtpFechaInicial, dtpFechaFinal, valEsFacturayRenglon)
h_EXIT: On Error GoTo 0
   fSQLExportarVendedorSobreRenglon = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExportarVendedorSobreRenglon", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLExportarRenglonCobroDeFacturas() As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT factura.Numero, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, True), "TipoDeDocumento") & ", "
   SQL = SQL & "renglonCobroDeFactura.CodigoFormaDelCobro, "
   SQL = SQL & "renglonCobroDeFactura.NumeroDelDocumento, "
   SQL = SQL & "renglonCobroDeFactura.CodigoBanco, "
   SQL = SQL & "renglonCobroDeFactura.Monto "
   SQL = SQL & "FROM  factura INNER JOIN"
   SQL = SQL & " renglonCobroDeFactura ON factura.ConsecutivoCompania = renglonCobroDeFactura.ConsecutivoCompania"
   SQL = SQL & " AND factura.Numero = renglonCobroDeFactura.NumeroFactura"
   SQL = SQL & " AND factura.TipoDeDocumento = renglonCobroDeFactura.TipoDeDocumento"
   SQL = SQL & " WHERE (factura.ConsecutivoCompania = " & mConsecutivoCompaniaActual & ")"
   SQL = SQL & " AND (factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & ")"
   SQL = SQL & " AND (factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA) & " OR "
   SQL = SQL & " factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   SQL = SQL & "OR factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL) & ")"
   If mIsEntreFechas Then
      SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("factura.fecha", mFechaInicial, mFechaFinal) & ")"
   End If
   SQL = SQL & " ORDER BY " & "factura.Numero"
h_EXIT: On Error GoTo 0
   fSQLExportarRenglonCobroDeFacturas = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExportarRenglonCobroDeFacturas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fReplace(ByVal valFieldName As String, ByVal valOriginalValueExpression As String, ByVal valNewValueExpression As String) As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = ""
   If gTexto.DfLen(valOriginalValueExpression) > 0 And gTexto.DfLen(valNewValueExpression) > 0 Then
      vResult = "REPLACE(" & valFieldName & ", " & valOriginalValueExpression & ", " & valNewValueExpression & ")"
   Else
      gMessage.ProgrammerMessage "Valores no válidos en invocación de fReplace"
   End If
   fReplace = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fReplace", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExportarVendedorSobreRenglon2(Optional ByVal valEntreFechas As Boolean = False, Optional ByVal dtpFechaInicial As Date, Optional ByVal dtpFechaFinal As Date, Optional ByVal valEsFacturayRenglon As Boolean = False) As String
   Dim SQL As String
   Dim TipoComision As String
   Dim sqlreplaceDireccion As String
   Dim sqlreplaceNotas As String
   On Error GoTo h_ERROR
   sqlreplaceDireccion = ""
   sqlreplaceDireccion = fReplace("vendedor.Direccion", gUtilSQL.DfChar(13), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceDireccion = fReplace(sqlreplaceDireccion, gUtilSQL.DfChar(12), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceDireccion = fReplace(sqlreplaceDireccion, gUtilSQL.DfChar(11), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceDireccion = fReplace(sqlreplaceDireccion, gUtilSQL.DfChar(10), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceDireccion = fReplace(sqlreplaceDireccion, gUtilSQL.DfChar(9), gUtilSQL.fSimpleSqlValue(""))
   
   sqlreplaceNotas = ""
   sqlreplaceNotas = fReplace("vendedor.Notas", gUtilSQL.DfChar(13), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceNotas = fReplace(sqlreplaceNotas, gUtilSQL.DfChar(12), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceNotas = fReplace(sqlreplaceNotas, gUtilSQL.DfChar(11), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceNotas = fReplace(sqlreplaceNotas, gUtilSQL.DfChar(10), gUtilSQL.fSimpleSqlValue(""))
   sqlreplaceNotas = fReplace(sqlreplaceNotas, gUtilSQL.DfChar(9), gUtilSQL.fSimpleSqlValue(""))
   
   TipoComision = gUtilSQL.DfSQLCaseIfForEnum("VendedorDetalleComisiones.TipoDeComision", enum_TipoComision.eTC_PorPorcentaje, gEnumProyecto.fEnumTipoComisionToStringInArray(True, True), "TipoDeComision")
   SQL = "SELECT DISTINCT " & "vendedor.Codigo, "
   SQL = SQL & "vendedor.Nombre, "
   SQL = SQL & "vendedor.RIF, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("vendedor.StatusVendedor", enum_StatusVendedor.eSV_ACTIVO, gEnumProyecto.fEnumStatusVendedorToStringInArray(True, True), "StatusVendedor") & ", "
   SQL = SQL & sqlreplaceDireccion & " AS Direccion, "
   SQL = SQL & "vendedor.Ciudad, "
   SQL = SQL & "vendedor.ZonaPostal, "
   SQL = SQL & "vendedor.Telefono, "
   SQL = SQL & "vendedor.Fax, "
   SQL = SQL & "vendedor.email, "
   SQL = SQL & sqlreplaceNotas & " AS Notas, "
   SQL = SQL & "vendedor.comisionPorVenta, "
   SQL = SQL & "vendedor.comisionPorCobro, "
   SQL = SQL & "vendedor.TopeInicialVenta1, "
   SQL = SQL & "vendedor.TopeFinalVenta1, "
   SQL = SQL & "vendedor.PorcentajeVentas1, "
   SQL = SQL & "vendedor.TopeFinalVenta2, "
   SQL = SQL & "vendedor.PorcentajeVentas2, "
   SQL = SQL & "vendedor.TopeFinalVenta3, "
   SQL = SQL & "vendedor.PorcentajeVentas3, "
   SQL = SQL & "vendedor.TopeFinalVenta4, "
   SQL = SQL & "vendedor.PorcentajeVentas4, "
   SQL = SQL & "vendedor.TopeFinalVenta5, "
   SQL = SQL & "vendedor.PorcentajeVentas5, "
   SQL = SQL & "vendedor.TopeInicialCobranza1, "
   SQL = SQL & "vendedor.TopeFinalCobranza1, "
   SQL = SQL & "vendedor.PorcentajeCobranza1, "
   SQL = SQL & "vendedor.TopeFinalCobranza2, "
   SQL = SQL & "vendedor.PorcentajeCobranza2, "
   SQL = SQL & "vendedor.TopeFinalCobranza3, "
   SQL = SQL & "vendedor.PorcentajeCobranza3, "
   SQL = SQL & "vendedor.TopeFinalCobranza4, "
   SQL = SQL & "vendedor.PorcentajeCobranza4, "
   SQL = SQL & "vendedor.TopeFinalCobranza5, "
   SQL = SQL & "vendedor.PorcentajeCobranza5, "
   SQL = SQL & "vendedor.UsaComisionPorVenta, "
   SQL = SQL & "vendedor.UsaComisionPorCobranza, "
   SQL = SQL & "VendedorDetalleComisiones.NombreDeLineaDeProducto, "
   SQL = SQL & "VendedorDetalleComisiones.Consecutivo, "
   SQL = SQL & TipoComision & ", "
   SQL = SQL & "VendedorDetalleComisiones.Monto, "
   SQL = SQL & "VendedorDetalleComisiones.Porcentaje, "
   SQL = SQL & "Vendedor.ConsecutivoRutaDeComercializacion "
   SQL = SQL & " FROM Adm.Vendedor"
   SQL = SQL & " INNER JOIN Adm.VendedorDetalleComisiones ON"
   SQL = SQL & " (vendedor.Consecutivo = VendedorDetalleComisiones.ConsecutivoVendedor) AND"
   SQL = SQL & " (vendedor.ConsecutivoCompania = VendedorDetalleComisiones.ConsecutivoCompania)"
   SQL = SQL & " INNER JOIN renglonFactura ON"
   SQL = SQL & " (vendedor.codigo = renglonFactura.CodigoVendedor1 OR"
   SQL = SQL & " vendedor.codigo = renglonFactura.CodigoVendedor2 OR"
   SQL = SQL & " vendedor.codigo = renglonFactura.CodigoVendedor3) AND"
   SQL = SQL & " (vendedor.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)"
   SQL = SQL & " RIGHT JOIN Factura ON"
   SQL = SQL & " Factura.Numero = renglonFactura.NumeroFactura AND"
   SQL = SQL & " Factura.TipoDeDocumento = renglonFactura.TipoDeDocumento AND"
   SQL = SQL & " Factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania"
   SQL = SQL & " WHERE (vendedor.ConsecutivoCompania = " & mConsecutivoCompaniaActual & ")"
   If valEntreFechas Then
      SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("Factura.FechaUltimaModificacion", dtpFechaInicial, dtpFechaFinal) & ")"
   End If
h_EXIT: On Error GoTo 0
   fSQLExportarVendedorSobreRenglon2 = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExportarVendedorSobreRenglon2", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExportarCabeceraMercantil(ByVal valNumeroCuenta As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valFechaGestion As Date) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT '1' as Cabecera, "
   SQL = SQL & "IdBanco, "
   SQL = SQL & "NumeroArchivo, "
   SQL = SQL & "TipoProducto, "
   SQL = SQL & "TipoPago, "
   SQL = SQL & "TipoId, "
   SQL = SQL & "Rif, "
   SQL = SQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fCast("SUM(TotalRegistro)", eTDSS_VARCHAR, ""), 8, 0) & " as TotalRegistro, "
   SQL = SQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fCast(gUtilSQL.DfSQLReplace("Sum(TotalDocumentos)", ".", ""), eTDSS_VARCHAR, ""), 17, 0) & " as TotalDocumentos, "
   SQL = SQL & gUtilSQL.fCast(gUtilDate.fYear(valFechaGestion), eTDSS_VARCHAR, "") & gUtilSQL.CharConcat & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fCast(gUtilDate.fMonth(valFechaGestion), eTDSS_VARCHAR, ""), 2, 0) & gUtilSQL.CharConcat & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fCast(gUtilDate.fDay(valFechaGestion), eTDSS_VARCHAR, ""), 2, 0) & " AS FechaGestion, "
   SQL = SQL & gUtilSQL.DfSQLJustifyLeftNCaracteres(gUtilSQL.DfTrimSQL(gUtilSQL.DfSQLReplace("NumeroCuenta", "-", "")), 20) & " AS NumeroCuenta, "
   SQL = SQL & "AreaReservada1, "
   SQL = SQL & "NumeroSerial, "
   SQL = SQL & "CodigoRespuesta, "
   SQL = SQL & "FechaProceso, "
   SQL = SQL & "AreaReservada2 "
   SQL = SQL & "FROM IGV_MercantilCabecera "
   SQL = SQL & "WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual
   SQL = SQL & " AND CodigoCuentaBancaria = " & gUtilSQL.fSimpleSqlValue(valNumeroCuenta)
   SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("Fecha", valFechaInicial, valFechaFinal) & ")"
   SQL = SQL & " GROUP BY IdBanco, "
   SQL = SQL & " NumeroArchivo,"
   SQL = SQL & " TipoProducto,"
   SQL = SQL & " TipoPago,"
   SQL = SQL & " TipoId,"
   SQL = SQL & " Rif,"
   SQL = SQL & " FechaGestion,"
   SQL = SQL & " NumeroCuenta,"
   SQL = SQL & " AreaReservada1,"
   SQL = SQL & " NumeroSerial,"
   SQL = SQL & " CodigoRespuesta,"
   SQL = SQL & " FechaProceso,"
   SQL = SQL & " AreaReservada2"
h_EXIT: On Error GoTo 0
   fSQLExportarCabeceraMercantil = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExportarCabeceraMercantil", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExportarPagoMercantil(ByVal valCodigoCuentaBancaria As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valFormaPago As Boolean) As String
   Dim SQL As String
   Dim vFormaPago As String
   On Error GoTo h_ERROR
   If valFormaPago = True Then
      vFormaPago = "1" 'Abono en cuenta
   Else
      vFormaPago = "3" 'Transferencia
   End If
   SQL = "SELECT '2' AS detalle, "
   SQL = SQL & "TipoId, "
   SQL = SQL & "Rif, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue(vFormaPago) & " As FormaPago, "
   SQL = SQL & "AreaReservada1, "
   SQL = SQL & "AreaReservada2, "
   SQL = SQL & "AreaReservada3, "
   SQL = SQL & "NumeroCuentaProv, "
   SQL = SQL & "TotalDocumentos, "
   SQL = SQL & "IdCliente, "
   SQL = SQL & "TipoPago, "
   SQL = SQL & "AreaReservada4, "
   SQL = SQL & "Proveedor, "
   SQL = SQL & "AreaReservada5, "
   SQL = SQL & "NumeroComprobante, "
   SQL = SQL & "email, "
   SQL = SQL & "CodRespuesta, "
   SQL = SQL & "MsjRespuesta, "
   SQL = SQL & "ConceptoPago, "
   SQL = SQL & "AreaReservada6 "
   SQL = SQL & "FROM IGV_MercantilPago "
   SQL = SQL & "WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual
   SQL = SQL & " AND CuentaBancariaPago = " & gUtilSQL.fSimpleSqlValue(valCodigoCuentaBancaria)
   SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("FechaPago", valFechaInicial, valFechaFinal) & ")"
h_EXIT: On Error GoTo 0
   fSQLExportarPagoMercantil = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExportarPagoMercantil", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExportarOperacionMercantil(ByVal valCodigoCuentaBancaria As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valFormaPago As Boolean, ByVal valNumeroComprobante As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT '3' AS Operacion, "
   SQL = SQL & "NumeroCheque, "
   SQL = SQL & "TipoDocumento, "
   SQL = SQL & "TotalDocumento as TotalDocumento, "
   SQL = SQL & "fecha, "
   SQL = SQL & "AreaReservada1, "
   SQL = SQL & "NumeroComprobante, "
   SQL = SQL & "NumeroOrden, "
   SQL = SQL & "DescripcionPago, "
   SQL = SQL & "AreaReservada2 "
   SQL = SQL & "FROM IGV_MercantilOperaciones "
   SQL = SQL & "WHERE NumeroComprobante = " & gUtilSQL.fSimpleSqlValue(valNumeroComprobante)
   SQL = SQL & " AND CuentaBancariaPago = " & gUtilSQL.fSimpleSqlValue(valCodigoCuentaBancaria)
   SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("FechaPago", valFechaInicial, valFechaFinal) & ")"
   SQL = SQL & " AND ConsecutivoCompania = " & mConsecutivoCompaniaActual
h_EXIT: On Error GoTo 0
   fSQLExportarOperacionMercantil = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExportarOperacionMercantil", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLExportarCotizacion() As String
   Dim SQL As String
   Dim sqlReplaceCondicionPago As String
   Dim sqlReplaceObservaciones
   On Error GoTo h_ERROR
   sqlReplaceCondicionPago = ""
   sqlReplaceCondicionPago = fReplace(sqlReplaceCondicionPago, gUtilSQL.DfChar(12), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceCondicionPago = fReplace(sqlReplaceCondicionPago, gUtilSQL.DfChar(11), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceCondicionPago = fReplace(sqlReplaceCondicionPago, gUtilSQL.DfChar(10), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceCondicionPago = fReplace(sqlReplaceCondicionPago, gUtilSQL.DfChar(9), gUtilSQL.fSimpleSqlValue(""))
   
   sqlReplaceObservaciones = ""
   sqlReplaceObservaciones = fReplace("cotizacion.Observaciones", gUtilSQL.DfChar(13), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceObservaciones = fReplace(sqlReplaceObservaciones, gUtilSQL.DfChar(12), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceObservaciones = fReplace(sqlReplaceObservaciones, gUtilSQL.DfChar(11), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceObservaciones = fReplace(sqlReplaceObservaciones, gUtilSQL.DfChar(10), gUtilSQL.fSimpleSqlValue(""))
   sqlReplaceObservaciones = fReplace(sqlReplaceObservaciones, gUtilSQL.DfChar(9), gUtilSQL.fSimpleSqlValue(""))
   
   SQL = "SELECT cotizacion.Numero, "
   SQL = SQL & "cotizacion.Fecha, "
   SQL = SQL & "cotizacion.CodigoCliente, "
   SQL = SQL & "cotizacion.CodigoVendedor, "
   SQL = SQL & sqlReplaceObservaciones & " AS Observaciones, "
   SQL = SQL & "cotizacion.TotalMontoExento, "
   SQL = SQL & "cotizacion.TotalBaseImponible, "
   SQL = SQL & "cotizacion.TotalRenglones, "
   SQL = SQL & "cotizacion.TotalIVA, "
   SQL = SQL & "cotizacion.Totalcotizacion, "
   SQL = SQL & "cotizacion.PorcentajeDescuento, "
   SQL = SQL & "cotizacion.CodigoMoneda, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cotizacion.NivelDePrecio", enum_NivelDePrecio.eND_PRECIO_1, gEnumProyecto.fEnumNivelDePrecioToStringInArray(True, True), "NivelDePrecio") & ", "
   SQL = SQL & "cotizacion.UsarDireccionFiscal, "
   SQL = SQL & "cotizacion.NoDirDespachoAimprimir, "
   SQL = SQL & "cotizacion.ReservarMercancia, "
   SQL = SQL & "cotizacion.FechaDeRetiro, "
   SQL = SQL & "cotizacion.NoDirDespachoAimprimir, "
   SQL = SQL & "renglonCotizacion.Descripcion, "
   SQL = SQL & "renglonCotizacion.Cantidad, "
   SQL = SQL & "renglonCotizacion.PrecioSinIVA, "
   SQL = SQL & "renglonCotizacion.PrecioConIVA, "
   SQL = SQL & "renglonCotizacion.PorcentajeDescuento, "
   SQL = SQL & "renglonCotizacion.TotalRenglon, "
   SQL = SQL & "renglonCotizacion.PorcentajeBaseImponible, "
   SQL = SQL & "renglonCotizacion.COdigoArticulo, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("renglonCotizacion.AlicuotaIVA", enum_TipoDeAlicuota.eTD_EXENTO, gEnumProyecto.fEnumTipoDeAlicuotaToStringInArray(True, True), "AlicuotaIVA") & ", "
   SQL = SQL & "cotizacion.CambioABolivares, "
   SQL = SQL & "cotizacion.CambioMostrarTotalEnDivisas, "
   SQL = SQL & "cotizacion.NroDiasMantenerCambioAMonedaLocal, "
   SQL = SQL & "cotizacion.fechaLimiteCambioAMonedaLocal"
   SQL = SQL & " FROM cotizacion"
   SQL = SQL & " INNER JOIN renglonCotizacion ON "
   SQL = SQL & " (cotizacion.Numero = "
   SQL = SQL & "renglonCotizacion.NumeroCotizacion)"
   SQL = SQL & " AND (cotizacion.ConsecutivoCompania = "
   SQL = SQL & "renglonCotizacion.ConsecutivoCompania)"
   SQL = SQL & " WHERE (cotizacion.ConsecutivoCompania = " & mConsecutivoCompaniaActual & ")"
   If mIsEntreFechas Then
      SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("cotizacion.fecha", mFechaInicial, mFechaFinal) & ")"
   End If
   SQL = SQL & " ORDER BY " & "cotizacion.Numero"
h_EXIT: On Error GoTo 0
   fSQLExportarCotizacion = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExportarCotizacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExportarCabeceraBOD(ByVal valNumeroCuenta As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valFechaGestion As Date, ByVal valFormaPago As Boolean, ByVal valContrato As String) As String
   Dim SQL As String
   Dim vFormaPago As String
   On Error GoTo h_ERROR
   If valFormaPago = True Then
      vFormaPago = "Proveedores"
   Else
      vFormaPago = "Nómina"
   End If
   SQL = "SELECT '01' AS Cabecera"
   SQL = SQL & " ,CAST('" & vFormaPago & "' + REPLICATE(' ', 20 - LEN('" & vFormaPago & "')) AS VARCHAR (20)) AS Descripcion"
   SQL = SQL & " ,TipoId"
   SQL = SQL & " ,RIGHT(REPLICATE('0', 9) + CAST((Rif) AS VARCHAR (9)),9) AS Rif"
   SQL = SQL & " ,RIGHT(REPLICATE('0', 17)+ CAST((" & valContrato & ") AS VARCHAR (17)),17) AS Contrato "
   SQL = SQL & " ,RIGHT(REPLICATE('0', 9) + CAST((NumeroLote) AS VARCHAR (9)),9) AS NumeroLote "
   SQL = SQL & " ," & gUtilSQL.fCast(gUtilSQL.fYear("" & gUtilSQL.fSimpleSqlValue(valFechaGestion), ""), eTDSS_VARCHAR, "") & "+" & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fCast(gUtilSQL.fMonth("" & gUtilSQL.fSimpleSqlValue(valFechaGestion), ""), eTDSS_VARCHAR, ""), 2, 0) & "+" & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fCast(gUtilSQL.DfSQLDayOfDate("" & gUtilSQL.fSimpleSqlValue(valFechaGestion), ""), eTDSS_VARCHAR, ""), 2, 0) & " AS FechaGestion"
   SQL = SQL & " ,RIGHT(REPLICATE('0', 6) + CAST(COUNT(Fecha) AS VARCHAR(6)), 6) AS CantidadRegistros"
   SQL = SQL & " ,RIGHT(REPLICATE('0', 17)+ REPLACE(CAST(SUM(TotalDocumentos) AS VARCHAR (17)), '.', ''),17) AS TotalDocumentos"
   SQL = SQL & " ,'VEB' as Moneda"
   SQL = SQL & " FROM IGV_BODCabecera"
   SQL = SQL & " WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual
   SQL = SQL & " AND CodigoCuentaBancaria = " & gUtilSQL.fSimpleSqlValue(valNumeroCuenta)
   SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("Fecha", valFechaInicial, valFechaFinal) & ")"
   SQL = SQL & " GROUP BY TipoId, Rif, NumeroLote"
h_EXIT: On Error GoTo 0
   fSQLExportarCabeceraBOD = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExportarCabeceraBOD", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExportarPagoBOD(ByVal valCodigoCuentaBancaria As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valFormaPago As Boolean, ByVal valFechaGestion As Date) As String
   Dim SQL As String
   Dim vFormaPago As String
   On Error GoTo h_ERROR
   If valFormaPago = True Then
      vFormaPago = "Proveedores"
   Else
      vFormaPago = "Nómina"
   End If
   SQL = "SELECT '02' AS Detalle "
   SQL = SQL & ", TipoId "
   SQL = SQL & ", Rif "
   SQL = SQL & ", CAST(Proveedor + REPLICATE(' ', 60 - LEN(Proveedor)) AS VARCHAR (60)) AS Proveedor "
   SQL = SQL & ", NumeroComprobante "
   SQL = SQL & ", CASE WHEN DescripcionPago = '' THEN"
   SQL = SQL & " 'SIN DESCRIPCION'"
   SQL = SQL & " ELSE"
   SQL = SQL & " CAST(SUBSTRING (DescripcionPago,0,31) + REPLICATE(' ', 30 - LEN(SUBSTRING (DescripcionPago,0,30))) AS VARCHAR (30)) "
   SQL = SQL & " END AS DescripcionPago "
   SQL = SQL & ",ModalidadPago "
   SQL = SQL & ",CASE WHEN LEN(CuentaAcreditar) = 20 THEN"
   SQL = SQL & " SUBSTRING (CuentaAcreditar,0,5)"
   SQL = SQL & " ELSE"
   SQL = SQL & " '0000'"
   SQL = SQL & " END AS CodigoBanco "
   SQL = SQL & ",CASE WHEN LEN(CuentaAcreditar) = 20 THEN"
   SQL = SQL & " CuentaAcreditar"
   SQL = SQL & " ELSE"
   SQL = SQL & " '00000000000000000000'"
   SQL = SQL & " END AS CuentaAcreditar, "
   SQL = SQL & " FechaValor"
   SQL = SQL & ",TotalDocumentos "
   SQL = SQL & ",ImpuestoRetenido "
   SQL = SQL & ",CASE WHEN CorreoElectronico='' THEN"
   SQL = SQL & " '                                        ' "
   SQL = SQL & " ELSE"
   SQL = SQL & " CAST(CorreoElectronico + REPLICATE(' ', 40 - LEN(CorreoElectronico)) AS VARCHAR (40))"
   SQL = SQL & " END AS CorreoElectronico "
   SQL = SQL & ",Telefonos "
   SQL = SQL & "FROM IGV_BODPago "
   SQL = SQL & " WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual
   SQL = SQL & " AND CuentaBancariaPago = " & gUtilSQL.fSimpleSqlValue(valCodigoCuentaBancaria)
   SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("FechaPago", valFechaInicial, valFechaFinal) & ")"
   SQL = SQL & " GROUP BY NumeroDelComprobante, NumeroComprobante,TipoId , Rif,Proveedor,"
   SQL = SQL & "DescripcionPago,ModalidadPago,CuentaAcreditar,FechaValor,TotalDocumentos,"
   SQL = SQL & "ImpuestoRetenido,CorreoElectronico,Telefonos"
h_EXIT: On Error GoTo 0
   fSQLExportarPagoBOD = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExportarPagoBOD", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExportarOperacionBOD(ByVal valCodigoCuentaBancaria As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valFormaPago As Boolean, ByVal valNumeroComprobante As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT '03' AS Operacion "
   SQL = SQL & ",FacturaAsociado "
   SQL = SQL & ",TotalDocumentos "
   SQL = SQL & ",FechaPago AS FechaValor "
   SQL = SQL & ",ImpuestoRetenido "
   SQL = SQL & "FROM IGV_BODOperaciones "
   SQL = SQL & "WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual
   SQL = SQL & " AND CuentaBancariaPago = " & gUtilSQL.fSimpleSqlValue(valCodigoCuentaBancaria)
   SQL = SQL & " AND NumeroComprobante = " & gUtilSQL.fSimpleSqlValue(valNumeroComprobante)
   SQL = SQL & " GROUP BY NumeroComprobante,FacturaAsociado ,TotalDocumentos ,FechaPago ,ImpuestoRetenido"
h_EXIT: On Error GoTo 0
   fSQLExportarOperacionBOD = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExportarOperacionBOD", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExportarCabeceraBanesco(ByVal valNumeroCuenta As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valFechaGestion As Date, ByVal valNumeroReferencia) As String
   Dim SQL As String
   Dim vNombreCompania As String
   Dim vFechaGestion As String
   On Error GoTo h_ERROR
   vFechaGestion = gUtilDate.fDateFormat(valFechaGestion, "yyyymmdd")
   vNombreCompania = gTexto.fCleanTextOfInvalidChars(mNombreCompaniaActual, "-()+/*;:_#$&'")
   vNombreCompania = gTexto.DfReplace(vNombreCompania, ",", " ")
   vNombreCompania = gTexto.DfReplace(vNombreCompania, ".", " ")
   vNombreCompania = gTexto.DfReplace(vNombreCompania, "Ñ", "N")
   vNombreCompania = gTexto.DfReplace(vNombreCompania, "Á", "A")
   vNombreCompania = gTexto.DfReplace(vNombreCompania, "É", "E")
   vNombreCompania = gTexto.DfReplace(vNombreCompania, "Í", "I")
   vNombreCompania = gTexto.DfReplace(vNombreCompania, "Ó", "O")
   vNombreCompania = gTexto.DfReplace(vNombreCompania, "Ú", "U")
   vNombreCompania = gTexto.DfReplace(vNombreCompania, "Ü", "U")
   vNombreCompania = gTexto.DfReplace(vNombreCompania, "À", "A")
   vNombreCompania = gTexto.DfReplace(vNombreCompania, "È", "E")
   vNombreCompania = gTexto.DfReplace(vNombreCompania, "Ì", "I")
   vNombreCompania = gTexto.DfReplace(vNombreCompania, "Ò", "O")
   vNombreCompania = gTexto.DfReplace(vNombreCompania, "Ù", "U")
      
   SQL = "SELECT " & gUtilSQL.fSimpleSqlValue("02") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(gTexto.llenaConCaracterALaIzquierda(valNumeroReferencia, "0", gTexto.DfLen(valNumeroReferencia))) & " AS Seccion, "
   SQL = SQL & gUtilSQL.fSQLFillRightWithCaracter("''", 22, " ") & " AS Blanco1, "
   SQL = SQL & "TipoId" & gUtilSQL.CharConcat & "Rif" & " AS NumeroRif, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue(vNombreCompania) & " AS Compania, "
   SQL = SQL & gUtilSQL.DfRightSQL(gUtilSQL.fSQLFillRightWithCaracter("''", "15", "0") & gUtilSQL.CharConcat & gUtilSQL.DfSQLReplace("CAST(SUM(TotalDocumentos) AS VARCHAR (15))", ".", "") & gUtilSQL.CharConcat & "'VEF '", "19") & " AS MontoTotal, "
   SQL = SQL & "NumeroCuenta AS CuentaDebitar, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue(vFechaGestion) & " AS FechaDeposito"
   SQL = SQL & " FROM dbo.IGV_BODCabecera"
   SQL = SQL & " WHERE ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(mConsecutivoCompaniaActual)
   SQL = SQL & " AND CodigoCuentaBancaria = " & gUtilSQL.fSimpleSqlValue(valNumeroCuenta)
   SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("Fecha", valFechaInicial, valFechaFinal) & ")"
   SQL = SQL & " GROUP BY TipoId, Rif, NumeroCuenta"
h_EXIT: On Error GoTo 0
   fSQLExportarCabeceraBanesco = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExportarCabeceraBanesco", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExportarPagoBanesco(ByVal valCodigoCuentaBancaria As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valFechaGestion As Date) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT " & gUtilSQL.fSimpleSqlValue("03") & "AS Seccion1" & ", "
   SQL = SQL & "NumeroComprobante AS NumeroComprobante, "
   SQL = SQL & gUtilSQL.DfSQLReplace("TotalDocumentos", "B", "F") & " AS MontoAcreditar, "
   SQL = SQL & "CuentaAcreditar AS CuentaProveedor, "
   SQL = SQL & "CodigoBanco AS PrefijoBanco, "
   SQL = SQL & "TipoId" & gUtilSQL.CharConcat & "Rif AS RIF, "
   SQL = SQL & "Proveedor AS Beneficiario, "
   SQL = SQL & gUtilSQL.getIIF("CodigoBanco = '0134'", gUtilSQL.fSimpleSqlValue("42 "), gUtilSQL.fSimpleSqlValue("425")) & " AS CaracterControl, "
   SQL = SQL & gUtilSQL.DfSQLReplace("TotalDocumentos", "VEB", "") & " AS Monto"
   SQL = SQL & " FROM dbo.IGV_BODPago"
   SQL = SQL & " WHERE ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(mConsecutivoCompaniaActual)
   SQL = SQL & " AND CuentaBancariaPago = " & gUtilSQL.fSimpleSqlValue(valCodigoCuentaBancaria)
   SQL = SQL & " AND CuentaAcreditar <> '" & gTexto.nCar("0", 20) & "'"
   SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("FechaPago", valFechaInicial, valFechaFinal) & ")"
   SQL = SQL & " GROUP BY  NumeroComprobante,TipoId , Rif,Proveedor, DescripcionPago,ModalidadPago, CodigoBanco ,CuentaAcreditar,FechaValor,TotalDocumentos"
h_EXIT: On Error GoTo 0
   fSQLExportarPagoBanesco = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExportarPagoBanesco", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlExportacionSegunEntidad(ByVal valEntidad As enum_EntidadAExportarOImportar, ByVal valEsFacturaBorradores As Boolean, ByRef refNumFilasVector As Integer) As String
   Dim vSql As String
   On Error GoTo h_ERROR
   Select Case valEntidad
      Case eEAEI_PROVEEDOR:
         refNumFilasVector = 2
         vSql = fSQLDeProveedores
      Case eEAEI_CLIENTE:
         refNumFilasVector = 3
         vSql = fSQLDeClientes
      Case eEAEI_ARTICULO_INVENTARIO:
         refNumFilasVector = 2
         vSql = fSQLArticulosAExportar
      Case eEAEI_FACTURA:
         refNumFilasVector = 2
         If valEsFacturaBorradores Then
             vSql = fSQLExportarFacturas(True)
         Else
             vSql = fSQLExportarFacturas(False)
         End If
      Case eEAEI_CxC:
         refNumFilasVector = 3
         vSql = fSQLDeCxC
      Case eEAEI_CxP:
         refNumFilasVector = 3
         vSql = fSQLDeCxP
      Case eEAEI_VENDEDOR:
         refNumFilasVector = 3
         vSql = fSQLExportarVendedor
      Case eEAEI_COTIZACION:
         refNumFilasVector = 3
         vSql = fSQLExportarCotizacion
   End Select
   fSqlExportacionSegunEntidad = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlExportacionSegunEntidad", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


