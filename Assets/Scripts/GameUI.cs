using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
//
    [SerializeField]
    private GameObject restartButton;

    [Header("Katana Count Display")]
    [SerializeField]
    private GameObject panelKatanas;
    [SerializeField]
    private GameObject iconKatana;
    [SerializeField]
    private Color usedKatanaIconColor;

    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }

    public void SetInitialDisplayedKatanaCount(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(iconKatana, panelKatanas.transform);
        }
    }

    private int katanaIconIndexToChange = 0;

    public void DecrementDisplayKatanaCount()
    {
        panelKatanas.transform.GetChild(katanaIconIndexToChange++)
            .GetComponent<Image>().color = usedKatanaIconColor;
    }


}
