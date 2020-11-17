using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Galac.Adm.Ccl.GestionCompras {
    public class ResultUpdatedRecord {
        public bool Succesfull { get; set; }
        public XmlDocument UpdatedRecord { get; set; }
    }
}
