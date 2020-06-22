using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance { get; private set; }

    Dictionary<Object, Queue<Object>> pools = new Dictionary<Object, Queue<Object>>();

    private void Awake()
    {
        instance = this;
    }

    public void InitObjectPool(Object prefab,int size)
    {
        if (pools.ContainsKey(prefab)) return;

        Queue<Object> queue = new Queue<Object>();

        for(int i = 0; i < size; ++i)
        {
            var o = Instantiate(prefab);
            SetActive(o, false);
            queue.Enqueue(o);
        }

        pools[prefab] = queue;
    }

    public T GetInstance<T>(Object prefab)where T : Object
    {
        Queue<Object> queue;
        if(pools.TryGetValue(prefab,out queue))
        {
            Object obj;

            if (queue.Count > 0)
            {
                obj = queue.Dequeue();
            }
            else
            {
                obj = Instantiate(prefab);
            }
            SetActive(obj, true);
            queue.Enqueue(obj);

            return obj as T;
        }

        UnityEngine.Debug.LogError("No pool was init with this prefab");
        return null;
    }

    static void SetActive(Object obj,bool active)
    {
        GameObject go = null;

        if(obj is Component component)
        {
            go = component.gameObject;
        }
        else
        {
            go = obj as GameObject;
        }

        go.SetActive(active);
    }
}
