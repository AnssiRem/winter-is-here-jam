using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 m_lerpPos;
    private Vector3 m_curPos;
    private Vector3 m_targetPos;

    [SerializeField] private float m_lerpSpeed = 0.1f;
    [SerializeField] private Transform m_target;


    void Start()
    {
        m_lerpPos = m_curPos = m_targetPos = m_target.position;
    }


    void Update()
    {
        m_curPos = Vector3.Lerp(m_curPos, m_target.position,
            m_lerpSpeed * Time.deltaTime);

        transform.LookAt(m_curPos);
    }
}
