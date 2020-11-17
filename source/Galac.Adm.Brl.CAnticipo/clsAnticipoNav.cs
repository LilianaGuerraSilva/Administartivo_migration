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

using LibGalac.Aos.Catching;
using LibGalac.Aos.Dal;

using Galac.Adm.Ccl.CAnticipo;
using Galac.Adm.Dal.CAnticipo;
using Galac.Adm.Ccl.CajaChica ;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Adm.Ccl.Banco;
//using Galac.Comun.Ccl.TablasGen;
using Galac.Saw.Brl.Cliente;
using Galac.Saw.Ccl.Cliente;
using Galac.Comun.Brl.TablasGen;
using Galac.Adm.Ccl.Venta;


using Entity = Galac.Adm.Ccl.CAnticipo;

namespace Galac.Adm.Brl.CAnticipo {
    public partial class clsAnticipoNav : LibBaseNav<IList<Entity.Anticipo>, IList<Entity.Anticipo>>, Entity.IAnticipoPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsAnticipoNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<Entity.Anticipo>, IList<Entity.Anticipo>> GetDataInstance() {
            return new Galac.Adm.Dal.CAnticipo.clsAnticipoDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.CAnticipo.clsAnticipoDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.CAnticipo.clsAnticipoDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_AnticipoSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<Entity.Anticipo>, IList<Entity.Anticipo>> instanciaDal = new Galac.Adm.Dal.CAnticipo.clsAnticipoDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "dbo.Gp_AnticipoGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Anticipo":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Cliente":
                    vPdnModule = new Galac.Saw.Brl.Cliente.clsClienteNav();
                    vResult = vPdnModule.GetDataForList("Anticipo", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Proveedor":
                    vPdnModule = new Galac.Adm.Brl.GestionCompras.clsProveedorNav();
                    vResult = vPdnModule.GetDataForList("Anticipo", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Moneda":
                    vPdnModule = new Galac.Comun.Brl.TablasGen.clsMonedaNav();
                    vResult = vPdnModule.GetDataForList("Anticipo", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Cuenta Bancaria":
                    vPdnModule = new Galac.Adm.Brl.Banco.clsCuentaBancariaNav();
                    vResult = vPdnModule.GetDataForList("Anticipo", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Concepto Bancario":
                    vPdnModule = new Galac.Adm.Brl.Banco.clsConceptoBancarioNav();
                    vResult = vPdnModule.GetDataForList("Anticipo", ref refXmlDocument, valXmlParamsExpression);
                    break;
                //case "3.1.- Cotización":
                //    vPdnModule = new Galac.Comun.Brl.SttDef.clsCotizacionNav();
                //    vResult = vPdnModule.GetDataForList("Anticipo", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                case "Reposición de Caja Chica":
                    vPdnModule = new Galac.Adm.Brl.CajaChica.clsRendicionNav();
                    vResult = vPdnModule.GetDataForList("Anticipo", ref refXmlDocument, valXmlParamsExpression);
                    break;
                //case "Caja":
                //    vPdnModule = new Galac.dbo.Brl.ComponenteNoEspecificado.clsCajaNav();
                //    vResult = vPdnModule.GetDataForList("Anticipo", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        XElement Entity.IAnticipoPdn.FindByConsecutivoAnticipo(int valConsecutivoCompania, int valConsecutivoAnticipo) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoAnticipo", valConsecutivoAnticipo);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Adm.Anticipo");
            SQL.AppendLine("WHERE ConsecutivoAnticipo = @ConsecutivoAnticipo");
            SQL.AppendLine("AND ConsecutivoCompania = @ConsecutivoCompania");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }
        #endregion //Metodos Generados


        bool Entity.IAnticipoPdn.GenerarAnticiposCobrados(string valNumeroDeCobranza, string valcodigocliente, string valSimboloMoneda, List<RenglonCobroDeFactura> ListDeCobro) {
           
            bool vResult = false;
            int vConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            decimal mMontoCobranza;
            string vNumeroAnticipo ;
            int iSecuencial = 0;
            clsAnticipoDat insAnticipoDat = new clsAnticipoDat();
            foreach (var vitem in ListDeCobro.Where(p => p.CodigoFormaDelCobro == eFormaDeCobro.Anticipo.GetDescription(1))) {
                mMontoCobranza = LibConvert.ToDec(vitem.Monto);
                vNumeroAnticipo = vitem.NumeroDelDocumento;
                iSecuencial = iSecuencial + 1;
                RegisterClient();
                vResult = insAnticipoDat.GenerarAnticipoCobradoActualizado(vConsecutivoCompania, valNumeroDeCobranza, valcodigocliente, mMontoCobranza, 
                          vNumeroAnticipo, iSecuencial, valSimboloMoneda);
            }
            return true;
           }


        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IAnticipoPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<Anticipo>, IList<Anticipo>> instanciaDal = new clsAnticipoDat();
            IList<Anticipo> vLista = new List<Anticipo>();
            Anticipo vCurrentRecord = new Galac.Adm.Dal.CAnticipoAnticipo();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.ConsecutivoAnticipo = 0;
            vCurrentRecord.StatusAsEnum = eStatusAnticipo.Vigente;
            vCurrentRecord.TipoAsEnum = eTipoDeAnticipo.Cobrado;
            vCurrentRecord.Fecha = LibDate.Today();
            vCurrentRecord.Numero = "";
            vCurrentRecord.CodigoCliente = "";
            vCurrentRecord.CodigoProveedor = "";
            vCurrentRecord.Moneda = "";
            vCurrentRecord.Cambio = 0;
            vCurrentRecord.GeneraMovBancarioAsBool = false;
            vCurrentRecord.CodigoCuentaBancaria = "";
            vCurrentRecord.CodigoConceptoBancario = "";
            vCurrentRecord.GeneraImpuestoBancarioAsBool = false;
            vCurrentRecord.FechaAnulacion = LibDate.Today();
            vCurrentRecord.FechaCancelacion = LibDate.Today();
            vCurrentRecord.FechaDevolucion = LibDate.Today();
            vCurrentRecord.Descripcion = "";
            vCurrentRecord.MontoTotal = 0;
            vCurrentRecord.MontoUsado = 0;
            vCurrentRecord.MontoDevuelto = 0;
            vCurrentRecord.MontoDiferenciaEnDevolucion = 0;
            vCurrentRecord.DiferenciaEsIDBAsBool = false;
            vCurrentRecord.EsUnaDevolucionAsBool = false;
            vCurrentRecord.NumeroDelAnticipoDevuelto = 0;
            vCurrentRecord.NumeroCheque = "";
            vCurrentRecord.AsociarAnticipoACotizAsBool = false;
            vCurrentRecord.NumeroCotizacion = "";
            vCurrentRecord.ConsecutivoRendicion = 0;
            vCurrentRecord.NombreOperador = "";
            vCurrentRecord.FechaUltimaModificacion = LibDate.Today();
            vCurrentRecord.CodigoMoneda = "";
            vCurrentRecord.NombreBeneficiario = "";
            vCurrentRecord.ConsecutivoRendicion = 0;
            vCurrentRecord.CodigoBeneficiario = "";
            vCurrentRecord.AsociarAnticipoACajaAsBool = false;
            vCurrentRecord.ConsecutivoCaja = 0;
            vCurrentRecord.NumeroDeCobranzaAsociado = "";
            vCurrentRecord.GeneradoPorAsEnum = eGeneradoPor.Usuario;
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<Anticipo> ParseToListEntity(XElement valXmlEntity) {
            List<Anticipo> vResult = new List<Anticipo>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                Anticipo vRecord = new Anticipo();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoAnticipo"), null))) {
                    vRecord.ConsecutivoAnticipo = LibConvert.ToInt(vItem.Element("ConsecutivoAnticipo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Status"), null))) {
                    vRecord.Status = vItem.Element("Status").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Tipo"), null))) {
                    vRecord.Tipo = vItem.Element("Tipo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Fecha"), null))) {
                    vRecord.Fecha = LibConvert.ToDate(vItem.Element("Fecha"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Numero"), null))) {
                    vRecord.Numero = vItem.Element("Numero").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoCliente"), null))) {
                    vRecord.CodigoCliente = vItem.Element("CodigoCliente").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoProveedor"), null))) {
                    vRecord.CodigoProveedor = vItem.Element("CodigoProveedor").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Moneda"), null))) {
                    vRecord.Moneda = vItem.Element("Moneda").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Cambio"), null))) {
                    vRecord.Cambio = LibConvert.ToDec(vItem.Element("Cambio"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("GeneraMovBancario"), null))) {
                    vRecord.GeneraMovBancario = vItem.Element("GeneraMovBancario").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoCuentaBancaria"), null))) {
                    vRecord.CodigoCuentaBancaria = vItem.Element("CodigoCuentaBancaria").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoConceptoBancario"), null))) {
                    vRecord.CodigoConceptoBancario = vItem.Element("CodigoConceptoBancario").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("GeneraImpuestoBancario"), null))) {
                    vRecord.GeneraImpuestoBancario = vItem.Element("GeneraImpuestoBancario").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaAnulacion"), null))) {
                    vRecord.FechaAnulacion = LibConvert.ToDate(vItem.Element("FechaAnulacion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaCancelacion"), null))) {
                    vRecord.FechaCancelacion = LibConvert.ToDate(vItem.Element("FechaCancelacion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaDevolucion"), null))) {
                    vRecord.FechaDevolucion = LibConvert.ToDate(vItem.Element("FechaDevolucion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Descripcion"), null))) {
                    vRecord.Descripcion = vItem.Element("Descripcion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoTotal"), null))) {
                    vRecord.MontoTotal = LibConvert.ToDec(vItem.Element("MontoTotal"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoUsado"), null))) {
                    vRecord.MontoUsado = LibConvert.ToDec(vItem.Element("MontoUsado"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoDevuelto"), null))) {
                    vRecord.MontoDevuelto = LibConvert.ToDec(vItem.Element("MontoDevuelto"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoDiferenciaEnDevolucion"), null))) {
                    vRecord.MontoDiferenciaEnDevolucion = LibConvert.ToDec(vItem.Element("MontoDiferenciaEnDevolucion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("DiferenciaEsIDB"), null))) {
                    vRecord.DiferenciaEsIDB = vItem.Element("DiferenciaEsIDB").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("EsUnaDevolucion"), null))) {
                    vRecord.EsUnaDevolucion = vItem.Element("EsUnaDevolucion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDelAnticipoDevuelto"), null))) {
                    vRecord.NumeroDelAnticipoDevuelto = LibConvert.ToInt(vItem.Element("NumeroDelAnticipoDevuelto"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroCheque"), null))) {
                    vRecord.NumeroCheque = vItem.Element("NumeroCheque").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AsociarAnticipoACotiz"), null))) {
                    vRecord.AsociarAnticipoACotiz = vItem.Element("AsociarAnticipoACotiz").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroCotizacion"), null))) {
                    vRecord.NumeroCotizacion = vItem.Element("NumeroCotizacion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoRendicion"), null))) {
                    vRecord.ConsecutivoRendicion = LibConvert.ToInt(vItem.Element("ConsecutivoRendicion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NombreOperador"), null))) {
                    vRecord.NombreOperador = vItem.Element("NombreOperador").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaUltimaModificacion"), null))) {
                    vRecord.FechaUltimaModificacion = LibConvert.ToDate(vItem.Element("FechaUltimaModificacion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoMoneda"), null))) {
                    vRecord.CodigoMoneda = vItem.Element("CodigoMoneda").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NombreBeneficiario"), null))) {
                    vRecord.NombreBeneficiario = vItem.Element("NombreBeneficiario").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoRendicion"), null))) {
                    vRecord.ConsecutivoRendicion = LibConvert.ToInt(vItem.Element("ConsecutivoRendicion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoBeneficiario"), null))) {
                    vRecord.CodigoBeneficiario = vItem.Element("CodigoBeneficiario").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AsociarAnticipoACaja"), null))) {
                    vRecord.AsociarAnticipoACaja = vItem.Element("AsociarAnticipoACaja").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCaja"), null))) {
                    vRecord.ConsecutivoCaja = LibConvert.ToInt(vItem.Element("ConsecutivoCaja"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDeCobranzaAsociado"), null))) {
                    vRecord.NumeroDeCobranzaAsociado = vItem.Element("NumeroDeCobranzaAsociado").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("GeneradoPor"), null))) {
                    vRecord.GeneradoPor = vItem.Element("GeneradoPor").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo


    } //End of class clsAnticipoNav

} //End of namespace Galac.Adm.Brl.CAnticipo

