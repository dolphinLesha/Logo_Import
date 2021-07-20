using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showPathSave : MonoBehaviour
{

    public Image fullPath;
    public Image dialog;


    void Start()
    {
        fullPath.gameObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        t = 0;
        alpha = 0;
        flag = true;
        fullPath.color = new Color(fullPath.color.r, fullPath.color.g, fullPath.color.b, alpha);
        StartCoroutine(showIm());
    }
    private void OnMouseOver()
    {
        fullPath.GetComponentInChildren<Text>().text = saveImageDialog._saveImageDialog.currentPath;
    }
    private void OnMouseExit()
    {
        t = 0;
        flag = false;
        isShowing = false;
        StopCoroutine(showIm());
        fullPath.gameObject.SetActive(false);
    }
    int t = 0;
    bool isShowing = false;
    bool flag = false;
    float alpha = 0;
    IEnumerator showIm()
    {
        while (t < 2 && flag)
        {
            if (t == 1 && flag)
            {
                fullPath.gameObject.SetActive(true);
                isShowing = true;
                fullPath.GetComponentInChildren<Text>().text = saveImageDialog._saveImageDialog.currentPath;
                if (saveImageDialog._saveImageDialog.currentPath.Length > 57)
                {
                    fullPath.rectTransform.offsetMax = new Vector2(0, 20);
                    fullPath.rectTransform.offsetMin = new Vector2(0, 0);
                    dialog.rectTransform.offsetMin = new Vector2(
                        dialog.rectTransform.offsetMin.x, 0);
                    dialog.rectTransform.offsetMax = new Vector2(
                        dialog.rectTransform.offsetMax.x, 0);
                }
                if (saveImageDialog._saveImageDialog.currentPath.Length > 114)
                {
                    fullPath.rectTransform.offsetMax = new Vector2(0, 20);
                    fullPath.rectTransform.offsetMin = new Vector2(0, -20);
                    dialog.rectTransform.offsetMin = new Vector2(
                        dialog.rectTransform.offsetMin.x, -20);
                    dialog.rectTransform.offsetMax = new Vector2(
                        dialog.rectTransform.offsetMax.x, -20);
                }
                if (saveImageDialog._saveImageDialog.currentPath.Length <= 57)
                {
                    fullPath.rectTransform.offsetMax = new Vector2(0, 0);
                    fullPath.rectTransform.offsetMin = new Vector2(0, 0);
                    dialog.rectTransform.offsetMin = new Vector2(
                        dialog.rectTransform.offsetMin.x, 0);
                    dialog.rectTransform.offsetMax = new Vector2(
                        dialog.rectTransform.offsetMax.x, 0);
                }
            }
            t++;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Update()
    {
        if (isShowing)
        {
            alpha = Mathf.MoveTowards(alpha, 0.6f, 1f * Time.deltaTime);
            fullPath.color = new Color(fullPath.color.r, fullPath.color.g, fullPath.color.b, alpha);
            if (alpha > 0.6f)
            {
                isShowing = false;
            }
        }
    }
}
