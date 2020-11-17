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
    public class UrbanizacionZPViewModel : LibInputViewModel<UrbanizacionZP> {
        #region Constantes
        public const string UrbanizacionPropertyName = "Urbanizacion";
        public const string ZonaPostalPropertyName = "ZonaPostal";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Urbanización - Zona Postal"; }
        }

        [LibGridColum("Urbanización")]
        [LibRequired(ErrorMessage = "El campo Urbanización es requerido.")]
        public string  Urbanizacion {
            get {
                return Model.Urbanizacion;
            }
            set {
                if (Model.Urbanizacion != value) {
                    Model.Urbanizacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(UrbanizacionPropertyName);
                }
            }
        }

        [LibGridColum("Zona Postal")]
        [LibRequired(ErrorMessage = "El campo Zona Postal es requerido.")]
        public string  ZonaPostal {
            get {
                return Model.ZonaPostal;
            }
            set {
                if (Model.ZonaPostal != value) {
                    Model.ZonaPostal = value;
                    IsDirty = true;
                    RaisePropertyChanged(ZonaPostalPropertyName);
                }
            }
        }
        #endregion //Propiedades
        #region Constructores
        public UrbanizacionZPViewModel()
            : this(new UrbanizacionZP(), eAccionSR.Insertar) {
        }
        public UrbanizacionZPViewModel(UrbanizacionZP initModel, eAccionSR initAction)
            : base(initModel, initAction) {
            DefaultFocusedPropertyName = UrbanizacionPropertyName;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(UrbanizacionZP valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override UrbanizacionZP FindCurrentRecord(UrbanizacionZP valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Urbanizacion", valModel.Urbanizacion, 30);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "UrbanizacionZPGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<UrbanizacionZP>, IList<UrbanizacionZP>> GetBusinessComponent() {
            return new clsUrbanizacionZPNav();
        }
        #endregion //Metodos Generados


    } //End of class UrbanizacionZPViewModel

} //End of namespace Galac.Saw.Uil.Tablas

