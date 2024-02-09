VERSION 5.00
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomct2.ocx"
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Begin VB.Form frmInformesDePago 
   BackColor       =   &H00F3F3F3&
   ClientHeight    =   5595
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   10440
   LinkTopic       =   "Form1"
   ScaleHeight     =   5595
   ScaleWidth      =   10440
   Begin VB.Frame frameFechas 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Fechas"
      ForeColor       =   &H80000010&
      Height          =   1035
      Left            =   2700
      TabIndex        =   43
      Top             =   600
      Width           =   2955
      Begin MSComCtl2.DTPicker txtFechaInicial 
         Height          =   285
         Left            =   1440
         TabIndex        =   12
         Top             =   240
         Width           =   1335
         _ExtentX        =   2355
         _ExtentY        =   503
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   99745795
         CurrentDate     =   36978
      End
      Begin MSComCtl2.DTPicker txtFechaFinal 
         Height          =   285
         Left            =   1440
         TabIndex        =   13
         Top             =   645
         Width           =   1335
         _ExtentX        =   2355
         _ExtentY        =   503
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   99745795
         CurrentDate     =   36978
      End
      Begin VB.Label lblFechaFinal 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Fecha Final"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   49
         Top             =   690
         Width           =   825
      End
      Begin VB.Label lblFechaInicial 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Fecha  Inicial"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   48
         Top             =   285
         Width           =   945
      End
   End
   Begin VB.Frame frameLoteDetracciones 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Lote de Detracciones"
      ForeColor       =   &H80000010&
      Height          =   675
      Left            =   5760
      TabIndex        =   80
      Top             =   600
      Width           =   2655
      Begin VB.TextBox txtNumeroDeLote 
         Height          =   285
         Left            =   1560
         MaxLength       =   6
         TabIndex        =   36
         Top             =   255
         Width           =   915
      End
      Begin VB.Label lblNumeroDeLote 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Número de Lote"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   240
         TabIndex        =   81
         Top             =   255
         Width           =   1140
      End
   End
   Begin VB.CheckBox chkMostrarObservaciones 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Mostrar Observaciones"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   7380
      TabIndex        =   77
      Top             =   1680
      Width           =   1995
   End
   Begin VB.CheckBox chkIncluirMontosComprobantesAnulados 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Incluir Montos Comprobantes Anulados"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   2700
      TabIndex        =   78
      TabStop         =   0   'False
      Top             =   3960
      Width           =   3975
   End
   Begin VB.Frame frmPeriodo 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Período     (Mes / Año)"
      ForeColor       =   &H00808080&
      Height          =   975
      Left            =   3000
      TabIndex        =   72
      Top             =   600
      Width           =   2295
      Begin VB.TextBox txtMesDesde 
         BackColor       =   &H00FFFFFF&
         ForeColor       =   &H00000000&
         Height          =   285
         Left            =   870
         MaxLength       =   2
         TabIndex        =   16
         Top             =   255
         Width           =   495
      End
      Begin VB.TextBox txtAnoDesde 
         BackColor       =   &H00FFFFFF&
         ForeColor       =   &H00000000&
         Height          =   285
         Left            =   1425
         MaxLength       =   4
         TabIndex        =   17
         Top             =   255
         Width           =   735
      End
      Begin VB.TextBox txtMesHasta 
         BackColor       =   &H00FFFFFF&
         ForeColor       =   &H00000000&
         Height          =   285
         Left            =   870
         MaxLength       =   2
         TabIndex        =   18
         Top             =   600
         Width           =   495
      End
      Begin VB.TextBox txtAnoHasta 
         BackColor       =   &H00FFFFFF&
         ForeColor       =   &H00000000&
         Height          =   285
         Left            =   1425
         MaxLength       =   4
         TabIndex        =   19
         Top             =   600
         Width           =   735
      End
      Begin VB.Label lblFrmARCVGenerar 
         AutoSize        =   -1  'True
         BackColor       =   &H80000016&
         BackStyle       =   0  'Transparent
         Caption         =   "Hasta"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   2
         Left            =   105
         TabIndex        =   74
         Top             =   645
         Width           =   420
      End
      Begin VB.Label lblFrmARCVGenerar 
         AutoSize        =   -1  'True
         BackColor       =   &H80000016&
         BackStyle       =   0  'Transparent
         Caption         =   "Desde"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   105
         TabIndex        =   73
         Top             =   300
         Width           =   465
      End
   End
   Begin VB.CommandButton cmdGenerar 
      Caption         =   "&Generar"
      Height          =   375
      Left            =   120
      TabIndex        =   37
      Top             =   5100
      Width           =   1335
   End
   Begin VB.Frame frameTipoDeclaracion 
      BackColor       =   &H00F3F3F3&
      ForeColor       =   &H00808080&
      Height          =   2415
      Left            =   120
      TabIndex        =   67
      Top             =   600
      Width           =   2775
      Begin VB.OptionButton optInformeDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Salarios y otras Retenciones.."
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   14
         Left            =   180
         TabIndex        =   11
         Top             =   2040
         Width           =   2415
      End
      Begin VB.OptionButton optInformeDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Dividendos y Acciones..........."
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   9
         Left            =   180
         TabIndex        =   9
         Top             =   960
         Width           =   2415
      End
      Begin VB.OptionButton optInformeDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Salarios y otras Retenciones.."
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   8
         Left            =   180
         TabIndex        =   8
         Top             =   600
         Width           =   2415
      End
      Begin VB.OptionButton optInformeDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Ganancias Fortuitas..............."
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   10
         Left            =   180
         TabIndex        =   10
         Top             =   1320
         Width           =   2415
      End
      Begin VB.OptionButton optInformeDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Información de Proveedores.."
         ForeColor       =   &H00A84439&
         Height          =   315
         Index           =   11
         Left            =   180
         TabIndex        =   69
         Top             =   3000
         Visible         =   0   'False
         Width           =   2415
      End
      Begin VB.OptionButton optInformeDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Retenciones Agrupadas........."
         ForeColor       =   &H00A84439&
         Height          =   315
         Index           =   12
         Left            =   180
         TabIndex        =   68
         Top             =   3360
         Visible         =   0   'False
         Width           =   2415
      End
      Begin VB.Label lblTitulosInformes 
         Alignment       =   2  'Center
         BackColor       =   &H00A86602&
         Caption         =   "Relación Informativa"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FFFFFF&
         Height          =   255
         Index           =   2
         Left            =   120
         TabIndex        =   75
         Top             =   1680
         Width           =   2475
      End
      Begin VB.Label lblTitulosInformes 
         Alignment       =   2  'Center
         BackColor       =   &H00A86602&
         Caption         =   "Retenciones Por"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FFFFFF&
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   71
         Top             =   240
         Width           =   2475
      End
      Begin VB.Label lblTitulosInformes 
         Alignment       =   2  'Center
         BackColor       =   &H00A86602&
         Caption         =   "Informes"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FFFFFF&
         Height          =   255
         Index           =   1
         Left            =   120
         TabIndex        =   70
         Top             =   2640
         Visible         =   0   'False
         Width           =   2475
      End
   End
   Begin VB.CheckBox chkSobreEscribirArchivo 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Reemplazar el Documento si Existe"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00800000&
      Height          =   195
      Left            =   3000
      TabIndex        =   22
      TabStop         =   0   'False
      Top             =   1680
      Visible         =   0   'False
      Width           =   3975
   End
   Begin VB.Frame frameQuincenas 
      BackColor       =   &H00F3F3F3&
      ForeColor       =   &H80000010&
      Height          =   675
      Left            =   2700
      TabIndex        =   62
      Top             =   1275
      Width           =   3735
      Begin VB.ComboBox cmbQuincenaAGenerar 
         Height          =   315
         Left            =   1620
         TabIndex        =   21
         Top             =   240
         Width           =   1635
      End
      Begin VB.Label lblQuincenaAGenerar 
         AutoSize        =   -1  'True
         BackStyle       =   0  'Transparent
         Caption         =   "Quincena a Generar"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   120
         TabIndex        =   63
         Top             =   300
         Width           =   1440
      End
   End
   Begin VB.Frame frameTipoDePersona 
      BackColor       =   &H00F3F3F3&
      ForeColor       =   &H80000010&
      Height          =   675
      Left            =   5640
      TabIndex        =   54
      Top             =   600
      Width           =   3735
      Begin VB.ComboBox CmbTipoDePersona 
         Height          =   315
         Left            =   1470
         TabIndex        =   20
         Text            =   "Jurídica No Domiciliada"
         Top             =   240
         Width           =   2175
      End
      Begin VB.Label lblTipoDePersona 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Tipo de Persona"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   55
         Top             =   300
         Width           =   1170
      End
   End
   Begin VB.Frame frameMoneda 
      BackColor       =   &H00F3F3F3&
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00A84439&
      Height          =   915
      Left            =   2700
      TabIndex        =   50
      Top             =   3720
      Width           =   2895
      Begin VB.ComboBox cmbMonedaDeLosReportes 
         Height          =   315
         Left            =   840
         TabIndex        =   32
         Top             =   240
         Width           =   1755
      End
      Begin VB.Label lblMonedaDeLosReportes 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Moneda"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   51
         Top             =   300
         Width           =   585
      End
   End
   Begin VB.Frame frameTasaDeCambio 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Tasa de cambio"
      ForeColor       =   &H00808080&
      Height          =   915
      Left            =   5700
      TabIndex        =   45
      Top             =   3720
      Width           =   1875
      Begin VB.OptionButton optTasaDeCambio 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Original"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   180
         TabIndex        =   33
         Top             =   240
         Width           =   1455
      End
      Begin VB.OptionButton optTasaDeCambio 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Del día"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   180
         TabIndex        =   34
         Top             =   555
         Width           =   1455
      End
   End
   Begin VB.CommandButton cmdImpresora 
      Caption         =   "&Impresora"
      Height          =   375
      Left            =   120
      TabIndex        =   35
      Top             =   5100
      Width           =   1335
   End
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "&Pantalla"
      Height          =   375
      Left            =   1680
      TabIndex        =   40
      Top             =   5100
      Width           =   1335
   End
   Begin VB.CommandButton cmdSalir 
      Cancel          =   -1  'True
      Caption         =   "&Salir"
      CausesValidation=   0   'False
      Height          =   375
      Left            =   3240
      TabIndex        =   41
      Top             =   5100
      Width           =   1335
   End
   Begin VB.Frame framePeriodoDeAplicacion 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Período de Aplicación"
      ForeColor       =   &H80000010&
      Height          =   675
      Left            =   3000
      TabIndex        =   44
      Top             =   600
      Width           =   2415
      Begin VB.TextBox txtMes 
         Height          =   285
         Left            =   960
         MaxLength       =   2
         TabIndex        =   14
         Top             =   240
         Width           =   495
      End
      Begin VB.TextBox txtAno 
         Height          =   285
         Left            =   1560
         MaxLength       =   4
         TabIndex        =   15
         Top             =   240
         Width           =   735
      End
      Begin VB.Label lblMesAno 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Mes/Año "
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   47
         Top             =   300
         Width           =   705
      End
   End
   Begin VB.CommandButton cmdExportar 
      Caption         =   "Exportar"
      Height          =   375
      Left            =   120
      TabIndex        =   61
      Top             =   5100
      Width           =   1335
   End
   Begin VB.Frame frameProveedor 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Proveedor"
      ForeColor       =   &H80000010&
      Height          =   1095
      Left            =   2700
      TabIndex        =   56
      Top             =   1950
      Width           =   7575
      Begin VB.CheckBox chkCambiandodePagina 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Una página por proveedor"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   28
         Top             =   705
         Width           =   2235
      End
      Begin VB.TextBox txtNombreDeProveedor 
         Height          =   285
         Left            =   2700
         TabIndex        =   27
         Top             =   660
         Width           =   4755
      End
      Begin VB.ComboBox CmbCantidadAImprimir 
         Height          =   315
         Left            =   1680
         TabIndex        =   25
         Top             =   240
         Width           =   1575
      End
      Begin VB.TextBox txtCodigoDeProveedor 
         Height          =   285
         Left            =   1680
         TabIndex        =   26
         Top             =   660
         Width           =   975
      End
      Begin VB.Label lblCantidadAimprimir 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Cantidad a Imprimir"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   58
         Top             =   300
         Width           =   1335
      End
      Begin VB.Label lblNombre 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Nombre "
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   57
         Top             =   705
         Width           =   600
      End
   End
   Begin VB.ComboBox cmbTipoDeOperacionAExportar 
      Height          =   315
      Left            =   5160
      TabIndex        =   23
      Top             =   2160
      Width           =   1635
   End
   Begin VB.CheckBox chkAgruparPorProveedor 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Agrupar por Proveedor"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   2940
      TabIndex        =   24
      Top             =   2640
      Width           =   1935
   End
   Begin MSComDlg.CommonDialog cdControlDeArchivo 
      Left            =   0
      Top             =   0
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.Frame frmRutaExportacion 
      BackColor       =   &H00F3F3F3&
      Caption         =   "RutadeDestino"
      ForeColor       =   &H00808080&
      Height          =   735
      Left            =   120
      TabIndex        =   66
      Top             =   3120
      Width           =   7635
      Begin VB.TextBox txtNombreDelArchivo 
         Enabled         =   0   'False
         ForeColor       =   &H00808080&
         Height          =   315
         Left            =   120
         TabIndex        =   38
         Top             =   360
         Width           =   6825
      End
      Begin VB.CommandButton cmdBuscar 
         Caption         =   "..."
         Height          =   315
         Left            =   7080
         TabIndex        =   39
         Top             =   310
         Width           =   420
      End
   End
   Begin VB.Frame frameConceptoDeRetencion 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Concepto de Retención"
      ForeColor       =   &H80000010&
      Height          =   675
      Left            =   2700
      TabIndex        =   52
      Top             =   3045
      Width           =   7575
      Begin VB.TextBox txtCodigoDeRetencion 
         Height          =   285
         Left            =   1860
         TabIndex        =   30
         Top             =   240
         Width           =   840
      End
      Begin VB.Label lblCodigoDeRetencion 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Código de Retención"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   53
         Top             =   300
         Width           =   1500
      End
      Begin VB.Label lblDescripcionRetencion 
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         BorderStyle     =   1  'Fixed Single
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00A84439&
         Height          =   285
         Left            =   2760
         TabIndex        =   31
         Top             =   240
         Width           =   4695
      End
   End
   Begin VB.Frame frameInformes 
      BackColor       =   &H00F3F3F3&
      ForeColor       =   &H80000010&
      Height          =   3795
      Left            =   120
      TabIndex        =   42
      Top             =   0
      Width           =   2475
      Begin VB.OptionButton optInformeDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Generar archivo para pagos de detracciones...."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   390
         Index           =   15
         Left            =   120
         TabIndex        =   79
         Top             =   3720
         Visible         =   0   'False
         Width           =   2115
      End
      Begin VB.OptionButton optInformeDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Listado de comprobantes de Retención I.V.A. ....."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   390
         Index           =   13
         Left            =   120
         TabIndex        =   7
         Top             =   3240
         Width           =   2115
      End
      Begin VB.OptionButton optInformeDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Libro de Retenciones Renta  ..........................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   390
         Index           =   7
         Left            =   120
         TabIndex        =   65
         Top             =   3240
         Width           =   2115
      End
      Begin VB.OptionButton optInformeDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Comprobante Resumen de Retención I.V.A. ....."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   390
         Index           =   6
         Left            =   120
         TabIndex        =   6
         Top             =   2760
         Width           =   2115
      End
      Begin VB.OptionButton optInformeDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Declaración Informativa .."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   5
         Left            =   120
         TabIndex        =   5
         Top             =   2400
         Width           =   2115
      End
      Begin VB.OptionButton optInformeDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Borrador de Planilla de Declaración y Pago ........"
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   390
         Index           =   4
         Left            =   120
         TabIndex        =   4
         Top             =   1800
         Width           =   2115
      End
      Begin VB.OptionButton optInformeDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Retenciones &Mensuales   X  Tipo de Persona ........."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   390
         Index           =   3
         Left            =   120
         TabIndex        =   3
         Top             =   1320
         Width           =   2115
      End
      Begin VB.OptionButton optInformeDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Retenciones &X Concepto"
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   2
         Left            =   120
         TabIndex        =   2
         Top             =   960
         Width           =   2115
      End
      Begin VB.OptionButton optInformeDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "&Retenciones .................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   120
         TabIndex        =   1
         Top             =   600
         Width           =   2115
      End
      Begin VB.OptionButton optInformeDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "&Pagos entre fechas........."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   0
         Top             =   240
         Width           =   2115
      End
   End
   Begin VB.CheckBox chkIncluirRegistrosMtoRetIvaCero 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Incluir registros con Monto Iva Retenido igual a 0"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   2700
      TabIndex        =   29
      Top             =   3285
      Width           =   3975
   End
   Begin VB.CheckBox chkExcluirComprobantesSinNumero 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Excluir Comprobantes Sin Nro. Asignado"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   2700
      TabIndex        =   76
      TabStop         =   0   'False
      Top             =   3600
      Width           =   3975
   End
   Begin VB.Label lblNota2 
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Nota: Los Informes no disponibles son para Contribuyentes Especiales."
      ForeColor       =   &H00A84439&
      Height          =   615
      Left            =   120
      TabIndex        =   60
      Top             =   4350
      Width           =   2475
   End
   Begin VB.Label lblNota1 
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Nota: Los Informes no disponibles requieren el uso de Retenciones."
      ForeColor       =   &H00A84439&
      Height          =   375
      Left            =   120
      TabIndex        =   59
      Top             =   3855
      Width           =   2475
   End
   Begin VB.Label lblDatosDelInforme 
      AutoSize        =   -1  'True
      BackColor       =   &H00F3F3F3&
      BackStyle       =   0  'Transparent
      Caption         =   "lblDatosDelInforme"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000040C0&
      Height          =   300
      Left            =   3000
      TabIndex        =   46
      Top             =   180
      Width           =   2340
   End
   Begin VB.Label lblTipoDeOperacionAExportar 
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "Tipo de Operación a exportar"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   2700
      TabIndex        =   64
      Top             =   2220
      Width           =   2070
   End
End
Attribute VB_Name = "frmInformesDePago"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private mInformeSeleccionado As Integer
Private mDondeImprimir As enum_DondeImprimir

Private gProyCompaniaActual As Object
Private insTablaRetencion As Object
Private insProveedor As Object
Private insPagoSQL As Object
Private insPagoRpt As Object
Private insLibWincont As Object
Private insComprobante As Object
Private insRptPagoConfigurar As Object
Private insSearchConnectionComunAOS As Object
Private insConexionesSawAOS As Object
Private insFactura As Object
Private insRenglonFact As Object
Private insPago As Object
Private insCxP As Object
Private insUtilXml As Object

Private mRutaDelArchivo As String
Private mFechaInicial As Date
Private mFechaFinal As Date
Private mMes As Integer
Private mAno As Long
Private mYaAjustoFechaDeclInfor As Boolean

Private Const CM_FILE_NAME As String = "frmInformesDePago"
Private Const CM_MESSAGE_NAME As String = "Informes De Pago"

Private Const CM_OPT_PAGOS_ENTRE_FECHAS As Integer = 0
Private Const CM_OPT_RET_EN_PAGOS As Integer = 1
Private Const CM_OPT_RET_POR_CONCEPTO As Integer = 2
Private Const CM_OPT_RET_MENSUALES_POR_TIPOPERSONA As Integer = 3
Private Const CM_OPT_BORRADOR_DE_PLANILLA_DE_DECLARACION_Y_PAGO As Integer = 4
Private Const CM_OPT_DECLARACION_INFORMATIVA As Integer = 5
Private Const CM_OPT_COMPROBANTE_RESUMEN_IVA As Integer = 6
Private Const CM_OPT_LIBRO_RETENCIONES_RENTA As Integer = 7
Private Const CM_OPT_TIPO_SALARIOS_OTROS As Integer = 8
Private Const CM_OPT_TIPO_DIVIDENDOS_ACCIONES As Integer = 9
Private Const CM_OPT_TIPO_GANANCIAS_FORTUITAS As Integer = 10
Private Const CM_OPT_TIPO_RELACION_INFORMATIVA_SALARIO As Integer = 14
Private Const CM_QDI_PrimeraQuincena As Integer = 0
Private Const CM_QDI_SegundaQuincena As Integer = 1
Private Const CM_QDI_TodoElMes As Integer = 2
Private Const CM_TOE_Compras As Integer = 0
Private Const CM_TOE_ComprasYVentas As Integer = 1
Private Const CM_OPT_GENERAR_ARCHIVO_PAGO_DETRACCIONES As Integer = 15

Private Const GetGender As Integer = eg_Male


Private Sub cmbMonedaDeLosReportes_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbMonedaDeLosReportes_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbTipoDePersona_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "CmbTipoDePersona_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCheckForSpecialKeys(ByVal valKeyCode As Integer, ByVal valShift As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, valKeyCode) Then
   ElseIf valKeyCode = vbKeyF6 Then
      gAPI.ssSetFocus cmdGrabar
      cmdGrabar_Click
   ElseIf valKeyCode = vbKeyF8 Then
      gAPI.ssSetFocus cmdImpresora
      cmdImpresora_Click
   ElseIf valKeyCode = vbKeyF5 Then
      gAPI.ssSetFocus cmdGenerar
      cmdGenerar_Click
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCheckForSpecialKeys", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdBuscar_Click()
   On Error GoTo h_ERROR
   If mInformeSeleccionado = CM_OPT_GENERAR_ARCHIVO_PAGO_DETRACCIONES Then
      cdControlDeArchivo.InitDir = fBuscaRutaDestino
      cdControlDeArchivo.FileName = "D" & gProyCompaniaActual.getNumeroDeRif & txtNumeroDeLote.Text '& ".txt"
      cdControlDeArchivo.DefaultExt = "txt"
      cdControlDeArchivo.Filter = "*.TXT"
      cdControlDeArchivo.DialogTitle = "Buscar Archivo a Generar"
      cdControlDeArchivo.ShowSave
         If (cdControlDeArchivo.FileName <> "") And (UCase(cdControlDeArchivo.FileName) <> UCase("*.*")) Then
            txtNombreDelArchivo.Text = gUtilFile.fDirectorioDe(cdControlDeArchivo.FileName) & "\D" & gProyCompaniaActual.getNumeroDeRif & txtNumeroDeLote.Text & ".txt"
            cmdGrabar.Enabled = True
         End If
   Else
      cdControlDeArchivo.InitDir = mRutaDelArchivo
      cdControlDeArchivo.FileName = "retISLR" & gConvert.fConvierteAString(txtAno) & gConvert.fConvierteAString(txtMes) & fNombreDeclaracion & ".xml"
      cdControlDeArchivo.DefaultExt = "*.xml*"
      cdControlDeArchivo.DialogTitle = "Buscar Archivo a Generar"
      cdControlDeArchivo.ShowSave
      If (cdControlDeArchivo.FileName <> "") And (UCase(cdControlDeArchivo.FileName) <> UCase("*.*")) Then
         txtNombreDelArchivo.Text = UCase(cdControlDeArchivo.FileName)
         cmdGrabar.Enabled = True
      End If
   End If
h_EXIT:     On Error GoTo 0
   Exit Sub
h_ERROR:
      gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdBuscar_Click", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub cmdExportar_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_IMPRESORA
   sMuestraElInformeSeleccionado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdExportar_Click", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdGenerar_Click()
   On Error GoTo h_ERROR
   If mInformeSeleccionado = CM_OPT_GENERAR_ARCHIVO_PAGO_DETRACCIONES Then
      sGenerarArchivoPagoDetracciones
   Else
      If gLibControlesGalac.fValidateAllFields(Me) And gAPI.ValidateAllFields(Me) Then
         sGenerarArchivoXmlRet
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdGenerar_Click", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdGrabar_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_PANTALLA
   sMuestraElInformeSeleccionado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdGrabar_Click", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdImpresora_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdImpresora_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdSalir_Click()
   On Error GoTo h_ERROR
   Unload Me
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdSalir_Click", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdGrabar_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdGrabar_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdSalir_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
  sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdSalir_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdImpresora_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_IMPRESORA
   sMuestraElInformeSeleccionado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdGrabar_Click", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Form_Load()
   On Error GoTo h_ERROR
   Me.Caption = CM_MESSAGE_NAME
   Me.AutoRedraw = True
   Me.ZOrder 0
   If gDefgen.getMainForm.Width > Width Then
      Left = (gDefgen.getMainForm.Width - Width) / 4
      Top = (gDefgen.getMainForm.Height - Height) / 4
   Else
      Left = 0
      Top = 0
   End If
   Me.Width = 10560
   Me.Height = 6180
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Form_Load", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Form_Unload(Cancel As Integer)
   Dim insFechasDeLosInformes As clsFechasDeLosInformesNav
   On Error GoTo h_ERROR
   Set insFechasDeLosInformes = New clsFechasDeLosInformesNav
   insFechasDeLosInformes.sGrabasLasFechasDeInformes txtFechaInicial.Value, txtFechaFinal.Value, gProyUsuarioActual.GetNombreDelUsuario
   
   Set insTablaRetencion = Nothing
   Set insProveedor = Nothing
   Set insFechasDeLosInformes = Nothing
   Set insPagoSQL = Nothing
   Set insPagoRpt = Nothing
   Set insLibWincont = Nothing
   Set insComprobante = Nothing
   Set insRptPagoConfigurar = Nothing
   Set insSearchConnectionComunAOS = Nothing
   Set insFactura = Nothing
   Set insRenglonFact = Nothing
   Set insPago = Nothing
   Set insCxP = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Form_Unload", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optInformeDePago_Click(Index As Integer)
   On Error GoTo h_ERROR
   mInformeSeleccionado = Index
   sOcultaTodosLosCampos
   Select Case mInformeSeleccionado
      Case CM_OPT_PAGOS_ENTRE_FECHAS: sActivarCamposDePagosEntreFechas
      Case CM_OPT_RET_EN_PAGOS: sActivarCamposDeRetencionesEnPagos
      Case CM_OPT_RET_POR_CONCEPTO: sActivarCamposDeRetencionesXConcepto
      Case CM_OPT_RET_MENSUALES_POR_TIPOPERSONA: sActivarCamposDeRetMenXTipoPersona
      Case CM_OPT_BORRADOR_DE_PLANILLA_DE_DECLARACION_Y_PAGO: sActivarCamposDeBorradorDePlanillaDeDeclaracionYPago
      Case CM_OPT_DECLARACION_INFORMATIVA: sActivarCamposDeDeclaracionInformativa
      Case CM_OPT_COMPROBANTE_RESUMEN_IVA: sActivarCamposDeComprobanteResumenIVA
      Case CM_OPT_LIBRO_RETENCIONES_RENTA: sActivarCamposDeLibroDeRetencionesRenta
      Case CM_OPT_TIPO_SALARIOS_OTROS: sActivarCamposDeDeclaracionXmlSueldosYOtros
      Case CM_OPT_TIPO_DIVIDENDOS_ACCIONES: sActivarCamposDeDeclaracionXmlDividendoAcciones
      Case CM_OPT_TIPO_RELACION_INFORMATIVA_SALARIO: sActivarCamposDeRelcionInformativa
      Case CM_OPT_TIPO_GANANCIAS_FORTUITAS: sActivarCamposDeDeclaracionXmlGanaciaFortuita
      Case CM_OPT_TIPO_LISTADO_RET_IVA: sActivarCamposDeListadoRetencionIva
      Case CM_OPT_GENERAR_ARCHIVO_PAGO_DETRACCIONES: sActivarCamposDeGenerarArchivoPagoDetracciones
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optInformeDePago_Click", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAnoDesde_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtAnoDesde
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAnoDesde_GotFocus", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAnoDesde_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAnoDesde_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAnoDesde_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If Not gAPI.esUnAsciiCodeDeInputNumerico(KeyAscii, False, False) Then
      KeyAscii = 0
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAnoDesde_KeyPress", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAnoDesde_LostFocus()
   On Error GoTo h_ERROR
   sActualizaFechaFinal
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAnoDesde_LostFocus", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAnoDesde_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   mFechaInicial = gConvert.fConvertStringToDate("01/" & txtMesDesde.Text & "/" & txtAnoDesde.Text)
   If txtAnoDesde.Text = "" Or Len(txtAnoDesde.Text) = 0 Or gConvert.ConvierteAInteger(txtAnoDesde.Text) > 3999 Or gConvert.ConvierteAInteger(txtAnoDesde.Text) < 1900 Then
      txtAnoDesde.Text = gTexto.llenaConCaracterALaIzquierda(Year(mFechaInicial), "0", 2)
      sActualizaFechaFinal
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAnoDesde_Validate", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAnoHasta_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtAnoHasta
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAnoHasta_GotFocus", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAnoHasta_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAnoHasta_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAnoHasta_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If Not gAPI.esUnAsciiCodeDeInputNumerico(KeyAscii, False, False) Then
      KeyAscii = 0
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAnoHasta_KeyPressv", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAnoHasta_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   mFechaFinal = gUtilDate.fColocaUltimoDiaDelMes(gConvert.fConvertStringToDate("01/" & txtMesHasta.Text & "/" & txtAnoHasta.Text))
   If txtAnoHasta.Text = "" Or Len(txtAnoHasta.Text) = 0 Or gConvert.ConvierteAInteger(txtAnoHasta.Text) > 3999 Or gConvert.ConvierteAInteger(txtAnoHasta.Text) < 1900 Then
'      txtAnoHasta.Text = gTexto.llenaConCaracterALaIzquierda(Year(mFechaFinal), "0", 2)
      sActualizaFechaFinal
   ElseIf gUtilDate.fNumberOfMonthsFromF1ToF2(mFechaInicial, mFechaFinal) > 12 Then
      txtAnoHasta.Text = Year(gUtilDate.SumaNmeses(mFechaInicial, 11, True))
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAnoHasta_Validate", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoDeProveedor_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtCodigoDeProveedor
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoDeProveedor_GotFocus", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoDeProveedor_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoDeProveedor_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoDeProveedor_Validate(Cancel As Boolean)
   Dim refCodigoProveedor As String
   Dim refNombreProveedor As String
   On Error GoTo h_ERROR
   If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      If LenB(txtCodigoDeProveedor.Text) = 0 Then
         txtCodigoDeProveedor.Text = "*"
      End If
      If insConexionesSawAOS.fSelectAndSetValuesOfProveedorFromAOS(insProveedor, refCodigoProveedor, refNombreProveedor, txtCodigoDeProveedor.Text, "") Then
         sAssignFieldsFromConnectionProveedor
      Else
         Cancel = True
         GoTo h_EXIT
      End If
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoDeProveedor_Validate", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtFechaInicial_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtFechaInicial_KeyDown()", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtFechaFinal_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtFechaFinal_KeyDown()", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtMes_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtMes
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMes_GotFocus", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtMes_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMes_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtMes_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If Not txtMes.Visible Then
      GoTo h_EXIT
   End If
   txtMes.Text = gTexto.fLimpiaStringdeBlancosAAmbosLados(txtMes.Text)
   If LenB(txtMes.Text) = 0 Or txtMes.Text = "" Then
      sShowMessageForRequiredFields "Mes", txtMes
      Cancel = True
   ElseIf gConvert.ConvierteAInteger(txtMes.Text) < 1 Or gConvert.ConvierteAInteger(txtMes.Text) > 12 Then
      gMessage.Advertencia "Debe introducir un numero de 'Mes' válido"
      Cancel = True
   End If
   txtMes.Text = gTexto.llenaConCaracterALaIzquierda(txtMes.Text, "0", 2)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMes_Validate", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sMuestraElInformeSeleccionado()
   On Error GoTo h_ERROR
   Select Case mInformeSeleccionado
      Case CM_OPT_PAGOS_ENTRE_FECHAS: sMuestraPagosEntreFechas 'Ok pasado rpt
      Case CM_OPT_RET_EN_PAGOS: sMuestraRetEnPagos 'ok pasado rpt
      Case CM_OPT_RET_POR_CONCEPTO: sMuestraRetencionesXConcepto 'Ok pasado rpt
      Case CM_OPT_RET_MENSUALES_POR_TIPOPERSONA: sMuestraRetMesualesXTipoPersona 'por pasar
      Case CM_OPT_BORRADOR_DE_PLANILLA_DE_DECLARACION_Y_PAGO: sMuestraBorradorDePlanillaDeDeclaracionYPago
      Case CM_OPT_DECLARACION_INFORMATIVA: sMuestraDeclaracionInformativa 'Ok pasado Vista
      Case CM_OPT_COMPROBANTE_RESUMEN_IVA: sMuestraComprobanteResumenIVA 'Ok pasado Vista
      Case CM_OPT_LIBRO_RETENCIONES_RENTA: sMuestraLibroDeRetencionesRenta
      Case CM_OPT_TIPO_SALARIOS_OTROS: sMuestraSalariosYOtros
      Case CM_OPT_TIPO_DIVIDENDOS_ACCIONES: sMuestraDividendosYAcciones
      Case CM_OPT_TIPO_GANANCIAS_FORTUITAS: sMuestraInformeGanaciasFortuitas
      Case CM_OPT_TIPO_LISTADO_RET_IVA: sMuestraListadoRetencionIva
      Case CM_OPT_GENERAR_ARCHIVO_PAGO_DETRACCIONES: sGenerarArchivoPagoDetracciones
'      Case CM_OPT_RELACION_INFORMATIVA: sGenerarXMLRelacionAnual
'      Case CM_OPT_TIPO_INFORME_PROVEEDORES: sMuestraInformeInconsistenciaProveedores
'      Case CM_OPT_TIPO_INFORME_PAGOS: sMuestraInformeInconsistenciaPagos
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sMuestraElInformeSeleccionado", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sMuestraPagosEntreFechas()
   Dim SqlDelReporte As String
   Dim varAgruparPorProveedor As Boolean
   Dim reporte As DDActiveReports2.ActiveReport
   Dim usarCambioOriginal As Boolean
   Dim unaPaginaPorProveedor As Boolean
   Dim ReporteEnMonedaLocal As Boolean
   Dim CantidadAImprimirUno As Boolean
   Dim MensajesDeMonedaParaInformes As String
   On Error GoTo h_ERROR
   If gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      If txtNombreDeProveedor.Text = "" Then
         sShowMessageForRequiredFields "Nombre", txtNombreDeProveedor
         GoTo h_EXIT
      End If
   End If
   varAgruparPorProveedor = (CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno)) Or (chkAgruparPorProveedor.Value = vbChecked)
   If txtFechaFinal.Value < txtFechaInicial.Value Then
      txtFechaFinal.Value = txtFechaInicial.Value
   End If
   If gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
      unaPaginaPorProveedor = gAPI.fGetCheckBoxValue(chkCambiandodePagina)
   End If
   If optTasaDeCambio(0).Value Then
      MensajesDeMonedaParaInformes = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
   ElseIf optTasaDeCambio(1).Value Then
      MensajesDeMonedaParaInformes = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioDelDia)
   End If
   ReporteEnMonedaLocal = gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal)
   CantidadAImprimirUno = gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_uno)
   SqlDelReporte = insPagoSQL.fConstruirSQLDelReportePagosEntreFechas(varAgruparPorProveedor, optTasaDeCambio(0).Value, ReporteEnMonedaLocal, gProyCompaniaActual.GetUsaModuloDeContabilidad, gProyCompaniaActual.GetConsecutivoCompania, txtFechaInicial.Value, txtFechaFinal.Value, CantidadAImprimirUno, txtCodigoDeProveedor.Text, gProyParametros.GetUsarCodigoProveedorEnPantalla, gMonedaLocalActual, gUltimaTasaDeCambio, insComprobante.GetTableName, gConvert.ConvertByteToBoolean(chkMostrarObservaciones.Value))
   Set reporte = New DDActiveReports2.ActiveReport
   If insPagoRpt.fConfigurarDatosDePagosEntreFechas(reporte, SqlDelReporte, txtFechaInicial.Value, txtFechaFinal.Value, varAgruparPorProveedor, usarCambioOriginal, unaPaginaPorProveedor, ReporteEnMonedaLocal, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gUtilDate.getFechaDeHoy, gUtilDate.getHoraActualString, gProyCompaniaActual.GetUsaModuloDeContabilidad, gGlobalization, gConvert.ConvertByteToBoolean(chkMostrarObservaciones.Value), gProyCompaniaActual, MensajesDeMonedaParaInformes) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Pagos entre Fechas"
   End If
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sMuestraPagosEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDePagosEntreFechas()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Pagos entre fechas"
   gEnumTablaRetencion.FillComboBoxWithTipodePersonaRetencion CmbTipoDePersona, Buscar
   CmbTipoDePersona.Width = 2175
   frameFechas.Visible = True
   frameProveedor.Visible = True
   chkMostrarObservaciones.Visible = True
      frameTasaDeCambio.Visible = True
      frameMoneda.Visible = True
   If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      If gProyParametros.GetUsarCodigoProveedorEnPantalla Then
         txtCodigoDeProveedor.Visible = True
         txtNombreDeProveedor.Enabled = False
      Else
         txtCodigoDeProveedor.Visible = False
         txtNombreDeProveedor.Enabled = True
         txtNombreDeProveedor.Left = txtCodigoDeProveedor.Left
      End If
      chkCambiandodePagina.Visible = False
      chkCambiandodePagina.Enabled = False
   Else
      chkCambiandodePagina.Visible = True
      chkCambiandodePagina.Enabled = True
   End If
   chkAgruparPorProveedor.Visible = (CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS))
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDePagosEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub


Private Sub sInitDefaultValues()
   Dim insFechasDeLosInformes As clsFechasDeLosInformesNav
   On Error GoTo h_ERROR
   Set insFechasDeLosInformes = New clsFechasDeLosInformesNav
   insFechasDeLosInformes.sLeeLasFechasDeInformes txtFechaInicial, txtFechaFinal, gProyUsuarioActual.GetNombreDelUsuario
   gEnumTablaRetencion.FillComboBoxWithTipodePersonaRetencion CmbTipoDePersona, Buscar
   CmbTipoDePersona.Width = 2175
   gEnumReport.FillComboBoxWithCantidadAImprimir CmbCantidadAImprimir
   CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno)
   CmbCantidadAImprimir.ListIndex = 0
   gEnumReport.FillComboBoxWithMonedaDeLosReportes cmbMonedaDeLosReportes, gProyParametros.GetNombreMonedaLocal
   cmbMonedaDeLosReportes.Width = 1980
   cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal)
   cmbMonedaDeLosReportes.ListIndex = eMR_EnMonedaOriginal
   FillComboBoxWithTipoOperacionAExportar
   Set insFechasDeLosInformes = Nothing
   sAjustaOpcionesSegunParametrosCompania
   mInformeSeleccionado = 0
   optTasaDeCambio(0).Value = True
   sAjustaOpcionesSegunPais
   mRutaDelArchivo = fBuscaRutaDestino
   mYaAjustoFechaDeclInfor = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitDefaultValues", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sShowMessageForRequiredFields(ByVal valCampo As String, ByRef refCampo As TextBox)
   On Error GoTo h_ERROR
   gMessage.ShowRequiredFields valCampo
   gAPI.ssSetFocus refCampo
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sShowMessageForRequiredFields", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeRetencionesEnPagos()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Retenciones en Pagos"
   gEnumTablaRetencion.FillComboBoxWithTipodePersonaRetencion CmbTipoDePersona, Buscar
   CmbTipoDePersona.Width = 2175
   framePeriodoDeAplicacion.Visible = True
   frameTipoDePersona.Visible = True
   frameMoneda.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeRetencionesEnPagos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sMuestraRetEnPagos()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim SqlDelReporte As String

   On Error GoTo h_ERROR
   If txtMes.Text = "" Then
      sShowMessageForRequiredFields "Mes", txtMes
      GoTo h_EXIT
   ElseIf txtAno.Text = "" Then
      sShowMessageForRequiredFields "Año", txtAno
       GoTo h_EXIT
   End If
   Set reporte = New DDActiveReports2.ActiveReport
   SqlDelReporte = insPagoSQL.fSQLRetencionesPorMesAnoTipoDePersona(gProyCompaniaActual.GetConsecutivoCompania, txtMes.Text, txtAno.Text, CmbTipoDePersona.Text, gUltimaTasaDeCambio, gMonedaLocalActual)
   If insPagoRpt.fConfigurarInformeDeRetencionesPorMesAnoTipoDePersona(reporte, SqlDelReporte, Trim(txtMes.Text), Trim(txtAno.Text), Trim(CmbTipoDePersona.Text), gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gGlobalization) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Retenciones por Mes - Año"
   End If
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sMuestraPagosEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoDeRetencion_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtCodigoDeRetencion
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoDeRetencion_GotFocus", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoDeRetencion_KeyDown(KeyCode As Integer, Shift As Integer)
  On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoDeRetencion_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoDeRetencion_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If gTexto.DfLen(txtCodigoDeRetencion.Text) = 0 Then
      txtCodigoDeRetencion.Text = "*"
   End If

   If LenB(txtCodigoDeRetencion.Text) = 0 Then
      txtCodigoDeRetencion = "*"
   End If
   If Not fSelectAndSetValuesOfTablaRetencionAOS() Then
      Cancel = True
      GoTo h_EXIT
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoDeRetencion_Validate", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fSelectAndSetValuesOfTablaRetencionAOS() As Boolean
   Dim xmlstring As String
   Dim vResult As Boolean
   Dim vTipoPersona As String
   On Error GoTo h_ERROR
   vResult = True
   If CmbTipoDePersona.Text = "Todos(as)" Then
      vTipoPersona = "*"
   Else
      vTipoPersona = gEnumProyecto.strTipodePersonaRetencionToNum(CmbTipoDePersona, False)
   End If
   If insSearchConnectionComunAOS.fSelectAndSetValuesOfTablaRetencionAOS(txtCodigoDeRetencion.Text, gEnumProyecto.strTipodePersonaRetencionToNum(CmbTipoDePersona, False), gUtilDate.getFechaDeHoy, xmlstring) Then
      gLibGalacDataParse.Initialize xmlstring
      txtCodigoDeRetencion.Text = gLibGalacDataParse.GetString(0, "Codigo", "")
      lblDescripcionRetencion.Caption = gLibGalacDataParse.GetString(0, "TipoDePago", "")
   Else
      txtCodigoDeRetencion.Text = ""
      lblDescripcionRetencion.Caption = ""
      vResult = False
   End If
   fSelectAndSetValuesOfTablaRetencionAOS = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSelectAndSetValuesOfTablaRetencionAOS", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sActivarCamposDeRetencionesXConcepto()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Retenciones por concepto"
   framePeriodoDeAplicacion.Visible = True
   frameConceptoDeRetencion.Visible = True
   frameTipoDePersona.Visible = True
   frameProveedor.Visible = True
   If Not gProyParametros.GetUsarCodigoProveedorEnPantalla Then
      txtCodigoDeProveedor.Enabled = False
   End If
   If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
      lblNombre.Visible = False
      txtCodigoDeProveedor.Visible = False
      txtNombreDeProveedor.Visible = False
   Else
      lblNombre.Visible = True
      txtCodigoDeProveedor.Visible = True
      txtNombreDeProveedor.Visible = True
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeRetencionesXConcepto", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sMuestraRetencionesXConcepto()
   Dim SqlDelReporte As String
   Dim varReporte As DDActiveReports2.ActiveReport
   Dim CantidadAImprimirUno As Boolean
   Dim vFechaIniciodeVigenciaTablaRetencion As Date
   On Error GoTo h_ERROR
   vFechaIniciodeVigenciaTablaRetencion = insSearchConnectionComunAOS.fBuscaFechaDeInicioDeVigenciaTablaRetencion(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(txtMes.Text, txtAno.Text, False))

   If txtMes.Text = "" Then
      sShowMessageForRequiredFields "Mes", txtMes
      GoTo h_EXIT
   ElseIf txtAno.Text = "" Then
      sShowMessageForRequiredFields "Año", txtAno
       GoTo h_EXIT
   End If
   If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      If txtNombreDeProveedor.Text = "" Then
         sShowMessageForRequiredFields "Nombre", txtNombreDeProveedor
         GoTo h_EXIT
      End If
   End If
   If txtCodigoDeRetencion.Text = "" Then
      sShowMessageForRequiredFields "Codigo de Retención", txtCodigoDeRetencion
      GoTo h_EXIT
   End If
   CantidadAImprimirUno = CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno)
   SqlDelReporte = insPagoSQL.fConstruirSQLDelReporteRetXConcepto(gProyCompaniaActual.GetConsecutivoCompania, txtMes.Text, txtAno.Text, txtCodigoDeRetencion.Text, CantidadAImprimirUno, txtCodigoDeProveedor.Text, CmbTipoDePersona.Text, gUltimaTasaDeCambio, gMonedaLocalActual, vFechaIniciodeVigenciaTablaRetencion)
   Set varReporte = New ActiveReport
   If insPagoRpt.fConfigurarRetPagosXConcepto(varReporte, SqlDelReporte, txtMes.Text, txtAno.Text, CmbTipoDePersona.Text, txtCodigoDeRetencion.Text, lblDescripcionRetencion.Caption, gDefgen.getSearchAllComboBox, gProyCompaniaActual.GetNombreCompaniaParaInformes(True), gUtilDate.getFechaDeHoy, gUtilDate.getHoraActualString, gGlobalization) Then
      gUtilReports.sMostrarOImprimirReporte varReporte, 1, mDondeImprimir, "Retenciones por Concepto"
   End If
   Set varReporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sMuestraPagosEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbCantidadAImprimir_Click()
   On Error GoTo h_ERROR
   txtNombreDeProveedor.Text = ""
   txtCodigoDeProveedor.Text = ""
   If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
      lblNombre.Visible = False
      txtNombreDeProveedor.Visible = False
      txtCodigoDeProveedor.Visible = False
      chkCambiandodePagina.Visible = True
      chkCambiandodePagina.Enabled = True
      If mInformeSeleccionado = CM_OPT_COMPROBANTE_RESUMEN_IVA Then
         chkAgruparPorProveedor.Visible = False
         chkAgruparPorProveedor.Value = vbUnchecked
      Else
         chkAgruparPorProveedor.Visible = False
         chkAgruparPorProveedor.Value = vbChecked
      End If
      If mInformeSeleccionado = CM_OPT_PAGOS_ENTRE_FECHAS Then
         chkAgruparPorProveedor.Visible = True
         chkAgruparPorProveedor.Value = vbChecked
         chkAgruparPorProveedor.Left = 7380
         chkAgruparPorProveedor.Top = 1320
      End If
   Else
      chkAgruparPorProveedor.Visible = False
      chkAgruparPorProveedor.Value = vbUnchecked
      lblNombre.Visible = True
      txtNombreDeProveedor.Visible = True
      chkCambiandodePagina.Visible = False
      chkCambiandodePagina.Enabled = False
      If gProyParametros.GetUsarCodigoProveedorEnPantalla Then
         txtCodigoDeProveedor.Visible = True
         txtNombreDeProveedor.Enabled = False
      Else
         txtCodigoDeProveedor.Visible = False
         txtNombreDeProveedor.Enabled = True
         txtNombreDeProveedor.Left = txtCodigoDeProveedor.Left
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbCantidadAImprimir_Click", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbCantidadAImprimir_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbCantidadAImprimir_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtMesDesde_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtMesDesde
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMesDesde_GotFocus", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtMesDesde_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMesDesde_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtmesDesde_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If Not gAPI.esUnAsciiCodeDeInputNumerico(KeyAscii, False, False) Then
      KeyAscii = 0
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMesDesde_KeyPress", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtMesDesde_LostFocus()
   On Error GoTo h_ERROR
   sActualizaFechaFinal
h_EXIT:    On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMesDesde_LostFocus", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtMesDesde_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If Not txtMesDesde.Visible Then
      GoTo h_EXIT
   End If
   txtMesDesde.Text = gTexto.fLimpiaStringdeBlancosAAmbosLados(txtMesDesde.Text)
   If gConvert.ConvierteAInteger(txtMesDesde.Text) < 1 Or gConvert.ConvierteAInteger(txtMesDesde.Text) > 12 Then
      gMessage.Advertencia "Debe introducir un numero de 'Mes' válido"
      Cancel = True
   End If
   txtMesDesde.Text = gTexto.llenaConCaracterALaIzquierda(txtMesDesde.Text, "0", 2)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMesDesde_Validate", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtMesHasta_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtMesHasta
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMesHasta_GotFocus", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtMesHasta_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMesHasta_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtMesHasta_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If Not gAPI.esUnAsciiCodeDeInputNumerico(KeyAscii, False, False) Then
      KeyAscii = 0
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMesHasta_KeyPress", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeProveedor_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtNombreDeProveedor
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeProveedor_GotFocus", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeProveedor_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeProveedor_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeProveedor_Validate(Cancel As Boolean)
   Dim refCodigoProveedor As String
   Dim refNombreProveedor As String
   On Error GoTo h_ERROR
   If CmbCantidadAImprimir.Text <> gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
      If LenB(txtNombreDeProveedor.Text) = 0 Then
         txtNombreDeProveedor.Text = "*"
      End If
      If insConexionesSawAOS.fSelectAndSetValuesOfProveedorFromAOS(insProveedor, refCodigoProveedor, refNombreProveedor, txtNombreDeProveedor, "NombreProveedor") Then
         sAssignFieldsFromConnectionProveedor
      Else
         Cancel = True
         GoTo h_EXIT
      End If
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeProveedor_Validate", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAssignFieldsFromConnectionProveedor()
   On Error GoTo h_ERROR
    txtNombreDeProveedor.Text = insProveedor.GetNombreProveedor
    txtCodigoDeProveedor.Text = insProveedor.GetCodigoProveedor
    CmbTipoDePersona.Text = insProveedor.GetTipoDePersonaStr
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAssignFieldsFromConnectionProveedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAno_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtAno
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAno_GotFocus", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAno_KeyDown(KeyCode As Integer, Shift As Integer)
  On Error GoTo h_ERROR
  sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAno_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAno_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.EsUnCaracterAsciiValidoParaCampoTipoIntegerSoloPositivo(KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAno_KeyPress", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAno_Validate(Cancel As Boolean)
   Dim valFechaCompleta As Date
   On Error GoTo h_ERROR
   If Not txtAno.Visible Then
      GoTo h_EXIT
   End If
   If LenB(txtAno.Text) = 0 Then
      sShowMessageForRequiredFields "Año", txtAno
      Cancel = True
      GoTo h_EXIT
   ElseIf gTexto.DfLen(txtAno.Text) < 4 Then
      gMessage.Advertencia "Debe introducir un 'Año' de cuatro dígitos'"
      Cancel = True
      GoTo h_EXIT
   End If
   valFechaCompleta = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(txtMes.Text, txtAno.Text, False)
   If gDefgen.LaFechaEsMayorQueElLimiteDeIngresoDeDatos(valFechaCompleta, True, Generar) Then
      txtMes.Text = gTexto.llenaConCaracterALaIzquierda(Month(gDefgen.fFechaLimiteParaIngresoDeDatos), "0", 2)
      txtAno.Text = Year(gDefgen.fFechaLimiteParaIngresoDeDatos)
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAno_Validate", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtMes_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.EsUnCaracterAsciiValidoParaCampoTipoIntegerSoloPositivo(KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMes_KeyPress", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sMuestraRetMesualesXTipoPersona()
   Dim SqlDelReporte As String
   Dim varReporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsPagoRpt
   On Error GoTo h_ERROR
   If txtMes.Text = "" Then
      sShowMessageForRequiredFields "Mes", txtMes
      GoTo h_EXIT
   ElseIf txtAno.Text = "" Then
      sShowMessageForRequiredFields "Año", txtAno
       GoTo h_EXIT
   End If
   SqlDelReporte = insPagoSQL.fSQLDelReporteRetMenXTipoPersona(gProyCompaniaActual.GetConsecutivoCompania, txtMes.Text, txtAno.Text, CmbTipoDePersona.Text)
   Set varReporte = New ActiveReport
   If insPagoRpt.fConfigurarRetPagosMensualesPorTipoDePersona(varReporte, SqlDelReporte, CmbTipoDePersona.Text, txtMes.Text, txtAno.Text, gProyCompaniaActual.GetNombreCompaniaParaInformes(True), gUtilDate.getFechaDeHoy, gUtilDate.getHoraActualString) Then
      gUtilReports.sMostrarOImprimirReporte varReporte, 1, mDondeImprimir, "Retenciones Mensuales por Tipo de Persona"
   End If
   Set varReporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sMuestraRetMesualesXTipoPersona()", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeRetMenXTipoPersona()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Retenciones Mensuales X Tipo de Persona"
   If mInformeSeleccionado = 3 Then
      gEnumTablaRetencion.FillComboBoxWithTipodePersonaRetencion CmbTipoDePersona, Buscar
      CmbTipoDePersona.Width = 2175
   ElseIf mInformeSeleccionado = 2 Then
      gEnumTablaRetencion.FillComboBoxWithTipodePersonaRetencion CmbTipoDePersona
      CmbTipoDePersona.Width = 2175
   End If
   framePeriodoDeAplicacion.Visible = True
   frameTipoDePersona.Visible = True
   lblTipoDePersona.Visible = True
   CmbTipoDePersona.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeRetMenXTipoPersona", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sOcultaTodosLosCampos()
   On Error GoTo h_ERROR
   frameProveedor.Visible = False
   frameConceptoDeRetencion.Visible = False
   frameTipoDePersona.Visible = False
   frameTasaDeCambio.Visible = False
   frameFechas.Visible = False
   frameMoneda.Visible = False
   framePeriodoDeAplicacion.Visible = False
   chkAgruparPorProveedor.Visible = False
   lblTipoDeOperacionAExportar.Visible = False
   cmbTipoDeOperacionAExportar.Visible = False
   chkIncluirRegistrosMtoRetIvaCero.Visible = False
   frameQuincenas.Visible = False
   cmdExportar.Visible = False
   cmdImpresora.Visible = True
   frmRutaExportacion.Visible = False
   chkSobreEscribirArchivo.Visible = False
   frmPeriodo.Visible = False
   chkExcluirComprobantesSinNumero.Visible = False
   chkMostrarObservaciones.Visible = False
   chkIncluirMontosComprobantesAnulados.Visible = False
   frameLoteDetracciones.Visible = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sOCultaTodosLosCampos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeBorradorDePlanillaDeDeclaracionYPago()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Borrador de Planilla de Declaración y Pago"
   gEnumTablaRetencion.FillComboBoxWithTipodePersonaRetencion CmbTipoDePersona, BORRADOR
   CmbTipoDePersona.Width = 2175
   framePeriodoDeAplicacion.Visible = True
   frameTipoDePersona.Visible = True
   lblTipoDePersona.Visible = True
   CmbTipoDePersona.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeBorradorDePlanillaDeDeclaracionYPago", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sMuestraBorradorDePlanillaDeDeclaracionYPago()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim SqlDelReporte As String
   Dim diaJuego As Integer
   Dim mesJuego As Integer
   Dim valFechaInicioVigencia As Date
   On Error GoTo h_ERROR
   If txtMes.Text = "" Then
      sShowMessageForRequiredFields "Mes", txtMes
      GoTo h_EXIT
   ElseIf txtAno.Text = "" Then
      sShowMessageForRequiredFields "Año", txtAno
       GoTo h_EXIT
   End If
   Set reporte = New DDActiveReports2.ActiveReport
   valFechaInicioVigencia = insSearchConnectionComunAOS.fBuscaFechaDeInicioDeVigenciaTablaRetencion(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(txtMes.Text, txtAno.Text, False))
   SqlDelReporte = insPagoSQL.fGetQueryPagosPorTipoDePersonaMesAno(txtMes.Text, txtAno.Text, CmbTipoDePersona.Text, True, gEnumTablaRetencion.strTipodePersonaRetencionToNum(CmbTipoDePersona.Text), gProyParametros.GetCentimosEnPlanillaMensual, gProyCompaniaActual.GetConsecutivoCompania, valFechaInicioVigencia)

   sDiaYMesDeGananciasFortuitas txtMes.Text, txtAno.Text, CmbTipoDePersona.Text, diaJuego, mesJuego, insTablaRetencion

   If insRptPagoConfigurar.fConfigurarDatosDelBorradorDeLaPlanilla(reporte, txtMes.Text, txtAno.Text, CmbTipoDePersona.Text, SqlDelReporte) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Borrador de Planilla de Declaración y Pago"
   End If
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sMuestraBorradorDePlanillaDeDeclaracionYPago", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAjustaOpcionesSegunParametrosCompania()
   On Error GoTo h_ERROR
   If gProyParametrosCompania.GetUsaRetencion Then
      optInformeDePago(CM_OPT_RET_EN_PAGOS).Enabled = True
      optInformeDePago(CM_OPT_RET_POR_CONCEPTO).Enabled = True
      optInformeDePago(CM_OPT_RET_MENSUALES_POR_TIPOPERSONA).Enabled = True
      optInformeDePago(CM_OPT_BORRADOR_DE_PLANILLA_DE_DECLARACION_Y_PAGO).Enabled = True
      lblNota1.Visible = False
   Else
      optInformeDePago(CM_OPT_RET_EN_PAGOS).Enabled = False
      optInformeDePago(CM_OPT_RET_POR_CONCEPTO).Enabled = False
      optInformeDePago(CM_OPT_RET_MENSUALES_POR_TIPOPERSONA).Enabled = False
      optInformeDePago(CM_OPT_BORRADOR_DE_PLANILLA_DE_DECLARACION_Y_PAGO).Enabled = False
      lblNota1.Visible = True
   End If
   If gProyCompaniaActual.fPuedoUsarOpcionesDeContribuyenteEspecial Then
      optInformeDePago(CM_OPT_DECLARACION_INFORMATIVA).Enabled = True
      optInformeDePago(CM_OPT_COMPROBANTE_RESUMEN_IVA).Enabled = True
      optInformeDePago(CM_OPT_TIPO_LISTADO_RET_IVA).Enabled = True
      lblNota2.Visible = False
   Else
      optInformeDePago(CM_OPT_DECLARACION_INFORMATIVA).Enabled = False
      optInformeDePago(CM_OPT_COMPROBANTE_RESUMEN_IVA).Enabled = False
      optInformeDePago(CM_OPT_TIPO_LISTADO_RET_IVA).Enabled = False
      lblNota2.Visible = True
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAjustaOpcionesSegunParametrosCompania", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeDeclaracionInformativa()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Declaración Informativa"
   FillComboBoxWithQuincenaDeclaracionInformativa False
   framePeriodoDeAplicacion.Visible = False ' true
   lblTipoDeOperacionAExportar.Visible = True
   cmbTipoDeOperacionAExportar.Visible = True
   chkIncluirRegistrosMtoRetIvaCero.Visible = True
   frameQuincenas.Visible = False  'True
   frameFechas.Visible = True
   cmdExportar.Visible = True
   cmdImpresora.Visible = False
   sSugerirFechaDeclaracionInformativa
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeDeclaracionInformativa", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub FillComboBoxWithQuincenaDeclaracionInformativa(ByVal valIncluirTodoElMes As Boolean)
   Dim nCount As Integer
   On Error GoTo h_ERROR
   cmbQuincenaAGenerar.Clear
   nCount = 0
   cmbQuincenaAGenerar.Text = CM_QuincenaDeclaracionInformativaToString(CM_QDI_PrimeraQuincena)
   cmbQuincenaAGenerar.List(nCount) = CM_QuincenaDeclaracionInformativaToString(CM_QDI_PrimeraQuincena)
   nCount = nCount + 1
   cmbQuincenaAGenerar.List(nCount) = CM_QuincenaDeclaracionInformativaToString(CM_QDI_SegundaQuincena)
   If valIncluirTodoElMes Then
      nCount = nCount + 1
      cmbQuincenaAGenerar.List(nCount) = CM_QuincenaDeclaracionInformativaToString(CM_QDI_TodoElMes)
   End If
   cmbQuincenaAGenerar.Width = gAPI.sugestedWidthForComboBox(cmbQuincenaAGenerar)
   cmbQuincenaAGenerar.ListIndex = 0
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "FillComboBoxWithQuincenaDeclaracionInformativa", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function CM_QuincenaDeclaracionInformativaToString(ByVal valQuincenaADeclarar As Integer) As String
   On Error GoTo h_ERROR
   Select Case valQuincenaADeclarar
      Case CM_QDI_PrimeraQuincena: CM_QuincenaDeclaracionInformativaToString = "Primera Quincena"
      Case CM_QDI_SegundaQuincena: CM_QuincenaDeclaracionInformativaToString = "Segunda Quincena"
      Case CM_QDI_TodoElMes: CM_QuincenaDeclaracionInformativaToString = "Todo el Mes"
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "enumQuincenaDeclaracionInformativaToString", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub FillComboBoxWithTipoOperacionAExportar()
   Dim nCount As Integer
   On Error GoTo h_ERROR
   nCount = 0
   cmbTipoDeOperacionAExportar.Text = CM_TipoOperacionAExportarToString(CM_TOE_Compras)
   cmbTipoDeOperacionAExportar.List(nCount) = CM_TipoOperacionAExportarToString(CM_TOE_Compras)
   nCount = nCount + 1
   cmbTipoDeOperacionAExportar.List(nCount) = CM_TipoOperacionAExportarToString(CM_TOE_ComprasYVentas)
   cmbTipoDeOperacionAExportar.Width = gAPI.sugestedWidthForComboBox(cmbTipoDeOperacionAExportar)
   cmbTipoDeOperacionAExportar.ListIndex = 0
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "FillComboBoxWithTipoOperacionAExportar", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function CM_TipoOperacionAExportarToString(ByVal valTipoOperacionAExportar As Integer) As String
   On Error GoTo h_ERROR
   Select Case valTipoOperacionAExportar
      Case CM_TOE_Compras: CM_TipoOperacionAExportarToString = "Compras"
      Case CM_TOE_ComprasYVentas: CM_TipoOperacionAExportarToString = "Compras Y Ventas"
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "CM_TipoOperacionAExportarToString", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sMuestraDeclaracionInformativa()
   Dim FechaInicial As Date
   Dim FechaFinal As Date
   Dim SqlDelReporte As String
   On Error GoTo h_ERROR
   FechaInicial = txtFechaInicial.Value
   FechaFinal = txtFechaFinal.Value
   If fValidarFechaDeclaracionInformativa Then
      If mDondeImprimir = eDI_PANTALLA Then
         sMuestraElReporteDeLaDeclaracionInformativa FechaInicial, FechaFinal
      Else
         SqlDelReporte = insPagoSQL.fSQLDeLaDeclaracionInformativa(FechaInicial, FechaFinal, gProyCompaniaActual.getNumeroDeRif, txtAno.Text, txtMes.Text, False, cmbTipoDeOperacionAExportar.Text, gProyCompaniaActual.GetConsecutivoCompania, chkIncluirRegistrosMtoRetIvaCero.Value, insFactura, insRenglonFact, insPago, gUltimaTasaDeCambio, gMonedaLocalActual, gListLibrary, gProyParametrosCompania.GetUsaMonedaExtranjera, gProyParametrosCompania.GetUsaMultiplesAlicuotas)
         sGeneraDeclaracionInformativa SqlDelReporte, FechaInicial, FechaFinal
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sMuestraDeclaracionInformativa", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sGeneraDeclaracionInformativa(ByVal valSQL As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date)
   Dim lineasDeTexto
   Dim RS As ADODB.Recordset
   Dim nombreDelArchivo As String
   Dim mensaje As String
   Dim mCaracterDeSeparacion As String
   On Error GoTo h_ERROR
   nombreDelArchivo = fNombreDelArchivoDeLaDeclaracionInformativa
   mCaracterDeSeparacion = vbTab
   Set RS = New ADODB.Recordset
   If gDbUtil.fOpenRecordset(RS, valSQL, gDefDatabaseConexion) Then
      If RS.RecordCount > 0 Then
         RS.MoveFirst
         If fCreaElArchivoDeExportacion(nombreDelArchivo) Then
            lineasDeTexto = gDbUtil.fExportRsToStringWith(RS, mCaracterDeSeparacion, True, 2, False, False, -1, vbCrLf)
            If gUtilFile.fWriteTextInFile(nombreDelArchivo, lineasDeTexto, False) Then
               mensaje = "La Declaración Informativa de Retención del IVA, " & vbCrLf
               mensaje = mensaje & "ha sido exportada en el archivo: " & vbCrLf
               mensaje = mensaje & nombreDelArchivo
               gMessage.exito mensaje
            Else
               mensaje = "Se ha producido un error al generar el archivo." & vbCrLf
               mensaje = mensaje & "Por favor vuelva a intentarlo."
               gMessage.Advertencia mensaje
            End If
         End If
        gDbUtil.sCloseIfOpened RS
      Else
        gDbUtil.sCloseIfOpened RS
        mensaje = "No existen registros que declarar en el período seleccionado. Desea generar archivo " & _
        "para  declaración informativa sin movimientos en el periodo ?"
        If gMessage.YesNoMessage(mensaje, "No Se encontrarón Registros a Declarar...") Then
            sCreaElArchivoDeExportacionParaPeriodoSinMovimiento nombreDelArchivo
        End If
      End If
   End If
   gDbUtil.sDestroyRecordSet RS
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sGeneraDeclaracionInformativa", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fNombreDelArchivoDeLaDeclaracionInformativa() As String
   Dim nombreDelArchivo As String
   On Error GoTo h_ERROR
   nombreDelArchivo = "DI_"
   nombreDelArchivo = nombreDelArchivo & gProyCompaniaActual.GetCodigo & "-"
   nombreDelArchivo = nombreDelArchivo & gProyCompaniaActual.GetNombre & "_"
   nombreDelArchivo = nombreDelArchivo & "_" & gUtilDate.fYear(txtFechaFinal.Value) & gUtilDate.fMonth(txtFechaFinal.Value) & gUtilDate.fDay(txtFechaFinal.Value)
   nombreDelArchivo = gUtilReports.getOutputPath & "\" & nombreDelArchivo & ".txt"
h_EXIT:
   fNombreDelArchivoDeLaDeclaracionInformativa = nombreDelArchivo
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreDelArchivoDeLaDeclaracionInformativa", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreaElArchivoDeExportacion(ByVal valNombreDeArchivo As String) As Boolean
   On Error GoTo h_ERROR
   If gUtilFile.fExisteElArchivo(valNombreDeArchivo) Then
      gUtilFile.sBorraElArchivo valNombreDeArchivo
      fCreaElArchivoDeExportacion = gUtilFile.fCreaArchivoDeTexto(valNombreDeArchivo)
   Else
      If gUtilFile.fDfMkPath(gUtilFile.getPathName(valNombreDeArchivo), False) Then
         fCreaElArchivoDeExportacion = gUtilFile.fCreaArchivoDeTexto(valNombreDeArchivo)
      Else
         fCreaElArchivoDeExportacion = False
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSeCreaElArchivoDeExportacion", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sMuestraElReporteDeLaDeclaracionInformativa(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date)
   Dim reporte As DDActiveReports2.ActiveReport
   Dim SqlDelReporte As String
   On Error GoTo h_ERROR
   On Error GoTo h_ERROR
   Set reporte = New DDActiveReports2.ActiveReport
   SqlDelReporte = insPagoSQL.fSQLDeLaDeclaracionInformativa(valFechaInicial, valFechaFinal, gProyCompaniaActual.getNumeroDeRif, txtAno.Text, txtMes.Text, False, cmbTipoDeOperacionAExportar.Text, gProyCompaniaActual.GetConsecutivoCompania, chkIncluirRegistrosMtoRetIvaCero.Value, insFactura, insRenglonFact, insPago, gUltimaTasaDeCambio, gMonedaLocalActual, gListLibrary, gProyParametrosCompania.GetUsaMonedaExtranjera, gProyParametrosCompania.GetUsaMultiplesAlicuotas)
   If insRptPagoConfigurar.fConfigurarDatosDeDeclaracionInformativaPreliminar(reporte, SqlDelReporte, valFechaInicial, valFechaFinal) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, eDI_PANTALLA, "Declaración Informativa - Preliminar"
   End If
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sMuestraElReporteDeLaDeclaracionInformativa", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeComprobanteResumenIVA()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Comprobante Resumen IVA"
   FillComboBoxWithQuincenaDeclaracionInformativa True
   framePeriodoDeAplicacion.Visible = True
   frameProveedor.Visible = True
   If Not gProyParametros.GetUsarCodigoProveedorEnPantalla Then
      txtCodigoDeProveedor.Enabled = False
   End If
   frameQuincenas.Visible = True
   If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
      lblNombre.Visible = False
      txtCodigoDeProveedor.Visible = False
      txtNombreDeProveedor.Visible = False
   Else
      lblNombre.Visible = True
      txtCodigoDeProveedor.Visible = True
      txtNombreDeProveedor.Visible = True
   End If
   chkIncluirRegistrosMtoRetIvaCero.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeComprobanteResumenIVA", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sMuestraComprobanteResumenIVA()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim SqlDelReporte As String
   Dim mesAno As String
   Dim periodoDelResumen As String
   Dim valCantidadAImprimir As Boolean
   On Error GoTo h_ERROR
   If txtMes.Text = "" Then
      sShowMessageForRequiredFields "Mes", txtMes
      GoTo h_EXIT
   ElseIf txtAno.Text = "" Then
      sShowMessageForRequiredFields "Año", txtAno
       GoTo h_EXIT
   ElseIf CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      If txtNombreDeProveedor.Text = "" Then
         sShowMessageForRequiredFields "Nombre ", txtNombreDeProveedor
         GoTo h_EXIT
      End If
   End If
   If fHayNumeroDeRETIVAenBlanco Then
      fMensajeDeRETIVAEsBlanco
   End If
   valCantidadAImprimir = CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno)
   SqlDelReporte = insPagoSQL.fSQLDelComprobanteResumenIVA(txtCodigoDeProveedor.Text, cmbQuincenaAGenerar.Text, valCantidadAImprimir, gConvert.ConvertByteToBoolean(chkIncluirRegistrosMtoRetIvaCero.Value), txtAno.Text, txtMes.Text, gProyCompaniaActual.GetConsecutivoCompania, gUltimaTasaDeCambio)
   mesAno = gTexto.llenaConCaracterALaIzquierda(txtMes.Text, "0", 2) & " / " & txtAno.Text
   If gAPI.SelectedElementInComboBoxToString(cmbQuincenaAGenerar) = CM_QuincenaDeclaracionInformativaToString(CM_QDI_PrimeraQuincena) Then
      periodoDelResumen = CM_QuincenaDeclaracionInformativaToString(CM_QDI_PrimeraQuincena) & " " & mesAno
   ElseIf gAPI.SelectedElementInComboBoxToString(cmbQuincenaAGenerar) = CM_QuincenaDeclaracionInformativaToString(CM_QDI_SegundaQuincena) Then
      periodoDelResumen = CM_QuincenaDeclaracionInformativaToString(CM_QDI_SegundaQuincena) & " " & mesAno
   Else 'If gAPI.SelectedElementInComboBoxToString(cmbQuincenaAGenerar) = CM_QuincenaDeclaracionInformativaToString(CM_QDI_TodoElMes) Then
      periodoDelResumen = mesAno
   End If
   Set reporte = New DDActiveReports2.ActiveReport
   Set reporte = insRptPagoConfigurar.getSubReportComprobanteDeRetencionIVA(SqlDelReporte, True, periodoDelResumen, gProyParametrosCompania.GetNombreLogo, gProyParametrosCompania.GetNombreSello, gProyParametrosCompania.GetNombreFirma)
   reporte.Sections("GHTotalesRetencion").DataField = "CodigoProveedor"
   gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Comprobante Resumen " & gGlobalization.fPromptIVA
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
    Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sMuestraComprobanteResumenIVA", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fSQLConsultaNumerosDeComprobanteDeRETIVA() As String
   Dim SQL As String
   Dim fechaIni As Date
   Dim fechaFin As Date
   On Error GoTo h_ERROR
   If cmbQuincenaAGenerar.Text = CM_QuincenaDeclaracionInformativaToString(CM_QDI_PrimeraQuincena) Then
      fechaIni = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(txtMes.Text, txtAno.Text, True)
      fechaFin = gUtilDate.fColocaDiaEnFecha(15, gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(txtMes.Text, txtAno.Text, True))
   ElseIf cmbQuincenaAGenerar.Text = CM_QuincenaDeclaracionInformativaToString(CM_QDI_SegundaQuincena) Then
      fechaIni = gUtilDate.fColocaDiaEnFecha(15, gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(txtMes.Text, txtAno.Text, True))
      fechaFin = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(txtMes.Text, txtAno.Text, False)
   Else
      fechaIni = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(txtMes.Text, txtAno.Text, True)
      fechaFin = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(txtMes.Text, txtAno.Text, False)
   End If
   SQL = "SELECT "
   SQL = SQL & "cxP.NumeroComprobanteRetencion, "
   SQL = SQL & "cxP.PorcentajeRetencionAplicado, "
   SQL = SQL & "cxP.Fecha AS FechaDelDocOrigen, "
   SQL = SQL & "cxP.Numero, "
   SQL = SQL & "cxP.CodigoProveedor"
   SQL = SQL & " FROM Adm.Proveedor INNER JOIN cxP ON ("
   SQL = SQL & "Adm.Proveedor.CodigoProveedor = cxP.CodigoProveedor"
   SQL = SQL & ") AND (Adm.Proveedor.ConsecutivoCompania = cxP.ConsecutivoCompania)"
   SQL = SQL & " WHERE Adm.Proveedor.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("cxP.FechaAplicacionRetIVA", fechaIni, fechaFin)
   If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND Adm.Proveedor.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(Trim(txtCodigoDeProveedor.Text))
   End If
   SQL = SQL & " AND cxP.SeHizoLaRetencionIVA = " & gUtilSQL.fBooleanToSqlValue(True)
h_EXIT:
   fSQLConsultaNumerosDeComprobanteDeRETIVA = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLConsultaNumerosDeComprobanteDeRETIVA", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fHayNumeroDeRETIVAenBlanco() As Boolean
   Dim EsBlanco As Boolean
   Dim SqlConsultaNComprobanteIva As String
   On Error GoTo h_ERROR
   SqlConsultaNComprobanteIva = fSQLConsultaNumerosDeComprobanteDeRETIVA
   EsBlanco = fSiHayNumeroDeRETIVAesBlancoEnUnConsulta(SqlConsultaNComprobanteIva)
h_EXIT:
   fHayNumeroDeRETIVAenBlanco = EsBlanco
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fHayNumeroDeRETIVAenBlanco", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSiHayNumeroDeRETIVAesBlancoEnUnConsulta(ByVal valSQL As String) As Boolean
   Dim EsBlanco As Boolean
   Dim rsPago As ADODB.Recordset
   On Error GoTo h_ERROR
   Set rsPago = New ADODB.Recordset
   EsBlanco = False
   If gDbUtil.fOpenRecordset(rsPago, valSQL, gDefDatabaseConexion) Then
      If rsPago.RecordCount > 0 Then
         rsPago.MoveFirst
         While Not rsPago.EOF
            If (IsNull(rsPago("NumeroComprobanteRetencion").Value) Or rsPago("NumeroComprobanteRetencion").Value = 0) Then
               EsBlanco = True
            End If
            rsPago.MoveNext
         Wend
       End If
   End If
   gDbUtil.sDestroyRecordSet rsPago
h_EXIT:
   fSiHayNumeroDeRETIVAesBlancoEnUnConsulta = EsBlanco
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSiHayNumeroDeRETIVAesBlancoEnUnConsulta", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub fMensajeDeRETIVAEsBlanco()
   On Error GoTo h_ERROR
   gMessage.Advertencia "Hay un número de comprobante RET de IVA en el Informe que esta en blanco o no tiene."
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fMensajeDeRETIVAEsBlanco", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sDiaYMesDeGananciasFortuitas(ByRef valMes As Long, ByRef ValAno As Long, ByVal valTipoDePersonaStr As String, ByRef refDiaJuego As Integer, ByRef refMesJuego As Integer, ByRef insTablaRet As Object)
   Dim SQL As String
   Dim RS As ADODB.Recordset
   Dim codigoPlantilla As String
   Dim Fecha As Date
   On Error GoTo h_ERROR
   Set RS = New ADODB.Recordset
   refDiaJuego = 0
   refMesJuego = 0
   SQL = ""
   SQL = gUtilSQL.fSQLValueWithAnd(SQL, "descripcion", "*JUEGO*", False)
   SQL = gUtilSQL.fSQLValueWithAnd(SQL, "TipoDePersona", gConvert.enumerativoAChar(gEnumTablaRetencion.strTipodePersonaRetencionToNum(valTipoDePersonaStr, False)), False)
   SQL = "SELECT * FROM plantillaRET WHERE " & SQL
   RS.Open SQL, gDefDatabaseConexion, adOpenDynamic, adLockReadOnly
   If Not RS.EOF And Not RS.BOF Then
      RS.MoveFirst
      codigoPlantilla = RS("Codigo").Value
      insTablaRet.sClrRecord
      insTablaRet.SetTipoDePersonaStr valTipoDePersonaStr
      insTablaRet.SetSecuencialDePlantilla codigoPlantilla
'      If insTablaRet.fSearchSelectConnection(False) Then
         SQL = "SELECT retPago.Fecha"
         SQL = SQL & " FROM retPago INNER JOIN "
         SQL = SQL & "retDocumentoPagado ON "
         SQL = SQL & "retPago.NumeroComprobante = "
         SQL = SQL & "retDocumentoPagado.NumeroComprobante"
         SQL = SQL & " AND "
         SQL = SQL & "retPago.ConsecutivoCompania = "
         SQL = SQL & "retDocumentoPagado.ConsecutivoCompania"
         SQL = SQL & " WHERE "
         SQL = SQL & "retPago.MesAplicacion = "
         SQL = SQL & valMes & " AND "
         SQL = SQL & "retPago.AnoAplicacion = "
         SQL = SQL & ValAno & " AND "
         SQL = SQL & "retDocumentoPagado.CodigoRetencion"
         SQL = SQL & " ='GANANC'"
         SQL = SQL & " ORDER BY retPago.Fecha"
         Set RS = New ADODB.Recordset
         RS.Open SQL, gDefDatabaseConexion, adOpenDynamic, adLockReadOnly
         If Not RS.EOF And Not RS.BOF Then
            RS.MoveFirst
            Fecha = RS("Fecha").Value
            refDiaJuego = Day(Fecha)
            refMesJuego = Month(Fecha)
         End If
'      End If
   End If
   gDbUtil.sDestroyRecordSet RS
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sDiaYMesDeGananciasFortuitas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAjustaOpcionesSegunPais()
   On Error GoTo h_ERROR
      optInformeDePago(CM_OPT_LIBRO_RETENCIONES_RENTA).Visible = gGlobalization.fEsCodigoPeru
      optInformeDePago(6).Caption = "Comprobante Resumen de Retención " & gGlobalization.fPromptIVA & " ....."
      optInformeDePago(13).Caption = "Listado de Comprobantes de Retención " & gGlobalization.fPromptIVA & " ....."
      If gGlobalization.fEsCodigoPeru Then
         frameInformes.Height = 4345
         optInformeDePago(CM_OPT_GENERAR_ARCHIVO_PAGO_DETRACCIONES).Visible = True
         frmRutaExportacion.Left = 2700
      Else
         frameLoteDetracciones.Visible = False
      End If

h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAjustaOpcionesSegunPais", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeLibroDeRetencionesRenta()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Libro de Retenciones Renta"
   frameFechas.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeLibroDeRetencionesRenta", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sMuestraLibroDeRetencionesRenta()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim SqlDelReporte As String
   On Error GoTo h_ERROR
   Set reporte = New DDActiveReports2.ActiveReport
   If txtFechaFinal.Value < txtFechaInicial.Value Then
      txtFechaFinal.Value = txtFechaInicial.Value
   End If
   SqlDelReporte = insPagoSQL.fSqlLibroDeRetencionesRenta(gProyCompaniaActual.GetConsecutivoCompania, txtFechaInicial.Value, txtFechaFinal.Value, gMonedaLocalActual.GetHoyCodigoMoneda, gUltimaTasaDeCambio)
   If insPagoRpt.fConfigurarDatosDelLibroRetencionesRenta(reporte, SqlDelReporte, txtFechaInicial.Value, txtFechaFinal.Value, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False)) Then
        gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Libro de Retenciones de Impuesto a la Renta"
   End If
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sMuestraLibroDeRetencionesRenta", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sGenerarDeclaracionMensualXML(ByVal valMes As String, ByVal ValAno As String, ByVal valTipoDeclaracion As Integer, ByVal valPathFile As String, ByVal valPathSchema As String, ByVal valValidateFile As Boolean, ByVal valSobreescribirArchivo As Boolean, Optional ByVal valShowMessage As Boolean = True)
   Dim varIndice As Long
   Dim varIndiceDetalle As Long
   Dim varSQL As String
   Dim varValor As String
   Dim RstSeccion As ADODB.Recordset
   Dim insProgress As clsUtilProcessProgress
   Dim varPuedoCerrarElArchivo As Boolean
   Dim valSeGeneroDocumento As Boolean
   Dim varHayRegistros As Boolean
   Dim MensajeFacturasTruncadas As String
   Dim ContadorFacturasTruncadas As Integer
   Dim valFecha As Date
   On Error GoTo h_ERROR
   Set insProgress = New clsUtilProcessProgress
   valSeGeneroDocumento = True
   varPuedoCerrarElArchivo = True
   MensajeFacturasTruncadas = "Existen Documentos con Numeros de Factura Mayor al permitido por el Portal del Seniat(10 digitos), estas serán truncadas para generar el xml de retenciones. " & vbNewLine & vbNewLine & "    NRO FACTURA          RIF RETENIDO" & vbNewLine & vbNewLine
   Set RstSeccion = New ADODB.Recordset
   varHayRegistros = True
   varSQL = ""
   valFecha = insSearchConnectionComunAOS.fBuscaFechaDeInicioDeVigenciaTablaRetencion(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(txtMes.Text, txtAno.Text, False))
   varSQL = insPagoSQL.fGetQueryPagosDeclracionMensualXML(valMes, ValAno, True, valTipoDeclaracion, False, gProyCompaniaActual.GetConsecutivoCompania, gProyCompaniaActual.getNumeroDeRif, gProyParametrosCompania.GetTomarEnCuentaRetencionesCeroParaARCVyRA, valFecha)
   insUtilXml.sStarDocument "UTF-8", "RelacionRetencionesISLR"
   insUtilXml.sWriteAttribute "", "", "RifAgente", gProyCompaniaActual.getNumeroDeRif, False
   If valTipoDeclaracion = 9 Then
        insUtilXml.sWriteAttribute "", "", "Fecha", "01/" & gTexto.llenaConCaracterALaIzquierda(valMes, "0", 2) & "/" & ValAno, False
   Else
        insUtilXml.sWriteAttribute "", "", "Periodo", ValAno & gTexto.llenaConCaracterALaIzquierda(valMes, "0", 2), False
   End If
   If gDbUtil.fOpenRecordset(RstSeccion, varSQL, gDefDatabaseConexion) Then
      If gDbUtil.fRecordCount(RstSeccion) <= 0 Then
         If valShowMessage Then
            gMessage.Advertencia "No Existen registros"
         End If
         varHayRegistros = False
         varSQL = insPagoSQL.fGetQueryPagosDeclracionMensualXML(valMes, ValAno, False, valTipoDeclaracion, False, gProyCompaniaActual.GetConsecutivoCompania, gProyCompaniaActual.getNumeroDeRif, gProyParametrosCompania.GetTomarEnCuentaRetencionesCeroParaARCVyRA, valFecha)
         gDbUtil.sCloseIfOpened RstSeccion
         If gDbUtil.fOpenRecordset(RstSeccion, varSQL, gDefDatabaseConexion) Then
         End If
      End If
      If varHayRegistros Then
         If insProgress.fInitializeProgress("Creando Archivo XML de " & fTitulomensaje(valTipoDeclaracion), "", True, True, 20, True, gDefgen.getMainForm) Then
         End If
      End If
      varIndiceDetalle = 0
      ContadorFacturasTruncadas = 0
      While (Not RstSeccion.EOF) And (varPuedoCerrarElArchivo)
         If varHayRegistros Then
            insProgress.sAddStandardRsProgress
         End If
         insUtilXml.sWriteElement "RelacionRetencionesISLR", "0", "DetalleRetencion", ""
         For varIndice = 0 To RstSeccion.Fields.Count - 1
          Dim numeroFactura As String
          Dim RifRetenido As String
            If (IsNull(RstSeccion(varIndice).Value)) Then
               varPuedoCerrarElArchivo = False
               Exit For
            End If
            If RstSeccion(varIndice).Type = adCurrency Then
               varValor = gConvert.fNumToStringWithoutThousandsDelimiter(RstSeccion(varIndice).Value, False, 2, ".")
            Else
               varValor = RstSeccion(varIndice).Value
            End If
            If ContadorFacturasTruncadas < 10 Then
               If (RstSeccion.Fields(varIndice).Name = "NumeroFacturaSinTruncar") Then
                  numeroFactura = RstSeccion.Fields(varIndice).Value & " "
               End If
               If (RstSeccion.Fields(varIndice).Name = "RifRetenido") Then
                  RifRetenido = RstSeccion.Fields(varIndice).Value & " " & vbNewLine & vbNewLine
               End If
               If (RstSeccion.Fields(varIndice).Name = "ExedeLimite") Then
                  If (RstSeccion.Fields(varIndice).Value = "S") Then
                     ContadorFacturasTruncadas = ContadorFacturasTruncadas + 1
                     MensajeFacturasTruncadas = MensajeFacturasTruncadas & ContadorFacturasTruncadas & " ) " & numeroFactura & "   " & RifRetenido
                  End If
               End If
            End If
            If (Not (RstSeccion.Fields(varIndice).Name = "NumeroFacturaSinTruncar" Or RstSeccion.Fields(varIndice).Name = "ExedeLimite")) Then
               insUtilXml.sWriteElement "RelacionRetencionesISLR" + insUtilXml.GetDelimiter + "DetalleRetencion", "0" + insUtilXml.GetDelimiter + gConvert.fConvierteAString(varIndiceDetalle), RstSeccion.Fields(varIndice).Name, varValor
            End If
                    Next varIndice
         varIndiceDetalle = varIndiceDetalle + 1
         RstSeccion.MoveNext
      Wend
      If ContadorFacturasTruncadas > 0 Then
      MensajeFacturasTruncadas = MensajeFacturasTruncadas + " ..."
      MsgBox MensajeFacturasTruncadas
      End If
      If varHayRegistros Then
         If insProgress.fDestroyProgress Then
         End If
      End If
      If (varPuedoCerrarElArchivo) Then
         If insUtilXml.fEndDocument(valPathFile, valSobreescribirArchivo) Then
            valSeGeneroDocumento = True
         End If
      End If
      sMandaMensajeFinalProcesoGeneracionDeclaracion valValidateFile, valPathFile, valPathSchema, valSeGeneroDocumento, varHayRegistros, valShowMessage
   End If
   gDbUtil.sDestroyRecordSet RstSeccion
   Set insProgress = Nothing
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sGenerarDeclaracionMensualXML", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sMandaMensajeFinalProcesoGeneracionDeclaracion(ByVal valValidateFile As Boolean, ByVal valPathFile As String, ByVal valPathSchema As String, ByVal valSeGeneroDocumento As Boolean, ByVal valHayRegistros As Boolean, ByVal valShowMessage As Boolean)
   Dim varElArchivoEsValido As Boolean
   On Error GoTo h_ERROR
   varElArchivoEsValido = True
   If valHayRegistros Then
      If (gUtilFile.fExisteElArchivo(valPathFile)) Then
         If valValidateFile Then
            If Not insUtilXml.fValidateXML(valPathFile, valPathSchema, "Resultados validación archivo xml", gWorkPaths.fAppPath()) Then
               varElArchivoEsValido = False
            End If
         End If
      Else
         varElArchivoEsValido = False
         gMessage.AlertMessage "Debido a Inconsistencia de los Datos, no se puede crear el archivo", "Declaración Mensual"
      End If
      If varElArchivoEsValido And valSeGeneroDocumento And valHayRegistros And valShowMessage Then
         gMessage.InformationMessage "El archivo " & valPathFile & " se generó correctamente.", "Declaración Mensual"
      End If
   ElseIf Not valHayRegistros Then
   End If
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sMandaMensajeFinalProcesoGeneracionDeclaracion", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fTitulomensaje(ByVal vTipoDeclaracion As Integer) As String
   Dim valTipoDeclaracion As String
   On Error GoTo h_ERROR
   Select Case vTipoDeclaracion
      Case CM_OPT_TIPO_SALARIOS_OTROS: valTipoDeclaracion = "Salarios y Otros"
      Case CM_OPT_TIPO_DIVIDENDOS_ACCIONES: valTipoDeclaracion = "Dividendos y Acciones"
   End Select
   fTitulomensaje = valTipoDeclaracion
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Generar", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sMuestraSalariosYOtros()
   Dim rptReporte As DDActiveReports2.ActiveReport
   Dim SQL As String
   Dim Mes As Integer
   Dim ano As Long
   Dim valFecha As Date
   Dim vFechaIniciodeVigenciaTablaRetencion  As Date
   On Error GoTo h_ERROR
   If txtMes.Text = "" Then
      sShowMessageForRequiredFields "Mes", txtMes
      GoTo h_EXIT
   ElseIf txtAno.Text = "" Then
      sShowMessageForRequiredFields "Año", txtAno
       GoTo h_EXIT
   End If
   Mes = gConvert.ConvierteAInteger(txtMes.Text)
   ano = gConvert.ConvierteAInteger(txtAno.Text)
   valFecha = gUtilDate.fColocaUltimoDiaDelMes("01/" & Mes & "/" & ano)
   vFechaIniciodeVigenciaTablaRetencion = insSearchConnectionComunAOS.fBuscaFechaDeInicioDeVigenciaTablaRetencion(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(txtMes.Text, txtAno.Text, False))

   SQL = insPagoSQL.fGetQueryPagosDeclracionMensualXML(Mes, ano, True, mInformeSeleccionado, True, gProyCompaniaActual.GetConsecutivoCompania, gProyCompaniaActual.getNumeroDeRif, gProyParametrosCompania.GetTomarEnCuentaRetencionesCeroParaARCVyRA, vFechaIniciodeVigenciaTablaRetencion)
   Set rptReporte = New DDActiveReports2.ActiveReport
   If insPagoRpt.fConfigurarDatosDelReporteDeSalariosYOtros(rptReporte, Mes, ano, SQL, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gProyCompaniaActual.getNumeroDeRif, gProyCompaniaActual.GetNombre) Then
      gUtilReports.sMostrarOImprimirReporte rptReporte, 1, mDondeImprimir, "Declaracion Mensual - Salarios y Otras Retenciones"
   End If
   Set rptReporte = Nothing
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
    Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sMuestraPagosEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sMuestraDividendosYAcciones()
   Dim rptReporte As DDActiveReports2.ActiveReport
   Dim SQL As String
   Dim Mes As Integer
   Dim ano As Long
   Dim valFecha As Date
   Dim vFechaIniciodeVigenciaTablaRetencion  As Date
   On Error GoTo h_ERROR
   If txtMes.Text = "" Then
      sShowMessageForRequiredFields "Mes", txtMes
      GoTo h_EXIT
   ElseIf txtAno.Text = "" Then
      sShowMessageForRequiredFields "Año", txtAno
       GoTo h_EXIT
   End If
   Mes = gConvert.ConvierteAInteger(txtMes.Text)
   ano = gConvert.ConvierteAInteger(txtAno.Text)
   valFecha = gUtilDate.fColocaUltimoDiaDelMes("01/" & Mes & "/" & ano)
   vFechaIniciodeVigenciaTablaRetencion = insSearchConnectionComunAOS.fBuscaFechaDeInicioDeVigenciaTablaRetencion(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(txtMes.Text, txtAno.Text, False))
   SQL = insPagoSQL.fGetQueryPagosDeclracionMensualXML(Mes, ano, True, mInformeSeleccionado, True, gProyCompaniaActual.GetConsecutivoCompania, gProyCompaniaActual.getNumeroDeRif, gProyParametrosCompania.GetTomarEnCuentaRetencionesCeroParaARCVyRA, vFechaIniciodeVigenciaTablaRetencion)
   Set rptReporte = New DDActiveReports2.ActiveReport
   If insPagoRpt.fConfigurarDatosDelReporteDeDividendosYAcciones(rptReporte, Mes, ano, SQL, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gProyCompaniaActual.getNumeroDeRif, gProyCompaniaActual.GetNombre) Then
      gUtilReports.sMostrarOImprimirReporte rptReporte, 1, mDondeImprimir, "Declaracion Mensual - Dividendo y Acciones"
   End If
   Set rptReporte = Nothing
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
    Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sMuestraDividendosYAcciones", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sMuestraInformeGanaciasFortuitas()
   Dim rptReporte As DDActiveReports2.ActiveReport
   Dim SqlDelReporte As String
   On Error GoTo h_ERROR
   If txtMes.Text = "" Then
      sShowMessageForRequiredFields "Mes", txtMes
      GoTo h_EXIT
   ElseIf txtAno.Text = "" Then
      sShowMessageForRequiredFields "Año", txtAno
      GoTo h_EXIT
   End If
   SqlDelReporte = insPagoSQL.fSQLGananciasFortuitas(txtMes.Text, txtAno, gProyCompaniaActual.GetConsecutivoCompania)
   Set rptReporte = New DDActiveReports2.ActiveReport
   If insPagoRpt.fConfigurarDatosDelReporteDeGananciasFortuitas(rptReporte, txtMes, txtAno, SqlDelReporte, gProyCompaniaActual.GetNombreCompaniaParaInformes(False)) Then
      gUtilReports.sMostrarOImprimirReporte rptReporte, 1, eDI_PANTALLA, "Declaracion Mensual - Ganancias Fortuitas Distintas a Loterías"
   End If
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
    Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sMuestraInformeGanaciasFortuitas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fBuscaRutaDestino() As String
   Dim vNombreCarpetaRaiz As String
   On Error GoTo h_ERROR
   Select Case mInformeSeleccionado
      Case CM_OPT_TIPO_RELACION_INFORMATIVA_SALARIO
         vNombreCarpetaRaiz = "\RI"
      Case CM_OPT_GENERAR_ARCHIVO_PAGO_DETRACCIONES
         vNombreCarpetaRaiz = "\TXTDetracciones"
      Case Else
         vNombreCarpetaRaiz = "\XMLAdministrativo"
   End Select
   fBuscaRutaDestino = gDefgen.fDataPathUser & vNombreCarpetaRaiz
   gUtilFile.fDfMkPath fBuscaRutaDestino & "\", False
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fBuscaRutaDestino", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelArchivoXML() As String
   Dim nombreDelArchivo As String
   Dim valOutputPath As String
   On Error GoTo h_ERROR
   If LenB(txtNombreDelArchivo.Text) = 0 Then
      nombreDelArchivo = fBuscaRutaDestino
   End If
   valOutputPath = fBuscaRutaDestino
   Select Case mInformeSeleccionado
      Case CM_OPT_TIPO_RELACION_INFORMATIVA_SALARIO
      txtNombreDelArchivo.Text = valOutputPath & "\SawISLR..."
      nombreDelArchivo = valOutputPath & "\SawISLR" & gConvert.fConvierteAString(mAno) & gConvert.fConvierteAString(gTexto.llenaConCaracterALaIzquierda(mMes, "0", 2)) & fNombreDeclaracion & insLibWincont.fToXmlValidStrValue(gTexto.DfReplace(gTexto.fCleanTextOfInvalidChars(gProyCompaniaActual.GetNombre, ".,/"), " ", "")) & ".xml"
      Case Else
      nombreDelArchivo = valOutputPath & "\SawISLR" & gConvert.fConvierteAString(mAno) & gConvert.fConvierteAString(gTexto.llenaConCaracterALaIzquierda(mMes, "0", 2)) & fNombreDeclaracion & insLibWincont.fToXmlValidStrValue(gTexto.DfReplace(gTexto.fCleanTextOfInvalidChars(gProyCompaniaActual.GetNombre, ".,/"), " ", "")) & ".xml"
      txtNombreDelArchivo.Text = nombreDelArchivo
   End Select
h_EXIT:
   fNombreDelArchivoXML = nombreDelArchivo
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreDelArchivoXML", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fPathSchema() As String
   Dim vNombreEsquema As String
   Dim vPathSchema As String
   On Error GoTo h_ERROR
   Select Case mInformeSeleccionado
      Case CM_OPT_TIPO_DIVIDENDOS_ACCIONES
         vNombreEsquema = "DeclaracionDividendosyAcciones.xsd"
      Case CM_OPT_TIPO_SALARIOS_OTROS
         vNombreEsquema = "DeclaracionSalariosOtrasRetenciones.xsd"
      Case Else
         vNombreEsquema = "DeclaracionSalariosOtrasRetenciones.xsd"
      End Select
 If (gWorkPaths.fEjecutandoDesdeElIDE) Then
      vPathSchema = gWorkPaths.fPathDeLasTablasComunes(gDefProg.GetSiglasDelPrograma, gDefProg.GetPais)
      vPathSchema = gTexto.DfReplace(vPathSchema, "Tablas", "Esquemas")
      vPathSchema = gUtilFile.fAddSlashCharToEndOfPathIfRequired(vPathSchema) & vNombreEsquema
   Else
      vPathSchema = gWorkPaths.GetProgramDir & "Esquemas\" & vNombreEsquema
   End If
   fPathSchema = vPathSchema
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fPathSchema", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDeclaracion() As String
   Dim vDeclaracion As String
   On Error GoTo h_ERROR
   Select Case mInformeSeleccionado
      Case CM_OPT_TIPO_SALARIOS_OTROS: vDeclaracion = "SalariosOtros"
      Case CM_OPT_TIPO_DIVIDENDOS_ACCIONES: vDeclaracion = "DividendoAcciones"
   End Select
   fNombreDeclaracion = vDeclaracion
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreDeclaracion", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sInitLookAndFeel(ByVal valMostrarPantallaXML As Boolean)
   Dim fechaInicialStr As String
   Dim fechaInicialDate As Date
   On Error GoTo h_ERROR
   If valMostrarPantallaXML Then
      fechaInicialStr = "01/" & gProyParametrosCompania.GetMesDelCierreFiscal & "/"
      If gDefgen.GetEsUnProgramaEnDemostracion Then
         fechaInicialStr = fechaInicialStr & gConvert.fConvierteAString(Year(gDefgen.fFechaLimiteParaIngresoDeDatos))
      Else
         fechaInicialStr = fechaInicialStr & gConvert.fConvierteAString(Year(gUtilDate.getFechaDeHoy))
      End If
      If Not gUtilDate.fElStringContieneUnaFechaValida(fechaInicialStr, False) Then
         If gDefgen.GetEsUnProgramaEnDemostracion Then
            fechaInicialStr = gDefgen.fFechaLimiteParaIngresoDeDatos
         Else
            fechaInicialStr = gUtilDate.getFechaDeHoy
         End If
      End If
      fechaInicialDate = gConvert.fConvertStringToDate(fechaInicialStr, True)
      fechaInicialDate = DateAdd("yyyy", -1, fechaInicialDate)
      fechaInicialDate = DateAdd("m", 1, fechaInicialDate)
      mFechaInicial = fechaInicialDate
      txtMesDesde.Text = gTexto.llenaConCaracterALaIzquierda(Month(mFechaInicial), "0", 2)
      txtAnoDesde.Text = gConvert.fConvierteAString(Year(mFechaInicial))
      sActualizaFechaFinal
      gAPI.ssSetFocus optInformeDePago(CM_OPT_TIPO_SALARIOS_OTROS)
      frameTipoDeclaracion.Visible = True
      frameInformes.Visible = False
      cmdGenerar.Visible = True
      cmdImpresora.Visible = False
      lblNota2.Visible = False
      txtNombreDelArchivo.Text = mRutaDelArchivo
      txtMes.Text = gTexto.llenaConCaracterALaIzquierda(Month(gUtilDate.getFechaDeHoy), "0", 2)
      txtAno.Text = Year(gUtilDate.getFechaDeHoy)
   Else
      gAPI.ssSetFocus optInformeDePago(CM_OPT_PAGOS_ENTRE_FECHAS)
      frameInformes.Visible = True
      frameTipoDeclaracion.Visible = False
      cmdImpresora.Visible = True
      cmdGenerar.Visible = False
      lblNota2.Visible = True
      frameLoteDetracciones.Visible = False
   End If
   gAPI.ssSetFocus optInformeDePago(CM_OPT_PAGOS_ENTRE_FECHAS)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitLookAndFeel", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeDeclaracionXmlSueldosYOtros()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Declaración Mensual XML - Sueldos y Otras Retenciones"
   framePeriodoDeAplicacion.Visible = True
   frmRutaExportacion.Visible = True
   cmdGenerar.Visible = True
   cmdGrabar.Visible = True
   chkSobreEscribirArchivo.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeDeclaracionXmlSueldosYOtros", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeDeclaracionXmlDividendoAcciones()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Declaración Mensual XML - Dividendos y Acciones"
   framePeriodoDeAplicacion.Visible = True
   cmdGenerar.Visible = True
   cmdGrabar.Visible = True
   frmRutaExportacion.Visible = True
   chkSobreEscribirArchivo.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeDeclaracionXmlDividendoAcciones", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeRelcionInformativa()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Relación Infomátiva XML - Sueldos y Otras Retenciones"
   cmdGenerar.Visible = True
   frmRutaExportacion.Visible = True
   frmPeriodo.Visible = True
   cmdGrabar.Visible = False
   chkSobreEscribirArchivo.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeRelcionInformativa", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeDeclaracionXmlGanaciaFortuita()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Declaración Mensual XML - Ganancias Fortuitas"
   framePeriodoDeAplicacion.Visible = True
   cmdGenerar.Visible = False
   cmdGrabar.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeDeclaracionXmlGanaciaFortuita", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function CM_OPT_TIPO_LISTADO_RET_IVA() As Integer
   CM_OPT_TIPO_LISTADO_RET_IVA = 13
End Function

Private Sub sActivarCamposDeListadoRetencionIva()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Listado de Retención de IVA  "
   frameFechas.Visible = True
   frameProveedor.Visible = True
   chkExcluirComprobantesSinNumero.Visible = True
   If Not gProyParametros.GetUsarCodigoProveedorEnPantalla Then
      txtCodigoDeProveedor.Enabled = False
   End If
   If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
      lblNombre.Visible = False
      txtCodigoDeProveedor.Visible = False
      txtNombreDeProveedor.Visible = False
   Else
      lblNombre.Visible = True
      txtCodigoDeProveedor.Visible = True
      txtNombreDeProveedor.Visible = True
   End If
   chkIncluirRegistrosMtoRetIvaCero.Visible = True
   chkIncluirMontosComprobantesAnulados.Visible = True
   cmdGenerar.Visible = False
   If Not gProyParametrosCompania.GetEnDondeRetenerIVAAsEnum = eDS_CXP Then
      chkExcluirComprobantesSinNumero.Enabled = False
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeDeclaracionXmlGanaciaFortuita", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sMuestraListadoRetencionIva()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim SqlDelReporte As String
   Dim valCantidadAImprimir As Boolean
   Dim unaPaginaPorProveedor As Boolean
   Dim ReporteEnMonedaLocal As Boolean
   On Error GoTo h_ERROR
    If gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      If txtNombreDeProveedor.Text = "" Then
         sShowMessageForRequiredFields "Nombre", txtNombreDeProveedor
         GoTo h_EXIT
      End If
   End If
   If txtFechaFinal.Value < txtFechaInicial.Value Then
      txtFechaFinal.Value = txtFechaInicial.Value
   End If
   If gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
      unaPaginaPorProveedor = gAPI.fGetCheckBoxValue(chkCambiandodePagina)
   End If
   ReporteEnMonedaLocal = gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal)
   valCantidadAImprimir = CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno)
   SqlDelReporte = insPagoSQL.fSQLDelListadoRetencionIVA(txtCodigoDeProveedor.Text, valCantidadAImprimir, gConvert.ConvertByteToBoolean(chkIncluirRegistrosMtoRetIvaCero.Value), txtFechaInicial.Value, txtFechaFinal.Value, gProyCompaniaActual.GetConsecutivoCompania, gUltimaTasaDeCambio, chkExcluirComprobantesSinNumero.Value, chkIncluirMontosComprobantesAnulados.Value)
   Set reporte = New DDActiveReports2.ActiveReport
   Set reporte = insRptPagoConfigurar.getSubReportListadoDeRetencionIVA(SqlDelReporte, False, "Del " & gConvert.dateToString(txtFechaInicial.Value) & " al " & gConvert.dateToString(txtFechaFinal.Value))
   gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Listado de Retenciones de IVA"
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
    Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sMuestraListadoRetencionIva", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sActualizaFechaFinal()
   Dim FechaFinal As Date

   Dim fechaStr As String
   Dim fechaDate As Date
   On Error GoTo h_ERROR
   If Len(txtMesDesde.Text) > 0 And Len(txtAnoDesde.Text) > 0 Then
      fechaStr = "01/" & txtMesDesde.Text & "/" & txtAnoDesde.Text
      If gUtilDate.fElStringContieneUnaFechaValida(fechaStr, False) Then
         fechaDate = gConvert.fConvertStringToDate(fechaStr, True)
         fechaDate = gUtilDate.LastDayOfTheMonthAsDate(fechaDate)
         If gDefgen.LaFechaEsMayorQueElLimiteDeIngresoDeDatos(fechaDate, True, insertar) Then
            txtMesDesde.Text = gTexto.llenaConCaracterALaIzquierda(Month(gDefgen.fFechaLimiteParaIngresoDeDatos), "0", 2)
            txtAnoDesde.Text = Year(gDefgen.fFechaLimiteParaIngresoDeDatos)
            fechaStr = "01/" & txtMesDesde.Text & "/" & txtAnoDesde.Text
            fechaDate = gConvert.fConvertStringToDate(fechaStr, True)
         Else
         End If
      Else
         txtMesDesde.Text = gTexto.llenaConCaracterALaIzquierda(Month(mFechaInicial), "0", 2)
         txtAnoDesde.Text = gConvert.fConvierteAString(Year(mFechaInicial))
         fechaStr = "01/" & txtMesDesde.Text & "/" & txtAnoDesde.Text
         fechaDate = gConvert.fConvertStringToDate(fechaStr, True)
      End If
      fechaDate = DateAdd("m", 11, fechaDate)
      txtMesHasta = gConvert.MesAString2Digitos(fechaDate)
      txtAnoHasta.Text = gConvert.fConvierteAString(Year(fechaDate))
      FechaFinal = gUtilDate.fColocaUltimoDiaDelMes(gConvert.fConvertStringToDate("01/" & txtMesHasta.Text & "/" & txtAnoHasta.Text, True))
      mFechaFinal = FechaFinal
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActualizaFechaFinal", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sGenerarArchivoXmlRet()
   Dim mensaje As String
   On Error GoTo h_ERROR
   If gUtilFile.fExisteElFolder(mRutaDelArchivo) Then
      If mInformeSeleccionado <> CM_OPT_TIPO_RELACION_INFORMATIVA_SALARIO Then
         mMes = txtMes.Text
         mAno = txtAno.Text
         sGenerarDeclaracionMensualXML mMes, mAno, mInformeSeleccionado, fNombreDelArchivoXML, fPathSchema, True, chkSobreEscribirArchivo.Value
      Else
         sGenerarXMLRelacionInformativa
      End If
   Else
      mensaje = "La ruta destino no ha podido ser accesada." & vbCr
      mensaje = mensaje & "Verifique que exista e intentelo nuevamente."
      gMessage.Warning mensaje
   End If
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
    Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sGenerarArchivoXmlRet", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sGenerarXMLRelacionInformativa()
   Dim FechaEnCurso As Date
   Dim FechaInicial As Date
   Dim FechaFinal As Date
   Dim mensaje As String
   On Error GoTo h_ERROR
   If fMandaMensajeParaGenerarRI Then
      FechaInicial = gConvert.fConvertStringToDate("01/" & txtMesDesde.Text & "/" & txtAnoDesde.Text)
      FechaFinal = gConvert.fConvertStringToDate("01/" & txtMesHasta.Text & "/" & txtAnoHasta.Text)
      FechaEnCurso = gUtilDate.fColocaUltimoDiaDelMes(FechaInicial)
      FechaFinal = gUtilDate.fColocaUltimoDiaDelMes(FechaFinal)
      While FechaEnCurso <= FechaFinal
         mMes = Month(FechaEnCurso)
         mAno = Year(FechaEnCurso)
         sGenerarDeclaracionMensualXML gTexto.llenaConCaracterALaIzquierda(mMes, "0", 2), mAno, CM_OPT_TIPO_SALARIOS_OTROS, fNombreDelArchivoXML, fPathSchema, True, chkSobreEscribirArchivo.Value, False
         FechaEnCurso = gUtilDate.SumaNmeses(FechaEnCurso, 1, True)
      Wend
      mensaje = "Se generaron correctamente los archivos de declaración XML" & vbCrLf
      mensaje = mensaje & "para el periodo:" & vbCrLf
      mensaje = mensaje & "Desde " & FechaInicial & " al " & FechaFinal
      gMessage.exito mensaje
   End If
h_EXIT: On Error GoTo 0
    Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sGenerarXMLRelacionInformativa", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fMandaMensajeParaGenerarRI() As Boolean
   Dim fechaDesde As Date
   Dim FechaHasta As Date
   On Error GoTo h_ERROR
   fMandaMensajeParaGenerarRI = False
   fechaDesde = gConvert.fConvertStringToDate("01/" & txtMesDesde.Text & "/" & txtAnoDesde.Text)
   FechaHasta = gConvert.fConvertStringToDate("01/" & txtMesHasta.Text & "/" & txtAnoHasta.Text)
   FechaHasta = gUtilDate.fColocaUltimoDiaDelMes(FechaHasta)
'   FechaHasta = gUtilDate.SumaNmeses(fechaDesde, 11, True)
   If Not gDefgen.LaFechaEsMayorQueElLimiteDeIngresoDeDatos(FechaHasta, True, Generar) Then
      If gMessage.Confirm("¿Desea Generar la Relación Informátiva para el periodo " & fechaDesde & " al " & FechaHasta & " ?.") Then
         fMandaMensajeParaGenerarRI = True
      Else
         fMandaMensajeParaGenerarRI = False
      End If
   Else
      fMandaMensajeParaGenerarRI = False
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fMandaMensajeParaGenerarRI", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sActivarCamposDeGenerarArchivoPagoDetracciones()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Generar Archivo de Pago de Detracciones"
   framePeriodoDeAplicacion.Visible = True
   frameLoteDetracciones.Visible = True
   cmdGenerar.Visible = True
   cmdGenerar.Visible = True
   cmdImpresora.Visible = False
   cmdExportar.Visible = False
   cmdGrabar.Visible = False
   chkSobreEscribirArchivo.Visible = True
   txtNombreDelArchivo.Text = fBuscaRutaDestino & "\D" & gProyCompaniaActual.getNumeroDeRif & txtNumeroDeLote.Text & ".txt"
   frmRutaExportacion.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeDeclaracionXmlGanaciaFortuita", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sGenerarArchivoPagoDetracciones()
   Dim vSobreescribirArchivo As Boolean
   On Error GoTo h_ERROR
   If txtNumeroDeLote.Text = "" Then
      sShowMessageForRequiredFields "Número de lote", txtNumeroDeLote
      GoTo h_EXIT
   End If
   If txtMes.Text = "" Then
      sShowMessageForRequiredFields "Mes", txtMes
      GoTo h_EXIT
   End If
   If txtAno.Text = "" Then
      sShowMessageForRequiredFields "Año", txtAno
      GoTo h_EXIT
   End If
   vSobreescribirArchivo = gConvert.ConvertByteToBoolean(chkSobreEscribirArchivo.Value)
   insCxP.sGeneraArchivoDePagoDeDetracciones txtMes.Text, txtAno.Text, gUtilFile.fDirectorioDe(txtNombreDelArchivo.Text), txtNumeroDeLote.Text, vSobreescribirArchivo
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sMuestraDeclaracionInformativa", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNumeroDeLote_Validate(Cancel As Boolean)
On Error GoTo h_ERROR
   If gTexto.DfLen(txtNumeroDeLote.Text) < 6 Then
      txtNumeroDeLote.Text = gTexto.llenaConCaracterALaIzquierda(txtNumeroDeLote.Text, "0", 6)
   End If
   If gTexto.DfLen(txtNombreDelArchivo) > 0 Or txtNombreDelArchivo.Text <> "" Then
      txtNombreDelArchivo.Text = gUtilFile.fDirectorioDe(txtNombreDelArchivo) & "\D" & gProyCompaniaActual.getNumeroDeRif & txtNumeroDeLote.Text & ".txt"
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sMuestraDeclaracionInformativa", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLoadObjectValues(ByVal valCompaniaActual As Object, _
                           ByVal valProyParametrosCompania As Object, _
                           ByVal valInsTablaRetencion As Object, _
                           ByVal valInsProveedor As Object, _
                           ByVal valInsPagoSQL As Object, _
                           ByVal valInsPagoRpt As Object, _
                           ByVal valInsLibWincont As Object, _
                           ByVal valInsComprobante As Object, _
                           ByVal valInsRptPagoConfigurar As Object, _
                           ByVal valInsSearchConnectionComunAOS As Object, _
                           ByVal valConexionesSawAOS As Object, _
                           ByVal valInsFactura As Object, _
                           ByVal valInsRenglonFact As Object, _
                           ByVal valInsPago As Object, _
                           ByVal valInsCxP As Object, _
                           ByVal valInsUtilXml As Object)
   On Error GoTo h_ERROR
   Set gProyCompaniaActual = valCompaniaActual
   Set gProyParametrosCompania = valProyParametrosCompania
   Set insTablaRetencion = valInsTablaRetencion
   Set insProveedor = valInsProveedor
   Set insPagoSQL = valInsPagoSQL
   Set insPagoRpt = valInsPagoRpt
   Set insLibWincont = valInsLibWincont
   Set insComprobante = valInsComprobante
   Set insRptPagoConfigurar = valInsRptPagoConfigurar
   Set insSearchConnectionComunAOS = valInsSearchConnectionComunAOS
   Set insConexionesSawAOS = valConexionesSawAOS
   Set insFactura = valInsFactura
   Set insRenglonFact = valInsRenglonFact
   Set insPago = valInsPago
   Set insCxP = valInsCxP
   Set insUtilXml = valInsUtilXml

   sInitDefaultValues

h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLoadObjectValues", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fValidarFechaDeclaracionInformativa() As Boolean
Dim vResult As Boolean
On Error GoTo h_ERROR
   vResult = (gUtilDate.fDiasDeDistanciaEntre2Fechas(txtFechaFinal.Value, txtFechaInicial.Value) <= 31)
   If Not vResult Then
      gMessage.AlertMessage "Las Fechas de Declaración Inicial y Final no deben ser mayores a 31 días", ""
   End If
   fValidarFechaDeclaracionInformativa = vResult
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fValidarFechaDeclaracionInformativa", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sSugerirFechaDeclaracionInformativa()
Dim vResult As Boolean
On Error GoTo h_ERROR
   If Not (mYaAjustoFechaDeclInfor) Then
      txtFechaInicial.Value = gUtilDate.SumaNdias(gUtilDate.getFechaDeHoy, -8)
      txtFechaFinal.Value = gUtilDate.SumaNdias(gUtilDate.getFechaDeHoy, -1)
      mYaAjustoFechaDeclInfor = True
   End If
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fValidarFechaDeclaracionInformativa", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCreaElArchivoDeExportacionParaPeriodoSinMovimiento(ByVal valNombreDelArchivo As String)
Dim vResult As Boolean
Dim valSQL As String
Dim lineasDeTexto As String
Dim mensaje As String
Dim RS As ADODB.Recordset
Dim mCaracterDeSeparacion As String
On Error GoTo h_ERROR
    mCaracterDeSeparacion = vbTab
    Set RS = New ADODB.Recordset
    valSQL = insPagoSQL.fConstruirSqlParaPeriodoSinMovimiento(txtFechaFinal.Value, gProyCompaniaActual.GetConsecutivoCompania, gProyCompaniaActual.getNumeroDeRif)
    If fCreaElArchivoDeExportacion(valNombreDelArchivo) Then
        If gDbUtil.fOpenRecordset(RS, valSQL, gDefDatabaseConexion) Then
            lineasDeTexto = gDbUtil.fExportRsToStringWith(RS, mCaracterDeSeparacion, True, 2, False, False, -1, vbCrLf)
            If gUtilFile.fWriteTextInFile(valNombreDelArchivo, lineasDeTexto, False) Then
                gMessage.exito "Proceso de Exportación a: " & valNombreDelArchivo & " para Declaración Informativa Fue Concluida Satisfactoriamente...."
            Else
                mensaje = "Se ha producido un error al generar el archivo." & vbCrLf
                mensaje = mensaje & "Por favor vuelva a intentarlo."
                gMessage.Advertencia mensaje
            End If
        End If
    End If
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCreaElArchivoDeExportacionParaPeriodoSinMovimiento", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
