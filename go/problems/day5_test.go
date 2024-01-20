package problems

import "testing"

var day5 Solvable[int64] = Day5{}

func TestDay5_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day5_1")
	result := day5.Solve(input)

	var expected int64 = 35
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay5_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day5_2")
	result := day5.Solve(input)

	var expected int64 = 486613012
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
