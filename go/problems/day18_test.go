package problems

import "testing"

var day18 Solvable[int] = Day18{}

func TestDay18_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day18_1")
	result := day18.Solve(input)

	var expected int = 62
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay18_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day18_2")
	result := day18.Solve(input)

	var expected int = 45159
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
