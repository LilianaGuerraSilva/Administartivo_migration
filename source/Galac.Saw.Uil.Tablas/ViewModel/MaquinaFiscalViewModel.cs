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
using Galac.Saw.Brl.Tablas;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Uil.Tablas.ViewModel {
    public class MaquinaFiscalViewModel : LibInputViewModelMfc<MaquinaFiscal> {
        #region Constantes
        public const string ConsecutivoMaquinaFiscalPropertyName = "ConsecutivoMaquinaFiscal";
        public const string DescripcionPropertyName = "Descripcion";
        public const string NumeroRegistroPropertyName = "NumeroRegistro";
        public const string StatusPropertyName = "Status";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Máquina Fiscal"; }
        }

        public int  ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if (Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Consecutivo es requerido.")]
        [LibGridColum("Consecutivo")]
        public string  ConsecutivoMaquinaFiscal {
            get {
                return Model.ConsecutivoMaquinaFiscal;
            }
            set {
                if (Model.ConsecutivoMaquinaFiscal != value) {
                    Model.ConsecutivoMaquinaFiscal = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConsecutivoMaquinaFiscalPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Descripción es requerido.")]
        [LibGridColum("Descripción", Width=300)]
        public string  Descripcion {
            get {
                return Model.Descripcion;
            }
            set {
                if (Model.Descripcion != value) {
                    Model.Descripcion = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nro. Registro es requerido.")]
        [LibGridColum("Nro.Registro", Width=250)]
        public string  NumeroRegistro {
            get {
                return Model.NumeroRegistro;
            }
            set {
                if (Model.NumeroRegistro != value) {
                    Model.NumeroRegistro = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroRegistroPropertyName);
                }
            }
        }

        [LibGridColum("Status", eGridColumType.Enum, PrintingMemberPath = "StatusStr")]
        public eStatusMaquinaFiscal  Status {
            get {
                return Model.StatusAsEnum;
            }
            set {
                if (Model.StatusAsEnum != value) {
                    Model.StatusAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(StatusPropertyName);
                }
            }
        }

        public int  LongitudNumeroFiscal {
            get {
                return Model.LongitudNumeroFiscal;
            }
            set {
                if (Model.LongitudNumeroFiscal != value) {
                    Model.LongitudNumeroFiscal = value;
                }
            }
        }

        public string  NombreOperador {
            get {
                return Model.NombreOperador;
            }
            set {
                if (Model.NombreOperador != value) {
                    Model.NombreOperador = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreOperadorPropertyName);
                }
            }
        }

        public DateTime  FechaUltimaModificacion {
            get {
                return Model.FechaUltimaModificacion;
            }
            set {
                if (Model.FechaUltimaModificacion != value) {
                    Model.FechaUltimaModificacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaUltimaModificacionPropertyName);
                }
            }
        }

        public eStatusMaquinaFiscal[] ArrayStatusMaquinaFiscal {
            get {
                return LibEnumHelper<eStatusMaquinaFiscal>.GetValuesInArray();
            }
        }

        public bool IsEnabledConsecutivoMaquina {
            get {
                return Action == eAccionSR.Insertar;
            }
        }
        #endregion //Propiedades
        #region Constructores
        public MaquinaFiscalViewModel()
            : this(new MaquinaFiscal(), eAccionSR.Insertar) {
                //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
                Model.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");


        }
        public MaquinaFiscalViewModel(MaquinaFiscal initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = ConsecutivoMaquinaFiscalPropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(MaquinaFiscal valModel) {
            base.InitializeLookAndFeel(valModel);
            Model.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");

            if (LibString.IsNullOrEmpty(ConsecutivoMaquinaFiscal, true)) {
                ConsecutivoMaquinaFiscal = GenerarProximoConsecutivoMaquinaFiscal();
            }
            if (Action == eAccionSR.Activar) {
                Status = eStatusMaquinaFiscal.Activa;
            } else if (Action == eAccionSR.Desactivar) {
                Status = eStatusMaquinaFiscal.Inactiva;
            }
        }

        protected override MaquinaFiscal FindCurrentRecord(MaquinaFiscal valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInString("ConsecutivoMaquinaFiscal", valModel.ConsecutivoMaquinaFiscal, 9);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "MaquinaFiscalGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<MaquinaFiscal>, IList<MaquinaFiscal>> GetBusinessComponent() {
            return new clsMaquinaFiscalNav();
        }

        private string GenerarProximoConsecutivoMaquinaFiscal() {
            string vResult = string.Empty;
            XElement vResulset = GetBusinessComponent().QueryInfo(eProcessMessageType.Message, "ProximoConsecutivoMaquinaFiscal", Mfc.GetIntAsParam("Compania"));
            vResult = LibXml.GetPropertyString(vResulset, "ConsecutivoMaquinaFiscal");
            return vResult;
        }

        protected override void  ExecuteSpecialAction(eAccionSR valAction){
            if (Action == eAccionSR.Activar || Action == eAccionSR.Desactivar) {
                CloseOnActionComplete = true;
                DialogResult = UpdateRecord();
            }
        }

        private bool SpecializedUpdate() {
            throw new NotImplementedException();
        }
        #endregion //Metodos Generados


    } //End of class MaquinaFiscalViewModel

} //End of namespace Galac.Saw.Uil.Tablas

