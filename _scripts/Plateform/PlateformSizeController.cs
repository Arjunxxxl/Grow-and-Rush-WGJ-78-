using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformSizeController : MonoBehaviour
{
    public Vector3 originalPos;
    public Vector3 finalPos;

    public Vector3 originalSize;
    public Vector3 finalSize;

    public float speed = 5f;
    public bool hit, shrink;

    public float duration = 3f;

    public bool moveingTile;
    

    // Start is called before the first frame update
    void Start()
    {
        originalSize = transform.localScale;
        originalPos = transform.position;
        hit = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moveingTile)
        {
            originalPos = transform.position;
            finalPos = transform.position;
        }

        if (hit)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, finalSize, Time.deltaTime * speed);
            transform.position = Vector3.Lerp(transform.position, finalPos, Time.deltaTime * speed);

            StartCoroutine(startShrinking());
            return;
        }
        /*else if(shrink)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalSize, Time.deltaTime * speed * 4f);
        }*/
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet_Growth")
        {
            hit = true;
        }
    }



    IEnumerator startShrinking()
    {
        yield return new WaitForSeconds(duration);
        shrink = true;
        hit = false;
    }

}
