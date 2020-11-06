using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public class KeyItem{

    }

    public void OnTriggerEnter2D(Collider2D collsion)
    {
        Movement p1 = collsion.GetComponent<Movement>();
        if (p1 != null)
        {
            p1.keys.Enqueue(new KeyItem());
            Debug.Log("got a key!!!");
        }
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
