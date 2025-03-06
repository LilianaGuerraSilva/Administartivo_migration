using Galac.Saw.Brl.SttDef;
using Galac.Saw.Ccl.SttDef;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.InteropServices;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Mvvm.Messaging;
using System.Text;

namespace Galac.Saw.Wrp.Administrativo {
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpVersionApi : System.EnterpriseServices.ServicedComponent, IWrpVersionApi { 
        #region Variables
        string _Title = "VersionApi";
        #endregion //Variables
        
        private string Title {
            get { return _Title; }
        }
        bool IWrpVersionApi.EstaHomologadaLaVersion(string valVersionActual) {
            try {
                StringBuilder vMensajeHologacion = new StringBuilder();
                ISettValueByCompanyPdn insSettValueByCompanyNav = new clsSettValueByCompanyNav();
                var jsonString = insSettValueByCompanyNav.VersionHomologada();
                JObject json = JObject.Parse(jsonString);
                var versionhomologada = json["value"].Value<string>();
                vMensajeHologacion.AppendLine("Está utilizando una versión desactualizada del sistema que ya NO ESTÁ homologada por el SENIAT.");
                vMensajeHologacion.AppendLine("Es importante que actualice a la versión " + versionhomologada + " que es la versión que actualmente está homologada por el SENIAT.");
                vMensajeHologacion.AppendLine("No actualizar la versión le puede acarrear sanciones por incumplimiento de la Providencia Administrativa 00121 de fecha 19 / 12 / 2024, emitida por el SENIAT.");
                bool result = versionhomologada == valVersionActual;
                if (!result) {
                    LibMessages.MessageBox.Alert(null, vMensajeHologacion.ToString(), "ADVERTENCIA - Validación de Versión Homologada por SENIAT");
                }
                return result;
            } catch (Exception) {
                return false;
            }
        }
    }
}
