using Assets.scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : DoorBlocker
{
    public Lerper lerper;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (lerper != null)
        {
            lerper.lerpUpdate();
        }
    }

    protected override void DoActivateKey(Collision2D collision, movement p1, KeyPickup.KeyItem correctKey)
    {
        GameObject lerpee = collision.gameObject;
        var newYLocation = lerpee.transform.position.y + (33 * Random.Range(0.6f, 1f));
        //}
        float range = 4.5f -1 ;
        var newLocation = new Vector2(Random.Range(-range, range), newYLocation);
        lerper = new Lerper(lerpee, 0.2f, newLocation);


        // this is copied code FORDOR
        p1.keys.Remove(correctKey);
        doorOpen = true;
    }
}
