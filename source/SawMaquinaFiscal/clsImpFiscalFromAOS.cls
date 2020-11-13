VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsImpFiscalFromAOS"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit
Dim insclsUtilImprFiscal As clsUtilImprFiscal

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsImpFiscalFromAOS"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Impresora Fiscal From AOS"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Private Function fWrpImpresoraFiscalName() As String
   fWrpImpresoraFiscalName = "Galac.Saw.Wrp.DispositivosExternos.wrpImpresoraFiscal"
End Function

Public Function fSerialMemoriaFiscal(ByRef refReady As Boolean, valPuerto As String, ByVal valImpresora As Enum_ImpresorasFiscales) As String
   Dim vSerial As String
   Dim vResultado As Boolean
   Dim objWrapper  As Object
   On Error GoTo h_Error
   Set insclsUtilImprFiscal = New clsUtilImprFiscal
   Set objWrapper = CreateObject(fWrpImpresoraFiscalName)
   objWrapper.InitializeComponent gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword, gWorkPaths.fAppPath & gDefProg.GetConfigFileName
   objWrapper.InitializeDefProg gDefProg.GetSiglasDelPrograma, gDefProg.GetVersionDelPrograma, gDefProg.GetVersionBaseDeDatos, gConvert.dateToString(gDefProg.GetFechaDeLaVersion), gConvert.dateToString(gDefProg.GetFechaDeLaVersion), "", "", gConvert.fConvierteAString(1), False
   vSerial = objWrapper.ObtenerSerialMaquinaFiscal(insclsUtilImprFiscal.fGenerarXmlConDatosDeLaMaquinaFiscal(valPuerto, valImpresora))
   If gTexto.DfLen(vSerial) < 1 Then
      refReady = False
      gMessage.Advertencia "Lo sentimos ha ocurrido un error al obtener el serial de la impresora"
   Else
      refReady = True
   End If
   Set objWrapper = Nothing
   Set insclsUtilImprFiscal = Nothing
h_EXIT: On Error GoTo 0   
   fSerialMemoriaFiscal = vSerial
   Exit Function
h_Error:
   fSerialMemoriaFiscal = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSerialMemoriaFiscal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fUltimoNumeroMemoriaFiscal(ByVal valPuerto As String, ByVal valImpresora As Enum_ImpresorasFiscales, ByVal valEsNotaDeCredito As Boolean, ByRef refComandoEjecutado) As String
   Dim vNumeroDocumento As String
   Dim vResultado As Boolean
   Dim objWrapper  As Object
   On Error GoTo h_Error
   Set insclsUtilImprFiscal = New clsUtilImprFiscal
   Set objWrapper = CreateObject(fWrpImpresoraFiscalName)
   objWrapper.InitializeComponent gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword, gWorkPaths.fAppPath & gDefProg.GetConfigFileName
   objWrapper.InitializeDefProg gDefProg.GetSiglasDelPrograma, gDefProg.GetVersionDelPrograma, gDefProg.GetVersionBaseDeDatos, gConvert.dateToString(gDefProg.GetFechaDeLaVersion), gConvert.dateToString(gDefProg.GetFechaDeLaVersion), "", "", gConvert.fConvierteAString(1), False
   If valEsNotaDeCredito Then
      vNumeroDocumento = objWrapper.ObtenerUltimoNumeroNotaDeCreditoImpresa(insclsUtilImprFiscal.fGenerarXmlConDatosDeLaMaquinaFiscal(valPuerto, valImpresora))
   Else
      vNumeroDocumento = objWrapper.ObtenerUltimoNumeroFacturaImpresa(insclsUtilImprFiscal.fGenerarXmlConDatosDeLaMaquinaFiscal(valPuerto, valImpresora))
   End If
   If gTexto.DfLen(vNumeroDocumento) < 1 Then
      refComandoEjecutado = False
      gMessage.Advertencia "Lo sentimos ha ocurrido un error al obtener el numero del documento fiscal"
   Else
      vNumeroDocumento = gTexto.llenaConCaracterALaIzquierda(vNumeroDocumento, "0", 8)
      refComandoEjecutado = True
   End If
   Set objWrapper = Nothing
   Set insclsUtilImprFiscal = Nothing
h_EXIT: On Error GoTo 0   
   fUltimoNumeroMemoriaFiscal = vNumeroDocumento
   Exit Function
h_Error:
   refComandoEjecutado = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fUltimoNumeroMemoriaFiscal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fObtenerUltimoNumeroDeReporteZ(ByVal valPuerto As String, ByVal valImpresora As Enum_ImpresorasFiscales) As String
   Dim vUltimoNumeroDeReporteZ As String
   Dim vResultado As Boolean
   Dim objWrapper  As Object
   On Error GoTo h_Error
   Set insclsUtilImprFiscal = New clsUtilImprFiscal
   Set objWrapper = CreateObject(fWrpImpresoraFiscalName)
   objWrapper.InitializeComponent gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword, gWorkPaths.fAppPath & gDefProg.GetConfigFileName
   objWrapper.InitializeDefProg gDefProg.GetSiglasDelPrograma, gDefProg.GetVersionDelPrograma, gDefProg.GetVersionBaseDeDatos, gConvert.dateToString(gDefProg.GetFechaDeLaVersion), gConvert.dateToString(gDefProg.GetFechaDeLaVersion), "", "", gConvert.fConvierteAString(1), False
   vUltimoNumeroDeReporteZ = objWrapper.ObtenerUltimoNumeroReporteZ(insclsUtilImprFiscal.fGenerarXmlConDatosDeLaMaquinaFiscal(valPuerto, valImpresora))
   Set objWrapper = Nothing
   Set insclsUtilImprFiscal = Nothing
h_EXIT:    On Error GoTo 0
    fObtenerUltimoNumeroDeReporteZ = vUltimoNumeroDeReporteZ
   Exit Function
h_Error:
   fObtenerUltimoNumeroDeReporteZ = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fObtenerUltimoNumeroDeReporteZ", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sCierreX(ByVal valPuerto As String, ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, ByRef refComandoProcesado As Boolean)
   Dim objWrapper  As Object
   On Error GoTo h_Error
   Set objWrapper = CreateObject(fWrpImpresoraFiscalName)
   Set insclsUtilImprFiscal = New clsUtilImprFiscal
   objWrapper.InitializeComponent gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword, gWorkPaths.fAppPath & gDefProg.GetConfigFileName
   objWrapper.InitializeDefProg gDefProg.GetSiglasDelPrograma, gDefProg.GetVersionDelPrograma, gDefProg.GetVersionBaseDeDatos, gConvert.dateToString(gDefProg.GetFechaDeLaVersion), gConvert.dateToString(gDefProg.GetFechaDeLaVersion), "", "", gConvert.fConvierteAString(1), False
   refComandoProcesado = objWrapper.RealizarCierreX(insclsUtilImprFiscal.fGenerarXmlConDatosDeLaMaquinaFiscal(valPuerto, valImpresoraFiscal))
   Set objWrapper = Nothing
   Set insclsUtilImprFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error:
   refComandoProcesado = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCierreX", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sCierreZ(ByRef refReady As Boolean, valPuerto As String, ByVal valImpresora As Enum_ImpresorasFiscales, ByRef refLasNumZ As String)
   Dim objWrapper  As Object
   On Error GoTo h_Error
   Set insclsUtilImprFiscal = New clsUtilImprFiscal
   Set objWrapper = CreateObject(fWrpImpresoraFiscalName)
   objWrapper.InitializeComponent gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword, gWorkPaths.fAppPath & gDefProg.GetConfigFileName
   objWrapper.InitializeDefProg gDefProg.GetSiglasDelPrograma, gDefProg.GetVersionDelPrograma, gDefProg.GetVersionBaseDeDatos, gConvert.dateToString(gDefProg.GetFechaDeLaVersion), gConvert.dateToString(gDefProg.GetFechaDeLaVersion), "", "", gConvert.fConvierteAString(1), False
   refReady = objWrapper.RealizarCierreZ(insclsUtilImprFiscal.fGenerarXmlConDatosDeLaMaquinaFiscal(valPuerto, valImpresora), refLasNumZ)
   Set objWrapper = Nothing
   Set insclsUtilImprFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error:
   refReady = False
Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCierreZ", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sImprimeVentaArticulo(ByVal valEnumPrinter As Enum_ImpresorasFiscales, ByVal valDatosImprFiscal As clsDatosImprFiscal, ByRef refReady As Boolean)
   Dim objWrapper  As Object
   Dim vNumDocumento As String
   On Error GoTo h_Error
   Set objWrapper = CreateObject(fWrpImpresoraFiscalName)
   Set insclsUtilImprFiscal = New clsUtilImprFiscal
   objWrapper.InitializeComponent gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword, gWorkPaths.fAppPath & gDefProg.GetConfigFileName
   objWrapper.InitializeDefProg gDefProg.GetSiglasDelPrograma, gDefProg.GetVersionDelPrograma, gDefProg.GetVersionBaseDeDatos, gConvert.dateToString(gDefProg.GetFechaDeLaVersion), gConvert.dateToString(gDefProg.GetFechaDeLaVersion), "", "", gConvert.fConvierteAString(1), False
   refReady = objWrapper.ImprimirVenta(insclsUtilImprFiscal.fGenerarXmlConDatosDeLaMaquinaFiscal(valDatosImprFiscal.GetPuertoImpresoraFiscal, valEnumPrinter), insclsUtilImprFiscal.fGenerarXmlConDatosDeLaVenta(valDatosImprFiscal), vNumDocumento)
   valDatosImprFiscal.SetUltimoNumeroFiscal vNumDocumento
   Set objWrapper = Nothing
   Set insclsUtilImprFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error:
   refReady = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeVentaArticulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sImprimeNotaDeCredito(ByVal valEnumPrinter As Enum_ImpresorasFiscales, ByVal valPuerto As String, insDatosImprFiscal As clsDatosImprFiscal, ByRef refReady As Boolean)
   Dim objWrapper  As Object
   Dim vNumDocumento As String
   On Error GoTo h_Error
   Set objWrapper = CreateObject(fWrpImpresoraFiscalName)
   Set insclsUtilImprFiscal = New clsUtilImprFiscal
   objWrapper.InitializeComponent gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword, gWorkPaths.fAppPath & gDefProg.GetConfigFileName
   objWrapper.InitializeDefProg gDefProg.GetSiglasDelPrograma, gDefProg.GetVersionDelPrograma, gDefProg.GetVersionBaseDeDatos, gConvert.dateToString(gDefProg.GetFechaDeLaVersion), gConvert.dateToString(gDefProg.GetFechaDeLaVersion), "", "", gConvert.fConvierteAString(1), False
   refReady = objWrapper.ImprimirNotaDeCredito(insclsUtilImprFiscal.fGenerarXmlConDatosDeLaMaquinaFiscal(valPuerto, valEnumPrinter), insclsUtilImprFiscal.fGenerarXmlConDatosDeLaVenta(insDatosImprFiscal), vNumDocumento)
   insDatosImprFiscal.SetUltimoNumeroFiscal vNumDocumento
   Set objWrapper = Nothing
   Set insclsUtilImprFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeNotaDeCredito", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fCancelaCuponFiscal(ByVal valEnumPrinter As Enum_ImpresorasFiscales, ByVal valPuerto As String) As Boolean
   Dim vResultado As Boolean
   Dim objWrapper  As Object
   On Error GoTo h_Error
   Set objWrapper = CreateObject(fWrpImpresoraFiscalName)
   Set insclsUtilImprFiscal = New clsUtilImprFiscal
   objWrapper.InitializeComponent gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword, gWorkPaths.fAppPath & gDefProg.GetConfigFileName
   objWrapper.InitializeDefProg gDefProg.GetSiglasDelPrograma, gDefProg.GetVersionDelPrograma, gDefProg.GetVersionBaseDeDatos, gConvert.dateToString(gDefProg.GetFechaDeLaVersion), gConvert.dateToString(gDefProg.GetFechaDeLaVersion), "", "", gConvert.fConvierteAString(1), False
   vResultado = objWrapper.AnularDocumentoFiscal(insclsUtilImprFiscal.fGenerarXmlConDatosDeLaMaquinaFiscal(valPuerto, valEnumPrinter), True)
   Set objWrapper = Nothing
   Set insclsUtilImprFiscal = Nothing
h_EXIT: On Error GoTo 0
   fCancelaCuponFiscal = vResultado
   Exit Function
h_Error:
   fCancelaCuponFiscal = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCancelaCuponFiscal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
