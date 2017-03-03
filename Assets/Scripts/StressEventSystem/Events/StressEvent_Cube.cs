using UnityEngine;
using System.Collections;

public class StressEvent_Cube : StressEvent_InContainer
{
    public GameObject cube;
    private Color m_originColor;
    //------------------------------------------------------
    //  Initialization
    //------------------------------------------------------
    protected override void Start()
    {
        base.Start();
        m_originColor = cube.GetComponent<Renderer>().material.color;
    }

    //------------------------------------------------------
    //  implement the behavior of the event
    //------------------------------------------------------
    public override void StartContainerEvent()
    {
        base.StartContainerEvent();
        cube.GetComponent<Renderer>().material.color = Color.red;
    }

    //------------------------------------------------------
    //  Reset event values, prepare it for the next call
    //------------------------------------------------------
    public override void EndContainerEvent()
    {
        base.EndContainerEvent();
        cube.GetComponent<Renderer>().material.color = m_originColor;
    }
}
