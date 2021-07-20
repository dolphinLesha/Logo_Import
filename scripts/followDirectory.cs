using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followDirectory : MonoBehaviour
{
    public int rejim = 0;
    public void changeDirectory()
    {
        if (rejim == 0)
            folderBrowseScript._folderBrowser.openFolder(name);
        else if (rejim == 1)
            folderBrowserLogo._folderBrowserLogo.openFolder(name);
        else if (rejim == 2)
            saveImageDialog._saveImageDialog.openFolder(name);
    }

    
    
}
