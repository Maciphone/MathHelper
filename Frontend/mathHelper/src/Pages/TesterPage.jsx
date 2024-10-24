import React, { useCallback, useEffect, useState } from "react";
import Groq from "groq-sdk";
import testComp from "../Components/TestComp";
import TestComp from "../Components/TestComp";
import ExercisePage from "../Components/ExercisePage";

export default function TesterPage() {
  const operation = "Algebra";
  const transltedOperation = "összeadás";

  return (
    <ExercisePage
      operation={operation}
      transltedOperation={transltedOperation}
    />
  );
}
