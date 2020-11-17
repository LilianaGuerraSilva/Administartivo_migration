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
    public class clsCategoriaIpl: LibMROMF, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<Categoria>, IList<Categoria>> _Reglas;
        IList<Categoria> _ListCategoria;
        #endregion //Variables
        #region Propiedades

        public IList<Categoria> ListCategoria {
            get { return _ListCategoria; }
            set { _ListCategoria = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCategoriaIpl(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
            ListCategoria = new List<Categoria>();
            ListCategoria.Add(new Categoria());
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(Categoria refRecord) {
            refRecord.Clear();
            refRecord.ConsecutivoCompania = Mfc.GetInt("Compania");
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<Categoria>, IList<Categoria>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Inventario.clsCategoriaNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((Categoria)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((Categoria)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Categoria"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((Categoria)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((Categoria)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        internal bool InsertRecord(Categoria refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<Categoria> vBusinessObject = new List<Categoria>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Modificar")]
        internal bool UpdateRecord(Categoria refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<Categoria> vBusinessObject = new List<Categoria>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Eliminar")]
        internal bool DeleteRecord(Categoria refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<Categoria> vBusinessObject = new List<Categoria>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(int valConsecutivoCompania, int valConsecutivo) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valConsecutivo);
            ListCategoria = _Reglas.GetData(eProcessMessageType.SpName, "CategoriaGET", vParams.Get());
        }

        public bool ValidateAll(Categoria refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidDescripcion(valAction, refRecord.Descripcion, false);
            outErrorMessage = Information.ToString();
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
            valDescripcion = LibString.Trim(valDescripcion);
            if (LibString.IsNullOrEmpty(valDescripcion, true)) {
                BuildValidationInfo(MsgRequiredField("Categoría"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCategoriaIpl

} //End of namespace Galac.Saw.Uil.Inventario

