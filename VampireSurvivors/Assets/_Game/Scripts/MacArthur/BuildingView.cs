using System;
using UnityEngine;


namespace MacArthur
{




    public class BuildingView : MonoBehaviour
    {
        [SerializeField]
        int type;


        public int Type => type;
        public string Id { get; private set; }

        [SerializeField]
        Collider gridCollider;

        [SerializeField]
        Renderer buildingRenderer;

        [SerializeField]
        Material normalMaterial, previewMaterial;

        [SerializeField]
        Canvas hpBarCanvas;

        [SerializeField]
        UnityEngine.UI.Image hpBar;

        public void Init(string id, EmbedType embedType)
        {
            Id = id;

            hpBarCanvas.enabled = false;

            Action initAction = embedType switch
            {
                EmbedType.Building => BuildInit,
                EmbedType.Preview => PreviewInit,
                _ => throw new ArgumentException($"Unhandled embedType value: {embedType}"),
            };
            initAction();

            void PreviewInit()
            {
                gridCollider.enabled = false;
                buildingRenderer.material = previewMaterial;
            }

            void BuildInit()
            {
                gridCollider.enabled = true;
                buildingRenderer.material = normalMaterial;
            }
        }

        public void UdpateHpDisplay(int hp)
        {
            hpBar.fillAmount = (float)hp / 100;

            if (hp == 100)
                hpBarCanvas.enabled = false;
        }


    }
}