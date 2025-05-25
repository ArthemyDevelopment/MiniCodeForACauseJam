
using System;
using ArthemyDev.ScriptsTools;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class EnemyHealthManager : HealthManager
{
    [FormerlySerializedAs("ScoreEvent")] [FormerlySerializedAs("EO")] [SerializeField] private EventObserver EventObserver;
    
    [BoxGroup("Stats"),SerializeField]protected Image HealthShader;


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        EventObserver.OnResetGame += DestroyEnemy;
        ResetHealth();
    }

    private void OnDisable()
    {
        EventObserver.OnResetGame -= DestroyEnemy;
        
    }

    public override void DeathBehaviour()
    {
        if (ActHealth <= 0)
        {
            EventObserver.AddScore(1);
            
        }
        else
        {
            EventObserver.RestScore(BaseHealth);
            
        }
        DestroyEnemy();
    }

    void DestroyEnemy()
    {
        //Pools.current.StoreObject(thisEnemy_Type, this.gameObject);w
        Destroy(gameObject);
    }

    public override void ApplyDamage(float damage)
    {
        base.ApplyDamage(damage);
    }

    public override void UpdateHealthGUI()
    {
        float temp = ScriptsTools.MapValues(ActHealth, 0, BaseHealth, 0, 1);
        HealthShader.fillAmount = temp;
    }
    
    
}
