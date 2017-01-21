using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BuildModeUI : MonoBehaviour {

    [SerializeField]
    private Text titleText;

    public void ShowBuildTime(){
        titleText.gameObject.gameObject.SetActive(true);
        titleText.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutBack);

        titleText.transform.DOScale(Vector3.zero, 0.8f).SetEase(Ease.InBack).SetDelay(3).OnComplete(delegate() {
            titleText.gameObject.SetActive(false);
        });
    }
}
