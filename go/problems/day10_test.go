package problems

import "testing"

var day10 Solvable[int] = Day10{}

func TestDay10_TestSet_ShouldYield_Result(t *testing.T) {
	tests := map[string]int{
		"day10_1": 4,
		"day10_2": 8,
	}

	for problem, expected := range tests {
		input := readProblemInput(problem)
		result := day10.Solve(input)

		if result != expected {
			t.Errorf("expected %d but got %d", expected, result)
		}
	}
}

func TestDay10_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day10_3")
	result := day10.Solve(input)

	var expected int = 6867
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
