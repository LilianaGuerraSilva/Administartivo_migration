using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.Cliente;

namespace Galac.Saw.Uil.Cliente {
    internal class clsDireccionDeDespachoIpl: LibMROMF {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsDireccionDeDespachoIpl(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
        }
        #endregion //Constructores
        #region Metodos Generados

        public bool ValidateAll(DireccionDeDespacho refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigoCliente(valAction, refRecord.CodigoCliente, false);
            vResult = IsValidDireccion(valAction, refRecord.Direccion, false) && vResult;
            vResult = IsValidCiudad(valAction, refRecord.Ciudad, false) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidCodigoCliente(eAccionSR valAction, string valCodigoCliente, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCodigoCliente, true)) {
                BuildValidationInfo(MsgRequiredField("C?digo del Cliente"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidDireccion(eAccionSR valAction, string valDireccion, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valDireccion, true)) {
                BuildValidationInfo(MsgRequiredField("Direcci?n"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCiudad(eAccionSR valAction, string valCiudad, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCiudad, true)) {
                BuildValidationInfo(MsgRequiredField("Ciudad"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsDireccionDeDespachoIpl

} //End of namespace Galac.Saw.Uil.Cliente

