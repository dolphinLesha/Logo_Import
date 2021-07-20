using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading.Tasks;
using TMPro;

public class saveImageDialog : MonoBehaviour
{
    public static saveImageDialog _saveImageDialog;
    public GameObject folder, image;
    public GameObject content;
    public Image mainFolderBrowse;
    //public Image confirmLogo;
    public TextMeshProUGUI errorMessage;
    RectTransform contentSize;
    Vector2 screenSize;

    void Start()
    {
        currentPath = "";
        screenSize = new Vector2(Screen.width / can.scaleFactor, Screen.height / can.scaleFactor);


        contentSize = content.GetComponent<RectTransform>();
        _saveImageDialog = this;
        openFiles(false);

    }


    List<int> ammountOfImagesInFolder = new List<int>();
    List<GameObject> folders = new List<GameObject>();
    List<GameObject> images = new List<GameObject>();
    //List<string> hashChecked = new List<string>();
    string hashLogo = "";
    bool isEndedCount = false;
    bool isFirstEnded = true;
    string lastPath = "";
    public string currentPath = "";

    public void openFiles(bool isOpen)
    {
        if (isOpen)
        {
            if (currentPath == "" || currentPath == "drivers")
                showDrives();
            else
                openFolder(currentPath);
        }
        //menubar._menubar.wasChosen(-1);
        mainFolderBrowse.gameObject.SetActive(isOpen);
        if (isOpen)
        {
            settingsScript._settingsScript.showSettings(false);
            workPlaceScript._workPlaceScript.showWork(false);
            folderBrowseScript._folderBrowser.openFiles(false);
            folderBrowserLogo._folderBrowserLogo.openFiles(false);
            menubar._menubar.wasChosen(2);
        }
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

    void resizeContent(int am, int widthAmmount, float heightRes, float spacing, RectTransform content)
    {
        int mnoj;
        float t = (float)am / widthAmmount;
        if (t == (int)t)
            mnoj = (int)t;
        else
            mnoj = (int)t + 1;
        content.sizeDelta = new Vector2(content.sizeDelta.x, mnoj * heightRes + (mnoj - 1) * spacing);
    }

    public void confirmIm()
    {
        if (hashLogo == "")
        {
            errorMessage.text = "внимание, не был выбран логотип";
            errorMessage.gameObject.SetActive(true);
            return;
        }
        mainFolderBrowse.gameObject.SetActive(false);
        errorMessage.gameObject.SetActive(false);
        openFiles(false);
        workPlaceScript._workPlaceScript.confirmLogo(hashLogo);
    }

    public void showDrives()
    {
        currentPath = "drivers";
        DriveInfo[] drives = DriveInfo.GetDrives();
        refresh();
        List<string> names = new List<string>();
        for (int i = 0; i < drives.Length; i++)
        {

            GameObject temp = Instantiate(folder, content.transform);
            temp.GetComponent<followDirectory>().rejim = 2;
            temp.name = drives[i].Name;
            names.Add(temp.name);

            temp.GetComponentsInChildren<TextMeshProUGUI>()[1].text = drives[i].Name;
            folders.Add(temp);
        }
        resizeContent(folders.Count, 5, 80f, 5f, contentSize);
        if (names.Count != 0)
            retCountOfIm(names);
    }
    public Sprite[] formatsImages;
    public void openFolder(string path)
    {

        currentPath = path;
        string[] tempe = currentPath.Split((char)(92));
        lastPath = "";
        for (int i = 0; i < tempe.Length - 2; i++)
        {
            lastPath += tempe[i] + (char)(92);
        }
        refresh();
        DirectoryInfo info = new DirectoryInfo("temp");
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
        FileInfo[] filesTiff = info.GetFiles("*.tiff");
        int ammountOfDirrectories = directories.Length;
        List<string> names = new List<string>();
        for (int i = 0; i < ammountOfDirrectories; i++)
        {
            if (directories[i].Name == "$Recycle.Bin")
            {
                i++;
                ammountOfDirrectories--;
            }

            GameObject temp = Instantiate(folder, content.transform);
            temp.name = directories[i].FullName + (char)(92);
            temp.GetComponent<followDirectory>().rejim = 2;
            names.Add(temp.name);

            temp.GetComponentsInChildren<TextMeshProUGUI>()[1].text = directories[i].Name;
            folders.Add(temp);
        }
        for (int i = 0; i < filesPng.Length; i++)
        {
            GameObject temp = Instantiate(image, content.transform);
            temp.name = filesPng[i].FullName;
            
            temp.GetComponentsInChildren<Image>()[1].sprite = formatsImages[0];
            temp.GetComponentsInChildren<TextMeshProUGUI>()[0].text = filesPng[i].Name;

            images.Add(temp);
        }
        for (int i = 0; i < filesJpg.Length; i++)
        {
            GameObject temp = Instantiate(image, content.transform);
            temp.name = filesJpg[i].FullName;
            
            temp.GetComponentsInChildren<Image>()[1].sprite = formatsImages[1];
            temp.GetComponentsInChildren<TextMeshProUGUI>()[0].text = filesJpg[i].Name;

            images.Add(temp);
        }
        for (int i = 0; i < filesTiff.Length; i++)
        {
            GameObject temp = Instantiate(image, content.transform);
            temp.name = filesTiff[i].FullName;
            
            temp.GetComponentsInChildren<Image>()[1].sprite = formatsImages[3];
            temp.GetComponentsInChildren<TextMeshProUGUI>()[0].text = filesTiff[i].Name;

            images.Add(temp);
        }
        resizeContent(folders.Count + images.Count, 5, 80f, 5f, contentSize);
        if (names.Count != 0)
            retCountOfIm(names);
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
        for (int i = 0; i < directory.Count - 1; i++)
        {
            await Task.Run(() => countimages(directory[i], false));
        }
        await Task.Run(() => countimages(directory[directory.Count - 1], true));
    }

    void countimages(string directory, bool end)
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
        for (int i = 0; i < folders.Count; i++)
        {
            if (ammountOfImagesInFolder[i] != 0)
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


    public string format = ".png";
    public InputField nameOfFolder;
    
    public void createFolder()
    {
        DirectoryInfo info = new DirectoryInfo(currentPath);
        if(nameOfFolder.text=="")
        {
            errorMessage.text = "вы не ввели имя каталога для создания";
            errorMessage.gameObject.SetActive(true);
            return;
        }
        
        try
        {
            info.CreateSubdirectory(nameOfFolder.text);
        }
        catch
        {
            errorMessage.text = "вы ввели неверное имя папки или не в недоступном месте";
            errorMessage.gameObject.SetActive(true);
            return;
        }
        errorMessage.gameObject.SetActive(false);
        openFiles(true);
    }
    List<Texture2D> texturs = new List<Texture2D>();
    List<string> namesTexture = new List<string>();
    public Button thisBut;
    public void getImages(List<Texture2D> textures,List<string> nam)
    {
        thisBut.interactable = true;
        texturs = textures;
        namesTexture = nam;
    }

    public void save()
    {
        StartCoroutine(savingProc());
        
    }

    IEnumerator savingProc()
    {
        errorMessage.gameObject.SetActive(false);
        
            for (int i = 0; i < texturs.Count; i++)
            {
                yield return StartCoroutine(saving(texturs[i], namesTexture[i], i, texturs.Count));
            }
        
        mainFolderBrowse.gameObject.SetActive(false);
        process.gameObject.SetActive(false);
        menubar._menubar.wasChosen(-1);
    }

    public Image process, bar;
    public TextMeshProUGUI nameOfProc;
    float procAmmount;

    IEnumerator saving(Texture2D textur,string name,int ind,int am)
    {
        process.gameObject.SetActive(true);
        procAmmount = (float)ind / (float)am;
        bar.fillAmount = procAmmount;
        nameOfProc.text = name;
        WWW www = new WWW("temp");
        yield return www;
        byte[] bytes=null;
        if (format == ".png")
            bytes = textur.EncodeToPNG();
        else if (format == "jpg")
            bytes = textur.EncodeToJPG();
        try
        {
            File.WriteAllBytes(currentPath + name + format, bytes);
        }
        catch
        {
            errorMessage.text = "выбранная папка не дает права на создание файлов";
            errorMessage.gameObject.SetActive(true);
        }
    }

    void Update()
    {

        if (isEndedCount && isFirstEnded)
        {
            isFirstEnded = false;
            showResultsOfCount();
        }

    }
}
