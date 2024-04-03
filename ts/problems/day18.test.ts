import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day18.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 18", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day18_1");
    const result = solve(input);
    assert.equal(result, 62);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day18_2");
    const result = solve(input);
    assert.equal(result, 45159);
  });
});
