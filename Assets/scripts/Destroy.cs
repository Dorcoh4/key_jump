using Assets.scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destroy : MonoBehaviour
{
    public GameObject p1;
    public GameObject floorPrefab;
    public GameObject topFloor;
    public GameObject[] coloredFloorPrefabs;
    public GameObject[] keyPrefabs;
    public GameObject[] backgrounds;
    public GameObject[] bigStems;
    private List<Vector2> previousList = new List<Vector2>();
    public static readonly float CREATION_RANGE = 4.5f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("welcome to destruction");
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // death

        if (collision.gameObject.GetComponent<movement>() != null)
        {
            Debug.LogError("GAME OVER :_____________(");
            return;
        }

        Debug.Log("hit something " + collision.gameObject.name);

        for (int i = 0; i < backgrounds.Length ; i++)
        {
            GameObject bgPiece = backgrounds[i];
            if (collision.gameObject.Equals(bgPiece))
            {
                Debug.Log("FORDOR WOOOHHOOOO IM HERE FUCK YESSSSSSSSSSSSSSSSSSSSS!!!!!");
                float newBGY = 278.278f; //backgrounds[i == 1 ? 0 : 1].transform.GetChild(0).transform.position.y; //+ backgrounds[i].GetComponent<SpriteRenderer>().sprite.bounds.size.y;
                float PPU = backgrounds[i == 1 ? 0 : 1].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
                Debug.Log("FORDOR " +i+ " dude - " + newBGY);
                //Debug.Log($"ymax {backgrounds[i].GetComponent<SpriteRenderer>().sprite.rect.yMax}, height {backgrounds[i].GetComponent<SpriteRenderer>().sprite.rect.height}, ymin {backgrounds[i].GetComponent<SpriteRenderer>().sprite.rect.yMin}, ");
                bgPiece.transform.position = new Vector3(bgPiece.transform.position.x, bgPiece.transform.position.y + newBGY, bgPiece.transform.position.z);                
                return;
            }

        }
        for (int i = 0; i < bigStems.Length; i++)
        {
            GameObject bgPiece = bigStems[i];
            if (collision.gameObject.Equals(bgPiece))
            {
                //Debug.Log("FORDOR WOOOHHOOOO IM HERE FUCK YESSSSSSSSSSSSSSSSSSSSS!!!!!");
                float newBGY = bigStems[i == 1 ? 0 : 1].GetComponent<SpriteRenderer>().sprite.bounds.size.y + bigStems[i].GetComponent<SpriteRenderer>().sprite.bounds.size.y;
                float PPU = bigStems[i == 1 ? 0 : 1].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
                //Debug.Log("FORDOR " + i + " dude - " + newBGY);
                //Debug.Log($"ymax {backgrounds[i].GetComponent<SpriteRenderer>().sprite.rect.yMax}, height {backgrounds[i].GetComponent<SpriteRenderer>().sprite.rect.height}, ymin {backgrounds[i].GetComponent<SpriteRenderer>().sprite.rect.yMin}, ");
                bgPiece.transform.position = new Vector3(bgPiece.transform.position.x, bgPiece.transform.position.y + newBGY, bgPiece.transform.position.z);
                return;
            }

        }

        keyGeneration();

        if (collision.gameObject.name.Contains("floor"))
        {
            GameObject prefabToCreate;
            int dice = Random.Range(0, 5);
            bool noOverlap = false;

            prefabToCreate = dice < coloredFloorPrefabs.Length ? coloredFloorPrefabs[dice] : floorPrefab;

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

            List<GameObject> allFloorPrefabs = new List<GameObject>(coloredFloorPrefabs);
            allFloorPrefabs.Add(floorPrefab);

            for (int i = 0; !moveFloor && i < allFloorPrefabs.Count; i++)
            {
                GameObject tmpFloorPrefab = allFloorPrefabs[i];
                moveFloor = checkIdentityBadly("floor " + ColorUtils.getColorName(ColorUtils.ColorList[i]), collision, prefabToCreate, tmpFloorPrefab);
            }
            //if (checkIdentityBadly("floor", collision, prefabToCreate, floorPrefab) || checkIdentityBadly("red floor", collision, prefabToCreate, bigJumpFloorPrefab) || checkIdentityBadly("blue floor", collision, prefabToCreate, blueFloorPrefab) || checkIdentityBadly("yellow floor", collision, prefabToCreate, yellowFloorPrefab))
            //{
            //    moveFloor = true;
            //}

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
            //p1.GetComponent<movement>().lastDestroyedHeight = collision.gameObject.transform.position.y;
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
        int dice = Random.Range(0, 18);
        GameObject prefabToCreate = dice < keyPrefabs.Length ? keyPrefabs[dice] : null;
        //if (dice == 1)
        //{
        //    prefabToCreate = redKeyPrefab;
        //    //Debug.Log("dice say yes key pleasey");
            
        //    //Debug.Log("key created!!!!");
        //}
        //else if (dice == 2)
        //{
        //    prefabToCreate = blueKeyPrefab;
        //}
        //else if (dice == 3)
        //{
        //    prefabToCreate = yellowKeyPrefab;
        //}
        //else
        //{
        //    //Debug.Log("dice say no key");
        //}
        if (prefabToCreate != null)
        {
            var newLocation = new Vector2(Random.Range(-CREATION_RANGE, CREATION_RANGE), p1.transform.position.y + 13 * Random.Range(0.5f, 1f));
            var newKey = (GameObject)Instantiate(prefabToCreate, newLocation, Quaternion.identity);
            //Debug.Log("fordor k" + prefabToCreate.ToString());
        }
        else
        {
            //Debug.Log("fordor kak" + keyPrefabs.Length);
        }
        
    }
    private bool checkIdentityBadly(string prefabName, Collider2D collision, GameObject prefabToCreate, GameObject floorPrefab)
    {
        return collision.gameObject.name.StartsWith(prefabName) && prefabToCreate.Equals(floorPrefab);
    }
}
