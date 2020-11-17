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
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Brl.GestionCompras {
    public partial class clsCompraDetalleSerialRolloNav: LibBaseNavDetail<IList<CompraDetalleSerialRollo>, IList<CompraDetalleSerialRollo>> {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCompraDetalleSerialRolloNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<CompraDetalleSerialRollo>, IList<CompraDetalleSerialRollo>> GetDataInstance() {
            return new Galac.Adm.Dal.GestionCompras.clsCompraDetalleSerialRolloDat();
        }

        private void FillWithForeignInfo(ref IList<CompraDetalleSerialRollo> refData) {
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool ICompraDetalleSerialRolloPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<CompraDetalleSerialRollo>, IList<CompraDetalleSerialRollo>> instanciaDal = new clsCompraDetalleSerialRolloDat();
            IList<CompraDetalleSerialRollo> vLista = new List<CompraDetalleSerialRollo>();
            CompraDetalleSerialRollo vCurrentRecord = new Galac.Adm.Dal.GestionComprasCompraDetalleSerialRollo();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.ConsecutivoCompra = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.CodigoArticulo = "";
            vCurrentRecord.Serial = "";
            vCurrentRecord.Rollo = "";
            vCurrentRecord.Cantidad = 0;
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<CompraDetalleSerialRollo> ParseToListEntity(XElement valXmlEntity) {
            List<CompraDetalleSerialRollo> vResult = new List<CompraDetalleSerialRollo>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                CompraDetalleSerialRollo vRecord = new CompraDetalleSerialRollo();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompra"), null))) {
                    vRecord.ConsecutivoCompra = LibConvert.ToInt(vItem.Element("ConsecutivoCompra"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoArticulo"), null))) {
                    vRecord.CodigoArticulo = vItem.Element("CodigoArticulo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Serial"), null))) {
                    vRecord.Serial = vItem.Element("Serial").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Rollo"), null))) {
                    vRecord.Rollo = vItem.Element("Rollo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Cantidad"), null))) {
                    vRecord.Cantidad = LibConvert.ToDec(vItem.Element("Cantidad"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo


    } //End of class clsCompraDetalleSerialRolloNav

} //End of namespace Galac.Adm.Brl.GestionCompras

