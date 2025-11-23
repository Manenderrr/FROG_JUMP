using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporting : MonoBehaviour
{
	[Header("Teleporting")]
	public LayerMask player;
	public bool teleport;
	public int toScene;

    void Update() {
        if(teleport == true) {
			SceneManager.LoadScene(toScene);
		}
    }

	private void FixedUpdate() {
		teleport = Physics2D.OverlapCircle(transform.position, 0.38f, player);
	}
}
