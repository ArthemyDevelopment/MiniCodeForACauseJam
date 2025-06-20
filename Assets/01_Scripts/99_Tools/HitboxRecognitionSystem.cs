using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HitboxRecognitionSystem
{
    private static Dictionary<Collider, Action<float>> _DamageDic= new Dictionary<Collider, Action<float>>();

    public static void AddDamagableObject(Collider col, Action<float> method)
    {
        _DamageDic.Add(col, method);
    }

    public static void RemoveDamagableObject(Collider col)
    {
        _DamageDic.Remove(col);
    }

    public static Action<float> GetDamage(Collider col)
    {
        if(_DamageDic.ContainsKey(col))
            return _DamageDic[col];
        else
        {
            Debug.LogError("Collider not found in dictionary: Please check the awake method of the target and verify its init", col.gameObject);
            return null;
        }
        
    }
    
    public static void ApplyDamage(Collider col, float damage)
    {
        if(_DamageDic.ContainsKey(col))
            _DamageDic[col].Invoke(damage);
        else
            Debug.LogError("Collider not found in dictionary: Please check the awake method of the target and verify its init", col.gameObject);
    }
    
}
