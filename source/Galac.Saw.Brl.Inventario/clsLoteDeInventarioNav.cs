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
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.Dal;

namespace Galac.Saw.Brl.Inventario {
    public partial class clsLoteDeInventarioNav: LibBaseNavMaster<IList<LoteDeInventario>, IList<LoteDeInventario>>, ILoteDeInventarioPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsLoteDeInventarioNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataMasterComponentWithSearch<IList<LoteDeInventario>, IList<LoteDeInventario>> GetDataInstance() {
            return new Galac.Saw.Dal.Inventario.clsLoteDeInventarioDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Inventario.clsLoteDeInventarioDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Inventario.clsLoteDeInventarioDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Saw.Gp_LoteDeInventarioSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<LoteDeInventario>, IList<LoteDeInventario>> instanciaDal = new Galac.Saw.Dal.Inventario.clsLoteDeInventarioDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Saw.Gp_LoteDeInventarioGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Lote de Inventario":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Artículo Inventario":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
                    vResult = vPdnModule.GetDataForList("Lote de Inventario", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<LoteDeInventario> refData) {
            FillWithForeignInfoLoteDeInventario(ref refData);
        }
        #region LoteDeInventario

        private void FillWithForeignInfoLoteDeInventario(ref IList<LoteDeInventario> refData) {
            XElement vInfoConexionArticuloInventario = FindInfoArticuloInventario(refData);
            var vListArticuloInventario = (from vRecord in vInfoConexionArticuloInventario.Descendants("GpResult")
                                      select new {
                                          ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                          Codigo = vRecord.Element("Codigo").Value, 
                                          Descripcion = vRecord.Element("Descripcion").Value, 
                                          LineaDeProducto = vRecord.Element("LineaDeProducto").Value, 
                                          StatusdelArticulo = vRecord.Element("StatusdelArticulo").Value, 
                                          TipoDeArticulo = vRecord.Element("TipoDeArticulo").Value, 
                                          AlicuotaIVA = vRecord.Element("AlicuotaIVA").Value, 
                                          PrecioSinIVA = LibConvert.ToDec(vRecord.Element("PrecioSinIVA")), 
                                          PrecioConIVA = LibConvert.ToDec(vRecord.Element("PrecioConIVA")), 
                                          PrecioSinIVA2 = LibConvert.ToDec(vRecord.Element("PrecioSinIVA2")), 
                                          PrecioConIVA2 = LibConvert.ToDec(vRecord.Element("PrecioConIVA2")), 
                                          PrecioSinIVA3 = LibConvert.ToDec(vRecord.Element("PrecioSinIVA3")), 
                                          PrecioConIVA3 = LibConvert.ToDec(vRecord.Element("PrecioConIVA3")), 
                                          PrecioSinIVA4 = LibConvert.ToDec(vRecord.Element("PrecioSinIVA4")), 
                                          PrecioConIVA4 = LibConvert.ToDec(vRecord.Element("PrecioConIVA4")), 
                                          PorcentajeBaseImponible = LibConvert.ToDec(vRecord.Element("PorcentajeBaseImponible")), 
                                          CostoUnitario = LibConvert.ToDec(vRecord.Element("CostoUnitario")), 
                                          Existencia = LibConvert.ToDec(vRecord.Element("Existencia")), 
                                          CantidadMinima = LibConvert.ToDec(vRecord.Element("CantidadMinima")), 
                                          CantidadMaxima = LibConvert.ToDec(vRecord.Element("CantidadMaxima")), 
                                          EstadisticasdeProducto = vRecord.Element("EstadisticasdeProducto").Value, 
                                          Categoria = vRecord.Element("Categoria").Value, 
                                          TipoDeProducto = vRecord.Element("TipoDeProducto").Value, 
                                          NombrePrograma = vRecord.Element("NombrePrograma").Value, 
                                          OtrosDatos = vRecord.Element("OtrosDatos").Value, 
                                          Marca = vRecord.Element("Marca").Value, 
                                          FechaDeVencimiento = vRecord.Element("FechaDeVencimiento").Value, 
                                          UnidadDeVenta = vRecord.Element("UnidadDeVenta").Value, 
                                          CampoDefinible1 = vRecord.Element("CampoDefinible1").Value, 
                                          CampoDefinible2 = vRecord.Element("CampoDefinible2").Value, 
                                          CampoDefinible3 = vRecord.Element("CampoDefinible3").Value, 
                                          CampoDefinible4 = vRecord.Element("CampoDefinible4").Value, 
                                          CampoDefinible5 = vRecord.Element("CampoDefinible5").Value, 
                                          MeCostoUnitario = LibConvert.ToDec(vRecord.Element("MeCostoUnitario")), 
                                          UnidadDeVentaSecundaria = LibConvert.ToDec(vRecord.Element("UnidadDeVentaSecundaria")), 
                                          CodigoLote = vRecord.Element("CodigoLote").Value, 
                                          PorcentajeComision = LibConvert.ToDec(vRecord.Element("PorcentajeComision")), 
                                          ExcluirDeComision = vRecord.Element("ExcluirDeComision").Value, 
                                          CantArtReservado = LibConvert.ToDec(vRecord.Element("CantArtReservado")), 
                                          CodigoGrupo = vRecord.Element("CodigoGrupo").Value, 
                                          RetornaAAlmacen = vRecord.Element("RetornaAAlmacen").Value, 
                                          PedirDimension = vRecord.Element("PedirDimension").Value, 
                                          MargenGanancia = LibConvert.ToDec(vRecord.Element("MargenGanancia")), 
                                          MargenGanancia2 = LibConvert.ToDec(vRecord.Element("MargenGanancia2")), 
                                          MargenGanancia3 = LibConvert.ToDec(vRecord.Element("MargenGanancia3")), 
                                          MargenGanancia4 = LibConvert.ToDec(vRecord.Element("MargenGanancia4")), 
                                          TipoArticuloInv = vRecord.Element("TipoArticuloInv").Value, 
                                          ComisionaPorcentaje = vRecord.Element("ComisionaPorcentaje").Value, 
                                          UsaBalanza = vRecord.Element("UsaBalanza").Value
                                      }).Distinct();

            foreach (LoteDeInventario vItem in refData) {
            }
        }

        private XElement FindInfoArticuloInventario(IList<LoteDeInventario> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(LoteDeInventario vItem in valData) {
                vXElement.Add(FilterLoteDeInventarioByDistinctArticuloInventario(vItem).Descendants("GpResult"));
            }
            ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            XElement vXElementResult = insArticuloInventario.GetFk("LoteDeInventario", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterLoteDeInventarioByDistinctArticuloInventario(LoteDeInventario valMaster) {
            XElement vXElement = new XElement("GpData",
                new XElement("GpResult",
                    new XElement("CodigoArticulo", valMaster.CodigoArticulo)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKArticuloInventarioForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }
        #endregion //LoteDeInventario

        XElement ILoteDeInventarioPdn.FindByConsecutivoCompaniaCodigoLoteCodigoArticulo(int valConsecutivoCompania, string valCodigoLote, string valCodigoArticulo) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoLote", valCodigoLote, 30);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Saw.LoteDeInventario");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND CodigoLote = @CodigoLote");
            SQL.AppendLine("AND CodigoArticulo = @CodigoArticulo");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }

        XElement ILoteDeInventarioPdn.FindByConsecutivoCompaniaCodigoArticulo(int valConsecutivoCompania, string valCodigoArticulo) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Saw.LoteDeInventario");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND CodigoArticulo = @CodigoArticulo");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool ILoteDeInventarioPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<LoteDeInventario>, IList<LoteDeInventario>> instanciaDal = new clsLoteDeInventarioDat();
            IList<LoteDeInventario> vLista = new List<LoteDeInventario>();
            LoteDeInventario vCurrentRecord = new Galac.Saw.Dal.InventarioLoteDeInventario();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.CodigoLote = "";
            vCurrentRecord.CodigoArticulo = "";
            vCurrentRecord.FechaDeElaboracion = LibDate.Today();
            vCurrentRecord.FechaDeVencimiento = LibDate.Today();
            vCurrentRecord.Existencia = 0;
            vCurrentRecord.StatusLoteInvAsEnum = eStatusLoteDeInventario.Vigente;
            vCurrentRecord.NombreOperador = "";
            vCurrentRecord.FechaUltimaModificacion = LibDate.Today();
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }


        */
        #endregion //Codigo Ejemplo

        internal List<LoteDeInventario> ParseToListEntity(XElement valXmlEntity) {
            List<LoteDeInventario> vResult = new List<LoteDeInventario>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                LoteDeInventario vRecord = new LoteDeInventario();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoLote"), null))) {
                    vRecord.CodigoLote = vItem.Element("CodigoLote").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoArticulo"), null))) {
                    vRecord.CodigoArticulo = vItem.Element("CodigoArticulo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaDeElaboracion"), null))) {
                    vRecord.FechaDeElaboracion = LibConvert.ToDate(vItem.Element("FechaDeElaboracion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaDeVencimiento"), null))) {
                    vRecord.FechaDeVencimiento = LibConvert.ToDate(vItem.Element("FechaDeVencimiento"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Existencia"), null))) {
                    vRecord.Existencia = LibConvert.ToDec(vItem.Element("Existencia"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("StatusLoteInv"), null))) {
                    vRecord.StatusLoteInv = vItem.Element("StatusLoteInv").Value;
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

        bool ILoteDeInventarioPdn.ExisteLoteDeInventario(int valConsecutivoCompania, string valCodigoArticulo, string valLoteDeInventario) {
            bool vResult = false;
            LibGpParams vParam = new LibGpParams();
            vParam.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParam.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            vParam.AddInString("CodigoLote", valLoteDeInventario, 30);
            vResult = new LibDatabase().ExistsRecord("Saw.LoteDeInventario", "CodigoLote", vParam.Get());
            return vResult;
        }


        LibResponse ILoteDeInventarioPdn.AgregarLote(IList<LoteDeInventario> valListaLote) {
            LibResponse vResult = new LibResponse();
            RegisterClient();
            foreach (LoteDeInventario vItemLote in valListaLote) {
                LibGpParams vParams = new LibGpParams();
                vParams.AddInInteger("ConsecutivoCompania", vItemLote.ConsecutivoCompania);
                XElement vData = _Db.QueryInfo(eProcessMessageType.Message, "ProximoConsecutivo", vParams.Get());
                int vProximoConsecutivo = LibConvert.ToInt(LibXml.GetPropertyString(vData, "Consecutivo"));
                vItemLote.Consecutivo = vProximoConsecutivo;
                vResult = InsertRecord(new List<LoteDeInventario>() { vItemLote }, true);
                if (!vResult.Success) {
                    return vResult;
                }
            }
            return vResult;
        }

        LibResponse ILoteDeInventarioPdn.ActualizarLote(IList<LoteDeInventario> valListaLote) {
            return UpdateRecord(valListaLote, true, eAccionSR.Modificar);
        }
    } //End of class clsLoteDeInventarioNav

} //End of namespace Galac.Saw.Brl.Inventario

