using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Kinect;
using System.Diagnostics;

namespace KinectSekiro {
    public partial class Form1 : Form {

        PoseTranslator poseTranslator;

        public Form1() {
            InitializeComponent();
            poseTranslator = new PoseTranslator();
            poseTranslator.Begin();
            poseTranslator.additionalActions = ShowHandInfo;
        }

        private void ShowHandInfo() {
            Body body = poseTranslator.TrackedBody;
            if (body == null) return;
            Process process = Process.GetCurrentProcess();
            VirtualController controller = new VirtualController(process);
            debugLabel.Text = "Right Hand: " + body.HandRightState + "\n";
            debugLabel.Text += "Left Hand: " + body.HandLeftState + "\n";
            if (body.HandRightState == HandState.Closed) {
                //SendKeys.Send("a");
                bool result = controller.SendKeyDown(VirtualController.KeyCodes.VM_PASTE);
                debugLabel.Text += result ? "Typed\n" : "Error\n";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
        }
    }
}
