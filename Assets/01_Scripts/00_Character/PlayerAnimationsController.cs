using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationsController : MonoBehaviour
{
    [SerializeField] private Animator anims;


    private void Awake()
    {
        if (anims == null) anims = GetComponent<Animator>();
    }

    public UnityEvent OnBeginRoll;
    public UnityEvent OnEndRoll;

    public void TriggerAnim(PlayerAnims anim)
    {
        switch (anim)
        {
            case PlayerAnims.Idle:
                anims.SetTrigger("Idle");
                break;
            case PlayerAnims.Run:
                anims.SetTrigger("Run");
                break;
            case PlayerAnims.Roll:
                anims.SetTrigger("Roll");
                anims.ResetTrigger("Idle");
                anims.ResetTrigger("Run");
                break;
            case PlayerAnims.Throw:
                anims.SetTrigger("Throw");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(anim), anim, null);
        }
    }

    public void BeginRoll()
    {
        OnBeginRoll.Invoke();
    }

    public void EndRoll()
    {
        OnEndRoll.Invoke();
    }
}
