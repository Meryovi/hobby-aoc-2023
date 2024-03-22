package problems

import (
	"math"
	"strconv"
	"strings"
)

type Day5 struct{}

func (d Day5) Solve(input string) int64 {
	return findLowestSeedLocation(input)
}

func findLowestSeedLocation(input string) int64 {
	rngMaps := make([][]day5Range, 7)
	currMap := -1

	instructions := strings.Split(input, NewLine)

	for i, line := range instructions {
		if i < 2 || line == "" {
			continue
		}

		if strings.HasSuffix(line, ":") {
			currMap++
			rngMaps[currMap] = make([]day5Range, 0, 2)
		} else {
			rngMap := createDay5Range(line)
			rngMaps[currMap] = append(rngMaps[currMap], rngMap)
		}
	}

	var lowest int64 = math.MaxInt64
	seeds := instructions[0][strings.Index(instructions[0], ":")+2:]

	for _, seedStr := range strings.Split(seeds, " ") {
		dest, _ := strconv.ParseInt(seedStr, 10, 64)

		for _, ranges := range rngMaps {
			dest += getDiffForValueInRange(ranges, dest)
		}

		lowest = min(lowest, dest)
	}

	return lowest
}

func getDiffForValueInRange(ranges []day5Range, value int64) int64 {
	for _, rng := range ranges {
		if value >= rng.Start && value <= rng.End {
			return rng.Diff
		}
	}
	return 0
}

type day5Range struct {
	Start int64
	End   int64
	Diff  int64
}

func createDay5Range(rangeStr string) day5Range {
	sep1 := strings.Index(rangeStr, " ")
	sep2 := strings.LastIndex(rangeStr, " ")

	dest, _ := strconv.ParseInt(rangeStr[:sep1], 10, 64)
	src, _ := strconv.ParseInt(rangeStr[sep1+1:sep2], 10, 64)
	rang, _ := strconv.ParseInt(rangeStr[sep2+1:], 10, 64)

	return day5Range{src, src + rang - 1, dest - src}
}
