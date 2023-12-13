package problems

import (
	"fmt"
	"log"
	"os"
)

func readDayInput(day uint8) string {
	file, err := os.ReadFile(fmt.Sprintf("../input/day%d.txt", day))
	if err != nil {
		log.Fatalf("could not read problem file day%d.txt", day)
	}
	return string(file)
}
