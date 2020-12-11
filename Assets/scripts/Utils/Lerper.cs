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