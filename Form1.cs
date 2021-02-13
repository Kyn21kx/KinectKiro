using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Kinect;

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
            debugLabel.Text = "Right Hand: " + body.HandRightState + "\n";
            debugLabel.Text += "Left Hand: " + body.HandLeftState + "\n";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
        }
    }
}
