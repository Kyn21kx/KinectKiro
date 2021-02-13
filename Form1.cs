using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Kinect;

namespace KinectSekiro {
    public partial class Form1 : Form {

        private KinectSensor sensor;
        private IList<Body> bodies;
        private MultiSourceFrameReader reader;
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
                reader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | 
                    FrameSourceTypes.Infrared | FrameSourceTypes.Body);
                reader.MultiSourceFrameArrived += OnFrame;
            }
        }

        private void OnFrame(object sender, MultiSourceFrameArrivedEventArgs e) {
            MultiSourceFrame frame = e.FrameReference.AcquireFrame();

            using (var bFrame = frame.BodyFrameReference.AcquireFrame()) {
                if (bFrame != null) {

                    bodies = new Body[bFrame.BodyFrameSource.BodyCount];

                    bFrame.GetAndRefreshBodyData(bodies);

                    foreach (var body in bodies) {
                        if (body != null) {
                            if (body.IsTracked) {
                                debugLabel.Text = body.HandRightState.ToString();
                            }
                        }
                    }
                }
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            reader.Dispose();
            sensor.Close();
        }
    }
}
