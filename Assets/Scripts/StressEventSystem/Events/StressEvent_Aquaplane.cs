using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class StressEvent_Aquaplane : StressEvent_Immediate
{
    public GameObject rainDropController;
    public UnityStandardAssets.Vehicles.Car.CarController carController;

    private Animator m_rainDropAnim;
    private float[] m_fOriginalForwardStiffness = new float[4];
    private float[] m_fOriginalSidewaysStiffness = new float[4];

    //------------------------------------------------------
    //  Initialization
    //------------------------------------------------------
    override protected void Start()
    {
        base.Start();

        // Check
        Assert.IsNotNull(rainDropController, "Please set a rain drop controller to the Aquaplane event!");
        Assert.IsNotNull(carController, "Please set a car controller to the Aquaplane event!");
        m_rainDropAnim = rainDropController.GetComponent<Animator>();
        Assert.IsNotNull(m_rainDropAnim, "The rain drop controller must have an animator!");

        // Set values
        for (int i = 0; i < 4; ++i)
        {
            m_fOriginalForwardStiffness[i] = carController.WheelColliders[i].forwardFriction.stiffness;
            m_fOriginalSidewaysStiffness[i] = carController.WheelColliders[i].sidewaysFriction.stiffness;
        }
    }
    

    //------------------------------------------------------
    //  Stress Event Start
    //------------------------------------------------------
    override public void StartEvent()
    {
        base.StartEvent();

        // Start Rain
        m_rainDropAnim.SetTrigger("Start");

        // Change Car wheel stiffness
        foreach (WheelCollider wheel in carController.WheelColliders)
        {
            WheelFrictionCurve forwardFriction = wheel.forwardFriction;
            forwardFriction.stiffness = 0.8f;
            wheel.forwardFriction = forwardFriction;
            WheelFrictionCurve sidewaysFriction = wheel.sidewaysFriction;
            sidewaysFriction.stiffness = 0.1f;
            wheel.sidewaysFriction = sidewaysFriction;
        }
    }

    //------------------------------------------------------
    //  Stress Event End
    //------------------------------------------------------
    override public void EndEvent()
    {
        base.EndEvent();

        // End Rain
        m_rainDropAnim.SetTrigger("End");

        // Change Car wheel stiffness
        for (int i = 0; i < 4; ++i)
        {
            WheelCollider wheel = carController.WheelColliders[i];

            // Set values
            WheelFrictionCurve forwardFriction = wheel.forwardFriction;
            forwardFriction.stiffness = m_fOriginalForwardStiffness[i];
            wheel.forwardFriction = forwardFriction;
            WheelFrictionCurve sidewaysFriction = wheel.sidewaysFriction;
            sidewaysFriction.stiffness = m_fOriginalSidewaysStiffness[i];
            wheel.sidewaysFriction = sidewaysFriction;
        }
    }
}
