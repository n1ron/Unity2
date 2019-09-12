using UnityEngine;
using UnityEngine.UI;

namespace Geekbrains
{
    public sealed class FlashLightUi : MonoBehaviour
    {
	    private Image _image;

	    private void Awake()
	    {
		    _image = GetComponent<Image>();
	    }

	    public float FillAmount
	    {
		    set => _image.fillAmount = value;
	    }

	    public void SetActive(bool value)
	    {
		    _image.gameObject.SetActive(value);
	    }
    }
}
