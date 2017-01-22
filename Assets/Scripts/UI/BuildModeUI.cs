using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using System;

public class BuildModeUI : MonoBehaviour {

    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text placeMessageText;

    [SerializeField]
    private BuildModeOverlay[] overlays;

    private Builder[] _builders;

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

    protected void Update() {
        if (placeMessageText.gameObject.activeInHierarchy && _builders != null) {
            string text = "Press <A> to build. Waiting for AMOUNT players to build...";
            text = text.Replace("AMOUNT", _builders.Where(b => !b.HasBuild()).Count().ToString());
            placeMessageText.text = text;
        }
    }

    public void ShowBuildOverlays(List<Builder> builders){
        _builders = builders.ToArray();
        for(int i = 0; i < builders.Count; i++){
            overlays[i].Setup(builders[i]);
        }
    }

    public void HideBuildOverlay(Builder builder){
        BuildModeOverlay overlay =  overlays.FirstOrDefault(o => o.Builder == builder);
        if(overlay != null){
            overlay.Hide();
        }
    }

    public void HidePlacingHelpMessage() {
        placeMessageText.gameObject.SetActive(false);
    }

    public void ShowPlacingHelpMessage() {
        placeMessageText.gameObject.SetActive(true);
    }
}
