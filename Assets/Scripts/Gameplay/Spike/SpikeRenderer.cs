using UnityEngine;

public class SpikeRenderer : ISpriteRenderer
{
    public void UpdateRenderer(SpriteRenderer spriteRenderer, Transform parentTransform)
    {
        if (parentTransform.position.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}