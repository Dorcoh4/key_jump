using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboMeasure : MonoBehaviour
{
    public static int Combo;
    private static float timeElapsed;
    private int maxCombo;
    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > 3f)
        {
            Combo = Combo > 0 ? Combo - 1 : 0 ;
            timeElapsed = 0;
        }
        if (Combo > maxCombo)
        {
            maxCombo = Combo;
        }
        gameObject.GetComponent<Text>().text = "Combo: " + Combo + "\n MAX: " + maxCombo ;
    }

    public static void IncrementCombo()
    {
        Combo++;
        timeElapsed = 0;
    }
}
