package problems

import "testing"

var day4 Solvable[int] = Day4{}

func TestDay4_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day4_1")
	result := day4.Solve(input)

	expected := 13
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay4_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day4_2")
	result := day4.Solve(input)

	expected := 17803
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
