using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System;
using System.IO;
using System.Collections.Generic;

namespace Engine.System;

class SceneManager
{
    public class GameObjectData
    {
        public string Name { get; set; } = "";
        public string Tag { get; set; } = "";
        public List<int> Components { get; set; } = [];
        public bool IsActive { get; set; }
    }
    
    public class ComponentData
    {
        public string Name { get; set; } = "";
        public int GameObject { get; set; }
    }

    public void Load()
    {
        string path = Path.Combine("Game", "Scenes", "ExampleScene.yaml");
        
        if (!File.Exists(path))
        {
            Console.WriteLine($"Файл не найден: {path}");
            return;
        }
        
        string yaml = File.ReadAllText(path);
        
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();
            
        string[] yamlDocuments = yaml.Split(new[] { "\n--- " }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var doc in yamlDocuments)
        {
            if (doc.Contains("GameObject:"))
            {
                var go = deserializer.Deserialize<GameObjectData>(doc);
                Console.WriteLine($"GameObject: {go.Name}");
            }
            else if (doc.Contains("Component:"))
            {
                var comp = deserializer.Deserialize<ComponentData>(doc);
                Console.WriteLine($"Component: {comp.Name}");
            }
        }
    }
}