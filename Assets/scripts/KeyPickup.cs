using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private KeyColor color;
    public class KeyItem{
        public KeyColor Color { get; set; }
        public KeyItem(KeyColor color)
        {
            Color = color;
        }
    }

    public void OnTriggerEnter2D(Collider2D collsion)
    {
        Movement p1 = collsion.GetComponent<Movement>();
        if (p1 != null)
        {
            p1.keys.Enqueue(new KeyItem(color));
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
    public enum KeyColor
    {
        NONE,
        RED,
        BLUE
    }
}
