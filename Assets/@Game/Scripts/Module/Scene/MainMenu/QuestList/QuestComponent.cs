using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTA.Scene.QuestList
{
    public class QuestComponent : MonoBehaviour
    {
        [SerializeField] private TMP_Text _labelText;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private Slider _slider;

        public TMP_Text LabelText => _labelText;
        public TMP_Text ProgressText => _progressText;
        public Slider Slider => _slider;
    }
}