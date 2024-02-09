package problems

import "testing"

var day19 Solvable[int] = Day19{}

func TestDay19_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day19_1")
	result := day19.Solve(input)

	var expected int = 19114
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}

func TestDay19_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day19_2")
	result := day19.Solve(input)

	var expected int = 353046
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
