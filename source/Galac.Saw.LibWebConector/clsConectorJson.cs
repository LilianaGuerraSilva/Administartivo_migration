using System;
using Newtonsoft.Json;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.IO;
using Galac.Saw.Ccl.SttDef;
using Galac.Adm.Ccl.ImprentaDigital;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System.Reflection;
using Castle.Windsor.Installer;

namespace Galac.Saw.LibWebConnector {
    public abstract class clsConectorJson {
        string _Token;
        public string Token {
            get {
                return _Token;
            }
            internal set {
                _Token = value;
            }
        }

        internal ILoginUser LoginUser {
            get; set;
        }

        public clsConectorJson(ILoginUser valloginUser) {
            LoginUser = valloginUser;
            _Token = string.Empty;
        }

        public static string SerializeJSON(object valElemento) {
            try {
                string vResult = JsonConvert.SerializeObject(valElemento, Formatting.Indented);
                return vResult;
            } catch(JsonException) {
                throw;
            } catch(Exception) {
                throw;
            }
        }

        public static string LimpiaRegistrosTempralesEnJSON(string valDocJSon) {
            string vResult = "";
            JObject vResponse = JObject.Parse(valDocJSon);
            RemoveItemArray(vResponse.SelectToken("documentoElectronico.encabezado.totales.formasPago"));
            RemoveItemArray(vResponse.SelectToken("documentoElectronico.encabezado.comprador.correo"));
            RemoveItemArray(vResponse.SelectToken("documentoElectronico.encabezado.comprador.telefono"));
            RemoveItemArray(vResponse.SelectToken("documentoElectronico.detallesItems"));
            RemoveItemArray(vResponse.SelectToken("documentoElectronico.InfoAdicional"));
            vResult = vResponse.ToString(Formatting.Indented);
            return vResult;
        }

        private static void RemoveItemArray(JToken valProperty) {
            if(valProperty != null) {
                valProperty.First().Remove();
            }
        }

        public string GetJsonUser(ILoginUser valloginUser, eProveedorImprentaDigital valProveedorImprentaDigital) {
            string vResult = "";
            string vPassword = valloginUser.Password;
            if(valProveedorImprentaDigital == eProveedorImprentaDigital.Unidigital) {
                vPassword = ComputeSha256Hash(vPassword);
            }
            JObject vLoginUser = new JObject {
                        {valloginUser.UserKey, valloginUser.User},
                        {valloginUser.PasswordKey, vPassword}};
            vResult = vLoginUser.ToString();
            return vResult;
        }

        public void GeneraLogDeErrores(string valMensajeResultado, string valJSon) {
            try {
                string vPath = Path.Combine(LibDirectory.GetProgramFilesGalacDir(), Path.Combine(LibDefGen.ProgramInfo.ProgramInitials, "ImprentaDigital"));
                if(!LibDirectory.DirExists(vPath)) {
                    LibDirectory.CreateDir(vPath);
                }
                vPath = vPath + @"\ImprentaDigitalResult.txt";
                LibFile.WriteLineInFile(vPath, valMensajeResultado + "\r\n" + valJSon, false);
            } catch(Exception) {
                throw;
            }
        }

        private string ComputeSha256Hash(string rawData) {
            using(SHA512 sha256Hash = SHA512.Create()) {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for(int i = 0; i < bytes.Length; i++) {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public abstract bool CheckConnection(ref string refMensaje, string valComandoApi);
        public string ExecutePostJson(string valJsonStr, string valComandoApi, string valToken, string valNumeroDocumento = "", eTipoDocumentoFactura valTipoDocumento = eTipoDocumentoFactura.NoAsignado) {
            try {
                string vResult = string.Empty;
                string strTipoDocumento = LibEnumHelper.GetDescription(valTipoDocumento);
                strTipoDocumento = "La " + strTipoDocumento + " No. " + valNumeroDocumento;
                var vContainer = new WindsorContainer();
                vContainer.Install(FromAssembly.Containing<WindsorInstaller>());
                var myService = vContainer.Resolve<ILibHttp>();
                vResult = myService.HttpExecutePost(valJsonStr, LoginUser.URL, valComandoApi, valToken);
                return vResult;
            } catch(Exception vEx) {
                throw vEx;
            }
        }
		
        public string ExecuteGetJson(string valContent, string valComandoApi, string valToken, string valNumeroDocumento = "", eTipoDocumentoFactura valTipoDocumento = eTipoDocumentoFactura.NoAsignado) {
            try {
                string vResult = string.Empty;
                string strTipoDocumento = LibEnumHelper.GetDescription(valTipoDocumento);
                strTipoDocumento = "La " + strTipoDocumento + " No. " + valNumeroDocumento;
                var vContainer = new WindsorContainer();
                vContainer.Install(FromAssembly.Containing<WindsorInstaller>());
                var myService = vContainer.Resolve<ILibHttp>();
                vResult = myService.HttpExecuteGet(valContent, LoginUser.URL, valComandoApi, valToken);
                return vResult;
            } catch(Exception vEx) {
                throw vEx;
            }
        }     
    }

    #region Clase de Inicializacion del Windsor No Borrar
    public class WindsorInstaller : IWindsorInstaller {
        public void Install(IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store) {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            var dllFiles = Directory.GetFiles(appPath, "Galac.Saw.LibHttp.dll");
            foreach(var dll in dllFiles) {
                try {
                    var assembly = Assembly.LoadFrom(dll);
                    container.Register(
                        Classes.FromAssembly(assembly)
                               .BasedOn<ILibHttp>()
                               .WithService.FromInterface()
                               .LifestyleTransient()
                    );
                } catch(Exception ex) {
                    Console.WriteLine($"No se pudo cargar la DLL: {dll}. Error: {ex.Message}");
                }
            }
        }
    }
    #endregion Clase de Inicializacion del Windsor
}