using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    private float m_hunger = 1f;
    private float m_sleep = 1f;
    private float m_sanity = 1f;

    private float m_fdUpness;
    private float m_time;
    private Bloom m_bloom;
    private Grain m_grain;
    private Vignette m_vignette;

    private float m_initBloom;
    private float m_initGrain;
    private float m_initVignette;

    private bool m_phoned;
    private bool m_knocked;

    [SerializeField] private float m_fadeSpeed = 2f;
    [SerializeField] private Text m_text;
    [SerializeField] private PostProcessVolume m_postProcess;
    [SerializeField] private GameObject m_hungerMeter;
    [SerializeField] private GameObject m_sleepMeter;
    [SerializeField] private GameObject m_sanityMeter;
    [SerializeField] private Phone m_phone;
    [SerializeField] private Door m_door;


    private void Start()
    {
        m_postProcess.profile.TryGetSettings(out m_bloom);
        m_postProcess.profile.TryGetSettings(out m_grain);
        m_postProcess.profile.TryGetSettings(out m_vignette);
        m_initBloom = m_bloom.intensity.value;
        m_initGrain = m_grain.intensity.value;
        m_initVignette = m_vignette.intensity.value;

        ShowText("WAKE UP");
    }


    private void Update()
    {
        m_time += Time.deltaTime;

        if (m_hungerMeter.activeSelf)
        {
            if (m_hunger > 0)
            {
                m_hunger -= Time.deltaTime * 0.01f;
            }

            Image fill;
            fill = m_hungerMeter.transform.Find("Fill").GetComponent<Image>();
            fill.fillAmount = m_hunger;
        }
        else if (m_time >= 10f)
        {
            m_hunger = 0.5f;
            Image fill;
            fill = m_hungerMeter.transform.Find("Fill").GetComponent<Image>();
            fill.fillAmount = m_hunger;

            m_hungerMeter.SetActive(true);
            ShowText("EAT");
        }
        if (m_sanityMeter.activeSelf)
        {
            if (m_sanity > 0)
            {
                m_sanity -= Time.deltaTime * 0.01f;
            }

            Image fill;
            fill = m_sanityMeter.transform.Find("Fill").GetComponent<Image>();
            fill.fillAmount = m_sanity;
        }
        if (m_sleepMeter.activeSelf)
        {
            if (m_sleep > 0)
            {
                m_sleep -= Time.deltaTime * 0.01f;
            }

            Image fill;
            fill = m_sleepMeter.transform.Find("Fill").GetComponent<Image>();
            fill.fillAmount = m_sleep;
        }

        m_text.color = Color.Lerp(m_text.color, new Color(0, 0, 0, 0),
            Time.deltaTime * m_fadeSpeed);

        float total = m_hunger + m_sanity + m_sleep;
        m_fdUpness = 1 - (total / 3);

        m_bloom.intensity.value = m_initBloom * (1 + m_fdUpness);
        m_grain.intensity.value = m_initGrain * (1 + m_fdUpness * 10);
        m_vignette.intensity.value = m_initVignette * (1 + m_fdUpness * 10);

        if(m_time >= 180 && !m_phoned)
        {
            m_phoned = true;
            m_phone.SwitchOn();
        }
        if(m_time >= 360 && !m_knocked)
        {
            m_knocked = true;
            m_door.SwitchOn();
        }

        if(m_phone.isOn || m_door.isOn)
        {
            m_sanity -= Time.deltaTime * 0.02f;
            m_hunger -= Time.deltaTime * 0.02f;
            m_sleep -= Time.deltaTime * 0.02f;
        }

        if(m_fdUpness >= 1)
        {
            Application.Quit();
        }
    }


    public void Interact(string objectName)
    {
        switch (objectName)
        {
            case "Computer":
                ShowText("LOGGED IN");
                m_sanity = 1f;
                m_hunger = Mathf.Clamp(m_hunger - 0.1f, 0, 1);
                m_sleep = Mathf.Clamp(m_sleep - 0.1f, 0, 1);

                if (!m_sleepMeter.activeSelf)
                {
                    m_sleep = 0.5f;
                    Image fill;
                    fill = m_sleepMeter.transform.Find("Fill").GetComponent<Image>();
                    fill.fillAmount = m_sleep;

                    m_sleepMeter.SetActive(true);
                    ShowText("SLEEP");
                }

                break;
            case "Bed":
                ShowText("SLEPT");
                m_sleep = 1f;
                m_hunger = Mathf.Clamp(m_hunger - 0.1f, 0, 1);
                m_sanity = Mathf.Clamp(m_sanity - 0.1f, 0, 1);

                break;
            case "Kitchen":
                ShowText("ATE");
                m_hunger = 1f;
                m_sanity = Mathf.Clamp(m_sanity - 0.1f, 0, 1);
                m_sleep = Mathf.Clamp(m_sleep - 0.1f, 0, 1);

                if (!m_sanityMeter.activeSelf)
                {
                    m_sanity = 0.5f;
                    Image fill;
                    fill = m_sanityMeter.transform.Find("Fill").GetComponent<Image>();
                    fill.fillAmount = m_sanity;

                    m_sanityMeter.SetActive(true);
                    ShowText("LOG IN");
                }

                break;
            case "Phone":
                if (m_phone.isOn)
                {
                    m_phone.SwitchOff();

                    ShowText("HUNG UP");
                    m_sanity = 1f;
                    m_hunger = Mathf.Clamp(m_hunger - 0.1f, 0, 1);
                    m_sleep = Mathf.Clamp(m_sleep - 0.1f, 0, 1);
                }

                break;
            case "Door":
                ShowText("...");
                Application.Quit();

                break;
            default:
                ShowText("HOW?");

                break;
        }
    }


    public void ShowText(string text)
    {
        m_text.text = text;
        m_text.color = Color.white;
    }
}
