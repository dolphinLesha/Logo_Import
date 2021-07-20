using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class testScript : MonoBehaviour
{
    public Image main;
    void Start()
    {
        loadImage();
    }
    //byte[] data = File.ReadAllBytes(pathOfSaving);
    //Texture2D result = new Texture2D((int)(Screen.height / her2.scaleFactor), (int)(Screen.height / her2.scaleFactor));
    //result.LoadImage(data);
    //openIm.sprite = Sprite.Create(result,new Rect(0,0, result.width, result.height),new Vector2(0,0));
    void loadImage()
    {
        DirectoryInfo dirInfo = new DirectoryInfo("C:/Users/FDAly/Desktop/игра");
        DirectoryInfo[] dirs = dirInfo.GetDirectories();
        
        FileInfo[] her = dirInfo.GetFiles("262*");
        WWW www = new WWW(her[0].FullName);
        //main.sprite = Sprite.Create(www.texture,new Rect(0,0,www.texture.width,www.texture.height),new Vector2(0,0),100f,0,)
        float koefX = 1f,koefY=1f;
        //Sprite spr = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        Texture2D texture = www.texture;
        int width = texture.width, height = texture.height;
        Texture2D newText = new Texture2D((int)((float)width *koefX), (int)((float)height *koefY));
        int width2 = newText.width, height2 = newText.height;
        print(width2 + " " + height2);

        Color[,] temp = new Color[width2, height2];
        int predelLeft = (int)(width2 /2);
        int predelDown = (int)(height2 /2);
        //for (int i = -predelLeft; i < predelLeft; i++)
        //{
        //    for (int e = -predelDown; e < predelDown; e++)
        //    {
        //        newText.SetPixel(i + predelLeft, e + predelDown, texture.GetPixel((int)((float)(i + predelLeft)/koefX), (int)((float)(e + predelDown) /koefY)));
        //        //temp[i + predelLeft, e + predelDown] = newText.GetPixel(i + predelLeft, e + predelDown);
        //    }
        //}
        for (int i = 0; i < width2; i++)
        {
            for (int e = 0; e < height2; e++)
            {
                newText.SetPixel(i, e, texture.GetPixel((int)((float)(i) / koefX), (int)((float)(e) / koefY)));
                //temp[i + predelLeft, e + predelDown] = newText.GetPixel(i + predelLeft, e + predelDown);
            }
        }
        //for (int i = 0; i < width2-1; i+=2)
        //{
        //    for (int e = 0; e < height2 - 1; e += 2)
        //    {
        //        try
        //        {
        //            Color t1 = newText.GetPixel(i - 1, e);
        //            Color t2 = newText.GetPixel(i, e - 1);
        //            temp[i, e] = (t1 + t2) / 2f;
        //        }
        //        catch { }
        //        try
        //        {
        //            Color t1 = newText.GetPixel(i - 1, e+1);
        //            Color t2 = newText.GetPixel(i, e);
        //            temp[i, e] = (t1 + t2) / 2f;
        //        }
        //        catch { }
        //    }
        //}
        newText.SetPixel(0, 0, Color.blue);
        //newText.SetPixel(0, height2-1, Color.yellow);
        //newText.SetPixel(width2-1, 0, Color.red);
        //newText.SetPixel(width2-1, height2-1, Color.green);
        newText.Apply();
        byte[] bytes = newText.EncodeToPNG();
        File.WriteAllBytes("C:/Users/FDAly/Desktop/игра/262T2.png", bytes);

        //main.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }
    
    void Update()
    {
        
    }
}
