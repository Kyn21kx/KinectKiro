using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace KinectSekiro {
    class VirtualController {

        [StructLayout(LayoutKind.Sequential)]
        public struct KeyboardInput {
            public ushort vk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MouseInput {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HardwareInput {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion {
            [FieldOffset(0)] public MouseInput mi;
            [FieldOffset(0)] public KeyboardInput ki;
            [FieldOffset(0)] public HardwareInput hi;
        }

        public struct Input {
            public int type;
            public InputUnion union;
        }

        [Flags]
        public enum InputType {
            Mouse = 0,
            Keyboard = 1,
            Hardware = 2
        }

        public enum KeyCodes {
            DIK_W = 0x11,
            DIK_A = 0x1E,
            DIK_S = 0x1F,
            DIK_D = 0x20,
            DIK_SPACE = 0x39
        }

        #region Variables
        public Process TargetProcess { get; private set; }
        private const uint KEYDOWN = 0x0000;
        private const uint KEYUP = 0x0002;
        const uint UNICODE = 0x0004;
        const uint SCANCODE = 0x0008;
        #endregion



        [DllImport("user32.dll")]
        public static extern uint SendInput(uint cInputs, Input[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        public static extern IntPtr GetMessageExtraInfo();


        public void SendKeyDown(ushort keyCode) {

        }

        public void SendKeyDown(KeyCodes keyCode) {
            Input[] sequence = GetSingleKeyDownSequence(keyCode);
            SendInput(2u, sequence, Marshal.SizeOf(typeof(Input)));
        }

        public bool SendActions(Input[] sequence) {
            return false;
        }

        private Input[] GetSingleKeyDownSequence(KeyCodes keyCode) {
            Input downInput = new Input();
            downInput.type = (int)InputType.Keyboard;
            downInput.union = new InputUnion {
                ki = new KeyboardInput {
                    vk = 0,
                    wScan = (ushort)keyCode,
                    dwFlags = SCANCODE,
                    time = 0,
                    dwExtraInfo = IntPtr.Zero
                }
            };

            Input upInput = new Input();
            upInput.type = (int)InputType.Keyboard;
            upInput.union = new InputUnion {
                ki = new KeyboardInput {
                    vk = 0,
                    wScan = (ushort)keyCode,
                    dwFlags = KEYUP | SCANCODE,
                    time = 0,
                    dwExtraInfo = IntPtr.Zero
                }
            };

            return new Input[] { downInput, upInput };
        }

    }
}
