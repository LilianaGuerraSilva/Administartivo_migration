using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using System.Data.SqlClient;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Base.Dal;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Dal.Venta;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Brl.Venta {
    public partial class clsRenglonCobroDeFacturaNav : LibBaseNavDetail<IList<RenglonCobroDeFactura>, IList<RenglonCobroDeFactura>>, IRenglonCobroDeFacturaPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsRenglonCobroDeFacturaNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<RenglonCobroDeFactura>, IList<RenglonCobroDeFactura>> GetDataInstance() {
            return new Galac.Adm.Dal.Venta.clsRenglonCobroDeFacturaDat();
        }
        #endregion //Metodos Generados



        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsFacturaRapidaDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {

            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsFacturacionRapidaDat();
            //if (valCallingModule == "Factura") {
            //    refXmlDocument = LibXml.ToXmlDocument(LibBusiness.ExecuteSelect("",,"",0));
            //    return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.Query , "Adm.Gp_FacturacionRapidaSCH", valXmlParamsExpression);
            //} else {              
            //    return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_FacturacionRapidaSCH", valXmlParamsExpression);
            //}
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsFacturacionRapidaDat();
            //return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_FacturacionRapidaSCH", valXmlParamsExpression);
            return true;

            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsFacturaRapidaDat();
            //return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_FacturaRapidaSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<FacturaRapida>, IList<FacturaRapida>> instanciaDal = new Galac.Adm.Dal.Venta.clsFacturaRapidaDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_FacturaRapidaGetFk", valParameters);
        }



        #region Codigo Ejemplo
        /* Codigo de Ejemplo*/
         LibResponse IRenglonCobroDeFacturaPdn.InsertRenglonCobroDeFactura(IList<RenglonCobroDeFactura> refRecord)  {

             IRenglonCobroDeFacturaPdn c = new clsRenglonCobroDeFacturaNav ();
            try {
                RegisterClient();
                return base.InsertRecord(refRecord, null);
            } catch (SqlException e) {
               // if (LibExceptionMng.IsUniqueKeyViolation(e))
               //     return this.InsertRecord(refRecord, valUseDetail);
                throw;
            }

        }


         
         // ojo samuel

          public  LibResponse InsertChildRenglonCobroDeFactura(int valConsecutivoCompania, string valNumeroFactura,eTipoDocumentoFactura valTipoDocumento, List<RenglonCobroDeFactura> valRecord) {
             //RenglonCobroDeFactura vRenglon = new RenglonCobroDeFactura();
             //Rendicion rendicion = refRecord[0];
              clsRenglonCobroDeFacturaDat c = new clsRenglonCobroDeFacturaDat();
             try {

                 RegisterClient();
                 return c.InsertChild( valConsecutivoCompania,  valNumeroFactura, valTipoDocumento,  valRecord);
             } catch (SqlException e) {
                 // if (LibExceptionMng.IsUniqueKeyViolation(e))
                 //     return this.InsertRecord(refRecord, valUseDetail);
                 throw;
             }

         }


        public XmlReader generarResultadoXml(RenglonCobroDeFactura refRecord) {
            string msj = "<GPResult><DatosContab><Datos><Documento>";
               // +
            //      "<Numero>" + refRecord.Numero + "</Numero>"
            //      + "<Fecha>" + refRecord.FechaCierre.ToShortDateString() + "</Fecha>"
            //      + "<Consecutivo>" + refRecord.Consecutivo + "</Consecutivo>"
            //      + "<FechaAnulacion>" + refRecord.FechaAnulacion + "</FechaAnulacion>"
            //      + "<CodigoCuentaCajaChica>" + refRecord.CodigoCtaBancariaCajaChica + "</CodigoCuentaCajaChica>"
            //      + "<CodigoCuentaBancariaDeReposicion>" + refRecord.CodigoCuentaBancaria + "</CodigoCuentaBancariaDeReposicion>"
            //      + "<MontoCheque>" + refRecord.TotalGastos + "</MontoCheque>"
            //      + "<BeneficiarioCheque>" + refRecord.NombreBeneficiario + "</BeneficiarioCheque>"
            //      + "</Documento></Datos></DatosContab></GPResult>";
            return new XmlTextReader(new System.IO.StringReader(msj));
        }

        //bool IRenglonCobroDeFacturaPdn.InsertDefaultRecord(int valConsecutivoCompania) {
        //    ILibDataComponent<IList<RenglonCobroDeFactura>, IList<RenglonCobroDeFactura>> instanciaDal = new clsRenglonCobroDeFacturaDat();
        //    IList<RenglonCobroDeFactura> vLista = new List<RenglonCobroDeFactura>();
        //    RenglonCobroDeFactura vCurrentRecord = new Galac.Adm.Dal.Venta.RenglonCobroDeFactura();
        //    vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
        //    vCurrentRecord.ConsecutivoCompania = 0;
        //    vCurrentRecord.NumeroFactura = "";
        //    vCurrentRecord.TipoDeDocumentoAsEnum = eTipoDocumentoFactura.Factura;
        //    vCurrentRecord.ConsecutivoRenglon = 0;
        //    vCurrentRecord.CodigoFormaDelCobro = "";
        //    vCurrentRecord.NumeroDelDocumento = "";
        //    vCurrentRecord.CodigoBanco = 0;
        //    vCurrentRecord.Monto = 0;
        //    vCurrentRecord.CodigoPuntoDeVenta = 0;
        //    vCurrentRecord.NumeroDocumentoAprobacion = "";
        //    vLista.Add(vCurrentRecord);
        //    return instanciaDal.Insert(vLista).Success;
        //}

        private List<RenglonCobroDeFactura> ParseToListEntity(XElement valXmlEntity) {
            List<RenglonCobroDeFactura> vResult = new List<RenglonCobroDeFactura>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                RenglonCobroDeFactura vRecord = new RenglonCobroDeFactura();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroFactura"), null))) {
                    vRecord.NumeroFactura = vItem.Element("NumeroFactura").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDeDocumento"), null))) {
                    vRecord.TipoDeDocumento = vItem.Element("TipoDeDocumento").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoRenglon"), null))) {
                    vRecord.ConsecutivoRenglon = LibConvert.ToInt(vItem.Element("ConsecutivoRenglon"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoFormaDelCobro"), null))) {
                    vRecord.CodigoFormaDelCobro = vItem.Element("CodigoFormaDelCobro").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDelDocumento"), null))) {
                    vRecord.NumeroDelDocumento = vItem.Element("NumeroDelDocumento").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoBanco"), null))) {
                    vRecord.CodigoBanco = LibConvert.ToInt(vItem.Element("CodigoBanco"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Monto"), null))) {
                    vRecord.Monto = LibConvert.ToDec(vItem.Element("Monto"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoPuntoDeVenta"), null))) {
                    vRecord.CodigoPuntoDeVenta = LibConvert.ToInt(vItem.Element("CodigoPuntoDeVenta"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDocumentoAprobacion"), null))) {
                    vRecord.NumeroDocumentoAprobacion = vItem.Element("NumeroDocumentoAprobacion").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        
        #endregion //Codigo Ejemplo


    } //End of class clsRenglonCobroDeFacturaNav

} //End of namespace Galac..Brl.ComponenteNoEspecificado

