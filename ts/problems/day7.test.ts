import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day7.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 7", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day7_1");
    const result = solve(input);
    assert.equal(result, 6440);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day7_2");
    const result = solve(input);
    assert.equal(result, 250957639);
  });
});
