using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UIElements;

public class CustomEdge : Edge
{
	public string customProperty;


	public CustomEdge()
	{

		customProperty = "Init";

	}

	protected override EdgeControl CreateEdgeControl()
	{
		return new CustomEdgeControl()
		{

		};
	}

}

public class CustomEdgeControl : EdgeControl
{
	public CustomEdgeControl()
	{
		base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(OnGenerateVisualContent));
	}

	private void OnGenerateVisualContent(MeshGenerationContext mgc)
	{

	}

}