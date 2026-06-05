using System;
using UnityEngine;

public class Eventos : MonoBehaviour
{
    public static Eventos Instance;

    public event Action eventoPrueba;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        if (InputManager.Instance.IsMainFirePressed())
        {
            eventoPrueba?.Invoke();
        }
    }

    private void Prueba()
    {

    }
}
