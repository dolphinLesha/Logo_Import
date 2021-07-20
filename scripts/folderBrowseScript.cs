using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using System.Management;
using System;
using System.Linq;

public class folderBrowseScript : MonoBehaviour
{
    public static folderBrowseScript _folderBrowser;
    public GameObject folder, image;
    public GameObject content;
    public Image mainFolderBrowse;
    public TextMeshProUGUI errorMessage;
    public Image confirmImages;
    RectTransform contentSize;
    Vector2 screenSize;

    void Start()
    {
        currentPath = "";
        screenSize = new Vector2(Screen.width / can.scaleFactor, Screen.height / can.scaleFactor);
        xToTempBrowser = (tempResourses.rectTransform.anchorMax.x -
            tempResourses.rectTransform.anchorMin.x) * screenSize.x/2f;
        xTempBrowser = xToTempBrowser;
        xToMainBrowser = xToTempBrowser;

        contentSize = content.GetComponent<RectTransform>();
        _folderBrowser = this;
        openFiles(false);
        
    }


    List<int> ammountOfImagesInFolder = new List<int>();
    List<GameObject> folders = new List<GameObject>();
    List<GameObject> images = new List<GameObject>();
    List<string> hashChecked = new List<string>();
    bool isEndedCount = false;
    bool isFirstEnded = true;
    string lastPath = "";
    public string currentPath="";


    

    public void openFiles(bool isOpen)
    {
        if(isOpen)
        {
            if (currentPath == "" || currentPath == "drivers")
                showDrives();
            else
                openFolder(currentPath);
        }
        confirmImages.gameObject.SetActive(isOpen);
        if (hashChecked.Count==0 || !isOpen)
            tempResourses.gameObject.SetActive(false);
        else
            tempResourses.gameObject.SetActive(true);
        mainFolderBrowse.gameObject.SetActive(isOpen);
        //menubar._menubar.wasChosen(-1);
        if (isOpen)
        {
            settingsScript._settingsScript.showSettings(false);
            workPlaceScript._workPlaceScript.showWork(false);
            folderBrowserLogo._folderBrowserLogo.openFiles(false);
            saveImageDialog._saveImageDialog.openFiles(false);
            menubar._menubar.wasChosen(0);
        }
        
    }

    public void confirmIm()
    {
        if(hashChecked.Count==0)
        {
            errorMessage.text = "внимание, не было выбрано ни одно изображение";
            errorMessage.gameObject.SetActive(true);
            return;
        }
        buts[0].interactable = true;
        //buts[1].interactable = true;
        errorMessage.gameObject.SetActive(false);
        openFiles(false);
        workPlaceScript._workPlaceScript.showWorkPlace(hashChecked,true);
    }

    void refresh()
    {
        for (int i = 0; i < folders.Count; i++)
        {
            Destroy(folders[i].gameObject);
        }
        for (int i = 0; i < images.Count; i++)
        {
            Destroy(images[i].gameObject);
        }
        ammountOfImagesInFolder = new List<int>();
        folders = new List<GameObject>();
        images = new List<GameObject>();
        isEndedCount = false;
        isFirstEnded = true;
    }

    void resizeContent(int am, int widthAmmount,float heightRes,float spacing,RectTransform content)
    {
        int mnoj;
        float t = (float)am / widthAmmount;
        if (t == (int)t)
            mnoj = (int)t;
        else
            mnoj = (int)t+1;
        content.sizeDelta = new Vector2(content.sizeDelta.x, mnoj * heightRes+(mnoj-1)*spacing);
    }

    public void showDrives()
    {
        currentPath = "drivers";
        DriveInfo[] drives = DriveInfo.GetDrives();
        refresh();
        List<string> names = new List<string>();
        for (int i=0;i<drives.Length;i++)
        {
            
            GameObject temp = Instantiate(folder, content.transform);
            temp.name = drives[i].Name;
            names.Add(temp.name);

            temp.GetComponentsInChildren<TextMeshProUGUI>()[1].text = drives[i].Name;
            folders.Add(temp);
        }
        resizeContent(folders.Count,5,80f,5f,contentSize);
        if (names.Count != 0)
            retCountOfIm(names);
    }
    public Sprite[] formatsImages;
    public void openFolder(string path)
    {
        firstIndex = -1;
        currentPath = path;
        string[] tempe = currentPath.Split((char)(92));
        lastPath = "";
        for(int i=0;i<tempe.Length-2;i++)
        {
            lastPath += tempe[i] + (char)(92); 
        }
        refresh();
        DirectoryInfo info=new DirectoryInfo("temp");
        try
        {
            info = new DirectoryInfo(path);
        }
        catch
        {

        }
        DirectoryInfo[] directories = info.GetDirectories();
        FileInfo[] filesPng = info.GetFiles("*.png");
        FileInfo[] filesJpg = info.GetFiles("*.jpg");
        FileInfo[] filesBmp = info.GetFiles("*.tiff");
        int ammountOfDirrectories= directories.Length;
        List<string> names = new List<string>();
        for(int i=0;i< ammountOfDirrectories; i++)
        {
            if (directories[i].Name == "$Recycle.Bin")
            {
                i++;
                ammountOfDirrectories--;
            }
                
            GameObject temp = Instantiate(folder, content.transform);
            temp.name = directories[i].FullName + (char)(92);
            names.Add(temp.name);

            temp.GetComponentsInChildren<TextMeshProUGUI>()[1].text= directories[i].Name;
            folders.Add(temp);
        }
        for (int i = 0; i < filesPng.Length; i++)
        {
            GameObject temp = Instantiate(image, content.transform);
            temp.name = filesPng[i].FullName;
            if (hashChecked.Contains(filesPng[i].FullName))
                temp.GetComponent<imageChosen>().wasChosen();
            temp.GetComponentsInChildren<Image>()[1].sprite = formatsImages[0];
            temp.GetComponentsInChildren<TextMeshProUGUI>()[0].text = filesPng[i].Name;
            temp.GetComponent<imageChosen>().index=images.Count;
            images.Add(temp);
        }
        for (int i = 0; i < filesJpg.Length; i++)
        {
            GameObject temp = Instantiate(image, content.transform);
            temp.name = filesJpg[i].FullName;
            if (hashChecked.Contains(filesJpg[i].FullName))
                temp.GetComponent<imageChosen>().wasChosen();
            temp.GetComponentsInChildren<Image>()[1].sprite = formatsImages[1];
            temp.GetComponentsInChildren<TextMeshProUGUI>()[0].text = filesJpg[i].Name;
            temp.GetComponent<imageChosen>().index = images.Count;
            images.Add(temp);
        }
        for (int i = 0; i < filesBmp.Length; i++)
        {
            GameObject temp = Instantiate(image, content.transform);
            temp.name = filesBmp[i].FullName;
            if (hashChecked.Contains(filesPng[i].FullName))
                temp.GetComponent<imageChosen>().wasChosen();
            temp.GetComponentsInChildren<Image>()[1].sprite = formatsImages[3];
            temp.GetComponentsInChildren<TextMeshProUGUI>()[0].text = filesBmp[i].Name;
            temp.GetComponent<imageChosen>().index = images.Count;
            images.Add(temp);
        }
        resizeContent(folders.Count+images.Count, 5, 80f, 5f, contentSize);
        if (names.Count!=0)
            retCountOfIm(names);
    }

    public int firstIndex=-1;
    public void choseSeveral(int index)
    {
        if(index>firstIndex)
            for(int i=firstIndex+1;i<index;i++)
            {
                images[i].GetComponent<imageChosen>().pick();
            }
        else
            for (int i = firstIndex-1; i > index; i--)
            {
                images[i].GetComponent<imageChosen>().pick();
            }
        
    }
    
    public void returnDialog()
    {
        if (lastPath != "")
            openFolder(lastPath);
        else
            showDrives();
    }
    async void retCountOfIm(List<string> directory)
    {
        for(int i=0;i<directory.Count-1;i++)
        {
            await Task.Run(() => countimages(directory[i],false));
        }
        await Task.Run(() => countimages(directory[directory.Count - 1], true));
    }

    void countimages(string directory,bool end)
    {
        
        try
        {
            DirectoryInfo info = new DirectoryInfo(directory);
            FileInfo[] filesPng = info.GetFiles("*.png");
            FileInfo[] filesJpg = info.GetFiles("*.jpg");
            FileInfo[] filesBmp = info.GetFiles("*.bmp");
            ammountOfImagesInFolder.Add(filesPng.Length + filesJpg.Length + filesBmp.Length);
            if (end)
                isEndedCount = true;
        }
        catch
        {
            ammountOfImagesInFolder.Add(0);
            if (end)
                isEndedCount = true;
        }
        
    }
    void showResultsOfCount()
    {
        for(int i=0;i<folders.Count;i++)
        {
            if(ammountOfImagesInFolder[i]!=0)
            {
                folders[i].gameObject.GetComponentsInChildren<TextMeshProUGUI>()[0].text = 
                    ammountOfImagesInFolder[i].ToString();
            }
            else
            {
                folders[i].gameObject.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "";
            }
        }
    }

    public Image shImageContent, shImageIm;
    public Canvas can;
    public void showImage(string path)
    {
        shImageContent.gameObject.SetActive(true);
        WWW www = new WWW(path);
        Texture2D texture = www.texture;
        float koef = (float)texture.width / (float)texture.height;

        Vector2 sizeParent = new Vector2((shImageIm.rectTransform.parent.GetComponent<RectTransform>().anchorMax.x
            - shImageIm.rectTransform.parent.GetComponent<RectTransform>().anchorMin.x) 
            * screenSize.x, (shImageIm.rectTransform.parent.GetComponent<RectTransform>().anchorMax.y
            - shImageIm.rectTransform.parent.GetComponent<RectTransform>().anchorMin.y)
            * screenSize.y);
        float koef2 = sizeParent.x / sizeParent.y;
        Vector2 siz;
        if(koef>koef2)
        {
            siz.x = sizeParent.x - 20f;
            siz.y = siz.x / koef;
        }
        else
        {
            siz.y = sizeParent.y - 20f;
            siz.x = siz.y * koef;
        }
        shImageIm.rectTransform.sizeDelta = siz;
        shImageIm.sprite = null;
        shImageIm.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0),100f);
    }
    public void hideImage()
    {
        shImageContent.gameObject.SetActive(false);
    }

    public GameObject resouse;
    public Image tempResourses;
    public RectTransform tempBrowserContent;
    List<GameObject> resourses = new List<GameObject>();
    bool isOnTempBrowser = false;
    bool isToTempBrowser = false;
    float xMainBrowser = 0f, xTempBrowser=100f,xToTempBrowser,xToMainBrowser;

    public void deleteAll()
    {
        for(int i=0;i<hashChecked.Count;)
        {
            showCheckedResourses(hashChecked[i], false);
        }
    }

    public void showCheckedResourses(string hash,bool wasChosen)
    {
        if(wasChosen)
        {
            hashChecked.Add(hash);
            if(hashChecked.Count!=0 && !isOnTempBrowser)
            {
                
                tempResourses.gameObject.SetActive(true);
                isToTempBrowser = true;
            }
            
            GameObject temp = Instantiate(resouse, tempBrowserContent);
            temp.name = hash;
            string[] slashDevide = hash.Split((char)(92));
            string resultName = slashDevide[slashDevide.Length-1];
            if(resultName.Substring(resultName.LastIndexOf('.')+1)=="png")
            {
                temp.GetComponentsInChildren<Image>()[0].sprite = formatsImages[0];
            }
            else if(resultName.Substring(resultName.LastIndexOf('.')+1) == "jpg")
            {
                temp.GetComponentsInChildren<Image>()[0].sprite = formatsImages[1];
            }
            else if(resultName.Substring(resultName.LastIndexOf('.')+1) == "tiff")
            {
                temp.GetComponentsInChildren<Image>()[0].sprite = formatsImages[2];
            }
            resultName = resultName.Substring(0, resultName.LastIndexOf('.'));
            temp.GetComponentInChildren<TextMeshProUGUI>().text = resultName;
            resourses.Add(temp);
        }
        else
        {
            hashChecked.Remove(hash);
            if(hashChecked.Count==0)
            {
                isToTempBrowser = true;
                buts[0].interactable = false;
                buts[1].interactable = false;
            }
            for(int i=0;i<resourses.Count;i++)
            {
                if(resourses[i].name==hash)
                {
                    Destroy(resourses[i].gameObject);
                    resourses.Remove(resourses[i]);
                    break;
                }
            }
            for (int i = 0; i < images.Count; i++)
            {
                if (images[i].name == hash)
                {
                    images[i].GetComponent<imageChosen>().wasUnChosen();
                    break;
                }
            }
        }
        resizeContent(hashChecked.Count, 1, 33.06f, 5f, tempBrowserContent);
    }
    bool isFirstBlock = true;
    public Button[] buts;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && Input.GetKey(KeyCode.LeftControl))
            openFiles(true);
        if(isEndedCount&&isFirstEnded)
        {
            isFirstEnded = false;
            showResultsOfCount();
        }
        if(isToTempBrowser)
        {
            if(!isOnTempBrowser)
            {
                xMainBrowser = Mathf.MoveTowards(xMainBrowser, xToMainBrowser, 600f * Time.deltaTime);
                xTempBrowser = Mathf.MoveTowards(xTempBrowser, 0, 600f * Time.deltaTime);
                if(xMainBrowser >= xToMainBrowser&& xTempBrowser<=0f)
                {
                    isToTempBrowser = false;
                    isOnTempBrowser = true;
                }
            }
            else
            {
                xMainBrowser = Mathf.MoveTowards(xMainBrowser, 0, 600f * Time.deltaTime);
                xTempBrowser = Mathf.MoveTowards(xTempBrowser, xToTempBrowser, 600f * Time.deltaTime);
                if (xMainBrowser <= 0 && xTempBrowser >= xToTempBrowser)
                {
                    tempResourses.gameObject.SetActive(false);
                    isToTempBrowser = false;
                    isOnTempBrowser = false;
                }
            }
            mainFolderBrowse.rectTransform.offsetMin = new Vector2(-xMainBrowser, 0);
            mainFolderBrowse.rectTransform.offsetMax = new Vector2(-xMainBrowser, 0);
            tempResourses.rectTransform.offsetMin = new Vector2(xTempBrowser, 0);
            tempResourses.rectTransform.offsetMax = new Vector2(-xTempBrowser, 0);
        }
        

    }
}
