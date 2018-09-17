using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class IO : MonoBehaviour {
    public static string Read(string path) {
        if (!File.Exists(path)) {
            FileStream fileStream = new FileStream(path, FileMode.Create);
            fileStream.Close();
            return null;
        }
        StreamReader reader = new StreamReader(path);
        string msg = reader.ReadLine();
        reader.Close();
        return msg;
    }

    public static void Write(string path, string msg) {
        if (!File.Exists(path)) {
            FileStream fileStream = new FileStream(path, FileMode.Create);
            fileStream.Close();
        }
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(msg);
        writer.Close();
    }
}
