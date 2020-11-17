using System;
using System.Collections.Generic;
using System.Linq;
using LibGalac.Aos.Catching;
using System.Text;
using System.IO;
using System.Net;

namespace Galac.Saw.Lib {
    internal class ConexionWebSunat {

        internal List<string> GetInfoDelDia(string UriBaseSunat) {

            WebClient vDataSunat = new WebClient();
            string vData = vDataSunat.DownloadString(UriBaseSunat);
            List<string> vElements = vData.Split('|').ToList();
            vElements.Remove(vElements.Last());
            return vElements;            
        }

        private string RequestWeb(string valURL) {
           // try {
                string vResult = string.Empty;
                HttpWebRequest vRequest = (HttpWebRequest)WebRequest.Create(valURL);
                HttpWebResponse vResponse = (HttpWebResponse)vRequest.GetResponse();
                if (vResponse.StatusCode == HttpStatusCode.OK) {
                    Stream vReceiveStream = vResponse.GetResponseStream();
                    StreamReader vReadStream = null;
                    if (vResponse.CharacterSet == null) {
                        vReadStream = new StreamReader(vReceiveStream);
                    } else {
                        vReadStream = new StreamReader(vReceiveStream, Encoding.GetEncoding(vResponse.CharacterSet));
                    }
                    vResult = vReadStream.ReadToEnd();
                    vResponse.Close();
                    vReadStream.Close();
                }
                return vResult;

//            } catch (WebException vEx) {
//                throw new GalacException("Error de conexion en timeout  " + vEx.Message, eExceptionManagementType.Alert);                
 //           } catch (Exception vEx2) {                
  //              throw new GalacException("Error de conexion global " + vEx2.Message, eExceptionManagementType.Alert);                
   //         }           
        }

    }
}
