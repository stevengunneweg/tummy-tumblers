using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class Tracker : MonoBehaviour 
{
    public bool updatePosition = false;
    public Transform track;
    public float trackTime = 0.3f;
	
	private List<TrackedPosition>	trackedPositions;
	
	[System.Serializable]
	private class TrackedPosition 
	{
		public 	float 		time		= float.NegativeInfinity;
		public 	Vector3 	position	= Vector3.zero;
		public 	Quaternion 	rotation	= Quaternion.identity;
	}
	
	private void Start()
	{
		trackedPositions = new List<TrackedPosition>(Mathf.FloorToInt(trackTime * 60.0f));
	}
	
    public Vector3 GetOldestTrackedPosition() {
        TrackedPosition oldestTP = trackedPositions.OrderBy(tp => tp.time).FirstOrDefault();
        return oldestTP == null ? Vector3.zero : oldestTP.position;
    }

	protected void Update()
	{
		if (track != null)
		{
			if (Application.isPlaying)
			{
				Save();
				Clean();
                if (updatePosition)
				    Load();
			}
			else if (updatePosition)
				transform.position = track.position;
		}
	}
	
	private void Save()
	{
		TrackedPosition tp = new TrackedPosition();
		
		tp.time = Time.time;
		tp.position = track.position;
		tp.rotation = track.rotation;
		
		trackedPositions.Add(tp);
	}
	
	private void Load()
	{
		TrackedPosition trackedPosition = trackedPositions.FirstOrDefault();
		if (trackedPosition == null)
		{
			transform.position = track.transform.position;
			transform.rotation = track.transform.rotation;
		}
		else
		{
			transform.position = trackedPosition.position;
			transform.rotation = trackedPosition.rotation;
		}
	}
	
	private void Clean()
	{
		trackedPositions.RemoveAll(tp => tp.time < Time.time - trackTime);
	}
	
	private void OnDrawGizmosSelected()
	{
		if (trackedPositions == null)
			return;
		
		Color c = Color.green;
		for (int i = 0; i < trackedPositions.Count; i++)
		{
			c.a = (float)i / (float)trackedPositions.Count;
			Gizmos.color = c;
			Gizmos.DrawSphere(trackedPositions[i].position, 0.1f);
		}
	}
}
