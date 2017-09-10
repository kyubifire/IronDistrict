using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

public class DialogueParser : MonoBehaviour
{
    // Struct to represent a line of dialogue.
    struct SpeechLine
    {
        public string name;
        public string content;
        public int pose;
        public string position;

        public SpeechLine(string _name, string _content, int _pose, string _position)
        {
            name = _name;
            content = _content;
            pose = _pose;
            position = _position;
        }
    }

    List<SpeechLine> lines;

    void Start()
    {
        // Form the file that will be read into the game
        string file = "Assets/Data/Dialogue";
        string sceneNum = EditorApplication.currentScene;
        sceneNum = Regex.Replace(sceneNum, "[^0-9]", "");
        file += sceneNum;
        file += ".txt";

        lines = new List<SpeechLine>();

        getSpeech(file);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void getSpeech(string fName)
    {
        string line = "init";
        StreamReader sR = new StreamReader(fName);
        using (sR)
        {
            while (line != null)
            {
                line = sR.ReadLine();
                if (line != null)
                {
                    string[] lineContent = line.Split(';');
                    SpeechLine entry = new SpeechLine(lineContent[0], lineContent[1], int.Parse(lineContent[2]), lineContent[3]);
                    lines.Add(entry);
                }
            }
            sR.Close();
        }
    }

    public string getPosition(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].position;
        }
        return "";
    }

    public string getName(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].name;
        }
        return "";
    }

    public string getContent(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].content;
        }
        return "";
    }

    public int getPose(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].pose;
        }
        return 0;
    }

}
