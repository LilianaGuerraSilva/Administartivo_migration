using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Base.Dal;
using Galac.Adm.Ccl.Banco;
using Galac.Adm.Dal.Banco;

namespace Galac.Adm.Brl.Banco {
    public partial class clsBeneficiarioNav: LibBaseNav<IList<Beneficiario>, IList<Beneficiario>>, ILibPdn, IBeneficiarioPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsBeneficiarioNav() {
        }
        #endregion //Constructores
      #region Metodos Generados
      protected override ILibDataComponentWithSearch<IList<Beneficiario>, IList<Beneficiario>> GetDataInstance() {
         return new Galac.Adm.Dal.Banco.clsBeneficiarioDat();
      }

      #region Miembros de ILibPdn
      bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
         bool vResult = false;
         //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Banco.clsBeneficiarioDat();
         switch (valCallingModule) {
            default:
               vResult = true;
               break;
         }
         return vResult;
      }

      bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
         ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Banco.clsBeneficiarioDat();
         return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Saw.Gp_BeneficiarioSCH", valXmlParamsExpression);
      }

      System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
         ILibDataComponent<IList<Beneficiario>, IList<Beneficiario>> instanciaDal = new Galac.Adm.Dal.Banco.clsBeneficiarioDat();
         return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Saw.Gp_BeneficiarioGetFk", valParameters);
      }
      #endregion //Miembros de ILibPdn

      protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
         bool vResult = false;
         switch (valModule) {
            case "Beneficiario":
               vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
               break;
            default: throw new NotImplementedException();
         }
         return vResult;
      }
      #endregion //Metodos Generados

      private void RegistraCliente() {
         if (WorkWithRemoting) {
            _Db = (ILibDataComponentWithSearch<IList<Beneficiario>, IList<Beneficiario>>)RegisterType();
         } else {
            _Db = new Galac.Adm.Dal.Banco.clsBeneficiarioDat();
         }
      }

      bool IBeneficiarioPdn.InsertaBeneficiariosDeNomina(XElement valItemMaster, int valConsecutivoCompania) {
         bool vResult = true;
         IList<Beneficiario> vListBeneficiarios = new List<Beneficiario>();
         vListBeneficiarios = ParseDetailToList(valItemMaster, valConsecutivoCompania);
         if (vListBeneficiarios.Count > 0) {
            ILibDataComponent<IList<Beneficiario>, IList<Beneficiario>> instanciaDal = new Galac.Adm.Dal.Banco.clsBeneficiarioDat();
            vResult = instanciaDal.SpecializedUpdate(vListBeneficiarios, "Nomina").Success;
         }
         return vResult;
      }

      IList<Beneficiario> ParseDetailToList(XElement valItemMaster, int valConsecutivoCompania) {
         IList<Beneficiario> vDetailList = new List<Beneficiario>();
         foreach (XElement vItemDetail in valItemMaster.Descendants("GpDetailRenglonSolicitudesDePago")) {
            Beneficiario insDetail = new Beneficiario();
            if (!(System.NullReferenceException.ReferenceEquals(vItemDetail.Element("CedulaDeIdentidad"), null))) {
               insDetail.ConsecutivoCompania = valConsecutivoCompania;
               insDetail.Codigo = vItemDetail.Element("CedulaDeIdentidad").Value;
               insDetail.NombreBeneficiario = vItemDetail.Element("NombreBeneficiario").Value;
               insDetail.NumeroRIF = vItemDetail.Element("CedulaDeIdentidad").Value;
               insDetail.OrigenAsEnum = eOrigenBeneficiario.Nomina;
               insDetail.TipoDeBeneficiarioAsEnum = eTipoDeBeneficiario.PersonaNatural;
               vDetailList.Add(insDetail);
            }

         }
         return vDetailList;
      }

      public Beneficiario BeneficiarioGenerico(int valConsecutivoCompania) {
         LibGpParams vParams = new LibGpParams();
         Beneficiario vResult = new Beneficiario();
         IList<Beneficiario> ListBeneficiario = new List<Beneficiario>();
         int vConsecutivoBeneficiario;
         RegistraCliente();
         ILibDataComponent<IList<Beneficiario>, IList<Beneficiario>> instanciaDal = new Galac.Adm.Dal.Banco.clsBeneficiarioDat();

         vConsecutivoBeneficiario = ((IBeneficiarioDat)_Db).BeneficiarioGenerico(valConsecutivoCompania);
         vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
         vParams.AddInInteger("Consecutivo", vConsecutivoBeneficiario);
         ListBeneficiario = instanciaDal.GetData(eProcessMessageType.SpName, "BeneficiarioGET", vParams.Get());
         if (ListBeneficiario.Count > 0) {
            vResult = ListBeneficiario[0];
         } else {
            vResult = null;
         }
         return vResult;
      }

      int IBeneficiarioPdn.ConsecutivoBeneficiarioGenerico(int valConsecutivoCompania) {
         LibGpParams vParams = new LibGpParams();
         int vResult;
         RegistraCliente();
         vResult = ((IBeneficiarioDat)_Db).BeneficiarioGenerico(valConsecutivoCompania);
         return vResult;
      }

      int IBeneficiarioPdn.ConsecutivoBeneficiarioGenericoParaCrearEmpresa(int valConsecutivoCompania) {
         LibGpParams vParams = new LibGpParams();
         int vResult;
         RegistraCliente();
         vResult = ((IBeneficiarioDat)_Db).BeneficiarioGenericoParaCrearEmpresa(valConsecutivoCompania);
         return vResult;
      }

      XElement IBeneficiarioPdn.ListadoBeneficiarios(StringBuilder valXmlParamsExpression) {
         ILibDataComponent<IList<Beneficiario>, IList<Beneficiario>> instanciaDal = new Galac.Adm.Dal.Banco.clsBeneficiarioDat();
         return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Saw.Gp_BeneficiarioSELECTList", valXmlParamsExpression);
      }

      XElement IBeneficiarioPdn.FindBeneficiarioBy(int valConsecutivoCompania, string valCodigo) {
         return null;
      }

   } //End of class clsBeneficiarioNav

}//End of namespace Galac.Adm.Brl.Banco

