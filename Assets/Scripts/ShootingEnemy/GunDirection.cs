using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class GunDirection : Gun
{
    [FormerlySerializedAs("target")] [Header("Direction")] [SerializeField]
    private Transform _target;

    [SerializeField] private bool _isAutoAttack;

    private Coroutine _autoAttack;

    private void Start()
    {
        if (_isAutoAttack)
            StartAutoAttack();
    }

    public void StopAutoAttack() => StopCoroutine(_autoAttack);

    private void StartAutoAttack() => _autoAttack =
        StartCoroutine(StartAutoAttack(_target.position + (_target.position - transform.position)));

    private IEnumerator StartAutoAttack(Vector3 target)
    {
        while (enabled)
        {
            TryAttack(target);
            yield return new WaitForSecondsRealtime(RechargeTime);
        }
    }

# if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var length = 10;
        Gizmos.color = Color.red;
        var position = transform.position;
        Gizmos.DrawLine(position, position + (_target.transform.position - position).normalized * length);
    }
#endif
}