using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public static class PlayerMovement
    {
        private static float m_rotationSpeed = 500f;

        private static Collider m_collider;
        private static Transform m_transform;
        private static Vector3 m_collExtents;
        private static bool m_rolling;

        private static Quaternion m_targetRotation;
        private static Vector3 m_rotationAxis;
        private static Vector3 m_rotationPoint;
        private static float m_rotatedAmount;
        public static bool rolling { get => m_rolling; }


        public static void Initialize(Transform transform, Collider collider)
        {
            m_transform = transform;
            m_collider = collider;

            m_collExtents = m_collider.bounds.extents;
        }


        public static void ManageInput()
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical"));

            if (rolling)
            {
                Roll();
            }
            else
            {
                SetRoll(moveInput);
            }
        }


        public static void Roll()
        {
            float rotateAmount = Time.deltaTime * m_rotationSpeed;
            m_rotatedAmount += rotateAmount;
            if (m_rotatedAmount > 90)
            {
                m_rolling = false;
                rotateAmount -= m_rotatedAmount - 90;

                GameObject.Find("Player").GetComponent<Player>().PlayPunch();
            }
            m_transform.RotateAround(m_rotationPoint, m_rotationAxis,
                rotateAmount);
        }


        public static void SetRoll(Vector2 moveInput)
        {
            if (rolling)
            {
                return;
            }

            //Right
            if (moveInput.x > 0)
            {
                m_rolling = true;
                m_rotatedAmount = 0;

                m_rotationPoint = m_transform.localPosition +
                    Vector3.right * m_collExtents.x -
                    Vector3.up * m_collExtents.y;
                m_rotationAxis = -Vector3.forward;

                Vector3 prevExtents = m_collExtents;
                m_collExtents.x = prevExtents.y;
                m_collExtents.y = prevExtents.x;
                return;
            }
            //Left
            else if (moveInput.x < 0)
            {
                m_rolling = true;
                m_rotatedAmount = 0;

                m_rotationPoint = m_transform.localPosition -
                    Vector3.right * m_collExtents.x -
                    Vector3.up * m_collExtents.y;
                m_rotationAxis = Vector3.forward;

                Vector3 prevExtents = m_collExtents;
                m_collExtents.x = prevExtents.y;
                m_collExtents.y = prevExtents.x;
                return;
            }

            //Forward
            if (moveInput.y > 0)
            {
                m_rolling = true;
                m_rotatedAmount = 0;

                m_rotationPoint = m_transform.localPosition +
                    Vector3.forward * m_collExtents.z -
                    Vector3.up * m_collExtents.y;
                m_rotationAxis = Vector3.right;

                Vector3 prevExtents = m_collExtents;
                m_collExtents.y = prevExtents.z;
                m_collExtents.z = prevExtents.y;
                return;
            }
            //Backward
            else if (moveInput.y < 0)
            {
                m_rolling = true;
                m_rotatedAmount = 0;

                m_rotationPoint = m_transform.localPosition -
                    Vector3.forward * m_collExtents.z -
                    Vector3.up * m_collExtents.y;
                m_rotationAxis = -Vector3.right;

                Vector3 prevExtents = m_collExtents;
                m_collExtents.y = prevExtents.z;
                m_collExtents.z = prevExtents.y;
                return;
            }
        }
    }
}
