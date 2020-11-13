VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsClienteInsercionRapidaNav"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private mNombreCliente As String
Private mCodigoCliente As String
Private mRifCliente As String

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsClienteInsercionRapida"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "ClienteInsercionRapida"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Public Function GetNombreCliente() As String
   GetNombreCliente = mNombreCliente
End Function

Private Sub SetNombreCliente(ByVal valNombreCliente As String)
   mNombreCliente = valNombreCliente
End Sub

Private Sub SetRifCliente(ByVal valRifCliente As String)
   mRifCliente = valRifCliente
End Sub
Public Function GetRifCliente() As String
   GetRifCliente = mRifCliente
End Function

Public Function GetCodigoCliente() As String
   GetCodigoCliente = mCodigoCliente
End Function

Private Sub SetCodigoCliente(ByVal valcodigocliente As String)
   mCodigoCliente = valcodigocliente
End Sub

Public Function fInsertarCliente(ByVal valTipoDocumentoFactura As Integer, ByVal valNombre As String, ByVal valRif As String, ByVal valCodigoGenericoVendedor As String, ByVal valRellenaCerosAlaIzquierda As Boolean, ByVal valValidaRifEnLaWeb As Boolean) As Boolean
   Dim wrpClienteInsercionRapida As Object
   Dim SeInserto As Boolean
   Dim vParametros As String
   On Error GoTo h_ERROR
   Set wrpClienteInsercionRapida = gUtilWrp.fCreateWrpForModule(gUtilWrp.fWrpClassName("Venta", "wrpClienteInsercionRapida"), gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword)
   vParametros = "<GpData><FacturaRapida>"
   vParametros = vParametros & "<ConsecutivoCompania>" & gConvert.fConvierteAString(gProyCompaniaActual.GetConsecutivoCompania) & "</ConsecutivoCompania>"
   vParametros = vParametros & "<Ciudad>" & gConvert.fConvierteAString(gProyCompaniaActual.GetCiudad) & "</Ciudad>"
   vParametros = vParametros & "<CodigoGenericoVendedor>" & valCodigoGenericoVendedor & "</CodigoGenericoVendedor>"
   vParametros = vParametros & "<RellenaCerosAlaIzquierda>" & gConvert.ConvertBooleanToString(valRellenaCerosAlaIzquierda) & "</RellenaCerosAlaIzquierda>"
   vParametros = vParametros & "<ValidaRifEnLaWeb>" & gConvert.ConvertBooleanToString(valValidaRifEnLaWeb) & "</ValidaRifEnLaWeb>"
   vParametros = vParametros & "</FacturaRapida></GpData>"
   wrpClienteInsercionRapida.Show vParametros, valNombre, valRif, valTipoDocumentoFactura
   SeInserto = wrpClienteInsercionRapida.SeInsertoCliente
   If (wrpClienteInsercionRapida.SeInsertoCliente) Then
     SetNombreCliente (wrpClienteInsercionRapida.ObtenerNombreCliente)
     SetCodigoCliente (wrpClienteInsercionRapida.ObtenerCodigoCliente)
     SetRIFCliente (wrpClienteInsercionRapida.ObtenerRifCliente)
   End If
   Set wrpClienteInsercionRapida = Nothing
h_EXIT: On Error GoTo 0
   fInsertarCliente = SeInserto
   Exit Function
h_ERROR:    Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fInsetarCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
