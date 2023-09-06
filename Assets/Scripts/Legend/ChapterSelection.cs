using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChapterSelection : MonoBehaviour
{
    public GameObject[] chapters;
    public Button[] buttons;
    public string[] chapterNames;
    public TextMeshProUGUI chapterName;

    void Start()
    {
        InteractableButtons();
    }

    void Update()
    {
        
    }

    public void SelectChapter(int chapterIndex)
    {
        for (int i = 0; i < chapters.Length; i++)
        {
            chapters[i].SetActive(i == chapterIndex);
            MakeTransparent(chapterIndex, i); //Make transparent only the pressed button
        }

        chapterName.text = chapterNames[chapterIndex];
    }

    public void MakeTransparent(int chapterIndex, int button)
    {
        Color color = buttons[button].GetComponent<Image>().color;
        if (button == chapterIndex)
        {
            color.a = 0;
        }
        else
        {
            color.a = 1;
        }
        buttons[button].GetComponent<Image>().color = color;
    }

    public void InteractableButtons()
    {
        int progress = PlayerPrefs.GetInt("Progress");
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = (i <= progress);
        }
    }
}
