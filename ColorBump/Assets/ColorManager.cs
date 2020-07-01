using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public Material PlayerMaterial;

    public List<Color> colors;
    public Color color;
    public List<Button> buttons;

    private void Start()
    {

        foreach (var item in buttons)
        {

            item.onClick.AddListener(delegate { ChangeColor(colors[buttons.IndexOf(item)]); });
          

        }

    }


    public void ChangeColor(Color color)
    {

        PlayerMaterial.color = color;

    }
}
