using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    private bool m_isOn;
    [SerializeField] private GameObject m_screen;

    public bool isOn { get => m_isOn; }

    public void SwitchOn()
    {
        m_screen.SetActive(true);
        m_isOn = true;
    }


    public void SwitchOff()
    {
        m_screen.SetActive(false);
        m_isOn = false;
    }
}
