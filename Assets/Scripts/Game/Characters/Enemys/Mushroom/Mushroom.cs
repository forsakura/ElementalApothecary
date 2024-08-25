using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Mushroom
{
    public class Mushroom : EnemyBase
    {
        [Header("SpecialPropertyOfMushroom")]
        [SerializeField]
        private float threshold;

        // Start is called before the first frame update
        void Start()
        {
            CurrentSpeed = 0.0f;
            characterActions.AfterGetHit += CreateArea;
        }

        private void CreateArea(Characters character, HitInstance hit)
        {
            if((1.0f * CurrentHealth / characterData.MaxHealth) < threshold)
            {
                // Create spore area.
                Debug.Log("Create spore area.");
                List<Characters> mushrooms = CharacterManager.Instance.FindCharacters(characterData.ID);
                foreach (Mushroom mushroom in mushrooms)
                {
                    if (mushroom != this)
                    {
                        mushroom.AssembleTo(new Vector2(transform.position.x, transform.position.y));
                    }
                }
                characterActions.AfterGetHit -= CreateArea;
            }
        }

        public void AssembleTo(Vector2 target)
        {
            CurrentSpeed = characterData.MoveSpeed;
        }
    }
} 

