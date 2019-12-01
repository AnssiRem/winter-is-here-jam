using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private AudioSource m_audioSource;

        [SerializeField] private AudioClip[] m_audioClips;

        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();

            PlayerMovement.Initialize(transform, GetComponent<Collider>());
            PlayerInteract.Initialize(transform, GetComponent<Collider>());
        }


        private void Update()
        {
            PlayerMovement.ManageInput();
            PlayerInteract.ManageInput();
        }


        public void PlayPunch()
        {
            m_audioSource.pitch = Random.Range(0.7f, 1f);
            m_audioSource.PlayOneShot(
                m_audioClips[Random.Range((int)0, m_audioClips.Length)]);
        }
    }
}
