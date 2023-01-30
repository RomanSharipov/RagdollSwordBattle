using System;
using System.Collections;
using TimeScale;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private const float TimeDeltaTime = 0.02f;

    [Header("Gun")] [SerializeField] private Bullet _bullet;
    [SerializeField] private TimeScaleObserver _timeScaleObserver;
    [SerializeField] private float _rechargeTime = 1;
    [SerializeField] private Transform _bulletSpawnPoint;
    private float _timeAfterLastShoot;

    public float RechargeTime => _rechargeTime;
    public bool IsAbleToAttack => _timeAfterLastShoot >= _rechargeTime;

    private float _timeCoeficent = 1;
    private Coroutine _attack;

    public event Action GunReloaded;
    public event Action Attacked;

    private void Awake()
    {
        _timeAfterLastShoot = _rechargeTime;
        _attack = StartCoroutine(UpdateOnRealTime());
    }

    protected virtual void OnEnable() => _timeScaleObserver.Subscribe();

    protected virtual void OnDisable() => _timeScaleObserver.UnSubscribe();

    private void Start()
    {
        _timeCoeficent = _timeScaleObserver.GetCurrentTimeScale();
        _timeScaleObserver.OnTimeScaleChanged(x => _timeCoeficent = x);
    }

    public void TryAttack(Vector3 target)
    {
        if (IsAbleToAttack)
        {
            _timeAfterLastShoot = 0;
            var newBullet = CreateBullet(target);
            Attack(target, newBullet);
        }
    }

    public void DisableGun() => StopCoroutine(_attack);

    private IEnumerator UpdateOnRealTime()
    {
        var wait = new WaitForSecondsRealtime(TimeDeltaTime);

        while (enabled)
        {
            yield return wait;
            _timeAfterLastShoot += TimeDeltaTime * _timeCoeficent;
            if (IsAbleToAttack)
                GunReloaded?.Invoke();
        }
    }

    private void Attack(Vector3 target, Bullet bullet)
    {
        Attacked?.Invoke();
        bullet.StartMove((target - transform.position).normalized);
    }

    private Bullet CreateBullet(Vector3 target)
    {
        var rotation = Quaternion.LookRotation((target - transform.position).normalized, _bulletSpawnPoint.up);
        return Instantiate(_bullet, _bulletSpawnPoint.position, rotation);
    }
}