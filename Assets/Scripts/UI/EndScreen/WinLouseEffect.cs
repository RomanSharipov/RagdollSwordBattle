using System;
using System.Collections;
using RootMotion.Demos;
using UnityEngine;

public class WinLouseEffect : MonoBehaviour
{
    [Header("Win")]
    [SerializeField] private GameObject _winPanel;

    [SerializeField] private Vector3 _offcet = new Vector3(0,0,22);
    [SerializeField] private GameObject _winEffect;
    [Header("Fail")]
    [SerializeField] private GameObject _failPanel;

    [SerializeField] private float _time = 0.5f;


    public void ShowWinUI()
    {
        _winPanel.SetActive(true);
        _winEffect.transform.position = Camera.main.transform.position + _offcet;
        _winEffect.SetActive(true);
    }

    public void ShowFailUI() => StartCoroutine(WaitAndUse(() => _failPanel.SetActive(true), _time));

    private IEnumerator WaitAndUse(Action action,float time)
    {
        yield return new WaitForSecondsRealtime(time);
        action();
    }
}
