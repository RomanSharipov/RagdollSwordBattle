using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimateShield : MonoBehaviour
{
    [SerializeField] private Transform _shield;
    [SerializeField] private float _time = 0.1f;
    [SerializeField] private int count = 10;
    [SerializeField] private float _rangeOffcet = 1;

    private Sequence _sequence;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out BouncingObject _))
        {
            PlayAnimation();
        }
    }

    [ContextMenu("PlayAnimation")]
    private void PlayAnimation()
    {
        var currentPositin = _shield.localPosition;

        _sequence = DOTween.Sequence();
        _sequence.SetUpdate(UpdateType.Late);
        for (int i = 0; i < count - 1; i++)
            _sequence.Append(_shield.DOLocalMove(currentPositin + RandOffcet(_rangeOffcet), _time));
        _sequence.Append(_shield.DOLocalMove(currentPositin, _time));
        
    }

    private Vector3 RandOffcet(float range) => new Vector3(Random.Range(-range, range)/2, Random.Range(-range, range),
        Random.Range(-range, range));
}