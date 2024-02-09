package problems

import "testing"

var day12 Solvable[int] = Day12{}

func TestDay12_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day12_1")
	result := day12.Solve(input)

	var expected int = 21
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay12_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day12_2")
	result := day12.Solve(input)

	var expected int = 7017
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
