using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public enum EContainerTriggerType
{
    e_ChangingDirection,
    e_Straight
}

public class ContainerEnterTrigger : MonoBehaviour
{
    // Trigger type : a crossing, a turning or a straight road
    public EContainerTriggerType triggerType = EContainerTriggerType.e_Straight;

    //------------------------------------------------------
    // When player enters this trigger of the container
    // This container will become the container 
    // where the next stress event is to be set
    //------------------------------------------------------
    void OnTriggerEnter(Collider p_player)
    {
        // When player enters
        if (p_player.gameObject.tag == "Player")
        {
            // Make sure that the car face the right direction
            float projection = Vector3.Dot(transform.forward, p_player.transform.forward);
            if (projection <= 0.35f)
            {
                if (triggerType == EContainerTriggerType.e_Straight)
                    UIManager.instance.SetWarning(true);
                return;
            }

            UIManager.instance.SetWarning(false);

            if (triggerType == EContainerTriggerType.e_Straight)
            {
                // set current container
                StressEventContainer container = GetComponentInParent<StressEventContainer>();
                Assert.IsNotNull(container);
                StressEventManager.instance.SetCurrentContainer(container);
            }
            else if (triggerType == EContainerTriggerType.e_ChangingDirection)
            {
                // if the player is changing direction, put the event in the waiting list
                StressEventManager.instance.SetCurrentContainer(null);

            }
            
        }
    }
}
