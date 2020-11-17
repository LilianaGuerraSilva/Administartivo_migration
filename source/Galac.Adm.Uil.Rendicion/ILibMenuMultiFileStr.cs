using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;

namespace Galac.Adm.Uil.CajaChica {
   public interface ILibMenuMultiFileStr : LibGalac.Aos.Base.ILibMenuMultiFile {

         string EjecutaStr(eAccionSR valAction, int handler, IDictionary<string, XmlDocument> refGlobalValues);
    }
}
 