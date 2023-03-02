using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyDropdown : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Dropdown _dropdown;

    private void Start()
    {
        _dropdown.value = GameData.Instance.GameDifficulty - 1;
        _dropdown.onValueChanged.AddListener(delegate { OnDropdownValueChanged(); });
    }
    private void OnDropdownValueChanged()
    {
        GameData.Instance.SetDifficulty(_dropdown.value + 1);
        SceneManager.LoadScene("SampleScene");
    }
}
