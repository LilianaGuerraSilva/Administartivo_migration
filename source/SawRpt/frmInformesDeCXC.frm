VERSION 5.00
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomct2.ocx"
Begin VB.Form frmInformesDeCXC 
   BackColor       =   &H00F3F3F3&
   Caption         =   "Informes de Cuentas por Cobrar"
   ClientHeight    =   6540
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   10995
   LinkTopic       =   "Form1"
   ScaleHeight     =   6540
   ScaleWidth      =   10995
   Begin VB.Frame frameFiltroZona 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Caption         =   "Frame1"
      Height          =   600
      Left            =   3360
      TabIndex        =   90
      Top             =   4320
      Visible         =   0   'False
      Width           =   8000
      Begin VB.ComboBox cmbFiltroZona 
         Enabled         =   0   'False
         Height          =   315
         Left            =   2520
         TabIndex        =   93
         Top             =   240
         Width           =   1935
      End
      Begin VB.CheckBox chkFiltroZona 
         Caption         =   "Check1"
         Height          =   255
         Left            =   2160
         TabIndex        =   91
         Top             =   240
         Width           =   255
      End
      Begin VB.Label lblFiltroZona 
         BackColor       =   &H00F3F3F3&
         Caption         =   "Zona de Cobranza"
         Height          =   255
         Left            =   2520
         TabIndex        =   94
         Top             =   0
         Width           =   1935
      End
      Begin VB.Label lblFiltrarPorZona 
         BackColor       =   &H00F3F3F3&
         Caption         =   "Filtrar por Zona de Cobranza"
         ForeColor       =   &H00A84439&
         Height          =   255
         Left            =   0
         TabIndex        =   92
         Top             =   240
         Width           =   2055
      End
   End
   Begin VB.Frame frameContacto 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Caption         =   "Frame1"
      Height          =   375
      Left            =   3360
      TabIndex        =   87
      Top             =   3720
      Width           =   1995
      Begin VB.CheckBox ChkContacto 
         Caption         =   "Check1"
         Height          =   255
         Left            =   1440
         TabIndex        =   88
         Top             =   120
         Width           =   255
      End
      Begin VB.Label lblContacto 
         BackColor       =   &H00F3F3F3&
         Caption         =   "Campo Contacto"
         ForeColor       =   &H00A84439&
         Height          =   255
         Left            =   0
         TabIndex        =   89
         Top             =   120
         Width           =   1335
      End
   End
   Begin VB.Frame frameAgruparInformeCXC 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Agrupado por"
      Height          =   1455
      Left            =   3240
      TabIndex        =   77
      Top             =   960
      Width           =   5900
      Begin VB.Frame frameOptTipoAgrupar 
         BackColor       =   &H00F3F3F3&
         BorderStyle     =   0  'None
         Height          =   1100
         Left            =   1680
         TabIndex        =   84
         Top             =   240
         Width           =   1700
         Begin VB.OptionButton optTipoAgrupar 
            BackColor       =   &H00F3F3F3&
            Caption         =   "Sector de Negocio"
            Height          =   255
            Index           =   0
            Left            =   0
            TabIndex        =   86
            Top             =   240
            Width           =   1815
         End
         Begin VB.OptionButton optTipoAgrupar 
            BackColor       =   &H00F3F3F3&
            Caption         =   "Zona Cobranza"
            Height          =   255
            Index           =   1
            Left            =   0
            TabIndex        =   85
            Top             =   480
            Width           =   1815
         End
      End
      Begin VB.ComboBox cmbAgruparZona 
         Height          =   315
         Left            =   3600
         TabIndex        =   83
         Top             =   600
         Visible         =   0   'False
         Width           =   1935
      End
      Begin VB.ComboBox cmbAgruparSectorDeNegocio 
         Height          =   315
         Left            =   3600
         TabIndex        =   81
         Top             =   600
         Width           =   1935
      End
      Begin VB.OptionButton OptAgruparInforme 
         BackColor       =   &H00F3F3F3&
         Caption         =   "Agrupar por..."
         Height          =   255
         Index           =   1
         Left            =   240
         TabIndex        =   79
         Top             =   840
         Width           =   1300
      End
      Begin VB.OptionButton OptAgruparInforme 
         BackColor       =   &H00F3F3F3&
         Caption         =   "No Agrupar"
         Height          =   195
         Index           =   0
         Left            =   240
         TabIndex        =   78
         Top             =   360
         Width           =   1200
      End
      Begin VB.Label lblAgruparZona 
         BackColor       =   &H00F3F3F3&
         Caption         =   "Zona de Cobranza"
         Height          =   255
         Left            =   3600
         TabIndex        =   82
         Top             =   360
         Visible         =   0   'False
         Width           =   1935
      End
      Begin VB.Label lblAgruparSectordeNegocio 
         BackColor       =   &H00F3F3F3&
         Caption         =   "Agrupar por..."
         Height          =   255
         Left            =   3600
         TabIndex        =   80
         Top             =   360
         Width           =   1935
      End
      Begin VB.Line Line1 
         X1              =   1560
         X2              =   1560
         Y1              =   240
         Y2              =   1320
      End
   End
   Begin VB.CheckBox chkMostrarFechaUltimoPago 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Fecha Ultimo pago "
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   6120
      TabIndex        =   76
      Top             =   3840
      Width           =   1935
   End
   Begin VB.Frame frameTipoDocumento 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   4200
      TabIndex        =   74
      Top             =   1920
      Width           =   6075
      Begin VB.ComboBox cmbTipodocumento 
         Height          =   315
         Left            =   1560
         TabIndex        =   23
         Top             =   0
         Width           =   2055
      End
      Begin VB.Label LblTipoDeDocumento 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Tipo de Documento"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   60
         TabIndex        =   75
         Top             =   60
         Width           =   1410
      End
   End
   Begin VB.CheckBox ChkAgruparPorTipo 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Agrupar por Tipo de Documento"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   3240
      TabIndex        =   21
      Top             =   1560
      Width           =   2775
   End
   Begin VB.CheckBox chkIncluirFacturasHistoricas 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Incluir Facturas Históricas......"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   3360
      TabIndex        =   14
      Top             =   720
      Visible         =   0   'False
      Width           =   2415
   End
   Begin VB.CheckBox chkAgruparPorVendedor 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Agrupar por Vendedor"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   3300
      TabIndex        =   34
      Top             =   3960
      Width           =   2115
   End
   Begin VB.CheckBox chkTotalizarPorMes 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Totalizar por Mes"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   3300
      TabIndex        =   33
      Top             =   3720
      Width           =   2115
   End
   Begin VB.Frame frameOrdenadoPor 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   5400
      TabIndex        =   72
      Top             =   3120
      Width           =   3435
      Begin VB.ComboBox cmbOrdenadoPor 
         Height          =   315
         Left            =   1560
         TabIndex        =   30
         Top             =   0
         Width           =   1575
      End
      Begin VB.Label lblOrdenadoPor 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Ordenado Por"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   60
         TabIndex        =   73
         Top             =   60
         Width           =   990
      End
   End
   Begin VB.CheckBox chkTotalizaPorCliente 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Totalizar por Cliente"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   6000
      TabIndex        =   35
      Top             =   3525
      Width           =   1935
   End
   Begin VB.Frame frameCantidadAImprimir 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Caption         =   "Cantidad a Imprimir"
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   3300
      TabIndex        =   43
      Top             =   660
      Width           =   4395
      Begin VB.ComboBox CmbCantidadAImprimir 
         Height          =   315
         Left            =   1455
         TabIndex        =   15
         Top             =   0
         Width           =   975
      End
      Begin VB.Label lblCantidadAImprimir 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Cantidad a Imprimir"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   0
         TabIndex        =   50
         Top             =   60
         Width           =   1335
      End
   End
   Begin VB.Frame frameStatus 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Caption         =   "Status de la Cuenta por Cobrar"
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   3300
      TabIndex        =   57
      Top             =   660
      Width           =   4395
      Begin VB.ComboBox cmbStatus 
         Height          =   315
         HelpContextID   =   1
         Left            =   2295
         TabIndex        =   18
         Top             =   0
         Width           =   1815
      End
      Begin VB.Label lblStatus 
         AutoSize        =   -1  'True
         BackColor       =   &H80000016&
         BackStyle       =   0  'Transparent
         Caption         =   "Status de la Cuenta por Cobrar"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   0
         TabIndex        =   58
         Top             =   60
         Width           =   2175
      End
   End
   Begin VB.Frame frameInformes 
      BackColor       =   &H00F3F3F3&
      ForeColor       =   &H80000010&
      Height          =   5775
      Left            =   120
      TabIndex        =   64
      Top             =   0
      Width           =   3075
      Begin VB.OptionButton optInformeDeCxC 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "CxC por Cliente.............................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   14
         Left            =   120
         TabIndex        =   95
         Top             =   2040
         Width           =   2775
      End
      Begin VB.OptionButton optInformeDeCxC 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Facturas sin CxC ..........................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   12
         Left            =   120
         TabIndex        =   13
         Top             =   5370
         Width           =   2775
      End
      Begin VB.OptionButton optInformeDeCxC 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Análisis  de Vencto. entre Fechas ."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   11
         Left            =   120
         TabIndex        =   6
         Top             =   2895
         Width           =   2775
      End
      Begin VB.OptionButton optInformeDeCxC 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Análisis de Vencto. a una Fecha ..."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   10
         Left            =   120
         TabIndex        =   9
         Top             =   3840
         Width           =   2775
      End
      Begin VB.OptionButton optInformeDeCxC 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Fact. Anuladas vs. CxC Vigentes .."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   9
         Left            =   120
         TabIndex        =   12
         Top             =   5055
         Width           =   2775
      End
      Begin VB.OptionButton optInformeDeCxC 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "CxC Pendientes entre Fechas  ....."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   7
         Left            =   120
         TabIndex        =   2
         Top             =   1130
         Width           =   2775
      End
      Begin VB.OptionButton optInformeDeCxC 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Análisis  de Vencimiento ..............."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   5
         Top             =   2580
         Width           =   2775
      End
      Begin VB.OptionButton optInformeDeCxC 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "CxC por Vendedor ........................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   120
         TabIndex        =   0
         Top             =   500
         Width           =   2775
      End
      Begin VB.OptionButton optInformeDeCxC 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Análisis CxC histórico ...................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   2
         Left            =   120
         TabIndex        =   7
         Top             =   3210
         Width           =   2775
      End
      Begin VB.OptionButton optInformeDeCxC 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Histórico de cliente ......................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   3
         Left            =   120
         TabIndex        =   8
         Top             =   3525
         Width           =   2775
      End
      Begin VB.OptionButton optInformeDeCxC 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "CxC entre Fechas ........................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   4
         Left            =   120
         TabIndex        =   1
         Top             =   815
         Width           =   2775
      End
      Begin VB.OptionButton optInformeDeCxC 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Estadístico de Vendedores .........."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   5
         Left            =   120
         TabIndex        =   10
         Top             =   4440
         Width           =   2775
      End
      Begin VB.OptionButton optInformeDeCxC 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Cobranzas por Vendedor .............."
         CausesValidation=   0   'False
         Enabled         =   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   6
         Left            =   120
         TabIndex        =   11
         Top             =   4740
         Visible         =   0   'False
         Width           =   2775
      End
      Begin VB.OptionButton optInformeDeCxC 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "CxC con Descripción ...................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   8
         Left            =   120
         TabIndex        =   3
         Top             =   1445
         Width           =   2775
      End
      Begin VB.OptionButton optInformeDeCxC 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Cheques Devueltos ......................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   13
         Left            =   120
         TabIndex        =   4
         Top             =   1760
         Width           =   2775
      End
      Begin VB.Label lblINformesEstadisticos 
         BackColor       =   &H00A86602&
         Caption         =   " Informes Estadísticos"
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
         Left            =   120
         TabIndex        =   67
         Top             =   4095
         Width           =   2775
      End
      Begin VB.Label lblInformesDeCxC 
         BackColor       =   &H00A86602&
         Caption         =   " Informes de CxC"
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
         Left            =   120
         TabIndex        =   66
         Top             =   160
         Width           =   2775
      End
      Begin VB.Label lblVarios 
         BackColor       =   &H00A86602&
         Caption         =   " Informes Varios de CxC"
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
         Left            =   120
         TabIndex        =   65
         Top             =   2265
         Width           =   2775
      End
   End
   Begin VB.Frame frameMoneda 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Caption         =   "Moneda en el Informe"
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   7200
      TabIndex        =   48
      Top             =   2460
      Width           =   3615
      Begin VB.ComboBox cmbMonedaDeLosReportes 
         Height          =   315
         Left            =   1665
         TabIndex        =   29
         Text            =   "En Moneda Original"
         Top             =   0
         Width           =   1935
      End
      Begin VB.Label lblMonedaDeLosReportes 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Moneda en el Informe"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   0
         TabIndex        =   49
         Top             =   60
         Width           =   1545
      End
   End
   Begin VB.Frame frameExportar 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Exportar a XLS"
      ForeColor       =   &H00808080&
      Height          =   675
      Left            =   4335
      TabIndex        =   45
      Top             =   5700
      Width           =   4935
      Begin VB.TextBox txtNombreArchivo 
         Height          =   285
         Left            =   1275
         TabIndex        =   38
         Top             =   285
         Width           =   3495
      End
      Begin VB.CommandButton cmdExportar 
         Caption         =   "&Exportar"
         Height          =   375
         Left            =   120
         TabIndex        =   42
         Top             =   240
         Width           =   1035
      End
   End
   Begin VB.CommandButton cmdImpresora 
      Caption         =   "&Impresora"
      Height          =   375
      Left            =   120
      TabIndex        =   39
      Top             =   5940
      Width           =   1215
   End
   Begin VB.CommandButton cmdPantalla 
      Caption         =   "&Pantalla"
      Height          =   375
      Left            =   1560
      TabIndex        =   40
      Top             =   5940
      Width           =   1215
   End
   Begin VB.CommandButton cmdSalir 
      Caption         =   "&Salir"
      CausesValidation=   0   'False
      Height          =   375
      Left            =   3000
      TabIndex        =   41
      Top             =   5940
      Width           =   1215
   End
   Begin VB.Frame frameFechas 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Fechas"
      ForeColor       =   &H00808080&
      Height          =   1035
      Left            =   3300
      TabIndex        =   44
      Top             =   2430
      Width           =   2055
      Begin MSComCtl2.DTPicker dtpFechaFinal 
         Height          =   315
         Left            =   720
         TabIndex        =   25
         Top             =   615
         Width           =   1275
         _ExtentX        =   2249
         _ExtentY        =   556
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   111476739
         CurrentDate     =   36978
      End
      Begin MSComCtl2.DTPicker dtpFechaInicial 
         Height          =   315
         Left            =   720
         TabIndex        =   24
         Top             =   240
         Width           =   1275
         _ExtentX        =   2249
         _ExtentY        =   556
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   111476739
         CurrentDate     =   36978
      End
      Begin VB.Label lblFechaFinal 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Final"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   47
         Top             =   675
         Width           =   330
      End
      Begin VB.Label lblFechaInicial 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Inicial"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   46
         Top             =   300
         Width           =   405
      End
   End
   Begin VB.CheckBox chkMontoTotalYRestanteConIva 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Montos con Iva"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   3300
      TabIndex        =   31
      Top             =   3525
      Width           =   1575
   End
   Begin VB.Frame frameZonaCobranza 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Zona de Cobranza"
      ForeColor       =   &H00808080&
      Height          =   615
      Left            =   3300
      TabIndex        =   55
      Top             =   1740
      Width           =   6675
      Begin VB.ComboBox CmbZonaCobranza 
         Height          =   315
         Left            =   675
         TabIndex        =   22
         Top             =   240
         Width           =   2295
      End
      Begin VB.Label lblZonaCobranza 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Zona"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   56
         Top             =   300
         Width           =   375
      End
   End
   Begin VB.CheckBox ChkCambiandodePagina 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Una página por cliente"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   3300
      TabIndex        =   32
      Top             =   3525
      Width           =   2115
   End
   Begin VB.Frame frameCamposDefinibles 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Caption         =   "Campos Definibles de Factura"
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   3300
      TabIndex        =   62
      Top             =   4980
      Width           =   6855
      Begin VB.ComboBox cmbCamposDefinibles 
         Height          =   315
         Left            =   2940
         TabIndex        =   37
         Text            =   "Ninguno"
         Top             =   0
         Width           =   2055
      End
      Begin VB.Label lblCamposDefinibles 
         AutoSize        =   -1  'True
         BackColor       =   &H80000016&
         BackStyle       =   0  'Transparent
         Caption         =   "Campos Definibles de Factura a Mostrar"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   0
         TabIndex        =   63
         Top             =   60
         Width           =   2820
      End
   End
   Begin VB.Frame frameVendedor 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Vendedor"
      ForeColor       =   &H00808080&
      Height          =   555
      Left            =   3300
      TabIndex        =   53
      Top             =   1035
      Width           =   6135
      Begin VB.TextBox txtNombreVendedor 
         Height          =   285
         Left            =   1890
         TabIndex        =   20
         Top             =   195
         Width           =   4155
      End
      Begin VB.TextBox txtCodigoVendedor 
         Height          =   285
         Left            =   735
         TabIndex        =   19
         Top             =   195
         Width           =   1095
      End
      Begin VB.Label lblCodigoVendedor 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Nombre"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   120
         TabIndex        =   54
         Top             =   240
         Width           =   555
      End
   End
   Begin VB.Frame frameCliente 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Cliente"
      ForeColor       =   &H00808080&
      Height          =   555
      Left            =   3300
      TabIndex        =   51
      Top             =   1035
      Width           =   6135
      Begin VB.TextBox txtCodigoDeCliente 
         Height          =   285
         Left            =   735
         TabIndex        =   16
         Top             =   195
         Width           =   1095
      End
      Begin VB.TextBox txtNombreDeCliente 
         Height          =   285
         Left            =   1890
         TabIndex        =   17
         Top             =   195
         Width           =   4215
      End
      Begin VB.Label lblNombreDeCliente 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Nombre"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   120
         TabIndex        =   52
         Top             =   240
         Width           =   555
      End
   End
   Begin VB.Frame frameAnalisisDeVencimientoPor 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Caption         =   "Orden del Análisis por Vencimiento"
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   3300
      TabIndex        =   59
      Top             =   4980
      Width           =   6315
      Begin VB.ComboBox cmbAnalisisDeVencimientoPor 
         Height          =   315
         Left            =   2565
         TabIndex        =   36
         Top             =   0
         Width           =   1815
      End
      Begin VB.Label lblAnalisisDeVencimientoPor 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "de cliente"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   5100
         TabIndex        =   71
         Top             =   60
         Width           =   690
      End
      Begin VB.Label lblAnalisisDeVencimientoPor 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Orden del Análisis por Vencimiento"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   0
         TabIndex        =   60
         Top             =   60
         Width           =   2445
      End
   End
   Begin VB.Frame frameTasaDeCambio 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Tasa de cambio"
      ForeColor       =   &H00808080&
      Height          =   915
      Left            =   5460
      TabIndex        =   68
      Top             =   2430
      Width           =   1635
      Begin VB.OptionButton optTasaDeCambio 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Del día"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   180
         TabIndex        =   69
         Top             =   615
         Width           =   1275
      End
      Begin VB.OptionButton optTasaDeCambio 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Original"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   180
         TabIndex        =   26
         Top             =   300
         Width           =   1275
      End
   End
   Begin VB.Frame frameInforme 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Informe"
      ForeColor       =   &H00808080&
      Height          =   915
      Left            =   5460
      TabIndex        =   70
      Top             =   2430
      Width           =   1635
      Begin VB.OptionButton optDetallado 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Detallado"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   27
         Top             =   300
         Width           =   1275
      End
      Begin VB.OptionButton optResumido 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Resumido"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   28
         Top             =   615
         Width           =   1275
      End
   End
   Begin VB.Label lblDatosDelReporte 
      AutoSize        =   -1  'True
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "lblDatosDelReporte"
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
      Left            =   3360
      TabIndex        =   61
      Top             =   180
      Width           =   2385
   End
End
Attribute VB_Name = "frmInformesDeCXC"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private mInformeSeleccionado As Integer
Private mTipoAgrupar As String
Private mCodigoCliente As String
Private mUsarCambioOriginal As Boolean
Private mDondeImprimir As enum_DondeImprimir
Private insVendedor As Object
Private insCliente As Object
Private gProyCompaniaActual As Object
Private insAdmPropAnalisisVenc As Object
Private insCamposDefFactura As Object
Dim xls As Object
Dim insFactura As Object
Dim insConfigurar  As Object

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "frmInformesDeCXC"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Informes De CXC"
End Function

Private Function OPT_ANALISIS_DE_VENCIMIENTO() As Integer
   OPT_ANALISIS_DE_VENCIMIENTO = 0
End Function

Private Function OPT_CXC_POR_VENDEDOR() As Integer
   OPT_CXC_POR_VENDEDOR = 1
End Function

Private Function OPT_ANALISIS_CxC_HISTORICO() As Integer
   OPT_ANALISIS_CxC_HISTORICO = 2
End Function

Private Function OPT_HISTORICO_DE_CLIENTE() As Integer
   OPT_HISTORICO_DE_CLIENTE = 3
End Function

Private Function OPT_CXC_ENTRE_FECHAS() As Integer
   OPT_CXC_ENTRE_FECHAS = 4
End Function

Private Function OPT_ESTADISTICO_DE_VENDEDORES() As Integer
   OPT_ESTADISTICO_DE_VENDEDORES = 5
End Function

Private Function OPT_CXC_PENDIENTES_ENTRE_FECHAS() As Integer
   OPT_CXC_PENDIENTES_ENTRE_FECHAS = 7
End Function

Private Function OPT_CXC_CON_DESCRIPCION() As Integer
   OPT_CXC_CON_DESCRIPCION = 8
End Function

Private Function OPT_FACTURAS_ANULADAS_VS_CXC_VIGENTES() As Integer
   OPT_FACTURAS_ANULADAS_VS_CXC_VIGENTES = 9
End Function

Private Function OPT_ANALISIS_DE_VENCIMIENTO_A_UNA_FECHA() As Integer
   OPT_ANALISIS_DE_VENCIMIENTO_A_UNA_FECHA = 10
End Function

Private Function OPT_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS() As Integer
   OPT_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS = 11
End Function

Private Function OPT_FACTURAS_SIN_CXC() As Integer
   OPT_FACTURAS_SIN_CXC = 12
End Function

Private Function OPT_CHEQUES_DEVUELTOS() As Integer
   OPT_CHEQUES_DEVUELTOS = 13
End Function

Private Function OPT_CXC_POR_CLIENTE() As Integer
   OPT_CXC_POR_CLIENTE = 14
End Function

Private Function OPT_NOAGRUPAR_INFORME() As Integer
   OPT_NOAGRUPAR_INFORME = 0
End Function
Private Function OPT_AGRUPARPOR_INFORME() As Integer
   OPT_AGRUPARPOR_INFORME = 1
End Function
Private Function OPT_TIPOAGRUPAR_SECTORDENEGOCIO() As Integer
   OPT_TIPOAGRUPAR_SECTORDENEGOCIO = 0
End Function
Private Function OPT_TIPOAGRUPAR_ZONACOBRANZA() As Integer
   OPT_TIPOAGRUPAR_ZONACOBRANZA = 1
End Function
Private Function OPT_TIPOAGRUPAR_SECTORYZONA() As Integer
   OPT_TIPOAGRUPAR_SECTORYZONA = 2
End Function
Private Function OPT_TIPOAGRUPAR_ZONAYSECTOR() As Integer
   OPT_TIPOAGRUPAR_ZONAYSECTOR = 3
End Function

Private Sub sCheckForSpecialKeys(ByVal valKeyCode As Integer, ByVal valShift As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, valKeyCode) Then
   End If
   Select Case valKeyCode
      Case vbKeyEscape
         Unload Me
      Case vbKeyF6
         gAPI.ssSetFocus cmdPantalla
         cmdPantalla_Click
      Case vbKeyF8
         gAPI.ssSetFocus cmdImpresora
         cmdImpresora_Click
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCheckForSpecialKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ChkAgruparPorTipo_Click()
   On Error GoTo h_ERROR
   If ChkAgruparPorTipo = vbChecked Then
      frameTipoDocumento.Visible = True
   Else
      frameTipoDocumento.Visible = False
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "ChkAgruparPorTipo_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub chkFiltroZona_Click()
   On Error GoTo h_ERROR
   cmbFiltroZona.Enabled = Not cmbFiltroZona.Enabled
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "chkFiltroZona_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbMonedaDeLosReportes_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbMonedaDeLosReportes_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdPantalla_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_PANTALLA
   sEjecutaElInformeSeleccionado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdPantalla_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdImpresora_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdImpresora_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdSalir_Click()
   On Error GoTo h_ERROR
   Unload Me
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdSalir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdPantalla_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdPantalla_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdSalir_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
  sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdSalir_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdImpresora_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_IMPRESORA
   sEjecutaElInformeSeleccionado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdImpresora_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sInitLookAndFeel()
   On Error GoTo h_ERROR
   Me.Caption = CM_MESSAGE_NAME
   optInformeDeCxC(OPT_FACTURAS_ANULADAS_VS_CXC_VIGENTES).Visible = gProyParametros.GetEsSistemaParaIG
   mInformeSeleccionado = 0
   sInitDefaultValues
   sInitDefaultValuesCasoSistemaInternoParaNivelPorUsuario

h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitLookAndFeel", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Form_Load()
   On Error GoTo h_ERROR
   Me.AutoRedraw = True
   Me.ZOrder 0
   Me.Width = 11115
   Me.Height = 7000
   If gDefgen.getMainForm.Width > Width Then
      Left = (gDefgen.getMainForm.Width - Width) / 4
      Top = (gDefgen.getMainForm.Height - Height) / 4
   Else
      Left = 0
      Top = 0
   End If
   mInformeSeleccionado = OPT_CXC_POR_VENDEDOR
 
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Form_Load", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Form_Unload(Cancel As Integer)
   Dim gFechasDeLosInformes As clsFechasDeLosInformesNav
   On Error GoTo h_ERROR
   Set insVendedor = Nothing
   Set insCliente = Nothing
   Set gFechasDeLosInformes = New clsFechasDeLosInformesNav
   gFechasDeLosInformes.sGrabasLasFechasDeInformes dtpFechaInicial.value, dtpFechaFinal.value, gProyUsuarioActual.GetNombreDelUsuario
   Set gFechasDeLosInformes = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Form_Unload", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub OptAgruparInforme_Click(Index As Integer)
On Error GoTo h_ERROR
   Select Case Index
      Case OPT_NOAGRUPAR_INFORME:             sDesactivarOptParaAgrupar
      Case OPT_AGRUPARPOR_INFORME:            sActivarOptParaAgrupar
    End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "OptAgruparInforme_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optTipoAgrupar_Click(Index As Integer)
  On Error GoTo h_ERROR
  Select Case Index
     Case OPT_TIPOAGRUPAR_SECTORDENEGOCIO:   sActivarCamposAgruparSectorDeNegocio
     Case OPT_TIPOAGRUPAR_ZONACOBRANZA:      sActivarCamposAgruparZonaCobranza
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optTipoAgrupar_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoDeCliente_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtCodigoDeCliente
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoDeCliente_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoDeCliente_KeyDown(KeyCode As Integer, Shift As Integer)
 On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoDeCliente_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoDeCliente_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If LenB(txtCodigoDeCliente.Text) = 0 Then
      txtCodigoDeCliente = "*"
   End If
   insCliente.sClrRecord
   insCliente.SetCodigo txtCodigoDeCliente.Text
   If insCliente.fSearchSelectConnection(True, False, False, 0, False, True) Then
      sSelectAndSetValuesOfCliente
   Else
      Cancel = True
      GoTo h_EXIT
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoDeCliente_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoVendedor_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If LenB(txtCodigoVendedor.Text) = 0 Then
      txtCodigoVendedor = "*"
   End If
   insVendedor.sClrRecord
   insVendedor.SetCodigo txtCodigoVendedor.Text
   If insVendedor.fSearchSelectConnection Then
      sSelectAndSetValuesOfVendedor
   Else
      Cancel = True
      GoTo h_EXIT
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoVendedor_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeCliente_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtNombreDeCliente
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeCliente_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeCliente_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeCliente_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeCliente_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If LenB(txtNombreDeCliente) = 0 Then
      txtNombreDeCliente = "*"
   End If
   insCliente.sClrRecord
   insCliente.SetNombre txtNombreDeCliente
   If insCliente.fSearchSelectConnection(True, False, False, 0, False, True) Then
      sSelectAndSetValuesOfCliente
   Else
      Cancel = True
      GoTo h_EXIT
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeCliente_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sSelectAndSetValuesOfCliente()
   On Error GoTo h_ERROR
   If insCliente.fRsRecordCount(False) = 1 Then
      sAssignFieldsFromConnectionCliente
   ElseIf insCliente.fRsRecordCount(False) > 1 Then
      If gProyParametros.GetUsaCodigoClienteEnPantalla Then
         insCliente.sShowListSelect "Codigo", txtCodigoDeCliente.Text
      Else
         insCliente.sShowListSelect "Nombre", txtNombreDeCliente.Text
      End If
      sAssignFieldsFromConnectionCliente
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sSelectAndSetValuesOfCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAssignFieldsFromConnectionCliente()
   On Error GoTo h_ERROR
    txtNombreDeCliente.Text = insCliente.GetNombre
    txtCodigoDeCliente.Text = insCliente.GetCodigo
    mCodigoCliente = insCliente.GetCodigo
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAssignFieldsFromConnectionCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optInformeDeCxC_Click(Index As Integer)
   On Error GoTo h_ERROR
   mInformeSeleccionado = Index
   sOcultaTodosLosCampos
   sInitCXCEntreFechas
   Select Case mInformeSeleccionado
      Case OPT_CXC_POR_VENDEDOR:             sActivarCamposDeCXCPorVendedor
      Case OPT_CXC_ENTRE_FECHAS:             sActivarCamposDeCXCEntreFechas
      Case OPT_CXC_PENDIENTES_ENTRE_FECHAS:  sActivarCamposDeCXCPendientesEntreFechas
      Case OPT_CXC_CON_DESCRIPCION:          sActivarCamposDeCxCConDescripcion
      Case OPT_ANALISIS_DE_VENCIMIENTO:      sActivarCamposDeAnalisisDeVencimiento
      Case OPT_ANALISIS_CxC_HISTORICO:       sActivarCamposDeAnalisisCXCHistorico
      Case OPT_HISTORICO_DE_CLIENTE:         sActivarCamposDeHisoricodeCliente
      Case OPT_ESTADISTICO_DE_VENDEDORES:    sActivarCamposDeEstadisticoDeVendedores
      Case OPT_FACTURAS_ANULADAS_VS_CXC_VIGENTES: sActivarCamposDeFacturasAnuladasVsCxCVigentes
      Case OPT_ANALISIS_DE_VENCIMIENTO_A_UNA_FECHA: sActivarCamposDeAnalisisDeVencimientoAUnaFecha
      Case OPT_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS: sActivarCamposDeAnalisisDeVencimientoEntreFechas
      Case OPT_FACTURAS_SIN_CXC: sActivarCamposDeFacturasSinCxC
      Case OPT_CHEQUES_DEVUELTOS: sActivarCamposDeChequesDevueltos
      Case OPT_CXC_POR_CLIENTE: sActivarCamposDeCxCPorCliente
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optInformeDeCxC_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optInformeDeCxC_KeyDown(Index As Integer, KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optInformeDeCxC_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optTasaDeCambio_Click(Index As Integer)
    On Error GoTo h_ERROR
    If Index = 0 Then
      mUsarCambioOriginal = True
    Else
      mUsarCambioOriginal = False
    End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optTasaDeCambio_Click()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optTasaDeCambio_KeyDown(Index As Integer, KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optTasaDeCambio_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub dtpFechaInicial_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "dtpFechaInicial_KeyDown()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub dtpFechaFinal_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "dtpFechaFinal_KeyDown()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub CmbZonaCobranza_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbZonaCobranza_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optDetallado_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optDetallado_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optResumido_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optResumido_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElInformeSeleccionado()
   On Error GoTo h_ERROR
   Select Case mInformeSeleccionado
      Case OPT_CXC_POR_VENDEDOR:                      sEjecutaCXCPorVendedor
      Case OPT_CXC_ENTRE_FECHAS:                      sEjecutaCXCEntreFechas
      Case OPT_CXC_PENDIENTES_ENTRE_FECHAS:           sEjecutaCXCPendientesEntreFechas
      Case OPT_ANALISIS_DE_VENCIMIENTO:               sEjecutaAnalisisDeVencimiento
      Case OPT_ANALISIS_CxC_HISTORICO:                sEjecutaAnalisisCxCHistorico
      Case OPT_HISTORICO_DE_CLIENTE:                  sEjecutaHistoricoDeCliente
      Case OPT_ESTADISTICO_DE_VENDEDORES:             sEjecutaEstadisticoDeVendedores
      Case OPT_CXC_CON_DESCRIPCION:                   sEjecutaCxCconDescripcion
      Case OPT_FACTURAS_ANULADAS_VS_CXC_VIGENTES:     sEjecutaFacturasAnuladasVsCxCVigentes
      Case OPT_ANALISIS_DE_VENCIMIENTO_A_UNA_FECHA:   sEjecutaAnalisisDeVencimientoAUnaFecha
      Case OPT_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHAS:  sEjecutaAnalisisDeVencimientoEntreFechas
      Case OPT_FACTURAS_SIN_CXC:                      sEjecutaFacturasSinCxC
      Case OPT_CHEQUES_DEVUELTOS:                     sEjecutaInformeDeChequesDevueltos
      Case OPT_CXC_POR_CLIENTE:                       sEjecutaCxCPorCliente
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElInformeSeleccionado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbAnalisisDeVencimientoPor_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbAnalisisDeVencimientoPor_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fSQLDeZonasDeCobranza() As String
   On Error GoTo h_ERROR
   fSQLDeZonasDeCobranza = "SELECT Nombre FROM ZonaCobranza  WHERE ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDeZonasDeCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub cmbCantidadAImprimir_Click()
   On Error GoTo h_ERROR
   Select Case mInformeSeleccionado
      Case OPT_CXC_POR_VENDEDOR, OPT_ANALISIS_DE_VENCIMIENTO, OPT_CHEQUES_DEVUELTOS
         txtNombreVendedor.Text = ""
         txtCodigoVendedor.Text = ""
         If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
            frameVendedor.Visible = True
         Else
            frameVendedor.Visible = False
         End If
      Case OPT_HISTORICO_DE_CLIENTE, OPT_ANALISIS_CxC_HISTORICO
         txtNombreDeCliente.Text = ""
         txtCodigoDeCliente.Text = ""
         If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
            frameCliente.Visible = False
            ChkCambiandodePagina.Visible = mInformeSeleccionado <> OPT_ANALISIS_CxC_HISTORICO
         Else
            frameCliente.Visible = True
            ChkCambiandodePagina.Visible = False
         End If
       Case OPT_CXC_POR_CLIENTE
         txtNombreDeCliente.Text = ""
         txtCodigoDeCliente.Text = ""
         If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
            frameCliente.Visible = False
         Else
            frameCliente.Visible = True
         End If
   End Select
   sInitDefaultValuesCasoSistemaInternoParaNivelPorUsuario
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbCantidadAImprimir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbCantidadAImprimir_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbCantidadAImprimir_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoVendedor_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtCodigoVendedor
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoVendedor_GotFocus()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoVendedor_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoVendedor_KeyDown()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreVendedor_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtNombreVendedor
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreVendedor_GotFocus()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreVendedor_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreVendedor_KeyDown()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreVendedor_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If LenB(txtNombreVendedor.Text) = 0 Then
      txtNombreVendedor.Text = "*"
   End If
   insVendedor.sClrRecord
   insVendedor.SetNombre txtNombreVendedor.Text
   If insVendedor.fSearchSelectConnection() Then
      sSelectAndSetValuesOfVendedor
   Else
      Cancel = True
      GoTo h_EXIT
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " txtCodigoVendedor_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sSelectAndSetValuesOfVendedor()
   On Error GoTo h_ERROR
   If insVendedor.fRsRecordCount(False) = 1 Then
      sAssignFieldsFromConnectionVendedor
   ElseIf insVendedor.fRsRecordCount(False) > 1 Then
      If gProyParametros.GetUsaCodigoVendedorEnPantalla Then
         insVendedor.sShowListSelect "Codigo", txtCodigoVendedor.Text
      Else
         insVendedor.sShowListSelect "Nombre", txtNombreVendedor.Text
      End If
      sAssignFieldsFromConnectionVendedor
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sSelectAndSetValuesOfVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAssignFieldsFromConnectionVendedor()
   On Error GoTo h_ERROR
   txtCodigoVendedor.Text = insVendedor.GetCodigo
   txtNombreVendedor.Text = insVendedor.GetNombre
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAssignFieldsFromConnectionVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInitDefaultValues()
   Dim mRecordsetZona As ADODB.Recordset
   Dim gFechasDeLosInformes As clsFechasDeLosInformesNav
   On Error GoTo h_ERROR
   Set gFechasDeLosInformes = New clsFechasDeLosInformesNav
   gFechasDeLosInformes.sLeeLasFechasDeInformes dtpFechaInicial, dtpFechaFinal, gProyUsuarioActual.GetNombreDelUsuario
   Set gFechasDeLosInformes = Nothing
   gEnumReport.FillComboBoxWithReporteOrdenadoPor cmbAnalisisDeVencimientoPor
   cmbAnalisisDeVencimientoPor.ListIndex = 0
   lblAnalisisDeVencimientoPor(1).Left = cmbAnalisisDeVencimientoPor.Left + cmbAnalisisDeVencimientoPor.Width + 45
   gEnumReport.FillComboBoxWithCantidadAImprimir CmbCantidadAImprimir
   gEnumProyecto.FillComboBoxWithFormaDeEscogerCompania cmbOrdenadoPor
   cmbOrdenadoPor.Text = gEnumProyecto.enumFormaDeEscogerCompaniaToString(enum_FormaDeEscogerCompania.eFD_PORNOMBRE)
   cmbOrdenadoPor.ListIndex = 0
   CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno)
   CmbCantidadAImprimir.ListIndex = 0
   Set mRecordsetZona = New ADODB.Recordset
   '***** pasar a sub
   CmbZonaCobranza.Clear
   gDbUtil.OpenRecordset mRecordsetZona, fSQLDeZonasDeCobranza
   CmbZonaCobranza.AddItem "TODAS"
   If mRecordsetZona.RecordCount > 0 Then
      gDbUtil.sFillComboBoxWithRecordSet CmbZonaCobranza, mRecordsetZona, "Nombre"
      End If
   CmbZonaCobranza.ListIndex = 0
   gDbUtil.sCloseIfOpened mRecordsetZona
   Set mRecordsetZona = Nothing
   '*****
   optResumido.value = True
   optDetallado.value = False
   gEnumProyecto.FillComboBoxWithStatusDocumento cmbStatus, Buscar
   cmbStatus.Text = gEnumProyecto.enumStatusDocumentoToString(eSD_PORCANCELAR)
   cmbStatus.ListIndex = 0
   gEnumReport.FillComboBoxWithMonedaDeLosReportes cmbMonedaDeLosReportes, gProyParametros.GetNombreMonedaLocal
   cmbMonedaDeLosReportes.Width = 1935
   cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal)
   cmbMonedaDeLosReportes.ListIndex = eMR_EnMonedaOriginal
   gProyParametrosCompania.sFillComboBoxWithCamposDefiniblesFactura cmbCamposDefinibles, True, True
   sFillComboBoxSectorDeNegocio
   sFillComboBoxZonaDeCobranza
   mTipoAgrupar = "NO AGRUPAR"
   optTasaDeCambio(0).value = True
   gAPI.ssSetFocus optInformeDeCxC(OPT_CXC_POR_VENDEDOR)
   gAPI.ssSetFocus OptAgruparInforme(OPT_NOAGRUPAR_INFORME)
   
   If gProyParametros.GetUsaCodigoVendedorEnPantalla Then
      lblCodigoVendedor.Caption = "Código"
      txtNombreVendedor.Enabled = False
   Else
      lblCodigoVendedor.Caption = "Nombre"
      txtNombreVendedor.Left = txtCodigoVendedor.Left
      txtCodigoVendedor.Visible = False
   End If
   If gProyParametros.GetUsaCodigoClienteEnPantalla Then
      lblNombreDeCliente.Caption = "Código"
      txtNombreDeCliente.Enabled = False
   Else
      lblNombreDeCliente.Caption = "Nombre"
      txtNombreDeCliente.Left = txtCodigoDeCliente.Left
      txtCodigoDeCliente.Visible = False
   End If
   optInformeDeCxC(OPT_FACTURAS_SIN_CXC).Visible = gDefgen.GetElProgramaEstaEnModoAvanzado
  
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitDefaltValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sShowMessageForRequiredFields(ByVal valCampo As String, ByRef refCampo As TextBox)
   On Error GoTo h_ERROR
   gMessage.ShowRequiredFields valCampo
   gAPI.ssSetFocus refCampo
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sShowMessageForRequiredFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeAnalisisDeVencimiento()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Informe - Análisis de vencimiento"
   frameAnalisisDeVencimientoPor.Visible = True
   frameZonaCobranza.Visible = True
   frameInforme.Visible = True
   frameMoneda.Visible = True
   frameCantidadAImprimir.Visible = True
   frameCantidadAImprimir.Top = 660
   frameVendedor.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeAnalisisDeVencimiento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaAnalisisDeVencimiento()
   Dim SqlDelReporte As String
   Dim insConfigurar As clsCxCRpt
   Dim reporte As DDActiveReports2.ActiveReport
   Dim detallado As Boolean
   Dim Titulo As String
   Dim tasaOrig As Boolean
   Dim monedaOrig As Boolean
   Dim TituloFecha As String
   Dim insCxcSQL As clsCxCSQL
   On Error GoTo h_ERROR
   Set insCxcSQL = New clsCxCSQL
   If gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_uno) And Len(txtCodigoVendedor.Text) = 0 Then
      If gProyParametros.GetUsaCodigoClienteEnPantalla Then
         sShowMessageForRequiredFields "Codigo del Vendedor", txtCodigoVendedor
      Else
         sShowMessageForRequiredFields "Nombre del Vendedor", txtNombreVendedor
      End If
      GoTo h_EXIT
   End If
   Set insConfigurar = New clsCxCRpt
   Set reporte = New DDActiveReports2.ActiveReport
   SqlDelReporte = insCxcSQL.fConstruirSQLDelReporteAnalisisDeVencimiento(txtCodigoVendedor.Text, CmbCantidadAImprimir.Text, CmbZonaCobranza.Text, cmbAnalisisDeVencimientoPor.Text, gProyCompaniaActual.GetConsecutivoCompania, cmbMonedaDeLosReportes.Text, gProyParametros.GetNombreMonedaLocal, InsAdmPropAnalisisVenc, gUltimaTasaDeCambio, gMonedaLocalActual)
   detallado = optDetallado.value
   Titulo = "Análisis de Vencimiento de Cuentas por Cobrar - "
   If detallado Then
      Titulo = Titulo & "Detallado"
   Else
      Titulo = Titulo & "Resumido"
   End If
   tasaOrig = optTasaDeCambio(0).value
   TituloFecha = "Del " & gConvert.dateToString(dtpFechaInicial) & " al " & gConvert.dateToString(dtpFechaFinal)
   monedaOrig = Not (cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal))
   If InsAdmPropAnalisisVenc.fBuscaValoresDeLasPropAnalisisVencActual Then
      If insConfigurar.fConfiguraLasSeccionesDelReporteDeAnalisisDeVencimiento(reporte, SqlDelReporte, detallado, TituloFecha, InsAdmPropAnalisisVenc.GetPrimerVencimiento, InsAdmPropAnalisisVenc.GetSegundoVencimiento, InsAdmPropAnalisisVenc.GetTercerVencimiento, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal), monedaOrig, False, txtCodigoVendedor.Text, txtNombreVendedor.Text) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, Titulo
      End If
   End If
   Set insConfigurar = Nothing
   Set reporte = Nothing
   Set insCxcSQL = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaAnalisisDeVencimiento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeCXCPorVendedor()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Informe - CxC por vendedor"
   frameCantidadAImprimir.Visible = True
   frameCantidadAImprimir.Top = 660
   frameVendedor.Visible = True
   cmbCantidadAImprimir_Click
   frameMoneda.Visible = True
   frameTasaDeCambio.Visible = True
   chkMontoTotalYRestanteConIva.Visible = True
   chkMontoTotalYRestanteConIva.value = vbChecked
   chkTotalizaPorCliente.Visible = True
   chkMostrarFechaUltimoPago.Visible = True
   frameFechas.Visible = True
   frameContacto.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeCXCPorVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaCXCPorVendedor()
   Dim SqlDelReporte As String
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCxCRpt
   Dim montosConIva As Boolean
   Dim monedaOrig As Boolean
   Dim totalizaPorCliente As Boolean
   Dim MensajesDeMonedaParaInformes As String
   Dim insCxcSQL As clsCxCSQL
   On Error GoTo h_ERROR
   Set insCxcSQL = New clsCxCSQL
   If LenB(txtNombreVendedor.Text) = 0 And CmbCantidadAImprimir.Text = "Uno" Then
      If gProyParametros.GetUsaCodigoClienteEnPantalla Then
         sShowMessageForRequiredFields "Codigo del Vendedor", txtCodigoVendedor
      Else
         sShowMessageForRequiredFields "Nombre del Vendedor", txtNombreVendedor
      End If
   Else
      Set reporte = New DDActiveReports2.ActiveReport
      Set insConfigurar = New clsCxCRpt
      montosConIva = (chkMontoTotalYRestanteConIva.value = vbChecked)
      SqlDelReporte = insCxcSQL.fConstruirSQLDelReporteDeCXCPorVendedor(gProyCompaniaActual.GetConsecutivoCompania, cmbMonedaDeLosReportes.Text, chkMontoTotalYRestanteConIva.value, optTasaDeCambio(0).value, CmbCantidadAImprimir.Text, txtCodigoVendedor.Text, dtpFechaInicial.value, dtpFechaFinal.value, gUltimaTasaDeCambio, gMonedaLocalActual, (chkMostrarFechaUltimoPago.value = vbChecked), gConvert.ConvertByteToBoolean(ChkContacto.value))
      If optTasaDeCambio(0).value Then
         MensajesDeMonedaParaInformes = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
      ElseIf optTasaDeCambio(1).value Then
         MensajesDeMonedaParaInformes = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioDelDia)
      End If
      monedaOrig = Not (cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal))
      totalizaPorCliente = (chkTotalizaPorCliente.value = vbChecked)
      If insConfigurar.fConfiguraLasSeccionesDelReporteDeCxCxVendedor(reporte, SqlDelReporte, montosConIva, totalizaPorCliente, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), MensajesDeMonedaParaInformes, monedaOrig, gGlobalization, dtpFechaInicial.value, dtpFechaFinal.value, (chkMostrarFechaUltimoPago.value = vbChecked), gConvert.ConvertByteToBoolean(ChkContacto.value)) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "CxC por Vendedor", vbModeless
      End If
      Set insConfigurar = Nothing
      Set reporte = Nothing
      Set insCxcSQL = Nothing
  End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaCXCPorVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeAnalisisCXCHistorico()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Informe - Análisis CXC histórico"
   frameCliente.Visible = True
   frameFechas.Visible = True
   frameMoneda.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeAnalisisCXCHistorico", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeEstadisticoDeVendedores()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Informe - Estadistico De Vendedores"
   frameFechas.Visible = True
   frameExportar.Visible = True
   txtNombreArchivo.Text = "C:\EstadisticoDeVendedores.xls"
   frameMoneda.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeEstadisticoDeVendedores", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeFacturasAnuladasVsCxCVigentes()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Informe - Facturas Anuladas con CxC aún Vigentes"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeFacturasAnuladasVsCxCVigentes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaAnalisisCxCHistorico()
   Dim insConfigurar As clsCxCRpt
   Dim reporte As DDActiveReports2.ActiveReport
   Dim SqlDelReporte As String
   Dim usarCambio As Boolean
   Dim insCxcSQL As clsCxCSQL
   On Error GoTo h_ERROR
   If dtpFechaFinal.value < dtpFechaInicial.value Then
      dtpFechaFinal.value = dtpFechaInicial.value
   End If
   If LenB(txtCodigoDeCliente.Text) = 0 Then
      sShowMessageForRequiredFields "Nombre del Cliente", txtNombreDeCliente
   Else
      Set insConfigurar = New clsCxCRpt
      Set reporte = New DDActiveReports2.ActiveReport
      Set insCxcSQL = New clsCxCSQL
      usarCambio = (gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal))
      SqlDelReporte = insCxcSQL.fSQLAnalisisCXCHistorico(Trim(txtCodigoDeCliente.Text), dtpFechaInicial.value, dtpFechaFinal.value, usarCambio, gProyCompaniaActual.GetConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
      If insConfigurar.fConfigurarDatosDelReporteAnalisisDeCxCHistorico(reporte, SqlDelReporte, dtpFechaInicial.value, dtpFechaFinal.value, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal), usarCambio, gGlobalization) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Análisis de Cuentas por Cobrar Histórico."
      End If
      Set insConfigurar = Nothing
      Set reporte = Nothing
      Set insCxcSQL = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaAnalisisCxCHistorico", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeCXCEntreFechas()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Informe - CXC entre fechas"
   frameFechas.Visible = True
'   If gProyParametrosCompania.GetUsaMonedaExtranjera Then
'      frameTasaDeCambio.Visible = True
'      frameMoneda.Visible = True
'   End If
   frameCamposDefinibles.Visible = gProyParametrosCompania.GetUsaCamposDefiniblesFactura
   frameStatus.Top = 660
   frameStatus.Visible = True
   frameAgruparInformeCXC.Visible = True
   frameContacto.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeCXCEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaCXCEntreFechas()
   Dim SqlDelReporte As String
   Dim insConfigurar As clsCxCRpt
   Dim reporte As DDActiveReports2.ActiveReport
   Dim monedaOriginal As Boolean
   Dim insCxcSQL As clsCxCSQL
   On Error GoTo h_ERROR
   If dtpFechaFinal.value < dtpFechaInicial.value Then
      dtpFechaFinal.value = dtpFechaInicial.value
   End If
   Set insConfigurar = New clsCxCRpt
   Set reporte = New DDActiveReports2.ActiveReport
   Set insCxcSQL = New clsCxCSQL
   If mTipoAgrupar = "SectorDeNegocio" Then
      SqlDelReporte = insCxcSQL.fSQLDelReporteCxCEntreFechasODelReporteCxCPendientesEntreFechas(mUsarCambioOriginal, insCamposDefFactura, cmbMonedaDeLosReportes.Text, gProyCompaniaActual.GetConsecutivoCompania, mInformeSeleccionado, cmbCamposDefinibles, gProyParametrosCompania, cmbStatus.Text, dtpFechaInicial.value, dtpFechaFinal.value, gProyCompaniaActual.GetUsaModuloDeContabilidad, gUltimaTasaDeCambio, gMonedaLocalActual, gListLibrary, mTipoAgrupar, cmbAgruparSectorDeNegocio.Text, gConvert.ConvertByteToBoolean(ChkContacto.value))
   ElseIf mTipoAgrupar = "ZonaDeCobranza" Then
      SqlDelReporte = insCxcSQL.fSQLDelReporteCxCEntreFechasODelReporteCxCPendientesEntreFechas(mUsarCambioOriginal, insCamposDefFactura, cmbMonedaDeLosReportes.Text, gProyCompaniaActual.GetConsecutivoCompania, mInformeSeleccionado, cmbCamposDefinibles, gProyParametrosCompania, cmbStatus.Text, dtpFechaInicial.value, dtpFechaFinal.value, gProyCompaniaActual.GetUsaModuloDeContabilidad, gUltimaTasaDeCambio, gMonedaLocalActual, gListLibrary, mTipoAgrupar, cmbAgruparZona.Text, gConvert.ConvertByteToBoolean(ChkContacto.value))
   Else
   SqlDelReporte = insCxcSQL.fSQLDelReporteCxCEntreFechasODelReporteCxCPendientesEntreFechas(mUsarCambioOriginal, insCamposDefFactura, cmbMonedaDeLosReportes.Text, gProyCompaniaActual.GetConsecutivoCompania, mInformeSeleccionado, cmbCamposDefinibles, gProyParametrosCompania, cmbStatus.Text, dtpFechaInicial.value, dtpFechaFinal.value, gProyCompaniaActual.GetUsaModuloDeContabilidad, gUltimaTasaDeCambio, gMonedaLocalActual, gListLibrary, mTipoAgrupar, "", gConvert.ConvertByteToBoolean(ChkContacto.value))
   End If
      monedaOriginal = (cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal))

   If mTipoAgrupar = "SectorDeNegocio" Then
      If insConfigurar.fConfiguraLasSeccionesDelReporteDeCxCEntreFechasPorSectorDeNegocio(reporte, SqlDelReporte, dtpFechaInicial.value, dtpFechaFinal.value, monedaOriginal, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gProyParametrosCompania.GetUsaCamposDefiniblesFactura, gProyCompaniaActual.GetUsaModuloDeContabilidad, gConvert.ConvertByteToBoolean(ChkContacto.value)) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Cuentas por Cobras entre Fechas"
      End If
   ElseIf mTipoAgrupar = "ZonaDeCobranza" Then
      If insConfigurar.fConfiguraLasSeccionesDelReporteDeCxCEntreFechasPorZonaDeCobranza(reporte, SqlDelReporte, dtpFechaInicial.value, dtpFechaFinal.value, monedaOriginal, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gProyParametrosCompania.GetUsaCamposDefiniblesFactura, gProyCompaniaActual.GetUsaModuloDeContabilidad, gConvert.ConvertByteToBoolean(ChkContacto.value)) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Cuentas por Cobras entre Fechas"
      End If
   Else
      If insConfigurar.fConfiguraLasSeccionesDelReporteDeCxCEntreFechas(reporte, SqlDelReporte, dtpFechaInicial.value, dtpFechaFinal.value, monedaOriginal, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gProyParametrosCompania.GetUsaCamposDefiniblesFactura, gProyCompaniaActual.GetUsaModuloDeContabilidad, gConvert.ConvertByteToBoolean(ChkContacto.value)) Then
        gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Cuentas por Cobras entre Fechas"
      End If
   End If
   Set insCxcSQL = New clsCxCSQL
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaCXCEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeHisoricodeCliente()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Informe - Histórico de cliente"
   frameCantidadAImprimir.Visible = True
   frameCantidadAImprimir.Top = 660
   frameCliente.Visible = True
   frameOrdenadoPor.Visible = True
   cmbCantidadAImprimir_Click
   frameFechas.Visible = True
   frameMoneda.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeHisoricodeCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaHistoricoDeCliente()
   Dim SqlDelReporte As String
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCxCRpt
   Dim usarCambioBs As Boolean
   Dim OrdenaXCodigo As Boolean
   Dim codigoCliente As String
   Dim vUsarCambioOriginal As Boolean
   Dim insCxC As clsCxCSQL
   On Error GoTo h_ERROR
   OrdenaXCodigo = False
   If dtpFechaFinal.value < dtpFechaInicial.value Then
      dtpFechaFinal.value = dtpFechaInicial.value
   End If
   If gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_uno) And LenB(txtNombreDeCliente.Text) = 0 Then
      sShowMessageForRequiredFields "Nombre del Cliente", txtNombreDeCliente
   Else
      Set reporte = New DDActiveReports2.ActiveReport
      Set insConfigurar = New clsCxCRpt
      Set insCxC = New clsCxCSQL
      usarCambioBs = (gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal))
      codigoCliente = ""
      If gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
         codigoCliente = Trim(txtCodigoDeCliente.Text)
      End If
      If cmbOrdenadoPor.ListIndex <> enum_ReporteOrdenadoPor.eRO_Codigo Then
         OrdenaXCodigo = True
      End If
      SqlDelReporte = insCxC.fSQLHistoricoDeCliente(dtpFechaInicial.value, dtpFechaFinal.value, codigoCliente, usarCambioBs, OrdenaXCodigo, gProyParametrosCompania.GetConsecutivoCompania, vUsarCambioOriginal, gMonedaLocalActual, gUltimaTasaDeCambio)
      If insConfigurar.fConfigurarDatosDelReporteHistoricoDeCliente(reporte, SqlDelReporte, dtpFechaInicial.value, dtpFechaFinal.value, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioDelDia), usarCambioBs, ChkCambiandodePagina) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Histórico de Cliente"
      End If
      Set reporte = Nothing
      Set insConfigurar = Nothing
      Set insCxC = Nothing
  End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaHistoricoDeCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sOcultaTodosLosCampos()
   On Error GoTo h_ERROR
   lblFechaInicial.Visible = True
   dtpFechaInicial.Visible = True
   frameAnalisisDeVencimientoPor.Visible = False
   frameZonaCobranza.Visible = False
   frameInforme.Visible = False
   frameCantidadAImprimir.Visible = False
   frameVendedor.Visible = False
   frameCliente.Visible = False
   frameFechas.Visible = False
   frameTasaDeCambio.Visible = False
   frameExportar.Visible = False
   frameStatus.Visible = False
   frameMoneda.Visible = False
   frameCamposDefinibles.Visible = False
   ChkCambiandodePagina.Visible = False
   chkMontoTotalYRestanteConIva.Visible = False
   chkTotalizaPorCliente.Visible = False
   frameOrdenadoPor.Visible = False
   chkTotalizarPorMes.Visible = False
   chkAgruparPorVendedor.Visible = False
   chkIncluirFacturasHistoricas.Visible = False
   ChkAgruparPorTipo.value = False
   ChkAgruparPorTipo.Visible = False
   frameTipoDocumento.Visible = False
   frameFechas.Top = 2430
   frameMoneda.Top = 2460
   frameTasaDeCambio.Top = 2430
   chkMostrarFechaUltimoPago.Visible = False
   frameAgruparInformeCXC.Visible = False
   frameContacto.Visible = False
   frameFiltroZona.Visible = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sOcultaTodosLosCampos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaEstadisticoDeVendedores()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim SqlDelReporte As String
   Dim InsCxCDsr As clsCxCDsr
   Dim insCxcSQL As clsCxCSQL
   On Error GoTo h_ERROR
   Set InsCxCDsr = New clsCxCDsr
   Set insCxcSQL = New clsCxCSQL
   If dtpFechaFinal.value < dtpFechaInicial.value Then
      dtpFechaFinal.value = dtpFechaInicial.value
   End If
   SqlDelReporte = insCxcSQL.fConstruirSQLDelReporteDeEstadisticoDeVendedor(gProyCompaniaActual.GetConsecutivoCompania, cmbMonedaDeLosReportes.Text, dtpFechaInicial.value, dtpFechaFinal.value, gUltimaTasaDeCambio, gMonedaLocalActual)
   If SqlDelReporte <> "" Then
      Set reporte = InsCxCDsr.fConfigurarDsrEstadisticoDeVendedores(SqlDelReporte, dtpFechaInicial.value, dtpFechaFinal.value, cmbMonedaDeLosReportes.Text, gProyCompaniaActual.GetNombreCompaniaParaInformes(False))
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Estadístico de Vendedores"
   End If
   Set reporte = Nothing
   Set InsCxCDsr = Nothing
   Set insCxcSQL = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaEstadisticoDeVendedores", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdExportar_Click()
   Dim SqlDelReporte As String
   Dim ExportarReporteEstDeVen As DDActiveReports2.ActiveReport
   Dim InsCxCDsr As clsCxCDsr
   Dim insCxcSQL As clsCxCSQL
   On Error GoTo h_ERROR
   Set insCxcSQL = New clsCxCSQL
   If mInformeSeleccionado = 5 Then
      Set InsCxCDsr = New clsCxCDsr
      SqlDelReporte = insCxcSQL.fConstruirSQLDelReporteDeEstadisticoDeVendedor(gProyCompaniaActual.GetConsecutivoCompania, cmbMonedaDeLosReportes.Text, dtpFechaInicial.value, dtpFechaFinal.value, gUltimaTasaDeCambio, gMonedaLocalActual)
      Set ExportarReporteEstDeVen = InsCxCDsr.fConfigurarDsrEstadisticoDeVendedores(SqlDelReporte, dtpFechaInicial.value, dtpFechaFinal.value, cmbMonedaDeLosReportes.Text, gProyCompaniaActual.GetNombreCompaniaParaInformes(False))
      ExportarReporteEstDeVen.Run
      If Not ExportarReporteEstDeVen Is Nothing Then
         xls.FileName = txtNombreArchivo ' "C:\EstadisticoDeVendedores.xls"
         ExportarReporteEstDeVen.Export xls
         gMessage.exito "La exportación ha sido un exito"
         Set ExportarReporteEstDeVen = Nothing
         Set InsCxCDsr = Nothing
      End If
   End If
   Set insCxcSQL = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdExportar_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdExportar_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdExportar_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaCXCPendientesEntreFechas()
   Dim SqlDelReporte As String
   Dim insConfigurar As clsCxCRpt
   Dim reporte As DDActiveReports2.ActiveReport
   Dim monedaOriginal As Boolean
   Dim insCxcSQL As clsCxCSQL
   On Error GoTo h_ERROR
   If dtpFechaFinal.value < dtpFechaInicial.value Then
      dtpFechaFinal.value = dtpFechaInicial.value
   End If
   Set insConfigurar = New clsCxCRpt
   Set reporte = New DDActiveReports2.ActiveReport
   Set insCxcSQL = New clsCxCSQL
   SqlDelReporte = insCxcSQL.fSQLDelReporteCxCEntreFechasODelReporteCxCPendientesEntreFechas(mUsarCambioOriginal, insCamposDefFactura, cmbMonedaDeLosReportes.Text, gProyCompaniaActual.GetConsecutivoCompania, mInformeSeleccionado, cmbCamposDefinibles, gProyParametrosCompania, cmbStatus.Text, dtpFechaInicial.value, dtpFechaFinal.value, gProyCompaniaActual.GetUsaModuloDeContabilidad, gUltimaTasaDeCambio, gMonedaLocalActual, gListLibrary, "", "", gConvert.ConvertByteToBoolean(ChkContacto.value))
       monedaOriginal = (cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal))
   If insConfigurar.fConfiguraLasSeccionesDelReporteDeCxCPendientesEntreFechas(reporte, SqlDelReporte, dtpFechaInicial.value, dtpFechaFinal.value, monedaOriginal, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gProyParametrosCompania.GetUsaCamposDefiniblesFactura, gProyCompaniaActual.GetUsaModuloDeContabilidad, gConvert.ConvertByteToBoolean(ChkContacto.value)) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Cuentas por Cobrar Pendientes entre Fechas"
   End If
   Set insCxcSQL = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaCXCPendientesEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeCXCPendientesEntreFechas()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Informe - CXC Pendientes entre Fechas"
   frameFechas.Visible = True
   frameContacto.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeCXCPendientesEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeCxCConDescripcion()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Informe - CxC con Descripción"
   frameAnalisisDeVencimientoPor.Visible = True
   frameZonaCobranza.Visible = True
   frameMoneda.Visible = True
   frameContacto = True
   frameContacto.Visible = True
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeCxCConDescripcion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaCxCconDescripcion()
   Dim rptMostrarReporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCxCRpt
   Dim SqlDelReporte As String
   Dim valMostrarMensaje As Boolean
   Dim insCxcSQL As clsCxCSQL
   On Error GoTo h_ERROR
   Set insCxcSQL = New clsCxCSQL
      valMostrarMensaje = False
      If cmbMonedaDeLosReportes.ListIndex = eMR_EnBs Then
         valMostrarMensaje = True
      End If
   SqlDelReporte = insCxcSQL.fConstruirSQLDelReporteCxCconDescripcion(CmbZonaCobranza.Text, cmbAnalisisDeVencimientoPor.Text, cmbMonedaDeLosReportes.Text, gProyCompaniaActual.GetConsecutivoCompania, gMonedaLocalActual.GetHoyNombreMoneda, gUltimaTasaDeCambio, gConvert.ConvertByteToBoolean(ChkContacto.value))
      Set rptMostrarReporte = New DDActiveReports2.ActiveReport
      Set insConfigurar = New clsCxCRpt
      If insConfigurar.fConfigurarDatosDelReporteCxCConDescripcion(rptMostrarReporte, SqlDelReporte, dtpFechaInicial.value, dtpFechaFinal.value, valMostrarMensaje, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gConvert.ConvertByteToBoolean(ChkContacto.value)) Then
         gUtilReports.sMostrarOImprimirReporte rptMostrarReporte, 1, mDondeImprimir, "CxC Con Descripción"
   End If
   Set insCxcSQL = Nothing
   Set rptMostrarReporte = Nothing
   Set insConfigurar = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaCxCconDescripcion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaFacturasAnuladasVsCxCVigentes()
   Dim SqlDelReporte As String
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCxCRpt
   Dim insCxcSQL As clsCxCSQL
   On Error GoTo h_ERROR
   Set insCxcSQL = New clsCxCSQL
   Set reporte = New DDActiveReports2.ActiveReport
   Set insConfigurar = New clsCxCRpt
   SqlDelReporte = insCxcSQL.fConstruirSQLDelReporteFacturasAnuladasVsCxCVigentes(gProyCompaniaActual.GetConsecutivoCompania, insFactura, gEnumProyecto)
   If insConfigurar.fConfiguraLasSeccionesDelReporteDeFacturasAnuladasVsCxCVigentes(reporte, SqlDelReporte, gProyCompaniaActual.GetNombreCompaniaParaInformes(False)) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Facturas Anuladas vs. Cuentas por Cobrar Vigentes"
   End If
   Set reporte = Nothing
   Set insConfigurar = Nothing
   Set insCxcSQL = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaFacturasAnuladasVsCxCVigentes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeAnalisisDeVencimientoAUnaFecha()
   Dim mAction As AccionSobreRecord
   On Error GoTo h_ERROR
   mAction = Buscar
   lblDatosDelReporte.Caption = "Datos del Informe - Análisis de Vencimiento a una Fecha"
   frameAnalisisDeVencimientoPor.Visible = True
   frameInforme.Visible = True
   frameFechas.Visible = True
   lblFechaInicial.Visible = False
   dtpFechaInicial.Visible = False
   gEnumProyecto.FillComboBoxWithTipodeCxC cmbTipodocumento, mAction
   ChkAgruparPorTipo.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeAnalisisDeVencimientoAUnaFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaAnalisisDeVencimientoAUnaFecha()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim detallado As Boolean
   Dim ordenadoPorNombre As Boolean
   Dim Titulo As String
   Dim Agrupar As Boolean
   Dim TipoDeDocumento As enum_TipoDeTransaccion
   Dim AgruparTodos As Boolean
   On Error GoTo h_ERROR
   Set reporte = New DDActiveReports2.ActiveReport
   detallado = optDetallado.value
   Titulo = "Análisis de Vencimiento a una Fecha - "
   Agrupar = (ChkAgruparPorTipo.value = vbChecked)
   AgruparTodos = True
   If Agrupar Then
      If (cmbTipodocumento.Text = "Todos(as)") Then
         AgruparTodos = False
      Else
         TipoDeDocumento = gEnumProyecto.strTipoDeCxCToNum(gAPI.SelectedElementInComboBoxToString(cmbTipodocumento))
      End If
   End If
   If detallado Then
      Titulo = Titulo & "Detallado (" & gUtilReports.fDateFormatConGuiones(dtpFechaFinal.value) & ")"
   Else
      Titulo = Titulo & "Resumido (" & gUtilReports.fDateFormatConGuiones(dtpFechaFinal.value) & ")"
   End If
   ordenadoPorNombre = (cmbAnalisisDeVencimientoPor.Text = gEnumReport.enumReporteOrdenadoPorToString(enum_ReporteOrdenadoPor.eRO_Nombre))
   If insConfigurar.fConfiguraAnalisisDeCxCAUnaFecha(reporte, dtpFechaFinal.value, detallado, True, True, ordenadoPorNombre, Agrupar, AgruparTodos, TipoDeDocumento) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, Titulo
   End If
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaAnalisisDeVencimientoAUnaFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeAnalisisDeVencimientoEntreFechas()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Informe - Análisis de Vencimiento entre Fechas"
   frameFechas.Visible = True
   chkTotalizarPorMes.Visible = True
   chkTotalizarPorMes.value = vbChecked
   chkAgruparPorVendedor.Visible = True
   chkAgruparPorVendedor.value = vbChecked
   frameAnalisisDeVencimientoPor.Visible = True
   frameMoneda.Visible = True
   frameFiltroZona.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeAnalisisDeVencimientoEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaAnalisisDeVencimientoEntreFechas()
   Dim SqlDelReporte As String
   Dim insConfigurar As clsCxCRpt
   Dim reporte As DDActiveReports2.ActiveReport
   Dim monedaOriginal As Boolean
   Dim totalizarPorMes As Boolean
   Dim agruparPorVendedor As Boolean
   Dim MensajesDeMonedaParaInformes As String
   Dim insCxcSQL As clsCxCSQL
   On Error GoTo h_ERROR
   Set insCxcSQL = New clsCxCSQL
   If dtpFechaFinal.value < dtpFechaInicial.value Then
      dtpFechaFinal.value = dtpFechaInicial.value
   End If
   Set insConfigurar = New clsCxCRpt
   Set reporte = New DDActiveReports2.ActiveReport
   SqlDelReporte = insCxcSQL.fSQLDelReporteAnalisisDeVencimientoEntreFechas(chkAgruparPorVendedor.value, cmbAnalisisDeVencimientoPor.Text, cmbMonedaDeLosReportes.Text, dtpFechaInicial.value, dtpFechaFinal.value, gProyParametros.GetNombreMonedaLocal, gProyCompaniaActual.GetConsecutivoCompania, InsAdmPropAnalisisVenc, gUltimaTasaDeCambio, gMonedaLocalActual, gConvert.ConvertByteToBoolean(chkFiltroZona.value), cmbFiltroZona.Text)
   monedaOriginal = Not (cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal))
   totalizarPorMes = (chkTotalizarPorMes.value = vbChecked)
   agruparPorVendedor = (chkAgruparPorVendedor.value = vbChecked)
   If InsAdmPropAnalisisVenc.fBuscaValoresDeLasPropAnalisisVencActual Then
      If insConfigurar.fConfigurarDatosDelReporteAnalisisDeVencimientoEntreFechas(reporte, SqlDelReporte, dtpFechaInicial, dtpFechaFinal, totalizarPorMes, agruparPorVendedor, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), InsAdmPropAnalisisVenc.GetPrimerVencimiento, InsAdmPropAnalisisVenc.GetSegundoVencimiento, InsAdmPropAnalisisVenc.GetTercerVencimiento, MensajesDeMonedaParaInformes = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal), monedaOriginal, gConvert.ConvertByteToBoolean(chkFiltroZona.value)) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Análisis de Vencimiento entre Fechas"
      End If
   End If
   Set insCxcSQL = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaAnalisisDeVencimientoEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeFacturasSinCxC()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Informe - Facturas Sin CxC"
   chkIncluirFacturasHistoricas.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeFacturasSinCxC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaFacturasSinCxC()
   Dim SqlDelReporte As String
   Dim insConfigurar As clsCxCRpt
   Dim insCxcSQL As clsCxCSQL
   Dim reporte As DDActiveReports2.ActiveReport
   On Error GoTo h_ERROR
   Set insConfigurar = New clsCxCRpt
   Set insCxcSQL = New clsCxCSQL
   Set reporte = New DDActiveReports2.ActiveReport
   SqlDelReporte = insCxcSQL.fConstruirSQLDelReporteFacturasSinCxC(gProyCompaniaActual.GetConsecutivoCompania, chkIncluirFacturasHistoricas.value)
   If insConfigurar.fConfigurarDatosDelReporteFacturasSinCxC(reporte, SqlDelReporte, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gGlobalization) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Análisis de Vencimiento entre Fechas"
   End If
   Set insCxcSQL = Nothing
   Set insConfigurar = Nothing
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaFacturasSinCxC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeChequesDevueltos()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Informe - Cheques Devueltos"
   frameFechas.Visible = True
   frameMoneda.Visible = True
   frameTasaDeCambio.Visible = True
   frameStatus.Visible = True
   frameStatus.Top = 2000
   frameCantidadAImprimir.Visible = True
   frameVendedor.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeChequesDevueltos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sEjecutaInformeDeChequesDevueltos()
   Dim SqlDelReporte As String
   Dim insConfigurar As clsCxCRpt
   Dim insCxcSQL As clsCxCSQL
   Dim reporte As DDActiveReports2.ActiveReport
   Dim ReporteEnMonedaLocal As Boolean
   Dim usarCambioOriginal As Boolean
   Dim valCambio As enum_MensajeMonedaParaInformes
   On Error GoTo h_ERROR
   Set insConfigurar = New clsCxCRpt
   Set insCxcSQL = New clsCxCSQL
   Set reporte = New DDActiveReports2.ActiveReport
   If optTasaDeCambio(0).value Then
      valCambio = eMM_CambioOriginal
      usarCambioOriginal = True
   Else
      valCambio = eMM_CambioDelDia
      usarCambioOriginal = False
   End If
   If cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal) Then
      ReporteEnMonedaLocal = True
   Else
      ReporteEnMonedaLocal = False
   End If
   SqlDelReporte = insCxcSQL.fSQLChequesDevueltos(ReporteEnMonedaLocal, usarCambioOriginal, gMonedaLocalActual, gUltimaTasaDeCambio, gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.value, dtpFechaFinal.value, txtCodigoVendedor.Text, cmbStatus.Text)
   If insConfigurar.fConfigurarDatosDelReporteChequesDevueltos(reporte, SqlDelReporte, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), ReporteEnMonedaLocal, gEnumReport.getMensajesDeMonedaParaInformes(valCambio), dtpFechaInicial.value, dtpFechaFinal.value) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Cheques Devueltos entre Fechas"
   End If
   
   Set insCxcSQL = Nothing
   Set insConfigurar = Nothing
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaInformeDeChequesDevueltos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInitDefaultValuesCasoSistemaInternoParaNivelPorUsuario()
 If (gProyParametros.GetEsSistemaParaIG) Then
   If (gProyUsuarioActual.GetInformesCxCPorVendedor() And Not gProyUsuarioActual.GetEsSupervisor) Then
      sAsignaCodigoVendedorFromConexionUsuario
   End If
 End If
End Sub

Private Sub sAsignaCodigoVendedorFromConexionUsuario()
   sOcultarOtrasOpciones
   optInformeDeCxC(1).Visible = True
   On Error GoTo h_ERROR
   If (insVendedor.fSearchByField("email", gProyUsuarioActual.GetEmail(), False, True, True)) Then
       txtCodigoVendedor.Text = insVendedor.GetCodigo
       txtNombreVendedor.Text = insVendedor.GetNombre
   End If
   txtNombreVendedor.Enabled = False
   CmbCantidadAImprimir.Enabled = False
   frameInformes.Enabled = False
h_EXIT:    On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAsignaCodigoVendedorFromConexionUsuario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub


Private Sub sActivarOptParaAgrupar()
  Dim contador As Integer
  On Error GoTo h_ERROR
   mTipoAgrupar = "SectorDeNegocio"
   For contador = 0 To optTipoAgrupar.Count - 1
     optTipoAgrupar.Item(contador).Enabled = True
   Next contador
   gAPI.ssSetFocus optTipoAgrupar(OPT_TIPOAGRUPAR_SECTORDENEGOCIO)
   lblAgruparSectordeNegocio.Caption = "Sector de Negocio"
   lblAgruparSectordeNegocio.Enabled = True
   cmbAgruparSectorDeNegocio.Enabled = True
   lblAgruparSectordeNegocio.Visible = True
   cmbAgruparSectorDeNegocio.Visible = True
h_EXIT:    On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarOptParaAgrupar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sDesactivarOptParaAgrupar()
Dim contador As Integer
On Error GoTo h_ERROR
  mTipoAgrupar = "NO AGRUPAR"
  For contador = 0 To optTipoAgrupar.Count - 1
   optTipoAgrupar.Item(contador).Enabled = False
  Next contador
  sOcultarCamposParaAgrupar
  lblAgruparSectordeNegocio.Visible = True
  lblAgruparSectordeNegocio.Caption = "Agrupar Por..."
  cmbAgruparSectorDeNegocio.Visible = True
  cmbAgruparSectorDeNegocio.Enabled = False
  cmbAgruparZona.Visible = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sDesactivarOptParaAgrupar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposAgruparSectorDeNegocio()
Dim contador As Integer
On Error GoTo h_ERROR
  sOcultarCamposParaAgrupar
  mTipoAgrupar = "SectorDeNegocio"
  lblAgruparSectordeNegocio.Visible = True
  lblAgruparSectordeNegocio.Caption = "Sector de Negocio"
  cmbAgruparSectorDeNegocio.Visible = True
  lblAgruparSectordeNegocio.Enabled = True
  cmbAgruparSectorDeNegocio.Enabled = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposAgruparSectorDeNegocio", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposAgruparZonaCobranza()
Dim contador As Integer
On Error GoTo h_ERROR
  sOcultarCamposParaAgrupar
  mTipoAgrupar = "ZonaDeCobranza"
  lblAgruparZona.Visible = True
  cmbAgruparZona.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposAgruparZonaCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sOcultarCamposParaAgrupar()
On Error GoTo h_ERROR
  lblAgruparSectordeNegocio.Visible = False
  cmbAgruparSectorDeNegocio.Visible = False
  lblAgruparSectordeNegocio.Enabled = False
  cmbAgruparSectorDeNegocio.Enabled = False
  lblAgruparZona.Visible = False
  cmbAgruparZona.Visible = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sOcultarCamposParaAgrupar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sFillComboBoxSectorDeNegocio()
   Dim SQLSectorDeNegocio As String
   Dim rsSectorDeNegocio As ADODB.Recordset
   Dim insClienteSQL As clsClienteSQL
   On Error GoTo h_ERROR
     Set rsSectorDeNegocio = New ADODB.Recordset
     Set insClienteSQL = New clsClienteSQL
     SQLSectorDeNegocio = insClienteSQL.fSQLSectorDeNegocio(gProyParametrosCompania.GetConsecutivoCompania)
     Set insClienteSQL = Nothing
     gDbUtil.OpenRecordset rsSectorDeNegocio, SQLSectorDeNegocio
     cmbAgruparSectorDeNegocio.AddItem "TODOS"
     If rsSectorDeNegocio.RecordCount > 0 Then
       gDbUtil.sFillComboBoxWithRecordSet cmbAgruparSectorDeNegocio, rsSectorDeNegocio, "Descripcion"
     End If
     gDbUtil.sDestroyRecordSet rsSectorDeNegocio
     cmbAgruparSectorDeNegocio.ListIndex = 0
     cmbAgruparSectorDeNegocio.Text = "TODOS"
h_EXIT:   On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sFillComboBoxSectorDeNegocio", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sFillComboBoxZonaDeCobranza()
   Dim mRecordsetZona As ADODB.Recordset
   On Error GoTo h_ERROR
     Set mRecordsetZona = New ADODB.Recordset
     cmbAgruparZona.Clear
     gDbUtil.OpenRecordset mRecordsetZona, fSQLDeZonasDeCobranza
     cmbAgruparZona.AddItem "TODAS"
     If mRecordsetZona.RecordCount > 0 Then
        gDbUtil.sFillComboBoxWithRecordSet cmbAgruparZona, mRecordsetZona, "Nombre"
     End If
     cmbAgruparZona.ListIndex = 0
     gDbUtil.sDestroyRecordSet mRecordsetZona
     cmbAgruparZona.Text = "TODAS"
     Set mRecordsetZona = New ADODB.Recordset
     cmbFiltroZona.Clear
     gDbUtil.OpenRecordset mRecordsetZona, fSQLDeZonasDeCobranza
     cmbFiltroZona.AddItem "TODAS"
     If mRecordsetZona.RecordCount > 0 Then
       gDbUtil.sFillComboBoxWithRecordSet cmbFiltroZona, mRecordsetZona, "Nombre"
     End If
     cmbFiltroZona.ListIndex = 0
     gDbUtil.sDestroyRecordSet mRecordsetZona
     cmbFiltroZona.Text = "TODAS"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sFillComboBoxZonaDeCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInitCXCEntreFechas()
On Error GoTo h_ERROR
  OptAgruparInforme.Item(0).value = True
  optTipoAgrupar.Item(0).value = True
  cmbAgruparSectorDeNegocio.Enabled = False
  cmbAgruparSectorDeNegocio.Text = "TODOS"
  cmbAgruparZona.Text = "TODAS"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitCXCEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sOcultarOtrasOpciones()
   Dim vIndx As Long
   On Error GoTo h_ERROR
   For vIndx = 0 To optInformeDeCxC.Count - 1 Step 1
      optInformeDeCxC(vIndx).Visible = False
   Next vIndx
   lblVarios.Visible = False
   lblINformesEstadisticos.Visible = False
h_EXIT:    On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAsignaCodigoVendedorFromConexionUsuario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeCxCPorCliente()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Informe - CxC por Cliente"
   frameAnalisisDeVencimientoPor.Visible = True
   frameZonaCobranza.Visible = True
   frameMoneda.Visible = True
   frameContacto.Visible = True
   frameFechas.Visible = True
   frameCantidadAImprimir.Visible = True
   frameCantidadAImprimir.Top = 660
   cmbCantidadAImprimir_Click
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeCxCPorCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaCxCPorCliente()
   Dim rptMostrarReporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCxCRpt
   Dim vSqlDelReporte As String
   Dim vMostrarMensaje As Boolean
   Dim insCxcSQL As clsCxCSQL
   On Error GoTo h_ERROR
   Set insCxcSQL = New clsCxCSQL
   If gTexto.DfLen(txtNombreDeCliente.Text) = 0 And CmbCantidadAImprimir.Text = "Uno" Then
      If gProyParametros.GetUsaCodigoClienteEnPantalla Then
         sShowMessageForRequiredFields "Codigo del Cliente", txtCodigoDeCliente
      Else
         sShowMessageForRequiredFields "Nombre del Cliente", txtNombreDeCliente
      End If
   Else
      vMostrarMensaje = False
      If cmbMonedaDeLosReportes.ListIndex = eMR_EnBs Then
         vMostrarMensaje = True
      End If
      vSqlDelReporte = insCxcSQL.fConstruirSQLDelReporteCxCPorCliente(CmbZonaCobranza.Text, cmbAnalisisDeVencimientoPor.Text, cmbMonedaDeLosReportes.Text, gProyCompaniaActual.GetConsecutivoCompania, gMonedaLocalActual.GetHoyNombreMoneda, gUltimaTasaDeCambio, gConvert.ConvertByteToBoolean(ChkContacto.value), dtpFechaInicial.value, dtpFechaFinal.value, Trim(txtCodigoDeCliente.Text))
      Set rptMostrarReporte = New DDActiveReports2.ActiveReport
      Set insConfigurar = New clsCxCRpt
      If insConfigurar.fConfigurarDatosDelReporteCxCPorCliente(rptMostrarReporte, vSqlDelReporte, dtpFechaInicial.value, dtpFechaFinal.value, vMostrarMensaje, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gConvert.ConvertByteToBoolean(ChkContacto.value)) Then
         gUtilReports.sMostrarOImprimirReporte rptMostrarReporte, 1, mDondeImprimir, "CxC Por Cliente"
      End If
   End If
   Set insCxcSQL = Nothing
   Set rptMostrarReporte = Nothing
   Set insConfigurar = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaCxCPorCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLoadObjectValues(ByVal valCompaniaActual As Object, _
                                                   ByVal valVendedor As Object, _
                                                      ByVal vaCliente As Object, _
                                                         ByVal valPropAnalisisVencNav As Object, _
                                                            ByVal valCamposDefFacturaNav As Object, _
                                                               ByVal valARExportExcel As Object, _
                                                                  ByVal valFacturaNav As Object, _
                                                                     ByVal valRptInformesEspecialesConfigurar As Object)
                                                            
On Error GoTo h_ERROR
   Set gProyCompaniaActual = valCompaniaActual
   Set insVendedor = valVendedor
   Set insCliente = vaCliente
   Set insAdmPropAnalisisVenc = valPropAnalisisVencNav
   Set insCamposDefFactura = valCamposDefFacturaNav
   Set xls = valARExportExcel
   Set insFactura = valFacturaNav
   Set insConfigurar = valRptInformesEspecialesConfigurar
   Set insAdmPropAnalisisVenc = valPropAnalisisVencNav
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLoadObjectValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub


