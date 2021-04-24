using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Proyecto26;

public static class SaveLoad
{
    public static void Save(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.game";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
        //upload the save file
        DatabaseHandler.onPushSaveFile(path);
    }


    public static PlayerData Load()
    {
        string path = Application.persistentDataPath + "/save.game";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            return new PlayerData();   //new save
        }
    }


}
