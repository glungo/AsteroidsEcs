using UnityEngine;

namespace GUI
{
    //TODO: is there a better way to handle GUI on DOTS?
    public class GUIManager : MonoBehaviour
    {
        public static GUIManager Instance { get; private set; }
        [SerializeField] private GameObject[] _managedObjects;
        private void Awake()
        {
            Instance = this;
        }

        public void ToggleGUI(bool show)
        {
            foreach (var obj in _managedObjects)
            {
                obj.SetActive(show);
            }
        }
    }
}