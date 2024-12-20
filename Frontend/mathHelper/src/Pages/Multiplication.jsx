import React, { useCallback, useEffect, useRef, useState } from "react";
import ExercisePageWithStopWatch from "../Components/ExercisePageWithStopWatch";

export default function Multiplication() {
  const operation = "Multiplication";
  const transltedOperation = "Szorzás";

  return (
    <ExercisePageWithStopWatch
      operation={operation}
      transltedOperation={transltedOperation}
    />
  );
}
