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
    public static readonly int maxKeys = 3;
    public Animator animator;
    public FixedSizedQueue<KeyPickup.KeyItem> keys = new FixedSizedQueue<KeyPickup.KeyItem>(maxKeys);
    public GameObject Canvas;
    private int timer = 0;
    private static readonly float INITIAL_FORCE_POWER = 2.2f;
    private float force = INITIAL_FORCE_POWER;
    public int maxSpeed;
    public GameObject MainCamera;
    public GameObject PlayerKiller;
    private float playerKillerDistanceFromCamera;
    public Lerper cameraLerper;
    public float lastDestroyedHeight;
    public int blockedRight;
    public int blockedLeft;
    public Sprite EmptyImage;
    void Start()
    {
        speed = 10;
        p1 = GetComponent<Rigidbody2D>();
        playerKillerDistanceFromCamera = MainCamera.transform.position.y - PlayerKiller.transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (p1.velocity.y < 0)
        {
            animator = GetComponentInChildren<Animator>();
            animator.SetBool("jump", false);
            animator.SetBool("jumpSide", false);
            animator.SetBool("fall", true);
        }
        

        Camera mainCamera = MainCamera.GetComponent<Camera>();
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
        for (int i = 0; i < ColorUtils.ColorList.Length; i++)
        {
            newText += "\n" + ColorUtils.getColorName(ColorUtils.ColorList[i]) + " keys:" + keyCounts[i];
        }
        Canvas.GetComponent<Text>().text = newText;
        //Canvas.GetComponent<Text>().text = "blue keys:" + blueCount + "\nred keys:" + redCount+ "\nyellow keys:"+ yellowCount + "\nyour mom: 69";
        moveInput = Input.GetAxis("Horizontal");
        p1.velocity = new Vector2(moveInput * speed, p1.velocity.y);
        // GOD MODE   
        moveInput = Input.GetAxis("Vertical");
        if (moveInput > 0) p1.velocity = new Vector2(p1.velocity.x, moveInput * speed*20);
        //if (timer == 0)
        //{
        //    if (!Input.touchSupported && moveInput != 0)
        //    {
        //        //Vector3 direction = moveInput >= 0 ? Vector3.right : Vector3.left;
        //        //p1.AddForce((direction * 2.2f), ForceMode2D.Impulse);
        //    }

        //}
        //int boostTime = 2;

        //Debug.Log($"touches: {Input.touches.Length} touchCOunt: {Input.touchCount} touchsuported: {Input.touchSupported} ");
        if (Input.touches.Length > 0) //&& timer <= boostTime)
        {

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            //Debug.Log("fordor22 " + touchPosition.ToString());
            //TODO add dead zone
            bool goRight = mainCamera.transform.position.x + 2 < touchPosition.x && blockedRight == 0;
            bool goLeft = goRight ? false : mainCamera.transform.position.x > touchPosition.x + 2 && blockedLeft == 0;
            if (goLeft || goRight)
            {
                if (goRight)
                {
                    blockedLeft = 0;
                }
                else
                {

                    blockedRight = 0;
                }
                //Debug.Log("fordor this is left or right or something");
                //p1.AddForce((goRight ? Vector3.right : Vector3.KCleft) * force, ForceMode2D.Impulse);
                //p1.AddForce((goRight ? Vector3.right : Vector3.left) * force * 70f, ForceMode2D.Impulse);
                p1.velocity = new Vector2((goRight ? +1 : -1) * (invertX ? -1 : 1) * 8f, p1.velocity.y);
            }
            //blockedRight = blockedRight != 0 ? blockedRight-1 : blockedRight;
            //blockedLeft = blockedLeft != 0 ? blockedLeft - 1 : blockedLeft; ;

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
            //p1.velocity = new Vector2(0, p1.velocity.y);
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

        // keys cheat

        //if (Input.GetKey(KeyCode.E))
        //{
        //    this.keys.Add(new KeyPickup.KeyItem(KeyPickup.KeyColor.RED));
        //}
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    this.keys.Add(new KeyPickup.KeyItem(KeyPickup.KeyColor.BLUE));
        //}
        //if (Input.GetKey(KeyCode.R))
        //{
        //    this.keys.Add(new KeyPickup.KeyItem(KeyPickup.KeyColor.YELLOW));
        //}
    }
    private static HashSet<Image> scaledImages = new HashSet<Image>();
    private void CountKeys(FixedSizedQueue<KeyPickup.KeyItem> keys, out int[] keyCounts)
    {
        keyCounts = new int[ColorUtils.ColorList.Length];
        int i = 0;
        float sizeFactor = 1.5f;
        int potatoSpin = 235;
        for (; i < keys.Count; i++)
        {
            var key = keys[i];
            keyCounts[ColorUtils.getKey(key.Color)]++;
            Image image = GetComponentsInChildren<Image>()[i];
            image.sprite = key.sprite;
            if (!scaledImages.Contains(image))
            {
                image.transform.localScale = image.transform.localScale = new Vector3(image.transform.localScale.x * sizeFactor, image.transform.localScale.y * sizeFactor, image.transform.localScale.z * sizeFactor);
                image.transform.Rotate(0, 0, potatoSpin);
                scaledImages.Add(image);
            }
            
            
        }
        for (; i < maxKeys; i++)
        {
            Image image = GetComponentsInChildren<Image>()[i];
            image.sprite = EmptyImage;
            if (scaledImages.Contains(image))
            {
                image.transform.localScale = image.transform.localScale = new Vector3(image.transform.localScale.x / sizeFactor, image.transform.localScale.y / sizeFactor, image.transform.localScale.z / sizeFactor);
                image.transform.Rotate(0, 0, -potatoSpin);
                scaledImages.Remove(image);
            }
                
        }
    }

    private void LateUpdate()
    {
        // camera entangle
        if (p1.velocity.x < 0)
        {
            this.transform.localScale = new Vector3((-1) * Mathf.Abs( transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (p1.velocity.x > 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs( transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
       
        Camera mainCamera = MainCamera.GetComponent<Camera>();
        if (p1.position.y + 6 >= lastDestroyedHeight)
        {
            mainCamera.transform.position = new Vector3(MainCamera.transform.position.x, this.gameObject.transform.position.y + 6, MainCamera.transform.position.z);  //new Lerper(MainCamera, 1f, new Vector3(MainCamera.transform.position.x, this.gameObject.transform.position.y + 10, MainCamera.transform.position.z));
            PlayerKiller.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - playerKillerDistanceFromCamera, mainCamera.transform.position.z);
            lastDestroyedHeight = mainCamera.transform.position.y;
        }
        
    }
}



// FORMICH 250f not impulse good for free fall game keft and right
// IMPULSE 2.2f is good for going circles