using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //shitccode
    private const int _maxHealth = 5;
    
    [SerializeField] private Image[] _healths;
    [SerializeField] private Image[] _locks;
    
    public void SetHealth(int current, int maxCurrent)
    {
        for (int i = 0; i < _maxHealth; i++)
        {
            var state = i <= current;

            _healths[i].enabled = state;
            _locks[i].enabled = !state;
        }
    }
}
