using UnityEngine;
using System.Collections;

public class StressEvent_Boy : StressEvent_InContainer
{
    public GameObject boy;          // the game object of the boy character to be assigned in inspector
    public float runningSpeed = 1f; // running speed of the boy

    private float m_fTimeCounter = 0f;
    private Vector3 m_initialPosition;
    private Quaternion m_initialRotation;

    //------------------------------------------------------
    //  Initialization
    //------------------------------------------------------
    protected override void Start()
    {
        base.Start();

        if (boy == null)
            Debug.LogError("Please assign a boy game object to the script StressEvent_boy!");

        m_initialPosition = boy.transform.localPosition;
        m_initialRotation = boy.transform.localRotation;
    }

    //------------------------------------------------------
    //  implement the behavior of the event
    //------------------------------------------------------
    public override void StartContainerEvent()
    {
        base.StartContainerEvent();
        m_fTimeCounter = 0f;
        boy.GetComponent<Animation>().Play("run ");
    }

    //------------------------------------------------------
    //  Reset event values, prepare it for the next call
    //------------------------------------------------------
    public override void EndContainerEvent()
    {
        base.EndContainerEvent();
        boy.GetComponent<Animation>().Stop("run "); // stop animation
        boy.transform.localPosition = m_initialPosition; // reset position
        boy.transform.localRotation = m_initialRotation; // reset rotation
    }

    //------------------------------------------------------
    //  boy running movement
    //------------------------------------------------------
    private void FixedUpdate()
    {
        // Move the boy if the event is happening
        if (m_eventState == EStressEventState.e_ongoing)
        {
            m_fTimeCounter += Time.deltaTime;
            boy.GetComponent<Rigidbody>().MovePosition(boy.transform.position + boy.transform.forward * Time.deltaTime * runningSpeed);

            // Condition of the end of the event : after 6 seconds, the boy will disappear
            if (m_fTimeCounter > 6f)
            {
                EndEvent();
            }
        }
    }
}