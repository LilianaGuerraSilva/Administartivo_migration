using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Ccl.SttDef {
    public interface ISettDefinition {
        string Module { get; set; }
        string GroupName { get; set; }
    }
}
