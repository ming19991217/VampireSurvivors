using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Linq;

public class SearchWindowProvider : ScriptableObject, ISearchWindowProvider
{
	List<Type> nodeTypes;
	Action<Vector2, Type> onSelectEntry;

	public void Init(List<Type> nodeTypes, Action<Vector2, Type> onSelectEntry)
	{
		this.nodeTypes = nodeTypes;
		this.onSelectEntry = onSelectEntry;
	}



	//创建搜索窗口
	public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
	{
		var entries = new List<SearchTreeEntry>();
		entries.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));

		nodeTypes.ForEach(type =>
		{
			entries.Add(new SearchTreeEntry(new GUIContent(type.Name)) { level = 1, userData = type });
		});

		return entries;
	}

	//當选择搜索窗口中的节点
	public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
	{
		var type = SearchTreeEntry.userData as System.Type;
		onSelectEntry(context.screenMousePosition, type);
		return true;
	}
}