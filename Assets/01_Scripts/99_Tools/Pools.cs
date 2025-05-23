
using System.Collections.Generic;
using ArthemyDev.ScriptsTools;
using UnityEngine;

//[DefaultExecutionOrder(-1)]
public class Pools : SingletonManager<Pools>
{

    public Dictionary<Pools_Items, Queue<GameObject>> _Pools= new Dictionary<Pools_Items, Queue<GameObject>>();
    public Dictionary<Pools_Items, GameObject> _Prefabs= new Dictionary<Pools_Items, GameObject>();

    public Transform PoolPosition;


    public GameObject GetObject(Pools_Items item)
    {
        if (_Pools.ContainsKey(item)&&_Pools[item].Count!=0)
            return _Pools[item].Dequeue();

        else
        {
            GameObject temp = Instantiate(_Prefabs[item]);
            temp.SetActive(false);
            return temp;
        }
      
    }
    
    

    public void StoreObject(Pools_Items item, GameObject obj)
    {
        obj.transform.position = PoolPosition.position;
        if (_Pools.ContainsKey(item))
        {
            obj.SetActive(false);
            _Pools[item].Enqueue(obj);
        }
        else
        {
            _Pools.Add(item,new Queue<GameObject>());
            obj.SetActive(false);
            _Pools[item].Enqueue(obj);
        }

    }
    
    

    

}
