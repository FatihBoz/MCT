using System;
using System.Collections.Generic;

[Serializable]
public class ModManifest
{
    public string id;
    public string name;
    public string version;
    public string gameVersion;
    public List<string> dependencies = new List<string>();
    public bool isCore;
    [NonSerialized] public string modPath; // Set at runtime
}
