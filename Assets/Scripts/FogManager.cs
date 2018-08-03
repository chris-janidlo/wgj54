using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogManager : MonoBehaviour {

	public static FogManager Instance;

	public FogSettings CurrentSettings;
	public FogMode GlobalMode;

	void Start () {
		Instance = this;
		ApplySettings(CurrentSettings);
	}

	public void ApplySettings (FogSettings settings) {
		RenderSettings.fogStartDistance = 0;
		RenderSettings.fog = settings.Active;
		RenderSettings.fogColor = settings.Color;
		SetFogDistance(settings.EndDistance);

		CurrentSettings = settings;
	}

	public void Fade (FogSettings target, float time) {
		StartCoroutine(lerp(CurrentSettings, target, time));
	}

	// continuous settings are lerped continuously from original to target over time seconds. discrete settings are switched from original to target after time seconds
	IEnumerator lerp (FogSettings original, FogSettings target, float time) {
		ApplySettings(original);

		float timer = 0;
		while (timer < time) {
			float dist = Mathf.Lerp(original.EndDistance, target.EndDistance, timer / time);
			Color col = Color.Lerp(original.Color, target.Color, timer / time);

			SetFogDistance(dist);
			RenderSettings.fogColor = col;

			timer += Time.deltaTime;
			yield return null;
		}

		ApplySettings(target);
	}

	public void SetFogDistance (float distance) {
		CurrentSettings.EndDistance = distance;

		bool squared = false;
		switch (GlobalMode) {
			case FogMode.Linear:
				RenderSettings.fogEndDistance = distance;
				return;
			case FogMode.Exponential:
				squared = false;
				goto default;
			case FogMode.ExponentialSquared:
				squared = true;
				goto default;
			default:
				float alpha = 1000; // this is arbitrarily large
				float ln = Mathf.Log(alpha);
				RenderSettings.fogDensity = (squared ? Mathf.Sqrt(ln) : ln) / distance;
				return;
		}
	}

}

[System.Serializable]
public struct FogSettings {
	public bool Active;
	public Color Color;
	public float EndDistance;

	// public FogMode Mode; // global
	// public float Density; // abstraced away
	// public float StartDistance; // always 0
}
