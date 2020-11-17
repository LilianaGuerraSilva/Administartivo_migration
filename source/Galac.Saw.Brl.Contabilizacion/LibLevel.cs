using System;
using LibGalac.Aos.Base;

namespace LibGalac {
    namespace CommonModules {
        public class LibLevel {
            private int[] mArrayOfLevels;
            private string[] mArrayOfNames;
            private bool mUseZeroAtRigth;
            private int mMaxNumLevels;
            private int mMaxNumLevelsAtMatrix;
            private int mMinNumLevels;
            private int mMaxLength;

            public LibLevel(int initMaxNumLevels, int initMaxNumLevelsAtMatrix, int initMinNumLevels, int initMaxLength, bool initUseZeroAtRigth) {
                mMaxNumLevels = initMaxNumLevels;
                mMaxNumLevelsAtMatrix = initMaxNumLevelsAtMatrix;
                mMinNumLevels = initMinNumLevels;
                mMaxLength = initMaxLength;
                UseZeroAtRigth = initUseZeroAtRigth;
                ArrayOfLevels = InitArrayOfLevels();
                ArrayOfNames = InitArrayOfNames();
            }

            public int MaxNumLevels {
                get { return mMaxNumLevels; }
            }

            private int MaxNumLevelsAtMatrix {
                get { return mMaxNumLevelsAtMatrix; }
            }

            private int MinNumLevels {
                get { return mMinNumLevels; }
            }

            public int MaxLength {
                get { return mMaxLength; }
            }

            private static string Guion {
                get { return "-"; }
            }

            private static string Punto {
                get { return "."; }
            }

            private static string Asterisco {
                get { return "*"; }
            }

            public int[] ArrayOfLevels {
                get { return mArrayOfLevels; }
                set { mArrayOfLevels = value; }
            }

            public string[] ArrayOfNames {
                get { return mArrayOfNames; }
                set { mArrayOfNames = value; }
            }

            public bool UseZeroAtRigth {
                get { return mUseZeroAtRigth; }
                set { mUseZeroAtRigth = value; }
            }

            public int[] InitArrayOfLevels() {
                int[] vResult = new int[MaxNumLevels];
                for (int index = 0; index < MaxNumLevels; index++) {
                    vResult[index] = 0;
                }
                return vResult;
            }

            public string[] InitArrayOfNames() {
                string[] vResult = new string[MaxNumLevels];
                for (int index = 0; index < MaxNumLevels; index++) {
                    vResult[index] = "";
                }
                return vResult;
            }

            public int[] FillArrayOfLevels(string valSetOfLevels) {
                string vConjuntoRestante, vNivelStr;
                int vIndex, vNivelInt;
                int[] vResult;
                vResult = InitArrayOfLevels();
                vConjuntoRestante = valSetOfLevels;
                for (vIndex = 0; vIndex < MaxNumLevels; vIndex++) {
                    vNivelStr = LibText.ExtractUntilSeparatorAndCut(ref vConjuntoRestante, LibText.StandardSeparator());
                    vNivelInt = LibConvert.ToInt(vNivelStr);
                    vResult[vIndex] = vNivelInt;
                }
                return vResult;
            }

            public string[] FillArrayOfNames(string valSetOfNames) {
                string vConjuntoRestante, vNombreStr;
                int vIndex;
                string[] vResult;
                vResult = InitArrayOfNames();
                vConjuntoRestante = valSetOfNames;
                for (vIndex = 0; vIndex < MaxNumLevels; vIndex++) {
                    vNombreStr = LibText.ExtractUntilSeparatorAndCut(ref vConjuntoRestante, LibText.StandardSeparator());
                    vResult[vIndex] = vNombreStr;
                }
                return vResult;
            }

            public void FillArraysOfLevelsAndNames(string valSetOfLevels, string valSetOfNames) {
                ArrayOfLevels = FillArrayOfLevels(valSetOfLevels);
                ArrayOfNames = FillArrayOfNames(valSetOfNames);
            }

            public int CodeLength() {
                int vResult;
                vResult = 0;
                for (int vIndex = 0; vIndex < ArrayOfLevels.Length; vIndex++) {
                    vResult += ArrayOfLevels[vIndex];
                }
                return vResult;
            }

            public string CodeMask() {
                int vIndex;
                string vResultado;
                vResultado = "";
                for (vIndex = 0; vIndex < MaxNumLevels; vIndex++)
                    if (ArrayOfLevels[vIndex] != 0) {
                        if (vIndex > 0) {
                            vResultado += Punto;
                        }
                        vResultado += LibText.NCar('X', ArrayOfLevels[vIndex]);
                    }
                return vResultado;
            }

            public bool CodeLengthIsGreaterThanMaxLength(bool valShowMessage) {
                bool vResultado;
                int vLongitudActual;
                vLongitudActual = CodeLength();
                vResultado = (vLongitudActual > MaxNumLevels);
                return vResultado;
            }

            private int[] QuitarNivelesVacios() {
                int i, j;
                int[] vResult;
                vResult = ArrayOfLevels;
                for (i = 0; i < MaxNumLevels; i++)
                    if (vResult[i] == 0) {
                        for (j = i; j < MaxNumLevels - 1; j++) {
                            vResult[j] = vResult[j + 1];
                        }
                    }
                return vResult;
            }

            public bool LevelHasInformationOnIndex(int valLevelIndex) {
                return (ArrayOfLevels[valLevelIndex] != 0);
            }

            public int LastLevelIndexWithInformation() {
                int vResultado;
                vResultado = 0;
                for (int i = 0; (i < MaxNumLevels && ArrayOfLevels[i] != 0); i++) {
                    vResultado = i;
                }
                return vResultado;
            }

            public int LevelLengthValueAtPosition(int valIndexPosition) {
                return ArrayOfLevels[valIndexPosition];
            }

            public string LevelNameValueAtPosition(int valIndexPosition) {
                return ArrayOfNames[valIndexPosition];
            }

            protected string ReemplazaGuionesConPuntos(string valCode) {
                return LibText.Replace(valCode, Guion, Punto);
            }

            private string QuitaInconsistenciasDeSeparadores(string valCode) {
                string vResultado;
                int vPosicion;
                vResultado = valCode;
                do {
                    vPosicion = LibText.InStr(vResultado, Punto + Punto);
                    if (vPosicion >= 0) {
                        vResultado = LibText.Replace(vResultado, Punto + Punto, Punto);
                    }
                } while (vPosicion >= 0);
                if (LibText.Left(vResultado, 1) == Punto) {
                    vResultado = LibText.Mid(vResultado, 1);
                }
                if (LibText.Right(vResultado, 1) == Punto) {
                    vResultado = LibText.Mid(vResultado, 0, LibText.Len(vResultado) - 1);
                }
                return vResultado;
            }

            private string CuadraElCodigoConLosNiveles(string valCode, bool valUseMaxNumLevelsAtMatrixAsMaxNumLevels) {
                string vResultado, vCodigoDeTrabajo, vSubCodigo;
                int vPosicion, vCount, vCantMaxNiveles;
                bool vTieneAsterisco;
                vResultado = "";
                vSubCodigo = "";
                vCount = 0;
                vPosicion = 0;
                vCodigoDeTrabajo = valCode;
                vTieneAsterisco = false;
                if (valUseMaxNumLevelsAtMatrixAsMaxNumLevels) {
                    vCantMaxNiveles = MaxNumLevelsAtMatrix;
                } else {
                    vCantMaxNiveles = MaxNumLevels;
                }
                for (vCount = 0; vCount < vCantMaxNiveles; vCount++)
                    if (ArrayOfLevels[vCount] != 0) {
                        if (LibText.Len(vCodigoDeTrabajo) != 0) {
                            vPosicion = LibText.InStr(vCodigoDeTrabajo, Punto) + 1;
                            if (vPosicion <= 0) {
                                vSubCodigo = vCodigoDeTrabajo;
                                vPosicion = LibText.Len(vCodigoDeTrabajo);
                            } else {
                                vSubCodigo = LibText.Mid(vCodigoDeTrabajo, 0, vPosicion - 1);
                            }
                            if (LibText.Len(vSubCodigo) > ArrayOfLevels[vCount]) {
                                vSubCodigo = LibText.Mid(vSubCodigo, 0, ArrayOfLevels[vCount]);
                                vCodigoDeTrabajo = LibText.Mid(vCodigoDeTrabajo, ArrayOfLevels[vCount]);
                            } else {
                                vCodigoDeTrabajo = LibText.Mid(vCodigoDeTrabajo, vPosicion);
                            }
                            vTieneAsterisco = (LibText.InStr(vSubCodigo, Asterisco) >= 0);
                            if (vCount > 0) {
                                vResultado = vResultado + Punto;
                            }
                            if (!vTieneAsterisco) {
                                vResultado = vResultado + LibText.Right(LibText.NCar('0', ArrayOfLevels[vCount]) + vSubCodigo, ArrayOfLevels[vCount]);
                            } else {
                                vResultado = vResultado + vSubCodigo;
                            }
                        }
                    }
                return vResultado;
            }

            private string AdjustCodeWithArrayOfLevels(string valCode, bool valUseMaxNumLevelsAtMatrixAsMaxNumLevels) {
                string vResultado;
                vResultado = "";
                if (valCode != "") {
                    vResultado = ReemplazaGuionesConPuntos(valCode);
                    vResultado = QuitaInconsistenciasDeSeparadores(vResultado);
                    vResultado = CuadraElCodigoConLosNiveles(vResultado, valUseMaxNumLevelsAtMatrixAsMaxNumLevels);
                    if (UseZeroAtRigth) {
                        vResultado = FillEmptyLevelsWithZero(vResultado);
                    }
                }
                return vResultado;
            }

            private bool IsValidCharForCode(char valChr, bool valAceptaAsterisco, bool valAceptaCC) {
                return (((valChr >= '0') && (valChr <= '9')) || ((valChr == '*' && valAceptaAsterisco)) || (valChr == LibConvert.ToChar(Punto)) || ((valChr == 'C') && valAceptaCC));
            }

            private bool HasOnlyZeros(string valTexto) {
                return (LibText.Len(LibText.EraseCharacter(valTexto, '0')) == 0);
            }

            private int LengthUntilLevel(int valLevel, bool valSumDelimiter) {
                int vLength, k, vIndex;
                vLength = 0;
                vIndex = valLevel - 1;
                for (k = 0; k <= vIndex; k++) {
                    vLength += ArrayOfLevels[k];
                    if (valSumDelimiter && (k != vIndex)) {
                        vLength++;
                    }
                }
                return vLength;
            }

            private string ExtractTextOfLevel(int valLevel, string valCode) {
                string vResultado;
                vResultado = "";
                if (valLevel <= 1) {
                    vResultado = LibText.SubString(valCode, 0, LibConvert.ToInt(ArrayOfLevels[0]));
                } else {
                    vResultado = LibText.SubString(valCode, LengthUntilLevel(valLevel - 1, true) + 1, ArrayOfLevels[valLevel - 1]);
                }
                return vResultado;
            }

            private int NumeroDeNivelesActual() {
                int vNumero;
                for (vNumero = 0; ((vNumero < MaxNumLevels) && (ArrayOfLevels[vNumero] > 0)); vNumero++) {
                }
                return vNumero;
            }

            public string QuitaCerosALaDerecha(string valCode, bool valCheckIfUseZeroAtRigth) {
                bool vContinuar;
                int vNivel;
                string vResultado;
                vResultado = valCode;
                if (valCheckIfUseZeroAtRigth && !UseZeroAtRigth) {
                    return vResultado;
                }
                vContinuar = true;
                vNivel = NumeroDeNivelesActual();
                while (vContinuar && vNivel > 0) {
                    if (HasOnlyZeros(ExtractTextOfLevel(vNivel, vResultado))) {
                        if (vNivel == 1) {
                            vContinuar = false;
                        } else {
                            vResultado = LibText.SubString(vResultado, 0, LengthUntilLevel(vNivel - 1, true));
                        }
                        vNivel--;
                    } else {
                        vContinuar = false;
                    }
                }
                return vResultado;
            }

            public string QuitaCerosALaDerechaSiAplica(string valCode) {
                return QuitaCerosALaDerecha(valCode, true);
            }

            public int CodeLevel(string valCode) {
                int vNivelActual, vLongActual;
                bool vSalir;
                vNivelActual = 0;
                if (LibText.Trim(valCode) == "") {
                    return vNivelActual;
                }
                valCode = QuitaCerosALaDerechaSiAplica(valCode);
                vNivelActual = 0;
                vSalir = false;
                vLongActual = 0;
                do {
                    if ((vLongActual + ArrayOfLevels[vNivelActual]) >= LibText.Len(valCode)) {
                        vNivelActual++;
                        vSalir = true;
                    } else {
                        vLongActual += ArrayOfLevels[vNivelActual] + 1;
                        vNivelActual++;
                        if (vNivelActual >= MaxNumLevels) {
                            vSalir = true;
                        }
                    }
                } while (!vSalir);
                return vNivelActual;
            }

            public string FillEmptyLevelsWithZero(string valTexto) {
                string vResultado;
                int vNivelActual, k;
                vResultado = valTexto;
                if (LibText.IndexOf(vResultado, "*") < 0) {
                    vNivelActual = CodeLevel(vResultado);
                    if (vNivelActual < NumeroDeNivelesActual()) {
                        vResultado = QuitaCerosALaDerechaSiAplica(vResultado);
                        for (k = vNivelActual; k < NumeroDeNivelesActual(); k++) {
                            vResultado += Punto + LibText.NCar('0', ArrayOfLevels[k]);
                        }
                    }
                }
                return vResultado;
            }

            public string CodeOfPreviousLevel(string valCode) {
                int vNivelAnt;
                string vResult;
                vResult = valCode;
                vNivelAnt = CodeLevel(valCode) - 1;
                if (vNivelAnt < 1) {
                    return "";
                }
                vResult = LibText.SubString(vResult, 0, LengthUntilLevel(vNivelAnt, true));
                if (UseZeroAtRigth) {
                    vResult = FillEmptyLevelsWithZero(vResult);
                }
                return vResult;
            }

            public bool IsCodeOfFirstLevel(string valCode) {
                return (CodeLevel(valCode) == 1);
            }

            private int LevelCount() {
                int vCount, vCant;
                vCant = 0;
                for (vCount = 0; vCount < MaxNumLevels; vCount++) {
                    if (ArrayOfLevels[vCount] != 0) {
                        vCant++;
                    }
                }
                return vCant;
            }

            public string CleanCodeOfInvalidChars(string valCode, bool valAllowAsterisk, bool valShowMessage) {
                string vCodigoLimpio;
                int vLongCodigo, vCount;
                char vChar;
                vCodigoLimpio = "";
                if (LibText.Len(valCode) > 0) {
                    vLongCodigo = LibText.Len(valCode);
                    for (vCount = 0; vCount < vLongCodigo; vCount++) {
                        vChar = LibConvert.ToChar(LibText.Mid(valCode, vCount, 1));
                        if (IsValidCharForCode(vChar, valAllowAsterisk, false)) {
                            vCodigoLimpio += Convert.ToString(vChar);
                        }
                    }
                   
                }
                return vCodigoLimpio;
            }

            public string QuitaCerosALaIzquierdaEnLosNiveles(string valCode) {
                string vTemporal, vResultado;
                int vPosicion;
                bool vDoProcess =  (LibString.Len(valCode, true) > 0);
                vResultado = "";
                if (vDoProcess && LibText.InStr(valCode, ".") < 0) {
                    if (valCode.Length > LengthUntilLevel(1, false)) {
                        vDoProcess = false;
                    }
                }
                if (vDoProcess) {
                    vTemporal = valCode;
                    vTemporal = LibText.Replace(vTemporal, "0", " ");
                    do {
                        vPosicion = LibText.InStr(vTemporal, ".") + 1;
                        vResultado += ValueWithoutLeftSpaces(vPosicion, vTemporal);
                        if (vPosicion == 0) {
                            vTemporal = "";
                        } else {
                            vTemporal = LibText.Mid(vTemporal, vPosicion);
                        }
                    } while (vTemporal != "");
                } else {
                    vResultado = valCode;
                }
                vResultado = LibText.Replace(vResultado, " ", "0");
                return vResultado;
            }

            private int LevelIndex(string valLevelName) {
                int vCount, vResultado;
                vResultado = 0;
                for (vCount = 0; vCount < MaxNumLevels; vCount++) {
                    if (LibString.S1IsEqualToS2(ArrayOfNames[vCount], valLevelName)) {
                        vResultado = vCount;
                        break;
                    }
                }
                return vResultado;
            }

            public bool IsCodeOfLastLevel(string valCode) {
                return (CodeLevel(valCode) == LevelCount());
            }

            private string CorrigeYAjustaLaCuenta(string valCode, bool valUseMaxNumLevelsAtMatrixAsMaxNumLevels, bool valAceptaAsteriscos) {
                string vResultado;
                vResultado = QuitaCerosALaIzquierdaEnLosNiveles(valCode);
                vResultado = CleanCodeOfInvalidChars(vResultado, valAceptaAsteriscos, true);
                vResultado = AdjustCodeWithArrayOfLevels(vResultado, valUseMaxNumLevelsAtMatrixAsMaxNumLevels);
                if (vResultado != "" && UseZeroAtRigth) {
                    vResultado = FillEmptyLevelsWithZero(vResultado);
                }
                return vResultado;
            }

            public string AdjustCodeInMatrix(string valCode) {
                return CorrigeYAjustaLaCuenta(valCode, true, false);
            }

            public string AdjustCode(string valCode, bool valAllowAsterisk) {
                return CorrigeYAjustaLaCuenta(valCode, false, valAllowAsterisk);
            }

            public string CodeUntilLevel(string valCode, int valNivel) {
                string vResult;
                vResult = "";
                if (valNivel < 1) {
                    return "";
                }
                vResult = LibText.Left(valCode, LengthUntilLevel(valNivel, true));
                vResult = CorrigeYAjustaLaCuenta(vResult, false, false);
                if (UseZeroAtRigth) {
                    vResult = FillEmptyLevelsWithZero(vResult);
                }
                return vResult;
            }
            public bool IsSetMinimunNumberOfLevels(ref string refErrorMessage) {
                bool vResult;
                string vBasicMessage, vMessage;
                int i;
                vResult = true;
                vBasicMessage = " no puede ser cero." + LibText.CRLF();
                vMessage = "";
                if (MinNumLevels == 1) {
                    vBasicMessage += "Debe existir al menos " + LibConvert.ToStr(MinNumLevels) + " nivel.";
                } else {
                    vBasicMessage += "Deben existir al menos " + LibConvert.ToStr(MinNumLevels) + " niveles.";
                }
                for (i = 0; i < MinNumLevels; i++) {
                    if (ArrayOfLevels[i] == 0) {
                        vResult = false;
                        vMessage = "El nivel " + LibConvert.ToStr(i + 1) + vBasicMessage;
                        break;
                    }
                }
                refErrorMessage += LibText.CRLF() + vMessage;
                return vResult;
            }

            public bool NamesAreComplete() {
                bool vResult;
                int vLastLevel;
                vResult = true;
                vLastLevel = LastLevelIndexWithInformation();
                for (int i = 0; (i <= vLastLevel && vResult); i++) {
                    if (LibText.Len(ArrayOfNames[i], true) == 0) {
                        vResult = false;
                    }
                }
                return vResult;
            }

            public bool NamesAreDifferent() {
                bool vResult;
                int vLastIndex, vLastLevelIndexWithInformation;
                vResult = true;
                vLastLevelIndexWithInformation = LastLevelIndexWithInformation();
                for (int vActual = 0; (vActual <= vLastLevelIndexWithInformation && vResult); vActual++) {
                    vLastIndex = LibArray.LastIndexOf(ArrayOfNames, ArrayOfNames[vActual], vActual + 1);
                    if (vLastIndex >= 0 && vLastIndex != vActual) {
                        vResult = false;
                    }
                    /*for (int i = vActual+1; (i <= valLastLevelIndexWithInformation && vResult); i++){
                        if (LibString.S1IsEqualToS2(valNames[vActual], valNames[i])){
                            vResult = false;
                        }
                    }*/
                }
                return vResult;
            }

            private string CorrigeYAjustaLaCuenta(string valCode, bool valUseMaxNumLevelsAtMatrixAsMaxNumLevels, bool valAceptaAsteriscos, bool valAceptaCC) {
                string vResultado;
                vResultado = QuitaCerosALaIzquierdaEnLosNiveles(valCode);
                vResultado = CleanCodeOfInvalidChars(vResultado, valAceptaAsteriscos, true, valAceptaCC);
                vResultado = AdjustCodeWithArrayOfLevels(vResultado, valUseMaxNumLevelsAtMatrixAsMaxNumLevels);
                if (vResultado != "" && UseZeroAtRigth) {
                    vResultado = FillEmptyLevelsWithZero(vResultado);
                }
                return vResultado;
            }

            public string CleanCodeOfInvalidChars(string valCode, bool valAllowAsterisk, bool valShowMessage, bool valPermiteCC) {
                string vMsg;
                string vCodigoLimpio;
                int vLongCodigo, vCount;
                char vChar;
                vCodigoLimpio = "";
                if (LibText.Len(valCode) > 0) {
                    vLongCodigo = LibText.Len(valCode);
                    for (vCount = 0; vCount < vLongCodigo; vCount++) {
                        vChar = LibConvert.ToChar(LibText.Mid(valCode, vCount, 1));
                        if (IsValidCharForCode(vChar, valAllowAsterisk, valPermiteCC)) {
                            vCodigoLimpio += Convert.ToString(vChar);
                        }
                    }
                   
                }
                return vCodigoLimpio;
            }
            public string AdjustCode(string valCode, bool valAllowAsterisk, bool valAceptaCC) {
             
                   return CorrigeYAjustaLaCuenta(valCode, false, valAllowAsterisk, valAceptaCC);
            }

            private string ValueWithoutLeftSpaces(int valPosition, string valValueTemp) {
                string vResult = "";
                if (valPosition == 0) {
                    if (LibString.Len(valValueTemp, true) == 0) {
                        vResult = "0";
                    } else {
                        vResult = LibText.CleanSpacesToLeft(valValueTemp);
                    }
                } else {
                    string vValue = LibText.Mid(valValueTemp, 0, valPosition - 1);
                    if (LibString.Len(vValue, true) == 0) {//Casos como: 3.1.8.00.002
                        vResult = "0.";
                    } else {
                        vResult = LibText.CleanSpacesToLeft(LibText.Mid(valValueTemp, 0, valPosition));
                    }
                }
                return vResult;
            }
        }//End of class LibLevel
    }//End of namespace CommonModules
}//End of namespace LibGalac
