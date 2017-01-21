using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildModeOverlay : MonoBehaviour {

    [SerializeField]
    private Text playerText;

    [SerializeField]
    private Image background;

    public Builder Builder;
    private RectTransform rectTransform;

    private void Start(){
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    public void Setup(Builder builder){
        Builder = builder;
        background.color = builder.player.color;
        gameObject.SetActive(true);
    }

    public void Hide(){
        Builder = null;
        gameObject.SetActive(false);
    }

    private void LateUpdate(){
        if(Builder == null){
            return;
        }

        Vector2 screenPos = Camera.main.WorldToScreenPoint(Builder.transform.position);
        transform.position = screenPos;

    }
}
