using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    DialogueParser parser;

    public string speech;
    public string characterName;
    public int lineNum;
    int pose;
    string position;
    List<Button> buttons = new List<Button>();

    public Text dialBox;
    public Text nameBox;
    // Use this for initialization
    void Start() {
        //inits
        speech = "";
        characterName = "";
        pose = 0;
        position = "L";
        parser = GameObject.Find("Parser").GetComponent<DialogueParser>();
        lineNum = 0;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            showSpeech();
            lineNum++;
        }
        updateUI();
    }

    public void showSpeech() {
        resetImages();
        parseLine();
    }

    void resetImages() {
        if (characterName != "") {
            GameObject character = GameObject.Find(characterName);
            SpriteRenderer currSprite = character.GetComponent<SpriteRenderer>();
            currSprite.sprite = null;
        }
    }

    void parseLine() {
        characterName = parser.getName(lineNum);
        speech = parser.getContent(lineNum);
        pose = 0;
        position = parser.getPosition(lineNum);
        displayImages();
    }

    void displayImages() {
        if (characterName != "") {
            GameObject character = GameObject.Find(characterName);
            setSpritePos(character);

            SpriteRenderer currSprite = character.GetComponent<SpriteRenderer>();
            currSprite.sprite = character.GetComponent<Character>().characterPoses[pose];
        }
    }

    void setSpritePos(GameObject spriteObj) {
        if (position == "L")
        {
            spriteObj.transform.position = new Vector2(-6, 200);
        }
        else if (position == "R")
        {
            spriteObj.transform.position = new Vector2(6, 200);
        }
        spriteObj.transform.position = new Vector2(spriteObj.transform.position.x, spriteObj.transform.position.y);
    }

    void updateUI() {
        dialBox.text = speech;
        nameBox.text = characterName;
    }
}
