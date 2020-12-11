using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        movement player = collision.gameObject.GetComponent<movement>();
        Rigidbody2D pbody = collision.gameObject.GetComponent<Rigidbody2D>();

        if ( player != null)
        {
            pbody.velocity = new Vector2(0, pbody.velocity.y);
            //Debug.Log($"playerx: {player.transform.position.x}, colliderx: {collision.collider.transform.position.x}, othercolliderx: {collision.otherCollider.transform.position.x}");
            if (collision.otherCollider.transform.position.x < player.transform.position.x)
            {
                player.blockedRight = 1;
                Debug.Log("cant go right!");
                //collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x - 1, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
            }
            else
            {
                //collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x + 1, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
                player.blockedLeft = 1;
                Debug.Log("cant go left!");
            }
        }
    }
}
