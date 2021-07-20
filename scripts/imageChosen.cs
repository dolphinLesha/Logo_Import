using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageChosen : MonoBehaviour
{
    ColorBlock col;
    bool isChosen=false;
    Button but;
    public int index;
    void Start()
    {
        but = GetComponent<Button>();
        col = but.colors;
        col.selectedColor = new Color(col.normalColor.r,
                col.normalColor.g,
                col.normalColor.b,0.5f);
        but.colors = col;
    }

    public void chosen()
    {
        if(Input.GetKey(KeyCode.V))
        {
            folderBrowseScript._folderBrowser.showImage(name);
            return;
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(folderBrowseScript._folderBrowser.firstIndex!=-1)
            {
                folderBrowseScript._folderBrowser.choseSeveral(index);
            }
        }
        folderBrowseScript._folderBrowser.firstIndex = index;
        if (!isChosen)
            col.normalColor = new Color(col.normalColor.r,
                col.normalColor.g,
                col.normalColor.b, 0.5f);
        else
            col.normalColor = new Color(col.normalColor.r,
               col.normalColor.g,
               col.normalColor.b, 0);
        isChosen = !isChosen;
        but.colors = col;
        folderBrowseScript._folderBrowser.showCheckedResourses(name, isChosen);
    }

    public void wasChosen()
    {
        col.normalColor = new Color(col.normalColor.r,
                col.normalColor.g,
                col.normalColor.b, 0.5f);
        isChosen = true;
        try
        {
            but.colors = col;
        }
        catch
        {
            but = GetComponent<Button>();
            col = but.colors;
            col.normalColor = new Color(col.normalColor.r,
                col.normalColor.g,
                col.normalColor.b, 0.5f);
            but.colors = col;
        }
        
    }
    
    public void wasUnChosen()
    {
        col.normalColor = new Color(col.normalColor.r,
               col.normalColor.g,
               col.normalColor.b, 0);
        isChosen = false;
        but.colors = col;
    }

    public void pick()
    {
        if (!isChosen)
            col.normalColor = new Color(col.normalColor.r,
                col.normalColor.g,
                col.normalColor.b, 0.5f);
        else
            col.normalColor = new Color(col.normalColor.r,
               col.normalColor.g,
               col.normalColor.b, 0);
        isChosen = !isChosen;
        but.colors = col;
        folderBrowseScript._folderBrowser.showCheckedResourses(name, isChosen);
    }

    
}
