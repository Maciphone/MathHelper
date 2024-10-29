import React from "react";
import ExercisePageWithStopWatch from "../Components/ExercisePageWithStopWatch";

export default function Division() {
  const operation = "Division";
  const transltedOperation = "Osztás";

  return (
    <ExercisePageWithStopWatch
      operation={operation}
      transltedOperation={transltedOperation}
    />
  );
}
