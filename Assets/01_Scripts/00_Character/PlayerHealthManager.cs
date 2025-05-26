using System;
using System.Collections;
using ArthemyDev.ScriptsTools;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthManager : HealthManager
{
    [BoxGroup("Stats"),SerializeField]protected Image HealthShader;
    [BoxGroup("Stats"), SerializeField] protected Sprite[] HealthSprites = new Sprite[5];
    [BoxGroup("Stats"), SerializeField] protected float damageCooldown;
    [BoxGroup("Stats"), SerializeField] protected bool canRecieveDamage=true;


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ResetHealth();
    }

    private void OnDisable()
    {
        
    }

    public override void DeathBehaviour()
    {
        StartCoroutine(ResetScene());
    }

    IEnumerator ResetScene()
    {
        yield return ScriptsTools.GetWait(5);

        SceneManager.LoadScene("GameScene");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")&&canRecieveDamage)
        {
            canRecieveDamage = false;
            StartCoroutine(CooldownDamage());
            ApplyDamage(1);
        }
    }

    IEnumerator CooldownDamage()
    {
        yield return ScriptsTools.GetWait(damageCooldown);
        canRecieveDamage = true;
    }


    public override void ApplyDamage(float damage)
    {
        base.ApplyDamage(damage);
    }

    public override void UpdateHealthGUI()
    {
        if (ActHealth < 5 && ActHealth >= 0)
        {
            HealthShader.sprite = HealthSprites[(int)ActHealth];
        }
    }
}
