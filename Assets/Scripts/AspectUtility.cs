using UnityEngine;

public class AspectUtility : MonoBehaviour {
	
	public double _wantedAspectRatio = 1.3333333f;
	static double wantedAspectRatio;
	static Camera cam;
	static Camera backgroundCam;
	
	void Awake () {
		cam = GetComponent<Camera> ();
//		if (!cam) {
//			cam = Camera.main;
//		}
//		if (!cam) {
//			Debug.LogError ("No camera available");
//			return;
//		}
		wantedAspectRatio = _wantedAspectRatio;
		SetCamera();
	}
	
	public static void SetCamera () {
		double currentAspectRatio = ((double)Screen.width) / ((double)Screen.height);

		// If the current aspect ratio is already approximately equal to the desired aspect ratio,
		// use a full-screen Rect (in case it was set to something else previously)
//		if ((int)(currentAspectRatio * 100) / 100.0f == (int)(wantedAspectRatio * 100) / 100.0f) {
//			cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
//			if (backgroundCam) {
//				Destroy(backgroundCam.gameObject);
//			}
//			return;
//		}
		// Pillarbox
		if (currentAspectRatio > wantedAspectRatio) {
			double one = 1.0f;
			double two = 2.0f;
			float inset = (float) (one - wantedAspectRatio/currentAspectRatio);
			double doubleinset = one - wantedAspectRatio/currentAspectRatio;
			Debug.LogWarning("wi: " + (one - wantedAspectRatio/currentAspectRatio));
			cam.rect = new Rect((float)(doubleinset/two), 0.0f, (float) (one-doubleinset), 1.0f);
//			cam.rect = new Rect(0.34175f, 0.0f, 0.3165f, 1.0f); //0.34175f // 0.3165f
		}
		// Letterbox
		else {
//			float inset = 1.0f - currentAspectRatio/wantedAspectRatio;
//			cam.rect = new Rect(0.0f, inset/2, 1.0f, 1.0f-inset);
		}
		if (!backgroundCam) {
			// Make a new camera behind the normal camera which displays black; otherwise the unused space is undefined
			backgroundCam = new GameObject("BackgroundCam", typeof(Camera)).GetComponent<Camera> ();
			backgroundCam.depth = int.MinValue;
			backgroundCam.clearFlags = CameraClearFlags.SolidColor;
			backgroundCam.backgroundColor = Color.black;
			backgroundCam.cullingMask = 0;
		}
	}
	
	public static int screenHeight {
		get {
			return (int) Mathf.Round((Screen.height * cam.rect.height));
		}
	}
	
	public static int screenWidth {
		get {
			return (int) Mathf.Round((Screen.width * cam.rect.width));
		}
	}
	
	public static int xOffset {
		get {
			return (int)Mathf.Round((Screen.width * cam.rect.x));
		}
	}
	
	public static int yOffset {
		get {
			return (int)Mathf.Round((Screen.height * cam.rect.y));
		}
	}
	
	public static Rect screenRect {
		get {
			return new Rect(cam.rect.x * Screen.width, cam.rect.y * Screen.height, cam.rect.width * Screen.width, cam.rect.height * Screen.height);
		}
	}
	
	public static Vector3 mousePosition {
		get {
			Vector3 mousePos = Input.mousePosition;
			mousePos.y -= (int)Mathf.Round((cam.rect.y * Screen.height));
			mousePos.x -= (int)Mathf.Round((cam.rect.x * Screen.width));
			return mousePos;
		}
	}
	
	public static Vector2 guiMousePosition {
		get {
			Vector2 mousePos = Event.current.mousePosition;
			mousePos.y = Mathf.Clamp(mousePos.y, cam.rect.y * Screen.height, cam.rect.y * Screen.height + cam.rect.height * Screen.height);
			mousePos.x = Mathf.Clamp(mousePos.x, cam.rect.x * Screen.width, cam.rect.x * Screen.width + cam.rect.width * Screen.width);
			return mousePos;
		}
	}
}