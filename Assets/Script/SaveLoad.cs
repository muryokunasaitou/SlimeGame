using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveLoad : MonoBehaviour
{
    public float hp;

    public void SaveData(SaveLoad save, string filepath)
    {
        Debug.Log(filepath);
        string json = JsonUtility.ToJson(save, true);
        StreamWriter streamWriter = new StreamWriter(filepath);
        json = Encode.Encrypt(json);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
    }

    public void SaveAndLoad(SaveLoad save, string filepath)
    {
        if (File.Exists(filepath))
        {
            StreamReader streamReader = new StreamReader(filepath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            data = Encode.Decrypt(data);
            JsonUtility.FromJsonOverwrite(data, save);
        }
        elseÅ@//new GemeÇ»ÇÁ
        {
            hp = 3;
        }
    }
}
