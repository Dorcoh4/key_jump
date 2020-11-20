using Assets.scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public Rigidbody2D p1;
    public float moveInput;
    public static readonly int maxKeys = 4;
    public FixedSizedQueue<KeyPickup.KeyItem> keys = new FixedSizedQueue<KeyPickup.KeyItem>(maxKeys);
    public GameObject Canvas;
    private int timer = 0;
    public int maxSpeed;
    void Start()
    {
        speed = 10;
        p1 = GetComponent<Rigidbody2D>();
}

    // Update is called once per frame
    void FixedUpdate()
    {

        // air resistance
        Vector2 vel = p1.velocity;
        vel.x *= 0.96f;
        float sign = vel.y < 0 ? -1f : 1f;
        vel.y = Math.Abs(vel.y) < maxSpeed ?  vel.y : sign * maxSpeed ; 
        p1.velocity = vel;
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
            if (moveInput > 0)
            {
                p1.AddForce((Vector3.right * 250f));
                timer = 15;
            }
            else if (moveInput < 0)
            {
                p1.AddForce((Vector3.left) * 250f);
                timer = 15;
            }
        }
        else
        {
            timer--;
        }

        if (Input.touchCount > 0)
        {
            //Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            ////TODO add dead zone
            //bool goRight = p1.position.x + 2 < touchPosition.x;
            //bool goLeft = goRight ? false : p1.position.x > touchPosition.x + 2;
            //if (goLeft || goRight)
            //{
            //    p1.AddForce((goRight ? Vector3.right : Vector3.left) * 100);

            //}
        }

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
