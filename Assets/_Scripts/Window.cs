﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Window
{
    public class Window : MonoBehaviour
    {
        private float m_timeMulti = 0.1f;
        private float m_rotateSpeed = 0.5f;

        [SerializeField] private Transform m_lightTransform;
        [SerializeField] private Light m_light;
        [SerializeField] private Material m_glass;


        private void Start()
        {
            DynamicGI.synchronousMode = true;
        }


        void Update()
        {
            Color newColor = new Color(Mathf.Sin(Time.time * m_timeMulti),
                Mathf.Cos(Time.time * m_timeMulti),
                -Mathf.Sin(Time.time * m_timeMulti));

            m_light.color = newColor;

            m_lightTransform.Rotate(Vector3.up,
                Time.deltaTime * m_rotateSpeed);

            //Intensity
            newColor *= 2f;
            m_glass.SetColor("_EmissionColor", newColor);
        }
    }
}