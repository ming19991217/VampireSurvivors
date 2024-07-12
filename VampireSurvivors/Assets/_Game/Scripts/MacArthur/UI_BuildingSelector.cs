using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MacArthur
{

    public record BuildData
    {
        public int type;
        public string name;
        public int cost;
        public bool enable;
    }

    public class UI_BuildingSelector : MonoBehaviour
    {
        [SerializeField]
        UI_BuildingItem buildItemPrefab;

        [SerializeField]
        Transform buildItemContainer;
        UIList<UI_BuildingItem, BuildData> buildList;

        public void Init(Action<int> onClick, List<BuildData> data)
        {
            buildList = new(buildItemPrefab, buildItemContainer, (item, data, index) =>
            {
                item.gameObject.SetActive(true);
                item.Init(() => onClick(data.type), data.name, data.cost, data.enable);
            });

            buildList.Update(data);
        }

    }
}