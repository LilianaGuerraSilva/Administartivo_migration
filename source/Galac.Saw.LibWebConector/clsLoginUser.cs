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
    internal struct stUserLoginCnn {
        public string usuario {
            get; set;
        }
        public string clave {
            get; set;
        }
    }
   
    public struct stPostResq {
        [JsonProperty("token")]
        public string token {
            get; set;
        }

        [JsonProperty("expiracion")]
        public string expiracion {
            get; set;
        }

        [JsonProperty("codigo")]
        public string codigo {
            get; set;
        }

        [JsonProperty("mensaje")]
        public string mensaje {
            get; set;
        }

        [JsonProperty("resultado")]
        public stRespuesta resultados { get; set; }
        
        [JsonProperty("estado")]
        public stEstado estado { get; set; }

    }
    public struct stRespuesta {             
        [JsonProperty("tipoDocumento")]
        public string tipoDocumento {
            get; set;
        }

        [JsonProperty("numeroDocumento")]
        public string numeroDocumento {
            get; set;
        }

        [JsonProperty("numeroControl")]
        public string numeroControl {
            get; set;
        }

        [JsonProperty("fechaAsignacion")]
        public string fechaAsignacion {
            get; set;
        }

        [JsonProperty("horaAsignacion")]
        public string horaAsignacion {
            get; set;
        }

        [JsonProperty("estado")]
        public string Estado { 
            get; set; 
        }
    }

    public struct stEstado {
        [JsonProperty("estadoDocumento")]
        public string estadoDocumento {
            get; set;
        }

        [JsonProperty("tipoDocumento")]
        public string tipoDocumento {
            get; set;
        }

        [JsonProperty("numeroDocumento")]
        public string numeroDocumento {
            get; set;
        }

        [JsonProperty("numeroControl")]
        public string numeroControl {
            get; set;
        }

        [JsonProperty("fechaAsignacion")]
        public string fechaAsignacion {
            get; set;
        }

        [JsonProperty("horaAsignacion")]
        public string horaAsignacion {
            get; set;
        }

    }

    public struct stSolicitudDeAccion {
        [JsonProperty("serie")]
        public string Serie {
            get; set;
        }
        [JsonProperty("tipoDocumento")]
        public string TipoDocumento {
            get; set;
        }
        [JsonProperty("numeroDocumento")]
        public string NumeroDocumento {
            get; set;
        }
        [JsonProperty("motivoAnulacion")]
        public string MotivoAnulacion {
            get; set;
        }
    }

    public struct stSolicitudDeConsulta {
        [JsonProperty("serie")]
        public string Serie {
            get; set;
        }
        [JsonProperty("tipoDocumento")]
        public string TipoDocumento {
            get; set;
        }
        [JsonProperty("numeroDocumento")]
        public string NumeroDocumento {
            get; set;
        }
    }
}
