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
using Galac.Saw.Brl.Integracion;
using Galac.Saw.Ccl.Integracion;

namespace Galac.Saw.Uil.Integracion.ViewModel {
    public class IntegracionViewModel : LibInputViewModel<IntegracionSaw> {
        #region Constantes
        public const string TipoIntegracionPropertyName = "TipoIntegracion";
        public const string versionPropertyName = "version";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Integración"; }
        }

        public eTipoIntegracion  TipoIntegracion {
            get {
                return Model.TipoIntegracionAsEnum;
            }
            set {
                if (Model.TipoIntegracionAsEnum != value) {
                    Model.TipoIntegracionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoIntegracionPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Version es requerido.")]
        [LibGridColum("Version")]
        public string  version {
            get {
                return Model.version;
            }
            set {
                if (Model.version != value) {
                    Model.version = value;
                    IsDirty = true;
                    RaisePropertyChanged(versionPropertyName);
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

        public eTipoIntegracion[] ArrayeTipoIntegracion {
            get {
                return LibEnumHelper<eTipoIntegracion>.GetValuesInArray();
            }
        }
        #endregion //Propiedades
        #region Constructores
        public IntegracionViewModel()
           : this(new IntegracionSaw(), eAccionSR.Insertar) {
        }
        public IntegracionViewModel(IntegracionSaw initModel, eAccionSR initAction)
            : base(initModel, initAction) {
            DefaultFocusedPropertyName = TipoIntegracionPropertyName;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(IntegracionSaw valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override IntegracionSaw FindCurrentRecord(IntegracionSaw valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInEnum("TipoIntegracion", LibConvert.EnumToDbValue((int)valModel.TipoIntegracionAsEnum));
            vParams.AddInString("version", valModel.version, 8);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "IntegracionGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<IntegracionSaw>, IList<IntegracionSaw>> GetBusinessComponent() {
            return new clsIntegracionSawNav();
        }
        #endregion //Metodos Generados


    } //End of class IntegracionViewModel

} //End of namespace Galac.Saw.Uil.Integracion

