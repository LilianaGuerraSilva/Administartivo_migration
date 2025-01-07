using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Galac.Adm.Ccl.DispositivosExternos;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Venta {

    public interface ICajaPdn:ILibPdn {
        #region Propiedades Generadas
        string SerialMaquinaFiscal { get; set; }
        #endregion
        #region Metodos Generados
        bool FindByConsecutivoCaja(int valConsecutivoCompania,int valConsecutivoCaja, string vSqlWhere ,ref XElement refXElement);
        string BuscarModeloDeMaquinaFiscal(int valConsecutivoCompania,int valConsecutivoCaja);
        XElement ValidateImpresoraFiscal(ref string refMensaje);
        bool InsertarCajaPorDefecto(int valConsecutivoCompania);
        bool ActualizaUltimoNumComprobante(int valConsecutivoCompania,int valConsecutivoCaja,string valNumero,bool valEsNotaDeCredito);
        void ActualizarRegistroDeMaquinaFiscal(eAccionSR valAccion,int valConsecutivoCompania,eImpresoraFiscal valModeloImpresoraFiscal,string valSerialMaquinaFiscal,string valUltimoNumeroComptbanteFiscal,string valNombreOperador);
        bool ActualizarCierreXEnFacturas(int valConsecutivoCompania,int valConsecutivoCaja, DateTime valFechaModificacion, string valHoraDesde,string valHoraHasta);
        bool ImpresoraFiscalEstaHomologada(int valConsecutivoCompania, int valConsecutivoCaja, ref string refMensaje)     ;
        LibResponse ActualizarYAuditarCambiosMF(IList<Caja> refRecord, bool valAuditarMF, string valMotivoCambiosMaqFiscal, string valFamiliaOriginal, string valModeloOriginal, string valTipoDeConexionOriginal, string valSerialMFOriginal, string valUltNumComprobanteFiscalOriginal, string valUltNumNCFiscalOriginal);
        bool ImpresoraFiscalEstaHomologada(string valFabricante, string valModelo, ref string refMensaje);
        #endregion //Metodos Generados
    } //End of class ICajaPdn
} //End of namespace Galac.Adm.Ccl.Venta

