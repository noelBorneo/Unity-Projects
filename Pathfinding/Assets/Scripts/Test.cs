using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public class Test : MonoBehaviour {

	public GameObject mapGroup;
	List<string> list;
	int i;
	// Use this for initialization
	void Start () {
		int[,] map = new int[5, 5]{
			{0,1,0,0,0},
			{0,1,0,1,0},
			{0,1,0,1,0},
			{0,1,0,1,0},
			{0,0,0,1,0}
		};


		list = new List<string>();
		i = 0;
		var graph = new Graph (map);

		var search = new Search (graph);
		search.Start (graph.nodes [0], graph.nodes [24]);

		while (!search.finished) {
			search.Step();
			pathSearch (search);

		}

		print ("Search done. Path length " + search.path.Count + " iterations " + search.iterations);
		ResetMapGroup (graph);
	}

	Image GetImage(string label) {
		var id = Int32.Parse (label);
		var go = mapGroup.transform.GetChild (id).gameObject;
		return go.GetComponent<Image> ();
	}

	void ResetMapGroup(Graph graph) {
		foreach (var node in graph.nodes) {
			GetImage (node.label).color = node.adjecent.Count == 0 ? Color.white : Color.gray;
		}
	}

	void pathSearch(Search search) {
		foreach (var node in search.path) {
			list.Add (node.label);
			print ("path " + node.label + " added"); 
		}
	}

	// Update is called once per frame
	void Update () {

		if (i < list.Count) {
			GetImage (list [i]).color = Color.red;
			i++;
		}

	}
}
