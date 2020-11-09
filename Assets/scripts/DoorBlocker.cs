using Assets.scripts.Utils;
using UnityEngine;

public class DoorBlocker : MonoBehaviour
{
    private bool doorOpen = false;
    public string ColorString;
    public KeyPickup.KeyColor keyColor;

    // Start is called before the first frame update
    void Start()
    {
        //keyColor = KeyPickup.StringToColor(ColorString);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            if (!doorOpen)
            {
                Movement p1 = collision.gameObject.GetComponent<Movement>();
                if (p1 != null)
                {
                    KeyPickup.KeyItem correctKey = GetCorrectKey(p1.keys);
                    if (correctKey == null)
                    {
                        Rigidbody2D playerBody = collision.gameObject.GetComponent<Rigidbody2D>();
                        playerBody.velocity = new Vector2(playerBody.velocity.y, 0);
                        playerBody.AddForce(Vector3.down * 100);
                        Debug.Log("no key :(");
                    }
                    else
                    {
                        p1.keys.Remove(correctKey);
                        doorOpen = true;
                    }
                }

            }
        }
    }

    private KeyPickup.KeyItem GetCorrectKey(FixedSizedQueue<KeyPickup.KeyItem> keys)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            var key = keys[i];
            if (key.Color.Equals(keyColor))
            {
                return key;
            }
        }
        return null;
    }
}
