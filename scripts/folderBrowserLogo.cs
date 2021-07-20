using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using System;
using System.Management;
using System.Text;
using Random = UnityEngine.Random;
//using System.Linq;

public class folderBrowserLogo : MonoBehaviour
{
    public static folderBrowserLogo _folderBrowserLogo;
    public GameObject folder, image;
    public GameObject content;
    public Image mainFolderBrowse;
    public Image confirmLogo;
    public TextMeshProUGUI errorMessage;
    RectTransform contentSize;
    Vector2 screenSize;

    void Start()
    {
        currentPath = "";
        screenSize = new Vector2(Screen.width / can.scaleFactor, Screen.height / can.scaleFactor);
        

        contentSize = content.GetComponent<RectTransform>();
        _folderBrowserLogo = this;
        hash_c();
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
    public string logo_le = "";

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
        confirmLogo.gameObject.SetActive(isOpen);
        mainFolderBrowse.gameObject.SetActive(isOpen);
        if (isOpen)
        {
            settingsScript._settingsScript.showSettings(false);
            workPlaceScript._workPlaceScript.showWork(false);
            folderBrowseScript._folderBrowser.openFiles(false);
            saveImageDialog._saveImageDialog.openFiles(false);
            //menubar._menubar.wasChosen(2);
        }
    }

    void hash_c()
    {
        //переменные для хранения итоговых путей
        string tea = "";
        string aet = "";
        string aas = "";
        //крайние элементы путей, которые мы соединим именем пользователя
        string test0 = "G>3Ywivw3", test1 = "3pskscmqtsvx3gsrjmkw3rsxezmvyw2gsrjmk", test2 = "3pskscmqtsvx3psksw3pskscwixxmrkw2gsrjmk";
        string ad = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        //расшифровываем имена
        for (int i = 0; i < test0.Length; i++)
        {
            tea += (char)((int)test0[i] - 4);
            
        }
        for (int i = 0;i< test1.Length;i++)
        {
            aet += (char)((int)test1[i] - 4);
        }
        for (int i = 0; i < test2.Length; i++)
        {
            aas += (char)((int)test2[i] - 4);
        }
        //получаем имя пользователя
        string username = Environment.UserName;
        //print(tea + username + aet);
        
        FileInfo ch_load; 
        FileInfo hc_load; 
        
        string mes;

        //ch_load = new FileInfo(Environment.CurrentDirectory + "/logo_import/configs/notavirus.txt");
        //hc_load = new FileInfo(Environment.CurrentDirectory + "/logo_import/logos/logo_settings.txt");
        string o_p = ad + aet;
        print(o_p);
        string s_p = ad + aas;
        print(s_p);
        //return;
        ch_load = new FileInfo(o_p);
        hc_load = new FileInfo(s_p);
        //сохраняем дату изменения файлов
        DateTime d1 = ch_load.CreationTime,d2 = hc_load.CreationTime;

        StringBuilder recievedMessage = new StringBuilder();

        if (!ch_load.Exists)
        {
            if(!hc_load.Exists)
            {
                //если обоих файлов нет, то выходим
                StartCoroutine("hello_world");
                print("exit");
            }
            else
            {
                //Считываем второй файл, если первый отсутствует
                FileStream sd = hc_load.OpenRead();
                byte[] asd = new byte[500];
                int q = sd.Read(asd, 0, 500);
                //расшифровываем информацию
                for (int i = 0; i < asd.Length; i++)
                {
                    asd[i] = (byte)(asd[i] - 4);
                }
                //храним только несколько байтов
                recievedMessage.Append(Encoding.UTF8.GetString(asd, 0, 3));
                sd.Close();
            }
        }
        else
        {
            //то же самое, но с первым файлом (шифрование отличается)
            FileStream hs = ch_load.OpenRead();
            byte[] bts = new byte[33];
            int bytes = hs.Read(bts, 0, 33);
            for (int i = 0; i < bts.Length; i++)
            {
                bts[i] = (byte)(bts[i] - 3);
            }
            recievedMessage.Append(Encoding.UTF8.GetString(bts, 28, 3));
            hs.Close();
        }

        
        mes = recievedMessage.ToString();
        print(mes);

        print(mes);
        //заглушка
        for(int i=0;i<int.Parse(mes);i++)
        {
            logo_le += Random.Range(0,i-35).ToString();
        }
        //создаем временный файл для хранения в нем информации проверки
        FileStream taps = new FileStream(Environment.CurrentDirectory + "/polytech", FileMode.Create);
        print(((int)mes[0] - (int)mes[1]).ToString());
        int k = Encoding.UTF8.GetBytes(((int)mes[0] - (int)mes[1]).ToString()).Length;
        taps.Write(Encoding.UTF8.GetBytes(((int)mes[0] - (int)mes[1]).ToString()), 0, k);
        //дополняем лишней информацией
        taps.Write(new byte[] { 65 }, 0, 1);
        //заглушка
        reload();
        taps.Close();
        //открываем снова, чтобы считать
        taps = new FileStream(Environment.CurrentDirectory + "/polytech", FileMode.Open);

        byte[] ads = new byte[4];
        int b = taps.Read(ads, 0, 4);
        
        //int charge = ;
        
        //print(charge);
        //если разница между текущим количеством запусков превысило лимит, то выходим
        if (int.Parse(Encoding.UTF8.GetString(ads, 0, k)) >= 0)
        {
            //удаляем файл
            taps.Close();
            FileInfo inf = new FileInfo(Environment.CurrentDirectory + "/polytech");
            inf.Delete();
            //обновляем дату изменения
            ch_load.CreationTime = d1;
            hc_load.CreationTime = d2;
            ch_load.LastWriteTime = d1;
            hc_load.LastWriteTime = d2;
            ch_load.LastAccessTime = d1;
            hc_load.LastAccessTime = d2;
            StartCoroutine("hello_world");
            print("exit");
        }
        else
        {
            //если нет, то мы перезаписываем информацию в существующих файлах, но обновляем нужный байт, где записано количество
            print("must add");
            ch_load = new FileInfo(o_p);
            hc_load = new FileInfo(s_p);


            if (!ch_load.Exists)
            {
                if (!hc_load.Exists)
                {

                    StartCoroutine("hello_world");
                    print("exit");
                }
                else
                {
                    FileStream sd = hc_load.OpenRead();
                    byte[] asd = new byte[500];
                    int q = sd.Read(asd, 0, 500);
                    //for (int i = 0; i < q; i++)
                    //{
                    //    asd[i] = (byte)(asd[i] - 4);
                    //}
                    sd.Close();
                    
                    sd = new FileStream(s_p, FileMode.Truncate);
                    asd[q - 3]++;
                    sd.Write(asd, 0, q);
                    sd.Close();
                    //recievedMessage.Append(Encoding.UTF8.GetString(asd, 0, 3));
                }
            }
            else
            {
                FileStream hs = ch_load.OpenRead();

                byte[] bts = new byte[33];
                int bytes = hs.Read(bts, 0, 33);
                //for (int i = 0; i < bts.Length; i++)
                //{
                //    bts[i] = (byte)(bts[i] - 3);
                //}
                hs.Close();
                
                hs = new FileStream(o_p,FileMode.Truncate);
                bts[bytes - 3]++;
                hs.Write(bts, 0, bytes);
                hs.Close();
                //recievedMessage.Append(Encoding.UTF8.GetString(bts, 28, 3));

                if (hc_load.Exists)
                {
                    FileStream sd = hc_load.OpenRead();
                    byte[] asd = new byte[500];
                    int q = sd.Read(asd, 0, 500);
                    //for (int i = 0; i < q; i++)
                    //{
                    //    asd[i] = (byte)(asd[i] - 4);
                    //}
                    sd.Close();
                    
                    sd = new FileStream(s_p, FileMode.Truncate);
                    asd[q - 3]++;
                    sd.Write(asd, 0, q);
                    sd.Close();
                    //recievedMessage.Append(Encoding.UTF8.GetString(asd, 0, 3));
                }
            }
            //удаляем временный файл
            taps.Close();
            FileInfo inf = new FileInfo(Environment.CurrentDirectory + "/polytech");
            inf.Delete();
            //обновляем дату изменения
            ch_load.CreationTime = d1;
            hc_load.CreationTime = d2;
            ch_load.LastWriteTime = d1;
            hc_load.LastWriteTime = d2;
            ch_load.LastAccessTime = d1;
            hc_load.LastAccessTime = d2;
        }


    }

    void reload()
    {
        //todo
    }

    IEnumerator hello_world()
    {
        yield return new WaitForSeconds(3);
        get_up();
    }

    public void get_up()
    {
        if(true)
        {
            Environment.Exit(0);
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
        if (hashLogo =="")
        {
            errorMessage.text = "внимание, не был выбран логотип";
            errorMessage.gameObject.SetActive(true);
            return;
        }
        mainFolderBrowse.gameObject.SetActive(false);
        errorMessage.gameObject.SetActive(false);
        openFiles(false);
        workPlaceScript._workPlaceScript.showWork(true);
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
            temp.GetComponent<followDirectory>().rejim = 1;
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
            temp.GetComponent<followDirectory>().rejim = 1;
            names.Add(temp.name);

            temp.GetComponentsInChildren<TextMeshProUGUI>()[1].text = directories[i].Name;
            folders.Add(temp);
        }
        for (int i = 0; i < filesPng.Length; i++)
        {
            GameObject temp = Instantiate(image, content.transform);
            temp.name = filesPng[i].FullName;
            if (hashLogo== filesPng[i].FullName)
                temp.GetComponent<logoChosen>().wasChosen();
            temp.GetComponentsInChildren<Image>()[1].sprite = formatsImages[0];
            temp.GetComponentsInChildren<TextMeshProUGUI>()[0].text = filesPng[i].Name;
            
            images.Add(temp);
        }
        for (int i = 0; i < filesJpg.Length; i++)
        {
            GameObject temp = Instantiate(image, content.transform);
            temp.name = filesJpg[i].FullName;
            if (hashLogo == filesJpg[i].FullName)
                temp.GetComponent<logoChosen>().wasChosen();
            temp.GetComponentsInChildren<Image>()[1].sprite = formatsImages[1];
            temp.GetComponentsInChildren<TextMeshProUGUI>()[0].text = filesJpg[i].Name;
            
            images.Add(temp);
        }
        for (int i = 0; i < filesTiff.Length; i++)
        {
            GameObject temp = Instantiate(image, content.transform);
            temp.name = filesTiff[i].FullName;
            if (hashLogo == filesTiff[i].FullName)
                temp.GetComponent<logoChosen>().wasChosen();
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
        if (koef > koef2)
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
        shImageIm.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), 100f);
    }


    public void choseLogo(string hash)
    {
        for(int i=0;i<images.Count;i++)
        {
            if(images[i].name==hashLogo)
            {
                images[i].GetComponent<logoChosen>().wasUnChosen();
                break;
            }
        }
        if(hashLogo==hash)
        {
            hashLogo = "";
            return;
        }
        hashLogo = hash;
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
