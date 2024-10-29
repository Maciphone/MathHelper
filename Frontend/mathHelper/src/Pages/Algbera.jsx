import React, { useCallback, useEffect, useRef, useState } from "react";
import LevelButtons from "../Components/LevelButtons";
import Stopwatch from "../Components/StopWatch";
import { useSelector } from "react-redux";
import ExercisePageWithStopWatch from "../Components/ExercisePageWithStopWatch";

export default function Algbera() {
  const operation = "Algebra";
  const transltedOperation = "Összeadás";

  return (
    <ExercisePageWithStopWatch
      operation={operation}
      transltedOperation={transltedOperation}
    />
  );
}
