package problems

import "testing"

var day22 Solvable[int] = Day22{}

func TestDay22_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day22_1")
	result := day22.Solve(input)

	var expected int = 5
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay22_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day22_2")
	result := day22.Solve(input)

	var expected int = 534
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
