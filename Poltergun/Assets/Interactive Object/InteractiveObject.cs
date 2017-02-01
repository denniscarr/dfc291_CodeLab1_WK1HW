using UnityEngine;
using System.Collections;

public class InteractiveObject : MonoBehaviour {

	// Used for getting damaged / breaking
	public bool breakable;
	public float hitPoints = 5f;
	public Color damageColor = Color.gray;
	public GameObject ObjectCollapse;

	SpriteRenderer spriteRenderer;


	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	

	public void GetShot()
	{
		if (!breakable) {
			return;
		}

		hitPoints -= 1;

		// See if I should get destroyed
		if (hitPoints <= 0) {

			Instantiate(ObjectCollapse, transform.position, Quaternion.identity);
			Destroy(gameObject);

			return;
		}

		// If not, edit my color to indicate damage
		Color newColor = new Color(
			Mathf.Lerp(spriteRenderer.color.r, damageColor.r, 1f/hitPoints),
			Mathf.Lerp(spriteRenderer.color.g, damageColor.g, 1f/hitPoints),
			Mathf.Lerp(spriteRenderer.color.b, damageColor.b, 1f/hitPoints)
		);

		Debug.Log(newColor);

		spriteRenderer.color = newColor;
	}
}
