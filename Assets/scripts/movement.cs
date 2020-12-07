using Assets.scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private bool invertX = false;

    internal void InvertControls()
    {
        invertX = !invertX;
        Debug.Log("hallo  " + invertX);
    }

    public Rigidbody2D p1;
    public float moveInput;
    public static readonly int maxKeys = 4;
    public FixedSizedQueue<KeyPickup.KeyItem> keys = new FixedSizedQueue<KeyPickup.KeyItem>(maxKeys);
    public GameObject Canvas;
    private int timer = 0;
    private static readonly float INITIAL_FORCE_POWER = 2.2f;
    private float force  = INITIAL_FORCE_POWER;
    public int maxSpeed;
    void Start()
    {
        speed = 10;
        p1 = GetComponent<Rigidbody2D>();
}

    // Update is called once per frame
    void FixedUpdate()
    {

        
        //if (Math.Abs( p1.velocity.x) < 2f)
        //{
        //    p1.velocity = new Vector2(0, p1.velocity.y);
        //}
        //if (p1.velocity.x > 0 && p1.velocity.x < 50)
        //{
        //    p1.velocity = new Vector2(p1.velocity.x - 0.4f - 1f /-p1.velocity.x, p1.velocity.y);;
        //}
        //else if (p1.velocity.x < 0)
        //{
        //    p1.velocity = new Vector2(p1.velocity.x + 0.4f + 1f /-p1.velocity.x, p1.velocity.y);
        //}



        CountKeys(this.keys, out int[] keyCounts);
        String newText = "";
        for (int i=0; i < ColorUtils.ColorList.Length; i++)
        {
            newText += "\n"+ ColorUtils.getColorName(ColorUtils.ColorList[i]) + " keys:" + keyCounts[i];
        }
        Canvas.GetComponent<Text>().text = newText;
        //Canvas.GetComponent<Text>().text = "blue keys:" + blueCount + "\nred keys:" + redCount+ "\nyellow keys:"+ yellowCount + "\nyour mom: 69";
        moveInput = Input.GetAxis("Horizontal");
        //p1.velocity = new Vector2(moveInput * speed, p1.velocity.y);
        if (timer == 0)
        {
            if (!Input.touchSupported && moveInput != 0)
            {
                //Vector3 direction = moveInput >= 0 ? Vector3.right : Vector3.left;
                //p1.AddForce((direction * 2.2f), ForceMode2D.Impulse);
            }
            
        }
        int boostTime = 2;
       
        //Debug.Log($"touches: {Input.touches.Length} touchCOunt: {Input.touchCount} touchsuported: {Input.touchSupported} ");
        if (Input.touches.Length > 0) //&& timer <= boostTime)
        {

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            Debug.Log("fordor22 " + touchPosition.ToString());
            //TODO add dead zone
            bool goRight = p1.position.x + 2 < touchPosition.x;
            bool goLeft = goRight ? false : p1.position.x > touchPosition.x + 2;
            if (goLeft || goRight)
            {
                Debug.Log("fordor this is left or right or something");
                //p1.AddForce((goRight ? Vector3.right : Vector3.KCleft) * force, ForceMode2D.Impulse);
                //p1.AddForce((goRight ? Vector3.right : Vector3.left) * force * 70f, ForceMode2D.Impulse);
                p1.velocity = new Vector2((goRight ? +1 : -1) * (invertX ? -1 : 1) * 8f , p1.velocity.y );
            }
            
            //    if (timer == 0)
            //    {
            //        timer = 8;
            //        force = 2.2f;
            //    }
            //    else
            //    {
            //        timer--;
            //        force = force / 1.5f;
        }
        else
        {
            p1.velocity = new Vector2(0, p1.velocity.y);
        }

        //}
        //if (timer == 0)
        //{
        //}
        //else if (timer > boostTime)
        //{
        //    timer--;
        // air resistance
        //Debug.Log($"touches: {Input.touches.Length} touchCOunt: {Input.touchCount} touchsuported: {Input.touchSupported} ");

        Vector2 vel = p1.velocity;
        //vel.x *= 0.96f;
        float sign = vel.y < 0 ? -1f : 1f;
        vel.y = Math.Abs(vel.y) < maxSpeed ? vel.y : sign * maxSpeed;
        p1.velocity = vel;
        //}

        // GOD MODE   
        moveInput = Input.GetAxis("Vertical");
        if (moveInput > 0 ) p1.velocity = new Vector2(p1.velocity.x, moveInput * speed);

        if (Input.GetKey(KeyCode.E))
        {
            this.keys.Add(new KeyPickup.KeyItem(KeyPickup.KeyColor.RED));
        }
        if (Input.GetKey(KeyCode.Q))
        {
            this.keys.Add(new KeyPickup.KeyItem(KeyPickup.KeyColor.BLUE));
        }
        if (Input.GetKey(KeyCode.R))
        {
            this.keys.Add(new KeyPickup.KeyItem(KeyPickup.KeyColor.YELLOW));
        }
    }
    private void CountKeys(FixedSizedQueue<KeyPickup.KeyItem> keys, out int[] keyCounts)
    {
        keyCounts = new int[ColorUtils.ColorList.Length];
        for (int i = 0; i < keys.Count; i++)
        {
            var key = keys[i];
            keyCounts[ColorUtils.getKey(key.Color)]++;
        }
    }
}



// FORMICH 250f not impulse good for free fall game keft and right
// IMPULSE 2.2f is good for going circles