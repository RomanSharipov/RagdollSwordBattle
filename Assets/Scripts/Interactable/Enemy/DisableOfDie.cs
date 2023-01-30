using UnityEngine;

public class DisableOfDie : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void OnEnable() => _health.Died += Hide;

    private void OnDisable() => _health.Died -= Hide;
    private void Hide() => gameObject.SetActive(false);
}