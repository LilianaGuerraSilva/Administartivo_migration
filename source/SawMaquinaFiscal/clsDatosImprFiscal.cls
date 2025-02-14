VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsDatosImprFiscal"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit
Private mNombreCliente As String
Private mNumeroRIFCliente As String
Private mRsItems As ADODB.Recordset
Private mTipoComprobante As String
Private mImpresoraFiscal As Enum_ImpresorasFiscales
Private mState As String
Private mFormDePago As ADODB.Recordset
Private mPorcentajeDeDescuento As String
Private mMontoDelPago As String
Private mSerialImpresoraFiscal As String
Private mNumeroTicket As String
Private mNumeroSerie As String
Private mUltimoNumeroFiscal As String
Private mTelefono As String
Private mDireccion As String
Private mHoraModificacion As String
Private mFecha As String
Private mTipoDocumento As enum_TipoDocumentoFactura
Private mPuerto As String
Private mPuertoImpresoraFiscal As String
Private mImprimeDireccionAlFinalDelComprobanteFiscal As Boolean
Private mObservaciones As String
Private mRSCamposDefinibles As ADODB.Recordset
Private mImprimeCamposDefinibles As Boolean
Private mRSDatosVehiculos As ADODB.Recordset
Private mImprimeDatosVehiculos As Boolean
Private mFechaFacturaAfectada As String
Private mImprimeSerialRolloLuegoDeDescripArticulo As Boolean
Private mUltimoReporteZ As String
Private mTipoConexion As enum_TipoConexion
Private mIp As String
Private mGateway As String
Private mMascaraSubRed As String
Private mCajaNumero As String
Private mPermitirDescripcionDelArticuloExtendida As Boolean
Private mPermitirNombreDelClienteExtendido As Boolean
Private mUsarModoDotNet As Boolean
Private mTotalFactura As Currency
Private mTotalesMonedaExtranjera As String
Private mCodigoMonedaCompania As String
Private mBaseImponibleIGTF As Currency
Private mTotalAPagar As Currency
Private mIGTFML As Currency
Private mRegistroDeRetornoEnTxt As Boolean
Private mListaVouchersDePago As String

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsDatosImprFiscal"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Datos Para Impresión Fiscal"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Sub Class_Initialize()
   On Error GoTo h_Error
   sClrRecord
h_EXIT:    On Error GoTo 0
   Exit Sub
h_Error: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Class_Initialize", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Class_Terminate()
   On Error GoTo h_Error
   sClrRecord
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Class_Initialize", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sClrRecord()
    mNombreCliente = ""
    mNumeroRIFCliente = ""
    mTipoComprobante = ""
    mState = ""
    mPorcentajeDeDescuento = ""
    mMontoDelPago = ""
    mSerialImpresoraFiscal = ""
    mNumeroTicket = ""
    mNumeroSerie = ""
    mUltimoNumeroFiscal = ""
    mTelefono = ""
    mDireccion = ""
    mHoraModificacion = ""
    mFecha = ""
    mTipoDocumento = enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL
    mPuerto = ""
    mPuertoImpresoraFiscal = ""
    mImprimeDireccionAlFinalDelComprobanteFiscal = True
    mObservaciones = ""
    mImprimeCamposDefinibles = True
    mImprimeDatosVehiculos = True
    mFechaFacturaAfectada = ""
    mImprimeSerialRolloLuegoDeDescripArticulo = True
    mUltimoReporteZ = ""
    mTipoConexion = 0
    mIp = ""
    mGateway = ""
    mMascaraSubRed = ""
    mCajaNumero = ""
    mPermitirDescripcionDelArticuloExtendida = True
    mPermitirNombreDelClienteExtendido = True
    mUsarModoDotNet = True
    mTotalFactura = 0
    mTotalesMonedaExtranjera = ""
    mCodigoMonedaCompania = ""
End Sub

Public Sub SetNombreCliente(ByVal newNombreCliente As String)
   mNombreCliente = newNombreCliente
End Sub

Public Function GetNombreCliente() As String
   GetNombreCliente = mNombreCliente
End Function

Public Sub SetNumeroRIFCliente(ByVal newNumeroRIFCliente As String)
   mNumeroRIFCliente = newNumeroRIFCliente
End Sub

Public Function GetNumeroRIFCliente() As String
   GetNumeroRIFCliente = mNumeroRIFCliente
End Function

Public Sub SetItems(ByVal newItems As ADODB.Recordset)
   Set mRsItems = newItems
End Sub

Public Function GetItems() As ADODB.Recordset
   Set GetItems = mRsItems
End Function

Public Sub SetImpresoraFiscal(ByVal newImpresoraFiscal As Enum_ImpresorasFiscales)
   mImpresoraFiscal = newImpresoraFiscal
End Sub

Public Function GetImpresoraFiscal() As Enum_ImpresorasFiscales
   GetImpresoraFiscal = mImpresoraFiscal
End Function

Public Sub SetTipoComprobante(ByVal newTipoComprobante As String)
   mTipoComprobante = newTipoComprobante
End Sub

Public Function GetTipoComprobante() As String
   GetTipoComprobante = mTipoComprobante
End Function

Public Sub SetEstado(ByVal newState As Enum_EstadoImpresionFiscal)
   mState = newState
End Sub

Public Function GetEstado() As Enum_EstadoImpresionFiscal
   GetEstado = mState
End Function

Public Sub SetFormaDePago(ByVal newFormDePago As ADODB.Recordset)
   Set mFormDePago = newFormDePago
End Sub

Public Function GetFormaDePago() As ADODB.Recordset
   Set GetFormaDePago = mFormDePago
End Function

Public Sub SetPorcentajeDeDescuento(ByVal newPorcentajeDeDescuento As String)
   mPorcentajeDeDescuento = newPorcentajeDeDescuento
End Sub

Public Function GetPorcentajeDeDescuento() As String
   GetPorcentajeDeDescuento = mPorcentajeDeDescuento
End Function

Public Sub SetMontoDelPago(ByVal newMontoDelPago As String)
   mMontoDelPago = newMontoDelPago
End Sub

Public Function GetMontoDelPago() As String
   GetMontoDelPago = mMontoDelPago
End Function

Public Sub SetSerialImpresoraFiscal(ByVal newSerialImpresoraFiscal As String)
   mSerialImpresoraFiscal = newSerialImpresoraFiscal
End Sub

Public Function GetSerialImpresoraFiscal() As String
   GetSerialImpresoraFiscal = mSerialImpresoraFiscal
End Function

Public Sub SetNumeroTicket(ByVal newNumeroTicket As String)
   mNumeroTicket = newNumeroTicket
End Sub

Public Function GetNumeroTicket() As String
   GetNumeroTicket = mNumeroTicket
End Function

Public Sub SetUltimoNumeroFiscal(ByVal newUltimoNumeroFiscal As String)
   mUltimoNumeroFiscal = newUltimoNumeroFiscal
End Sub

Public Function GetUltimoNumeroFiscal() As String
   GetUltimoNumeroFiscal = mUltimoNumeroFiscal
End Function

Public Sub SetNumeroSerie(ByVal newNumeroSerie As String)
   mNumeroSerie = newNumeroSerie
End Sub

Public Function GetNumeroSerie() As String
   GetNumeroSerie = mNumeroSerie
End Function

Public Sub SetTelefono(ByVal newTelefono As String)
   mTelefono = newTelefono
End Sub

Public Function GetTelefono() As String
   GetTelefono = mTelefono
End Function

Public Sub SetDireccion(ByVal newDireccion As String)
   mDireccion = newDireccion
End Sub

Public Function GetDireccion() As String
   GetDireccion = mDireccion
End Function
Public Sub SetHoraModificacion(ByVal newHoraModificacion As String)
   mHoraModificacion = newHoraModificacion
End Sub

Public Function GetHoraModificacion() As String
   GetHoraModificacion = mHoraModificacion
End Function

Public Sub SetFecha(ByVal newFecha As String)
   mFecha = newFecha
End Sub

Public Function GetFecha() As String
   GetFecha = mFecha
End Function

Public Sub SetTipoDocumentoAsEnum(ByVal newTipoDocumento As enum_TipoDocumentoFactura)
   mTipoDocumento = newTipoDocumento
End Sub

Public Function GetTipoDocumentoAsEnum() As enum_TipoDocumentoFactura
   GetTipoDocumentoAsEnum = mTipoDocumento
End Function

Public Function GetEsNotaDeCredito() As Boolean
   GetEsNotaDeCredito = (mTipoDocumento = eTF_NOTADECREDITOCOMPROBANTEFISCAL)
End Function

Public Sub SetPuerto(ByVal newPuerto As String)
   mPuerto = newPuerto
End Sub

Public Function GetPuerto() As String
   GetPuerto = mPuerto
End Function
Public Sub SetPuertoImpresoraFiscal(ByVal newPuerto As String)
   mPuertoImpresoraFiscal = newPuerto
End Sub

Public Function GetPuertoImpresoraFiscal() As String
   GetPuertoImpresoraFiscal = mPuertoImpresoraFiscal
End Function

Public Sub SetImprimeDireccionAlFinalDelComprobanteFiscal(ByVal newImprimeDireccionAlFinalDelComprobanteFiscal As Boolean)
   mImprimeDireccionAlFinalDelComprobanteFiscal = newImprimeDireccionAlFinalDelComprobanteFiscal
End Sub

Public Function GetImprimeDireccionAlFinalDelComprobanteFiscal() As Boolean
   GetImprimeDireccionAlFinalDelComprobanteFiscal = mImprimeDireccionAlFinalDelComprobanteFiscal
End Function

Public Sub SetObservaciones(ByVal newObservaciones As String)
   mObservaciones = newObservaciones
End Sub

Public Function GetObservaciones() As String
   GetObservaciones = mObservaciones
End Function

Public Sub SetRSCamposDefinibles(ByVal newRSCamposDefinibles As ADODB.Recordset)
   Set mRSCamposDefinibles = newRSCamposDefinibles
End Sub

Public Function GetRSCamposDefinibles() As ADODB.Recordset
   Set GetRSCamposDefinibles = mRSCamposDefinibles
End Function

Public Sub SetImprimeCamposDefinibles(ByVal newImprimeCamposDefinibles As Boolean)
   mImprimeCamposDefinibles = newImprimeCamposDefinibles
End Sub

Public Function GetImprimeCamposDefinibles() As Boolean
   GetImprimeCamposDefinibles = mImprimeCamposDefinibles
End Function

Public Sub SetRSDatosVehiculos(ByVal newRSDatosVehiculos As ADODB.Recordset)
   Set mRSDatosVehiculos = newRSDatosVehiculos
End Sub

Public Function GetRSDatosVehiculos() As ADODB.Recordset
   Set GetRSDatosVehiculos = mRSDatosVehiculos
End Function

Public Sub SetImprimeDatosVehiculos(ByVal newImprimeDatosVehiculos As Boolean)
   mImprimeDatosVehiculos = newImprimeDatosVehiculos
End Sub

Public Function GetImprimeDatosVehiculos() As Boolean
   GetImprimeDatosVehiculos = mImprimeDatosVehiculos
End Function

Public Sub SetFechaFacturaAfectada(ByVal newFechaFacturaAfectada As String)
   mFechaFacturaAfectada = newFechaFacturaAfectada
End Sub

Public Function GetFechaFacturaAfectada() As String
   GetFechaFacturaAfectada = mFechaFacturaAfectada
End Function

Public Sub SetImprimeSerialRolloLuegoDeDescripArticulo(ByVal newImprimeSerialRolloLuegoDeDescripArticulo As Boolean)
   mImprimeSerialRolloLuegoDeDescripArticulo = newImprimeSerialRolloLuegoDeDescripArticulo
End Sub

Public Function GetImprimeSerialRolloLuegoDeDescripArticulo() As Boolean
   GetImprimeSerialRolloLuegoDeDescripArticulo = mImprimeSerialRolloLuegoDeDescripArticulo
End Function

Public Sub SetUltimoReporteZ(ByVal newUltimoReporteZ As String)
   mUltimoReporteZ = newUltimoReporteZ
End Sub

Public Function GetUltimoReporteZ() As String
   GetUltimoReporteZ = mUltimoReporteZ
End Function

Public Sub SetTipoConexion(ByVal newTipoConexion As enum_TipoConexion)
   mTipoConexion = newTipoConexion
End Sub

Public Function GetTipoConexion() As enum_TipoConexion
   GetTipoConexion = mTipoConexion
End Function

Public Sub SetIp(ByVal newIp As String)
   mIp = newIp
End Sub

Public Function GetIp() As String
   GetIp = mIp
End Function

Public Sub SetCajaNumero(ByVal newCajaNumero As String)
   mCajaNumero = newCajaNumero
End Sub

Public Function GetCajaNumero() As String
   GetCajaNumero = mCajaNumero
End Function

Public Sub SetGateway(ByVal newGateway As String)
   mGateway = newGateway
End Sub

Public Function GetGateway() As String
   GetGateway = mGateway
End Function

Public Sub SetMascaraSubRed(ByVal newMascaraSubRed As String)
   mMascaraSubRed = newMascaraSubRed
End Sub

Public Function GetMascaraSubRed() As String
   GetMascaraSubRed = mMascaraSubRed
End Function

Public Sub SetPermitirDescripcionDelArticuloExtendida(ByVal newPermitirDescripcionDelArticuloExtendida As Boolean)
   mPermitirDescripcionDelArticuloExtendida = newPermitirDescripcionDelArticuloExtendida
End Sub

Public Function GetPermitirDescripcionDelArticuloExtendida() As Boolean
   GetPermitirDescripcionDelArticuloExtendida = mPermitirDescripcionDelArticuloExtendida
End Function

Public Sub SetPermitirNombreDelClienteExtendido(ByVal newPermitirNombreDelClienteExtendido As Boolean)
   mPermitirNombreDelClienteExtendido = newPermitirNombreDelClienteExtendido
End Sub

Public Function GetPermitirNombreDelClienteExtendido() As Boolean
   GetPermitirNombreDelClienteExtendido = mPermitirNombreDelClienteExtendido
End Function

Public Sub SetUsarModoDotNet(ByVal newUsarModoDotNet As Boolean)
   mUsarModoDotNet = newUsarModoDotNet
End Sub

Public Function GetUsarModoDotNet() As Boolean
   GetUsarModoDotNet = mUsarModoDotNet
End Function

Public Sub SetTotalFactura(ByVal newTotalFactura As Currency)
   mTotalFactura = newTotalFactura
End Sub

Public Function GetTotalFactura() As Currency
   GetTotalFactura = mTotalFactura
End Function

Public Sub SetTotalesMonedaExtranjera(ByVal newTotalesMonedaExtranjera As String)
   mTotalesMonedaExtranjera = newTotalesMonedaExtranjera
End Sub

Public Function GetTotalesMonedaExtranjera() As String
   GetTotalesMonedaExtranjera = mTotalesMonedaExtranjera
End Function

Public Sub SetCodigoMonedaCompania(ByVal newCodigoMonedaCompania As String)
   mCodigoMonedaCompania = newCodigoMonedaCompania
End Sub

Public Function GetCodigoMonedaCompania() As String
   GetCodigoMonedaCompania = mCodigoMonedaCompania
End Function

Public Sub SetBaseImponibleIGTF(ByVal newBaseImponibleIGTF As Currency)
   mBaseImponibleIGTF = newBaseImponibleIGTF
End Sub

Public Function GetBaseImponibleIGTF() As Currency
   GetBaseImponibleIGTF = mBaseImponibleIGTF
End Function

Public Sub SetTotalAPagar(ByVal newTotalAPagar As Currency)
   mTotalAPagar = newTotalAPagar
End Sub

Public Function GetTotalAPagar() As Currency
   GetTotalAPagar = mTotalAPagar
End Function

Public Sub SetIGTFML(ByVal newIGTFML As Currency)
   mIGTFML = newIGTFML
End Sub

Public Function GetIGTFML() As Currency
   GetIGTFML = mIGTFML
End Function

Public Sub SetRegistroDeRetornoEnTxt(ByVal newRegistroDeRetornoEnTxt As Boolean)
   mRegistroDeRetornoEnTxt = newRegistroDeRetornoEnTxt
End Sub

Public Function GetRegistroDeRetornoEnTxt() As Boolean
   GetRegistroDeRetornoEnTxt = mRegistroDeRetornoEnTxt
End Function

Public Sub SetListaVouchersDePago(ByVal newListaVouchersDePago As String)
   mListaVouchersDePago = newListaVouchersDePago
End Sub

Public Function GetListaVouchersDePago() As String
   GetListaVouchersDePago = mListaVouchersDePago
End Function

