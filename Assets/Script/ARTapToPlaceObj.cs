﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObj : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objInstantiate;
    public GameObject[] spawnObjects;
    public GameObject SphereDrag;
    public Text DisplaInfo;
    private GameObject _spawnedObj;
    private ARRaycastManager _aRRaycastManager;
    private Vector2 _touchPosition;

    private float width;
    private float height;
    private Vector3 position;
    private GameObject CurObjToInstantiate;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public GameObject[] tempspawnObjects;
    public GameObject[] UICrossButtons;

    private int curIndex = 0;
    public InputField Rotate;
    public InputField Scale;

    private float _defaultRotation = 25f;
    private float _defaultScale = 0.01f;
    
    private void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
        position = SphereDrag.transform.position;
        for(int i = 0; i < 4; i++)
        {
            tempspawnObjects[i] = null;
            UICrossButtons[i].SetActive(false);
        }
        _aRRaycastManager = GetComponent<ARRaycastManager>();

        Rotate.text = "25";
        Scale.text = "0.01";
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetTouchPos(out Vector2 touchPosition))
        {
            return;
        }
        if (_aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            _spawnedObj = tempspawnObjects[curIndex];
            var hitPose = hits[0].pose;
            if (_spawnedObj == null)
            {
                DisplaInfo.text = "Obj spawned new" + hitPose.position.x + "," + hitPose.position.y + "," + hitPose.position.z;
                _spawnedObj = Instantiate(spawnObjects[curIndex], hitPose.position, hitPose.rotation);
                tempspawnObjects[curIndex] = _spawnedObj;
                _spawnedObj.transform.Rotate(Vector3.up, 180f);
            }
            else
            {
                DisplaInfo.text = "Obj spawned old" + hitPose.position.x + "," + hitPose.position.y + "," + hitPose.position.z;
                _spawnedObj.transform.position = hitPose.position;
            }            
            CurObjToInstantiate = _spawnedObj;
        }
    }

    private bool GetTouchPos(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = touch.position;
                pos.x = (pos.x - width) / width;
                pos.y = (pos.y - height) / height;
                position = new Vector3(-pos.x, pos.y, 0.0f);

                // Position the cube.
                SphereDrag.transform.position = position;
                DisplaInfo.text = "Dragging cube";
            }
            DisplaInfo.text = "Final Touched";
            touchPosition = Input.GetTouch(0).position;
            DisplaInfo.text = "touched ended:" + touchPosition.x + "," + touchPosition.y;
            return true;
        }
        touchPosition = default;
        return false;
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
                CurObjToInstantiate.transform.Rotate(Vector3.up, -output);
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

    public void MoveToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Btn1Clicked()
    {
        curIndex = 0;
        UICrossButtons[0].SetActive(true);
    }
    public void Btn2Clicked()
    {
        curIndex = 1;
        UICrossButtons[1].SetActive(true);
    }
    public void Btn3Clicked()
    {
        curIndex = 2;
        UICrossButtons[2].SetActive(true);
    }
    public void Btn4Clicked()
    {
        curIndex = 3;
        UICrossButtons[3].SetActive(true);
    }

    public void Btn1CrossClicked()
    {
        if (tempspawnObjects[0] != null)
        {
            UICrossButtons[0].SetActive(false);
            Destroy(tempspawnObjects[0]);            
        }
    }
    public void Btn2CrossClicked()
    {
        if (tempspawnObjects[1] != null)
        {
            UICrossButtons[1].SetActive(false);
            Destroy(tempspawnObjects[1]);
        }
    }
    public void Btn3CrossClicked()
    {
        if (tempspawnObjects[2] != null)
        {
            UICrossButtons[2].SetActive(false);
            Destroy(tempspawnObjects[2]);
        }
    }
    public void Btn4CrossClicked()
    {
        if (tempspawnObjects[3] != null)
        {
            UICrossButtons[3].SetActive(false);
            Destroy(tempspawnObjects[3]);
        }
    }

}
