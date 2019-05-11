using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBreakable : MonoBehaviour
{

	public bool unbreakableWhenDry;
    // Start is called before the first frame update
    
	public bool getUWD() {
		return unbreakableWhenDry;
	}
}
