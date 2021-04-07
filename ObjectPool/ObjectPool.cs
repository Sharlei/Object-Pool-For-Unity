using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private DefaultPoolableBehaviour[] _prefabs;
    [Tooltip("Order of prefabs instantiation (if there is more than 1) will be random. " +
             "If it is not checked, prefabs will be instantiated one by one repeating.")][SerializeField] private bool instantiatePrefabsRandomly;
    
    [Tooltip("Pool capacity min - is the count of objects, that will be instantiated on startup. " +
             "In the case if all pool objects are in using, new ones will be instantiated, but the total " +
             "number of objects won't be more than Pool capacity max.")]
    [SerializeField][MinMaxSlider(0,50)] private Vector2Int poolCapacity;

    private List<DefaultPoolableBehaviour> _pool = new List<DefaultPoolableBehaviour>();
    private int _freeObjectPointer;
    private int _prefabsArrayPointer;
    
    private void Awake()
    {
        if (_prefabs == null) return;
        
        for (int i = 0; i < poolCapacity.x; i++)
        {
            CreateNewInstance();
        }
    }

    public DefaultPoolableBehaviour UseObjectAt(Vector3 pos)
    {
        if (_pool.Count == 0) Debug.LogError($"Trying to use empty pool: {gameObject}");
        
        var obj = _pool[_freeObjectPointer];
        obj.ActivateAt(pos, ObjectFreed);
        
        while (_pool[_freeObjectPointer].IsBusy)
        {
            if (_freeObjectPointer == _pool.Count - 1)
            {
                if (_pool.Count < poolCapacity.y) CreateNewInstance();
                else SetPointerToFirst();
                
                break;
            }
            _freeObjectPointer++;
        }

        return obj;
    }

    public void ObjectFreed(DefaultPoolableBehaviour freedObject)
    {
        _freeObjectPointer = _pool.IndexOf(freedObject);
    }

    private void SetPointerToFirst()
    {
        _freeObjectPointer = 0;
    }

    private void CreateNewInstance()
    {
        if (_prefabs.Length == 0) return;
        if (_prefabsArrayPointer == _prefabs.Length) _prefabsArrayPointer = 0;
        if (instantiatePrefabsRandomly) _prefabsArrayPointer = Random.Range(0, _prefabs.Length);
        
        var newInstance = Instantiate(_prefabs[_prefabsArrayPointer], transform).GetComponent<DefaultPoolableBehaviour>();
        _pool.Add(newInstance);
        newInstance.gameObject.SetActive(false);
        _prefabsArrayPointer++;
    }
}
