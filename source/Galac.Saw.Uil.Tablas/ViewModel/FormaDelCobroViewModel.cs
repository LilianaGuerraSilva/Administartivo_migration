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
    public class FormaDelCobroViewModel : LibInputViewModel<FormaDelCobro> {
        #region Constantes
        public const string CodigoPropertyName = "Codigo";
        public const string NombrePropertyName = "Nombre";
        public const string TipoDePagoPropertyName = "TipoDePago";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Forma Del Cobro"; }
        }

        [LibRequired(ErrorMessage = "El campo Código es requerido.")]
        [LibGridColum("Código")]
        public string  Codigo {
            get {
                return Model.Codigo;
            }
            set {
                if (Model.Codigo != value) {
                    Model.Codigo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoPropertyName);
                }
            }
        }
        
        [LibRequired(ErrorMessage = "El campo Nombre es requerido.")]
        [LibGridColum("Nombre",Width = 330)]
        public string  Nombre {
            get {
                return Model.Nombre;
            }
            set {
                if (Model.Nombre != value) {
                    Model.Nombre = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombrePropertyName);
                }
            }
        }

        [LibGridColum("TipoDePago", eGridColumType.Enum, PrintingMemberPath = "TipoDePagoStr")]
        public eTipoDeFormaDePago  TipoDePago {
            get {
                return Model.TipoDePagoAsEnum;
            }
            set {
                if (Model.TipoDePagoAsEnum != value) {
                    Model.TipoDePagoAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDePagoPropertyName);
                }
            }
        }

        public eTipoDeFormaDePago[] ArrayTipoDeFormaDePago {
            get {
                return LibEnumHelper<eTipoDeFormaDePago>.GetValuesInArray();
            }
        }


        public bool IsEnabledCodigo {
            get {
                return Action == eAccionSR.Insertar;
            }
        }

        #endregion //Propiedades
        #region Constructores
        public FormaDelCobroViewModel()
            : this(new FormaDelCobro(), eAccionSR.Insertar) {
        }
        public FormaDelCobroViewModel(FormaDelCobro initModel, eAccionSR initAction)
            : base(initModel, initAction) {
            DefaultFocusedPropertyName = CodigoPropertyName;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(FormaDelCobro valModel) {
            base.InitializeLookAndFeel(valModel);
            if (LibString.IsNullOrEmpty(Codigo, true)) {
                Codigo = GenerarProximoCodigo();
            }
            IsVisibleLastModifiedRegion = false;

        }

        protected override FormaDelCobro FindCurrentRecord(FormaDelCobro valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Codigo", valModel.Codigo, 5);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "FormaDelCobroGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<FormaDelCobro>, IList<FormaDelCobro>> GetBusinessComponent() {
            return new clsFormaDelCobroNav();
        }

        private string GenerarProximoCodigo() {
            string vResult = string.Empty;
            XElement vResulset = GetBusinessComponent().QueryInfo(eProcessMessageType.Message, "ProximoCodigo", null);
            vResult = LibXml.GetPropertyString(vResulset, "Codigo");
            return vResult;
        }
        #endregion //Metodos Generados

        #region Validaciones

        #endregion //Validaciones
    } //End of class FormaDelCobroViewModel

} //End of namespace Galac.Saw.Uil.Tablas

