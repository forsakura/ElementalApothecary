using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string sceneFrom;
    public string sceneTo;
    public Vector3 posToGo;
    public SceneLoadEventSO sceneLoadEvent;
    

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            sceneLoadEvent.RaiseLoadScenetEvent(sceneFrom,sceneTo,posToGo);
        }
    }

   
}
