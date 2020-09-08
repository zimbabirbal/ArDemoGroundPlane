using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempTest : MonoBehaviour
{
    // Update is called once per frame
    public GameObject CurObjToInstantiate;
    public InputField Rotate;
    public InputField Scale;

    private float _defaultRotation = 25f;
    private float _defaultScale = 0.5f;
    private void Start()
    {
        Rotate.text = "25";
        Scale.text = "0.5";
    }
    void Update()
    {
        
    }
    public void RotateLeftObject()
    {
        if (CurObjToInstantiate != null)
        {
            //var y = CurObjToInstantiate.transform.rotation.y;
            //y += 20f;
            //CurObjToInstantiate.transform.localRotation = Quaternion.Euler(0,y,0);
            if (Single.TryParse(Rotate.text, out float output))
            {
                CurObjToInstantiate.transform.Rotate(Vector3.up, output);
            }
            else
            {
                CurObjToInstantiate.transform.Rotate(Vector3.up, _defaultRotation);
            }
        }
    }
    public void RotateRightObject()
    {
        if (CurObjToInstantiate != null)
        {
            if (Single.TryParse(Rotate.text, out float output))
            {
                CurObjToInstantiate.transform.Rotate(Vector3.up, output);
            }
            else
            {
                CurObjToInstantiate.transform.Rotate(Vector3.up, -_defaultRotation);
            }
        }
    }

    public void ScaleUpObject()
    {
        if (CurObjToInstantiate != null)
        {
            if (Single.TryParse(Scale.text, out float output))
            {
                var curScale = CurObjToInstantiate.transform.localScale;
                var temp = new Vector3(curScale.x + output, curScale.y + output, curScale.z + output);
                CurObjToInstantiate.transform.localScale = temp;
            }
            else
            {
                var curScale = CurObjToInstantiate.transform.localScale;
                var temp = new Vector3(curScale.x + _defaultScale, curScale.y + _defaultScale, curScale.z + _defaultScale);
                CurObjToInstantiate.transform.localScale = temp;
            }
            
        }
    }
    public void ScaleDownObject()
    {
        if (CurObjToInstantiate != null)
        {
            if (Single.TryParse(Scale.text, out float output))
            {
                var curScale = CurObjToInstantiate.transform.localScale;
                var temp = new Vector3(curScale.x - output, curScale.y - output, curScale.z - output);
                CurObjToInstantiate.transform.localScale = temp;
            }
            else
            {
                var curScale = CurObjToInstantiate.transform.localScale;
                var temp = new Vector3(curScale.x - _defaultScale, curScale.y - _defaultScale, curScale.z - _defaultScale);
                CurObjToInstantiate.transform.localScale = temp;
            }
        }
    }
}
