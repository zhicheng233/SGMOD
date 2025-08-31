﻿namespace SGMOD.Config;

using MelonLoader;
using System.Reflection;
using System.Text.Json;

public static class ConfigManager {
    private static readonly JsonSerializerOptions options = new() {
        WriteIndented = true
    };

    public static MainConfig Load(string path) {
        MainConfig result;
        MainConfig defaults = new(); //默认Config

        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            try {
                var loaded = JsonSerializer.Deserialize<MainConfig>(json, options);
                result = MergeWithDefaults(loaded ?? new MainConfig(), defaults);
                MelonLogger.Msg("Config loaded successfully.");
                Save(path, result);
            }
            catch (Exception e) {
                MelonLogger.Error("配置文件加载错误，已恢复默认设置:" + e.Message);
                result = defaults;
            }
        }
        else {
            result = defaults;
            Save(path, result);
        }

        return result;
    }

    public static void Save(string path, MainConfig config) {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        string json = JsonSerializer.Serialize(config, options);
        File.WriteAllText(path, json);
    }

    private static T MergeWithDefaults<T>(T loaded, T defaults) where T : new() {
        var type = typeof(T);
        var result = new T();

        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
            object? loadedValue = prop.GetValue(loaded);
            object? defaultValue = prop.GetValue(defaults);

            if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string)) {
                //递归获取子对象
                var mergeMethod = typeof(ConfigManager).GetMethod(nameof(MergeWithDefaults), BindingFlags.NonPublic | BindingFlags.Static);
                var genericMergeMethod = mergeMethod!.MakeGenericMethod(prop.PropertyType);
 
                var subLoaded = loadedValue ?? Activator.CreateInstance(prop.PropertyType);
                var subDefault = defaultValue ?? Activator.CreateInstance(prop.PropertyType);
 
                object mergedSub = genericMergeMethod.Invoke(null, new[] { subLoaded, subDefault })!;
                prop.SetValue(result, mergedSub);
            }
            else {
                //非嵌套值
                prop.SetValue(result, loadedValue ?? defaultValue);
            }
        }

        return result;
    }
}