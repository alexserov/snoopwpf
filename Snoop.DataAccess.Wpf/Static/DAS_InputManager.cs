namespace Snoop.DataAccess.Wpf {
    using System;
    using System.Windows.Input;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class DAS_InputManager : DataAccessBase, IDAS_InputManager {
        Action preProcessInput = new Action(() => { });
        public event Action PreProcessInput {
            add { this.preProcessInput += value; }
            remove { this.preProcessInput -= value; }
        }

        public DAS_InputManager() {
            InputManager.Current.PreProcessInput+=CurrentOnPreProcessInput;
        }

        void CurrentOnPreProcessInput(object sender, PreProcessInputEventArgs e) {
            this.preProcessInput();
        }
    }
}