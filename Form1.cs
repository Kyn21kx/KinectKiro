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
            VirtualController controller = new VirtualController();
            debugLabel.Text = "Right Hand: " + body.HandRightState + "\n";
            debugLabel.Text += "Left Hand: " + body.HandLeftState + "\n";
            if (body.HandRightState == HandState.Closed) {
                controller.SendKeyDown(VirtualController.KeyCodes.DIK_W);
            }
            else if (body.HandRightState == HandState.Lasso) {
                controller.SendKeyDown(VirtualController.KeyCodes.DIK_S);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
        }
    }
}
