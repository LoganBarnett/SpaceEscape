using UnityEngine;
using System.Collections;
using System.Linq;

public class Title : MonoBehaviour {
	public Texture title;
	public float startTransitionSeconds = 4.0f;
	public GameObject nova;
	public GameObject navigationContainer;
	public GameObject sun;
	public GameObject corona;
	public GameObject game;
	
	const string CreativeCommons30Url = "http://creativecommons.org/licenses/by/3.0/";
	const string CreativeCommons30NonCommercialUrl = "http://creativecommons.org/licenses/by-nc/3.0/";
	
	MenuMode mode = MenuMode.Title;
	
	enum MenuMode {
		Title,
		Credits
	}

	void StartGame() {
		// Start nova
		nova.active = true;
		sun.active = false;
		corona.active = false;
		
		// Change Music
		var titleMusic = GameObject.Find("Title Music") as GameObject;
		titleMusic.active = false;
		var playMusic = GameObject.Find("Play Music") as GameObject;
		playMusic.audio.Play();
		// Move camera
		var destination = GameObject.Find("Start Camera") as GameObject;
		Camera.main.transform.parent = destination.transform;
		Tweeny.MoveTo(new MoveToArgs
		{
			Duration = startTransitionSeconds,
			Target = Camera.main.gameObject,
			Destination = Vector3.zero
		});
		
//		iTween.moveTo(Camera.main.gameObject, startTransitionSeconds, 0.0f, Vector3.zero);
		// destination.transform.rotation.eulerAngles
		//iTween.rotateTo(Camera.main.gameObject, startTransitionSeconds, 0.0f, Vector3.zero);
		Tweeny.RotateTo(new RotateToArgs
        {
			Target = Camera.main.gameObject,
			Duration = startTransitionSeconds,
			Destination = Vector3.zero
		});
		
		// Place ship behind camera
		var ark = GameObject.Find("Ark Node") as GameObject;
		var arkEntry = GameObject.Find("Ark Entry") as GameObject;
		var arkEntryStop = GameObject.Find("Ark Entry Stop") as GameObject;
		
		ark.transform.parent = destination.transform;
		Tweeny.MoveTo(new MoveToArgs
		{
			Target = ark,
			Delay = startTransitionSeconds,
			Destination = arkEntry.transform.localPosition
		});
//		iTween.moveTo(ark, 0.0f, startTransitionSeconds, arkEntry.transform.localPosition);
//		iTween.rotateTo(ark, 0.0f, startTransitionSeconds, arkEntry.transform.localRotation.eulerAngles);
		Tweeny.RotateTo(new RotateToArgs
        {
			Target = ark,
			Delay = startTransitionSeconds,
			Destination = arkEntry.transform.localRotation.eulerAngles
		});
		// Move ship in front of camera
		Tweeny.MoveTo(new MoveToArgs
        {
			Target = ark,
			Delay = startTransitionSeconds + 0.1f,
			Duration = startTransitionSeconds,
			Destination = arkEntryStop.transform.localPosition
		});
//		iTween.moveTo(ark, startTransitionSeconds, startTransitionSeconds + 0.1f, arkEntryStop.transform.localPosition);
		
		// Place Marker
//		iTween.
		var children = navigationContainer.transform; //.Select(t => t.gameObject);
		foreach (Transform navPoint in children)
		{
			navPoint.gameObject.active = true;
		}
		
		// Place UI
		game.active = true;
		
		
	}

	void OnGUI() {
		switch(mode) {
		case MenuMode.Title:
			TitleGui();
			break;
		case MenuMode.Credits:
			CreditsGui();
			break;
		}
	}
	
	void CreditsGui() {
		GUILayout.BeginHorizontal();
		var spacerWidth = (Screen.width - 227.0f) / 2.0f;
		GUILayout.Space(spacerWidth);
		
		GUILayout.BeginVertical();
		
		GUILayout.Label(title);
		if (GUILayout.Button("Back", GUILayout.Width(title.width)))
		{
			mode = MenuMode.Title;
		}
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		GUILayout.BeginVertical();
		GUILayout.Label("Music:");
		GUILayout.Label("     Winter Reflections - Kevin Macleod (incompetech.com)", GUILayout.ExpandWidth(true));
		GUILayout.Label("     Plans in Motion - Kevin Macleod (incompetech.com)", GUILayout.ExpandWidth(true));
		GUILayout.Label("Artwork:");
		GUILayout.BeginHorizontal();
		GUILayout.Label("     Nova texture - ", GUILayout.ExpandWidth(false));
		if (GUILayout.Button("Poe Tatum", GUILayout.ExpandWidth(false))) {
			Application.OpenURL("http://www.flickr.com/photos/poetatum/3418291075/");
		}
		GUILayout.EndHorizontal();
		GUILayout.Label("     Ship - Logan Barnett");
		GUILayout.Label("     Navigation Ring - Logan Barnett");
		GUILayout.BeginHorizontal();
		GUILayout.Label("     Planetary textures - James Hastings-Trew ", GUILayout.ExpandWidth(false));
		if (GUILayout.Button("Planet Pixel Emporium", GUILayout.ExpandWidth(false))) {
			Application.OpenURL("http://planetpixelemporium.com/planets.html");
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Kevin Macloed's music is distributed under the ", GUILayout.ExpandWidth(false));
		if (GUILayout.Button("Creative Commons 3.0 license", GUILayout.ExpandWidth(false))) {
			Application.OpenURL(CreativeCommons30Url);
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Poe Tatum's artwork is distributed under the ");
		if (GUILayout.Button("Creative Commons 3.0 non-commercial license", GUILayout.ExpandWidth(false))) {
			Application.OpenURL(CreativeCommons30NonCommercialUrl);
		}
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
		
	}
	
	void TitleGui() {
		GUILayout.BeginHorizontal();
		var spacerWidth = (Screen.width - 227.0f) / 2.0f;
		GUILayout.Space(spacerWidth);
		GUILayout.BeginVertical();
		
		GUILayout.Label(title);
		if (GUILayout.Button("Play"))
		{
			// Disable title
			gameObject.active = false;
			StartGame();
		}
		if (GUILayout.Button("Credits"))
		{
			mode = MenuMode.Credits;	
		}
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
	}
}

