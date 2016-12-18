using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public enum EContainerTriggerType
{
    e_Crossing,
    e_Straight
}

public class ContainerEnterTrigger : MonoBehaviour
{
    // Trigger type : a crossing or a straight road
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
            if (Vector3.Dot(transform.forward, p_player.transform.forward) <= 0)
            {
                if (triggerType == EContainerTriggerType.e_Straight)
                    Debug.LogError("The player drives in the wrong direction !");
                return;
            }

            // set current container
            StressEventContainer container = GetComponentInParent<StressEventContainer>();
            Assert.IsNotNull(container);
            StressEventManager.instance.SetCurrentContainer(container);
        }
    }
}
