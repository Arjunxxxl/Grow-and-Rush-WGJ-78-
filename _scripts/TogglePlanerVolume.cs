using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlanerVolume : MonoBehaviour
{
    public Transform player;
    float z = -16.6f;

    public GameObject volume;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.z < z)
        {
            volume.SetActive(true);
        }
        else
        {
            volume.SetActive(false);
        }
    }
}
