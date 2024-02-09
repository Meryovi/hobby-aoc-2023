package problems

import "testing"

var day15 Solvable[int] = Day15{}

func TestDay15_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day15_1")
	result := day15.Solve(input)

	var expected int = 1320
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay15_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day15_2")
	result := day15.Solve(input)

	var expected int = 505379
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
