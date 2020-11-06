using Assets.scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public Rigidbody2D p1;
    public float moveInput;
    public FixedSizedQueue<KeyPickup.KeyItem> keys = new FixedSizedQueue<KeyPickup.KeyItem>(3);
    void Start()
    {
        speed = 10;
        p1 = GetComponent<Rigidbody2D>();
}

    // Update is called once per frame
    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        p1.velocity = new Vector2(moveInput * speed, p1.velocity.y);
        
        
        moveInput = Input.GetAxis("Vertical");
        if (moveInput > 0 ) p1.velocity = new Vector2(p1.velocity.x, moveInput * speed);
    }

}
