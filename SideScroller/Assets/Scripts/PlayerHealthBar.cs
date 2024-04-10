using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField]
    private int _maxValue;

    [SerializeField]
    private Image _fill;

    private int _currentValue;

    private void Start()
    {
        _currentValue = _maxValue;
        _fill.fillAmount = 1;
    }

    public void Add(int i )
    {
        _currentValue += i;

        if(_currentValue > _maxValue)
        {
            _currentValue = _maxValue;
        }

        _fill.fillAmount = (float)_currentValue / _maxValue;
    }

    public void Deduct(int i)
    {
        _currentValue -= i;

        if (_currentValue < 0)
        {
            _currentValue = 0;
        }

        _fill.fillAmount = (float)_currentValue / _maxValue;
    }
}
