using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibConectorHdl;

namespace TestConexionLogin.Connector {
    public class clsLoginUserCnn:ILoginUser {
        public string URL { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string MessageResult { get; set; }
        public string UserKey {
            get; set;
        }
        public string PasswordKey {
            get; set;
        }

        public clsLoginUserCnn() {
            URL = string.Empty;
            User = string.Empty;
            Password = string.Empty;
            MessageResult = string.Empty;
            PasswordKey = string.Empty; 
            UserKey=string.Empty;   
        }
    }
}
