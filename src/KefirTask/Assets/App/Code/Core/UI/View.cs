using UnityEngine;

namespace App.Code.Core.UI
{
    public abstract class View : MonoBehaviour
    {
        public void Show() =>
            gameObject.SetActive(true);

        public void Hide() =>
            gameObject.SetActive(false);
        
        public abstract void ResetView();
    }
}