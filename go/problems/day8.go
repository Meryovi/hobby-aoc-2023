package problems

import "strings"

type Day8 struct{}

func (d Day8) Solve(input string) int {
	return countStepsThroughNodes(input)
}

func countStepsThroughNodes(input string) int {
	instructions := strings.Split(input, NewLine)
	nl := newNodeList()

	for i := 2; i < len(instructions); i++ {
		nodeStr := instructions[i]
		nl.AppendNode(nodeStr)
	}

	steps := 0
	node := nl.GetStartingNode()

	for node != nil {
		inst := steps % len(instructions[0])
		node = nl.GetNextNode(node, instructions[0][inst])
		steps++
	}

	return steps
}

type day8NodeList struct {
	store map[string]day8Node
}

const startNodeVal = "AAA"
const endNodeVal = "ZZZ"

func newNodeList() *day8NodeList {
	nl := day8NodeList{}
	nl.store = make(map[string]day8Node)
	return &nl
}

func (nl *day8NodeList) AppendNode(nodeStr string) {
	node := parseNode(nodeStr)
	nl.store[node.Value] = node
}

func (nl *day8NodeList) GetNextNode(current *day8Node, dir byte) *day8Node {
	nextVal := current.Right
	if dir == 'L' {
		nextVal = current.Left
	}

	next, ok := nl.store[nextVal]
	if !ok || next.Value == endNodeVal {
		return nil
	}

	return &next
}

func (nl *day8NodeList) GetStartingNode() *day8Node {
	first := nl.store[startNodeVal]
	return &first
}

type day8Node struct {
	Value string
	Left  string
	Right string
}

func parseNode(nodeStr string) day8Node {
	value := nodeStr[:3]
	left := nodeStr[7:10]
	right := nodeStr[12:15]

	return day8Node{value, left, right}
}
