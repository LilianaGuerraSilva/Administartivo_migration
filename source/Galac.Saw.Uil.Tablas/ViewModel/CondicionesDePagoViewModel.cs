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
    public class CondicionesDePagoViewModel : LibInputViewModelMfc<CondicionesDePago> {
        #region Constantes
        public const string DescripcionPropertyName = "Descripcion";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Condiciones De Pago"; }
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

        public int  Consecutivo {
            get {
                return Model.Consecutivo;
            }
            set {
                if (Model.Consecutivo != value) {
                    Model.Consecutivo = value;
                }
            }
        }

        [LibGridColum("Descripción", MaxLength=150)]
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
        #endregion //Propiedades
        #region Constructores
        public CondicionesDePagoViewModel()
            : this(new CondicionesDePago(), eAccionSR.Insertar) {
            Model.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");


        }
        public CondicionesDePagoViewModel(CondicionesDePago initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = DescripcionPropertyName;
            Model.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(CondicionesDePago valModel) {
            base.InitializeLookAndFeel(valModel);
            if (Consecutivo == 0 ) {
                Consecutivo = GenerarProximoConsecutivo();
            }
        }

        protected override CondicionesDePago FindCurrentRecord(CondicionesDePago valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "CondicionesDePagoGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<CondicionesDePago>, IList<CondicionesDePago>> GetBusinessComponent() {
            return new clsCondicionesDePagoNav();
        }

        private int GenerarProximoConsecutivo() {
            int vResult = 0;
            XElement vResulset = GetBusinessComponent().QueryInfo(eProcessMessageType.Message, "ProximoConsecutivo", Mfc.GetIntAsParam("Compania"));
            vResult = LibConvert.ToInt(LibXml.GetPropertyString(vResulset, "Consecutivo"));
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class CondicionesDePagoViewModel

} //End of namespace Galac.Saw.Uil.Tablas

