using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;
    [SerializeField] private Color _originalColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _propBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        _originalColor = _renderer.material.color;
    }

    void Update()
    {
        // Two lines below are code are identical so will not save memory
        // Each will create a duplicate material and apply the new color to 
        // the duplicated material, thus doubling materials & memory.
        //_renderer.material.color = GetRandomColor();
        //_renderer.material.SetColor("_Color", GetRandomColor());

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ColorSwitch();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(SwitchColor());
        }
    }

    private Color GetRandomColor()
    {
        return new Color(
            r: Random.Range(0f, 1f),
            g: Random.Range(0f, 1f),
            b: Random.Range(0f, 1f));
    }

    IEnumerator SwitchColor()
    {
        var SwapColors = Random.Range(4, 21);
        for (int i = 0; i < SwapColors; i++)
        {
            ColorSwitch();
            yield return new WaitForSeconds(.5f);
        }

        OriginalColor();
    }

    private void ColorSwitch()
    {
        // Get the current value of the material properties in the renderer.
        _renderer.GetPropertyBlock(_propBlock);
        // Assign the new color value
        _propBlock.SetColor(name: "_Color", value: GetRandomColor());
        // Apply the edited values to the renderer.
        _renderer.SetPropertyBlock(_propBlock);
    }

    private void OriginalColor()
    {
        // Get the current value of the material properties in the renderer.
        _renderer.GetPropertyBlock(_propBlock);
        // Assign the new color value
        _propBlock.SetColor(name: "_Color", value: _originalColor);
        // Apply the edited values to the renderer.
        _renderer.SetPropertyBlock(_propBlock);
    }
}