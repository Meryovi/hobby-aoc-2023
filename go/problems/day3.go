package problems

import (
	"strconv"
	"strings"
)

type Day3 struct{}

func (d Day3) Solve(input string) int {
	return extractPartNumber(input)
}

func extractPartNumber(input string) int {
	matrix := strings.Split(input, NewLine)
	partNo := 0

	for j, line := range matrix {
		symbol := false
		accum := 0
		for i, char := range line {
			num, err := strconv.Atoi(string(char))
			if err == nil {
				accum = accum*10 + num
				if !symbol {
					symbol = hasAdjacentSymbol(matrix, j, i)
				}
			} else if accum > 0 {
				if symbol {
					partNo += accum
				}
				accum = 0
				symbol = false
			}
		}
		if symbol {
			partNo += accum
		}
	}

	return partNo
}

func hasAdjacentSymbol(matrix []string, col int, row int) bool {
	size := len(matrix)
	for y := max(0, col-1); y <= min(size-1, col+1); y++ {
		for x := max(0, row-1); x <= min(size-1, row+1); x++ {
			_, err := strconv.Atoi(string(matrix[y][x]))
			if (x != row || y != col) && matrix[y][x] != '.' && err != nil {
				return true
			}
		}
	}
	return false
}
