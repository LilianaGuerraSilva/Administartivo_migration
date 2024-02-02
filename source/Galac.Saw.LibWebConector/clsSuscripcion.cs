using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using LibGalac.Aos.Base;
using Newtonsoft.Json.Converters;
using LibGalac.Aos.Cnf;
using LibGalac.Aos.Ccl.Settings;
using LibGalac.Aos.Brl.Settings;
using System.Collections.ObjectModel;
using LibGalac.Aos.Catching;
using System.Diagnostics.SymbolStore;

namespace Galac.Saw.LibWebConnector {
    public class clsSuscripcion {
        bool RifInfotax {
            get { return LibString.S1IsEqualToS2(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRIF"), "J305125430"); }
        }

        public string GetRutaEndpointProduccionInterno { get { return ""; } }
        public string GetRutaEndpointProduccionClientes { get { return ""; } }
      
        public class DatosSuscripcion {
            public string TenantNombre { get; set; }
            public string EdicionNombre { get; set; }
            public int NumeroMaximoDeUsuarios { get; set; }
            public bool EstaActivoPeriodoDeGracia { get; set; }
            public DateTime fechaDeFinalizacionDeLaActivacion { get; set; }
            public int CantidadDeUsuariosFacturados { get; set; }
            public DatosSuscripcion() {
                TenantNombre = string.Empty;
                EdicionNombre = string.Empty;
                NumeroMaximoDeUsuarios = 0;
                EstaActivoPeriodoDeGracia = false;
                fechaDeFinalizacionDeLaActivacion = LibDate.MinDateForDB();
                CantidadDeUsuariosFacturados = 0;
            }
            public List<DatosSuscripcionCaracteristicas> Caracteristicas { get; set; } = new List<DatosSuscripcionCaracteristicas>();

        }

        public class CompaniasDelTenant {
            public string numeroDeIdentificacion { get;set; }
            public string nombre { get; set; }
            public bool conectadaConAdministraivo { get; set; }
            public CompaniasDelTenant() {
                numeroDeIdentificacion = string.Empty;
                nombre = string.Empty;
                conectadaConAdministraivo = false;
            }
        }        

        public class DatosSuscripcionCaracteristicas {
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
        }

        public class DatosDeConexion {
            public string RIFCompaniaGVentas { get; set;  }
            public string RIFCompaniaAdministartivo { get; set;  }
            public string NombreCompaniaAdministartivo { get; set;  }
            public string SerialConector { get; set;  }
            public string NombreServidorBdd { get; set;  }
            public string NombreBdd { get; set;  }
            public string NombreUsuarioOperaciones { get; set;  }
            public DatosDeConexion() {
                RIFCompaniaGVentas = string.Empty;
                RIFCompaniaAdministartivo =  string.Empty;
                NombreCompaniaAdministartivo= string.Empty;
                SerialConector  = string.Empty;
                NombreServidorBdd  = string.Empty;
                NombreBdd  = string.Empty;
                NombreUsuarioOperaciones  = string.Empty;
            }
        }

        HttpWebResponse GetResponseGET(string valUrl) {
            try {
                HttpWebResponse vResult;
                Uri vBaseUri = new Uri(GetEndPointGVentas());
                var vRequest = (HttpWebRequest)WebRequest.Create(new Uri(vBaseUri, valUrl));
                vRequest.ContentType = "application/json";
                vRequest.Method = "GET";
                vResult = (HttpWebResponse)vRequest.GetResponse();
                return vResult;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message,eExceptionManagementType.Controlled);                
            }
        }

        HttpWebResponse PostResponse(string valUrl, string valToken, string valBlockData) {
            try {
                HttpWebResponse vResult;
                Uri vBaseUri = new Uri(GetEndPointGVentas());
                HttpWebRequest vRequest = (HttpWebRequest)WebRequest.Create(new Uri(vBaseUri, valUrl));
                vRequest.ContentType = "application/json";
                vRequest.Headers.Add("Authorization", "Bearer " + valToken);
                vRequest.Method = "POST";
                using (var stWriter = new StreamWriter(vRequest.GetRequestStream())) {                   
                    stWriter.Write(valBlockData);
                }
                vResult = (HttpWebResponse)vRequest.GetResponse();
                using (var streamReader = new StreamReader(vResult.GetResponseStream())) {
                    var result = streamReader.ReadToEnd();
                }
                return vResult;
            } catch (Exception vEx) {
                throw vEx;
            }
        }

        string AddParametroNumeroDeIdentificacionFiscal(string valUrl, string valNumeroDeIDentificacionFiscal, string valKeyNumeroDeIdentificacion) {
            string vResult = valUrl;
            if (!LibString.IsNullOrEmpty(valNumeroDeIDentificacionFiscal)) {
                vResult += System.Uri.EscapeDataString(valKeyNumeroDeIdentificacion);
                vResult += "=";
                vResult += System.Uri.EscapeDataString(valNumeroDeIDentificacionFiscal);
            }
            return vResult;
        }

        string GetResultFromResponse(HttpWebResponse valResponse) {
            string vResult = string.Empty;
            if (valResponse != null && (valResponse.StatusCode == HttpStatusCode.OK)) {
                vResult = new StreamReader(valResponse.GetResponseStream()).ReadToEnd();
            }
            return vResult;
        }

        public DatosSuscripcion GetCaracteristicaGVentas(string valNumeroDeIdentificacion) {
            DatosSuscripcion vResult = new DatosSuscripcion();
            try {
                //TODO:Falta Url real
                HttpWebResponse vResponse = GetResponseGET(AddParametroNumeroDeIdentificacionFiscal(@"/api/saas/tenants/datos-suscripcion-por-numero-de-identificacion?", valNumeroDeIdentificacion, "numeroDeIdentificacion"));
                if (vResponse.StatusCode == HttpStatusCode.OK) {
                    vResult = JsonConvert.DeserializeObject<DatosSuscripcion>(GetResultFromResponse(vResponse), new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" });
                    return vResult;
                }
            } catch (Exception) {
                throw;
            }
            return vResult;
        }

        public DatosSuscripcion GetCaracteristicaGVentas() {
            return GetCaracteristicaGVentas(GetNroDeIdentificacionFiscal());
        }

        public bool ActivarConexionGVentas(string valSerialConectorGVentas, string valRIFCompaniaAdministartivo, string valNombreCompaniaAdministartivo, string valNombreUsuarioOperaciones, string valDatabaseName, string valServerName,  string valNroDeIdentificacionFiscal) {
            bool vResult = false;
            try {               
                DatosDeConexion insDatosDeConexion = new DatosDeConexion();
                insDatosDeConexion.NombreBdd = valDatabaseName;
                insDatosDeConexion.NombreServidorBdd = valServerName;
                insDatosDeConexion.NombreUsuarioOperaciones = valNombreUsuarioOperaciones;
                insDatosDeConexion.RIFCompaniaAdministartivo = valRIFCompaniaAdministartivo;
                insDatosDeConexion.RIFCompaniaGVentas = valNroDeIdentificacionFiscal;
                insDatosDeConexion.NombreCompaniaAdministartivo = valNombreCompaniaAdministartivo;
                insDatosDeConexion.SerialConector = valSerialConectorGVentas;
                string JSonData = JsonConvert.SerializeObject(insDatosDeConexion,Formatting.Indented);
                //HttpWebResponse vResponse = PostResponse("", "Token", JSonData);
                vResult = true; //(vResponse.StatusCode == HttpStatusCode.OK);
                return vResult;
            } catch (Exception) {
                throw;
            }            
        }

        public int GetCantidadDeUsuariosActivos(string valNumeroDeIdentificacion) {
            int vResult = -1;
            //try {
            //    //TODO:Falta Url real              
            //    HttpWebResponse vResponse = GetResponseGET(AddParametroNumeroDeIdentificacionFiscal(@"/api/saas/tenants/usuarios-disponibles?", valNumeroDeIdentificacion, "numeroDeIdentificacionFiscal"));
            //    if (vResponse.StatusCode == HttpStatusCode.OK) {
            //        vResult = LibConvert.ToInt(new StreamReader(GetResultFromResponse(vResponse)).ReadToEnd());
            //        return vResult;
            //    }
            //} catch (Exception) {
            //    throw;
            //}
            //
            //
            return vResult;
        }

        public ObservableCollection<string> GetCompaniaGVentas(string valURI) {
            ObservableCollection<string> vResult = new ObservableCollection<string>();
            ObservableCollection<CompaniasDelTenant> vListaCompaniasDelTenant = new ObservableCollection<CompaniasDelTenant>();
            try {
                //  TODO:Falta Url real
                string vNumeroDeIdentificacion = GetNroDeIdentificacionFiscal();
                HttpWebResponse vResponse = GetResponseGET(AddParametroNumeroDeIdentificacionFiscal(@"/api/saas/tenants/companias-del-tenant?", vNumeroDeIdentificacion, "numeroDeIdentificacionFiscal"));
                if (vResponse.StatusCode == HttpStatusCode.OK) {
                    vListaCompaniasDelTenant = JsonConvert.DeserializeObject<ObservableCollection<CompaniasDelTenant>>(GetResultFromResponse(vResponse));
                    var vCompaniasParaMostrar = vListaCompaniasDelTenant.Where(CompaniaEnTenant => !CompaniaEnTenant.conectadaConAdministraivo).Select(CompaniaFueraDelTenant => CompaniaFueraDelTenant.nombre + " | " + CompaniaFueraDelTenant.numeroDeIdentificacion);
                    vResult = new ObservableCollection<string>(vCompaniasParaMostrar);                   
                }
                return vResult;
            } catch (Exception) {
                throw;
            }
        }

        private string GetEndPointGVentas() {
            string vEndpoint = LibAppSettings.ReadAppSettingsKey("URLENDPOINTGVENTAS");
            if (LibString.IsNullOrEmpty(vEndpoint)) { //Si el setting existe sabemos que vamos contra otro endpoint
                if (RifInfotax) { //Validamos si la compañia actual es infotax
                    return GetRutaEndpointProduccionInterno;
                } else {
                    return GetRutaEndpointProduccionClientes;
                }
            } else {
                return vEndpoint;
            }
        }

        static string GetNroDeIdentificacionFiscal() {
            string vNroRif = LibAppSettings.ReadAppSettingsKey("NRORIFQA");
            if (!LibString.IsNullOrEmpty(vNroRif)) { //Si el setting existe sabemos que vamos a utilizar el rif del setting y no la licencia
                return vNroRif;
            } else {
                return IDFiscal();
            }
        }

        private static string IDFiscal() {
            return ((IParametersLibPdn)new LibParametersLibNav()).GetIdFiscal();
        }
    }
}
