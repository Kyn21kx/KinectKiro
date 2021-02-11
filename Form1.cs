using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Kinect;

namespace KinectSekiro {
    public partial class Form1 : Form {

        private KinectSensor sensor;
        private IList<Body> bodies;
        private BodyFrameReader reader;
        public Form1() {
            InitializeComponent();
            Start();
        }

        private void Start() {
            sensor = KinectSensor.GetDefault();
            Stream();
        }

        public void Stream() {
            if (sensor != null) {
                sensor.Open();
                reader = sensor.BodyFrameSource.OpenReader();
                reader.FrameArrived += OnFrame;
            }
        }

        private void OnFrame(object sender, BodyFrameArrivedEventArgs e) {
            BodyFrame frame = e.FrameReference.AcquireFrame();
            if (frame == null) return;
            try {
                bodies = new Body[frame.BodyCount];
                frame.GetAndRefreshBodyData(bodies);
                debugLabel.Text = "Body information:\n";
                foreach (var body in bodies) {
                    debugLabel.Text += "Lean: (" + body.Lean.X + ", " + body.Lean.Y + ")";
                }
            }

            catch (Exception err) {
                debugLabel.Text = err.Message;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            sensor.Close();
        }
    }
}
