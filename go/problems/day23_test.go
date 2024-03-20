package problems

import "testing"

var day23 Solvable[int] = Day23{}

func TestDay23_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day23_1")
	result := day23.Solve(input)

	var expected int = 94
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay23_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day23_2")
	result := day23.Solve(input)

	var expected int = 2362
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
