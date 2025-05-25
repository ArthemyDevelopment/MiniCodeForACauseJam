using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class ElectricTriggerController : MonoBehaviour
{
    public bool isDeactivateable;
    public GameObject ElectricParticles;
    public UnityEvent OnTurnOn;
    [ShowIf("isDeactivateable", true)]public UnityEvent OnTurnOff;


    private void OnEnable()
    {
        if(isDeactivateable)OnTurnOff.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ElectricParticles.SetActive(true);
            OnTurnOn.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isDeactivateable && other.CompareTag("Ball"))
        {
            ElectricParticles.SetActive(false);
            OnTurnOff.Invoke();
        }
    }
}
