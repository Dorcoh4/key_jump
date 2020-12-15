using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertCamera : DoorBlocker
{
    private AngularLerper angelaMerkel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (angelaMerkel != null)
        {
           angelaMerkel = angelaMerkel.lerpUpdate();
        }
    }
    protected override void DoActivateKey(Collider2D collision, movement p1, KeyPickup.KeyItem correctKey)
    {
           //p1.MainCamera.transform.Rotate(0, 0, 180);
           //Debug.Log(t.transform.name);
            angelaMerkel = new AngularLerper(p1.MainCamera, 30);
    }
}
