using ProjectBase.Mono;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : SingletonByQing<CharacterManager>
{
    List<Characters> characters = new List<Characters>();

    public CharacterManager()
    {
        characters.Clear();
    }

    public void Register(Characters character)
    {
         characters.Add(character);
    }

    public void Unregister(Characters character)
    {
        characters.Remove(character);
    }

    public List<Characters> FindCharacters(ECharacterType characterType)
    {
        List<Characters> charactersList = new List<Characters>();
        foreach (var character in characters)
        {
            if (character.characterType == characterType)
            {
                charactersList.Add(character);
            }
        }
        return charactersList;
    }

    public List<Characters> FindCharacters(int characterID)
    {
        List<Characters> charactersList = new List<Characters>();
        foreach (var character in characters)
        {
            if (character.characterData.ID == characterID)
            {
                charactersList.Add(character);
            }
        }
        return charactersList;
    }
}
