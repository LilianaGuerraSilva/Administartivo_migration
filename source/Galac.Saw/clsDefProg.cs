using System;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.Cnf;

public class clsDefProg {
    public const string VersionDelPrograma = "26.2";
    public const string VersionBaseDeDatos = "6.77";
    public const string HoraDeLaVersion = "12:00 A.M.";
    private static DateTime mFechaDelaVersion = new DateTime(2021, 07, 21);
    private static string _Pais = "VE";
    private static int _CMTO = -1;

    private clsDefProg() {        
    }

    public static string SiglasDelPrograma {
        get { return LibGalac.Aos.DefGen.LibProduct.GetInitialsSaw(); }
    }

    public static DateTime FechaDelaVersion {
        get { return mFechaDelaVersion; }
    }

    #region Programador Agregue aquí Código Adicional
    public static string Pais() {
        string vValue = "";
        if (_Pais.Length == 0) {
            vValue = LibAppSettings.ReadAppSettingsKey("PAIS");
            if (LibGalac.Aos.Base.LibString.IsNullOrEmpty(vValue, true)) {
                _Pais = "VE";
            } else {
                _Pais = vValue.ToUpper();
            }
        }
        return _Pais;
    }

    public static int CMTO() {
        string vValue = "";
        if (_CMTO < 0) {
            vValue = LibAppSettings.ReadAppSettingsKey("CMTO");
            _CMTO = 0;
            if ((vValue != null) && (vValue.Length > 0)) {
                _CMTO = LibGalac.Aos.Base.LibConvert.ToInt(vValue);
            }
        }
        return _CMTO;
    }
    #endregion

}// End of class clsDefProg

