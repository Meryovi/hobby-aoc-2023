import { NEW_LINE } from "../inputReader.js";

const processAllPartRatings = (input: string) => {
  const [workflowInput, partsInput] = input.split(NEW_LINE + NEW_LINE);
  const workflowStrings = workflowInput.split(NEW_LINE);
  const partStrings = partsInput.split(NEW_LINE);

  const workflowMap = buildWorkflowMap(workflowStrings);

  return partStrings.map(Part.parse).reduce((acc, part) => {
    // Run the workflow until it reaches either accepted or rejected
    let workflow = workflowMap[INITIAL_WORKFLOW_CODE]!;

    while (workflow !== WORKFLOW_ACCEPTED && workflow !== WORKFLOW_REJECTED) {
      const nextWorkflow = workflow.process(part);
      workflow = workflowMap[nextWorkflow] || WORKFLOW_REJECTED;

      if (workflow === WORKFLOW_ACCEPTED) {
        acc += part.rating;
      }
    }
    return acc;
  }, 0);
};

const buildWorkflowMap = (workflowStrings: string[]) => {
  const workflowMap: Record<string, Workflow> = {};
  workflowStrings.map(Workflow.parse).forEach((workflow) => (workflowMap[workflow.code] = workflow));
  workflowMap[WORKFLOW_ACCEPTED.code] = WORKFLOW_ACCEPTED;
  workflowMap[WORKFLOW_REJECTED.code] = WORKFLOW_REJECTED;
  return workflowMap;
};

class Workflow {
  constructor(public readonly code: string, public readonly rules: WorkflowRule[] = []) {}

  static parse(workflowString: string) {
    const separator = workflowString.indexOf("{");
    const code = workflowString.slice(0, separator);
    const rules = workflowString
      .slice(separator + 1, -1)
      .split(",")
      .map(WorkflowRule.parse);

    return new Workflow(code, rules);
  }

  process(part: Part) {
    const matching = this.rules.find((rule) => rule.matches(part));
    return matching ? matching.nextWorkflow : this.rules.at(-1)!.nextWorkflow;
  }
}

class WorkflowRule {
  constructor(public nextWorkflow: string, public category?: string, public operand?: string, public value?: number) {}

  static parse(stepString: string) {
    const separator = stepString.indexOf(":");
    if (separator === -1) return new WorkflowRule(stepString);

    const [category, operand] = stepString;
    const next = stepString.slice(separator + 1);
    const value = Number(stepString.slice(2, separator));

    return new WorkflowRule(next, category, operand, value);
  }

  matches(part: Part) {
    if (!this.category || !this.operand || !this.value) return true;
    const value = part[this.category as keyof Part];
    return this.operand === ">" ? value > this.value : value < this.value;
  }
}

class Part {
  public readonly rating: number;

  constructor(public readonly x: number, public readonly m: number, public readonly a: number, public readonly s: number) {
    this.rating = this.x + this.m + this.a + this.s;
  }

  static parse(partString: string) {
    const [x, m, a, s] = [...partString.matchAll(/\d+/g)].map((match) => Number(match[0]));
    return new Part(x, m, a, s);
  }
}

const INITIAL_WORKFLOW_CODE = "in";
const WORKFLOW_ACCEPTED = new Workflow("A");
const WORKFLOW_REJECTED = new Workflow("R");

export default processAllPartRatings;
