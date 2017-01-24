using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search {

	// Field in reference to the graph that will be searched
	public Graph graph;

	// Create 3 Lists that are type Node
	public List<Node> reachable;
	public List<Node> explored;
	public List<Node> path;

	// Node to stop search
	public Node goalNode;

	// Number of iterations in a path
	public int iterations;

	// Boolean if search has finished
	public bool finished;

	public Search(Graph graph) {
		this.graph = graph;
	}

	// Start the search
	public void Start(Node start, Node goal) {
		reachable = new List<Node> ();  // Keep track of the reachable nodes
		reachable.Add (start); // Add 'start' node to the reachable list

		goalNode = goal;

		explored = new List<Node> (); // Nodes that have explored during the search
		path = new List<Node> (); // Path taken through the graph

		iterations = 0; // Reset number of iterations

		// Clear the nodes in the graph prior to searching
		for (var i = 0; i < graph.nodes.Length; i++) {
			graph.nodes [i].Clear ();
		}
	}

	// Step through one solution at a time
	// A solution is going to be testing a particular node and its neighbors for the next possible move to make
	public void Step() {
		if (path.Count > 0) // Exit function if a path to goal has been made
			return;
		
		if (reachable.Count == 0) { // Exit function if no more options
			finished = true;
			return;
		}

		iterations ++;

		var node = ChooseNode (); // Select a random node
		if (node == goalNode) { // Find a path into how goal was reached
			while (node != null) {	// Start from goal to the beginning [null reference]
				path.Insert(0, node); // Insert the current node to the path
				node = node.previous; // Keep track of previous node
			}

			finished = true;
			return;
		}
			

		reachable.Remove (node); // remove because the current node is not the destination
		explored.Add(node); // add current node to explored

		// Iterate through all of the adjacent nodes attached to the current node
		for (var i = 0; i < node.adjecent.Count; i++) { 
			AddAdjacent (node, node.adjecent [i]);
		}
	}

	// Populate next possible solutions
	public void AddAdjacent(Node node, Node adjecent) {
		if (FindNode (adjecent, explored) || FindNode (adjecent, reachable)) { // if true, found a new path
			return;
		}

		adjecent.previous = node; // push node to the previous
		reachable.Add (adjecent); // add node to reachable list
	}

	// helper method to complete AddAjacent
	public bool FindNode(Node node, List<Node> list) {
		return GetNodeIndex(node, list) >= 0;
	}

	// iterate through list of node and see if node exists
	public int GetNodeIndex(Node node, List<Node> list) {
		for (var i = 0; i < list.Count; i++) {
			if (node == list [i]) {
				return i;
			}
		}

		return -1;
	}

	// Pick a node to start searching with
	public Node ChooseNode() {
		return reachable [Random.Range (0, reachable.Count)];	// Return a random node form a reachable list
	}

}
