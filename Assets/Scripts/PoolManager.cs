using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonMonoBehaviour<PoolManager>
{
    [System.Serializable]
    public class Pool
    {
        public int poolSize;
        public GameObject prefab;
    }

    private Dictionary<int,Queue<GameObject>>poolDictionary=new Dictionary<int, Queue<GameObject>>();

    [SerializeField] private Pool[] pool = null;
    [SerializeField] private Transform poolTransform;

    void Start()
    {
        //Create Pool Objects
        for (int i = 0; i < pool.Length; i++)
        {
            CreatePool(pool[i].prefab, pool[i].poolSize);
        }
    }

    private void CreatePool(GameObject prefab, int poolSize)
    {
        int poolKey=prefab.GetInstanceID();
        string prefabName = prefab.name;

        //Create parent GameObject to parent pool GameObjects to it
        GameObject parentGameObject = new GameObject(prefabName+"Anchor");

        parentGameObject.transform.parent = poolTransform;

        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey,new Queue<GameObject>());

            for (int i = 0; i < poolSize; i++)
            {
                GameObject newGameObject =Instantiate(prefab,parentGameObject.transform)as GameObject;
                newGameObject.SetActive(false);
                poolDictionary[poolKey].Enqueue(newGameObject);

            }
        }
    }


    public GameObject ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        int poolKey = prefab.GetInstanceID();

        if (poolDictionary.ContainsKey(poolKey))
        {
            GameObject objectToReuse = GetObjectFromPool(poolKey);

            ResetObject(position, rotation, objectToReuse, prefab);

            return objectToReuse;
        } else
        {
            Debug.Log("No Object pool for"+ prefab);
            return null;
        }
    }

    private GameObject GetObjectFromPool(int poolKey)
    {
        GameObject objectToReuse=poolDictionary[poolKey].Dequeue();
        poolDictionary[poolKey].Enqueue(objectToReuse);

        if (objectToReuse.activeSelf==true)
        {
            objectToReuse.SetActive(false);
        }

        return objectToReuse;
    }

    private void ResetObject(Vector3 position, Quaternion rotation, GameObject objectToReuse, GameObject prefab)
    {
        objectToReuse.transform.position = position;
        objectToReuse.transform.rotation = rotation;

        objectToReuse.transform.localScale = prefab.transform.localScale;
    }
}
