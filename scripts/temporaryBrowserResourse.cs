using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporaryBrowserResourse : MonoBehaviour
{
    
    public void unCheckResourse()
    {
        folderBrowseScript._folderBrowser.showCheckedResourses(name, false);
    }

}
