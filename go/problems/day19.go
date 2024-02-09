package problems

import (
	"strconv"
	"strings"
)

type Day19 struct{}

func (d Day19) Solve(input string) int {
	return processAllPartRatings(input)
}

func processAllPartRatings(input string) int {
	wfs := make(map[string]workflow)
	parts := make([]workflowPart, 0)

	wfs[WORKFLOW_ACCEPTED.Code] = WORKFLOW_ACCEPTED
	wfs[WORKFLOW_REJECTED.Code] = WORKFLOW_REJECTED

	parseParts := false
	for _, line := range strings.Split(input, NewLine) {
		if line == "" {
			parseParts = true
			continue
		}

		if !parseParts {
			wf := parseWorkflow(line)
			wfs[wf.Code] = wf
		} else {
			p := parsePart(line)
			parts = append(parts, p)
		}
	}

	rating := 0

	for _, part := range parts {
		wf := wfs[INITIAL_WORKFLOW_NAME]

		for wf.Code != WORKFLOW_ACCEPTED.Code && wf.Code != WORKFLOW_REJECTED.Code {
			next := wf.Process(part)
			wf = wfs[next]

			if wf.Code == WORKFLOW_ACCEPTED.Code {
				rating += part.Rating()
			}
		}
	}

	return rating
}

var INITIAL_WORKFLOW_NAME string = "in"
var WORKFLOW_ACCEPTED workflow = workflow{"A", []workflowRule{}}
var WORKFLOW_REJECTED workflow = workflow{"R", []workflowRule{}}

type workflow struct {
	Code  string
	Rules []workflowRule
}

func parseWorkflow(workflowString string) workflow {
	stepsInx := strings.Index(workflowString, "{")
	code := workflowString[:stepsInx]

	stepsStr := workflowString[(stepsInx + 1) : len(workflowString)-1]
	rulesStrs := strings.Split(stepsStr, ",")

	rules := make([]workflowRule, len(rulesStrs))
	for i, ruleStr := range rulesStrs {
		rules[i] = parseWorkflowRule(ruleStr)
	}

	w := workflow{code, rules}
	return w
}

func (w *workflow) Process(p workflowPart) string {
	for _, rule := range w.Rules {
		if rule.Matches(p) {
			return rule.NextWorkflow
		}
	}
	return w.Rules[len(w.Rules)-1].NextWorkflow
}

type workflowRule struct {
	Category     byte
	Operand      byte
	Value        int
	NextWorkflow string
}

func parseWorkflowRule(workflowRuleStr string) workflowRule {
	sepInx := strings.Index(workflowRuleStr, ":")

	if sepInx == -1 {
		return workflowRule{' ', ' ', 0, workflowRuleStr}
	}

	cat := workflowRuleStr[0]
	opr := workflowRuleStr[1]
	val, _ := strconv.Atoi(workflowRuleStr[2:sepInx])
	next := workflowRuleStr[(sepInx + 1):]

	return workflowRule{cat, opr, val, next}
}

func (wr *workflowRule) Matches(p workflowPart) bool {
	if wr.Category == ' ' || wr.Operand == ' ' || wr.Value == 0 {
		return true
	}

	if wr.Operand == '>' {
		if wr.Category == 'x' {
			return p.X > wr.Value
		} else if wr.Category == 'm' {
			return p.M > wr.Value
		} else if wr.Category == 'a' {
			return p.A > wr.Value
		} else if wr.Category == 's' {
			return p.S > wr.Value
		} else {
			panic("invalid option")
		}
	} else if wr.Operand == '<' {
		if wr.Category == 'x' {
			return p.X < wr.Value
		} else if wr.Category == 'm' {
			return p.M < wr.Value
		} else if wr.Category == 'a' {
			return p.A < wr.Value
		} else if wr.Category == 's' {
			return p.S < wr.Value
		} else {
			panic("invalid option")
		}
	}

	panic("invalid operand")
}

type workflowPart struct {
	X int
	M int
	A int
	S int
}

func parsePart(partString string) workflowPart {
	wp := workflowPart{}
	data := strings.Split(partString[1:len(partString)-1], ",")
	wp.X, _ = strconv.Atoi(data[0][2:])
	wp.M, _ = strconv.Atoi(data[1][2:])
	wp.A, _ = strconv.Atoi(data[2][2:])
	wp.S, _ = strconv.Atoi(data[3][2:])
	return wp
}

func (wp *workflowPart) Rating() int {
	x, m, a, s := wp.X, wp.M, wp.A, wp.S
	return x + m + a + s
}
