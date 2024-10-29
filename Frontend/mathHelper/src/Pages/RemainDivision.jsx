import React from "react";
import ExercisePage from "../Components/ExercisePage";
import ExercisePageWithStopWatch from "../Components/ExercisePageWithStopWatch";

export default function RemainDivision() {
  const operation = "RemainDivision";
  const transltedOperation = "maradékosztás";

  return (
    <ExercisePageWithStopWatch
      operation={operation}
      transltedOperation={transltedOperation}
    />
  );
}
