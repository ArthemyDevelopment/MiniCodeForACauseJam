using System;
using System.Collections;
using ArthemyDev.ScriptsTools;
using UnityEngine;

public class EnemyAggroController : MonoBehaviour
{
    public Transform baseTransform;
    public Vector3 LookRotation;
    public GameObject Target;
    public GameObject BulletPrefab;
    public Transform[] ShootingPoint;
    public float fireRate;
    private bool isShooting;
    private Coroutine shootCorutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            LookRotation.y = ScriptsTools.GetRotation(baseTransform, Target.transform);
            baseTransform.rotation = Quaternion.Euler(LookRotation);
            

        }
        else
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Target = other.gameObject;
            isShooting = true;
            if(shootCorutine!=null) StopCoroutine(shootCorutine);
            shootCorutine = StartCoroutine(Shoot());
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Target = null;
            isShooting = false;
            if(shootCorutine!=null)StopCoroutine(shootCorutine);
            shootCorutine = null;
        }
    }

    IEnumerator Shoot()
    {
        while (isShooting)
        {
            for (int i = 0; i < ShootingPoint.Length; i++)
            {
                Instantiate(BulletPrefab, ShootingPoint[i].position, transform.rotation);
            }

            yield return ScriptsTools.GetWait(fireRate);
        }
    }

    private void OnDisable()
    {
        if(shootCorutine!=null)StopCoroutine(shootCorutine);
    }
}
