using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    CharacterData data;
    // Start is called before the first frame update
    void Start()
    {
        data = new CharacterData();
        data.InitResources();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
