using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeDropdown : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Dropdown _dropdown;

    private void Start()
    {
        _dropdown.value = GameData.Instance.GameMode;
        _dropdown.onValueChanged.AddListener(delegate { OnDropdownValueChanged(); });
    }
    private void OnDropdownValueChanged()
    {
        GameData.Instance.SetGameMode(_dropdown.value);
        SceneManager.LoadScene("SampleScene");
    }
}
