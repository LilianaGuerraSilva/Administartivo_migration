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
using Galac.Adm.Brl.Banco;
using Galac.Adm.Ccl.Banco;
using Galac.Adm.Uil.Banco.Properties;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Uil.Banco.ViewModel {
   public class BeneficiarioViewModel : LibInputViewModelMfc<Beneficiario> {
      #region Constantes
      public const string CodigoPropertyName = "Codigo";
      public const string NumeroRIFPropertyName = "NumeroRIF";
      public const string NombreBeneficiarioPropertyName = "NombreBeneficiario";
      public const string TipoDeBeneficiarioPropertyName = "TipoDeBeneficiario";
      public const string NombreOperadorPropertyName = "NombreOperador";
      public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
      #endregion

      #region Propiedades
      public override string ModuleName {
         get { return "Beneficiario"; }
      }

      public int ConsecutivoCompania {
         get {
            return Model.ConsecutivoCompania;
         }
         set {
            if (Model.ConsecutivoCompania != value) {
               Model.ConsecutivoCompania = value;
            }
         }
      }

      public int Consecutivo {
         get {
            return Model.Consecutivo;
         }
         set {
            if (Model.Consecutivo != value) {
               Model.Consecutivo = value;
            }
         }
      }

      [LibRequired(ErrorMessage = "El campo Código es requerido.")]
      [LibGridColum("Código")]
      public string Codigo {
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

      [LibRequired(ErrorMessage = "lblValidacionNumero")]
      [LibGridColum(HeaderResourceName = "lblEtiquetaNumero", HeaderResourceType = typeof(Resources))]
      public string NumeroRIF {
         get {
            return Model.NumeroRIF;
         }
         set {
            if (Model.NumeroRIF != value) {
               Model.NumeroRIF = value;
               IsDirty = true;
               RaisePropertyChanged(NumeroRIFPropertyName);
            }
         }
      }

      [LibRequired(ErrorMessage = "El campo Nombre Beneficiario es requerido.")]
      [LibGridColum("Nombre Beneficiario", Width = 400)]
      public string NombreBeneficiario {
         get {
            return Model.NombreBeneficiario;
         }
         set {
            if (Model.NombreBeneficiario != value) {
               Model.NombreBeneficiario = value;
               IsDirty = true;
               RaisePropertyChanged(NombreBeneficiarioPropertyName);
            }
         }
      }

      public eOrigenBeneficiario Origen {
         get {
            return Model.OrigenAsEnum;
         }
         set {
            if (Model.OrigenAsEnum != value) {
               Model.OrigenAsEnum = value;
            }
         }
      }

      [LibRequired(ErrorMessage = "El campo Tipo de Beneficiario es requerido.")]
      [LibGridColum("Tipo de Beneficiario", eGridColumType.Enum, PrintingMemberPath = "TipoDeBeneficiarioStr", Width = 150)]
      public eTipoDeBeneficiario TipoDeBeneficiario {
         get {
            return Model.TipoDeBeneficiarioAsEnum;
         }
         set {
            if (Model.TipoDeBeneficiarioAsEnum != value) {
               Model.TipoDeBeneficiarioAsEnum = value;
               IsDirty = true;
               RaisePropertyChanged(TipoDeBeneficiarioPropertyName);
            }
         }
      }

      public string NombreOperador {
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

      public DateTime FechaUltimaModificacion {
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

      public eOrigenBeneficiario[] ArrayOrigenBeneficiario {
         get {
            return LibEnumHelper<eOrigenBeneficiario>.GetValuesInArray();
         }
      }

      public eTipoDeBeneficiario[] ArrayTipoDeBeneficiario {
         get {
            return LibEnumHelper<eTipoDeBeneficiario>.GetValuesInArray();
         }
      }
      #endregion //Propiedades

      #region Constructores
      public BeneficiarioViewModel()
         : this(new Beneficiario(), eAccionSR.Insertar) {
      }

      public BeneficiarioViewModel(Beneficiario initModel, eAccionSR initAction)
         : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
         Model.ConsecutivoCompania = Mfc.GetInt("Compania");
         DefaultFocusedPropertyName = CodigoPropertyName;
      }
      #endregion //Constructores

      #region Metodos Generados
      protected override void InitializeLookAndFeel(Beneficiario valModel) {
         base.InitializeLookAndFeel(valModel);
         if (Consecutivo == 0) {
            Consecutivo = GenerarProximoConsecutivo();
         }
      }

      protected override Beneficiario FindCurrentRecord(Beneficiario valModel) {
         if (valModel == null) {
            return null;
         }
         LibGpParams vParams = new LibGpParams();
         vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
         vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
         return BusinessComponent.GetData(eProcessMessageType.SpName, "BeneficiarioGET", vParams.Get()).FirstOrDefault();
      }

      protected override ILibBusinessComponentWithSearch<IList<Beneficiario>, IList<Beneficiario>> GetBusinessComponent() {
         return new clsBeneficiarioNav();
      }

      private int GenerarProximoConsecutivo() {
         int vResult = 0;
         XElement vResulset = GetBusinessComponent().QueryInfo(eProcessMessageType.Message, "ProximoConsecutivo", Mfc.GetIntAsParam("Compania"));
         vResult = LibConvert.ToInt(LibXml.GetPropertyString(vResulset, "Consecutivo"));
         return vResult;
      }
      #endregion //Metodos Generados

       #region Métodos
      public string lblNumeroRif {
          get {
              string vResult = string.Empty;
              if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                  vResult = "Número RIF";
              }else if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                  vResult = "RUC";
              }
              return vResult;
          }
      }
       #endregion

   } //End of class BeneficiarioViewModel

} //End of namespace Galac.Adm.Uil.Banco

