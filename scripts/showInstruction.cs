using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class showInstruction : MonoBehaviour
{
    public int index;
    string[] instr = {"загрузить изображения",
    "настройки импорта лого",
    "сохранить получившиеся изображения",
    "настройки/помощь"};
    public TextMeshProUGUI instruction;
    void Start()
    {
        
    }

    private void OnMouseEnter()
    {
        instruction.gameObject.SetActive(true);
        instruction.text = instr[index];
    }

    private void OnMouseExit()
    {
        instruction.gameObject.SetActive(false);
    }

    
}
