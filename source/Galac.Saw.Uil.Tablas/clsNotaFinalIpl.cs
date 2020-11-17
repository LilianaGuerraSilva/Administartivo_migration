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
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Uil.Tablas {
    public class clsNotaFinalIpl: LibMROMF, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<NotaFinal>, IList<NotaFinal>> _Reglas;
        IList<NotaFinal> _ListNotaFinal;
        #endregion //Variables
        #region Propiedades

        public IList<NotaFinal> ListNotaFinal {
            get { return _ListNotaFinal; }
            set { _ListNotaFinal = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsNotaFinalIpl(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
            ListNotaFinal = new List<NotaFinal>();
            ListNotaFinal.Add(new NotaFinal());
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(NotaFinal refRecord) {
            refRecord.Clear();
            refRecord.ConsecutivoCompania = Mfc.GetInt("Compania");
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<NotaFinal>, IList<NotaFinal>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Tablas.clsNotaFinalNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((NotaFinal)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((NotaFinal)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Nota Final"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((NotaFinal)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((NotaFinal)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        internal bool InsertRecord(NotaFinal refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<NotaFinal> vBusinessObject = new List<NotaFinal>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Modificar")]
        internal bool UpdateRecord(NotaFinal refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<NotaFinal> vBusinessObject = new List<NotaFinal>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Eliminar")]
        internal bool DeleteRecord(NotaFinal refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<NotaFinal> vBusinessObject = new List<NotaFinal>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(int valConsecutivoCompania, string valCodigoDeLaNota) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoDeLaNota", valCodigoDeLaNota, 10);
            ListNotaFinal = _Reglas.GetData(eProcessMessageType.SpName, "NotaFinalGET", vParams.Get());
        }

        public bool ValidateAll(NotaFinal refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigoDeLaNota(valAction, refRecord.CodigoDeLaNota, false);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidCodigoDeLaNota(eAccionSR valAction, string valCodigoDeLaNota, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCodigoDeLaNota, true)) {
                BuildValidationInfo(MsgRequiredField("Codigo De La Nota"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsNotaFinalIpl

} //End of namespace Galac.Saw.Uil.Tablas

