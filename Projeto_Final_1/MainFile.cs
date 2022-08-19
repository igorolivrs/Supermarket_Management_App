using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Projeto_Final_1
{
    public class MainFile<T>
    {
        public string FileName { get; set; }
        public List<T> GetFromFile() // Ler Ficheiro
        {
            List<T> result = new List<T>();
            if (!File.Exists(FileName))
            {
                return new List<T>();
            }
            FileStream fileStream = File.OpenRead(FileName);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            while (fileStream.Position < fileStream.Length)
            {
                result = binaryFormatter.Deserialize(fileStream) as List<T>;
            }
            fileStream.Close();
            return result;
        }

        public void SaveToFile(List<T> ToSave) // Criar Ficheiro
        {
            FileStream fileStream = File.Create(FileName);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, ToSave);
            fileStream.Close();
        }
    }
}