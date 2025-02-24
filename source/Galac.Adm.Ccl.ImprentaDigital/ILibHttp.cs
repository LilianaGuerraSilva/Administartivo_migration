using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.Ccl.ImprentaDigital {
    public interface ILibHttp {
        string HttpExecutePost(string valJsonStr, string valUrl, string valComandoApi, string valToken);
    }
}
