using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger : MonoBehaviour
{

    public UnityEvent OnTrigger;
    private bool alreadyTrigger = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!alreadyTrigger)
        {
            alreadyTrigger = true;
            OnTrigger.Invoke();
        }
    }
}
