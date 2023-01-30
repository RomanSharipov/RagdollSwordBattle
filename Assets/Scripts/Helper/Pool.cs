using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Pool<T>
    where T : Component
{
    private readonly Stack<T> _prefabsPool = new Stack<T>();
    private readonly T _prefab;

    public Pool(T prefab) => _prefab = prefab;

    public void CreateItems(int count)
    {
        for (int i = 0; i < count; i++) 
            Return(Object.Instantiate(_prefab, Vector3.zero, Quaternion.identity));
    }

    public void Return(T prefab)
    {
        prefab.gameObject.SetActive(false);
        _prefabsPool.Push(prefab);
    }

    public void Return(IEnumerable<T> prefabs)
    {
        foreach (var prefab in prefabs) 
            Return(prefab);
    }

    public IEnumerator WaitAndReturn(T prefab, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Return(prefab);
    }
    
    public IEnumerator WaitAndReturn(IEnumerable<T> prefab, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Return(prefab);
    }

    public T Get(Vector3 startPosition, Transform parent, Quaternion rotation)
    {
        if (_prefabsPool.Count == 0)
            return Object.Instantiate(_prefab, startPosition, rotation, parent);

        var prefab = _prefabsPool.Pop();
        prefab.transform.SetPositionAndRotation(startPosition,rotation);
        prefab.transform.SetParent(parent);
        prefab.gameObject.SetActive(true);
        return prefab;
    }
    
    public T Get(Vector3 startPosition, Transform parent) => Get(startPosition, parent, quaternion.identity);
}