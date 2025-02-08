using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTA.Module.QuestList
{
    public class QuestComponent : MonoBehaviour
    {
        [field: SerializeField]
        public TMP_Text LabelText { get; set; }
        [field: SerializeField]
        public TMP_Text ProgressText { get; set; }
        [field: SerializeField]
        public Slider Slider { get; set; }
    }
}