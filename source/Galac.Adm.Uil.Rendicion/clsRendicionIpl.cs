using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Security.Permissions;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Cib;
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Uil.CajaChica {
    public class clsRendicionIpl: LibMROMF, ILibView {
        #region Variables
        ILibBusinessMasterComponent<IList<Rendicion>, IList<Rendicion>> _Reglas;
        IList<Rendicion> _ListRendicion;
        LibXmlMemInfo initAppMemoryInfo;
        LibXmlMFC initMfc;
        string _Result = "";
        #endregion //Variables
        #region Propiedades

        public IList<Rendicion> ListRendicion {
            get { return _ListRendicion; }
            set { _ListRendicion = value; }
        }
        public string ResultadoOperacion {
            get { return _Result; }
            set { _Result = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsRendicionIpl(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
            ListRendicion = new List<Rendicion>();
            ListRendicion.Add(new Rendicion());
            Clear(ListRendicion[0]);
            this.initAppMemoryInfo = initAppMemoryInfo;
            this.initMfc = initMfc;
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(Rendicion refRecord) {
            refRecord.Clear();
            refRecord.ConsecutivoCompania = Mfc.GetInt("Compania");
			refRecord.DetailDetalleDeRendicion.Clear();
        }

        #region Inicializacion BRL - a modificar si Remoting
     private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessMasterComponent<IList<Rendicion>, IList<Rendicion>>)RegisterType();
            } else {
                _Reglas = new Galac.Adm.Brl.CajaChica.clsRendicionNav(initAppMemoryInfo,initMfc);
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((Rendicion)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((Rendicion)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Rendicion"; }
            
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((Rendicion)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((Rendicion)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            return GenerarProximoConsecutivoRendicion(valSequentialName);
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            outErrorMsg = "";
            if (valAction.Equals(eAccionSR.Cerrar.ToString()))
                return cerrar(((Rendicion)refRecord),eAccionSR.Cerrar,out outErrorMsg).Success;
            else if (valAction.Equals(eAccionSR.Anular.ToString()))
                return Anular(((Rendicion)refRecord), eAccionSR.Anular, out outErrorMsg).Success;
            else
                return false;

        }

       
        #endregion //ILibView

        internal LibResponse cerrar(Rendicion refRecord, eAccionSR valAction, out string outErrorMessage) {
            LibResponse vResult = new LibResponse();
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                vResult = ((IRendicionPdn)_Reglas).cerrar(refRecord);
            }
            if (vResult.Info != null) {
                vResult.Info.MoveToContent();
                vResult.Info.Read();
                while (!vResult.Info.EOF) {
                    if (vResult.Info.Name == "Mensaje")
                        outErrorMessage = vResult.Info.ReadElementContentAsString();
                    else if (vResult.Info.Name == "DatosContab")
                        ResultadoOperacion = vResult.Info.ReadInnerXml();
                        vResult.Info.Read();
                }
            }
            return vResult;
        }

        internal LibResponse Anular(Rendicion refRecord, eAccionSR valAction, out string outErrorMessage) {
            LibResponse vResult = new LibResponse();
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                vResult = ((IRendicionPdn)_Reglas).anular(refRecord);
            }
            if (vResult.Info != null) {
                vResult.Info.MoveToContent();
                vResult.Info.Read();
                while (!vResult.Info.EOF) {
                    if (vResult.Info.Name == "Mensaje")
                        outErrorMessage = vResult.Info.ReadElementContentAsString();
                    else if (vResult.Info.Name == "DatosContab")
                        ResultadoOperacion = vResult.Info.ReadInnerXml();
                    vResult.Info.Read();
                }
            }
            return vResult;
        }


       // ####### cambiar [PrincipalPermission(SecurityAction.Demand, Role = "Rendicion.Insertar")]
        internal bool InsertRecord(Rendicion refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<Rendicion> vBusinessObject = new List<Rendicion>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null, true).Success;
              //  vResult = ((ILibBusinessMasterComponent<IList<Anticipo>, IList<Anticipo>>) new Galac.Adm.Brl.CajaChica.clsAnticipoNav()).DoAction(vBusinessObject, eAccionSR.Insertar, null, true).Success;
                
            }
            return vResult;
        }

       // [PrincipalPermission(SecurityAction.Demand, Role = "Rendicion.Modificar")]
        internal bool UpdateRecord(Rendicion refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<Rendicion> vBusinessObject = new List<Rendicion>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null, true).Success;
                
            }
            return vResult;
        }

       // [PrincipalPermission(SecurityAction.Demand, Role = "Rendicion.Eliminar")]
        internal bool DeleteRecord(Rendicion refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<Rendicion> vBusinessObject = new List<Rendicion>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null, true).Success;
            return vResult;
        }

        public void FindAndSetObject(int valConsecutivoCompania, int valConsecutivo) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valConsecutivo);
            ListRendicion = _Reglas.GetData(eProcessMessageType.SpName, "RendicionGET", vParams.Get(), true);
        }

        public bool ValidateAll(Rendicion refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidNumero(valAction, refRecord.Numero, false);
            vResult = IsValidConsecutivoBeneficiario(valAction, refRecord.ConsecutivoBeneficiario, false) && vResult;
            vResult = IsValidFechaApertura(valAction, refRecord.FechaApertura, false) && vResult;
            if (valAction.Equals(eAccionSR.Cerrar)) {
                vResult = IsValidFechaCierre(valAction, refRecord.FechaCierre, false) && vResult;
                vResult = IsValidCodigoCuentaBancaria(valAction, refRecord.CodigoCuentaBancaria, false) && vResult;
                vResult = IsValidCodigoConceptoBancario(valAction, refRecord.CodigoConceptoBancario, false) && vResult;
                //vResult = IsValidCodigoCtaBancariaCajaChica(valAction, refRecord.CodigoCtaBancariaCajaChica, false) && vResult;
				 vResult = IsValidNumeroDocumento(valAction, refRecord.NumeroDocumento, false) && vResult;
                 vResult = IsValidBeneficiarioCheque(valAction, refRecord.BeneficiarioCheque, false) && vResult;
            }

            if (valAction.Equals(eAccionSR.Anular))
            vResult = IsValidFechaAnulacion(valAction, refRecord.FechaAnulacion, false) && vResult;
            
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidNumero(eAccionSR valAction, string valNumero, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valNumero, true)) {
                BuildValidationInfo(MsgRequiredField("Numero"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidConsecutivoBeneficiario(eAccionSR valAction, int valConsecutivoBeneficiario, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valConsecutivoBeneficiario == 0) {
                BuildValidationInfo(MsgRequiredField("Código del Beneficiario"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidFechaApertura(eAccionSR valAction, DateTime valFechaApertura, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaApertura, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidFechaCierre(eAccionSR valAction, DateTime valFechaCierre, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaCierre, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidFechaAnulacion(eAccionSR valAction, DateTime valFechaAnulacion, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaAnulacion, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }
        public bool IsValidCodigoCuentaBancaria(eAccionSR valAction, string valCodigoCuentaBancaria, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCodigoCuentaBancaria, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Bancaria"));
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
                BuildValidationInfo(MsgRequiredField("Numero Documento"));
                vResult = false;
            }
            return vResult;
        }    


        public bool IsValidCodigoConceptoBancario(eAccionSR valAction, string valCodigoConceptoBancario, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            //if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) { 
            //    return true;
            //}
            //if (valCleanInfoBeforeStart) {
            //    ClearValidationInfo();
            //}
            //if (LibString.IsNullOrEmpty(valCodigoConceptoBancario, true)) {
            //    BuildValidationInfo(MsgRequiredField("Concepto Bancario"));
            //    vResult = false;
            //}
            return vResult;
        }

 public bool IsValidBeneficiarioCheque(eAccionSR valAction, string valBeneficiarioCheque, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valBeneficiarioCheque, true)) {
                BuildValidationInfo(MsgRequiredField("Beneficiario Cheque"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCodigoCtaBancariaCajaChica(eAccionSR valAction, string valCodigoCtaBancariaCajaChica, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCodigoCtaBancariaCajaChica, true)) {
                BuildValidationInfo(MsgRequiredField("Caja Chica"));
                vResult = false;
            }
            return vResult;
        }
        public void InitDetailForInsert() {
            InitDetailDetDeRenForInsert();
        }

        void InitDetailDetDeRenForInsert() {
            if (ListRendicion[0].DetailDetalleDeRendicion == null) {
                ListRendicion[0].DetailDetalleDeRendicion = new ObservableCollection<DetalleDeRendicion>();
            }
            Clear(ListRendicion[0]);
        }
        #endregion //Metodos Generados


        #region Metodos Creados

        private string GenerarProximoConsecutivoRendicion(string val)
        {
            string vResult = "";
            RegistraCliente();
            XElement vResulset = (_Reglas).QueryInfo(eProcessMessageType.Message, "ProximoConsecutivoRendicion", Mfc.GetIntAsParam("Compania"), true);
            vResult = LibXml.GetPropertyString(vResulset, val);
            return vResult;
        }
        
        #endregion

    } //End of class clsRendicionesIpl

} //End of namespace Galac.Saw.Uil.Rendicion

