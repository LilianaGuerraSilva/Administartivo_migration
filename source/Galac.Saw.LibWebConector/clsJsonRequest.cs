using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Galac.Saw.LibWebConnector {
    #region TheFactory
    internal struct stUserLoginCnn {
        public string usuario {
            get; set;
        }
        public string clave {
            get; set;
        }
    }
    public struct stRespuestaTF {
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

        [JsonProperty("validaciones")]
        public List<string> validaciones {
            get; set;
        }

        [JsonProperty("resultado")]
        public stResultadoTF resultados { get; set; }

        [JsonProperty("estado")]
        public stEstado estado { get; set; }

        public bool Aprobado { get; set; }

    }
    public struct stResultadoTF {
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

        [JsonProperty("fechaAsignacionNumeroControl")]
        public string fechaAsignacionNumeroControl {
            get; set;
        }

        [JsonProperty("horaAsignacionNumeroControl")]
        public string horaAsignacionNumeroControl {
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
    #endregion TheFactory    
    /////////////////////////////////////////////////////////////////////////////////////////    
    #region NOVUS
    public struct stErrorRespuestaNV {
        public string message { get; set; }
        public string code { get; set; }
    }

    public struct stDataRespuestaNV {
        [JsonProperty("numerodocumento")]
        public string numerodocumento { get; set; }
        [JsonProperty("identificador")]
        public string identificador { get; set; }
        [JsonProperty("corelativo")]
        public string corelativo { get; set; }
        [JsonProperty("documento")]
        public string documento { get; set; }
        [JsonProperty("datetime")]
        public DateTime datetime { get; set; }
        [JsonProperty("fecha")]
        public string fecha { get; set; }
        [JsonProperty("hora")]
        public string hora { get; set; }        
    }


    public struct stRespuestaNV {
        [JsonProperty("success")]
        public bool success { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }        
        [JsonProperty("data")]
        public stDataRespuestaNV? data { get; set; }
        [JsonProperty("error")]        
        public stErrorRespuestaNV? error { get; set; }

    }

    public struct stDataRespuestaStatusNV {
        [JsonProperty("numerodocumento")]
        public string numerodocumento { get; set; }
        [JsonProperty("numerointerno")]
        public string numerointerno { get; set; }
        [JsonProperty("fecha")]
        public string fecha { get; set; }
        [JsonProperty("documento")]
        public string documento { get; set; }
        [JsonProperty("idtipodocumento")]
        public string idtipodocumento { get; set; }

    }

    public struct stRespuestaStatusNV {
        [JsonProperty("success")]
        public bool success { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("data")]
        public stDataRespuestaStatusNV?[] data { get; set; }
        [JsonProperty("error")]
        public stErrorRespuestaNV? error { get; set; }
    }
    #endregion NOVUS
    #region UNIDIGITAL	   
    public struct stRespuestaUD {
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

        [JsonProperty("strongId")]
        public string strongId {
            get; set;
        }

        [JsonProperty("estado")]
        public stEstado estado {
            get; set;
        }

        [JsonProperty("ImprentaDigitalGUID")]
        public string IDGUID {
            get; set;
        }

        public bool Aprobado {
            get; set;
        }

    }





        #endregion UNIDIGITAL
    }

