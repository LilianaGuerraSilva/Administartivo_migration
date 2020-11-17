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
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Brl.CajaChica {
    public partial class clsAlicuotaIVANav: LibBaseNav<IList<AlicuotaIVA>, IList<AlicuotaIVA>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsAlicuotaIVANav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<AlicuotaIVA>, IList<AlicuotaIVA>> GetDataInstance() {
            return new Galac.Adm.Dal.CajaChica.clsAlicuotaIVADat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.dbo.Dal.CajaChica.clsAlicuotaIVADat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.CajaChica.clsAlicuotaIVADat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_AlicuotaIVASCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<AlicuotaIVA>, IList<AlicuotaIVA>> instanciaDal = new Galac.Adm.Dal.CajaChica.clsAlicuotaIVADat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "dbo.Gp_AlicuotaIVAGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            switch (valModule) {
                case "Alicuota IVA":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }
        #endregion //Metodos Generados
        /* Codigo de Ejemplo

        bool IAlicuotaIVAPdn.InsertarRegistroPorDefecto() {
            ILibDataComponent<IList<AlicuotaIVA>, IList<AlicuotaIVA>> instanciaDal = new clsAlicuotaIVADat();
            IList<AlicuotaIVA> vLista = new List<AlicuotaIVA>();
            AlicuotaIVA vCurrentRecord = new AlicuotaIVA();
            vCurrentRecord.FechaDeInicioDeVigencia = LibDate.Today();
            vCurrentRecord.MontoAlicuotaGeneral = 0;
            vCurrentRecord.MontoAlicuota2 = 0;
            vCurrentRecord.MontoAlicuota3 = 0;
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<AlicuotaIVA> ParseToListEntity(XElement valXmlEntity) {
            List<AlicuotaIVA> vResult = new List<AlicuotaIVA>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                AlicuotaIVA vRecord = new AlicuotaIVA();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaDeInicioDeVigencia"), null))) {
                    vRecord.FechaDeInicioDeVigencia = LibConvert.ToDate(vItem.Element("FechaDeInicioDeVigencia"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoAlicuotaGeneral"), null))) {
                    vRecord.MontoAlicuotaGeneral = LibConvert.ToDec(vItem.Element("MontoAlicuotaGeneral"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoAlicuota2"), null))) {
                    vRecord.MontoAlicuota2 = LibConvert.ToDec(vItem.Element("MontoAlicuota2"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoAlicuota3"), null))) {
                    vRecord.MontoAlicuota3 = LibConvert.ToDec(vItem.Element("MontoAlicuota3"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */

      public decimal GetAlicuotaGeneral(DateTime Fecha) {

         LibGpParams vParams = new LibGpParams();
         vParams.AddInString("SQLWhere", string.Format("FechaDeInicioDeVigencia <= '{0:dd-MM-yyyy}'", Fecha), 50);
         vParams.AddInString("SQLOrderBy", "FechaDeInicioDeVigencia DESC", 50);
         vParams.AddInString("DateFormat", "DMY", 50);

         IList<AlicuotaIVA> lstIva = GetDataInstance().GetData(eProcessMessageType.SpName, "AlicuotaIVASCH", vParams.Get());

         decimal Iva = 0;

         if (lstIva.Count > 0)
            Iva = lstIva[0].MontoAlicuotaGeneral;

         return Iva;
      }

      public decimal CalcularIva(DateTime Fecha, decimal MontoGravable) {
         return CalcularIva(GetAlicuotaGeneral(Fecha) , MontoGravable);
      }

      public decimal CalcularIva(decimal IVA, decimal MontoGravable) {
         return IVA * MontoGravable/100;
      }

   } //End of class clsAlicuotaIVANav

} //End of namespace Galac.Dbo.Brl.CajaChica

