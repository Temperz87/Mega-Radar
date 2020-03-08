using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Reflection;
using Harmony;
class LOL : VTOLMOD
{
    public IEnumerator main()
    {
        while (VTMapManager.fetch == null || !VTMapManager.fetch.scenarioReady)
        {
            yield return null;
        }
        foreach (Actor actor in FindObjectsOfType<Actor>())
        {
            FieldInfo lel = actor.GetType().GetField("permanentDiscovery", BindingFlags.Public | BindingFlags.Instance);
            lel.SetValue(actor, true);
        }
        foreach (Actor actor in TargetManager.instance.allActors)
        {
            if (actor.detectedByAllied != true)
            {
                actor.DetectActor(Teams.Allied);
                actor.DiscoverActor();
                Debug.Log("Detected " + actor + " via Mega Radar installation.");
            }
        }
    }
    private void Start()
    {
        ModLoaded();
    }
    public override void ModLoaded()
    {
        VTOLAPI.SceneLoaded += lol;
        base.ModLoaded();
    }
    private void lol(VTOLScenes scenes)
    {
        if (scenes == VTOLScenes.Akutan || scenes == VTOLScenes.CustomMapBase)
        {
            StartCoroutine(main());
        }
    }
}