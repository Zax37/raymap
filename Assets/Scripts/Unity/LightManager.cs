﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenSpace;
using System.Linq;
using OpenSpace.Visual;

public class LightManager : MonoBehaviour {
    bool loaded = false;
    public SectorManager sectorManager;
    List<LightInfo> lights;

    [Range(0.0f, 1.0f)]
    public float luminosity = 0.5f;
    private float curLiminosity = 0.5f;

    public bool IsLoaded {
        get { return loaded; }
    }

    // Use this for initialization
    void Start () {
        Shader.SetGlobalFloat("_Luminosity", luminosity);
    }
	
	// Update is called once per frame
	void Update () {
        if (loaded) {
            if (curLiminosity != luminosity) {
                curLiminosity = luminosity;
                Shader.SetGlobalFloat("_Luminosity", luminosity);
            }
            /*if (useFog && Camera.main.renderingPath == RenderingPath.DeferredShading) {
                // Fog doesn't work for deferred
                Camera.main.renderingPath = RenderingPath.Forward;
            }
            bool fogSet = false;
            bool ambientSet = false;
            if (simulateSectorLoading) {
                List<LightInfo> ambientLights = new List<LightInfo>();
                List<LightInfo> fogLights = new List<LightInfo>();
                for (int i = 0; i < lights.Count; i++) {
                    LightInfo l = lights[i];
                    if (l.containingSectors.FirstOrDefault(s => (neighborSectorLights ? s.Loaded : s.Active)) != null) {
                        l.Light.Activate();
                        if (l.type == 6) fogLights.Add(l);
                        if (l.type == 4) ambientLights.Add(l);
                    } else {
                        l.Light.Deactivate();
                    }
                }
                if (useAmbientColor) {
                    Color ambientLight = Color.black;
                    if (ambientLights.Count > 0) {
                        LightInfo l = ambientLights[0];
                        ambientLight = l.color;
                    }
                    RenderSettings.ambientLight = ambientLight;
                }

                if (useFog) {
                    if (fogLights.Count > 0) {
                        LightInfo l = fogLights[0];
                        float minFogDist = Vector3.Distance(fogLights[0].Light.transform.position, Camera.main.transform.position);
                        for (int i = 1; i < fogLights.Count; i++) {
                            float fogDist = Vector3.Distance(fogLights[i].Light.transform.position, Camera.main.transform.position);
                            if (fogDist < l.far && fogDist < minFogDist) {
                                l = fogLights[i];
                            }
                        }
                        RenderSettings.fog = true;
                        RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, l.Light.color, 0.5f * Time.deltaTime);
                        RenderSettings.fogMode = FogMode.Linear;
                        RenderSettings.fogStartDistance = Mathf.Lerp(RenderSettings.fogStartDistance, l.near, 0.5f * Time.deltaTime);
                        RenderSettings.fogEndDistance = Mathf.Lerp(RenderSettings.fogEndDistance, l.far, 0.5f * Time.deltaTime);
                        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, l.Light.backgroundColor, 0.5f * Time.deltaTime);
                        fogSet = true;
                    } else {
                        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, Color.black, 0.5f * Time.deltaTime);
                        RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, Color.black, 0.5f * Time.deltaTime);
                        RenderSettings.fogStartDistance += 50 * Time.deltaTime;
                        RenderSettings.fogEndDistance += 50 * Time.deltaTime;
                        if (RenderSettings.fogStartDistance > 500) {
                            RenderSettings.fog = false;
                        }
                    }
                }
            } else {
                for (int i = 0; i < lights.Count; i++) {
                    LightInfo l = lights[i];
                    l.Light.Activate();
                }
            }*/
        }
	}

    public void Init() {
        lights = MapLoader.Loader.lights;
        for (int i = 0; i < lights.Count; i++) {
            Register(lights[i]);
        }
        loaded = true;
    }

    public void Register(LightInfo light) {
        LightBehaviour l = light.Light;
        l.lightManager = this;
        l.Init();
        l.transform.parent = transform;
    }

    public void RecalculateSectorLighting() {
        if (sectorManager != null) sectorManager.RecalculateSectorLighting();
    }
}
