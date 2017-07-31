using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;
public class LightSource : MonoBehaviour {
	public ParticleSystem particle;
	public float maxSize = .36f;
	public Light light;
	public float maxBrightness;
	public bool burning;
	public float burnRate = 0.05f;
	public float maxHealth = 500f;
	public float health;
	public float normalizedHealth;
	// Use this for initialization
	void Start () {
		if(!light)
			light = GetComponent<Light>();
		particle = GetComponentInChildren<ParticleSystem>();
		Burn();
	}


	public void Burn(){
		if(!burning)
			Timing.RunCoroutine(_TryBurn());
	}

	IEnumerator<float> _TryBurn(){
		burning = true;
		while (health > 0f && burning){
			if(gInput.gameOn){
				health --;
				normalizedHealth = health/maxHealth;
				normalizedHealth = Mathf.Clamp01(normalizedHealth);
				UpdateLightData();
			}
			yield return Timing.WaitForSeconds(burnRate);;
		}

		burning = false;
		yield break;
	}


	public void UpdateLightData(){

		if(light) 
			light.intensity = maxBrightness * normalizedHealth;
		
	}

	public float LightStrength(Vector3 pos){
		distanceFromPos = Vector3.Distance(pos, transform.position);
		lStrength = (light.intensity * (1f-(distanceFromPos/light.range)));
		pMain = particle.main;
		pMain.startSize = Mathf.Lerp(0f,maxSize, normalizedHealth);
		return lStrength;

	}

	private ParticleSystem.MainModule pMain;
	public float lStrength;
	public float distanceFromPos;

}
