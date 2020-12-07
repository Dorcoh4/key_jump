using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertCamera : DoorBlocker
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void DoActivateKey(Collision2D collision, movement p1, KeyPickup.KeyItem correctKey)
    {
        for (int i = 0; i < p1.transform.childCount - 1; i++)
        {
            Transform t = p1.transform.GetChild(i);
            if (t.transform.name.Contains("Camera"))
            {
                t.transform.Rotate(0, 0, 180);
            }
            Debug.Log(t.transform.name);
        }
    }
}
