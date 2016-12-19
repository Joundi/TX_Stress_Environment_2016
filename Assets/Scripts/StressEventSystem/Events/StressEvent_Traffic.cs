using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class StressEvent_Traffic : StressEvent_Immediate
{
    public int maxCarNb = 150;
    public int normalCarNb = 10;

    public TSTrafficSpawner m_trafficController;
    //------------------------------------------------------
    //  Initialization
    //------------------------------------------------------
    override protected void Start()
    {
        base.Start();

        // get traffic controller
        Assert.IsNotNull(m_trafficController, 
            "Please attache a TSTrafficSpawner!");

        // Set car nb
        m_trafficController.amount = normalCarNb;
        m_trafficController.totalAmountOfCars = normalCarNb;
    }

    //------------------------------------------------------
    //  Stress Event Start
    //------------------------------------------------------
    override public void StartEvent()
    {
        base.StartEvent();

        // Set car nb
        m_trafficController.amount = maxCarNb;
        m_trafficController.totalAmountOfCars = maxCarNb;
    }

    //------------------------------------------------------
    //  Stress Event End
    //------------------------------------------------------
    override public void EndEvent()
    {
        base.EndEvent();

        // Set car nb
        m_trafficController.amount = normalCarNb;
        m_trafficController.totalAmountOfCars = normalCarNb;
    }
}
