using UnityEngine;
using System.Collections;

public class StringUtils : MonoBehaviour {
	public delegate void Void_String1(string str);
	
	static public IEnumerator ProgressiveString(string str, Void_String1 func, float time)
	{
		string current_string = "";
		for (int i = 0; i != str.Length; ++i)
		{
			current_string += str[i];
			func(current_string);
			yield return new WaitForSeconds(time);
		}
	}


}
