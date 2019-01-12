using UnityEngine;
using System.Collections;

public class TileMovement : MonoBehaviour
{

    public bool horizontal;

    public float speed = 2f;
    public bool dxn_bool;
    public Vector3 startPos, finalPos;

    // Use this for initialization
    void Start()
    {
        dxn_bool = true;
        transform.position = startPos;
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(transform.position == startPos)
        {
            dxn_bool = true;
        }
        else if(transform.position == finalPos)
        {
            dxn_bool = false;
        }

        if(horizontal)
        {
            Horizontal();
        }
        else
        {
            Vertical();
        }
    }

    void Horizontal()
    {
        if(dxn_bool)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPos, Time.deltaTime * speed);
        }
        else if(!dxn_bool)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * speed);
        }
    }

    void Vertical()
    {
        if (dxn_bool)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPos, Time.deltaTime * speed);
        }
        else if (!dxn_bool)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = null;
        }
    }

}
