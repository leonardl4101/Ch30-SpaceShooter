using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public Vector2  rotMinMax = new Vector2(15,90);
	public Vector2  driftMinMax = new Vector2(.25f,2);
	public float  lifeTime = 6f; // Seconds the PowerUp exists
	public float  fadeTime = 4f; // Seconds it will then fade
	public bool ___________;
	public WeaponType type; // The type of the PowerUp
	public GameObject  cube; // Reference to the Cube child
	public TextMesh  letter; // Reference to the TextMesh
	public Vector3  rotPerSecond; // Euler rotation speed
	public float  birthTime;

	void Awake() {
		cube = transform.Find("Cube").gameObject;
		letter = GetComponent<TextMesh>();

		Vector3 vel = Random.onUnitSphere;
		vel.z = 0; // Flatten the vel to the XY plane
		vel.Normalize(); // Make the length of the vel 1
		vel *= Random.Range(driftMinMax.x, driftMinMax.y);
		GetComponent<Rigidbody>().velocity = vel;
		
		transform.rotation = Quaternion.identity;

		rotPerSecond = new Vector3(Random.Range(rotMinMax.x,rotMinMax.y),Random.Range(rotMinMax.x,rotMinMax.y), Random.Range(rotMinMax.x,rotMinMax.y));

		InvokeRepeating( "CheckOffscreen", 2f, 2f );
		
		birthTime = Time.time;
	}
	
	void Update () {
		cube.transform.rotation = Quaternion.Euler( rotPerSecond*Time.time );

		float u = (Time.time - (birthTime+lifeTime)) / fadeTime;
		if (u >= 1) {
			Destroy( this.gameObject );
			return;
		}

		if (u>0) {
			Color c = cube.GetComponent<Renderer>().material.color;
			c.a = 1f-u;
			cube.GetComponent<Renderer>().material.color = c;
			c = letter.color;
			c.a = 1f - (u*0.5f);
			letter.color = c;
		}
	}
	public void SetType(WeaponType wt ) {
		WeaponDefinition def = Main.GetWeaponDefinition( wt );
		cube.GetComponent<Renderer>().material.color = def.color;

		letter.text = def.letter;
		type = wt;
	}
	
	public void AbsorbedBy( GameObject target ) {
		Destroy( this.gameObject );
	}
	
	void CheckOffscreen() {
		if ( Utils.ScreenBoundsCheck( cube.GetComponent<Collider>().bounds, BoundsTest.offScreen) != Vector3.zero ) {
			Destroy( this.gameObject );
		}
	}

}
