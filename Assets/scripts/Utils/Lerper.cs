using UnityEngine;
using System.Collections;

namespace Assets.scripts.Utils
{
    public class Lerper
    {
        private GameObject lerpee;
        private float timeElapsed = 0;
        private float lerpDuration ;
        private Vector3 lerpStartValue;
        private Vector3 lerpendValue;

        public Lerper(GameObject lerpee, float lerpDuration, Vector3 Lerpend)
        {
            this.lerpee = lerpee;
            this.lerpDuration = lerpDuration;
            this.lerpStartValue = lerpee.transform.position;
            this.lerpendValue = Lerpend;
        }

        public Lerper lerpUpdate()
        {
            if (lerpee != null && timeElapsed < lerpDuration)
            {
                lerpee.transform.position = Vector3.Lerp(lerpStartValue, lerpendValue, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                return this;
            }
            else
            {
                return null;
            } 
        }
    }
}

public class AngularLerper
{
    private GameObject lerpee;
    private int timeElapsed = 0;
    private int lerpDuration;
    private float lerpStartValue;
    private float lerpendValue;
    private static System.Collections.Generic.Queue<GameObject> queue = new System.Collections.Generic.Queue<GameObject>();

    public AngularLerper(GameObject lerpee, int lerpDuration)
    {
        
        this.lerpDuration = lerpDuration;
        this.lerpStartValue = lerpee.transform.eulerAngles.z;
        this.lerpendValue = lerpee.transform.eulerAngles.z + 180;
        queue.Enqueue(lerpee);
        this.lerpee = queue.Dequeue();
    }

    public AngularLerper lerpUpdate()
    {
        if (lerpee != null && timeElapsed < lerpDuration)
        {
            Debug.Log("angela merkel says go");
            lerpee.transform.Rotate(lerpee.transform.rotation.x, lerpee.transform.rotation.y, (180) / lerpDuration);
            timeElapsed++;
            return this;
        }
        else
        {
            Debug.Log("angela merkel says no");
            return null;
        }
    }
}
