using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class UIManager : MonoBehaviour
{
    // Singleton
    public static UIManager instance;

    public GameObject WarningSignal;

    // Use this for initialization
    void Start ()
    {
        // Singleton
        Assert.IsNull(instance, "UI manager has already been instantiated!");
        instance = this;
	}

    public void SetWarning(bool p_bActive) 
    {
        WarningSignal.GetComponent<Animator>().SetBool("Warning", p_bActive);
    }
}
