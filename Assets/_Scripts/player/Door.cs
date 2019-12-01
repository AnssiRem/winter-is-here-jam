using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    private bool m_isOn;

    public bool isOn { get => m_isOn; }


    public void SwitchOn()
    {
        m_isOn = true;
        GetComponent<AudioSource>().enabled = true;
    }
}
