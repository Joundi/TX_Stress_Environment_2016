using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        public int wheelRange = 360;
        private CarController m_Car; // the car controller we want to use

        // Use this for initialization
        void Start()
        {
            Debug.Log(LogitechGSDK.LogiSteeringInitialize(true));

            // Set Steering wheel range
            LogitechGSDK.LogiControllerPropertiesData controllerProperties = new LogitechGSDK.LogiControllerPropertiesData();
            LogitechGSDK.LogiGetCurrentControllerProperties(0, ref controllerProperties);
            //controllerProperties.wheelRange = wheelRange;
            //LogitechGSDK.LogiSetPreferredControllerProperties(controllerProperties); //THIS FUNCTION CAUSES CRASH WHEN EDITOR STOPPED for Unity version 5+ss!!
            m_Car.m_SteeingWheel.wheelRange = controllerProperties.wheelRange;
        }

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            float h = 0f;
            float v = 0f;
            float handbrake = 0f;

            // pass Logitech/keyboard input to the car
            if (LogitechGSDK.LogiIsConnected(0))
            {
                // pass logitech input to the car
                if (LogitechGSDK.LogiUpdate())
                {
                    LogitechGSDK.DIJOYSTATE2ENGINES rec;
                    rec = LogitechGSDK.LogiGetStateUnity(0);

                    // steering wheel
                    h = (float)rec.lX/short.MaxValue;

                    if (rec.lY != short.MaxValue)
                    {
                        // Acceleration pressed
                        v = -(float)(rec.lY - short.MaxValue) / (short.MaxValue - short.MinValue);
                    }
                    else if (rec.lRz != short.MaxValue)
                    {
                        // Brake pressed
                        v = (float)(rec.lRz - short.MaxValue) / (short.MaxValue - short.MinValue);
                    }
                }
            }
            else
            {
                // pass keyboard input to the car
                h = CrossPlatformInputManager.GetAxis("Horizontal");   // Steering wheel
                v = CrossPlatformInputManager.GetAxis("Vertical");     // Acceleration forward/backward
#if !MOBILE_INPUT
                handbrake = CrossPlatformInputManager.GetAxis("Jump"); // Handbrake
#endif
            }

            // move car
            m_Car.Move(h, v, v, handbrake);
        }

        void OnDestroy()
        {
            //Free G-Keys SDKs before quitting the game
            LogitechGSDK.LogiGkeyShutdown();
        }
    }
}
