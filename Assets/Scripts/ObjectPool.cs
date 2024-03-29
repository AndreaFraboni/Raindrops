using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    private List<GameObject> pooledObjects = new List<GameObject>();
    public int amountObjPool = 20;
    [SerializeField] private GameObject DropPrefabs;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {

        for (int i = 0; i < amountObjPool; i++)
        {
            GameObject obj = Instantiate(DropPrefabs);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

    }

    public void GeneratePoolObject()
    {
        for (int i = 0; i < amountObjPool; i++)
        {
            GameObject obj = Instantiate(DropPrefabs);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public GameObject GetActivePooledObject(int i)
    {
        return pooledObjects[i];
    }

}
