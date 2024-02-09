package problems

import "testing"

var day14 Solvable[int] = Day14{}

func TestDay14_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day14_1")
	result := day14.Solve(input)

	var expected int = 136
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay14_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day14_2")
	result := day14.Solve(input)

	var expected int = 107142
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
