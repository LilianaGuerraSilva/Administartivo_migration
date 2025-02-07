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
using Galac.Saw.Ccl.Tablas;
using Galac.Contab.Ccl.WinCont;

namespace Galac.Saw.Brl.Tablas {
    public partial class clsOtrosCargosDeFacturaNav: LibBaseNav<IList<OtrosCargosDeFactura>, IList<OtrosCargosDeFactura>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsOtrosCargosDeFacturaNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<OtrosCargosDeFactura>, IList<OtrosCargosDeFactura>> GetDataInstance() {
            return new Galac.Saw.Dal.Tablas.clsOtrosCargosDeFacturaDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.dbo.Dal.Tablas.clsOtrosCargosDeFacturaDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Tablas.clsOtrosCargosDeFacturaDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_OtrosCargosDeFacturaSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<OtrosCargosDeFactura>, IList<OtrosCargosDeFactura>> instanciaDal = new Galac.Saw.Dal.Tablas.clsOtrosCargosDeFacturaDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "dbo.Gp_OtrosCargosDeFacturaGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch(valModule) {
                case "Otros Cargos de Factura":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Cuenta":
                    vPdnModule = new Contab.Brl.WinCont.clsCuentaNav();
                    vResult = vPdnModule.GetDataForList("Cuenta Reglas", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<OtrosCargosDeFactura> refData) {
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IOtrosCargosDeFacturaPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<OtrosCargosDeFactura>, IList<OtrosCargosDeFactura>> instanciaDal = new clsOtrosCargosDeFacturaDat();
            IList<OtrosCargosDeFactura> vLista = new List<OtrosCargosDeFactura>();
            OtrosCargosDeFactura vCurrentRecord = new Galac.Dbo.Dal.TablasOtrosCargosDeFactura();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.Codigo = "";
            vCurrentRecord.Descripcion = "";
            vCurrentRecord.StatusAsEnum = eStatusOtrosCargosyDescuentosDeFactura.Vigente;
            vCurrentRecord.SeCalculaEnBaseAAsEnum = eBaseCalculoOtrosCargosDeFactura.Formula;
            vCurrentRecord.Monto = 0;
            vCurrentRecord.BaseFormulaAsEnum = eBaseFormulaOtrosCargosDeFactura.SubTotal;
            vCurrentRecord.PorcentajeSobreBase = 0;
            vCurrentRecord.Sustraendo = 0;
            vCurrentRecord.ComoAplicaAlTotalFacturaAsEnum = eComoAplicaOtrosCargosDeFactura.suma;
            vCurrentRecord.CuentaContableOtrosCargos = "";
            vCurrentRecord.PorcentajeComision = 0;
            vCurrentRecord.ExcluirDeComisionAsBool = false;
            vCurrentRecord.NombreOperador = "";
            vCurrentRecord.FechaUltimaModificacion = LibDate.Today();
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<OtrosCargosDeFactura> ParseToListEntity(XElement valXmlEntity) {
            List<OtrosCargosDeFactura> vResult = new List<OtrosCargosDeFactura>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                OtrosCargosDeFactura vRecord = new OtrosCargosDeFactura();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Codigo"), null))) {
                    vRecord.Codigo = vItem.Element("Codigo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Descripcion"), null))) {
                    vRecord.Descripcion = vItem.Element("Descripcion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Status"), null))) {
                    vRecord.Status = vItem.Element("Status").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("SeCalculaEnBaseA"), null))) {
                    vRecord.SeCalculaEnBaseA = vItem.Element("SeCalculaEnBaseA").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Monto"), null))) {
                    vRecord.Monto = LibConvert.ToDec(vItem.Element("Monto"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("BaseFormula"), null))) {
                    vRecord.BaseFormula = vItem.Element("BaseFormula").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeSobreBase"), null))) {
                    vRecord.PorcentajeSobreBase = LibConvert.ToDec(vItem.Element("PorcentajeSobreBase"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Sustraendo"), null))) {
                    vRecord.Sustraendo = LibConvert.ToDec(vItem.Element("Sustraendo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ComoAplicaAlTotalFactura"), null))) {
                    vRecord.ComoAplicaAlTotalFactura = vItem.Element("ComoAplicaAlTotalFactura").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaContableOtrosCargos"), null))) {
                    vRecord.CuentaContableOtrosCargos = vItem.Element("CuentaContableOtrosCargos").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeComision"), null))) {
                    vRecord.PorcentajeComision = LibConvert.ToDec(vItem.Element("PorcentajeComision"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ExcluirDeComision"), null))) {
                    vRecord.ExcluirDeComision = vItem.Element("ExcluirDeComision").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NombreOperador"), null))) {
                    vRecord.NombreOperador = vItem.Element("NombreOperador").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaUltimaModificacion"), null))) {
                    vRecord.FechaUltimaModificacion = LibConvert.ToDate(vItem.Element("FechaUltimaModificacion"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo


    } //End of class clsOtrosCargosDeFacturaNav

} //End of namespace Galac.Saw.Brl.Tablas

