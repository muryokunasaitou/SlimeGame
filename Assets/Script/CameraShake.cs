using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private Vector3 positionStrength;
    [SerializeField] private Vector3 rotationStrength;
    [SerializeField] NDTDEvent ndtdEvent;

    public float shakeDuration = 3;
    void Update()
    {
        if (ndtdEvent != null && ndtdEvent.isCamShake == true)
        {
            CameraShaker();
            ndtdEvent.isCamShake = false;
        }
    }

    private void CameraShaker()
    {
        camera.DOComplete();
        camera.DOShakePosition(shakeDuration, positionStrength);
        camera.DOShakeRotation(shakeDuration, rotationStrength)/*.OnComplete(StartTime)*/;
        Debug.Log("カメラシェイカー発動");
    }

    //void StartTime()
    //{
    //    DOTween
    //    .To(value => { }, 0, 1, 1)
    //    .SetUpdate(false);
    //}

}
