using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.LibWebConnector {
    public class clsLoginUser :ILoginUser {
        public string URL {
            get; set;
        }
        public string User {
            get; set;
        }
        public string Password {
            get; set;
        }
        public string MessageResult {
            get; set;
        }
        public string PasswordKey {
            get; set;
        }
        public string UserKey {
            get; set;
        }

        public clsLoginUser() {
            URL = string.Empty;
            User = string.Empty;
            Password = string.Empty;
            MessageResult = string.Empty;
            PasswordKey = string.Empty;
            UserKey = string.Empty;
        }
    }   
}
