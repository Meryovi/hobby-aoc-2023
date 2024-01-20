package problems

import "testing"

var day11 Solvable[int64] = Day11{}

func TestDay11_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day11_1")
	result := day11.Solve(input)

	var expected int64 = 374
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay11_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day11_2")
	result := day11.Solve(input)

	var expected int64 = 10292708
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
