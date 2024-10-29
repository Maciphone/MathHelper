import React from "react";
import ExercisePageWithStopWatch from "../Components/ExercisePageWithStopWatch";

export default function First() {
  const operation = "Division";
  const transltedOperation = "osztás";

  return (
    <ExercisePageWithStopWatch
      operation={operation}
      transltedOperation={transltedOperation}
    />
  );
}
