package problems

import "testing"

var day9 Solvable[int64] = Day9{}

func TestDay9_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day9_1")
	result := day9.Solve(input)

	var expected int64 = 114
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay9_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day9_2")
	result := day9.Solve(input)

	var expected int64 = 2043183816
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
