package problems

import "testing"

var day3 Solvable[int] = Day3{}

func TestDay3_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day3_1")
	result := day3.Solve(input)

	expected := 4361
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay3_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day3_2")
	result := day3.Solve(input)

	expected := 533784
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
