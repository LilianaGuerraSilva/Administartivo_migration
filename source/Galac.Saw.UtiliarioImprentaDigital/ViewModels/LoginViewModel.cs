using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Input;
using Galac.Saw.UtiliarioImprentaDigital.Connector;
using Galac.Saw.LibWebConnector;
using System.Threading.Tasks;

namespace Galac.Saw.UtiliarioImprentaDigital.ViewModels {
    public class LoginViewModel: ViewModelBase, ILoginUser {
        //Fields                                   
        private SecureString _PasswordSec;
        ILoginUser _LoginUser;
        //Properties

        public string URL {
            get {
                return _LoginUser.URL;
            }
            set {
                if (_LoginUser.URL != value) {
                    _LoginUser.URL = value;
                    RaisePropertyChanged(nameof(URL));
                }
            }
        }

        public string User {
            get {
                return _LoginUser.User;
            }
            set {
                if (_LoginUser.User != value) {
                    _LoginUser.User = value;
                    RaisePropertyChanged(nameof(User));
                }
            }
        }

        public SecureString PasswordSec {
            get {
                return _PasswordSec;
            }

            set {
                if (_PasswordSec != value) {
                    _PasswordSec = value;
                    Password = LibUtil.SecureStringToString(_PasswordSec);
                    RaisePropertyChanged(nameof(PasswordSec));
                    RaisePropertyChanged(nameof(Password));
                }
            }
        }         

        public string Password {
            get {
                return _LoginUser.Password; 
            }
            set {
                if (_LoginUser.Password != value) {
                    _LoginUser.Password = value;
                    RaisePropertyChanged(nameof(Password));
                }
            }
        }

        public string PasswordKey {
            get {
                return _LoginUser.PasswordKey;
            }
            set {
                if (_LoginUser.PasswordKey != value) {
                    _LoginUser.PasswordKey = value;
                    RaisePropertyChanged(nameof(PasswordKey));
                }
            }
        }

        public string UserKey {
            get {
                return _LoginUser.UserKey;
            }
            set {
                if (_LoginUser.UserKey != value) {
                    _LoginUser.UserKey = value;
                    RaisePropertyChanged(nameof(UserKey));
                }
            }
        }

        public string MessageResult {
            get {
                return _LoginUser.MessageResult;
            }
            set {
                if (_LoginUser.MessageResult != value) {
                    _LoginUser.MessageResult = value;
                }
                RaisePropertyChanged(nameof(MessageResult));
            }
        }

        // Commands
        public ICommand LoginCommand { get; }
        public ICommand ShowPasswordCommand { get; }

        //Constructor
        public LoginViewModel() {
            _LoginUser = new clsLoginUser();
            UserKey = "usuario";
            PasswordKey = "clave";
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }

        private bool CanExecuteLoginCommand(object obj) {
            return true;
        }

        bool ValidatingField(ref string refMensaje) {
            bool vResult = false;
            refMensaje = string.Empty;
            vResult = LibUtil.URLValidating(URL, ref refMensaje);
            refMensaje += (string.IsNullOrEmpty(URL) ? "falta colocar la URL,\r\n" : "");
            refMensaje += (string.IsNullOrEmpty(User) ? "falta colocar el Usuario,\r\n" : "");
            refMensaje += (string.IsNullOrEmpty(Password) ? "falta colocar la Contraseña,\r\n" : "");
            refMensaje += (string.IsNullOrEmpty(UserKey) ? "falta colocar la clave Usuario,\r\n" : "");
            refMensaje += (string.IsNullOrEmpty(PasswordKey) ? "falta colocar la clave Contraseña" : "");
            vResult = string.IsNullOrEmpty(refMensaje);
            return vResult;
        }

        private void ExecuteLoginCommand(object obj) {
            try {
                var taskTestConnection = Task.Factory.StartNew(() => DoGetLogin());
                Task.WaitAll(taskTestConnection);
                MessageResult = taskTestConnection.Result;
            } catch (Exception vEx) {
                MessageBox.Show(vEx.Message);
            }
        }

        private string DoGetLogin() {          
            try {
                string vMensaje = string.Empty;
                clsConnector _Connector = new clsConnector(_LoginUser);
                string vMessageResult = string.Empty;
                if (ValidatingField(ref vMensaje)) {
                    vMessageResult = _Connector.TestConnection();
                } else {
                    vMessageResult = vMensaje;
                }
                return vMessageResult;
            } catch (Exception vEx) {
                throw vEx;
            }
        }
    }
}
