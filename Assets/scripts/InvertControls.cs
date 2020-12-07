using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertControls : DoorBlocker
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void DoActivateKey(Collision2D collision, movement p1, KeyPickup.KeyItem correctKey)
    {
        p1.InvertControls();
    }

}
