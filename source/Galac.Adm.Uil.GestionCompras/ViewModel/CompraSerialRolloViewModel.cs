using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


namespace Galac.Adm.Uil.GestionCompras.ViewModel {
    public class CompraSerialRolloViewModel : LibGenericViewModel  {
        #region Constantes        
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Compra"; }
        }

        public CompraDetalleSerialRolloMngViewModel DetailCompraDetalleSerialRollo {
            get;
            set;
        }

        Saw.Ccl.Inventario.eTipoArticuloInv _TipoArticuloInv;

        public Saw.Ccl.Inventario.eTipoArticuloInv TipoArticuloInv
        {
            get { return _TipoArticuloInv; }
            set { _TipoArticuloInv = value; }
        }

        public bool IsVisibleTallaColor {
            get {
                return TipoArticuloInv == Saw.Ccl.Inventario.eTipoArticuloInv.UsaTallaColorySerial;
            }
        }

        public string Talla { get; set; }

        public string Color { get; set; }

        public string SinonimoColor {
            get {
                string vResult = "Color";
                if (!LibString.IsNullOrEmpty(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "SinonimoColor"))) {
                    vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "SinonimoColor");
                }
                return vResult;
            }
        }

        public string SinonimoTalla {
            get {
                string vResult = "Talla";
                if (!LibString.IsNullOrEmpty(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "SinonimoTalla"))) {
                    vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "SinonimoTalla");
                }
                return vResult;
            }
        }

        public RelayCommand GrabarCommand {
            get;
            private set;
        }

        #endregion //Propiedades
        #region Constructores
        public CompraSerialRolloViewModel(ObservableCollection<CompraDetalleSerialRolloViewModel> initDetail, Saw.Ccl.Inventario.eTipoArticuloInv initTipoArticuloInv)
            : base() {
                TipoArticuloInv = initTipoArticuloInv;
                InitializeDetails();
            DetailCompraDetalleSerialRollo.Items = initDetail;
        }

        

        private void InitializeDetails() {
            DetailCompraDetalleSerialRollo = new CompraDetalleSerialRolloMngViewModel(new CompraViewModel(), TipoArticuloInv);
        }

        #endregion //Constructores
        #region Metodos Generados

        #endregion //Metodos Generados

        protected override void InitializeCommands() {
            base.InitializeCommands();
            GrabarCommand = new RelayCommand(ExecuteGrabarCommand); 
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection.Insert(0, CreateExecuteActionRibbonButtonData());
            }
        }

        private LibRibbonButtonData CreateExecuteActionRibbonButtonData() {
            LibRibbonButtonData vButton = new LibRibbonButtonData() {
                Label = "Grabar",
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/saveAndClose.png", UriKind.Relative),
                Command = GrabarCommand,
                ToolTipDescription = "Ejecutar Grabar",
                ToolTipTitle = "Grabar" + " (F6)",
                IsVisible = true
            };
            return vButton;
        }

        private void ExecuteGrabarCommand() {
            try {
            
                if (!base.IsValid || !DetailCompraDetalleSerialRollo.IsValid) {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(new GalacValidationException(Error), ModuleName, ModuleName);
                    return;
                }
                DialogResult = true;
                RaiseRequestCloseEvent();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        public override string Error {
            get {
                List<string> vErrors = base.BuildValidationErrors().ToList();
                
                    if (!string.IsNullOrWhiteSpace(DetailCompraDetalleSerialRollo.Error)) {
                        vErrors.Add(string.Format("Detalle Serial y Rollo: \n{0}", DetailCompraDetalleSerialRollo.Error));
                    }
                
                string vResult = string.Join(Environment.NewLine, vErrors.ToArray());
                return vResult;
            }
        }

    } //End of class CompraSerialRolloViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

