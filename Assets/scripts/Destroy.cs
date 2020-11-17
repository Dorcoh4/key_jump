using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject p1;
    public GameObject floorPrefab;
    public GameObject bigJumpFloorPrefab;
    public GameObject blueFloorPrefab;
    public GameObject topFloor;
    public GameObject redKeyPrefab;
    public GameObject blueKeyPrefab;
    private List<Vector2> previousList = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("welcome to destruction");
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject prefabToCreate;
        int dice = Random.Range(1, 6);
        bool noOverlap = false;
        keyGeneration();
        if (dice == 1)
        {
            prefabToCreate = bigJumpFloorPrefab;
        }
        else if (dice == 2)
        {
            prefabToCreate = blueFloorPrefab;
        }
        else
        {
            prefabToCreate = floorPrefab;
        }

        //if (topFloor == null || floor.transform.position.y > topFloor.transform.position.y)
        //{
        //    topFloor = floor;
        //}

        //var newYLocation = p1.transform.position.y + (15);
        //if (floor != null && newYLocation - floor.transform.position.y < 7 )
        //{
        var newYLocation = topFloor.transform.position.y + (4 * Random.Range(0.6f, 1f));
        //}

        var newLocation = new Vector2(Random.Range(-9.0f, 9.0f), newYLocation);

        bool moveFloor = false;

        if (checkIdentityBadly("floor", collision, prefabToCreate, floorPrefab) || checkIdentityBadly("red floor", collision, prefabToCreate, bigJumpFloorPrefab) || checkIdentityBadly("blue floor", collision, prefabToCreate, blueFloorPrefab)
        {
            moveFloor = true;
        }

        if (moveFloor)
        {
            collision.gameObject.transform.position = newLocation;
            topFloor = collision.gameObject;
        }
        else
        {
            topFloor = (GameObject)Instantiate(prefabToCreate, newLocation, Quaternion.identity);
            Destroy(collision.gameObject);
        }

        //Vector2 newLocation;

        //do
        //{
        //    newLocation = new Vector2(Random.Range(-5.5f, 5.5f), p1.transform.position.y + (9 * Random.Range(0.5f, 0.8f)));

        //    foreach (Vector2 oldLocation in previousList)
        //    {
        //        if (Vector2.Distance(newLocation, oldLocation) < 10000000)
        //        {
        //            noOverlap = false;
        //            break;
        //        }
        //    }
        //    noOverlap = true;
        //} while (!noOverlap);

        //previousList.Add(newLocation);

        //floor = (GameObject)Instantiate(prefabToCreate, newLocation, Quaternion.identity);
        //Destroy(collision.gameObject);
        //if (previousList.Count > 1) previousList.Remove(previousList[0]);
    }
    private void keyGeneration()
    {
        int dice = Random.Range(1, 10);
        GameObject prefabToCreate = null;
        if (dice == 1)
        {
            prefabToCreate = redKeyPrefab;
            //Debug.Log("dice say yes key pleasey");
            
            //Debug.Log("key created!!!!");
        }
        else if (dice == 2)
        {
            prefabToCreate = blueKeyPrefab;
        }
        else
        {
            //Debug.Log("dice say no key");
        }
        if (prefabToCreate != null)
        {
            var newLocation = new Vector2(Random.Range(-5.5f, 5.5f), p1.transform.position.y + 13 * Random.Range(0.5f, 1f));
            var newKey = (GameObject)Instantiate(prefabToCreate, newLocation, Quaternion.identity);
        }
        
    }
    private bool checkIdentityBadly(string prefabName, Collider2D collision, GameObject prefabToCreate, GameObject floorPrefab)
    {
        return collision.gameObject.name.StartsWith(prefabName) && prefabToCreate.Equals(floorPrefab);
    }
}
