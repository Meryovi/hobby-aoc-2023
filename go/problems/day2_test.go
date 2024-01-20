package problems

import "testing"

var day2 Solvable[int] = Day2{}

func TestDay2_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day2_1")
	result := day2.Solve(input)

	expected := 8
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay2_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day2_2")
	result := day2.Solve(input)

	expected := 2331
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
