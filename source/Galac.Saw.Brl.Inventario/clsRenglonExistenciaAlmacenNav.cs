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

namespace Galac.Saw.Brl.Inventario {
    public partial class clsRenglonExistenciaAlmacenNav: LibBaseNav<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsRenglonExistenciaAlmacenNav() {
        }
        #endregion //Constructores
        #region Metodos Generados
        protected override ILibDataComponentWithSearch<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>> GetDataInstance() {
            return new Galac.Saw.Dal.Inventario.clsRenglonExistenciaAlmacenDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Inventario.clsRenglonExistenciaAlmacenDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Inventario.clsRenglonExistenciaAlmacenDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_RenglonExistenciaAlmacenSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>> instanciaDal = new Galac.Saw.Dal.Inventario.clsRenglonExistenciaAlmacenDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "dbo.Gp_RenglonExistenciaAlmacenGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            switch (valModule) {
                case "Renglon Existencia Almacen":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<RenglonExistenciaAlmacen> refData) {
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IRenglonExistenciaAlmacenPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>> instanciaDal = new clsRenglonExistenciaAlmacenDat();
            IList<RenglonExistenciaAlmacen> vLista = new List<RenglonExistenciaAlmacen>();
            RenglonExistenciaAlmacen vCurrentRecord = new Galac.Saw.Dal.InventarioRenglonExistenciaAlmacen();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.CodigoAlmacen = "";
            vCurrentRecord.CodigoArticulo = "";
            vCurrentRecord.ConsecutivoRenglon = 0;
            vCurrentRecord.CodigoSerial = "";
            vCurrentRecord.CodigoRollo = "";
            vCurrentRecord.Cantidad = 0;
            vCurrentRecord.Ubicacion = "";
            vCurrentRecord.ConsecutivoAlmacen = 0;
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<RenglonExistenciaAlmacen> ParseToListEntity(XElement valXmlEntity) {
            List<RenglonExistenciaAlmacen> vResult = new List<RenglonExistenciaAlmacen>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                RenglonExistenciaAlmacen vRecord = new RenglonExistenciaAlmacen();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoAlmacen"), null))) {
                    vRecord.CodigoAlmacen = vItem.Element("CodigoAlmacen").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoArticulo"), null))) {
                    vRecord.CodigoArticulo = vItem.Element("CodigoArticulo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoRenglon"), null))) {
                    vRecord.ConsecutivoRenglon = LibConvert.ToInt(vItem.Element("ConsecutivoRenglon"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoSerial"), null))) {
                    vRecord.CodigoSerial = vItem.Element("CodigoSerial").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoRollo"), null))) {
                    vRecord.CodigoRollo = vItem.Element("CodigoRollo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Cantidad"), null))) {
                    vRecord.Cantidad = LibConvert.ToDec(vItem.Element("Cantidad"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Ubicacion"), null))) {
                    vRecord.Ubicacion = vItem.Element("Ubicacion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoAlmacen"), null))) {
                    vRecord.ConsecutivoAlmacen = LibConvert.ToInt(vItem.Element("ConsecutivoAlmacen"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo


    } //End of class clsRenglonExistenciaAlmacenNav

} //End of namespace Galac.Saw.Brl.Inventario

