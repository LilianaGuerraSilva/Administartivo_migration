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
using LibGalac.Aos.Brl;

namespace Galac.Saw.Uil.Inventario {
    public class clsColorIpl: LibMROMF, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<Color>, IList<Color>> _Reglas;
        IList<Color> _ListColor;
        #endregion //Variables
        #region Propiedades

        public IList<Color> ListColor {
            get { return _ListColor; }
            set { _ListColor = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsColorIpl(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
            ListColor = new List<Color>();
            ListColor.Add(new Color());
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(Color refRecord) {
            refRecord.Clear();
            refRecord.ConsecutivoCompania = Mfc.GetInt("Compania");
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<Color>, IList<Color>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Inventario.clsColorNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((Color)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((Color)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Color"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((Color)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((Color)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Color.Insertar")]
        internal bool InsertRecord(Color refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<Color> vBusinessObject = new List<Color>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Color.Modificar")]
        internal bool UpdateRecord(Color refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<Color> vBusinessObject = new List<Color>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Color.Eliminar")]
        internal bool DeleteRecord(Color refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<Color> vBusinessObject = new List<Color>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(int valConsecutivoCompania, string valCodigoColor) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoColor", valCodigoColor, 3);
            ListColor = _Reglas.GetData(eProcessMessageType.SpName, "ColorGET", vParams.Get());
        }

        public bool ValidateAll(Color refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigoColor(valAction, refRecord.CodigoColor, false);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidCodigoColor(eAccionSR valAction, string valCodigoColor, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            valCodigoColor = LibString.Trim(valCodigoColor);
            if (LibString.IsNullOrEmpty(valCodigoColor, true)) {
                BuildValidationInfo(MsgRequiredField("Codigo Color"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsColorIpl

} //End of namespace Galac.Saw.Uil.Inventario

