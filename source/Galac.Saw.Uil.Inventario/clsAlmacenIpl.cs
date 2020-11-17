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
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Uil.Inventario {
    public class clsAlmacenIpl : LibMROMF, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<Almacen>, IList<Almacen>> _Reglas;
        IList<Almacen> _ListAlmacen;
        #endregion //Variables
        #region Propiedades

        public IList<Almacen> ListAlmacen {
            get { return _ListAlmacen; }
            set { _ListAlmacen = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsAlmacenIpl(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
            ListAlmacen = new List<Almacen>();
            ListAlmacen.Add(new Almacen());
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(Almacen refRecord) {
            refRecord.Clear();
            refRecord.ConsecutivoCompania = Mfc.GetInt("Compania");
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<Almacen>, IList<Almacen>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Inventario.clsAlmacenNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((Almacen)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((Almacen)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Almacén"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((Almacen)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((Almacen)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Almacén.Insertar")]
        internal bool InsertRecord(Almacen refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                if (refRecord.ConsecutivoCliente==0) {
                    Galac.Saw.Brl.Inventario.clsAlmacenNav insBrl = new Brl.Inventario.clsAlmacenNav();
                    int vConsecutivoGenericoCliente = ((int)insBrl.ConsultaCampoClientePorCodigo("Consecutivo", "000000000A", refRecord.ConsecutivoCompania));
                    refRecord.ConsecutivoCliente = vConsecutivoGenericoCliente;
                }
                IList<Almacen> vBusinessObject = new List<Almacen>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Almacén.Modificar")]
        internal bool UpdateRecord(Almacen refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                if (refRecord.ConsecutivoCliente == 0) {
                    Galac.Saw.Brl.Inventario.clsAlmacenNav insBrl = new Brl.Inventario.clsAlmacenNav();
                    int vConsecutivoGenericoCliente = ((int)insBrl.ConsultaCampoClientePorCodigo("Consecutivo", "000000000A", refRecord.ConsecutivoCompania));
                    refRecord.ConsecutivoCliente = vConsecutivoGenericoCliente;
                }
                IList<Almacen> vBusinessObject = new List<Almacen>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Almacén.Eliminar")]
        internal bool DeleteRecord(Almacen refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<Almacen> vBusinessObject = new List<Almacen>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(int valConsecutivoCompania, int valConsecutivo) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valConsecutivo);
            ListAlmacen = _Reglas.GetData(eProcessMessageType.SpName, "AlmacenGET", vParams.Get());
        }

        public bool ValidateAll(Almacen refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigo(valAction, refRecord.Codigo, false);
            vResult = IsValidConsecutivoCliente(valAction, refRecord.ConsecutivoCliente, false) && vResult;
            vResult = IsValidCodigoCc(valAction, refRecord.CodigoCc, false) && vResult;
            vResult = IsValidDescripcion(valAction, refRecord.Descripcion, false) && vResult;
            outErrorMessage = Information.ToString();
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

        public bool IsValidConsecutivoCliente(eAccionSR valAction, int valConsecutivoCliente, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            return vResult;
        }

        public bool IsValidCodigoCc(eAccionSR valAction, string valCodigoCc, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibConvert.SNToBool(AppMemoryInfo.GlobalValuesGetString("RecordName", "AsociaCentroDeCostoyAlmacen"))
                && LibConvert.SNToBool(AppMemoryInfo.GlobalValuesGetString("RecordName", "UsaCentroDeCostos"))) {
                if (LibString.IsNullOrEmpty(valCodigoCc, true)) {
                    BuildValidationInfo(MsgRequiredField("Código Centro de Costos"));
                    vResult = false;
                }
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
            if (LibConvert.SNToBool(AppMemoryInfo.GlobalValuesGetString("RecordName", "AsociaCentroDeCostoyAlmacen"))
                && LibConvert.SNToBool(AppMemoryInfo.GlobalValuesGetString("RecordName", "UsaCentroDeCostos"))) {
                if (LibString.IsNullOrEmpty(valDescripcion, true)) {
                    BuildValidationInfo(MsgRequiredField("Descripción Centro de Costos"));
                    vResult = false;
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados

    } //End of class clsAlmacenIpl

} //End of namespace Galac.Saw.Uil.Inventario

