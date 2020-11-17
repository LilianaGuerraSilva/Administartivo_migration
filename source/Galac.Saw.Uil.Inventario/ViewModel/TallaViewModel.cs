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
    public class TallaViewModel : LibInputViewModelMfc<Talla> {
        #region Constantes
        public const string CodigoTallaPropertyName = "CodigoTalla";
        public const string DescripcionTallaPropertyName = "DescripcionTalla";
        public const string CodigoLotePropertyName = "CodigoLote";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Talla"; }
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

        [LibRequired(ErrorMessage = "El campo Código Talla es requerido.")]
        [LibGridColum("Código Talla")]
        public string  CodigoTalla {
            get {
                return Model.CodigoTalla;
            }
            set {
                if (Model.CodigoTalla != value) {
                    Model.CodigoTalla = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoTallaPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Descripción es requerido.")]
        [LibGridColum("Descripción Talla")]
        public string  DescripcionTalla {
            get {
                return Model.DescripcionTalla;
            }
            set {
                if (Model.DescripcionTalla != value) {
                    Model.DescripcionTalla = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionTallaPropertyName);
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
        public TallaViewModel()
            : this(new Talla(), eAccionSR.Insertar) {
        }
        public TallaViewModel(Talla initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            DefaultFocusedPropertyName = CodigoTallaPropertyName;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(Talla valModel) {
            Model.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"); 
            base.InitializeLookAndFeel(valModel);
        }

        protected override Talla FindCurrentRecord(Talla valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInString("CodigoTalla", valModel.CodigoTalla, 3);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "TallaGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<Talla>, IList<Talla>> GetBusinessComponent() {
            return new clsTallaNav();
        }
        #endregion //Metodos Generados


    } //End of class TallaViewModel

} //End of namespace Galac.Saw.Uil.Inventario

