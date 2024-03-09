using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayer(PlayerHealth playerHealth)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";

        FileStream stream = new FileStream(path, FileMode.Create);

        DataPlayer data = new DataPlayer(playerHealth);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static DataPlayer LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataPlayer data = (DataPlayer) formatter.Deserialize(stream);
            stream.Close();
            return data;
        } else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }

}
