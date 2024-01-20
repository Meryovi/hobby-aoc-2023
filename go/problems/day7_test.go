package problems

import "testing"

var day7 Solvable[int64] = Day7{}

func TestDay7_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day7_1")
	result := day7.Solve(input)

	var expected int64 = 6440
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay7_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day7_2")
	result := day7.Solve(input)

	var expected int64 = 250957639
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
