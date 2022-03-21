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
using Galac.Adm.Ccl.CajaChica;
using Galac.Adm.Dal.CajaChica;
using Galac.Adm.Brl.Banco;
using Galac.Adm.Ccl.Banco;
using System.Transactions;
using LibGalac.Aos.Catching;
using System.Windows.Forms;
using Galac.Saw.Brl.Contabilizacion;
using Galac.Saw.Ccl.Contabilizacion;

namespace Galac.Adm.Brl.CajaChica {
    public partial class clsRendicionNav : LibBaseNavMaster<IList<Rendicion>, IList<Rendicion>>, IRendicionPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsRendicionNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataMasterComponentWithSearch<IList<Rendicion>, IList<Rendicion>> GetDataInstance() {
            return new Galac.Adm.Dal.CajaChica.clsRendicionDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.CajaChica.clsRendicionDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.CajaChica.clsRendicionDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_RendicionSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<Rendicion>, IList<Rendicion>> instanciaDal = new Galac.Adm.Dal.CajaChica.clsRendicionDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_RendicionGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Reposición de Caja Chica":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Beneficiario":
                    vPdnModule = new Galac.Adm.Brl.Banco.clsBeneficiarioNav();
                    vResult = vPdnModule.GetDataForList("Reposición de Caja Chica", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Cuenta Bancaria":
                    vPdnModule = new Galac.Adm.Brl.Banco.clsCuentaBancariaNav();
                    vResult = vPdnModule.GetDataForList("Reposición de Caja Chica", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Proveedor":
                    vPdnModule = new Galac.Adm.Brl.GestionCompras.clsProveedorNav();
                    vResult = vPdnModule.GetDataForList("Reposición de Caja Chica", ref refXmlDocument, valXmlParamsExpression);
                    break;
                //case "Cambio":
                //    vPdnModule = new Galac.Saw.Brl.Cambio.clsCambioNav();
                //    vResult = vPdnModule.GetDataForList("Rendiciones", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                case "ContabilizarRendicion":
                    vResult = ContabGetDataForList(ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<Rendicion> refData) {
            //FillWithForeignInfoDetalleDeRendicion(ref refData);
        }
        #region DetalleDeRendicion

        private void FillWithForeignInfoDetalleDeRendicion(ref IList<Rendicion> refData) {
            XElement vInfoConexion = FindInfoProveedor(refData);
            var vListProveedor = (from vRecord in vInfoConexion.Descendants("GpResult")
                                  select new {
                                      ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                      CodigoProveedor = vRecord.Element("CodigoProveedor").Value,
                                      NombreProveedor = vRecord.Element("NombreProveedor").Value
                                  }).Distinct();
            foreach (Rendicion vItem in refData) {
                vItem.DetailDetalleDeRendicion =
                    new System.Collections.ObjectModel.ObservableCollection<DetalleDeRendicion>((
                        from vDetail in vItem.DetailDetalleDeRendicion
                        join vProveedor in vListProveedor
                        on new { CodigoProveedor = vDetail.CodigoProveedor, ConsecutivoCompania = vDetail.ConsecutivoCompania }
                        equals
                        new { CodigoProveedor = vProveedor.CodigoProveedor, ConsecutivoCompania = vProveedor.ConsecutivoCompania }
                        select new DetalleDeRendicion {
                            ConsecutivoCompania = vDetail.ConsecutivoCompania,
                            ConsecutivoRendicion = vDetail.ConsecutivoRendicion,
                            ConsecutivoRenglon = vDetail.ConsecutivoRenglon,
                            NumeroDocumento = vDetail.NumeroDocumento,
                            NumeroControl = vDetail.NumeroControl,
                            Fecha = vDetail.Fecha,
                            CodigoProveedor = vDetail.CodigoProveedor,
                            NombreProveedor = vProveedor.NombreProveedor,
                            MontoExento = vDetail.MontoExento,
                            MontoGravable = vDetail.MontoGravable,
                            MontoIVA = vDetail.MontoIVA,
                            MontoRetencion = vDetail.MontoRetencion,
                            AplicaParaLibroDeComprasAsBool = vDetail.AplicaParaLibroDeComprasAsBool,
                            ObservacionesCxP = vDetail.ObservacionesCxP,
                            GeneradaPorAsEnum = vDetail.GeneradaPorAsEnum,
                            AplicaIvaAlicuotaEspecialAsBool = vDetail.AplicaIvaAlicuotaEspecialAsBool,
                            MontoGravableAlicuotaEspecial1 = vDetail.MontoGravable,
                            MontoIVAAlicuotaEspecial1 = vDetail.MontoIVA,
                            PorcentajeIvaAlicuotaEspecial1 = vDetail.MontoGravable,
                            MontoGravableAlicuotaEspecial2 = vDetail.MontoGravable,
                            MontoIVAAlicuotaEspecial2 = vDetail.MontoIVA,
                            PorcentajeIvaAlicuotaEspecial2 = vDetail.MontoGravable
                        }).ToList<DetalleDeRendicion>());
            }
        }

        private XElement FindInfoProveedor(IList<Rendicion> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (Rendicion vItem in valData) {
                vXElement.Add(FilterDetalleDeRendicionByDistinctProveedor(vItem).Descendants("GpResult"));
            }
            ILibPdn insProveedor = new Galac.Adm.Brl.GestionCompras.clsProveedorNav();
            XElement vXElementResult = insProveedor.GetFk("Rendicion", ParametersGetFKProveedorForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterDetalleDeRendicionByDistinctProveedor(Rendicion valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailDetalleDeRendicion.Distinct()
                select new XElement("GpResult",
                    new XElement("CodigoProveedor", vEntity.CodigoProveedor)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKProveedorForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }
        #endregion //DetalleDeRendicion
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IRendicionPdn.InsertarRegistroPorDefecto(int valConsecutivoCompania) {
            ILibDataComponent<IList<Rendicion>, IList<Rendicion>> instanciaDal = new clsRendicionDat();
            IList<Rendicion> vLista = new List<Rendicion>();
            Rendicion vCurrentRecord = new Galac.Adm.Dal.CajaChicaRendicion();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.Numero = "";
            vCurrentRecord.TipoDeDocumentoAsEnum = eTipoDeDocumentoRendicion.Redicion;
            vCurrentRecord.ConsecutivoBeneficiario = 0;
            vCurrentRecord.FechaApertura = LibDate.Today();
            vCurrentRecord.CodigoCtaBancariaCajaChica = "";
            vCurrentRecord.Descripcion = "";
            vCurrentRecord.StatusRendicionAsEnum = eStatusRendicion.EnProceso;
            vCurrentRecord.FechaCierre = LibDate.Today();
            vCurrentRecord.CodigoCuentaBancaria = "";
            vCurrentRecord.NumeroDocumento = "";
            vCurrentRecord.BeneficiarioCheque = "";
            vCurrentRecord.CodigoConceptoBancario = "";
            vCurrentRecord.FechaAnulacion = LibDate.Today();
            vCurrentRecord.TotalAdelantos = 0;
            vCurrentRecord.TotalGastos = 0;
            vCurrentRecord.TotalIVA = 0;
            vCurrentRecord.Observaciones = "";
            vCurrentRecord.NombreOperador = "";
            vCurrentRecord.FechaUltimaModificacion = LibDate.Today();
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<Rendicion> ParseToListEntity(XElement valXmlEntity) {
            List<Rendicion> vResult = new List<Rendicion>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                Rendicion vRecord = new Rendicion();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Numero"), null))) {
                    vRecord.Numero = vItem.Element("Numero").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDeDocumento"), null))) {
                    vRecord.TipoDeDocumento = vItem.Element("TipoDeDocumento").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoBeneficiario"), null))) {
                    vRecord.ConsecutivoBeneficiario = LibConvert.ToInt(vItem.Element("ConsecutivoBeneficiario"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaApertura"), null))) {
                    vRecord.FechaApertura = LibConvert.ToDate(vItem.Element("FechaApertura"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoCtaBancariaCajaChica"), null))) {
                    vRecord.CodigoCtaBancariaCajaChica = vItem.Element("CodigoCtaBancariaCajaChica").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Descripcion"), null))) {
                    vRecord.Descripcion = vItem.Element("Descripcion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("StatusRendicion"), null))) {
                    vRecord.StatusRendicion = vItem.Element("StatusRendicion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaCierre"), null))) {
                    vRecord.FechaCierre = LibConvert.ToDate(vItem.Element("FechaCierre"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoCuentaBancaria"), null))) {
                    vRecord.CodigoCuentaBancaria = vItem.Element("CodigoCuentaBancaria").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDocumento"), null))) {
                    vRecord.NumeroDocumento = vItem.Element("NumeroDocumento").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("BeneficiarioCheque"), null))) {
                    vRecord.BeneficiarioCheque = vItem.Element("BeneficiarioCheque").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoConceptoBancario"), null))) {
                    vRecord.CodigoConceptoBancario = vItem.Element("CodigoConceptoBancario").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaAnulacion"), null))) {
                    vRecord.FechaAnulacion = LibConvert.ToDate(vItem.Element("FechaAnulacion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TotalAdelantos"), null))) {
                    vRecord.TotalAdelantos = LibConvert.ToDec(vItem.Element("TotalAdelantos"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TotalGastos"), null))) {
                    vRecord.TotalGastos = LibConvert.ToDec(vItem.Element("TotalGastos"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TotalIVA"), null))) {
                    vRecord.TotalIVA = LibConvert.ToDec(vItem.Element("TotalIVA"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Observaciones"), null))) {
                    vRecord.Observaciones = vItem.Element("Observaciones").Value;
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

        private bool ContabGetDataForList(ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.CajaChica.clsRendicionDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_RendicionContabSCH", valXmlParamsExpression);
        }

        private StringBuilder ParametersGetFKCambioForXmlSubSet(XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }

        public LibResponse verificaCXPSonValidas(Rendicion refRecord) {
            clsCxPNav CxpNav = new clsCxPNav();
            LibResponse vResult = new LibResponse();
            vResult.Success = true;
            string msj = "";
            List<DetalleDeRendicion> RegistrosInvalidos = new List<DetalleDeRendicion>();

            if (refRecord.FechaApertura > refRecord.FechaCierre) {
                msj = "<GPResult><Error><ErrorCode>0</ErrorCode><Mensaje>La fecha de apertura no puede ser mayor a la fecha de cierre.</Mensaje></Error></GPResult>";
                vResult.Info = new XmlTextReader(new System.IO.StringReader(msj));
                vResult.Success = false;
                return vResult;
            }

            if (refRecord.DetailDetalleDeRendicion.Count == 1) {
                if (refRecord.DetailDetalleDeRendicion[0].ConsecutivoRendicion == new DetalleDeRendicion().ConsecutivoRendicion) {
                    refRecord.DetailDetalleDeRendicion.Clear();
                }
            }

            if (refRecord.DetailDetalleDeRendicion.Count == 0) {
                msj = "<GPResult><Error><ErrorCode>1</ErrorCode><Mensaje>Para realizar el cierre, debe existir por lo menos una factura asociada.</Mensaje></Error></GPResult>";
                vResult.Info = new XmlTextReader(new System.IO.StringReader(msj));
                vResult.Success = false;
                return vResult;
            }

            List<string> listMsj = new List<string>();
            foreach (DetalleDeRendicion aux1 in refRecord.DetailDetalleDeRendicion) {
                LibGpParams vParams = new LibGpParams();
                vParams.AddInString("SQLWhere", "dbo.Gv_CXP_B1.ConsecutivoCompania = " + refRecord.ConsecutivoCompania +
                " AND  dbo.Gv_CXP_B1.Numero = '" + aux1.NumeroDocumento + "' AND dbo.Gv_CXP_B1.CodigoProveedor = '" + aux1.CodigoProveedor + "'", 200);
                var parametros = vParams.Get();
                List<CxP> auxiliarList = ((List<CxP>)CxpNav.buscarCxp(parametros));
                LibResponse f = new LibResponse();

                if (aux1.Fecha > refRecord.FechaCierre) {
                    RegistrosInvalidos.Add(aux1);
                    aux1.ValidoAsBool = false;
                    //msj += "No se puede ingresar facturas con fecha posterior a la fecha de cierre de la Reposición.\n";
                    listMsj.Add("No se puede ingresar facturas con fecha posterior a la fecha de cierre de la Reposición.\n");
                    aux1.ErrorMsj = "No se puede ingresar facturas con fecha posterior a la fecha de cierre de la Reposición.";
                }

                if (auxiliarList.Count > 0) {
                    RegistrosInvalidos.Add(aux1);
                    aux1.ValidoAsBool = false;
                    //msj += "Alguno de los registros ya existe en Base de datos.\n";
                    listMsj.Add("Alguno de los registros ya existe en Base de datos.\n");
                    aux1.ErrorMsj = "Este registro ya existe en Base de datos..";
                }

                int total = ((refRecord.DetailDetalleDeRendicion).Where<DetalleDeRendicion>(de => de.NumeroDocumento == aux1.NumeroDocumento && de.CodigoProveedor == aux1.CodigoProveedor).Count());
                if (total > 1) {
                    RegistrosInvalidos.Add(aux1);
                    aux1.ValidoAsBool = false;
                    listMsj.Add("Alguno de los registros sen encuentran repetidos.\n");
                    aux1.ErrorMsj = "Este Registro se encuentra Repetido.";
                }
            }

            foreach (string aux in listMsj.Distinct<string>()) {
                msj += aux;
            }

            if (RegistrosInvalidos.Count > 0) {
                vResult.Info = new XmlTextReader(new System.IO.StringReader("<GPResult><Error><ErrorCode>1</ErrorCode><Mensaje>" + msj + "</Mensaje></Error></GPResult>"));
                vResult.Success = false;
                return vResult;
            }

            return vResult;
        }

        CxP generarCXP(DetalleDeRendicion refRecord, DateTime FechaCierre) {
            CxP cxp = new CxP();
            foreach (var p in refRecord.GetType().GetProperties()) {
                if (cxp.GetType().GetProperty(p.Name) != null) {
                    if (refRecord.GetType().GetProperty(p.Name).CanRead && refRecord.GetType().GetProperty(p.Name).CanWrite)
                        cxp.GetType().GetProperty(p.Name).SetValue(cxp, refRecord.GetType().GetProperty(p.Name).GetValue(refRecord, null), null);
                }
            }
            cxp.AnoDeAplicacion = FechaCierre.Year;
            cxp.Fecha = refRecord.Fecha;
            cxp.FechaAnulacion = refRecord.Fecha;
            cxp.FechaAplicacionImpuestoMunicipal = FechaCierre;
            cxp.FechaAplicacionRetIVA = FechaCierre;
            cxp.FechaCancelacion = FechaCierre;
            cxp.FechaUltimaModificacion = FechaCierre;
            cxp.FechaVencimiento = refRecord.Fecha;
            cxp.DiaDeAplicacion = FechaCierre.Day;
            cxp.MesDeAplicacion = FechaCierre.Month;
            cxp.Numero = refRecord.NumeroDocumento;
            cxp.NumeroControl = refRecord.NumeroControl;
            cxp.MontoGravado = refRecord.MontoGravable + refRecord.MontoGravableAlicuotaEspecial1 + refRecord.MontoGravableAlicuotaEspecial2;
            cxp.MontoAbonado = refRecord.MontoExento + refRecord.MontoGravable + refRecord.MontoGravableAlicuotaEspecial1 + refRecord.MontoGravableAlicuotaEspecial2 + refRecord.MontoIVA + refRecord.MontoIVAAlicuotaEspecial1 + refRecord.MontoIVAAlicuotaEspecial2;
            cxp.MontoIva = refRecord.MontoIVA + refRecord.MontoIVAAlicuotaEspecial1 + refRecord.MontoIVAAlicuotaEspecial2;
            cxp.MontoGravableAlicuotaGeneral = refRecord.MontoGravable;
            cxp.MontoIVAAlicuotaGeneral = refRecord.MontoIVA;
            cxp.Observaciones = refRecord.ObservacionesCxP;
            cxp.EstaAsociadoARendicionAsBool = true;
            cxp.StatusAsEnum = eStatusDocumento.Cancelado;
            cxp.AplicaParaLibrodeComprasAsBool = refRecord.AplicaParaLibroDeComprasAsBool;
            cxp.TipoDeCompraAsEnum = eTipoDeCompra.ComprasNacionales;
            cxp.CxPgeneradaPorAsEnum = eGeneradoPor.Rendicion;

            //      Manejo de Moneda.....
            cxp.CodigoMoneda = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("DatosMoneda", "Codigo");
            cxp.Moneda = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("DatosMoneda", "Nombre");
            cxp.CambioABolivares = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("DatosMoneda", "Cambio");
            //-.................................................

            //       ManejoRetenciones   
            cxp.SeContabilRetIvaAsBool = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("DatosRentencion", "SeContabilRetIva");
            cxp.DondeContabilRetIva = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("DatosRentencion", "DondeContabilRetIva");
            cxp.OrigenDeLaRetencionISLR = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("DatosRentencion", "OrigenDeLaRetencionISLR");
            cxp.DondeContabilISLR = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("DatosRentencion", "DondeContabilISLR");
            cxp.ISLRAplicadaEnPagoAsBool = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("DatosRentencion", "ISLRAplicadaEnPago");
            cxp.MontoRetenidoISLR = 0;
            cxp.SeContabilISLRAsBool = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("DatosRentencion", "SeContabilISLR");
            cxp.RetencionAplicadaEnPagoAsBool = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("DatosRentencion", "RetencionAplicadaEnPago"); ;
            cxp.NumeroComprobanteRetencion = "0";
            cxp.OrigenDeLaRetencionAsEnum = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "PuedoUsarOpcionesDeContribuyenteEspecial") ? eDondeSeEfectuaLaRetencionIVA.CxP : eDondeSeEfectuaLaRetencionIVA.NoRetenida;
            //............................................................

            cxp.FechaDeclaracionAduana = FechaCierre;

            cxp.UsaPrefijoSerieAsBool = false;
            cxp.CodigoProveedorOriginalServicio = "";
            cxp.EsUnaCuentaATercerosAsBool = false;
            cxp.AplicaIvaAlicuotaEspecialAsBool = refRecord.AplicaIvaAlicuotaEspecialAsBool;
            cxp.MontoGravableAlicuotaEspecial1 = refRecord.MontoGravableAlicuotaEspecial1;
            cxp.MontoIVAAlicuotaEspecial1 = refRecord.MontoIVAAlicuotaEspecial1;
            cxp.PorcentajeIvaAlicuotaEspecial1 = refRecord.PorcentajeIvaAlicuotaEspecial1;
            cxp.MontoGravableAlicuotaEspecial2 = refRecord.MontoGravableAlicuotaEspecial2;
            cxp.MontoIVAAlicuotaEspecial2 = refRecord.MontoIVAAlicuotaEspecial2;
            cxp.PorcentajeIvaAlicuotaEspecial2 = refRecord.PorcentajeIvaAlicuotaEspecial2;
            return cxp;
        }

        List<CxP> generarCXP(Rendicion refRecord) {
            List<CxP> listaCxp = new List<CxP>();
            foreach (DetalleDeRendicion aux1 in refRecord.DetailDetalleDeRendicion) {
                listaCxp.Add(generarCXP(aux1, refRecord.FechaCierre));
            }
            return listaCxp;
        }

        void EliminarCXP(List<CxP> list) {
            clsCxPNav CxpNav = new clsCxPNav();
            CxpNav.eliminarCXP(list);
        }

        List<CxP> buscarCXPAsociadas(Rendicion refRecord) {
            clsCxPNav CxpNav = new clsCxPNav();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("SQLWhere", "dbo.Gv_CXP_B1.ConsecutivoCompania = " + refRecord.ConsecutivoCompania +
            " AND  dbo.Gv_CXP_B1.EstaAsociadoARendicion = 'S' AND dbo.Gv_CXP_B1.ConsecutivoRendicion = " + refRecord.Consecutivo, 200);
            var parametros = vParams.Get();
            return (List<CxP>)CxpNav.buscarCxp(parametros);
        }

        MovimientoBancario generarMovimientoBancario(
            string Beneficiario,
            decimal CambioABolivares,
            string CodigoConceptoBancario,
            string CodigoCuentaBancaria,
            int consecutivoCompania,
            int consecutivoMovimiento,
            string descripcion,
            string descripcionConceptoBancario,
            DateTime fecha,
            DateTime fechaUltimaModificacion,
            string moneda,
            decimal monto,
            string nombreCuentaBancaria,
            string nombreOperador,
            string numeroMovimientoBancarioRelacionado,
            string nroConciliacion,
            string numeroDocumento,
            bool conciliado,
            eIngresoEgreso tipoConcepto,
            eGeneradoPor generadoPor,
            bool generaImpuestoBancario,
            bool generarAsientoDeRetiroEnCuenta,
            bool imprimirCheque,
            bool insertarInmediatamente,
            decimal AlicuotaImpBancario) {
            clsMovimientoBancarioNav movNav = new clsMovimientoBancarioNav();
            MovimientoBancario mb = new MovimientoBancario() {
                Beneficiario = Beneficiario,
                CambioABolivares = CambioABolivares,
                CodigoConcepto = CodigoConceptoBancario,
                CodigoCtaBancaria = CodigoCuentaBancaria,
                ConsecutivoCompania = consecutivoCompania,
                ConsecutivoMovimiento = consecutivoMovimiento,
                Descripcion = descripcion,
                DescripcionConcepto = descripcionConceptoBancario,
                Fecha = fecha,
                FechaUltimaModificacion = fechaUltimaModificacion,
                Moneda = moneda,
                Monto = monto,
                NombreCtaBancaria = nombreCuentaBancaria,
                NombreOperador = nombreOperador,
                NroMovimientoRelacionado = numeroMovimientoBancarioRelacionado,
                NroConciliacion = nroConciliacion,
                TipoConceptoAsEnum = tipoConcepto,
                NumeroDocumento = numeroDocumento,
                ConciliadoSNAsBool = conciliado,
                GeneradoPorAsEnum = generadoPor,
                GeneraImpuestoBancarioAsBool = generaImpuestoBancario,
                GenerarAsientoDeRetiroEnCuentaAsBool = generarAsientoDeRetiroEnCuenta,
                ImprimirChequeAsBool = imprimirCheque,
                AlicuotaImpBancario = AlicuotaImpBancario };
            if (insertarInmediatamente)
                movNav.Insert(new List<MovimientoBancario>() { mb });
            return mb;
        }

        void actualizarSaldoCuentaBancaria(int consecutivoCompania, MovimientoBancario mb) {
            if (mb == null)
                return;
            Galac.Adm.Ccl.Banco.ICuentaBancariaPdn cuentaNav = new clsCuentaBancariaNav();
            cuentaNav.ActualizaSaldoDisponibleEnCuenta(consecutivoCompania, mb.CodigoCtaBancaria, mb.Monto.ToString(), ((int)mb.TipoConceptoAsEnum).ToString(), ((int)eAccionSR.Insertar), "0", false);
        }

        void actualizarAnticiposUsados(Rendicion refRecord, eAccionSR accion) {
            Galac.Adm.Ccl.CajaChica.IAnticipoPdn AnticipoNav = new clsAnticipoNav();
            foreach (Anticipo ant in refRecord.Adelantos) {
                if (ant.ConsecutivoAnticipo == 0)
                    continue;
                if (accion.Equals(eAccionSR.Cerrar)) {
                    ant.StatusAsEnum = eStatusAnticipo.CompletamenteUsado;
                    ant.MontoUsado = ant.MontoTotal;
                } else if (accion.Equals(eAccionSR.Anular)) {
                    ant.StatusAsEnum = eStatusAnticipo.Vigente;
                    ant.MontoUsado = 0;
                    ant.ConsecutivoRendicion = 0;
                }
                AnticipoNav.actualizar(new List<Anticipo>() { ant });
            }
        }

        void actualizarStatusRendicion(Rendicion refRecord, eStatusRendicion status) {
            ILibDataMasterComponentWithSearch<IList<Rendicion>, IList<Rendicion>> rendicionDat = new Galac.Adm.Dal.CajaChica.clsRendicionDat();
            refRecord.StatusRendicionAsEnum = status;
            rendicionDat.Update(new List<Rendicion>() { refRecord }, false, eAccionSR.Cerrar);
        }

        protected override LibResponse UpdateRecord(IList<Rendicion> refRecord, bool valUseDetail, eAccionSR valAction) {
            LibResponse l = new LibResponse();

            Rendicion rendicion = refRecord[0];
            try {
                ValidarRendicion(rendicion);
            } catch (Exception) {
                throw;
            }

            try {
                using (TransactionScope ts = getScope()) {
                    l = base.UpdateRecord(refRecord, valUseDetail, valAction);
                    ts.Complete();
                }
            } catch (Exception) {
                throw;
            } finally {
                EndScopedBusinessProcess("");
            }

            return l;
        }

        protected override LibResponse InsertRecord(IList<Rendicion> refRecord, bool valUseDetail) {
            Rendicion rendicion = refRecord[0];

            try {
                ValidarRendicion(rendicion);
            } catch (Exception) {
                throw;
            }

            // ASIGNACION DE VALORES POR DEFECTO
            clsBeneficiarioNav beneficiarioNav = new clsBeneficiarioNav();
            rendicion.ConsecutivoBeneficiario = beneficiarioNav.BeneficiarioGenerico(rendicion.ConsecutivoCompania).Consecutivo;
            rendicion.TipoDeDocumentoAsEnum = eTipoDeDocumentoRendicion.Reposicion;
            rendicion.StatusRendicionAsEnum = eStatusRendicion.EnProceso;
            rendicion.Numero = SiguienteNumero(rendicion.ConsecutivoCompania);

            try {
                return base.InsertRecord(refRecord, valUseDetail);
            } catch (SqlException e) {
                if (LibExceptionMng.IsUniqueKeyViolation(e))
                    return this.InsertRecord(refRecord, valUseDetail);
                throw;
            }

        }

        private void ValidarRendicion(Rendicion rendicion) {
            // VALIDACION DE NUMERO DE DELLATE MAYOR A 0
            //if (rendicion.DetailDetalleDeRendicion.Count == 0)
            //    throw new LibGalac.Aos.Catching.GalacValidationException("No es posible guardar una Reposición sin detalle.");

            int renglon = 0;

            foreach (DetalleDeRendicion detalle in rendicion.DetailDetalleDeRendicion) {

                detalle.ConsecutivoRenglon = renglon;
                renglon++;

                // VALIDACION DE NUMEROS DE DOCUMENTOS
                if (LibString.IsNullOrEmpty(LibString.Trim(detalle.NumeroDocumento))) {
                    detalle.ValidoAsBool = false;
                    throw new LibGalac.Aos.Catching.GalacValidationException("El campo Número de Documento en Facturas es requerido ");
                } else if (LibString.IsNullOrEmpty(LibString.Trim(detalle.NumeroControl))) {
                    detalle.ValidoAsBool = false;
                    throw new LibGalac.Aos.Catching.GalacValidationException("El campo Número de Control en Facturas es requerido ");
                } else if (LibString.IsNullOrEmpty(LibString.Trim(detalle.CodigoProveedor))) {
                    detalle.ValidoAsBool = false;
                    throw new LibGalac.Aos.Catching.GalacValidationException("El campo Proveedor en Facturas es requerido ");
                }

                // VALIDACION DE REGISTROS REPETIDOS
                IEnumerable<DetalleDeRendicion> detallesRepetidos = rendicion.DetailDetalleDeRendicion.Where<DetalleDeRendicion>(de => de.NumeroDocumento == detalle.NumeroDocumento && de.CodigoProveedor == detalle.CodigoProveedor);
                if (detallesRepetidos.Count() > 1) {
                    DetalleDeRendicion detalleRepetido = detallesRepetidos.First();
                    detalleRepetido.ValidoAsBool = false;
                    throw new LibGalac.Aos.Catching.GalacValidationException("No es posible Insertar Documentos Repetidos. Número de documento: " + detalleRepetido.NumeroDocumento);
                }

                // VALIDACION DE MONTO TOTAL 0
                if (detalle.MontoExento + detalle.MontoGravable + detalle.MontoIVA + detalle.MontoGravableAlicuotaEspecial1 + detalle.MontoIVAAlicuotaEspecial1 + detalle.MontoGravableAlicuotaEspecial2 + detalle.MontoIVAAlicuotaEspecial2 <= 0) {
                    detalle.ValidoAsBool = false;
                    throw new LibGalac.Aos.Catching.GalacValidationException(string.Format("El monto de la factura {0} debe ser mayor a cero (0)", detalle.NumeroDocumento));
                }
            }
        }

        private void ValidarRendicionParaCerrarAnular(Rendicion rendicion) {
            if (rendicion.DetailDetalleDeRendicion.Count == 0)
                throw new LibGalac.Aos.Catching.GalacValidationException("No es posible cerrar una Reposición sin detalle.");

            foreach (DetalleDeRendicion detalle in rendicion.DetailDetalleDeRendicion) {
                if (rendicion.FechaCierre < detalle.Fecha)
                    throw new LibGalac.Aos.Catching.GalacValidationException(string.Format("La fecha de cierre no puede ser menor a la fecha de la factura {0}", detalle.NumeroDocumento));
            }

            if (ExisteConcialiacion(rendicion))
                throw new LibGalac.Aos.Catching.GalacValidationException("Existe una Conciliación Bancaria cerrada para la fecha de la operación");

            Galac.Adm.Ccl.Banco.ICuentaBancariaPdn cuentaNav = new clsCuentaBancariaNav();
            if (!cuentaNav.EsValidaCuentaBancariaCajaChica(rendicion.ConsecutivoCompania, rendicion.CodigoCtaBancariaCajaChica))
                throw new LibGalac.Aos.Catching.GalacValidationException("Caja Chica seleccionada no es válida");

        }

        bool ExisteConcialiacion(Rendicion refRecord) {
            int vResult;
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("CodigoCuenta", refRecord.CodigoCuentaBancaria, 5);
            vParams.AddInString("CodigoCuentaCajaChica", refRecord.CodigoCtaBancariaCajaChica, 5);
            vParams.AddInInteger("MesDeAplicacion", refRecord.FechaCierre.Month);
            vParams.AddInInteger("AnoDeAplicacion", refRecord.FechaCierre.Year);
            vParams.AddInInteger("Status", 1); // CERRADA
            vParams.AddInInteger("ConsecutivoCompania", refRecord.ConsecutivoCompania);

            RegisterClient();

            string vSql = "SELECT * FROM Conciliacion WHERE (CodigoCuenta = @CodigoCuenta OR  CodigoCuenta = @CodigoCuentaCajaChica) AND MesDeAplicacion = @MesDeAplicacion AND AnoDeAplicacion = @AnoDeAplicacion AND Status = @Status AND ConsecutivoCompania = @ConsecutivoCompania";

            XElement vResultset = _Db.QueryInfo(eProcessMessageType.Query, vSql, vParams.Get());

            if (vResultset != null) {
                vResult = (from vRecord in vResultset.Descendants("GpResult")
                           select vRecord).Count();
            } else {
                vResult = 0;
            }
            return vResult > 0;
        }

        LibResponse IRendicionPdn.cerrar(Rendicion refRecord) {
            LibResponse vResult = new LibResponse();
            clsCxPNav cxpNav = new clsCxPNav();
            List<CxP> ListaCxp = generarCXP(refRecord);
            foreach (var item in ListaCxp) {
                if (item.CxPgeneradaPorAsEnum == eGeneradoPor.Rendicion) {
                    cxpNav.insert(ListaCxp);
                } else if (item.CxPgeneradaPorAsEnum == eGeneradoPor.Usuario) {
                    cxpNav.ActualizarCxPAsociadas(ListaCxp, refRecord.FechaCierre);
                }
            }
            Galac.Adm.Ccl.Banco.ICuentaBancariaPdn cuentaNav = new clsCuentaBancariaNav();
            decimal vAlicuotaIGTF = cuentaNav.ObtieneAlicuotaIGTF(refRecord.ConsecutivoCompania, refRecord.CodigoCuentaBancaria,refRecord.FechaCierre);
            eIngresoEgreso tipoConcepto = eIngresoEgreso.Egreso;
            decimal monto = refRecord.TotalAdelantos - refRecord.TotalGastos - refRecord.TotalRetencion;
            if (monto < 0) {
                tipoConcepto = eIngresoEgreso.Egreso;
                monto = monto * -1;
            } else if (monto > 0) {
                tipoConcepto = eIngresoEgreso.Ingreso;
            }
            MovimientoBancario movimiento = CrearMovimientoBancario(refRecord, monto, Ccl.Banco.eConceptoBancarioPorDefecto.AnticipoCobrado, tipoConcepto, "Cierre Rendición Nro." + refRecord.Numero + ", Del Beneficiario " + refRecord.NombreBeneficiario, refRecord.CodigoCuentaBancaria, refRecord.NombreCuentaBancaria, true, vAlicuotaIGTF);
            actualizarSaldoCuentaBancaria(refRecord.ConsecutivoCompania, movimiento);
            actualizarAnticiposUsados(refRecord, eAccionSR.Cerrar);
            actualizarStatusRendicion(refRecord, eStatusRendicion.Cerrada);
            vResult.Info = generarResultadoXml(refRecord);
            vResult.Success = true;
            return vResult;
        }

        LibResponse IRendicionPdn.anular(Rendicion refRecord) {
            LibResponse vResult = new LibResponse();
            vResult.Success = true;
            List<CxP> list = buscarCXPAsociadas(refRecord);
            clsCxPNav insCxP = new clsCxPNav();
            insCxP.DesasociarCxP(list);
            foreach (CxP cxp in list) {
                EliminarCXP(new List<CxP>() { cxp });
            }
            Galac.Adm.Ccl.Banco.ICuentaBancariaPdn cuentaNav = new clsCuentaBancariaNav();
            decimal vAlicuotaIGTF = cuentaNav.ObtieneAlicuotaIGTF(refRecord.ConsecutivoCompania, refRecord.CodigoCuentaBancaria,refRecord.FechaCierre);
            eIngresoEgreso tipoConcepto = eIngresoEgreso.Egreso;
            decimal monto = refRecord.TotalAdelantos - refRecord.TotalGastos - refRecord.TotalRetencion;
            if (monto > 0) {
                tipoConcepto = eIngresoEgreso.Egreso;
            } else if (monto < 0) {
                tipoConcepto = eIngresoEgreso.Ingreso;
                monto = monto * -1;
            }
            MovimientoBancario movimiento = CrearMovimientoBancario(refRecord, monto, Galac.Adm.Ccl.Banco.eConceptoBancarioPorDefecto.AnticipoCobrado, tipoConcepto, "Anulacion Rendición Nro." + refRecord.Numero + ", Del Beneficiario " + refRecord.NombreBeneficiario, refRecord.CodigoCuentaBancaria, refRecord.NombreCuentaBancaria, true, vAlicuotaIGTF);
            actualizarSaldoCuentaBancaria(refRecord.ConsecutivoCompania, movimiento);
            actualizarAnticiposUsados(refRecord, eAccionSR.Anular);
            actualizarStatusRendicion(refRecord, eStatusRendicion.Anulada);
            vResult.Info = generarResultadoXml(refRecord);
            vResult.Success = true;
            return vResult;
        }


        bool IRendicionPdn.EsValidaCuentaBancariaCajaChica(int valConsecutivoCompania, string valCtaBancariaCajaChica) {
            Galac.Adm.Ccl.Banco.ICuentaBancariaPdn insReglasCtaBan = new clsCuentaBancariaNav();
            return insReglasCtaBan.EsValidaCuentaBancariaCajaChica(valConsecutivoCompania, valCtaBancariaCajaChica);
        }

        LibResponse IRendicionPdn.cerrarReposicion(Rendicion refRecord) {
            LibResponse vResult = new LibResponse();
            clsMovimientoBancarioNav movNav = new clsMovimientoBancarioNav();
            clsCxPNav cxpNav = new clsCxPNav();
            List<MovimientoBancario> listaMovimientos = new List<MovimientoBancario>();

            Galac.Adm.Ccl.Banco.ICuentaBancariaPdn cuentaNav = new clsCuentaBancariaNav();
            decimal vAlicuotaIGTF = cuentaNav.ObtieneAlicuotaIGTF(refRecord.ConsecutivoCompania, refRecord.CodigoCuentaBancaria,refRecord.FechaCierre);

            decimal monto = refRecord.Total - refRecord.TotalRetencion;
            bool generaITF = refRecord.GeneraImpuestoBancarioAsBool;
            decimal montoITF = 0;
            listaMovimientos.Add(CrearMovimientoBancario(refRecord, monto, Ccl.Banco.eConceptoBancarioPorDefecto.PagoCajaChica, eIngresoEgreso.Egreso, "Pago a proveedores,Cierre Reposición Nro." + refRecord.Numero + ", de la Caja Chica " + refRecord.NombreCuentaBancariaCajaChica, refRecord.CodigoCtaBancariaCajaChica, refRecord.NombreCuentaBancariaCajaChica, false, 0));
            listaMovimientos.Add(CrearMovimientoBancario(refRecord, monto, Ccl.Banco.eConceptoBancarioPorDefecto.EgresoReposicionCajaChica, eIngresoEgreso.Egreso, "Egreso para Reposición Nro." + refRecord.Numero + ", de la Caja Chica " + refRecord.NombreCuentaBancariaCajaChica, refRecord.CodigoCuentaBancaria, refRecord.NombreCuentaBancaria, false, vAlicuotaIGTF));
            listaMovimientos.Add(CrearMovimientoBancario(refRecord, monto, Ccl.Banco.eConceptoBancarioPorDefecto.IngresoReposicionCajaChica, eIngresoEgreso.Ingreso, "Reposición Nro." + refRecord.Numero + ", de la Caja Chica " + refRecord.NombreCuentaBancariaCajaChica, refRecord.CodigoCtaBancariaCajaChica, refRecord.NombreCuentaBancariaCajaChica, false, 0));
            if (generaITF) {                
                montoITF = CalcularITF(monto,vAlicuotaIGTF);
                listaMovimientos.Add(CrearMovimientoBancario(refRecord, montoITF, Ccl.Banco.eConceptoBancarioPorDefecto.DebitoBancarioAutomatico, eIngresoEgreso.Egreso, "Impuesto Bancario del Documento Nro." + refRecord.Numero + ", de la Caja Chica " + refRecord.NombreCuentaBancariaCajaChica, refRecord.CodigoCuentaBancaria, refRecord.NombreCuentaBancaria,  false, vAlicuotaIGTF));
            }

            try {
                ValidarRendicion(refRecord);
                ValidarRendicionParaCerrarAnular(refRecord);
            } catch (Exception) {
                throw;
            }

            try {

                using (TransactionScope scope = getScope()) {
                    List<CxP> CxPTemp = new List<CxP>();
                    List<CxP> CxPAsociadasTemp = new List<CxP>();
                    foreach (DetalleDeRendicion Item in refRecord.DetailDetalleDeRendicion) {
                        if (Item.GeneradaPorAsEnum == eGeneradoPor.Rendicion) {
                            CxPTemp.Add(generarCXP(Item, refRecord.FechaCierre));
                        } else if (Item.GeneradaPorAsEnum == eGeneradoPor.Usuario) {
                            CxPAsociadasTemp.Add(generarCXP(Item, refRecord.FechaCierre));
                        }
                    }
                    cxpNav.insert(CxPTemp);
                    cxpNav.ActualizarCxPAsociadas(CxPAsociadasTemp, refRecord.FechaCierre);
                    movNav.Insert(listaMovimientos);
                    foreach (MovimientoBancario mb in listaMovimientos) {
                        actualizarSaldoCuentaBancaria(refRecord.ConsecutivoCompania, mb);
                    }

                    actualizarStatusRendicion(refRecord, eStatusRendicion.Cerrada);
                    vResult.Info = generarResultadoXml(refRecord);
                    vResult.Success = true;
                    scope.Complete();
                }
            } catch (GalacException vEx) {
                throw vEx;
            } catch (Exception) {
                throw;
            } finally {
                EndScopedBusinessProcess("");

            }
            return vResult;
        }

        LibResponse IRendicionPdn.anularReposicion(Rendicion refRecord) {
            LibResponse vResult = new LibResponse();
            vResult.Success = true;
            clsMovimientoBancarioNav movNav = new clsMovimientoBancarioNav();
            List<CxP> list = buscarCXPAsociadas(refRecord);
            List<MovimientoBancario> listaMovimientos = new List<MovimientoBancario>();

            decimal monto = refRecord.Total - refRecord.TotalRetencion;
            bool generaITF = refRecord.GeneraImpuestoBancarioAsBool;
            decimal montoITF = 0;
            string descripcionMovimiento = "";
            Galac.Adm.Ccl.Banco.ICuentaBancariaPdn cuentaNav = new clsCuentaBancariaNav();
            decimal vAlicuotaIGTF = cuentaNav.ObtieneAlicuotaIGTF(refRecord.ConsecutivoCompania, refRecord.CodigoCuentaBancaria,refRecord.FechaCierre);

            listaMovimientos.Add(CrearMovimientoBancario(refRecord, monto, Ccl.Banco.eConceptoBancarioPorDefecto.ReversoPagoCajaChica, eIngresoEgreso.Ingreso, "Anulación de Pago a proveedores de Reposición Nro." + refRecord.Numero + ", de la Caja Chica " + refRecord.NombreCuentaBancariaCajaChica, refRecord.CodigoCtaBancariaCajaChica, refRecord.NombreCuentaBancariaCajaChica, false, 0, refRecord.FechaAnulacion));
            if (generaITF) {                
                montoITF = CalcularITF( monto,vAlicuotaIGTF);
                monto = monto + montoITF;
                descripcionMovimiento = "Anulación de Egreso e I.G.T.F. para Reposición Nro.";
            } else {
                descripcionMovimiento = "Anulación de Egreso para Reposición Nro.";
            }
            listaMovimientos.Add(CrearMovimientoBancario(refRecord, monto, Ccl.Banco.eConceptoBancarioPorDefecto.ReversoEgresoReposicionCajaChica, eIngresoEgreso.Ingreso, descripcionMovimiento + refRecord.Numero + ", de la Caja Chica " + refRecord.NombreCuentaBancariaCajaChica, refRecord.CodigoCuentaBancaria, refRecord.NombreCuentaBancaria, false, 0, refRecord.FechaAnulacion));
            monto = refRecord.Total - refRecord.TotalRetencion;
            listaMovimientos.Add(CrearMovimientoBancario(refRecord, monto, Ccl.Banco.eConceptoBancarioPorDefecto.ReversoIngresoReposicionCajaChica, eIngresoEgreso.Egreso, "Anulación Reposición Nro." + refRecord.Numero + ", de la Caja Chica " + refRecord.NombreCuentaBancariaCajaChica, refRecord.CodigoCtaBancariaCajaChica, refRecord.NombreCuentaBancariaCajaChica, false, 0, refRecord.FechaAnulacion));

            try {
                ValidarRendicion(refRecord);
                ValidarRendicionParaCerrarAnular(refRecord);
            } catch (Exception) {
                throw;
            }

            try {
                using (TransactionScope scope = getScope()) {
                    clsCxPNav cxpNav = new clsCxPNav();
                    foreach (CxP item in list) {
                        if (item.CxPgeneradaPorAsEnum == eGeneradoPor.Rendicion) {
                            EliminarCXP(new List<CxP>() { item });
                        } else if (item.CxPgeneradaPorAsEnum == eGeneradoPor.Usuario) {
                            cxpNav.DesasociarCxP(new List<CxP>() { item });
                        }
                    }

                    movNav.Insert(listaMovimientos);
                    foreach (MovimientoBancario mb in listaMovimientos) {
                        actualizarSaldoCuentaBancaria(refRecord.ConsecutivoCompania, mb);
                    }
                    actualizarStatusRendicion(refRecord, eStatusRendicion.Anulada);
                    vResult.Info = generarResultadoXml(refRecord);
                    vResult.Success = true;
                    scope.Complete();
                }
            } catch (Exception) {
                throw;
            } finally {
                EndScopedBusinessProcess("");
            }
            return vResult;
        }

        LibResponse IRendicionPdn.GenerarInfoContab(Rendicion refRecord) {
            LibResponse vResult = new LibResponse();
            vResult.Info = generarResultadoXml(refRecord);
            vResult.Success = true;
            return vResult;
        }

        public XmlReader generarResultadoXml(Rendicion refRecord) {
            string msj = "<GPResult><DatosContab><Datos><Documento>" +
                  "<Numero>" + refRecord.Numero + "</Numero>"
                  + "<Fecha>" + refRecord.FechaCierre.ToShortDateString() + "</Fecha>"
                  + "<Consecutivo>" + refRecord.Consecutivo + "</Consecutivo>"
                  + "<FechaAnulacion>" + refRecord.FechaAnulacion + "</FechaAnulacion>"
                  + "<CodigoCuentaCajaChica>" + refRecord.CodigoCtaBancariaCajaChica + "</CodigoCuentaCajaChica>"
                  + "<CodigoCuentaBancariaDeReposicion>" + refRecord.CodigoCuentaBancaria + "</CodigoCuentaBancariaDeReposicion>"
                  + "<MontoCheque>" + refRecord.TotalGastos + "</MontoCheque>"
                  + "<BeneficiarioCheque>" + refRecord.NombreBeneficiario + "</BeneficiarioCheque>"
                  + "</Documento></Datos></DatosContab></GPResult>";
            return new XmlTextReader(new System.IO.StringReader(msj));
        }

        private MovimientoBancario CrearMovimientoBancario(Rendicion refRecord, decimal monto, Ccl.Banco.eConceptoBancarioPorDefecto concepto, eIngresoEgreso tipo, string descripcion, string codigoCuentaBancaria, string nombreCuentaBancaria, bool insertarInmediatamente,decimal valAlicuotaImpBancario, Nullable<DateTime> fecha = null) {
            string CodigoConceptoBancario;
            string NombreConceptoBancario;
            if (refRecord.CodigoConceptoBancario == string.Empty) {

                CodigoConceptoBancario = ((Galac.Adm.Ccl.Banco.IConceptoBancarioPdn)new Galac.Adm.Brl.Banco.clsConceptoBancarioNav()).ConsultaCampoConceptoBancario("Codigo", concepto).ToString();

                NombreConceptoBancario = ((Galac.Adm.Ccl.Banco.IConceptoBancarioPdn)new Galac.Adm.Brl.Banco.clsConceptoBancarioNav()).ConsultaCampoConceptoBancario("Descripcion", concepto).ToString();
                if ((LibString.Len(CodigoConceptoBancario, true) == 0) && (refRecord.GeneraImpuestoBancarioAsBool)) {
                    concepto = Ccl.Banco.eConceptoBancarioPorDefecto.ITFDebitoBancario;
                    CodigoConceptoBancario = ((Galac.Adm.Ccl.Banco.IConceptoBancarioPdn)new Galac.Adm.Brl.Banco.clsConceptoBancarioNav()).ConsultaCampoConceptoBancario("Codigo", concepto).ToString();
                    NombreConceptoBancario = ((Galac.Adm.Ccl.Banco.IConceptoBancarioPdn)new Galac.Adm.Brl.Banco.clsConceptoBancarioNav()).ConsultaCampoConceptoBancario("Descripcion", concepto).ToString();
                }
            } else {
                CodigoConceptoBancario = refRecord.CodigoConceptoBancario;
                NombreConceptoBancario = refRecord.NombreConceptoBancario;
            }

            DateTime fechaAux = fecha == null ? fechaAux = refRecord.FechaCierre : fechaAux = fecha.Value;

            MovimientoBancario movimiento = generarMovimientoBancario(refRecord.NombreBeneficiario,
                LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("DatosMoneda", "Cambio"),
                CodigoConceptoBancario.ToString(),
                codigoCuentaBancaria,
                refRecord.ConsecutivoCompania,
                0,
                descripcion,
                NombreConceptoBancario.ToString(),
                fechaAux,
                fechaAux,
                LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("DatosMoneda", "Nombre"),
                monto,
                nombreCuentaBancaria,
                refRecord.NombreOperador,
                "",
                "",
                refRecord.NumeroDocumento,
                false,
                tipo,
                eGeneradoPor.ReposicionDeCajaChica,
                refRecord.GeneraImpuestoBancarioAsBool,
                false,
                false,
                insertarInmediatamente,
                valAlicuotaImpBancario);
            return movimiento;
        }

        TransactionScope getScope() {
            TransactionScopeOption tso = TransactionScopeOption.RequiresNew;
            return new TransactionScope(tso, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted });
        }

        public decimal CalcularITF(decimal valMontoTotal, decimal valAlicuota) {
            decimal vMontoITF = 0;
            vMontoITF = LibMath.RoundToNDecimals(valMontoTotal * valAlicuota / 100, 2);
            return vMontoITF;
        }

        public string SiguienteNumero(int valConsecutivoCompania) {
            string vResult = "";
            XElement vResulset = GetDataInstance().QueryInfo(eProcessMessageType.Message, "Numero", ParametrosProximoNumero(valConsecutivoCompania));
            vResult = LibXml.GetPropertyString(vResulset, "Numero");
            return vResult;
        }

        private StringBuilder ParametrosProximoNumero(int valConsecutivoCompania) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }

        protected override LibResponse DoSpecializedAction(IList<Rendicion> refRecord, eAccionSR valAction, XmlReader valExtended, bool valUseDetail) {
            if (valAction.Equals(eAccionSR.Cerrar) && EsValidoCondicionesContableParaCerrar(refRecord[0])) {
                return ((IRendicionPdn)this).cerrarReposicion(refRecord[0]);
            } else if (valAction.Equals(eAccionSR.Anular) && EsValidoCondicionesContableParaAnular(refRecord[0])) {
                return ((IRendicionPdn)this).anularReposicion(refRecord[0]);
            } else {
                return base.DoSpecializedAction(refRecord, valAction, valExtended, valUseDetail);
            }
        }

        private bool EsValidoCondicionesContableParaCerrar(Rendicion valRendicion) {
            bool vResult = false;
            bool vUsaContabilidad = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("DatosContabilidad", "UsaContabilidad");
            if (PerteneceAUnMesCerrado(valRendicion.FechaCierre)) {
                throw new LibGalac.Aos.Catching.GalacAlertException("La fecha de cierre: " + LibConvert.ToStr(valRendicion.FechaCierre) + ", pertenece a un mes cerrado.");
            } else if (PerteneceAUnPeriodoCerrado()) {
                throw new LibGalac.Aos.Catching.GalacAlertException("La fecha de cierre: " + LibConvert.ToStr(valRendicion.FechaCierre) + " de Cierre, pertenece a un Período Cerrado.");
            } else if (PerteneceAUnPeriodoCerradoAnterior(valRendicion.ConsecutivoCompania, valRendicion.FechaCierre)) {
                throw new LibGalac.Aos.Catching.GalacAlertException("La fecha de cierre: " + LibConvert.ToStr(valRendicion.FechaCierre) + " , pertenece a un Período Cerrado.");
            } else if (PerteneceAUnPeriodoCerrado()) {
                throw new LibGalac.Aos.Catching.GalacAlertException("La fecha de cierre: " + LibConvert.ToStr(valRendicion.FechaCierre) + " de Cierre, pertenece a un Período Cerrado.");
            } else if (vUsaContabilidad) {
                if (!ReglasContablesCompletas(valRendicion.ConsecutivoCompania, valRendicion.GeneraImpuestoBancarioAsBool)) {
                    throw new LibGalac.Aos.Catching.GalacAlertException("No se han definido las Cuentas Contables de Caja Chica, Movimiento Bancario o ITF");
                } else {
                    vResult = true;
                }
            } else {
                vResult = true;
            }
            return vResult;
        }

        private bool EsValidoCondicionesContableParaAnular(Rendicion valRendicion) {
            bool vResult = false;
            bool vUsaContabilidad = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("DatosContabilidad", "UsaContabilidad");
            if (PerteneceAUnMesCerrado(valRendicion.FechaAnulacion)) {
                throw new LibGalac.Aos.Catching.GalacAlertException("La fecha de anulación: " + LibConvert.ToStr(valRendicion.FechaAnulacion) + ",  pertenece a un mes cerrado.");
            } else if (PerteneceAUnPeriodoCerrado()) {
                throw new LibGalac.Aos.Catching.GalacAlertException("La fecha de anulación: " + LibConvert.ToStr(valRendicion.FechaAnulacion) + ", pertenece a un Período Cerrado.");
            } else if (PerteneceAUnPeriodoCerradoAnterior(valRendicion.ConsecutivoCompania, valRendicion.FechaAnulacion)) {
                throw new LibGalac.Aos.Catching.GalacAlertException("La fecha de anulacion: " + LibConvert.ToStr(valRendicion.FechaCierre) + " , pertenece a un Período Cerrado.");
            } else if (vUsaContabilidad) {
                if (!ReglasContablesCompletas(valRendicion.ConsecutivoCompania, valRendicion.GeneraImpuestoBancarioAsBool)) {
                    throw new LibGalac.Aos.Catching.GalacAlertException("No se han definido las Cuentas Contables de Caja Chica, Movimiento Bancario o ITF");
                } else {
                    vResult = true;
                }
            } else {
                vResult = true;
            }
            return vResult;
        }

        private bool PerteneceAUnMesCerrado(DateTime valFecha) {
            bool vResult = false;
            if (!LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Periodo", "UsaCierreDeMes")) {
                return vResult;
            }
            string vSQL = "SELECT Mes1Cerrado, Mes2Cerrado, Mes3Cerrado, Mes4Cerrado, Mes5Cerrado, Mes6Cerrado, Mes7Cerrado, Mes8Cerrado, Mes9Cerrado, Mes10Cerrado, Mes11Cerrado, Mes12Cerrado FROM PERIODO WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo";
            XElement vDataPeriodo = LibBusiness.ExecuteSelect(vSQL, LibGlobalValues.Instance.GetMfcInfo().GetIntAsParam("Periodo"), "", 0);
            vResult = FechaPerteneceAUnMesCerrado(valFecha, vDataPeriodo);
            return vResult;
        }
        private bool FechaPerteneceAUnMesCerrado(DateTime vFecha, XElement vDataPeriodo) {
            bool vResult = false;

            if (LibDate.F1IsGreaterOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaAperturaDelPeriodo")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre1")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes1Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre1")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre2")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes2Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre2")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre3")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes3Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre3")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre4")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes4Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre4")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre5")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes5Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre5")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre6")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes6Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre6")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre7")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes7Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre7")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre8")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes8Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre8")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre9")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes9Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre9")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre10")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes10Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre10")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre11")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes11Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre11")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaCierreDelPeriodo")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes12Cerrado"))) {
                vResult = true;
            }
            return vResult;
        }

        private bool PerteneceAUnPeriodoCerrado() {
            bool vResult = false;
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Periodo", "PeriodoCerrado")) {
                vResult = true;
            }
            return vResult;
        }

        private bool PerteneceAUnPeriodoCerradoAnterior(int valConsecutivoCompania, DateTime valFecha) {
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInDateTime("FechaCierre", valFecha);
            RegisterClient();
            string vSql = "SELECT PeriodoCerrado FROM Periodo WHERE ConsecutivoCompania = @ConsecutivoCompania AND @FechaCierre BETWEEN  FechaAperturaDelPeriodo and FechaCierreDelPeriodo";
            XElement vResultset = _Db.QueryInfo(eProcessMessageType.Query, vSql, vParams.Get());
            if (vResultset != null) {
                var vEntity = from vRecord in vResultset.Descendants("GpResult")
                              select vRecord;
                foreach (XElement vItem in vEntity) {
                    if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PeriodoCerrado"), null))) {
                        vResult = LibConvert.SNToBool(vItem.Element("PeriodoCerrado"));
                        break;
                    }
                }
            }
            return vResult;
        }

        private bool ReglasContablesCompletas(int valConsecutivoCompania, bool valgeneraITF) {
            bool vResult = false;
            bool generaITF = valgeneraITF;
            string eCG_REPOSICION = "ReposicionDeCajaChica";
            string eCG_MOVBANCARIO = "MovimientoBancario";
            vResult = ((Galac.Saw.Ccl.Contabilizacion.IReglasDeContabilizacionPdn)
                      new Galac.Saw.Brl.Contabilizacion.clsReglasDeContabilizacionNav()).
                      LasCuentasDeReglasDeContabilizacionEstanCompletas(valConsecutivoCompania, eCG_REPOSICION, false, false, 1);
            if (generaITF) {
                vResult = vResult && ((Galac.Saw.Ccl.Contabilizacion.IReglasDeContabilizacionPdn)
                      new Galac.Saw.Brl.Contabilizacion.clsReglasDeContabilizacionNav()).
                      LasCuentasDeReglasDeContabilizacionEstanCompletas(valConsecutivoCompania, eCG_MOVBANCARIO, false, false, 1);
            }
            return vResult;
        }

        protected override LibResponse DeleteRecord(IList<Rendicion> refRecord) {
            clsCxPNav insCxPNav = new clsCxPNav();
            List<CxP> Lista = buscarCXPAsociadas(refRecord[0]);
            if (refRecord != null && refRecord.Count > 0)
                insCxPNav.DesasociarCxP(Lista);
            return base.DeleteRecord(refRecord);
        }

    } //End of class clsRendicionesNav 

} //End of namespace Galac.Adm.Brl.CajaChica
