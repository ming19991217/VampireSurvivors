using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace MacArthur
{
    public class BuildController : MonoBehaviour
    {

        [SerializeField]
        UI_BuildingSelector buildSelector;

        [SerializeField]
        Camera buildCamera;

        [SerializeField]
        List<Building> buildingList;

        [SerializeField]
        Material previewMaterial;


        const string BUILDING_LAYER = "Buildable";
        Vector3 cursorTargetPos;
        Building cursor;

        int budget = 1000;



        private void Awake()
        {
            Init();
        }

        void Init()
        {
            buildSelector.Init(SelectBuilding, buildingList.Select(x => new BuildData
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

                Building building = Instantiate(cursor, cursorTargetPos, Quaternion.identity);
                building.Init(EmbedType.Building);
            }
        }





        void SelectBuilding(int type)
        {
            var building = buildingList.FirstOrDefault(x => x.Type == type);
            if (building == null)
            {
                Debug.LogError("Building not found");
                return;
            }

            if (cursor != null)
            {
                Destroy(cursor.gameObject);
            }

            cursor = Instantiate(building);
            cursor.Init(EmbedType.Preview);
        }



    }
}