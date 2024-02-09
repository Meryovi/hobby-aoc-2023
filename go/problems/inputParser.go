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

func parseNumbers64(line string, sep string) []int64 {
	nms := make([]int64, 0, 2)

	for _, val := range strings.Split(line, sep) {
		if val = strings.TrimSpace(val); val == "" {
			continue
		}

		if n, err := strconv.ParseInt(val, 10, 64); err == nil {
			nms = append(nms, n)
		}
	}

	return nms
}
