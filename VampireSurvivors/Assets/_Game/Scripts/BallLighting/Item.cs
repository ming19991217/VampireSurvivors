using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace BallLightning
{
    public class Item : MonoBehaviour
    {

        [SerializeField]
        Renderer renderer;
        [SerializeField]
        Collider collider;

        bool enable;
        Action<bool> onEnableChanged;

        [SerializeField]
        Material original, hover;


        private void Start()
        {
            EnableItem(true);
        }

        public void EnableItem(bool isEnable)
        {
            enable = isEnable;
            collider.enabled = isEnable;
            onEnableChanged?.Invoke(isEnable);
        }

        public void SetPosAndRot(Vector3 pos, Quaternion rot)
        {
            transform.position = pos;
            transform.rotation = rot;
        }

        public void SetHover(bool isHover)
        {
            renderer.material = isHover ? hover : original;
        }

        public void RegisterOnEnableChanged(Action<bool> action)
        {
            onEnableChanged += action;
        }



    }

}