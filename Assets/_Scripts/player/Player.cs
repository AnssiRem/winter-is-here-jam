using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private void Start()
        {
            PlayerMovement.Initialize(transform, GetComponent<Collider>());
            PlayerInteract.Initialize(transform, GetComponent<Collider>());
        }


        private void Update()
        {
            PlayerMovement.ManageInput();
            PlayerInteract.ManageInput();
        }
    }
}
