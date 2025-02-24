using Galac.Adm.Ccl.DispositivosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Adm.Ccl.Venta {

    public interface IFkCajaViewModel {
        #region Propiedades
        int ConsecutivoCompania { get; set; }
        int Consecutivo { get; set; }
        bool UsaMaquinaFiscal { get; set; }
        string NombreCaja { get; set; }
        bool UsaGaveta { get; set; }      
        ePuerto Puerto { get; set; }
        string Comando { get; set; }
        bool PermitirAbrirSinSupervisor { get; set; }
        bool UsaAccesoRapido { get; set; }
        eFamiliaImpresoraFiscal FamiliaImpresoraFiscal { get; set; }
        eImpresoraFiscal ModeloDeMaquinaFiscal { get; set; }
        string SerialDeMaquinaFiscal { get; set; }
        ePuerto PuertoMaquinaFiscal { get; set; }
        eTipoConexion TipoConexion { get; set; }
        bool AbrirGavetaDeDinero { get; set; }
        string PrimerNumeroComprobanteFiscal { get; set; }
        string PrimeNumeroNCFiscal { get; set; }
        string IpParaConexion { get; set; }
        string MascaraSubred { get; set; }
        string Gateway { get; set; }
        bool PermitirDescripcionDelArticuloExtendida { get; set; }
        bool PermitirNombreDelClienteExtendido { get; set; }
        bool UsarModoDotNet { get; set; }
        string NombreOperador { get; set; }
        DateTime FechaUltimaModificacion { get; set; }       
        #endregion //Propiedades
    } //End of class IFkCajaViewModel
}//End of namespace Galac.Adm.Ccl.Venta

