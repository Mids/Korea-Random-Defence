using UnityEngine;

namespace KRD
{
    public interface IRTSManager
    {
        bool GetSelecting();
        void CheckSelecting();
        bool IsWithinSelectionBounds(GameObject gameObject);
    }
}