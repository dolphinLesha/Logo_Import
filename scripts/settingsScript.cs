using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingsScript : MonoBehaviour
{
    public static settingsScript _settingsScript;

    public Image mainField;

    void Start()
    {
        _settingsScript = this;
    }


    public void showSettings(bool isOpen)
    {
        mainField.gameObject.SetActive(isOpen);
        if(isOpen)
        {
            saveImageDialog._saveImageDialog.openFiles(false);
            workPlaceScript._workPlaceScript.showWork(false);
            folderBrowseScript._folderBrowser.openFiles(false);
            folderBrowserLogo._folderBrowserLogo.openFiles(false);
            menubar._menubar.wasChosen(3);
        }
        else
        {
            menubar._menubar.wasChosen(-1);
        }
        
    }

    
    
}
