using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private float time = 0;
    private float step1 = 2f;
    private float step2 = 4f;
    private float step3 = 8f;

    [SerializeField] private GameObject m_name;
    [SerializeField] private GameObject m_text;


    void Update()
    {
        time += Time.deltaTime;
        if(time >= step1 && time < step2)
        {
            m_name.SetActive(true);
        }
        else if(time >= step2 && time < step3)
        {
            m_name.SetActive(false);
            m_text.SetActive(true);
        }
        else if(time >= step3)
        {
            m_text.SetActive(false);
            SceneManager.LoadScene(1);
        }
    }
}
