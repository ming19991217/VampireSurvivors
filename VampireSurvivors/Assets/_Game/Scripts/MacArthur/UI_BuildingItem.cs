using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BuildingItem : MonoBehaviour
{
    [SerializeField]
    Button button;

    [SerializeField]
    TMP_Text nameText, costText;

    [SerializeField]
    GameObject mask;

    public void Init(System.Action onClick, string name, int cost, bool enable)
    {
        nameText.text = name;
        costText.text = cost.ToString();
        mask.SetActive(!enable);
        button.onClick.AddListener(() => onClick());
    }

}