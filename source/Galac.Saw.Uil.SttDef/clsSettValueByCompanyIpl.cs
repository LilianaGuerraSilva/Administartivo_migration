using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Security.Permissions;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;
using System.Collections.ObjectModel;
using WindForm = System.Windows.Forms;
using System.IO;

namespace Galac.Saw.Uil.SttDef {
    public class clsSettValueByCompanyIpl: LibMROMF, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<SettValueByCompany>, IList<SettValueByCompany>> _Reglas;
        IList<SettValueByCompany> _ListSettValueByCompany;
        IList<Parametros> _ListParametro;
        List<Module> _ModuleList;
        #endregion //Variables
        #region Propiedades
        public IList<SettValueByCompany> ListSettValueByCompany {
            get { return _ListSettValueByCompany; }
            set { _ListSettValueByCompany = value; }
        }

        public IList<Parametros> ListParametro {
            get { return _ListParametro; }
            set { _ListParametro = value; }
        }

        public List<Module> ModuleList {
            get { return _ModuleList; }
            set { _ModuleList = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsSettValueByCompanyIpl(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc)
            : base(initAppMemoryInfo, initMfc) {
            ListSettValueByCompany = new List<SettValueByCompany>();
            ListSettValueByCompany.Add(new SettValueByCompany());
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(SettValueByCompany refRecord) {
            refRecord.Clear();
            refRecord.ConsecutivoCompania = Mfc.GetInt("Compania");
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if(WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<SettValueByCompany>, IList<SettValueByCompany>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.SttDef.clsSettValueByCompanyNav(AppMemoryInfo, Mfc);
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((SettValueByCompany)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((SettValueByCompany)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Sett Value By Company"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((List<Module>)refRecord, valAction, out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((SettValueByCompany)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Sett Value By Company.Insertar")]
        internal bool InsertRecord(SettValueByCompany refRecord, out string outErrorMsg) {
            bool vResult = false;
            outErrorMsg = string.Empty;
            //if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
            RegistraCliente();
            IList<SettValueByCompany> vBusinessObject = new List<SettValueByCompany>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            //}
            return vResult;
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "Sett Value By Company.Modificar")]
        internal bool UpdateRecord(List<Module> refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if(ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                SetInsertandoPorPrimeraVez(refRecord);
                vResult = ((ISettValueByCompanyPdn)_Reglas).SpecializedUpdate(refRecord);
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Sett Value By Company.Eliminar")]
        internal bool DeleteRecord(SettValueByCompany refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<SettValueByCompany> vBusinessObject = new List<SettValueByCompany>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public bool FindAndSetObject(int valConsecutivoCompania) {
            bool vResult = false;
            ListParametro = GetListParametros(valConsecutivoCompania);
            ModuleList = GetModuleList(valConsecutivoCompania);
            vResult = (ListParametro.Count > 0);
            return vResult;
        }

        public bool ValidateAll(List<Module> refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            
            bool vUsaCobroDirecto = (bool)GetPropertyValue(refRecord, "2.2.- Facturación (Continuación) ", "UsaCobroDirectoAsBool");
            string vConceptoBancarioCobroDirecto = (string)GetPropertyValue(refRecord, "2.2.- Facturación (Continuación) ", "ConceptoBancarioCobroDirecto");
            vResult = IsValidConceptoBancarioCobroDirecto(valAction, vConceptoBancarioCobroDirecto, vUsaCobroDirecto, false) && vResult;

            string vCuentaBancariaCobroDirecto = (string)GetPropertyValue(refRecord, "2.2.- Facturación (Continuación) ", "CuentaBancariaCobroDirecto");
            vResult = IsValidCuentaBancariaCobroDirecto(valAction, vCuentaBancariaCobroDirecto, vUsaCobroDirecto, false) && vResult;

            eModeloDeFactura vModeloFactura = (eModeloDeFactura)GetPropertyValue(refRecord, "2.4.- Modelo de Factura", "ModeloDeFacturaAsEnum");
            string vNombrePlantillaFactura = (string)GetPropertyValue(refRecord, "2.4.- Modelo de Factura", "NombrePlantillaFactura");
            vResult = IsValidModeloFactura(valAction, vModeloFactura, vNombrePlantillaFactura) && vResult;
            
            eTipoDePrefijo vTipoPrefijo = (eTipoDePrefijo)GetPropertyValue(refRecord, "2.4.- Modelo de Factura", "TipoDePrefijoAsEnum");
            string vPrefijo = (string)GetPropertyValue(refRecord, "2.4.- Modelo de Factura", "Prefijo");
            vResult = IsValidPrefijo(valAction, vTipoPrefijo, vPrefijo) && vResult;

            bool vUsaDosTalonarios = (bool)GetPropertyValue(refRecord, "2.4.- Modelo de Factura", "UsarDosTalonariosAsBool");
            if (vUsaDosTalonarios) {
                eModeloDeFactura vModeloFactura2 = (eModeloDeFactura)GetPropertyValue(refRecord, "2.4.- Modelo de Factura", "ModeloDeFactura2AsEnum");
                string vNombrePlantillaFactura2 = (string)GetPropertyValue(refRecord, "2.4.- Modelo de Factura", "NombrePlantillaFactura2");
                vResult = IsValidModeloFactura2(valAction, vModeloFactura2, vNombrePlantillaFactura2) && vResult;

                eTipoDePrefijo vTipoPrefijo2 = (eTipoDePrefijo)GetPropertyValue(refRecord, "2.4.- Modelo de Factura", "TipoDePrefijo2AsEnum");
                string vPrefijo2 = (string)GetPropertyValue(refRecord, "2.4.- Modelo de Factura", "Prefijo2");
                vResult = IsValidPrefijo2(valAction, vTipoPrefijo2, vPrefijo2) && vResult;
            }

            bool vUsaCamposDefinibles = (bool)GetPropertyValue(refRecord, "2.5.- Campos Definibles", "UsaCamposDefiniblesAsBool");
            string vNombreCampoDefinible1 = (string)GetPropertyValue(refRecord, "2.5.- Campos Definibles", "NombreCampoDefinible1");
            vResult = IsValidCamposDefinibles(valAction, vUsaCamposDefinibles, vNombreCampoDefinible1) && vResult;

            string vCodigoGenericoVendedor = (string)GetPropertyValue(refRecord, "3.3.- Vendedor ", "CodigoGenericoVendedor");
            vResult = IsValidCodigoGenericoVendedor(valAction, vCodigoGenericoVendedor, false) && vResult;

            string vConceptoReversoCobranza = (string)GetPropertyValue(refRecord, "4.2.- Cobranzas", "ConceptoReversoCobranza");
            vResult = IsValidConceptoReversoCobranza(valAction, vConceptoReversoCobranza, false) && vResult;

            string vCodigoAlmacenGenerico = (string)GetPropertyValue(refRecord, "5.1.- Inventario", "CodigoAlmacenGenerico");
            vResult = IsValidCodigoAlmacenGenerico(valAction, vCodigoAlmacenGenerico, false) && vResult;

            eTipoDeMetodoDeCosteo TipoMetodoCosteo = (eTipoDeMetodoDeCosteo)GetPropertyValue(refRecord, "5.3.- Método  de costos", "MetodoDeCosteoAsEnum");
            if (TipoMetodoCosteo == eTipoDeMetodoDeCosteo.CostoPromedio) {
                DateTime vFechaDesdeUsoMetodoDeCosteo = (DateTime)GetPropertyValue(refRecord, "5.3.- Método  de costos", "FechaDesdeUsoMetodoDeCosteo");
                vResult = EsValidaFechaDesdeUsoMetodoDeCosteo(vFechaDesdeUsoMetodoDeCosteo, out vFechaDesdeUsoMetodoDeCosteo) && vResult; ;

                DateTime vFechaContabilizacionDeCosteo = (DateTime)GetPropertyValue(refRecord, "5.3.- Método  de costos", "FechaContabilizacionDeCosteo");
                vResult = EsValidaFechaContabilizacionDeCosteo(vFechaContabilizacionDeCosteo, vFechaDesdeUsoMetodoDeCosteo, out vFechaContabilizacionDeCosteo) && vResult; ;
            }

            int vLongitudCodigoProveedor = (int)GetPropertyValue(refRecord, "6.2.- CxP / Proveedor / Pagos", "LongitudCodigoProveedor");
            vResult = IsValidLongitudCodigoProveedor(valAction, vLongitudCodigoProveedor, false) && vResult;

            int vNumCopiasComprobantepago = (int)GetPropertyValue(refRecord, "6.2.- CxP / Proveedor / Pagos", "NumCopiasComprobantepago");
            vResult = IsValidNumCopiasComprobantepago(valAction, vNumCopiasComprobantepago, false) && vResult;

            eTipoDeOrdenDePagoAImprimir vTipoDeOrdenDePagoAImprimir = (eTipoDeOrdenDePagoAImprimir)GetPropertyValue(refRecord, "6.2.- CxP / Proveedor / Pagos", "TipoDeOrdenDePagoAImprimirAsEnum");
            vResult = IsValidTipoDeOrdenDePagoAImprimir(valAction, vTipoDeOrdenDePagoAImprimir, false) && vResult;

            string vConceptoBancarioReversoDePago = (string)GetPropertyValue(refRecord, "6.2.- CxP / Proveedor / Pagos", "ConceptoBancarioReversoDePago");
            vResult = IsValidConceptoBancarioReversoDePago(valAction, vConceptoBancarioReversoDePago, false) && vResult;
           
            bool vRetieneImpuestoMunicipal = (bool) GetPropertyValue(refRecord, "6.2.- CxP / Proveedor / Pagos", "RetieneImpuestoMunicipalAsBool");
           
            if(vRetieneImpuestoMunicipal){
               int vPrimerNumeroComprobanteRetImpuestoMunicipal = (int) GetPropertyValue(refRecord, "6.2.- CxP / Proveedor / Pagos", "PrimerNumeroComprobanteRetImpuestoMunicipal");
               vResult = IsValidPrimerNumeroComprobanteRetImpuestoMunicipal(valAction, vPrimerNumeroComprobanteRetImpuestoMunicipal) && vResult;
            } 

            eDondeSeEfectuaLaRetencionIVA vDondeSeEfectuaLaRetencionIVA = (eDondeSeEfectuaLaRetencionIVA)GetPropertyValue(refRecord, "6.3.- Retención IVA", "EnDondeRetenerIVAAsEnum");
            vResult = IsValidDondeSeEfectuaLaRetencionIVA(valAction, vDondeSeEfectuaLaRetencionIVA) && vResult;

            int vPrimerNumeroComprobanteRetIVA = (int)GetPropertyValue(refRecord, "6.3.- Retención IVA", "PrimerNumeroComprobanteRetIVA");
            vResult = IsValidPrimerNumeroComprobanteRetIVA(valAction, vPrimerNumeroComprobanteRetIVA) && vResult;

            int vNumCopiasComprobanteRetencion = (int)GetPropertyValue(refRecord, "6.4.- Retención ISLR", "NumCopiasComprobanteRetencion");
            vResult = IsValidNumCopiasComprobanteRetencion(valAction, vNumCopiasComprobanteRetencion, false) && vResult;

            bool vUsaRetencion = (bool)GetPropertyValue(refRecord, "6.4.- Retención ISLR", "UsaRetencionAsBool");
            eDondeSeEfectuaLaRetencionISLR vEnDondeRetenerISLR = (eDondeSeEfectuaLaRetencionISLR)GetPropertyValue(refRecord, "6.4.- Retención ISLR", "EnDondeRetenerISLRAsEnum");
            vResult = IsValidEnDondeRetenerISLR(valAction, vEnDondeRetenerISLR, vUsaRetencion) && vResult;

            string vCiudadRepLegal = (string)GetPropertyValue(refRecord, "6.4.- Retención ISLR", "CiudadRepLegal");
            vResult = IsValidCiudadRepLegal(valAction, vCiudadRepLegal, false) && vResult;

            string vCodigoGenericoCuentaBancaria = (string)GetPropertyValue(refRecord, "7.1.- Bancos", "CodigoGenericoCuentaBancaria");
            vResult = IsValidCodigoGenericoCuentaBancaria(valAction, vCodigoGenericoCuentaBancaria, false) && vResult;

            bool vManejaCreditoBancario = (bool)GetPropertyValue(refRecord, "7.1.- Bancos", "ManejaCreditoBancarioAsBool");
            string vConceptoCreditoBancario = (string)GetPropertyValue(refRecord, "7.1.- Bancos", "ConceptoCreditoBancario");
            vResult = IsValidConceptoCreditoBancario(valAction, vConceptoCreditoBancario, vManejaCreditoBancario, false) && vResult;

            bool vManejaDebitoBancario = (bool)GetPropertyValue(refRecord, "7.1.- Bancos", "ManejaDebitoBancarioAsBool");
            string vConceptoDebitoBancario = (string)GetPropertyValue(refRecord, "7.1.- Bancos", "ConceptoDebitoBancario");
            vResult = IsValidConceptoDebitoBancario(valAction, vConceptoDebitoBancario, vManejaDebitoBancario, false) && vResult;

            string vCodigoMonedaLocal = (string)GetPropertyValue(refRecord, "7.2-Moneda", "CodigoMonedaLocal");
            vResult = IsValidCodigoMonedaLocal(valAction, vCodigoMonedaLocal, false) && vResult;

            string vNombreMonedaLocal = (string)GetPropertyValue(refRecord, "7.2-Moneda", "NombreMonedaLocal");
            vResult = IsValidNombreMonedaLocal(valAction, vNombreMonedaLocal, false) && vResult;

            string vNombreMonedaExtranjera = (string)GetPropertyValue(refRecord, "7.2-Moneda", "NombreMonedaExtranjera");
            vResult = IsValidNombreMonedaExtranjera(valAction, vNombreMonedaExtranjera, false) && vResult;

            string vConceptoBancarioAnticipoCobrado = (string)GetPropertyValue(refRecord, "7.3.- Anticipo", "ConceptoBancarioAnticipoCobrado");
            vResult = IsValidConceptoBancarioAnticipoCobrado(valAction, vConceptoBancarioAnticipoCobrado, false) && vResult;

            string vConceptoBancarioReversoAnticipoCobrado = (string)GetPropertyValue(refRecord, "7.3.- Anticipo", "ConceptoBancarioReversoAnticipoCobrado");
            vResult = IsValidConceptoBancarioReversoAnticipoCobrado(valAction, vConceptoBancarioReversoAnticipoCobrado, false) && vResult;

            string vConceptoBancarioAnticipoPagado = (string)GetPropertyValue(refRecord, "7.3.- Anticipo", "ConceptoBancarioAnticipoPagado");
            vResult = IsValidConceptoBancarioAnticipoPagado(valAction, vConceptoBancarioAnticipoPagado, false) && vResult;

            string vConceptoBancarioReversoAnticipoPagado = (string)GetPropertyValue(refRecord, "7.3.- Anticipo", "ConceptoBancarioReversoAnticipoPagado");
            vResult = IsValidConceptoBancarioReversoAnticipoPagado(valAction, vConceptoBancarioReversoAnticipoPagado, false) && vResult;

            string vConceptoBancarioReversoSolicitudDePago = (string)GetPropertyValue(refRecord, "7.4.- Movimiento Bancario", "ConceptoBancarioReversoSolicitudDePago");
            vResult = IsValidConceptoBancarioReversoSolicitudDePago(valAction, vConceptoBancarioReversoSolicitudDePago, false) && vResult;

            string vNombrePlantillaComprobanteDePagoSueldo = (string)GetPropertyValue(refRecord, "7.4.- Movimiento Bancario", "NombrePlantillaComprobanteDePagoSueldo");
            vResult = IsValidNombrePlantillaComprobanteDePagoSueldo(valAction, vNombrePlantillaComprobanteDePagoSueldo, false) && vResult;

            eModeloDeFactura vModeloNotaEntrega = (eModeloDeFactura)GetPropertyValue(refRecord, "8.1-Notas de entrega", "ModeloNotaEntregaAsEnum");
            string vNombrePlantillaNotaEntrega = (string)GetPropertyValue(refRecord, "8.1-Notas de entrega", "NombrePlantillaNotaEntrega");
            vResult = IsValidModeloNotaEntrega(valAction, vModeloNotaEntrega, vNombrePlantillaNotaEntrega) && vResult;

            //string vCodigoEmpresaIntegrada = (string)GetPropertyValue(refRecord, "9.1-Procesos", "CodigoEmpresaIntegrada");
            //vResult = IsValidCodigoEmpresaIntegrada(valAction, vCodigoEmpresaIntegrada, false) && vResult;

            outErrorMessage = Information.ToString();
            return vResult;
        }

        private object GetPropertyValue(List<Module> refRecord, string valGroupName, string valPropertyName) {
            object vResult = null;
            Group vGroup = null;
            foreach(var item in refRecord) {
                vGroup = item.Groups.Where(g => LibText.Trim(g.DisplayName) == LibText.Trim(valGroupName)).FirstOrDefault();
                if(vGroup != null) {
                    break;
                }
            }
            if(vGroup != null) {
                System.Reflection.PropertyInfo vProperty = vGroup.Content.GetType().GetProperty(valPropertyName);
                if(vProperty != null) {
                    try {
                        vResult = vProperty.GetValue(vGroup.Content, null);
                    } catch { }
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados

        #region Validation
        public bool IsValidLongitudCodigoProveedor(eAccionSR valAction, int valLongitudCodigoProveedor, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if(valLongitudCodigoProveedor == 0) {
                BuildValidationInfo(MsgRequiredField("6.2.- CxP / Proveedor / Pagos -> Longitud Codigo Proveedor"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidNumCopiasComprobantepago(eAccionSR valAction, int valNumCopiasComprobantepago, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if(valNumCopiasComprobantepago == 0) {
                BuildValidationInfo(MsgRequiredField("6.2.- CxP / Proveedor / Pagos -> NumCopias Comprobantepago"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidTipoDeOrdenDePagoAImprimir(eAccionSR valAction, eTipoDeOrdenDePagoAImprimir valTipoDeOrdenDePagoAImprimir, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            return vResult;
        }

        public bool IsValidConceptoBancarioReversoDePago(eAccionSR valAction, string valConceptoBancarioReversoDePago, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if(LibString.IsNullOrEmpty(valConceptoBancarioReversoDePago, true)) {
                BuildValidationInfo(MsgRequiredField("6.2.- CxP / Proveedor / Pagos -> Concepto Bancario de Reverso de Pago"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCodigoAlmacenGenerico(eAccionSR valAction, string valCodigoAlmacenGenerico, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if(LibString.IsNullOrEmpty(valCodigoAlmacenGenerico, true)) {
                BuildValidationInfo(MsgRequiredField("5.1.- Inventario -> Almacén genérico ..."));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidConceptoBancarioReversoSolicitudDePago(eAccionSR valAction, string valConceptoBancarioReversoSolicitudDePago, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if(LibString.IsNullOrEmpty(valConceptoBancarioReversoSolicitudDePago, true)) {
                BuildValidationInfo(MsgRequiredField("7.4.- Movimiento Bancario -> Concepto Bancario Reverso Solicitud De Pago"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidConceptoReversoCobranza(eAccionSR valAction, string valConceptoReversoCobranza, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if(LibString.IsNullOrEmpty(valConceptoReversoCobranza, true)) {
                BuildValidationInfo(MsgRequiredField("4.2.- Cobranzas -> Concepto Reverso Cobranza "));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidNombrePlantillaComprobanteDePagoSueldo(eAccionSR valAction, string valNombrePlantillaComprobanteDePagoSueldo, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if(LibString.IsNullOrEmpty(valNombrePlantillaComprobanteDePagoSueldo, true)) {
                BuildValidationInfo(MsgRequiredField("7.4.- Movimiento Bancario -> Nombre Plantilla Comprobante de PagoSueldo"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCodigoEmpresaIntegrada(eAccionSR valAction, string valCodigoEmpresaIntegrada, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if(LibString.IsNullOrEmpty(valCodigoEmpresaIntegrada, true)) {
                BuildValidationInfo(MsgRequiredField("9.1-Procesos -> CodigoEmpresaIntegrada"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCodigoMonedaLocal(eAccionSR valAction, string valCodigoMonedaLocal, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if(LibString.IsNullOrEmpty(valCodigoMonedaLocal, true)) {
                BuildValidationInfo(MsgRequiredField("7.2-Moneda -> Código"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidNombreMonedaLocal(eAccionSR valAction, string valNombreMonedaLocal, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if(LibString.IsNullOrEmpty(valNombreMonedaLocal, true)) {
                BuildValidationInfo(MsgRequiredField("7.2-Moneda -> Nombre Moneda Local"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidNombreMonedaExtranjera(eAccionSR valAction, string valNombreMonedaExtranjera, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if(LibString.IsNullOrEmpty(valNombreMonedaExtranjera, true)) {
                BuildValidationInfo(MsgRequiredField("7.2-Moneda -> Nombre Moneda Extranjera"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidNumCopiasComprobanteRetencion(eAccionSR valAction, int valNumCopiasComprobanteRetencion, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if(valNumCopiasComprobanteRetencion == 0) {
                BuildValidationInfo(MsgRequiredField("6.4.- Retención ISLR -> Num Copias Comprobante Retencion "));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCiudadRepLegal(eAccionSR valAction, string valCiudadRepLegal, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if(LibString.IsNullOrEmpty(valCiudadRepLegal, true)) {
                BuildValidationInfo(MsgRequiredField("6.4.- Retención ISLR -> Ciudad Rep Legal"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidNombreCiudad(eAccionSR valAction, string valNombreCiudad, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if(LibString.IsNullOrEmpty(valNombreCiudad, true)) {
                BuildValidationInfo(MsgRequiredField("Nombre de la Ciudad"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidDondeSeEfectuaLaRetencionIVA(eAccionSR valAction, eDondeSeEfectuaLaRetencionIVA valDondeSeEfectuaLaRetencionIVA) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (!EsValidaDondeRetenerIva(valDondeSeEfectuaLaRetencionIVA)) {
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidPrimerNumeroComprobanteRetIVA(eAccionSR valAction, int valPrimerNumeroComprobanteRetIVA) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (!EsValidoPrimerNumeroComprobanteRetIVA(valPrimerNumeroComprobanteRetIVA)) {
                return false;
            }
            return vResult;
        }

        public bool IsValidPrimerNumeroComprobanteRetImpuestoMunicipal(eAccionSR valAction, int valPrimerNumeroComprobanteRetImpuestoMunicipal) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (!EsValidoPrimerNumeroComprobanteRetImpMunicipal(valPrimerNumeroComprobanteRetImpuestoMunicipal)) {
                return false;
            }
            return vResult;
        }

        public bool IsValidConceptoBancarioCobroDirecto(eAccionSR valAction, string valConceptoBancarioCobroDirecto, bool valUsaCobroDirecto, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valUsaCobroDirecto) {
                if (LibString.IsNullOrEmpty(valConceptoBancarioCobroDirecto, true)) {
                    BuildValidationInfo(MsgRequiredField("2.2.- Facturación (Continuación) -> Concepto Bancario de Cobro Directo"));
                    vResult = false;
                }
            }
            return vResult;
        }

        public bool IsValidCuentaBancariaCobroDirecto(eAccionSR valAction, string valCuentaBancariaCobroDirecto, bool valUsaCobroDirecto, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valUsaCobroDirecto) {
                if (LibString.IsNullOrEmpty(valCuentaBancariaCobroDirecto, true)) {
                    BuildValidationInfo(MsgRequiredField("2.2.- Facturación (Continuación) -> Cuenta Bancaria de Cobro Directo"));
                    vResult = false;
                }
            }
            return vResult;
        }

        public bool IsValidCodigoGenericoVendedor(eAccionSR valAction, string valCodigoGenericoVendedor, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCodigoGenericoVendedor, true)) {
                BuildValidationInfo(MsgRequiredField("3.3.- Vendedor -> Código Generico de Vendedor"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidEnDondeRetenerISLR(eAccionSR valAction, eDondeSeEfectuaLaRetencionISLR valEnDondeRetenerISLR, bool valUsaRetencion) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valUsaRetencion) {
                if (!EsValidaEnDondeRetenerISLR(valEnDondeRetenerISLR)) {
                    vResult = false;
                }
            }
            return vResult;
        }

        internal bool EsValidaEnDondeRetenerISLR(eDondeSeEfectuaLaRetencionISLR valEnDondeRetenerISLR) {
            RegistraCliente();
            bool vResult = true;
            //Information.Clear();
                if (valEnDondeRetenerISLR == eDondeSeEfectuaLaRetencionISLR.NoRetenida) {
                    Information.AppendLine("Debe seleccionar dónde efectuar la Retención del ISLR");
                    vResult = false;
                } 
            return vResult;
        }

        public bool IsValidCodigoGenericoCuentaBancaria(eAccionSR valAction, string valCodigoGenericoCuentaBancaria, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCodigoGenericoCuentaBancaria, true)) {
                BuildValidationInfo(MsgRequiredField("7.1.- Bancos -> Cuenta Bancaria Genérica"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidConceptoDebitoBancario(eAccionSR valAction, string valConceptoDebitoBancario, bool valManejaDebitoBancario, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valManejaDebitoBancario) {
                if (LibString.IsNullOrEmpty(valConceptoDebitoBancario, true)) {
                    BuildValidationInfo(MsgRequiredField("7.1.- Bancos -> Concepto del Débito Bancario"));
                    vResult = false;
                }
            }
            return vResult;
        }

        public bool IsValidConceptoCreditoBancario(eAccionSR valAction, string valConceptoCreditoBancario, bool valManejaCreditoBancario, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valManejaCreditoBancario) {
                if (LibString.IsNullOrEmpty(valConceptoCreditoBancario, true)) {
                    BuildValidationInfo(MsgRequiredField("7.1.- Bancos -> Concepto del Crédito Bancario"));
                    vResult = false;
                }
            }
            return vResult;
        }

        public bool IsValidConceptoBancarioAnticipoCobrado(eAccionSR valAction, string valConceptoBancarioAnticipoCobrado, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valConceptoBancarioAnticipoCobrado, true)) {
                BuildValidationInfo(MsgRequiredField("7.3.- Anticipo -> Concepto Bancario del Anticipo Cobrado"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidConceptoBancarioReversoAnticipoCobrado(eAccionSR valAction, string valConceptoBancarioReversoAnticipoCobrado, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valConceptoBancarioReversoAnticipoCobrado, true)) {
                BuildValidationInfo(MsgRequiredField("7.3.- Anticipo -> Concepto Bancario de Reverso del Anticipo Cobrado"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidConceptoBancarioAnticipoPagado(eAccionSR valAction, string valConceptoBancarioAnticipoPagado, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valConceptoBancarioAnticipoPagado, true)) {
                BuildValidationInfo(MsgRequiredField("7.3.- Anticipo -> Concepto Bancario del Anticipo Pagado"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidConceptoBancarioReversoAnticipoPagado(eAccionSR valAction, string valConceptoBancarioReversoAnticipoPagado, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valConceptoBancarioReversoAnticipoPagado, true)) {
                BuildValidationInfo(MsgRequiredField("7.3.- Anticipo -> Concepto Bancario de Reverso del Anticipo Pagado"));
                vResult = false;
            }
            return vResult;
        }

        #endregion

        public bool EscogerParametrosActual(int valConsecutivoCompania) {
            bool vResult = false;
            if(FindAndSetObject(valConsecutivoCompania)) {
                vResult = true;
            } else {
                /* IMPLEMENTE EL SIGUIENTE CODIGO
                    if (valConsecutivoCompania != 0) {
                        if (((IReglasDeContabilizacionPdn)_Reglas).InsertarValoresPorDefecto(valConsecutivoCompania)) {
                            vResult = FindAndSetObject(valConsecutivoCompania);
                        }
                    }
                */
            }
            if(vResult) {
                //FALTA IMPLEMENTAR Set Valores Globales (LibGlobalValues)
            }
            return vResult;
        }

        private List<Parametros> GetListParametros(int valConsecutivoCompania) {
            List<Parametros> vResult = new List<Parametros>();
            RegistraCliente();
            vResult = ((ISettValueByCompanyPdn)_Reglas).ParametrosLis(valConsecutivoCompania);
            return vResult;
        }

        private List<Module> GetModuleList(int valConsecutivoCompania) {
            RegistraCliente();
            List<Module> vResult = ((ISettValueByCompanyPdn)_Reglas).GetModuleList(valConsecutivoCompania);
            return vResult;
        }


        public bool PuedeUsarTallerMecanico() {
            bool vResult = false;
            try {
                vResult = AppMemoryInfo.GlobalValuesGetBool("Parametros", "PuedeUsarTallerMecanico");
            } catch(GalacException vEx) {
                if(LibString.S1IsInS2("no se encuentra en el conjunto", vEx.Message)) {
                    vResult = false;
                } else {
                    throw;
                }
            }
            return vResult;
        }

        public bool CaracteristicaDeContabilidadActiva() {
            bool vResult = false;
            try {
                vResult = AppMemoryInfo.GlobalValuesGetBool("Parametros", "CaracteristicaDeContabilidadActiva");
            } catch (GalacException vEx) {
                if (LibString.S1IsInS2("no se encuentra en el conjunto", vEx.Message)) {
                    vResult = false;
                } else {
                    throw;
                }
            }
            return vResult;
        }

        public bool EsSistemaParaIG() {
            bool vResult = false;
            try {
                vResult = AppMemoryInfo.GlobalValuesGetBool("Parametros", "EsSistemaParaIG");
            } catch (GalacException vEx) {
                if (LibString.S1IsInS2("no se encuentra en el conjunto", vEx.Message)) {
                    vResult = false;
                } else {
                    throw;
                }
            }
            return vResult;
        }

        public List<string> GetModelosPlanillasList() {
            List<string> vResult = new List<string>();
            List<string> vListModelosPlantillas = new List<string>();
            LibXmlDataParse insDataParse = new LibXmlDataParse(AppMemoryInfo);
            string[] vModelosPlanillasArray;
            string vModelosPlanillas = insDataParse.GetString("Parametros", 0, "ModelosPlanillas", "");
            vModelosPlanillasArray = LibString.Split(vModelosPlanillas, ',');
            for(int i = 0; i < vModelosPlanillasArray.Length; i += 1) {
                vListModelosPlantillas.Add(vModelosPlanillasArray[i]);
            }
            vResult = vListModelosPlantillas;
            return vResult;
        }

        public string NombreArchivo() {
            string vResult = "";
            WindForm.OpenFileDialog diag = new WindForm.OpenFileDialog();
            diag.InitialDirectory = System.IO.Path.Combine(LibWorkPaths.ProgramDir, "Reportes");
            diag.InitialDirectory = System.IO.Path.Combine(diag.InitialDirectory, "Original");
            diag.Filter = "rpx   (*.rpx)|*.rpx";
            diag.FilterIndex = 2;
            diag.RestoreDirectory = true;
            if(diag.ShowDialog() == WindForm.DialogResult.OK) {
                if(LibText.Right(diag.SafeFileName, 4) == ".rpx") {
                    vResult = LibText.Left(diag.SafeFileName, diag.SafeFileName.Length - 4);
                } else {
                    vResult = diag.SafeFileName;
                }
            }
            return vResult;
        }


        public bool IsMaximoDescuentoEnFactura(eAccionSR valAction, decimal valMaximoDescuentoEnFactura, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if(valMaximoDescuentoEnFactura > 100) {
                BuildValidationInfo("El porcentaje de descuento no puede ser mayor al 100%");
                vResult = false;
            }

            return vResult;
        }


        public bool IsValidaFechaDeInicioContabilizacion(DateTime valFechaDeInicioContabilizacion) {
            bool vResult = false;
            try {
                DateTime vFechaDeApertura = AppMemoryInfo.GlobalValuesGetDateTime("Parametros", "FechaAperturaDelPrimerPeriodo");
                vResult = !LibGalac.Aos.Base.LibDate.F1IsLessThanF2(valFechaDeInicioContabilizacion, vFechaDeApertura);
            } catch(GalacException vEx) {
                if(LibString.S1IsInS2("no se encuentra en el conjunto", vEx.Message)) {
                    vResult = false;
                } else {
                    throw;
                }
            }
            return vResult;
        }

        public bool IsValidPrefijo (eAccionSR valAction, eTipoDePrefijo vTipoPrefijo, string vPrefijo) {
            bool vResult = true;
            if ((vTipoPrefijo == eTipoDePrefijo.Indicar) && LibText.IsNullOrEmpty(LibText.Trim(vPrefijo)) ) {
                string msg = "Usted ha especificado para el Talonario 1 que usará un prefijo particular, pero no ha introducido ningún valor para el prefijo. \n";
                msg += "Introduzca el valor del prefijo del Talonario 1 para poder continuar.";
                Information.AppendLine(msg);
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidPrefijo2(eAccionSR valAction, eTipoDePrefijo vTipoPrefijo2, string vPrefijo2) {
            bool vResult = true;
            if ((vTipoPrefijo2 == eTipoDePrefijo.Indicar) && LibText.IsNullOrEmpty(LibText.Trim(vPrefijo2))) {
                string msg = "Usted ha especificado para el Talonario 2 que usará un prefijo particular, pero no ha introducido ningún valor para el prefijo. \n";
                msg += "Introduzca el valor del prefijo del Talonario 2 para poder continuar.";
                Information.AppendLine(msg);
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidModeloFactura(eAccionSR valAction, eModeloDeFactura vModeloFactura, string vPlantillaFactura) {
            bool vResult = true;
            if (vModeloFactura == eModeloDeFactura.eMD_OTRO && LibText.IsNullOrEmpty(vPlantillaFactura)) {
                string msg = "Es necesario que especifique el Nombre de la Plantilla de Factura a utilizar.";
                Information.AppendLine(msg);
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidModeloFactura2(eAccionSR valAction, eModeloDeFactura vModeloFactura2, string vPlantillaFactura2) {
            bool vResult = true;
            if (vModeloFactura2 == eModeloDeFactura.eMD_OTRO && LibText.IsNullOrEmpty(vPlantillaFactura2)) {
                string msg = "Es necesario que especifique el Nombre de la Plantilla de Factura 2 a utilizar.";
                Information.AppendLine(msg);
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidModeloNotaEntrega(eAccionSR valAction, eModeloDeFactura vModeloNotaEntrega, string vPlantillaNotaEntrega) {
            bool vResult = true;
            if (vModeloNotaEntrega == eModeloDeFactura.eMD_OTRO && LibText.IsNullOrEmpty(vPlantillaNotaEntrega)) {
                string msg = "Es necesario que especifique el Nombre de la Plantilla de Nota Entrega a utilizar.";
                Information.AppendLine(msg);
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCamposDefinibles(eAccionSR valAction, bool vUsaCamposDefinibles, string vNombreCampoDefinible1) {
            bool vResult = true;
            if (vUsaCamposDefinibles && LibText.IsNullOrEmpty(vNombreCampoDefinible1)) {
                string msg = "Está usando la opción de usar campos definibles y no ha incluido ningún nombre para un campo definible. \n";
                msg += "Incluya un nombre de campo en la pestaña de 'Campos Definibles' o desmarque la opción 'Usar campos definibles' en la pestaña  de 'Factuación' para poder grabar.";
                Information.AppendLine(msg);
                vResult = false;
            }
            return vResult;
        }
        
        public bool IsValidaFechaMinimaIngresarDatos(DateTime valFechaMinimaIngresarDatos) {
            bool vResult = false;
            try {
                DateTime vFechaDeApertura = AppMemoryInfo.GlobalValuesGetDateTime("Parametros", "FechaAperturaDelPrimerPeriodo");
                vResult = LibGalac.Aos.Base.LibDate.F1IsLessThanF2(valFechaMinimaIngresarDatos, vFechaDeApertura);
                if(vResult) {
                    throw new GalacException("Esta fecha no puede ser mayor que la Fecha de hoy. ", eExceptionManagementType.Validation);
                }
            } catch(GalacException vEx) {
                if(LibString.S1IsInS2("no se encuentra en el conjunto", vEx.Message)) {
                    vResult = false;
                } else {
                    throw;
                }
            }
            return vResult;
        }

        internal bool SePuedeModificarParametrosDeConciliacion() {
            RegistraCliente();
            return ((Ccl.SttDef.ISettValueByCompanyPdn)_Reglas).SePuedeModificarParametrosDeConciliacion();
        }

        internal string BuscarNombrePlantilla(string valNameRpx) {
            string vResult = string.Empty;
            
            System.Windows.Forms.OpenFileDialog vFileDialog = new System.Windows.Forms.OpenFileDialog();
            if(LibString.S1IsInS2("*", valNameRpx)) {
                vFileDialog.Filter = valNameRpx;
            }
            vFileDialog.InitialDirectory = System.IO.Path.Combine(LibWorkPaths.ProgramDir, "Reportes");
            vFileDialog.InitialDirectory = System.IO.Path.Combine(vFileDialog.InitialDirectory, "Original");
            if(vFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                //vResult = vFileDialog.FileName;
                vResult = LibText.Left(vFileDialog.SafeFileName, vFileDialog.SafeFileName.Length - 4);
            }
            return vResult;
        }

        internal bool EsValidoNombrePlantilla(string valNameRpx) {
            bool vResult = false;
            
            string rutaReportes = System.IO.Path.Combine(LibWorkPaths.ProgramDir, "Reportes");
            string rutaOriginal = System.IO.Path.Combine(rutaReportes, "Original");
            string rutaUsuario = System.IO.Path.Combine(rutaReportes, "Usuario");
            if (File.Exists(rutaOriginal + @"\" + valNameRpx + ".rpx")) {
                vResult = true;
            }
            if (File.Exists(rutaUsuario + @"\" + valNameRpx + ".rpx")) {
                vResult = true;
            }
            return vResult;
        }


        internal bool EsValidaLongitudCodigoVendedor(decimal valLongitudCodigoVendedor) {
            bool vResult = true;
            //Information.Clear();
            if(LibText.Len(valLongitudCodigoVendedor.ToString()) == 0) {
                Information.AppendLine("La longitud del Código del Vendedor no puede ser igual a cero. ");
                vResult = false;
            } else if(LibConvert.ToInt(valLongitudCodigoVendedor) < 1) {
                Information.AppendLine("La longitud del Código del Vendedor no puede ser igual a cero.");
                vResult = false;
            } else if(LibConvert.ToInt(valLongitudCodigoVendedor) > 10) {
                Information.AppendLine("La longitud del Código del Vendedor no puede ser mayor a 5.");
                vResult = false;
            }
            return vResult;
        }

        internal bool EsValidaLongitudCodigoCliente(decimal valLongitudCodigoCliente) {
            bool vResult = true;
            //Information.Clear();
            if(LibText.Len(valLongitudCodigoCliente.ToString()) == 0) {
                Information.AppendLine("La longitud del Código del Cliente no puede ser igual a cero. ");
                vResult = false;
            } else if(LibConvert.ToInt(valLongitudCodigoCliente) < 1) {
                Information.AppendLine("La longitud del Código del Cliente no puede ser igual a cero.");
                vResult = false;
            } else if(LibConvert.ToInt(valLongitudCodigoCliente) > 10) {
                Information.AppendLine("La longitud del Código del Cliente no puede ser mayor a 10.");
                vResult = false;
            }
            return vResult;
        }

        internal bool EsValidaNumeroMaximoDeCerosALaIzquierda(decimal valNumeroMaximoDeCerosALaIzquierda) {
            bool vResult = true;
            //Information.Clear();
            if (LibText.Len(valNumeroMaximoDeCerosALaIzquierda.ToString()) < 1 || LibText.Len(valNumeroMaximoDeCerosALaIzquierda.ToString()) > 10 ) {
                Information.AppendLine("El número máximo de ceros a la izquierda a imprimir, \n "+ "debe ser mayor o igual a uno (1) \n " + "y menor o igual a diez (10). ");
                vResult = false;
            } 
            return vResult;
        }

        internal bool EsValidaDondeRetenerIva(eDondeSeEfectuaLaRetencionIVA valDondeSeEfectuaLaRetencionIVA) {
            RegistraCliente();
            bool vResult = true;
            //Information.Clear();
            if(AppMemoryInfo.GlobalValuesGetBool("Parametros", "PuedoUsarOpcionesDeContribuyenteEspecial")) {
                if(valDondeSeEfectuaLaRetencionIVA == eDondeSeEfectuaLaRetencionIVA.NoRetenida) {
                    Information.AppendLine("Debe seleccionar dónde efectuar la Retención del " + AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA"));
                    vResult = false;
                } else if(AppMemoryInfo.GlobalValuesGetBool("Parametros", "UsaModuloDeContabilidad") &&
                 !SonIgualesContabilizacionYAplicacionDeRetIVA(valDondeSeEfectuaLaRetencionIVA)) {
                    StringBuilder vMessageBuilder = new StringBuilder();
                    eDondeSeEfectuaLaRetencionIVA tDondeSeEfectuaLaRetencionIVA;
                    tDondeSeEfectuaLaRetencionIVA = (eDondeSeEfectuaLaRetencionIVA)LibConvert.DbValueToEnum(LibConvert.ToStr(AppMemoryInfo.GlobalValuesGetEnum("Parametros", "DondeContabilizarRet")));
                    string MsgDondeContabilizarRetIVA = LibEnumHelper.GetDescription(tDondeSeEfectuaLaRetencionIVA, AppMemoryInfo.GlobalValuesGetEnum("Parametros", "DondeContabilizarRet"));
                    Information.AppendLine("El momento en el cual se efectúa la Retención de " + AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA"));
                    Information.AppendLine("(Parámetros Administrativos -> Efectuar la Retención del " + AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA") + " en: " + (valDondeSeEfectuaLaRetencionIVA).ToString() + ")");
                    Information.AppendLine("es diferente al momento en el cual se está contabilizando dicha retención");
                    Information.AppendLine("(Reglas de Contabilización -> Contabilizar la Retención de " + AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA") + " en: " + MsgDondeContabilizarRetIVA + ").");
                }
            }
            return vResult;
        }

        private bool SonIgualesContabilizacionYAplicacionDeRetIVA(eDondeSeEfectuaLaRetencionIVA valDondeSeEfectuaLaRetencionIVA) {
            bool vResult = true;
            vResult = (valDondeSeEfectuaLaRetencionIVA == eDondeSeEfectuaLaRetencionIVA.CxP && AppMemoryInfo.GlobalValuesGetEnum("Parametros", "DondeContabilizarRet") == 1)
                || (valDondeSeEfectuaLaRetencionIVA == eDondeSeEfectuaLaRetencionIVA.Pago && AppMemoryInfo.GlobalValuesGetEnum("Parametros", "DondeContabilizarRet") == 2);
            return vResult;

        }

        internal bool EsValidoPrimerNumeroComprobanteRetIVA(decimal valPrimerNumeroComprobanteRetIVA) {
            bool vResult = true;
            if(AppMemoryInfo.GlobalValuesGetBool("Parametros", "PuedoUsarOpcionesDeContribuyenteEspecial")) {
                if(valPrimerNumeroComprobanteRetIVA < 1) {
                    Information.AppendLine("El Primer Número de Comprobante de Retención de " + AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA") + " debe ser Mayor a 0");
                    vResult = false;
                }
            }
            return vResult;
        }

       internal bool EsValidoPrimerNumeroComprobanteRetImpMunicipal(decimal valPrimerNumeroComprobanteRetImpMunicipal) {
            bool vResult = true;
            if (SePuedeRetenerParaEsteMunicipio() && PuedeActivarModulo()) {
                if(valPrimerNumeroComprobanteRetImpMunicipal< 1) {
                    Information.AppendLine("El Primer Número de Comprobante de Retención de Impuesto Municipal debe ser Mayor a 0");
                    vResult = false;
                }
            }
            return vResult;
        }

        internal bool EsFormaDeReiniciarElNumeroDeComprobanteRetIVA(eFormaDeReiniciarComprobanteRetIVA valFormaDeReiniciarComprobanteRetIVA) {
            RegistraCliente();
            bool vResult = true;
            //Information.Clear();
            if(AppMemoryInfo.GlobalValuesGetBool("Parametros", "PuedoUsarOpcionesDeContribuyenteEspecial")) {
                if(valFormaDeReiniciarComprobanteRetIVA == eFormaDeReiniciarComprobanteRetIVA.SinEscoger) {
                    Information.AppendLine("Debe Seleccionar una Forma de Reiniciar el Número de Comprobante de Retención del" + AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA"));
                    vResult = false;
                }
            }
            return vResult;

        }

        internal bool EsValidaFechaContabilizacionDeCosteo(DateTime valFechaContabilizacionDeCosteo, DateTime valFechaDesdeUsoMetodoDeCosteo, out DateTime vDateResult) {
            bool vResult = true;
            vDateResult = valFechaContabilizacionDeCosteo;
            if(valFechaContabilizacionDeCosteo.Date.Day != 1) {
                Information.AppendLine("Usted debe ingresar fecha 1° (primero de mes), como fecha de contabilización del metodo de costo.");
                vDateResult = new DateTime(vDateResult.Year, vDateResult.Month, 1);
                vResult = false;
            } else if(AppMemoryInfo.GlobalValuesGetBool("Parametros", "UsaModuloDeContabilidad") && LibGalac.Aos.Base.LibDate.F1IsLessThanF2(valFechaContabilizacionDeCosteo, GetValueFechaDeInicioContabilizacion())) {
                Information.AppendLine("La fecha de inicio de contabilización de costos no puede ser menor a la fecha de inicio de contabilización general del sistema.");
                vResult = false;
            } else if(LibGalac.Aos.Base.LibDate.F1IsLessThanF2(valFechaContabilizacionDeCosteo, valFechaDesdeUsoMetodoDeCosteo)) {
                Information.AppendLine("La fecha de inicio de contabilización de costos no puede ser menor a la fecha de inicio del uso del metodo de costo.");
                vResult = false;
            }
            return vResult;
        }

        internal bool EsValidaFechaDesdeUsoMetodoDeCosteo(DateTime valFechaDesdeUsoMetodoDeCosteo, out DateTime vDateResult) {
            bool vResult = true;
            vDateResult = valFechaDesdeUsoMetodoDeCosteo;
            if(valFechaDesdeUsoMetodoDeCosteo.Date.Day != 1) {
                Information.AppendLine("Usted debe ingresar fecha 1° (primero de mes), como fecha de inicio del uso del metodo de costo.");
                vDateResult = new DateTime(vDateResult.Year, vDateResult.Month, 1);
                vResult = false;
            }
            return vResult;
        }

        internal bool EsValidaNumCopiasComprobanteRetencion(decimal p, out string vMessage) {
            bool vResult = true;
            vMessage = "";
            if(p == 0) {
            }
            return vResult;
        }

        internal bool EsValidaPrimeraNotaDeCredito(string valPrimeraNotaCredito, out string outPrimeraNotaCredito) {
            bool vResult = true;
            outPrimeraNotaCredito = "";
            RegistraCliente();
            outPrimeraNotaCredito = ((ISettValueByCompanyPdn)_Reglas).GeneraPriemraNotaDeCredito(Mfc.GetInt("Compania"), LibConvert.ToInt(valPrimeraNotaCredito));
            outPrimeraNotaCredito = LibText.Mid(outPrimeraNotaCredito, LibText.InStr(outPrimeraNotaCredito, "-") + 1);
            if(LibConvert.ToInt(valPrimeraNotaCredito) < LibConvert.ToInt(outPrimeraNotaCredito)) {
                Information.Clear();
                Information.AppendLine("El número de inicial de la Nota de Crédito que se está indicando podría generar inconsistencias.");
                Information.AppendLine("El cambio no será tomado en cuenta.");
                outPrimeraNotaCredito = LibText.FillWithCharToLeft(outPrimeraNotaCredito, "0", 11);
                vResult = false;
            }
            return vResult;
        }

        internal bool EsValidaPrimeraNotaDeDebito(string valPrimeraNotaDeDebito, out string outPrimeraNotaDebito) {
            bool vResult = true;
            outPrimeraNotaDebito = "";
            RegistraCliente();
            outPrimeraNotaDebito = ((ISettValueByCompanyPdn)_Reglas).GeneraPriemraNotaDeDebito(Mfc.GetInt("Compania"), LibConvert.ToInt(valPrimeraNotaDeDebito));
            outPrimeraNotaDebito = LibText.Mid(outPrimeraNotaDebito, LibText.InStr(outPrimeraNotaDebito, "-") + 1);
            if(LibConvert.ToInt(valPrimeraNotaDeDebito) < LibConvert.ToInt(outPrimeraNotaDebito)) {
                Information.Clear();
                Information.AppendLine("El número de inicial de la Nota de Débito que se está indicando podría generar inconsistencias.");
                Information.AppendLine("El cambio no será tomado en cuenta.");
                outPrimeraNotaDebito = LibText.FillWithCharToLeft(outPrimeraNotaDebito, "0", 11);
                vResult = false;
            }
            return vResult;
        }

        internal bool EsValidaPrimeraBoleta(string valPrimeraBoleta, out string outPrimeraBoleta) {
            bool vResult = true;
            outPrimeraBoleta = "";
            RegistraCliente();
            outPrimeraBoleta = ((ISettValueByCompanyPdn)_Reglas).GeneraPriemraBoleta(Mfc.GetInt("Compania"), LibConvert.ToInt(valPrimeraBoleta));
            outPrimeraBoleta = LibText.Mid(outPrimeraBoleta, LibText.InStr(outPrimeraBoleta, "-") + 1);
            if(LibConvert.ToInt(valPrimeraBoleta) < LibConvert.ToInt(outPrimeraBoleta)) {
                //Information.Clear();
                Information.AppendLine("El número de inicial de la Boleta que se está indicando podría generar inconsistencias.");
                Information.AppendLine("El cambio no será tomado en cuenta.");
                outPrimeraBoleta = LibText.FillWithCharToLeft(outPrimeraBoleta, "0", 11);
                vResult = false;
            }
            return vResult;
        }

        internal decimal DefaultNumCopiasComprobantepago() {
            return 2;
        }

        internal decimal DefaultLongitudCodigoProveedor() {
            RegistraCliente();
            return ((Ccl.SttDef.ISettValueByCompanyPdn)_Reglas).DefaultLongitudCodigoProveedor();
        }

        internal decimal DefaultLongitudCodigoCliente() {
            RegistraCliente();
            return ((Ccl.SttDef.ISettValueByCompanyPdn)_Reglas).DefaultLongitudCodigoCliente();
        }

        internal decimal DefaultLongitudCodigoVendedor() {
            RegistraCliente();
            return ((Ccl.SttDef.ISettValueByCompanyPdn)_Reglas).DefaultLongitudCodigoVendedor();
        }

        internal decimal DefaultPrimerNumeroComprobanteRetIVA() {
            return 1;
        }

        internal decimal DefaultNumCopiasComprobanteRetencion() {
            return 2;
        }

        internal bool SePuedeRetenerParaEsteMunicipio() {
            string vNombreCiudad = clsGlobalValues.AppMemoryInfo.GlobalValuesGetString("Parametros", "Ciudad");
            int vConsecutivoMunicipio = clsGlobalValues.AppMemoryInfo.GlobalValuesGetInt("Parametros", "ConsecutivoMunicipio");
            RegistraCliente();
            return ((Ccl.SttDef.ISettValueByCompanyPdn)_Reglas).ExisteMunicipio(vConsecutivoMunicipio, vNombreCiudad);
        }

        internal bool PuedeActivarModulo() {
            string vCodigoMunicipio = clsGlobalValues.AppMemoryInfo.GlobalValuesGetString("Parametros", "CodigoMunicipio");
            RegistraCliente();
            return ((Ccl.SttDef.ISettValueByCompanyPdn)_Reglas).PuedeActivarModulo(vCodigoMunicipio);
        }

        private DateTime GetValueFechaDeInicioContabilizacion() {
            try {
                return AppMemoryInfo.GlobalValuesGetDateTime("Parametros", "FechaDeInicioContabilizacion");
            } catch(Exception) {
                return LibDate.Today();
            }
        }

        private void SetInsertandoPorPrimeraVez(List<Module> refRecord) {
            var ListProceso = refRecord[8].Groups[0].Content;
            ListProceso.GetType().GetProperty("InsertandoPorPrimeraVezAsBool").SetValue(ListProceso, false, null);
        }

    } //End of class clsSettValueByCompanyIpl

} //End of namespace Galac.Saw.Uil.SttDef

 