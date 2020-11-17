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
    public class clsPropAnalisisVencIpl: LibMRO, ILibView {
        #region Variables
        ILibBusinessComponent<IList<PropAnalisisVenc>, IList<PropAnalisisVenc>> _Reglas;
        IList<PropAnalisisVenc> _ListPropAnalisisVenc;
        #endregion //Variables
        #region Propiedades

        public IList<PropAnalisisVenc> ListPropAnalisisVenc {
            get { return _ListPropAnalisisVenc; }
            set { _ListPropAnalisisVenc = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsPropAnalisisVencIpl() {
            ListPropAnalisisVenc = new List<PropAnalisisVenc>();
            ListPropAnalisisVenc.Add(new PropAnalisisVenc());
        }
        public clsPropAnalisisVencIpl(int initSecuencialUnique0) {
            ListPropAnalisisVenc = new List<PropAnalisisVenc>();
            FindAndSetObject();
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(PropAnalisisVenc refRecord) {
            refRecord.Clear();
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponent<IList<PropAnalisisVenc>, IList<PropAnalisisVenc>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Tablas.clsPropAnalisisVencNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((PropAnalisisVenc)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            throw new NotImplementedException();
        }

        string ILibView.MessageName {
            get { return "Prop Analisis Venc"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((PropAnalisisVenc)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            throw new NotImplementedException();
        }

        object ILibView.NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Modificar")]
        internal bool UpdateRecord(PropAnalisisVenc refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<PropAnalisisVenc> vBusinessObject = new List<PropAnalisisVenc>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        public bool FindAndSetObject() {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("SecuencialUnique0",1);
            ListPropAnalisisVenc = _Reglas.GetData(eProcessMessageType.SpName, "PropAnalisisVencGET", vParams.Get());
            return (ListPropAnalisisVenc.Count > 0);
        }

        public bool ValidateAll(PropAnalisisVenc refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidPrimerVencimiento(valAction, refRecord.PrimerVencimiento, false);
            vResult = IsValidSegundoVencimiento(valAction, refRecord.SegundoVencimiento, false) && vResult;
            vResult = IsValidTercerVencimiento(valAction, refRecord.TercerVencimiento, false) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidPrimerVencimiento(eAccionSR valAction, int valPrimerVencimiento, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valPrimerVencimiento == 0) {
                BuildValidationInfo(MsgRequiredField("Primer Vencimiento"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidSegundoVencimiento(eAccionSR valAction, int valSegundoVencimiento, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valSegundoVencimiento == 0) {
                BuildValidationInfo(MsgRequiredField("Segundo Vencimiento"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidTercerVencimiento(eAccionSR valAction, int valTercerVencimiento, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valTercerVencimiento == 0) {
                BuildValidationInfo(MsgRequiredField("Tercer Vencimiento"));
                vResult = false;
            }
            return vResult;
        }

       public bool EscogerPropAnalisisVencActual() {
           bool vResult = false;
           if (FindAndSetObject()) {
               vResult = true;
           } else {
           /* IMPLEMENTE EL SIGUIENTE CODIGO
               if (((IPropAnalisisVencPdn)_Reglas).InsertarValoresPorDefecto()) {
                   vResult = FindAndSetObject();
               }
           */
           }
           if (vResult) {
               //FALTA IMPLEMENTAR Set Valores Globales (LibGlobalValues)
           }
           return vResult;
       }
        #endregion //Metodos Generados


    } //End of class clsPropAnalisisVencIpl

} //End of namespace Galac.Saw.Uil.Tablas

