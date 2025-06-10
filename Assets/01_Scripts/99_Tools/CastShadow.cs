using UnityEngine;

public class CastShadow : MonoBehaviour
{

    [SerializeField] private GameObject Shadow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 15))
        {
            Shadow.gameObject.SetActive(true);
            Shadow.transform.position = hit.point + new Vector3(0, 0.005f, 0);
        }
        else
            Shadow.gameObject.SetActive(false);
    }
    
    
}

