using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Game3D
{
    public class GamePlayManager : MonoBehaviour
    {
        [SerializeField]
        CameraController cameraController;

        public void Awake()
        {
            cameraController.Init();
        }



        bool TryRaycastInteractivable(Camera camera, Vector2 vector, out GameObject hitObject)
        {
            Ray ray = camera.ScreenPointToRay(vector);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                hitObject = hit.collider.gameObject;
                return true;
            }
            else
            {
                hitObject = null;
                return false;
            }
        }

    }
}