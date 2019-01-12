using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{   
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public int size;
        public GameObject perfab;
    }

    #region SingleTon
    public static BulletPool Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools = new List<Pool>();
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();

            for(int i = 0; i<pool.size; i++)
            {
                GameObject obj = Instantiate(pool.perfab);
                obj.transform.parent = this.transform;
                obj.SetActive(false);
                objPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objPool);
        }
    }

    public GameObject SpawmFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        GameObject spawnObj = poolDictionary[tag].Dequeue();
        spawnObj.transform.position = position;
        spawnObj.transform.rotation = rotation;
        spawnObj.SetActive(true);

        poolDictionary[tag].Enqueue(spawnObj);

        return spawnObj;
    }

  

}
