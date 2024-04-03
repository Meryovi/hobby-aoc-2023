import { NEW_LINE } from "../inputReader.js";

const sumOfArrangementCounts = (input: string) =>
  input
    .split(NEW_LINE)
    .map(processArrangementString)
    .reduce((sum, val) => sum + val, 0);

const processArrangementString = (arrangementString: string) => {
  const [arrangement, values] = arrangementString.split(" ");
  const group = values.split(",").map(Number);
  return getArrangementCount(arrangement, group);
};

const getArrangementCount = (arrangement: string, group: number[]): number => {
  const wildcard = arrangement.indexOf("?");

  if (wildcard == -1) return isGroupFulfilled(arrangement, group) ? 1 : 0;

  const variation1 = replaceCharAt(arrangement, wildcard, "#");
  const variation2 = replaceCharAt(arrangement, wildcard, ".");

  return getArrangementCount(variation1, group) + getArrangementCount(variation2, group);
};

const isGroupFulfilled = (input: string, groups: number[]) => {
  const actualGroups = input.split(".").filter((grp) => !!grp);
  return actualGroups.length === groups.length && actualGroups.every((actual, i) => actual.length === groups[i]);
};

const replaceCharAt = (str: string, index: number, replacement: string) =>
  str.substring(0, index) + replacement + str.substring(index + replacement.length);

export default sumOfArrangementCounts;
