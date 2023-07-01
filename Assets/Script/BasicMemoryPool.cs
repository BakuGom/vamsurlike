using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
    public GameObject prefab;
    public int maxCapacity = 1000;
    public int destroyThreshold = 200;
    public Transform parent; // Reference to the parent object in the Hierarchy

    // Additional fields for EXPItem
    public string itemName;
    public int initialCount;

    [HideInInspector]
    public Queue<GameObject> poolQueue;
}

[System.Serializable]
public class Pool
{
    public List<PoolItem> poolItems;
}

public class BasicMemoryPool : MonoBehaviour
{
    [SerializeField]
    private Pool pool;

    private Dictionary<string, PoolItem> poolDictionary;

    private void Awake()
    {
        InitializePools();
    }

    private void InitializePools()
    {
        poolDictionary = new Dictionary<string, PoolItem>();

        foreach (PoolItem poolItem in pool.poolItems)
        {
            if (poolItem.prefab != null && !poolDictionary.ContainsKey(poolItem.itemName))
            {
                poolItem.poolQueue = new Queue<GameObject>();

                // Create a parent object for the pool if specified
                GameObject parentObject = null;
                if (poolItem.parent != null)
                {
                    parentObject = new GameObject(poolItem.itemName + " Pool");
                    parentObject.transform.SetParent(poolItem.parent);
                }

                for (int i = 0; i < poolItem.initialCount; i++)
                {
                    GameObject newObj = Instantiate(poolItem.prefab, parentObject?.transform);
                    newObj.SetActive(false);
                    poolItem.poolQueue.Enqueue(newObj);
                }

                poolDictionary.Add(poolItem.itemName, poolItem);
            }
        }
    }

    public GameObject Spawn(string itemName, Vector3 position, Quaternion rotation)
    {
        if (poolDictionary.ContainsKey(itemName))
        {
            PoolItem poolItem = poolDictionary[itemName];

            if (poolItem.poolQueue.Count > 0)
            {
                GameObject obj = poolItem.poolQueue.Dequeue();
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
            else
            {
                if (poolItem.maxCapacity > poolItem.poolQueue.Count)
                {
                    GameObject newObj = Instantiate(poolItem.prefab, position, rotation, poolItem.parent);
                    return newObj;
                }
            }
        }

        return null;
    }

    public void Deactivate(GameObject obj)
    {
        PoolItem poolItem = FindPoolItem(obj);
        if (poolItem != null)
        {
            obj.SetActive(false);
            poolItem.poolQueue.Enqueue(obj);

            if (poolItem.poolQueue.Count > poolItem.destroyThreshold)
            {
                int excessCount = poolItem.poolQueue.Count - poolItem.destroyThreshold;
                for (int i = 0; i < excessCount; i++)
                {
                    GameObject objToDestroy = poolItem.poolQueue.Dequeue();
                    Destroy(objToDestroy);
                }
            }
        }
    }

    private PoolItem FindPoolItem(GameObject obj)
    {
        foreach (PoolItem poolItem in pool.poolItems)
        {
            if (poolItem.poolQueue.Contains(obj))
            {
                return poolItem;
            }
        }
        return null;
    }
}
