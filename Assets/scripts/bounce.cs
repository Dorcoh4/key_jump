﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : MonoBehaviour
{
    public float jumpFactor;
    private readonly float DEFAULT_JUMP = 550;
    // Start is called before the first frame update
    void Start()
    {
        jumpFactor = jumpFactor != 0 ? jumpFactor : DEFAULT_JUMP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if ( collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpFactor);
            //Debug.Log("something touched me from above");
        }
        else
        {
            //Debug.Log("something touched me from below");
        }
    }
}
