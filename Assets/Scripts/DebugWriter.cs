using UnityEngine;
using System.IO;

public class DebugWriter : MonoBehaviour {

    private string sysFilePath;

	void Start () {
        sysFilePath = Application.persistentDataPath + "/foodLogs.txt";

        if (File.Exists(sysFilePath))
        {
            try
            {
                File.Delete(sysFilePath);
                Debug.Log("File deleted");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Cannot delete the file : "+e.Message);
            }
        }
	}
	
	public void WriteToFile(string message)
    {
        try
        {
            StreamWriter fileWriter = new StreamWriter(sysFilePath, true);

            fileWriter.Write(message+"\n");
            fileWriter.Close();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Cannot write into the file");
        }
        
    }
}
