using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Kinect;

namespace KinectSekiro {
    public partial class Form1 : Form {

        private KinectSensor sensor;
        
        public Form1() {
            InitializeComponent();
            Start();
        }

        private void Start() {
            sensor = KinectSensor.GetDefault();
        }

        public void Stream() {
            if(sensor != null) {
                sensor.Open();

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            sensor.Close();
        }
    }
}
