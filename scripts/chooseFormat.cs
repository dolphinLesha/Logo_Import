using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chooseFormat : MonoBehaviour
{
    bool isChosen = false;
    Button but;
    public Button[] anothers;
    ColorBlock col;
    void Start()
    {
        but = GetComponent<Button>();
        col = but.colors;
        col.selectedColor = new Color(col.normalColor.r - 0.5f,
                col.normalColor.g,
                col.normalColor.b - 0.5f);
        but.colors = col;
    }
    public void setFormat()
    {
        saveImageDialog._saveImageDialog.format="." + name;
        if (!isChosen)
            col.normalColor = new Color(col.normalColor.r - 0.5f,
                col.normalColor.g ,
                col.normalColor.b - 0.5f);
        else
            col.normalColor = new Color(col.normalColor.r + 0.5f,
               col.normalColor.g,
               col.normalColor.b + 0.5f);
        isChosen = !isChosen;
        but.colors = col;
        for (int i = 0; i < anothers.Length; i++)
        {
            if (anothers[i].name != name)
                anothers[i].GetComponent<chooseFormat>().setUnchosen();
        }
    }
    public void setUnchosen()
    {
        if (isChosen)
        {
            col.normalColor = new Color(col.normalColor.r + 0.5f,
               col.normalColor.g,
               col.normalColor.b + 0.5f);
            but.colors = col;
            isChosen = false;
        }
    }


}
