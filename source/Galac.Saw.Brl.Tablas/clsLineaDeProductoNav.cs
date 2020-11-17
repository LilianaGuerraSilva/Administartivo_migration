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
using Galac.Contab.Brl.WinCont;

namespace Galac.Saw.Brl.Tablas {
    public partial class clsLineaDeProductoNav:LibBaseNav<IList<LineaDeProducto>, IList<LineaDeProducto>>, ILineaDeProductoPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsLineaDeProductoNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<LineaDeProducto>, IList<LineaDeProducto>> GetDataInstance() {
            return new Galac.Saw.Dal.Tablas.clsLineaDeProductoDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Tablas.clsLineaDeProductoDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Tablas.clsLineaDeProductoDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_LineaDeProductoSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<LineaDeProducto>, IList<LineaDeProducto>> instanciaDal = new Galac.Saw.Dal.Tablas.clsLineaDeProductoDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_LineaDeProductoGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Línea de Producto":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Centro de Costos":
                    vPdnModule = new Galac.Contab.Brl.WinCont.clsCentroDeCostosNav();
                    vResult = vPdnModule.GetDataForList("Tablas", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }
        #endregion //Metodos Generados

        internal bool ValidateAll(XmlReader valRecord, eAccionSR eAccionSR, StringBuilder refErrorMessage) {
            bool vResult = true;
            return vResult;
        }
        private StringBuilder ParametroCompania(int valConsecutivoCompania) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        XElement ILineaDeProductoPdn.FindByConsecutivoCompaniaNombre(int valConsecutivoCompania, string valNombre) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Nombre", valNombre, 20);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Adm.LineaDeProducto");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND Nombre = @Nombre");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }

        int GetNextConsecutivo(int valConsecutivoCompania, ILibDataComponent<IList<LineaDeProducto>, IList<LineaDeProducto>> instanciaDal) {
            XElement vResult;
            int vNumero;
            vResult = instanciaDal.QueryInfo(eProcessMessageType.Message, "ProximoConsecutivo", ParametroCompania(valConsecutivoCompania));
            vNumero = LibConvert.ToInt(LibXml.GetPropertyString(vResult, "Consecutivo"));
            return vNumero;
        }
        void ILineaDeProductoPdn.InsertaPrimeraLineaDeProducto(int valConsecutivoCompania) {
            ILibDataComponent<IList<LineaDeProducto>, IList<LineaDeProducto>> instanciaDal = new Dal.Tablas.clsLineaDeProductoDat();
            IList<LineaDeProducto> vLista = new List<LineaDeProducto>();
            LineaDeProducto vCurrentRecord = new LineaDeProducto();

            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.Consecutivo = GetNextConsecutivo(valConsecutivoCompania, instanciaDal);
            vCurrentRecord.Nombre = "LINEA DE PRODUCTO";
            vCurrentRecord.PorcentajeComision = 0;
            vCurrentRecord.CentroDeCosto = "";
            vCurrentRecord.PorcentajeComision = 0;
            vLista.Add(vCurrentRecord);
            instanciaDal.Insert(vLista);
        }

        string ILineaDeProductoPdn.GetNextConsecutivoLineaDeProducto(int valConsecutivoCompania) {
            ILibDataComponent<IList<LineaDeProducto>, IList<LineaDeProducto>> instanciaDal = new Dal.Tablas.clsLineaDeProductoDat();
            return LibConvert.ToStr(GetNextConsecutivo(valConsecutivoCompania, instanciaDal));            
        }

        #region Ejemplo
        //private List<LineaDeProducto> ParseToListEntity(XElement valXmlEntity) {
        //    List<LineaDeProducto> vResult = new List<LineaDeProducto>();
        //    var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
        //                  select vRecord;
        //    foreach (XElement vItem in vEntity) {
        //        LineaDeProducto vRecord = new LineaDeProducto();
        //        vRecord.Clear();
        //        if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
        //            vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
        //        }
        //        if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
        //            vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
        //        }
        //        if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Nombre"), null))) {
        //            vRecord.Nombre = vItem.Element("Nombre").Value;
        //        }
        //        if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeComision"), null))) {
        //            vRecord.PorcentajeComision = LibConvert.ToDec(vItem.Element("PorcentajeComision"));
        //        }
        //        if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CentroDeCosto"), null))) {
        //            vRecord.CentroDeCosto = vItem.Element("CentroDeCosto").Value;
        //        }
        //        if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NombreOperador"), null))) {
        //            vRecord.NombreOperador = vItem.Element("NombreOperador").Value;
        //        }
        //        if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaUltimaModificacion"), null))) {
        //            vRecord.FechaUltimaModificacion = LibConvert.ToDate(vItem.Element("FechaUltimaModificacion"));
        //        }
        //        vResult.Add(vRecord);
        //    }
        //    return vResult;
        //}
        #endregion ejemplo

    }

}

