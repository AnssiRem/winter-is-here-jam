using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    private float m_fdUpness;
    private Bloom m_bloom;
    private Grain m_grain;
    private Vignette m_vignette;

    [SerializeField] private float m_fadeSpeed = 1f;
    [SerializeField] private Text m_text;
    [SerializeField] private PostProcessVolume m_postProcess;

    void Start()
    {
        m_postProcess.profile.TryGetSettings(out m_bloom);
        m_postProcess.profile.TryGetSettings(out m_grain);
        m_postProcess.profile.TryGetSettings(out m_vignette);

        ShowText("WAKE UP");
    }


    void Update()
    {
        m_text.color = Color.Lerp(m_text.color, new Color(0, 0, 0, 0),
            Time.deltaTime * m_fadeSpeed);

        m_fdUpness += Time.deltaTime * 0.0001f;

        m_bloom.intensity.value += m_fdUpness;
        m_grain.intensity.value += m_fdUpness;
        m_vignette.intensity.value += m_fdUpness;
    }


    void ShowText(string text)
    {
        m_text.text = text;
        m_text.color = Color.white;
    }
}
