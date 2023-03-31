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
using Galac.Adm.Brl.ImprentaDigital;
using Galac.Saw.Ccl.ImprentaDigital;

namespace Galac.Adm.Uil.ImprentaDigital.ViewModel {
    public class EnviarDocumentoViewModel : LibInputViewModel<EnviarDocumento> {
        #region Constantes
        public const string NumeroFacturaPropertyName = "NumeroFactura";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Facturación Digital"; }
        }

        public string  NumeroFactura {
            get {
                return Model.NumeroFactura;
            }
            set {
                if (Model.NumeroFactura != value) {
                    Model.NumeroFactura = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroFacturaPropertyName);
                }
            }
        }
        #endregion //Propiedades
        #region Constructores
        public EnviarDocumentoViewModel()
            : this(new EnviarDocumento(), eAccionSR.Insertar) {
        }
        public EnviarDocumentoViewModel(EnviarDocumento initModel, eAccionSR initAction)
            : base(initModel, initAction) {
            DefaultFocusedPropertyName = NumeroFacturaPropertyName;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(EnviarDocumento valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override EnviarDocumento FindCurrentRecord(EnviarDocumento valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("NumeroFactura", valModel.NumeroFactura, 11);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "EnviarDocumentoGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<EnviarDocumento>, IList<EnviarDocumento>> GetBusinessComponent() {
            return new clsEnviarDocumentoNav();
        }
        #endregion //Metodos Generados


    } //End of class EnviarDocumentoViewModel

} //End of namespace Galac.Adm.Uil.ImprentaDigital

