
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;






public interface IExecutable
{
	Task Execute();
}

public interface IHasOutput
{
	Port OutputPort { get; }
}


public class OutputBase : Port
{


	protected OutputBase(Orientation portOrientation, Direction portDirection, Capacity portCapacity, Type type) : base(portOrientation, portDirection, portCapacity, type)
	{


	}


}

public class NodeBase : Node
{


	public NodeBase() : base()
	{
		Debug.Log("NodeBase");
		var titleLabel = this.Q<Label>("title-label");
		var editLabelAction = GraphEditorUtility.AddEditableLabel(titleLabel, titleContainer, (title) => this.title = title);

		titleContainer.RegisterCallback<MouseDownEvent>(evt =>
		{
			if (evt.clickCount == 2)
			{
				editLabelAction?.Invoke();
				evt.StopPropagation();
			}
		});

	}



}


public class RootNode : Node
{
	public Port OutputPort;

	public RootNode()
	{
		title = "Root";

		//移除删除功能
		capabilities -= Capabilities.Deletable;

		OutputPort = Port.Create<CustomEdge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
		OutputPort.portName = "Out";
		outputContainer.Add(OutputPort);

	}
}

public class ActionNode : NodeBase
{
	public Port InputPort;
	public Port OutputPort;


	public ActionNode() : base()
	{
		title = "Action";


		InputPort = Port.Create<CustomEdge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(Port));
		InputPort.portName = "In";
		inputContainer.Add(InputPort);


		OutputPort = Port.Create<CustomEdge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(Port));
		OutputPort.portName = "Out";
		outputContainer.Add(OutputPort);

	}


}

public class ConditionNode : NodeBase
{
	public Port InputPort;
	Dictionary<string, Port> outputPorts = new Dictionary<string, Port>();

	public ConditionNode() : base()
	{
		title = "Condition";

		InputPort = Port.Create<CustomEdge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
		InputPort.portName = "In";
		inputContainer.Add(InputPort);

		var btnRoot = new VisualElement() { style = { flexDirection = FlexDirection.Row } };
		btnRoot.Add(new Button(() =>
		{
			var port = Port.Create<CustomEdge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
			outputContainer.Add(port);
			port.portName = "Out";
			var portLabel = port.Q<Label>("type"); // 你可能需要根据你的实现调整选择器
			portLabel.pickingMode = PickingMode.Position; // 确保点击事件能够触发
			var action = GraphEditorUtility.AddEditableLabel(portLabel, port.contentContainer, (portName) => port.portName = portName, 1);

			portLabel.RegisterCallback<MouseDownEvent>(evt =>
			{
				action?.Invoke();
				evt.StopPropagation();
			});

		})
		{ text = "+" });

		btnRoot.Add(new Button(() =>
		{
			if (outputContainer.childCount > 1)
				outputContainer.RemoveAt(outputContainer.childCount - 1);
		})
		{ text = "-" });

		outputContainer.Add(btnRoot);
	}



}



