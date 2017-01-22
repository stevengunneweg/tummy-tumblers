using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TimeElement : MonoBehaviour {

    [SerializeField]
    private RectTransform visualTransform;

    [SerializeField]
    public Image background;

    [SerializeField]
    public Text time;

    [SerializeField]
    public Text position;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private List<Sprite> cups;

    [SerializeField]
    private Sprite dead;

    private void Start(){
        InstantHide();
        Show();
    }

    public void Show(){
        gameObject.SetActive(true);
        visualTransform.DOAnchorPosX(0, 0.35f).SetEase(Ease.OutBack);
    }

    public void Kill(){
        visualTransform.DOAnchorPosX(visualTransform.sizeDelta.x - 300, 0.25f).SetEase(Ease.InBack).OnComplete(delegate() {
            Destroy(gameObject); 
        });
    }

    public void InstantHide(){
        gameObject.SetActive(false);
        visualTransform.anchoredPosition -= new Vector2(visualTransform.sizeDelta.x + 300, 0);
    }

    public void SetPosition(int position){
        if(position <= 3){
            icon.gameObject.SetActive(true);
            icon.sprite = cups[position - 1];
            icon.transform.localScale = Vector3.zero;
            icon.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack).SetDelay(0.5f);
        }else{
            icon.gameObject.SetActive(false);
        }
    }

    public void SetDead(){
        icon.gameObject.SetActive(true);
        icon.sprite = dead;
    }

}
