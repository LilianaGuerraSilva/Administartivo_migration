using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Uil.CajaChica {
    internal class clsDetalleDeRendicionIpl : LibMROMF, ILibView {
        #region Variables
        IList<DetalleDeRendicion> _ListDetalleDeRendicion;
        #endregion //Variables
        #region Propiedades

        public IList<DetalleDeRendicion> ListDetalleDeRendicion {
            get { return _ListDetalleDeRendicion; }
            set { _ListDetalleDeRendicion = value; }
        }
        #endregion //Propiedades
        #region Constructores
        public clsDetalleDeRendicionIpl(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
        }
        #endregion //Constructores
        #region Metodos Generados

        internal void ParseDetailToList(XElement valItemMaster, ref GBindingList<DetalleDeRendicion> refDetailList) {
            foreach (XElement vItemDetail in valItemMaster.Descendants("GpDetailDetalleDeRendicion")) {
                DetalleDeRendicion insDetail = new DetalleDeRendicion();
                insDetail.ConsecutivoCompania = LibConvert.ToInt(vItemDetail.Element("ConsecutivoCompania"));
                insDetail.ConsecutivoRendicion = LibConvert.ToInt(vItemDetail.Element("ConsecutivoRendicion"));
                insDetail.ConsecutivoRenglon = LibConvert.ToInt(vItemDetail.Element("ConsecutivoRenglon"));
                insDetail.NumeroDocumento = vItemDetail.Element("NumeroDocumento").Value;
                insDetail.NumeroControl = vItemDetail.Element("NumeroControl").Value;
                insDetail.Fecha = LibConvert.ToDate(vItemDetail.Element("Fecha"));
                insDetail.CodigoProveedor = vItemDetail.Element("CodigoProveedor").Value;
                insDetail.MontoExento = LibConvert.ToDec(vItemDetail.Element("MontoExento"), 2);
                insDetail.MontoGravable = LibConvert.ToDec(vItemDetail.Element("MontoGravable"), 2);
                insDetail.MontoIVA = LibConvert.ToDec(vItemDetail.Element("MontoIVA"), 2);
                insDetail.AplicaParaLibroDeCompras = vItemDetail.Element("AplicaParaLibroDeCompras").Value;
                insDetail.ObservacionesCxP = vItemDetail.Element("ObservacionesCxP").Value;
                refDetailList.Add(insDetail);
            }
        }

        internal XElement ElementDetail(Rendicion valMaster) {
            XElement vXElement = new XElement("GpDataDetalleDeRendicion",
                from vEntity in valMaster.DetailDetalleDeRendicion
                select new XElement("GpDetailDetalleDeRendicion",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("ConsecutivoRendicion", valMaster.Consecutivo),
                    new XElement("ConsecutivoRenglon", vEntity.ConsecutivoRenglon),
                    new XElement("NumeroDocumento", vEntity.NumeroDocumento),
                    new XElement("NumeroControl", vEntity.NumeroControl),
                    new XElement("Fecha", vEntity.Fecha),
                    new XElement("CodigoProveedor", vEntity.CodigoProveedor),
                    new XElement("MontoExento", LibConvert.ToStr(vEntity.MontoExento)),
                    new XElement("MontoGravable", LibConvert.ToStr(vEntity.MontoGravable)),
                    new XElement("MontoIVA", LibConvert.ToStr(vEntity.MontoIVA)),
                    new XElement("AplicaParaLibroDeCompras", vEntity.AplicaParaLibroDeComprasAsBool),
                    new XElement("ObservacionesCxP", vEntity.ObservacionesCxP)));
            return vXElement;
        }
 public bool ValidateAll(DetalleDeRendicion refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            //vResult = IsValidConsecutivoRendicion(valAction, refRecord.ConsecutivoRendicion, false);
            //vResult = IsValidConsecutivoRenglon(valAction, refRecord.ConsecutivoRenglon, false) && vResult;
            vResult = IsValidNumeroDocumento(valAction, refRecord.NumeroDocumento, false) && vResult;
            vResult = IsValidNumeroControl(valAction, refRecord.NumeroControl, false) && vResult;
            vResult = IsValidFecha(valAction, refRecord.Fecha, false) && vResult;
            vResult = IsValidCodigoProveedor(valAction, refRecord.CodigoProveedor, false) && vResult;
            vResult = IsValidMontoExento(valAction, refRecord.MontoExento, false) && vResult;
            vResult = IsValidMontoGravable(valAction, refRecord.MontoGravable, false) && vResult;
            vResult = IsValidMontoIVA(valAction, refRecord.MontoIVA, false) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidConsecutivoRendicion(eAccionSR valAction, int valConsecutivoRendicion, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valConsecutivoRendicion == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Rendicion"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidConsecutivoRenglon(eAccionSR valAction, int valConsecutivoRenglon, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valConsecutivoRenglon == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Renglon"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidNumeroDocumento(eAccionSR valAction, string valNumeroDocumento, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valNumeroDocumento, true)) {
                BuildValidationInfo(MsgRequiredField("Número del Documento"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidNumeroControl(eAccionSR valAction, string valNumeroControl, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valNumeroControl, true)) {
                BuildValidationInfo(MsgRequiredField("Número de Control"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidFecha(eAccionSR valAction, DateTime valFecha, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFecha, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCodigoProveedor(eAccionSR valAction, string valCodigoProveedor, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCodigoProveedor, true)) {
                BuildValidationInfo(MsgRequiredField("Código del Proveedor"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidMontoExento(eAccionSR valAction, decimal valMontoExento, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            //throw new NotImplementedException("Campo Decimal Obligatorio, debe especificar cual es su validacion");
            return vResult;
        }

        public bool IsValidMontoGravable(eAccionSR valAction, decimal valMontoGravable, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            //throw new NotImplementedException("Campo Decimal Obligatorio, debe especificar cual es su validacion");
            return vResult;
        }

        public bool IsValidMontoIVA(eAccionSR valAction, decimal valMontoIVA, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
           // throw new NotImplementedException("Campo Decimal Obligatorio, debe especificar cual es su validacion");
            return vResult;
        }
        #endregion //Metodos Generados


        public bool IsValidNroDocumentoCodigoProveedorKey(DetalleDeRendicion detalle){
           bool vResult= new Galac.Adm.Brl.CajaChica.clsDetalleDeRendicionNav().IsValidNroDocumentoCodigoProveedorKey(detalle);
            if(!vResult){
             BuildValidationInfo("El Numero de Documento " + detalle.NumeroDocumento + ", para el Proveedor " + detalle.NombreProveedor + " ya existe en el sistema." );
            }
            return vResult;  
         
        }

      



        public void Clear(object refRecord) {
            throw new NotImplementedException();
        }

        public bool DeleteRecord(object refRecord) {
            throw new NotImplementedException();
        }

        public bool InsertRecord(object refRecord, out string outErrorMsg) {
            throw new NotImplementedException();
        }

        public string MessageName {
            get { throw new NotImplementedException(); }
        }

        public object NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        public bool SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }

        public bool UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    } //End of class clsDetalleDeRendicionIpl

} //End of namespace Galac.Saw.Uil.Rendicion

