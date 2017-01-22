using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimetrailUI : MonoBehaviour {

    [SerializeField]
    private TimeElement elementTemplate;

    public List<TimeElement> spawnedElements = new List<TimeElement>();

    private void Start(){
        elementTemplate.gameObject.SetActive(false);
    }

    public void SpawnElement(Player player, float time, int position, bool died = false){
        TimeElement element = Instantiate(elementTemplate, elementTemplate.transform.parent);
        element.Show();
        element.background.color = player.color;
        element.player = player;

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
        }else{
            spawnedElements.Add(element);
        }
    }

    public void DoPointAnimation(int maxPoints){
        int i = maxPoints;
        foreach(TimeElement element in spawnedElements){
            element.DoPointAnimation(i--);
        }
    }

    public void Hide(){
        spawnedElements.Clear();

        foreach(TimeElement element in GetComponentsInChildren<TimeElement>()){
            element.Kill();
        }
    }
}
