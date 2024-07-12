using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;



interface IUIList<TItem, TData> : IDisposable
{
    void Update(IList<TData> data);
}

internal class VirtaulUIList_v2<TItem, TData> : IUIList<TItem, TData>, IDisposable
{
    public record UIListConfigs
    {
        public Func<int, TItem> RequestItem { get; set; } = null;
        public Action<TItem> RecoveryItem { get; set; } = null;
        public Action<TItem, TData, int> OnUpdate { get; set; } = null;
        public Func<TData, TData, bool> EqualComparer { get; set; } = null;
    }

    readonly UIListConfigs configs;


    public VirtaulUIList_v2(UIListConfigs configs)
    {
        this.configs = configs;
    }

    IList<TData> prevData = null;
    List<TItem> items = new();

    public void Update(IList<TData> data)
    {
        int maxCount = Mathf.Max(data.Count, prevData?.Count ?? 0);

        List<TItem> newItems = new();

        for (int i = 0; i < maxCount; i++)
        {
            bool hasPrevData = prevData != null && prevData.Count > i;
            bool hasCurrentData = data.Count > i;

            if (hasPrevData == true && hasCurrentData == true && configs.EqualComparer(data[i], prevData[i]))
            {
                TItem item = items[i];
                newItems.Add(item);
                continue;
            }

            if (hasPrevData == true)
            {
                TItem prevItem = items[i];
                configs.RecoveryItem?.Invoke(prevItem);
            }

            if (hasCurrentData == true)
            {
                int index = i;
                TItem item = configs.RequestItem.Invoke(index);
                newItems.Add(item);

                configs.OnUpdate?.Invoke(item, data[index], index);
            }
        }

        prevData = data;
        items = newItems;
    }

    void Clear()
    {
        foreach (var item in items)
        {
            configs.RecoveryItem?.Invoke(item);
        }

        items.Clear();
    }

    public void Dispose()
    {
        Clear();
    }
}

internal class UIList<TItem, TData> : IUIList<TItem, TData> where TItem : MonoBehaviour
{

    readonly VirtaulUIList_v2<TItem, TData> virtaulUIList;

    readonly List<TItem> items = new();

    IList<TData> prevData = null;
    public UIList(TItem template, Transform root, Action<TItem, TData, int> onUpdate, Func<TData, TData, bool> equalComparer = null)
    {
        virtaulUIList = new(new()
        {
            RequestItem = (index) =>
            {
                if (items.Count > index)
                {
                    var item = items[index];
                    item.gameObject.SetActive(true);
                    return item;
                }
                else
                {
                    var item = UnityEngine.Object.Instantiate(template, root).GetComponent<TItem>();
                    items.Add(item);
                    return item;
                }
            },
            RecoveryItem = (item) =>
            {
                item.gameObject.SetActive(false);
            },
            OnUpdate = onUpdate,
            EqualComparer = equalComparer ?? ((a, b) => false),
        });
    }



    public void Update(IList<TData> data)
    {
        virtaulUIList.Update(data);
        prevData = data;
    }

    public void Refresh()
    {
        virtaulUIList.Update(prevData);
    }

    public void Dispose()
    {
        virtaulUIList.Dispose();

        for (int i = 0; i < items.Count; i++)
        {
            UnityEngine.Object.Destroy(items[i].gameObject);
        }

        items.Clear();
    }

}

