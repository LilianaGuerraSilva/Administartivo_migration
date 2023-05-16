VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsImportar"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit
Private Const FOLDER_NAME_IMPORTACION_EXPORTACION As String = "ImpExp"
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsImportar"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Importar"
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Public Function fAbrirFileForInput(ByRef refFileNumberLectura As Integer, ByVal valNombreArchivoImportacion As String) As Boolean
   On Error GoTo h_ERROR
   fAbrirFileForInput = False
   refFileNumberLectura = FreeFile
   Open valNombreArchivoImportacion For Input As refFileNumberLectura
h_EXIT: On Error GoTo 0
   fAbrirFileForInput = True
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fAbrirFileForInput", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fAbrirFileForOuputAndCreatePath(ByRef refFileNumberEscritura As Integer, ByVal valCompletePath As String, ByVal valFileDeErrores As String) As Boolean
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   If gUtilFile.fCrearCarpeta(valCompletePath) Then
      refFileNumberEscritura = FreeFile
      Open valCompletePath & "\" & valFileDeErrores For Output As refFileNumberEscritura
      exito = True
   End If
h_EXIT: On Error GoTo 0
   fAbrirFileForOuputAndCreatePath = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fAbrirFileForOuputAndCreatePath", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Sub sCierraElFile(ByRef refFileNumber As Integer)
   On Error GoTo h_ERROR
   Close refFileNumber
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCierraElFiledeErrores", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function getPathDelFileDeErrores() As String
   getPathDelFileDeErrores = gDefDatabase.getPathActualDeLaBaseDeDatos & "\" & FOLDER_NAME_IMPORTACION_EXPORTACION
End Function
Public Function fNumeroDelineasDelArchivo(ByVal valFileNumberdeLectura As Integer) As Long
   Dim lineaActual As String
   Dim NumeroDelineas As Long
   On Error GoTo h_ERROR
   NumeroDelineas = 0
   Do Until EOF(valFileNumberdeLectura)
      Line Input #valFileNumberdeLectura, lineaActual
      If lineaActual <> "" Then
         NumeroDelineas = NumeroDelineas + 1
      End If
   Loop
h_EXIT: On Error GoTo 0
   fNumeroDelineasDelArchivo = NumeroDelineas
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNumeroDelineasDelArchivo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fYaFueCargadoElItemEnElArray(ByVal valItemsArray As Variant, ByVal valIndiceActual As Long, ByVal valValorItem As String, ByVal valIndex As Long) As Boolean
   Dim valregistroActual As Long
   Dim existe As Boolean
   On Error GoTo h_ERROR
   existe = False
   If valIndiceActual > 0 And valIndex >= LBound(valItemsArray) And valIndex < UBound(valItemsArray) Then
      For valregistroActual = 0 To valIndiceActual - 1
         If UCase(valItemsArray(valIndex, valregistroActual)) = UCase(valValorItem) Then
            existe = True
            Exit For
         End If
      Next valregistroActual
   End If
h_EXIT:
  fYaFueCargadoElItemEnElArray = existe
  On Error GoTo 0
  Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "fYaFueCargadoElItemEnElArray", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fDecideSiContinuaConLaImportacion(ByVal valModuloAImportar As enum_SystemModules) As Boolean
   Dim insLibGalacForms As clsLibGalacForms
   Dim varContinuar As Boolean
   On Error GoTo h_ERROR
   varContinuar = False
   Set insLibGalacForms = New clsLibGalacForms
   insLibGalacForms.sInvokeImportMessage varContinuar, gUtilFile.fAddSlashCharToEndOfPathIfRequired(getPathDelFileDeErrores) & getFileDeErrores, gEnumProyecto.enum_SystemModulesToStr(valModuloAImportar)
   Set insLibGalacForms = Nothing
h_EXIT: On Error GoTo 0
   fDecideSiContinuaConLaImportacion = varContinuar
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "fDecideSiContinuaConLaImportacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function getFileDeErrores() As String
   getFileDeErrores = "Errores.txt"
End Function

Public Function fObtenerSeparador(ByVal valExtesion As String) As String
   Dim Separador As String
   On Error GoTo h_ERROR
   If valExtesion = "TXT" Or valExtesion = "TEXT" Or valExtesion = "DBF" Then
      Separador = Chr(9)
   Else
      Separador = ";"
   End If
h_EXIT: On Error GoTo 0
   fObtenerSeparador = Separador
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "fObtenerSeparador", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

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
h_EXIT:
   fPorcentaje = valporcentaje
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "fPorcentaje", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fVerificaValorImportarEnElArray(ByVal valItemsArray As Variant, _
            ByVal valIndiceActual As Long, ByVal valValorItem1 As String, ByVal valValorItem2 As Boolean, ByVal valIndex1 As Long, ByVal valIndex2 As Long) As Boolean
   Dim valregistroActual As Long
   Dim Importa As Boolean
   On Error GoTo h_ERROR
   Importa = False
   If valIndiceActual >= 0 And valIndex1 >= LBound(valItemsArray) And valIndex1 < UBound(valItemsArray) And valIndex2 >= LBound(valItemsArray) And valIndex2 < UBound(valItemsArray) Then
      For valregistroActual = 0 To valIndiceActual
         If UCase(valItemsArray(valIndex1, valregistroActual)) = UCase(valValorItem1) Then
            If UCase(valItemsArray(valIndex2, valregistroActual)) = UCase(valValorItem2) Then
               Importa = True
               Exit For
            End If
            Exit For
         End If
      Next valregistroActual
   End If
h_EXIT:
  fVerificaValorImportarEnElArray = Importa
  On Error GoTo 0
  Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "fVerificaValorImportarEnElArray", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sCambiaAFalseElRegistroDelArraySiExiste(ByRef valItemsArray As Variant, _
            ByVal valIndiceActual As Long, ByVal valValorItem As String, ByVal valIndex1 As Long, ByVal valIndex2 As Long, ByVal valIndex3 As Long, ByVal valor1 As Boolean, ByVal valor2 As Boolean)
   Dim valregistroActual As Long
   On Error GoTo h_ERROR
   If valIndiceActual >= 0 And valIndex1 >= LBound(valItemsArray) And valIndex1 < UBound(valItemsArray) And valIndex2 >= LBound(valItemsArray) And valIndex2 < UBound(valItemsArray) Then
      For valregistroActual = 0 To valIndiceActual
         If UCase(valItemsArray(valIndex1, valregistroActual)) = UCase(valValorItem) Then
            valItemsArray(valIndex2, valregistroActual) = valor1
            valItemsArray(valIndex3, valregistroActual) = valor2
            Exit For
         End If
      Next valregistroActual
   End If
h_EXIT: On Error GoTo 0
  Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sCambiaAFalseElRegistroDelArraySiExiste", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
