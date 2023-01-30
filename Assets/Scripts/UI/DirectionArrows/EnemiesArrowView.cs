using System.Collections;
using UnityEngine;

public class EnemiesArrowView : ArrowHandler<BotHealth>
{
    [SerializeField] private BotHealth[] _enemies;
    
    private Coroutine _drawArrowsCoroutine;

    private void OnDisable()
    {
        if (_drawArrowsCoroutine != null)
            StopCoroutine(_drawArrowsCoroutine);
    }

    private void Start()
    {
        Init(_enemies);
        _drawArrowsCoroutine = StartCoroutine(DrawArrows());
    }

    private IEnumerator DrawArrows()
    {
        var endOfFrame = new WaitForEndOfFrame();

        while (enabled)
        {
            UpdateArrowsPosition();

            yield return endOfFrame;
        }
    }
}
