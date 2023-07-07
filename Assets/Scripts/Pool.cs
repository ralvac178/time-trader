using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PoolItem
{
    public GameObject prefab;
    public int amount;
    public bool isExtensible;
}

public class Pool : MonoBehaviour
{
    public static Pool singleton; // Sigleton

    [SerializeField] private List<PoolItem> items;
    [SerializeField] private List<GameObject> pooledItems;

    private void Awake()
    {
        singleton = this;

        // Creating the pool
        pooledItems = new List<GameObject>();

        foreach (PoolItem item in items)
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
            }
        }
        // End of poolObject

        Utils.Shuffle(pooledItems); //Suffle the list of pooled items
    }

    public GameObject GetRandomItem()
    {
        
        for (int i = 0; i < pooledItems.Count; i++)
        {
            if (!pooledItems[i].activeInHierarchy)
            {
                return pooledItems[i];
            }
            
        }
        foreach (PoolItem item in items)
        {
            if (item.isExtensible)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
                return obj;
            }
        }
        return null;
    }
}

public static class Utils
{
    public static System.Random r = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = r.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
