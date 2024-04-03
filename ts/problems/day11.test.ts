import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day11.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 11", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day11_1");
    const result = solve(input);
    assert.equal(result, 374);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day11_2");
    const result = solve(input);
    assert.equal(result, 10292708);
  });
});
