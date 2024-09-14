using ProjectBase.Mono;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : SingletonByQing<CharacterManager>
{
    List<Characters> characters = new();

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
        List<Characters> charactersList = new();
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
        List<Characters> charactersList = new();
        foreach (var character in characters)
        {
            if (character.characterData.ID == characterID)
            {
                charactersList.Add(character);
            }
        }
        return charactersList;
    }

    public List<Characters> GetAllCharacters()
    {
        return characters;
    }
}
