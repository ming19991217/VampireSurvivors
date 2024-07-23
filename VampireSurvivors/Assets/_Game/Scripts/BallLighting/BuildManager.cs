using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallLightning
{

    public class BuildManager : MonoBehaviour
    {
        const string BUILDING_LAYER = "Buildable";
        const string GROUND_LAYER = "Ground";

        [SerializeField]
        Item cursor;
        Item hoverItem;
        Vector3 targetPos;
        Quaternion targetRot;


        private void Update()
        {
            if (cursor != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool isHit = Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, LayerMask.GetMask(GROUND_LAYER));
                if (isHit == false)
                    return;

                targetPos = Embedder.CursorPosToGridPos(hitInfo.point);
                if (Input.GetKeyDown(KeyCode.R))
                {
                    targetRot = Quaternion.Euler(0, targetRot.eulerAngles.y + 45, 0);
                }

                cursor.SetPosAndRot(targetPos, targetRot);

                if (Input.GetMouseButtonDown(0))
                {
                    cursor.EnableItem(true);
                    cursor = null;
                }
                return;
            }
            else
            {
                if (hoverItem != null)
                    hoverItem.SetHover(false);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool isHit = Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, LayerMask.GetMask(BUILDING_LAYER));
                if (isHit == false)
                    return;

                Item item = hitInfo.rigidbody.gameObject.GetComponent<Item>();



                if (Input.GetMouseButtonDown(0))
                {
                    cursor = item;
                    cursor.EnableItem(false);
                    targetRot = item.transform.rotation;
                }
                else
                {
                    hoverItem = item;
                    item.SetHover(true);
                }
            }




        }






    }

    public class Embedder
    {
        const float GRID_UNIT = 1f;
        public static Vector3 CursorPosToGridPos(Vector3 cursorPos)
        {
            Vector3 grid = cursorPos;

            float x = Mathf.Round(grid.x / GRID_UNIT) * GRID_UNIT;
            float y = Mathf.Round(grid.y / GRID_UNIT) * GRID_UNIT;
            float z = Mathf.Round(grid.z / GRID_UNIT) * GRID_UNIT;
            Vector3 gridPos = new Vector3(x, y, z);

            return gridPos;
        }
    }
}