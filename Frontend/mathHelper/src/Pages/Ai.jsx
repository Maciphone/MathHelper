import React, { useState, useEffect } from "react";
import ExercisePageWithStopWatch from "../Components/ExercisePageWithStopWatch";
import TaskSlider from "../Components/TaskSlider";
import ExercisePageWithStopWatchAI from "../Components/ExercisePageWithStopWatchAI";

export default function Ai() {
  const ai = true;
  const [operation, setOperation] = useState("Algebra");
  const [transltedOperation, setTranslatedOperation] = useState("Összeadás");
  const dictionary = {
    Összeadás: "Algebra",
    Osztás: "Division",
    Szorzás: "Multiplication",
    MaradékOsztás: "RemainDivision",
  };

  useEffect(() => {
    setOperation("Algebra");
    setTranslatedOperation("Összeadás");
  }, []);

  const handleOperation = (key) => {
    setOperation(dictionary[key]);
    setTranslatedOperation(key);
  };

  return (
    <div>
      <TaskSlider handleOperation={handleOperation} />
      {operation && (
        <ExercisePageWithStopWatchAI
          operation={operation}
          transltedOperation={transltedOperation}
          ai={ai}
        />
      )}
    </div>
  );
}
