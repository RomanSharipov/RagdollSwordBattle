using UnityEngine;
public class Panel : MonoBehaviour
{
    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    public void Invert() => gameObject.SetActive(!gameObject.activeSelf);
}
