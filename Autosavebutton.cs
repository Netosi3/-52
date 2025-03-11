using UnityEngine;
using UnityEngine.UI;

public class SaveLoadUI : MonoBehaviour {
    public AutoSaveManager autoSaveManager;
    public Button saveButton;
    public Button loadButton;

    void Start() {
        saveButton.onClick.AddListener(() => autoSaveManager.SaveGame());
        loadButton.onClick.AddListener(() => autoSaveManager.LoadGame());
    }
}
