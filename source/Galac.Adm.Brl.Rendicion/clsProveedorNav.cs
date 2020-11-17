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
    public partial class clsProveedorNav: LibBaseNav<IList<Proveedor>, IList<Proveedor>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsProveedorNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<Proveedor>, IList<Proveedor>> GetDataInstance() {
            return new Galac.Adm.Dal.CajaChica.clsProveedorDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.dbo.Dal.CajaChica.clsProveedorDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.CajaChica.clsProveedorDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_ProveedorSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<Proveedor>, IList<Proveedor>> instanciaDal = new Galac.Adm.Dal.CajaChica.clsProveedorDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "dbo.Gp_ProveedorGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            switch (valModule) {
                case "Proveedor":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                //case "Tabla Retencion":
                //    vPdnModule = new Galac.Adm.Brl.TablaRetencion.clsTablaRetencionNav();
                //    vResult = vPdnModule.GetDataForList("Proveedor", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                //case "Tipo Proveedor":
                //    vPdnModule = new Galac.Saw.Brl.Tablas.clsTipoProveedorNav();
                //    vResult = vPdnModule.GetDataForList("Proveedor", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                //case "Cuenta":
                //    vPdnModule = new Galac.dbo.Brl.Cuenta.clsCuentaNav();
                //    vResult = vPdnModule.GetDataForList("Proveedor", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }
        #endregion //Metodos Generados
        /* Codigo de Ejemplo

        bool IProveedorPdn.InsertarRegistroPorDefecto(int valConsecutivoCompania) {
            ILibDataComponent<IList<Proveedor>, IList<Proveedor>> instanciaDal = new clsProveedorDat();
            IList<Proveedor> vLista = new List<Proveedor>();
            Proveedor vCurrentRecord = new Proveedor();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.CodigoProveedor = "";
            vCurrentRecord.NombreProveedor = "";
            vCurrentRecord.Contacto = "";
            vCurrentRecord.NumeroRIF = "";
            vCurrentRecord.NumeroNIT = "";
            vCurrentRecord.TipoDePersonaAsEnum = eTipodePersonaRetencion.PJ_Domiciliada;
            vCurrentRecord.CodigoRetencionUsual = "";
            vCurrentRecord.Telefonos = "";
            vCurrentRecord.Direccion = "";
            vCurrentRecord.Fax = "";
            vCurrentRecord.Email = "";
            vCurrentRecord.TipodeProveedor = "";
            vCurrentRecord.TipoDeProveedorDeLibrosFiscalesAsEnum = eTipoDeProveedorDeLibrosFiscales.ConRif;
            vCurrentRecord.PorcentajeRetencionIVA = 0;
            vCurrentRecord.CuentaContableCxP = "";
            vCurrentRecord.CuentaContableGastos = "";
            vCurrentRecord.CuentaContableAnticipo = "";
            vCurrentRecord.CodigoLote = "";
            vCurrentRecord.Beneficiario = "";
            vCurrentRecord.UsarBeneficiarioImpCheqAsBool = false;
            vCurrentRecord.TipoDocumentoIdentificacionAsEnum = eTipoDocumentoIdentificacion.RUC;
            vCurrentRecord.EsAgenteDeRetencionIvaAsBool = false;
            vCurrentRecord.Nombre = "";
            vCurrentRecord.ApellidoPaterno = "";
            vCurrentRecord.ApellidoMaterno = "";
            vCurrentRecord.CodigoContribuyente = "";
            vCurrentRecord.NombreOperador = "";
            vCurrentRecord.FechaUltimaModificacion = LibDate.Today();
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<Proveedor> ParseToListEntity(XElement valXmlEntity) {
            List<Proveedor> vResult = new List<Proveedor>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                Proveedor vRecord = new Proveedor();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoProveedor"), null))) {
                    vRecord.CodigoProveedor = vItem.Element("CodigoProveedor").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NombreProveedor"), null))) {
                    vRecord.NombreProveedor = vItem.Element("NombreProveedor").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Contacto"), null))) {
                    vRecord.Contacto = vItem.Element("Contacto").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroRIF"), null))) {
                    vRecord.NumeroRIF = vItem.Element("NumeroRIF").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroNIT"), null))) {
                    vRecord.NumeroNIT = vItem.Element("NumeroNIT").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDePersona"), null))) {
                    vRecord.TipoDePersona = vItem.Element("TipoDePersona").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoRetencionUsual"), null))) {
                    vRecord.CodigoRetencionUsual = vItem.Element("CodigoRetencionUsual").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Telefonos"), null))) {
                    vRecord.Telefonos = vItem.Element("Telefonos").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Direccion"), null))) {
                    vRecord.Direccion = vItem.Element("Direccion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Fax"), null))) {
                    vRecord.Fax = vItem.Element("Fax").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Email"), null))) {
                    vRecord.Email = vItem.Element("Email").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipodeProveedor"), null))) {
                    vRecord.TipodeProveedor = vItem.Element("TipodeProveedor").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDeProveedorDeLibrosFiscales"), null))) {
                    vRecord.TipoDeProveedorDeLibrosFiscales = vItem.Element("TipoDeProveedorDeLibrosFiscales").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeRetencionIVA"), null))) {
                    vRecord.PorcentajeRetencionIVA = LibConvert.ToDec(vItem.Element("PorcentajeRetencionIVA"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaContableCxP"), null))) {
                    vRecord.CuentaContableCxP = vItem.Element("CuentaContableCxP").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaContableGastos"), null))) {
                    vRecord.CuentaContableGastos = vItem.Element("CuentaContableGastos").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaContableAnticipo"), null))) {
                    vRecord.CuentaContableAnticipo = vItem.Element("CuentaContableAnticipo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoLote"), null))) {
                    vRecord.CodigoLote = vItem.Element("CodigoLote").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Beneficiario"), null))) {
                    vRecord.Beneficiario = vItem.Element("Beneficiario").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("UsarBeneficiarioImpCheq"), null))) {
                    vRecord.UsarBeneficiarioImpCheq = vItem.Element("UsarBeneficiarioImpCheq").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDocumentoIdentificacion"), null))) {
                    vRecord.TipoDocumentoIdentificacion = vItem.Element("TipoDocumentoIdentificacion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("EsAgenteDeRetencionIva"), null))) {
                    vRecord.EsAgenteDeRetencionIva = vItem.Element("EsAgenteDeRetencionIva").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Nombre"), null))) {
                    vRecord.Nombre = vItem.Element("Nombre").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ApellidoPaterno"), null))) {
                    vRecord.ApellidoPaterno = vItem.Element("ApellidoPaterno").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ApellidoMaterno"), null))) {
                    vRecord.ApellidoMaterno = vItem.Element("ApellidoMaterno").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoContribuyente"), null))) {
                    vRecord.CodigoContribuyente = vItem.Element("CodigoContribuyente").Value;
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


    } //End of class clsProveedorNav

} //End of namespace Galac.Dbo.Brl.CajaChica

