﻿using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour {
    public float speed = 0.006f;
    public float FadeInterval = 2f;
    public float ExchangeInterval= 1f;
    public Sprite[] Sprites;

    private Image FadeImage;

    void Start ()
    {
        //アタッチされてるImageを取得
        FadeImage = gameObject.GetComponent<Image>();
        StartCoroutine("FadeControl");
    }

    public  IEnumerator FadeControl()
    {
        foreach (Sprite sprite in Sprites)
        {
            yield return FadeInOut(sprite,FadeInterval);
            yield return new WaitForSecondsRealtime(ExchangeInterval);
        }
    }

    public IEnumerator FadeInOut(Sprite sprite,float Fadeinterval)
    {
        //絵を設定
        SetImage(sprite);
        //最初に透明にする
        SetAlpha(0);
        //フェードイン開始
        yield return FadeIn();
        //フェードイン後待機
        yield return new WaitForSecondsRealtime(Fadeinterval);
        //フェードアウト開始
        yield return FadeOut();
        //フェードアウトが終わるまでは次の処理を行わない
    }


    private void SetImage(Sprite sprite)
    {
        FadeImage.sprite = sprite;
    }

    private void SetAlpha(float alpha) {
        FadeImage.color = new Color (FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, alpha);
    }

    private IEnumerator FadeIn()
    {
        float alpha = FadeImage.color.a;
        while (true)
        {
            alpha += speed;
            SetAlpha (alpha);
            if (FadeImage.color.a >= 1f) {
                yield break;
            }
            //1フレームごとにループさせる
            yield return null;
        }
    }

    private IEnumerator FadeOut() {
        float alpha = FadeImage.color.a;
        while (true)
        {
            alpha -= speed;
            SetAlpha (alpha);
            if (FadeImage.color.a <= 0f) {
                yield break;
            }
            //1フレームごとにループさせる
            yield return null;
        }
    }
}
