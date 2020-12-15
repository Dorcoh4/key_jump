using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : MonoBehaviour
{
    public float jumpFactor;
    private readonly float DEFAULT_JUMP = 1000;
    private int cnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        jumpFactor = jumpFactor != 0 ? jumpFactor : DEFAULT_JUMP;

        //GetComponent<AudioSource>().playOnAwake = false;
        //GetComponent<AudioSource>().clip = sound;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
        if ( rigidBody != null && rigidBody.velocity.y <= 0)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            rigidBody.AddForce(Vector3.up * jumpFactor);

            //Debug.Log("bboooouuuunnnnnccccccccceeeeee (::) " + cnt);
            if (collision.gameObject.GetComponent<movement>() != null)
            {
                //GetComponent<AudioSource>().Play();

            }
        }
        else
        {
            //Debug.Log("something touched me from below");
        }
    }
}
