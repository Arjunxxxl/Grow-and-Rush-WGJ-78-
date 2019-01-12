using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quality : MonoBehaviour
{
    int index;

    LevelLoader loader;

    public TMP_Dropdown drop;

    // Start is called before the first frame update
    void Start()
    {
       // drop = GetComponent<TMP_Dropdown>();

        index = PlayerPrefs.GetInt("Level", 2);
        QualitySettings.SetQualityLevel(index);
        loader = LevelLoader.Insatnce;

        drop.value = index;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(drop.value);
    }

    public void setQuality(int level)
    {
        QualitySettings.SetQualityLevel(level);
        

        PlayerPrefs.SetInt("Level", level);

        if(level != index)
            loader.Load();
    }

}
