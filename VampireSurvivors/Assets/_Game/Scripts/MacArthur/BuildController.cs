using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace MacArthur
{

    public record BuildingInfo
    {
        public string id;
        public HP hP;
        public BuildingView view;
    }

    public class BuildController : MonoBehaviour
    {

        [SerializeField]
        UI_BuildingSelector buildSelector;

        [SerializeField]
        Camera buildCamera;

        [SerializeField]
        List<BuildingView> viewGroup;

        [SerializeField]
        Material previewMaterial;


        const string BUILDING_LAYER = "Buildable";
        Vector3 cursorTargetPos;
        BuildingView cursor;
        int budget = 1000;
        List<BuildingInfo> buildings = new List<BuildingInfo>();



        private void Awake()
        {
            Init();
        }

        void Init()
        {
            buildSelector.Init(SelectBuilding, viewGroup.Select(x => new BuildData
            {
                type = x.Type,
                name = x.Type.ToString(),
                cost = 20,
                enable = budget >= 20
            }).ToList());
        }

        void Update()
        {
            if (cursor == null)
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool isHit = Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity);
            bool isBuildable = isHit && hitInfo.transform.gameObject.layer == LayerMask.NameToLayer(BUILDING_LAYER);

            Vector3 gridPos = Embedder.CursorPosToGridPos(hitInfo.point);
            cursorTargetPos = gridPos;
            cursor.transform.position = Vector3.Lerp(cursor.transform.position, cursorTargetPos, Time.deltaTime * 10f);

            if (isBuildable)
            {
                Debug.LogError("Buildable" + hitInfo.transform.gameObject.name);
                previewMaterial.color = Color.green;
                BuildCommand(gridPos);
            }
            else if (isHit && isBuildable == false)
            {
                previewMaterial.color = Color.red;
            }

            void BuildCommand(Vector3 gridPos)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) == false)
                    return;

                Build(cursor.Type, gridPos);
            }
        }


        void Build(int type, Vector3 position)
        {
            BuildingView prefab = viewGroup.FirstOrDefault(x => x.Type == type);

            if (prefab == null)
            {
                Debug.LogError("Building not found");
                return;
            }

            string id = Guid.NewGuid().ToString();

            BuildingView view = Instantiate(prefab, position, Quaternion.identity, transform);
            view.Init(id, EmbedType.Building);

            BuildingInfo buildingInfo = new BuildingInfo
            {
                id = id,
                hP = new HP(100, OnBuildingTakeDamage, OnBuildingDie),
                view = view
            };

            void OnBuildingTakeDamage(int hp)
            {
                view.UdpateHpDisplay(hp);
            }

            void OnBuildingDie()
            {
                Destroy(view.gameObject);
                var info = buildings.FirstOrDefault(x => x.id == id);
                buildings.Remove(info);
            }
        }



        void SelectBuilding(int type)
        {
            var building = viewGroup.FirstOrDefault(x => x.Type == type);
            if (building == null)
            {
                Debug.LogError("Building not found");
                return;
            }

            if (cursor != null)
            {
                Destroy(cursor.gameObject);
            }

            cursor = Instantiate(building, parent: transform);
            cursor.Init("cursor", EmbedType.Preview);
        }



    }
}