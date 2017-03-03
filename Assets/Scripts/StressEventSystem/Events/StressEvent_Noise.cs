using UnityEngine;
using System.Collections;

public class StressEvent_Noise : StressEvent_Immediate
{
    public AudioSource noise;

    //------------------------------------------------------
    //  Init
    //------------------------------------------------------
    override protected void Start()
    {
        base.Start();
    }

    //------------------------------------------------------
    //  Stress Event Start
    //------------------------------------------------------
    override public void StartEvent()
    {
        base.StartEvent();
        noise.Play();
        StartCoroutine(EndCounter());
    }

    //------------------------------------------------------
    //  End Counter
    //------------------------------------------------------
    IEnumerator EndCounter()
    {
        yield return new WaitForSeconds(noise.clip.length);
        if (m_eventState != EStressEventState.e_inactive)
            EndEvent();
    }

    //------------------------------------------------------
    //  Stress Event End
    //------------------------------------------------------
    override public void EndEvent()
    {
        base.EndEvent();
        if (noise.isPlaying)
            noise.Stop();
    }
}
