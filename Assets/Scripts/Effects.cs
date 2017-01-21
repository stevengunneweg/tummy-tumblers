using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Effects : MonoBehaviour {
    
    public static Effects instance {
        get {
            return GameObject.FindGameObjectsWithTag("Effects").Where(g => g.GetComponent<Effects>() != null).Select(g => g.GetComponent<Effects>()).FirstOrDefault();
        }
    }

    public enum EffectType {
        FireWorks,
		PlayerDetect,
        Explosion
    }

    [System.Serializable]
	public class Effect {
        public EffectType effectType;
        public GameObject[] variationPrefabs;
        public float time = 3f;
        
        public GameObject GetRandomVariationPrefab() {
            return variationPrefabs[Random.Range(0, variationPrefabs.Length)];
        }
    }
    public Effect[] effects;

    public GameObject Do(EffectType effectType, Vector3 worldPosition) { return Do(effectType, worldPosition, Quaternion.identity); }
    public GameObject Do(EffectType effectType, Vector3 worldPosition, Quaternion worldRotation, Transform parent = null) {
        Effect effect = effects.Where(e => e.effectType == effectType).FirstOrDefault();
        if (effect == null) {
            Debug.LogWarning("Effect of type '" + effectType.ToString() + "' does not exist!");
        }
        GameObject instance = Instantiate(effect.GetRandomVariationPrefab(), worldPosition, worldRotation, parent == null ? transform : parent);
        Destroy(instance, effect.time);
        return instance;
    }
}
