using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

        // Use this for initialization
        void Start()
        {
            //Debug.Log(LogitechGSDK.LogiSteeringInitialize(true));
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

            // pass the input to the car!
            if (LogitechGSDK.LogiIsConnected(0))
            {
                // pass logitech input to the car
                if (LogitechGSDK.LogiUpdate())
                {
                    LogitechGSDK.DIJOYSTATE2ENGINES rec;
                    rec = LogitechGSDK.LogiGetStateUnity(0);

                    h = (float)rec.lX/short.MaxValue;

                    if (rec.lY != short.MaxValue)
                    {
                        // Acceleration pressed
                        v = -(float)(rec.lY - short.MaxValue) / (short.MaxValue - short.MinValue);
                    }
                    handbrake = -(float)(rec.lRz - short.MaxValue) / (short.MaxValue - short.MinValue);
                }
            }
            else
            {
                // pass key board input to the car
                h = CrossPlatformInputManager.GetAxis("Horizontal");
                v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
                handbrake = CrossPlatformInputManager.GetAxis("Jump");
#endif
            }
            m_Car.Move(h, v, v, handbrake);
        }

        void OnDestroy()
        {
            //Free G-Keys SDKs before quitting the game
            LogitechGSDK.LogiGkeyShutdown();
        }
    }
}
