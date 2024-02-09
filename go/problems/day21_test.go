package problems

import "testing"

var day21 Solvable[int] = Day21{}

func TestDay21_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day21_1")
	result := day21.Solve(input)

	var expected int = 29
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay21_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day21_2")
	result := day21.Solve(input)

	var expected int = 3776
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
