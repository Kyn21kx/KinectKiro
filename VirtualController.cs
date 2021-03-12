using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace KinectSekiro {
    class VirtualController {
        public enum KeyCodes {
            VK_W = 0x57,
            VK_A = 0x41,
            VK_S = 0x53,
            VK_D = 0x44,
            VM_PASTE = 0x0302
        }

        #region Variables
        public Process TargetProcess { get; private set; }
        private const uint WM_KEYDOWN = 0x0100;
        #endregion



        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        public VirtualController(Process targetProcess) {
            this.TargetProcess = targetProcess;
        }

        public bool SendKeyDown(int keyCode) {
            return PostMessage(TargetProcess.MainWindowHandle, WM_KEYDOWN, keyCode, 0);
        }

        public bool SendKeyDown(KeyCodes keyCode) {
            return PostMessage(TargetProcess.MainWindowHandle, WM_KEYDOWN, (int)keyCode, 0);
        }
    }
}
