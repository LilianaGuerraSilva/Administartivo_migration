using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using LibGalac.Aos.Base;

namespace Galac.Adm.Uil.DispositivosExternos
{
    public interface ILibMenuMultiFileStr : ILibMenuMultiFile{
        string EjecutaStr(eAccionSR valAction, int handler, IDictionary<string, XmlDocument> refGlobalValues);
        
    }
}
