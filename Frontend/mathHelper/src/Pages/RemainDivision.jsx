import React from "react";
import ExercisePage from "../Components/ExercisePage";

export default function RemainDivision() {
  const operation = "RemainDivision";
  const transltedOperation = "maradékosztás";

  return (
    <ExercisePage
      operation={operation}
      transltedOperation={transltedOperation}
    />
  );
}
