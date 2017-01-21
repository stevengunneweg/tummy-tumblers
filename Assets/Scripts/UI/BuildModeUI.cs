using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class BuildModeUI : MonoBehaviour {

    [SerializeField]
    private Text titleText;

    [SerializeField]
    private BuildModeOverlay[] overlays;

    private void Awake(){
        titleText.gameObject.SetActive(false);
        overlays = GetComponentsInChildren<BuildModeOverlay>();
    }

    public void ShowBuildTime(){
        titleText.gameObject.gameObject.SetActive(true);
        titleText.transform.localScale = Vector3.zero;
        titleText.transform.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack);

        titleText.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack).SetDelay(2).OnComplete(delegate() {
            titleText.gameObject.SetActive(false);
        });
    }

    public void ShowBuildOverlays(List<Builder> builders){
        for(int i = 0; i < overlays.Length; i++){
            overlays[i].Setup(builders[i]);
        }
    }

    public void HideBuildOverlay(Builder builder){
        BuildModeOverlay overlay =  overlays.FirstOrDefault(o => o.Builder == builder);
        if(overlay != null){
            overlay.Hide();
        }
    }
}
