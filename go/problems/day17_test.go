package problems

import "testing"

var day17 Solvable[int] = Day17{}

func TestDay17_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day17_1")
	result := day17.Solve(input)

	var expected int = 102
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay17_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day17_2")
	result := day17.Solve(input)

	var expected int = 970
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
