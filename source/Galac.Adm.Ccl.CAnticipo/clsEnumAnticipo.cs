using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.CAnticipo {

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eStatusAnticipo {
        [LibEnumDescription("Vigente")]
        Vigente = 0,
        [LibEnumDescription("Anulado")]
        Anulado,
        [LibEnumDescription("Parcialmente  Usado")]
        ParcialmenteUsado,
        [LibEnumDescription("Completamente  Usado")]
        CompletamenteUsado,
        [LibEnumDescription("Completamente  Devuelto")]
        CompletamenteDevuelto,
        [LibEnumDescription("Parcialmente  Devuelto")]
        ParcialmenteDevuelto
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeAnticipo {
        [LibEnumDescription("Cobrado")]
        Cobrado = 0,
        [LibEnumDescription("Pagado")]
        Pagado
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eGeneradoPor {
        [LibEnumDescription("Usuario")]
        Usuario = 0,
        [LibEnumDescription("Cobranza")]
        Cobranza,
        [LibEnumDescription("Orden de Pago")]
        OrdendePago,
        [LibEnumDescription("CxP")]
        CxP,
        [LibEnumDescription("Débito  Bancario")]
        DebitoBancario,
        [LibEnumDescription("CxC")]
        CxC,
        [LibEnumDescription("Gestión  Cobranza")]
        GestionCobranza,
        [LibEnumDescription("Compra")]
        Compra,
        [LibEnumDescription("Anticipo")]
        Anticipo,
        [LibEnumDescription("Reconversión Monetaria")]
        ReconversionMonetaria,        
        [LibEnumDescription("Rendición")]
        Rendicion,
        [LibEnumDescription("Solicitud de Pago")]
        SolicitudDePago,
        [LibEnumDescription("Reposición De Caja Chica")]
        ReposicionDeCajaChica
    }

    public enum eStatusAnticipoInformes {
        [LibEnumDescription("Todos")]
        Todos = 0,
        [LibEnumDescription("Vigente")]
        Vigente,
        [LibEnumDescription("Anulado")]
        Anulado,
        [LibEnumDescription("Parcialmente  Usado")]
        ParcialmenteUsado,
        [LibEnumDescription("Completamente  Usado")]
        CompletamenteUsado,
        [LibEnumDescription("Completamente  Devuelto")]
        CompletamenteDevuelto,
        [LibEnumDescription("Parcialmente  Devuelto")]
        ParcialmenteDevuelto
    }

} //End of namespace namespace  Galac.Adm.Ccl.CAnticipo
