using System;
using System.Collections.Generic;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Catching;
using Galac.Adm.Brl.ImprentaDigital;
using System.Threading.Tasks;
using System.Threading;
using LibGalac.Aos.UI.Mvvm.Messaging;

namespace Galac.Adm.Uil.ImprentaDigital {
    public class clsDocumentoDigitalMenu: ILibMenu {
        #region Metodos Generados       
        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            new NotImplementedException();
        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return false; // LibFKRetrievalHelper.ChooseRecord<FkDocumentoDigitalViewModel>("Imprenta Digital", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsDocumentoDigitalNav());
        }     
        #endregion //Metodos Generados
    } //End of class clsDocumentoDigitalMenu
} //End of namespace Galac.Adm.Uil.ImprentaDigital

