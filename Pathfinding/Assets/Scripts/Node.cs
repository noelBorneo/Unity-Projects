using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

	// Keep track of other nodes adjacent to the current node
	public List<Node> adjecent = new List<Node>();
	public Node previous = null;  // Use this field to keep track of seen nodes

	public string label = "";

	public void Clear() {  // Reset previous field
		previous = null;
	}
}
