namespace Snoop.DataAccess.WinUI3 {
    using System;
    using Windows.Devices.Input;
    using Windows.UI.Input.Preview.Injection;
    using Microsoft.UI.Xaml.Input;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class DAS_InputManager : DataAccessBase, IDAS_InputManager {
        Action preProcessInput = new Action(() => { });
        InputInjector injector;
        public event Action PreProcessInput {
            add { this.preProcessInput += value; }
            remove { this.preProcessInput -= value; }
        }

        public DAS_InputManager() {
            MouseDevice.GetForCurrentView().MouseMoved+=OnMouseMoved;
            this.injector = InputInjector.TryCreate();
        }

        void OnMouseMoved(MouseDevice sender, MouseEventArgs args) {
            this.preProcessInput();
        }
    }
}