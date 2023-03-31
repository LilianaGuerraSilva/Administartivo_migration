using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace Galac.Saw.UtiliarioImprentaDigital.Connector {
    
    public static class LibUtil {
        public static string SecureStringToString(SecureString vPassword) {
            IntPtr valuePtr = IntPtr.Zero;
            try {
                if (vPassword == null) {
                    vPassword = new SecureString();
                    vPassword.AppendChar('\0');
                }
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(vPassword);
                return Marshal.PtrToStringUni(valuePtr);
            } finally {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
        public static bool URLValidating(string valURL, ref string refMensaje) {
            bool vResult = false;
            Regex Rgx = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");
            vResult = Rgx.IsMatch(valURL);
            if (!vResult) {
                refMensaje = "el formato de la URL es incorrecto,\r\n";
            }
            return vResult;
        }
    }
}
