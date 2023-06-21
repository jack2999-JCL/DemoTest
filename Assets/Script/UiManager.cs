using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private List<ButtonTab> _buttonTabs;
    [SerializeField] private TextMeshProUGUI _textFPS;
    void Update()
    {
        float fps = 1.0f / Time.deltaTime;
        _textFPS.text = fps.ToString("0") + "  Fps "; 
    }
    private void OnEnable()
    {
        _buttonTabs.ForEach(btn => btn.OnClicked += ChangeTab);
    }
    private void OnDisable()
    {
        _buttonTabs.ForEach(btn => btn.OnClicked -= ChangeTab);
    }
    public void ChangeTab(ESpawnUnits data)
    {
        GameManager.Instance.CurrentTab = data;
    }
}
