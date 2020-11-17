using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Banco {
    public interface ICuentaBancariaPdn: ILibPdn {
        void GeneraCuentaBancariaGenericaSiCiaNoGeneraMovimientosBancarios(int vfwConsecutivoCompania, int vfwCodigoBanco, string valCodigoMonedaLocal, string valNombreMonedaLocal);
        void InsertaCuentaBancariaGenericaSiHaceFalta(int vfwConsecutivoCompania, int vfwCodigoBanco, string valCodigoMonedaLocal, string valNombreMonedaLocal);
        bool ActualizaSaldoDisponibleEnCuenta(int vfwlConsecutivoCompania, string vfwCodigoCuenta, string vfwMonto, string vfwIngresoEgreso, int vfwmAction, string vfwMontoOriginal, bool vfwSeModificoTipoConcepto);
        bool RecalculaSaldoCuentasBancarias(int valConsecutivoCompania);
        string GetCuentaBancariaGenericaPorDefecto();
        bool TieneMovimientosBancariosDeReposicionCajaChica(int valConsecutivoCompania, string valCodigoCuentaBancaria);
        bool EsValidaCuentaBancariaCajaChica(int valConsecutivoCompania, string valCodigoCuentaBancaria);
        bool ExistenMovimientosCuentaBancaria(int valConsecutivoCompania);
    }
    
}
