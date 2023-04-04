
using System;
using System.Threading.Tasks;
using Galac.Saw.LibWebConnector;

namespace Galac.Saw.UtiliarioImprentaDigital.Connector {
    public class clsConnector {
        ILoginUser _LoginUser;

        public ILoginUser LoginUser {
            get {
                return _LoginUser;
            }
            set {
                _LoginUser = value;
            }
        }

        public clsConnector(ILoginUser valLoginUser) {
            _LoginUser = valLoginUser;
        }
        
        public string TestConnection() {
            try {              
                clsConectorJson _LibConectorJson = new clsConectorJson(_LoginUser);
                string vReq = _LibConectorJson.CheckConnection();
                return vReq;

            } catch (Exception) {
                throw;
            }
        }
    }
}
