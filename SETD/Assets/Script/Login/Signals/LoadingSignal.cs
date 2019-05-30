using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSignal
{
    public string loadingContent { get; private set; }

    public LoadingSignal(string content)
    {
        loadingContent = content;
    }
}
