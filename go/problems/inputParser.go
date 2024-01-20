package problems

import (
	"strconv"
	"strings"
)

func parseNumbers(line string, sep string) []int {
	nms := make([]int, 0, 2)

	for _, val := range strings.Split(line, sep) {
		if val = strings.TrimSpace(val); val == "" {
			continue
		}

		if n, err := strconv.Atoi(val); err == nil {
			nms = append(nms, n)
		}
	}

	return nms
}
