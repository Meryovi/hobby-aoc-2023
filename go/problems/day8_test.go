package problems

import "testing"

var day8 Solvable[int] = Day8{}

func TestDay8_TestSet_ShouldYield_Result(t *testing.T) {
	tests := map[string]int{
		"day8_1": 2,
		"day8_2": 6,
	}

	for problem, expected := range tests {
		input := readProblemInput(problem)
		result := day8.Solve(input)

		if result != expected {
			t.Errorf("expected %d but got %d", expected, result)
		}
	}
}

func TestDay8_FullSet_ShouldYield_Result(t *testing.T) {
	input := readProblemInput("day8_3")
	result := day8.Solve(input)

	var expected int = 14893
	if result != expected {
		t.Errorf("expected %d but got %d", expected, result)
	}
}
