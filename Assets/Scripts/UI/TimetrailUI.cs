using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimetrailUI : MonoBehaviour {

    [SerializeField]
    private TimeElement elementTemplate;

    private void Start(){
        elementTemplate.gameObject.SetActive(false);
    }

    public void SpawnElement(Player player, float time, int position, bool died = false){
        TimeElement element = Instantiate(elementTemplate, elementTemplate.transform.parent);
        element.Show();
        element.background.color = player.color;

        if(time == float.MaxValue){
            element.time.text = "died..";
        }else{
            element.time.text = time.ToString("F1") + " <size='27'>sec</size>";
        }


        if(position == int.MaxValue){
            element.position.text = "";
        }else{
            element.position.text = position + ".";
        }
        element.SetPosition(position);

        if(died){
            element.SetDead();
        }
    }

    public void Hide(){
        foreach(TimeElement element in GetComponentsInChildren<TimeElement>()){
            element.Kill();
        }
    }
}
