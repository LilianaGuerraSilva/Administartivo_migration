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
using Galac.Adm.Brl.DispositivosExternos.BalanzaElectronica;
using Galac.Adm.Ccl.DispositivosExternos;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Timers;
using Galac.Adm.Brl.DispositivosExternos;

namespace Galac.Adm.Uil.DispositivosExternos.ViewModel {
    public class BalanzaTomarPesoViewModel : LibGenericViewModel {
        #region Constantes
        #endregion
        #region Propiedades

        public const string PesoBalanzaDisplayPropertyName = "PesoBalanzaDisplay";
        public const string PesoBalanzaPropertyName = "PesoBalanza";
        string _PesoBalanzaDisplay;        
        decimal _PesoBalanza;
        clsBalanza _insBalanza;
        BackgroundWorker BWorker;
        bool ActivarBWorker = false;
        private bool _GuardarPeso = false;
               

        public RelayCommand TomarPesoCommand {
            get;
            private set;
        }       

        private bool CanExecuteTomarPesoCommand() {
            return true;
        }
        
        public override string ModuleName {
            get { return "Balanza Tomar Peso"; }
        }

        public string PesoBalanzaDisplay {
            get {
                return _PesoBalanzaDisplay;
            }
            private set {
                if (_PesoBalanzaDisplay != value) {
                    _PesoBalanzaDisplay = value;
                    RaisePropertyChanged(PesoBalanzaDisplayPropertyName);
                }
            }
        }

        public decimal PesoBalanza {
            get {
                return _PesoBalanza;
            }
            private set {
                if (_PesoBalanza != value) {
                    _PesoBalanza = value;
                    RaisePropertyChanged(PesoBalanzaPropertyName);
                }
            }
        }

        public bool GuardarPeso {
            get { return _GuardarPeso; }
            set { _GuardarPeso = value; }
        }

        #endregion //Propiedades       
        #region Metodos Generados

        private void InitializeBackgroundWorker() {
            BWorker = new BackgroundWorker();
            BWorker.WorkerSupportsCancellation = true;
            BWorker.WorkerReportsProgress = true;
            BWorker.DoWork += new DoWorkEventHandler(BWorkerDoWork);
            BWorker.ProgressChanged += new ProgressChangedEventHandler(BWorkerProgressChanged);
            BWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BWorkerRunWorkerCompleted);
            if (_insBalanza.abrirConexion()) {
                PesoBalanzaDisplay = "0.00";
                PesoBalanza = 0;
                GuardarPeso = false;
                ActivarBWorker = true;
                BWorker.RunWorkerAsync();
            } else {
                throw new GalacException("Error al abrir el puerto de comunicacíon, revisar conexiones",eExceptionManagementType.Alert);
            }
        }

        public void IniciarBalanza() {
            InitializeBackgroundWorker();
        }

        private void BWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Alert(this, "La acción ha sido cancelada por el dispositivo , revisar estado de la balanza.", "Leer Peso");
                CancelCommand.RaiseCanExecuteChanged();
                RaiseRequestCloseEvent();
            }
            if (e.Error != null) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Error(this, e.Error.Message, "Leer Peso");
                CancelCommand.RaiseCanExecuteChanged();
                RaiseRequestCloseEvent();
            } else {
                CancelCommand.RaiseCanExecuteChanged();
                RaiseRequestCloseEvent();
            }
        }

        private void BWorkerProgressChanged(object sender, ProgressChangedEventArgs e) {

        }

        private void BWorkerDoWork(object sender, DoWorkEventArgs e) {
            try {
                MostrarPeso();
            } catch (SystemException vEx) {
                _insBalanza.cerrarConexion();
                throw vEx;
            }
        }

        private void MostrarPeso() {
            while (ActivarBWorker) {
                PesoBalanzaDisplay = _insBalanza.GetPeso();
                PesoBalanza = DarFormatoNumerico(PesoBalanzaDisplay);
            }
            _insBalanza.cerrarConexion();
        }

        private decimal DarFormatoNumerico(string valPesoIn) {
            decimal vResult = 0;
            string vDecimalSeparator = "";
            int vIndex = 0;
            vDecimalSeparator = LibConvert.CurrentDecimalSeparator();

            if (LibString.S1IsInS2(".", valPesoIn) && vDecimalSeparator != ".") {
                valPesoIn = LibString.Replace(valPesoIn, ".", vDecimalSeparator);
            } else if (LibString.S1IsInS2(",", valPesoIn) && vDecimalSeparator != ",") {
                valPesoIn = LibString.Replace(valPesoIn, ",", vDecimalSeparator);
            }

            if ((vIndex = LibText.IndexOf(LibText.UCase(valPesoIn), "L")) > 0) {
                vResult = LibConvert.ToDec(LibText.SubString(valPesoIn, 0, vIndex));
            } else if ((vIndex = LibText.IndexOf(LibText.UCase(valPesoIn), "K")) > 0) {
                vResult = LibConvert.ToDec(LibText.SubString(valPesoIn, 0, vIndex));
            } else if (LibString.HasOnlyNumbers(valPesoIn)) {
                vResult = LibConvert.ToDec(vIndex);
            }

            return vResult;
        }

        private LibRibbonButtonData CreateGrabarRibbonButtonGroup() {
            LibRibbonButtonData vResult = new LibRibbonButtonData() {
                Label = "Grabar",
                Command = TomarPesoCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F6.png", UriKind.Relative),
                ToolTipDescription = "Guarda los cambios en " + ModuleName + ".",
                ToolTipTitle = "Ejecutar Acción (F6)",
                IsVisible = true,
                KeyTip = "F6"
            };
            return vResult;
        }       

        protected override void InitializeCommands() {
            base.InitializeCommands();
            TomarPesoCommand = new RelayCommand(ExecuteTomarPesoCommand, CanExecuteTomarPesoCommand);           
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            RibbonData.ApplicationMenuData = new LibRibbonMenuButtonData() {
                IsVisible = false
            };

            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection.Insert(0, CreateGrabarRibbonButtonGroup());                
            }
        }

        protected override void InitializeLookAndFeel() {
            base.InitializeLookAndFeel();
            int vConsecutivoBalanza = 0;
            int vConsecutivoCompania = 0;
            try {
                RaiseMoveFocus(PesoBalanzaDisplayPropertyName);
                vConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
                vConsecutivoBalanza = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("FacturaRapida","ConsecutivoBalanza");
                clsBalanzaNav insBalanzaNav = new clsBalanzaNav();
                _insBalanza = insBalanzaNav.CreateBalanza(vConsecutivoCompania,vConsecutivoBalanza);
            } catch(Exception) {
                throw new GalacException(" No se ha seleccionado una balanza para esta caja ",eExceptionManagementType.Validation);
            }
        }

        public void ExecuteTomarPesoCommand() {
            if(PesoBalanza > 0) {
                if(LibMessages.MessageBox.YesNo(this,"¿Peso correcto?","")) {
                    ActivarBWorker = false;
                    GuardarPeso = true;
                }
            } else {
                if(LibMessages.MessageBox.YesNo(this,"El peso es cero, tomar este valor?","")) {
                    ActivarBWorker = false;
                    GuardarPeso = false;
                }
            }
        }              

        public override void OnClosed() {
            if (ActivarBWorker) {
                ActivarBWorker = false;
            }

            if (_insBalanza != null) {               
                BWorker = null;
            }
        }

        public bool ComprobarEstado() {
            bool vResult=false;           
            vResult = _insBalanza.VerficarEstado();            
            return vResult;
        }
        #endregion //Metodos Generados

    } //End of class BalanzaTomarPesoViewModel

} //End of namespace Galac.Adm.Uil.DispositivosExternos

