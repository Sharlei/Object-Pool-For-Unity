using System;
using System.Collections;
using UnityEngine;

public class DefaultPoolableBehaviour : MonoBehaviour
{
    [SerializeField] protected float lifeTime;
    
    public float LifeTime => lifeTime;
    public bool IsBusy => _currentLifecycleCoroutine != null;

    private Coroutine _currentLifecycleCoroutine;
    private Action<DefaultPoolableBehaviour> _onFreeCallback;
    
    public void ActivateAt(Vector3 pos, Quaternion rotation, Action<DefaultPoolableBehaviour> onFreeCallback)
    {
        if (_currentLifecycleCoroutine != null) StopCoroutine(_currentLifecycleCoroutine);
        
        transform.SetPositionAndRotation(pos, rotation);
        gameObject.SetActive(true);
        _onFreeCallback = onFreeCallback;
        _currentLifecycleCoroutine = StartCoroutine(LifecycleCoroutine());
    }

    public void ActivateAt(Vector3 pos, Action<DefaultPoolableBehaviour> onFreeCallback)
    {
        ActivateAt(pos, transform.rotation, onFreeCallback);
    }

    public void Interrupt()
    {
        if (_currentLifecycleCoroutine != null) StopCoroutine(_currentLifecycleCoroutine);
        OnInterrupt();
    }
    
    private IEnumerator LifecycleCoroutine()
    {
        OnUseActions();
        yield return new WaitForSeconds(lifeTime);
        OnBeforeInterruptActions();
        OnInterrupt();
    }
    
    private void OnInterrupt()
    {
        _currentLifecycleCoroutine = null;
        gameObject.SetActive(false);
        _onFreeCallback.Invoke(this);
    }

    protected virtual void OnUseActions() {}
    
    protected virtual void OnBeforeInterruptActions() {}
    
}
