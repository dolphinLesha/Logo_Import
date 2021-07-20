using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class workPlaceScript : MonoBehaviour
{
    public static workPlaceScript _workPlaceScript;

    public Image mainPlace;
    public GameObject logoPlacement;
    public Sprite[] formats;
    public Image logoUploadB;
    public InputField centerFitX, centerFitY, cornerFitX, cornerFitY;
    public TMP_Dropdown units;
    

    List<string> images = new List<string>();
    string logoPath="";
    public int logoCorner = 3; //0-leftDown,1-leftUp,2-rightUp
    int pixelsCenterX, pixelsCenterY, pixelsCornerX, pixelsCornerY;
    float persantCenterX, persantCenterY, persantCornerX, persantCornerY;
    int rejim = 0; //0-pixels,1-prosent

    void Start()
    {
        _workPlaceScript = this;
    }

    public void showWorkPlace(List<string> hashes,bool isShow)
    {
        names = new List<string>();
        textures = new List<Texture2D>();
        
            
        mainPlace.gameObject.SetActive(isShow);
        if (logoPath != "")
            confirmLogo(logoPath);
        if(isShow)
        {
            images = hashes;
            settingsScript._settingsScript.showSettings(false);
            folderBrowseScript._folderBrowser.openFiles(false);
            folderBrowserLogo._folderBrowserLogo.openFiles(false);
            saveImageDialog._saveImageDialog.openFiles(false);
            menubar._menubar.wasChosen(1);
        }
        
    }

    public void showWork(bool isShow)
    {
        if (images.Count == 0)
        {
            mainPlace.gameObject.SetActive(false);
            return;
        }
            
        mainPlace.gameObject.SetActive(isShow);
        
        if (isShow)
        {
            settingsScript._settingsScript.showSettings(false);
            folderBrowseScript._folderBrowser.openFiles(false);
            folderBrowserLogo._folderBrowserLogo.openFiles(false);
            saveImageDialog._saveImageDialog.openFiles(false);
            menubar._menubar.wasChosen(1);
        }
    }
    public Image logoIm;

    IEnumerator setImage(string path)
    {
        WWW www = new WWW(path);
        yield return www;
        Texture2D texture = www.texture;
        if (texture.width > texture.height)
        {
            float koef = (float)texture.width / (float)texture.height;
            logoIm.rectTransform.sizeDelta = new Vector2(100, 100f / koef);
        }
        else
        {
            float koef = (float)texture.width / (float)texture.height;
            logoIm.rectTransform.sizeDelta = new Vector2(100f*koef, 100);
        }
        logoIm.sprite = null;
        logoIm.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
        logoIm.gameObject.SetActive(true);
    }

    public void confirmLogo(string path)
    {
        string[] t = path.Split((char)(92));
        string[] f = t[t.Length-1].Split('.');
        string name = "", format=f[f.Length-1];
        for(int i=0;i<f.Length-1;i++)
        {
            name += f[i];
        }
        if (format == "png")
            logoPlacement.GetComponentsInChildren<Image>()[1].sprite = formats[0];
        else if (format == "jpg")
            logoPlacement.GetComponentsInChildren<Image>()[1].sprite = formats[1];
        else if (format == "tiff")
            logoPlacement.GetComponentsInChildren<Image>()[1].sprite = formats[2];
        logoPlacement.GetComponentInChildren<Text>().text = name;
        logoPlacement.gameObject.SetActive(true);
        logoUploadB.gameObject.SetActive(false);
        logoPath = path;
        if(images.Count!=0)
        {
            showWork(true);
        }
        StartCoroutine(setImage(path));
    }
    public TextMeshProUGUI trunText;
    public void truncateSwitch()
    {
        isTruncate = !isTruncate;
        if (!isTruncate)
            trunText.text = "пропорции не сохраняются";
        else
            trunText.text = "пропорции сохраняются";
    }

    public void changeSettings()
    {
        rejim = units.value;
        if(rejim==0)
        {
            int a;
            if (int.TryParse(centerFitX.text, out a))
                pixelsCenterX = a;
            if (int.TryParse(centerFitY.text, out a))
                pixelsCenterY = a;
            if (int.TryParse(cornerFitX.text, out a))
                pixelsCornerX = a;
            if (int.TryParse(cornerFitY.text, out a))
                pixelsCornerY = a;
        }
        else if(rejim==1)
        {
            float a;
            if (float.TryParse(centerFitX.text, out a))
                persantCenterX = a/100f;
            if (float.TryParse(centerFitY.text, out a))
                persantCenterY = a/100f;
            if (float.TryParse(cornerFitX.text, out a))
                persantCornerX = a/100f;
            if (float.TryParse(cornerFitY.text, out a))
                persantCornerY = a/100f;
        }
    }
    int isSingle = 0;
    public Sprite[] singleSpr;
    public Image singleMode;

    public void changeSingleMode()
    {
        isSingle++;
        if (isSingle > 1)
            isSingle = 0;
        if(isSingle==0)
        {
            centerFitX.interactable = false;
            centerFitY.interactable = false;
        }
        else
        {
            centerFitX.interactable = true;
            centerFitY.interactable = true;
        }
        singleMode.sprite = singleSpr[isSingle];
    }

    bool checkSettings()
    {
        if (logoPath != "")
        {
            if (isSingle==0)
            {
                if (
            cornerFitX.text != "" &&
            cornerFitY.text != "")
                    return true;
                else
                    return false;
            }   
            else
            {
                if (centerFitX.text == "" ||
            centerFitY.text == "" ||
            cornerFitX.text == "" ||
            cornerFitY.text == "")
                    return false;
                else
                    return true;
            }
            //else
            //{
            //    if (
            //centerFitX.text != "" &&
            //centerFitY.text != "")
            //        return true;
            //    else
            //        return false;
            //}
        }
        else
            return false;
    }

    public void startImportLogo()
    {
        if (!checkSettings())
            return;
        process.gameObject.SetActive(true);
        bar.fillAmount = 0;
        string[] t = images[0].Split((char)(92));
        string[] f = t[t.Length - 1].Split('.');
        string name = "", format = f[f.Length - 1];
        for (int e = 0; e < f.Length - 1; e++)
        {
            name += f[e];
        }
        nameOfprocess.text = name;
        StartCoroutine(performImport());

    }
    float perOfDone = 0f;
    IEnumerator performImport()
    {
        for(int i=0;i<images.Count;i++)
        {
            WWW www = new WWW("temp");
            yield return www;
            perOfDone = (float)i / (float)images.Count;
            bar.fillAmount = perOfDone;
            string[] t = images[i].Split((char)(92));
            string[] f = t[t.Length - 1].Split('.');
            string name = "", format = f[f.Length - 1];
            for (int e = 0; e < f.Length - 1; e++)
            {
                name += f[e];
            }
            nameOfprocess.text = name;
            yield return StartCoroutine(placeLogo(images[i]));
            
        }
        process.gameObject.SetActive(false);
        mainPlace.gameObject.SetActive(false);
        saveImageDialog._saveImageDialog.getImages(textures, names);
        saveImageDialog._saveImageDialog.openFiles(true);
    }
    bool isTruncate = true;
    public Image process;
    public Image bar;
    public TextMeshProUGUI nameOfprocess;
    IEnumerator placeLogo(string image)
    {
        
        
        //print(image);
        WWW www = new WWW(image);
        Texture2D mainTexture = www.texture;
        www = new WWW(logoPath);
        yield return www;
        Texture2D logoTexture = www.texture;
        int leftCornerX=0, leftCornerY=0, rightCornerX=0, rightCornerY=0;
        if(rejim==0 && isSingle==1)
        {
            if(logoCorner==0)
            {
                leftCornerX = pixelsCornerX;
                leftCornerY = pixelsCornerY;
                rightCornerX = mainTexture.width / 2 - pixelsCenterX;
                rightCornerY = mainTexture.height / 2 - pixelsCenterY;
            }
            else if(logoCorner == 1)
            {
                leftCornerX = pixelsCornerX;
                leftCornerY = mainTexture.height / 2 + pixelsCenterY;
                rightCornerX = mainTexture.width / 2 - pixelsCenterX;
                rightCornerY = mainTexture.height - pixelsCornerY;
            }
            else if (logoCorner == 2)
            {
                leftCornerX = mainTexture.width / 2 + pixelsCenterX;
                leftCornerY = mainTexture.height / 2 + pixelsCenterY;
                rightCornerX = mainTexture.width - pixelsCornerX;
                rightCornerY = mainTexture.height - pixelsCornerY;
            }
            else if (logoCorner == 3)
            {
                leftCornerX = mainTexture.width / 2 + pixelsCenterX;
                leftCornerY = pixelsCornerY;
                rightCornerX = mainTexture.width - pixelsCornerX;
                rightCornerY = mainTexture.height / 2 - pixelsCenterY;
            }
        }
        else if(rejim==1 && isSingle==1)
        {
            float w2 = mainTexture.width / 2;
            float h2 = mainTexture.height / 2;
            if (logoCorner == 0)
            {
                leftCornerX = (int)(w2 * persantCornerX);
                leftCornerY = (int)(h2 * persantCornerY);
                rightCornerX = (int)w2-(int)(w2*persantCenterX);
                rightCornerY = (int)h2 - (int)(h2 * persantCenterY);
            }
            else if (logoCorner == 1)
            {
                leftCornerX = (int)(w2 * persantCornerX);
                leftCornerY = (int)h2 + (int)(h2 * persantCenterY);
                rightCornerX = (int)w2 - (int)(w2 * persantCenterX);
                rightCornerY = mainTexture.height - (int)(h2 * persantCornerY);
            }
            else if (logoCorner == 2)
            {
                leftCornerX = (int)w2 + (int)(w2 * persantCenterX);
                leftCornerY = (int)h2 + (int)(h2 * persantCenterY);
                rightCornerX = mainTexture.width - (int)(w2 * persantCornerX);
                rightCornerY = mainTexture.height - (int)(h2 * persantCornerY);
            }
            else if (logoCorner == 3)
            {
                leftCornerX = (int)w2 + (int)(w2 * persantCenterX);
                leftCornerY = (int)(h2 * persantCornerY);
                rightCornerX = mainTexture.width - (int)(w2 * persantCornerX);
                rightCornerY = (int)h2 - (int)(h2 * persantCenterY);
            }
        }
        int logoDWidth=rightCornerX-leftCornerX, logoDHeight=rightCornerY-leftCornerY;
        
        float koefX, koefY;
        koefX = (float)logoDWidth / (float)logoTexture.width;
        koefY = (float)logoDHeight / (float)logoTexture.height;
        
        if(isTruncate && isSingle==1)
        {
            if(koefX>koefY)
            {
                koefX = koefY;
                logoDWidth = (int)(koefX * logoTexture.width);
                rightCornerX = leftCornerX + logoDWidth;
            }
            else
            {
                koefY = koefX;
                logoDHeight = (int)(koefY * logoTexture.height);
                rightCornerY = leftCornerY + logoDHeight;
            }
        }
        if(isSingle==0)
        {
            if (rejim == 0)
            {
                if (logoCorner == 0)
                {
                    leftCornerX = pixelsCornerX;
                    leftCornerY = pixelsCornerY;
                    rightCornerX = leftCornerX+logoTexture.width;
                    rightCornerY = leftCornerY+logoTexture.height;
                }
                else if (logoCorner == 1)
                {
                    leftCornerX = pixelsCornerX;
                    rightCornerY = mainTexture.height - pixelsCornerY;
                    leftCornerY = rightCornerY-logoTexture.height;
                    rightCornerX = leftCornerX + logoTexture.width; 
                }
                else if (logoCorner == 2)
                {
                    rightCornerX = mainTexture.width - pixelsCornerX;
                    rightCornerY = mainTexture.height - pixelsCornerY;
                    leftCornerX = rightCornerX - logoTexture.width;
                    leftCornerY = rightCornerY - logoTexture.height;
                }
                else if (logoCorner == 3)
                {
                    rightCornerX = mainTexture.width - pixelsCornerX;
                    leftCornerX = rightCornerX-logoTexture.width;
                    leftCornerY = pixelsCornerY;
                    rightCornerY = leftCornerY+logoTexture.height;
                }
            }
            else if (rejim == 1)
            {
                float w2 = mainTexture.width;
                float h2 = mainTexture.height;
                if (logoCorner == 0)
                {
                    leftCornerX = (int)(w2 * persantCornerX);
                    leftCornerY = (int)(h2 * persantCornerY);
                    rightCornerX = leftCornerX + logoTexture.width;
                    rightCornerY = leftCornerY + logoTexture.height;
                }
                else if (logoCorner == 1)
                {
                    leftCornerX = (int)(w2 * persantCornerX);
                    rightCornerY = mainTexture.height - (int)(h2 * persantCornerY);
                    leftCornerY = rightCornerY-logoTexture.height;
                    rightCornerX = leftCornerX + logoTexture.width;
                    
                }
                else if (logoCorner == 2)
                {
                    rightCornerX = mainTexture.width - (int)(w2 * persantCornerX);
                    rightCornerY = mainTexture.height - (int)(h2 * persantCornerY);
                    leftCornerX = rightCornerX-logoTexture.width;
                    leftCornerY = rightCornerY-logoTexture.height;
                    
                }
                else if (logoCorner == 3)
                {
                    rightCornerX = (int)w2 - (int)(w2 * persantCornerX);
                    leftCornerX = rightCornerX-logoTexture.width;
                    leftCornerY = (int)(h2 * persantCornerY);
                    rightCornerY = leftCornerY+logoTexture.height;
                }
            }
            koefX = 1f;
            koefY = 1f;
        }
        //else if(isSingle==1)
        //{

        //}
        for (int i=leftCornerX,i2=0;i<rightCornerX;i++,i2++)
        {
            for (int e = leftCornerY,e2=0; e < rightCornerY; e++,e2++)
            {
                //int xP = i - leftCornerX,yP=e - leftCornerY;
                Color tempCol = logoTexture.GetPixel((int)(i2/koefX), (int)(e2/koefY));
                Color tempColMain = mainTexture.GetPixel(i, e);
                //ClrF.A + (1 - ClrF.A) * SrcF.A;

                //RsltF.R := (ClrF.R * ClrF.A + SrcF.R * SrcF.A * (1 - ClrF.A)) / RsltF.A;
                //RsltF.G := (ClrF.G * ClrF.A + SrcF.G * SrcF.A * (1 - ClrF.A)) / RsltF.A;
                //RsltF.B := (ClrF.B * ClrF.A + SrcF.B * SrcF.A * (1 - ClrF.A)) / RsltF.A;
                float aR = tempCol.a + (1 - tempCol.a) * tempColMain.a;
                float rR = (tempCol.r * tempCol.a + tempColMain.r * tempColMain.a * (1 - tempCol.a)) / aR;
                float gR = (tempCol.g * tempCol.a + tempColMain.g * tempColMain.a * (1 - tempCol.a)) / aR;
                float bR = (tempCol.b * tempCol.a + tempColMain.b * tempColMain.a * (1 - tempCol.a)) / aR;
                mainTexture.SetPixel(i, e, new Color(rR, gR, bR, aR));
            }
        }
        mainTexture.Apply();
        string[] t = image.Split((char)(92));
        string[] f = t[t.Length - 1].Split('.');
        string name = "", format = f[f.Length - 1];
        for (int i = 0; i < f.Length - 1; i++)
        {
            name += f[i];
        }
        textures.Add(mainTexture);
        names.Add(name);
    }

    List<string> names = new List<string>();
    List<Texture2D> textures = new List<Texture2D>();

    
}
