using System;
using System.Collections.Generic;
using System.Linq;
sealed class SettingsManager
{
    private readonly List<ISettingsModule> _modules;
    
    public SettingsManager(List<ISettingsModule> modules)
    {
        _modules = modules ?? throw new ArgumentNullException(nameof(modules));
    }
    public T GetModule<T>() where T : class, ISettingsModule
    {
        return _modules.OfType<T>().FirstOrDefault();
    }
    
    
    
    public void LoadAllSettings() { }


    public void SaveAllSettings() { }

 
    public void LoadNecessarySettings<T>(T module) where T : ISettingsModule{ module.Load(); }
    
    
    public void SaveNecessarySettings<T>(T module) where T : ISettingsModule { module.Save(); }
}
