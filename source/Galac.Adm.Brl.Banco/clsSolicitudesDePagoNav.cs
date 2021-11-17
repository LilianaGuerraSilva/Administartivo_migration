using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using Galac.Adm.Ccl.Banco;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Brl.Banco {
    public partial class clsSolicitudesDePagoNav : LibBaseNavMaster<IList<SolicitudesDePago>, IList<SolicitudesDePago>>, ILibPdn, ISolicitudesDePagoPdn {
        #region Variables

        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsSolicitudesDePagoNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataMasterComponentWithSearch<IList<SolicitudesDePago>, IList<SolicitudesDePago>> GetDataInstance() {
            return new Galac.Adm.Dal.Banco.clsSolicitudesDePagoDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Banco.clsSolicitudesDePagoDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Banco.clsSolicitudesDePagoDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Saw.Gp_SolicitudesDePagoSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>> instanciaDal = new Galac.Adm.Dal.Banco.clsSolicitudesDePagoDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Saw.Gp_SolicitudesDePagoGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Solicitudes De Pago":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                //case "Cuenta Bancaria":
                //    vPdnModule = new Galac.Adm.Brl.Banco.clsCuentaBancariaNav();
                //    vResult = vPdnModule.GetDataForList("Solicitudes De Pago", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                case "Beneficiario":
                    vPdnModule = new Galac.Adm.Brl.Banco.clsBeneficiarioNav();
                    vResult = vPdnModule.GetDataForList("Solicitudes De Pago", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: break;
            }
            return vResult;
        }
        #endregion //Metodos Generados

        bool ISolicitudesDePagoPdn.InsertoSolicitudesDePagoDesdeNomina(XmlReader valXmlEntityReader, int valConsecutivoCompania, string valCuentaBancariaGenerica) {
            bool vResult = false;
            vResult = InsertRecord(ParseToEntity(valXmlEntityReader, valConsecutivoCompania, valCuentaBancariaGenerica));
            return vResult;
        }

        bool ISolicitudesDePagoPdn.EliminarSolicitudesDePagoDesdeNomina(string valNumeroDocumentoOrigen, int valConsecutivoCompania) {
            return EliminarRecord(SolicitudesDePago(valConsecutivoCompania, GetConsecutivoSolictud(valConsecutivoCompania, valNumeroDocumentoOrigen)));
        }

       string  ISolicitudesDePagoPdn.GetSolicitudDePago(string valNumeroDocumentoOrigen, int valConsecutivoCompania) {
            return GetSolicitudesDePago(valConsecutivoCompania, GetConsecutivoSolictud(valConsecutivoCompania, valNumeroDocumentoOrigen)).ToString() ;
        }



        int ISolicitudesDePagoPdn.GenerarProximoConsecutivoSolicitud(int valConsecutivoCompania) {
            LibGpParams vParams = new LibGpParams();
            int vResult = 0;
            ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>> instanciaDal = new Galac.Adm.Dal.Banco.clsSolicitudesDePagoDat();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            XElement vResulset = instanciaDal.QueryInfo(eProcessMessageType.Message, "ProximoConsecutivoSolicitud", vParams.Get());
            vResult = LibConvert.ToInt(LibXml.GetPropertyString(vResulset, "ConsecutivoSolicitud"));
            return vResult;
        }

        private bool InsertRecord(SolicitudesDePago refRecord) {
            bool vResult = false;
            IList<SolicitudesDePago> vBusinessObject = new List<SolicitudesDePago>();
            ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>> instanciaDal = new Galac.Adm.Dal.Banco.clsSolicitudesDePagoDat();
            vBusinessObject.Add(refRecord);
            vResult = instanciaDal.Insert(vBusinessObject, true).Success;
            return vResult;
        }

        private bool EliminarRecord(SolicitudesDePago refRecord) {
            bool vResult = false;
            IList<SolicitudesDePago> vBusinessObject = new List<SolicitudesDePago>();
            ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>> instanciaDal = new Galac.Adm.Dal.Banco.clsSolicitudesDePagoDat();
            vBusinessObject.Add(refRecord);
            vResult = instanciaDal.Delete(vBusinessObject).Success;
            return vResult;
        }
        SolicitudesDePago ParseToEntity(XmlReader valXmlEntityReader, int valConsecutivoCompania, string valCuentaBancariaGenerica) {
            SolicitudesDePago vResult = new SolicitudesDePago();
            XDocument xDoc = XDocument.Load(valXmlEntityReader);
            var vEntity = from vRecord in xDoc.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                vResult.ConsecutivoCompania = valConsecutivoCompania;
                vResult.ConsecutivoSolicitud = GenerarProximoConsecutivoSolicitud(vResult.ConsecutivoCompania);
                if (InsertoBeneficiarios(vItem, vResult.ConsecutivoCompania)) {
                    if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDocumentoOrigen"), null))) {
                        vResult.NumeroDocumentoOrigen = LibConvert.ToInt(vItem.Element("NumeroDocumentoOrigen"));
                    }
                    if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaSolicitud"), null))) {
                        vResult.FechaSolicitud = LibConvert.ToDate(vItem.Element("FechaSolicitud"));
                    }
                    vResult.StatusAsEnum = eStatusSolicitud.PorProcesar;
                    if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("GeneradoPor"), null))) {
                        vResult.GeneradoPorAsEnum = ToEnumFromeSolicitudGeneradaPor(vItem.Element("GeneradoPor").Value);
                    }
                    if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Observaciones"), null))) {
                        vResult.Observaciones = vItem.Element("Observaciones").Value;
                    }
                    clsRenglonSolicitudesDePagoNav insRenglonSolicitudesDePagoNav = new clsRenglonSolicitudesDePagoNav();
                    GBindingList<RenglonSolicitudesDePago> refDetailList = new GBindingList<RenglonSolicitudesDePago>();
                    insRenglonSolicitudesDePagoNav.ParseDetailToList(vItem, ListadoBeneficiarioConConsecutivo(vItem, valConsecutivoCompania), valConsecutivoCompania, ref refDetailList, valCuentaBancariaGenerica);
                    vResult.DetailRenglonSolicitudesDePago = refDetailList;
                }
            }
            return vResult;
        }

        private eSolicitudGeneradaPor ToEnumFromeSolicitudGeneradaPor(string valValor) {
            switch (valValor) {
                case "Cálculos de Nómina": return eSolicitudGeneradaPor.CalculosDeNomina;
                case "Vacaciones": return eSolicitudGeneradaPor.Vacaciones;
                case "Utilidades": return eSolicitudGeneradaPor.Utilidades;
                case "Pago de Intereses": return eSolicitudGeneradaPor.PagoDeIntereses;
                case "Fideicomiso": return eSolicitudGeneradaPor.Fideicomiso;
				case "Liquidación": return eSolicitudGeneradaPor.Liquidacion;
                case "Adelanto de Vacaciones": return eSolicitudGeneradaPor.AdelantoDeVacaciones;
                case "Anticipo de Prestaciones": return eSolicitudGeneradaPor.AnticipoDePrestaciones;
                case "Tickets de Alimentación": return eSolicitudGeneradaPor.TicketsAlimentacion;
                default:
                    return eSolicitudGeneradaPor.NoAplica;
            }
        }

        private bool InsertoBeneficiarios(XElement valItemMaster, int valConsecutivoCompania) {
            bool vResult = false;
            IBeneficiarioPdn vPdnModule;
            vPdnModule = new Galac.Adm.Brl.Banco.clsBeneficiarioNav();
            vResult = vPdnModule.InsertaBeneficiariosDeNomina(valItemMaster, valConsecutivoCompania);
            return vResult;
        }


        XElement ListadoBeneficiarioConConsecutivo(XElement valItemMaster, int valConsecutivoCompania) {
            XElement vResult = null;
            XElement vListado = null ;
            IBeneficiarioPdn vPdnModule;
            vPdnModule = new Galac.Adm.Brl.Banco.clsBeneficiarioNav();
            vListado = valListadoBeneficiarios(valItemMaster, valConsecutivoCompania);
            if(vListado!= null){
                vResult = vPdnModule.ListadoBeneficiarios(ParametrosBeneficiario(valConsecutivoCompania, vListado));
            }
            return vResult;
        }

        private StringBuilder ParametrosBeneficiario(int valConsecutivoCompania, XElement valListadoBeneficiarios) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlDataList", valListadoBeneficiarios);
            vResult = vParams.Get();
            return vResult;
        }


        XElement valListadoBeneficiarios(XElement valItemMaster, int valConsecutivoCompania) {
            XElement vResult = null;
            XElement vXElement= null ;
            IList< Beneficiario> vBusinessObject = new List< Beneficiario>();
            var vEntity = from vRecord in valItemMaster.Descendants("GpDetailRenglonSolicitudesDePago")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CedulaDeIdentidad"), null))) {
                     Beneficiario insBeneficiario = new Beneficiario();
                    insBeneficiario.ConsecutivoCompania = valConsecutivoCompania;
                    insBeneficiario.NumeroRIF = LibConvert.ToStr(vItem.Element("CedulaDeIdentidad").Value);
                   vBusinessObject.Add(insBeneficiario);
                  
                }
            }
            vXElement = new XElement("GpData",
                  from vEntityData in  vBusinessObject
                   select new XElement("GpResult",
                  new XElement("ConsecutivoCompania", vEntityData.ConsecutivoCompania),
                  new XElement("CedulaDeIdentidad", vEntityData.NumeroRIF)));
            vResult = vXElement;
            return vResult;
        }
     

        SolicitudesDePago SolicitudesDePago(int valConsecutivoCompania, int valConsecutivoSolicitud) {
            LibGpParams vParams = new LibGpParams();
            SolicitudesDePago vResult = new SolicitudesDePago();
            IList<SolicitudesDePago> ListSolicitudesDePago = new List<SolicitudesDePago>();
            ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>> instanciaDal = new Galac.Adm.Dal.Banco.clsSolicitudesDePagoDat();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoSolicitud", valConsecutivoSolicitud);
            ListSolicitudesDePago = instanciaDal.GetData(eProcessMessageType.SpName, "SolicitudesDePagoGET", vParams.Get(), false);
            if (ListSolicitudesDePago.Count > 0) {
                vResult = ListSolicitudesDePago[0];
            } else {
                vResult = null;
            }
            return vResult;
        }


        int GetConsecutivoSolictud(int valConsecutivoCompania, string valNumeroDocumentoOrigen) {
            LibGpParams vParams = new LibGpParams();
            int vResult = 0;
            IList<SolicitudesDePago> ListSolicitudesDePago = new List<SolicitudesDePago>();
            ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>> instanciaDal = new Galac.Adm.Dal.Banco.clsSolicitudesDePagoDat();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("NumeroDocumentoOrigen", LibConvert.ToInt(valNumeroDocumentoOrigen));
            ListSolicitudesDePago = instanciaDal.GetData(eProcessMessageType.SpName, "SolicitudesDePagoGETNumeroDocumentoOrigen", vParams.Get(), false);
            if (ListSolicitudesDePago.Count > 0) {
                vResult = ListSolicitudesDePago[0].ConsecutivoSolicitud;
            }
            return vResult;
        }

        string GetNumeroDocumentoOrigen(string valParametros) {
            string vResult = "";
            XDocument xDoc = XDocument.Load(XDocument.Parse(valParametros).CreateReader());
            var vEntity = from vRecord in xDoc.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                vResult = LibConvert.ToStr(vItem.Element("NumeroDocumentoOrigen").Value);
            }
            return vResult;
        }


        string  GetSolicitudesDePago(int valConsecutivoCompania, int valConsecutivoSolicitud) {
            LibGpParams vParams = new LibGpParams();
           string  vResult;
            IList<SolicitudesDePago> ListSolicitudesDePago = new List<SolicitudesDePago>();
            ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>> instanciaDal = new Galac.Adm.Dal.Banco.clsSolicitudesDePagoDat();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoSolicitud", valConsecutivoSolicitud);
            ListSolicitudesDePago = instanciaDal.GetData(eProcessMessageType.SpName, "SolicitudesDePagoGET", vParams.Get(), true);
            if (ListSolicitudesDePago.Count > 0) {
                vResult = ElementDetail(ListSolicitudesDePago[0]).ToString();
            } else {
                vResult = "";
            }
            return vResult;
        }

        XElement ElementDetail(SolicitudesDePago valMaster) {
            XElement vXElement = new XElement("GpData",
                    new XElement("GpResult",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("ConsecutivoSolicitud", valMaster.ConsecutivoSolicitud),
                    new XElement("NumeroDocumentoOrigen", valMaster.NumeroDocumentoOrigen),
                    new XElement("StatusStr", valMaster.StatusAsString),
                    new XElement("GeneradoPorAsString", valMaster.GeneradoPorAsString)));
            return vXElement;
        }

        private int GenerarProximoConsecutivoSolicitud(int valConsecutivoCompania) {
            LibGpParams vParams = new LibGpParams();
            int vResult = 0;
            ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>> instanciaDal = new Galac.Adm.Dal.Banco.clsSolicitudesDePagoDat();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            XElement vResulset = instanciaDal.QueryInfo(eProcessMessageType.Message, "ProximoConsecutivoSolicitud", vParams.Get());
            vResult = LibConvert.ToInt(LibXml.GetPropertyString(vResulset, "ConsecutivoSolicitud"));
            return vResult;
        }
    } //End of class clsSolicitudesDePagoNav

}//End of namespace Galac.Adm.Brl.Banco

