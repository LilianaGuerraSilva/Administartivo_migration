using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using LibGalac.Aos.UI.Wpf;

namespace Galac.Saw.Lib {
    public class TipoCambioSunat { 
        private string _UriBaseSunat = "http://www.sunat.gob.pe/" + "a/txt/tipoCambio.txt";
        DateTime _FechaEnCurso;

        public string UriBaseSunat {
            get;
            set;
        }

        public decimal Venta {
            get;
            set;
        }

        public decimal Compra {
            get;
            set;
        }

        public int DiaDelMesEnCurso {
            get;
            set;
        }

        public DateTime FechaEnCurso {
            get {
                if (DiaDelMesEnCurso < 1) {
                    return LibDate.Today();
                } else {
                    return _FechaEnCurso;
                }
            }
            set {
                if (_FechaEnCurso != value) {
                    _FechaEnCurso = value;
                } else {
                    _FechaEnCurso = LibDate.Today();
                }
            }
        }
        public TipoCambioSunat GetCambio() {
            ConexionWebSunat vConexion = new ConexionWebSunat();
            
            try {
                List<string> vInfoDelDia = vConexion.GetInfoDelDia(_UriBaseSunat);
                if (vInfoDelDia.Count > 0) {
                    if (vInfoDelDia[0] != string.Empty) {
                        Venta = LibImportData.ToDec(vInfoDelDia[2],3);

                    }
                    if (vInfoDelDia[1] != string.Empty) {
                        Compra = LibImportData.ToDec(vInfoDelDia[1],3);
                    }
                    if (vInfoDelDia[2] != string.Empty) {
                        DiaDelMesEnCurso = LibConvert.ToDate(vInfoDelDia[0]).Day;
                        FechaEnCurso = LibConvert.ToDate(vInfoDelDia[0]);
                    }
                }
            } catch (WebException gEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(gEx, "En Get Cambio");
                return null;
            } catch (Exception vEx) {
                LibExceptionDisplay.Show(vEx);
            }
            return this;
        }

        public TipoCambioSunat() {
            UriBaseSunat = _UriBaseSunat;
        }

    }

}