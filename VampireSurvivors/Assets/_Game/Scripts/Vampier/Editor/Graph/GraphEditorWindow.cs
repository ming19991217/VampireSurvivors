using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Unity.VisualScripting;

public class GraphEditorWindow : EditorWindow
{
    [MenuItem("Window/Graph Editor")]
    private static void OpenWindow()
    {
        GraphEditorWindow window = GetWindow<GraphEditorWindow>("Graph Editor");
    }

    void OnEnable()
    {
        AddToolbar();
        AddGraphView();
    }

    void AddGraphView()
    {
        var graphView = new GameLoopEditorView(this)
        {
            name = "GameLoop Graph Editor",
            style =
            {
                flexGrow = 1
            }
        };

        rootVisualElement.Add(graphView);

        rootVisualElement.Add(new Button(graphView.Execute) { text = "Execute" });
    }

    void AddToolbar()
    {
        var toolbar = new Toolbar();

        var saveButton = new ToolbarButton() { style = { borderRightWidth = 0 } };
        saveButton.text = "Save";
        saveButton.clickable.clicked += () => Save();
        toolbar.Add(saveButton);

        var loadButton = new ToolbarButton() { };
        loadButton.text = "Load";
        loadButton.clickable.clicked += () => Load();
        toolbar.Add(loadButton);

        rootVisualElement.Add(toolbar);
    }

    void Save()
    {

    }

    void Load()
    {

    }



}