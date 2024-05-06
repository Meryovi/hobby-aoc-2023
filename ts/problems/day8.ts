import { NEW_LINE } from "../inputReader.js";

const countStepsThroughNodes = (input: string) => {
  const [instructions, _, ...nodeStrings] = input.split(NEW_LINE);
  const nodeList = NodeList.Parse(nodeStrings);

  let steps = 0;
  let node: Node | null = nodeList.GetStartingNode();

  // Wanted to use a recursive function, but set 3 blows up the stack.
  while (node) {
    const instruction = steps % instructions.length;
    node = nodeList.GetNextNode(node, instructions[instruction]);
    steps++;
  }

  return steps;
};

// This one will use classes and stuff...

class NodeList {
  private static START_NODE = "AAA";
  private static END_NODE = "ZZZ";

  #store: Record<string, Node> = {};

  private constructor() {}

  GetStartingNode() {
    return this.#store[NodeList.START_NODE]!;
  }

  GetNextNode(current: Node, direction: string) {
    const nextValue = direction === "L" ? current.left : current.right;
    if (nextValue === NodeList.END_NODE) return null;

    const next = this.#store[nextValue];
    return next;
  }

  static Parse(nodeLists: string[]) {
    const nodeList = new NodeList();
    nodeLists.map(Node.Parse).forEach((node) => nodeList.AddNode(node));
    return nodeList;
  }

  private AddNode(node: Node) {
    this.#store[node.value] = node;
  }
}

class Node {
  value: string;
  left: string;
  right: string;

  constructor(value: string, left: string, right: string) {
    this.value = value;
    this.left = left;
    this.right = right;
  }

  static Parse(nodeString: string) {
    const [value, left, right] = nodeString.match(/[A-Z]{3}/g)!;
    return Object.freeze(new Node(value, left, right));
  }
}

export default countStepsThroughNodes;
