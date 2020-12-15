using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace _Project.Scripts.Utils
{
    public class SaveSystem
    {
        private const string Extension = ".nin";
        
        public static void Save<T>(T model) where T : class
        {
            var binaryFormatter = new BinaryFormatter();
            
            var path = $"{Application.persistentDataPath}/{model.GetType().Name}{Extension}";
            var fileStream = new FileStream(path, FileMode.Create);
            
            binaryFormatter.Serialize(fileStream, model);
            
            fileStream.Close();
        }    
        
        public static T Load<T>() where T : class
        {
            var path = $"{Application.persistentDataPath}/{typeof(T).Name}{Extension}";

            if (File.Exists(path))
            {
                var binaryFormatter = new BinaryFormatter();
                
                var fileStream = new FileStream(path, FileMode.Open);
                
                var characterInfo = binaryFormatter.Deserialize(fileStream) as T;
                
                fileStream.Close();
                
                return characterInfo;
            }

            Debug.LogError($"File not found in path {path}");
            return null;
        }
    }
}