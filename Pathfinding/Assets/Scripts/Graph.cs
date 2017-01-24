using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph {

	// Step 1) Create fields to keep track of the rows and columns of the graph and nodes
	public int rows = 0;
	public int cols = 0;

	public Node[] nodes;

	// Create a graph class constructor
	public Graph(int[,] grid) {  // accepts 2D array type int. Represents a tile map
		rows = grid.GetLength (0);
		cols = grid.GetLength (1);

		// Keep all of the node data as a flat array inside of our graph. 
		// Create enough spaces in the array to match the length of the grid.
		nodes = new Node[grid.Length];

		// Creates an array of nodes
		for (var i = 0; i < nodes.Length; i++) {
			var node = new Node ();	// Creating a node that will go inside of the array
			node.label = i.ToString (); 
			nodes[i] = node;

		}

		// Step 2) Index the grid and build out the node's association
		for (var r = 0; r < rows; r++) {	// Iterate through rows
			for (var c = 0; c < cols; c++) {	// Iterate through cols

				// Formula to figure out how to convert the rows and cols into the correct space in array
				var node = nodes [cols * r + c];	// Get a reference to the node inside the node array

				// Create a condition to test the position in the grid based on its row and col in the loop
				if (grid [r, c] == 1) {  // Test for closed tile [0 - open, 1 - solid]
					continue;
				}

				// -CONNECT- a single node to all of its neighbors
				// Up
				if (r > 0) {	// Add any node above the current node
					// Look at the node's array and find the pos above the current row and col
					node.adjecent.Add (nodes [cols * (r - 1) + c]); 
				}

				// Right 
				if (c < cols - 1) {
					// Look for the node to the right
					node.adjecent.Add (nodes [cols * r + c + 1]);
				}

				// Down
				if (r < rows - 1) {
					// Add the neighbor below
					node.adjecent.Add (nodes [cols * (r + 1) + c]);
				}

				// Left
				if (c > 0) {
					// Add the node to the left
					node.adjecent.Add(nodes[cols*r+c-1]);
				}
				// END-CONNECT
			}
		}
	}
}
