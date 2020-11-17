using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using System.Xml;

namespace Galac.Saw.Ccl.Integracion {
    
    public interface IIntegracionSawPdn : ILibPdn {
        bool ConectarCompanias(string valCondigoCompania, string valCodigoConexion);
        bool DesConectarCompanias(string valCodigoConexion);
        bool ActualizaVersion();
        bool InsertaValorPorDefecto();
        bool VersionesCompatibles();
        int GetConsecutivoCompaniaIntegrada(string valCodigoConexion);
        string GetCuentaBancariaGenerica(int valConcecutivoCompania);
        int GetConsecutivoBeneficiarioGenerico(int valConcecutivoCompania);
        
    }
}


      
         
      

      