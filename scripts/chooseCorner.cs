using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chooseCorner : MonoBehaviour
{
    public int corner;
    ColorBlock col;
    bool isChosen = false;
    Button but;
    public Button[] anothers;

    void Start()
    {
        but = GetComponent<Button>();
        col = but.colors;
        col.selectedColor = new Color(col.normalColor.r,
                col.normalColor.g + 0.5f,
                col.normalColor.b);
        but.colors = col;
    }

    public void setCorner()
    {
        workPlaceScript._workPlaceScript.logoCorner = corner;
        if (!isChosen)
            col.normalColor = new Color(col.normalColor.r,
                col.normalColor.g+0.5f,
                col.normalColor.b);
        else
            col.normalColor = new Color(col.normalColor.r,
               col.normalColor.g-0.5f,
               col.normalColor.b);
        isChosen = !isChosen;
        but.colors = col;
        for (int i=0;i<anothers.Length;i++)
        {
            if (anothers[i].name != name)
                anothers[i].GetComponent<chooseCorner>().setUnchosen();
        }
    }
    public void setUnchosen()
    {
        if(isChosen)
        {
            col.normalColor = new Color(col.normalColor.r,
               col.normalColor.g - 0.5f,
               col.normalColor.b);
            but.colors = col;
            isChosen = false;
        }
    }
    
}
