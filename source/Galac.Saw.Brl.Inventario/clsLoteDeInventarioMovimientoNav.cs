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
using System.Collections.ObjectModel;

namespace Galac.Saw.Brl.Inventario {
    public partial class clsLoteDeInventarioMovimientoNav: LibBaseNavDetail<IList<LoteDeInventarioMovimiento>, IList<LoteDeInventarioMovimiento>> {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsLoteDeInventarioMovimientoNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<LoteDeInventarioMovimiento>, IList<LoteDeInventarioMovimiento>> GetDataInstance() {
            return new Galac.Saw.Dal.Inventario.clsLoteDeInventarioMovimientoDat();
        }

        private void FillWithForeignInfo(ref IList<LoteDeInventarioMovimiento> refData) {
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool ILoteDeInventarioMovimientoPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<LoteDeInventarioMovimiento>, IList<LoteDeInventarioMovimiento>> instanciaDal = new clsLoteDeInventarioMovimientoDat();
            IList<LoteDeInventarioMovimiento> vLista = new List<LoteDeInventarioMovimiento>();
            LoteDeInventarioMovimiento vCurrentRecord = new Galac.Saw.Dal.InventarioLoteDeInventarioMovimiento();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.ConsecutivoLote = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.Fecha = LibDate.Today();
            vCurrentRecord.ModuloAsEnum = eOrigenLoteInv.Factura;
            vCurrentRecord.TipoOperacionAsEnum = eTipodeOperacion.EntradadeInventario;
            vCurrentRecord.Cantidad = 0;
            vCurrentRecord.ConsecutivoDocumentoOrigen = 0;
            vCurrentRecord.NumeroDocumentoOrigen = "";
            vCurrentRecord.StatusDocumentoOrigenAsEnum = eStatusDocOrigenLoteInv.Vigente;
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }
                
        */
        #endregion //Codigo Ejemplo
        internal List<LoteDeInventarioMovimiento> ParseToListEntity(XElement valXmlEntity) {
            List<LoteDeInventarioMovimiento> vResult = new List<LoteDeInventarioMovimiento>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult") select vRecord;
            foreach (XElement vItem in vEntity) {
                LoteDeInventarioMovimiento vRecord = new LoteDeInventarioMovimiento();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoLote"), null))) {
                    vRecord.ConsecutivoLote = LibConvert.ToInt(vItem.Element("ConsecutivoLote"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Fecha"), null))) {
                    vRecord.Fecha = LibConvert.ToDate(vItem.Element("Fecha"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Modulo"), null))) {
                    vRecord.Modulo = vItem.Element("Modulo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoOperacion"), null))) {
                    vRecord.TipoOperacion = vItem.Element("TipoOperacion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Cantidad"), null))) {
                    vRecord.Cantidad = LibConvert.ToDec(vItem.Element("Cantidad"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoDocumentoOrigen"), null))) {
                    vRecord.ConsecutivoDocumentoOrigen = LibConvert.ToInt(vItem.Element("ConsecutivoDocumentoOrigen"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDocumentoOrigen"), null))) {
                    vRecord.NumeroDocumentoOrigen = vItem.Element("NumeroDocumentoOrigen").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("StatusDocumentoOrigen"), null))) {
                    vRecord.StatusDocumentoOrigen = vItem.Element("StatusDocumentoOrigen").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }

        internal ObservableCollection<LoteDeInventarioMovimiento> ParseToListObservableCollectionEntity(XElement valXmlEntity) {
            ObservableCollection<LoteDeInventarioMovimiento> vResult = new ObservableCollection<LoteDeInventarioMovimiento>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult") select vRecord;
            foreach (XElement vItem in vEntity) {
                LoteDeInventarioMovimiento vRecord = new LoteDeInventarioMovimiento();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoLote"), null))) {
                    vRecord.ConsecutivoLote = LibConvert.ToInt(vItem.Element("ConsecutivoLote"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Fecha"), null))) {
                    vRecord.Fecha = LibConvert.ToDate(vItem.Element("Fecha"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Modulo"), null))) {
                    vRecord.Modulo = vItem.Element("Modulo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoOperacion"), null))) {
                    vRecord.TipoOperacion = vItem.Element("TipoOperacion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Cantidad"), null))) {
                    vRecord.Cantidad = LibConvert.ToDec(vItem.Element("Cantidad"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoDocumentoOrigen"), null))) {
                    vRecord.ConsecutivoDocumentoOrigen = LibConvert.ToInt(vItem.Element("ConsecutivoDocumentoOrigen"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDocumentoOrigen"), null))) {
                    vRecord.NumeroDocumentoOrigen = vItem.Element("NumeroDocumentoOrigen").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("StatusDocumentoOrigen"), null))) {
                    vRecord.StatusDocumentoOrigen = vItem.Element("StatusDocumentoOrigen").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }

        internal int ProximoConsecutivo(int valConsecutivoCompania, int valConsecutivoLote) {
            LibGpParams vParam = new LibGpParams();
            vParam.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParam.AddInInteger("ConsecutivoLote", valConsecutivoLote);
            int vResult = new LibDatabase().NextLngConsecutive("Saw.LoteDeInventarioMovimiento", "Consecutivo", vParam.Get());
            return vResult;
        }


    } //End of class clsLoteDeInventarioMovimientoNav

} //End of namespace Galac.Saw.Brl.Inventario

