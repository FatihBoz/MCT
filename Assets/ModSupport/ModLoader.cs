using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using HypeAPI;

public class ModLoader : MonoBehaviour
{
    [SerializeField] private string modsFolder = "Mods";
    private List<IMod> loadedMods = new List<IMod>();
    private List<ModManifest> allManifests = new List<ModManifest>();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        LoadMods();
    }
    void LoadMods()
    {
        string modsPath = Path.Combine(Application.dataPath,modsFolder);
        if (!Directory.Exists(modsPath)) Directory.CreateDirectory(modsPath);

        LoadAllManifests(modsPath);

        List<ModManifest> sortedManifests = SortModsByDependencies(allManifests);

        foreach (ModManifest manifest in sortedManifests)
        {
            string modPath = Path.Combine(modsPath, manifest.id);
            LoadModAssemblies(modPath);
        }
        
        foreach (IMod mod in loadedMods)
        {
            try
            {
                mod.OnEnable();
                Debug.Log($"Initialized mod: {mod.GetType().Name}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to enable mod: {e}");
            }
        }   
    }

    private void LoadAllManifests(string modsPath)
    {
        foreach (string modDir in Directory.GetDirectories(modsPath))
        {
            string manifestPath = Path.Combine(modDir, "manifest.json");
            if (File.Exists(manifestPath))
            {
                try
                {
                    ModManifest manifest = JsonUtility.FromJson<ModManifest>(File.ReadAllText(manifestPath));
                    manifest.modPath = modDir;
                    allManifests.Add(manifest);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to parse manifest in {modDir}: {e}");
                }
            }
        }
    }
    private void LoadModAssemblies(string modPath)
    {
        foreach (string dllPath in Directory.GetFiles(modPath, "*.dll"))
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(dllPath);
                LoadModTypes(assembly);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load {Path.GetFileName(dllPath)}: {e}");
            }
        }
    }

    private void LoadModTypes(Assembly assembly)
    {
        foreach (Type type in assembly.GetTypes())
        {
            if (typeof(IMod).IsAssignableFrom(type) && !type.IsAbstract)
            {
                try
                {
                    IMod mod = (IMod)Activator.CreateInstance(type);
                    loadedMods.Add(mod);
                    Debug.Log($"Loaded mod: {type.Name}");
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to instantiate {type.Name}: {e}");
                }
            }
        }
    }
    private List<ModManifest> SortModsByDependencies(List<ModManifest> manifests)
    {
        List<ModManifest> sorted = new List<ModManifest>();
        HashSet<string> resolved = new HashSet<string>();

        // Core mods first (e.g., HarmonyLib)
        foreach (ModManifest manifest in manifests.Where(m => m.isCore))
        {
            sorted.Add(manifest);
            resolved.Add(manifest.id);
        }

        // Resolve remaining dependencies
        bool progress;
        do
        {
            progress = false;
            foreach (ModManifest manifest in manifests)
            {
                if (sorted.Contains(manifest)) continue;

                bool dependenciesMet = manifest.dependencies.All(d => resolved.Contains(d));
                if (dependenciesMet)
                {
                    sorted.Add(manifest);
                    resolved.Add(manifest.id);
                    progress = true;
                }
            }
        } while (progress);

        // Error: Unresolved dependencies
        foreach (ModManifest manifest in manifests)
        {
            if (!sorted.Contains(manifest))
            {
                Debug.LogError($"Unresolved dependencies for mod: {manifest.id}");
            }
        }

        return sorted;
    }

    void OnDestroy()
    {
        foreach (IMod mod in loadedMods)
        {
            try
            {
                mod.OnDisable();
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to disable mod: {e}");
            }
        }
    }
}
