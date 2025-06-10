using UnityEngine;
using UnityEngine.Events;

public class InteractivesController : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private UnityEvent OnTriggerOn;
    [SerializeField] private UnityEvent OnTriggerOff;
    

    public void SetTrigger(bool state)
    {
        if (!isActive) return;
        
        if(state) OnTriggerOn.Invoke();
        else OnTriggerOff.Invoke();
    }

    public void DeactiveTrigger()
    {
        isActive = false;
    }
}
