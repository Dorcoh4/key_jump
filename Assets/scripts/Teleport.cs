using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : DoorBlocker
{
    public GameObject lerpee;
    private float timeElapsed = 0;
    public float lerpDuration = 0.2f;
    Vector2 lerpStartValue;
    Vector2 lerpendValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (lerpee != null && timeElapsed < lerpDuration)
        {
            lerpee.transform.position = Vector2.Lerp(lerpStartValue, lerpendValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
        }
    }

    protected override void ActivateKey(Collision2D collision, movement p1, KeyPickup.KeyItem correctKey)
    {
        lerpee = collision.gameObject;
        timeElapsed = 0;
        lerpStartValue = lerpee.transform.position;
        var newYLocation = lerpee.transform.position.y + (33 * Random.Range(0.6f, 1f));
        //}
        float range = 4.5f -1 ;
        var newLocation = new Vector2(Random.Range(-range, range), newYLocation);
        lerpendValue = newLocation;

        


        // this is copied code FORDOR
        p1.keys.Remove(correctKey);
        doorOpen = true;
    }
}
