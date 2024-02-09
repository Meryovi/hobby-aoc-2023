package problems

import "testing"

var day13 Solvable[int] = Day13{}

func TestDay13_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day13_1")
	result := day13.Solve(input)

	var expected int = 405
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay13_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day13_2")
	result := day13.Solve(input)

	var expected int = 29213
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
