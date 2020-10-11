using System;
using System.Threading;
using System.Windows.Forms;
using static KioskModeTestGui.KioskModeAPI;

namespace KioskModeTestGui {
    public partial class Form1: Form {

        private VKey[] AllowedKeys = new VKey[] {
                VKey.Number0,
                VKey.Number1,
                VKey.Number2,
                VKey.Number3,
                VKey.Number4,
                VKey.Number5,
                VKey.Number6,
                VKey.Number7,
                VKey.Number8,
                VKey.Number9,
                VKey.Multiply,
                VKey.Add,
                VKey.Separator,
                VKey.Subtract,
                VKey.Decimal,
                VKey.Divide,
                VKey.Up,
                VKey.Left,
                VKey.Right,
                VKey.Down,
                VKey.Delete,
                VKey.Back,
                VKey.NumberPad0,
                VKey.NumberPad1,
                VKey.NumberPad2,
                VKey.NumberPad3,
                VKey.NumberPad4,
                VKey.NumberPad5,
                VKey.NumberPad6,
                VKey.NumberPad7,
                VKey.NumberPad8,
                VKey.NumberPad9,
                VKey.NumberKeyLock,
            };

        private VKey[] EscapeKeys = new VKey[] {
                VKey.E,
                VKey.S,
                VKey.C,
                VKey.F,
                VKey.R,
                VKey.O,
                VKey.M,
                VKey.K,
                VKey.I,
                VKey.O,
                VKey.S,
                VKey.K,
                VKey.F1
            };

        
        public Form1()
        {
            InitializeComponent();
            BtnEndKiosk.Enabled = false;
            KioskModeAPI.AddOnKioskExitEventHandler(OnKioskExitEvent);//Send to the DLL the Address of the Method
        }

        private void OnKioskExitEvent()
        {//kiosk mode has been ended; event may be fired when escape key sequence has been entered.
            BtnStartKiosk.Enabled = true;
            BtnEndKiosk.Enabled = false;
            new Thread(() => {
                MessageBox.Show(null, "The Kiosk Mode has been shutdown!", "OnKioskExitEvent");
            }).Start();
        }

        private void BtnStartKiosk_Click(object sender, EventArgs e)
        {
            BtnEndKiosk.Enabled = true;
            BtnStartKiosk.Enabled = false;
            KioskModeAPI.StartKioskMode(AllowedKeys, EscapeKeys, Handle);
        }

        private void BtnEndKiosk_Click(object sender, EventArgs e)
        {
            BtnStartKiosk.Enabled = true;
            BtnEndKiosk.Enabled = false;
            KioskModeAPI.EndKioskMode(Handle);
        }
    }
}
