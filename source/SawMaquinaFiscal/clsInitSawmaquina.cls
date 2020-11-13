VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsInitSawmaquina"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit

Public Sub sInitializeInternalGlobalClasses(ByRef refInsError As clsError, _
            ByRef refInsMessage As clsMessage, _
            ByRef refInsTexto As clsTexto, _
            ByRef refInsDefGen As clsDefgen, _
            ByRef refInsConvert As clsConvert, _
            ByRef refInsApi As clsDefensiveAPI, _
            ByRef refInsUtilGlobalization As clsUtilGlobalization, _
            ByRef refInsDButil As clsDBUtil, _
            ByRef refInsUtilFile As clsUtilFiles, _
            ByRef refInsUtilMathOperations As clsUtilMathOperations, _
            ByRef refUsuarioNavigator As Object, _
            ByRef refEnumAdministrativo As clsEnumAdministrativo, _
            ByRef refDefProg As Object, _
            ByRef refWorkPaths As clsUtilWorkPaths)

      Set gError = refInsError
      Set gMessage = refInsMessage
      Set gTexto = refInsTexto
      Set gDefGen = refInsDefGen
      Set gConvert = refInsConvert
      Set gApi = refInsApi
      Set gGlobalization = refInsUtilGlobalization
      Set gDbUtil = refInsDButil
      Set gUtilFile = refInsUtilFile
      Set gUtilMathOperations = refInsUtilMathOperations
      Set gProyUsuarioActual = refUsuarioNavigator
      Set gEnumProyecto = refEnumAdministrativo
      Set gDefProg = refDefProg
      Set gWorkPaths = refWorkPaths
End Sub

Public Sub sSendDllVersionToLibGalacCI()
   Dim insInicializaClases As clsInitLibGalac
   Set insInicializaClases = New clsInitLibGalac
   insInicializaClases.sAddDllInfoToListOfDllsInUse getDllVersion
   Set insInicializaClases = New clsInitLibGalac
End Sub


