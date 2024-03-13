VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsLibEnum"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsLibEnum"
Private Const CM_MESSAGE_NAME As String = "Lib Enum Saw"

Public Function fEsNumeroValidoParaWhatsapp(ByVal valNumeroTelefonico, Optional ByVal valShowMessage As Boolean = False) As String
   Dim vResult As String
   Dim vRegEx As RegExp
   On Error Resume Next
   Set vRegEx = New RegExp
   If IsNull(valNumeroTelefonico) Then
      vResult = ""
   Else
      valNumeroTelefonico = gTexto.fLimpiaStringdeBlancosAAmbosLados(valNumeroTelefonico)
      vRegEx.Pattern = "^(041|042)\d{8}(,\d{11})*$" 'Patrón Para validar Números telefónicos de la forma 04147142289 o 04247142289 o 04127142289 o 04167142289 o 04267142289
      If vRegEx.Test(valNumeroTelefonico) Then
         vResult = valNumeroTelefonico
      Else
         vResult = ""
         If valShowMessage Then
            gMessage.AlertMessage "El número " & valNumeroTelefonico & " no tiene un formato telefónico valido ", " Validación del Número Telefónico"
         End If
      End If
   End If
   Set vRegEx = Nothing
   On Error GoTo 0
End Function

Public Function fEsDireccionDeEmailValida(ByVal valDireccionEmail, Optional ByVal valShowMessage As Boolean = False) As String
   Dim vResult As String
   Dim vRegEx As RegExp
   On Error Resume Next
   Set vRegEx = New RegExp
   If IsNull(valDireccionEmail) Then
      vResult = ""
   Else
      valDireccionEmail = gTexto.fLimpiaStringdeBlancosAAmbosLados(valDireccionEmail)
      vRegEx.Pattern = "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$" 'Patrón Para validar formato de email de la forma alguien@algunlugar.es
      If vRegEx.Test(valDireccionEmail) Then
         vResult = valDireccionEmail
      Else
         vResult = ""
         If valShowMessage Then
            gMessage.AlertMessage "La dirección " & valDireccionEmail & " no tiene un formato de email valido ", " Validación de Email"
         End If
      End If
   End If
   Set vRegEx = Nothing
   On Error GoTo 0
End Function

