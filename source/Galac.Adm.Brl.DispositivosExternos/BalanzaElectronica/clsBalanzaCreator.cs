using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Galac.Adm.Ccl.DispositivosExternos;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Brl.DispositivosExternos.BalanzaElectronica {
    public static  class clsBalanzaCreator {
        public static clsBalanza Create(XElement balanzaDat) {            
            eModeloDeBalanza vModelo = (eModeloDeBalanza)LibConvert.DbValueToEnum(LibXml.GetPropertyString(balanzaDat,"Modelo"));           
            switch (vModelo) {
                case eModeloDeBalanza.Xacta:
                case eModeloDeBalanza.AclasOS2X:
                    return new clsBalanzaXACTA(new clsConexionPuertoSerial(balanzaDat),vModelo);
                case eModeloDeBalanza.OHAUST23:
                    return new clsBalanzaOHAUST23(new clsConexionPuertoSerial(balanzaDat));
                default:
                    throw new GalacException("Modelo de Balanza aun no implementado", eExceptionManagementType.Controlled);
            }
        }
    }
}
