import { readDayInput } from "../fileReader.js";

const sumOfTwoDigits = () =>
  readDayInput(1)
    .split("\n")
    .map((str) => {
      let d1 = "";
      let d2 = "";
      for (let i = 0, j = str.length - 1; i < str.length; i++, j--) {
        if (!d1 && Number(str[i])) d1 = str[i];
        if (!d2 && Number(str[j])) d2 = str[j];
        if (d1 && d2) break;
      }
      if (!d1 && !d2) return 0;
      return Number(d1) * 10 + Number(d2);
    })
    .reduce((acc, val) => acc + val, 0);

export default function () {
  console.log(sumOfTwoDigits());
}
