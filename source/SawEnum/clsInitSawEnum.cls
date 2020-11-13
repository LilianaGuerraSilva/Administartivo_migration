VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsInitSawEnum"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Public Sub sInitializeInternalGlobalClasses(ByRef refInsError As clsError, _
            ByRef refInsMessage As clsMessage, _
            ByRef refInsTexto As clsTexto, _
            ByRef refInsDefGen As clsDefgen, _
            ByRef refInsConvert As clsConvert, _
            ByRef refInsApi As clsDefensiveAPI, _
            ByRef refInsUtilGlobalization As clsUtilGlobalization)
'            , _
'            ByRef refInsUtilFile As clsUtilFiles, _
'            ByRef refInsUtilDate As clsUtilDate, _
'            ByRef refInsErrorCodesIG As clsErrorCodesIG, _
'            ByRef refInsUtilMathOperations As clsUtilMathOperations)
      Set gError = refInsError
      Set gMessage = refInsMessage
      Set gTexto = refInsTexto
      Set gDefGen = refInsDefGen
      Set gConvert = refInsConvert
      Set gApi = refInsApi
      Set gGlobalization = refInsUtilGlobalization
'      Set gUtilFile = refInsUtilFile
'      Set gUtilDate = refInsUtilDate
'      Set gErrorCodesIG = refInsErrorCodesIG
'      Set gUtilMathOperations = refInsUtilMathOperations
End Sub

Public Sub sSendDllVersionToLibGalacCI()
   Dim insInicializaClases As clsInitLibGalac
   Set insInicializaClases = New clsInitLibGalac
   insInicializaClases.sAddDllInfoToListOfDllsInUse getDllVersion
   Set insInicializaClases = New clsInitLibGalac
End Sub

Public Sub SetEsSistemaAdmInterno(ByVal valEsSistemaInfotax As Boolean)
   mEsSistemaAdmInterno = valEsSistemaInfotax
End Sub
