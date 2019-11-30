using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public static class PlayerInteract
    {
        private static Collider m_collider;
        private static Transform m_transform;
        private static GameManager m_gm;


        public static void Initialize(Transform transform, Collider collider)
        {
            m_transform = transform;
            m_collider = collider;

            GameObject managerObject = GameObject.Find("Game Manager Object");
            m_gm = managerObject.GetComponent<GameManager>();
        }


        public static void ManageInput()
        {
            bool input = Input.GetButtonDown("Jump");

            if (!input)
            {
                return;
            }

            if (PlayerMovement.rolling)
            {
                return;
            }
            else
            {
                Ray ray = new Ray(m_transform.position, m_transform.forward);
                Debug.DrawRay(m_transform.position, m_transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1f))
                {
                    m_gm.Interact(hit.collider.name);
                }
            }
        }
    }
}
