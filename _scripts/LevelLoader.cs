using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{   
    public GameObject loadingScreen;
    public Slider slider;

    #region SingleTon
    public static LevelLoader Insatnce;
    private void Awake()
    {
        Insatnce = this;
    }
    #endregion  

    // Start is called before the first frame update
    void Start()
    {
        loadingScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.J))
        {
            StartCoroutine(LoadLevel(0));
        }
    }

    public void Load()
    {
        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int name)
    {
        loadingScreen.SetActive(true);

        AsyncOperation op = SceneManager.LoadSceneAsync(name);
        while(!op.isDone)
        {
            slider.value = Mathf.Clamp01(op.progress / 0.9f);
            yield return null;
        }
    }

}
