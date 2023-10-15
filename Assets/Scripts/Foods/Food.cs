using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

[CreateAssetMenu(fileName = "Food", menuName = "TH/Food", order = 0)]
public class Food : ScriptableObject
{
    public string foodName;
	public Sprite sprite;
	public GameObject prefab;
	public Food chopped;
	public Food inBowl;
}

}