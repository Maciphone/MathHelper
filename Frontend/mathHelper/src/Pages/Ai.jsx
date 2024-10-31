import React, { useState } from "react";
import ExercisePageWithStopWatch from "../Components/ExercisePageWithStopWatch";
import TaskSlider from "../Components/TaskSlider";

export default function Ai() {
  const ai = true;
  const [operation, setOperation] = useState("Algebra");
  const [transltedOperation, setTranslatedOperation] = useState("");
  const dictionary = {
    Összeadás: "Algebra",
    Osztás: "Division",
    Szorzás: "Multiplication",
    MaradékOsztás: "RemainDivision",
  };

  const handleOperation = (key) => {
    setOperation(dictionary[key]);
    setTranslatedOperation(key);
  };

  return (
    <div>
      <TaskSlider handleOperation={handleOperation} />
      {operation && (
        <ExercisePageWithStopWatch
          operation={operation}
          transltedOperation={transltedOperation}
          ai={ai}
        />
      )}
    </div>
  );
}
