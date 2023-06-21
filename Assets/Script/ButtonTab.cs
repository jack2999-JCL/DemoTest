using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonTab : MonoBehaviour
{
    [SerializeField] private ESpawnUnits _typeMainUI;
    [SerializeField] private Button _btn;

    private Action<ESpawnUnits> _onClicked;
    public Action<ESpawnUnits> OnClicked { get => _onClicked; set => _onClicked = value; }

    private void Awake()
    {
        _btn.onClick.AddListener(() =>
        {
            _onClicked?.Invoke(_typeMainUI);
        });
    }
}
