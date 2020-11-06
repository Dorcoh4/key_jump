using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigbounce : bounce
{
    private readonly float DEFAULT_JUMP = 750;
    public float downBounce;
    void Start()
    {
        jumpFactor = jumpFactor != 0 ? jumpFactor : DEFAULT_JUMP;
    }
    public float superjumpFactor;
    //protected float getJumpFactor()
    //{
    //    return superjumpFactor;
    //}
    override protected void underEffect(Collision2D collision)
    {
        Rigidbody2D playerBody = collision.gameObject.GetComponent<Rigidbody2D>();
        playerBody.velocity = new Vector2(playerBody.velocity.y, 0); ;
        playerBody.AddForce(Vector3.down * 100);
    }
}
