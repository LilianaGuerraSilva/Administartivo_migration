VERSION 5.00
Object = "{648A5603-2C6E-101B-82B6-000000000014}#1.1#0"; "MSCOMM32.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.1#0"; "MSCOMCTL.OCX"
Object = "{18B1931C-D536-4582-BC33-49A47A9683D0}#3.0#0"; "VMAX.ocx"
Begin VB.Form frmImpresorasFiscales 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   1710
   ClientLeft      =   45
   ClientTop       =   45
   ClientWidth     =   6495
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1710
   ScaleWidth      =   6495
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VMAXOCX.VMAX obVMAX 
      Left            =   5760
      Top             =   1320
      _ExtentX        =   1402
      _ExtentY        =   1349
      NAutoGaveta     =   "0"
      NPuertoVisor    =   "0"
   End
   Begin VB.CommandButton cmdSalir 
      Caption         =   "&Aceptar"
      Height          =   375
      Left            =   2280
      TabIndex        =   2
      Top             =   1320
      Visible         =   0   'False
      Width           =   1695
   End
   Begin MSComctlLib.ProgressBar pgrImprimir 
      Height          =   255
      Left            =   240
      TabIndex        =   1
      Top             =   960
      Width           =   6015
      _ExtentX        =   10610
      _ExtentY        =   450
      _Version        =   393216
      Appearance      =   1
      Max             =   10
      Scrolling       =   1
   End
   Begin MSCommLib.MSComm cmmPuerto 
      Left            =   4920
      Top             =   1320
      _ExtentX        =   1005
      _ExtentY        =   1005
      _Version        =   393216
      DTREnable       =   -1  'True
   End
   Begin VB.Label lblPrueba 
      Height          =   255
      Left            =   360
      TabIndex        =   3
      Top             =   1440
      Width           =   1575
   End
   Begin VB.Image Image1 
      Height          =   795
      Left            =   120
      Picture         =   "frmImpresorasFiscales.frx":0000
      Stretch         =   -1  'True
      Top             =   0
      Width           =   735
   End
   Begin VB.Label lblImpresion 
      BackColor       =   &H00FFFFFF&
      Caption         =   "Verificando Conexión con la Impresora"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   615
      Left            =   960
      TabIndex        =   0
      Top             =   120
      Width           =   5175
   End
End
Attribute VB_Name = "frmImpresorasFiscales"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Option Explicit
'**********************************************************************************************************************************************
' FUNCIONES DE API DE WINDOWS
'==============================================================================================================================================
'Función api que recupera un valor-dato de un archivo Ini
Private Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" ( _
   ByVal lpApplicationName As String, _
   ByVal lpKeyName As String, _
   ByVal lpDefault As String, _
   ByVal lpReturnedString As String, _
   ByVal nSize As Long, _
   ByVal lpFileName As String) As Long
'**********************************************************************************************************************************************
' DELCARACIONES DE FUNCIONES PUBLICAS PARA TRABAJAR CON LA IMPRESORA FISCAL BEMATECH MP-20 FI II
'==============================================================================================================================================

' Funciones de Inicialización

Private Declare Function Bematech_FI_CambiaSimboloMoneda Lib "BEMAFI32.DLL" (ByVal SimboloMoneda As String) As Integer
Private Declare Function Bematech_FI_ProgramaAlicuota Lib "BEMAFI32.DLL" (ByVal Alicuota As String, ByVal ICMS_ISS As Integer) As Integer
Private Declare Function Bematech_FI_ProgramaHorarioDeVerano Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_CrearDepartamento Lib "BEMAFI32.DLL" (ByVal Indice As Integer, ByVal Departamento As String) As Integer
Private Declare Function Bematech_FI_CrearTotalizadorSinIcms Lib "BEMAFI32.DLL" (ByVal Indice As Integer, ByVal Totalizador As String) As Integer
Private Declare Function Bematech_FI_ProgramaRedondeo Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_ProgramaTruncamiento Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_LineasEntreCupones Lib "BEMAFI32.DLL" (ByVal Lineas As Integer) As Integer
Private Declare Function Bematech_FI_EspacioEntreLineas Lib "BEMAFI32.DLL" (ByVal Dots As Integer) As Integer
Private Declare Function Bematech_FI_FuerzaImpactoAgujas Lib "BEMAFI32.DLL" (ByVal FuerzaImpacto As Integer) As Integer

' Funciones do Cupon Fiscal
Private Declare Function Bematech_FI_NumeroComprobanteFiscal Lib "BEMAFI32.DLL" (ByVal NumeroComprobante As String) As Integer

Private Declare Function Bematech_FI_AbreCupon Lib "BEMAFI32.DLL" (ByVal Rif As String) As Integer
Private Declare Function Bematech_FI_AbreComprobanteDeVenta Lib "BEMAFI32.DLL" (ByVal Rif As String, ByVal Nombre As String) As Integer
Private Declare Function Bematech_FI_AbreComprobanteDeVentaEx Lib "BEMAFI32.DLL" (ByVal Rif As String, ByVal Nombre As String, ByVal Direccion As String) As Integer
Private Declare Function Bematech_FI_AbreNotaDeCredito Lib "BEMAFI32.DLL" (ByVal Nombre As String, ByVal NumeroSerie As String, ByVal Rif As String, ByVal Dia As String, ByVal Mes As String, ByVal Ano As String, ByVal Hora As String, ByVal Minuto As String, ByVal Secundo As String, ByVal COO As String) As Integer
Private Declare Function Bematech_FI_CierraNotaDeCredito Lib "BEMAFI32.DLL" (ByVal msg As String) As Integer
Private Declare Function Bematech_FI_DevolucionArticulo Lib "BEMAFI32.DLL" (ByVal Codigo As String, ByVal Descripcion As String, ByVal Alicuota As String, ByVal TipoCantidad As String, ByVal Cantidad As String, ByVal CasasDecimales As Integer, ByVal ValorUnit As String, ByVal TIPODESCUENTO As String, ByVal ValorDesc As String) As Integer
Private Declare Function Bematech_FI_VendeArticulo Lib "BEMAFI32.DLL" (ByVal Codigo As String, ByVal Descripcion As String, ByVal Alicuota As String, ByVal TipoCantidad As String, ByVal Cantidad As String, ByVal CasasDecimales As Integer, ByVal ValorUnitario As String, ByVal TIPODESCUENTO As String, ByVal Descuento As String) As Integer
Private Declare Function Bematech_FI_AnulaArticuloAnterior Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_AnulaArticuloGenerico Lib "BEMAFI32.DLL" (ByVal NumeroArticulo As String) As Integer
Private Declare Function Bematech_FI_AnulaCupon Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_CierraCuponReducido Lib "BEMAFI32.DLL" (ByVal FormaPago As String, ByVal Mensaje As String) As Integer
Private Declare Function Bematech_FI_CierraCupon Lib "BEMAFI32.DLL" (ByVal FormaPago As String, ByVal DescuentoIncremento As String, ByVal TipoDescuentoIncremento As String, ByVal ValorIncrementoDescuento As String, ByVal ValorPago As String, ByVal Mensaje As String) As Integer
Private Declare Function Bematech_FI_VendeArticuloDepartamento Lib "BEMAFI32.DLL" (ByVal Codigo As String, ByVal Descripcion As String, ByVal Alicuota As String, ByVal ValorUnitario As String, ByVal Cantidad As String, ByVal Acrecimo As String, ByVal Descuento As String, ByVal IndiceDepartamento As String, ByVal UnidadMedida As String) As Integer
Private Declare Function Bematech_FI_ExtenderDescripcionArticulo Lib "BEMAFI32.DLL" (ByVal Descripcion As String) As Integer
Private Declare Function Bematech_FI_UsaUnidadMedida Lib "BEMAFI32.DLL" (ByVal UnidadMedida As String) As Integer
Private Declare Function Bematech_FI_RectificaFormasPago Lib "BEMAFI32.DLL" (ByVal FormaOrigen As String, ByVal FormaDestino As String, ByVal Valor As String) As Integer
Private Declare Function Bematech_FI_IniciaCierreCupon Lib "BEMAFI32.DLL" (ByVal IncrementoDescuento As String, ByVal TipoIncrementoDescuento As String, ByVal ValorIncrementoDescuento As String) As Integer
Private Declare Function Bematech_FI_EfectuaFormaPago Lib "BEMAFI32.DLL" (ByVal FormaPago As String, ByVal ValorFormaPago As String) As Integer
Private Declare Function Bematech_FI_EfectuaFormaPagoDescripcionForma Lib "BEMAFI32.DLL" (ByVal FormaPago As String, ByVal ValorFormaPago As String, ByVal DescripcionOpcional As String) As Integer
Private Declare Function Bematech_FI_FinalizarCierreCupon Lib "BEMAFI32.DLL" (ByVal Mensaje As String) As Integer

' Funciones de los Informes Fiscales

Private Declare Function Bematech_FI_LecturaX Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_LecturaXSerial Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_ReduccionZ Lib "BEMAFI32.DLL" (ByVal Fecha As String, ByVal Hora As String) As Integer
Private Declare Function Bematech_FI_InformeGerencial Lib "BEMAFI32.DLL" (ByVal cTexto As String) As Integer
Private Declare Function Bematech_FI_InformeGerencialTEF Lib "BEMAFI32.DLL" (ByVal cTexto As String) As Integer
Private Declare Function Bematech_FI_CierraInformeGerencial Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_LecturaMemoriaFiscalFecha Lib "BEMAFI32.DLL" (ByVal cFechaInicial As String, ByVal cFechaFinal As String) As Integer
Private Declare Function Bematech_FI_LecturaMemoriaFiscalReduccion Lib "BEMAFI32.DLL" (ByVal cReduccionInicial As String, ByVal cReduccionFinal As String) As Integer
Private Declare Function Bematech_FI_LecturaMemoriaFiscalSerialFecha Lib "BEMAFI32.DLL" (ByVal cFechaInicial As String, ByVal cFechaFinal As String) As Integer
Private Declare Function Bematech_FI_LecturaMemoriaFiscalSerialReduccion Lib "BEMAFI32.DLL" (ByVal cReduccionInicial As String, ByVal cReduccionFinal As String) As Integer

' Funciones de las Operaciones No Fiscales

Private Declare Function Bematech_FI_RecebimientoNoFiscal Lib "BEMAFI32.DLL" (ByVal IndiceTotalizador As String, ByVal Valor As String, ByVal FormaPago As String) As Integer
Private Declare Function Bematech_FI_AbreComprobanteNoFiscalVinculado Lib "BEMAFI32.DLL" (ByVal FormaPago As String, ByVal Valor As String, ByVal NumeroCupon As String) As Integer
Private Declare Function Bematech_FI_ImprimeComprobanteNoFiscalVinculado Lib "BEMAFI32.DLL" (ByVal Texto As String) As Integer
Private Declare Function Bematech_FI_UsaComprobanteNoFiscalVinculadoTEF Lib "BEMAFI32.DLL" (ByVal Texto As String) As Integer
Private Declare Function Bematech_FI_CierraComprobanteNoFiscalVinculado Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_Sangria Lib "BEMAFI32.DLL" (ByVal Valor As String) As Integer
Private Declare Function Bematech_FI_Provision Lib "BEMAFI32.DLL" (ByVal Valor As String, ByVal FormaPago As String) As Integer

' Funciones de Informaciones de la Impresora

Private Declare Function Bematech_FI_NumeroSerie Lib "BEMAFI32.DLL" (ByVal NumeroSerie As String) As Integer
Private Declare Function Bematech_FI_SubTotal Lib "BEMAFI32.DLL" (ByVal SubTotal As String) As Integer
Private Declare Function Bematech_FI_NumeroCupon Lib "BEMAFI32.DLL" (ByVal NumeroCupon As String) As Integer
Private Declare Function Bematech_FI_VersionFirmware Lib "BEMAFI32.DLL" (ByVal VersionFirmware As String) As Integer
Private Declare Function Bematech_FI_CGC_IE Lib "BEMAFI32.DLL" (ByVal CGC As String, ByVal IE As String) As Integer
Private Declare Function Bematech_FI_GranTotal Lib "BEMAFI32.DLL" (ByVal GranTotal As String) As Integer
Private Declare Function Bematech_FI_Cancelamientos Lib "BEMAFI32.DLL" (ByVal ValorCancelamientos As String) As Integer
Private Declare Function Bematech_FI_Descuentos Lib "BEMAFI32.DLL" (ByVal ValorDescuentos As String) As Integer
Private Declare Function Bematech_FI_NumeroOperacionesNoFiscales Lib "BEMAFI32.DLL" (ByVal NumeroOperaciones As String) As Integer
Private Declare Function Bematech_FI_NumeroCuponesAnulados Lib "BEMAFI32.DLL" (ByVal NumeroCancelamientos As String) As Integer
Private Declare Function Bematech_FI_NumeroIntervenciones Lib "BEMAFI32.DLL" (ByVal NumeroIntervenciones As String) As Integer
Private Declare Function Bematech_FI_NumeroReducciones Lib "BEMAFI32.DLL" (ByVal NumeroReducciones As String) As Integer
Private Declare Function Bematech_FI_NumeroSustituicionesPropietario Lib "BEMAFI32.DLL" (ByVal NumeroSustituiciones As String) As Integer
Private Declare Function Bematech_FI_UltimoArticuloVendido Lib "BEMAFI32.DLL" (ByVal NumeroArticulo As String) As Integer
Private Declare Function Bematech_FI_ClichePropietario Lib "BEMAFI32.DLL" (ByVal Cliche As String) As Integer
Private Declare Function Bematech_FI_NumeroCaja Lib "BEMAFI32.DLL" (ByVal NumeroCaja As String) As Integer
Private Declare Function Bematech_FI_NumeroTienda Lib "BEMAFI32.DLL" (ByVal NumeroTienda As String) As Integer
Private Declare Function Bematech_FI_SimboloMoneda Lib "BEMAFI32.DLL" (ByVal SimboloMoneda As String) As Integer
Private Declare Function Bematech_FI_MinutosPrendida Lib "BEMAFI32.DLL" (ByVal Minutos As String) As Integer
Private Declare Function Bematech_FI_MinutosImprimiendo Lib "BEMAFI32.DLL" (ByVal Minutos As String) As Integer
Private Declare Function Bematech_FI_VerificaModoOperacion Lib "BEMAFI32.DLL" (ByVal Modo As String) As Integer
Private Declare Function Bematech_FI_VerificaEpromConectada Lib "BEMAFI32.DLL" (ByVal Flag As String) As Integer
Private Declare Function Bematech_FI_FlagsFiscales Lib "BEMAFI32.DLL" (ByRef Flag As Integer) As Integer
Private Declare Function Bematech_FI_ValorPagoUltimoCupon Lib "BEMAFI32.DLL" (ByVal ValorCupon As String) As Integer
Private Declare Function Bematech_FI_FechaHoraImpresora Lib "BEMAFI32.DLL" (ByVal Fecha As String, ByVal Hora As String) As Integer
Private Declare Function Bematech_FI_ContadoresTotalizadoresNoFiscales Lib "BEMAFI32.DLL" (ByVal Contadores As String) As Integer
Private Declare Function Bematech_FI_VerificaTotalizadoresNoFiscales Lib "BEMAFI32.DLL" (ByVal Totalizadores As String) As Integer
Private Declare Function Bematech_FI_FechaHoraReducion Lib "BEMAFI32.DLL" (ByVal Fecha As String, ByVal Hora As String) As Integer
Private Declare Function Bematech_FI_FechaMovimiento Lib "BEMAFI32.DLL" (ByVal Fecha As String) As Integer
Private Declare Function Bematech_FI_VerificaTruncamiento Lib "BEMAFI32.DLL" (ByVal Flag As String) As Integer
Private Declare Function Bematech_FI_Agregado Lib "BEMAFI32.DLL" (ByVal ValorIncrementos As String) As Integer
Private Declare Function Bematech_FI_ContadorBilletePasaje Lib "BEMAFI32.DLL" (ByVal ContadorPasaje As String) As Integer
Private Declare Function Bematech_FI_VerificaAlicuotasIss Lib "BEMAFI32.DLL" (ByVal AlicuotasIss As String) As Integer
Private Declare Function Bematech_FI_VerificaFormasPago Lib "BEMAFI32.DLL" (ByVal Formas As String) As Integer
Private Declare Function Bematech_FI_VerificaRecebimientoNoFiscal Lib "BEMAFI32.DLL" (ByVal Recebimientos As String) As Integer
Private Declare Function Bematech_FI_VerificaDepartamentos Lib "BEMAFI32.DLL" (ByVal Departamentos As String) As Integer
Private Declare Function Bematech_FI_VerificaTipoImpresora Lib "BEMAFI32.DLL" (ByRef TipoImpresora As String) As Integer
Private Declare Function Bematech_FI_VerificaTotalizadoresParciales Lib "BEMAFI32.DLL" (ByVal cTotalizadores As String) As Integer
Private Declare Function Bematech_FI_RetornoAlicuotas Lib "BEMAFI32.DLL" (ByVal cAliquotas As String) As Integer
Private Declare Function Bematech_FI_DatosUltimaReduccion Lib "BEMAFI32.DLL" (ByVal datosreduccion As String) As Integer
Private Declare Function Bematech_FI_MonitoramentoPapel Lib "BEMAFI32.DLL" (ByRef Lineas As String) As Integer
Private Declare Function Bematech_FI_ValorFormaPago Lib "BEMAFI32.DLL" (ByVal FormaPago As String, ByVal Valor As String) As Integer
Private Declare Function Bematech_FI_ValorTotalizadorNoFiscal Lib "BEMAFI32.DLL" (ByVal Totalizador As String, ByVal Valor As String) As Integer

' Funciones de Autenticación

Private Declare Function Bematech_FI_Autenticacion Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_ProgramaCaracterAutenticacion Lib "BEMAFI32.DLL" (ByVal Parametros As String) As Integer

' Funciones de Gaveta de Dinero
   
Private Declare Function Bematech_FI_AccionaGaveta Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_VerificaEstadoGaveta Lib "BEMAFI32.DLL" (ByRef EstadoGaveta As Integer) As Integer

' Otras Funciones

Private Declare Function Bematech_FI_ResetaImpresora Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_AbrePuertaSerial Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_VerificaEstadoImpresora Lib "BEMAFI32.DLL" (ByRef ACK As Integer, ByRef ST1 As Integer, ByRef ST2 As Integer) As Integer
Private Declare Function Bematech_FI_RetornoImpresora Lib "BEMAFI32.DLL" (ByRef ACK As Integer, ByRef ST1 As Integer, ByRef ST2 As Integer) As Integer
Private Declare Function Bematech_FI_CierraPuertaSerial Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_VerificaImpresoraPrendida Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_ImprimeConfiguracionesImpresora Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_ImprimeDepartamentos Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_AperturaDelDia Lib "BEMAFI32.DLL" (ByVal Valor As String, ByVal FormaPago As String) As Integer
Private Declare Function Bematech_FI_CierreDelDia Lib "BEMAFI32.DLL" () As Integer
Private Declare Function Bematech_FI_ImpresionCarne Lib "BEMAFI32.DLL" (ByVal Titulo As String, ByVal Percelas As String, ByVal Fechas As Integer, ByVal Cantidad As Integer, ByVal Texto As String, ByVal Cliente As String, ByVal RG_CPF As String, ByVal Cupon As String, ByVal Vias As Integer, ByVal Firma As Integer) As Integer
Private Declare Function Bematech_FI_InfoBalanza Lib "BEMAFI32.DLL" (ByVal Puerta As String, ByVal Modelo As Integer, ByVal Peso As String, ByVal PrecioKilo As String, ByVal Total As String) As Integer
Private Declare Function Bematech_FI_VersionDll Lib "BEMAFI32.DLL" (ByVal Version As String) As Integer
Private Declare Function Bematech_FI_LeerArchivoRetorno Lib "BEMAFI32.DLL" (ByVal Retorno As String) As Integer


'//////////////////////////////////////////////////////////
'// IMPRESORA FISCAL SANSSUMG
'//////////////////////////////////////////////////////////


'//status
   Const FPCTL_NON_STATUS = &H0
   Const FPCTL_NON_FISCAL_IN_IDLE = &H1
   Const FPCTL_NON_FISCAL_IN_TRANSACTION = &H2
   Const FPCTL_NON_FISCAL_NON_TRANSACTION = &H3
   Const FPCTL_FISCAL_IN_IDLE = &H4
   Const FPCTL_FISCAL_IN_TRANSACTION = &H5
   Const FPCTL_FISCAL_NON_TRANSACTION = &H6
   Const FPCTL_FISCAL_MEMORY_NEAR_FULL_IN_TRANSACTION = &H7
   Const FPCTL_FISCAL_MEMORY_NEAR_FULL_NON_TRANSACTION = &H8
   Const FPCTL_FISCAL_MEMORY_NEAR_FULL_IN_IDLE = &H9
   Const FPCTL_FISCAL_MEMORY_FULL_IN_TRANSACTION = &HA
   Const FPCTL_FISCAL_MEMORY_FULL_IN_NON_TRANSACTION = &HB
   Const FPCTL_FISCAL_MEMORY_FULL_IN_IDLE = &HC
   Const FPCTL_SENDNCMD_SUCCESS = &H0
   Const MAX_LINEAS As Integer = 10001
   
   Private sLineas(0 To MAX_LINEAS)
    
'// error
   Const FPCTL_ERROR_NO_ERROR = &H0
   Const FPCTL_ERROR_PAPER_END = &H1
   Const FPCTL_ERROR_PRINTER_MECHA_ERROR = &H2
   Const FPCTL_ERROR_PAPER_END_MECHA_ERROR = &H3
   Const FPCTL_ERROR_COMMAND_INVALID_VAL = &H50
   Const FPCTL_ERROR_COMMAND_INVALID_TAX = &H54
   Const FPCTL_ERROR_NOT_ASSIGNED_CLERK = &H58
   Const FPCTL_ERROR_INVALID_COMMAND = &H5C
   Const FPCTL_ERROR_FISCAL_ERROR = &H60
   Const FPCTL_ERROR_FISCAL_MEMORY_ERROR = &H64
   Const FPCTL_ERROR_FISCAL_MEMORY_FULL = &H6C
   
   Const FPCTL_ERROR_BUFFER_FULL = &H70
   Const FPCTL_ERROR_ANSWERED_NAK = &H80
   Const FPCTL_ERROR_NOT_ANSWERED = &H89
   Const FPCTL_ERROR_UNKNOWN_ANSWERED = &H90
   Const FPCTL_ERROR_COMM_API = &H91
   Const FPCTL_ERROR_FILE_OPEN = &H99
   Const FORMATO_PRECIO As String = "0000000000"
   Const FORMATO_CANTIDAD As String = "00000000"
   Const FORMATO_DECIMAL As String = "#0.00"
   Const FORMATO_ENTERO_2 As String = "00"
   Const FORMATO_ENTERO_12 As String = "000000000000"

'*******************************************************************************************
' Declaraciones Propias del Módulo
'*******************************************************************************************
Private insDatosImprFiscal As clsDatosImprFiscal
Private insImprFiscal As clsUtilImprFiscal
Private Secuencia As Integer
Private mMensaje As String
Private mSerial As String
Private mUltimoNoComprobanteFiscal As String
Private mReady As Boolean
Private mStatusEpson As String
Private mbRet As Boolean
Private mDatosFiscales As String
Private Const FIELD = 28
Private gWorksPath As clsUtilWorkPaths
Private MaxLenEpson As Integer

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "frmImpresorasFiscales"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Impresoras Fiscales"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Private Function fAbrirComprobanteFiscal(ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, valPuerto As String, ByVal valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String) As Boolean
   Dim insImpFiscalBMC As clsUtilImpFiscalBMC
   Dim insImpFiscalQPrint As clsImpFiscalQPrint
   Dim insImpFiscalFamFactory As clsImpFiscalFamFactory
   On Error GoTo h_Error
   MaxLenEpson = 16
   
   If (insDatosImprFiscal.GetUsarModoDotNet) Then
     mReady = True
   Else
    Select Case valImpresoraFiscal
       Case Enum_ImpresorasFiscales.eIF_EPSON_PF_220:                 sAbreCFEpson (valPuerto)
       Case Enum_ImpresorasFiscales.eIF_EPSON_PF_220II:
          MaxLenEpson = 37
          sAbreCFEpson (valPuerto)
       Case Enum_ImpresorasFiscales.eIF_EPSON_PF_300
          MaxLenEpson = 37
          sAbreCFEpson (valPuerto)
       Case Enum_ImpresorasFiscales.eIF_EPSON_TM_675_PF:              sAbreCFEpson (valPuerto)
       Case Enum_ImpresorasFiscales.eIF_EPSON_TM_950_PF:              sAbreCFEpson (valPuerto)
       Case Enum_ImpresorasFiscales.eIF_BEMATECH_MP_20_FI_II, Enum_ImpresorasFiscales.eIF_BEMATECH_MP_2100_FI, Enum_ImpresorasFiscales.eIF_BEMATECH_MP_4000_FI: sAbreCF_BEMATECH
        
       Case Enum_ImpresorasFiscales.eIF_BMC_CAMEL, Enum_ImpresorasFiscales.eIF_BMC_SPARK_614, Enum_ImpresorasFiscales.eIF_BMC_TH34_EJ
          Set insImpFiscalBMC = New clsUtilImpFiscalBMC
          mReady = True
          Set insImpFiscalBMC = Nothing
       Case Enum_ImpresorasFiscales.eIF_QPRINT_MF
          Set insImpFiscalQPrint = New clsImpFiscalQPrint
          insImpFiscalQPrint.sAbreCF_QPrintMF valImpresoraFiscal, insDatosImprFiscal, mReady, valPuerto, valTipoConexion, valIp, valCajaNumero
          Set insImpFiscalQPrint = Nothing
       Case Enum_ImpresorasFiscales.eIF_DASCOM_TALLY_1125, eIF_SAMSUNG_BIXOLON_SRP_270, eIF_ACLAS_PP1F3, eIF_SAMSUNG_BIXOLON_SRP_350, eIF_OKI_ML_1120, eIF_DASCOM_TALLY_DT_230, eIF_SAMSUNG_BIXOLON_SRP_280, eIF_ACLAS_PP9, eIF_SAMSUNG_BIXOLON_SRP_812, eif_HKA80, eif_HKA112:
          Set insImpFiscalFamFactory = New clsImpFiscalFamFactory
          mReady = insImpFiscalFamFactory.sAbreCF(valPuerto, valImpresoraFiscal)
          Set insImpFiscalFamFactory = Nothing
       Case eIF_ELEPOS_VMAX_220_F, eIF_ELEPOS_VMAX_300, eIF_ELEPOS_VMAX_580
          mReady = True
    End Select
   End If
h_EXIT: On Error GoTo 0
   fAbrirComprobanteFiscal = True
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fAbrirComprobanteFiscal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sAbreCFEpson(valPuerto As String, Optional ByVal Devolucion As Boolean)
'Esta rutina abre un comprobante fiscal
'Razon: es la razon social o nombre del comprador que comprador que aparece en el comprobante fiscal
'Rif: es el Rif o la CI del comprador
'Tipo: determina el tipo de documento a emitir. Si tipo = "D" entoces se realiza un comprobante de devolucion, EN CUALQUIER otro caso se realiza un comprobante fiscal ordinario
'NCD: indica el numero del comprobante para el cual se esta realizando el documento de devolucion
'SMaquina: indica el serial de la maquina que realizao el comprobante sobre el cual se esta emitiendo una devolucion
'Fecha y Hora = indican la fecha y la hora en la cual fue emitido el dcoumento sobre el cual se emite la devolucion

   Dim NCD As String
   Dim SMaquina As String
   Dim Fecha As String
   Dim Hora As String
   Dim Salida As String
   Dim Dia As String
   Dim Mes As String
   Dim Ano As String
   Dim NombreDelCliente As String
   On Error GoTo h_Error
   mReady = False
   Salida = ""
   If gTexto.DfLen(insDatosImprFiscal.GetTipoComprobante) = 0 Then
       insDatosImprFiscal.SetTipoComprobante Chr$(127)
   End If
   If gTexto.DfLen(insDatosImprFiscal.GetNombreCliente) = 0 Then
       insDatosImprFiscal.SetNombreCliente Chr$(127)
   End If
   If gTexto.DfLen(insDatosImprFiscal.GetNumeroRIFCliente) = 0 Then
       insDatosImprFiscal.SetNumeroRIFCliente Chr$(127)
   End If
   If NCD = "" Then
       NCD = Chr$(127)
   End If
   If SMaquina = "" Then
       SMaquina = Chr$(127)
   End If
   If Fecha = "" Then
      Dia = gConvert.fConvierteAString(Day(insDatosImprFiscal.GetFecha))
      Mes = gConvert.fConvierteAString(Month(insDatosImprFiscal.GetFecha))
      Ano = gTexto.DfRight(Year(insDatosImprFiscal.GetFecha), 2)
      Fecha = Dia & "/" & Mes & "/" & Ano
   End If
   If Hora = "" Then
      Hora = insDatosImprFiscal.GetHoraModificacion
   End If
   Salida = Chr$(64) & Chr$(FIELD)
   If Not (insDatosImprFiscal.GetPermitirNombreDelClienteExtendido) Then
      Salida = Salida & fCadenaValidaEpson(insDatosImprFiscal.GetNombreCliente) & Chr$(FIELD)
   Else
      Salida = Salida & Chr$(32) & Chr$(FIELD)
   End If
   Salida = Salida & fCadenaValidaEpson(insDatosImprFiscal.GetNumeroRIFCliente) & Chr$(FIELD)
   If Devolucion Then
      Salida = Salida & fCadenaValidaEpson(insDatosImprFiscal.GetNumeroTicket) & Chr$(FIELD)
   Else
      Salida = Salida & NCD & Chr$(FIELD)
   End If
   Salida = Salida & fCadenaValidaEpson(insDatosImprFiscal.GetSerialImpresoraFiscal) & Chr$(FIELD)
   Salida = Salida & Fecha & Chr$(FIELD)
   Salida = Salida & Hora & Chr$(FIELD)
   If Devolucion Then
      Salida = Salida & fCadenaValidaEpson(insDatosImprFiscal.GetTipoComprobante) & Chr$(FIELD) & Chr$(127)
   Else
      Salida = Salida & Chr(FIELD) & Chr$(127)
   End If
   Salida = Salida & Chr(FIELD) & Chr$(127)
   sEnviar Salida, "", valPuerto
   Salida = ""
   If insDatosImprFiscal.GetPermitirNombreDelClienteExtendido Then
      NombreDelCliente = "Razon Social:" & fCadenaValidaEpson(insDatosImprFiscal.GetNombreCliente, True)
      If gTexto.DfLen(NombreDelCliente) <= 40 Then
         Salida = Salida & Chr$(65) & Chr$(FIELD)
         Salida = Salida & NombreDelCliente & Chr$(FIELD) & Chr$(127)
         sEnviar Salida, "", valPuerto
         Salida = ""
      Else
         Salida = Salida & Chr$(65) & Chr$(FIELD)
         Salida = Salida & gTexto.DfMid(NombreDelCliente, 1, 40) & Chr$(FIELD) & Chr$(127)
         sEnviar Salida, "", valPuerto
         Salida = ""
         Salida = Salida & Chr$(65) & Chr$(FIELD)
         Salida = Salida & gTexto.DfMid(NombreDelCliente, 41) & Chr$(FIELD) & Chr$(127)
         sEnviar Salida, "", valPuerto
         Salida = ""
      End If
   End If
   'Call Campos
   'Call DefEstados
   
   'AbrirCF = True
   'mReady = True
'   If (EstadoImpresora_15 = True) Or (EstadoFiscal_15 = True) Then
'       mReady = False
'   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sAbreCFEpson", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sEnviar(ByVal Salida As String, ByRef refEntrada As String, valPuerto As String)

   Dim Suma As Variant
   Dim Num As Integer
   Dim Resi As Variant
   
   Dim i As Integer
   Dim j As Integer
   Dim Numer As Integer
   Dim NumerB As Integer
   Dim NumerC As Integer
   Dim Temp As Long
   Dim Temp2 As Long
   
   Dim Fin0, Fin1 As Boolean
   Dim Finalizar As Boolean
   
   Dim TXT As String
   Dim Carac As Variant
   Dim BCCTOTAL As String
   Dim PUERTO As String
   
'   Const FIELD = 28
   Const STX = 2
   Const ETX = 3
   Const Tiempomaximo = 10
   
   Dim Entrada As String
   Dim Entrada2 As String
'prevee la rutina de errores
   On Error GoTo h_Error
   PUERTO = valPuerto
   

   refEntrada = ""
'establece la Secuencia
   Secuencia = Secuencia + 1
   If Secuencia < 32 Or Secuencia > 70 Then
       While (Secuencia < 32) Or (Secuencia > 70)
           Secuencia = Right(Format(Time, "HHMMSS"), 2) + Secuencia
           If (Secuencia < 35) Then Secuencia = Secuencia + 35
           If (Secuencia > 70) Then Secuencia = Secuencia / 2
       Wend
   End If
   
   'establece inicio, secuencia y fin
   Salida = Chr$(STX) & Chr$(Secuencia) & Salida & Chr$(ETX)
   
   Finalizar = False
   TXT = Salida
   Num = Len(TXT)
   
   'calcula BCC
   Suma = 0
   For i = 1 To Num                    'calcula el valor
   Carac = Right(TXT, i)               'en ASCII de una
   Num = Asc(Carac)                    'cadena.
   Suma = Num + Suma                   'y lo devuelve en divi.
   Next i
   BCCTOTAL = "0000"
   For i = 1 To 4                      'conviete un numero
   Resi = Suma Mod 16                  'de bcd a
   Suma = Suma \ 16                   'hexageximal y lo
   Select Case Resi                   'devuelve en la
       Case Is = 10                   'palabra TamPal.
           Resi = "A"
       Case Is = 11
           Resi = "B"
       Case Is = 12
           Resi = "C"
       Case Is = 13
           Resi = "D"
       Case Is = 14
           Resi = "E"
       Case Is = 15
           Resi = "F"
   End Select

   j = 5 - i
   Mid(BCCTOTAL, j) = Resi
   Next i                              'fin de la rutina.'
   
   'TRANSMITE la respuesta

  
'   gMessage.Advertencia Puerto
   cmmPuerto.CommPort = PUERTO
   cmmPuerto.PortOpen = True
   cmmPuerto.Output = Salida & BCCTOTAL
   'Puerto.PortOpen = False
   
   'RESPUESTA
   Entrada = "0"
   Entrada2 = Entrada
   Numer = 1
   Temp = 0
   Temp2 = 0
   Fin1 = True
   Fin0 = False
   cmmPuerto.InBufferCount = 0
   Temp = Timer + Tiempomaximo
   While (Timer < Temp) And (Fin0 = False)
       If (cmmPuerto.InBufferCount >= 4) And (Fin1 = True) Then
           NumerB = cmmPuerto.InBufferCount
           Entrada2 = cmmPuerto.Input
           Entrada = Entrada & Entrada2
           Numer = Numer + NumerB
           NumerC = Len(Entrada2) - InStr(Entrada2, Chr(3))
       End If
       If (InStr(Entrada2, Chr(18)) > 0) Then
           Temp = Timer + Tiempomaximo
       End If
       If (InStr(Entrada2, Chr(3)) > 0) Or (Fin1 = False) Then
           Fin1 = False
           If cmmPuerto.InBufferCount >= (4 - NumerC) Then
               Entrada2 = cmmPuerto.Input
               Entrada = Entrada & Entrada2
               Fin0 = True
           End If
       End If
   Wend
   If Fin0 = False Then
       GoTo h_Error
       Exit Sub
   End If
   If Finalizar = False Then
      refEntrada = Entrada
      Numer = InStr(Entrada, Chr(3))
      Entrada = Left(Entrada, Numer + 4)
      Numer = InStr(Entrada, Chr(2))
      mSerial = Right(Entrada, Len(Entrada) - Numer + 1)
      mUltimoNoComprobanteFiscal = Right(Entrada, Len(Entrada) - Numer + 1)
      mStatusEpson = Right(Entrada, Len(Entrada) - Numer + 1)
      mReady = Not fValorCampoError
   End If
   If cmmPuerto.PortOpen Then cmmPuerto.PortOpen = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error:
   mReady = False
   If cmmPuerto.PortOpen Then cmmPuerto.PortOpen = False
   If Err.Number = 8005 Then
      gMessage.Advertencia "Error de comunicación." & vbCrLf & " Verifique el Estado del Puerto y/o la Conexión con la Impresora." & vbCrLf & " No se logra establecer conexión con la impresora."
      Err.Clear
   Else
      Err.Raise Err.Number, "Time Out", Err.Description
      Entrada = Chr(2) & "" & Chr(FIELD) & "" & Chr(FIELD) & "" & Chr(3)
   End If
   Exit Sub
End Sub

Private Sub cmdSalir_Click()
   On Error GoTo h_Error
   Unload Me
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "cmdSalir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Form_Load()
On Error GoTo h_Error
   sInitDefaultValues
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "Form_Load", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInitDefaultValues()
   Dim insEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_Error
   Set insEnumProyecto = New clsEnumAdministrativo
   Set gWorksPath = New clsUtilWorkPaths
   lblImpresion.Caption = insEnumProyecto.enumEstadoImpresionFiscalToString(insDatosImprFiscal.GetEstado)
   
   Set insEnumProyecto = Nothing
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sInitDefaultValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInitLookAndFeel()
   On Error GoTo h_Error
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sInitLookAndFeel", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sInitLookAndFeelAndSetValues(ByRef refDatosImprFiscal As clsDatosImprFiscal, ByRef refReady As Boolean, Optional ByVal gAdmAlicuotaIvaActual As Object)
   On Error GoTo h_Error
   Set insDatosImprFiscal = refDatosImprFiscal
   Set insImprFiscal = New clsUtilImprFiscal
   Select Case insDatosImprFiscal.GetEstado
      Case Enum_EstadoImpresionFiscal.eEIF_VERIFICANDO_CONEXION_CON_IMPRESORA: fAbrirComprobanteFiscal insDatosImprFiscal.GetImpresoraFiscal, insDatosImprFiscal.GetPuertoImpresoraFiscal, insDatosImprFiscal.GetTipoConexion, insDatosImprFiscal.GetIp, insDatosImprFiscal.GetCajaNumero
      Case Enum_EstadoImpresionFiscal.eEIF_CIERRE_Z: fEjecutaCierreZ insDatosImprFiscal.GetImpresoraFiscal, insDatosImprFiscal.GetPuertoImpresoraFiscal, insDatosImprFiscal.GetTipoConexion, insDatosImprFiscal.GetIp, insDatosImprFiscal.GetCajaNumero
      Case Enum_EstadoImpresionFiscal.eEIF_CIERRE_X: fEjecutaCierreX insDatosImprFiscal.GetImpresoraFiscal, insDatosImprFiscal.GetPuertoImpresoraFiscal, insDatosImprFiscal.GetTipoConexion, insDatosImprFiscal.GetIp, insDatosImprFiscal.GetCajaNumero
      Case Enum_EstadoImpresionFiscal.eEIF_OBTENER_SERIAL: sObtenerSerialImpresoraFiscal insDatosImprFiscal.GetImpresoraFiscal, insDatosImprFiscal.GetPuertoImpresoraFiscal, insDatosImprFiscal.GetTipoConexion, insDatosImprFiscal.GetIp, insDatosImprFiscal.GetCajaNumero
      Case Enum_EstadoImpresionFiscal.eEIF_IMPRIMIR_VENTA: sEfectuaVenta insDatosImprFiscal.GetImpresoraFiscal, insDatosImprFiscal.GetPuertoImpresoraFiscal, insDatosImprFiscal.GetTipoConexion, insDatosImprFiscal.GetIp, insDatosImprFiscal.GetCajaNumero
      Case Enum_EstadoImpresionFiscal.eEIF_IMPRIMIR_NOTA_DE_CREDITO: sEfectuaNotaDeCredito insDatosImprFiscal.GetImpresoraFiscal, insDatosImprFiscal.GetPuertoImpresoraFiscal, insDatosImprFiscal.GetTipoConexion, insDatosImprFiscal.GetIp, insDatosImprFiscal.GetCajaNumero
      Case Enum_EstadoImpresionFiscal.eEIF_OBTENER_ULTIMO_NUMERO_FISCAL: sObtenerUltimoNumeroFiscal insDatosImprFiscal.GetImpresoraFiscal, refReady, insDatosImprFiscal.GetPuertoImpresoraFiscal, insDatosImprFiscal.GetTipoConexion, insDatosImprFiscal.GetIp, insDatosImprFiscal.GetCajaNumero
      Case Enum_EstadoImpresionFiscal.eEIF_CANCELA_CUPON_FISCAL: sCancelaCuponFiscal insDatosImprFiscal.GetImpresoraFiscal, refReady, insDatosImprFiscal.GetPuertoImpresoraFiscal
      Case Enum_EstadoImpresionFiscal.eEIF_VERIFICA_ALICUOTA_VIGENTE: sVerificaVigenciaDeLaAlicuota insDatosImprFiscal.GetImpresoraFiscal, refReady, gAdmAlicuotaIvaActual, insDatosImprFiscal.GetPuertoImpresoraFiscal
      Case Enum_EstadoImpresionFiscal.eEIF_VER_STATUS: sVerStatusImpresoraFiscal insDatosImprFiscal.GetImpresoraFiscal, insDatosImprFiscal.GetPuertoImpresoraFiscal
      Case Enum_EstadoImpresionFiscal.eEIF_ACTIVA_REDONDEO: sActivaRedondeo insDatosImprFiscal.GetImpresoraFiscal, refReady
      Case Enum_EstadoImpresionFiscal.eEIF_OBTENER_ULTIMO_REPORTE_Z: sObtenerUltimoNumeroDeReporteZ insDatosImprFiscal.GetImpresoraFiscal, insDatosImprFiscal.GetPuertoImpresoraFiscal, insDatosImprFiscal.GetTipoConexion, insDatosImprFiscal.GetIp, insDatosImprFiscal.GetCajaNumero
      Case Enum_EstadoImpresionFiscal.eEIF_ASIGNAR_CONFIGURACION:  sAsignarConfiguracion insDatosImprFiscal.GetImpresoraFiscal, insDatosImprFiscal.GetPuertoImpresoraFiscal, insDatosImprFiscal.GetTipoConexion, insDatosImprFiscal.GetIp, insDatosImprFiscal.GetCajaNumero, insDatosImprFiscal.GetGateway, insDatosImprFiscal.GetMascaraSubRed
      Case Enum_EstadoImpresionFiscal.eEIF_CORTAR_PAPEL:   sCortarPapel insDatosImprFiscal.GetImpresoraFiscal, insDatosImprFiscal.GetPuertoImpresoraFiscal, insDatosImprFiscal.GetTipoConexion, insDatosImprFiscal.GetIp, insDatosImprFiscal.GetCajaNumero
   End Select
   refReady = mReady
h_EXIT: On Error GoTo 0
   If cmmPuerto.PortOpen Then cmmPuerto.PortOpen = False
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sInitLookAndFeelAndSetValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fEjecutaCierreZ(ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, valPuerto As String, valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String) As Boolean
   Dim insImpFiscalEleposVMAX As clsImpFiscalEleposVMAX
   Dim insImpFiscalFromAOS As clsImpFiscalFromAOS
   Dim vLastZ As String
   On Error GoTo h_Error
   
   If insDatosImprFiscal.GetUsarModoDotNet Then
      Set insImpFiscalFromAOS = New clsImpFiscalFromAOS
          insImpFiscalFromAOS.sCierreZ mReady, valPuerto, valImpresoraFiscal, vLastZ
          insDatosImprFiscal.SetCajaNumero vLastZ
          fEjecutaCierreZ = mReady
      Set insImpFiscalFromAOS = Nothing
   Else
    Select Case valImpresoraFiscal
       Case eIF_EPSON_PF_220:                                   sCierreZ_EPSON (valPuerto)
       Case eIF_EPSON_PF_300:                                   sCierreZ_EPSON (valPuerto)
       Case eIF_EPSON_PF_220II:                                 sCierreZ_EPSON (valPuerto)
       Case eIF_EPSON_TM_675_PF:                                sCierreZ_EPSON (valPuerto)
       Case eIF_EPSON_TM_950_PF:                                sCierreZ_EPSON (valPuerto)
       Case eIF_BEMATECH_MP_20_FI_II, eIF_BEMATECH_MP_2100_FI, eIF_BEMATECH_MP_4000_FI: sCierreZ_BEMATECH
       Case eIF_BMC_CAMEL, eIF_BMC_SPARK_614, eIF_BMC_TH34_EJ:                  sCierreZ_BMC
       Case eIF_QPRINT_MF:                                      sCierreZ_QPrintMF valPuerto, valTipoConexion, valIp, valCajaNumero
       Case eIF_DASCOM_TALLY_1125, eIF_SAMSUNG_BIXOLON_SRP_270, eIF_SAMSUNG_BIXOLON_SRP_350, eIF_ACLAS_PP1F3, eIF_OKI_ML_1120, eIF_DASCOM_TALLY_DT_230, eIF_SAMSUNG_BIXOLON_SRP_280, eIF_ACLAS_PP9, eIF_SAMSUNG_BIXOLON_SRP_812, eif_HKA80, eif_HKA112:
          sCierreZ_FamiliaFactory valPuerto, valImpresoraFiscal
       Case eIF_ELEPOS_VMAX_220_F, eIF_ELEPOS_VMAX_300, eIF_ELEPOS_VMAX_580
         Set insImpFiscalEleposVMAX = New clsImpFiscalEleposVMAX
         insImpFiscalEleposVMAX.InitializeValues obVMAX
         insImpFiscalEleposVMAX.sCierreZ mReady, valPuerto
         Set insImpFiscalEleposVMAX = Nothing
    End Select
   End If
h_EXIT: On Error GoTo 0
   fEjecutaCierreZ = True
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEjecutaCierreZ", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sCierreZ_EPSON(valPuerto As String)
'Realiza un reporte Z
   Dim Salida As String
   On Error GoTo h_Error
   Salida = Chr$(57) & Chr$(FIELD) & Chr$(90)
   sEnviar Salida, "", valPuerto
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCierreZ_EPSON", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCierreZ_BEMATECH()
'Realiza un reporte Z
   Dim Retorno As Integer
   On Error GoTo h_Error
   Retorno = Bematech_FI_ReduccionZ("", "")
   insImprFiscal.fVerificaRetornoImpresora "", Retorno, mReady
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCierreZ_EPSON", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCierreZ_BMC()
   Dim insImpFiscalBMC  As clsUtilImpFiscalBMC
   On Error GoTo h_Error
   Set insImpFiscalBMC = New clsUtilImpFiscalBMC
   insImpFiscalBMC.sRealizarCierreZ_BMC mReady, insDatosImprFiscal.GetPuertoImpresoraFiscal
   Set insImpFiscalBMC = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCierreZ_BMC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCierreZ_QPrintMF(ByVal valPuerto As String, ByVal valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String)
   Dim insImpFiscalQPrint As clsImpFiscalQPrint
   On Error GoTo h_Error
   Set insImpFiscalQPrint = New clsImpFiscalQPrint
   insImpFiscalQPrint.sRealizarCierreZ_QPrintMF mReady, insDatosImprFiscal.GetPuertoImpresoraFiscal, insDatosImprFiscal.GetTipoConexion, valIp, valCajaNumero
   Set insImpFiscalQPrint = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCierreZ_QPrintMF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fEjecutaCierreX(ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, ByVal valPuerto As String, ByVal valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String) As Boolean
   Dim insImpFiscalBMC As clsUtilImpFiscalBMC
   Dim insImpFiscalQPrintMF As clsImpFiscalQPrint
   Dim insImpFiscalEleposVMAX As clsImpFiscalEleposVMAX
   Dim insImpFiscalFamFactory As clsImpFiscalFamFactory
    
   On Error GoTo h_Error
   If insDatosImprFiscal.GetUsarModoDotNet Then
   Dim insImpFiscalFromAOS As clsImpFiscalFromAOS
   Set insImpFiscalFromAOS = New clsImpFiscalFromAOS
      insImpFiscalFromAOS.sCierreX valPuerto, valImpresoraFiscal, mReady
      fEjecutaCierreX = mReady
   Set insImpFiscalFromAOS = Nothing
   Else
    Select Case valImpresoraFiscal
       Case eIF_EPSON_PF_220:  sCierreX_EPSON (valPuerto)
       Case eIF_EPSON_PF_300:  sCierreX_EPSON (valPuerto)
       Case eIF_EPSON_PF_220II:   sCierreX_EPSON (valPuerto)
       Case eIF_EPSON_TM_675_PF:  sCierreX_EPSON (valPuerto)
       Case eIF_EPSON_TM_950_PF:  sCierreX_EPSON (valPuerto)
       Case eIF_BEMATECH_MP_20_FI_II, eIF_BEMATECH_MP_2100_FI, eIF_BEMATECH_MP_4000_FI: sCierreX_BEMATECH
       
       Case eIF_BMC_CAMEL, eIF_BMC_SPARK_614, eIF_BMC_TH34_EJ
         Set insImpFiscalBMC = New clsUtilImpFiscalBMC
         insImpFiscalBMC.sRealizarCierreX_BMC mReady, valPuerto
         Set insImpFiscalBMC = Nothing
         
       Case eIF_QPRINT_MF
         Set insImpFiscalQPrintMF = New clsImpFiscalQPrint
         insImpFiscalQPrintMF.sRealizarCierreX_QPrintMF mReady, valPuerto, valTipoConexion, valIp, valCajaNumero
         Set insImpFiscalQPrintMF = Nothing
       
       Case eIF_DASCOM_TALLY_1125, eIF_ACLAS_PP1F3, eIF_SAMSUNG_BIXOLON_SRP_350, eIF_OKI_ML_1120, eIF_SAMSUNG_BIXOLON_SRP_270, eIF_DASCOM_TALLY_DT_230, eIF_SAMSUNG_BIXOLON_SRP_280, eIF_ACLAS_PP9, eIF_SAMSUNG_BIXOLON_SRP_812, eif_HKA80, eif_HKA112:
          Set insImpFiscalFamFactory = New clsImpFiscalFamFactory
          insImpFiscalFamFactory.sCierreX valPuerto, valImpresoraFiscal, mReady
          Set insImpFiscalFamFactory = Nothing
       Case eIF_ELEPOS_VMAX_220_F, eIF_ELEPOS_VMAX_300, eIF_ELEPOS_VMAX_580
         Set insImpFiscalEleposVMAX = New clsImpFiscalEleposVMAX
         insImpFiscalEleposVMAX.InitializeValues obVMAX
         insImpFiscalEleposVMAX.sCierreX mReady, valPuerto
         Set insImpFiscalEleposVMAX = Nothing
    End Select
   End If
h_EXIT: On Error GoTo 0
   fEjecutaCierreX = True
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fEjecutaCierreX", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sCierreX_EPSON(valPuerto As String)
'Realiza un Reporte X
   Dim Salida As String
   On Error GoTo h_Error
   Salida = Chr$(57) & Chr$(FIELD) & Chr$(88) & "P"
   sEnviar Salida, "", valPuerto
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCierreX_EPSON", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCierreX_BEMATECH()
'Realiza un Reporte X
   Dim Retorno As Integer
   On Error GoTo h_Error
   Retorno = Bematech_FI_LecturaX()
   insImprFiscal.fVerificaRetornoImpresora "", Retorno, mReady
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCierreX_BEMATECH", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAbreCF_BEMATECH()
   Dim Retorno As Integer
   Dim vRIFValido As String
   Dim vNombreValido As String
   Dim vDireccionValida As String
   Dim vEdoGaveta As Integer
   On Error GoTo h_Error
   vRIFValido = gTexto.DfLeft(insDatosImprFiscal.GetNumeroRIFCliente, 18)
   vNombreValido = gTexto.DfLeft(insDatosImprFiscal.GetNombreCliente, 41)
   vDireccionValida = gTexto.DfLeft(insDatosImprFiscal.GetDireccion, 133)
   Retorno = Bematech_FI_AbreComprobanteDeVentaEx(vRIFValido, vNombreValido, vDireccionValida)
   'Bematech_FI_AbreComprobanteDeVentaEx para colocar la direccion
   insImprFiscal.fVerificaRetornoImpresora "", Retorno, mReady
   Retorno = Bematech_FI_VerificaEstadoGaveta(vEdoGaveta)
   insImprFiscal.fVerificaRetornoImpresora "", Retorno, mReady
   If vEdoGaveta = 0 Then
      Retorno = Bematech_FI_AccionaGaveta()
      insImprFiscal.fVerificaRetornoImpresora "", Retorno, mReady
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sAbreCF_BEMATECH", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImprimeVentaArticulo_BEMATECH()
      Dim Retorno As Integer
      Dim rsDatosArticulo As ADODB.Recordset
      Const CANT_FRACCIONARIA  As String = "F"
      Const NUM_DECIMALES  As Integer = 3
      Dim Codigo As String
      Dim Descripcion As String
      Dim Alicuotas As String
      Dim Cantidades As String
      Dim Precios As String
      Dim Descuentos As String
      Dim DescripcionResumida As String
      On Error GoTo h_Error
      Set rsDatosArticulo = insDatosImprFiscal.GetItems
      If Not rsDatosArticulo.BOF And Not rsDatosArticulo.EOF Then
         rsDatosArticulo.MoveFirst
         Do While Not rsDatosArticulo.EOF
            Codigo = gTexto.DfLeft(rsDatosArticulo.Fields("Codigo").Value, 12)
            Descripcion = gTexto.DfLeft(rsDatosArticulo.Fields("Descripcion").Value, 150) & " " & gTexto.DfLeft(rsDatosArticulo.Fields("Serial").Value, 20) & " " & gTexto.DfLeft(rsDatosArticulo.Fields("Rollo").Value, 20)
            DescripcionResumida = gTexto.DfLeft(rsDatosArticulo.Fields("Descripcion").Value, 29)
            Alicuotas = fConvierteAlicuotaBEMATECH(rsDatosArticulo.Fields("AlicuotaIVA").Value)
            If Alicuotas = "0000" Then
               Alicuotas = "FF"
            End If
            Cantidades = fConvierteCantidadBEMATECH(rsDatosArticulo.Fields("Cantidad").Value, False)
            Precios = fConvierteCantidadBEMATECH(rsDatosArticulo.Fields("PrecioSinIVA").Value, False)
            Descuentos = fConvierteCantidadBEMATECHParaDescuentos(rsDatosArticulo.Fields("Descuento").Value)
            Retorno = Bematech_FI_ExtenderDescripcionArticulo(Descripcion)
            insImprFiscal.fVerificaRetornoImpresora "", Retorno, mReady
            Retorno = Bematech_FI_VendeArticulo(Codigo, DescripcionResumida, _
                   Alicuotas, CANT_FRACCIONARIA, Cantidades, NUM_DECIMALES, Precios, "%", Descuentos)
            insImprFiscal.fVerificaRetornoImpresora "", Retorno, mReady
            rsDatosArticulo.MoveNext
         Loop
      End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImprimeVentaArticulo_BEMATECH", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImprimeNotaDeCredito_BEMATECH()
      Dim Retorno As Integer
      Dim Nombre As String
      Dim NumeroSerie As String
      Dim Rif As String
      Dim Dia As String
      Dim Mes As String
      Dim Ano As String
      Dim Hora As String
      Dim Minuto As String
      Dim Segundo As String
      Dim CCO As String
      On Error GoTo h_Error
      Rif = gTexto.DfLeft(insDatosImprFiscal.GetNumeroRIFCliente, 14)
      Nombre = gTexto.DfLeft(insDatosImprFiscal.GetNombreCliente, 38)
      CCO = gTexto.DfRight(insDatosImprFiscal.GetNumeroTicket, 6)
      NumeroSerie = gTexto.DfRight(insDatosImprFiscal.GetNumeroSerie, 15)
      Dia = gConvert.fConvierteAString(Day(insDatosImprFiscal.GetFecha))
      Mes = gConvert.fConvierteAString(Month(insDatosImprFiscal.GetFecha))
      Ano = gTexto.DfRight(Year(insDatosImprFiscal.GetFecha), 2)
      Hora = gConvert.fConvierteAString(Hour(insDatosImprFiscal.GetHoraModificacion))
      Minuto = gConvert.fConvierteAString(Minute(insDatosImprFiscal.GetHoraModificacion))
      Segundo = "00"
      If gTexto.DfLen(Dia) = 1 Then
         Dia = "0" & Dia
      End If
      If gTexto.DfLen(Mes) = 1 Then
         Mes = "0" & Mes
      End If
      Retorno = Bematech_FI_AbreNotaDeCredito(Nombre, NumeroSerie, Rif, Dia, Mes, Ano, Hora, Minuto, Segundo, CCO)
      insImprFiscal.fVerificaRetornoImpresora "", Retorno, mReady
      If mReady Then
         sImprimeVentaArticulo_BEMATECH
      End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImprimeNotaDeCredito_BEMATECH", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImprimeVentaArticulo_Epson(valPuerto As String)
      Dim rsDatosArticulo As ADODB.Recordset
      Dim Alicuotas As String
      Dim Cantidades As String
      Dim Precios As String
      Dim Salida As String
      Dim Descripcion As String
      Dim DescuentoRenglon As Currency
      Dim vDescuentoRenglonString As String
      Dim vSerial As String
      Dim vRollo As String
      On Error GoTo h_Error
      Set rsDatosArticulo = insDatosImprFiscal.GetItems
      If Not insDatosImprFiscal.GetImprimeDireccionAlFinalDelComprobanteFiscal Then
         sImprimeDireccionEpson insDatosImprFiscal.GetDireccion, valPuerto
      End If
      If Not rsDatosArticulo.BOF And Not rsDatosArticulo.EOF Then
         rsDatosArticulo.MoveFirst
         Do While Not rsDatosArticulo.EOF
            Alicuotas = fConvierteAlicuotaBEMATECH(rsDatosArticulo.Fields("AlicuotaIVA").Value)
            Cantidades = fConvierteCantidadEpson(rsDatosArticulo.Fields("Cantidad").Value, False)
            Precios = fConvierteCantidadEpson(rsDatosArticulo.Fields("PrecioSinIVA").Value, True)
            DescuentoRenglon = rsDatosArticulo.Fields("Descuento").Value
            vSerial = rsDatosArticulo.Fields("Serial").Value
            vRollo = rsDatosArticulo.Fields("Rollo").Value
            If gTexto.DfLen(vSerial) > 0 Then
               If fExtraItem(vSerial, 0, valPuerto) Then
               Else
                  fCancelaCuponFiscal_EPSON valPuerto
                  mReady = False
                  Exit Do
               End If
            End If
            If gTexto.DfLen(vRollo) > 0 Then
               If fExtraItem(vRollo, 0, valPuerto) Then
               Else
                  fCancelaCuponFiscal_EPSON valPuerto
                  mReady = False
                  Exit Do
               End If
            End If
            If insDatosImprFiscal.GetPermitirDescripcionDelArticuloExtendida Then
               Descripcion = fCadenaValidaEpson(rsDatosArticulo.Fields("Descripcion").Value, True)
               Salida = Chr$(65) & Chr$(FIELD)
               Salida = Salida & gTexto.DfMid(Descripcion, 1, 40) & Chr$(FIELD)
               sEnviar Salida, "", valPuerto
               Descripcion = "COD:" & fCadenaValidaEpson(rsDatosArticulo.Fields("Codigo").Value)
            Else
               Descripcion = gTexto.DfLeft(fCadenaValidaEpson(rsDatosArticulo.Fields("Descripcion").Value), 20)
            End If
            If fItem(Descripcion, 0, Cantidades, Precios, Alicuotas, valPuerto) Then
               If DescuentoRenglon <> 0 Then
                  DescuentoRenglon = (rsDatosArticulo.Fields("Cantidad").Value * rsDatosArticulo.Fields("PrecioSinIVA").Value) * DescuentoRenglon / 100
                  vDescuentoRenglonString = gConvert.fConvierteAString(gConvert.ConvierteAInteger(gConvert.fDfTruncaANDecimales(DescuentoRenglon * 100, 0)))
                  Salida = Chr$(68) & Chr$(FIELD) & "Descuento" & Chr(FIELD) & vDescuentoRenglonString & Chr(FIELD) & "P" & Chr(FIELD) & Alicuotas
                  sEnviar Salida, "", valPuerto
                  Descripcion = ""
                  Salida = ""
               End If
            Else
               fCancelaCuponFiscal_EPSON valPuerto
               mReady = False
               Exit Do
            End If
            rsDatosArticulo.MoveNext
         Loop
      End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImprimeVentaArticulo_Epson", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImprimeNotaDeCredito_EPSON(valPuerto As String)
      Dim rsDatosArticulo As ADODB.Recordset
      Dim Alicuotas As String
      Dim Cantidades As String
      Dim Precios As String
      Dim Salida As String
      Dim DescuentoRenglon As Currency
      Dim Descripcion As String
      On Error GoTo h_Error
      Set rsDatosArticulo = insDatosImprFiscal.GetItems
      sAbreCFEpson valPuerto, True
      If Not rsDatosArticulo.BOF And Not rsDatosArticulo.EOF Then
         rsDatosArticulo.MoveFirst
         Do While Not rsDatosArticulo.EOF
            Alicuotas = fConvierteAlicuotaBEMATECH(rsDatosArticulo.Fields("AlicuotaIVA").Value)
            Cantidades = fConvierteCantidadEpson(Abs(rsDatosArticulo.Fields("Cantidad").Value), False)
            Precios = fConvierteCantidadEpson(rsDatosArticulo.Fields("PrecioSinIVA").Value, True)
            If insDatosImprFiscal.GetPermitirDescripcionDelArticuloExtendida Then
               Descripcion = fCadenaValidaEpson(rsDatosArticulo.Fields("Descripcion").Value, True)
               Salida = Chr$(65) & Chr$(FIELD)
               Salida = Salida & gTexto.DfMid(Descripcion, 1, 40) & Chr$(FIELD)
               sEnviar Salida, "", valPuerto
               Descripcion = "COD:" & fCadenaValidaEpson(rsDatosArticulo.Fields("Codigo").Value)
            Else
               Descripcion = gTexto.DfLeft(fCadenaValidaEpson(rsDatosArticulo.Fields("Descripcion").Value), 20)
            End If
            If fItem(Descripcion, 0, Cantidades, Precios, Alicuotas, valPuerto) Then
               DescuentoRenglon = rsDatosArticulo.Fields("Descuento").Value
               If DescuentoRenglon > 0 Then
                  DescuentoRenglon = Abs((rsDatosArticulo.Fields("Cantidad").Value * rsDatosArticulo.Fields("PrecioSinIVA").Value) * DescuentoRenglon / 100)
                  If rsDatosArticulo.Fields("Descuento").Value < 10 Then
                     DescuentoRenglon = DescuentoRenglon * 10
                  Else
                     DescuentoRenglon = DescuentoRenglon * 100
                  End If
                  Salida = Chr$(68) & Chr$(FIELD) & "Descuento" & Chr(FIELD) & DescuentoRenglon & Chr(FIELD) & "P" & Chr(FIELD) & Alicuotas
                  sEnviar Salida, "", valPuerto
               End If
            End If
            rsDatosArticulo.MoveNext
         Loop
      End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImprimeNotaDeCredito_EPSON", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sFormaDePago_EPSON()
   Dim MontoCancelado As Currency
   Dim rsFormaDePago As ADODB.Recordset
   Dim vFormaDePagoStr As String
   Dim Salida As String
   Dim EnumAdministrativo As clsEnumAdministrativo
   On Error GoTo h_Error
   Set EnumAdministrativo = New clsEnumAdministrativo
   Set rsFormaDePago = insDatosImprFiscal.GetFormaDePago
   If gDbUtil.fRecordCount(rsFormaDePago) > 0 Then
      If Not insDatosImprFiscal.GetEsNotaDeCredito Then
         rsFormaDePago.MoveFirst
         While Not rsFormaDePago.EOF
            MontoCancelado = 0
            vFormaDePagoStr = gConvert.fConvierteAString(rsFormaDePago.Fields("CodigoFormaDelCobro").Value)
            Select Case vFormaDePagoStr
               Case "00001"
                  vFormaDePagoStr = EnumAdministrativo.enumFormaDeCancelarPagoToString(eFD_EFECTIVO)
               Case "00002"
                  vFormaDePagoStr = EnumAdministrativo.enumFormaDeCancelarPagoToString(eFD_CHEQUE)
               Case "00003"
                  vFormaDePagoStr = EnumAdministrativo.enumFormaDeCancelarPagoToString(eFD_TARJETA)
               Case Else
                  vFormaDePagoStr = EnumAdministrativo.enumFormaDeCancelarPagoToString(eFD_EFECTIVO)
            End Select
            vFormaDePagoStr = gTexto.DfLeft(vFormaDePagoStr, 20)
            MontoCancelado = gConvert.fConvertStringToCurrency(rsFormaDePago.Fields("Monto").Value)
            If gDbUtil.fRecordCount(rsFormaDePago) > 1 Then
               Salida = Chr$(68) & Chr$(FIELD) & vFormaDePagoStr & Chr(FIELD) & MontoCancelado & Chr(FIELD) & "T" & Chr(FIELD) & Chr(127)
               sEnviar Salida, "", insDatosImprFiscal.GetPuertoImpresoraFiscal
            End If
            rsFormaDePago.MoveNext
         Wend
      End If
   End If
   Set EnumAdministrativo = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sFormaDePago_EPSON", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sFormaDePagoCF_BEMATECH()
   Dim Retorno As Integer
   Dim MontoCancelado As String
   Dim rsFormaDePago As ADODB.Recordset
   Dim formadePago As String
   Dim EnumAdministrativo As clsEnumAdministrativo
   On Error GoTo h_Error
   Set EnumAdministrativo = New clsEnumAdministrativo
   Set rsFormaDePago = insDatosImprFiscal.GetFormaDePago
   If Not insDatosImprFiscal.GetEsNotaDeCredito Then
      rsFormaDePago.MoveFirst
      While Not rsFormaDePago.EOF
         MontoCancelado = 0
         Select Case rsFormaDePago("CodigoFormaDelCobro").Value
            Case "00001":       formadePago = EnumAdministrativo.enumFormaDeCancelarPagoToString(eFD_EFECTIVO)
            Case "00002":       formadePago = EnumAdministrativo.enumFormaDeCancelarPagoToString(eFD_CHEQUE)
            Case "00003":       formadePago = EnumAdministrativo.enumFormaDeCancelarPagoToString(eFD_TARJETA)
            Case "00004":       formadePago = EnumAdministrativo.enumFormaDeCancelarPagoToString(eFD_DEPOSITO)
            Case Else:          formadePago = EnumAdministrativo.enumFormaDeCancelarPagoToString(eFD_EFECTIVO)
         End Select
         formadePago = gTexto.DfLeft(formadePago, 20)
         If Not (IsNull(rsFormaDePago.Fields("Monto").Value)) Then
            MontoCancelado = gConvert.fConvierteAString(rsFormaDePago.Fields("Monto").Value) '
            MontoCancelado = fConvierteCantidadBEMATECHParaPagos(MontoCancelado)
            If rsFormaDePago.RecordCount > 0 Then
               Retorno = Bematech_FI_EfectuaFormaPagoDescripcionForma(formadePago, MontoCancelado, "")
               insImprFiscal.fVerificaRetornoImpresora "", Retorno, mReady
            End If
         End If
         rsFormaDePago.MoveNext
      Wend
   End If
   mReady = True
   Set EnumAdministrativo = Nothing
   Set rsFormaDePago = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sFormaDePagoCF_BEMATECH", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sFormaDePagoCF_EPSON()
   Dim Salida As String
   Dim TXT As String
   Dim verificado As Boolean
   Dim Monto As String
   On Error GoTo h_Error
   If gTexto.DfLen(insDatosImprFiscal.GetMontoDelPago) > 0 Then
      TXT = insDatosImprFiscal.GetMontoDelPago
      verificado = verifica(2, TXT)
      If Not verificado Then
         Exit Sub
      End If
      Monto = TXT
      Salida = Chr$(68) & Chr$(FIELD)
      Salida = Salida & Chr$(127) & Chr$(FIELD)
      Salida = Salida & Monto & Chr$(FIELD)
      Salida = Salida & "T"
      sEnviar Salida, "", insDatosImprFiscal.GetPuertoImpresoraFiscal
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sFormaDePagoCF_EPSON", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCierraCF_BEMATECH(ByVal vModeloImpresora As Enum_ImpresorasFiscales)
   Const Descuento As String = "D"
   Const Porcentaje As String = "%"
   Dim Retorno As Integer
   Dim ValorDelDescuento As String
   Dim ImprimioFormaDePago As Boolean
   Dim vCamposDefinibles As String
   Dim vfieldDef As String
   Dim vSinObsevSinDireccion As Boolean
   Dim vCaracteresRestantes As Integer
   Dim vTotalMonedaExtranjera As String
   On Error GoTo h_Error
   ImprimioFormaDePago = False
   ValorDelDescuento = fConvierteCantidadBEMATECHParaDescuentos(insDatosImprFiscal.GetPorcentajeDeDescuento)
   Retorno = Bematech_FI_IniciaCierreCupon(Descuento, Porcentaje, ValorDelDescuento)
   insImprFiscal.fVerificaRetornoImpresora "", Retorno, mReady
   If Not insDatosImprFiscal.GetFormaDePago Is Nothing Then
      sFormaDePagoCF_BEMATECH
   End If
   If insDatosImprFiscal.GetEsNotaDeCredito And vModeloImpresora = eIF_BEMATECH_MP_20_FI_II Then
      Retorno = Bematech_FI_FinalizarCierreCupon(mMensaje)
   Else
       vTotalMonedaExtranjera = insDatosImprFiscal.GetTotalesMonedaExtranjera
       If gTexto.DfLen(vTotalMonedaExtranjera) > 0 And Not insDatosImprFiscal.GetEsNotaDeCredito Then
         vCaracteresRestantes = gTexto.DfLen(vTotalMonedaExtranjera)
         mMensaje = mMensaje & vTotalMonedaExtranjera
       End If
      If gTexto.DfLen(insDatosImprFiscal.GetObservaciones) > 0 Then
          mMensaje = mMensaje & vbCrLf & gTexto.DfLeft(insDatosImprFiscal.GetObservaciones, 320 - vCaracteresRestantes)
      End If
      vSinObsevSinDireccion = (insDatosImprFiscal.GetObservaciones = "" And Not (insDatosImprFiscal.GetDireccion <> "" And insDatosImprFiscal.GetImprimeDireccionAlFinalDelComprobanteFiscal))
      If insDatosImprFiscal.GetImprimeCamposDefinibles And gTexto.DfLen(vTotalMonedaExtranjera) = 0 Then
        sImprimeCamposDefiniblesBEMATECH insDatosImprFiscal, vSinObsevSinDireccion, vCamposDefinibles
        mMensaje = mMensaje & vCamposDefinibles
      End If
      Retorno = Bematech_FI_FinalizarCierreCupon(mMensaje)
      insImprFiscal.fVerificaRetornoImpresora "", Retorno, mReady
    End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCierraCF_BEMATECH", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCierraCF_EPSON(ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, valPuerto As String)
   Dim vRsCamposDefinibles As ADODB.Recordset
   Dim vRsDatosVehiculos As ADODB.Recordset
   Dim vMax As Long
   Dim vUltimoComprobanteFiscal As String
   On Error GoTo h_Error
   vMax = 12
   sDescuentoCF_EPSON (insDatosImprFiscal.GetPorcentajeDeDescuento), valPuerto
   If Not insDatosImprFiscal.GetFormaDePago Is Nothing Then
      sFormaDePago_EPSON
   End If
   'sFormaDePagoCF_EPSON
   sEnviar Chr$(69) & Chr(FIELD) & "A", "", valPuerto 'Cierre Parcial
   If gTexto.DfLen(insDatosImprFiscal.GetNombreCliente) = 0 Then
      'sEnviar Chr$(69) & Chr$(FIELD) & "A"
      sEnviar Chr$(&H41) & Chr$(FIELD) & "   Sin Derecho a Credito Fiscal" & Chr$(FIELD) & "T", "", valPuerto
   End If
   If mReady Then
      If insDatosImprFiscal.GetImprimeDireccionAlFinalDelComprobanteFiscal Then
         sImprimeDireccionEpson insDatosImprFiscal.GetDireccion, valPuerto
         If Not mReady Then mReady = True
      End If
      sImprimeObservacionesEpson insDatosImprFiscal.GetObservaciones, valPuerto
      If Not mReady Then mReady = True
      If insDatosImprFiscal.GetImprimeDatosVehiculos Then
         Set vRsDatosVehiculos = insDatosImprFiscal.GetRSDatosVehiculos
         If Not (vRsDatosVehiculos Is Nothing) Then
            If gDbUtil.fRecordCount(vRsDatosVehiculos) > 0 Then
               sImprimeDatosVehiculosEpson insDatosImprFiscal.GetRSDatosVehiculos, valPuerto
            End If
         End If
         vMax = 5
      End If
      If insDatosImprFiscal.GetImprimeCamposDefinibles Then
         Set vRsCamposDefinibles = insDatosImprFiscal.GetRSCamposDefinibles
         If Not (vRsCamposDefinibles Is Nothing) Then
            If gDbUtil.fRecordCount(vRsCamposDefinibles) > 0 Then
               sImprimeCamposDefiniblesEpson insDatosImprFiscal.GetRSCamposDefinibles, vMax, valPuerto
            End If
         End If
      End If
      vUltimoComprobanteFiscal = fUltimoNumeroMemoriaFiscal_EPSON(valImpresoraFiscal, valPuerto)
      sEnviar Chr$(69), "", valPuerto  'Cierre total
      If gConvert.ConvierteAlong(vUltimoComprobanteFiscal) = gConvert.ConvierteAlong(insDatosImprFiscal.GetUltimoNumeroFiscal) Then
         mReady = False
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCierraCF_EPSON", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fItem(Des As String, efecto As Integer, Cant As String, Monto As String, Tasa As String, valPuerto As String, Optional Tipo As Integer) As Boolean
'Este comando se utiliza para ingresar los diferentes productos durante el proceso de venta
'Des: contiene la descripción del articulo en venta
'Efecto: indica el efecto de impresion que se aplicará sobre el articulo.
'Cant: indica la cantida del producto
'Monto: es el precio sin impuesto del articulo
'tasa: es la tasa de impuesto que corresponde a dicho producto
'tipo: indica la forma en la cual el producto es adicionado al comprobante. Si tipo = "m"  el producto se concidera como una anulacion
   Dim Dev As String
   Dim TXT As String
   Dim Salida As String
   Dim vExito As Boolean
'Establece las condiciones iniciales
   On Error GoTo h_Error
   vExito = False
   TXT = Cant
   vExito = verifica(3, TXT)
   If Not vExito Then
      GoTo h_EXIT
   End If
   Cant = TXT

   TXT = Monto
   vExito = verifica(2, TXT)
   If Not vExito Then
      GoTo h_EXIT
   End If
   Monto = TXT

   TXT = Tasa
   vExito = verifica(2, TXT)
   If Not vExito Then
      GoTo h_EXIT
   End If
   Tasa = TXT

   Dev = "M"
   If Tipo = 1 Then
      Dev = "M"
   End If

   Select Case efecto
      Case Is = 1
         Des = Chr$(241) & Des
      Case Is = 2
         Des = Chr$(248) & Des
      Case Is = 3
         Des = Chr$(249) & Des
   End Select
   
   Salida = Chr$(66) & Chr$(FIELD)
   Salida = Salida & Des & Chr$(FIELD)
   gTexto.ReemplazaCaracteresEnElString Cant, ",", ""
   Salida = Salida & Cant & Chr$(FIELD)
   gTexto.ReemplazaCaracteresEnElString Monto, ",", ""
   Salida = Salida & Monto & Chr$(FIELD)
   Salida = Salida & Tasa & Chr$(FIELD)
   Salida = Salida & Dev & Chr$(FIELD) & Chr$(&H7F) & Chr$(FIELD) & Chr$(&H7F) & Chr$(FIELD) & Chr$(&H7F)
  
'Salida = Chr$(66) & Chr$(FIELD)
'Salida = Salida & "DAVID" & Chr$(FIELD)
'Salida = Salida & "1000" & Chr$(FIELD)
'Salida = Salida & "10000" & Chr$(FIELD)
'Salida = Salida & "1100" & Chr$(FIELD)
'Salida = Salida & "M" & Chr$(FIELD) & Chr$(&H7F) & Chr$(FIELD) & Chr$(&H7F) & Chr$(FIELD) & Chr$(&H7F)
   
   sEnviar Salida, "", valPuerto
'Call Campos
 'Call DefEstados
'R esp = Entrada

   vExito = mReady 'True
'If (EstadoImpresora_15 = True) Or (EstadoFiscal_15 = True) Then
'    Item = False
'End If
h_EXIT:
   fItem = vExito
   Exit Function
h_Error:
   fItem = False
   'Resume Next
End Function

Private Function verifica(NC As String, ByVal TXT As String) As Boolean
'Crea un campo que cumple con las especificaciones del protocolo de la TMu200AF
'a partir de un monto (string TXT) que puede tener coma o punto como separador decimal
'y que debe ser un valor numerico
'El valor NC indica el # de decimales que deben conciderarse despues del separador decimal
   Dim NumComa As Integer
   Dim Longitud As Integer
   Dim TxtI As String
   Dim TxtD As String
   Dim NumDecimales As Integer

'prevee la rutina de errores
   On Error GoTo EnErrordedatos

'verifica la existencia del separador de decimales
   NumComa = InStr(TXT, ",")
   If NumComa = 0 Then
      NumComa = InStr(TXT, ".")
   End If

'establece la longitud máxima de la cadena
   Longitud = Len(TXT)

   If NumComa = 0 Then
      TXT = TXT * (10 ^ NC)
   Else
      TxtI = Left(TXT, NumComa - 1)
      If NumComa = Longitud Then
         TXT = TXT * (10 ^ NC)
      Else
         NumDecimales = Longitud - NumComa
         TxtD = Right(TXT, NumDecimales)
         TxtD = TxtD & "0000000000000000000000000000"
         TxtD = Left(TxtD, NC)
         TXT = TxtI & TxtD
      End If
   End If
   verifica = True
   Exit Function

'rutina para control de errores
EnErrordedatos: Err.Raise 100, "Error en trama de datos"
   TXT = ""
   verifica = False
   Exit Function
End Function

Private Sub sDescuentoCF_EPSON(ByVal Porcentaje As String, valPuerto As String)
'Esta rutina permite realizar un descuento global sobre el subtotal de un comprobante fiscal
'porcentaje = representa el porcentaje que será aplicado al subtotal( nn,nnnnnn%)
   Dim TXT As String
   Dim Verifico As Boolean
   Dim Salida As String
   
   TXT = 100 - Porcentaje
   TXT = TXT * 1000000
   Salida = Chr$(68) & Chr$(FIELD) & Chr(127) & Chr(FIELD) & TXT & Chr(FIELD) & "D" & Chr(FIELD) & Chr(127)
   If gTexto.DfLen(Porcentaje) > 0 And Porcentaje <> "0" Then
      'TXT = Porcentaje
      
      'Verifico = verifica(8, TXT)
'      If Not Verifico Then
'        Exit Sub
'      End If
'      Porcentaje = CDec(TXT)
'      Salida = Chr$(68) & Chr$(FIELD)
'      Salida = Salida & Chr$(127) & Chr$(FIELD)
'      Salida = Salida & Porcentaje & Chr$(FIELD)
'      Salida = Salida & "D"
      sEnviar Salida, "", valPuerto
   End If
End Sub

Private Sub sObtenerSerialImpresoraFiscal(ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, valPuerto As String, valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String)
   Dim Serial As String
   Dim insImpFiscalBMC As clsUtilImpFiscalBMC
   Dim insImpFiscalQPrint As clsImpFiscalQPrint
   Dim insImpFiscalFamFactory As clsImpFiscalFamFactory
   Dim insImpFiscalEleposVMAX As clsImpFiscalEleposVMAX
   Dim insImpFiscalFromAOS As clsImpFiscalFromAOS
   On Error GoTo h_Error
   If insDatosImprFiscal.GetUsarModoDotNet Then
      Set insImpFiscalFromAOS = New clsImpFiscalFromAOS
      Serial = insImpFiscalFromAOS.fSerialMemoriaFiscal(mReady, valPuerto, valImpresoraFiscal)
      Set insImpFiscalFromAOS = Nothing
   Else
       Select Case valImpresoraFiscal
          Case eIF_EPSON_PF_220:              Serial = fSerialMemoriaFiscal_EPSON(valPuerto)
          Case eIF_EPSON_PF_300:              Serial = fSerialMemoriaFiscal_EPSON(valPuerto)
          Case eIF_EPSON_PF_220II:            Serial = fSerialMemoriaFiscal_EPSON(valPuerto)
          Case eIF_EPSON_TM_675_PF:           Serial = fSerialMemoriaFiscal_EPSON(valPuerto)
          Case eIF_EPSON_TM_950_PF:           Serial = fSerialMemoriaFiscal_EPSON(valPuerto)
          Case eIF_BEMATECH_MP_20_FI_II, eIF_BEMATECH_MP_2100_FI, eIF_BEMATECH_MP_4000_FI: Serial = fSerialMemoriaFiscal_BEMATECH
       Case eIF_DASCOM_TALLY_1125, eIF_SAMSUNG_BIXOLON_SRP_350, eIF_OKI_ML_1120, eIF_SAMSUNG_BIXOLON_SRP_270, eIF_ACLAS_PP1F3, eIF_DASCOM_TALLY_DT_230, eIF_SAMSUNG_BIXOLON_SRP_280, eIF_ACLAS_PP9, eIF_SAMSUNG_BIXOLON_SRP_812, eif_HKA80, eif_HKA112:
             Set insImpFiscalFamFactory = New clsImpFiscalFamFactory
             Serial = insImpFiscalFamFactory.fSerialMemoriaFiscal(mReady, insDatosImprFiscal.GetPuertoImpresoraFiscal, insDatosImprFiscal.GetImpresoraFiscal)
             Set insImpFiscalFamFactory = Nothing
             
       Case eIF_BMC_CAMEL, eIF_BMC_SPARK_614, eIF_BMC_TH34_EJ
             Set insImpFiscalBMC = New clsUtilImpFiscalBMC
             Serial = insImpFiscalBMC.fSerialMemoriaFiscal_BMC(mReady, insDatosImprFiscal.GetPuertoImpresoraFiscal, insDatosImprFiscal.GetImpresoraFiscal)
             Set insImpFiscalBMC = Nothing
             
       Case eIF_QPRINT_MF
            Set insImpFiscalQPrint = New clsImpFiscalQPrint
            Serial = insImpFiscalQPrint.fSerialMemoriaFiscal_QPrintMF(mReady, insDatosImprFiscal.GetPuertoImpresoraFiscal, valTipoConexion, valIp, valCajaNumero)
            Set insImpFiscalQPrint = Nothing
       Case eIF_ELEPOS_VMAX_220_F, eIF_ELEPOS_VMAX_300, eIF_ELEPOS_VMAX_580
            Set insImpFiscalEleposVMAX = New clsImpFiscalEleposVMAX
            insImpFiscalEleposVMAX.InitializeValues obVMAX
            Serial = insImpFiscalEleposVMAX.fSerialMemoriaFiscal(mReady, insDatosImprFiscal.GetPuertoImpresoraFiscal, insDatosImprFiscal.GetImpresoraFiscal)
            Set insImpFiscalEleposVMAX = Nothing
      End Select
   End If
   insDatosImprFiscal.SetSerialImpresoraFiscal Serial
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sObtenerSerialImpresoraFiscal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fSerialMemoriaFiscal_BEMATECH() As String
   Dim Serial As String
   Dim Retorno As Integer
   Dim InitSawmaquina As clsInitSawmaquina
   Dim insImprFiscal As clsUtilImprFiscal
   On Error GoTo h_Error
   Set InitSawmaquina = New clsInitSawmaquina
   Set insImprFiscal = New clsUtilImprFiscal
   Retorno = 0
   Serial = Space(15)
   InitSawmaquina.sInitializeInternalGlobalClasses gError, gMessage, gTexto, gDefGen, gConvert, gApi, gGlobalization, gDbUtil, gUtilFile, gUtilMathOperations, gProyUsuarioActual, gEnumProyecto, gDefGen, gWorkPaths
   If (fModificarArchivoPropiedadesBematech("Puerta", "COM" & insDatosImprFiscal.GetPuertoImpresoraFiscal)) Then
      If (Bematech_FI_AbrePuertaSerial <> -5) Then
         Serial = Space(15)
         Retorno = Bematech_FI_NumeroSerie(Serial)
         Bematech_FI_CierraPuertaSerial
         Serial = gTexto.fLimpiaStringdeBlancosAAmbosLados(Serial)
         mReady = True
      Else
         mReady = False
         gMessage.Advertencia "No se puede establecer comunicación con la impresora." & vbCrLf & "1.- Por favor salga del Sistema." & vbCrLf & "2.-  Verifique todas las conexiones desde el PC a l Impresora. " & vbCrLf & "3.- Vuelva a Intentar la operación."
      End If
   End If
h_EXIT: On Error GoTo 0
   fSerialMemoriaFiscal_BEMATECH = Serial
   Exit Function
h_Error:
   mReady = False
    Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSerialMemoriaFiscal_BEMATECH", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fModificarArchivoPropiedadesBematech(valPropiedad As String, valNuevoValor As String) As Boolean
    Dim vPath As String
    Dim vModifico As Boolean
    On Error GoTo h_Error
    vPath = "C:\Windows\System32\BemaFI32.ini"
    If (gUtilFile.fExisteElArchivo(vPath)) Then
        fReemplazarLinea vPath, valPropiedad, valNuevoValor
    Else
        gMessage.Advertencia "No se encuentra el archivo: BemaFI32.ini, en la ubicacion C:\Windows\System32, no se puede continuar con la configuracion"
    End If
   
h_EXIT: On Error GoTo 0
    fModificarArchivoPropiedadesBematech = True
   Exit Function
h_Error:
    fModificarArchivoPropiedadesBematech = False
    Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sModificarArchivoPropiedadesBematech", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Function

Public Function fReemplazarLinea(ByVal rutaArchivo As String, valNombreIdentificador As String, valNuevoValor As String) As Boolean
   Dim lineaActual As String
   Dim fileNumberForRead As Integer
   Dim fileNumberForWrite As Integer
   Dim varRutaActual As String
   Dim varFileNameBackUp As String
   Dim varNombreArchivoOld As String
   Dim got_file As Boolean
   Dim encontreElIdentificador As Boolean
   Dim varCadenaAuxiliar As String
   Dim vPuntoCorte As Long
   On Error GoTo h_Error
   fReemplazarLinea = False
   encontreElIdentificador = False
   fileNumberForRead = 1
   fileNumberForWrite = 2
   varRutaActual = rutaArchivo
   varFileNameBackUp = gTexto.DfReplace(rutaArchivo, ".ini", ".backup")
   varNombreArchivoOld = gTexto.DfReplace(rutaArchivo, ".INI", ".OLD")
   If Dir(varNombreArchivoOld) <> "" Then _
      Kill varNombreArchivoOld
   Open varRutaActual For Input As fileNumberForRead
   Open varFileNameBackUp For Output As fileNumberForWrite
   got_file = (Err.Number = 0)
   On Error GoTo 0
   If got_file Then
      Do While Not EOF(fileNumberForRead)
         Line Input #fileNumberForRead, lineaActual
         vPuntoCorte = InStr(lineaActual, "=") - 1
         If (vPuntoCorte > 0) Then
            varCadenaAuxiliar = Mid(lineaActual, 1, vPuntoCorte)
         Else
            varCadenaAuxiliar = lineaActual
         End If
         If gTexto.fS1EsIgualAS2(valNombreIdentificador, varCadenaAuxiliar) Then
            encontreElIdentificador = True
            Print #fileNumberForWrite, valNombreIdentificador & "=" & valNuevoValor
         Else
            Print #fileNumberForWrite, lineaActual
         End If
      Loop
        If Not encontreElIdentificador Then
            Print #fileNumberForWrite, valNombreIdentificador & "=" & valNuevoValor
        End If
      Close fileNumberForRead 'Cerrando el archivo de lectura
   End If
   Close fileNumberForWrite 'Cerrando el archivo de escritura
   Name varRutaActual As varNombreArchivoOld
   Name varFileNameBackUp As varRutaActual
   fReemplazarLinea = True
h_EXIT:
   On Error GoTo 0
   Exit Function
h_Error:
   fReemplazarLinea = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fReemplazarLinea", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSerialMemoriaFiscal_EPSON(valPuerto As String) As String
   Dim Salida As String
   On Error GoTo h_Error
   Salida = Chr$(128)
   sEnviar Salida, "", valPuerto
   If mReady Then
      Salida = ValorCampo
      Salida = ValorCampo
      mSerial = ValorCampo
      fSerialMemoriaFiscal_EPSON = mSerial
   Else
      fSerialMemoriaFiscal_EPSON = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_Error:
   If Err.Number = 1000005 Or Err.Number = 5 Then
      gMessage.Advertencia "Error de comunicación."
      Err.Clear
      mReady = False
   Else
      If Err.Number = 8002 Then
         gMessage.Advertencia ("Verifique que la Impresora Fiscal este conectada al puerto que selecciono e intente de nuevo")
      Else
         Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSerialMemoriaFiscal_EPSON", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
      End If
   End If
End Function

Public Function fStatusEPSON(valPuerto As String) As String
   Dim vRespuesta As String
   Dim vSalida As String
   Dim vCampo As String
   On Error GoTo h_Error
   vRespuesta = ""
   vSalida = Chr$(56) & Chr$(FIELD) & "N" ' Comando 38h
   sEnviar vSalida, vRespuesta, valPuerto ' Envia Comando a Printer y Obtiene respues en vRespuesta
   'Busca campo 04 para ver respuesta
   vCampo = vRespuesta
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
h_EXIT: On Error GoTo 0
   fStatusEPSON = vCampo
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function ValorCampo() As String
'Esta rutina permite separar por campos a la trama de respuesta (colocada en mSerial) que llega desde
'la impresora fiscal
   Dim inicio As Integer
   Dim fin As Integer
   Dim Campo As String
   inicio = InStr(1, mSerial, Chr(28))
   fin = InStr(inicio + 1, mSerial, Chr(28))
   If fin = 0 Then
      fin = InStr(inicio + 1, mSerial, Chr(3))
   End If
   If fin = 0 Then
      ValorCampo = ""
      Exit Function
   End If
   Campo = Left(mSerial, fin - 1)
   Campo = Right(Campo, Len(Campo) - inicio)
   ValorCampo = Campo
   mSerial = Right(mSerial, Len(mSerial) - fin + 1)
End Function

Private Sub sObtenerUltimoNumeroFiscal(ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, ByRef Ready As Boolean, ByVal valPuerto As String, ByVal valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String)
   Dim NumeroCupon As String
   Dim insImprFiscalBMC As clsUtilImpFiscalBMC
   Dim insImprFiscalQPrint As clsImpFiscalQPrint
   Dim insImpFiscalFamFactory As clsImpFiscalFamFactory
   Dim insImpFiscalEleposVMAX As clsImpFiscalEleposVMAX
   Dim insImpFiscalFromAOS As clsImpFiscalFromAOS
   On Error GoTo h_Error
   
   If insDatosImprFiscal.GetUsarModoDotNet Then
   Set insImpFiscalFromAOS = New clsImpFiscalFromAOS
        NumeroCupon = insImpFiscalFromAOS.fUltimoNumeroMemoriaFiscal(valPuerto, valImpresoraFiscal, insDatosImprFiscal.GetEsNotaDeCredito, mReady)
   Set insImpFiscalFromAOS = Nothing
   Else
    Select Case valImpresoraFiscal
       Case eIF_BEMATECH_MP_20_FI_II, eIF_BEMATECH_MP_2100_FI, eIF_BEMATECH_MP_4000_FI: NumeroCupon = fUltimoNumeroMemoriaFiscal_BEMATECH
       Case eIF_EPSON_PF_300:                 NumeroCupon = fUltimoNumeroMemoriaFiscal_EPSON(valImpresoraFiscal, valPuerto)
       Case eIF_EPSON_PF_220:                 NumeroCupon = fUltimoNumeroMemoriaFiscal_EPSON(valImpresoraFiscal, valPuerto)
       Case eIF_EPSON_PF_220II:               NumeroCupon = fUltimoNumeroMemoriaFiscal_EPSON(valImpresoraFiscal, valPuerto)
       Case eIF_EPSON_TM_675_PF:              NumeroCupon = fUltimoNumeroMemoriaFiscal_EPSON(valImpresoraFiscal, valPuerto)
       Case eIF_EPSON_TM_950_PF:              NumeroCupon = fUltimoNumeroMemoriaFiscal_EPSON(valImpresoraFiscal, valPuerto)
       
       Case eIF_DASCOM_TALLY_1125, eIF_SAMSUNG_BIXOLON_SRP_270, eIF_OKI_ML_1120, eIF_SAMSUNG_BIXOLON_SRP_350, eIF_ACLAS_PP1F3, eIF_DASCOM_TALLY_DT_230, eIF_SAMSUNG_BIXOLON_SRP_280, eIF_ACLAS_PP9, eIF_SAMSUNG_BIXOLON_SRP_812, eif_HKA80, eif_HKA112:
          Set insImpFiscalFamFactory = New clsImpFiscalFamFactory
          NumeroCupon = insImpFiscalFamFactory.fUltimoNumeroMemoriaFiscal(insDatosImprFiscal.GetPuertoImpresoraFiscal, insDatosImprFiscal.GetImpresoraFiscal, insDatosImprFiscal.GetEsNotaDeCredito, mReady)
          Set insImpFiscalFamFactory = Nothing
       
       Case eIF_BMC_CAMEL, eIF_BMC_SPARK_614, eIF_BMC_TH34_EJ
          Set insImprFiscalBMC = New clsUtilImpFiscalBMC
          NumeroCupon = insImprFiscalBMC.fUltimoNumeroMemoriaFiscal_BMC(mReady, insDatosImprFiscal, valPuerto)
          Set insImprFiscalBMC = Nothing
      Case eIF_QPRINT_MF
          Set insImprFiscalQPrint = New clsImpFiscalQPrint
          NumeroCupon = insImprFiscalQPrint.fUltimoNumeroMemoriaFiscal_QPrintMF(mReady, insDatosImprFiscal, valPuerto, valTipoConexion, valIp, valCajaNumero)
          Set insImprFiscalQPrint = Nothing
       Case eIF_ELEPOS_VMAX_220_F, eIF_ELEPOS_VMAX_300, eIF_ELEPOS_VMAX_580
         Set insImpFiscalEleposVMAX = New clsImpFiscalEleposVMAX
         insImpFiscalEleposVMAX.InitializeValues obVMAX
         NumeroCupon = insImpFiscalEleposVMAX.fUltimoNumeroMemoriaFiscal(insDatosImprFiscal.GetPuertoImpresoraFiscal, insDatosImprFiscal.GetImpresoraFiscal, insDatosImprFiscal.GetEsNotaDeCredito, mReady)
         Set insImpFiscalEleposVMAX = Nothing
    End Select
   End If
   insDatosImprFiscal.SetUltimoNumeroFiscal NumeroCupon
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sObtenerUltimoNumeroFiscal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fUltimoNumeroMemoriaFiscal_BEMATECH() As String
   Dim NumeroComprobante As String
   Dim vTextoEnElArchivo As String
   Dim vInicioCorte As Long
   Dim Retorno As Integer
   Dim vDirectorioRetorno As String
   Dim vPathFileName As String
   On Error GoTo h_Error
   NumeroComprobante = Space(6)
   If (Not insDatosImprFiscal.GetEsNotaDeCredito) Then
      Retorno = Bematech_FI_NumeroComprobanteFiscal(NumeroComprobante)
      insImprFiscal.fVerificaRetornoImpresora "", Retorno, mReady
   Else
      Retorno = Bematech_FI_LecturaXSerial()
      insImprFiscal.fVerificaRetornoImpresora "", Retorno, mReady
      If (gUtilFile.fExisteElArchivo("C:\Windows\System32\BemaFI32.ini")) Then
         vDirectorioRetorno = fValorDesdeArchivoINI("C:\Windows\System32\BemaFI32.ini", "Path", "C:\")
         If gTexto.DfRight(vDirectorioRetorno, 1) <> "\" Or gTexto.DfRight(vDirectorioRetorno, 1) <> "/" Then
            vDirectorioRetorno = vDirectorioRetorno & "\"
         End If
         vPathFileName = vDirectorioRetorno & "\Retorno.txt"
         If (gUtilFile.fExisteElArchivo(vPathFileName)) Then
            vTextoEnElArchivo = gUtilFile.fReadFile(vPathFileName)
            gUtilFile.fBorraElArchivo (vPathFileName)
            vInicioCorte = InStr(1, vTextoEnElArchivo, "Contador de Nota de Crédito:")
            vTextoEnElArchivo = gTexto.DfMid(vTextoEnElArchivo, vInicioCorte)
            vTextoEnElArchivo = gTexto.DfMid(vTextoEnElArchivo, 43, 6)
            NumeroComprobante = vTextoEnElArchivo
         Else
            NumeroComprobante = ""
         End If
      Else
         NumeroComprobante = ""
      End If
   End If
h_EXIT: On Error GoTo 0
   fUltimoNumeroMemoriaFiscal_BEMATECH = NumeroComprobante
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fUltimoNumeroMemoriaFiscal_BEMATECH", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fUltimoNumeroMemoriaFiscal_EPSON(ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, valPuerto As String) As String
   Dim Salida As String
   Dim NumeroCupon As String
   Dim vRespuesta As String
   Dim NroDeVecesAEjecutarLaFuncion As Integer
   On Error GoTo h_Error
   NroDeVecesAEjecutarLaFuncion = 0
VOLVERAJECUTAR:
   NroDeVecesAEjecutarLaFuncion = NroDeVecesAEjecutarLaFuncion + 1
   If (Not insDatosImprFiscal.GetEsNotaDeCredito) Or (valImpresoraFiscal = eIF_EPSON_PF_220) Then
      Salida = Chr$(56) & Chr$(FIELD) & Chr$(78)
      sEnviar Salida, "", valPuerto
      NumeroCupon = ValorCampoNumeroCupon
   ElseIf valImpresoraFiscal <> eIF_EPSON_PF_220 Then
      Salida = Chr$(56) & Chr$(FIELD) & "T" ' Comando 38h
      sEnviar Salida, vRespuesta, valPuerto
      NumeroCupon = ValorCampoNumeroCupon
      NumeroCupon = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
      NumeroCupon = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
      NumeroCupon = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
      NumeroCupon = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
      NumeroCupon = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
      NumeroCupon = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
      NumeroCupon = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
      NumeroCupon = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
      NumeroCupon = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
      NumeroCupon = gTexto.fPrimerTokenYLoEliminaDelTexto(NumeroCupon, "")
   End If
   fUltimoNumeroMemoriaFiscal_EPSON = NumeroCupon
   mReady = True
h_EXIT: On Error GoTo 0
   Exit Function
h_Error:
   If Err.Number = 5 Then
      If NroDeVecesAEjecutarLaFuncion >= 5 Then
         gMessage.Advertencia "Error de comunicación, Obteniendo el Nro de Impresión Fiscal." & vbCrLf & "Es recomendable que cierre el siste y verifique la conexión de su impresora con el computador."
         Err.Clear
         mReady = False
      Else
         GoTo VOLVERAJECUTAR
      End If
   Else
      Err.Raise Err.Number, "Time Out", Err.Description
      cmmPuerto.PortOpen = False
      mReady = False
   End If
End Function

Private Function ValorCampoNumeroCupon() As String
'Esta rutina permite separar por campos a la trama de respuesta (colocada en mSerial) que llega desde
'la impresora fiscal
   Dim inicio As Integer
   Dim fin As Integer
   Dim Campo As String
   inicio = InStr(45, mUltimoNoComprobanteFiscal, Chr(28))
   fin = InStr(inicio + 1, mUltimoNoComprobanteFiscal, Chr(28))
   If fin = 0 Then
      fin = InStr(inicio + 1, mUltimoNoComprobanteFiscal, Chr(3))
   End If
   If fin = 0 Then
      ValorCampoNumeroCupon = ""
      Exit Function
   End If
   Campo = Left(mSerial, fin - 1)
   Campo = Right(Campo, Len(Campo) - inicio)
   ValorCampoNumeroCupon = Campo
   mSerial = Right(mUltimoNoComprobanteFiscal, Len(mUltimoNoComprobanteFiscal) - fin + 1)
End Function

Private Function fValorCampoError() As Boolean
'Esta rutina permite separar por campos a la trama de respuesta (colocada en mSerial) que llega desde
'la impresora fiscal
   Dim inicio  As Integer
   Dim fin As Integer
   Dim Campo As String
   inicio = InStr(10, mStatusEpson, Chr(28))
   fin = InStr(inicio + 1, mStatusEpson, Chr(28))
   If fin = 0 Then
      fin = InStr(inicio + 1, mStatusEpson, Chr(3))
   End If
   If fin = 0 Then
      fValorCampoError = True
      Exit Function
   End If
   Campo = Left(mSerial, fin - 1)
   Campo = Right(Campo, Len(Campo) - inicio)
   If Campo = "ERROR 8" Then
      fValorCampoError = True
   Else
      fValorCampoError = False
   End If
End Function

Private Sub sEfectuaVenta(ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, ByVal valPuerto As String, ByVal valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String)
   Dim insImprFiscalBMC As clsUtilImpFiscalBMC
   Dim insImprFiscalQPrint As clsImpFiscalQPrint
   Dim insImprFiscalFamFactory As clsImpFiscalFamFactory
   Dim insImpFiscalEleposVMAX As clsImpFiscalEleposVMAX
   Dim insImpFiscalFromAOS As clsImpFiscalFromAOS
   On Error GoTo h_Error
   MaxLenEpson = 16
   If insDatosImprFiscal.GetUsarModoDotNet Then
   Set insImpFiscalFromAOS = New clsImpFiscalFromAOS
      insImpFiscalFromAOS.sImprimeVentaArticulo valImpresoraFiscal, insDatosImprFiscal, mReady
   Set insImpFiscalFromAOS = Nothing
   Else
    Select Case valImpresoraFiscal
       Case eIF_EPSON_PF_220:
             sImprimeVentaArticulo_Epson valPuerto
       Case eIF_EPSON_PF_220II:
             sImprimeVentaArticulo_Epson valPuerto
       Case eIF_EPSON_PF_300:
             MaxLenEpson = 37
             sImprimeVentaArticulo_Epson valPuerto
       Case eIF_EPSON_TM_675_PF:              sImprimeVentaArticulo_Epson valPuerto
       Case eIF_EPSON_TM_950_PF:              sImprimeVentaArticulo_Epson valPuerto
       Case eIF_BEMATECH_MP_20_FI_II, eIF_BEMATECH_MP_2100_FI, eIF_BEMATECH_MP_4000_FI: sImprimeVentaArticulo_BEMATECH
       
       Case eIF_BMC_CAMEL, eIF_BMC_SPARK_614, eIF_BMC_TH34_EJ
          Set insImprFiscalBMC = New clsUtilImpFiscalBMC
          insImprFiscalBMC.sImprimeVentaArticulo_BMC valImpresoraFiscal, insDatosImprFiscal, mReady, valPuerto
          Set insImprFiscalBMC = Nothing
       Case eIF_QPRINT_MF
          Set insImprFiscalQPrint = New clsImpFiscalQPrint
          insImprFiscalQPrint.sImprimeVentaArticulo_QPrintMF valImpresoraFiscal, insDatosImprFiscal, mReady, valPuerto, valTipoConexion, valIp, valCajaNumero
          Set insImprFiscalQPrint = Nothing
       Case eIF_DASCOM_TALLY_1125, eIF_SAMSUNG_BIXOLON_SRP_270, eIF_OKI_ML_1120, eIF_SAMSUNG_BIXOLON_SRP_350, eIF_ACLAS_PP1F3, eIF_DASCOM_TALLY_DT_230, eIF_SAMSUNG_BIXOLON_SRP_280, eIF_ACLAS_PP9, eIF_SAMSUNG_BIXOLON_SRP_812, eif_HKA80, eif_HKA112:
          Set insImprFiscalFamFactory = New clsImpFiscalFamFactory
          insImprFiscalFamFactory.sImprimeVentaArticulo valImpresoraFiscal, insDatosImprFiscal, mReady
          Set insImprFiscalFamFactory = Nothing
       Case eIF_ELEPOS_VMAX_220_F, eIF_ELEPOS_VMAX_300, eIF_ELEPOS_VMAX_580
         Set insImpFiscalEleposVMAX = New clsImpFiscalEleposVMAX
         insImpFiscalEleposVMAX.InitializeValues obVMAX
         insImpFiscalEleposVMAX.sImprimeVentaArticulo valImpresoraFiscal, insDatosImprFiscal, False, mReady
         Set insImpFiscalEleposVMAX = Nothing
    End Select
      If mReady Then
         sCerrarVenta valImpresoraFiscal, valPuerto
      End If
    End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sEfectuaVenta", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEfectuaNotaDeCredito(ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, ByVal valPuerto As String, ByVal valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String)
   Dim insImpFiscalBMC As clsUtilImpFiscalBMC
   Dim insImpFiscalQPrintMF As clsImpFiscalQPrint
   Dim insImprFiscalFactory As clsImpFiscalFamFactory
   Dim insImpFiscalEleposVMAX As clsImpFiscalEleposVMAX
   Dim insImpFiscalFromAOS As clsImpFiscalFromAOS
   On Error GoTo h_Error
   MaxLenEpson = 16
   
   If insDatosImprFiscal.GetUsarModoDotNet Then
      
      Set insImpFiscalFromAOS = New clsImpFiscalFromAOS
         insImpFiscalFromAOS.sImprimeNotaDeCredito valImpresoraFiscal, valPuerto, insDatosImprFiscal, mReady
      Set insImpFiscalFromAOS = Nothing
   Else
      Select Case valImpresoraFiscal
         Case eIF_EPSON_PF_220:                 sImprimeNotaDeCredito_EPSON valPuerto
         Case eIF_EPSON_PF_220II
            MaxLenEpson = 37
            sImprimeNotaDeCredito_EPSON valPuerto
         Case eIF_EPSON_PF_300:
            MaxLenEpson = 37
            sImprimeNotaDeCredito_EPSON valPuerto
         Case eIF_EPSON_TM_675_PF:              sImprimeNotaDeCredito_EPSON valPuerto
         Case eIF_EPSON_TM_950_PF:              sImprimeNotaDeCredito_EPSON valPuerto
         Case eIF_BEMATECH_MP_20_FI_II, Enum_ImpresorasFiscales.eIF_BEMATECH_MP_2100_FI:          sImprimeNotaDeCredito_BEMATECH
   
         Case eIF_BMC_CAMEL, eIF_BMC_SPARK_614, eIF_BMC_TH34_EJ
            Set insImpFiscalBMC = New clsUtilImpFiscalBMC
            insImpFiscalBMC.sImprimeVentaArticulo_BMC valImpresoraFiscal, insDatosImprFiscal, mReady, valPuerto
            Set insImpFiscalBMC = Nothing
         Case eIF_QPRINT_MF
            Set insImpFiscalQPrintMF = New clsImpFiscalQPrint
            insImpFiscalQPrintMF.sAbreCF_QPrintMF valImpresoraFiscal, insDatosImprFiscal, mReady, valPuerto, valTipoConexion, valIp, valCajaNumero
            If mReady Then
               insImpFiscalQPrintMF.sImprimeVentaArticulo_QPrintMF valImpresoraFiscal, insDatosImprFiscal, mReady, valPuerto, valTipoConexion, valIp, valCajaNumero
            End If
            Set insImpFiscalQPrintMF = Nothing
         Case eIF_DASCOM_TALLY_1125, eIF_SAMSUNG_BIXOLON_SRP_270, eIF_OKI_ML_1120, eIF_SAMSUNG_BIXOLON_SRP_350, eIF_ACLAS_PP1F3
            Set insImprFiscalFactory = New clsImpFiscalFamFactory
            insImprFiscalFactory.sImprimeNotaDeCredito valImpresoraFiscal, valPuerto, insDatosImprFiscal, mReady
            Set insImprFiscalFactory = Nothing
         Case eIF_DASCOM_TALLY_DT_230, eIF_SAMSUNG_BIXOLON_SRP_280, eIF_ACLAS_PP9, eIF_SAMSUNG_BIXOLON_SRP_812, eif_HKA80
            Set insImprFiscalFactory = New clsImpFiscalFamFactory
            insImprFiscalFactory.sImprimeNotaDeCreditoDascomT valImpresoraFiscal, valPuerto, insDatosImprFiscal, mReady
            Set insImprFiscalFactory = Nothing
         Case eif_HKA112
            Set insImprFiscalFactory = New clsImpFiscalFamFactory
            insImprFiscalFactory.sImprimeNotaDeCreditoHKA112 valImpresoraFiscal, valPuerto, insDatosImprFiscal, mReady
            Set insImprFiscalFactory = Nothing
         Case eIF_ELEPOS_VMAX_220_F, eIF_ELEPOS_VMAX_300, eIF_ELEPOS_VMAX_580
           Set insImpFiscalEleposVMAX = New clsImpFiscalEleposVMAX
           insImpFiscalEleposVMAX.InitializeValues obVMAX
           insImpFiscalEleposVMAX.sImprimeVentaArticulo valImpresoraFiscal, insDatosImprFiscal, True, mReady
           Set insImpFiscalEleposVMAX = Nothing
       End Select
      sCerrarVenta valImpresoraFiscal, valPuerto
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sEfectuaNotaDeCredito", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCerrarVenta(ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, valPuerto As String)
   Dim insImpFiscalFamFactory As clsImpFiscalFamFactory
   Dim insImpFiscalEleposVMAX  As clsImpFiscalEleposVMAX
   On Error GoTo h_Error
   Select Case valImpresoraFiscal
      Case eIF_EPSON_PF_220, eIF_EPSON_PF_220II:                  sCierraCF_EPSON (valImpresoraFiscal), valPuerto
      Case eIF_EPSON_PF_300:                 sCierraCF_EPSON (valImpresoraFiscal), valPuerto
      Case eIF_EPSON_TM_675_PF:              sCierraCF_EPSON (valImpresoraFiscal), valPuerto
      Case eIF_EPSON_TM_950_PF:              sCierraCF_EPSON (valImpresoraFiscal), valPuerto
      Case eIF_BEMATECH_MP_20_FI_II, eIF_BEMATECH_MP_2100_FI, eIF_BEMATECH_MP_4000_FI: sCierraCF_BEMATECH valImpresoraFiscal
      Case eIF_DASCOM_TALLY_1125, eIF_SAMSUNG_BIXOLON_SRP_350, eIF_ACLAS_PP1F3, eIF_OKI_ML_1120, eIF_SAMSUNG_BIXOLON_SRP_270, eIF_DASCOM_TALLY_DT_230, eIF_SAMSUNG_BIXOLON_SRP_280, eIF_ACLAS_PP9, eIF_SAMSUNG_BIXOLON_SRP_812, eif_HKA80, eif_HKA112:
         Set insImpFiscalFamFactory = New clsImpFiscalFamFactory
         insImpFiscalFamFactory.sCierraCF True
         Set insImpFiscalFamFactory = Nothing
     Case eIF_ELEPOS_VMAX_220_F, eIF_ELEPOS_VMAX_300, eIF_ELEPOS_VMAX_580
     Set insImpFiscalEleposVMAX = New clsImpFiscalEleposVMAX
      insImpFiscalEleposVMAX.InitializeValues obVMAX
      insImpFiscalEleposVMAX.sCierraCF
      mReady = True
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCerrarVenta", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fConvierteAlicuotaBEMATECH(ByVal valAlicuota As String) As String
   Dim AlicuotaConvertida As String
   Dim Posicion As Long
   On Error GoTo h_Error
   AlicuotaConvertida = valAlicuota
   Posicion = gTexto.DfInStr(AlicuotaConvertida, ".")
   If Posicion > 0 Then
      gTexto.ReemplazaCaracteresEnElString AlicuotaConvertida, ".", ","
   End If
   If gConvert.fConvertStringToCurrency(valAlicuota) < 10 Then
      AlicuotaConvertida = "0" & AlicuotaConvertida
   End If
   If gTexto.DfLen(AlicuotaConvertida) < 3 Then
      AlicuotaConvertida = AlicuotaConvertida & "0"
   End If
   If gTexto.DfLen(AlicuotaConvertida) < 4 Then
      AlicuotaConvertida = AlicuotaConvertida & "0"
   End If
h_EXIT: On Error GoTo 0
   fConvierteAlicuotaBEMATECH = AlicuotaConvertida
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConvierteAlicuotaBEMATECH", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConvierteCantidadBEMATECH(ByVal valCantidad As String, ByVal valEsParaDescuento As Boolean) As String
   Dim CantidadConvertida As String
   Dim Posicion As String
   Dim Fraccion As String
   On Error GoTo h_Error
   CantidadConvertida = gTexto.DfReplace(valCantidad, "-", "")
   Posicion = gTexto.DfInStr(CantidadConvertida, ".")
   If Posicion = 0 Then Posicion = gTexto.DfInStr(CantidadConvertida, ",")
   gTexto.ReemplazaCaracteresEnElString CantidadConvertida, ".", ","
   If Posicion > 0 Then
      Fraccion = gTexto.DfMid(CantidadConvertida, Posicion, gTexto.DfLen(CantidadConvertida) - Posicion)
      If gTexto.DfLen(Fraccion) = 1 And (Not valEsParaDescuento) Then
         CantidadConvertida = CantidadConvertida & "00"
      ElseIf gTexto.DfLen(Fraccion) = 2 Then
         CantidadConvertida = CantidadConvertida & "0"
      ElseIf gTexto.DfLen(Fraccion) = 4 Then
         CantidadConvertida = gTexto.DfLeft(CantidadConvertida, gTexto.DfLen(CantidadConvertida) - 1)
      End If
   ElseIf Not valEsParaDescuento Then
      CantidadConvertida = CantidadConvertida & ",000"
   ElseIf valEsParaDescuento Then
      CantidadConvertida = CantidadConvertida & ",00"
   End If
h_EXIT: On Error GoTo 0
   fConvierteCantidadBEMATECH = CantidadConvertida
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConvierteCantidadBEMATECH", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCadenaValidaEpson(ByVal valTexto As String, Optional ByVal valVieneDeDireccion As Boolean = False)
   Dim CadenaValida As String
   Dim Index As Long
   Dim caracter As String
   On Error GoTo h_Error
   If valVieneDeDireccion Then
      CadenaValida = valTexto
   Else
      CadenaValida = gTexto.DfLeft(valTexto, MaxLenEpson)
   End If
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "á", "a"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "é", "e"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "í", "i"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ó", "o"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ú", "u"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Á", "A"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "É", "E"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Í", "I"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ó", "O"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ú", "U"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ñ", "n"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ñ", "N"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "'", "´"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "@", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "#", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "%", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ª", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "º", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "·", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "¨", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ç", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ç", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "&", ""
h_EXIT: On Error GoTo 0
   fCadenaValidaEpson = CadenaValida
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCadenaValidaEpson", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sVerRespuesta(ByVal FileName As String)
    Dim NLineas As Integer
    Dim i As Integer
    Set gWorksPath = New clsUtilWorkPaths
    mSerial = ""
    If (mbRet = True) Then
        NLineas = LeerArchivo(FileName)
'        sAux = ""
        For i = 0 To NLineas - 1
         mDatosFiscales = mDatosFiscales & sLineas(i) + vbCrLf
       Next i
    Else
       mSerial = "Sin respuesta"
    End If
   Set gWorksPath = Nothing
End Sub

Public Function LeerArchivo(sNombreArchivo As String) As Integer
   Dim NumeroArchivo As String
   Dim i As Integer
   NumeroArchivo = FreeFile   ' Obtiene un número de archivo que no se ha utilizado.
   Open sNombreArchivo For Input As #NumeroArchivo
   i = 0
   While Not EOF(NumeroArchivo) And i < MAX_LINEAS
      Line Input #NumeroArchivo, sLineas(i)
      i = i + 1
   Wend
   LeerArchivo = i
   Close #NumeroArchivo   ' Cierra el archivo.
End Function

Private Sub sImprimeDireccionEpson(valDireccion As String, valPuerto As String)
   Dim textoOriginal As String
   Dim Texto As String
   Dim MaxLen As Integer
   Dim i As Integer
   Dim Cont As Integer
   Dim Salida As String
   On Error GoTo h_Error
   If Len(valDireccion) > 0 Then
      i = 1
      Cont = 1
      textoOriginal = "DIRECCION: " & valDireccion
      textoOriginal = fCadenaValidaEpson(textoOriginal, True)
      MaxLen = gTexto.DfLen(textoOriginal)
      While i <= MaxLen
         Texto = gTexto.DfMid(textoOriginal, i, 38)
         i = i + 38
         If Cont <= 3 Then
            Salida = Chr$(65) & Chr$(FIELD) & Texto & Chr(FIELD) & Chr(127)
            sEnviar Salida, "", valPuerto
            Cont = Cont + 1
         End If
      Wend
      'Salida = Chr$(80) & Chr$(FIELD) & Chr$(1) 'DEJA UNA LINEA EN BLANCO
      'sEnviar Salida
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeDireccionEpson", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCancelaCuponFiscal(ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, ByRef Ready As Boolean, valPuerto As String)
   Dim NumeroCupon As String
   Dim insImpFicalBmc As clsUtilImpFiscalBMC
   Dim insImpFiscalFamFactory As clsImpFiscalFamFactory
   Dim insImpFiscalEleposVMAX As clsImpFiscalEleposVMAX
   Dim insImpFiscalFromAOS As clsImpFiscalFromAOS
   On Error GoTo h_Error
   
   If insDatosImprFiscal.GetUsarModoDotNet Then
   Set insImpFiscalFromAOS = New clsImpFiscalFromAOS
       insImpFiscalFromAOS.fCancelaCuponFiscal valImpresoraFiscal, valPuerto
   Set insImpFiscalFromAOS = Nothing
   Else
    Select Case valImpresoraFiscal
       Case eIF_BEMATECH_MP_20_FI_II, eIF_BEMATECH_MP_2100_FI, eIF_BEMATECH_MP_4000_FI: fCancelaCuponFiscal_BEMATECH
       Case eIF_EPSON_PF_300:                 fCancelaCuponFiscal_EPSON valPuerto
       Case eIF_EPSON_PF_220:                 fCancelaCuponFiscal_EPSON valPuerto
       Case eIF_EPSON_PF_220II:               fCancelaCuponFiscal_EPSON valPuerto
       Case eIF_EPSON_TM_675_PF:              fCancelaCuponFiscal_EPSON valPuerto
       Case eIF_EPSON_TM_950_PF:              fCancelaCuponFiscal_EPSON valPuerto
       
       Case eIF_BMC_CAMEL, eIF_BMC_SPARK_614, eIF_BMC_TH34_EJ
          Set insImpFicalBmc = New clsUtilImpFiscalBMC
          insImpFicalBmc.fCancelaCuponFiscal_BMC True, valPuerto
          Set insImpFicalBmc = Nothing
       Case eIF_DASCOM_TALLY_1125, eIF_ACLAS_PP1F3, eIF_SAMSUNG_BIXOLON_SRP_350, eIF_OKI_ML_1120, eIF_SAMSUNG_BIXOLON_SRP_270, eIF_DASCOM_TALLY_DT_230, eIF_SAMSUNG_BIXOLON_SRP_280, eIF_ACLAS_PP9, eIF_SAMSUNG_BIXOLON_SRP_812, eif_HKA80, eif_HKA112:
          Set insImpFiscalFamFactory = New clsImpFiscalFamFactory
          insImpFiscalFamFactory.fCancelaCuponFiscal True, valPuerto, valImpresoraFiscal
          Set insImpFiscalFamFactory = Nothing
       Case eIF_ELEPOS_VMAX_220_F, eIF_ELEPOS_VMAX_300, eIF_ELEPOS_VMAX_580
         Set insImpFiscalEleposVMAX = New clsImpFiscalEleposVMAX
         insImpFiscalEleposVMAX.InitializeValues obVMAX
         insImpFiscalEleposVMAX.fCancelaCuponFiscal True, valPuerto
         Set insImpFiscalEleposVMAX = Nothing
    End Select
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCancelaCuponFiscal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sVerificaVigenciaDeLaAlicuota(ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, ByRef Ready As Boolean, ByVal gAdmAlicuotaIvaActual As Object, valPuerto As String)
   On Error GoTo h_Error
   mReady = True
   If insDatosImprFiscal.GetUsarModoDotNet Then
      Ready = True
   Else
      Select Case valImpresoraFiscal
         Case eIF_BEMATECH_MP_20_FI_II, eIF_BEMATECH_MP_2100_FI, eIF_BEMATECH_MP_4000_FI: fValidaAlicuotaIVA_BEMATECH gAdmAlicuotaIvaActual
         Case eIF_EPSON_PF_300:                 fCancelaCuponFiscal_EPSON valPuerto
         Case eIF_EPSON_PF_220:                 fCancelaCuponFiscal_EPSON valPuerto
         Case eIF_EPSON_PF_220II:               fCancelaCuponFiscal_EPSON valPuerto
         Case eIF_EPSON_TM_675_PF:              fCancelaCuponFiscal_EPSON valPuerto
         Case eIF_EPSON_TM_950_PF:              fCancelaCuponFiscal_EPSON valPuerto
         Case eIF_SAMSUNG_BIXOLON_SRP_270:      'fValidaAlicuotaIVA
         Case eIF_OKI_ML_1120:                  'fValidaAlicuotaIVA
         Case eIF_SAMSUNG_BIXOLON_SRP_350:
         Case eIF_ACLAS_PP1F3, eIF_DASCOM_TALLY_1125, eIF_DASCOM_TALLY_DT_230:
         Case eIF_QPRINT_MF:
         Case eIF_SAMSUNG_BIXOLON_SRP_280:
         Case eIF_ACLAS_PP9:
         Case eIF_SAMSUNG_BIXOLON_SRP_812:
         Case eif_HKA80:
         Case eif_HKA112:
      End Select
   End If
   Ready = mReady
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sVerificaVigenciaDeLaAlicuota", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fCancelaCuponFiscal_BEMATECH() As String
   Dim Retorno As Integer
   On Error GoTo h_Error
   Retorno = Bematech_FI_AnulaCupon
   insImprFiscal.fVerificaRetornoImpresora "", Retorno, mReady
h_EXIT: On Error GoTo 0
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fCancelaCuponFiscal_BEMATECH", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCancelaCuponFiscal_EPSON(valPuerto As String) As String
   Dim Salida As String
   On Error GoTo h_Error
   Salida = Chr$(68) & Chr$(FIELD) & Chr(127)
   Salida = Salida & Chr(FIELD) & "1" & Chr(FIELD)
   Salida = Salida & "C" & Chr(FIELD) & Chr(127)
   sEnviar Salida, "", valPuerto
h_EXIT: On Error GoTo 0
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fCancelaCuponFiscal_EPSON", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fObtenerVersion_Epson(valPuerto As String) As String
   Dim vRespuesta As String
   Dim vSalida As String
   Dim vCampo As String
   On Error GoTo h_Error
   vRespuesta = ""
   vSalida = Chr$(56) & Chr$(FIELD) & "V" ' Comando 38h
   sEnviar vSalida, vRespuesta, valPuerto ' Envia Comando a Printer y Obtiene respues en vRespuesta
   'Busca campo 04 para ver respuesta
   vCampo = vRespuesta
   gMessage.Advertencia vRespuesta
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   sEnviar Chr$(69), "", valPuerto  'Cierre total
   
h_EXIT: On Error GoTo 0
   fObtenerVersion_Epson = vCampo
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fValidaAlicuotaIVA_BEMATECH(ByVal gAdmAlicuotaIvaActual As Object) As String
   Dim Texto As String
   Dim textoOriginal As String
   Dim i As Integer
   Dim MaxLen As Integer
   Dim Retorno As Integer
   Dim Alicuota As String
   On Error GoTo h_Error
   textoOriginal = Space(79)
   Retorno = Bematech_FI_RetornoAlicuotas(textoOriginal)
   i = 1
   mReady = False
   MaxLen = gTexto.DfLen(textoOriginal)
   While i <= MaxLen
      Texto = gTexto.DfMid(textoOriginal, i, 4)
      i = i + 5
      Alicuota = gAdmAlicuotaIvaActual.GetAlicuotaIVA(insDatosImprFiscal.GetFecha, eTD_ALICUOTAGENERAL)
      If Alicuota < 10 Then
         Alicuota = 0 & Alicuota
      End If
      If Alicuota & "00" = Texto Then
         mReady = True
      End If
   Wend
h_EXIT: On Error GoTo 0
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fValidaAlicuotaIVA_BEMATECH", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sImprimeObservacionesEpson(valObservacion As String, valPuerto As String)
   Dim textoOriginal As String
   Dim Texto As String
   Dim MaxLen As Integer
   Dim i As Integer
   Dim Cont As Integer
   Dim Salida As String
   On Error GoTo h_Error
   If Len(valObservacion) > 0 Then
      i = 1
      Cont = 1
      MaxLenEpson = 38
      textoOriginal = "OBSERVACION: " & valObservacion
      textoOriginal = fCadenaValidaEpson(textoOriginal, True)
      MaxLen = gTexto.DfLen(textoOriginal)
      While i <= MaxLen
         Texto = gTexto.DfMid(textoOriginal, i, 38)
         i = i + MaxLenEpson
         If Cont <= 3 Then
            Salida = Chr$(65) & Chr$(FIELD) & Texto & Chr(FIELD) & Chr(127)
            sEnviar Salida, "", valPuerto
            Cont = Cont + 1
         End If
      Wend
      'Salida = Chr$(80) & Chr$(FIELD) & Chr$(1) ' DEJA UNA LINEA EN BLANCO
      'sEnviar Salida
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeDireccionEpson", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fFormaDeCobro(FormaDeCobro As String) As String
    Dim Resultado As String
      Select Case FormaDeCobro
        Case "00001":    Resultado = "01" 'Format(1, FORMATO_ENTERO_2)
        Case "00002":    Resultado = "05" 'Format(5, FORMATO_ENTERO_2)
        Case "00003":    Resultado = "09" 'Format(9, FORMATO_ENTERO_2)
        Case Else: Resultado = "01"
      End Select
    On Error GoTo h_Error
   fFormaDeCobro = Resultado
h_EXIT: On Error GoTo 0
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFormaDeCobro", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sImprimeCamposDefiniblesEpson(ByVal valRSCamposDefinibles As ADODB.Recordset, ByVal valMax As Integer, valPuerto As String)
   Dim vText As String
   Dim vOutPut As String
   On Error GoTo h_Error
      valRSCamposDefinibles.MoveFirst
      Do While Not valRSCamposDefinibles.EOF And valRSCamposDefinibles.AbsolutePosition <= valMax
         vText = fCadenaValidaEpson(gTexto.DfMid(valRSCamposDefinibles.Fields("CampoDefinible").Value, 1, 40), True)
         vOutPut = Chr$(65) & Chr$(FIELD) & vText & Chr(FIELD) & Chr(127)
         sEnviar vOutPut, "", valPuerto
         valRSCamposDefinibles.MoveNext
      Loop
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeCamposDefiniblesEpson", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImprimeDatosVehiculosEpson(ByVal valRSDatosVehiculos As ADODB.Recordset, valPuerto As String)
   Dim vText As String
   Dim vOutPut As String
   On Error GoTo h_Error
      valRSDatosVehiculos.MoveFirst
      Do While Not valRSDatosVehiculos.EOF
         vText = fCadenaValidaEpson(gTexto.DfMid(valRSDatosVehiculos.Fields("DatosVehiculo").Value, 1, 40), True)
         vOutPut = Chr$(65) & Chr$(FIELD) & vText & Chr(FIELD) & Chr(127)
         sEnviar vOutPut, "", valPuerto
         valRSDatosVehiculos.MoveNext
      Loop
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeDireccionEpson", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub


Public Function fExtraItem(ByVal valTextoNoFiscal As String, valEfecto As Integer, valPuerto As String) As Boolean
'Esta rutina permite ingresar lineas adicionales a la descripcion de un producto
 Dim Salida As String
If Len(valTextoNoFiscal) > 40 Then
    fExtraItem = False
    Exit Function
End If

If ((Len(valTextoNoFiscal) > 20) And ((valEfecto = 1) Or (valEfecto = 3))) Then
    fExtraItem = False
    Exit Function
End If
    
 Select Case valEfecto
 'resaltado
 Case Is = 1
    valTextoNoFiscal = Chr$(241) & valTextoNoFiscal
 'rojo
 Case Is = 2
   valTextoNoFiscal = Chr$(248) & valTextoNoFiscal
 'resaltado + rojo
 Case Is = 3
   valTextoNoFiscal = Chr$(249) & valTextoNoFiscal
 End Select

 Salida = Chr$(65) & Chr$(FIELD)
 Salida = Salida & valTextoNoFiscal
 'Envio = Salida
 Call sEnviar(Salida, "", valPuerto)
 fExtraItem = mReady
 'Call Campos
 'Call DefEstados
 'Resp = Entrada
 
'ExtraItem = True
'If (EstadoImpresora_15 = True) Or (EstadoFiscal_15 = True) Then
'    ExtraItem = False
'End If
End Function

Private Sub sVerStatusImpresoraFiscal(ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, valPuerto As String)
   Dim vMensajeError As String
   On Error GoTo h_Error
   If insDatosImprFiscal.GetUsarModoDotNet Then
      mReady = True
   Else
   Select Case valImpresoraFiscal
      Case Enum_ImpresorasFiscales.eIF_EPSON_PF_220, Enum_ImpresorasFiscales.eIF_EPSON_PF_220II, Enum_ImpresorasFiscales.eIF_EPSON_PF_300 _
      , Enum_ImpresorasFiscales.eIF_EPSON_TM_675_PF, Enum_ImpresorasFiscales.eIF_EPSON_TM_950_PF
         vMensajeError = fMensajeErrorEPSONSiExiste(fStatusEPSON(valPuerto))
         If gTexto.DfLen(vMensajeError) > 0 Then
            gMessage.sCriticalErrorMessage vMensajeError, "Error en Impresora Fiscal"
            mReady = False
         End If
      Case Else
         mReady = True
      End Select
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sVerStatusImpresoraFiscal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fMensajeErrorEPSONSiExiste(ByVal vStatus As String) As String
   Dim vMessege As String
   On Error GoTo h_Error
   vMessege = ""
   Select Case vStatus
      Case "00"
      Case "04"
         vMessege = "Se ha detectado que tiene más de un día sin hacer Cierre Z, Por favor vaya a la opción Resumen Diario>Realizar Cierre Z para continuar con el proceso."
      Case "08"
         vMessege = "Se ha detectado que la impresora esta bloqueda, Por favor vaya a la opción Resumen Diario>Realizar Cierre Z para continuar con el proceso."
      Case "10"
         vMessege = "Se ha detectado un Error Critico en la impresora. Error de Tipo BCC RAM. Debe contactar a su proveedor para que Verifique el funcionamiento de la misma."
      Case "11"
         vMessege = "Se ha detectado un Error Critico en la impresora. Error de Tipo BCC ROM. Debe contactar a su proveedor para que Verifique el funcionamiento de la misma."
      Case "12"
         vMessege = "Se ha detectado un Error Critico en la impresora. Error de Formato FECHA en RAM. Debe contactar a su proveedor para que Verifique el funcionamiento de la misma."
      Case "13"
         vMessege = "Se ha detectado un Error Critico en la impresora. Error de formato de datos al realizar un Z. Debe contactar a su proveedor para que Verifique el funcionamiento de la misma."
      Case "14"
         vMessege = "Se ha detectado un Error Critico en la impresora. Limite de memoria fiscal. Debe contactar a su proveedor para que Verifique el funcionamiento de la misma."
   End Select
h_EXIT:    On Error GoTo 0
   fMensajeErrorEPSONSiExiste = vMessege
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fMensajeErrorEPSONSiExiste", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fPosicionDeCaracterInv(ByVal valCadena As String, ByVal valCaracter As String) As Long
   Dim vPos As Long
   Dim i As Long
   Dim vCount As Long
   On Error GoTo h_Error
   vPos = 0
   vCount = 0
   If gTexto.DfLen(valCadena) > 0 Then
      For i = gTexto.DfLen(valCadena) To 0 Step -1
         If gTexto.DfMid(valCadena, i, 1) = valCaracter Then
            vPos = vCount
            Exit For
         End If
         vCount = vCount + 1
      Next i
   End If
h_EXIT:    On Error GoTo 0
   fPosicionDeCaracterInv = vPos
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fPosicionDeCaracterInv", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConvierteCantidadEpson(ByVal valCantidad As String, ByVal valEsParaPrecio As Boolean) As String
   Dim CantidadConvertida As String
   Dim Posicion As String
   Dim Fraccion As String
   On Error GoTo h_Error
   CantidadConvertida = valCantidad
   If (gTexto.DfInStr(CantidadConvertida, ",") > 0) Then
      gTexto.ReemplazaCaracteresEnElString CantidadConvertida, ",", "."
   End If
   Posicion = gTexto.DfInStr(CantidadConvertida, ".")
   If Posicion > 0 Then
      Fraccion = gTexto.DfMid(CantidadConvertida, Posicion + 1, gTexto.DfLen(CantidadConvertida) - Posicion)
      If gTexto.DfLen(Fraccion) = 1 And (Not valEsParaPrecio) Then
         CantidadConvertida = CantidadConvertida & "00"
      ElseIf (gTexto.DfLen(Fraccion) = 1 Or (Not valEsParaPrecio)) And gTexto.DfLen(Fraccion) < 3 Then
         CantidadConvertida = CantidadConvertida & "0"
      ElseIf gTexto.DfLen(Fraccion) = 3 And valEsParaPrecio Then
         CantidadConvertida = gTexto.DfMid(CantidadConvertida, 1, gTexto.DfLen(CantidadConvertida) - 1)
      ElseIf gTexto.DfLen(Fraccion) = 4 And valEsParaPrecio Then
         CantidadConvertida = gTexto.DfMid(CantidadConvertida, 1, gTexto.DfLen(CantidadConvertida) - 2)
      End If
      gTexto.ReemplazaCaracteresEnElString CantidadConvertida, ".", ","
   ElseIf Posicion <= 0 And valEsParaPrecio Then
      CantidadConvertida = CantidadConvertida & ",00"
   Else
      CantidadConvertida = CantidadConvertida & ",000"
   End If
   
h_EXIT: On Error GoTo 0
   fConvierteCantidadEpson = CantidadConvertida
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConvierteCantidadEpson", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sActivaRedondeo(ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, ByRef refReady As Boolean)
   Dim vMensajeError As String
   On Error GoTo h_Error
   Select Case valImpresoraFiscal
   Case Enum_ImpresorasFiscales.eIF_BEMATECH_MP_20_FI_II, Enum_ImpresorasFiscales.eIF_BEMATECH_MP_2100_FI, Enum_ImpresorasFiscales.eIF_BEMATECH_MP_4000_FI
      sActivaRedondeoBematech (refReady)
      If gTexto.DfLen(vMensajeError) > 0 Then
         gMessage.sCriticalErrorMessage vMensajeError, "Error en Impresora Fiscal"
         mReady = False
      End If
   Case Else
      mReady = True
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivaRedondeo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sActivaRedondeoBematech(ByRef refReady As Boolean)
   Dim vRetorno As Long
   On Error GoTo h_Error
   vRetorno = Bematech_FI_ProgramaRedondeo()
   insImprFiscal.fVerificaRetornoImpresora "", vRetorno, refReady
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivaRedondeoBematech", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fConvierteCantidadBEMATECHParaPagos(ByVal valCantidad As String) As String
   Dim CantidadConvertida As String
   Dim Posicion As String
   Dim Fraccion As String
   On Error GoTo h_Error
   CantidadConvertida = valCantidad
   Posicion = gTexto.DfInStr(CantidadConvertida, ".")
   If Posicion = 0 Then Posicion = gTexto.DfInStr(CantidadConvertida, ",")
   gTexto.ReemplazaCaracteresEnElString CantidadConvertida, ".", ","
   If Posicion > 0 Then
      Fraccion = gTexto.DfMid(CantidadConvertida, Posicion + 1, gTexto.DfLen(CantidadConvertida) - Posicion)
      CantidadConvertida = gTexto.DfMid(CantidadConvertida, 1, Posicion - 1)
      If CantidadConvertida = 0 Then
         CantidadConvertida = Fraccion
      Else
         CantidadConvertida = CantidadConvertida & "," & gTexto.LlenaConCaracterALaDerecha(Fraccion, "0", 2)
      End If
   Else
      CantidadConvertida = CantidadConvertida & "," & "00"
   End If
h_EXIT: On Error GoTo 0
   fConvierteCantidadBEMATECHParaPagos = CantidadConvertida
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConvierteCantidadBEMATECHParaPagos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConvierteCantidadBEMATECHParaDescuentos(ByVal valCantidad As String) As String
   Dim CantidadConvertida As String
   Dim Posicion As String
   Dim Fraccion As String
   Dim Entero As String
   On Error GoTo h_Error
   CantidadConvertida = valCantidad
   Posicion = gTexto.DfInStr(CantidadConvertida, ".")
   If Posicion = 0 Then Posicion = gTexto.DfInStr(CantidadConvertida, ",")
   gTexto.ReemplazaCaracteresEnElString CantidadConvertida, ".", ","
   If Posicion > 0 Then
      Fraccion = gTexto.DfMid(CantidadConvertida, Posicion + 1, gTexto.DfLen(CantidadConvertida) - Posicion)
      Fraccion = gTexto.DfLeft(Fraccion, 2)
      Entero = gTexto.DfMid(CantidadConvertida, 1, Posicion - 1)
      CantidadConvertida = ""
      If gTexto.DfLen(Entero) = 1 Then
         CantidadConvertida = "0" & Entero & "," & Fraccion
      Else
         CantidadConvertida = Entero & "," & Fraccion
      End If
   Else
      If gTexto.DfLen(CantidadConvertida) = 1 Then
         CantidadConvertida = "0" & CantidadConvertida & ",00"
      Else
         CantidadConvertida = CantidadConvertida & ",00"
      End If
   End If
h_EXIT: On Error GoTo 0
   fConvierteCantidadBEMATECHParaDescuentos = CantidadConvertida
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConvierteCantidadBEMATECHParaDescuentos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sImprimeCamposDefiniblesBEMATECH(ByVal valInsDatosImprFiscal As clsDatosImprFiscal, ByVal valSinObsevSinDireccion As Boolean, ByRef refCamposDefinibles As String)
   Dim vRsCamposDefinibles As ADODB.Recordset
   Dim vText As String
   Dim vOutPut As String
   Dim Cmd As String
   Dim CuentaLineas As Byte
   Dim TopLine As Byte
   On Error GoTo h_Error
   CuentaLineas = 0
   TopLine = 5
   If valSinObsevSinDireccion Then
        TopLine = 7
        refCamposDefinibles = ""
    Else
        refCamposDefinibles = vbCrLf & refCamposDefinibles
   End If
   If insDatosImprFiscal.GetImprimeCamposDefinibles Then
      Set vRsCamposDefinibles = valInsDatosImprFiscal.GetRSCamposDefinibles
      If Not (vRsCamposDefinibles Is Nothing) Then
         If gDbUtil.fRecordCount(vRsCamposDefinibles) > 0 Then
            vRsCamposDefinibles.MoveFirst
            Do While Not vRsCamposDefinibles.EOF
            If CuentaLineas > TopLine Then Exit Do
               vText = gTexto.DfMid(vRsCamposDefinibles.Fields("CampoDefinible").Value, 1, 48)
               If gTexto.DfLen(vText) > 0 Then
                  refCamposDefinibles = refCamposDefinibles & vText & vbCrLf
               End If
               vRsCamposDefinibles.MoveNext
            CuentaLineas = CuentaLineas + 1
            Loop
         End If
      End If
      vRsCamposDefinibles.Close
      Set vRsCamposDefinibles = Nothing
      refCamposDefinibles = gTexto.DfMid(refCamposDefinibles, 1, gTexto.DfLen(refCamposDefinibles) - 1)
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeCamposDefiniblesBEMATECH", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fObtenerUltimoNumeroDeReporteZ_Familia_Bematech() As String
   Dim vUltimoNumeroDeReporteZ As String
   Dim vRetorno As Integer
   On Error GoTo h_Error
   vUltimoNumeroDeReporteZ = Space(4)
   vRetorno = Bematech_FI_NumeroReducciones(vUltimoNumeroDeReporteZ)
h_EXIT:    On Error GoTo 0
   fObtenerUltimoNumeroDeReporteZ_Familia_Bematech = vUltimoNumeroDeReporteZ
   Exit Function
h_Error:
   fObtenerUltimoNumeroDeReporteZ_Familia_Bematech = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fObtenerUltimoNumeroDeReporteZ_Familia_Bematech", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fObtenerUltimoNumeroDeReporteZ_Familia_Epson(ByVal valPuerto As String) As String
   Dim vRespuesta As String
   Dim vSalida As String
   Dim vCampo As String
   Const ETX = 3
   Dim vIndice As Long
   On Error GoTo h_Error
   gMessage.InformationMessage "Impresión completada.", "Obtener Nro de Reporte Z"
   vRespuesta = ""
   vSalida = Chr$(56) & Chr$(FIELD) & "N" ' Comando 38h
   sEnviar vSalida, vRespuesta, valPuerto
   vCampo = vRespuesta
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vCampo = gTexto.fPrimerTokenYLoEliminaDelTexto(vRespuesta, Chr(FIELD))
   vIndice = InStr(vRespuesta, Chr$(ETX)) - 1
   vCampo = gTexto.DfMid(vRespuesta, 1, vIndice) 'Numero de reportes Z
h_EXIT: On Error GoTo 0
   fObtenerUltimoNumeroDeReporteZ_Familia_Epson = vCampo
   Exit Function
h_Error:
   fObtenerUltimoNumeroDeReporteZ_Familia_Epson = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fObtenerUltimoNumeroDeReporteZ_Familia_Epson", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sObtenerUltimoNumeroDeReporteZ(ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, ByVal valPuerto As String, ByVal valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String)
   Dim insImpFiscalBMC As clsUtilImpFiscalBMC
   Dim insImpFiscalQPrint As clsImpFiscalQPrint
   Dim insImpFiscalFamiliaFactory As clsImpFiscalFamFactory
   Dim vUltimoNumeroDeReporteZ As String
   Dim insImpFiscalEleposVMAX As clsImpFiscalEleposVMAX
   Dim insImpFiscalFromAOS As clsImpFiscalFromAOS
   
   On Error GoTo h_Error
   vUltimoNumeroDeReporteZ = ""
   
   If insDatosImprFiscal.GetUsarModoDotNet Then
   Set insImpFiscalFromAOS = New clsImpFiscalFromAOS
      vUltimoNumeroDeReporteZ = insImpFiscalFromAOS.fObtenerUltimoNumeroDeReporteZ(valPuerto, valImpresoraFiscal)
   Set insImpFiscalFromAOS = Nothing
   Else
    Select Case valImpresoraFiscal
        Case Enum_ImpresorasFiscales.eIF_BEMATECH_MP_20_FI_II, Enum_ImpresorasFiscales.eIF_BEMATECH_MP_2100_FI, Enum_ImpresorasFiscales.eIF_BEMATECH_MP_4000_FI:  vUltimoNumeroDeReporteZ = fObtenerUltimoNumeroDeReporteZ_Familia_Bematech
        Case Enum_ImpresorasFiscales.eIF_BMC_CAMEL, Enum_ImpresorasFiscales.eIF_BMC_SPARK_614, Enum_ImpresorasFiscales.eIF_BMC_TH34_EJ:
           Set insImpFiscalBMC = New clsUtilImpFiscalBMC
           vUltimoNumeroDeReporteZ = insImpFiscalBMC.fObtenerUltimoReporteZ_BMC(valPuerto)
           Set insImpFiscalBMC = Nothing
        Case Enum_ImpresorasFiscales.eIF_EPSON_PF_220, Enum_ImpresorasFiscales.eIF_EPSON_PF_220II, Enum_ImpresorasFiscales.eIF_EPSON_PF_300, Enum_ImpresorasFiscales.eIF_EPSON_TM_675_PF, Enum_ImpresorasFiscales.eIF_EPSON_TM_950_PF: vUltimoNumeroDeReporteZ = fObtenerUltimoNumeroDeReporteZ_Familia_Epson(valPuerto)
        Case Enum_ImpresorasFiscales.eIF_QPRINT_MF
           Set insImpFiscalQPrint = New clsImpFiscalQPrint
           vUltimoNumeroDeReporteZ = insImpFiscalQPrint.fObtenerUltimoReporteZ_QPrintMF(valPuerto, valTipoConexion, valIp, valCajaNumero)
           Set insImpFiscalQPrint = Nothing
           
        Case eIF_DASCOM_TALLY_1125, eIF_SAMSUNG_BIXOLON_SRP_270, eIF_SAMSUNG_BIXOLON_SRP_350, eIF_OKI_ML_1120, eIF_ACLAS_PP1F3, eIF_DASCOM_TALLY_DT_230, eIF_SAMSUNG_BIXOLON_SRP_280, eIF_ACLAS_PP9, eIF_SAMSUNG_BIXOLON_SRP_812, eif_HKA80, eif_HKA112:
           Set insImpFiscalFamiliaFactory = New clsImpFiscalFamFactory
           vUltimoNumeroDeReporteZ = insImpFiscalFamiliaFactory.fObtenerUltimoNumeroDeReporteZ(valPuerto, valImpresoraFiscal)
           Set insImpFiscalFamiliaFactory = Nothing
        Case eIF_ELEPOS_VMAX_220_F, eIF_ELEPOS_VMAX_300, eIF_ELEPOS_VMAX_580
          Set insImpFiscalEleposVMAX = New clsImpFiscalEleposVMAX
          insImpFiscalEleposVMAX.InitializeValues obVMAX
          vUltimoNumeroDeReporteZ = insImpFiscalEleposVMAX.fObtenerUltimoNumeroDeReporteZ(valPuerto)
          Set insImpFiscalEleposVMAX = Nothing
    End Select
   End If
   insDatosImprFiscal.SetUltimoReporteZ vUltimoNumeroDeReporteZ
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sObtenerUltimoNumeroDeReporteZ", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sAsignarConfiguracion(ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, valPuerto As String, ByVal valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String, ByVal valGateway As String, ByVal valMascaraSubRed As String)
   Dim insImpFiscalQPrint As clsImpFiscalQPrint
   On Error GoTo h_Error
   Select Case valImpresoraFiscal
      Case Enum_ImpresorasFiscales.eIF_QPRINT_MF
         Set insImpFiscalQPrint = New clsImpFiscalQPrint
         If valTipoConexion = eTDC_PUERTO_SERIAL Then
            insImpFiscalQPrint.sSetPuertoSerialDesdeUSBOrLAN valPuerto, insDatosImprFiscal.GetTipoConexion, mReady, valIp, valCajaNumero
         ElseIf valTipoConexion = eTDC_USB Then
            insImpFiscalQPrint.sSetPuertoUSBDesdeSerialOrLAN valPuerto, insDatosImprFiscal.GetTipoConexion, mReady, valIp, valCajaNumero
         ElseIf valTipoConexion = eTDC_LAN Then
            insImpFiscalQPrint.sSetLANDesdePuertoSerialOrUSB valPuerto, valTipoConexion, mReady, valIp, valCajaNumero, valGateway, valMascaraSubRed
         End If
         Set insImpFiscalQPrint = Nothing
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAsignarConfiguracion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sCortarPapel(ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, valPuerto As String, ByVal valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String)
   Dim insImpFiscalQPrint As clsImpFiscalQPrint
   On Error GoTo h_Error
   Select Case valImpresoraFiscal
      Case Enum_ImpresorasFiscales.eIF_QPRINT_MF
         Set insImpFiscalQPrint = New clsImpFiscalQPrint
         insImpFiscalQPrint.sCortarPapel valTipoConexion, valPuerto, valIp, valCajaNumero
         Set insImpFiscalQPrint = Nothing
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAsignarConfiguracion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCierreZ_FamiliaFactory(ByVal valPuerto As String, ByVal valImpresoraFiscal As Enum_ImpresorasFiscales)
   Dim insImpFiscalFamFactory As clsImpFiscalFamFactory
   On Error GoTo h_Error
   Set insImpFiscalFamFactory = New clsImpFiscalFamFactory
   insImpFiscalFamFactory.sCierreZ mReady, valPuerto, valImpresoraFiscal
   Set insImpFiscalFamFactory = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCierreZ_FamiliaFactory", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fValorDesdeArchivoINI(valRutaArchivoINI As String, valLlaveALeer As String, valValorPorDefectoSiNoExisteLlave As String) As String
   Dim vBuffer As String * 256
   Dim vPosicion As Long
   Dim vResult As String
   On Error GoTo h_Error
   vPosicion = GetPrivateProfileString("Sistema", valLlaveALeer, valValorPorDefectoSiNoExisteLlave, vBuffer, Len(vBuffer), valRutaArchivoINI)
   vResult = Left$(vBuffer, vPosicion)
h_EXIT: On Error GoTo 0
   fValorDesdeArchivoINI = vResult
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fValorDesdeArchivoINI", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
