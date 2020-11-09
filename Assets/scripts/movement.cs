using Assets.scripts.Utils;
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
    public static readonly int maxKeys = 3;
    public FixedSizedQueue<KeyPickup.KeyItem> keys = new FixedSizedQueue<KeyPickup.KeyItem>(maxKeys);
    public GameObject Canvas;
    void Start()
    {
        speed = 10;
        p1 = GetComponent<Rigidbody2D>();
}

    // Update is called once per frame
    void FixedUpdate()
    {
        CountKeys(this.keys, out int blueCount, out int redCount);
        Canvas.GetComponent<Text>().text = "blue keys:" + blueCount + "\nred keys:" + redCount+ "\nyour mom: 69";
        moveInput = Input.GetAxis("Horizontal");
        p1.velocity = new Vector2(moveInput * speed, p1.velocity.y);
        
        if (Input.touchCount > 0)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            //TODO add dead zone
            bool goRight = p1.position.x + 2 < touchPosition.x;
            bool goLeft = goRight ? false : p1.position.x > touchPosition.x + 2;
            if (goLeft || goRight)
            {
                p1.AddForce((goRight ? Vector3.right : Vector3.left) * 100);

            }
        }

        // GOD MODE   
        moveInput = Input.GetAxis("Vertical");
        if (moveInput > 0 ) p1.velocity = new Vector2(p1.velocity.x, moveInput * speed);
    }
    private void CountKeys(FixedSizedQueue<KeyPickup.KeyItem> keys, out int blueCount, out int redCount)
    {
        blueCount = 0;
        redCount = 0;
        for (int i = 0; i < keys.Count; i++)
        {
            var key = keys[i];
            switch (key.Color)
            {
                case KeyPickup.KeyColor.BLUE:
                    blueCount++;
                    break;
                case KeyPickup.KeyColor.RED:
                    redCount++;
                    break;
            }
        }
    }

}
