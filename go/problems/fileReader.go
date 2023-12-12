package problems

import (
	"fmt"
	"log"
	"os"
)

func readDayInput(day int32) string {
	file, err := os.ReadFile(fmt.Sprintf("../input/day%d.txt", day))
	if err != nil {
		log.Fatalf("could not read problem file %d", day)
	}
	return string(file)
}
