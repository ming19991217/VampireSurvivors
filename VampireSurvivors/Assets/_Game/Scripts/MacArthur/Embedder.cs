
using UnityEngine;

namespace MacArthur
{

    public enum EmbedType
    {
        Building = 0,
        Preview = 1,
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