using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    
    public KeyColor keyColor;
    public string ColorString;
    public class KeyItem{
        public KeyColor Color { get; set; }
        public KeyItem(KeyColor color)
        {
            Color = color;
        }
    }

    //public static KeyColor StringToColor(string colorString)
    //{
    //    switch (colorString.ToLower())
    //    {
    //        case "red":
    //            return KeyColor.RED;
    //            break;
    //        case "blue":
    //            return KeyColor.BLUE;
    //                break;
    //        default:
    //            Debug.Log("bad key color string - " + colorString);
    //            return KeyColor.NONE;
    //    }
    //}

    public void OnTriggerEnter2D(Collider2D collsion)
    {
        movement p1 = collsion.GetComponent<movement>();
        if (p1 != null)
        {
            p1.keys.Add(new KeyItem(keyColor));
            //Debug.Log("got a key!!!");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //keyColor = KeyPickup.StringToColor(ColorString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public enum KeyColor
    {
        RED,
        BLUE,
        YELLOW,
        PURPLE,
        NONE
    }
}
