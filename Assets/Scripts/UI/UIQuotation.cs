using UnityEngine;

public class UIQuotation : MonoBehaviour
{
    private HostageHealth _hostageHealth;
    private Camera _camera;

    private void OnEnable()
    {
        _camera = Camera.main;
    }
    public void Init(HostageHealth hostageHealth)
    {
        _hostageHealth = hostageHealth;
    }

    private void Update()
    {
        transform.position = _camera.WorldToScreenPoint(_hostageHealth.transform.position);
    }
}
