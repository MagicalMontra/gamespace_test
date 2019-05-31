using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshCharacterListSignal
{
    public List<CharacterData> _characters { get; private set; }

    public RefreshCharacterListSignal(List<CharacterData> characters)
    {
        _characters = characters;
    }
}
