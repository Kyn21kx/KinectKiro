using System;
using System.Collections.Generic;
using Microsoft.Kinect;

namespace KinectSekiro {
    class PoseTranslator {

        /// <summary>
        /// A cached reference to the default Kinect sensor
        /// </summary>
        public KinectSensor Sensor { get; private set; }
        /// <summary>
        /// Will return null if no body is detected and will be updated every frame once the reader is open
        /// </summary>
        public Body TrackedBody { get; private set; }

        private IList<Body> bodies;
        private FrameSourceTypes sourceTypes;
        
        private MultiSourceFrameReader mReader;

        /// <summary>
        /// Initiates the translator with a sensor
        /// </summary>
        public PoseTranslator () {
            //This is just a frankly unnecesary wrapper
            this.Sensor = KinectSensor.GetDefault();
            sourceTypes = FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body;
        }

        ~PoseTranslator() {
            this.Dispose();
        } 

        private void UpdateControls() {

        }

        /// <summary>
        /// Opens the kinect's reader and sets up an event for a received frame
        /// </summary>
        public void OpenReader() {
            Sensor.Open();
            mReader = Sensor.OpenMultiSourceFrameReader(sourceTypes);
            mReader.MultiSourceFrameArrived += OnSensorFrame;
        }

        /// <summary>
        /// Closes and disposes all resources that the instance is currently using
        /// </summary>
        public void Dispose() {
            mReader.Dispose();
            Sensor.Close();
            TrackedBody = null;
        }

        /// <summary>
        /// Sets and iterates through the list of bodies until it finds a valid body
        /// </summary>
        /// <param name="frame">Currently active frame on the sensor event</param>
        /// <returns>A valid tracked body / null</returns>
        private Body GetTrackedBody (MultiSourceFrame frame) {
            using (var bFrame = frame.BodyFrameReference.AcquireFrame()) {
                if (bFrame != null) {

                    bodies = new Body[bFrame.BodyFrameSource.BodyCount];
                    bFrame.GetAndRefreshBodyData(bodies);

                    foreach (var body in bodies) {
                        if (body != null && body.IsTracked) 
                            return body; //A valid body was found
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Event to update body information
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Frame argument</param>
        private void OnSensorFrame(object sender, MultiSourceFrameArrivedEventArgs e) {
            MultiSourceFrame frame = e.FrameReference.AcquireFrame();
            //Update the currently tracked body
            TrackedBody = GetTrackedBody(frame);
            UpdateControls();
        }
    }
}
