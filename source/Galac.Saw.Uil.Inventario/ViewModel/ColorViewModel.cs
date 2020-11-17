using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Uil.Inventario.ViewModel {
    public class ColorViewModel : LibInputViewModelMfc<Color> {
        #region Constantes
        public const string CodigoColorPropertyName = "CodigoColor";
        public const string DescripcionColorPropertyName = "DescripcionColor";
        public const string CodigoLotePropertyName = "CodigoLote";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Color"; }
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

        [LibRequired(ErrorMessage = "El campo Codigo Color es requerido.")]
        [LibGridColum("Código", Width=80)]
        public string  CodigoColor {
            get {
                return Model.CodigoColor;
            }
            set {
                if (Model.CodigoColor != value) {
                    Model.CodigoColor = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoColorPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Descripción es requerido.")]
        [LibGridColum("Descripción", Width=200)]
        public string  DescripcionColor {
            get {
                return Model.DescripcionColor;
            }
            set {
                if (Model.DescripcionColor != value) {
                    Model.DescripcionColor = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionColorPropertyName);
                }
            }
        }

        public string  CodigoLote {
            get {
                return Model.CodigoLote;
            }
            set {
                if (Model.CodigoLote != value) {
                    Model.CodigoLote = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoLotePropertyName);
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

        public bool IsEnabledCodigo {
           get {
              return Action == eAccionSR.Insertar;
           }
        }
        #endregion //Propiedades
        #region Constructores
        public ColorViewModel()
            : this(new Color(), eAccionSR.Insertar) {
        }
        public ColorViewModel(Color initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            DefaultFocusedPropertyName = CodigoColorPropertyName;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(Color valModel) {
            Model.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"); 
            base.InitializeLookAndFeel(valModel);
        }

        protected override Color FindCurrentRecord(Color valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInString("CodigoColor", valModel.CodigoColor, 3);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "ColorGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<Color>, IList<Color>> GetBusinessComponent() {
            return new clsColorNav();
        }
        #endregion //Metodos Generados


    } //End of class ColorViewModel

} //End of namespace Galac.Saw.Uil.Inventario

