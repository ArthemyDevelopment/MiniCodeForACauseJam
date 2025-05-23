
using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;



public class HealthManager : MonoBehaviour
{
    [BoxGroup("Stats"), SerializeField] protected Collider2D hitBox;
    [BoxGroup("Stats"),SerializeField]protected int BaseHealth;
    [BoxGroup("Stats"),SerializeField, ReadOnly]protected float _actHealth;
    public UnityEvent OnDeath;
    
#if UNITY_EDITOR

    [FoldoutGroup("DEBUG")][Button]public void DEBUG_ApplyDamage(float damage){ApplyDamage(damage);}
    [FoldoutGroup("DEBUG")][Button]public void DEBUG_HealDamage(float damage){AddHealth(damage);}
    [FoldoutGroup("DEBUG")][Button]public void DEBUG_ResetHealth(){ResetHealth();}
    [FoldoutGroup("DEBUG")][Button] public void DEBUG_Kill() { ActHealth = 0;}

#endif

    protected virtual void Awake()
    {
        if (hitBox == null) hitBox = GetComponent<Collider2D>();
        HitboxRecognitionSystem.AddDamagableObject(hitBox, ApplyDamage);
    }

    private void OnDestroy()
    {
        HitboxRecognitionSystem.RemoveDamagableObject(hitBox);
    }

    public float ActHealth
    {
        get => _actHealth;
        set
        {
            _actHealth = value;
            CheckDeath();
            UpdateHealthGUI();
        }
    }

    protected virtual void OnEnable()
    {
        //ResetHealth();
    }

    public virtual void SetHealth(int i)
    {
        BaseHealth = i;
        ResetHealth();
    }

    public virtual void ResetHealth()
    {
        ActHealth = BaseHealth;
    }

    public virtual void AddHealth(float heal)
    {
        ActHealth += heal;
    }

    public virtual void ApplyDamage(float damage)
    {
        ActHealth -= damage;
    }

    public virtual void CheckDeath()
    {
        if (ActHealth <= 0)
        {
            OnDeath?.Invoke();
            DeathBehaviour();
        }
    }

    public virtual void DeathBehaviour()
    {
        
    }

    public virtual void UpdateHealthGUI()
    {
        
    }
}
