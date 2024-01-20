package problems

import "testing"

var day6 Solvable[int] = Day6{}

func TestDay6_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day6_1")
	result := day6.Solve(input)

	var expected int = 288
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay6_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day6_2")
	result := day6.Solve(input)

	var expected int = 138915
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
