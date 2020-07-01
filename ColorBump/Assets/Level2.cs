using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2 : MonoBehaviour
{

    public Image Fade;
    
    void Awake()
    {

        Fade.DOFade(0, 1);

    }


    void Update()
    {
        
    }
}
