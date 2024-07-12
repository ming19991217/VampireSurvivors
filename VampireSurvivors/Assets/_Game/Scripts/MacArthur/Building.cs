using UnityEngine;


namespace MacArthur
{

    public class Building : MonoBehaviour
    {
        [SerializeField]
        int type;

        public int Type => type;

        [SerializeField]
        Collider gridCollider;

        [SerializeField]
        Renderer buildingRenderer;

        [SerializeField]
        Material normalMaterial, previewMaterial;


        public void Init(EmbedType embedType)
        {
            buildingRenderer.material = embedType switch
            {
                EmbedType.Building => normalMaterial,
                EmbedType.Preview => previewMaterial,
                _ => throw new System.NotImplementedException()
            };
        }


    }
}