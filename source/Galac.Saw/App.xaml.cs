using System;
using System.Linq;
using System.Threading;
using System.Windows;
using LibGalac.Aos.Uil;
using System.Text;
using System.Reflection;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Catching;

namespace Galac.Saw {
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            try {
                App.Current.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
                Bootstrapper bootstrapper = new Bootstrapper();
                bootstrapper.Run();
            } catch (System.AccessViolationException) {
                throw;
            } catch (ReflectionTypeLoadException vEx) {
                string[] vErrors = vEx.LoaderExceptions.Select(ex => ex.GetBaseException().Message).ToArray();
                string vMessage = string.Join(Environment.NewLine + Environment.NewLine, vErrors);
                LibExceptionDisplay.Show(new GalacException(vMessage, eExceptionManagementType.Uncontrolled, vEx));
                App.Current.Shutdown();
            } catch (System.Exception vEx) {
                LibExceptionDisplay.Show(vEx);
                App.Current.Shutdown();
            }
        }
    }
}
