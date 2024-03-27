using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class GameLoopEditorView : GraphView
{

	EditorWindow editorWindow;
	RootNode root;






	public GameLoopEditorView(EditorWindow editorWindow) : base()
	{
		this.editorWindow = editorWindow;

		//添加背景
		AddGridBackground();

		//添加拖拽
		BindManipulator();


		//添加搜索窗口
		var searchWindowProvider = ScriptableObject.CreateInstance<SearchWindowProvider>();
		searchWindowProvider.Init(new()
		{
			 typeof(ActionNode),
			 typeof(ConditionNode)
		}, (pos, type) =>
		{
			var node = CreateNode(type, GetLocalMousePosition(pos, true));
			AddElement(node);
		});

		//添加节点新增事件
		nodeCreationRequest += context =>
		{
			SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindowProvider);
		};

		root = new RootNode();
		AddElement(root);


		void BindManipulator()
		{
			//设置缩放
			SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);


			//使GraphView支持拖拽
			this.AddManipulator(new SelectionDragger());
			this.AddManipulator(new ContentDragger());
			// this.AddManipulator(new RectangleSelector());

			this.AddManipulator(CreateNodeContextualMenu("Add Node (Single Choice)", typeof(ActionNode)));

			IManipulator CreateNodeContextualMenu(string actionTitle, Type type)
			{
				ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
					menuEvent => menuEvent.menu.AppendAction(actionTitle, actionEvent => AddElement(CreateNode(type, GetLocalMousePosition(actionEvent.eventInfo.localMousePosition))))
				);

				return contextualMenuManipulator;
			}

		}
	}

	Node CreateNode(Type type, Vector2 position)
	{
		var node = (Node)System.Activator.CreateInstance(type);
		node.SetPosition(new Rect(position, Vector2.zero));
		return node;
	}

	public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
	{
		var compatiblePorts = ports
		.Where(port =>
		(port.direction != startPort.direction) &&
		(port.node != startPort.node) &&
		(port.portType == startPort.portType))
		.ToList();

		return compatiblePorts;
	}

	void AddGridBackground()
	{
		GridBackground gridBackground = new GridBackground();

		gridBackground.StretchToParentSize();

		Insert(0, gridBackground);
	}

	public async void Execute()
	{
		var rootEdge = root.OutputPort.connections.FirstOrDefault();
		if (rootEdge == null)
			return;

		var currentNode = rootEdge.input.node as IExecutable;

		while (true)
		{
			if (currentNode == null)
				break;

			await currentNode.Execute();

			var edge = (currentNode as IHasOutput)?.OutputPort.connections.FirstOrDefault();

			if (edge == null)
				break;

			currentNode = edge.input.node as IExecutable;
		}
	}
	Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false)
	{
		Vector2 worldMousePosition = mousePosition;

		if (isSearchWindow)
		{
			worldMousePosition = editorWindow.rootVisualElement.ChangeCoordinatesTo(editorWindow.rootVisualElement.parent, mousePosition - editorWindow.position.position);
		}

		Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);

		return localMousePosition;
	}


}