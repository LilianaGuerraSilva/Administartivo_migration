using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Venta {
    public interface IFacturaPdn {
        bool CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow);
        bool GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression);
        XElement GetFk(string valCallingModule, StringBuilder valParameters);
        //XElement FindByNumero(int valConsecutivoCompania, string valNumero);
        string MensajeDeNotificacionSiEsNecesario(string valActionStr, int valConsecutivoCompania, string valNumero, int valTipoDocumento);

    } //End of class IFacturaPdn
} //End of namespace Galac.Adm.Ccl.Venta
