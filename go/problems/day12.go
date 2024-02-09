package problems

import (
	"slices"
	"strings"
)

type Day12 struct{}

func (d Day12) Solve(input string) int {
	return sumOfArrangementCounts(input)
}

func sumOfArrangementCounts(input string) int {
	cnt := 0

	for _, line := range strings.Split(input, NewLine) {
		cnt += parseAndCountArrangement(line)
	}

	return cnt
}

func parseAndCountArrangement(line string) int {
	sep := strings.Index(line, " ")
	arr := []rune(line[:sep])
	grp := parseNumbers(line[sep+1:], ",")
	return getArrangementCount(arr, grp)
}

func getArrangementCount(arr []rune, grp []int) int {
	wildcard := slices.Index(arr, '?')

	if wildcard == -1 {
		if isGroupFulfilled(arr, grp) {
			return 1
		} else {
			return 0
		}
	}

	newArr := slices.Clone(arr)

	newArr[wildcard] = '#'
	hashCnt := getArrangementCount(newArr, grp)

	newArr[wildcard] = '.'
	dotCnt := getArrangementCount(newArr, grp)

	return hashCnt + dotCnt
}

func isGroupFulfilled(arr []rune, grp []int) bool {
	actualGrp := strings.FieldsFunc(string(arr), func(c rune) bool {
		return c == '.'
	})

	if len(grp) != len(actualGrp) {
		return false
	}

	for i := range actualGrp {
		if len(actualGrp[i]) != grp[i] {
			return false
		}
	}

	return true
}
