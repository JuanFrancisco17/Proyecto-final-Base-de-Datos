using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

public class SaveGame : MonoBehaviour
{
    [Header("SINGLETON")]

    private static SaveGame instance;

    public static SaveGame Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<SaveGame>();
            }
            return instance;
        }
    }

    public void Save()
    {
        //creamos un jobj que contendrá toda la información necesaria para guardar
        JObject jobj = new JObject();
        //Guardamos el jugador
        jobj.Add("player", PlayerController.Instance.Serialize());
        //Guardamos los puntos actualess
        jobj.Add("points", PointsController.Instance.Serialize());
        //generamos la ruta de guardado para el archivo
        string filePath = Application.persistentDataPath + "/save.sav";
        //encriptamos 
        byte[] encryptedMessage = Encrypt(jobj.ToString());
        File.WriteAllBytes(filePath, encryptedMessage);
        //generamos el archivo de guardado
        Debug.Log("saved in: " + filePath);
    }

    public void Load()
    {
        Debug.Log("loading");
        //cogemos la ruta
        string filePath = Application.persistentDataPath + "/save.sav";

        //desencriptamos
        byte[] decryptedMessage = File.ReadAllBytes(filePath);
        string jsonString = Decrypt(decryptedMessage);

        //deserialzamos
        JObject jobj = JObject.Parse(jsonString);

        PlayerController.Instance.Deserialize(jobj["player"].ToObject<JObject>());
        PointsController.Instance.Deserialize(jobj["points"].ToObject<JObject>());
    }


    byte[] _key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
    byte[] _inicializationVector = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
    byte[] Encrypt(string message)
    {
        //encriptación del archivo
        AesManaged aes = new AesManaged();
        ICryptoTransform encryptor = aes.CreateEncryptor(_key, _inicializationVector);
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        StreamWriter streamWriter = new StreamWriter(cryptoStream);
        streamWriter.WriteLine(message);
        streamWriter.Close();
        cryptoStream.Close();
        memoryStream.Close();
        return memoryStream.ToArray();
    }

    string Decrypt(byte[] message)
    {
        //desencriptación del archivo
        AesManaged aes = new AesManaged();
        ICryptoTransform decrypter = aes.CreateDecryptor(_key, _inicializationVector);
        MemoryStream memoryStream = new MemoryStream(message);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decrypter, CryptoStreamMode.Read);
        StreamReader streamReader = new StreamReader(cryptoStream);
        string decryptedMessage = streamReader.ReadToEnd();
        memoryStream.Close();
        cryptoStream.Close();
        streamReader.Close();
        return decryptedMessage;
    }
}