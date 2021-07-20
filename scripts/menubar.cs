using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menubar : MonoBehaviour
{
    public static menubar _menubar;
    public Image[] isChoosen;
    Color trueCol, falseCol;
    void Start()
    {
        _menubar = this;
        trueCol = new Color(0.4165896f, 0.6792453f, 0.3812745f);
        falseCol = new Color(0.509434f, 0.509434f, 0.509434f);
    }

    public void wasChosen(int function)
    {
        for(int i=0;i<isChoosen.Length;i++)
        {
            isChoosen[i].color = falseCol;
        }
        try
        {
            isChoosen[function].color = trueCol;
        }   
        catch { }
        
    }

    
    void Update()
    {
        
    }
}
