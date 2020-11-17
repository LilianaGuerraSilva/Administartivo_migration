using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Ccl.Tablas {
    public interface IFkCentrodeCostosViewModel {

        int ConsecutivoCompania { get; set; }
        int Consecutivo { get; set; }
        string Codigo { get; set; }
    }
}
