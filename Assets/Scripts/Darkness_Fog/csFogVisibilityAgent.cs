/*
 * Created :    Winter 2022
 * Author :     SeungGeon Kim (keithrek@hanmail.net)
 * Project :    FogWar
 * Filename :   csFogVisibilityAgent.cs (non-static monobehaviour module)
 * 
 * All Content (C) 2022 Unlimited Fischl Works, all rights reserved.
 */

/*
 * This script is just an example of what you can do with the visibility check interface.
 * You can create whatever agent that you want based on this script.
 * Also, I recommend you to change the part where the FogWar module is fetched with Find()...
 */



using UnityEngine;                  // Monobehaviour
using System.Collections.Generic;   // List
using System.Linq;                  // ToList



namespace FischlWorks_FogWar
{



    /// An example of an monobehaviour agent that utilizes the public interfaces of csFogWar class.

    /// Fetches all MeshRenderers and SkinnedMeshRenderers of child objects,\n
    /// then enables / disables them based on each FogRevealer's FOV.
    public class csFogVisibilityAgent : MonoBehaviour
    {
        [SerializeField]
        private csFogWar fogWar = null;

        [SerializeField]
        LayerMask CheckLayers;

        [SerializeField]
        private bool visibility = false;

        [SerializeField]
        bool HighQuility = false;

        [SerializeField]
        private Vector2Int additionalAera;

        private List<SpriteRenderer> spriterenders = null;



        private void Start()
        {
            // This part is meant to be modified following the project's scene structure later...
            try
            {
                fogWar = GameObject.Find("Darkness").GetComponent<csFogWar>();
            }
            catch
            {
                Debug.LogErrorFormat("Failed to fetch csFogWar component. " +
                    "Please rename the gameobject that the module is attachted to as \"FogWar\", " +
                    "or change the implementation located in the csFogVisibilityAgent.cs script.");
            }

            spriterenders = GetComponentsInChildren<SpriteRenderer>().ToList();
        }



        private void Update()
        {
            if (fogWar == null || fogWar.CheckWorldGridRange(transform.position) == false)
            {
                return;
            }

            visibility = fogWar.CheckVisibility(transform.position, additionalAera, HighQuility, CheckLayers);

            foreach (SpriteRenderer renderer in spriterenders)
            {
                renderer.enabled = visibility;
            }
        }



#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (fogWar == null || Application.isPlaying == false)
            {
                return;
            }

            if (fogWar.CheckWorldGridRange(transform.position) == false)
            {
                Gizmos.color = Color.red;

                Gizmos.DrawWireCube(
                 new Vector3(
                    Mathf.RoundToInt(transform.position.x),
                    Mathf.RoundToInt(transform.position.y),
                    0),
                    new Vector3
                    (fogWar._UnitScale / 2.0f + additionalAera.x,
                    fogWar._UnitScale / 2.0f + additionalAera.y,
                    fogWar._UnitScale / 2.0f));

                return;
            }

            if (visibility == true)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.yellow;
            }

            Gizmos.DrawWireCube(
             new Vector3(
                Mathf.RoundToInt(transform.position.x),
                Mathf.RoundToInt(transform.position.y),
                0),
                new Vector3
                (fogWar._UnitScale / 2.0f + additionalAera.x,
                fogWar._UnitScale / 2.0f + additionalAera.y,
                fogWar._UnitScale / 2.0f));
        }
#endif
    }



}