using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.LibWebConnector {
    public interface ILoginUser {
        string URL { get; set; }
        string User { get; set;}
        string Password { get; set; }
        string MessageResult { get; set; }
        string UserKey { get; set; }
        string PasswordKey { get; set; }
    }
}
