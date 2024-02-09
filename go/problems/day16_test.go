package problems

import "testing"

var day16 Solvable[int] = Day16{}

func TestDay16_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day16_1")
	result := day16.Solve(input)

	var expected int = 46
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay16_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day16_2")
	result := day16.Solve(input)

	var expected int = 7067
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
