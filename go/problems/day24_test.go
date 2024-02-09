package problems

import "testing"

var day24 Solvable[int] = Day24{}

func TestDay24_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day24_1")
	result := day24.Solve(input)

	var expected int = 2
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay24_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day24_2")
	result := day24.Solve(input)

	var expected int = 20336
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
