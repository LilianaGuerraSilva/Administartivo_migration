VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsImportarDatosSAW"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit
'============================================>>>PROVEEDORES<<<====================================================================================
Private Const CM_COD_DE_RET_USUAL_PREDEF_PARA_IMP As String = "NORET"
Private Const CM_TIPO_DE_PROVEEDOR_PREDEF_PARA_IMP As String = "NACIONAL"
Private Const CM_INDEX_CODIGO_PROVEEDOR = 0
Private Const CM_INDEX_DESCRIPCION_PROVEEDOR As Long = 1
Private Const CM_MESSAGE_NAME_PROVEEDOR As String = "Importar Proveedores"
'============================================>>>CLIENTES<<<=======================================================================================
Private Const CM_INDEX_CODIGO_CLIENTE As Long = 0
Private Const CM_INDEX_NOMBRE_CLIENTE As Long = 1
Private Const CM_INDEX_RIF_CLIENTE As Long = 2
Private Const CM_MESSAGE_NAME_CLIENTE As String = "Importar Datos Cliente"
'============================================>>>VENDEDORES<<<<=======================================================================================
Private Const CM_MESSAGE_NAME_VENDEDOR As String = "Importar Vendedores"
Private Const CM_INDEX_NUMERO_VENDEDOR As Long = 1
Private Const CM_INDEX_VALOR_IMPORTA_VENDEDOR As Integer = 2
Private Const CM_INDEX_VALOR_IMPORTA_COMISION_VENDEDOR As Integer = 3
'=======================================>>FACTURAS<<<======================================================================================
Private Const CM_MESSAGE_NAME_FACTURA As String = "Importar Factura"
Private Const CM_INDEX_NUMERO_FACTURA As Long = 1
Private Const CM_INDEX_CONSECUTIVO_COMPANIA_DETALLE_FACTURA As Integer = 0
Private Const CM_INDEX_NUMERO_FACTURA_DETALLE As Integer = 1
Private Const CM_INDEX_VALOR_IMPORTA_FACTURA_DETALLE As Integer = 2
'============================================== Importacion de CxC =================================================================================
Private Const CM_INDEX_NUMERO_Y_TIPO_DE_CXC As Long = 0
Private Const CM_MESSAGE_NAME_CXC As String = "Importar CxC"
'============================================== Importacion de CxP =================================================================================
Private Const CM_MESSAGE_NAME_CXP As String = "Importar CxP"
Private Const CM_INDEX_NUMERO_CXC_Y_CODIGO_PROVEEDOR As Long = 0
'============================================>>>ARTICULOS DE INVENTARIO<<<<=======================================================================================
Private Const CM_MESSAGE_NAME_ARTICULOS As String = "Importar Articulos de Inventario"
Private Const CM_INDEX_CODIGO_ARTICULO As Long = 1
Private Const CM_INDEX_CONSECUTIVO_COMPANIA_DETALLE_ARTICULO As Integer = 0
Private Const CM_INDEX_CODIGO_ARTICULO_DETALLE As Integer = 3
Private Const CM_INDEX_VALOR_IMPORTA_ARTICULO_DETALLE As Integer = 2

Private LineaDeErrores As String
Private NumerodeLinea As Integer
Private ErrorDeTraduccion As Boolean
Private FileNumberdeLectura As Integer
Private FileNumberdeErrores As Integer
Private NombreArchivoImportacion As String
Private RegistrosYaExistentesEnBD As Long
Private mNumerodeRegistros As Long
Private mNregistroAInsertar As Long
Private mCodigoLote As String
Private mNumeroDeLote As String
Private mRealizarBusquedaEnArchivo As Boolean
Private mProgressBar As ProgressBar
Private mYaIngresoUnoMas As Boolean
Private mVectorDeVerificaciones() As Variant
Private mConsecutivoRenglon As Long
Private mValorDelCampoDeId As String
Private mImportoRegistros As Boolean
Private vConsecutivoCompania As Long
Private vFecha As Date
Private vFechaUltimaModificacion As Date
Private vNombre As String
Private vNumeroFactura As String
Private vCodigoCliente As String
Private vCodigoVendedor As String
Private vCodigoAlmacen As String
Private vArticulo As String
Private vMoneda As String
Private vMonto As Currency
Private vDescripcion As String
Private vObservaciones As String
Private vTotalRenglones As Currency
Private vTipoDeDocumentoStr As String
Private vConsecutivoCaja
Private vConsecutivoRenglon As Long
Private vNombreOperador As String
Private vConsecutivoAlmacen As String
Private vAlicuotaIVA As String

Private mEnumClaseDeTrabajoFactura As enum_ClaseDeTrabajoFacturaCotizacion
Private mModuloAImportar As enum_EntidadAExportarOImportar
Private EnumSystemModules As enum_SystemModules
   
Private insImportar As clsImportar
Private mSeImportaComoHistorica As Boolean
'VENDEDORES
Private vConsecutivoVendedor As Integer
Private vRIF As String
Private vStatusVendedor As String
Private vDireccion As String
Private vCiudad As String
Private vZonaPostal As String
Private vTelefono As String
Private vFax As String
Private vEmail As String
Private vNotas As String
Private vComisionPorVenta As Currency
Private vComisionPorCobro As Currency
Private vTopeInicialVenta1 As Currency
Private vTopeFinalVenta1 As Currency
Private vPorcentajeVentas1 As Currency
Private vTopeFinalVenta2 As Currency
Private vPorcentajeVentas2 As Currency
Private vTopeFinalVenta3 As Currency
Private vPorcentajeVentas3 As Currency
Private vTopeFinalVenta4 As Currency
Private vPorcentajeVentas4 As Currency
Private vTopeFinalVenta5 As Currency
Private vPorcentajeVentas5 As Currency
Private vTopeInicialCobranza1 As Currency
Private vTopeFinalCobranza1 As Currency
Private vPorcentajeCobranza1 As Currency
Private vTopeFinalCobranza2 As Currency
Private vPorcentajeCobranza2 As Currency
Private vTopeFinalCobranza3 As Currency
Private vPorcentajeCobranza3 As Currency
Private vTopeFinalCobranza4 As Currency
Private vPorcentajeCobranza4 As Currency
Private vTopeFinalCobranza5 As Currency
Private vPorcentajeCobranza5 As Currency
Private vUsaComisionPorVenta As Boolean
Private vUsaComisionPorCobranza As Boolean
Private mNombreDeLineaDeProducto As String
Private mTipoDeComision As String
Private mPorcentaje As Currency
Private vConsecutivoRutaDeComercialzacion As Integer
'CxC
Private mNumeroYTipoDeCxC As String
'Cotizacion
Private vCambioABolivaresEnCotizacion As Currency
Private vCambioMostrarTotalEnDivisasEnCotizacion As Currency
Private vNroDiasMantenerCambioAMonedaLocal As Integer
Private vFechaLimiteCambioAMonedaLocal As Date
'FACTURA
Private mGenerarCxC As Boolean
Private vMontoExentoDespuesDeDescuento As Currency
Private vBaseImponibleDespuesDescuento As Currency
Private vTotalIVA As Currency
Private vTotalFactura As Currency
Private vPorcentajeDescuento As Currency
Private vCodigoNota1 As String
Private vCodigoNota2 As String
Private vNivelPrecio As String
Private vStatusFactura As String
Private vInsertadaManualmente As Boolean
Private vCancelada As Boolean
Private vUsarDireccionFiscal As Boolean
Private vNoDirDespachoAimprimir As Long
Private vCambioABolivares As Currency
Private vMontoDelAbono As Currency
Private vFechaDeVencimiento As Date
Private vCondicionesDePago As String
Private vFacturaHistorica As Boolean
Private vTalonario As String
Private vFormaDeLaInicialStr As String
Private vPorcentajeDeLaInicial As Currency
Private vNumeroDeCuotas As Long
Private vMontoDeLasCuotas As Currency
Private vMontoUltimaCuota As Currency
Private vFormaDePagoStr As String
Private vNumDiasDeVencimiento1aCuota As Long
Private vEditarMontoCuota As Boolean
Private vNumeroControl As String
Private vTipoDeTransaccionStr As String
Private vNumeroPlanillaExportacion As String
Private vNumeroFacturaAfectada As String
Private vTipoDeVentaStr As String
Private vUsaMaquinaFiscal As Boolean
Private vCodigoMaquinaRegistradora As String
Private vNumeroDesde As String
Private vNumeroHasta As String
Private vNumeroControlHasta As String
Private vMontoIvaRetenido As Currency
Private vFechaAplicacionRetIVA As Date
Private vNumeroComprobanteRetIVA As Long
Private vFechaComprobanteRetIVA As Date
Private vSeRetuvoIva As Boolean
Private vFacturaConPreciosSinIva As Boolean
Private vGeneraCobroDirecto As Boolean
Private vFechaDeFacturaAfectada As Date
Private vPorcentajeBaseImponible As Currency
Private vAlicuotaIVAStr As String
Private vCantidad As Currency
Private vPrecioSinIVA As Currency
Private vPrecioConIVA As Currency
Private vTotalRenglon As Currency
Private vCampoExtraEnRenglonFactura1
Private vCampoExtraEnRenglonFactura2
Private vDescuentoRenglon As Currency
Private vVendedor1Renglon As String
Private vVendedor2Renglon As String
Private vVendedor3Renglon As String
Private mPorcentajeAlicuota1 As Currency
Private mPorcentajeAlicuota2 As Currency
Private mPorcentajeAlicuota3 As Currency
Private mAplicaDecretoIvaEspecial As String
Private mPorcentajeAlicuotaRenglonFactura As Currency
Private mTipoDeContribuyenteClienteFactura As String
Private mTipoDePersonaClienteFactura As String
Private mMontoGravableAlicuota1 As Currency
Private mMontoGravableAlicuota2 As Currency
Private mMontoGravableAlicuota3 As Currency
Private mMontoIVAAlicuota1 As Currency
Private mMontoIVAAlicuota2 As Currency
Private mMontoIVAAlicuota3 As Currency
'CXP
Private mNumeroYCodigoProveedor As String
'ARTICULOS DE INVENTARIO
Private Const CM_MERCANCIA As String = "Mercancia"
Private Const CM_ALICUOTA_GENERAL As String = "Alicuota General"
Private SeImportaConSeparador As Boolean
Private valSeparador As String
Private tipoDeSeparacion As String
Private CrearComprobanteApertura As Boolean
Private FileNumberdeCreado As Integer
Private mLineaDeProducto() As Variant
Private NombreArchivodeErrores As String
Private mLabelPorcentaje As Label
Private mMensajePrecioConIva As Boolean
Private mMensajePrecioSinIva As Boolean
Private mCambiarAUsaMultiplesAlicuotas As Long
Private mArchivoCreado As String
Private mCallingWindow As Form
Private mNregistroExistenBd As Long
Private valDatosDeInventario() As Variant

Private valCodigoAlmacenExtra As String
Private valNombreAlmacenExtra As String
Private valTipoDeAlmacenExtra As String
Private valCodigoDelClienteExtra As String
Private valCodigoCcExtra As String
Private valDescripcionExtra As String
Private valExistenciaAlmacenExtra As Currency
Private valCodigoArticulo As String
Private valConsecutivoAlmacen As Long
Private mBaseImponibleIGTF As Currency
Private mIGTFML As Currency
Private mIGTFME As Currency
Private mAlicuotaIGTF As Currency

Const REGISTRO_LINEA_PRODUCTO = 0
Const CONSECUTIVO_COMPANIA = 1
Const CODIGO_ARTICULO = 0
Const CANTIDAD_DE_ARTICULO = 1
Const TIPO_DE_ARTICULO = 2
Const CODIGO_ALMACEN = 3
Const SERIAL = 4
Const ROLLO = 5
Const TIPO_ARTICULO_INV = 6
Const CONSECUTIVO_ALMACEN = 7
Const NUMERO_FACTURA = 0
Const MONTOMAXIMODECRETO2602 = 200000

Private Function getPathDelFileDeErrores() As String
   getPathDelFileDeErrores = gDefDatabase.getPathActualDeLaBaseDeDatos & "\ImpExp"
End Function

Private Sub SetCodigoLote(ByVal valNewValue As String)
   mCodigoLote = gTexto.DfMid(valNewValue, 1, 10)
End Sub

Private Function getNombreFileDeErrores() As String
   getNombreFileDeErrores = getPathDelFileDeErrores & "\Errores.txt"
End Function

Private Function fDistribuyeLaLineaDeProveedor(ByRef refLineaActual As String, ByVal valLineaActual As Long, ByVal valSeparador As String, ByVal valVerificarSiEstaRepetido As Boolean) As Boolean
   Dim texto As String
   Dim linea As String
   Dim lineaTipo As String
   Dim tipoProveedor As String
   Dim ErrorAlTraducirLaLinea As Boolean
   Dim ExisteArticuloEnArchivo As Boolean
   Dim TipoPersona As String
   Dim i As Integer
   Dim vEmail As String
   Dim vArrayEmail As Variant
   Dim j As Integer
   Dim vItem As String
   Dim CodigoPaisResidencia As String
   Dim tipoDocumentoIdentificacion As String
   Dim TipoPersonaLibrosElectronicos As String
   Dim CodigoConveniosSunat As String
   On Error GoTo h_ERROR
   ErrorAlTraducirLaLinea = False
   LineaDeErrores = ""
   linea = refLineaActual
   lineaTipo = refLineaActual
   insProveedor.SetNombreOperador mNombreDelUsuario
   insProveedor.SetCodigoProveedor gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetNombreProveedor gTexto.DfMid(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador), 1, 255)
   insProveedor.SetContacto gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetNumeroRIF gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetNumeroNIT gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   TipoPersona = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetCodigoRetencionUsual gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetTelefonos gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetDireccion gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetFax gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetEmail gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetTipodeProveedor gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetTipoDeProveedorDeLibrosFiscalesStr gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetPorcentajeRetencionIVA gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insProveedor.SetCuentaContableCxP gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetCuentaContableGastos gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetCuentaContableAnticipo gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetBeneficiario gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetUsarBeneficiarioImpCheq gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   tipoDocumentoIdentificacion = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   If (tipoDocumentoIdentificacion = "") Then
      insProveedor.SetTipoDocumentoIdentificacionStr "0"
   Else
      insProveedor.SetTipoDocumentoIdentificacionStr tipoDocumentoIdentificacion
   End If
   insProveedor.SetNumeroCuentaBancaria gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetNombre gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetApellidoPaterno gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insProveedor.SetApellidoMaterno gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   TipoPersonaLibrosElectronicos = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   If (TipoPersonaLibrosElectronicos = "") Then
      insProveedor.SetTipoDePersonaLibrosElectronicosStr gConvert.enumerativoAChar(enum_TipoDePersonaLibrosElectronicos.eTDPLE_JuridicoDomiciliado)
   ElseIf insProveedor.fEsInvalidoTipoDePersonaLibElectonico(TipoPersonaLibrosElectronicos) Then
      LineaDeErrores = LineaDeErrores & " ::. ADVERTENCIA El Tipo de Persona para libros electrónicos ==>" & TipoPersonaLibrosElectronicos & "<==  No es válido, se colocará por defecto " & gEnumProyecto.enumTipoDePersonaLibrosElectronicosToString(enum_TipoDePersonaLibrosElectronicos.eTDPLE_JuridicoDomiciliado) & " .:: "
   Else
      insProveedor.SetTipoDePersonaLibrosElectronicosStr gEnumProyecto.enumTipoDePersonaLibrosElectronicosToString(gConvert.charAEnumerativoInt(TipoPersonaLibrosElectronicos))
   End If
   CodigoPaisResidencia = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   If (CodigoPaisResidencia = Null) Then
      insProveedor.SetCodigoPaisResidencia ""
   Else
      insProveedor.SetCodigoPaisResidencia CodigoPaisResidencia
   End If
   CodigoConveniosSunat = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   If (CodigoConveniosSunat = Null) Then
      insProveedor.SetCodigoConveniosSunat ""
   Else
      insProveedor.SetCodigoConveniosSunat CodigoConveniosSunat
   End If
   If (gProyParametrosCompania.GetRetieneImpuestoMunicipal) Then
      insProveedor.SetCodigoContribuyente gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      insProveedor.SetNumeroRUC gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   End If
   LineaDeErrores = LineaDeErrores & String(Len(insProveedor.getCodigoProveedor), " ")
   LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
   If valVerificarSiEstaRepetido Then
      If insImportar.fYaFueCargadoElItemEnElArray(mVectorDeVerificaciones, valLineaActual, insProveedor.getCodigoProveedor, CM_INDEX_CODIGO_PROVEEDOR) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & " ::. ERROR El Código del proveedor  ==>" & insProveedor.getCodigoProveedor & "-" & insProveedor.getNombreProveedor & "<==  aparece más de una vez en el archivo de importación, este último proveedor no se importará .:: "
      Else
         If CM_INDEX_CODIGO_PROVEEDOR < UBound(mVectorDeVerificaciones) And CM_INDEX_CODIGO_PROVEEDOR >= LBound(mVectorDeVerificaciones) Then
            mVectorDeVerificaciones(CM_INDEX_CODIGO_PROVEEDOR, valLineaActual) = insProveedor.getCodigoProveedor
         End If
      End If
      If insImportar.fYaFueCargadoElItemEnElArray(mVectorDeVerificaciones, valLineaActual, insProveedor.getNombreProveedor, CM_INDEX_DESCRIPCION_PROVEEDOR) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & " ::. ADVERTENCIA El Nombre del proveedor  ==>" & insProveedor.getCodigoProveedor & "-" & insProveedor.getNombreProveedor & "<==  aparece más de una vez en el archivo de importaciónsin embargo se permitirá su importación, modifique luego este dato .:: "
      ElseIf insProveedor.fSearchByFieldImpExp("NombreProveedor", insProveedor.getNombreProveedor) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & " ::. ADVERTENCIA El Nombre del proveedor  ==>" & insProveedor.getCodigoProveedor & "-" & insProveedor.getNombreProveedor & "<==  ya se encuentra asignado en el sistema, sin embargo se permitirá su importación, modifique luego este dato .:: "
      Else
         If CM_INDEX_DESCRIPCION_PROVEEDOR < UBound(mVectorDeVerificaciones) And CM_INDEX_DESCRIPCION_PROVEEDOR >= LBound(mVectorDeVerificaciones) Then
            mVectorDeVerificaciones(CM_INDEX_DESCRIPCION_PROVEEDOR, valLineaActual) = insProveedor.getNombreProveedor
         End If
      End If
   End If
   LineaDeErrores = LineaDeErrores & String(Len(insProveedor.getContacto), " ")
   If gTexto.DfLen(insProveedor.getNumeroRIF) = 0 And valVerificarSiEstaRepetido Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & " ::. ERROR EL Número de RIF del Proveedor  ==>" & insProveedor.getCodigoProveedor & "-" & insProveedor.getNombreProveedor & "<==  No puede estar en blanco, no se importará este registro .:: "
   ElseIf insProveedor.fSearchByFieldImpExp("NumeroRIF", insProveedor.getNumeroRIF) And valVerificarSiEstaRepetido Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & " ERROR EL Número de RIF del Proveedor  ==>" & insProveedor.getCodigoProveedor & "-" & insProveedor.getNombreProveedor & "<==  ya ha sido asignado a un proveedor registrado en el sistema."
   ElseIf Not insInputCamposEspeciales.fNumeroDeRifIsValid(insProveedor.getNumeroRIF, False) And valVerificarSiEstaRepetido Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & " ERROR EL Número de RIF (" & insProveedor.getNumeroRIF & ") del Proveedor  ==>" & insProveedor.getCodigoProveedor & "-" & insProveedor.getNombreProveedor & "<==  no es válido, por lo tanto este registro no se importará."
   Else
      LineaDeErrores = LineaDeErrores & String(Len(insProveedor.getNumeroRIF), " ")
   End If
   LineaDeErrores = LineaDeErrores & String(Len(insProveedor.getNumeroNIT), " ")
   LineaDeErrores = LineaDeErrores & String(Len(insProveedor.getTelefonos), " ")
   LineaDeErrores = LineaDeErrores & String(Len(insProveedor.getFax), " ")
   If gTexto.DfLen(insProveedor.getEmail) > 0 Then
      vEmail = insProveedor.getEmail
      vArrayEmail = gTexto.fConvertCadenaEnArray(vEmail, ";")
      For j = LBound(vArrayEmail) To UBound(vArrayEmail)
         vItem = vArrayEmail(j)
         If Not gTexto.fDireccionDeMailEsValida(vItem, False) And valVerificarSiEstaRepetido Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & " ::. ERROR La Dirección de Correo Electrónico (" & insProveedor.getEmail & ")  del Proveedor  ==>" & insProveedor.getCodigoProveedor & "-" & insProveedor.getNombreProveedor & "<==  no es válida, por lo tanto este campo del registro se importará en >> Blanco << .:: "
            Exit For
         ElseIf Not gTexto.fDireccionDeMailEsValida(vItem, False) Then
            insProveedor.SetEmail ""
         Else
            LineaDeErrores = LineaDeErrores & String(Len(insProveedor.getEmail), " ")
            Exit For
         End If
      Next
   Else
      LineaDeErrores = LineaDeErrores & String(Len(insProveedor.getEmail), " ")
   End If
   If gTexto.DfLen(insProveedor.GetTipoDeProveedorDeLibrosFiscalesStr) > 0 And valVerificarSiEstaRepetido Then
      Select Case gEnumProyecto.strTipoDeProveedorDeLibrosFiscalesToNum(insProveedor.GetTipoDeProveedorDeLibrosFiscalesStr)
         Case enum_TipoDeProveedorDeLibrosFiscales.eTD_CON_RIF, enum_TipoDeProveedorDeLibrosFiscales.eTD_NO_DOMICILIADO, enum_TipoDeProveedorDeLibrosFiscales.eTD_NO_RESIDENCIADO, enum_TipoDeProveedorDeLibrosFiscales.eTD_SIN_RIF
            LineaDeErrores = LineaDeErrores & String(Len(insProveedor.GetTipoDeProveedorDeLibrosFiscalesStr), " ")
         Case Else: LineaDeErrores = LineaDeErrores & " ERROR en el Campo PROVEEDOR DE LIBROS FISCALES ==> " & insProveedor.GetTipoDeProveedorDeLibrosFiscalesStr & " no es un valor válido para éste."
      End Select
   ElseIf gTexto.DfLen(insProveedor.GetTipoDeProveedorDeLibrosFiscalesStr) = 0 Then
      insProveedor.SetTipoDeProveedorDeLibrosFiscalesStr gEnumProyecto.enumTipoDeProveedorDeLibrosFiscalesToString(eTD_CON_RIF)
   Else
      Select Case gEnumProyecto.strTipoDeProveedorDeLibrosFiscalesToNum(insProveedor.GetTipoDeProveedorDeLibrosFiscalesStr)
         Case enum_TipoDeProveedorDeLibrosFiscales.eTD_CON_RIF, enum_TipoDeProveedorDeLibrosFiscales.eTD_NO_DOMICILIADO, enum_TipoDeProveedorDeLibrosFiscales.eTD_NO_RESIDENCIADO, enum_TipoDeProveedorDeLibrosFiscales.eTD_SIN_RIF
         Case Else: insProveedor.SetTipoDeProveedorDeLibrosFiscalesStr gEnumProyecto.enumTipoDeProveedorDeLibrosFiscalesToString(eTD_CON_RIF)
      End Select
   End If
   If valVerificarSiEstaRepetido And gTexto.DfLen(insProveedor.getCuentaContableCxP) > 0 Then
      If Not insCtaContable.fSearchCuentaByCodigo(insProveedor.getCuentaContableCxP) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & " ::. ERROR en el Campo CUENTA CONTABLE CxP del Proveedor ==> " & insProveedor.getCodigoProveedor & " - " & insProveedor.getNombreProveedor & ", la cta contable " & insProveedor.getCuentaContableCxP & " no esta creada en el catlógo de ctas de esta empresa. Este campo se colocará en blanco."
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insProveedor.getCuentaContableCxP), " ")
      End If
   Else
      If Not insCtaContable.fSearchCuentaByCodigo(insProveedor.getCuentaContableCxP) Then
         insProveedor.SetCuentaContableCxP ""
      End If
      LineaDeErrores = LineaDeErrores & String(Len(insProveedor.getCuentaContableCxP), " ")
   End If
   If valVerificarSiEstaRepetido And gTexto.DfLen(insProveedor.getCuentaContableGastos) > 0 Then
      If Not insCtaContable.fSearchCuentaByCodigo(insProveedor.getCuentaContableGastos) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & " ::. ERROR en el Campo CUENTA CONTABLE Gastos del Proveedor ==> " & insProveedor.getCodigoProveedor & " - " & insProveedor.getNombreProveedor & ", la cta contable: " & insProveedor.getCuentaContableGastos & " no esta creada en el catlógo de ctas de esta empresa. Este campo se colocará en blanco .::"
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insProveedor.getCuentaContableCxP), " ")
      End If
   Else
      If Not insCtaContable.fSearchCuentaByCodigo(insProveedor.getCuentaContableGastos) Then
         insProveedor.SetCuentaContableGastos ""
      End If
      LineaDeErrores = LineaDeErrores & String(Len(insProveedor.getCuentaContableGastos), " ")
   End If
   If gTexto.DfLen(insProveedor.getTipodeProveedor) = 0 Then
      If valVerificarSiEstaRepetido Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & " ADVERTENCIA: Como el tipo de proveedor esta en blanco, se le asignará como valor por defecto >> " & CM_TIPO_DE_PROVEEDOR_PREDEF_PARA_IMP & " <<."
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insProveedor.getTipodeProveedor), " ")
      End If
      sVerificaSiElTipoDeProveedorExisteSiNoLoCrea
      insProveedor.SetTipodeProveedor CM_TIPO_DE_PROVEEDOR_PREDEF_PARA_IMP
   Else
      sVerificaSiElTipoDeProveedorExisteSiNoLoCrea
      If Not insTipoProveedor.fSearchByField("Nombre", insProveedor.getTipodeProveedor) Then
         insTipoProveedor.sInsertaValorPorDefecto mConsecutivoCompaniaActual, insProveedor.getTipodeProveedor
      End If
      LineaDeErrores = LineaDeErrores & String(Len(insProveedor.getTipodeProveedor), " ")
   End If
   LineaDeErrores = LineaDeErrores & String(Len(insProveedor.getDireccion), " ")
   If gTexto.DfLen(TipoPersona) > 0 And valVerificarSiEstaRepetido Then
     TipoPersona = gEnumTablaRetencion.fAbreviacionTipodePersonaRetToTitulo(TipoPersona)
     If gTexto.DfLen(TipoPersona) = 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & " ERROR en el Campo TIPO DE PERSONA FISCAL  contiene una valor inválido para proveedores, será sustituido por >> " & gEnumTablaRetencion.enumTipodePersonaRetencionToString4(eTP_PJ_DOMICILIADA) & " <<."
         insProveedor.SetTipoDePersonaStr gEnumProyecto.enumTipodePersonaRetencionToString(eTP_PJ_DOMICILIADA)
     Else
         LineaDeErrores = LineaDeErrores & String(Len(TipoPersona), " ")
         insProveedor.SetTipoDePersonaStr TipoPersona
      End If
   ElseIf gTexto.DfLen(TipoPersona) = 0 Then
      insProveedor.SetTipoDePersonaStr gEnumProyecto.enumTipodePersonaRetencionToString(eTP_PJ_DOMICILIADA)
   Else
      TipoPersona = gEnumTablaRetencion.fAbreviacionTipodePersonaRetToTitulo(TipoPersona)
      If gTexto.DfLen(TipoPersona) = 0 Then
         insProveedor.SetTipoDePersonaStr gEnumProyecto.enumTipodePersonaRetencionToString(eTP_PJ_DOMICILIADA)
     Else
         insProveedor.SetTipoDePersonaStr TipoPersona
      End If
   End If
   If gTexto.DfLen(insProveedor.GetCodigoRetencionUsual) = 0 Then
      sVerificaSiElCodigoRetUsualPredefinidoExisteSiNoLoCrea insProveedor.GetTipoDePersonaChar, insProveedor.getTipoDePersonaStr
      insProveedor.SetCodigoRetencionUsual CM_COD_DE_RET_USUAL_PREDEF_PARA_IMP
   End If
   If valVerificarSiEstaRepetido Then
      If gTexto.DfLen(insProveedor.GetCodigoRetencionUsual) = 0 Then
         LineaDeErrores = LineaDeErrores & " ::. Advertencia  El Código de Retención Usuaal del Proveedor ==> " & insProveedor.getCodigoProveedor & " -  esta en blanco. Este valor no es aceptado por la tanto se reemplazará por >> " & CM_COD_DE_RET_USUAL_PREDEF_PARA_IMP & " << .::"
      ElseIf Not insTablaRetencionNavigator.fSearchByCodigoRetencionAndTipoPersona(insProveedor.GetCodigoRetencionUsual, insProveedor.GetTipoDePersonaChar, False, False) Then
         LineaDeErrores = LineaDeErrores & " ::. Error  El Código de Retención Usual (" & insProveedor.GetCodigoRetencionUsual & ") asignado al Proveedor ==> " & insProveedor.getCodigoProveedor & " -  contiene un valor no contemplado en la Tabla de Retención de esta compañía o su combinación con el Tipo de Persona de Retención de I.S.R.L. no existe. Este valor no es aceptado por la tanto se reemplazará por >> " & CM_COD_DE_RET_USUAL_PREDEF_PARA_IMP & " <<  y Tipo de Persona >> " & insProveedor.getTipoDePersonaStr & " << .::"
         ErrorAlTraducirLaLinea = True
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insProveedor.GetCodigoRetencionUsual), " ")
      End If
   End If
   LineaDeErrores = LineaDeErrores & String(Len(insProveedor.GetCodigoRetencionUsual), " ")
h_EXIT: On Error GoTo 0
   fDistribuyeLaLineaDeProveedor = Not ErrorAlTraducirLaLinea
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fDistribuyeLaLineaDeProveedor", CM_MESSAGE_NAME_PROVEEDOR, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fVerificaSiNoExisteElProveedorYLoAgrega() As Boolean
   Dim Continuar As Boolean
   On Error GoTo h_ERROR
   Continuar = False
   If gTexto.DfLen(insProveedor.getCodigoProveedor) > 0 And gTexto.DfLen(insProveedor.getNumeroRIF) > 0 And Not insProveedor.fSearchByFieldImpExp("CodigoProveedor", insProveedor.getCodigoProveedor) And _
      Not insProveedor.fSearchByFieldImpExp("NumeroRIF", insProveedor.getNumeroRIF) And insInputCamposEspeciales.fNumeroDeRifIsValid(insProveedor.getNumeroRIF, True) And gTexto.DfLen(insProveedor.getNombreProveedor) > 0 And Not insProveedor.fSearchByFieldImpExp("NombreProveedor", insProveedor.getNombreProveedor) Then
      If fInsertarProveedor Then
         Continuar = True
      End If
   Else
      RegistrosYaExistentesEnBD = RegistrosYaExistentesEnBD + 1
   End If
h_EXIT: On Error GoTo 0
   fVerificaSiNoExisteElProveedorYLoAgrega = Continuar
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fVerificaSiNoExisteElProveedorYLoAgrega", CM_MESSAGE_NAME_PROVEEDOR, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sVerificaSiElCodigoRetUsualPredefinidoExisteSiNoLoCrea(ByVal valGetTipoDePersonaChar As String, ByVal valGetTipoDePersonaStr As String)
   On Error GoTo h_ERROR
   If Not insTablaRetencionNavigator.fSearchByCodigoRetencionAndTipoPersona(CM_COD_DE_RET_USUAL_PREDEF_PARA_IMP, valGetTipoDePersonaChar, False, False) Then
      insTablaRetencionNavigator.SetCodigo CM_COD_DE_RET_USUAL_PREDEF_PARA_IMP
      insTablaRetencionNavigator.SetTipoDePago "Honorarios Profesionales"
      insTablaRetencionNavigator.SetComentarios "Incluido en forma automática en un proceso de importación."
      insTablaRetencionNavigator.SetAcumulaParaPJND False
      insTablaRetencionNavigator.SetBaseImponible 100
      insTablaRetencionNavigator.SetTarifa 5
      insTablaRetencionNavigator.SetFechaAplicacion #1/7/1994#
      insTablaRetencionNavigator.SetParaPagosMayoresDe 25000
      insTablaRetencionNavigator.SetSustraendo 0
      insTablaRetencionNavigator.SetSecuencialDePlantilla "13002"
      insTablaRetencionNavigator.SetTipoDePersonaStr valGetTipoDePersonaStr
      If insTablaRetencionNavigator.fInsert Then
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sVerificaSiElCodigoRetUsualPredefinidoExisteSiNoLoCrea", CM_MESSAGE_NAME_PROVEEDOR, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sVerificaSiElTipoDeProveedorExisteSiNoLoCrea()
   On Error GoTo h_ERROR
   If Not insTipoProveedor.fSearchByField("Nombre", CM_TIPO_DE_PROVEEDOR_PREDEF_PARA_IMP) Then
      insTipoProveedor.sInsertaValorPorDefecto mConsecutivoCompaniaActual, CM_TIPO_DE_PROVEEDOR_PREDEF_PARA_IMP
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sVerificaSiElTipoDeProveedorExisteSiNoLoCrea", CM_MESSAGE_NAME_PROVEEDOR, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fDistribuyeLaLineaDeCliente(ByRef refLineaActual As String, ByVal valLineaActual As Long, ByVal valSeparador As String, ByVal valVerificarSiEstaRepetido As Boolean) As Boolean
   Dim linea As String
   Dim ErrorAlTraducirLaLinea As Boolean
   Dim ExisteArticuloEnArchivo As Boolean
   Dim valProveedorArray() As Variant
   Dim TextoAuxiliar As String
   Dim mrsZona As ADODB.Recordset
   Dim SQL As String
   Dim existeZona As Boolean
   Dim clienteDesdeFecha As String
   Dim vTipoDeContribuyente As String
   On Error GoTo h_ERROR
   ErrorAlTraducirLaLinea = False
   LineaDeErrores = ""
   linea = refLineaActual
   insCliente.SetNombreOperador mNombreDelUsuario
   insCliente.SetCodigo gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetNombre gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetNumeroRIF gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetNumeroNIT gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetCuentaContableCxC gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetCuentaContableIngresos gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetStatusStr gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetTelefono gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetFax gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetDireccion gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetCiudad gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetZonaPostal gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetZonaDeCobranza gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetSectorDeNegocio gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetCodigoVendedor gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetEsExtranjero gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCliente.SetEmail gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetContacto gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetRazonInactividad gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetActivarAvisoAlEscoger gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCliente.SetTextoDelAviso gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetCuentaContableAnticipo gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetNivelDePrecioStr gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetOrigenStr gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   clienteDesdeFecha = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vTipoDeContribuyente = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCliente.SetTipoDeContribuyenteStr gEnumProyecto.enumTipoDeContribuyenteToString(gConvert.charAEnumerativoInt(vTipoDeContribuyente))
   If gUtilDate.fElStringContieneUnaFechaValida(clienteDesdeFecha, False) Then
      insCliente.SetClienteDesdeFecha clienteDesdeFecha
   Else
      insCliente.SetClienteDesdeFecha gUtilDate.getFechaDeHoy
   End If
   If gProyParametrosCompania.GetEsAsociadoEnCtaDeParticipacion Then
      insCliente.SetDiaCumpleanos gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      insCliente.SetMesCumpleanos gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      insCliente.SetCorrespondenciaXEnviar gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   End If
   If valVerificarSiEstaRepetido Then
      If gTexto.DfLen(insCliente.GetCodigo) = 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Código del Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==  no puede estar en blanco .:::"
      ElseIf insCliente.fSearchByField("Codigo", insCliente.GetCodigo, False, False) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Código del Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==  ya esta registrado en el sistema .:::"
      ElseIf insImportar.fYaFueCargadoElItemEnElArray(mVectorDeVerificaciones, valLineaActual, insCliente.GetCodigo, CM_INDEX_CODIGO_CLIENTE) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Código del Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==  aparece más de una vez en el archivo de importación .:::"
      Else
         If CM_INDEX_CODIGO_CLIENTE < UBound(mVectorDeVerificaciones) And CM_INDEX_CODIGO_CLIENTE >= LBound(mVectorDeVerificaciones) Then
            mVectorDeVerificaciones(CM_INDEX_CODIGO_CLIENTE, valLineaActual) = insCliente.GetCodigo
         End If
         LineaDeErrores = LineaDeErrores & String(Len(insCliente.GetCodigo), " ")
      End If
      If gTexto.DfLen(insCliente.getNombre) = 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Nombre del Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==  no puede estar en blanco .:::"
      ElseIf insCliente.fSearchByField("Nombre", insCliente.getNombre, False, False) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Nombre del Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==  ya esta registrado en el sistema .:::"
      ElseIf insImportar.fYaFueCargadoElItemEnElArray(mVectorDeVerificaciones, valLineaActual, insCliente.getNombre, CM_INDEX_NOMBRE_CLIENTE) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Nombre del Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==  aparece más de una vez en el archivo de importación .:::"
      Else
         If CM_INDEX_NOMBRE_CLIENTE < UBound(mVectorDeVerificaciones) And CM_INDEX_NOMBRE_CLIENTE >= LBound(mVectorDeVerificaciones) Then
            mVectorDeVerificaciones(CM_INDEX_NOMBRE_CLIENTE, valLineaActual) = insCliente.getNombre
         End If
         LineaDeErrores = LineaDeErrores & String(Len(insCliente.getNombre), " ")
      End If
      If gTexto.DfLen(insCliente.getNumeroRIF) = 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Número de RIF  del Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==  no puede estar en blanco .:::"
      ElseIf insCliente.fSearchByField("NumeroRIF", insCliente.getNumeroRIF, False, False) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Número de RIF  del Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & " - RIF: " & insCliente.getNumeroRIF & " <==  ya esta registrado en el sistema .:::"
      ElseIf insImportar.fYaFueCargadoElItemEnElArray(mVectorDeVerificaciones, valLineaActual, insCliente.getNumeroRIF, CM_INDEX_RIF_CLIENTE) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Número de RIF  del Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & " - RIF: " & insCliente.getNumeroRIF & "<==  aparece más de una vez en el archivo de importación .:::"
      ElseIf Not insInputCamposEspeciales.fNumeroDeRifIsValid(insCliente.getNumeroRIF, False) And valVerificarSiEstaRepetido Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & " ERROR EL Número de RIF (" & insCliente.getNumeroRIF & ") del Proveedor  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==  no es válido, por lo tanto este registro no se importará."
      Else
        LineaDeErrores = LineaDeErrores & String(Len(insCliente.getNombre), " ")
        LineaDeErrores = LineaDeErrores & String(Len(insCliente.getNumeroRIF), " ")
      End If
      LineaDeErrores = LineaDeErrores & String(Len(insCliente.getNumeroNIT), " ")
      If gTexto.DfLen(insCliente.GetCuentaContableCxC) > 0 Then
         If Not insCtaContable.fSearchCuentaByCodigo(insCliente.GetCuentaContableCxC) Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR La Cta. Contable para CxC del Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==  No esta definida en el catalogo de cuentas, se colocará en blanco .:::"
         Else
            LineaDeErrores = LineaDeErrores & String(Len(insCliente.GetCuentaContableCxC), " ")
         End If
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insCliente.GetCuentaContableCxC), " ")
      End If
      If gTexto.DfLen(insCliente.GetCuentaContableIngresos) > 0 Then
         If Not insCtaContable.fSearchCuentaByCodigo(insCliente.GetCuentaContableIngresos) Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR La Cta. Contable para Ingresos del Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==  No esta definida en el catalogo de cuentas, se colocará en blanco .:::"
         Else
            LineaDeErrores = LineaDeErrores & String(Len(insCliente.GetCuentaContableIngresos), " ")
         End If
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insCliente.GetCuentaContableIngresos), " ")
      End If
      If gTexto.DfLen(insCliente.GetStatusStr) > 0 Then
         If gEnumProyecto.strStatusClienteToNum(insCliente.GetStatusStr) <> enum_StatusCliente.eSC_ACTIVO And gEnumProyecto.strStatusClienteToNum(insCliente.GetStatusStr) <> enum_StatusCliente.eSC_DATOS_DESACTUALIZADOS And gEnumProyecto.strStatusClienteToNum(insCliente.GetStatusStr) <> enum_StatusCliente.eSC_INACTIVO And gEnumProyecto.strStatusClienteToNum(insCliente.GetStatusStr) <> enum_StatusCliente.eSC_RESTRINGIDO And gEnumProyecto.strStatusClienteToNum(insCliente.GetStatusStr) <> enum_StatusCliente.eSC_SUSPENDIDO And gEnumProyecto.strStatusClienteToNum(insCliente.GetStatusStr) <> enum_StatusCliente.eSC_TIEMPO_SIN_CONTACTO Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR Indicó un Status no válido para el Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==   se reemplazará por " & gEnumProyecto.enumStatusClienteToString(eSC_ACTIVO) & " .:::"
         Else
            LineaDeErrores = LineaDeErrores & String(Len(insCliente.GetStatusStr), " ")
         End If
      Else
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Status del Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==  esta en blanco. Se remplazará por " & gEnumProyecto.enumStatusClienteToString(eSC_ACTIVO) & " .:::"
      End If
      LineaDeErrores = LineaDeErrores & String(Len(insCliente.getTelefono), " ")
      LineaDeErrores = LineaDeErrores & String(Len(insCliente.getFax), " ")
      LineaDeErrores = LineaDeErrores & String(Len(insCliente.getDireccion), " ")
      If gTexto.DfLen(insCliente.getCiudad) > 0 Then
         If Not insCiudad.fSearchByField("NombreCiudad", insCliente.getCiudad, False) Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR Indicó una Ciudad <<< " & insCliente.getCiudad & " >>> para el Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==   que no esta Almacenada en la Tabla de Ciudades. Se incluirá automaticamente.:::"
            insCiudad.SetNombreCiudad (insCliente.getCiudad)
            insCiudad.fInsert
         Else
            LineaDeErrores = LineaDeErrores & String(Len(insCliente.getCiudad), " ")
         End If
      Else
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Status del Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==  esta en blanco. Se remplazará por " & gEnumProyecto.enumStatusClienteToString(eSC_ACTIVO) & " .:::"
      End If
      LineaDeErrores = LineaDeErrores & String(Len(insCliente.getZonaPostal), " ")
      If gTexto.DfLen(insCliente.GetZonaDeCobranza) > 0 Then
         Set mrsZona = New ADODB.Recordset
         existeZona = False
         SQL = "SELECT Nombre FROM Saw.ZonaCobranza WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual & " AND Nombre = " & gUtilSQL.fSimpleSqlValue(insCliente.GetZonaDeCobranza)
         If gDbUtil.fOpenRecordSetAllParameters(mrsZona, SQL, gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
            If mrsZona.RecordCount <= 0 Then
               ErrorAlTraducirLaLinea = True
               LineaDeErrores = LineaDeErrores & ":::. ERROR Indicó la Zona de Cobranza '" & insCliente.GetZonaDeCobranza & "' para el Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==   que no esta Almacenada en la Tabla de Zonas de Cobranza. Se reemplazará por >> " & fPrimerZonaCobranza & " << .:::"
               existeZona = False
            Else
               LineaDeErrores = LineaDeErrores & String(Len(insCliente.GetStatusStr), " ")
               existeZona = True
            End If
         End If
         Set mrsZona = Nothing
      Else
         LineaDeErrores = LineaDeErrores & ":::. ERROR La Zona de Cobranza del Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==  esta en blanco. Se remplazará por >> " & fPrimerZonaCobranza & " << .:::"
      End If
      If gTexto.DfLen(insCliente.GetSectorDeNegocio) > 0 Then
         If Not insSectorNegocio.fSearchByField("Descripcion", insCliente.GetSectorDeNegocio, False, False) Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR Indicó un Sector de Negocios para el Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==   que no esta Almacenado en la Tabla de Sector de Negocio. Se reemplazará por >> " & insSectorNegocio.fPrimerSectorDeNegocio & " << .:::"
         Else
            LineaDeErrores = LineaDeErrores & String(Len(insCliente.GetStatusStr), " ")
         End If
      Else
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Sector de Negocios del Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==  esta en blanco. Se remplazará por >> " & insSectorNegocio.fPrimerSectorDeNegocio & " << .:::"
      End If
      If gTexto.DfLen(insCliente.GetCodigoVendedor) > 0 Then
         If Not insVendedor.fSearchByField("Codigo", insCliente.GetCodigoVendedor, False, False) Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR Indicó un Código de Vendedor para el Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==   que no esta Almacenado en el Sistema. Se reemplazará por >> " & gProyParametrosCompania.getCodigoGenericoVendedor & " <<, que corresponde al código del vendedor genérico asignado en los parámetros de esta compañía .:::"
         Else
            LineaDeErrores = LineaDeErrores & String(Len(insCliente.GetStatusStr), " ")
         End If
      Else
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Código del Vendedor asignado para el Cliente  ==>" & insCliente.GetCodigo & "-" & insCliente.getNombre & "<==  esta en blanco. Se remplazará por >> " & gProyParametrosCompania.getCodigoGenericoVendedor & " << .:::"
      End If
   End If
   If gTexto.DfLen(insCliente.GetCuentaContableCxC) > 0 And Not insCtaContable.fSearchCuentaByCodigo(insCliente.GetCuentaContableCxC) Then
     insCliente.SetCuentaContableCxC ""
   End If
   If gTexto.DfLen(insCliente.GetCuentaContableIngresos) > 0 And Not insCtaContable.fSearchCuentaByCodigo(insCliente.GetCuentaContableIngresos) Then
     insCliente.SetCuentaContableCxC ""
   End If
   If gTexto.DfLen(insCliente.GetStatusStr) = 0 Or (gEnumProyecto.strStatusClienteToNum(insCliente.GetStatusStr) <> enum_StatusCliente.eSC_ACTIVO And gEnumProyecto.strStatusClienteToNum(insCliente.GetStatusStr) <> enum_StatusCliente.eSC_DATOS_DESACTUALIZADOS And gEnumProyecto.strStatusClienteToNum(insCliente.GetStatusStr) <> enum_StatusCliente.eSC_INACTIVO And gEnumProyecto.strStatusClienteToNum(insCliente.GetStatusStr) <> enum_StatusCliente.eSC_RESTRINGIDO And gEnumProyecto.strStatusClienteToNum(insCliente.GetStatusStr) <> enum_StatusCliente.eSC_SUSPENDIDO And gEnumProyecto.strStatusClienteToNum(insCliente.GetStatusStr) <> enum_StatusCliente.eSC_TIEMPO_SIN_CONTACTO) Then
      insCliente.SetStatusStr gEnumProyecto.enumStatusClienteToString(eSC_ACTIVO)
   End If
   If gTexto.DfLen(insCliente.getCiudad) = 0 Or Not insCiudad.fSearchByField("NombreCiudad", insCliente.getCiudad, False, False) Then
      insCliente.SetCiudad mCiudadCompaniaActual
   End If
   If gTexto.DfLen(insCliente.GetZonaDeCobranza) = 0 Or Not existeZona Then
      insCliente.SetZonaDeCobranza fPrimerZonaCobranza
   End If
   If gTexto.DfLen(insCliente.GetSectorDeNegocio) = 0 Or Not insSectorNegocio.fSearchByField("Descripcion", insCliente.GetSectorDeNegocio, False, False) Then
      insCliente.SetSectorDeNegocio insSectorNegocio.fPrimerSectorDeNegocio
   End If
   If gTexto.DfLen(insCliente.GetCodigoVendedor) = 0 Or Not insVendedor.fSearchByField("Codigo", insCliente.GetCodigoVendedor, False, False) Then
      insCliente.SetCodigoVendedor gProyParametrosCompania.getCodigoGenericoVendedor
   End If
   If (insCliente.GetDiaCumpleanos < 1) Or (insCliente.GetDiaCumpleanos > 31) Then
      insCliente.SetDiaCumpleanos 0
   End If
   If (insCliente.GetMesCumpleanos < 1) Or (insCliente.GetMesCumpleanos > 12) Then
      insCliente.SetMesCumpleanos 0
   End If
   If Not gTexto.fDireccionDeMailEsValida(insCliente.getEmail, False) Then
      insCliente.SetEmail ""
   End If
h_EXIT: On Error GoTo 0
   fDistribuyeLaLineaDeCliente = Not ErrorAlTraducirLaLinea
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fDistribuyeLaLineaDeCliente", CM_MESSAGE_NAME_CLIENTE, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fVerificaSiNoExisteElClienteYLoAgrega() As Boolean
   Dim Continuar As Boolean
   On Error GoTo h_ERROR
   Continuar = False
   If Not insCliente.fSearchByField("Codigo", insCliente.GetCodigo, False, False, False) And Not insCliente.fSearchByField("NumeroRif", insCliente.getNumeroRIF, False, False, False) Then
      If insCliente.fInsert(insertar, False, False) Then
         Continuar = True
      End If
   Else
      RegistrosYaExistentesEnBD = RegistrosYaExistentesEnBD + 1
   End If
h_EXIT: On Error GoTo 0
   fVerificaSiNoExisteElClienteYLoAgrega = Continuar
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fVerificaSiNoExisteElClienteYLoAgrega", CM_MESSAGE_NAME_CLIENTE, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fPrimerZonaCobranza() As String
   Dim rsZonaCobranza As ADODB.Recordset
   Dim SQL As String
   Dim PrimeraZona As String
   On Error GoTo h_ERROR
   PrimeraZona = ""
   SQL = " SELECT zonaCobranza.Nombre FROM zonaCobranza  WHERE zonaCobranza.ConsecutivoCompania = " & mConsecutivoCompaniaActual
   Set rsZonaCobranza = New ADODB.Recordset
   If gDbUtil.fOpenRecordset(rsZonaCobranza, SQL, Conexion) Then
      If gDbUtil.fRecordCount(rsZonaCobranza) Then
         rsZonaCobranza.MoveFirst
         PrimeraZona = rsZonaCobranza.Fields("Nombre").Value
      End If
   End If
   gDbUtil.sDestroyRecordSet rsZonaCobranza
h_EXIT: On Error GoTo 0
   fPrimerZonaCobranza = PrimeraZona
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fPrimerZonaCobranza", CM_MESSAGE_NAME_CLIENTE, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDistribuyeLaLineaVendedor(ByRef refLineaActual As String, ByVal valSeparador As String) As Boolean
   Dim linea As String
   On Error GoTo h_ERROR
   linea = refLineaActual
   fDistribuyeLaLineaVendedor = False
   vConsecutivoCompania = mConsecutivoCompaniaActual
   vNombre = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vRIF = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vStatusVendedor = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vDireccion = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vCiudad = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vZonaPostal = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vTelefono = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vFax = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vEmail = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vNotas = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vComisionPorVenta = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vComisionPorCobro = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vTopeInicialVenta1 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vTopeFinalVenta1 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vPorcentajeVentas1 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vTopeFinalVenta2 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vPorcentajeVentas2 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vTopeFinalVenta3 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vPorcentajeVentas3 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vTopeFinalVenta4 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vPorcentajeVentas4 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vTopeFinalVenta5 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vPorcentajeVentas5 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vTopeInicialCobranza1 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vTopeFinalCobranza1 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vPorcentajeCobranza1 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vTopeFinalCobranza2 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vPorcentajeCobranza2 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vTopeFinalCobranza3 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vPorcentajeCobranza3 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vTopeFinalCobranza4 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vPorcentajeCobranza4 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vTopeFinalCobranza5 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vPorcentajeCobranza5 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vUsaComisionPorVenta = gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vUsaComisionPorCobranza = gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   If gProyParametrosCompania.GetComisionesEnFacturaAsEnum = eCEF_SobreRenglones Or gProyParametrosCompania.GetComisionesEnFacturaAsEnum = eCEF_SobreTotalFacturayRenglones Then
      mNombreDeLineaDeProducto = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      mConsecutivoRenglon = gConvert.fConvertStringToLong(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      mTipoDeComision = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      vMonto = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      mPorcentaje = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   End If
   vConsecutivoRutaDeComercialzacion = gUtilImport.fToInt(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   If Not gDbUtil.fRecordCountOfSql("SELECT Consecutivo FROM Saw.RutaDeComercializacion WHERE ConsecutivoCompania = " & vConsecutivoCompania & " AND Consecutivo = " & vConsecutivoRutaDeComercialzacion) > 0 Then
      vConsecutivoRutaDeComercialzacion = 1
   End If
h_EXIT: On Error GoTo 0
   fDistribuyeLaLineaVendedor = True
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fDistribuyeLaLineaVendedor", CM_MESSAGE_NAME_VENDEDOR, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fInsertarVendedor() As Boolean
   Dim vResult As Boolean
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vSql As String
   Dim Rs As ADODB.Recordset
   On Error GoTo h_ERROR
   vResult = False
   Set insUtAdoCmd = New clsUtilAdoCmd
   Set vCmd = insUtAdoCmd.fCreateCmd("Adm.Gp_VendedorINS", gDefDatabase.Conexion)
   Set Rs = New ADODB.Recordset
   vSql = "SELECT " & gUtilSQL.fMax("Consecutivo", "Consecutivo") & " FROM Adm.Vendedor WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual
   If gDbUtil.fOpenRecordset(Rs, vSql, Conexion) Then
      If gDbUtil.fRecordCount(Rs) > 0 Then
         If (Rs("Consecutivo").Value <> "NULL") Then
            vConsecutivoVendedor = gConvert.ConvierteAInteger(gConvert.fConvierteAString(Rs("Consecutivo").Value) + 1)
         Else
            vConsecutivoVendedor = 1
         End If
      Else
         vConsecutivoVendedor = 1
      End If
      Set Rs = Nothing
   End If
   If Not insVendedor.fSearchByField("Consecutivo", vConsecutivoVendedor, False, False) Then
      If Not insVendedor.fSearchByField("Codigo", insVendedor.GetCodigo, False, False) Then
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", gProyParametrosCompania.GetConsecutivoCompania)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", vConsecutivoVendedor)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Codigo", 5, insVendedor.GetCodigo)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Nombre", 35, insVendedor.getNombre)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@RIF", 20, insVendedor.getRIF)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@StatusVendedor", 1, gConvert.enumerativoAChar(gEnumProyecto.strStatusVendedorToNum(insVendedor.GetStatusVendedorStr, False)))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Direccion", 255, insVendedor.getDireccion)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Ciudad", 100, insVendedor.getCiudad)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@ZonaPostal", 7, insVendedor.getZonaPostal)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Telefono", 40, insVendedor.getTelefono)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Fax", 20, insVendedor.getFax)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Email", 40, insVendedor.getEmail)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Notas", 255, insVendedor.getNotas)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoRutaDeComercializacion", insVendedor.getConsecutivoRutaDeComercializacion)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@ComisionPorVenta", insVendedor.getComisionPorVenta)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@ComisionPorCobro", insVendedor.getComisionPorCobro)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@TopeInicialVenta1", insVendedor.getTopeInicialVenta1)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@TopeFinalVenta1", insVendedor.getTopeFinalVenta1)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeVentas1", insVendedor.getPorcentajeVentas1)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@TopeFinalVenta2", insVendedor.GetTopeFinalVenta2)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeVentas2", insVendedor.getPorcentajeVentas2)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@TopeFinalVenta3", insVendedor.GetTopeFinalVenta3)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeVentas3", insVendedor.GetPorcentajeVentas3)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@TopeFinalVenta4", insVendedor.GetTopeFinalVenta4)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeVentas4", insVendedor.GetPorcentajeVentas4)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@TopeFinalVenta5", insVendedor.GetTopeFinalVenta5)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeVentas5", insVendedor.GetPorcentajeVentas5)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@TopeInicialCobranza1", insVendedor.GetTopeInicialCobranza1)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@TopeFinalCobranza1", insVendedor.GetTopeFinalCobranza1)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeCobranza1", insVendedor.GetPorcentajeCobranza1)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@TopeFinalCobranza2", insVendedor.GetTopeFinalCobranza2)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeCobranza2", insVendedor.GetPorcentajeCobranza2)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@TopeFinalCobranza3", insVendedor.GetTopeFinalCobranza3)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeCobranza3", insVendedor.GetPorcentajeCobranza3)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@TopeFinalCobranza4", insVendedor.GetTopeFinalCobranza4)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeCobranza4", insVendedor.GetPorcentajeCobranza4)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@TopeFinalCobranza5", insVendedor.GetTopeFinalCobranza5)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeCobranza5", insVendedor.GetPorcentajeCobranza5)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterBooleanSN("@UsaComisionPorVenta", insVendedor.GetUsaComisionPorVenta)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterBooleanSN("@UsaComisionPorCobranza", insVendedor.GetUsaComisionPorCobranza)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoLote", 10, insVendedor.GetCodigoLote)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 10, insVendedor.GetNombreOperador)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", gUtilDate.getFechaDeHoy)
         insUtAdoCmd.fExecCmd vCmd
         vResult = True
      End If
   End If
   Set vCmd = Nothing
h_EXIT: On Error GoTo 0
   fInsertarVendedor = vResult
   Exit Function
   fInsertarVendedor = False
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fInsertarVendedor", CM_MESSAGE_NAME_VENDEDOR, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sSeteaValoresDeLaLineaDelArchivo_VENDEDOR()
   On Error GoTo h_ERROR
   With insVendedor
      .SetConsecutivoCompania vConsecutivoCompania
      .SetCodigo mValorDelCampoDeId
      .SetNombre vNombre
      .SetRIF vRIF
      .SetStatusVendedorStr gEnumProyecto.enumStatusVendedorToString(gEnumProyecto.fSiglasStatusVendedorToEnum(vStatusVendedor))
      .SetDireccion vDireccion
      .SetCiudad vCiudad
      .SetZonaPostal vZonaPostal
      .SetTelefono vTelefono
      .SetFax vFax
      .SetEmail vEmail
      .SetNotas vNotas
      .SetConsecutivoRutaDeComercializacion vConsecutivoRutaDeComercialzacion
      .SetComisionPorVenta vComisionPorVenta
      .SetComisionPorCobro vComisionPorCobro
      .SetTopeInicialVenta1 vTopeInicialVenta1
      .SetTopeFinalVenta1 vTopeFinalVenta1
      .SetPorcentajeVentas1 vPorcentajeVentas1
      .SetTopeFinalVenta2 vTopeFinalVenta2
      .SetPorcentajeVentas2 vPorcentajeVentas2
      .SetTopeFinalVenta3 vTopeFinalVenta3
      .SetPorcentajeVentas3 vPorcentajeVentas3
      .SetTopeFinalVenta4 vTopeFinalVenta4
      .SetPorcentajeVentas4 vPorcentajeVentas4
      .SetTopeFinalVenta5 vTopeFinalVenta5
      .SetPorcentajeVentas5 vPorcentajeVentas5
      .SetTopeInicialCobranza1 vTopeInicialCobranza1
      .SetTopeFinalCobranza1 vTopeFinalCobranza1
      .SetPorcentajeCobranza1 vPorcentajeCobranza1
      .SetTopeFinalCobranza2 vTopeFinalCobranza2
      .SetPorcentajeCobranza2 vPorcentajeCobranza2
      .SetTopeFinalCobranza3 vTopeFinalCobranza3
      .SetPorcentajeCobranza3 vPorcentajeCobranza3
      .SetTopeFinalCobranza4 vTopeFinalCobranza4
      .SetPorcentajeCobranza4 vPorcentajeCobranza4
      .SetTopeFinalCobranza5 vTopeFinalCobranza5
      .SetPorcentajeCobranza5 vPorcentajeCobranza5
      .SetUsaComisionPorVenta vUsaComisionPorVenta
      .SetUsaComisionPorCobranza vUsaComisionPorCobranza
      .SetNombreOperador mNombreDelUsuario
      .SetFechaUltimaModificacion gUtilDate.getFechaDeHoy
      .SetCodigoLote mCodigoLote
   End With
   If gProyParametrosCompania.GetComisionesEnFacturaAsEnum = eCEF_SobreRenglones Or gProyParametrosCompania.GetComisionesEnFacturaAsEnum = eCEF_SobreTotalFacturayRenglones Then
      If mConsecutivoRenglon > 0 Then
         With insRenglonComisionesDeVendedor
            .SetConsecutivoCompania mConsecutivoCompaniaActual
            .SetCodigoVendedor mValorDelCampoDeId
            .SetConsecutivoRenglon mConsecutivoRenglon
            .SetNombreDeLineaDeProducto mNombreDeLineaDeProducto
            .SetTipoDeComisionStr gEnumProyecto.enumTipoComisionToString(gEnumProyecto.fSiglasTipoComisionToEnum(mTipoDeComision))
            .SetMonto vMonto
            .SetPorcentaje mPorcentaje
         End With
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sSeteaValoresDeLaLineaDelArchivo_VENDEDOR", CM_MESSAGE_NAME_VENDEDOR, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fImportaLineasVendedoryComision(ByVal NumeroDeLineaActual As Long) As Boolean
   Dim exito As Boolean
   On Error GoTo h_ERROR
   If fInsertarVendedor() Then
      If fInsertarComisionesDelVendedor(NumeroDeLineaActual) Then
         mNregistroAInsertar = mNregistroAInsertar + 1
         exito = True
      End If
   Else
      mConsecutivoRenglon = insRenglonComisionesDeVendedor.fGenerateNextConsecutivoRenglon(insVendedor.GetCodigo)
      insRenglonComisionesDeVendedor.SetConsecutivo mConsecutivoRenglon
      If Not fInsertarComisionesDelVendedor(NumeroDeLineaActual) Then
        gMessage.Advertencia "No se pudo importar algunos de los registros del vendedor " & insVendedor.GetCodigo
      Else
         exito = True
      End If
   End If
h_EXIT:  On Error GoTo 0
   fImportaLineasVendedoryComision = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fImportaLineasVendedoryComision", CM_MESSAGE_NAME_VENDEDOR, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fInsertarComisionesDelVendedor(ByVal NumeroDeLineaActual As Long) As Boolean
   Dim Continuar As Boolean
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vSql As String
   Dim Rs As ADODB.Recordset
   On Error GoTo h_ERROR
   Continuar = False
   Set Rs = New ADODB.Recordset
   If mConsecutivoRenglon > 0 Then
      insRenglonComisionesDeVendedor.SetConsecutivoVendedor vConsecutivoVendedor
      If insVendedor.fSearchByField("Consecutivo", insRenglonComisionesDeVendedor.GetConsecutivoVendedor, False, False) Then
         If Not insRenglonComisionesDeVendedor.fSearchByField("ConsecutivoVendedor", insRenglonComisionesDeVendedor.GetConsecutivoVendedor, False) Then
            Set insUtAdoCmd = New clsUtilAdoCmd
            Set vCmd = insUtAdoCmd.fCreateCmd("Adm.Gp_VendedorDetalleComisionesINS", gDefDatabase.Conexion)
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", insRenglonComisionesDeVendedor.GetConsecutivoCompania)
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoVendedor", insRenglonComisionesDeVendedor.GetConsecutivoVendedor)
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", mConsecutivoRenglon)
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreDeLineaDeProducto", 20, insRenglonComisionesDeVendedor.GetNombreDeLineaDeProducto)
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoDeComision", 1, gConvert.enumerativoAChar(gEnumProyecto.strTipoComisionToNum(insRenglonComisionesDeVendedor.GetTipoDeComisionStr, False)))
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@Monto", insRenglonComisionesDeVendedor.GetMonto)
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@Porcentaje", insRenglonComisionesDeVendedor.GetPorcentaje)
            insUtAdoCmd.fExecCmd vCmd
            Set vCmd = Nothing
         End If
      End If
   End If
   Continuar = True
h_EXIT: On Error GoTo 0
   fInsertarComisionesDelVendedor = Continuar
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fInsertarComisionesDelVendedor", CM_MESSAGE_NAME_VENDEDOR, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sVerificaSiHayErrorEnLaLinea_VENDEDOR(ByVal NumeroDeLineaActual As Long, ByRef ErrorAlTraducirLaLinea As Boolean, ByRef DebeModificarArray As Boolean)
   On Error GoTo h_ERROR
   If gTexto.DfLen(mValorDelCampoDeId) = 0 Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & ":::. ERROR El Número de Código del Vendedor  ==>" & mValorDelCampoDeId & "<==  no puede estar en blanco .:::"
   ElseIf insVendedor.fSearchByField("Codigo", mValorDelCampoDeId, False, False) Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & ":::. ERROR El Número de Código del Vendedor  ==>" & mValorDelCampoDeId & "<==  ya se encuentra registrado en el sistema .:::"
      RegistrosYaExistentesEnBD = RegistrosYaExistentesEnBD + 1
   ElseIf insRenglonComisionesDeVendedor.fSearchByField("CodigoVendedor", mValorDelCampoDeId, False, True) Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & ":::. ERROR El Vendedor ==>" & vNombre & "<== de código ==>" & mValorDelCampoDeId & "<==  posee datos de Comisiones registradas en el sistema, para poder importar los datos de este vendedor, asegúrese de que el vendedor no se encuentre ya registrado en el sistema y de no ser este el caso, contacte a su administrador de bases de datos y notifíquele que la data está errada y deberá eliminar las comisiones asociadas a este vendedor inexistente, para poder llevar acabo la importación  .:::"
   ElseIf Not insImportar.fYaFueCargadoElItemEnElArray(mVectorDeVerificaciones, NumeroDeLineaActual, mValorDelCampoDeId, CM_INDEX_NUMERO_VENDEDOR) Then
      If CM_INDEX_NUMERO_VENDEDOR < UBound(mVectorDeVerificaciones) And CM_INDEX_NUMERO_VENDEDOR >= LBound(mVectorDeVerificaciones) Then
         mVectorDeVerificaciones(CM_INDEX_NUMERO_VENDEDOR, NumeroDeLineaActual) = mValorDelCampoDeId
      End If
      If CM_INDEX_VALOR_IMPORTA_VENDEDOR < UBound(mVectorDeVerificaciones) And CM_INDEX_VALOR_IMPORTA_VENDEDOR >= LBound(mVectorDeVerificaciones) Then
         mVectorDeVerificaciones(CM_INDEX_VALOR_IMPORTA_VENDEDOR, NumeroDeLineaActual) = True
      End If
      If gProyParametrosCompania.GetComisionesEnFacturaAsEnum = eCEF_SobreTotalFactura Then
         If CM_INDEX_VALOR_IMPORTA_COMISION_VENDEDOR < UBound(mVectorDeVerificaciones) And CM_INDEX_VALOR_IMPORTA_COMISION_VENDEDOR >= LBound(mVectorDeVerificaciones) Then
            mVectorDeVerificaciones(CM_INDEX_VALOR_IMPORTA_COMISION_VENDEDOR, NumeroDeLineaActual) = False
         End If
      Else
         If CM_INDEX_VALOR_IMPORTA_COMISION_VENDEDOR < UBound(mVectorDeVerificaciones) And CM_INDEX_VALOR_IMPORTA_COMISION_VENDEDOR >= LBound(mVectorDeVerificaciones) Then
            mVectorDeVerificaciones(CM_INDEX_VALOR_IMPORTA_COMISION_VENDEDOR, NumeroDeLineaActual) = True
         End If
      End If
   End If
   If gTexto.DfLen(vNombre) = 0 Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & ":::. ERROR El Nombre del Vendedor  ==>" & mValorDelCampoDeId & "-" & vNombre & "<==  no puede estar en blanco .:::"
   ElseIf insVendedor.fSearchByField("Nombre", vNombre, False) Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & ":::. ERROR El Nombre del Vendedor  ==>" & mValorDelCampoDeId & "-" & vNombre & "<==  ya esta registrado en el sistema .:::"
   End If
   If gTexto.DfLen(vStatusVendedor) = 0 Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & ":::. ERROR el status de vendedor código  ==>" & mValorDelCampoDeId & "<==  no puede estar en blanco .:::"
   ElseIf Not vStatusVendedor = gEnumProyecto.enumStatusVendedorToStringSiglas(eSV_ACTIVO) And Not vStatusVendedor = gEnumProyecto.enumStatusVendedorToStringSiglas(eSV_INACTIVO) _
         And Not vStatusVendedor = gEnumProyecto.enumStatusVendedorToStringSiglas(eSV_RESTRINGIDO) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Tipo de Status de Vendedor  ==>" & vStatusVendedor & "<==   que colocó no es válido.:::"
   End If
   If Not gProyParametrosCompania.GetComisionesEnFacturaAsEnum = eCEF_SobreTotalFactura Then
      If gTexto.DfLen(mTipoDeComision) = 0 Then
         If gProyParametrosCompania.GetComisionesEnFacturaAsEnum = eCEF_SobreRenglones Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR el tipo de comisión de vendedor código  ==>" & mValorDelCampoDeId & "<==  no puede estar en blanco .:::"
         Else
            insImportar.sCambiaAFalseElRegistroDelArraySiExiste mVectorDeVerificaciones, NumeroDeLineaActual, mValorDelCampoDeId, CM_INDEX_NUMERO_VENDEDOR, CM_INDEX_VALOR_IMPORTA_VENDEDOR, CM_INDEX_VALOR_IMPORTA_COMISION_VENDEDOR, True, False
         End If
      Else
         Select Case mTipoDeComision
         Case gEnumProyecto.enumTipoComisionToStringSiglas(eTC_PorMonto):
               If mPorcentaje <> 0 Then
                  ErrorAlTraducirLaLinea = True
                  LineaDeErrores = LineaDeErrores & ":::. ERROR El Tipo de Comisión del Vendedor indicado es ==>" & gEnumProyecto.enumTipoComisionToString(gEnumProyecto.fSiglasTipoComisionToEnum(mTipoDeComision)) & "<== por lo que el valor de la comisión ==>" & mPorcentaje & "<== indicado en el campo de ==> Comisión Por Porcentaje <== debe ser ==> 0 <==.:::"
               End If
         Case gEnumProyecto.enumTipoComisionToStringSiglas(eTC_PorPorcentaje):
               If vMonto <> 0 Then
                  ErrorAlTraducirLaLinea = True
                  LineaDeErrores = LineaDeErrores & ":::. ERROR El Tipo de Comisión del Vendedor indicado es ==>" & gEnumProyecto.enumTipoComisionToString(gEnumProyecto.fSiglasTipoComisionToEnum(mTipoDeComision)) & "<== por lo que el valor de la comisión ==>" & vMonto & "<== indicado en el campo de ==> Comisión Por Monto <== debe ser ==> 0 <==.:::"
               End If
         Case Else
               ErrorAlTraducirLaLinea = True
               LineaDeErrores = LineaDeErrores & ":::. ERROR El Tipo de Comisión de Vendedor  ==>" & mTipoDeComision & "<==   que colocó no es válido.:::"
         End Select
      End If
   End If
h_EXIT: On Error GoTo 0
   DebeModificarArray = ErrorAlTraducirLaLinea
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sVerificaSiHayErrorEnLaLinea_VENDEDOR", CM_MESSAGE_NAME_VENDEDOR, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sVerificaSiHayErrorEnLaLineaValoresAjustables_VENDEDOR(ByRef ErrorAlTraducirLaLinea As Boolean, Optional ByRef DebeImportar As Boolean = False, Optional ByRef DebeModificarArray As Boolean)
   On Error GoTo h_ERROR
   If gTexto.DfLen(vCiudad) > 0 Then
      If Not insCiudad.fSearchByField("NombreCiudad", vCiudad, False) Then
         If DebeImportar Then
            vCiudad = mCiudadCompaniaActual
         Else
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR Indicó una Ciudad para el Vendedor  ==>" & mValorDelCampoDeId & "-" & vNombre & "<==   que no esta Almacenada en la Tabla de Ciudades. Se reemplazará por >> " & mCiudadCompaniaActual & " << .:::"
         End If
      End If
   Else
      If DebeImportar Then
         vCiudad = mCiudadCompaniaActual
      Else
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El campo ciudad del Vendedor  ==>" & mValorDelCampoDeId & "-" & vNombre & "<==  esta en blanco. Se remplazará por >>" & mCiudadCompaniaActual & " << .:::"
      End If
   End If
   If gTexto.fLimpiaStringdeBlancosAAmbosLados(vEmail) <> "" Then
      If Not gTexto.fDireccionDeMailCorrecta(vEmail, _
         gEnumProyecto.enum_EnviadorODestinatarioToString(eEM_DESTINATARIO), vNombre) Then
         If DebeImportar Then
            vEmail = ""
         Else
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR Indicó una Direccion de Correo Electrónica ==>" & vEmail & "<== Inválida. Si elige continuar con la importación será eliminada .:::"
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sVerificaSiHayErrorEnLaLineaValoresAjustables_VENDEDOR", CM_MESSAGE_NAME_VENDEDOR, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fDistribuyeLaLineaDeFactura(ByRef refLineaActual As String, ByVal valLineaActual As Long, ByVal valSeparador As String) As Boolean
   Dim linea As String
   Dim var_AlicuotaIvaEnum As enum_TipoDeAlicuota
   Dim Rs As ADODB.Recordset
   Dim vSql As String
   Dim vTipoDePersona As String
   Dim vEsGeneradaPorPuntoDeVenta As Boolean
   Dim vCodigoMonedaDeCobro  As String
   On Error GoTo h_ERROR
   fDistribuyeLaLineaDeFactura = False
   linea = refLineaActual
   With insFactura
      vConsecutivoCompania = mConsecutivoCompaniaActual
      .SetConsecutivoCompania vConsecutivoCompania
      .SetNumero mValorDelCampoDeId
      vFecha = gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetFecha vFecha
      .SetFechaDeEntrega vFecha
      vCodigoCliente = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetCodigoCliente vCodigoCliente
      Set Rs = New ADODB.Recordset
      vSql = "SELECT * FROM Cliente WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual
      vSql = vSql & " AND Codigo = " & gUtilSQL.fSimpleSqlValue(vCodigoCliente)
      If gDbUtil.fOpenRecordset(Rs, vSql, Conexion) Then
         If Rs.RecordCount > 0 Then
           If gTexto.DfLen(Rs.Fields("TipoDeContribuyente").Value) = 0 Then
             mTipoDeContribuyenteClienteFactura = "1"
           Else
             mTipoDeContribuyenteClienteFactura = Rs.Fields("TipoDeContribuyente").Value
           End If
            mTipoDePersonaClienteFactura = gTexto.DfMid(Rs.Fields("NumeroRIF").Value, 1, 1)
         End If
      End If
      Set Rs = Nothing
      vCodigoVendedor = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetCodigoVendedor vCodigoVendedor
      vObservaciones = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetObservaciones vObservaciones
      vMontoExentoDespuesDeDescuento = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetMontoExentoDespuesDeDescuento vMontoExentoDespuesDeDescuento
      vBaseImponibleDespuesDescuento = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetBaseImponibleDespuesDescuento vBaseImponibleDespuesDescuento
      vTotalRenglones = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetTotalRenglones vTotalRenglones
      vTotalIVA = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetTotalIVA vTotalIVA
      vTotalFactura = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetTotalFactura vTotalFactura
      vPorcentajeDescuento = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetPorcentajeDescuento vPorcentajeDescuento
      vCodigoNota1 = ""
      .SetCodigoNota1 vCodigoNota1
      vCodigoNota2 = ""
      .SetCodigoNota2 vCodigoNota2
      vMoneda = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      vMoneda = fValidaLamoneda(vMoneda)
      .SetCodigoMoneda vMoneda
      vNivelPrecio = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      If vNivelPrecio = gEnumProyecto.enumNivelDePrecioToStringSiglas(eND_PRECIO_1) Or vNivelPrecio = gEnumProyecto.enumNivelDePrecioToStringSiglas(eND_PRECIO_2) Or vNivelPrecio = gEnumProyecto.enumNivelDePrecioToStringSiglas(eND_PRECIO_3) Or vNivelPrecio = gEnumProyecto.enumNivelDePrecioToStringSiglas(eND_PRECIO_4) Then
         .SetNivelDePrecioStr gEnumProyecto.enumNivelDePrecioToString(gEnumProyecto.fSiglasNivelDePrecioToEnum(vNivelPrecio, False))
      End If
      vCodigoAlmacen = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetCodigoAlmacen vCodigoAlmacen
      Set Rs = New ADODB.Recordset
      vSql = "SELECT Consecutivo FROM Saw.Almacen WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual & " AND Codigo = " & gUtilSQL.fSimpleSqlValue(vCodigoAlmacen)
      If gDbUtil.fOpenRecordset(Rs, vSql, Conexion) Then
         If Rs.RecordCount > 0 Then
            .SetConsecutivoAlmacen Rs.Fields("Consecutivo").Value
         Else
            .SetConsecutivoAlmacen 1
         End If
      End If
      Set Rs = Nothing
      vStatusFactura = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      If vStatusFactura = gEnumProyecto.enumStatusFacturaToStringSiglas(eSF_EMITIDA) Or vStatusFactura = gEnumProyecto.enumStatusFacturaToStringSiglas(eSF_ANULADA) Or vStatusFactura = gEnumProyecto.enumStatusFacturaToStringSiglas(eSF_BORRADOR) Then
         .SetStatusFacturaStr gEnumProyecto.enumStatusFacturaToString(gEnumProyecto.fSiglasStatusFacturaToEnum(vStatusFactura, False))
      End If
      vTipoDeDocumentoStr = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      If vTipoDeDocumentoStr = gEnumProyecto.enumTipoDocumentoFacturaToSiglas(eTF_FACTURA) Or vTipoDeDocumentoStr = gEnumProyecto.enumTipoDocumentoFacturaToSiglas(eTF_COMPROBANTEFISCAL) Or vTipoDeDocumentoStr = gEnumProyecto.enumTipoDocumentoFacturaToSiglas(eTF_NOTADECREDITO) Or vTipoDeDocumentoStr = gEnumProyecto.enumTipoDocumentoFacturaToSiglas(eTF_NOTADEDEBITO) Then
         .SetTipoDeDocumentoStr gEnumProyecto.enumTipodocumentoFacturaToString(gEnumProyecto.fSiglasTipoDocumentoFacturaToEnum(vTipoDeDocumentoStr, False))
      End If
      vInsertadaManualmente = False
      .SetInsertadaManualmente vInsertadaManualmente
      vCancelada = gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetCancelada vCancelada
      vUsarDireccionFiscal = gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetUsarDireccionFiscal vUsarDireccionFiscal
      vNoDirDespachoAimprimir = gUtilImport.fToLong(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetNoDirDespachoAimprimir vNoDirDespachoAimprimir
      vCambioABolivares = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetCambioABolivares vCambioABolivares
      vMontoDelAbono = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetMontoDelAbono vMontoDelAbono
      vFechaDeVencimiento = gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetFechaDeVencimiento vFechaDeVencimiento
      vCondicionesDePago = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetCondicionesDePago vCondicionesDePago
      vNombreOperador = mNombreDelUsuario
      .SetNombreOperador vNombreOperador
      vFechaUltimaModificacion = gUtilDate.getFechaDeHoy
      .SetFechaUltimaModificacion vFechaUltimaModificacion
      vFacturaHistorica = mSeImportaComoHistorica
      .SetFacturaHistorica vFacturaHistorica
      vTalonario = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      If gTexto.DfLen(vTalonario) = 0 Then
         vTalonario = gEnumProyecto.enumTalonarioToStringSiglas(eTa_TALONARIO1)
      End If
      If vTalonario = gEnumProyecto.enumTalonarioToStringSiglas(eTa_TALONARIO1) Or vTalonario = gEnumProyecto.enumTalonarioToStringSiglas(eTa_TALONARIO2) Then
        .SetTalonarioStr gEnumProyecto.enumTalonarioToString(gEnumProyecto.fSiglasTalonarioToEnum(vTalonario))
      End If
      vFormaDeLaInicialStr = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      If vFormaDeLaInicialStr = gEnumProyecto.enumFormaDeLaInicialToStringSiglas(eFD_POR_MONTO) Or vFormaDeLaInicialStr = gEnumProyecto.enumFormaDeLaInicialToStringSiglas(eFD_POR_PORCENTAJE) Then
        .SetFormaDeLaInicialStr gEnumProyecto.enumFormaDeLaInicialToString(gEnumProyecto.fSiglasFormaDeLaInicialToEnum(vFormaDeLaInicialStr))
      End If
      vPorcentajeDeLaInicial = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetPorcentajeDeLaInicial vPorcentajeDeLaInicial
      vNumeroDeCuotas = gUtilImport.fToLong(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetNumeroDeCuotas vNumeroDeCuotas
      vMontoDeLasCuotas = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetMontoDeLasCuotas vMontoDeLasCuotas
      vMontoUltimaCuota = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetMontoUltimaCuota vMontoUltimaCuota
      vFormaDePagoStr = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      If vFormaDePagoStr = gEnumProyecto.enumFormadePagoToStringSiglas(eFP_CONTADO) Or vFormaDePagoStr = gEnumProyecto.enumFormadePagoToStringSiglas(eFP_CREDITO) Then
        .SetFormaDePagoStr gEnumProyecto.enumFormadePagoToString(gEnumProyecto.fSiglasFormadePagoToEnum(vFormaDePagoStr, False))
      End If
      vNumDiasDeVencimiento1aCuota = gUtilImport.fToLong(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetNumDiasDeVencimiento1aCuota vNumDiasDeVencimiento1aCuota
      vEditarMontoCuota = gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetEditarMontoCuota vEditarMontoCuota
      vNumeroControl = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      If gProyParametrosCompania.GetUsaImprentaDigital Then
         vNumeroControl = ""
      End If
      .SetNumeroControl vNumeroControl
      vTipoDeTransaccionStr = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      If vTipoDeTransaccionStr = gEnumProyecto.enumTipoDeTransaccionDeLibrosFiscalesToSiglas(eTD_A01_REGISTRO) Or vTipoDeTransaccionStr = gEnumProyecto.enumTipoDeTransaccionDeLibrosFiscalesToSiglas(eTD_A02_COMPLEMENTO) Or vTipoDeTransaccionStr = gEnumProyecto.enumTipoDeTransaccionDeLibrosFiscalesToSiglas(eTD_A03_ANULACION) Or vTipoDeTransaccionStr = gEnumProyecto.enumTipoDeTransaccionDeLibrosFiscalesToSiglas(eTD_A04_AJUSTE) Then
         .SetTipoDeTransaccionStr gEnumProyecto.enumTipoDeTransaccionDeLibrosFiscalesToString(gEnumProyecto.fSiglasTipoDeTransaccionDeLibrosFiscalesToEnum(vTipoDeTransaccionStr, False))
      End If
      vNumeroPlanillaExportacion = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetNumeroPlanillaExportacion vNumeroPlanillaExportacion
      vNumeroFacturaAfectada = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetNumeroFacturaAfectada vNumeroFacturaAfectada
      vTipoDeVentaStr = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      If vTipoDeVentaStr = gEnumProyecto.enumTipoDeVentaToStringSiglas(eTD_EXPORTACION) Or vTipoDeVentaStr = gEnumProyecto.enumTipoDeVentaToStringSiglas(eTD_A_CONTRIBUYENTE) Or vTipoDeVentaStr = gEnumProyecto.enumTipoDeVentaToStringSiglas(eTD_A_NO_CONTRIBUYENTE) Or vTipoDeVentaStr = gEnumProyecto.enumTipoDeVentaToStringSiglas(eTD_INTERNA) Or vTipoDeVentaStr = gEnumProyecto.enumTipoDeVentaToStringSiglas(eTD_SIN_DERECHO_A_CREDITO_FISCAL) Then
         .SetTipoDeVentaStr gEnumProyecto.enumTipoDeVentaToString(gEnumProyecto.fSiglasTipoDeVentaToEnum(vTipoDeVentaStr, False))
      End If
      vUsaMaquinaFiscal = False
      .SetUsaMaquinaFiscal vUsaMaquinaFiscal
      vCodigoMaquinaRegistradora = ""
      .SetCodigoMaquinaRegistradora vCodigoMaquinaRegistradora
      vNumeroDesde = ""
      .SetNumeroDesde vNumeroDesde
      vNumeroHasta = ""
      .SetNumeroHasta vNumeroHasta
      vNumeroControlHasta = ""
      .SetNumeroControlHasta vNumeroControlHasta
      vMontoIvaRetenido = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetMontoIvaRetenido vMontoIvaRetenido
      vFechaAplicacionRetIVA = gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetFechaAplicacionRetIVA vFechaAplicacionRetIVA
      vNumeroComprobanteRetIVA = gUtilImport.fToLong(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetNumeroComprobanteRetIVA vNumeroComprobanteRetIVA
      vFechaComprobanteRetIVA = gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetFechaComprobanteRetIVA vFechaComprobanteRetIVA
      vSeRetuvoIva = gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetSeRetuvoIVA vSeRetuvoIva
      vFacturaConPreciosSinIva = gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetFacturaConPreciosSinIva vFacturaConPreciosSinIva
      vConsecutivoCaja = gConvert.enumerativoAChar(insCaja.CM_CONSECUTIVO_INICIAL_DE_CAJA)
      .SetConsecutivoCaja vConsecutivoCaja
      vGeneraCobroDirecto = False
      .SetGeneraCobroDirecto vGeneraCobroDirecto
      .SetCodigoLote mCodigoLote
      .SetReservarMercancia gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetFechaDeRetiro gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetCodigoAlmacen gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetNoDirDespachoAimprimir gUtilImport.fToLong(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetVueltoDelCobroDirecto gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetPorcentajeDescuento1 gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetPorcentajeDescuento2 gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetMontoDescuento1 gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetMontoDescuento2 gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      mPorcentajeAlicuota1 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      mPorcentajeAlicuota2 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      mPorcentajeAlicuota3 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetPorcentajeAlicuota1 mPorcentajeAlicuota1
      .SetPorcentajeAlicuota2 mPorcentajeAlicuota2
      .SetPorcentajeAlicuota3 mPorcentajeAlicuota3
      mMontoIVAAlicuota1 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      mMontoIVAAlicuota2 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      mMontoIVAAlicuota3 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetMontoIvaAlicuota1 mMontoIVAAlicuota1
      .SetMontoIVAAlicuota2 mMontoIVAAlicuota2
      .SetMontoIVAAlicuota3 mMontoIVAAlicuota3
      mMontoGravableAlicuota1 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      mMontoGravableAlicuota2 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      mMontoGravableAlicuota3 = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetMontoGravableAlicuota1 mMontoGravableAlicuota1
      .SetMontoGravableAlicuota2 mMontoGravableAlicuota2
      .SetMontoGravableAlicuota3 mMontoGravableAlicuota3
      .SetMoneda fDevuelveElNombreDeLaMonedaIfExist(vMoneda)
      vFechaDeFacturaAfectada = gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetFechaDeFacturaAfectada vFechaDeFacturaAfectada
      mAplicaDecretoIvaEspecial = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetAplicaDecretoIvaEspecial gConvert.ConvertStringToBoolean(mAplicaDecretoIvaEspecial)
      mBaseImponibleIGTF = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetBaseImponibleIGTF mBaseImponibleIGTF
      mIGTFML = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetIGTFML mIGTFML
      mIGTFME = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetIGTFME mIGTFME
      mAlicuotaIGTF = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetAlicuotaIGTF mAlicuotaIGTF
      vEsGeneradaPorPuntoDeVenta = False
      .SetEsGeneradaPorPuntoDeVenta vEsGeneradaPorPuntoDeVenta
   End With
   With insRenglonFactura
      .SetConsecutivoCompania vConsecutivoCompania
      .SetNumeroFactura mValorDelCampoDeId
      If vConsecutivoRenglon = 0 Then
         vConsecutivoRenglon = vConsecutivoRenglon + 1
      End If
      .SetConsecutivoRenglon vConsecutivoRenglon
      vDescripcion = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetDescripcion vDescripcion
      var_AlicuotaIvaEnum = gEnumProyecto.fSiglasTipoDeAlicuotaToEnum(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador), False)
      vAlicuotaIVAStr = (gEnumProyecto.enumTipoDeAlicuotaToString(var_AlicuotaIvaEnum))
      .SetAlicuotaIVAStr vAlicuotaIVAStr
      mPorcentajeAlicuotaRenglonFactura = gConvert.fConvierteACurrency(gAdmAlicuotaIvaActual.GetAlicuotaIVA(vFecha, var_AlicuotaIvaEnum))
      .SetPorcentajeAlicuota mPorcentajeAlicuotaRenglonFactura
      vCantidad = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetCantidad vCantidad
      vPrecioSinIVA = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetPrecioSinIVA vPrecioSinIVA
      vPrecioConIVA = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetPrecioConIVA vPrecioConIVA
      vDescuentoRenglon = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetPorcentajeDescuento vDescuentoRenglon
      vTotalRenglon = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetTotalRenglon vTotalRenglon
      vPorcentajeBaseImponible = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetPorcentajeBaseImponible vPorcentajeBaseImponible
      If vTipoDeDocumentoStr = gEnumProyecto.enumTipoDocumentoFacturaToSiglas(eTF_FACTURA) Or vTipoDeDocumentoStr = gEnumProyecto.enumTipoDocumentoFacturaToSiglas(eTF_COMPROBANTEFISCAL) Or vTipoDeDocumentoStr = gEnumProyecto.enumTipoDocumentoFacturaToSiglas(eTF_NOTADECREDITO) Or vTipoDeDocumentoStr = gEnumProyecto.enumTipoDocumentoFacturaToSiglas(eTF_NOTADEDEBITO) Then
        .SetTipoDeDocumentoStr gEnumProyecto.enumTipodocumentoFacturaToString(gEnumProyecto.fSiglasTipoDocumentoFacturaToEnum(vTipoDeDocumentoStr, False))
      End If
      vVendedor1Renglon = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetCodigoVendedor1 vVendedor1Renglon
      vVendedor2Renglon = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetCodigoVendedor2 vVendedor2Renglon
      vVendedor3Renglon = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetCodigoVendedor3 vVendedor3Renglon
      vCampoExtraEnRenglonFactura1 = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetCampoExtraEnRenglonFactura1 vCampoExtraEnRenglonFactura1
      vCampoExtraEnRenglonFactura2 = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetCampoExtraEnRenglonFactura2 vCampoExtraEnRenglonFactura2
      vArticulo = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetArticulo vArticulo
   End With
   With insFactura
      vCodigoMonedaDeCobro = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      vCodigoMonedaDeCobro = fValidaMonedaDeCobro(vCodigoMonedaDeCobro)
      .SetCodigoMonedaDeCobro vCodigoMonedaDeCobro
      .SetFechaLimiteCambioAMonedaLocal vFecha
   End With
   
h_EXIT: On Error GoTo 0
    fDistribuyeLaLineaDeFactura = True
    Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fDistribuyeLaLineaDeFactura", CM_MESSAGE_NAME_FACTURA, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fInsertarLaFactura() As Boolean
   Dim Continuar As Boolean
   On Error GoTo h_ERROR
   Continuar = False
   If Not gTexto.DfLen(insFactura.GetNumero) = 0 Then
      If Not insFactura.fSearchByField("Numero", insFactura.GetNumero, False, False) Then
         If insFactura.fInsert(insertar, True, False) Then
            If fInsertarElDetalleDeLaFactura Then
               Continuar = True
            End If
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   fInsertarLaFactura = Continuar
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fInsertarLaFactura", CM_MESSAGE_NAME_FACTURA, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sVerificaSiHayErrorEnLaLineaValoresAjustables_FACTURA(ByRef ErrorAlTraducirLaLinea As Boolean, Optional ByVal DebeImportar As Boolean = False, Optional ByRef DebeModificarArray As Boolean = False, Optional ByVal valEsCotizacion As Boolean)
   Dim refCodigo As String
   Dim refNombreAlmacen As String
   On Error GoTo h_ERROR
      If gTexto.DfLen(vCodigoVendedor) > 0 Then
         If Not insVendedor.fSearchByField("Codigo", vCodigoVendedor, False, False) Then
            If DebeImportar Then
               ErrorAlTraducirLaLinea = False
            Else
               ErrorAlTraducirLaLinea = True
               LineaDeErrores = LineaDeErrores & ":::. ERROR Indicó el Código de Vendedor  ==>" & vCodigoVendedor & "<==   que no esta Almacenado en el Sistema. Se reemplazará por >> " & gProyParametrosCompania.getCodigoGenericoVendedor & " <<, que corresponde al código del vendedor genérico asignado en los parámetros de esta compañía .:::"
            End If
            vCodigoVendedor = gProyParametrosCompania.getCodigoGenericoVendedor
        End If
      Else
         If DebeImportar Then
            ErrorAlTraducirLaLinea = False
         Else
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR El Código del Vendedor  ==>" & vCodigoVendedor & "<==  esta en blanco. Se remplazará por >> " & gProyParametrosCompania.getCodigoGenericoVendedor & " << .:::"
         End If
          vCodigoVendedor = gProyParametrosCompania.getCodigoGenericoVendedor
      End If
      If (Not valEsCotizacion) Then
         If Not fSelectAndSetValuesOfAlmacenFromAOS(refCodigo, refNombreAlmacen, vCodigoAlmacen) Then
            If DebeImportar Then
               Dim insUtAdoCmd As clsUtilAdoCmd
               Dim vCmd As ADODB.Command
               Dim vSql As String
               Dim Rs As ADODB.Recordset
               Dim vConsecutivoCliente As Long
               Set insUtAdoCmd = New clsUtilAdoCmd
               Set vCmd = insUtAdoCmd.fCreateCmd("Saw.Gp_AlmacenINS", gDefDatabase.Conexion)
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", mConsecutivoCompaniaActual)
               
               Set Rs = New ADODB.Recordset
               vSql = "SELECT " & gUtilSQL.fMax("Consecutivo", "Consecutivo") & " FROM Saw.Almacen WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual
               If gDbUtil.fOpenRecordset(Rs, vSql, Conexion) Then
                  If Rs.RecordCount > 0 Then
                     vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", Rs("Consecutivo").Value)
                  Else
                     vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", 1)
                  End If
               End If
               Set Rs = Nothing
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Codigo", 5, vCodigoAlmacen)
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreAlmacen", 40, vCodigoAlmacen)
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoDeAlmacen", 1, gConvert.enumerativoAChar(enum_TipoDeAlmacen.eTD_PRINCIPAL))
               If insCliente.fSearchByField("Codigo", vCodigoCliente, False) Then
                  Set Rs = New ADODB.Recordset
                  vSql = "SELECT Consecutivo FROM Cliente WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual & " AND Codigo =" & gUtilSQL.fSimpleSqlValue(vCodigoCliente)
                  If gDbUtil.fOpenRecordset(Rs, vSql, Conexion) Then
                     If Rs.RecordCount > 0 Then
                        vConsecutivoCliente = Rs("Consecutivo").Value
                     End If
                  End If
                  Set Rs = Nothing
                  vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("ConsecutivoCliente", vConsecutivoCliente)
               Else
                  Set Rs = New ADODB.Recordset
                  vSql = "SELECT Consecutivo FROM Cliente WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual & " AND Codigo =" & gUtilSQL.fSimpleSqlValue(gProyParametrosCompania.getCodigoGenericoCliente)
                  If gDbUtil.fOpenRecordset(Rs, vSql, Conexion) Then
                     If Rs.RecordCount > 0 Then
                        vConsecutivoCliente = Rs("Consecutivo").Value
                     End If
                  End If
                  Set Rs = Nothing
                  vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("ConsecutivoCliente", vConsecutivoCliente)
               End If
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoCc", 5, "")
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Descripcion", 40, "")
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 10, mNombreDelUsuario)
               vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", gUtilDate.getFechaDeHoy)
               insUtAdoCmd.fExecCmd vCmd
               Set vCmd = Nothing
               ErrorAlTraducirLaLinea = False
            Else
               ErrorAlTraducirLaLinea = True
               LineaDeErrores = LineaDeErrores & ":::. Indicó un Código de Almacen  ==>" & vCodigoAlmacen & "<==   que no esta Almacenado en el Sistema. Se agregará con nombre igual al código si desea continuar con la importación.:::"
            End If
         End If
      End If
      If gProyParametrosCompania.GetComisionesEnFacturaAsEnum = eCEF_SobreRenglones Or gProyParametrosCompania.GetComisionesEnFacturaAsEnum = eCEF_SobreTotalFacturayRenglones Then
           If gTexto.DfLen(vVendedor1Renglon) > 0 Then
              If Not insVendedor.fSearchByField("Codigo", vCodigoVendedor, False, False) Then
                 If DebeImportar Then
                    vVendedor1Renglon = gProyParametrosCompania.getCodigoGenericoVendedor
                    ErrorAlTraducirLaLinea = False
                 Else
                    ErrorAlTraducirLaLinea = True
                    LineaDeErrores = LineaDeErrores & ":::. ERROR Indicó el Código de Vendedor  ==>" & vVendedor1Renglon & "<==   que no está Almacenado en el Sistema. Se reemplazará por >> " & gProyParametrosCompania.getCodigoGenericoVendedor & " <<, que corresponde al código del vendedor genérico asignado en los parámetros de esta compañía .:::"
                 End If
              End If
           Else
              If DebeImportar Then
                 vVendedor1Renglon = vCodigoVendedor
                 ErrorAlTraducirLaLinea = False
              Else
                 ErrorAlTraducirLaLinea = True
                 LineaDeErrores = LineaDeErrores & ":::. ERROR El Código del Vendedor utilizado para la venta del artículo ==>" & vArticulo & "<==  está en blanco. Se remplazará por >> " & vCodigoVendedor & " << correspondiente al vendedor genérico de la factura.:::"
              End If
           End If
       End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sVerificaSiHayErrorEnLaLineaValoresAjustables_FACTURA", CM_MESSAGE_NAME_FACTURA, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fImportaLineasFacturayDetalle() As Boolean
   Dim exito As Boolean
   On Error GoTo h_ERROR
   If fInsertarLaFactura Then
      mNregistroAInsertar = mNregistroAInsertar + 1
      exito = True
   Else
      vConsecutivoRenglon = insRenglonFactura.fGenerateNextConsecutivoRenglon(insFactura.GetNumero)
      insRenglonFactura.SetConsecutivoRenglon vConsecutivoRenglon
      If Not fInsertarElDetalleDeLaFactura Then
        gMessage.Advertencia "No se pudo importar algunos ó todos los registros de la factura " & insFactura.GetNumero
      Else
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fImportaLineasFacturayDetalle = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fImportaLineasFacturayDetalle", CM_MESSAGE_NAME_FACTURA, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fInsertarElDetalleDeLaFactura() As Boolean
   Dim Continuar As Boolean
   On Error GoTo h_ERROR
   Continuar = False
   If Not gTexto.DfLen(insRenglonFactura.GetNumeroFactura) = 0 Then
      If insRenglonFactura.fInsert Then
         Continuar = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fInsertarElDetalleDeLaFactura = Continuar
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fInsertarElDetalleDeLaFactura", CM_MESSAGE_NAME_FACTURA, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sVerificaSiHayErrorEnLaLinea_FACTURA(ByVal valLineaActual As Long, ByRef ErrorAlTraducirLaLinea As Boolean, ByRef DebeModificarArray As Boolean, ByVal valEsFacturaBorradores As Boolean, ByVal valEsCotizacion)
   Dim vCampoAVerificar As String
   On Error GoTo h_ERROR
   vCampoAVerificar = "Factura"
   If valEsCotizacion Then
      insFactura.setClaseDeTrabajo eCTFC_Cotizacion
      insRenglonFactura.setClaseDeTrabajo eCTFC_Cotizacion
      vCampoAVerificar = "Cotizacion"
   End If
   If gTexto.DfLen(mValorDelCampoDeId) = 0 Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & ":::. ERROR El Número de " & vCampoAVerificar & " ==>" & mValorDelCampoDeId & "<==  no puede estar en blanco .:::"
      GoTo h_EXIT
   ElseIf insFactura.fSearchByField("Numero", mValorDelCampoDeId, False, False) Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & ":::. ERROR El Número de " & vCampoAVerificar & " ==>" & mValorDelCampoDeId & "<==  ya esta registrado en el sistema .:::"
      RegistrosYaExistentesEnBD = RegistrosYaExistentesEnBD + 1
      GoTo h_EXIT
   ElseIf insRenglonFactura.fSearchByField("Numero" & vCampoAVerificar, mValorDelCampoDeId) Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & ":::. ERROR El Número de " & vCampoAVerificar & " ==>" & mValorDelCampoDeId & "<==  posee datos de Detalle de la Factura registrados en el sistema .:::"
      GoTo h_EXIT
   ElseIf gUtilDate.fF1IsGreaterThanF2(gProyParametrosCompania.GetFechaMinimaIngresarDatos, vFecha) Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & ":::. ERROR La fecha de " & vCampoAVerificar & " ==>" & gConvert.dateToString(vFecha) & " <==  es menor a la fecha mínima permitida por el sistema para ingresar documentos en esta compañía .:::"
      GoTo h_EXIT
   ElseIf Not insImportar.fYaFueCargadoElItemEnElArray(mVectorDeVerificaciones, valLineaActual, mValorDelCampoDeId, CM_INDEX_NUMERO_FACTURA) Then
      If CM_INDEX_NUMERO_FACTURA < UBound(mVectorDeVerificaciones) And CM_INDEX_NUMERO_FACTURA >= LBound(mVectorDeVerificaciones) Then
         mVectorDeVerificaciones(CM_INDEX_NUMERO_FACTURA, valLineaActual) = mValorDelCampoDeId
      End If
      If CM_INDEX_VALOR_IMPORTA_FACTURA_DETALLE < UBound(mVectorDeVerificaciones) And CM_INDEX_VALOR_IMPORTA_FACTURA_DETALLE >= LBound(mVectorDeVerificaciones) Then
         mVectorDeVerificaciones(CM_INDEX_VALOR_IMPORTA_FACTURA_DETALLE, valLineaActual) = True
      End If
   End If
   If gTexto.DfLen(vCodigoCliente) = 0 Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & ":::. ERROR El Código del Cliente  ==>" & vCodigoCliente & "<==  no puede estar en blanco .:::"
      GoTo h_EXIT
   ElseIf Not insCliente.fSearchByField("Codigo", vCodigoCliente, False, False) Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & ":::. ERROR Indicó un Código de Cliente  ==>" & vCodigoCliente & "<==   que no esta Almacenado en el Sistema.:::"
      GoTo h_EXIT
   End If
   If Not vNivelPrecio = gEnumProyecto.enumNivelDePrecioToStringSiglas(eND_PRECIO_1) And Not vNivelPrecio = gEnumProyecto.enumNivelDePrecioToStringSiglas(eND_PRECIO_2) And Not vNivelPrecio = gEnumProyecto.enumNivelDePrecioToStringSiglas(eND_PRECIO_3) And Not vNivelPrecio = gEnumProyecto.enumNivelDePrecioToStringSiglas(eND_PRECIO_4) Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & ":::. ERROR El Nivel de precio que escogió  ==>" & vNivelPrecio & "<==   no es válido.:::"
      GoTo h_EXIT
   End If
   If (Not valEsCotizacion) Then
      Select Case vStatusFactura
         Case gEnumProyecto.enumStatusFacturaToStringSiglas(eSF_EMITIDA):
            If valEsFacturaBorradores Then
               ErrorAlTraducirLaLinea = True
               LineaDeErrores = LineaDeErrores & ":::. ERROR El Status de la Factura  ==>" & gEnumProyecto.enumStatusFacturaToString(eSF_EMITIDA) & "<==   no es válido para la importación de Borradores de Facturas. Para importar facturas emitidas deberá hacerlo a través del modulo para importar Facturas Historicas.:::"
               GoTo h_EXIT
            End If
         Case gEnumProyecto.enumStatusFacturaToStringSiglas(eSF_ANULADA):
            If valEsFacturaBorradores Then
               ErrorAlTraducirLaLinea = True
               LineaDeErrores = LineaDeErrores & ":::. ERROR El Status de la Factura  ==>" & gEnumProyecto.enumStatusFacturaToString(eSF_ANULADA) & "<==   no es válido para la importación de Borradores de Facturas. Para importar facturas anuladas deberá hacerlo a través del modulo para importar Facturas Historicas.:::"
               GoTo h_EXIT
            End If
         Case gEnumProyecto.enumStatusFacturaToStringSiglas(eSF_BORRADOR):
            If Not valEsFacturaBorradores Then
               ErrorAlTraducirLaLinea = True
               LineaDeErrores = LineaDeErrores & ":::. ERROR El Status de la Factura  ==>" & gEnumProyecto.enumStatusFacturaToString(eSF_BORRADOR) & "<==   no es válido para la importación de Facturas Historicas. Para importar borradores de facturas deberá hacerlo a través del modulo para importar Borradores de Facturas.:::"
               GoTo h_EXIT
            End If
         Case Else:
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR El Status de la Factura  ==>" & vStatusFactura & "<==   no es válido.:::"
            GoTo h_EXIT
      End Select
      If Not vTalonario = gEnumProyecto.enumTalonarioToStringSiglas(eTa_TALONARIO1) And Not vTalonario = gEnumProyecto.enumTalonarioToStringSiglas(eTa_TALONARIO2) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El talonario  ==>" & vTalonario & "<==   no es válido.:::"
         GoTo h_EXIT
      End If
      If Not vFormaDeLaInicialStr = gEnumProyecto.enumFormaDeLaInicialToStringSiglas(eFD_POR_MONTO) And Not vFormaDeLaInicialStr = gEnumProyecto.enumFormaDeLaInicialToStringSiglas(eFD_POR_PORCENTAJE) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR La forma de inicial  ==>" & vFormaDeLaInicialStr & "<==   no es válida.:::"
         GoTo h_EXIT
      End If
      If Not vFormaDePagoStr = gEnumProyecto.enumFormadePagoToStringSiglas(eFP_CONTADO) And Not vFormaDePagoStr = gEnumProyecto.enumFormadePagoToStringSiglas(eFP_CREDITO) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR La forma de pago que escogió  ==>" & vFormaDePagoStr & "<==   no es válido.:::"
         GoTo h_EXIT
      End If
      If Not vTipoDeTransaccionStr = gEnumProyecto.enumTipoDeTransaccionDeLibrosFiscalesToSiglas(eTD_A01_REGISTRO) And Not vTipoDeTransaccionStr = gEnumProyecto.enumTipoDeTransaccionDeLibrosFiscalesToSiglas(eTD_A02_COMPLEMENTO) And Not vTipoDeTransaccionStr = gEnumProyecto.enumTipoDeTransaccionDeLibrosFiscalesToSiglas(eTD_A03_ANULACION) And Not vTipoDeTransaccionStr = gEnumProyecto.enumTipoDeTransaccionDeLibrosFiscalesToSiglas(eTD_A04_AJUSTE) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Tipo de transacción  ==>" & vTipoDeTransaccionStr & "<==   no es válido.:::"
         GoTo h_EXIT
      End If
      If vTipoDeVentaStr = gEnumProyecto.enumTipoDeVentaToStringSiglas(eTD_EXPORTACION) Then
         If gTexto.DfLen(vNumeroPlanillaExportacion) = 0 Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR El numero de planilla de exportación  ==>" & vNumeroPlanillaExportacion & "<==  no puede estar en blanco .:::"
            GoTo h_EXIT
         ElseIf insFactura.fSearchByField("NumeroPlanillaExportacion", vNumeroPlanillaExportacion, False, False) Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR Indicó un numero de planilla de exportación  ==>" & vNumeroPlanillaExportacion & "<== que ya existe.:::"
            GoTo h_EXIT
         End If
      Else
         If Not vTipoDeVentaStr = gEnumProyecto.enumTipoDeVentaToStringSiglas(eTD_A_CONTRIBUYENTE) And Not vTipoDeVentaStr = gEnumProyecto.enumTipoDeVentaToStringSiglas(eTD_A_NO_CONTRIBUYENTE) And Not vTipoDeVentaStr = gEnumProyecto.enumTipoDeVentaToStringSiglas(eTD_INTERNA) And Not vTipoDeVentaStr = gEnumProyecto.enumTipoDeVentaToStringSiglas(eTD_SIN_DERECHO_A_CREDITO_FISCAL) Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR El Tipo de venta  ==>" & vTipoDeVentaStr & "<==   no es válido.:::"
            GoTo h_EXIT
         End If
      End If
      If gProyParametrosCompania.GetAplicacionAlicuotaEspecialAsEnum = gEnumProyecto.strAplicacionAlicuotaToNum(gEnumProyecto.enumAplicacionAlicuotaToString(eAA_NoAplica)) Then
         If mPorcentajeAlicuota1 = 10 Or mPorcentajeAlicuota1 = 9 Or mPorcentajeAlicuota1 = 7 Then
             ErrorAlTraducirLaLinea = True
             LineaDeErrores = LineaDeErrores & ":::. ERROR La empresa No Aplica el decreto especial del IVA. La Factura retiene porcentaje al 10 %, lo cuál generará inconsistencia en la data. La Factura no será Importada.:::"
             GoTo h_EXIT
         End If
      End If
      If mAplicaDecretoIvaEspecial = gConvert.ConvertBooleanToString(True) Then '**** Decreto Especial Iva
        'reducida
         If gProyParametrosCompania.GetAplicacionAlicuotaEspecialAsEnum = gEnumProyecto.strAplicacionAlicuotaToNum(gEnumProyecto.enumAplicacionAlicuotaToString(eAA_SustituirAlicuotaReducida)) Then
             If gEnumProyecto.strTipoDeAlicuotaToNum(vAlicuotaIVAStr) = enum_TipoDeAlicuota.eTD_ALICUOTA2 Then
                ErrorAlTraducirLaLinea = True
                LineaDeErrores = LineaDeErrores & ":::. ERROR La empresa Aplica el decreto especial del IVA: ==> Sustituir Alícuota Reducida <==. El Renglón de la Factura retiene Alícuota Reducida, lo cuál generará inconsistencia en la data. La Factura no será Importada.:::"
                GoTo h_EXIT
             End If
         End If
         'Adicional
         If gProyParametrosCompania.GetAplicacionAlicuotaEspecialAsEnum = gEnumProyecto.strAplicacionAlicuotaToNum(gEnumProyecto.enumAplicacionAlicuotaToString(eAA_SustituirAlicuotaAdicional)) Then
             If gEnumProyecto.strTipoDeAlicuotaToNum(vAlicuotaIVAStr) = enum_TipoDeAlicuota.eTD_ALICUOTA3 Then
                 ErrorAlTraducirLaLinea = True
                 LineaDeErrores = LineaDeErrores & ":::. ERROR La empresa Aplica el decreto especial del IVA: ==> Sustituir Alícuota Adicional <==. El Renglón de la Factura retiene Alícuota Adicional, lo cuál generará inconsistencia en la data. La Factura no será Importada.:::"
                 GoTo h_EXIT
             End If
         End If
         If gTexto.DfLen(mTipoDeContribuyenteClienteFactura) > 0 Then
            If mPorcentajeAlicuota1 = 10 Then
               If mTipoDeContribuyenteClienteFactura = gEnumProyecto.strTipoDeContribuyenteToNum(gEnumProyecto.enumTipoDeContribuyenteToString(eTD_CONTRIBUYENTE)) Or (mTipoDePersonaClienteFactura = "J" Or mTipoDePersonaClienteFactura = "G") Then
                  ErrorAlTraducirLaLinea = True
                  LineaDeErrores = LineaDeErrores & ":::. ERROR La empresa Aplica el decreto especial del IVA. La factura que está importando, tiene cliente contribuyente, lo cuál generará inconsistencia en la data. La Factura no será Importada.:::"
                  GoTo h_EXIT
               End If
            End If
         Else
             ErrorAlTraducirLaLinea = True
             LineaDeErrores = LineaDeErrores & ":::. Debe definir el Tipo De Contribuyente del o los clientes a importar.:::"
             GoTo h_EXIT
         End If
      End If
   End If
   If gProyParametrosCompania.GetUsaMonedaExtranjera Then
      If Not insMoneda.fSearchByField("Codigo", vMoneda, False, False) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR Indicó una Moneda  ==>" & vMoneda & "<==   No válida. No podrá importar este dato.:::"
      End If
   Else
      If Not mCodigoMonedaLocal = vMoneda Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR La compañía actual no utiliza moneda extranjera. Indicó una moneda  ==>" & vMoneda & "<== distinta a la moneda local.:::"
         GoTo h_EXIT
      End If
   End If
   If gTexto.DfLen(vArticulo) = 0 Then 'valores de la tabla de detalle
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & ":::. ERROR El Código del Artículo del detalle de la factura  ==>" & vArticulo & "<==  no puede estar en blanco .:::"
      GoTo h_EXIT
   ElseIf Not insArticuloInventario.fSearchByField("Codigo", vArticulo, False) Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & ":::. ERROR Indicó un Código de Articulo  ==>" & vArticulo & "<==   que no esta Almacenado en el Sistema.:::"
      GoTo h_EXIT
   End If
   If (Not valEsCotizacion) Then
      If gTexto.DfLen(vAlicuotaIVAStr) = 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR La Alicuota del IVA para el detalle de la factura  ==>" & vAlicuotaIVAStr & "<==  no puede estar en blanco .:::"
         GoTo h_EXIT
      ElseIf Not vAlicuotaIVAStr = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_EXENTO) And Not vAlicuotaIVAStr = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTAGENERAL) _
         And Not vAlicuotaIVAStr = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTA3) And Not vAlicuotaIVAStr = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTA2) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Tipo de Alicuota IVA  ==>" & vAlicuotaIVAStr & "<==   que colocó en el detalle no es válido.:::"
         GoTo h_EXIT
      End If
   End If
   If Not (gEnumProyecto.fSiglasTipoDocumentoFacturaToEnum(vTipoDeDocumentoStr, False) = (enum_TipoDocumentoFactura.eTF_FACTURA) Or gEnumProyecto.fSiglasTipoDocumentoFacturaToEnum(vTipoDeDocumentoStr, False) = (enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)) And Not gEnumProyecto.fSiglasTipoDocumentoFacturaToEnum(vTipoDeDocumentoStr, False) = (enum_TipoDocumentoFactura.eTF_NOTADECREDITO) And Not gEnumProyecto.fSiglasTipoDocumentoFacturaToEnum(vTipoDeDocumentoStr, False) = (enum_TipoDocumentoFactura.eTF_NOTADEDEBITO) Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & ":::. ERROR El tipo de documento que escogió  ==>" & gEnumProyecto.enumTipodocumentoFacturaToString(gEnumProyecto.fSiglasTipoDocumentoFacturaToEnum(vTipoDeDocumentoStr, False)) & "<==   no es válido.:::"
      GoTo h_EXIT
   End If
h_EXIT: On Error GoTo 0
   DebeModificarArray = ErrorAlTraducirLaLinea
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sVerificaSiHayErrorEnLaLinea_FACTURA", CM_MESSAGE_NAME_FACTURA, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fDistribuyeLaLineaDeRenglonCobroDeFactura(ByRef refLineaActual As String, ByVal valLineaActual As Long, ByVal valSeparador As String) As Boolean
   Dim linea As String
   Dim existe As Boolean
   On Error GoTo h_ERROR
   linea = refLineaActual
   With insRenglonCobroDeFactura
      .SetConsecutivoCompania mConsecutivoCompaniaActual
      vNumeroFactura = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetNumeroFactura vNumeroFactura
      vConsecutivoRenglon = .fGenerateNextConsecutivoRenglonCobroFactura(vNumeroFactura)
      .SetConsecutivoRenglon vConsecutivoRenglon
      vTipoDeDocumentoStr = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetTipoDeDocumentoStr gEnumProyecto.enumTipodocumentoFacturaToString(gEnumProyecto.fSiglasTipoDocumentoFacturaToEnum(vTipoDeDocumentoStr, False))
      .SetCodigoFormaDelCobro gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetNumeroDelDocumento gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetCodigoBanco gUtilImport.fToLong(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetMonto gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      If insFactura.fSearchByField("Numero", vNumeroFactura, False) Then
         existe = True
      End If
   End With
h_EXIT: On Error GoTo 0
    fDistribuyeLaLineaDeRenglonCobroDeFactura = existe
    Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fDistribuyeLaLineaDeRenglonCobroDeFactura", CM_MESSAGE_NAME_FACTURA, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fImportaLineasDetalleCobroDeFactura() As Boolean
   Dim exito As Boolean
   On Error GoTo h_ERROR
   If Not gTexto.DfLen(insRenglonCobroDeFactura.GetNumeroFactura) = 0 Then
      If Not insFactura.fSearchByField("Numero", insRenglonCobroDeFactura.GetNumeroFactura, False, False) Then
         If insRenglonCobroDeFactura.fInsert Then
            exito = True
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   fImportaLineasDetalleCobroDeFactura = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fImportaLineasDetalleCobroDeFactura", CM_MESSAGE_NAME_FACTURA, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDistribuyeLaLineaDeLaCxC(ByRef refLineaActual As String, ByVal valregistroActual As Long, ByVal valSeparador As String, ByVal valVerificarSiEstaRepetido As Boolean, ByRef refEsteRegistroSeIncluye As Boolean, ByVal valVerificarParaImportacion As Boolean) As Boolean
   Dim linea As String
   Dim ErrorAlTraducirLaLinea As Boolean
   Dim ExisteArticuloEnArchivo As Boolean
   Dim TextoAuxiliar As String
   On Error GoTo h_ERROR
   ErrorAlTraducirLaLinea = False
   LineaDeErrores = ""
   linea = refLineaActual
   insCxC.SetNombreOperador mNombreDelUsuario
   insCxC.SetNumero gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetStatusStr gEnumProyecto.strStatusDocumentoToSiglasToString(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetTipoCxCStr gEnumProyecto.strTipoDeCxCToSiglasToString(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   mNumeroYTipoDeCxC = insCxC.GetNumero + insCxC.GetTipoCxCStr
   insCxC.SetCodigoCliente gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetNombreCliente gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetCodigoVendedor gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetNombreVendedor gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetFecha gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetFechaCancelacion gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetFechaVencimiento gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetFechaAnulacion gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetMontoExento gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetMontoGravado gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetMontoIva gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetMontoAbonado gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetDescripcion gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetCodigoMoneda fValidaLamoneda(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetCambioABolivares gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetCodigoCc gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetCentroDeCostos gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetSeRetuvoIVA gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetNumeroDocumentoOrigen gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetNoAplicaParaLibroDeVentas gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetMoneda fDevuelveElNombreDeLaMonedaIfExist(insCxC.GetCodigoMoneda)
   insCxC.SetCodigoLote insLoteDeAdm.GetCodigoLote
   insCxC.SetNumeroControl gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetNumeroComprobanteFiscal gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   If valVerificarSiEstaRepetido Then
      If gTexto.DfLen(insCxC.GetNumero) = 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Número de la CxC " & insCxC.GetNumero & " No puede estar en blanco .:::"
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetNumero), " ")
      End If
      If gTexto.DfLen(insCxC.GetTipoCxCStr) = 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Tipo de la CxC Nro " & insCxC.GetNumero & " NO puede estar en blanco .:::"
      ElseIf (insCxC.fSearchByNumeroTipoDeCxC(insCxC.GetNumero, gEnumProyecto.strTipoDeCxCToNum(insCxC.GetTipoCxCStr), False, False)) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR La CxC Nro:" & insCxC.GetNumero & " De Tipo " & insCxC.GetTipoCxCStr & " <== ya se encuentra registrada en el sistema .:::"
      ElseIf gTexto.DfLen(insCxC.GetTipoCxCStr) > 0 And Not fEsUnTpoDeCuentaPorCobrarPagar(insCxC.GetTipoCxCStr) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR Indicó un tipo de CxC no válido para el Documento ==>" & insCxC.GetNumero & "-" & insCxC.GetNombreCliente & "<==   se reemplazará por " & gEnumProyecto.enumTipoDeCxCToSiglas(eTD_FACTURA) & " .:::"
      ElseIf insImportar.fYaFueCargadoElItemEnElArray(mVectorDeVerificaciones, valregistroActual, mNumeroYTipoDeCxC, CM_INDEX_NUMERO_Y_TIPO_DE_CXC) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR la CxC Nro " & insCxC.GetNumero & " del Tipo " & insCxC.GetTipoCxCStr & " Aparece más de una vez en el archivo de importación .:::"
         GoTo h_EXIT
      Else
         If CM_INDEX_NUMERO_Y_TIPO_DE_CXC < UBound(mVectorDeVerificaciones) And CM_INDEX_NUMERO_Y_TIPO_DE_CXC >= LBound(mVectorDeVerificaciones) Then
            mVectorDeVerificaciones(CM_INDEX_NUMERO_Y_TIPO_DE_CXC, valregistroActual) = mNumeroYTipoDeCxC
         End If
         LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetTipoCxCStr), " ")
      End If
      If gTexto.DfLen(insCxC.GetStatusStr) > 0 And gEnumProyecto.strStatusDocumentoToNum(insCxC.GetStatusStr) <> enum_StatusDocumento.eSD_PORCANCELAR Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR solo se importaran documentos cuyo Estatus sea POR CANCELAR  .:::"
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetStatusStr), " ")
      End If
      If gTexto.DfLen(insCxC.GetCodigoCliente) > 0 And Not insCliente.fSearchByField("Codigo", insCxC.GetCodigoCliente, False, False) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR el Cliente " & insCxC.GetCodigoCliente & " de la CxC nro " & insCxC.GetNumero & " No existe. Debe incluirlo antes de ejecutar el proceso .:::"
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetCodigoCliente), " ")
      End If
      If gTexto.DfLen(insCxC.GetNombreCliente) = 0 And Not insCliente.fSearchByField("Nombre", insCxC.GetNombreCliente, False, False) Then
         LineaDeErrores = LineaDeErrores & ":::. El Nombre del Cliente de la CxC Nro " & insCxC.GetNumero & " No puede estar en blanco .:::"
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetNombreCliente), " ")
      End If
      If gTexto.DfLen(insCxC.GetCodigoVendedor) > 0 And Not insVendedor.fSearchByField("Codigo", insCxC.GetCodigoVendedor, False, False) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR el vendedor " & insCxC.GetCodigoVendedor & " de la CxC Nro " & insCxC.GetNumero & " No esta registrado en el sistema. Debe incluirlo antes de ejecutar el proceso .:::"
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetCodigoVendedor), " ")
      End If
      If gTexto.DfLen(insCxC.GetNombreVendedor) > 0 And Not insVendedor.fSearchByField("Nombre", insCxC.GetNombreVendedor, False, False) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. El Nombre del Vendedor " & insCxC.GetNombreVendedor & "  de la CxC Nro " & insCxC.GetNumero & " No sta registrado en el sistema. Debe incluirlo antes de ejecutar el proceso.:::"
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetNombreVendedor), " ")
      End If
      If gTexto.DfLen(gConvert.fechaAString(insCxC.GetFecha)) = 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR La Fecha de la CxC " & insCxC.GetNumero & " NO puede estar en blanco.:::"
      Else
         LineaDeErrores = LineaDeErrores & String(gTexto.DfLen(gConvert.fechaAString(insCxC.GetFecha)), " ")
      End If
      LineaDeErrores = LineaDeErrores & String(gTexto.DfLen(gConvert.fechaAString(insCxC.GetFechaCancelacion)), " ")
      If gUtilDate.fF1IsGreaterThanF2(gProyParametrosCompania.GetFechaMinimaIngresarDatos, insCxC.GetFecha) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR La Fecha de la CxC " & insCxC.GetNumero & "-" & gConvert.dateToString(insCxC.GetFecha) & " es menor a la fecha mínima permitida por el sistema para ingresar documentos en esta compania .:::"
      End If
      If gTexto.DfLen(CStr(insCxC.GetFechaVencimiento)) = 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR La Fecha de Vencimiemto de la CxC Nro " & insCxC.GetNumero & " no puede estar en blanco .:::"
      Else
         LineaDeErrores = LineaDeErrores & String(gTexto.DfLen(gConvert.fechaAString(insCxC.GetFechaVencimiento)), " ")
      End If
      If (insCxC.GetFechaVencimiento < insCxC.GetFecha) Then
         LineaDeErrores = LineaDeErrores & ":::.La fecha de vencimiento de la CxC Nro " & insCxC.GetNumero & " No puede ser inferior a la fecha origen.::: "
      Else
         LineaDeErrores = LineaDeErrores & String(Len(gConvert.fechaAString(insCxC.GetFechaVencimiento)), " ")
      End If
      If insCxC.GetMontoExento = 0 And insCxC.GetMontoGravado = 0 And insCxC.GetMontoIva = 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR La CxC Nro " & insCxC.GetNumero & " Debe tener por lo menos uno de los Montos no debe ser Cero .:::"
      ElseIf insCxC.GetMontoGravado > 0 And insCxC.GetMontoIva <= 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR La CxC Nro " & insCxC.GetNumero & " no puede tener Monto Gravado sin IVA .:::"
      ElseIf insCxC.GetMontoGravado = 0 And insCxC.GetMontoIva > 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR La CxC Nro " & insCxC.GetNumero & " no puede tener Monto IVA sin Monto Gravado .:::"
      Else
         LineaDeErrores = LineaDeErrores & String(gTexto.DfLen(gConvert.fConvierteAString(insCxC.GetMontoExento)), " ")
         LineaDeErrores = LineaDeErrores & String(gTexto.DfLen(gConvert.fConvierteAString(insCxC.GetMontoGravado)), " ")
         LineaDeErrores = LineaDeErrores & String(gTexto.DfLen(gConvert.fConvierteAString(insCxC.GetMontoIva)), " ")
      End If
      If gTexto.DfLen(insCxC.GetCodigoMoneda) > 0 And Not insMoneda.fSearchByField("Codigo", insCxC.GetCodigoMoneda, False, False) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR La Moneda " & insCxC.GetCodigoMoneda & " No existe. Debe incluirla antes de ejecutar la importacion .:::"
      Else
         If Not gProyParametrosCompania.GetUsaMonedaExtranjera And insCxC.GetCodigoMoneda <> mCodigoMonedaLocal Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR La Compañia NO usa moneda Extrajera .:::"
         Else
            LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetCodigoMoneda), " ")
         End If
      End If
      LineaDeErrores = LineaDeErrores & String(Len(CStr(insCxC.GetCambioABolivares)), " ")
      LineaDeErrores = LineaDeErrores & String(Len(CStr(insCxC.GetCodigoCC)), " ")
      LineaDeErrores = LineaDeErrores & String(Len(CStr(insCxC.GetCentroDeCostos)), " ")
      LineaDeErrores = LineaDeErrores & String(Len(CStr(insCxC.GetSeRetuvoIVA)), " ")
      LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetNumeroDocumentoOrigen), " ")
      LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetRefinanciadoSNStr), " ")
   End If
   If valVerificarParaImportacion Then
      If gTexto.DfLen(insCxC.GetNumero) = 0 Then
         refEsteRegistroSeIncluye = False
      ElseIf gEnumProyecto.strStatusDocumentoToNum(insCxC.GetStatusStr) <> enum_StatusDocumento.eSD_PORCANCELAR Then
         refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(insCxC.GetTipoCxCStr) = 0 Or Not fEsUnTpoDeCuentaPorCobrarPagar(insCxC.GetTipoCxCStr) Then
         refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(insCxC.GetCodigoCliente) = 0 Or Not insCliente.fSearchByField("Codigo", insCxC.GetCodigoCliente, False, False) Then
         refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(insCxC.GetCodigoVendedor) = 0 Or Not insVendedor.fSearchByField("Codigo", insCxC.GetCodigoVendedor, False, False) Then
         refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(insCxC.GetNombreVendedor) = 0 Or Not insVendedor.fSearchByField("Nombre", insCxC.GetNombreVendedor, False, False) Then
         refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(CStr(insCxC.GetFecha)) = 0 Then
         refEsteRegistroSeIncluye = False
      ElseIf gUtilDate.fF1IsGreaterThanF2(gProyParametrosCompania.GetFechaMinimaIngresarDatos, insCxC.GetFecha) Then
         refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(CStr(insCxC.GetFechaVencimiento)) = 0 Then
         refEsteRegistroSeIncluye = False
      ElseIf (insCxC.GetFechaVencimiento < insCxC.GetFecha) Then
         refEsteRegistroSeIncluye = False
      ElseIf insCxC.GetMontoExento = 0 And insCxC.GetMontoGravado = 0 And insCxC.GetMontoIva = 0 Then
         refEsteRegistroSeIncluye = False
      ElseIf insCxC.GetMontoGravado > 0 And insCxC.GetMontoIva <= 0 Then
         refEsteRegistroSeIncluye = False
      ElseIf insCxC.GetMontoGravado = 0 And insCxC.GetMontoIva > 0 Then
         refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(insCxC.GetCodigoMoneda) > 0 And Not insMoneda.fSearchByField("Codigo", insCxC.GetCodigoMoneda, False, False) Then
         refEsteRegistroSeIncluye = False
      ElseIf Not gProyParametrosCompania.GetUsaMonedaExtranjera And insCxC.GetCodigoMoneda <> mCodigoMonedaLocal Then
         refEsteRegistroSeIncluye = False
      Else
         If gTexto.DfLen(insCxC.GetTipoCxCStr) = 0 Or Not fEsUnTpoDeCuentaPorCobrarPagar(insCxC.GetTipoCxCStr) Then
            insCxC.SetTipoCxCStr gEnumProyecto.enumTipoDeCxCToString(eTD_FACTURA)
         End If
         insCxC.SetRefinanciadoSNStr gEnumProyecto.enumRefinanciadoToString(eRe_SIN_REFINANCIAR)
         insCxC.SetOrigenStr gEnumProyecto.enumOrigenFacturacionOManualToString(eOF_MANUAL)
         refEsteRegistroSeIncluye = True
      End If
         ErrorAlTraducirLaLinea = Not refEsteRegistroSeIncluye
   End If
h_EXIT: On Error GoTo 0
   fDistribuyeLaLineaDeLaCxC = Not ErrorAlTraducirLaLinea
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fDistribuyeLaLineaDeLaCxC", CM_MESSAGE_NAME_CXC, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDistribuyeLaLineaDeLaCxCEspecial(ByRef refLineaActual As String, ByVal valregistroActual As Long, ByVal valSeparador As String, ByVal valVerificarSiEstaRepetido As Boolean, ByRef refEsteRegistroSeIncluye As Boolean, ByVal valVerificarParaImportacion As Boolean) As Boolean
   Dim linea As String
   Dim ErrorAlTraducirLaLinea As Boolean
   Dim ExisteArticuloEnArchivo As Boolean
   Dim TextoAuxiliar As String
   On Error GoTo h_ERROR
   ErrorAlTraducirLaLinea = False
   LineaDeErrores = ""
   linea = refLineaActual
   insCxC.SetNombreOperador mNombreDelUsuario
   insCxC.SetNumero gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetStatusStr gEnumProyecto.strStatusDocumentoToSiglasToString(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetTipoCxCStr gEnumProyecto.strTipoDeCxCToSiglasToString(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   mNumeroYTipoDeCxC = insCxC.GetNumero + insCxC.GetTipoCxCStr
   insCxC.SetCodigoCliente gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetNombreCliente gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetCodigoVendedor gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetNombreVendedor gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetFecha gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetFechaCancelacion gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetFechaVencimiento gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetFechaAnulacion gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetMontoExento gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetMontoGravado gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetMontoIva gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetMontoAbonado gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetDescripcion gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetCodigoMoneda fValidaLamoneda(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetCambioABolivares gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetCodigoCc gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetCentroDeCostos gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetSeRetuvoIVA gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetNumeroDocumentoOrigen gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetNoAplicaParaLibroDeVentas gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxC.SetMoneda fDevuelveElNombreDeLaMonedaIfExist(insCxC.GetCodigoMoneda)
   insCxC.SetCodigoLote insLoteDeAdm.GetCodigoLote
   insCxC.SetNumeroControl gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxC.SetNumeroComprobanteFiscal gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   If valVerificarSiEstaRepetido Then
      If gTexto.DfLen(insCxC.GetNumero) = 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Número de la CxC " & insCxC.GetNumero & " No puede estar en blanco .:::"
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetNumero), " ")
      End If
      If gTexto.DfLen(insCxC.GetTipoCxCStr) = 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR El Tipo de la CxC Nro " & insCxC.GetNumero & " NO puede estar en blanco .:::"
      ElseIf (insCxC.fSearchByNumeroTipoDeCxC(insCxC.GetNumero, gEnumProyecto.strTipoDeCxCToNum(insCxC.GetTipoCxCStr), False, False)) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR La CxC Nro:" & insCxC.GetNumero & " De Tipo " & insCxC.GetTipoCxCStr & " <== ya se encuentra registrada en el sistema .:::"
      ElseIf gTexto.DfLen(insCxC.GetTipoCxCStr) > 0 And Not fEsUnTpoDeCuentaPorCobrarPagar(insCxC.GetTipoCxCStr) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR Indicó un tipo de CxC no válido para el Documento ==>" & insCxC.GetNumero & "-" & insCxC.GetNombreCliente & "<==   se reemplazará por " & gEnumProyecto.enumTipoDeCxCToSiglas(eTD_FACTURA) & " .:::"
      ElseIf insImportar.fYaFueCargadoElItemEnElArray(mVectorDeVerificaciones, valregistroActual, mNumeroYTipoDeCxC, CM_INDEX_NUMERO_Y_TIPO_DE_CXC) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR la CxC Nro " & insCxC.GetNumero & " del Tipo " & insCxC.GetTipoCxCStr & " Aparece más de una vez en el archivo de importación .:::"
         GoTo h_EXIT
      Else
         If CM_INDEX_NUMERO_Y_TIPO_DE_CXC < UBound(mVectorDeVerificaciones) And CM_INDEX_NUMERO_Y_TIPO_DE_CXC >= LBound(mVectorDeVerificaciones) Then
            mVectorDeVerificaciones(CM_INDEX_NUMERO_Y_TIPO_DE_CXC, valregistroActual) = mNumeroYTipoDeCxC
         End If
         LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetTipoCxCStr), " ")
      End If
      If gTexto.DfLen(insCxC.GetStatusStr) > 0 And (gEnumProyecto.strStatusDocumentoToNum(insCxC.GetStatusStr) <> enum_StatusDocumento.eSD_PORCANCELAR And gEnumProyecto.strStatusDocumentoToNum(insCxC.GetStatusStr) <> enum_StatusDocumento.eSD_CANCELADO) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR solo se importarán documentos cuyo Estatus sea POR CANCELAR y CANCELADO .:::"
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetStatusStr), " ")
      End If
      If gTexto.DfLen(insCxC.GetCodigoCliente) > 0 And Not insCliente.fSearchByField("Codigo", insCxC.GetCodigoCliente, False, False) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR el Cliente " & insCxC.GetCodigoCliente & " de la CxC nro " & insCxC.GetNumero & " No existe. Debe incluirlo antes de ejecutar el proceso .:::"
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetCodigoCliente), " ")
      End If
      If gTexto.DfLen(insCxC.GetNombreCliente) = 0 And Not insCliente.fSearchByField("Nombre", insCxC.GetNombreCliente, False, False) Then
         LineaDeErrores = LineaDeErrores & ":::. El Nombre del Cliente de la CxC Nro " & insCxC.GetNumero & " No puede estar en blanco .:::"
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetNombreCliente), " ")
      End If
      If gTexto.DfLen(insCxC.GetCodigoVendedor) > 0 And Not insVendedor.fSearchByField("Codigo", insCxC.GetCodigoVendedor, False, False) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR el vendedor " & insCxC.GetCodigoVendedor & " de la CxC Nro " & insCxC.GetNumero & " No esta registrado en el sistema. Debe incluirlo antes de ejecutar el proceso .:::"
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetCodigoVendedor), " ")
      End If
      If gTexto.DfLen(insCxC.GetNombreVendedor) > 0 And Not insVendedor.fSearchByField("Nombre", insCxC.GetNombreVendedor, False, False) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. El Nombre del Vendedor " & insCxC.GetNombreVendedor & "  de la CxC Nro " & insCxC.GetNumero & " No sta registrado en el sistema. Debe incluirlo antes de ejecutar el proceso.:::"
      Else
         LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetNombreVendedor), " ")
      End If
      If gTexto.DfLen(gConvert.fechaAString(insCxC.GetFecha)) = 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR La Fecha de la CxC " & insCxC.GetNumero & " NO puede estar en blanco.:::"
      Else
         LineaDeErrores = LineaDeErrores & String(gTexto.DfLen(gConvert.fechaAString(insCxC.GetFecha)), " ")
      End If
      LineaDeErrores = LineaDeErrores & String(gTexto.DfLen(gConvert.fechaAString(insCxC.GetFechaCancelacion)), " ")
      If gUtilDate.fF1IsGreaterThanF2(gProyParametrosCompania.GetFechaMinimaIngresarDatos, insCxC.GetFecha) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR La Fecha de la CxC " & insCxC.GetNumero & "-" & gConvert.dateToString(insCxC.GetFecha) & " es menor a la fecha mínima permitida por el sistema para ingresar documentos en esta compania .:::"
      End If
      If gTexto.DfLen(CStr(insCxC.GetFechaVencimiento)) = 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR La Fecha de Vencimiemto de la CxC Nro " & insCxC.GetNumero & " no puede estar en blanco .:::"
      Else
         LineaDeErrores = LineaDeErrores & String(gTexto.DfLen(gConvert.fechaAString(insCxC.GetFechaVencimiento)), " ")
      End If
      If (insCxC.GetFechaVencimiento < insCxC.GetFecha) Then
         LineaDeErrores = LineaDeErrores & ":::.La fecha de vencimiento de la CxC Nro " & insCxC.GetNumero & " No puede ser inferior a la fecha origen.::: "
      Else
         LineaDeErrores = LineaDeErrores & String(Len(gConvert.fechaAString(insCxC.GetFechaVencimiento)), " ")
      End If
      If insCxC.GetTipoCxCStr = gEnumProyecto.enumTipoDeCxCToString(eTD_NOTADECREDITO) Then
         If insCxC.GetMontoExento > 0 Or insCxC.GetMontoGravado > 0 Or insCxC.GetMontoIva > 0 Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR La CxC Nro " & insCxC.GetNumero & " Debe tener los montos menores a Cero, por ser Nota de Crédito.:::"
         ElseIf insCxC.GetMontoGravado < 0 And insCxC.GetMontoIva = 0 Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR La CxC Nro " & insCxC.GetNumero & " no puede tener Monto Gravado sin IVA .:::"
         ElseIf insCxC.GetMontoGravado = 0 And insCxC.GetMontoIva < 0 Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR La CxC Nro " & insCxC.GetNumero & " no puede tener Monto IVA sin Monto Gravado .:::"
         Else
            LineaDeErrores = LineaDeErrores & String(gTexto.DfLen(gConvert.fConvierteAString(insCxC.GetMontoExento)), " ")
            LineaDeErrores = LineaDeErrores & String(gTexto.DfLen(gConvert.fConvierteAString(insCxC.GetMontoGravado)), " ")
            LineaDeErrores = LineaDeErrores & String(gTexto.DfLen(gConvert.fConvierteAString(insCxC.GetMontoIva)), " ")
         End If
      Else
         If insCxC.GetMontoExento = 0 And insCxC.GetMontoGravado = 0 And insCxC.GetMontoIva = 0 Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR La CxC Nro " & insCxC.GetNumero & " Debe tener por lo menos uno de los Montos no debe ser Cero .:::"
         ElseIf insCxC.GetMontoGravado > 0 And insCxC.GetMontoIva <= 0 Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR La CxC Nro " & insCxC.GetNumero & " no puede tener Monto Gravado sin IVA .:::"
         ElseIf insCxC.GetMontoGravado = 0 And insCxC.GetMontoIva > 0 Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR La CxC Nro " & insCxC.GetNumero & " no puede tener Monto IVA sin Monto Gravado .:::"
         Else
            LineaDeErrores = LineaDeErrores & String(gTexto.DfLen(gConvert.fConvierteAString(insCxC.GetMontoExento)), " ")
            LineaDeErrores = LineaDeErrores & String(gTexto.DfLen(gConvert.fConvierteAString(insCxC.GetMontoGravado)), " ")
            LineaDeErrores = LineaDeErrores & String(gTexto.DfLen(gConvert.fConvierteAString(insCxC.GetMontoIva)), " ")
         End If
      End If
      If gTexto.DfLen(insCxC.GetCodigoMoneda) > 0 And Not insMoneda.fSearchByField("Codigo", insCxC.GetCodigoMoneda, False, False) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & ":::. ERROR La Moneda " & insCxC.GetCodigoMoneda & " No existe. Debe incluirla antes de ejecutar la importacion .:::"
      Else
         If Not gProyParametrosCompania.GetUsaMonedaExtranjera And insCxC.GetCodigoMoneda <> mCodigoMonedaLocal Then
            ErrorAlTraducirLaLinea = True
            LineaDeErrores = LineaDeErrores & ":::. ERROR La Compañia NO usa moneda Extrajera .:::"
         Else
            LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetCodigoMoneda), " ")
         End If
      End If
      LineaDeErrores = LineaDeErrores & String(Len(CStr(insCxC.GetCambioABolivares)), " ")
      LineaDeErrores = LineaDeErrores & String(Len(CStr(insCxC.GetCodigoCC)), " ")
      LineaDeErrores = LineaDeErrores & String(Len(CStr(insCxC.GetCentroDeCostos)), " ")
      LineaDeErrores = LineaDeErrores & String(Len(CStr(insCxC.GetSeRetuvoIVA)), " ")
      LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetNumeroDocumentoOrigen), " ")
      LineaDeErrores = LineaDeErrores & String(Len(insCxC.GetRefinanciadoSNStr), " ")
   End If
   If valVerificarParaImportacion Then
      If gTexto.DfLen(insCxC.GetNumero) = 0 Then
         refEsteRegistroSeIncluye = False
      ElseIf gEnumProyecto.strStatusDocumentoToNum(insCxC.GetStatusStr) <> enum_StatusDocumento.eSD_PORCANCELAR And gEnumProyecto.strStatusDocumentoToNum(insCxC.GetStatusStr) <> enum_StatusDocumento.eSD_CANCELADO Then
         refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(insCxC.GetTipoCxCStr) = 0 Or Not fEsUnTpoDeCuentaPorCobrarPagar(insCxC.GetTipoCxCStr) Then
         refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(insCxC.GetCodigoCliente) = 0 Or Not insCliente.fSearchByField("Codigo", insCxC.GetCodigoCliente, False, False) Then
         refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(insCxC.GetCodigoVendedor) = 0 Or Not insVendedor.fSearchByField("Codigo", insCxC.GetCodigoVendedor, False, False) Then
         refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(insCxC.GetNombreVendedor) = 0 Or Not insVendedor.fSearchByField("Nombre", insCxC.GetNombreVendedor, False, False) Then
         refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(CStr(insCxC.GetFecha)) = 0 Then
         refEsteRegistroSeIncluye = False
      ElseIf gUtilDate.fF1IsGreaterThanF2(gProyParametrosCompania.GetFechaMinimaIngresarDatos, insCxC.GetFecha) Then
         refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(CStr(insCxC.GetFechaVencimiento)) = 0 Then
         refEsteRegistroSeIncluye = False
      ElseIf (insCxC.GetFechaVencimiento < insCxC.GetFecha) Then
         refEsteRegistroSeIncluye = False
      ElseIf (insCxC.GetTipoCxCStr = gEnumProyecto.enumTipoDeCxCToString(eTD_NOTADECREDITO)) And (insCxC.GetMontoExento > 0 Or insCxC.GetMontoGravado > 0 Or insCxC.GetMontoIva > 0) Then
         refEsteRegistroSeIncluye = False
      ElseIf (insCxC.GetTipoCxCStr = gEnumProyecto.enumTipoDeCxCToString(eTD_NOTADECREDITO)) And (insCxC.GetMontoGravado < 0 And insCxC.GetMontoIva = 0) Then
         refEsteRegistroSeIncluye = False
      ElseIf (insCxC.GetTipoCxCStr = gEnumProyecto.enumTipoDeCxCToString(eTD_NOTADECREDITO)) And (insCxC.GetMontoGravado = 0 And insCxC.GetMontoIva < 0) Then
         refEsteRegistroSeIncluye = False
      ElseIf (insCxC.GetTipoCxCStr <> gEnumProyecto.enumTipoDeCxCToString(eTD_NOTADECREDITO)) And (insCxC.GetMontoExento = 0 And insCxC.GetMontoGravado = 0 And insCxC.GetMontoIva = 0) Then
         refEsteRegistroSeIncluye = False
      ElseIf (insCxC.GetTipoCxCStr <> gEnumProyecto.enumTipoDeCxCToString(eTD_NOTADECREDITO)) And (insCxC.GetMontoGravado > 0 And insCxC.GetMontoIva <= 0) Then
         refEsteRegistroSeIncluye = False
      ElseIf (insCxC.GetTipoCxCStr <> gEnumProyecto.enumTipoDeCxCToString(eTD_NOTADECREDITO)) And (insCxC.GetMontoGravado = 0 And insCxC.GetMontoIva > 0) Then
         refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(insCxC.GetCodigoMoneda) > 0 And Not insMoneda.fSearchByField("Codigo", insCxC.GetCodigoMoneda, False, False) Then
         refEsteRegistroSeIncluye = False
      ElseIf Not gProyParametrosCompania.GetUsaMonedaExtranjera And insCxC.GetCodigoMoneda <> mCodigoMonedaLocal Then
         refEsteRegistroSeIncluye = False
      Else
         If gTexto.DfLen(insCxC.GetTipoCxCStr) = 0 Or Not fEsUnTpoDeCuentaPorCobrarPagar(insCxC.GetTipoCxCStr) Then
            insCxC.SetTipoCxCStr gEnumProyecto.enumTipoDeCxCToString(eTD_FACTURA)
         End If
         insCxC.SetRefinanciadoSNStr gEnumProyecto.enumRefinanciadoToString(eRe_SIN_REFINANCIAR)
         insCxC.SetOrigenStr gEnumProyecto.enumOrigenFacturacionOManualToString(eOF_MANUAL)
         refEsteRegistroSeIncluye = True
      End If
         ErrorAlTraducirLaLinea = Not refEsteRegistroSeIncluye
   End If
h_EXIT: On Error GoTo 0
   fDistribuyeLaLineaDeLaCxCEspecial = Not ErrorAlTraducirLaLinea
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fDistribuyeLaLineaDeLaCxCEspecial", CM_MESSAGE_NAME_CXC, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fVerificaSiNoExisteLaCxCYLoAgrega(ByRef refRegistrosImportados As Long) As Boolean
   Dim Continuar As Boolean
   On Error GoTo h_ERROR
   Continuar = False
   If Not (insCxC.fSearchByNumeroTipoDeCxC(insCxC.GetNumero, gEnumProyecto.strTipoDeCxCToNum(insCxC.GetTipoCxCStr), False, False)) Then
      If insCxC.fInsert(False) Then
         Continuar = True
         refRegistrosImportados = refRegistrosImportados + 1
      End If
   Else
      RegistrosYaExistentesEnBD = RegistrosYaExistentesEnBD + 1
   End If
h_EXIT: On Error GoTo 0
   fVerificaSiNoExisteLaCxCYLoAgrega = Continuar
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fVerificaSiNoExisteLaCxCYLoAgrega", CM_MESSAGE_NAME_CXC, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDeComprobanteCxC(ByRef refNumero As String) As String
   Dim SQL As String
   Dim sqlTotal As String
   Dim sqlMontoRestante As String
   On Error GoTo h_ERROR
   SQL = " SELECT "
   SQL = SQL & "cxC.Numero AS Numero, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxC.TipoCxC", enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True), "TipoCxC") & ","
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxC.Status", enum_StatusDocumento.eSD_PORCANCELAR, gEnumProyecto.fEnumStatusCxCToStringInArray(True), "Status") & ","
   SQL = SQL & "cxC.CodigoMoneda AS CodigoMoneda, "
   SQL = SQL & "cxC.CambioABolivares AS CambioABolivares, "
   SQL = SQL & "cliente.Codigo As CodigoCliente, "
   SQL = SQL & "cliente.Nombre As NombreCliente, "
   SQL = SQL & "vendedor.Codigo As CodigoVendedor, "
   SQL = SQL & "vendedor.Nombre As NombreVendedor, "
   SQL = SQL & "cxC.Fecha AS Fecha, "
   SQL = SQL & "cxC.FechaVencimiento AS FechaVencimiento, "
   SQL = SQL & "cxC.Descripcion AS Descripcion, "
   SQL = SQL & "cxC.CentroDeCostos AS CentroDeCostos, "
   SQL = SQL & "cxC.MontoExento AS MontoExento,"
   SQL = SQL & "cxC.MontoGravado AS MontoGravado,"
   SQL = SQL & "cxC.MontoIVA AS MontoIVA,"
   SQL = SQL & "cxC.Numero AS NumeroDocumento, "
   SQL = SQL & "cxC.NumeroControl AS NumeroControl, "
   sqlTotal = "(cxC.MontoExento + cxC.MontoGravado + cxC.MontoIVA)"
   SQL = SQL & sqlTotal & " AS MontoTotal, "
   SQL = SQL & "cxC.MontoAbonado AS MontoAbonado,"
   sqlMontoRestante = sqlTotal & " - cxC.MontoAbonado"
   SQL = SQL & sqlMontoRestante & " AS RestaPorPagar"
   SQL = SQL & " FROM vendedor INNER JOIN cliente INNER JOIN cxC ON cliente.Codigo = cxC.CodigoCliente"
   SQL = SQL & " AND cliente.ConsecutivoCompania = cxC.ConsecutivoCompania  ON vendedor.Codigo = cxC.CodigoVendedor "
   SQL = SQL & " AND vendedor.ConsecutivoCompania = cxC.ConsecutivoCompania  AND vendedor.Codigo = cliente.CodigoVendedor "
   SQL = SQL & " AND vendedor.ConsecutivoCompania = cliente.ConsecutivoCompania "
   SQL = SQL & "WHERE cxC.ConsecutivoCompania = " & mConsecutivoCompaniaActual & " AND Numero = " & gUtilSQL.fSimpleSqlValue(refNumero)
h_EXIT: On Error GoTo 0
   fConstruirSQLDeComprobanteCxC = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fConstruirSQLDeComprobanteCxC", CM_MESSAGE_NAME_CXC, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fEsUnTpoDeCuentaPorCobrarPagar(ByVal valTipoDeCXCobrarPagar As String) As Boolean
   On Error GoTo h_ERROR
   If gEnumProyecto.strTipoDeCxCToNumBoolean(valTipoDeCXCobrarPagar) Then
      fEsUnTpoDeCuentaPorCobrarPagar = True
   Else
      fEsUnTpoDeCuentaPorCobrarPagar = False
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fEsUnTpoDeCuentaPorCobrarPagar", CM_MESSAGE_NAME_CXC, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDistribuyeLaLineaDeLaCxP(ByRef refLineaActual As String, ByVal valLineaActual As Long, ByVal valSeparador As String, ByRef refLineaDeError As String, ByVal valVerificarSiEstaRepetido As Boolean, ByRef refEsteRegistroSeIncluye As Boolean, ByVal valVerificarParaImportacion As Boolean, ByRef mCxPArray() As Variant) As Boolean
   Dim linea As String
   Dim ErrorAlTraducirLaLinea As Boolean
   Dim ExisteArticuloEnArchivo As Boolean
   Dim insImportar As clsImportar
   Dim Fecha As String
   Dim SumatoriasDeMontoIva As Currency
   Dim vEsUnaCuentaATerceros As String
   Dim vSeHizoLaDetraccion As String
   Dim vAplicaIvaAlicuotaEspecial As String
   On Error GoTo h_ERROR
   Set insImportar = New clsImportar
   ErrorAlTraducirLaLinea = False
   refLineaDeError = ""
   mNumeroYCodigoProveedor = ""
   linea = refLineaActual
   insCxP.SetNombreOperador mNombreDelUsuario
   If gGlobalization.fEsCodigoPeru Then
      insCxP.SetNumeroSerie gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      insCxP.SetNumeroDeDocumento gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      insCxP.SetNumero insCxP.GetNumeroSerie & "-" & insCxP.GetNumeroDeDocumento
      insCxP.SetCodigoTipoDeComprobante gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      insCxP.SetTipoDeCxPStr insCxP.ObtenerTipoDeCxPEquivalenteATipoDeComprobante(insCxP.GetCodigoTipoDeComprobante)
   Else
      insCxP.SetNumero gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      insCxP.SetTipoDeCxPStr gEnumProyecto.strTipoDeCxCToSiglasToString(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   End If
   insCxP.SetStatusStr gEnumProyecto.strStatusDocumentoToSiglasToString(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetCodigoProveedor gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   mNumeroYCodigoProveedor = insCxP.GetNumero + insCxP.getCodigoProveedor
   insCxP.SetNombreProveedor gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   Fecha = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxP.SetFecha gConvert.fConvertStringToDate(Fecha)
   insCxP.SetFechaCancelacion gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetFechaVencimiento gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetFechaAnulacion gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetCodigoMoneda fValidaLamoneda(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetCambioABolivares gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetAplicaParaLibrodeCompras gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetMontoExento gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetMontoGravado gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetMontoIva gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   SumatoriasDeMontoIva = insCxP.GetMontoIva
   insCxP.SetMontoAbonado gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetMesDeAplicacion gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetAnoDeAplicacion gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetObservaciones gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetCreditoFiscalStr gEnumProyecto.strSiglasCreditoFiscalToString(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetTipoDeCompraStr gEnumProyecto.strSiglasTipoDeCompraToString(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetSeHizoLaRetencion gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetMontoGravableAlicuotaGeneral gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetMontoGravableAlicuota2 gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetMontoGravableAlicuota3 gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetMontoIVAAlicuotaGeneral gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetMontoIVAAlicuota2 gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetMontoIVAAlicuota3 gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetNumeroPlanillaDeImportacion gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxP.SetNumeroExpedienteDeImportacion gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxP.SetTipoDeTransaccionStr gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxP.SetNumeroDeFacturaAfectada gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxP.SetNumeroControl gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxP.SetSeHizoLaRetencionIVA gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetNumeroComprobanteRetencion gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxP.SetFechaAplicacionRetIVA gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetPorcentajeRetencionAplicado gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetMontoRetenido gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetOrigenDeLaRetencionStr gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxP.SetRetencionAplicadaEnPago gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetOrigenInformacionRetencionStr gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxP.SetCxPgeneradaPorStr gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxP.SetEsCxPhistorica gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetNumDiasDeVencimiento gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetNumeroDocOrigen gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxP.SetGenerarAsientoDeRetiroEnCuenta gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetSeContabilRetIva gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetDondeContabilRetIVAStr gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxP.SetMoneda fDevuelveElNombreDeLaMonedaIfExist(insCxP.GetCodigoMoneda)
   insCxP.SetFechaAplicacionImpuestoMunicipal gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetNumeroDeclaracionAduana gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   Dim FechaDeclaracionAduana As String
   FechaDeclaracionAduana = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   If gTexto.DfLen(CStr(FechaDeclaracionAduana)) > 0 And gUtilDate.fElStringContieneUnaFechaValida(FechaDeclaracionAduana, False) Then
      insCxP.SetFechaDeclaracionAduana gConvert.fConvertStringToDate(FechaDeclaracionAduana)
   Else
      insCxP.SetFechaDeclaracionAduana gConvert.fConvertStringToDate(Fecha)
   End If
   Dim UsaPrefijoSerie As String
   UsaPrefijoSerie = gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   If gTexto.DfLen(CStr(UsaPrefijoSerie)) > 0 And (gConvert.ConvertBooleanToString(True) = gConvert.ConvertBooleanToString(UsaPrefijoSerie) Or gConvert.ConvertBooleanToString(False) = gConvert.ConvertBooleanToString(UsaPrefijoSerie)) Then
      insCxP.setUsaPrefijoSerie gConvert.ConvertStringToBoolean(gConvert.ConvertBooleanToString(UsaPrefijoSerie))
   Else
      insCxP.setUsaPrefijoSerie gConvert.ConvertStringToBoolean("N")
   End If
   vEsUnaCuentaATerceros = gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   If gTexto.DfLen(CStr(vEsUnaCuentaATerceros)) > 0 And (gConvert.ConvertBooleanToString(True) = gConvert.ConvertBooleanToString(vEsUnaCuentaATerceros) Or gConvert.ConvertBooleanToString(False) = gConvert.ConvertBooleanToString(vEsUnaCuentaATerceros)) Then
      insCxP.SetEsUnaCuentaATerceros gConvert.ConvertStringToBoolean(gConvert.ConvertBooleanToString(vEsUnaCuentaATerceros))
   Else
      insCxP.SetEsUnaCuentaATerceros gConvert.ConvertStringToBoolean("N")
   End If
   insCxP.SetCodigoProveedorOriginalServicio gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vSeHizoLaDetraccion = gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   If gTexto.DfLen(CStr(vSeHizoLaDetraccion)) > 0 And (gConvert.ConvertBooleanToString(True) = gConvert.ConvertBooleanToString(vSeHizoLaDetraccion) Or gConvert.ConvertBooleanToString(False) = gConvert.ConvertBooleanToString(vSeHizoLaDetraccion)) Then
      insCxP.SetSeHizoLaDetraccion gConvert.ConvertStringToBoolean(gConvert.ConvertBooleanToString(vSeHizoLaDetraccion))
   Else
      insCxP.SetSeHizoLaDetraccion gConvert.ConvertStringToBoolean("N")
   End If
   vAplicaIvaAlicuotaEspecial = gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   If gTexto.DfLen(CStr(vAplicaIvaAlicuotaEspecial)) > 0 And (gConvert.ConvertBooleanToString(True) = gConvert.ConvertBooleanToString(vAplicaIvaAlicuotaEspecial) Or gConvert.ConvertBooleanToString(False) = gConvert.ConvertBooleanToString(vAplicaIvaAlicuotaEspecial)) Then
      insCxP.SetAplicaIvaAlicuotaEspecial gConvert.ConvertStringToBoolean(gConvert.ConvertBooleanToString(vAplicaIvaAlicuotaEspecial))
   Else
      insCxP.SetAplicaIvaAlicuotaEspecial gConvert.ConvertStringToBoolean("N")
   End If
   insCxP.SetMontoGravableAlicuotaEspecial1 gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetMontoIVAAlicuotaEspecial1 gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetPorcentajeIvaAlicuotaEspecial1 gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetMontoGravableAlicuotaEspecial2 gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetMontoIVAAlicuotaEspecial2 gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetPorcentajeIvaAlicuotaEspecial2 gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetDiaDeAplicacion gUtilImport.fToInt(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   insCxP.SetNumeroSerieDocAfectado gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   insCxP.SetNumeroDeDocumentoAfectado gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   If Not (fFechaAplicacionEnCxPEsValida(refLineaDeError)) Then
      ErrorAlTraducirLaLinea = True
   End If
   ' Aqui termina la lectura de líneas
   If valVerificarSiEstaRepetido Then
      If gGlobalization.fEsCodigoPeru Then
         If gTexto.DfLen(insCxP.GetNumeroSerie) = 0 Then
            refLineaDeError = refLineaDeError & ":::. ERROR El número de serie No debe estar en blanco  .:::"
         End If
         If gTexto.DfLen(insCxP.GetNumeroDeDocumento) = 0 Then
            refLineaDeError = refLineaDeError & ":::. ERROR El número de documento No debe estar en blanco  .:::"
         End If
         If gTexto.DfLen(insCxP.GetCodigoTipoDeComprobante) = 0 Then
            refLineaDeError = refLineaDeError & ":::. ERROR El código del tipo de comprobante No debe estar en blanco  .:::"
         ElseIf gTexto.DfLen(insCxP.GetCodigoTipoDeComprobante) > 0 Then
            If Not insTipoDeComprobante.fSearchByField("Codigo", insCxP.GetCodigoTipoDeComprobante) Then
               refLineaDeError = refLineaDeError & ":::. ERROR El código del tipo de comprobante: " & insCxP.GetCodigoTipoDeComprobante & ", No existe, debe verificar dicho valor  .:::"
            End If
         End If
      Else
         If gTexto.DfLen(insCxP.GetNumero) = 0 Then
            ErrorAlTraducirLaLinea = True
            refLineaDeError = refLineaDeError & ":::. ERROR El Número de la CxP " & insCxP.GetNumero & " No puede estar en blanco .:::"
         Else
            refLineaDeError = refLineaDeError & String(Len(insCxP.GetNumero), " ")
         End If
         If gTexto.DfLen(insCxP.GetTipoDeCxPStr) > 0 And Not fEsUnTpoDeCuentaPorCobrarPagar(insCxP.GetTipoDeCxPStr) Then
            ErrorAlTraducirLaLinea = True
            refLineaDeError = refLineaDeError & ":::. ERROR Indicó un tipo de CxP  " & insCxP.GetTipoDeCxPStr & ". Se reemplazará por: FACTURA .:::"
         Else
            refLineaDeError = refLineaDeError & String(Len(insCxP.GetTipoDeCxPStr), " ")
         End If
      End If
      If gTexto.DfLen(insCxP.getCodigoProveedor) = 0 Then
         ErrorAlTraducirLaLinea = True
         refLineaDeError = refLineaDeError & ":::. ERROR El Código del proveedor de la CxP  " & insCxP.GetNumero & " No puede estar en blanco .:::"
      ElseIf Not insProveedor.fSearchByFieldImpExp("CodigoProveedor", insCxP.getCodigoProveedor) Then
         ErrorAlTraducirLaLinea = True
         refLineaDeError = refLineaDeError & ":::. ERROR el Proveedor " & insCxP.getCodigoProveedor & " No existe. Debe incluirlo antes de ejecutar la importacion .:::"
      ElseIf insImportar.fYaFueCargadoElItemEnElArray(mCxPArray, valLineaActual, mNumeroYCodigoProveedor, CM_INDEX_NUMERO_CXC_Y_CODIGO_PROVEEDOR) Then
         ErrorAlTraducirLaLinea = True
         refLineaDeError = refLineaDeError & ":::. ERROR la CxP Nro " & insCxP.GetNumero & " del Proveedor " & insCxP.getNombreProveedor & " Aparece más de una vez en el archivo de importación .:::"
         GoTo h_EXIT
      Else
         mCxPArray(CM_INDEX_NUMERO_CXC_Y_CODIGO_PROVEEDOR, valLineaActual) = mNumeroYCodigoProveedor
         refLineaDeError = refLineaDeError & String(Len(insCxP.getCodigoProveedor), " ")
      End If
      If gEnumProyecto.strStatusDocumentoToNum(insCxP.GetStatusStr) <> enum_StatusDocumento.eSD_PORCANCELAR Then
         ErrorAlTraducirLaLinea = True
         refLineaDeError = refLineaDeError & ":::. ERROR El Status " & insCxP.GetStatusStr & " de la CxP Nro " & insCxP.GetNumero & " Debe ser P\C.:::"
      ElseIf gTexto.DfLen(insCxP.GetStatusStr) = 0 Then
         ErrorAlTraducirLaLinea = True
         refLineaDeError = refLineaDeError & ":::. ERROR El Status de la CxP No puede estar en blanco. Se remplazara a:  Status Por Cancela& .:::"
      Else
         refLineaDeError = refLineaDeError & String(Len(insCxP.GetStatusStr), " ")
      End If
      If gTexto.DfLen(insCxP.getNombreProveedor) > 0 And Not insProveedor.fSearchByFieldImpExp("NombreProveedor", insCxP.getNombreProveedor) Then
         ErrorAlTraducirLaLinea = True
         refLineaDeError = refLineaDeError & ":::. ERROR El Nombre del Proveedor " & insCxP.getNombreProveedor & " de la CxP Nro " & insCxP.GetNumero & " No se encuentra registrado en el sistema. .:::"
      Else
         refLineaDeError = refLineaDeError & String(Len(insCxP.getNombreProveedor), " ")
      End If
      If gTexto.DfLen(CStr(insCxP.GetFecha)) = 0 Then
         ErrorAlTraducirLaLinea = True
         refLineaDeError = refLineaDeError & ":::. ERROR La Fecha de la CxP  " & insCxP.GetNumero & "-" & insCxP.GetNumero & "  no puede estar en blanco .:::"
      End If
      If gUtilDate.fF1IsGreaterThanF2(gProyParametrosCompania.GetFechaMinimaIngresarDatos, insCxP.GetFecha) Then
         ErrorAlTraducirLaLinea = True
         refLineaDeError = refLineaDeError & ":::. ERROR La Fecha de la CxP " & insCxP.GetNumero & "-" & gConvert.dateToString(insCxP.GetFecha) & " es menor a la fecha mínima permitida por el sistema para ingresar documentos en esta compania .:::"
      End If
      If gTexto.DfLen(CStr(insCxP.GetFechaVencimiento)) = 0 And (insCxP.GetFechaVencimiento < insCxP.GetFecha) Then
         refLineaDeError = refLineaDeError & ":::.La fecha de vencimiento de la CxP " & insCxP.GetNumero & "-" & insCxP.getCodigoProveedor & " no puede ser inferior a la fecha origen.::: "
      Else
         refLineaDeError = refLineaDeError & String(Len(CStr(insCxP.GetFechaVencimiento)), " ")
      End If
      If gTexto.DfLen(insCxP.GetCodigoMoneda) > 0 And Not insMoneda.fSearchByField("Codigo", insCxP.GetCodigoMoneda, False, False) Then
         ErrorAlTraducirLaLinea = True
         refLineaDeError = refLineaDeError & ":::. ERROR La Moneda " & insCxP.GetCodigoMoneda & " No existe. Debe incluirla antes de ejecutar la importacion .:::"
      Else
         If Not gProyParametrosCompania.GetUsaMonedaExtranjera And insCxP.GetCodigoMoneda <> mCodigoMonedaLocal Then
            ErrorAlTraducirLaLinea = True
            refLineaDeError = refLineaDeError & ":::. ERROR La Compañia NO usa moneda Extrajera y usted a indicado una moneda extranjera.:::"
         Else
            refLineaDeError = refLineaDeError & String(Len(insCxP.GetCodigoMoneda), " ")
         End If
      End If
      refLineaDeError = refLineaDeError & String(Len(CStr(insCxP.GetCambioABolivares)), " ")
      If fTipoDeComprasParaCreditofiscal(insCxP.GetTipoDeCompraStr) Then
         If insCxP.GetMontoExento = 0 Then
            ErrorAlTraducirLaLinea = True
            refLineaDeError = refLineaDeError & ":::. ERROR El Tipo de Compra " & insCxP.GetTipoDeCompraStr & " de la CxP " & insCxP.GetNumero & "-" & insCxP.getCodigoProveedor & " No puede tener monto exento en cero  .:::"
         ElseIf SumatoriasDeMontoIva > 0 Then
            ErrorAlTraducirLaLinea = True
            refLineaDeError = refLineaDeError & ":::. ERROR El Tipo de Compra " & insCxP.GetTipoDeCompraStr & " <= de la CxP " & insCxP.GetNumero & " - " & insCxP.getCodigoProveedor & " <= Solo puede tener monto exento.:::"
         ElseIf gEnumProyecto.strSiglasCreditoFiscalToNum(insCxP.GetCreditoFiscalStr) <> enum_CreditoFiscal.eCF_DEDUCIBLE Then
            ErrorAlTraducirLaLinea = True
            refLineaDeError = refLineaDeError & ":::.ERROR: El Tipo de Crédito Fiscal Para la CxP Nro " & insCxP.GetNumero & " Debe ser DEDUCIBLE .:::"
         Else
            refLineaDeError = refLineaDeError & String(gTexto.DfLen(CStr(insCxP.GetTipoDeCompraStr)), " ")
            refLineaDeError = refLineaDeError & String(gTexto.DfLen(CStr(insCxP.GetMontoIva)), " ")
            refLineaDeError = refLineaDeError & String(gTexto.DfLen(insCxP.GetCreditoFiscalStr), " ")
         End If
      Else
         refLineaDeError = refLineaDeError & String(gTexto.DfLen(CStr(insCxP.GetTipoDeCompraStr)), " ")
         refLineaDeError = refLineaDeError & String(gTexto.DfLen(CStr(insCxP.GetMontoExento)), " ")
         refLineaDeError = refLineaDeError & String(gTexto.DfLen(CStr(insCxP.GetMontoIva)), " ")
      End If
      If Not fMesYAñoDeAplicacionValido Or CStr(insCxP.GetAnoDeAplicacion) < 0 Then
         ErrorAlTraducirLaLinea = True
         refLineaDeError = refLineaDeError & ":::.ERROR La combinacion de Mes y Año de Aplicacion de la CxP " & insCxP.GetNumero & " No coinciden con el mes y año de plicación indicada en la factura .:::"
       Else
         refLineaDeError = refLineaDeError & String(Len(CStr(insCxP.GetMesDeAplicacion)), " ")
         refLineaDeError = refLineaDeError & String(Len(CStr(insCxP.GetAnoDeAplicacion)), " ")
       End If
      refLineaDeError = refLineaDeError & String(gTexto.DfLen(insCxP.GetObservaciones), " ")
      If insCxP.GetTipoDeCompraChar = enum_TipoDeCompra.eTD_COMPRASIMPORTACION _
         And (gTexto.DfLen(insCxP.GetNumeroPlanillaDeImportacion) = 0 Or gTexto.DfLen(insCxP.GetNumeroExpedienteDeImportacion) = 0) Then
            ErrorAlTraducirLaLinea = True
            refLineaDeError = refLineaDeError & ":::. ERROR El Número de Planilla, de Expediente de la CxP  y el número de declaración de aduana " & insCxP.GetNumero & "  No puede estar en blanco.:::"
      ElseIf insCxP.GetTipoDeCompraChar <> enum_TipoDeCompra.eTD_COMPRASIMPORTACION _
         And (gTexto.DfLen(insCxP.GetNumeroPlanillaDeImportacion) > 0 Or gTexto.DfLen(insCxP.GetNumeroExpedienteDeImportacion) > 0) Then
            ErrorAlTraducirLaLinea = True
            refLineaDeError = refLineaDeError & ":::. ERROR El Número de Planilla, de Expediente de la CxP y el número de declaración de aduana " & insCxP.GetNumero & " Solo es requerida en Compras de Importacion .:::"
      Else
         refLineaDeError = refLineaDeError & String(gTexto.DfLen(CStr(insCxP.GetMontoExento)), " ")
         refLineaDeError = refLineaDeError & String(gTexto.DfLen(insCxP.GetNumeroExpedienteDeImportacion), " ")
         refLineaDeError = refLineaDeError & String(gTexto.DfLen(insCxP.GetNumeroPlanillaDeImportacion), " ")
      End If
      refLineaDeError = refLineaDeError & String(gTexto.DfLen(CStr(insCxP.GetSeHizoLaRetencion)), " ")
      If insCxP.GetMontoGravableAlicuotaGeneral > 0 And Not insCxP.GetMontoIVAAlicuotaGeneral > 0 Then
         ErrorAlTraducirLaLinea = True
         refLineaDeError = refLineaDeError & " :::. Error El Monto IVA Alicuota General no puede ser Cero .:::"
      Else
         refLineaDeError = refLineaDeError & String(gTexto.DfLen(CStr(insCxP.GetMontoGravableAlicuotaGeneral)), " ")
      End If
      If insCxP.GetMontoGravableAlicuota2 > 0 And Not insCxP.GetMontoIVAAlicuota2 > 0 Then
         ErrorAlTraducirLaLinea = True
         refLineaDeError = refLineaDeError & " :::. Error El Monto IVA Alicuota 2 no puede ser Cero .:::"
      Else
         refLineaDeError = refLineaDeError & String(gTexto.DfLen(CStr(insCxP.GetMontoGravableAlicuota2)), " ")
      End If
      If insCxP.GetMontoGravableAlicuota3 > 0 And Not insCxP.GetMontoIVAAlicuota3 > 0 Then
         ErrorAlTraducirLaLinea = True
         refLineaDeError = refLineaDeError & " :::. Error El Monto IVA Alicuota 3 no puede ser Cero .:::"
      Else
         refLineaDeError = refLineaDeError & String(gTexto.DfLen(CStr(insCxP.GetMontoGravableAlicuota3)), " ")
      End If
      refLineaDeError = refLineaDeError & String(gTexto.DfLen(CStr(insCxP.GetMontoIVAAlicuotaGeneral)), " ")
      refLineaDeError = refLineaDeError & String(gTexto.DfLen(CStr(insCxP.GetMontoIVAAlicuota2)), " ")
      refLineaDeError = refLineaDeError & String(gTexto.DfLen(CStr(insCxP.GetMontoIVAAlicuota3)), " ")
      refLineaDeError = refLineaDeError & String(gTexto.DfLen(insCxP.GetTipoDeTransaccionStr), " ")
      If gTexto.DfLen(insCxP.GetNumeroDeFacturaAfectada) > 0 And (gEnumProyecto.strTipoDeCxCToNumToSiglas(insCxP.GetTipoDeCxPStr) <> enum_TipoDeTransaccion.eTD_NOTADECREDITO Or gEnumProyecto.strTipoDeCxCToNumToSiglas(insCxP.GetTipoDeCxPStr) <> enum_TipoDeTransaccion.eTD_NOTADEDEBITO) Then
         ErrorAlTraducirLaLinea = True
         refLineaDeError = refLineaDeError & ":::. ERROR El número de Factura Afectada de la CxP Nro " & insCxP.GetNumero & " No es valida para " & insCxP.GetTipoDeCxPStr & " .:::"
      ElseIf gTexto.DfLen(insCxP.GetNumeroDeFacturaAfectada) = 0 And (gEnumProyecto.strTipoDeCxCToNumToSiglas(insCxP.GetTipoDeCxPStr) = enum_TipoDeTransaccion.eTD_NOTADECREDITO Or gEnumProyecto.strTipoDeCxCToNumToSiglas(insCxP.GetTipoDeCxPStr) = enum_TipoDeTransaccion.eTD_NOTADEDEBITO) Then
         ErrorAlTraducirLaLinea = True
         refLineaDeError = refLineaDeError & ":::. ERROR El número de Factura Afectada de la CxP Nro " & insCxP.GetNumero & " No debe estar en blanco  .:::"
      Else
         refLineaDeError = refLineaDeError & String(Len(insCxP.GetNumeroDeFacturaAfectada), " ")
      End If
      If gGlobalization.fEsCodigoPeru And ((gTexto.DfLen(insCxP.GetNumeroSerieDocAfectado) = 0 Or gTexto.DfLen(insCxP.GetNumeroDeDocumentoAfectado) = 0) And (insCxP.GetTipoDeCxPAsEnum <> enum_TipoDeTransaccion.eTD_NOTADEDEBITO And insCxP.GetTipoDeCxPAsEnum <> enum_TipoDeTransaccion.eTD_NOTADECREDITO)) Then
         ErrorAlTraducirLaLinea = True
         refLineaDeError = refLineaDeError & ":::. ERROR El número de serie y documento del Documento Afectado de la CxP Nro " & insCxP.GetNumero & " No deben estar en blanco  .:::"
      End If
      If gTexto.DfLen(insCxP.GetNumeroControl) = 0 Then
         ErrorAlTraducirLaLinea = True
         refLineaDeError = refLineaDeError & ":::. ERROR El Numero de Control  de la CxP  " & insCxP.GetNumero & " No puede estar en blanco.:::"
      Else
         refLineaDeError = refLineaDeError & String(Len(insCxP.GetNumeroControl), " ")
      End If
   End If
   If valVerificarParaImportacion Then
      If gTexto.DfLen(insCxP.GetNumero) = 0 Then
         refEsteRegistroSeIncluye = False
      ElseIf gEnumProyecto.strStatusDocumentoToNum(insCxP.GetStatusStr) <> enum_StatusDocumento.eSD_PORCANCELAR Then
         refEsteRegistroSeIncluye = False
      ElseIf Not insProveedor.fSearchByFieldImpExp("CodigoProveedor", insCxP.getCodigoProveedor) Then
         refEsteRegistroSeIncluye = False
      ElseIf Not insProveedor.fSearchByFieldImpExp("NombreProveedor", insCxP.getNombreProveedor) Then
         refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(CStr(insCxP.GetFecha)) = 0 Then
         refEsteRegistroSeIncluye = False
      ElseIf gUtilDate.fF1IsGreaterThanF2(gProyParametrosCompania.GetFechaMinimaIngresarDatos, insCxP.GetFecha) Then
         refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(CStr(insCxP.GetFechaVencimiento)) = 0 Then
         refEsteRegistroSeIncluye = False
      ElseIf fTipoDeComprasParaCreditofiscal(insCxP.GetTipoDeCompraStr) And (insCxP.GetMontoExento = 0 Or SumatoriasDeMontoIva > 0) Then
         refEsteRegistroSeIncluye = False
      ElseIf Not fMesYAñoDeAplicacionValido Then
         refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(insCxP.GetNumeroDeFacturaAfectada) > 0 And (gEnumProyecto.strTipoDeCxCToNumToSiglas(insCxP.GetTipoDeCxPStr) <> enum_TipoDeTransaccion.eTD_NOTADECREDITO Or gEnumProyecto.strTipoDeCxCToNumToSiglas(insCxP.GetTipoDeCxPStr) <> enum_TipoDeTransaccion.eTD_NOTADEDEBITO) Then
            refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(insCxP.GetNumeroDeFacturaAfectada) = 0 And (gEnumProyecto.strTipoDeCxCToNumToSiglas(insCxP.GetTipoDeCxPStr) = enum_TipoDeTransaccion.eTD_NOTADECREDITO Or gEnumProyecto.strTipoDeCxCToNumToSiglas(insCxP.GetTipoDeCxPStr) = enum_TipoDeTransaccion.eTD_NOTADEDEBITO) Then
          refEsteRegistroSeIncluye = False
      ElseIf gTexto.DfLen(insCxP.GetCodigoMoneda) > 0 And Not insMoneda.fSearchByField("Codigo", insCxP.GetCodigoMoneda, False, False) Then
         refEsteRegistroSeIncluye = False
      ElseIf Not gProyParametrosCompania.GetUsaMonedaExtranjera And insCxP.GetCodigoMoneda <> mCodigoMonedaLocal Then
         refEsteRegistroSeIncluye = False
      ElseIf gGlobalization.fEsCodigoPeru And (insCxP.GetTipoDeCxPAsEnum = enum_TipoDeTransaccion.eTD_NOTADEDEBITO Or insCxP.GetTipoDeCxPAsEnum = enum_TipoDeTransaccion.eTD_NOTADECREDITO) And _
      (gTexto.DfLen(insCxP.GetNumeroSerieDocAfectado) = 0 Or gTexto.DfLen(insCxP.GetNumeroDeDocumentoAfectado) = 0) Then
         refEsteRegistroSeIncluye = False
      Else
         If gTexto.DfLen(insCxP.GetTipoDeCxPStr) = 0 Or Not fEsUnTpoDeCuentaPorCobrarPagar(insCxP.GetTipoDeCxPStr) Then
            insCxP.SetTipoDeCxPStr gEnumProyecto.enumTipoDeCxCToString(eTD_FACTURA)
         End If
         If fTipoDeComprasParaCreditofiscal(insCxP.GetMontoExento) And gEnumProyecto.strCreditoFiscalToNum(insCxP.GetCreditoFiscalStr) <> enum_CreditoFiscal.eCF_DEDUCIBLE Then
            insCxP.SetCreditoFiscalStr gEnumProyecto.enumCreditoFiscalToString(eCF_DEDUCIBLE)
         End If
         If ((insCxP.GetTipoDeCxPAsEnum <> enum_TipoDeTransaccion.eTD_NOTADEDEBITO And insCxP.GetTipoDeCxPAsEnum <> enum_TipoDeTransaccion.eTD_NOTADECREDITO)) Then
            insCxP.SetNumeroSerieDocAfectado ""
            insCxP.SetNumeroDeDocumentoAfectado ""
         End If
         refEsteRegistroSeIncluye = True
      End If
      ErrorAlTraducirLaLinea = Not refEsteRegistroSeIncluye
   End If
   Set insImportar = Nothing
h_EXIT: On Error GoTo 0
   fDistribuyeLaLineaDeLaCxP = Not ErrorAlTraducirLaLinea
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fDistribuyeLaLineaDeLaCxP", CM_MESSAGE_NAME_CXP, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fVerificaSiNoExisteLaCxPYLoAgrega(ByRef refRegistrosImportados As Long) As Boolean
   Dim Continuar As Boolean
   On Error GoTo h_ERROR
   Continuar = False
   If Not insCxP.fSearchByCodigoProveedoryNumeroCxP(insCxP.getCodigoProveedor, insCxP.GetNumero, False) Then
      insCxP.sInsertNewEmptyRecord False
      If insCxP.fInsert(insertar, False, True) Then
         Continuar = True
         refRegistrosImportados = refRegistrosImportados + 1
      End If
   Else
      RegistrosYaExistentesEnBD = RegistrosYaExistentesEnBD + 1
   End If
h_EXIT: On Error GoTo 0
   fVerificaSiNoExisteLaCxPYLoAgrega = Continuar
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fVerificaSiNoExisteLaCxPYLoAgrega", CM_MESSAGE_NAME_CXP, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fTipoDeComprasParaCreditofiscal(ByVal valTipoDeCompra As String) As Boolean
   On Error GoTo h_ERROR
   If gEnumProyecto.strTipoDeCompraToNum(valTipoDeCompra, False) = enum_TipoDeCompra.eTD_COMPRASEXENTAS Then
      fTipoDeComprasParaCreditofiscal = True
   ElseIf gEnumProyecto.strTipoDeCompraToNum(valTipoDeCompra, False) = enum_TipoDeCompra.eTD_COMPRASEXONERADAS Then
      fTipoDeComprasParaCreditofiscal = True
   ElseIf gEnumProyecto.strTipoDeCompraToNum(valTipoDeCompra, False) = enum_TipoDeCompra.eTD_COMPRASNOSUJETAS Then
      fTipoDeComprasParaCreditofiscal = True
   ElseIf gEnumProyecto.strTipoDeCompraToNum(valTipoDeCompra, False) = enum_TipoDeCompra.eTD_COMPRASSINDERECHOACREDITO Then
      fTipoDeComprasParaCreditofiscal = True
   Else
      fTipoDeComprasParaCreditofiscal = False
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fTipoDeComprasParaCreditofiscal", CM_MESSAGE_NAME_CXP, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fMesYAñoDeAplicacionValido() As Boolean
   Dim fechaDeAplicacionValida As Boolean
   On Error GoTo h_ERROR
   fechaDeAplicacionValida = False
   If CStr(insCxP.GetMesDeAplicacion) > 0 And CStr(insCxP.GetMesDeAplicacion) < 13 Then
      If gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(CStr(insCxP.GetMesDeAplicacion), CStr(insCxP.GetAnoDeAplicacion), False) >= insCxP.GetFecha Then
         fechaDeAplicacionValida = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fMesYAñoDeAplicacionValido = fechaDeAplicacionValida
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fMesYAñoDeAplicacionValido", CM_MESSAGE_NAME_CXP, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function getRealizarBusquedaEnArchivo() As Boolean
   getRealizarBusquedaEnArchivo = mRealizarBusquedaEnArchivo
End Function

Private Sub SetRealizarBusquedaEnArchivo(ByVal valNewValue As Long)
   mRealizarBusquedaEnArchivo = valNewValue
End Sub

Private Function fAgregaElArticulo() As Boolean
   Dim Continuar As Boolean
   On Error GoTo h_ERROR
   Continuar = False
   If Not insArticuloInventario.fSearchByCodigoArticulo(insArticuloInventario.GetCodigo, True, False, False, True, True) Then
      If fAgregaAlmacen Then
         If insArticuloInventario.fInsert(insertar, False, False) Then           'se inserta el renglon de la nota de entrada si es mercancia y variable boolean true
            mNregistroExistenBd = mNregistroExistenBd + 1
            If Not insCamposMonedaExtranjera.fSearchByField("Codigo", insArticuloInventario.GetCodigo, False, False) Then
               If insCamposMonedaExtranjera.fInsert Then
                  insCamposMonedaExtranjera.sClrRecord
               End If
            End If
            insArticuloInventario.sInsertNewEmptyMasterDetailArticuloInventario
            Continuar = True
            If fAgregaTallasyColores Then
            End If
         End If
      End If
   Else
      If fAgregaTallasyColores Then
         Continuar = True
      End If
      If fAgregaAlmacen Then
         Continuar = True
      End If
   End If
h_EXIT: fAgregaElArticulo = Continuar
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fAgregaElArticulo", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDistribuyeLaLineaDeArticulo(ByRef refLineaActual As String, ByVal valLineaActual As Long) As Boolean
   Dim texto As String, linea As String
   Dim ErrorAlTraducirLaLinea As Boolean
   Dim ExisteLineaDeProducto As Boolean
   Dim valPrecioSinIVA As Currency
   Dim valPrecioConIVA As Currency
   Dim valPrecioSinIva2 As Currency
   Dim valPrecioConIva2 As Currency
   Dim valPrecioSinIva3 As Currency
   Dim valPrecioConIva3 As Currency
   Dim valPrecioSinIva4 As Currency
   Dim valPrecioConIva4 As Currency
   Dim valPorcentajeBase As Currency
   Dim valCostoUnitario As Currency
   Dim valExistencia As Currency
   Dim valAlicuota As String
   Dim valMoneda As String
   Dim valCodigo As String
   Dim valDescripcion As String
   Dim valMontoBlanco As Currency
   Dim mrsUnidad As ADODB.Recordset
   Dim SQL As String
   Dim vEsResumenDiario As Boolean
   On Error GoTo h_ERROR
   Set insImportar = New clsImportar
   ErrorAlTraducirLaLinea = False
   ExisteLineaDeProducto = False
   vEsResumenDiario = False
   insArticuloInventario.sClrRecord
   LineaDeErrores = ""
   With insArticuloInventario
      linea = refLineaActual
      .SetConsecutivoCompania mConsecutivoCompaniaActual
       texto = gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
      If insArticuloInventario.fEsArticuloResumenDiario(texto) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & " ADVERTENCIA: código reservado para resumen diario (" & texto & ")"
         vEsResumenDiario = True
         GoTo h_EXIT
      Else
         .SetCodigo texto
         valCodigo = texto
         LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
         texto = ""
         texto = gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
         .SetDescripcion gTexto.DfMid(texto, 1, 255)
         valDescripcion = texto
         LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
         texto = ""
         texto = gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
         ExisteLineaDeProducto = fEncontraLineaDeProductoEnArreglo(mLineaDeProducto, texto, mConsecutivoCompaniaActual)
         If Not ExisteLineaDeProducto And texto <> "" Then
            Dim vConsecutivoLinea As Integer
            vConsecutivoLinea = insLineaDeProducto.getNextConsecutivo(mConsecutivoCompaniaActual)
            insLineaDeProducto.sInsertaLineaDeProductoDesdeExportarImportar mConsecutivoCompaniaActual, vConsecutivoLinea, texto, 0, "", mNombreDelUsuario, gUtilDate.getFechaDeHoy
            sLlenarArregloConLineaDeProducto mLineaDeProducto
         End If
         .SetLineaDeProducto gTexto.DfMid(texto, 1, 20)
         LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
         texto = ""
         texto = gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
         .SetStatusdelArticuloStr texto
         LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
         texto = ""
         texto = gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
         Select Case UCase(texto)
            Case UCase(gEnumProyecto.enumTipoDeArticuloToString(eTD_MERCANCIA)), UCase(CM_MERCANCIA)
               .SetTipoDeArticuloStr gEnumProyecto.enumTipoDeArticuloToString(eTD_MERCANCIA)
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            Case UCase(gEnumProyecto.enumTipoDeArticuloToString(eTD_SERVICIO))
               .SetTipoDeArticuloStr gEnumProyecto.enumTipoDeArticuloToString(eTD_SERVICIO)
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            Case UCase(gEnumProyecto.enumTipoDeArticuloToString(eTD_PRODUCTO_COMPUESTO))
               ErrorAlTraducirLaLinea = True
               texto = " ERROR Tipo de Articulo: * No se pueden importar Productos Compuestos * "
               LineaDeErrores = LineaDeErrores & texto
            Case Else
               ErrorAlTraducirLaLinea = True
               texto = " ERROR Tipo de Articulo:* " & texto & "*"
               LineaDeErrores = LineaDeErrores & texto
         End Select
         texto = ""
         texto = gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
         Select Case UCase(texto)
            Case UCase(gEnumProyecto.enumTipoDeAlicuotaToString(eTD_EXENTO))
               .SetAlicuotaIVAStr gEnumProyecto.enumTipoDeAlicuotaToString(eTD_EXENTO)
               valAlicuota = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_EXENTO)
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            Case UCase(gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTAGENERAL)), UCase(CM_ALICUOTA_GENERAL)
               .SetAlicuotaIVAStr gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTAGENERAL)
               valAlicuota = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTAGENERAL)
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            Case UCase(gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTA2))
               If Not gProyParametrosCompania.GetUsaMultiplesAlicuotas Then
                    mCambiarAUsaMultiplesAlicuotas = True
               End If
               .SetAlicuotaIVAStr gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTA2)
               valAlicuota = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTA2)
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            Case UCase(gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTA3))
               If Not gProyParametrosCompania.GetUsaMultiplesAlicuotas Then
                  mCambiarAUsaMultiplesAlicuotas = True
               End If
               .SetAlicuotaIVAStr gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTA3)
               valAlicuota = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTA3)
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            Case Else
               .SetAlicuotaIVAStr gEnumProyecto.enumTipoDeAlicuotaToString(eTD_EXENTO)
               valAlicuota = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_EXENTO)
               ErrorAlTraducirLaLinea = True
               texto = " ERROR Alícuota de IVA :* " & texto & "*"
               LineaDeErrores = LineaDeErrores & texto
         End Select
         texto = fValidaLamoneda(gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion))
         valMoneda = UCase(fSustituirSiEsNecesario(texto))
         If mCodigoMonedaLocal = valMoneda Then
            texto = ""
            insCamposMonedaExtranjera.SetCodigo valCodigo
            sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioSinIVA, "Precio Sin IVA"
            LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            texto = ""
            sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioConIVA, "Precio Con IVA"
            LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            valPrecioSinIVA = fDevolveElPrecioSinIVA(valPrecioConIVA, valAlicuota, valPrecioSinIVA)
            valPrecioConIVA = fDevolveElPrecioConIVA(valPrecioSinIVA, valAlicuota, valPrecioConIVA)
            .SetPrecioSinIVA valPrecioSinIVA
            .SetPrecioConIVA valPrecioConIVA
            texto = ""
            sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioSinIva2, "Precio Sin IVA2"
            LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            texto = ""
            sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioConIva2, "Precio Con IVA2"
            LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            valPrecioSinIva2 = fDevolveElPrecioSinIVA(valPrecioConIva2, valAlicuota, valPrecioSinIva2)
            valPrecioConIva2 = fDevolveElPrecioConIVA(valPrecioSinIva2, valAlicuota, valPrecioConIva2)
            .SetPrecioSinIVA2 valPrecioSinIva2
            .SetPrecioConIVA2 valPrecioConIva2
            texto = ""
            sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioSinIva3, "Precio Sin IVA3"
            LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            texto = ""
            sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioConIva3, "Precio Con IVA3"
            LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            valPrecioSinIva3 = fDevolveElPrecioSinIVA(valPrecioConIva3, valAlicuota, valPrecioSinIva3)
            valPrecioConIva3 = fDevolveElPrecioConIVA(valPrecioSinIva3, valAlicuota, valPrecioConIva3)
            .SetPrecioSinIVA3 valPrecioSinIva3
            .SetPrecioConIVA3 valPrecioConIva3
            texto = ""
            sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioSinIva4, "Precio Sin IVA4"
            LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            texto = ""
            sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioConIva4, "Precio Con IVA4"
            LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            valPrecioSinIva4 = fDevolveElPrecioSinIVA(valPrecioConIva4, valAlicuota, valPrecioSinIva4)
            valPrecioConIva4 = fDevolveElPrecioConIVA(valPrecioSinIva4, valAlicuota, valPrecioConIva4)
            .SetPrecioSinIVA4 valPrecioSinIva4
            .SetPrecioConIVA4 valPrecioConIva4
            texto = ""
            sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
            texto = ""
            sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
            texto = ""
            sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
            texto = ""
            sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
            texto = ""
            sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
            texto = ""
            sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
            texto = ""
            sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
            texto = ""
            sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
         Else
            If gProyParametrosCompania.GetUsaMonedaExtranjera Then
               texto = ""
               insCamposMonedaExtranjera.SetCodigo valCodigo
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioSinIVA, "Precio Sin IVA"
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioConIVA, "Precio Con IVA"
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
               valPrecioSinIVA = fDevolveElPrecioSinIVA(valPrecioConIVA, valAlicuota, valPrecioSinIVA)
               valPrecioConIVA = fDevolveElPrecioConIVA(valPrecioSinIVA, valAlicuota, valPrecioConIVA)
               .SetPrecioSinIVA valPrecioSinIVA
               .SetPrecioConIVA valPrecioConIVA
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioSinIva2, "Precio Sin IVA2"
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioConIva2, "Precio Con IVA2"
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
               valPrecioSinIva2 = fDevolveElPrecioSinIVA(valPrecioConIva2, valAlicuota, valPrecioSinIva2)
               valPrecioConIva2 = fDevolveElPrecioConIVA(valPrecioSinIva2, valAlicuota, valPrecioConIva2)
               .SetPrecioSinIVA2 valPrecioSinIva2
               .SetPrecioConIVA2 valPrecioConIva2
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioSinIva3, "Precio Sin IVA3"
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioConIva3, "Precio Con IVA3"
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
               valPrecioSinIva3 = fDevolveElPrecioSinIVA(valPrecioConIva3, valAlicuota, valPrecioSinIva3)
               valPrecioConIva3 = fDevolveElPrecioConIVA(valPrecioSinIva3, valAlicuota, valPrecioConIva3)
               .SetPrecioSinIVA3 valPrecioSinIva3
               .SetPrecioConIVA3 valPrecioConIva3
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioSinIva4, "Precio Sin IVA4"
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioConIva4, "Precio Con IVA4"
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
               valPrecioSinIva4 = fDevolveElPrecioSinIVA(valPrecioConIva4, valAlicuota, valPrecioSinIva4)
               valPrecioConIva4 = fDevolveElPrecioConIVA(valPrecioSinIva4, valAlicuota, valPrecioConIva4)
               .SetPrecioSinIVA4 valPrecioSinIva4
               .SetPrecioConIVA4 valPrecioConIva4
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioSinIVA, "Precio sin IVA en moneda extranjera"
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioConIVA, "Precio Con IVA en moneda extranjera"
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
               valPrecioSinIVA = fDevolveElPrecioSinIVA(valPrecioConIVA, valAlicuota, valPrecioSinIVA)
               valPrecioConIVA = fDevolveElPrecioConIVA(valPrecioSinIVA, valAlicuota, valPrecioConIVA)
               insCamposMonedaExtranjera.SetMePrecioSinIVA valPrecioSinIVA
               insCamposMonedaExtranjera.SetMePrecioConIVA valPrecioConIVA
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioSinIva2, "Precio sin IVA2 en moneda extranjera"
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioConIva2, "Precio con IVA2 en moneda extranjera"
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
               valPrecioSinIva2 = fDevolveElPrecioSinIVA(valPrecioConIva2, valAlicuota, valPrecioSinIva2)
               valPrecioConIva2 = fDevolveElPrecioConIVA(valPrecioSinIva2, valAlicuota, valPrecioConIva2)
               insCamposMonedaExtranjera.SetMePrecioSinIVA2 valPrecioSinIva2
               insCamposMonedaExtranjera.SetMePrecioConIVA2 valPrecioConIva2
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioSinIva3, "Precio sin IVA3 en moneda extranjera"
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioConIva3, "Precio con IVA3 en moneda extranjera"
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
               valPrecioSinIva3 = fDevolveElPrecioSinIVA(valPrecioConIva3, valAlicuota, valPrecioSinIva3)
               valPrecioConIva3 = fDevolveElPrecioConIVA(valPrecioSinIva3, valAlicuota, valPrecioConIva3)
               insCamposMonedaExtranjera.SetMePrecioSinIVA3 valPrecioSinIva3
               insCamposMonedaExtranjera.SetMePrecioConIVA3 valPrecioConIva3
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioSinIva4, "Precio sin IVA4 en moneda extranjera"
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPrecioConIva4, "Precio con IVA2 en moneda extranjera"
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
               valPrecioSinIva4 = fDevolveElPrecioSinIVA(valPrecioConIva4, valAlicuota, valPrecioSinIva4)
               valPrecioConIva4 = fDevolveElPrecioConIVA(valPrecioSinIva4, valAlicuota, valPrecioConIva4)
               insCamposMonedaExtranjera.SetMePrecioSinIVA4 valPrecioSinIva4
               insCamposMonedaExtranjera.SetMePrecioConIVA4 valPrecioConIva4
            Else
               ErrorAlTraducirLaLinea = True
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
               texto = ""
               sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valMontoBlanco, ""
               texto = " ERROR: " & texto & "*"
               texto = " ERROR: **La Compañia no Usa moneda Extranjera y Coloco una moneda Distinta a la local *** "
               LineaDeErrores = LineaDeErrores & texto
            End If
         End If
         texto = ""
         sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valPorcentajeBase, "Porcentaje Base Imponible"
         .SetPorcentajeBaseImponible valPorcentajeBase
         texto = ""
         sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valCostoUnitario, "Costo unitario"
         .SetCostoUnitario valCostoUnitario
         If valCostoUnitario > 0 Then
            .SetMargenGanancia ((valPrecioSinIVA / valCostoUnitario * 100) - 100)
            .SetMargenGanancia2 ((valPrecioSinIva2 / valCostoUnitario * 100) - 100)
            .SetMargenGanancia3 ((valPrecioSinIva3 / valCostoUnitario * 100) - 100)
            .SetMargenGanancia4 ((valPrecioSinIva4 / valCostoUnitario * 100) - 100)
         End If
         texto = ""
         sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, linea, valExistencia, "Existencia"
         If .GetTipoDeArticuloStr = gEnumProyecto.enumTipoDeArticuloToString(eTD_SERVICIO) Then
            .SetExistencia 0
         Else
            .SetExistencia valExistencia
         End If
         .SetCantidadMinima gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion))
         .SetCantidadMaxima gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion))
         If Not insCategoriaNav.fSearchByField("Descripcion", "CATEGORIA") Then
            insCategoriaNav.sInsertarValorporDefecto mConsecutivoCompaniaActual
            insCategoriaNav.fSearchByField "Descripcion", "CATEGORIA"
         End If
         .SetCategoria insCategoriaNav.GetDescripcion
         .SetConsecutivoCategoria insCategoriaNav.GetConsecutivo
         .SetTipoDeProductoStr gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
         .SetNombrePrograma gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
         .SetMarca gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
         .SetFechaDeVencimiento gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion))
         texto = ""
         texto = gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
         If texto = "" Then
            ErrorAlTraducirLaLinea = True
            texto = " ERROR UNIDADES DE VENTA EN BLANCO* " & texto & "*"
            LineaDeErrores = LineaDeErrores & texto
         Else
            Set mrsUnidad = New ADODB.Recordset
            SQL = "SELECT Nombre FROM Saw.UnidadDeVenta WHERE Nombre = " & gUtilSQL.fSimpleSqlValue(texto)
            If gDbUtil.fOpenRecordSetAllParameters(mrsUnidad, SQL, gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
               If mrsUnidad.RecordCount <= 0 Then
                  SQL = "INSERT INTO Saw.UnidadDeVenta (Nombre, NombreOperador, FechaUltimaModificacion) VALUES (" & gUtilSQL.fSimpleSqlValue(gTexto.DfMid(texto, 1, 20)) & ", '" & mNombreDelUsuario & "', " & gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy) & ")"
                  gDbUtil.Execute gDefDatabase.Conexion, SQL
                  .SetUnidadDeVenta gTexto.DfMid(texto, 1, 20)
               Else
                  .SetUnidadDeVenta gTexto.DfMid(texto, 1, 20)
               End If
            End If
            Set mrsUnidad = Nothing
         End If
        .SetCampoDefinible1 gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
        .SetCampoDefinible2 gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
        .SetCampoDefinible3 gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
        .SetCampoDefinible4 gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
        .SetCampoDefinible5 gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
        .SetMeCostoUnitario gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion))
        .SetUnidadDeVentaSecundaria gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion))
        .SetCuentaContableIngreso gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
        .SetCodigoLote mCodigoLote
        .SetPorcentajeComision gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion))
        .SetExcluirDeComision gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion))
        .SetCantArtReservado gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion))
        .SetCodigoGrupo gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
         texto = ""
         texto = gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
         Select Case (texto)
            Case gConvert.enumerativoAChar(eTAI_Simple)
               .SetTipoArticuloInvStr gEnumProyecto.enumTipoArticuloInvToString(eTAI_Simple)
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            Case gConvert.enumerativoAChar(eTAI_UsaSerial)
               .SetTipoArticuloInvStr gEnumProyecto.enumTipoArticuloInvToString(eTAI_UsaSerial)
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            Case gConvert.enumerativoAChar(eTAI_UsaSerial_Rollo)
               .SetTipoArticuloInvStr gEnumProyecto.enumTipoArticuloInvToString(eTAI_UsaSerial_Rollo)
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            Case gConvert.enumerativoAChar(eTAI_UsaTalla_Color)
               .SetTipoArticuloInvStr gEnumProyecto.enumTipoArticuloInvToString(eTAI_UsaTalla_Color)
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
            Case gConvert.enumerativoAChar(eTAI_UsaTalla_ColorySerial)
               .SetTipoArticuloInvStr gEnumProyecto.enumTipoArticuloInvToString(eTAI_UsaTalla_ColorySerial)
               LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
           Case Else
               ErrorAlTraducirLaLinea = True
               texto = " ERROR Tipo de Articulo:* " & texto & "*"
               LineaDeErrores = LineaDeErrores & texto
         End Select
        .SetUsaBalanza gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion))
        .SetNombreOperador mNombreDelUsuario
        .SetFechaUltimaModificacion gUtilDate.getFechaDeHoy
      End If
      If Not fDistribuyeLaLineaDeTallasyColores(linea, valLineaActual) Then
         ErrorAlTraducirLaLinea = True
         texto = " ERROR AL IMPORTAR ARTICULO DE TALLAS Y COLORES CON CODIGO" & .GetCodigo & "*"
         LineaDeErrores = LineaDeErrores & texto
      End If
      If Not fDistribuyeLaLineaDeAlmacen(linea, valLineaActual) Then
         ErrorAlTraducirLaLinea = True
         texto = " NO EXISTE CLIENTE GENERICO, ERROR INTENTAR CREAR ALMACEN del ARTICULO " & .GetCodigo & "*"
         LineaDeErrores = LineaDeErrores & texto
      End If
      If Not (vEsResumenDiario) Then
         texto = gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
         If gTexto.DfLen(texto) = 0 Then
            If Not IsNumeric(texto) Then
               texto = "0"
            End If
         End If
         .SetPeso gUtilImport.fToCurrency(texto)
         texto = gTexto.fLimpiaStringdeBlancosAAmbosLados(gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion))
         If gTexto.DfLen(texto) = 0 Then
            If Not gGlobalization.fEsCodigoPeru Then
               texto = "000"
            Else
               texto = "0000.00.00.00"
            End If
            .SetCodigoArancel texto
         Else
            Dim mrsArancel As ADODB.Recordset
            Set mrsArancel = New ADODB.Recordset
            SQL = "SELECT Codigo FROM Comun.Aranceles WHERE Codigo = " & gUtilSQL.fSimpleSqlValue(texto)
            If gDbUtil.fOpenRecordSetAllParameters(mrsArancel, SQL, gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
               If gDbUtil.fRecordCount(mrsArancel) > 0 Then
                  .SetCodigoArancel texto
               Else
                  If gGlobalization.fEsCodigoPeru Then
                     texto = "0000.00.00.00"
                  Else
                     texto = "000"
                  End If
                  .SetCodigoArancel texto
               End If
            End If
            gDbUtil.sDestroyRecordSet mrsArancel
            Set mrsArancel = Nothing
         End If
      End If
      texto = gUtilImport.fReadValueUntilDelimiterAndCut(linea, tipoDeSeparacion)
      Select Case (texto)
         Case gConvert.enumerativoAChar(eTDM_NoAplica)
            .SetTipoDeMercanciaProduccionStr gEnumProyecto.enumTipoDeMercanciaToString(eTDM_NoAplica)
            LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
         Case gConvert.enumerativoAChar(eTDM_MateriaPrima)
            .SetTipoDeMercanciaProduccionStr gEnumProyecto.enumTipoDeMercanciaToString(eTDM_MateriaPrima)
            LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
         Case gConvert.enumerativoAChar(eTDM_ProductoTerminado)
            .SetTipoDeMercanciaProduccionStr gEnumProyecto.enumTipoDeMercanciaToString(eTDM_ProductoTerminado)
            LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
         Case Else
            ErrorAlTraducirLaLinea = True
            texto = " ERROR Tipo de Mercancia Producción:* " & texto & "*"
            LineaDeErrores = LineaDeErrores & texto
      End Select
   End With
   refLineaActual = linea
h_EXIT: On Error GoTo 0
   fDistribuyeLaLineaDeArticulo = Not ErrorAlTraducirLaLinea
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fDistribuyeLaLineaDeArticulo", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sDesglosaNumeroDouble(ByRef texto As String, ByRef ErrorAlTraducirLinea As Boolean, ByRef Lin As String, ByRef Numero As Currency, ByVal valNombreDeCol As String)
   On Error GoTo h_ERROR
   Numero = 0
   If Lin = "" Then
      LineaDeErrores = LineaDeErrores & "* -Falta Monto-" & valNombreDeCol
      ErrorAlTraducirLinea = True
   Else
      texto = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(Lin, tipoDeSeparacion))
      If gTexto.fHayCaracteresEnNumeroTexto(texto) Then
        ErrorAlTraducirLinea = True
        LineaDeErrores = LineaDeErrores & "*-Monto Inválido-* " & valNombreDeCol
        texto = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(Lin, tipoDeSeparacion))
      Else
        If gTexto.DfInStr(texto, ".") <> 0 And gTexto.DfInStr(texto, ",") <> 0 Then
            ErrorAlTraducirLinea = True
            LineaDeErrores = LineaDeErrores & "*-Monto Inválido Formato-Tiene coma y punto a las vez-* " & valNombreDeCol & ":" & texto
        Else
            LineaDeErrores = LineaDeErrores & String(Len(texto), " ")
        End If
      End If
      Numero = gConvert.ConvierteAdouble(texto)
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sDesglosaNumeroDouble", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fDevolveElPrecioSinIVA(ByVal valPrecioConIVA As Currency, ByVal valAlicuota As String, ByVal valPrecioSinIVA As Currency) As Currency
   Dim valPrecio As Currency
   On Error GoTo h_ERROR
   valPrecio = valPrecioSinIVA
   If valAlicuota <> gEnumProyecto.enumTipoDeAlicuotaToString(eTD_EXENTO) And Not gProyParametrosCompania.GetUsaPrecioSinIvaSegunTipoDocumento(eTF_FACTURA) Then
      If valPrecioSinIVA > 0 Then
         mMensajePrecioSinIva = True
      End If
      valPrecio = insArticuloInventario.fCalculaElPrecioSinIVAAPartirDelPrecioConIVA(valPrecioConIVA, 100, gDefGen.fGetValorDeFechaParaInicializarCampo, gEnumProyecto.strTipoDeAlicuotaToNum(valAlicuota))
   End If
h_EXIT: fDevolveElPrecioSinIVA = valPrecio
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fDevolveElPrecioSinIVA", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDevolveElPrecioConIVA(ByVal valPrecioSinIVA As Currency, ByVal valAlicuota As String, ByVal valPrecioConIVA As Currency) As Currency
   Dim valPrecio As Currency
   On Error GoTo h_ERROR
   valPrecio = valPrecioConIVA
   If valAlicuota <> gEnumProyecto.enumTipoDeAlicuotaToString(eTD_EXENTO) And gProyParametrosCompania.GetUsaPrecioSinIvaSegunTipoDocumento(eTF_FACTURA) Then
      If valPrecioConIVA > 0 Then
         mMensajePrecioConIva = True
       End If
      valPrecio = insArticuloInventario.fCalculaElPrecioConIVAAPartirDelPrecioSinIVA(valPrecioSinIVA, 100, gDefGen.fGetValorDeFechaParaInicializarCampo, gEnumProyecto.strTipoDeAlicuotaToNum(valAlicuota))
   Else
      If valAlicuota = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_EXENTO) Then
         valPrecio = valPrecioSinIVA
      End If
   End If
h_EXIT: fDevolveElPrecioConIVA = valPrecio
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fDevolveElPrecioConIVA", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fMensajeDeConfirmacion(ByVal valMensajePrecioConIva As Boolean, ByVal valMensajePrecioSinIva As Boolean) As Boolean
   Dim Continuar As Boolean
   Dim valMensaje As String
   On Error GoTo h_ERROR
   If mModuloAImportar = eEAEI_ARTICULO_INVENTARIO Then
      valMensaje = ""
      Continuar = True
      If valMensajePrecioConIva Then
         valMensaje = "La compañia usa el precio sin Iva y el Archivo "
         valMensaje = valMensaje & " tiene información en la columna precio con Iva este es " & vbCrLf
         valMensaje = valMensaje & " Cálculado por el sistema ¿Desea continuar con el proceso?"
         Continuar = gMessage.Confirm(valMensaje)
      End If
      If valMensajePrecioSinIva Then
         valMensaje = "La compañia usa el precio con Iva y el Archivo "
         valMensaje = valMensaje & " tiene información en la columna precio sin Iva este es " & vbCrLf
         valMensaje = valMensaje & " Cálculado por el sistema ¿Desea continuar con el proceso?"
         Continuar = gMessage.Confirm(valMensaje)
       End If
   Else
      Continuar = True
   End If
h_EXIT: fMensajeDeConfirmacion = Continuar
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fMensajeDeConfirmacion", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSearchParamByCompany() As Boolean
   Dim vRs As ADODB.Recordset
   Dim vSql  As String
   Dim vSN As Boolean
   On Error GoTo h_ERROR
   Set vRs = New ADODB.Recordset
   vSql = "SELECT Value FROM comun.SettValueByCompany WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual & " AND NameSettDefinition = " & gUtilSQL.fSimpleSqlValue("UsaMultiplesAlicuotas")
   If gDbUtil.fOpenRecordset(vRs, vSql, Conexion) Then
      If gDbUtil.fRecordCount(vRs) > 0 Then
         vSN = (gConvert.ConvertStringToBoolean(vRs("Value").Value) = False)
      End If
   End If
   vRs.Close
   gDbUtil.sDestroyRecordSet vRs
h_EXIT: fSearchParamByCompany = vSN
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fSearchParamByCompany", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fContinuarConAlicuotasDelArchivo() As Boolean
   Dim valMensaje As String
   Dim Continuar As Boolean
   Dim vSql  As String
   On Error GoTo h_ERROR
   If mModuloAImportar = eEAEI_ARTICULO_INVENTARIO Then
      Continuar = True
      If mCambiarAUsaMultiplesAlicuotas Then
         Continuar = False
         valMensaje = "La compañia No usa Multiples Alicuotas y el Archivo tiene información  diferente a Alícuota General. " & vbCrLf
         valMensaje = valMensaje & "**SI DESEA CONTINUAR SE CAMBIARA ESTA OPCIÓN A - USAR MULTIPLES ALICUOTAS -** " & vbCrLf
         valMensaje = valMensaje & "¿Desea continuar con el proceso?"
         Continuar = gMessage.Confirm(valMensaje)
         If fSearchParamByCompany And Continuar Then
            vSql = "UPDATE comun.SettValueByCompany SET Value = " & gUtilSQL.fBooleanToStrSQL(True) & " WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual & " AND NameSettDefinition  = " & gUtilSQL.fSimpleSqlValue("UsaMultiplesAlicuotas")
            gDbUtil.Execute gDefDatabase.Conexion, vSql
            Continuar = True
         End If
      End If
   Else
       Continuar = True
   End If
h_EXIT: fContinuarConAlicuotasDelArchivo = Continuar
  On Error GoTo 0
  Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fContinuarConAlicuotasDelArchivo", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sLlenarArregloConLineaDeProducto(ByRef RefLineaDeProducto As Variant)
   Dim SQL As String
   Dim rsLineaDeProducto As ADODB.Recordset
   On Error GoTo h_ERROR
   Set rsLineaDeProducto = New ADODB.Recordset
   SQL = "SELECT lineaDeProducto.Nombre, lineaDeProducto.ConsecutivoCompania"
   SQL = SQL & " FROM lineaDeProducto WHERE lineaDeProducto.ConsecutivoCompania = " & mConsecutivoCompaniaActual
   If gDbUtil.fOpenRecordset(rsLineaDeProducto, SQL, Conexion) Then
      If Not rsLineaDeProducto.EOF Then
         RefLineaDeProducto = rsLineaDeProducto.GetRows
      End If
   End If
   gDbUtil.sDestroyRecordSet rsLineaDeProducto
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sLlenarArregloConLineaDeProducto", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fEncontraLineaDeProductoEnArreglo(ByVal valLineaDeProducto As Variant, ByVal valRegistro As String, ByVal valConsecutivoCompania As Long) As Boolean
   Dim valregistroActual As Long
   Dim ValNumeroDeRegistro As Long
   Dim Encontro As Boolean
   On Error GoTo h_ERROR
   ValNumeroDeRegistro = UBound(valLineaDeProducto, 2) + 1
   Encontro = False
   For valregistroActual = 0 To ValNumeroDeRegistro - 1
      If UCase(valLineaDeProducto(REGISTRO_LINEA_PRODUCTO, valregistroActual)) = UCase(valRegistro) And valLineaDeProducto(CONSECUTIVO_COMPANIA, valregistroActual) = valConsecutivoCompania Then
         Encontro = True
         Exit For
      End If
   Next valregistroActual
h_EXIT:
   fEncontraLineaDeProductoEnArreglo = Encontro
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fEncontraLineaDeProductoEnArreglo", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fEncontrarArticuloEnArchivo(ByRef refArticulosEnElArchivo As Variant, ByVal valCantidadActual As Long, ByVal valRegistro As String) As Boolean
   Dim valregistroActual As Long
   Dim ValNumeroDeRegistro As Long
   Dim Encontro As Boolean
   On Error GoTo h_ERROR
   ValNumeroDeRegistro = valCantidadActual
   Encontro = False
   For valregistroActual = 0 To ValNumeroDeRegistro - 1
      If UCase(refArticulosEnElArchivo(CODIGO_ARTICULO, valregistroActual)) = UCase(valRegistro) Then
         Encontro = True
         Exit For
      End If
   Next valregistroActual
h_EXIT:
  fEncontrarArticuloEnArchivo = Encontro
  On Error GoTo 0
  Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fEncontrarArticuloEnArchivo", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDistribuyeLaLineaDeTallasyColores(ByRef refLineaActual As String, ByVal valLineaActual As Long) As Boolean
   On Error GoTo h_ERROR
   fDistribuyeLaLineaDeTallasyColores = False
   Dim Continuar As Boolean
   Continuar = True
   'SE DISTRIBUYEN LOS CAMPOS DE LA TABLA COLOR'
   insColor.sInicializarValores mConsecutivoCompaniaActual, gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion), gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion), mCodigoLote
   'SE DISTRIBUYEN LOS CAMPOS DE LA TABLA TALLA'
   insTalla.sInicializarValores mConsecutivoCompaniaActual, gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion), gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion), mCodigoLote
   With insGrupoTallaColor  'SE DISTRIBUYEN LOS CAMPOS DE LA TABLA GRUPOTALLACOLOR
      .SetConsecutivoCompania mConsecutivoCompaniaActual
      .SetCodigoGrupo gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
      .SetDescripcion gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
      .SetUsaTalla gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion))
      .SetUsaColor gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion))
      .SetNombreOperador mNombreDelUsuario
      .SetFechaUltimaModificacion gUtilDate.getFechaDeHoy
      .SetCodigoLote mCodigoLote
   End With
   With insRenglonGrupoColor  'SE DISTRIBUYEN LOS CAMPOS DE LA TABLA RENGLONGRUPOCOLOR
      .SetConsecutivoCompania mConsecutivoCompaniaActual
      .SetCodigoGrupo gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
      .SetCodigoColor gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
   End With
   With insRenglonGrupoTalla  'SE DISTRIBUYEN LOS CAMPOS DE LA TABLA RENGLONGRUPOTALLA
      .SetConsecutivoCompania mConsecutivoCompaniaActual
      .SetCodigoGrupo gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
      .SetCodigoTalla gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
   End With
   With insExistenciaPorGrupo 'SE DISTRIBUYEN LOS CAMPOS DE LA TABLA EXISTENCIAPORGRUPO
      .SetConsecutivoCompania mConsecutivoCompaniaActual
      .SetCodigoArticulo gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
      .SetCodigoGrupo gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
      .SetCodigoTalla gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
      .SetCodigoColor gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
      .SetExistencia gUtilImport.fToLong(gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion))
      .SetSerial gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
      .SetRollo gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
      .SetCantReservada gUtilImport.fToLong(gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion))
      .SetFecha gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion))
      .SetUbicacion gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
      .SetCodigoLote mCodigoLote
   End With
   If Not Continuar Then
      fDistribuyeLaLineaDeTallasyColores = False
      Exit Function
   End If
h_EXIT: On Error GoTo 0
   fDistribuyeLaLineaDeTallasyColores = True
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fDistribuyeLaLineaDeTallasyColores", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDistribuyeLaLineaDeAlmacen(ByRef refLineaActual As String, ByVal valLineaActual As Long) As Boolean
   On Error GoTo h_ERROR
   fDistribuyeLaLineaDeAlmacen = False
   Dim texto As String
   Dim ErrorAlTraducirLaLinea As Boolean
   Dim Continuar As Boolean
   Dim ExistenciaAlmacen As Long
   Dim TipoDeAlmacen As String
   Continuar = True
   ErrorAlTraducirLaLinea = False
   'SE DISTRIBUYEN LOS CAMPOS DE LA TABLA ALMACEN
   texto = ""
   valCodigoAlmacenExtra = gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
   sDesglosaNumeroDouble texto, ErrorAlTraducirLaLinea, refLineaActual, valExistenciaAlmacenExtra, "ExistenciaAlmacen"
   valCodigoArticulo = gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
   valNombreAlmacenExtra = gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
   TipoDeAlmacen = gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
   valTipoDeAlmacenExtra = gConvert.fConvierteAString(TipoDeAlmacen)
   valCodigoDelClienteExtra = gProyParametrosCompania.getCodigoGenericoCliente
   valCodigoCcExtra = gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
   valDescripcionExtra = gUtilImport.fReadValueUntilDelimiterAndCut(refLineaActual, tipoDeSeparacion)
   If ErrorAlTraducirLaLinea Then
      Continuar = False
      Exit Function
   End If
h_EXIT: On Error GoTo 0
    fDistribuyeLaLineaDeAlmacen = Continuar
    Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fDistribuyeLaLineaDeAlmacen", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fAgregaTallasyColores() As Boolean
   Dim Continuar As Boolean
   On Error GoTo h_ERROR
   Continuar = False
   If insTalla.GetCodigoTalla <> "" Then 'AGREGA TALLA
      If Not insTalla.fSearchByField("CodigoTalla", insTalla.GetCodigoTalla) Then
         insTalla.sInsertarValoresDesdeImportacion mCodigoLote
         Continuar = True
      End If
   End If
   If insColor.GetCodigoColor <> "" Then 'AGREGA COLOR
      If Not insColor.fSearchByField("CodigoColor", insColor.GetCodigoColor) Then
         insColor.sInsertarValoresDesdeImportacion mCodigoLote
         Continuar = True
      End If
   End If
   If insGrupoTallaColor.getCodigoGrupo <> "" Then 'AGREGA GRUPO TALLA Y COLOR
      If Not insRenglonGrupoTalla.fSearchByFieldGrupoYTalla(insRenglonGrupoTalla.getCodigoGrupo, insRenglonGrupoTalla.GetCodigoTalla, False) _
         Or Not insRenglonGrupoColor.fSearchByFieldGrupoYColor(insRenglonGrupoColor.getCodigoGrupo, insRenglonGrupoColor.GetCodigoColor, False) _
         Then
         If ContinuaDespuesDeVerificarExistenciaTallaColor Then
            If insGrupoTallaColor.fInsert(insertar, True, False) Then
                If fAgregaElDetalleTallaColor Then
                   Continuar = True
                End If
            End If
         Else
            If fAgregaElDetalleTallaColor Then
                Continuar = True
            End If
         End If
      End If
      Continuar = True
   End If
   If insExistenciaPorGrupo.getCodigoArticulo <> "" Then 'AGREGA existencia * GRUPO
     If ContinuaDespuesDeVerificarExistenciaEnExistenciaxGrupo Then
        If insExistenciaPorGrupo.fInsert Then
           Continuar = True
        End If
     End If
   End If
h_EXIT: fAgregaTallasyColores = Continuar
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fAgregaTallasyColores", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fAgregaAlmacen() As Boolean
   Dim refCodigo As String
   Dim refNombreAlmacen As String
   Dim Rs As ADODB.Recordset
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vSql As String
   Dim xConsecutivoAlmacen As Long
   Dim vConsecutivoCliente As Long
   Dim Continuar As Boolean
   On Error GoTo h_ERROR
   Continuar = True
   If valCodigoAlmacenExtra <> "" Then   'AGREGA ALMACEN
      If Not fSelectAndSetValuesOfAlmacenFromAOS(refCodigo, refNombreAlmacen, valCodigoAlmacenExtra, "", xConsecutivoAlmacen) Then
         Set Rs = New ADODB.Recordset
         vSql = "SELECT Consecutivo FROM Cliente WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual & " AND Codigo =" & gUtilSQL.fSimpleSqlValue(valCodigoDelClienteExtra)
         If gDbUtil.fOpenRecordset(Rs, vSql, Conexion) Then
            If Rs.RecordCount > 0 Then
               vConsecutivoCliente = Rs("Consecutivo").Value
            Else
               Continuar = False
            End If
         End If
         gDbUtil.sDestroyRecordSet Rs
         If Continuar Then
            Set insUtAdoCmd = New clsUtilAdoCmd
            Set vCmd = insUtAdoCmd.fCreateCmd("Saw.Gp_AlmacenINS", gDefDatabase.Conexion)
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", mConsecutivoCompaniaActual)
            Set Rs = New ADODB.Recordset
            vSql = "SELECT (CASE WHEN MAX(Consecutivo) IS NULL THEN 1 ELSE MAX(Consecutivo) + 1 END) AS Consecutivo FROM Saw.Almacen WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual
            If gDbUtil.fOpenRecordset(Rs, vSql, Conexion) Then
               If Rs.RecordCount > 0 Then
                  valConsecutivoAlmacen = Rs("Consecutivo").Value
               Else
                  valConsecutivoAlmacen = 1
               End If
            End If
            gDbUtil.sDestroyRecordSet Rs
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", valConsecutivoAlmacen)
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Codigo", 5, valCodigoAlmacenExtra)
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreAlmacen", 40, valNombreAlmacenExtra)
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoDeAlmacen", 1, valTipoDeAlmacenExtra)
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCliente", vConsecutivoCliente)
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoCc", 5, valCodigoCcExtra)
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Descripcion", 40, valDescripcionExtra)
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 10, mNombreDelUsuario)
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", gUtilDate.getFechaDeHoy)
            insUtAdoCmd.fExecCmd vCmd
            Set vCmd = Nothing
         End If
      Else
         valConsecutivoAlmacen = xConsecutivoAlmacen
      End If
   End If
h_EXIT: fAgregaAlmacen = Continuar
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fAgregaAlmacen", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function ContinuaDespuesDeVerificarExistenciaTallaColor() As Boolean
   Dim Continuar As Boolean
   On Error GoTo h_ERROR
   Continuar = False
   If Not insGrupoTallaColor.fSearchByField("CodigoGrupo", insGrupoTallaColor.getCodigoGrupo, True) Then
      If insColor.fSearchByField("CodigoColor", insColor.GetCodigoColor) Then
         If insTalla.fSearchByField("CodigoTalla", insTalla.GetCodigoTalla) Then
            Continuar = True
         End If
      End If
   End If
h_EXIT: ContinuaDespuesDeVerificarExistenciaTallaColor = Continuar
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "ContinuaDespuesDeVerificarExistenciaTallaColor", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fAgregaElDetalleTallaColor() As Boolean
   Dim Continuar As Boolean
   On Error GoTo h_ERROR
   Continuar = False
   If insGrupoTallaColor.fSearchByField("CodigoGrupo", insGrupoTallaColor.getCodigoGrupo, True) Then
      If fAgregaElDetalleTalla Then
         Continuar = True
      End If
      If fAgregaElDetalleColor Then
         Continuar = True
      End If
   End If
h_EXIT: fAgregaElDetalleTallaColor = Continuar
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fAgregaElDetalleTallaColor", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fAgregaElDetalleTalla() As Boolean
   Dim Continuar As Boolean
   On Error GoTo h_ERROR
   Continuar = False
   vConsecutivoRenglon = insRenglonGrupoTalla.fGenerateNextConsecutivoRenglon(insGrupoTallaColor.getCodigoGrupo)
   insRenglonGrupoTalla.SetConsecutivoRenglon vConsecutivoRenglon
   If Not insRenglonGrupoTalla.fSearchByFieldGrupoYTalla(insRenglonGrupoTalla.getCodigoGrupo, insRenglonGrupoTalla.GetCodigoTalla, False) Then
      If insRenglonGrupoTalla.fInsert Then
          Continuar = True
      End If
    End If
h_EXIT: fAgregaElDetalleTalla = Continuar
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fAgregaElDetalleTalla", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fAgregaElDetalleColor() As Boolean
   Dim Continuar As Boolean
   On Error GoTo h_ERROR
   Continuar = False
   vConsecutivoRenglon = insRenglonGrupoColor.fGenerateNextConsecutivoRenglon(insGrupoTallaColor.getCodigoGrupo)
   insRenglonGrupoColor.SetConsecutivoRenglon vConsecutivoRenglon
   If Not insRenglonGrupoColor.fSearchByFieldGrupoYColor(insRenglonGrupoColor.getCodigoGrupo, insRenglonGrupoColor.GetCodigoColor, False) Then
      If insRenglonGrupoColor.fInsert Then
         Continuar = True
      End If
   End If
h_EXIT: fAgregaElDetalleColor = Continuar
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fAgregaElDetalleColor", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fContinuaConProcesosDeInsercionDeArticulos(ByVal valTotalRegistro As Long) As Boolean
    Dim Continuar As Boolean
    Dim valNumeroNotaEntrada As String
    On Error GoTo h_ERROR
    Continuar = insNotaDeEntrada.fInsertDesdeImportacionInventario(mConsecutivoCompaniaActual, gProyParametrosCompania.getCodigoGenericoCliente, gProyParametrosCompania.GetCodigoAlmacenGenerico, valDatosDeInventario, valTotalRegistro, mCodigoLote)
    If Continuar Then
       Continuar = insExistenciaNavigator.fInsertDesdeImportacionInventario(mConsecutivoCompaniaActual, valDatosDeInventario, valTotalRegistro)
    End If
h_EXIT: fContinuaConProcesosDeInsercionDeArticulos = Continuar
    On Error GoTo 0
    Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fContinuaConProcesosDeInsercionDeArticulos", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function ContinuaDespuesDeVerificarExistenciaEnExistenciaxGrupo() As Boolean
   Dim Continuar As Boolean
   On Error GoTo h_ERROR
   Continuar = True
   If insExistenciaPorGrupo.fSearchByFieldExistenciaGrupo(insExistenciaPorGrupo.getCodigoArticulo, insExistenciaPorGrupo.GetCodigoColor, insExistenciaPorGrupo.GetCodigoTalla, False) And insExistenciaPorGrupo.fSearchByFieldExistenciaGrupoSerialRollo(insExistenciaPorGrupo.getCodigoArticulo, insExistenciaPorGrupo.GetSerial, insExistenciaPorGrupo.GetRollo, False) Then
      Continuar = False
   End If
h_EXIT: ContinuaDespuesDeVerificarExistenciaEnExistenciaxGrupo = Continuar
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "ContinuaDespuesDeVerificarExistenciaEnExistenciaxGrupo", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'= *** <<< <<< PROCESOS COMUNES >>> >>> *** =
Public Sub sImportarModulosSaw(ByVal valNombreDelArchivo As String, ByVal valExtension As String, ByRef refProgressBar As ProgressBar, ByRef exito As Boolean, ByVal valEntidad As enum_EntidadAExportarOImportar, ByVal NumFilasVector As Integer, ByRef valLabelPorcentaje As Label, ByRef refCallingWindow As Form, Optional ByVal valEsFacturaBorradores As Boolean = False, Optional ByVal generarCxC As Boolean = False, Optional ByRef reftxtProgreso As TextBox)
   Dim Separador As String
   Dim FileErrorNumber As Integer
   Dim NumeroDelineas As Long
   Dim Continuar As Boolean
   On Error GoTo h_ERROR
   Set mCallingWindow = refCallingWindow
   Set insImportar = New clsImportar
   Set mProgressBar = refProgressBar
   Set mLabelPorcentaje = valLabelPorcentaje
   ReDim mVectorDeVerificaciones(0, 0)
   RegistrosYaExistentesEnBD = 0
   mNregistroAInsertar = 0
   mNumerodeRegistros = 0
   ErrorDeTraduccion = False
   Continuar = False
   mGenerarCxC = generarCxC
   mModuloAImportar = valEntidad
   NombreArchivoImportacion = valNombreDelArchivo
   mProgressBar.Visible = True
   mProgressBar.Value = 0
   mLabelPorcentaje.Visible = True
   mLabelPorcentaje.Caption = ""
   If gUtilFile.fExisteElArchivo(valNombreDelArchivo) Then
      reftxtProgreso.Text = "Verificando la data...."
      Separador = insImportar.fObtenerSeparador(valExtension)
      tipoDeSeparacion = Separador
      If insImportar.fAbrirFileForInput(FileNumberdeLectura, valNombreDelArchivo) Then
         NumeroDelineas = insImportar.fNumeroDelineasDelArchivo(FileNumberdeLectura)
         ReDim mVectorDeVerificaciones(NumFilasVector, NumeroDelineas)
         insImportar.sCierraElFile (FileNumberdeLectura)
         mNumerodeRegistros = NumeroDelineas
      End If
      If insImportar.fAbrirFileForInput(FileNumberdeLectura, valNombreDelArchivo) Then
         EnumSystemModules = fEntidadToEnumSystemModules
         If insImportar.fAbrirFileForOuputAndCreatePath(FileErrorNumber, insImportar.getPathDelFileDeErrores, insImportar.getFileDeErrores) Then
            mSeImportaComoHistorica = False
            If valEntidad = eEAEI_FACTURA And valEsFacturaBorradores Then
                If gProyParametrosCompania.GetUsaImprentaDigital Then
                    mSeImportaComoHistorica = False
                Else
                    mSeImportaComoHistorica = gMessage.YesNoMessage("¿Desea Importar los Borradores de Factura como Históricos?", "Importar Borradores Factura")
                End If
            End If
            sChequeaLaIntegridadDeLosDatosSaw FileErrorNumber, Separador, ErrorDeTraduccion, refProgressBar, valNombreDelArchivo, valEsFacturaBorradores
            insImportar.sCierraElFile FileErrorNumber
         End If
         insImportar.sCierraElFile FileNumberdeLectura
         refProgressBar.Max = NumeroDelineas
         If fMensajeDeConfirmacion(mMensajePrecioConIva, mMensajePrecioSinIva) And fContinuarConAlicuotasDelArchivo Then
             If ErrorDeTraduccion Then
                Continuar = insImportar.fDecideSiContinuaConLaImportacion(EnumSystemModules)
             Else
                Continuar = True
             End If
             If Continuar Then
                reftxtProgreso.Text = "Importando archivos...."
                sEjecutaLaImportacionDeModulosSaw FileNumberdeLectura, valNombreDelArchivo, Separador, refProgressBar, valEsFacturaBorradores
             End If
         Else
             Continuar = False
         End If
      End If
      exito = Continuar
   Else
      reftxtProgreso.Text = ""
      EnumSystemModules = fEntidadToEnumSystemModules
      gMessage.Advertencia "No existe el archivo del modulo de " & gEnumProyecto.enum_SystemModulesToStr(EnumSystemModules) & " que especificó en la ruta para la importación."
   End If
   If Not exito Then
      reftxtProgreso.Text = ""
      gMessage.Advertencia "No se realizó la importación."
   End If
   Set insImportar = Nothing
h_EXIT: On Error GoTo 0
    Exit Sub
h_ERROR: refProgressBar.Visible = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sImportarModulosSaw", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sChequeaLaIntegridadDeLosDatosSawSinDetail(ByVal FileErrorNumber As Integer, ByVal valSeparador As String, ByRef refErrorDeTraduccion As Boolean)
   Dim lineaActual As String
   Dim NumeroDeLineaActual As Long
   Dim NumerodeLinea As Integer
   On Error GoTo h_ERROR
   refErrorDeTraduccion = False
   NumerodeLinea = 0
   Do Until EOF(FileNumberdeLectura)
      Line Input #FileNumberdeLectura, lineaActual
      If lineaActual <> "" Then
         NumeroDeLineaActual = NumeroDeLineaActual + 1
         If Not fDistribuyeLaLineaModulosSaw(lineaActual, NumeroDeLineaActual, valSeparador) Then
            refErrorDeTraduccion = True
            NumerodeLinea = NumerodeLinea + 1
            Write #FileErrorNumber, "-----" & gConvert.fConvierteAString(NumerodeLinea) & "-----"
            Write #FileErrorNumber, lineaActual
            Write #FileErrorNumber, LineaDeErrores
         Else
            mNregistroAInsertar = mNregistroAInsertar + 1
         End If
         sMuestraProgreso True, mNumerodeRegistros, NumeroDeLineaActual
      End If
   Loop
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sChequeaLaIntegridadDeLosDatosSawSinDetail", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sChequeaLaIntegridadDeLosDatosConDetail(ByVal valSeparador As String, ByRef refProgressBar As ProgressBar, ByVal FileErrorNumber As Integer, Optional ByVal valEsFacturaBorradores As Boolean = False, Optional ByRef DebeImportar As Boolean = False)
   Dim lineaActual As String
   Dim NumeroDeLineaActual As Long
   Dim DebeModificarArray As Boolean
   Dim refErrorDeTraduccion As Boolean
   Dim valIndex1 As Integer
   Dim valIndex2 As Integer
   Dim valorDelIdMaster As String
   Dim valordelIdAnterior As String 'OJO
   Dim Contador As Long
   On Error GoTo h_ERROR
   DebeModificarArray = False
   NumeroDeLineaActual = 0
   NumerodeLinea = 0
   mYaIngresoUnoMas = False
   mImportoRegistros = False
   valorDelIdMaster = ""
   Contador = 0
   Do Until EOF(FileNumberdeLectura)
      Line Input #FileNumberdeLectura, lineaActual
      If lineaActual <> "" Then
         LineaDeErrores = ""
         mValorDelCampoDeId = gUtilImport.fReadValueUntilDelimiterAndCut(lineaActual, valSeparador)
         If mValorDelCampoDeId <> valorDelIdMaster Then
            valordelIdAnterior = valorDelIdMaster ' OJO
            valorDelIdMaster = mValorDelCampoDeId
            Contador = Contador + 1
         End If
         sDevuelveLosIndexDeCadaModulo valIndex1, valIndex2
         If DebeImportar Then
            If fPreparaLosDatosConDetalleEImporta(valSeparador, NumeroDeLineaActual, lineaActual, valIndex1, valIndex2, valEsFacturaBorradores) Then
               sMuestraProgreso True, mNumerodeRegistros, NumeroDeLineaActual
               mImportoRegistros = True
               If (valordelIdAnterior <> "") Then
                  If Not insCxC.fSearchByField("Numero", valordelIdAnterior, False, False) Then
                     If insFactura.fSearchByField("Numero", valordelIdAnterior, False, True, False, True) Then
                        If insFactura.GetStatusFacturaAsEnum = enum_StatusFactura.eSF_EMITIDA Then
                           If mGenerarCxC Then
                              insFactura.sGeneraCXC False
                           End If
                       End If
                     End If
                   End If
               End If
            End If
         Else
            refErrorDeTraduccion = False
            If fDistribuyeLaLineaModulosSaw(lineaActual, NumeroDeLineaActual, valSeparador) Then
              sVerificaSiHayErrorEnLaLinea NumeroDeLineaActual, refErrorDeTraduccion, DebeModificarArray, valEsFacturaBorradores
            End If
            If refErrorDeTraduccion = True Then
               NumerodeLinea = NumerodeLinea + 1
               Write #FileErrorNumber, "-----" & gConvert.fConvierteAString(NumerodeLinea) & "-----"
               Write #FileErrorNumber, lineaActual
               Write #FileErrorNumber, LineaDeErrores
               If DebeModificarArray Then
                  sCambiaAFalseElRegistroDelArraySiExiste mVectorDeVerificaciones, NumeroDeLineaActual, mValorDelCampoDeId, valIndex1, valIndex2
               End If
               ErrorDeTraduccion = refErrorDeTraduccion
            End If
            sMuestraProgreso True, mNumerodeRegistros, NumeroDeLineaActual
         End If
      End If
      NumeroDeLineaActual = NumeroDeLineaActual + 1
   Loop
   If mGenerarCxC Then
      If Not insCxC.fSearchByField("Numero", valorDelIdMaster, False, False) Then
         If insFactura.fSearchByField("Numero", valorDelIdMaster, False, True, False, True) Then
            If insFactura.GetStatusFacturaAsEnum = enum_StatusFactura.eSF_EMITIDA Then
               insFactura.sGeneraCXC False
            End If
         End If
      End If
   End If
   mNumerodeRegistros = Contador
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sChequeaLaIntegridadDeLosDatosConDetail", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sChequeaLaIntegridadDeLosDatosSaw(ByVal FileErrorNumber As Integer, ByVal valSeparador As String, ByRef refErrorDeTraduccion As Boolean, ByRef refProgressBar As ProgressBar, ByVal valNombreArchivoImportacion As String, Optional ByVal valEsFacturaBorradores As Boolean = False, Optional ByRef DebeImportar As Boolean = False)
   On Error GoTo h_ERROR
   Select Case mModuloAImportar
      Case eEAEI_PROVEEDOR: sChequeaLaIntegridadDeLosDatosSawSinDetail FileErrorNumber, valSeparador, refErrorDeTraduccion
      Case eEAEI_CLIENTE: sChequeaLaIntegridadDeLosDatosSawSinDetail FileErrorNumber, valSeparador, refErrorDeTraduccion
      Case eEAEI_ARTICULO_INVENTARIO:
         mMensajePrecioConIva = False
         mMensajePrecioSinIva = False
         mCambiarAUsaMultiplesAlicuotas = False
         SetRealizarBusquedaEnArchivo True
         sLlenarArregloConLineaDeProducto mLineaDeProducto
         sChequeaLaIntegridadDeLosDatosSawSinDetail FileErrorNumber, valSeparador, refErrorDeTraduccion
         SetRealizarBusquedaEnArchivo False
      Case eEAEI_CxC: sChequeaLaIntegridadDeLosDatosSawSinDetail FileErrorNumber, valSeparador, refErrorDeTraduccion
      Case eEAEI_CxC_ESPECIAL: sChequeaLaIntegridadDeLosDatosSawSinDetail FileErrorNumber, valSeparador, refErrorDeTraduccion
      Case eEAEI_CxP: sChequeaLaIntegridadDeLosDatosSawSinDetail FileErrorNumber, valSeparador, refErrorDeTraduccion
      Case eEAEI_FACTURA: sChequeaLaIntegridadDeLosDatosConDetail valSeparador, refProgressBar, FileErrorNumber, valEsFacturaBorradores, DebeImportar
      Case eEAEI_VENDEDOR: sChequeaLaIntegridadDeLosDatosConDetail valSeparador, refProgressBar, FileErrorNumber, False, DebeImportar
      Case eEAEI_COTIZACION: sChequeaLaIntegridadDeLosDatosConDetail valSeparador, refProgressBar, FileErrorNumber, valEsFacturaBorradores, DebeImportar
      Case eEAEI_RDV: sChequeaLaIntegridadDeLosDatosSawSinDetail FileErrorNumber, valSeparador, refErrorDeTraduccion
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sChequeaLaIntegridadDeLosDatos", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fDistribuyeLaLineaModulosSaw(ByRef refLineaActual As String, ByVal NumeroDeLineaActual As Long, ByVal valSeparador As String, Optional ByVal valVerificarSiEstaRepetido As Boolean = True, Optional ByRef refEsteRegistroSeIncluye As Boolean = False, Optional ByVal valVerificarParaImportacion As Boolean = False) As Boolean
   On Error GoTo h_ERROR
   Select Case mModuloAImportar
      Case eEAEI_PROVEEDOR: fDistribuyeLaLineaModulosSaw = fDistribuyeLaLineaDeProveedor(refLineaActual, NumeroDeLineaActual, valSeparador, valVerificarSiEstaRepetido)
      Case eEAEI_CLIENTE: fDistribuyeLaLineaModulosSaw = fDistribuyeLaLineaDeCliente(refLineaActual, NumeroDeLineaActual, valSeparador, valVerificarSiEstaRepetido)
      Case eEAEI_ARTICULO_INVENTARIO: fDistribuyeLaLineaModulosSaw = fDistribuyeLaLineaDeArticulo(refLineaActual, NumeroDeLineaActual)
      Case eEAEI_CxC: fDistribuyeLaLineaModulosSaw = fDistribuyeLaLineaDeLaCxC(refLineaActual, NumeroDeLineaActual, valSeparador, valVerificarSiEstaRepetido, refEsteRegistroSeIncluye, valVerificarParaImportacion)
      Case eEAEI_CxC_ESPECIAL: fDistribuyeLaLineaModulosSaw = fDistribuyeLaLineaDeLaCxCEspecial(refLineaActual, NumeroDeLineaActual, valSeparador, valVerificarSiEstaRepetido, refEsteRegistroSeIncluye, valVerificarParaImportacion)
      Case eEAEI_CxP: fDistribuyeLaLineaModulosSaw = fDistribuyeLaLineaDeLaCxP(refLineaActual, NumeroDeLineaActual, valSeparador, LineaDeErrores, valVerificarSiEstaRepetido, refEsteRegistroSeIncluye, valVerificarParaImportacion, mVectorDeVerificaciones)
      Case eEAEI_FACTURA: fDistribuyeLaLineaModulosSaw = fDistribuyeLaLineaDeFactura(refLineaActual, NumeroDeLineaActual, valSeparador)
      Case eEAEI_VENDEDOR: fDistribuyeLaLineaModulosSaw = fDistribuyeLaLineaVendedor(refLineaActual, valSeparador)
      Case eEAEI_COTIZACION: fDistribuyeLaLineaModulosSaw = fDistribuyeLaLineaDeCotizacion(refLineaActual, NumeroDeLineaActual, valSeparador)
      Case eEAEI_RDV: fDistribuyeLaLineaModulosSaw = fDistribuyeLaLineaDeRDV(refLineaActual, NumerodeLinea, valSeparador)
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fDistribuyeLaLineaModulosSaw", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sEjecutaLaImportacionDeModulosSaw(ByRef FileNumber As Integer, ByVal valNombreArchivoImportacion As String, ByVal valSeparador As String, ByRef refProgressBar As ProgressBar, Optional ByVal valEsFacturaBorradores As Boolean = False)
   On Error GoTo h_ERROR
   Select Case mModuloAImportar
      Case eEAEI_PROVEEDOR: sEjecutaLaImportacionModulosSinDetail FileNumber, valNombreArchivoImportacion, valSeparador, refProgressBar
      Case eEAEI_CLIENTE: sEjecutaLaImportacionModulosSinDetail FileNumber, valNombreArchivoImportacion, valSeparador, refProgressBar
      Case eEAEI_ARTICULO_INVENTARIO: sEjecutaLaImportacionModulosSinDetail FileNumber, valNombreArchivoImportacion, valSeparador, refProgressBar
      Case eEAEI_CxC: sEjecutaLaImportacionModulosSinDetail FileNumber, valNombreArchivoImportacion, valSeparador, refProgressBar
      Case eEAEI_CxC_ESPECIAL: sEjecutaLaImportacionModulosSinDetail FileNumber, valNombreArchivoImportacion, valSeparador, refProgressBar
      Case eEAEI_CxP: sEjecutaLaImportacionModulosSinDetail FileNumber, valNombreArchivoImportacion, valSeparador, refProgressBar
      Case eEAEI_FACTURA: sEjecutaLaImportacionDeLModuloConDetalle FileNumber, valNombreArchivoImportacion, valSeparador, refProgressBar, valEsFacturaBorradores
      Case eEAEI_VENDEDOR: sEjecutaLaImportacionDeLModuloConDetalle FileNumber, valNombreArchivoImportacion, valSeparador, refProgressBar
      Case eEAEI_COTIZACION: sEjecutaLaImportacionDeLModuloConDetalle FileNumber, valNombreArchivoImportacion, valSeparador, refProgressBar
      Case eEAEI_RDV: sEjecutaLaImportacionModulosSinDetail FileNumber, valNombreArchivoImportacion, valSeparador, refProgressBar
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sEjecutaLaImportacionDeModulosSaw", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fEntidadToEnumSystemModules() As enum_SystemModules
  On Error GoTo h_ERROR
   Select Case mModuloAImportar
      Case eEAEI_PROVEEDOR: fEntidadToEnumSystemModules = eSM_Proveedor
      Case eEAEI_CLIENTE: fEntidadToEnumSystemModules = eSM_Cliente
      Case eEAEI_ARTICULO_INVENTARIO: fEntidadToEnumSystemModules = eSM_ArticuloInventario
      Case eEAEI_CxC: fEntidadToEnumSystemModules = eSM_CxC
      Case eEAEI_CxC_ESPECIAL: fEntidadToEnumSystemModules = eSM_CxC
      Case eEAEI_CxP: fEntidadToEnumSystemModules = eSM_CxP
      Case eEAEI_FACTURA: fEntidadToEnumSystemModules = eSM_Factura
      Case eEAEI_VENDEDOR: fEntidadToEnumSystemModules = eSM_Vendedor
      Case eEAEI_COTIZACION: fEntidadToEnumSystemModules = eSM_Cotizacion
      Case eEAEI_RDV: fEntidadToEnumSystemModules = eSM_ResumenDiarioVentas
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fEntidadToEnumSystemModules", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sVerificaSiHayErrorEnLaLinea(ByVal NumeroDeLineaActual As Long, ByRef ErrorAlTraducirLaLinea As Boolean, ByRef DebeModificarArray As Boolean, Optional ByVal valEsFacturaBorradores As Boolean = False, Optional ByVal DebeImportar As Boolean = False)
   On Error GoTo h_ERROR
   Select Case mModuloAImportar
      Case eEAEI_FACTURA, eEAEI_COTIZACION:
         sVerificaSiHayErrorEnLaLinea_FACTURA NumeroDeLineaActual, ErrorAlTraducirLaLinea, DebeModificarArray, valEsFacturaBorradores, mModuloAImportar = eEAEI_COTIZACION
         sVerificaSiHayErrorEnLaLineaValoresAjustables_FACTURA ErrorAlTraducirLaLinea, DebeImportar, DebeModificarArray, mModuloAImportar = eEAEI_COTIZACION
      Case eEAEI_VENDEDOR:
         sVerificaSiHayErrorEnLaLinea_VENDEDOR NumeroDeLineaActual, ErrorAlTraducirLaLinea, DebeModificarArray
         sVerificaSiHayErrorEnLaLineaValoresAjustables_VENDEDOR ErrorAlTraducirLaLinea, DebeImportar, DebeModificarArray
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sVerificaSiHayErrorEnLaLinea", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sVerificaSiHayErrorEnLaLineaConValoresAjustables(ByRef ErrorAlTraducirLaLinea As Boolean, Optional ByVal DebeImportar As Boolean = False, Optional ByRef DebeModificarArray As Boolean = False)
   On Error GoTo h_ERROR
   Select Case mModuloAImportar
      Case eEAEI_FACTURA: sVerificaSiHayErrorEnLaLineaValoresAjustables_FACTURA ErrorAlTraducirLaLinea, DebeImportar, DebeModificarArray
      Case eEAEI_VENDEDOR: sVerificaSiHayErrorEnLaLineaValoresAjustables_VENDEDOR ErrorAlTraducirLaLinea, DebeImportar, DebeModificarArray
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sVerificaSiHayErrorEnLaLineaConValoresAjustables", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sDevuelveLosIndexDeCadaModulo(ByRef valIndex1 As Integer, ByRef valIndex2 As Integer, Optional ByRef valIndex3 As Integer)
   On Error GoTo h_ERROR
   Select Case mModuloAImportar
      Case eEAEI_PROVEEDOR:
         valIndex1 = CM_INDEX_CODIGO_PROVEEDOR
         valIndex2 = CM_INDEX_DESCRIPCION_PROVEEDOR
      Case eEAEI_CLIENTE:
         valIndex1 = CM_INDEX_CODIGO_CLIENTE
         valIndex2 = CM_INDEX_NOMBRE_CLIENTE
         valIndex3 = CM_INDEX_RIF_CLIENTE
      Case eEAEI_ARTICULO_INVENTARIO:
         valIndex1 = CM_INDEX_CODIGO_ARTICULO
         valIndex2 = CM_INDEX_VALOR_IMPORTA_ARTICULO_DETALLE
      Case eEAEI_CxC:
         valIndex1 = CM_INDEX_NUMERO_Y_TIPO_DE_CXC
         valIndex2 = CM_INDEX_NUMERO_CXC_Y_CODIGO_PROVEEDOR
      Case eEAEI_CxC_ESPECIAL:
         valIndex1 = CM_INDEX_NUMERO_Y_TIPO_DE_CXC
         valIndex2 = CM_INDEX_NUMERO_CXC_Y_CODIGO_PROVEEDOR
      Case eEAEI_CxP:
         valIndex1 = CM_INDEX_NUMERO_CXC_Y_CODIGO_PROVEEDOR
      Case eEAEI_FACTURA, eEAEI_COTIZACION, eEAEI_RDV:
         valIndex1 = CM_INDEX_NUMERO_FACTURA
         valIndex2 = CM_INDEX_VALOR_IMPORTA_FACTURA_DETALLE
      Case eEAEI_VENDEDOR:
         valIndex1 = CM_INDEX_NUMERO_VENDEDOR
         valIndex2 = CM_INDEX_VALOR_IMPORTA_VENDEDOR
         valIndex3 = CM_INDEX_VALOR_IMPORTA_COMISION_VENDEDOR
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sDevuelveLosIndexDeCadaModulo", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fImportaLasLineas(Optional ByVal NumeroDeLineaActual As Long, Optional ByRef numeroDeRegistrosImportados As Long) As Boolean
  On Error GoTo h_ERROR
   Select Case mModuloAImportar
      Case eEAEI_PROVEEDOR:
         insProveedor.SetCodigoLote mCodigoLote
         fImportaLasLineas = fVerificaSiNoExisteElProveedorYLoAgrega
      Case eEAEI_CLIENTE:
         insCliente.SetCodigoLote mCodigoLote
         fImportaLasLineas = fVerificaSiNoExisteElClienteYLoAgrega
      Case eEAEI_ARTICULO_INVENTARIO:
         Dim valCantidad As Currency
         Dim valTipoDeArticulo As String
         Dim valTipoDeArticuloInv As String
         Dim valCodigoInventario As String
         Dim valAlmacen As String
         Dim valSerial As String
         Dim valRollo As String
         Dim vConsecutivoAlmacen As Long
         valCodigoInventario = valCodigoArticulo
         valCantidad = valExistenciaAlmacenExtra
         valTipoDeArticulo = insArticuloInventario.GetTipoDeArticuloStr
         valAlmacen = valCodigoAlmacenExtra
         valSerial = insExistenciaPorGrupo.GetSerial
         valRollo = insExistenciaPorGrupo.GetRollo
         valTipoDeArticuloInv = insArticuloInventario.GetTipoArticuloInvStr
         insArticuloInventario.SetCodigoLote mCodigoLote
         vConsecutivoAlmacen = valConsecutivoAlmacen
         ReDim Preserve valDatosDeInventario(7, NumeroDeLineaActual)
         fImportaLasLineas = fAgregaElArticulo
         If fImportaLasLineas Then
            valDatosDeInventario(CODIGO_ARTICULO, NumeroDeLineaActual) = valCodigoInventario
            valDatosDeInventario(CANTIDAD_DE_ARTICULO, NumeroDeLineaActual) = valCantidad
            valDatosDeInventario(TIPO_DE_ARTICULO, NumeroDeLineaActual) = valTipoDeArticulo
            valDatosDeInventario(CODIGO_ALMACEN, NumeroDeLineaActual) = valAlmacen
            valDatosDeInventario(SERIAL, NumeroDeLineaActual) = valSerial
            valDatosDeInventario(ROLLO, NumeroDeLineaActual) = valRollo
            valDatosDeInventario(TIPO_ARTICULO_INV, NumeroDeLineaActual) = valTipoDeArticuloInv
            valDatosDeInventario(CONSECUTIVO_ALMACEN, NumeroDeLineaActual) = vConsecutivoAlmacen
         End If
      Case eEAEI_CxC:
         insCxC.SetCodigoLote mCodigoLote
         fImportaLasLineas = fVerificaSiNoExisteLaCxCYLoAgrega(numeroDeRegistrosImportados)
      Case eEAEI_CxC_ESPECIAL:
         insCxC.SetCodigoLote mCodigoLote
         fImportaLasLineas = fVerificaSiNoExisteLaCxCYLoAgrega(numeroDeRegistrosImportados)
      Case eEAEI_CxP:
         insCxP.SetCodigoLote mCodigoLote
         fImportaLasLineas = fVerificaSiNoExisteLaCxPYLoAgrega(numeroDeRegistrosImportados)
      Case eEAEI_FACTURA, eEAEI_COTIZACION:
         insFactura.SetCodigoLote mCodigoLote
         If (mModuloAImportar = eEAEI_COTIZACION) Then
            insFactura.setClaseDeTrabajo eCTFC_Cotizacion
            insRenglonFactura.setClaseDeTrabajo eCTFC_Cotizacion
            insRenglonFactura.SetRsRenglonFactura Nothing
         End If
         fImportaLasLineas = fImportaLineasFacturayDetalle
      Case eEAEI_VENDEDOR:
         sSeteaValoresDeLaLineaDelArchivo_VENDEDOR
         fImportaLasLineas = fImportaLineasVendedoryComision(NumeroDeLineaActual)
      Case eEAEI_RDV: fImportaLasLineas = fImportaRDV
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fImportaLasLineas", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fgetCodigoSegunModulo() As String
   Dim valCodigo As String
   On Error GoTo h_ERROR
   Select Case mModuloAImportar
      Case eEAEI_PROVEEDOR: valCodigo = insProveedor.getCodigoProveedor
      Case eEAEI_CLIENTE:  valCodigo = insCliente.GetCodigo
      Case eEAEI_ARTICULO_INVENTARIO: valCodigo = insArticuloInventario.GetCodigo
      Case eEAEI_CxC: valCodigo = insCxC.GetNumero
      Case eEAEI_CxC_ESPECIAL: valCodigo = insCxC.GetNumero
      Case eEAEI_CxP: valCodigo = insCxP.GetNumero
   End Select
h_EXIT: On Error GoTo 0
   fgetCodigoSegunModulo = valCodigo
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fgetCodigoSegunModulo ", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sCambiaAFalseElRegistroDelArraySiExiste(ByRef valItemsArray As Variant, ByVal valIndiceActual As Long, ByVal valValorItem As String, ByVal valIndex1 As Long, ByVal valIndex2 As Long)
   Dim valor As Boolean
   Dim valregistroActual As Long
   On Error GoTo h_ERROR
   valor = False
   If valIndiceActual >= 0 And valIndex1 >= LBound(valItemsArray) And valIndex1 < UBound(valItemsArray) And valIndex2 >= LBound(valItemsArray) And valIndex2 < UBound(valItemsArray) Then
      For valregistroActual = 0 To valIndiceActual
         If UCase(valItemsArray(valIndex1, valregistroActual)) = UCase(valValorItem) Then
            valItemsArray(valIndex2, valregistroActual) = valor
            Exit For
         End If
      Next valregistroActual
   End If
h_EXIT: On Error GoTo 0
  Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sCambiaAFalseElRegistroDelArraySiExiste", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fPreparaLosDatosConDetalleEImporta(ByVal valSeparador As String, ByVal NumeroDeLineaActual As Long, ByVal lineaActual As String, ByVal valIndex1 As Long, ByVal valIndex2 As Long, Optional ByVal valEsFacturaBorradores As Boolean = False) As Boolean
   Dim refErrorDeTraduccion As Boolean
   Dim exito As Boolean
   Dim borrador As String
   Dim valLoteAdm As Integer
   Dim vContinuar As Boolean
   On Error GoTo h_ERROR
   exito = False
   borrador = ""
   refErrorDeTraduccion = False
   If valEsFacturaBorradores Then
      borrador = "Borradores de "
   End If
   vContinuar = True
   If NumeroDeLineaActual > 0 Then
      vContinuar = insImportar.fVerificaValorImportarEnElArray(mVectorDeVerificaciones, NumeroDeLineaActual, mValorDelCampoDeId, True, valIndex1, valIndex2)
   End If
   If vContinuar Then
      If fDistribuyeLaLineaModulosSaw(lineaActual, NumeroDeLineaActual, valSeparador) Then
         sVerificaSiHayErrorEnLaLineaConValoresAjustables refErrorDeTraduccion, True
         If Not refErrorDeTraduccion Then
            If Not mYaIngresoUnoMas Then
               EnumSystemModules = fEntidadToEnumSystemModules
               valLoteAdm = insLoteDeAdm.EnumSystemModulesToModuloDelLoteAdm(EnumSystemModules)
               sAsignaValoresDelLoteAImportar valLoteAdm, "Lote de " & borrador & gEnumProyecto.enumEntidadAExportarOImportarToString(mModuloAImportar) & " Generado desde un Archivo de Impotación."
               If insLoteDeAdm.fInsert Then
                  mYaIngresoUnoMas = True
               End If
            End If
            If fImportaLasLineas(NumeroDeLineaActual) Then
               exito = True
            End If
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   fPreparaLosDatosConDetalleEImporta = exito
   Exit Function
h_ERROR:
Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fPreparaLosDatosYDetalleEImporta", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fVerificaValorImportarEnElArray(ByVal valItemsArray As Variant, ByVal valIndiceActual As Long, ByVal valValorItem1 As String, ByVal valValorItem2 As Boolean, ByVal valIndex1 As Long, ByVal valIndex2 As Long) As Boolean
   Dim valregistroActual As Long
   Dim Importa As Boolean
   On Error GoTo h_ERROR
   Importa = False
   For valregistroActual = 0 To valIndiceActual
      If UCase(valItemsArray(valIndex1, valregistroActual)) = UCase(valValorItem1) Then
         If UCase(valItemsArray(valIndex2, valregistroActual)) = UCase(valValorItem2) Then
            Importa = True
            Exit For
         End If
         Exit For
      End If
   Next valregistroActual
h_EXIT:
  fVerificaValorImportarEnElArray = Importa
  On Error GoTo 0
  Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fVerificaValorImportarEnElArray", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sAsignaValoresDelLoteAImportar(ByVal modulo As Integer, ByVal Descripción As String)
   On Error GoTo h_ERROR
   insLoteDeAdm.SetConsecutivoCompania mConsecutivoCompaniaActual
   insLoteDeAdm.SetFechaLote gUtilDate.getFechaDeHoy
   insLoteDeAdm.SetModulo modulo
   insLoteDeAdm.SetDescripcion Descripción
   insLoteDeAdm.SetNombreOperador mNombreDelUsuario
   insLoteDeAdm.SetFechaUltimaModificacion gUtilDate.getFechaDeHoy
   insLoteDeAdm.SetCodigoLote insLoteDeAdm.fGenerateNextCodigoLote(True)
   SetCodigoLote insLoteDeAdm.GetCodigoLote
h_EXIT: On Error GoTo 0
  Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sAsignaValoresDelLoteAImportar", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaLaImportacionDeLModuloConDetalle(ByRef refFileNumberdeLectura As Integer, ByVal valNombreArchivoImportacion As String, ByVal valSeparador As String, ByRef refProgressBar As ProgressBar, Optional ByVal valEsFacturaBorradores As Boolean = False)
   Dim FileErrorNumber As Integer
   On Error GoTo h_ERROR
   FileErrorNumber = 0
   mNregistroAInsertar = 0
   If insImportar.fAbrirFileForInput(refFileNumberdeLectura, valNombreArchivoImportacion) Then
      sChequeaLaIntegridadDeLosDatosSaw FileErrorNumber, valSeparador, ErrorDeTraduccion, refProgressBar, valNombreArchivoImportacion, valEsFacturaBorradores, True
      insImportar.sCierraElFile refFileNumberdeLectura
   End If
   sManejoDeMensajes mNumerodeRegistros, mNregistroAInsertar, mNregistroExistenBd, valEsFacturaBorradores
h_EXIT: On Error GoTo 0
  Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sEjecutaLaImportacionDeLModuloConDetalle", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaLaImportacionModulosSinDetail(ByRef refFileNumberdeLectura As Integer, ByVal valNombreArchivoImportacion As String, ByVal valSeparador As String, ByRef refProgressBar As ProgressBar)
   Dim lineaActual As String
   Dim valTotalRegistro As Long
   Dim numeroDeRegistrosImportados As Long
   Dim YaInsertoUnoOMas As Boolean
   Dim valLoteAdm As Integer
   On Error GoTo h_ERROR
   Set insImportar = New clsImportar
   YaInsertoUnoOMas = False
   RegistrosYaExistentesEnBD = 0
   numeroDeRegistrosImportados = 0
   mNregistroAInsertar = 0
   If insImportar.fAbrirFileForInput(refFileNumberdeLectura, valNombreArchivoImportacion) Then
      mImportoRegistros = False
      valTotalRegistro = 0
      Do Until EOF(refFileNumberdeLectura)
         Line Input #refFileNumberdeLectura, lineaActual
         If lineaActual <> "" Then
            valTotalRegistro = valTotalRegistro + 1
            If fDistribuyeLaLineaModulosSaw(lineaActual, valTotalRegistro, valSeparador, False, True, True) Then
               If Not YaInsertoUnoOMas Then
                  EnumSystemModules = fEntidadToEnumSystemModules
                  valLoteAdm = insLoteDeAdm.EnumSystemModulesToModuloDelLoteAdm(EnumSystemModules)
                  sAsignaValoresDelLoteAImportar valLoteAdm, "Lote de " & gEnumProyecto.enumEntidadAExportarOImportarToString(mModuloAImportar) & " Generado desde un Archivo de Importación."
                  If insLoteDeAdm.fInsert Then
                     YaInsertoUnoOMas = True
                  End If
               End If
               If fImportaLasLineas(mNregistroAInsertar, numeroDeRegistrosImportados) Then
                  mNregistroAInsertar = mNregistroAInsertar + 1
                  mImportoRegistros = True
                  sMuestraProgreso True, mNumerodeRegistros, mNregistroAInsertar
               End If
            Else
               RegistrosYaExistentesEnBD = RegistrosYaExistentesEnBD + 1
            End If
         End If
      Loop
      If mImportoRegistros Then
         If mModuloAImportar = eEAEI_ARTICULO_INVENTARIO Then
            fContinuaConProcesosDeInsercionDeArticulos (valTotalRegistro)
         End If
      End If
      If mImportoRegistros Then
         If (mModuloAImportar = eEAEI_PROVEEDOR) Or (mModuloAImportar = eEAEI_CLIENTE) Then
            If gProyCompaniaActual.GetUsaModuloDeContabilidad Then
               sGenerarAuxiliaresParaProveedoresYClientes
            End If
         End If
      End If
   End If
   insImportar.sCierraElFile refFileNumberdeLectura
   sManejoDeMensajes mNumerodeRegistros, mNregistroAInsertar, mNregistroExistenBd
h_EXIT: On Error GoTo 0
   Set insImportar = Nothing
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sEjecutaLaImportacionModulosSinDetail", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sManejoDeMensajes(ByVal valTotalRegistro As Long, ByVal valNumeroRegistro As Long, ByVal mNregistroExistenBd As Long, Optional ByVal valEsFacturaBorradores As Boolean = False)
   Dim valporcentaje As Currency
   Dim msgFacturaBorrador As String
   Dim vMsg As String
   On Error GoTo h_ERROR
   If valEsFacturaBorradores Then
      msgFacturaBorrador = "Borradores de "
   Else
      msgFacturaBorrador = ""
   End If
   If mImportoRegistros Then
      valporcentaje = fPorcentaje(valTotalRegistro, valNumeroRegistro, False)
      vMsg = "El proceso de importación de " & msgFacturaBorrador & gEnumProyecto.enum_SystemModulesToStr(EnumSystemModules) & " ha concluido. " & vbCr
      vMsg = vMsg & "El número de " & msgFacturaBorrador & gEnumProyecto.enumEntidadAExportarOImportarToString(mModuloAImportar) & " importados es : " & valNumeroRegistro & vbCr
      vMsg = vMsg & " De: " & valTotalRegistro & " Se realizó el : " & valporcentaje & " % de los datos."
      gMessage.Success vMsg
      If valTotalRegistro > valNumeroRegistro Then
         gMessage.InformationMessage "No se importó la totalidad de los registros en el módulo de " & msgFacturaBorrador & gEnumProyecto.enum_SystemModulesToStr(EnumSystemModules) & " debido a que la data ya se encontraba existente en el sistema ó a que se encontraron posibles inconsistencias.", "Importación de " & gEnumProyecto.enum_SystemModulesToStr(EnumSystemModules)
      End If
   Else
      If mNregistroExistenBd = valTotalRegistro And valTotalRegistro <> 0 Then
         vMsg = "Todos Los registros a importar en el módulo " & msgFacturaBorrador & gEnumProyecto.enum_SystemModulesToStr(EnumSystemModules) & ", ya se encontraban existentes en el sistema. " & vbCr
         vMsg = vMsg & "No se efectuó la Importación del módulo " & msgFacturaBorrador & gEnumProyecto.enumEntidadAExportarOImportarToString(mModuloAImportar)
         gMessage.InformationMessage vMsg, "Importación de " & gEnumProyecto.enum_SystemModulesToStr(EnumSystemModules)
      ElseIf mNregistroExistenBd > 0 Then
         gMessage.Advertencia "No se importaron un total de " & RegistrosYaExistentesEnBD & vbCrLf & " registros en el módulo de " & msgFacturaBorrador & gEnumProyecto.enumEntidadAExportarOImportarToString(mModuloAImportar) & " debido a que ya se encontraban existentes en el sistema."
      ElseIf mNregistroAInsertar = 0 Then
         gMessage.Advertencia "No se importó ningun registro en el módulo de " & msgFacturaBorrador & gEnumProyecto.enumEntidadAExportarOImportarToString(mModuloAImportar) & ", debido a que todos los datos ya se encontraban existentes en el sistema ó a que se encontraron posibles inconsitencias en la data."
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sManejoDeMensajes", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sGenerarAuxiliaresParaProveedoresYClientes()
   Dim ModuloACargarAuxiliares As enum_GrupoAuxiliar
   On Error GoTo h_ERROR
   Select Case mModuloAImportar
      Case enum_EntidadAExportarOImportar.eEAEI_PROVEEDOR: ModuloACargarAuxiliares = eGA_PROVEEDORES
      Case enum_EntidadAExportarOImportar.eEAEI_CLIENTE: ModuloACargarAuxiliares = eGA_CLIENTES
      Case Else: ModuloACargarAuxiliares = eGA_SINGRUPO
   End Select
   varNoComunWincont.sEjecutaAuxiliarGenerar (ModuloACargarAuxiliares)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sGenerarAuxiliaresParaProveedoresYClientes", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fPorcentaje(ByVal va1TotalRegistros As Long, ByVal valNumerodelRegistro As Long, valRedondear As Boolean) As Currency
   Dim valporcentaje As Currency
   On Error GoTo h_ERROR
   If va1TotalRegistros <> 0 Then
      If valRedondear Then
         valporcentaje = Round((valNumerodelRegistro * 100) / va1TotalRegistros)
      Else
         valporcentaje = gConvert.fNumToStringConSeparadorDeMiles((valNumerodelRegistro * 100) / va1TotalRegistros, False, 2)
      End If
   Else
      valporcentaje = 0
   End If
h_EXIT: fPorcentaje = valporcentaje
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fPorcentaje", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sMuestraProgreso(ByVal valMuestraProgreso As Boolean, ByVal valNumerodeRegistros As Long, ByVal valNumeroRegistro As Long)
   On Error GoTo h_ERROR
   If valMuestraProgreso Then
      gApi.sAddProgress mProgressBar
      mLabelPorcentaje.Caption = "Porcentaje:" & fPorcentaje(valNumerodeRegistros, valNumeroRegistro, True) & " % "
      mLabelPorcentaje.Refresh
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sMuestraProgreso", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fDevuelveElNombreDeLaMonedaIfExist(ByVal valMonedaCodigo As String, Optional ByVal valShowMessageIfNotFound As Boolean = False) As String
   Dim SQL As String
   Dim mrsMoneda As ADODB.Recordset
   Dim varMonedaCodigo As String
   On Error GoTo h_ERROR
   Set mrsMoneda = New ADODB.Recordset
   varMonedaCodigo = fSustituirSiEsNecesario(valMonedaCodigo)
   SQL = "SELECT Moneda.Nombre FROM Moneda WHERE Moneda.Codigo = '" & varMonedaCodigo & "'"
   gDbUtil.sInitRS mrsMoneda, "Moneda", False
   If gDbUtil.fOpenRecordSetAllParameters(mrsMoneda, SQL, Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If gDbUtil.fRecordCount(mrsMoneda) = 0 Then
         If valShowMessageIfNotFound Then
            gMessage.sNoExistenRegistrosConEstasCaracteristicas "Importar modulos Administrativo", eg_Male
         End If
         fDevuelveElNombreDeLaMonedaIfExist = varMonedaCodigo
      Else
         fDevuelveElNombreDeLaMonedaIfExist = mrsMoneda.Fields("Nombre").Value
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fDevuelveElNombreDeLaMonedaIfExist", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSustituirSiEsNecesario(ByVal valMoneda As String) As String
   Dim strValor As String
   On Error GoTo h_ERROR
   If UCase(valMoneda) = "BOLIVARES" Or UCase(valMoneda) = "BS" Then
      strValor = "VEB"
   ElseIf UCase(valMoneda) = "DOLARES" Or UCase(valMoneda) = "DS" Then
      strValor = "USD"
   ElseIf UCase(valMoneda) = "EURO" Then
      strValor = "EUR"
   ElseIf UCase(valMoneda) = "ML" Then
      strValor = mCodigoMonedaLocal
   ElseIf UCase(valMoneda) = "ME" Then
      strValor = mCodigoMonedaExtranjera
   Else
      strValor = valMoneda
   End If
   fSustituirSiEsNecesario = strValor
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR: fSustituirSiEsNecesario = valMoneda
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fSustituirSiEsNecesario", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub Class_Initialize()
   On Error GoTo h_ERROR
   Set gUtilImport = New clsUtilImport
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "Class_Initialize", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Class_Terminate()
   On Error GoTo h_ERROR
   Set gUtilImport = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "Class_Terminate", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fValidaLamoneda(Optional ByVal valMoneda As String) As String
   Dim strValor As String
   On Error GoTo h_ERROR
   Select Case UCase(valMoneda)
      Case "BOLIVAR", "BOLÍVAR", "BOLIVARES": strValor = "VED"
      Case "DOLARES", "DOLAR", "DOLAR EEUU", "DÓLAR", "DÓLARES": strValor = "USD"
      Case "EURO", "EUROS": strValor = "EUR"
      Case Else: strValor = valMoneda
   End Select
h_EXIT: On Error GoTo 0
   fValidaLamoneda = strValor
   Exit Function
h_ERROR: fValidaLamoneda = valMoneda
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fValidaLamoneda", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sEjecutaLaImportacionDeRenglonCobroDeFactura(ByVal valNombreDelArchivo As String, ByVal valSeparador As String, ByVal valExtension As String)
   On Error GoTo h_ERROR
   Dim valNombreArchivoRenglonCobro As String
   Dim PuntoInicial As Long
   Dim lineaActual As String
   Dim NumeroDeLineaActual As Long
   PuntoInicial = gTexto.DfLen(valNombreDelArchivo) - 4
   valNombreArchivoRenglonCobro = gTexto.DfMid(valNombreDelArchivo, 1, PuntoInicial) & "RenglonCobro." & valExtension
   If gUtilFile.fExisteElArchivo(valNombreArchivoRenglonCobro) Then
      If insImportar.fAbrirFileForInput(FileNumberdeLectura, valNombreArchivoRenglonCobro) Then
         NumeroDeLineaActual = 0
         Do Until EOF(FileNumberdeLectura)
            Line Input #FileNumberdeLectura, lineaActual
            If lineaActual <> "" Then
               NumeroDeLineaActual = NumeroDeLineaActual + 1
               If fDistribuyeLaLineaDeRenglonCobroDeFactura(lineaActual, NumeroDeLineaActual, valSeparador) Then
                  If insRenglonCobroDeFactura.fInsert Then
                  End If
               End If
            End If
         Loop
         insImportar.sCierraElFile (FileNumberdeLectura)
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sEjecutaLaImportacionDeRenglonCobroDeFactura", "Importar modulos Administrativo", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fSelectAndSetValuesOfAlmacenFromAOS(ByRef refCodigo As String, ByRef refNombreAlmacen As String, ByVal valDescripcionBusqueda As String, Optional ByVal valCampo As String, Optional ByRef refConsecutivoAlmacen As Long) As Boolean
   Dim vResult As Boolean
   Dim refXmlString As String
   On Error GoTo h_ERROR
   vResult = False
   If valCampo = "" Then
      valCampo = "Saw.Gv_Almacen_B1.Codigo"
   End If
   refXmlString = insWrpAlmacen.Choose(valCampo & "=" & valDescripcionBusqueda, "Saw.Gv_Almacen_B1.ConsecutivoCompania=" & gConvert.fConvierteAString(mConsecutivoCompaniaActual))
   vResult = (gTexto.DfLen(refXmlString) > 0)
   If vResult Then
      gLibGalacDataParse.Initialize refXmlString
      refCodigo = gLibGalacDataParse.GetString(0, "Codigo", "")
      refNombreAlmacen = gLibGalacDataParse.GetString(0, "NombreAlmacen", "")
      refConsecutivoAlmacen = gLibGalacDataParse.GetLong(0, "Consecutivo", 0)
   Else
      vResult = False
   End If
   fSelectAndSetValuesOfAlmacenFromAOS = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSelectAndSetValuesOfAlmacenFromAOS = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fSelectAndSetValuesOfAlmacenFromAOS", "fSelectAndSetValuesOfAlmacenFromAOS", Enum_Gender.eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fInsertarProveedor() As Boolean
   Dim vResult As Boolean
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vSql As String
   Dim Rs As ADODB.Recordset
   On Error GoTo h_ERROR
   vResult = False
   Set insUtAdoCmd = New clsUtilAdoCmd
   Set vCmd = insUtAdoCmd.fCreateCmd("Adm.Gp_ProveedorINS", gDefDatabase.Conexion)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", gProyParametrosCompania.GetConsecutivoCompania)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoProveedor", 10, insProveedor.getCodigoProveedor)
   Set Rs = New ADODB.Recordset
   vSql = "SELECT " & gUtilSQL.fMax("Consecutivo", "Consecutivo") & " FROM Adm.Proveedor WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual
   If gDbUtil.fOpenRecordset(Rs, vSql, Conexion) Then
      If Rs.RecordCount > 0 Then
         If (Rs("Consecutivo").Value <> "NULL") Then
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", Rs("Consecutivo").Value + 1)
         Else
            vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", 1)
         End If
      Else
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", 1)
      End If
   End If
   Set Rs = Nothing
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreProveedor", 60, insProveedor.getNombreProveedor)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Contacto", 35, insProveedor.getContacto)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NumeroRIF", 20, insProveedor.getNumeroRIF)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NumeroNIT", 12, insProveedor.getNumeroNIT)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoDePersona", 1, gEnumTablaRetencion.strTipodePersonaRetencionToNum(insProveedor.getTipoDePersonaStr, False))
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoRetencionUsual", 6, insProveedor.GetCodigoRetencionUsual)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Telefonos", 40, insProveedor.getTelefonos)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Direccion", 255, insProveedor.getDireccion)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Fax", 25, insProveedor.getFax)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Email", 100, insProveedor.getEmail)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@TipodeProveedor", 20, insProveedor.getTipodeProveedor)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoDeProveedorDeLibrosFiscales", 1, gConvert.enumerativoAChar(gEnumProyecto.strTipoDeProveedorDeLibrosFiscalesToNum(insProveedor.GetTipoDeProveedorDeLibrosFiscalesStr, False)))
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeRetencionIVA", insProveedor.getPorcentajeRetencionIVA)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaContableCxP", 30, insProveedor.getCuentaContableCxP)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaContableGastos", 30, insProveedor.getCuentaContableGastos)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaContableAnticipo", 30, insProveedor.getCuentaContableAnticipo)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoLote", 10, insProveedor.GetCodigoLote)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Beneficiario", 60, insProveedor.getBeneficiario)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterBooleanSN("@UsarBeneficiarioImpCheq", insProveedor.getUsarBeneficiarioImpCheq)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoDocumentoIdentificacion", 1, gConvert.enumerativoAChar(gEnumProyecto.strTipoDocumentoIdentificacionPeruToNum(insProveedor.GetTipoDocumentoIdentificacionStr, False)))
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterBooleanSN("@EsAgenteDeRetencionIva", insProveedor.getEsAgenteDeRetencionIva)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Nombre", 20, insProveedor.getNombre)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@ApellidoPaterno", 20, insProveedor.getApellidoPaterno)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@ApellidoMaterno", 20, insProveedor.getApellidoMaterno)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NumeroCuentaBancaria", 20, insProveedor.getNumeroCuentaBancaria)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoContribuyente", 20, insProveedor.getCodigoContribuyente)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NumeroRUC", 20, insProveedor.GetNumeroRUC)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoDePersonaLibrosElectronicos", 1, gConvert.enumerativoAChar(gEnumProyecto.strTipoDePersonaLibrosElectronicosToNum(insProveedor.GetTipoDePersonaLibrosElectronicosStr, False)))
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoPaisResidencia", 4, insProveedor.GetCodigoPaisResidencia)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoConveniosSunat", 2, insProveedor.GetCodigoConveniosSunat)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, insProveedor.GetNombreOperador)
   vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", gUtilDate.getFechaDeHoy)
   insUtAdoCmd.fExecCmd vCmd
   Set vCmd = Nothing
   vResult = True
   fInsertarProveedor = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fInsertarProveedor = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fInsertarProveedor", "fInsertarProveedor", Enum_Gender.eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDistribuyeLaLineaDeCotizacion(ByRef refLineaActual As String, ByVal valLineaActual As Long, ByVal valSeparador As String) As Boolean
   Dim linea As String
   Dim vAlicuotaIvaEnum As enum_TipoDeAlicuota
   Dim Rs As ADODB.Recordset
   Dim vSql As String
   On Error GoTo h_ERROR
   fDistribuyeLaLineaDeCotizacion = False
   linea = refLineaActual
   With insFactura
      vConsecutivoCompania = mConsecutivoCompaniaActual
      .SetConsecutivoCompania vConsecutivoCompania
      .SetNumero mValorDelCampoDeId
      vFecha = gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetFecha vFecha
      .SetFechaDeEntrega vFecha
      vCodigoCliente = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetCodigoCliente vCodigoCliente
      vCodigoVendedor = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetCodigoVendedor vCodigoVendedor
      vObservaciones = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetObservaciones vObservaciones
      vMontoExentoDespuesDeDescuento = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetMontoExentoDespuesDeDescuento vMontoExentoDespuesDeDescuento
      vBaseImponibleDespuesDescuento = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetBaseImponibleDespuesDescuento vBaseImponibleDespuesDescuento
      vTotalRenglones = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetTotalRenglones vTotalRenglones
      vTotalIVA = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetTotalIVA vTotalIVA
      vTotalFactura = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetTotalFactura vTotalFactura
      vPorcentajeDescuento = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetPorcentajeDescuento vPorcentajeDescuento
      vMoneda = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      vMoneda = fValidaLamoneda(vMoneda)
      .SetCodigoMoneda fValidaLamoneda(vMoneda)
      vNivelPrecio = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      If vNivelPrecio = gEnumProyecto.enumNivelDePrecioToStringSiglas(eND_PRECIO_1) Or vNivelPrecio = gEnumProyecto.enumNivelDePrecioToStringSiglas(eND_PRECIO_2) Or vNivelPrecio = gEnumProyecto.enumNivelDePrecioToStringSiglas(eND_PRECIO_3) Or vNivelPrecio = gEnumProyecto.enumNivelDePrecioToStringSiglas(eND_PRECIO_4) Then
         .SetNivelDePrecioStr gEnumProyecto.enumNivelDePrecioToString(gEnumProyecto.fSiglasNivelDePrecioToEnum(vNivelPrecio, False))
      End If
      vUsarDireccionFiscal = gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetUsarDireccionFiscal vUsarDireccionFiscal
      vNoDirDespachoAimprimir = gUtilImport.fToLong(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetNoDirDespachoAimprimir vNoDirDespachoAimprimir
      vFechaUltimaModificacion = gUtilDate.getFechaDeHoy
      .SetFechaUltimaModificacion vFechaUltimaModificacion
      .SetReservarMercancia gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetFechaDeRetiro gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetNoDirDespachoAimprimir gUtilImport.fToLong(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetMoneda fDevuelveElNombreDeLaMonedaIfExist(vMoneda)
   End With
   With insRenglonFactura
      .SetConsecutivoCompania vConsecutivoCompania
      .SetNumeroFactura mValorDelCampoDeId
      If vConsecutivoRenglon = 0 Then
         vConsecutivoRenglon = vConsecutivoRenglon + 1
      End If
      .SetConsecutivoRenglon vConsecutivoRenglon
      vDescripcion = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetDescripcion vDescripcion
      vCantidad = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetCantidad vCantidad
      vPrecioSinIVA = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetPrecioSinIVA vPrecioSinIVA
      vPrecioConIVA = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetPrecioConIVA vPrecioConIVA
      vDescuentoRenglon = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetPorcentajeDescuento vDescuentoRenglon
      vTotalRenglon = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetTotalRenglon vTotalRenglon
      vPorcentajeBaseImponible = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetPorcentajeBaseImponible vPorcentajeBaseImponible
      vArticulo = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      .SetArticulo vArticulo
      vAlicuotaIVA = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
      vAlicuotaIvaEnum = gEnumProyecto.fSiglasTipoDeAlicuotaToEnum(vAlicuotaIVA, False)
      .SetAlicuotaIVAStr gEnumProyecto.enumTipoDeAlicuotaToString(vAlicuotaIvaEnum)
   End With
   With insFactura
      vCambioABolivaresEnCotizacion = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      If (vCambioABolivaresEnCotizacion = 0) Or (gTexto.DfLen(vCambioABolivaresEnCotizacion) = 0) Or Not IsNumeric(vCambioABolivaresEnCotizacion) Then
         .SetCambioABolivaresEnCotizacion (1)
      Else
         .SetCambioABolivaresEnCotizacion vCambioABolivaresEnCotizacion
      End If
      vCambioMostrarTotalEnDivisasEnCotizacion = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      If (vCambioMostrarTotalEnDivisasEnCotizacion = 0) Or (gTexto.DfLen(vCambioMostrarTotalEnDivisasEnCotizacion) = 0) Or Not IsNumeric(vCambioMostrarTotalEnDivisasEnCotizacion) Then
         .SetCambioMostrarTotalEnDivisasEnCotizacion (1)
      Else
         .SetCambioMostrarTotalEnDivisasEnCotizacion vCambioMostrarTotalEnDivisasEnCotizacion
      End If
      vNroDiasMantenerCambioAMonedaLocal = gUtilImport.fToInt(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      If Not IsNumeric(vNroDiasMantenerCambioAMonedaLocal) Or IsNull(vNroDiasMantenerCambioAMonedaLocal) Then
         .SetNroDiasMantenerCambioAMonedaLocal (0)
      Else
         .SetNroDiasMantenerCambioAMonedaLocal vNroDiasMantenerCambioAMonedaLocal
      End If
      vFechaLimiteCambioAMonedaLocal = gConvert.fConvertStringToDate(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
      .SetFechaLimiteCambioAMonedaLocal vFechaLimiteCambioAMonedaLocal
   End With
h_EXIT: On Error GoTo 0
    fDistribuyeLaLineaDeCotizacion = True
    Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fDistribuyeLaLineaDeCotizacion", CM_MESSAGE_NAME_FACTURA, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFechaAplicacionEnCxPEsValida(ByRef refMensajeError As String) As Boolean
   Dim vFechaAplicacionValida As Date
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = False
   If gProyParametrosCompania.GetEnDondeRetenerIVAAsEnum = enum_DondeSeEfectuaLaRetencionIVA.eDS_NO_RETENIDA Then
      insCxP.SetDiaDeAplicacion 1
      vResult = True
   Else
      If gUtilDate.fElStringContieneUnaFechaValida(insCxP.GetDiaDeAplicacion & "/" & insCxP.GetMesDeAplicacion & "/" & insCxP.GetAnoDeAplicacion) Then
         vFechaAplicacionValida = gConvert.fConvertStringToDate(insCxP.GetDiaDeAplicacion & "/" & insCxP.GetMesDeAplicacion & "/" & insCxP.GetAnoDeAplicacion)
         If (insCxP.GetSeHizoLaRetencion) Then
            insCxP.SetDiaDeAplicacion gUtilDate.fDay(insCxP.GetFechaAplicacionRetIVA)
            insCxP.SetMesDeAplicacion gUtilDate.fMonth(insCxP.GetFechaAplicacionRetIVA)
            insCxP.SetAnoDeAplicacion gUtilDate.fYear(insCxP.GetFechaAplicacionRetIVA)
         ElseIf (insCxP.GetMesDeAplicacion = gUtilDate.fMonth(insCxP.GetFecha) And insCxP.GetAnoDeAplicacion = gUtilDate.fYear(insCxP.GetFechaAplicacionRetIVA)) Then
            insCxP.SetDiaDeAplicacion gUtilDate.fDay(insCxP.GetFecha)
         Else
            insCxP.SetDiaDeAplicacion 1
         End If
         vResult = True
      Else
         refMensajeError = refMensajeError & ":::. ERROR Fecha de Aplicación de la CxP " & insCxP.GetNumero & " contiene una fecha INVÁLIDA, " & insCxP.GetDiaDeAplicacion & "/" & insCxP.GetMesDeAplicacion & "/" & insCxP.GetAnoDeAplicacion & " .:::"
      End If
   End If
h_EXIT: On Error GoTo 0
   fFechaAplicacionEnCxPEsValida = vResult
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fFechaAplicacionEnCxPEsValida", CM_MESSAGE_NAME_CXP, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fValidaMonedaDeCobro(ByVal valMoneda As String) As String
   Dim vCodigoMoneda As String
   Dim vMoneda As String
   On Error GoTo h_ERROR
   vMoneda = UCase(gTexto.fLimpiaStringdeBlancosAAmbosLados(valMoneda))
   vCodigoMoneda = "VED"
   If gTexto.DfLen(vMoneda) > 0 Then
      Select Case vMoneda
         Case "BOLIVAR", "BOLÍVAR", "BOLIVARES", "VED", "BS": vCodigoMoneda = "VED"
         Case "DOLARES", "DOLAR", "DOLAR EEUU", "DÓLAR", "DÓLARES", "USD", "DOLARES AMERICANOS", "DÓLARES AMERICANOS", "DÓLAR AMERICANO": vCodigoMoneda = "USD"
         Case "EURO", "EUROS", "€", "EUR": vCodigoMoneda = "EUR"
         Case "PESOS", "PESO COLIMBIANO", "PESO", "PESOS COLIMBIANOS", "PESOS COLIMBIANO", "PESO COLIMBIANOS", "COP": vCodigoMoneda = "COP"
         Case "SOL", "SOLES", "NUEVOS SOLES", "NUEVO SOL", "NUEVO SOLES", "NUEVO SOLES", "PEN": vCodigoMoneda = "PEN"
      End Select
   End If
h_EXIT: On Error GoTo 0
   fValidaMonedaDeCobro = vCodigoMoneda
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fValidaMonedaDeCobro", CM_MESSAGE_NAME_ARTICULOS, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDistribuyeLaLineaDeRDV(ByRef refLineaActual As String, ByVal valLineaActual As Long, ByVal valSeparador As String) As Boolean
   Dim linea As String
   Dim ErrorAlTraducirLaLinea As Boolean
   Dim vNumeroComprobanteFiscalDesde As String
   Dim vNumeroComprobanteFiscalHasta As String
   Dim vFechaRDV As Date
   Dim vNumeroReporteZ As String
   
   Dim vStrMontoExento As String
   Dim vStrMontoBIAlicuotaGeneral As String
   Dim vStrMontoBIAlicuotaReducida As String
   Dim vStrMontoBIAlicuotaExtendida As String
   Dim vStrMontoNCExento As String
   Dim vStrMontoNCBIAlicuotaGeneral As String
   Dim vStrMontoNCBIAlicuotaReducida As String
   Dim vStrMontoNCBIAlicuotaExtendida As String
   
   Dim vBIIGTF As Currency
   Dim vIGTFML As Currency
   Dim vAlicuotaIGTF As Currency
   Dim vUsaMF As Boolean
   Dim vCodigoMF As String
   Dim vNroCtrlDesde As String
   Dim vNroCtrlHasta As String
   Dim vSeRetIva As Boolean
   Dim vNroCompRetIvaStr As String
   Dim vNroCompRetIva As Long
   Dim vFechaCompRetIva As Date
   Dim vFechaAplRetIva As Date
   Dim vMtoIvaRetenido As Currency
   Dim vFechaStr As String
   On Error GoTo h_ERROR
   ErrorAlTraducirLaLinea = False
   LineaDeErrores = ""
   linea = refLineaActual
   fDistribuyeLaLineaDeRDV = False
   
   vConsecutivoCompania = mConsecutivoCompaniaActual
   vNumeroComprobanteFiscalDesde = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vNumeroComprobanteFiscalHasta = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vFechaStr = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   If Trim(vFechaStr) <> "" And gUtilDate.fElStringContieneUnaFechaValida(vFechaStr, False) Then
      vFechaRDV = gUtilImport.fToDate(vFechaStr, #1/1/1900#)
   Else
      vFechaRDV = #1/1/1900#
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = " Fecha de documento: Valor/formato inválido para fecha."
   End If
   vNumeroReporteZ = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   
   vStrMontoExento = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vStrMontoBIAlicuotaGeneral = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vStrMontoBIAlicuotaReducida = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vStrMontoBIAlicuotaExtendida = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vStrMontoNCExento = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vStrMontoNCBIAlicuotaGeneral = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vStrMontoNCBIAlicuotaReducida = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vStrMontoNCBIAlicuotaExtendida = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   
   vBIIGTF = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vIGTFML = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vAlicuotaIGTF = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vUsaMF = gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vCodigoMF = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vNroCtrlDesde = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vNroCtrlHasta = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   vSeRetIva = gConvert.ConvertStringToBoolean(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   vNroCompRetIvaStr = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   
   vFechaStr = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   If Trim(vFechaStr) <> "" And gUtilDate.fElStringContieneUnaFechaValida(vFechaStr, False) Then
      vFechaCompRetIva = gUtilImport.fToDate(vFechaStr, #1/1/1900#)
   Else
      vFechaCompRetIva = #1/1/1900#
   End If
   vFechaStr = gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador)
   If Trim(vFechaStr) <> "" And gUtilDate.fElStringContieneUnaFechaValida(vFechaStr, False) Then
      vFechaAplRetIva = gUtilImport.fToDate(vFechaStr, #1/1/1900#)
   Else
      vFechaAplRetIva = #1/1/1900#
   End If
   
   vMtoIvaRetenido = gUtilImport.fToCurrency(gUtilImport.fReadValueUntilDelimiterAndCut(linea, valSeparador))
   If vSeRetIva Then
      vNroCompRetIva = gUtilImport.fToLong(vNroCompRetIvaStr)
   End If
   
   If gTexto.DfLen(vNumeroComprobanteFiscalDesde) = 0 Or gTexto.DfLen(vNumeroComprobanteFiscalHasta) = 0 Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & " Los valores Número Comprobante Desde y Hasta son obligatorios."
   End If
   If gDefGen.LaFechaEsMayorQueElLimiteDeIngresoDeDatos(vFechaRDV, False, insertar) Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & " El valor de la Fecha del documento no es válido."
   End If
   If gTexto.DfLen(vNumeroReporteZ) = 0 Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & " El valor de Número de Reporte Z es obligatorio."
   End If
   If (vStrMontoExento = "" Or vStrMontoExento = "NA") And (vStrMontoBIAlicuotaGeneral = "" Or vStrMontoBIAlicuotaGeneral = "NA") And (vStrMontoBIAlicuotaReducida = "" Or vStrMontoBIAlicuotaReducida = "NA") And (vStrMontoBIAlicuotaExtendida = "" Or vStrMontoBIAlicuotaExtendida = "NA") And (vStrMontoNCExento = "" Or vStrMontoNCExento = "NA") And (vStrMontoNCBIAlicuotaGeneral = "" Or vStrMontoNCBIAlicuotaGeneral = "NA") And (vStrMontoNCBIAlicuotaReducida = "" Or vStrMontoNCBIAlicuotaReducida = "NA") And (vStrMontoNCBIAlicuotaExtendida = "" Or vStrMontoNCBIAlicuotaExtendida = "NA") Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & " Debe proporcionar un valor válido para el detalle del documento."
   ElseIf Not (IsNumeric(vStrMontoExento) Or IsNumeric(vStrMontoBIAlicuotaGeneral) Or IsNumeric(vStrMontoBIAlicuotaReducida) Or IsNumeric(vStrMontoBIAlicuotaExtendida) Or IsNumeric(vStrMontoNCExento) Or IsNumeric(vStrMontoNCBIAlicuotaGeneral) Or IsNumeric(vStrMontoNCBIAlicuotaReducida) Or IsNumeric(vStrMontoNCBIAlicuotaExtendida)) Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & " Debe proporcionar un valor válido para el detalle del documento."
   ElseIf gUtilImport.fToCurrency(vStrMontoExento) < 0 Or gUtilImport.fToCurrency(vStrMontoBIAlicuotaGeneral) < 0 Or gUtilImport.fToCurrency(vStrMontoBIAlicuotaReducida) < 0 Or gUtilImport.fToCurrency(vStrMontoBIAlicuotaExtendida) < 0 Or gUtilImport.fToCurrency(vStrMontoNCExento) < 0 Or gUtilImport.fToCurrency(vStrMontoNCBIAlicuotaGeneral) < 0 Or gUtilImport.fToCurrency(vStrMontoNCBIAlicuotaReducida) < 0 Or gUtilImport.fToCurrency(vStrMontoNCBIAlicuotaExtendida) < 0 Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & " Los montos ingresados deben ser mayor o igual a 0."
   End If
   If (vBIIGTF <> 0 And vIGTFML = 0) Or (vBIIGTF = 0 And vIGTFML <> 0) Or (vAlicuotaIGTF = 0 And (vBIIGTF <> 0 Or vIGTFML <> 0)) Or (vBIIGTF < 0) Or (vIGTFML < 0) Then
      ErrorAlTraducirLaLinea = True
      LineaDeErrores = LineaDeErrores & " Hay inconsistencia en la información de IGTF suministrada."
   End If
   If vUsaMF Then
      vCodigoMF = fConsecutivoStringMaquinaFiscal(vCodigoMF)
      If gTexto.DfLen(vCodigoMF) = 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & " El Código de la Máquina Fiscal debe ser válido."
      End If
   Else
      If gTexto.DfLen(vNroCtrlDesde) = 0 Or gTexto.DfLen(vNroCtrlHasta) = 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & " Hay inconsistencia en la información de Número de Control Desde y Hasta."
      End If
   End If
   If vSeRetIva Then
      If gDefGen.LaFechaEsMayorQueElLimiteDeIngresoDeDatos(vFechaCompRetIva, False, insertar) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & " El valor de la Fecha del Comprobante de Retención de IVA no es válido."
      End If
      If gDefGen.LaFechaEsMayorQueElLimiteDeIngresoDeDatos(vFechaAplRetIva, False, insertar) Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & " El valor de la Fecha de Aplicación de la Retención de IVA no es válido."
      End If
      If vMtoIvaRetenido < 0 Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & " El valor del Monto IVA Retenido no es válido."
      End If
      If vNroCompRetIvaStr = "" Then
         ErrorAlTraducirLaLinea = True
         LineaDeErrores = LineaDeErrores & " El valor del Número de Comprobante de Retención de IVA no es válido."
      End If
   Else
      vNroCompRetIva = 0
      vFechaCompRetIva = gUtilDate.getFechaDeHoy
      vFechaAplRetIva = gUtilDate.fColocaDiaEnFecha(1, gUtilDate.getFechaDeHoy)
      vMtoIvaRetenido = 0
   End If
   If Not ErrorAlTraducirLaLinea Then
      sConstruyeRDV vNumeroComprobanteFiscalDesde, vNumeroComprobanteFiscalHasta, vFechaRDV, vNumeroReporteZ, vBIIGTF, vIGTFML, vAlicuotaIGTF, vUsaMF, vCodigoMF, vNroCtrlDesde, vNroCtrlHasta, vSeRetIva, vNroCompRetIva, vFechaCompRetIva, vFechaAplRetIva, vMtoIvaRetenido
      sConstruyeDetalleRDV vStrMontoExento, vStrMontoBIAlicuotaGeneral, vStrMontoBIAlicuotaReducida, vStrMontoBIAlicuotaExtendida, vStrMontoNCExento, vStrMontoNCBIAlicuotaGeneral, vStrMontoNCBIAlicuotaReducida, vStrMontoNCBIAlicuotaExtendida
   End If
h_EXIT: On Error GoTo 0
    fDistribuyeLaLineaDeRDV = Not ErrorAlTraducirLaLinea
    Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fDistribuyeLaLineaDeRDV", CM_MESSAGE_NAME_FACTURA, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sConstruyeRDV(ByVal valNumeroComprobanteFiscalDesde As String, ByVal valNumeroComprobanteFiscalHasta As String, ByVal valFechaRDV As Date, ByVal valNumeroReporteZ As String, ByVal valBIIGTF As Currency, ByVal valIGTFML As Currency, ByVal valAlicuotaIGTF As Currency, ByVal valUsaMF As Boolean, ByVal valCodigoMF As String, ByVal valNroCtrlDesde As String, ByVal valNroCtrlHasta As String, ByVal valSeRetIva As Boolean, ByVal valNroCompRetIva As Long, ByVal valFechaCompRetIva As Date, ByVal valFechaAplRetIva As Date, ByVal valMtoIvaRetenido As Currency)
   Dim vAlicGeneral As Currency
   Dim vAlicReducida As Currency
   Dim vAlicExtendida As Currency
   On Error GoTo h_ERROR
   vAlicGeneral = gAdmAlicuotaIvaActual.GetAlicuotaIVA(gUtilImport.fToDate(valFechaRDV, #1/1/2000#), enum_TipoDeAlicuota.eTD_ALICUOTAGENERAL)
   vAlicReducida = gAdmAlicuotaIvaActual.GetAlicuotaIVA(gUtilImport.fToDate(valFechaRDV, #1/1/2000#), enum_TipoDeAlicuota.eTD_ALICUOTA2)
   vAlicExtendida = gAdmAlicuotaIvaActual.GetAlicuotaIVA(gUtilImport.fToDate(valFechaRDV, #1/1/2000#), enum_TipoDeAlicuota.eTD_ALICUOTA3)
   With insFactura
      .SetConsecutivoCompania vConsecutivoCompania
      .SetNumero .fGenerateNextNumeroBorrador(gEnumProyecto.enumTipodocumentoFacturaToString(eTF_RESUMENDIARIODEVENTAS))
      .SetTipoDeDocumentoStr gEnumProyecto.enumTipodocumentoFacturaToString(eTF_RESUMENDIARIODEVENTAS)
      .SetStatusFacturaStr gEnumProyecto.enumStatusFacturaToString(eSF_BORRADOR)
      .SetFecha valFechaRDV
      .SetFechaDeEntrega valFechaRDV
      .SetCodigoCliente "RD_Cliente"
      .SetCodigoVendedor gProyParametrosCompania.getCodigoGenericoVendedor
      .SetObservaciones ""
      .SetCodigoNota1 ""
      .SetCodigoNota2 ""
      .SetCodigoMoneda gProyCompaniaActual.GetCodigoMoneda
      .SetNivelDePrecioStr gEnumProyecto.enumNivelDePrecioToString(eND_PRECIO_1)
      .SetCodigoAlmacen gProyParametrosCompania.GetCodigoAlmacenGenerico
      .SetConsecutivoAlmacen fConsecutivoAlmacen(gProyParametrosCompania.GetCodigoAlmacenGenerico)
      .SetInsertadaManualmente False
      .SetCancelada False
      .SetUsarDireccionFiscal True
      .SetNoDirDespachoAimprimir 0
      .SetCambioABolivares 1
      .SetMontoDelAbono 0
      .SetFechaDeVencimiento valFechaRDV
      .SetCondicionesDePago ""
      .SetFacturaHistorica False
      .SetTalonarioStr gEnumProyecto.enumTalonarioToString(eTa_TALONARIO1)
      .SetFormaDeLaInicialStr gEnumProyecto.enumFormaDeLaInicialToString(eFD_POR_MONTO)
      .SetPorcentajeDeLaInicial 0
      .SetNumeroDeCuotas 1
      .SetFormaDePagoStr gEnumProyecto.enumFormadePagoToString(eFP_CONTADO)
      .SetNumDiasDeVencimiento1aCuota 30
      .SetEditarMontoCuota False
      .SetTipoDeTransaccionStr gEnumProyecto.enumTipoDeTransaccionDeLibrosFiscalesToString(eTD_A01_REGISTRO)
      .SetNumeroPlanillaExportacion ""
      .SetNumeroFacturaAfectada ""
      .SetTipoDeVentaStr gEnumProyecto.enumTipoDeVentaToString(eTD_A_CONTRIBUYENTE)
      .SetUsaMaquinaFiscal valUsaMF
      .SetCodigoMaquinaRegistradora valCodigoMF
      .SetNumeroDesde valNumeroComprobanteFiscalDesde
      .SetNumeroHasta valNumeroComprobanteFiscalHasta
      .SetNumeroResumenDiario valNumeroReporteZ
      .SetNumeroControl valNroCtrlDesde
      .SetNumeroControlHasta valNroCtrlHasta
      .SetSeRetuvoIVA valSeRetIva
      If valSeRetIva Then
         .SetMontoIvaRetenido valMtoIvaRetenido
         .SetFechaAplicacionRetIVA valFechaAplRetIva
         .SetNumeroComprobanteRetIVA valNroCompRetIva
         .SetFechaComprobanteRetIVA valFechaCompRetIva
      Else
         .SetMontoIvaRetenido 0
         .SetFechaAplicacionRetIVA valFechaRDV
         .SetNumeroComprobanteRetIVA 0
         .SetFechaComprobanteRetIVA valFechaRDV
      End If
      .SetFacturaConPreciosSinIva False
      .SetConsecutivoCaja 0
      .SetGeneraCobroDirecto False
      .SetCodigoLote mCodigoLote
      .SetReservarMercancia False
      .SetFechaDeRetiro valFechaRDV
      .SetNoDirDespachoAimprimir 0
      .SetVueltoDelCobroDirecto 0
      .SetPorcentajeDescuento1 0
      .SetPorcentajeDescuento2 0
      .SetMontoDescuento1 0
      .SetMontoDescuento2 0
      .SetPorcentajeAlicuota1 vAlicGeneral
      .SetPorcentajeAlicuota2 vAlicReducida
      .SetPorcentajeAlicuota3 vAlicExtendida
      .SetMoneda gProyCompaniaActual.GetCodigoMoneda
      .SetFechaDeFacturaAfectada valFechaRDV
      .SetAplicaDecretoIvaEspecial False
      .SetBaseImponibleIGTF valBIIGTF
      .SetIGTFML valIGTFML
      .SetIGTFME valIGTFML
      .SetAlicuotaIGTF valAlicuotaIGTF
      .SetEsGeneradaPorPuntoDeVenta False
      .SetCodigoMonedaDeCobro gProyCompaniaActual.GetCodigoMoneda
      .SetFechaLimiteCambioAMonedaLocal valFechaRDV
      .SetNombreOperador mNombreDelUsuario
      .SetFechaUltimaModificacion gUtilDate.getFechaDeHoy
   End With
   
h_EXIT: On Error GoTo 0
    Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sConstruyeRDV", CM_MESSAGE_NAME_FACTURA, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fConsecutivoAlmacen(ByVal valCodigoAlmacen) As Long
   Dim vResult As Long
   Dim vRs As ADODB.Recordset
   Dim vSql As String
   On Error GoTo h_ERROR
   vResult = 1
   Set vRs = New ADODB.Recordset
   vSql = "SELECT Consecutivo FROM Saw.Almacen WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual & " AND Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen)
   If gDbUtil.fOpenRecordset(vRs, vSql, Conexion) Then
      If gDbUtil.fRecordCount(vRs) > 0 Then
         vResult = gConvert.fConvertStringToLong(vRs.Fields("Consecutivo").Value)
      End If
   End If
   gDbUtil.sDestroyRecordSet vRs
h_EXIT: On Error GoTo 0
    fConsecutivoAlmacen = vResult
    Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fConsecutivoAlmacen", CM_MESSAGE_NAME_FACTURA, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConsecutivoStringMaquinaFiscal(ByVal valCodigoMaquinaFiscal) As String
   Dim vResult As String
   Dim vRs As ADODB.Recordset
   Dim vSql As String
   On Error GoTo h_ERROR
   vResult = ""
   Set vRs = New ADODB.Recordset
   vSql = "SELECT ConsecutivoMaquinaFiscal FROM Saw.MaquinaFiscal WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual & " AND ConsecutivoMaquinaFiscal = " & gUtilSQL.fSimpleSqlValue(valCodigoMaquinaFiscal)
   If gDbUtil.fOpenRecordset(vRs, vSql, Conexion) Then
      If gDbUtil.fRecordCount(vRs) > 0 Then
         vResult = gConvert.fConvierteAString(vRs.Fields("ConsecutivoMaquinaFiscal").Value)
      End If
   End If
   gDbUtil.sDestroyRecordSet vRs
h_EXIT: On Error GoTo 0
    fConsecutivoStringMaquinaFiscal = vResult
    Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fConsecutivoStringMaquinaFiscal", CM_MESSAGE_NAME_FACTURA, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sConstruyeDetalleRDV(ByVal valStrMontoExento As String, ByVal valStrMontoBIAlicuotaGeneral As String, ByVal valStrMontoBIAlicuotaReducida As String, ByVal valStrMontoBIAlicuotaExtendida As String, ByVal valStrMontoNCExento As String, ByVal valStrMontoNCBIAlicuotaGeneral As String, ByVal valStrMontoNCBIAlicuotaReducida As String, ByVal valStrMontoNCBIAlicuotaExtendida As String)
   Dim vCodigoArticuloRDV As String
   Dim vDescripcionArticuloRDV As String
   Dim vPorcentajeAlicuota As Currency
   Dim vTipoDeAlicuotaStr As String
   Dim vPSI As Currency
   Dim vPCI As Currency
   Dim vUno As Integer
   Dim vConsecRenglon As Long
   Dim i As Integer
   Dim vRs As ADODB.Recordset
   On Error GoTo h_ERROR
   Set vRs = New ADODB.Recordset
   vRs.Fields.Append "Articulo", adVarChar, 30
   vRs.Fields.Append "Descripcion", adVarChar, 7000
   vRs.Fields.Append "AlicuotaIVA", adChar, 1
   vRs.Fields.Append "PorcentajeAlicuota", adCurrency
   vRs.Fields.Append "Cantidad", adCurrency
   vRs.Fields.Append "PorcentajeDescuento", adCurrency
   vRs.Fields.Append "PorcentajeBaseImponible", adCurrency
   vRs.Fields.Append "PrecioSinIVA", adCurrency
   vRs.Fields.Append "PrecioConIVA", adCurrency
   vRs.Fields.Append "TotalRenglon", adCurrency
   vRs.Fields.Append "Serial", adVarChar, 50
   vRs.Fields.Append "Rollo", adVarChar, 20
   vRs.Open
   For i = enum_TipoDeAlicuota.eTD_EXENTO To enum_TipoDeAlicuota.eTD_ALICUOTA3NC
      vPSI = 0
      Select Case i
         Case eTD_EXENTO:
            If gTexto.DfLen(valStrMontoExento) = 0 Or gTexto.fS1EsIgualAS2(valStrMontoExento, "NA") Then
               GoTo Exit_For
            End If
            vPSI = gUtilImport.fToCurrency(valStrMontoExento)
            vPorcentajeAlicuota = 0
            vTipoDeAlicuotaStr = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_EXENTO)
         Case eTD_ALICUOTAGENERAL:
            If gTexto.DfLen(valStrMontoBIAlicuotaGeneral) = 0 Or gTexto.fS1EsIgualAS2(valStrMontoBIAlicuotaGeneral, "NA") Then
               GoTo Exit_For
            End If
            vPSI = gUtilImport.fToCurrency(valStrMontoBIAlicuotaGeneral)
            vPorcentajeAlicuota = gAdmAlicuotaIvaActual.GetAlicuotaIVA(insFactura.GetFecha, eTD_ALICUOTAGENERAL)
            vTipoDeAlicuotaStr = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTAGENERAL)
         Case eTD_ALICUOTA2:
            If gTexto.DfLen(valStrMontoBIAlicuotaReducida) = 0 Or gTexto.fS1EsIgualAS2(valStrMontoBIAlicuotaReducida, "NA") Then
               GoTo Exit_For
            End If
            vPSI = gUtilImport.fToCurrency(valStrMontoBIAlicuotaReducida)
            vPorcentajeAlicuota = gAdmAlicuotaIvaActual.GetAlicuotaIVA(insFactura.GetFecha, eTD_ALICUOTA2)
            vTipoDeAlicuotaStr = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTA2)
         Case eTD_ALICUOTA3:
            If gTexto.DfLen(valStrMontoBIAlicuotaExtendida) = 0 Or gTexto.fS1EsIgualAS2(valStrMontoBIAlicuotaExtendida, "NA") Then
               GoTo Exit_For
            End If
            vPSI = gUtilImport.fToCurrency(valStrMontoBIAlicuotaExtendida)
            vPorcentajeAlicuota = gAdmAlicuotaIvaActual.GetAlicuotaIVA(insFactura.GetFecha, eTD_ALICUOTA3)
            vTipoDeAlicuotaStr = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTA3)
         Case eTD_EXENTONC:
            If gTexto.DfLen(valStrMontoNCExento) = 0 Or gTexto.fS1EsIgualAS2(valStrMontoNCExento, "NA") Then
               GoTo Exit_For
            End If
            vPSI = gUtilImport.fToCurrency(valStrMontoNCExento)
            vPorcentajeAlicuota = 0
            vTipoDeAlicuotaStr = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_EXENTO)
         Case eTD_ALICUOTAGENERALNC:
            If gTexto.DfLen(valStrMontoNCBIAlicuotaGeneral) = 0 Or gTexto.fS1EsIgualAS2(valStrMontoNCBIAlicuotaGeneral, "NA") Then
               GoTo Exit_For
            End If
            vPSI = gUtilImport.fToCurrency(valStrMontoNCBIAlicuotaGeneral)
            vPorcentajeAlicuota = gAdmAlicuotaIvaActual.GetAlicuotaIVA(insFactura.GetFecha, eTD_ALICUOTAGENERAL)
            vTipoDeAlicuotaStr = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTAGENERAL)
         Case eTD_ALICUOTA2NC:
            If gTexto.DfLen(valStrMontoNCBIAlicuotaReducida) = 0 Or gTexto.fS1EsIgualAS2(valStrMontoNCBIAlicuotaReducida, "NA") Then
               GoTo Exit_For
            End If
            vPSI = gUtilImport.fToCurrency(valStrMontoNCBIAlicuotaReducida)
            vPorcentajeAlicuota = gAdmAlicuotaIvaActual.GetAlicuotaIVA(insFactura.GetFecha, eTD_ALICUOTA2)
            vTipoDeAlicuotaStr = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTA2)
         Case eTD_ALICUOTA3NC:
            If gTexto.DfLen(valStrMontoNCBIAlicuotaExtendida) = 0 Or gTexto.fS1EsIgualAS2(valStrMontoNCBIAlicuotaExtendida, "NA") Then
               GoTo Exit_For
            End If
            vPSI = gUtilImport.fToCurrency(valStrMontoNCBIAlicuotaExtendida)
            vPorcentajeAlicuota = gAdmAlicuotaIvaActual.GetAlicuotaIVA(insFactura.GetFecha, eTD_ALICUOTA3)
            vTipoDeAlicuotaStr = gEnumProyecto.enumTipoDeAlicuotaToString(eTD_ALICUOTA3)
         Case Else: GoTo Exit_For
      End Select
      
      vConsecRenglon = i + 1
      vUno = 1
      If i > enum_TipoDeAlicuota.eTD_ALICUOTA3 Then
         vUno = -1
      End If
      vCodigoArticuloRDV = insArticuloInventario.getCodigoArticuloResumenDiario(i)
      vDescripcionArticuloRDV = insArticuloInventario.getDescripcionArticuloResumenDiario(i)
      
      vPSI = Math.Abs(vPSI)
      vPCI = gUtilMathOperations.fRedondeaANDecimales(vPSI * (1 + (vPorcentajeAlicuota / 100)), 2)
      
      insRenglonFactura.SetArticulo vCodigoArticuloRDV
      insRenglonFactura.SetDescripcion vDescripcionArticuloRDV
      insRenglonFactura.SetAlicuotaIVAStr vTipoDeAlicuotaStr
      insRenglonFactura.SetPorcentajeAlicuota vPorcentajeAlicuota
      insRenglonFactura.SetCantidad vUno
      insRenglonFactura.SetPorcentajeDescuento 0
      insRenglonFactura.SetPorcentajeBaseImponible 100
      insRenglonFactura.SetPrecioSinIVA vPSI
      insRenglonFactura.SetPrecioConIVA vPCI
      insRenglonFactura.SetTotalRenglon vPSI * vUno
      insRenglonFactura.SetSerial ""
      insRenglonFactura.SetRollo ""
   
      vRs.AddNew
      vRs("Articulo").Value = insRenglonFactura.GetArticulo
      vRs("Descripcion").Value = insRenglonFactura.GetDescripcion
      vRs("AlicuotaIVA").Value = insRenglonFactura.GetAlicuotaIVAChar
      vRs("PorcentajeAlicuota").Value = insRenglonFactura.GetPorcentajeAlicuota
      vRs("Cantidad").Value = insRenglonFactura.GetCantidad
      vRs("PorcentajeDescuento").Value = insRenglonFactura.GetPorcentajeDescuento
      vRs("PorcentajeBaseImponible").Value = insRenglonFactura.GetPorcentajeBaseImponible
      vRs("PrecioSinIVA").Value = insRenglonFactura.GetPrecioSinIVA
      vRs("PrecioConIVA").Value = insRenglonFactura.GetPrecioConIVA
      vRs("TotalRenglon").Value = insRenglonFactura.GetTotalRenglon
      vRs("Serial").Value = insRenglonFactura.GetSerial
      vRs("Rollo").Value = insRenglonFactura.GetRollo
      vRs.Update
Exit_For: 'llega hasta acá para saltar el loop del for
   Next
   insRenglonFactura.SetRsRenglonFactura vRs
h_EXIT: On Error GoTo 0
    Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "sConstruyeDetalleRDV", CM_MESSAGE_NAME_FACTURA, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fImportaRDV() As Boolean
   Dim vResult As Boolean
   Dim i As Long
   Dim vRs As ADODB.Recordset
   Dim vPSI As Currency
   Dim vPCI As Currency
   On Error GoTo h_ERROR
   vResult = False
   If Not gTexto.DfLen(insFactura.GetNumero = 0) Then
      If Not insFactura.fSearchByField("Numero", insFactura.GetNumero, False, False) Then
         insFactura.SetCodigoLote mCodigoLote
         If insFactura.fInsert(insertar, True, False) Then
            i = 1
            Set vRs = insRenglonFactura.GetRsRenglonFactura
            If gDbUtil.fRecordCount(vRs) > 0 Then
               vRs.MoveFirst
               While Not vRs.EOF
                  vPSI = gConvert.fConvierteACurrency(vRs("PrecioSinIVA").Value)
                  vPCI = gConvert.fConvierteACurrency(vRs("PrecioConIVA").Value)
                  insRenglonFactura.SetRsRenglonFactura Nothing
                  insRenglonFactura.SetConsecutivoCompania insFactura.GetConsecutivoCompania
                  insRenglonFactura.SetTipoDeDocumentoStr insFactura.GetTipoDeDocumentoStr
                  insRenglonFactura.SetNumeroFactura insFactura.GetNumero
                  insRenglonFactura.SetConsecutivoRenglon i
                  insRenglonFactura.SetArticulo gConvert.fConvierteAString(vRs("Articulo").Value)
                  insRenglonFactura.SetDescripcion gConvert.fConvierteAString(vRs("Descripcion").Value)
                  insRenglonFactura.SetAlicuotaIVAStr gEnumProyecto.enumTipoDeAlicuotaToString(gConvert.charAEnumerativoInt(gConvert.fConvierteAString(vRs("AlicuotaIVA").Value)))
                  insRenglonFactura.SetPorcentajeAlicuota gConvert.fConvierteACurrency(vRs("PorcentajeAlicuota").Value)
                  insRenglonFactura.SetCantidad gConvert.fConvierteACurrency(vRs("Cantidad").Value)
                  insRenglonFactura.SetPorcentajeDescuento 0
                  insRenglonFactura.SetPorcentajeBaseImponible 100
                  insRenglonFactura.SetPrecioSinIVA vPSI
                  insRenglonFactura.SetPrecioConIVA vPCI
                  insRenglonFactura.SetTotalRenglon vPSI
                  insRenglonFactura.SetSerial ""
                  insRenglonFactura.SetRollo ""
                  insRenglonFactura.SetCodigoVendedor1 ""
                  insRenglonFactura.SetCodigoVendedor2 ""
                  insRenglonFactura.SetCodigoVendedor3 ""
                  insRenglonFactura.SetRsRenglonFactura Nothing
                  insRenglonFactura.fInsert
                  i = i + 1
                  vRs.MoveNext
               Wend
            End If
            vResult = True
            If insFactura.fSearchByField("Numero", insFactura.GetNumero, True, False) Then
               insFactura.sCalculaTotalesFactura False
               insFactura.SetMontoDeLasCuotas insFactura.GetTotalFactura
               insFactura.SetMontoUltimaCuota insFactura.GetTotalFactura
               insFactura.fUpdateRecord False, False, Modificar, False, Nothing, insFactura.GetNumero, eTF_RESUMENDIARIODEVENTAS, ""
            End If
         
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   fImportaRDV = vResult
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsImportarDatosSAW", "fImportaRDV", CM_MESSAGE_NAME_FACTURA, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
