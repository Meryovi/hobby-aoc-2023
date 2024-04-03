const sumOfHashValues = (input: string) => {
  let runningValue = 0;
  return input.split("").reduce((sumOfHashes, char, i) => {
    if (char == ",") {
      sumOfHashes += runningValue; // Flush.
      runningValue = 0;
    } else {
      runningValue += char.charCodeAt(0);
      runningValue = (runningValue * 17) % 256;
    }
    if (i === input.length - 1) sumOfHashes += runningValue;
    return sumOfHashes;
  }, 0);
};

export default sumOfHashValues;
