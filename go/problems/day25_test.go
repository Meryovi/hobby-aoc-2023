package problems

import "testing"

var day25 Solvable[int] = Day25{}

func TestDay25_TestSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day25_1")
	result := day25.Solve(input)

	var expected int = 625
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
