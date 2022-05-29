using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceInfoPanelDisplayer : MonoBehaviour
{
    [SerializeField] Die die;
    DiceInfoPanel infoPanel;
    private void Start()
    {
        infoPanel = FindObjectOfType<DiceInfoPanel>();

        if (infoPanel == null)
            Destroy(this);
        if (die == null)
            Destroy(this);
    }
    private void OnMouseOver()
    {
        DiceInfoPanel.displayerHovered = this;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            infoPanel.Show(die);
        }
    }
    private void OnMouseExit()
    {
        DiceInfoPanel.displayerHovered = null;
    }
}
