using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimetrailUI : MonoBehaviour {

    [SerializeField]
    private TimeElement elementTemplate;

    private void Start(){
        elementTemplate.gameObject.SetActive(false);
    }

    public void SpawnElement(Player player, float time, int position){
        TimeElement element = Instantiate(elementTemplate, elementTemplate.transform.parent);
        element.Show();
        element.background.color = player.color;
        element.time.text = time.ToString("F1") + " <size='27'>sec</size>";
        element.position.text = position + ".";
    }

    public void Hide(){
        foreach(TimeElement element in GetComponentsInChildren<TimeElement>()){
            element.Kill();
        }
    }
}
