using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class CobroDeFacturaRapidaEfectivoViewModel : LibGenericViewModel {
        #region Constantes
        public const string TipoEfectivoPropertyName = "TipoEfectivo";
        public const string MontoEfectivoPropertyName = "MontoEfectivo";
        public const string TotalACobrarPropertyName = "TotalACobrar";
        public const string TotalCobradoPropertyName = "TotalCobrado";
        public const string MontoDiferenciaPropertyName = "MontoDiferencia";
        public const string DiferenciaPropertyName = "Diferencia";
        public const string ColorDiferenciaPropertyName = "ColorDiferencia";
        public const string lblMontoDiferenciaPropertyName = "lblMontoDiferencia";
        public const string lblMontoTotalCobradoPropertyName = "lblMontoTotalCobrado";
        public const string lblMontoTotalACobrarPropertyName = "lblMontoTotalACobrar";
        public const bool EsBotonGrabarPropertyName = false;
        int _ConsecutivoCompania;
        string _NumeroFactura;
        string _CodigoFormaDelCobro;
        decimal _MontoEfectivo;
        decimal _TotalACobrar;
        decimal _TotalCobrado;
        decimal _MontoDiferencia;
        bool _EsBotonGrabar;
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Cobro Efectivo de Punto de Venta"; }
        }

        public int  ConsecutivoCompania {
            get {
                return _ConsecutivoCompania;
            }
            set {
                if (_ConsecutivoCompania != value) {
                    _ConsecutivoCompania = value;
                }
            }
        }

        public string  NumeroFactura {
            get {
                return _NumeroFactura;
            }
            set {
                if (_NumeroFactura != value) {
                    _NumeroFactura = value;
                }
            }
        }

        public string  CodigoFormaDelCobro {
            get {
                return _CodigoFormaDelCobro;
            }
            set {
                if (_CodigoFormaDelCobro != value) {
                    _CodigoFormaDelCobro = value;
                }
            }
        }

        public decimal  MontoEfectivo {
            get {
                return _MontoEfectivo;
            }
            set {
                if (_MontoEfectivo != value) {
                    _MontoEfectivo = value;
                    RaisePropertyChanged(MontoEfectivoPropertyName);
                }
            }
        }

        public void ActualizarTotales() {
            Diferencia = null;
            ColorDiferencia = null;
            RaisePropertyChanged(TotalCobradoPropertyName);
            RaisePropertyChanged(lblMontoTotalCobradoPropertyName);
            RaisePropertyChanged(MontoDiferenciaPropertyName);
            RaisePropertyChanged(lblMontoDiferenciaPropertyName);
            RaisePropertyChanged(DiferenciaPropertyName);
            RaisePropertyChanged(ColorDiferenciaPropertyName);
        }

        public decimal  TotalACobrar {
            get {
                return _TotalACobrar;
            }
            set {
                if (_TotalACobrar != value) {
                    _TotalACobrar = value;
                    RaisePropertyChanged(lblMontoTotalACobrarPropertyName);
                    RaisePropertyChanged(TotalACobrarPropertyName);
                    RaisePropertyChanged(lblMontoTotalCobradoPropertyName);
                    RaisePropertyChanged(DiferenciaPropertyName);
                    RaisePropertyChanged(ColorDiferenciaPropertyName);
                }
            }
        }
        
        public string lblMontoTotalACobrar {
            get {
                return LibConvert.NumToString(TotalACobrar, 2);
            }
            set {

            }
        }

        public decimal  TotalCobrado {
            get {
                return  MontoEfectivo + TotalCobradoInicial;  
            }
            set {
                if (_TotalCobrado != value) {
                    _TotalCobrado =   value;
                    RaisePropertyChanged(TotalCobradoPropertyName);
                    RaisePropertyChanged(lblMontoTotalCobradoPropertyName);
                    RaisePropertyChanged(DiferenciaPropertyName);
                    RaisePropertyChanged(ColorDiferenciaPropertyName);
                }
            }
        }

        decimal _TotalCobradoInicial;
        public decimal TotalCobradoInicial {
            get {
                return _TotalCobradoInicial;
            }
            set {
                if (_TotalCobradoInicial != value) {
                    _TotalCobradoInicial = value;
                    RaisePropertyChanged(TotalCobradoPropertyName);
                    RaisePropertyChanged(lblMontoTotalCobradoPropertyName);
                }
            }
        }

        public decimal SumarMontoCobrado {
            get {
                return MontoEfectivo;
            }
        }

        public decimal  MontoDiferencia {
            get {
                return LibMath.RoundToNDecimals(TotalACobrar - TotalCobrado,2); 
            }
            set {
                if (_MontoDiferencia != value) {
                    _MontoDiferencia = value;
                    Diferencia = null;
                    ColorDiferencia = null;
                    RaisePropertyChanged(MontoDiferenciaPropertyName);
                    RaisePropertyChanged(lblMontoDiferenciaPropertyName);
                    RaisePropertyChanged(DiferenciaPropertyName);
                    RaisePropertyChanged(ColorDiferenciaPropertyName);
                }
            }
        }
        
        public string lblMontoDiferencia {
            get {
                return LibConvert.NumToString( MontoDiferencia,2);
            }
            set {
               
            }
        }
        
        public string lblMontoTotalCobrado {
            get {
                return LibConvert.NumToString(TotalCobrado, 2);
            }
            set {

            }
        }

        string _Diferencia;
        public string Diferencia {
            get {
                return _Diferencia;
            }
            set {
                if (MontoDiferencia > 0) {
                    _Diferencia = "Falta";
                } else {
                    _Diferencia = "Cambio";
                }
                RaisePropertyChanged(ColorDiferenciaPropertyName);

            }
        }

        string _ColorDiferencia;
        public string ColorDiferencia {
            get {
                return _ColorDiferencia;
            }
            set {
                if (MontoDiferencia > 0) {
                    _ColorDiferencia = "Red";
                } else {
                    _ColorDiferencia = "Green";
                }
                RaisePropertyChanged(DiferenciaPropertyName);

            }
        }

        public bool EsBotonGrabar {
            get {
                return _EsBotonGrabar;
            }
            set {
                if (EsBotonGrabar != value) {
                    _EsBotonGrabar = value;
                }
            }
        }
        
        public RelayCommand InsertCommand {
            get;
            private set;
        }

        private bool Cerrar { get; set; }
        #endregion //Propiedades
        #region Constructores

        public CobroDeFacturaRapidaEfectivoViewModel()
            : base() {
            _ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
        }
        
        #endregion //Constructores
        #region Metodos Generados

        internal void InitLookAndFeel(string valNumeroFactura, decimal valTotalACobrar, decimal valTotalCobrado, decimal valEfectivo, decimal valTotalCobradoOtrosTipos) {
            NumeroFactura = valNumeroFactura;
            TotalACobrar = valTotalACobrar;
            TotalCobrado = valTotalCobrado + valTotalCobradoOtrosTipos;
            TotalCobradoInicial = valTotalCobradoOtrosTipos;
            MontoEfectivo = valEfectivo;
            Diferencia = "Cambio";
            ColorDiferencia = "Green";
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            InsertCommand = new RelayCommand(ExecuteInsertCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            RibbonData.ApplicationMenuData = new LibRibbonMenuButtonData() {
                IsVisible = false
            };
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection.Insert(0, CreateActionRibbonButton());
            }
        }

        private LibRibbonButtonData CreateActionRibbonButton() {
            LibRibbonButtonData vResult = new LibRibbonButtonData() {
                Label = "Grabar",
                Command = InsertCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F6.png", UriKind.Relative),
                ToolTipDescription = "Guarda los cambios en " + ModuleName + ".",
                ToolTipTitle = "Ejecutar Acción (F6)",
                IsVisible = true,
                KeyTip = "F6"
            };
            return vResult;
        }

        private void ExecuteInsertCommand() {
            try {
                if (!IsValid) {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(new GalacValidationException(Error), ModuleName, ModuleName);
                    return;
                }
                EsBotonGrabar = true;
                RaiseRequestCloseEvent();
            } catch (System.AccessViolationException) {
                throw;
            } catch (GalacException vEx) {
                if (vEx.ExceptionManagementType == eExceptionManagementType.Validation) {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Warning(null, vEx.Message, "Validación de Consistencia");
                } else {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
                }
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }
        #endregion //Metodos Generados
    } //End of class CobroDeFacturaRapidaEfectivoViewModel

} //End of namespace Galac.Adm.Uil.Venta

