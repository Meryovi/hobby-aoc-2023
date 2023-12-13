package problems

import "testing"

var sut Solvable[int] = Day1{}

func TestDay1_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day1_1")
	result := sut.Solve(input)

	expected := 142
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay1_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day1_2")
	result := sut.Solve(input)

	expected := 55017
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
