using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
namespace Galac.Adm.Ccl.Venta {

    public interface ICajaAperturaPdn:ILibPdn {
        #region Metodos Generados
        XElement FindByConsecutivoApertura(int valConsecutivoCompania,int valConsecutivoApertura);       
        bool CerrarCaja(CajaApertura vRecord);
        bool GetCajaCerrada(int valConsecutivoCompania,int valConsecutivoCaja,bool valIsCajaCerrada);
        bool UsuarioFueAsignado(int valConsecutivoCompania,int valConsecutivoCaja,string valNombreDelUsuario,bool valIsCajaCerrada,bool valResumenDiario);
        bool TotalesMontosPorFormaDecobro(ref XElement refResult,int valconsecutivoCompania,int valConsecutivoCaja,string valHoraApertura,string valHoraCierre);
        bool AsignarCaja(int ConsecutivoCaja);
        #endregion //Metodos Generados
    } //End of class ICajaAperturaPdn    
} //End of namespace Galac.Adm.Ccl.Venta

