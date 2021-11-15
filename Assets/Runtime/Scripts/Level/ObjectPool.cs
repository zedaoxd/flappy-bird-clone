using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    private T prefab;
    private List<T> freeList;
    private List<T> usedList;

    public ObjectPool(T prefab, int initialSize, Vector3 position, Quaternion rotation, Transform parent)
    {
        this.prefab = prefab;
        freeList = new List<T>(initialSize);
        usedList = new List<T>(initialSize);

        for (int i = 0; i < initialSize; i++)
        {
            T currentPrefab = Instantiate(prefab, position, rotation, parent);
            currentPrefab.gameObject.SetActive(false);
            freeList.Add(currentPrefab);
        }
    }

    public T GetFromPool(Vector2 position, Quaternion rotation, Transform parent)
    {
        int availableT = freeList.Count;
        T currentObj;
        if (availableT > 0)
        {
            currentObj = freeList[availableT - 1];
            freeList.RemoveAt(availableT - 1);
            usedList.Add(currentObj);

        }
        else
        {
            currentObj = Instantiate(prefab);
            usedList.Add(currentObj);
        }

        currentObj.transform.position = position;
        currentObj.transform.rotation = rotation;
        currentObj.transform.SetParent(parent);
        currentObj.gameObject.SetActive(true);
        return currentObj;
    }

    public void ReturnToPool(T obj)
    {
        if (usedList.Contains(obj))
        {
            obj.gameObject.SetActive(false);
            usedList.Remove(obj);
            freeList.Add(obj);
        }
        else
        {
            Debug.LogError("Isso n veio daqui");
        }
    }
}
