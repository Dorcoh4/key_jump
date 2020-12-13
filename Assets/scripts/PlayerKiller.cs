using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // death

        if (collision.gameObject.GetComponent<movement>() != null)
        {
            Debug.LogError("GAME OVER :_____________(");
            Destroy(collision.gameObject);
            return;
        }
    }
}
