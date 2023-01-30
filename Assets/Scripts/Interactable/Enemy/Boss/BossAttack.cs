using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BossAttack : MonoBehaviour
{
    private const float RightSideRotateAngle = -90f;
    private const float LeftSideRotateAngle = 90f;

    [SerializeField] private float _rotationTime = 0.25f;
    [SerializeField] private PlayerHealth _player;
    [SerializeField] private Gun _gun;

    private Coroutine _rotate;

    private void OnEnable()
    {
        _gun.GunReloaded += OnGunReloaded;
    }

    private void OnDisable()
    {
        _gun.GunReloaded -= OnGunReloaded;
    }

    private void Start()
    {
        _rotate = StartCoroutine(RotateOnTarget());
    }

    public void DisableGun() => StopCoroutine(_rotate);

    private void OnGunReloaded()
    {
        _gun.TryAttack(_player.transform.position);
    }

    private IEnumerator RotateOnTarget()
    {
        while (enabled)
        {
            var rotationAngle = transform.position.x > _player.transform.position.x
                ? LeftSideRotateAngle
                : RightSideRotateAngle;
            transform.DORotate(new Vector3(0f, rotationAngle, 0f), _rotationTime);

            yield return null;
        }
    }
}