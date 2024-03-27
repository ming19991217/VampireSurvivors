
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphEditorUtility
{
	public static Action AddEditableLabel(Label label, VisualElement container, Action<string> onFinishEdit, int insertIndex = 0)
	{
		var field = new TextField
		{
			value = label.text,
			name = "label-editor"
		};
		field.style.minWidth = 50;

		field.RegisterCallback<FocusOutEvent>(evt =>
		{
			UpdateTitle();
		});

		field.RegisterCallback<KeyDownEvent>(evt =>
		{
			if (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter)
			{
				UpdateTitle();
			}
		});

		void UpdateTitle()
		{
			container.Remove(field);
			container.Insert(insertIndex, label);
			label.text = field.value;
			onFinishEdit?.Invoke(field.value);
		}

		return () =>
		{
			Debug.LogError("Invoke");
			container.Remove(label);
			container.Insert(insertIndex, field);
			field.value = label.text;
			field.Focus();
			field.SelectAll();
		};


	}
}