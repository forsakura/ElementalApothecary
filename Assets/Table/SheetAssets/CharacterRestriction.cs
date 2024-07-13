using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExcelAsset,CreateAssetMenu(menuName = "Character/CharacterRestriction")]
public class CharacterRestriction : ScriptableObject
{
	public UnityAction<Character> OnEventRaised;
	public List<CharacterRestrictionEntity> CharacterRestrictionEntity;

	public void RaiseEvent(Character character)
    {
        OnEventRaised?.Invoke(character);
    }
}
