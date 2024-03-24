using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class ScrollBG : MonoBehaviour 
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;

    public void Start()
    {
        _img = gameObject.GetComponent<RawImage>();
    }

    void Update()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x,_y) * Time.deltaTime,_img.uvRect.size);
    }
}