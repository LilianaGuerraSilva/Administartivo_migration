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
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Uil.Banco {
    public class clsConceptoBancarioIpl: LibMRO, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<ConceptoBancario>, IList<ConceptoBancario>> _Reglas;
        IList<ConceptoBancario> _ListConceptoBancario;
        #endregion //Variables
        #region Propiedades

        public IList<ConceptoBancario> ListConceptoBancario {
            get { return _ListConceptoBancario; }
            set { _ListConceptoBancario = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsConceptoBancarioIpl() {
            ListConceptoBancario = new List<ConceptoBancario>();
            ListConceptoBancario.Add(new ConceptoBancario());
        }
        public clsConceptoBancarioIpl(int initConsecutivo) {
            ListConceptoBancario = new List<ConceptoBancario>();
            FindAndSetObject(initConsecutivo);
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(ConceptoBancario refRecord) {
            refRecord.Clear();
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<ConceptoBancario>, IList<ConceptoBancario>>)RegisterType();
            } else {
                _Reglas = new Galac.Adm.Brl.Banco.clsConceptoBancarioNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((ConceptoBancario)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((ConceptoBancario)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Concepto Bancario"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((ConceptoBancario)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((ConceptoBancario)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Concepto Bancario.Insertar")]
        internal bool InsertRecord(ConceptoBancario refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<ConceptoBancario> vBusinessObject = new List<ConceptoBancario>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Concepto Bancario.Modificar")]
        internal bool UpdateRecord(ConceptoBancario refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<ConceptoBancario> vBusinessObject = new List<ConceptoBancario>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Concepto Bancario.Eliminar")]
        internal bool DeleteRecord(ConceptoBancario refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<ConceptoBancario> vBusinessObject = new List<ConceptoBancario>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(int valConsecutivo) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("Consecutivo", valConsecutivo);
            ListConceptoBancario = _Reglas.GetData(eProcessMessageType.SpName, "ConceptoBancarioGET", vParams.Get());
        }

        public bool ValidateAll(ConceptoBancario refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            //vResult = IsValidConsecutivo(valAction, refRecord.Consecutivo, false);
            vResult = IsValidCodigo(valAction, refRecord.Codigo, false) && vResult;
            vResult = IsValidDescripcion(valAction, refRecord.Descripcion, false) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }
        public bool IsValidConsecutivo(eAccionSR valAction, int valConsecutivo, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valConsecutivo == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCodigo(eAccionSR valAction, string valCodigo, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCodigo, true)) {
                BuildValidationInfo(MsgRequiredField("Codigo"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidDescripcion(eAccionSR valAction, string valDescripcion, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valDescripcion, true)) {
                BuildValidationInfo(MsgRequiredField("Descripcion"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsConceptoBancarioIpl

} //End of namespace Galac.Adm.Uil.Banco

