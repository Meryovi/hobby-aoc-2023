package problems

import (
	"strings"
)

type Day9 struct{}

func (d Day9) Solve(input string) int64 {
	return predictEnvironmentalInstabilities(input)
}

func predictEnvironmentalInstabilities(input string) int64 {
	var total int64 = 0

	for _, reading := range strings.Split(input, NewLine) {
		readings := parseNumbers(reading, " ")
		predict := predictNextSequenceValue(readings)
		total += int64(predict)
	}

	return total
}

func predictNextSequenceValue(seq []int) int {
	reads := len(seq)
	diffs := make([]int, reads-1)

	for i := 1; i < reads; i++ {
		diffs[i-1] = seq[i] - seq[i-1]
	}

	pred := 0

	if diffs[reads-2] != 0 {
		pred = predictNextSequenceValue(diffs)
	}

	return seq[reads-1] + pred
}
